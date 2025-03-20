using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Models.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required, MaxLength(1000)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsApprove { get; set; }

        [ForeignKey("User")]
        public int Fk_UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("News")]
        public int Fk_NewId { get; set; }
        public News News { get; set; }

        public int? ParentId { get; set; } // FK đến CommentId

        [ForeignKey("ParentId")]
        public Comment ParentComment { get; set; }

        [InverseProperty("ParentComment")]
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}
