using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTinTuc.Services;

namespace WebTinTuc.Controllers
{
    [Route("api/[controller]")]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("delete")]
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
    }
}
