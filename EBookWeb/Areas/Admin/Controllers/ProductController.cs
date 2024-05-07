using Ebook.DataAccess.Repository.IRepository;
using Ebook.Models;
using Ebook.Models.ViewModel;
using EBook.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EBook.Controllers;
[Area("Admin")]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
	private readonly IWebHostEnvironment _webHostEnvironment;

	public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
		    _webHostEnvironment = webHostEnvironment;
	}
        public IActionResult Index()
        {
            
            return View();
        }

        

        public IActionResult Upsert(int? id) 
        {
        ProductVM productVM = new()
        {
            Product = new(),
            CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),


        };
            

		if (id== null || id==0)
            {
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList;
				//Create Product
				return View(productVM);
            }else
            {
                //Update Product
            }
            
            return View(productVM); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
           
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRoot, @"images\products");
                    var extension =Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                obj.Product.ImageUrl = @"\images\products" + fileName + extension;
                }

                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";


                return RedirectToAction("Index");
            }
            return View(obj);
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

    #region API CALLS
    [HttpGet]
    public IActionResult Getall()
    {
        var productList = _unitOfWork.Product.GetAll();
        return Json(new {data =  productList});
    }
    #endregion

}

