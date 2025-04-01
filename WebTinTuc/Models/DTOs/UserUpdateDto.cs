using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Models.DTOs
{
    public class UserUpdateDto
    {
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ và tên phải từ 2 đến 50 ký tự")]
        public string? FullName { get; set; }

        [MaxLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [MaxLength(12)]
        [RegularExpression(@"^(?:\+84|0)(?:3[2-9]|5[6|8|9]|7[0|6-9]|8[1-6|8|9]|9[0-4|6-9])[0-9]{7}$",
            ErrorMessage = "Số điện thoại phải là số Việt Nam.")]
        public string? PhoneNumber { get; set; }

        [StringLength(255, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có tối thiểu 8 ký tự")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$",
            ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public int? RoleId { get; set; }
    }
}
