using MBADevExpertModulo1.Domain.Models;

namespace MBADevExpertModulo1.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        public Task AddCategoryAsync(Category category);
        public Task UpdateCategoryAsync(Category category);
        public Task RemoveCategoryAsync(Category category);
        public Task<Category> FindCategoryByIdAsync(int id);
        public Task<ICollection<Category>> FindAllCategoriesAsync();
        public Task<ICollection<Category>> FindAllActiveCategoriesAsync();
    }
}
