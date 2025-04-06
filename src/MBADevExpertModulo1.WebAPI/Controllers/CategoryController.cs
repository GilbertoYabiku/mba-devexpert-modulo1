using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBADevExpertModulo1.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/categories")]
public class CategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<Category>>> GetAllAsync()
    {
        return Ok(await categoryRepository.FindAllActiveCategoriesAsync());
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.FindCategoryByIdAsync(id);
        if (category == null) return NotFound(id);
            
        return Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateCategoryAsync(Category category)
    {
        if(!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState));
            
        await categoryRepository.AddCategoryAsync(category);
        return CreatedAtAction("GetByIdAsync", category.Id, category);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateCategoryAsync(int id, Category category)
    {
        if (id != category.Id) return BadRequest("Provided IDs don't match");
        if (!ModelState.IsValid) return ValidationProblem(new ValidationProblemDetails(ModelState));

        var categoryInDB = await categoryRepository.FindCategoryByIdAsync(id);
        if (categoryInDB == null) return NotFound(id);

        await categoryRepository.UpdateCategoryAsync(category);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    {
        var categoryInDB = await categoryRepository.FindCategoryByIdAsync(id);
        if (categoryInDB == null) return NotFound(id);

        var relatedProduct = await productRepository.FindAllProductsByCategoryIdAsync(id);
        if (relatedProduct != null || relatedProduct.Count > 0) return BadRequest("Category has product linked");

        await categoryRepository.RemoveCategoryAsync(categoryInDB);
        return NoContent();
    }
}
