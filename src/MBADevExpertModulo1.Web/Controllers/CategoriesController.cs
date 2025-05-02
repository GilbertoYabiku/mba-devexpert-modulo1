using MBADevExpertModulo1.Core.Interfaces;
using MBADevExpertModulo1.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MBADevExpertModulo1.Web.Controllers;

[Authorize]
[Route("categories")]
public class CategoriesController(ICategoryRepository categoryRepository, IProductRepository productRepository) : Controller
{
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await categoryRepository.FindAllActiveCategoriesAsync());
    }

    [Route("new")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("new")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Deleted")] Category category)
    {
        if (ModelState.IsValid)
        {
            await categoryRepository.AddCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    [AllowAnonymous]
    [Route("details/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var category = await categoryRepository.FindCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [Route("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var category = await categoryRepository.FindCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost("edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Deleted")] Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var categoryInDB = await categoryRepository.FindCategoryByIdAsync(id);
            if (categoryInDB == null) return NotFound(id);

            await categoryRepository.UpdateCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    [Route("delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var category = await categoryRepository.FindCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost("delete/{id:guid}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var category = await categoryRepository.FindCategoryByIdAsync(id);
        if (category == null) return NotFound();

        var relatedProduct = await productRepository.FindAllProductsByCategoryIdAsync(id);
        if (relatedProduct.Count > 0) return Problem("Category has product linked");

        category.Deleted = true;

        await categoryRepository.RemoveCategoryAsync(category);

        return RedirectToAction(nameof(Index));
    }
}