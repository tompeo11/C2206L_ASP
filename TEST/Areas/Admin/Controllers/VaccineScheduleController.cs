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
    public class VaccineScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaccineScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/VaccineSchedule
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VaccineSchedules.Include(v => v.Vaccine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/VaccineSchedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VaccineSchedules == null)
            {
                return NotFound();
            }

            var vaccineSchedule = await _context.VaccineSchedules
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccineSchedule == null)
            {
                return NotFound();
            }

            return View(vaccineSchedule);
        }

        // GET: Admin/VaccineSchedule/Create
        public IActionResult Create()
        {
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Country");
            return View();
        }

        // POST: Admin/VaccineSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,VaccinationDates,VaccineId,CreateAt")] VaccineSchedule vaccineSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccineSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Country", vaccineSchedule.VaccineId);
            return View(vaccineSchedule);
        }

        // GET: Admin/VaccineSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VaccineSchedules == null)
            {
                return NotFound();
            }

            var vaccineSchedule = await _context.VaccineSchedules.FindAsync(id);
            if (vaccineSchedule == null)
            {
                return NotFound();
            }
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Country", vaccineSchedule.VaccineId);
            return View(vaccineSchedule);
        }

        // POST: Admin/VaccineSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,VaccinationDates,VaccineId,CreateAt")] VaccineSchedule vaccineSchedule)
        {
            if (id != vaccineSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccineSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineScheduleExists(vaccineSchedule.Id))
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
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "Country", vaccineSchedule.VaccineId);
            return View(vaccineSchedule);
        }

        // GET: Admin/VaccineSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VaccineSchedules == null)
            {
                return NotFound();
            }

            var vaccineSchedule = await _context.VaccineSchedules
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccineSchedule == null)
            {
                return NotFound();
            }

            return View(vaccineSchedule);
        }

        // POST: Admin/VaccineSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VaccineSchedules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VaccineSchedules'  is null.");
            }
            var vaccineSchedule = await _context.VaccineSchedules.FindAsync(id);
            if (vaccineSchedule != null)
            {
                _context.VaccineSchedules.Remove(vaccineSchedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineScheduleExists(int id)
        {
          return (_context.VaccineSchedules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
