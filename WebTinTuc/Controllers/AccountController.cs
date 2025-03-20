using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebTinTuc.Models.DTOs;
using WebTinTuc.Services;

namespace WebTinTuc.Controllers
{
    [Route("api/[controller]")]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            try
            {
                var user = await _userService.Register(userDto);
                return Ok(new
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FullName = user.FullName,
                    Avatar = user.Avatar,
                    Message = "Đăng ký thành công! Vui lòng kiểm tra email."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
        {
            try
            {
                var user = await _userService.Login(userDto);

                string roleName = user.Role?.RoleName ?? "User";

                // lưu phiên
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, roleName),
                    new Claim("Avatar", user.FullAvatarPath)
                };

                // Tạo identity và principal
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Đăng nhập người dùng bằng cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Ok(new
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Role = userDto.Role, // Trả về RoleName
                    Avatar = user.FullAvatarPath,
                    Message = "Đăng nhập thành công!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            try
            {
                await _userService.ConfirmEmail(token);
                return Ok(new { message = "Email đã được xác nhận thành công! Bạn có thể đăng nhập ngay bây giờ." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            ViewBag.FullName = User.Identity.Name;
            ViewBag.Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            ViewBag.Avatar = User.Claims.FirstOrDefault(c => c.Type == "Avatar")?.Value;
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AdminDashboard")]
        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
