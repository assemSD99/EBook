using EBookWeb.Data;
using EBookWeb.Models;
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
            IEnumerable<Category> objCategoryList = _context.Categories;
            return View(objCategoryList);
        }
    }
}
