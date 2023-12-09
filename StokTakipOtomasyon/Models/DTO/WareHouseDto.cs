namespace StokTakipOtomasyon.Models.DTO
{
    public class WareHouseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }

        public List<ProductDto> ProductDtos { get; set; }
    }
}
