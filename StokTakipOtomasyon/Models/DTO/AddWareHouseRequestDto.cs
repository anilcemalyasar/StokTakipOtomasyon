using System.ComponentModel.DataAnnotations;

namespace StokTakipOtomasyon.Models.DTO
{
    public class AddWareHouseRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
