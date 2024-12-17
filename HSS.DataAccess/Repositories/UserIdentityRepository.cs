using HSS.DataAccess.Contexts;
using HSS.DataAccess.Helpers;
using HSS.Domain.Abstractions;
using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HSS.DataAccess.Repositories
{
    public class UserIdentityRepository
        (ApplicationDbContext context,AccountServicesHelpers accountServices, IUserLogRepository userLogRepository) :
        IUserIdentityRepository
    {
        public Task<IdentityResult> BlockAccount(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> BlockAccount(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> BlockAccount(string userNationalId)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityCheckPasswordResult> CheckUserPassword(IdentityUser user, string password)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
            var userCheck = await GetIdentityUserById(user.Id);
            if (userCheck == null)
                throw new Exception($"Can't find user with id {user.Id}");
            if (string.IsNullOrEmpty(userCheck.Password) || string.IsNullOrEmpty(userCheck.Salt))
            {
                return IdentityCheckPasswordResult.AccountHasNoPassword;
            }
            bool result = ComparePasswords(userCheck.Password, password, userCheck.Salt);
            if (result)
                return IdentityCheckPasswordResult.Available;
            return IdentityCheckPasswordResult.AccountHasNoPassword;
        }
      
        public async Task<IdentityCheckPasswordResult> CheckUserPassword(string userNationalId, string password)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(userNationalId, nameof(userNationalId));
            ArgumentException.ThrowIfNullOrWhiteSpace(password, nameof(password));
            var user = await GetIdentityUserByNationalId(userNationalId);
            if (user == null)
                throw new Exception($"Can't find user with nationalId {userNationalId}");
            if (string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Salt))
            {
                return IdentityCheckPasswordResult.AccountHasNoPassword;
            }
            bool result = ComparePasswords(user.Password, password, user.Salt);
            if (result)
                return IdentityCheckPasswordResult.Available;
            return IdentityCheckPasswordResult.AccountHasNoPassword;
        }

        public async Task<IdentityResult> CreateNewAccount(IdentityUser model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));
            var user = await GetIdentityUserByNationalId(model.NationalId);
            if (user != null)
                throw new Exception($"An Account EXISTS with national id '{model.NationalId}'");
            await context.IdentityUsers.AddAsync(model);
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAccount(IdentityUser user)
        {
            var result = await context.IdentityUsers.Where(u => u.NationalId == user.NationalId).ExecuteDeleteAsync();
            if (result > 0)
                return IdentityResult.Success;
            await context.SaveChangesAsync();
            return IdentityResult.Fail;
        }

        public async Task<IdentityResult> DeleteAccount(int userId)
        {
            var result = await context.IdentityUsers.Where(u => u.Id == userId).ExecuteDeleteAsync();
            if (result > 0)
                return IdentityResult.Success;
            await context.SaveChangesAsync();
            return IdentityResult.Fail;
        }

        public async Task<IdentityResult> DeleteAccount(string userNationalId)
        {
            var result = await context.IdentityUsers.Where(u => u.NationalId == userNationalId).ExecuteDeleteAsync();
            
            if (result > 0)
                return IdentityResult.Success;
            await context.SaveChangesAsync();
            return IdentityResult.Fail;
        }

        public async Task<IdentityUser?> GetIdentityUserByEmail(string email)
        {
            return await context.IdentityUsers.FirstOrDefaultAsync(u => !string.IsNullOrEmpty(u.Email) && u.Email == email);
        }

        public async Task<IdentityUser?> GetIdentityUserById(int id)
        {
            return await context.IdentityUsers.FirstOrDefaultAsync(u =>  u.Id == id);
        }

        public async Task<IdentityUser?> GetIdentityUserByNationalId(string nationalId)
        {
            return await context.IdentityUsers.FirstOrDefaultAsync(u => u.NationalId == nationalId);
        }

        public async Task<IdentityUser?> GetUserByCriteria(Expression<Func<IdentityUser, bool>> value)
        {
            return await context.IdentityUsers.FirstOrDefaultAsync(value);
        }

        public async Task<List<Role>> GetUserRoles(int userId)
        {
            var user = await context.IdentityUsers.Where(u => u.Id == userId)
                .Include(u => u.Roles).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
                throw new Exception($"No user found with this id '{userId}'");
            return user.Roles;
        }

        public async Task<IdentityResult> LogIn(IdentityUser model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));
            ArgumentException.ThrowIfNullOrWhiteSpace(model.RefreshToken, "Refresh token must be given");
            ArgumentNullException.ThrowIfNull(model.ExpirationOfRefreshToken, "Expiration Time must be given");
            var result1 = await UpdateAccount(model);
            if (result1 == IdentityResult.Fail)
                return result1;
            var userLog = await userLogRepository.GetLastOpenUserLog(model.Id);
            if (userLog != null)
            {
                // handle if there is open session still open
            }
            await userLogRepository.GenerateNewUserLog(model.Id);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> Logout(IdentityUser model)
        {
            ArgumentNullException.ThrowIfNull(model, nameof(model));
            var user = model;
            user.RefreshToken = null;
            user.ExpirationOfRefreshToken = null;
            var result1 = await UpdateAccount(user);
            if (result1 == IdentityResult.Fail)
                return result1;

            ////Maybe check first if user has open session or not ??

            await userLogRepository.EndLogByUserId(user.Id);
            context.IdentityUsers.Update(user);
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }
        
        public async Task<IdentityResult> Logout(int userId)
        {
            var user = await GetIdentityUserById(userId);
            if (user == null)
                throw new Exception($"No user with this id '{userId}'");
            var result = await Logout(user);
            return result;
        }

        public async Task<IdentityResult> UpdateAccount(IdentityUser user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            var result = await context.IdentityUsers.Where(u => u.Id == user.Id).ExecuteUpdateAsync(us => us
                        .SetProperty(u => u.Roles, user.Roles)
                        .SetProperty(u => u.RefreshToken, user.RefreshToken)
                        .SetProperty(u => u.ExpirationOfRefreshToken, user.ExpirationOfRefreshToken)
                        .SetProperty(u => u.ContactNumber, user.ContactNumber)
                        .SetProperty(u => u.Name, user.Name)
                        .SetProperty(u => u.Password, user.Password)
                        .SetProperty(u => u.Salt, user.Salt)
                        .SetProperty(u => u.UpdatedAt, DateTime.UtcNow)
                        );
            await context.SaveChangesAsync();
            if (result > 0)
                return IdentityResult.Success;
            return IdentityResult.Fail;
        }

        public async Task<IdentityResult> UpdateAccount(Expression<Func<IdentityUser, bool>> predicate, IdentityUser user)
        {
            var result = await context.IdentityUsers.Where(predicate).ExecuteUpdateAsync(us => us
                        .SetProperty(u => u.Roles, user.Roles)
                        .SetProperty(u => u.RefreshToken, user.RefreshToken)
                        .SetProperty(u => u.ExpirationOfRefreshToken, user.ExpirationOfRefreshToken)
                        .SetProperty(u => u.ContactNumber, user.ContactNumber)
                        .SetProperty(u => u.Name, user.Name)
                        .SetProperty(u => u.Password, user.Password)
                        .SetProperty(u => u.Salt, user.Salt)
                        .SetProperty(u => u.UpdatedAt, DateTime.UtcNow));
            if (result > 0)
                return IdentityResult.Success;
            return IdentityResult.Fail;
        }

        public async Task<IdentityResult> UpdateAllWithFunc(Action<IdentityUser> predicate)
        {
            var users = context.IdentityUsers;
            foreach (var user in users) 
                predicate(user);
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateByCriteriaWithFunc(Expression<Func<IdentityUser, bool>> criteria, Action<IdentityUser> action)
        {
            var users = context.IdentityUsers.Where(criteria);
            foreach (var user in users)
                action(user);
            await context.SaveChangesAsync();
            return IdentityResult.Success;
        }


        private bool ComparePasswords(string originalPassword, string comparablePassword, string salt)
        {
            string hashedGivenPassword = accountServices.HashPasswordWithSalt(comparablePassword, salt);
            return originalPassword.Equals(hashedGivenPassword);
        }

    }
}
