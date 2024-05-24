using Ebook.DataAccess.Repository.IRepository;
using Ebook.Models;
using Ebook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]

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
    public IActionResult Create(Category Category)
    {

        if (Category.Name == Category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
        }

        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(Category);
            _unitOfWork.Save();
            TempData["success"] = "Category Created successfully";

            return RedirectToAction("Index");
        }
        return View(Category);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var CategoryFromDb = _context.Categories.Find(id);
        var CategoryFirst = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);
        //var CategorySingle = _context.Categories.SingleOrDefault(i => i.Id == id);
        if (CategoryFirst == null)
        {
            return NotFound();
        }
        return View(CategoryFirst);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category Category)
    {
        if (Category.Name == Category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");
        }

        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(Category);
            _unitOfWork.Save();
            TempData["success"] = "Category Edited successfully";


            return RedirectToAction("Index");
        }
        return View(Category);
    }


    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //var CategoryFromDb = _context.Categories.Find(id);
        var CategoryFirst = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);
        //var CategorySingle = _context.Categories.SingleOrDefault(i => i.Id == id);
        if (CategoryFirst == null)
        {
            return NotFound();
        }
        return View(CategoryFirst);
    }

    [HttpPost, ActionName("Delete")]
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

