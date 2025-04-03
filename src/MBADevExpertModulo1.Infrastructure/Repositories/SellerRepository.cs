using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Database;
using MBADevExpertModulo1.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBADevExpertModulo1.Infrastructure.Repositories;
public class SellerRepository(DatabaseContext db) : ISellerRepository
{
    public async Task AddSellerAsync(Seller seller)
    {
        db.Seller.Add(seller);
        await db.SaveChangesAsync();
    }

    public async Task<Seller> FindSellerByIdAsync(int id)
    {
        return await db.Seller.Where(c => c.Id == id).SingleOrDefaultAsync() ?? new Seller();
    }
}

