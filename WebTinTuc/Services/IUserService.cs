using WebTinTuc.Models.DTOs;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDto userDto);
        Task<User> Login(UserLoginDto userDto);
        Task<int> GetOrCreateDefaultUserId(); // Lấy hoặc tạo user mặc định
        Task DeleteUserAsync(int userId);
        Task ConfirmEmail(string token);
    }
}
