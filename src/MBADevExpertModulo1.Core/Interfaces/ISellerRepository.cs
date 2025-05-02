using MBADevExpertModulo1.Core.Models;

namespace MBADevExpertModulo1.Core.Interfaces;

public interface ISellerRepository
{
    public Task AddSellerAsync(Seller seller);
    public Task<Seller> FindSellerByIdAsync(Guid id);
}

