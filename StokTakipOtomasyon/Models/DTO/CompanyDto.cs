using System.ComponentModel.DataAnnotations;

namespace StokTakipOtomasyon.Models.DTO
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CompanyCode { get; set; }
        public List<WareHouseDto> WareHouseDtos { get; set; }
    }
}
