using Microsoft.AspNetCore.Mvc;

namespace MBADevExpertModulo1.Web.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
