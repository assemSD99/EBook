using EBookWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace EBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View();
        }
    }
}
