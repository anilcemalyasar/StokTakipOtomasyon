using Microsoft.EntityFrameworkCore;
using StokTakipOtomasyon.Data;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Repositories.Abstracts;

namespace StokTakipOtomasyon.Repositories.Concretes
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dbContext;

        public CategoryRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> CreateAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }


        public async Task<Category?> DeleteByIdAsync(int id)
        {
            // Find domain model with given id
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory is null)
            {
                return null;
            }

            _dbContext.Categories.Remove(existingCategory);
            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }


        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(int id, Category category)
        {
            // Find domain model with given id
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory is null)
            {
                return null;
            }

            existingCategory.Name = category.Name;
            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
