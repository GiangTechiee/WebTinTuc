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
                    Address = user.Address,
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
                return View("ConfirmEmailSuccess", "Email đã được xác nhận thành công! Bạn có thể đăng nhập ngay bây giờ.");
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
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userService.GetByIdAsync(userId);

            ViewBag.FullName = user.FullName;
            ViewBag.Role = user.Role?.RoleName ?? "User";
            ViewBag.Avatar = user.FullAvatarPath;
            ViewBag.Email = user.Email;
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.DateOfBirth = user.DateOfBirth?.ToString("dd/MM/yyyy");
            ViewBag.Address = user.Address;
            ViewBag.CreatedAt = user.CreatedAt.ToString("dd/MM/yyyy HH:mm");

            return View();
        }

        [Authorize]
        [HttpPost("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UserUpdateDto userDto, IFormFile avatar)
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = await _userService.GetByIdAsync(userId);

                // Xử lý upload ảnh đại diện nếu có
                if (avatar != null && avatar.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + avatar.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await avatar.CopyToAsync(fileStream);
                    }
                    userDto.Avatar = uniqueFileName; // Cập nhật đường dẫn ảnh mới
                }

                // Cập nhật thông tin người dùng
                await _userService.UpdateUserAsync(userId, userDto);

                // Cập nhật Claims nếu cần (FullName, Avatar)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, userDto.FullName ?? user.FullName),
                    new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User"),
                    new Claim("Avatar", userDto.Avatar != null ? $"/images/{userDto.Avatar}" : user.FullAvatarPath)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Ok(new { message = "Cập nhật thông tin thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}