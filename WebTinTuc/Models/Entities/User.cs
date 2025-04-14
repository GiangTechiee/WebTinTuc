using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ và tên phải từ 2 đến 50 ký tự")]
        public string FullName { get; set; }


        public DateTime? DateOfBirth { get; set; }


        [Required, MaxLength(12)]
        [RegularExpression(@"^(?:\+84|0)(?:3[2-9]|5[2|6|8|9]|7[0|6-9]|8[1-6|8|9]|9[0-4|6-9])[0-9]{7}$",
            ErrorMessage = "Số điện thoại phải là số Việt Nam.")]
        public string PhoneNumber { get; set; }


        public string Avatar { get; set; }
        public string FullAvatarPath => $"/images/{Avatar ?? "default-avatar.jpg"}";


        [Required(ErrorMessage = "Email là bắt buộc")]
        [MaxLength(255, ErrorMessage = "Email không được vượt quá 255 ký tự")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có tối thiểu 8 ký tự")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z\d]).{8,}$",
            ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string PasswordHash { get; set; }


        [ForeignKey("Role")]
        public int Fk_RoleId { get; set; }


        public DateTime CreatedAt { get; set; }


        public string? Address { get; set; }


        public bool IsEmailConfirmed { get; set; } = false;


        public string? EmailConfirmationToken { get; set; } // Token xác nhận email


        public DateTime? TokenExpiration { get; set; } // Thời gian hết hạn của token


        public int FailedLoginAttempts { get; set; } = 0; // Số lần đăng nhập thất bại


        public DateTime? LockoutEnd { get; set; } // Thời gian mở khóa acc


        public Role Role { get; set; }


        public ICollection<News> News { get; set; } = new List<News>();


        public ICollection<Comment> Comments { get; set; } = new List<Comment>();


        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
