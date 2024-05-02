using Microsoft.AspNetCore.Mvc;

namespace EBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
