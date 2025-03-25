using WebTinTuc.Models.DTOs;
using WebTinTuc.Models.Entities;
using WebTinTuc.Repositories;

namespace WebTinTuc.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService; // Service để gửi email
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IEmailService emailService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<User> Register(UserRegisterDto userDto)
        {
            var user = new User
            {
                FullName = userDto.FullName,
                PasswordHash = userDto.Password,
                PhoneNumber = userDto.PhoneNumber,
                Email = userDto.Email,
                DateOfBirth = userDto.DateOfBirth,
                Fk_RoleId = 2,
                CreatedAt = DateTime.Now,
                Avatar = "default-avatar.jpg",
                IsEmailConfirmed = false,
                EmailConfirmationToken = Guid.NewGuid().ToString(), // Tạo token ngẫu nhiên
                TokenExpiration = DateTime.Now.AddHours(24) // Token hết hạn sau 24 giờ
            };

            using (var transaction = await _userRepository.BeginTransactionAsync())
            {
                try
                {
                    var registeredUser = await _userRepository.Register(user);

                    var baseUrl = _configuration["AppSettings:BaseUrl"];
                    var confirmationLink = $"{baseUrl}/api/account/confirm-email?token={registeredUser.EmailConfirmationToken}";
                    await _emailService.SendEmailConfirmation(user.Email, user.FullName, confirmationLink);

                    // Nếu mọi thứ thành công, commit giao dịch
                    await transaction.CommitAsync();
                    return registeredUser;
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi (ví dụ: gửi email thất bại), rollback giao dịch
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Lỗi khi đăng ký: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<User> Login(UserLoginDto userDto)
        {
            var user = await _userRepository.Login(userDto.LoginId, userDto.Password);
            if (!user.IsEmailConfirmed)
            {
                throw new Exception("Tài khoản chưa được xác nhận. Vui lòng kiểm tra email.");
            }
            return user;
        }

        public async Task ConfirmEmail(string token)
        {
            var user = await _userRepository.GetByToken(token);
            if (user == null)
            {
                throw new Exception("Token không hợp lệ.");
            }

            if (user.TokenExpiration < DateTime.Now)
            {
                throw new Exception("Token đã hết hạn.");
            }

            user.IsEmailConfirmed = true;
            user.EmailConfirmationToken = null; // Xóa token sau khi xác nhận
            user.TokenExpiration = null;

            await _userRepository.Update(user);
        }

        public async Task<int> GetOrCreateDefaultUserId()
        {
            var defaultUser = await _userRepository.GetByUsername("defaultuser@system.com");
            if (defaultUser == null)
            {
                defaultUser = new User
                {
                    FullName = "Default User",
                    Email = "defaultuser@system.com",
                    PasswordHash = "defaultpassword", // Sẽ được hash trong Register
                    PhoneNumber = "0999999999", // Giá trị giả, cần thay đổi nếu cần
                    Fk_RoleId = 2,
                    CreatedAt = DateTime.Now,
                    Avatar = "default-avatar.jpg",
                    IsEmailConfirmed = true // Không cần xác nhận email
                };
                await _userRepository.Register(defaultUser);
            }
            return defaultUser.UserId;
        }

        public async Task DeleteUserAsync(int userId)
        {
            var defaultUserId = await GetOrCreateDefaultUserId();
            await _userRepository.DeleteUserAsync(userId, defaultUserId);
        }
    }
}
