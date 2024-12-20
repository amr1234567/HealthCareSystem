using HSS.Domain.Helpers;

namespace HSS.Services.Abstractions
{
    public interface IUserIdentityServices
    {
        Task<TokenModel> Login(string nationalId, string password);
    }
}