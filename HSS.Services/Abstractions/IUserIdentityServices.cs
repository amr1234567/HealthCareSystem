using HSS.Domain.BaseModels;
using HSS.Domain.Helpers;

namespace HSS.Services.Abstractions
{
    public interface IUserIdentityServices<T> where T : IdentityUser
    {
        Task LoginWithCookie(string nationalId, string password, bool isPersistent = false);
        Task<TokenModel> LoginWithJwt(string nationalId, string password);
        Task LogoutInApi(int userId);
        Task LogOutInMvc();
    }
}