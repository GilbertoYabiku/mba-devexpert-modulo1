using System.Diagnostics;
using MBADevExpertModulo1.Core.Interfaces;
using MBADevExpertModulo1.Models;
using Microsoft.AspNetCore.Mvc;

namespace MBADevExpertModulo1.Controllers
{
    public class HomeController(IProductRepository productRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await productRepository.FindAllProductsAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}