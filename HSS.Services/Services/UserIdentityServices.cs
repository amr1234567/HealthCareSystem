using HSS.Domain.Abstractions;
using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
using HSS.Domain.Helpers;
using HSS.Domain.Models;
using HSS.Services.Abstractions;
using System.Linq;
using System.Security.Claims;

namespace HSS.Services.Services
{
    public class UserIdentityServices<T>
        (IUserIdentityRepository userRepository, ITokenServices tokenServices, CustomSignInManager signInManager)
        : IUserIdentityServices<T> where T : IdentityUser
    {
        public async Task<TokenModel> LoginWithJwt(string nationalId, string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(password, nameof(password));
            ArgumentException.ThrowIfNullOrEmpty(nationalId, nameof(nationalId));
            var user = await userRepository.GetIdentityUserByNationalId(nationalId);
            if (user == null)
                throw new Exception("Id or password NOT CORRECT");
            var result = await userRepository.CheckUserPassword(user, password);
            if (result == IdentityCheckPasswordResult.NotFound)
                throw new Exception("Id or password NOT CORRECT");
            if (result == IdentityCheckPasswordResult.AccountHasNoPassword)
            {
                ////TODO 
                return new();
            }

            var roles = await userRepository.GetUserRoles(user.Id);
            if (roles.Count == 0)
                throw new Exception("User has no role");

            var claims = new List<Claim>()
            {
                new(CustomClaimType.NationalId, user.NationalId),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.MobilePhone, user.ContactNumber),
            };

            foreach (var role in roles)
            {
                claims.Add(new(ClaimTypes.Role, role.RoleName.ToString()));
            }

            ////TODO: Add Claims for each type of user

            var token = await tokenServices.GenerateNewTokenModel(user.Id, claims);
            await userRepository.UpdateByCriteriaWithFunc(x => x.Id == user.Id, u =>
            {
                u.ExpirationOfRefreshToken = token.ExpireDate;
                u.RefreshToken = token.RefreshToken;
            });
            return token;
        }

        public async Task LoginWithCookie(string nationalId, string password, bool isPersistent = false)
        {
            ArgumentException.ThrowIfNullOrEmpty(password, nameof(password));
            ArgumentException.ThrowIfNullOrEmpty(nationalId, nameof(nationalId));
            var user = await userRepository.GetIdentityUserByNationalId(nationalId);
            if (user == null)
                throw new Exception("Id or password NOT CORRECT");
            var result = await userRepository.CheckUserPassword(user.NationalId, password);
            if (result == IdentityCheckPasswordResult.NotFound)
                throw new Exception("Id or password NOT CORRECT");
            if (result == IdentityCheckPasswordResult.AccountHasNoPassword)
            {
                ////TODO 
                return;
            }

            var roles = await userRepository.GetUserRoles(user.Id);
            if (roles.Count == 0)
                throw new Exception("User has no role");

            var claims = GetUserClaims(user, roles);

            await signInManager.SignInAsync(user, isPersistent, claims);
        }

        public async Task LogOutInMvc()
        {
            await signInManager.SignOutAsync();
        }

        public async Task LogoutInApi(int userId)
        {
            await userRepository.UpdateByCriteriaWithFunc(x => x.Id == userId, u =>
            {
                u.ExpirationOfRefreshToken = null;
                u.RefreshToken = null;
            });
        }

        private List<Claim> GetUserClaims(IdentityUser user, List<Role> roles)
        {
            var claims = new List<Claim>
                {
                    new(CustomClaimType.NationalId, user.NationalId),
                    new(ClaimTypes.Name, user.Name),
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email ?? ""),
                    new(ClaimTypes.MobilePhone, user.ContactNumber ?? "")
                };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.RoleName.ToString())));
            return claims;
        }

    }
}
