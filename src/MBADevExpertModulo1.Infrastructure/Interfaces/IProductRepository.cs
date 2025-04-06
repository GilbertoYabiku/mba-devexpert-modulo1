using MBADevExpertModulo1.Domain.Models;

namespace MBADevExpertModulo1.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        public Task AddProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public Task RemoveProductAsync(int id);
        public Task<Product> FindProductByIdAsync(int id);
        public Task<ICollection<Product>> FindAllProductsAsync();
        public Task<ICollection<Product>> FindAllActiveProductsAsync();
        public Task<ICollection<Product>> FindAllProductsByCategoryIdAsync(int categoryId);
        public Task<ICollection<Product>> FindAllProductsBySellerIdAsync(int sellerId);
    }
}
