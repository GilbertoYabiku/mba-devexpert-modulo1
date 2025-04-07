using System.ComponentModel;
using MBADevExpertModulo1.Domain.Models;
using MBADevExpertModulo1.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MBADevExpertModulo1.Web.Controllers
{
    public class CategoriesController(ICategoryRepository categoryRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await categoryRepository.FindAllActiveCategoriesAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

        public async Task<IActionResult> Details(int id)
        {
            var category = await categoryRepository.FindCategoryByIdAsync(id);
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryRepository.FindCategoryByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Deleted")] Category category)
        {
            if(id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await categoryRepository.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
