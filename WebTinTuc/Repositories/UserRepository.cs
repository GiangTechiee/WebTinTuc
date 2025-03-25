using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebTinTuc.Data;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebTinTucContext _context;

        public UserRepository(WebTinTucContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new Exception("Email đã tồn tại");

            if (await _context.Users.AnyAsync(u => u.PhoneNumber == user.PhoneNumber))
                throw new Exception("Số điện thoại đã tồn tại");

            // Hash password trước khi lưu
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.Now;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Login(string loginId, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == loginId || u.PhoneNumber == loginId);

            if (user == null)
            {
                throw new Exception("Email hoặc số điện thoại không tồn tại");
            }

            // Kiểm tra nếu tài khoản bị khóa
            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.Now)
            {
                var remainingTime = (user.LockoutEnd.Value - DateTime.Now).TotalMinutes;
                throw new Exception($"Tài khoản của bạn đã bị khóa. Vui lòng thử lại sau {Math.Ceiling(remainingTime)} phút.");
            }

            // check MK
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                user.FailedLoginAttempts += 1;

                if (user.FailedLoginAttempts >= 5)
                {
                    user.LockoutEnd = DateTime.Now.AddMinutes(30); // Khóa trong 30 phút
                    user.FailedLoginAttempts = 0; // Reset lại số lần thử sau khi khóa
                    await _context.SaveChangesAsync();
                    throw new Exception("Bạn đã nhập sai mật khẩu quá 5 lần. Tài khoản của bạn bị khóa trong 30 phút.");
                }

                await _context.SaveChangesAsync();
                throw new Exception("Tài khoản hoặc mật khẩu không đúng");
            }

            // Đăng nhập thành công, reset số lần thử sai
            if (user.FailedLoginAttempts > 0)
            {
                user.FailedLoginAttempts = 0;
                user.LockoutEnd = null; // Xóa trạng thái khóa nếu có
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == username);
        }

        public async Task<User> GetByToken(string token)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId, int defaultUserId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user == null)
                        throw new Exception("Không tìm thấy người dùng.");

                    // Cập nhật News sang user mặc định
                    var newsToUpdate = await _context.News
                        .Where(n => n.Fk_UserId == userId)
                        .ToListAsync();

                    foreach (var news in newsToUpdate)
                    {
                        news.Fk_UserId = defaultUserId;
                    }

                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<bool> IsAdminAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Role) 
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user != null && user.Role != null && user.Role.RoleId == 1; // 1 là role của admin
        }
    }
}
