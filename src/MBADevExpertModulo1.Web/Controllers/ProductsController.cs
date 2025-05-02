using System.Security.Claims;
using MBADevExpertModulo1.Core.Interfaces;
using MBADevExpertModulo1.Core.Models;
using MBADevExpertModulo1.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MBADevExpertModulo1.Web.Controllers;

[Authorize]
[Route("products")]
public class ProductsController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment, ICategoryRepository categoryRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return View(await productRepository.FindAllProductsBySellerIdAsync(Guid.Parse(userId)));
    }

    [Route("new")]
    public async Task<IActionResult> Create()
    {
        var categories = await categoryRepository.FindAllActiveCategoriesAsync();
        if (categories.Count < 1) return Problem("There is no categories in the system");

        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }

    [HttpPost("new")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Stock,CategoryId,ImageFormFile,Deleted")] Product product)
    {
        ModelState.Remove("Image");
        product.SellerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        product.Image = ConvertImageToString(product!.ImageFormFile);
        if (ModelState.IsValid)
        {
            var category = await categoryRepository.FindCategoryByIdAsync(product.CategoryId);
            if (category == null) return NotFound();
            product.Category = category;
            await productRepository.AddProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction("Index");
    }

    [Route("details/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var product = await productRepository.FindProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [Route("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var product = await productRepository.FindProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var categories = await categoryRepository.FindAllActiveCategoriesAsync();
        if (categories.Count < 1) return Problem("There is no categories in the system");

        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View(product);
    }

    [HttpPost("edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,Stock,CategoryId,ImageFormFile,Deleted")] Product product, IFormFile ImageFormFile)
    {
        ModelState.Remove("Image");
        if (id != product.Id)
        {

            return NotFound();
        }
        product.SellerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        product.Image = ConvertImageToString(ImageFormFile);
        if (ModelState.IsValid)
        {
            var category = await categoryRepository.FindCategoryByIdAsync(product.CategoryId);
            if (category == null) return NotFound();

            if (!HasCorrectProductSeller(product).Result)
            {
                return Problem("User can't modify the product");
            }

            var productInDB = await productRepository.FindProductByIdAsync(product.Id);
            if (productInDB == null) return NotFound(id);

            await productRepository.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction("Index");
    }

    [Route("delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await productRepository.FindProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost("delete/{id:guid}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var product = await productRepository.FindProductByIdAsync(id);

        if (product == null) return NotFound();
        product.SellerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (!HasCorrectProductSeller(product).Result) return Problem("User can't delete the product");

        product.Deleted = true;

        await productRepository.RemoveProductAsync(id);

        return RedirectToAction(nameof(Index));
    }

    private string ConvertImageToString(IFormFile formFile)
    {
        var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
        if (formFile != null && formFile.Length > 0)
        {
            var uniqueFileName = Guid.NewGuid().ToString()+"_"+formFile.FileName;
            var filePath = Path.Combine(path, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        return null;
    }

    private async Task<bool> HasCorrectProductSeller(Product product)
    {
        var productInDb = await productRepository.FindProductByIdAsync(product.Id);
        if (productInDb != null && product.SellerId != productInDb.SellerId) return false;
        return true;
    }
}