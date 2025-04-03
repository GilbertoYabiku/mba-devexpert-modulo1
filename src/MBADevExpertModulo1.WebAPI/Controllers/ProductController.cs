using System.Threading.Tasks;
using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBADevExpertModulo1.WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ICollection<Product>>> GetAllAsync()
        {
            return Ok(await productRepository.FindAllActiveProductsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetByIdAsync(int id)
        {
            var category = await productRepository.FindProductByIdAsync(id);
            if (category != null)
            {
                return Ok(category);

            }
            else
            {
                return NotFound(id);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(Product product)
        {
            var category = await categoryRepository.FindCategoryByIdAsync(product.CategoryId);
            if (category == null) return BadRequest("Linked category does not exist");

            await productRepository.AddProductAsync(product);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, Product product)
        {
            var category = await categoryRepository.FindCategoryByIdAsync(product.CategoryId);
            if (category == null) return BadRequest("Linked category does not exist");

            await productRepository.UpdateProductAsync(product);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            await productRepository.RemoveProductAsync(id);
            return Ok();
        }
    }
}
