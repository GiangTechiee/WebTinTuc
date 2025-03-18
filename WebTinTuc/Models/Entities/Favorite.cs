using System.ComponentModel.DataAnnotations.Schema;

namespace WebTinTuc.Models.Entities
{
    public class Favorite
    {
        [ForeignKey("User")]
        public int Fk_UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("News")]
        public int Fk_NewId { get; set; }
        public News News { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
