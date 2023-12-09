using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;

namespace StokTakipOtomasyon.Repositories.Abstracts
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> AddProductAsync(AddProductRequestDto request);
        Task<Product?> UpdateProductAsync(int id, Product product);
        Task<Product?> DeleteProductByIdAsync(int id);

    }
}
