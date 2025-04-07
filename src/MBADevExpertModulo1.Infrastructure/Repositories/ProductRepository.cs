using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Database;
using MBADevExpertModulo1.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBADevExpertModulo1.Infrastructure.Repositories;

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
        var productDb = db.Product.Find(product);
        if (productDb != null)
        {
            db.Product.Update(product);
            await db.SaveChangesAsync();
        }
    }

    public async Task RemoveProductAsync(int id)
    {
        var productDb = db.Product.Find(id);
        if (productDb != null)
        {
            productDb.Deleted = true;
            db.Product.Remove(productDb);
            await db.SaveChangesAsync();
        }
    }

    public async Task<Product> FindProductByIdAsync(int id)
    {
        return await db.Product.Include(c => c.Category).Include(c => c.Seller).Where( c => c.Id == id && !c.Deleted).SingleOrDefaultAsync() ?? new Product();
    }

    public async Task<ICollection<Product>> FindAllProductsAsync()
    {
        return await db.Product.Include(c => c.Category).Where(c => c.Id > 0).OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<ICollection<Product>> FindAllActiveProductsAsync()
    {
        return await db.Product.Include(c => c.Category).Where(c => c.Id > 0 && !c.Deleted).OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<ICollection<Product>> FindAllProductsByCategoryIdAsync(int categoryId)
    {
        return await db.Product.Include(c => c.Category).Include(c => c.Seller).Where(c => c.CategoryId == categoryId && !c.Deleted).OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<ICollection<Product>> FindAllProductsBySellerIdAsync(int sellerId)
    {
        return await db.Product.Include(c => c.Category).Include(c => c.Seller).Where(c => c.SellerId == sellerId).OrderBy(c => c.Id).ToListAsync();
    }
}

