using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Models.Entities
{
    public class News
    {
        [Key]
        public int NewId { get; set; }

        [Required]
        public string Title { get; set; }

        [MaxLength(500)]
        public string Abstract { get; set; }

        public string Content { get; set; }

        public DateTime PostedDate { get; set; }
        public int ViewCount { get; set; }

        public bool IsApprove { get; set; }

        public string Image { get; set; }

        public string FullImagePath => $"/images/{Image}";

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
