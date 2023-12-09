namespace StokTakipOtomasyon.Models.DTO
{
    public class AddProductRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }

        public int WarehouseId { get; set; }
    }
}
