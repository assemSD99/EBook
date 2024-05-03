using Ebook.DataAccess.Repository.IRepository;
using Ebook.Models;
using EBook.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
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
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
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
            //var categoryFromDb = _context.Categories.Find(id);
            var categoryFirst = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);
            //var categorySingle = _context.Categories.SingleOrDefault(i => i.Id == id);
            if(categoryFirst == null)
            {
                return NotFound();
            }
            return View(categoryFirst); 
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
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
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
            //var categoryFromDb = _context.Categories.Find(id);
            var categoryFirst = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);
            //var categorySingle = _context.Categories.SingleOrDefault(i => i.Id == id);
            if (categoryFirst == null)
            {
                return NotFound();
            }
            return View(categoryFirst);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

                _unitOfWork.Category.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Deleted successfully";


            return RedirectToAction("Index");
           
        }

    }
}
