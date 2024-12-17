using HSS.Domain.Abstractions;
using HSS.Domain.Enums;
using HSS.Domain.Helpers;
using HSS.Services.Abstractions;
using System.Security.Claims;

namespace HSS.Services.Services
{
    public class UserIdentityServices(IUserIdentityRepository userRepository, ITokenServices tokenServices)
    {
        public async Task<TokenModel> Login(string nationalId, string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(password, nameof(password));
            ArgumentException.ThrowIfNullOrEmpty(nationalId, nameof(nationalId));
            var user = await userRepository.GetIdentityUserByNationalId(nationalId);
            if (user == null)
                throw new Exception("Id or password NOT CORRECT");
            var result = await userRepository.CheckUserPassword(user, password);
            if (result == IdentityCheckPasswordResult.NotFound)
                throw new Exception("Id or password NOT CORRECT");
            if(result == IdentityCheckPasswordResult.AccountHasNoPassword)
            {
                ////TODO 
                return new();
            }
            var claims = new List<Claim>()
            {
                new(CustomClaimType.NationalId, user.NationalId),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            foreach(var role in await userRepository.GetUserRoles(user.Id))
            {
                claims.Add(new(ClaimTypes.Role, role.RoleName.ToString()));
            }

            ////TODO: Add Claims for each type of user

            var token = await tokenServices.GenerateNewTokenModel(user.Id, claims);
            return token;
        }
    }
}
