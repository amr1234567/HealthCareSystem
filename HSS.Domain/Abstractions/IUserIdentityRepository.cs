using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
using HSS.Domain.Models;
using System.Linq.Expressions;

namespace HSS.Domain.Abstractions
{
    public interface IUserIdentityRepository
    {
        Task<IdentityUser?> GetIdentityUserById(int id);
        Task<IdentityUser?> GetIdentityUserByEmail(string email);
        Task<IdentityUser?> GetIdentityUserByNationalId(string nationalId);
        Task<IdentityUser?> GetUserByCriteria(Expression<Func<IdentityUser, bool>> value);

        Task<List<Role>> GetUserRoles(int userId);

        Task<IdentityCheckPasswordResult> CheckUserPassword(IdentityUser user, string password);
        Task<IdentityCheckPasswordResult> CheckUserPassword(string userNationalId, string password);

        Task<IdentityResult> CreateNewAccount(IdentityUser user);
        Task<IdentityResult> AssignUserToRole(IdentityUser user, params string[] roleNames);

        Task<IdentityResult> UpdateAccount(IdentityUser user);
        Task<IdentityResult> UpdateAccount(Expression<Func<IdentityUser, bool>> predicate, IdentityUser user);
        Task<IdentityResult> LogIn(IdentityUser model);
        Task<IdentityResult> Logout(IdentityUser model);
        Task<IdentityResult> Logout(int userId);

        Task<IdentityResult> DeleteAccount(IdentityUser user);
        Task<IdentityResult> DeleteAccount(int userId);
        Task<IdentityResult> DeleteAccount(string userNationalId);

        Task<IdentityResult> BlockAccount(IdentityUser user);
        Task<IdentityResult> BlockAccount(int userId);
        Task<IdentityResult> BlockAccount(string userNationalId);


        Task<IdentityResult> UpdateAllWithFunc(Action<IdentityUser> value);
        Task<IdentityResult> UpdateByCriteriaWithFunc(Expression<Func<IdentityUser, bool>> criteria, Action<IdentityUser> action);
        Task<IdentityResult> AssignUserToRole(IdentityUser user, params ApplicationRole[] roleNames);
    }
}
