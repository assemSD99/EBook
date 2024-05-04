using Ebook.DataAccess.Repository.IRepository;
using Ebook.Models;
using EBook.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EBook.Controllers;
[Area("Admin")]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
            return View(objProductList);
        }

        

        public IActionResult Upsert(int? id) 
        {
            Product product = new();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
            u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
		    IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
		    u => new SelectListItem
		    {
			    Text = u.Name,
			    Value = u.Id.ToString()
		    });

		if (id== null || id==0)
            {
                //Create Product
                return View(product);
            }else
            {
                //Update Product
            }
            
            return View(product); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product Product)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Edited successfully";


                return RedirectToAction("Index");
            }
            return View(Product);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var ProductFromDb = _context.Categories.Find(id);
            var ProductFirst = _unitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
            //var ProductSingle = _context.Categories.SingleOrDefault(i => i.Id == id);
            if (ProductFirst == null)
            {
                return NotFound();
            }
            return View(ProductFirst);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Deleted successfully";


            return RedirectToAction("Index");
           
        }

    }

