namespace StokTakipOtomasyon.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }

        public WareHouseDto WareHouse { get; set; }
    }
}
