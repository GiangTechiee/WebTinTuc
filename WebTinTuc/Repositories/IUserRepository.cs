using Microsoft.EntityFrameworkCore.Storage;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public interface IUserRepository
    {
        Task<User> Register(User user);
        Task<User> Login(string loginId, string password);
        Task<User> GetByUsername(string email);
        Task<User> GetByToken(string token);
        Task Update(User user);
        Task DeleteUserAsync(int userId, int defaultUserId);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<bool> IsAdminAsync(int userId);

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetByIdAsync(int userId);
    }
}
