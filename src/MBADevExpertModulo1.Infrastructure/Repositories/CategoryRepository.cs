using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Database;
using MBADevExpertModulo1.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace MBADevExpertModulo1.Infrastructure.Repositories;

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

    public async Task<Category> FindCategoryByIdAsync(int id)
    {
        return await db.Category.Include(c => c.Products).Where(c => c.Id == id && !c.Deleted).SingleOrDefaultAsync() ?? new Category();
    }

    public async Task<ICollection<Category>> FindAllCategoriesAsync()
    {
        return await db.Category.Include(c => c.Products).Where(c => c.Id > 0).OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<ICollection<Category>> FindAllActiveCategoriesAsync()
    {
        return await db.Category.Include(c => c.Products).Where(c => c.Id > 0 && !c.Deleted).OrderBy(c => c.Id).ToListAsync();
    }
}

