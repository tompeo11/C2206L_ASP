using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TEST.Data;
using TEST.Models;

namespace TEST.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VaccineTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaccineTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/VaccineType
        public async Task<IActionResult> Index()
        {
              return _context.VaccinesTypes != null ? 
                          View(await _context.VaccinesTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.VaccinesTypes'  is null.");
        }

        // GET: Admin/VaccineType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VaccinesTypes == null)
            {
                return NotFound();
            }

            var vaccineType = await _context.VaccinesTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccineType == null)
            {
                return NotFound();
            }

            return View(vaccineType);
        }

        // GET: Admin/VaccineType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/VaccineType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreateAt")] VaccineType vaccineType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccineType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaccineType);
        }

        // GET: Admin/VaccineType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VaccinesTypes == null)
            {
                return NotFound();
            }

            var vaccineType = await _context.VaccinesTypes.FindAsync(id);
            if (vaccineType == null)
            {
                return NotFound();
            }
            return View(vaccineType);
        }

        // POST: Admin/VaccineType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreateAt")] VaccineType vaccineType)
        {
            if (id != vaccineType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccineType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineTypeExists(vaccineType.Id))
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
            return View(vaccineType);
        }

        // GET: Admin/VaccineType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VaccinesTypes == null)
            {
                return NotFound();
            }

            var vaccineType = await _context.VaccinesTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccineType == null)
            {
                return NotFound();
            }

            return View(vaccineType);
        }

        // POST: Admin/VaccineType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VaccinesTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VaccinesTypes'  is null.");
            }
            var vaccineType = await _context.VaccinesTypes.FindAsync(id);
            if (vaccineType != null)
            {
                _context.VaccinesTypes.Remove(vaccineType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineTypeExists(int id)
        {
          return (_context.VaccinesTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
