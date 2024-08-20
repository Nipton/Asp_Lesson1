using System.ComponentModel.DataAnnotations;

namespace Asp_Lesson1.Models
{
    public class ProductGroup
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}