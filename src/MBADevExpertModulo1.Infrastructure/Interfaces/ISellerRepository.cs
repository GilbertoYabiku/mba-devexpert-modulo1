using MBADevExpertModulo1.Domain.Models;

namespace MBADevExpertModulo1.Infrastructure.Interfaces;

public interface ISellerRepository
{
    public void AddSellerAsync(Seller seller);
    public Task<Seller> FindSellerByIdAsync(int id);
}

