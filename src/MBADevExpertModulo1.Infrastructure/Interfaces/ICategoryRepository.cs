using MBADevExpertModulo1.Domain.Models;

namespace MBADevExpertModulo1.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        public void AddCategory(Category category);
        public void UpdateCategory(Category category);
        public void RemoveCategory(Category category);
        public Category FindCategoryById(int id);
        public ICollection<Category> FindAllCategories();
        public ICollection<Category> FindAllActiveCategories();
    }
}
