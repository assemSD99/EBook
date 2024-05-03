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

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if(ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["success"] = "Category Created successfully";

                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id) 
        { 
            if(id== null || id==0)
            {
                return NotFound();
            }
            var categoryFromDb = _context.Categories.Find(id);
            //var categoryFirst = _context.Categories.FirstOrDefault(i => i.Id == id);
            //var categorySingle = _context.Categories.SingleOrDefault(i => i.Id == id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["success"] = "Category Edited successfully";


                return RedirectToAction("Index");
            }
            return View(category);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _context.Categories.Find(id);
            //var categoryFirst = _context.Categories.FirstOrDefault(i => i.Id == id);
            //var categorySingle = _context.Categories.SingleOrDefault(i => i.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
                _context.Categories.Remove(obj);
                _context.SaveChanges();
                TempData["success"] = "Category Deleted successfully";


            return RedirectToAction("Index");
           
        }

    }
}
