using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEST.DAO;
using TEST.Models;

namespace TEST.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string? search)
        {
            var products = _unitOfWork.productRepository.GetAll(includeProperties : "Category,CoverType");

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(c => c.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return View(products);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_unitOfWork.categoryRepository.GetAll(),"Id","Name");
            ViewData["CoverTypeId"] = new SelectList(_unitOfWork.coverTypeRepository.GetAll(),"Id","Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Description,ISBN,Author,Price,Price50,Price100,CategoryId,CoverTypeId")] Product product,
            IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var productPath = Path.Combine(wwwRootPath, "images/product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    product.ImageUrl = fileName;
                }

                TempData["successCreate"] = "Add product successfully";
                _unitOfWork.productRepository.Add(product);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _unitOfWork.productRepository.GetEntities(null, includeProperties: "Category,CoverType").FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _unitOfWork.productRepository.GetEntities(null, includeProperties : "Category").FirstOrDefault();
            if (product != null)
            {
                TempData["successDelete"] = "Delete product successfully";
                _unitOfWork.productRepository.Delete(product);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _unitOfWork.productRepository.GetEntityById((int)id);

            ViewData["CategoryId"] = new SelectList(_unitOfWork.categoryRepository.GetAll(), "Id", "Name", product.CategoryId);
            ViewData["CoverTypeId"] = new SelectList(_unitOfWork.coverTypeRepository.GetAll(), "Id", "Name", product.CoverTypeId);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Title,Description,ISBN,Author,Price,Price50,Price100,CategoryId,CoverTypeId")] Product product,
            IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var productPath = Path.Combine(wwwRootPath, "images/product");
                    var oldImagePath = Path.Combine(productPath, product.ImageUrl);

                    if(System.IO.File.Exists(oldImagePath) && product.ImageUrl != "default.jpg")
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    product.ImageUrl = fileName;
                }
                TempData["successEdit"] = "Edit product successfully";
                _unitOfWork.productRepository.Update(product);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _unitOfWork.productRepository.GetEntities((p => p.Id == id), includeProperties: "Category,CoverType").FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}