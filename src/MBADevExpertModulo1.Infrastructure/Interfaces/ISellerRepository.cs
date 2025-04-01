using MBADevExpertModulo1.Domain.Models;

namespace MBADevExpertModulo1.Infrastructure.Interfaces;

public interface ISellerRepository
{
    public void AddSeller(Seller seller);
    public Seller FindSellerById(int id);
}

