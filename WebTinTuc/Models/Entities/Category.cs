using System.ComponentModel.DataAnnotations;

namespace WebTinTuc.Models.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MaxLength(50)]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public ICollection<News> News { get; set; } = new List<News>();
    }
}
