using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Asp_Lesson1.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public double? Price { get; set; }
        public virtual ProductGroup? ProductGroup { get; set; }
        public int? ProductGroupId { get; set; }
        public virtual ICollection<Store> Stores { get; set; } = new List<Store>();

    }
}
