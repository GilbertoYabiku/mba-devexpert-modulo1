using MBADevExpertModulo1.Core.Models;
using MBADevExpertModulo1.Core.Database;
using MBADevExpertModulo1.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MBADevExpertModulo1.Core.Repositories;
public class SellerRepository(DatabaseContext db) : ISellerRepository
{
    public async Task AddSellerAsync(Seller seller)
    {
        db.Seller.Add(seller);
        await db.SaveChangesAsync();
    }

    public async Task<Seller> FindSellerByIdAsync(Guid id)
    {
        return await db.Seller.Where(c => c.Id == id).AsNoTracking().SingleOrDefaultAsync() ?? new Seller();
    }
}

