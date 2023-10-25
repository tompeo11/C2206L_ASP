using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TEST.DAO;
using TEST.Models;

namespace TEST.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string? search)
        {
            var coverTypes = _unitOfWork.coverTypeRepository.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                coverTypes = coverTypes.Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            return View(coverTypes);
        }


        public IActionResult Upsert(int? id)
        {
            CoverType coverType;

            if(id == null || id == 0)
            {
                coverType = new CoverType();
            }
            else
            {
                coverType = _unitOfWork.coverTypeRepository.GetEntityById((int)id);
                if (coverType == null)
                {
                    return  NotFound();
                }
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert([Bind("Id,Name")] CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if(coverType.Id == 0)
                {
                    _unitOfWork.coverTypeRepository.Add(coverType);
                    TempData["successCreate"] = "Add covertype successfully";
                }
                else
                {
                    TempData["successUpdate"] = "Update covertype successfully";
                    _unitOfWork.coverTypeRepository.Update(coverType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.coverTypeRepository.GetEntityById((int)id);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var coverType = _unitOfWork.coverTypeRepository.GetEntityById(id);

            if (coverType != null)
            {
                TempData["successDelete"] = "Delete coverType successfully";
                _unitOfWork.coverTypeRepository.Delete(id);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        } 

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.coverTypeRepository.GetEntityById((int)id);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }
    }
}
