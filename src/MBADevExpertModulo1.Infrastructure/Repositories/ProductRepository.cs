using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Interfaces;

namespace MBADevExpertModulo1.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public ProductRepository() { }

    public void AddProduct(Product product)
    {
        using var db = new Database.DatabaseContext();
        db.Product.Add(product);
        db.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        using var db = new Database.DatabaseContext();
        var productDb = db.Product.Find(product);
        if (productDb != null)
        {
            db.Product.Update(product);
            db.SaveChanges();
        }
    }

    public void RemoveProduct(Product product)
    {
        using var db = new Database.DatabaseContext();
        var productDb = db.Product.Find(product);
        if (productDb != null)
        {
            db.Product.Remove(product);
            db.SaveChanges();
        }
    }

    public Product FindProductById(int id)
    {
        using var db = new Database.DatabaseContext();
        var productById = db.Product.Where( c => c.Id == id).SingleOrDefault();
        return productById;
    }

    public ICollection<Product> FindAllProducts()
    {
        using var db = new Database.DatabaseContext();
        var products = db.Product.Where(c => c.Id > 0).OrderBy(c => c.Id).ToList();
        return products;
    }

    public ICollection<Product> FindAllActiveProducts()
    {
        using var db = new Database.DatabaseContext();
        var products = db.Product.Where(c => c.Id > 0 && !c.Deleted).OrderBy(c => c.Id).ToList();
        return products;
    }

    public ICollection<Product> FindProductsBySellerId(int sellerId)
    {
        using var db = new Database.DatabaseContext();
        var productsBySellerId = db.Product.Where(c => c.SellerId == sellerId).OrderBy(c => c.Id).ToList();
        return productsBySellerId;
    }
}

