using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Interfaces;
namespace MBADevExpertModulo1.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public CategoryRepository() { }

    public void AddCategory(Category category)
    {
        using var db = new Database.DatabaseContext();
        db.Category.Add(category);
        db.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        using var db = new Database.DatabaseContext();
        var categoryDb = db.Category.Find(category.Id);
        if (categoryDb != null)
        {
            db.Category.Update(category);
            db.SaveChanges();
        }
    }

    public void RemoveCategory(Category category)
    {
        using var db = new Database.DatabaseContext();
        var categoryDb = db.Category.Find(category.Id);
        if (categoryDb != null)
        {
            var relatedProducts = db.Product.Where(p => p.CategoryId == category.Id).ToList();
            if (relatedProducts.Count < 1)
            {
                db.Category.Remove(category);

                db.SaveChanges();
            }
        }
    }

    public Category FindCategoryById(int id)
    {
        using var db = new Database.DatabaseContext();
        var categoryById = db.Category.Where(c => c.Id == id).SingleOrDefault();
        return categoryById;
    }

    public ICollection<Category> FindAllCategories()
    {
        using var db = new Database.DatabaseContext();
        var categories = db.Category.Where(c => c.Id > 0).OrderBy(c => c.Id).ToList();
        return categories;
    }

    public ICollection<Category> FindAllActiveCategories()
    {
        using var db = new Database.DatabaseContext();
        var categories = db.Category.Where(c => c.Id > 0 && !c.Deleted).OrderBy(c => c.Id).ToList();
        return categories;
    }
}

