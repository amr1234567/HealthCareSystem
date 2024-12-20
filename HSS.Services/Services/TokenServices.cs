
using HSS.Domain.Abstractions;
using HSS.Domain.Helpers;
using HSS.Services.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HSS.Services.Services
{
    public class TokenServices(
        IOptions<JwtHelper> jwtOptions,
        IUserIdentityRepository userRepository) : ITokenServices
    {
        private readonly JwtHelper _jwtHelper = jwtOptions.Value;

        public async Task<TokenModel> GenerateNewTokenModel(int userId, IEnumerable<Claim>? claims = null)
        {
            var refreshToken = await GenerateNewRefreshToken();
            var token = await GenerateNewToken(claims);
            var user = await userRepository.GetIdentityUserById(userId);
            if (user == null)
                throw new Exception($"no user with this id '{userId}'");
            await userRepository.LogIn(user);
            return new TokenModel
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpireDate = DateTime.Now.AddMinutes(_jwtHelper.JwtExpireMinutes),
            };
        }

        public async Task<bool> RevokeAllTokens()
        {
            await userRepository.UpdateAllWithFunc(u =>
            {
                u.RefreshToken = null;
                u.ExpirationOfRefreshToken = null;
            });
            return true;
        }

        public async Task<bool> RevokeToken(string refreshToken)
        {
            await userRepository.UpdateByCriteriaWithFunc(criteria: u => u.RefreshToken == refreshToken,
                action: u =>
                {
                    u.RefreshToken = null;
                    u.ExpirationOfRefreshToken = null;
                });
            return true;
        }

        public async Task<TokenModel> RefreshTheToken(string refreshToken, string token)
        {
            var user = await userRepository.GetUserByCriteria(u => u.RefreshToken == refreshToken);
            if (user is null || user.ExpirationOfRefreshToken < DateTime.UtcNow)
                throw new SecurityTokenExpiredException(token);
            var principal = GetPrincipalFromExpiredToken(token);

            var idClaim = principal.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));

            if (idClaim is null || idClaim.Value != user.Id.ToString())
                throw new Exception("Token Not Valid");
            return new TokenModel
            {
                ExpireDate = DateTime.Now.AddMinutes(_jwtHelper.JwtExpireMinutes),
                RefreshToken = refreshToken,
                Token = GenerateTokenFromPrincipal(principal),
            };
        }

        public async Task<bool> RevokeTokenWithUserId(int userId)
        {

            await userRepository.Logout(userId);
            return true;
        }

        private Task<string> GenerateNewRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Task.FromResult(Convert.ToBase64String(randomNumber));
        }

        private Task<string> GenerateNewToken(IEnumerable<Claim>? claims)
        {
            var customClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.PrimarySid, Guid.NewGuid().ToString()),
            };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtHelper.JwtKey));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
            var tokeOptions = new JwtSecurityToken(
                claims: customClaims.Union(claims ?? []),
                issuer: _jwtHelper.JwtIssuer,
                audience: _jwtHelper.JwtAudience,
                expires: DateTime.Now.AddMinutes(_jwtHelper.JwtExpireMinutes),
                signingCredentials: signInCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Task.FromResult(tokenString);
        }

        private string GenerateTokenFromPrincipal(ClaimsPrincipal principal)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtHelper.JwtKey);

            // Extract claims from the principal
            var claims = new List<Claim>(principal.Claims);

            // Define the token descriptor with claims, expiration, signing key, etc.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtHelper.JwtExpireMinutes),
                Issuer = _jwtHelper.JwtIssuer,
                Audience = _jwtHelper.JwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the serialized token
            return tokenHandler.WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(token))
            {
                throw new Exception("Not Valid Token Signature");
            }
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtHelper.JwtIssuer,
                ValidateAudience = true,
                ValidAudience = _jwtHelper.JwtAudience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtHelper.JwtKey)),
                ValidateLifetime = true, // Ensure token is not expired
                ClockSkew = TimeSpan.Zero // No tolerance for expiration
            };
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
