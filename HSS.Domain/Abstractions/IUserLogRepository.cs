using HSS.Domain.Models;

namespace HSS.Domain.Abstractions
{
    public interface IUserLogRepository
    {
        Task<UserLog> EndLog(int userLogId, string? notes = null);
        Task<UserLog> EndLogByUserId(int userId, string? notes = null);
        Task<UserLog> GenerateNewUserLog(int userId, string? notes = null);
        Task<UserLog?> GetLastUserLog(int userId);
        Task<UserLog?> GetLastOpenUserLog(int userId);
        Task<List<UserLog>> GetUserLogs(int id);
    }
}