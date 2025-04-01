using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Models.DTOs;
using WebTinTuc.Repositories;
using WebTinTuc.Services;

namespace WebTinTuc.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("manage-users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserRegisterDto userDto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(userDto);
                return Ok(new { message = "Thêm người dùng thành công", userId = user.UserId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi thêm người dùng: {ex.Message}" });
            }
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto userDto)
        {
            try
            {
                await _userService.UpdateUserAsync(id, userDto);
                return Ok(new { message = "Cập nhật người dùng thành công" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi cập nhật người dùng: {ex.Message}" });
            }
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok(new { message = "Xóa người dùng thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi khi xóa người dùng: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "Không tìm thấy người dùng" });
                }
                return Ok(new
                {
                    userId = user.UserId,
                    fullName = user.FullName,
                    email = user.Email,
                    phoneNumber = user.PhoneNumber,
                    dateOfBirth = user.DateOfBirth?.ToString("yyyy-MM-dd"),
                    address = user.Address,
                    fk_RoleId = user.Fk_RoleId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Lỗi khi lấy thông tin người dùng: {ex.Message}" });
            }
        }
    }
}
