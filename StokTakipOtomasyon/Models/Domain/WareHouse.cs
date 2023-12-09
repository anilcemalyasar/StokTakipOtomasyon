using System.Text.Json.Serialization;

namespace StokTakipOtomasyon.Models.Domain
{
    public class WareHouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public ICollection<Product> Products { get; } = new List<Product>(); // Collection navigation containing dependents
        [JsonIgnore]
        public Company Company { get; set; } = null!; // Required reference navigation to principal


    }
}
