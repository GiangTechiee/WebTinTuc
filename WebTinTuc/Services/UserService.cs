using System.Net.Http;
using System.Text.Json;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IUserRepository userRepository, IEmailService emailService, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        // Phương thức lấy URL ngrok động
        private async Task<string> GetNgrokPublicUrl()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetStringAsync("http://localhost:4040/api/tunnels");
                using var doc = JsonDocument.Parse(response);
                var tunnels = doc.RootElement.GetProperty("tunnels");
                var publicUrl = tunnels.EnumerateArray().First().GetProperty("public_url").GetString();
                return publicUrl ?? _configuration["AppSettings:BaseUrl"]; // Fallback nếu không lấy được
            }
            catch
            {
                return _configuration["AppSettings:BaseUrl"]; // Dùng BaseUrl mặc định nếu lỗi
            }
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
                Address = userDto.Address,
                Avatar = "default-avatar.jpg",
                IsEmailConfirmed = false,
                EmailConfirmationToken = Guid.NewGuid().ToString(), // Tạo token ngẫu nhiên
                TokenExpiration = DateTime.Now.AddMinutes(15) // Token hết hạn sau 24 giờ
            };

            using (var transaction = await _userRepository.BeginTransactionAsync())
            {
                try
                {
                    var registeredUser = await _userRepository.Register(user);

                    var baseUrl = await GetNgrokPublicUrl(); /*_configuration["AppSettings:BaseUrl"];*/
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

        public async Task<int> GetDefaultUserId()
        {
            var defaultUser = await _userRepository.GetByUsername("defaultuser@system.com");
            if (defaultUser == null)
            {
                throw new Exception("Tài khoản mặc định không tồn tại trong hệ thống.");
            }
            return defaultUser.UserId;
        }

        public async Task<User> CreateUserAsync(UserRegisterDto userDto)
        {
            var user = new User
            {
                FullName = userDto.FullName,
                PasswordHash = userDto.Password,
                PhoneNumber = userDto.PhoneNumber,
                Email = userDto.Email,
                DateOfBirth = userDto.DateOfBirth,
                Fk_RoleId = userDto.RoleId, 
                CreatedAt = DateTime.Now,
                Address = userDto.Address,
                Avatar = "default-avatar.jpg",
                IsEmailConfirmed = true
            };
            return await _userRepository.Register(user);
        }

        public async Task UpdateUserAsync(int userId, UserUpdateDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("Không tìm thấy người dùng.");

            user.FullName = userDto.FullName ?? user.FullName;
            user.PhoneNumber = userDto.PhoneNumber ?? user.PhoneNumber;
            user.Email = userDto.Email ?? user.Email;
            
            user.DateOfBirth = userDto.DateOfBirth ?? user.DateOfBirth;
            user.Address = userDto.Address ?? user.Address;
            user.Fk_RoleId = userDto.RoleId ?? user.Fk_RoleId;
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.PasswordHash = userDto.Password;
            }
            user.Avatar = userDto.Avatar ?? user.Avatar;

            await _userRepository.Update(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var defaultUserId = await GetDefaultUserId();
            await _userRepository.DeleteUserAsync(userId, defaultUserId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Không tìm thấy người dùng.");
            }
            return user;
        }
    }
}
