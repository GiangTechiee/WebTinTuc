using WebTinTuc.Models.DTOs;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDto userDto);
        Task<User> Login(UserLoginDto userDto);
        Task<int> GetDefaultUserId(); 
        Task DeleteUserAsync(int userId);
        Task ConfirmEmail(string token);

        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(UserRegisterDto userDto); 
        Task UpdateUserAsync(int userId, UserUpdateDto userDto);
        Task<User> GetByIdAsync(int userId);
    }
}
