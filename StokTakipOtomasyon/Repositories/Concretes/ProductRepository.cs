using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StokTakipOtomasyon.Data;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;
using StokTakipOtomasyon.Repositories.Abstracts;

namespace StokTakipOtomasyon.Repositories.Concretes
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dbContext;
        public ProductRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product?> AddProductAsync(AddProductRequestDto request)
        {
            var warehouse = await _dbContext.WareHouses
                .Include(w => w.Products)
                .Include(w => w.Company)
                .FirstOrDefaultAsync(w => w.Id == request.WarehouseId);

            if (warehouse == null)
            {
                return null;
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                StockAmount = request.StockAmount,
                Price = request.Price,
                WareHouse = warehouse
            };

            warehouse.Products.Add(product);
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public Task<Product?> DeleteProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Include("WareHouse").FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.Include("WareHouse").ToListAsync();
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProduct is null)
            {
                return null;
            }

            // Update properties of existing Product
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockAmount = product.StockAmount;

            // Save all changes asynchronous
            await _dbContext.SaveChangesAsync();
            return existingProduct;

        }
    }
}
