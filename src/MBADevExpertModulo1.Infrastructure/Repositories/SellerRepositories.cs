using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Interfaces;

namespace MBADevExpertModulo1.Infrastructure.Repositories;
public class SellerRepositories : ISellerRepository
{
    public SellerRepositories() { }

    public void AddSeller(Seller seller)
    {
        using var db = new Database.DatabaseContext();
        db.Seller.Add(seller);
        db.SaveChanges();
    }

    public Seller FindSellerById(int id)
    {
        using var db = new Database.DatabaseContext();
        var sellerById = (from c in db.Seller where c.Id == id select c).SingleOrDefault();
        return sellerById;
    }
}

