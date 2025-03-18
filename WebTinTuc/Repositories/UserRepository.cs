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

            // check MK
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new Exception("Tài khoản hoặc mật khẩu không đúng");
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
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
