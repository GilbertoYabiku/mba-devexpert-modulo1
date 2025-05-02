using MBADevExpertModulo1.Core.Models;
using MBADevExpertModulo1.Core.Database;
using MBADevExpertModulo1.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace MBADevExpertModulo1.Core.Repositories;

public class CategoryRepository (DatabaseContext db): ICategoryRepository
{
    public async Task AddCategoryAsync(Category category)
    {
        category.Deleted = false;
        db.Category.Add(category);
        await db.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        db.Category.Update(category);
        await db.SaveChangesAsync();
    }

    public async Task RemoveCategoryAsync(Category category)
    {
        db.Category.Update(category);
        await db.SaveChangesAsync();
    }

    public async Task<Category> FindCategoryByIdAsync(Guid id)
    {
        return await db.Category.Include(c => c.Products).Where(c => c.Id == id && !c.Deleted).AsNoTracking().SingleOrDefaultAsync() ?? new Category();
    }

    public async Task<ICollection<Category>> FindAllCategoriesAsync()
    {
        return await db.Category.OrderBy(c => c.Id).AsNoTracking().ToListAsync();
    }

    public async Task<ICollection<Category>> FindAllActiveCategoriesAsync()
    {
        return await db.Category.OrderBy(c => c.Id).Where(c => !c.Deleted).AsNoTracking().ToListAsync();
    }
}

