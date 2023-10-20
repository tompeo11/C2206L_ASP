using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEST.DAO;
using TEST.Data;
using TEST.Models;

namespace TEST.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly GenericRepository<Category> _unitOfWork.categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string? search)
        {
            //IEnumerable<Category> categories = _unitOfWork.categoryRepository.GetEntities(null,
            //    q => q.OrderByDescending(c => c.DisplayOrder)).AsQueryable();
            var categories = _unitOfWork.categoryRepository.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return View(categories);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.categoryRepository.GetEntityById((int)id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,DisplayOrder,CreatedDate")] Category category)
        {
            bool checkCategoryNameExist = _unitOfWork.categoryRepository.GetEntities(i => i.Name == category.Name).Any();
            bool checkCategoryDisplayOrderExist = _unitOfWork.categoryRepository.GetEntities(i => i.DisplayOrder == category.DisplayOrder).Any();



            //validate name
            //bool checkValidateName = _unitOfWork.categoryRepository.GetAll().Any(i => i.Name == category.Name);

            if (checkCategoryNameExist)
            {
                TempData["categoryNameError"] = "The category name already exist";
                ModelState.AddModelError("Name", "The category name already exist");
            }

            //validate displayorder
            //bool checkValidateDisplayOrder = _unitOfWork.categoryRepository.GetAll().Any(i => i.DisplayOrder == category.DisplayOrder);

            if (checkCategoryDisplayOrderExist)
            {
                TempData["categoryDisplayOrderError"] = "The category display order already exist";
                ModelState.AddModelError("DisplayOrder", "The category display order already exist");
            }


            if (ModelState.IsValid)
            {
                TempData["successCreate"] = "Add category successfully";
                _unitOfWork.categoryRepository.Add(category);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.categoryRepository.GetEntityById((int)id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Name,DisplayOrder,CreatedDate")] Category category)
        {
            bool checkCategoryNameExist = _unitOfWork.categoryRepository.GetEntities(i => i.Name == category.Name && i.Id != category.Id).Any();
            bool checkCategoryDisplayOrderExist = _unitOfWork.categoryRepository.GetEntities(i => i.DisplayOrder == category.DisplayOrder && i.Id != category.Id).Any();
            //bool checkValidateName = _unitOfWork.categoryRepository.GetAll().Any(i => i.Name == category.Name && i.Id != category.Id);

            if (checkCategoryNameExist)
            {
                ModelState.AddModelError("Name", "The category name already exist");
            }

            //bool checkValidateDisplayOrder = _unitOfWork.categoryRepository.GetAll().Any(i => i.DisplayOrder == category.DisplayOrder && i.Id != category.Id);

            if (checkCategoryDisplayOrderExist)
            {
                ModelState.AddModelError("DisplayOrder", "The category display order already exist");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["successEdit"] = "Edit category successfully";
                    _unitOfWork.categoryRepository.Update(category);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.categoryRepository.GetEntityById((int)id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _unitOfWork.categoryRepository.GetEntityById(id);
            if (category != null)
            {
                TempData["successDelete"] = "Delete category successfully";
                _unitOfWork.categoryRepository.Delete(category);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return (_unitOfWork.categoryRepository.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
