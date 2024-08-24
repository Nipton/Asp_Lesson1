using System.ComponentModel.DataAnnotations;

namespace Asp_Lesson1.Models.DTO
{
    public class ProductGroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
    }
}
