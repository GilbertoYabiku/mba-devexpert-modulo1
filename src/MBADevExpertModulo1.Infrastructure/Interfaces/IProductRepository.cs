using MBADevExpertModulo1.Domain.Models;

namespace MBADevExpertModulo1.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
        public void RemoveProduct(Product product);
        public Product FindProductById(int id);
        public ICollection<Product> FindAllProducts();
        public ICollection<Product> FindAllActiveProducts();
        public ICollection<Product> FindProductsBySellerId(int sellerId);
    }
}
