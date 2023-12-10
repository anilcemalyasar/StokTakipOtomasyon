using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StokTakipOtomasyon.Models.Domain
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string CompanyCode { get; set; }

        [JsonIgnore]
        public ICollection<WareHouse> WareHouses { get; set; }// Collection navigation containing dependents

    }
}
