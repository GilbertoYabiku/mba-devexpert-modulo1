using MBADevExpertModulo1.Core.Models;

namespace MBADevExpertModulo1.Core.Interfaces
{
    public interface IProductRepository
    {
        public Task AddProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public Task RemoveProductAsync(Guid id);
        public Task<Product> FindProductByIdAsync(Guid id);
        public Task<ICollection<Product>> FindAllProductsAsync();
        public Task<ICollection<Product>> FindAllActiveProductsAsync();
        public Task<ICollection<Product>> FindAllProductsByCategoryIdAsync(Guid categoryId);
        public Task<ICollection<Product>> FindAllProductsBySellerIdAsync(Guid sellerId);
    }
}
