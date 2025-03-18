using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Models.DTOs
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Email hoặc số điện thoại là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string LoginId { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
