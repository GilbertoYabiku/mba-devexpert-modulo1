using MBADevExpertModulo1.Core.Models;

namespace MBADevExpertModulo1.Core.Interfaces
{
    public interface ICategoryRepository
    {
        public Task AddCategoryAsync(Category category);
        public Task UpdateCategoryAsync(Category category);
        public Task RemoveCategoryAsync(Category category);
        public Task<Category> FindCategoryByIdAsync(Guid id);
        public Task<ICollection<Category>> FindAllCategoriesAsync();
        public Task<ICollection<Category>> FindAllActiveCategoriesAsync();
    }
}
