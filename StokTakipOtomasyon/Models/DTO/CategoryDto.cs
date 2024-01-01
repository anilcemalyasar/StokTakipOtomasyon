using System.ComponentModel.DataAnnotations;

namespace StokTakipOtomasyon.Models.DTO
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
