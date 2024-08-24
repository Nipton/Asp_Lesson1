namespace Asp_Lesson1.Models.DTO
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public int? ProductGroupId { get; set; }
        public double? Price { get; set; }
    }
}
