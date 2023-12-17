using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;

namespace StokTakipOtomasyon.Repositories.Abstracts
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
                                        string? sortBy = null, bool isAscending = true, 
                                        int pageNumber = 1, int pageSize = 5);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> AddProductAsync(AddProductRequestDto request);
        Task<Product?> UpdateProductAsync(int id, Product product);
        Task<Product?> DeleteProductByIdAsync(int id);
        Task<Product?> UpdateStockAsync(int id, int quantity);

    }
}
