using System.ComponentModel.DataAnnotations;

namespace StokTakipOtomasyon.Models.DTO
{
    public class UpdateCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
