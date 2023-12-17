using System.ComponentModel.DataAnnotations;

namespace StokTakipOtomasyon.Models.DTO
{
    public class UpdateCompanyRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string CompanyCode { get; set; }
    }
}
