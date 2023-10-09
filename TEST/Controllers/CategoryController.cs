using Microsoft.AspNetCore.Mvc;
using TEST.Data;
using TEST.Models;

namespace TEST.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories;
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            //validate name
            bool checkValidateName = _db.Categories.Any(i => i.Name == category.Name);
            if (checkValidateName)
            {
                ModelState.AddModelError("Name", "The category name already exist");
            }

            //validate displayorder
            bool checkValidateDisplayOrder = _db.Categories.Any(i => i.DisplayOrder == category.DisplayOrder);
            if (checkValidateDisplayOrder)
            {
                ModelState.AddModelError("DisplayOrder", "The category display order already exist");
            }

            //savedata
            if (ModelState.IsValid) 
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            return View();
        }
    }
}
