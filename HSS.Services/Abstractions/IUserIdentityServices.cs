using HSS.Domain.BaseModels;
using HSS.Domain.Helpers;
using HSS.Domain.Models;

namespace HSS.Services.Abstractions
{
    public interface IUserIdentityServices<T> where T : IdentityUser
    {
        Task<List<Role>> LoginWithCookie(string nationalId, string password, bool isPersistent = false);
        Task<TokenModel> LoginWithJwt(string nationalId, string password);
        Task LogoutInApi(int userId);
        Task LogOutInMvc();
    }
}