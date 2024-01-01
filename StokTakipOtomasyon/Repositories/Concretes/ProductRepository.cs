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

            var company = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id ==  request.CategoryId);

            if (company == null)
            {
                return null;
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                StockAmount = request.StockAmount,
                Price = request.Price,
                WareHouse = warehouse,
                Category = company
            };

            await _dbContext.Products.AddAsync(product);
            warehouse.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductByIdAsync(int id)
        {
            var existingProduct = await _dbContext.Products
                                    .Include(p => p.WareHouse)
                                    .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct is null)
            {
                return null;
            }

            _dbContext.Products.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products.Include("WareHouse").FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
                                                    string? sortBy = null, bool isAscending = true,
                                                    int pageNumber = 1, int pageSize = 5)
        {
            // Get IQueryable for Filtering, Sorting and Paging
            var products = _dbContext.Products.Include("WareHouse").AsQueryable();

            // Filtering
            if (String.IsNullOrWhiteSpace(filterOn) == false && String.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(p => p.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(p => p.Description.Contains(filterQuery));
                }
            }

            // Sorting
            if (String.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAscending ? products.OrderBy(p => p.Price) : products.OrderByDescending(p => p.Price);
                }
                else if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAscending ? products.OrderBy(p => p.Name) : products.OrderByDescending(p => p.Name);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await products.Skip(skipResults).Take(pageSize).ToListAsync();
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

        public async Task<Product?> UpdateStockAsync(int id, int quantity)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct is null)
            {
                return null;
            }

            // Update Stock Amount
            existingProduct.StockAmount += quantity;

            // Save all changes asynchronous
            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }
    }
}
