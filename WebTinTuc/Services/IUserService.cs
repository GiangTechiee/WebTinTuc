using WebTinTuc.Models.DTOs;
using WebTinTuc.Models.Entities;

namespace WebTinTuc.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDto userDto);
        Task<User> Login(UserLoginDto userDto);
        Task ConfirmEmail(string token);
    }
}
