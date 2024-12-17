using HSS.DataAccess.Contexts;
using HSS.Domain.Abstractions;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HSS.DataAccess.Repositories
{
    public class UserLogRepository(ApplicationDbContext context) : IUserLogRepository
    {
        public async Task<UserLog> GenerateNewUserLog(int userId, string? notes = null)
        {
            var userLog = new UserLog()
            {
                UserId = userId,
                IsLoggedIn = true,
                LoginTime = DateTime.UtcNow,
                Notes = notes
            };
            await context.UserLogs.AddAsync(userLog);
            await context.SaveChangesAsync();
            return userLog;
        }

        public async Task<UserLog> EndLog(int userLogId, string? notes = null)
        {
            var log = await context.UserLogs.FirstOrDefaultAsync(l => l.Id == userLogId);
            if (log == null)
                throw new Exception($"No logs with this id '{userLogId}'");
            log.Notes = notes;
            log.LogoutTime = DateTime.UtcNow;
            log.IsLoggedIn = false;
            await context.SaveChangesAsync();
            return log;
        }
        public async Task<UserLog> EndLogByUserId(int userId, string? notes = null)
        {
            var log = await GetLastUserLog(userId);
            if (log == null)
                throw new Exception("No logs for this user");
            log.Notes = notes;
            log.LogoutTime = DateTime.UtcNow;
            log.IsLoggedIn = false;
            context.UserLogs.Update(log);
            await context.SaveChangesAsync();
            return log;
        }

        public async Task<UserLog?> GetLastUserLog(int userId)
        {
            return await context.UserLogs.AsNoTracking().Where(u => u.UserId == userId)
                .OrderByDescending(u => u.LoginTime).FirstOrDefaultAsync();
        }

        public async Task<List<UserLog>> GetUserLogs(int id)
        {
            return await context.UserLogs.AsNoTracking().Where(l => l.UserId == id).ToListAsync();
        }

        public async Task<UserLog?> GetLastOpenUserLog(int userId)
        {
            return await context.UserLogs.AsNoTracking().Where(u => u.UserId == userId && u.IsLoggedIn)
                .OrderByDescending(u => u.LoginTime).FirstOrDefaultAsync();
        }
    }
}
