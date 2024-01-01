using StokTakipOtomasyon.Models.Domain;

namespace StokTakipOtomasyon.Repositories.Abstracts
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category?> CreateAsync(Category category);
        Task<Category?> UpdateAsync(int id, Category category);
        Task<Category?> DeleteByIdAsync(int id);

    }
}
