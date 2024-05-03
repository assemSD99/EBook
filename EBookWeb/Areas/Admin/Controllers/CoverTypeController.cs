using Ebook.DataAccess.Repository.IRepository;
using Ebook.Models;
using EBook.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace EBook.Controllers;
[Area("Admin")]

    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType CoverType)
        {
            
            if(ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(CoverType);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Created successfully";

                return RedirectToAction("Index");
            }
            return View(CoverType);
        }

        public IActionResult Edit(int? id) 
        { 
            if(id== null || id==0)
            {
                return NotFound();
            }
            //var CoverTypeFromDb = _context.Categories.Find(id);
            var CoverTypeFirst = _unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);
            //var CoverTypeSingle = _context.Categories.SingleOrDefault(i => i.Id == id);
            if(CoverTypeFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFirst); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType CoverType)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(CoverType);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Edited successfully";


                return RedirectToAction("Index");
            }
            return View(CoverType);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var CoverTypeFromDb = _context.Categories.Find(id);
            var CoverTypeFirst = _unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);
            //var CoverTypeSingle = _context.Categories.SingleOrDefault(i => i.Id == id);
            if (CoverTypeFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFirst);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

                _unitOfWork.CoverType.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Deleted successfully";


            return RedirectToAction("Index");
           
        }

    }

