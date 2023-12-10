using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace StokTakipOtomasyon.Models.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }

        [JsonIgnore]
        public WareHouse WareHouse { get; set; }// Required reference navigation to principal
    }
}
