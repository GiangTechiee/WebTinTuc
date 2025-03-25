using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTinTuc.Models.Entities
{
    public class News
    {
        [Key]
        public int NewId { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Tóm tắt là bắt buộc")]
        [MaxLength(500, ErrorMessage = "Tóm tắt không được vượt quá 500 ký tự")]
        public string Abstract { get; set; }


        [Required(ErrorMessage = "Nội dung là bắt buộc")]
        public string Content { get; set; }

        public DateTime PostedDate { get; set; }
        public int ViewCount { get; set; }

        public bool IsApprove { get; set; }

        public string? Image { get; set; }

        public string FullImagePath => $"/images/{Image}";


        [ForeignKey("User")]
        public int Fk_UserId { get; set; }

        public User? User { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
