using System.ComponentModel.DataAnnotations;

namespace StokTakipOtomasyon.Models.DTO
{
    public class AddCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
