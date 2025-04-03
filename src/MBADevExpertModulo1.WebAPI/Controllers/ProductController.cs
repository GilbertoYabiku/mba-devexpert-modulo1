using System.Security.Claims;
using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBADevExpertModulo1.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/products")]
public class ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<Product>>> GetAllAsync()
    {
        return Ok(await productRepository.FindAllActiveProductsAsync());
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetByIdAsync(int id)
    {
        var category = await productRepository.FindProductByIdAsync(id);
        if (category == null) return NotFound(id);
        
        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(Product product)
    {
        if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState));
        var category = await categoryRepository.FindCategoryByIdAsync(product.CategoryId);
        if (category == null) return BadRequest("Linked category does not exist");

        return CreatedAtAction("GetByIdAsync", product.Id, product);
    }


    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProductAsync(int id, Product product)
    {
        if (id != product.Id) return BadRequest("Provided IDs don't match");
        if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState));

        var category = await categoryRepository.FindCategoryByIdAsync(product.CategoryId);
        if (category == null) return BadRequest("Linked category does not exist");

        if (!HasCorrectProductSeller(id).Result)
        {
            return BadRequest("User can't modify the product");
        }

        var productInDB = await productRepository.FindProductByIdAsync(product.Id);
        if (productInDB == null) return NotFound(id);

        await productRepository.UpdateProductAsync(product);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        var productInDB = await productRepository.FindProductByIdAsync(id);
        if (productInDB == null) return NotFound(id);

        if (!HasCorrectProductSeller(id).Result)
        {
            return BadRequest("User can't delete the product");
        }
        await productRepository.RemoveProductAsync(id);
        return Ok();
    }

    private async Task<bool> HasCorrectProductSeller(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var productInDb = await productRepository.FindProductByIdAsync(id);
        if (productInDb != null && userId != productInDb.SellerId.ToString()) return false;
        return true;
    }
}
