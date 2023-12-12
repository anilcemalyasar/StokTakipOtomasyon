using System.ComponentModel.DataAnnotations;

namespace StokTakipOtomasyon.Models.DTO
{
    public class UpdateWareHouseRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Warehouse Name can be maximum 100 char length!")]
        public string Name { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }
    }
}
