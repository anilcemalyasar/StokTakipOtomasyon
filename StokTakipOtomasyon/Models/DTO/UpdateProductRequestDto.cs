using System.ComponentModel.DataAnnotations;

namespace StokTakipOtomasyon.Models.DTO
{
    public class UpdateProductRequestDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int StockAmount { get; set; }
    }
}
