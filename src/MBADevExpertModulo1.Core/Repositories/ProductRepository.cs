using MBADevExpertModulo1.Core.Models;
using MBADevExpertModulo1.Core.Database;
using MBADevExpertModulo1.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBADevExpertModulo1.Core.Repositories;

public class ProductRepository(DatabaseContext db) : IProductRepository
{
    public async Task AddProductAsync(Product product)
    {
        product.Deleted = false;
        db.Product.Add(product);
        await db.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {        
        db.Product.Update(product);
        await db.SaveChangesAsync();
    }

    public async Task RemoveProductAsync(Guid id)
    {
        var productDb = db.Product.Find(id);
        if (productDb != null)
        {
            productDb.Deleted = true;
            db.Product.Remove(productDb);
            await db.SaveChangesAsync();
        }
    }

    public async Task<Product> FindProductByIdAsync(Guid id)
    {
        return await db.Product.Include(c => c.Category).Include(c => c.Seller).Where( c => c.Id == id && !c.Deleted).AsNoTracking().SingleOrDefaultAsync() ?? new Product();
    }

    public async Task<ICollection<Product>> FindAllProductsAsync()
    {
        return await db.Product.Include(c => c.Category).OrderBy(c => c.Id).AsNoTracking().ToListAsync();
    }

    public async Task<ICollection<Product>> FindAllActiveProductsAsync()
    {
        return await db.Product.Include(c => c.Category).Where(c => !c.Deleted).OrderBy(c => c.Id).AsNoTracking().ToListAsync();
    }

    public async Task<ICollection<Product>> FindAllProductsByCategoryIdAsync(Guid categoryId)
    {
        return await db.Product.Include(c => c.Category).Include(c => c.Seller).Where(c => c.CategoryId == categoryId && !c.Deleted).OrderBy(c => c.Id).AsNoTracking().ToListAsync();
    }

    public async Task<ICollection<Product>> FindAllProductsBySellerIdAsync(Guid sellerId)
    {
        return await db.Product.Include(c => c.Category).Include(c => c.Seller).Where(c => c.SellerId == sellerId).OrderBy(c => c.Id).AsNoTracking().ToListAsync();
    }
}

