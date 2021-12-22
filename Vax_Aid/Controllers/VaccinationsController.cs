using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vax_Aid.Data;
using Vax_Aid.Models;

namespace Vax_Aid.Controllers
{
    public class VaccinationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaccinationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vaccinations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vaccinations.Include(v => v.userDetails).Include(v => v.vaccineInfo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vaccinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .Include(v => v.userDetails)
                .Include(v => v.vaccineInfo)
                .FirstOrDefaultAsync(m => m.VaccinationId == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // GET: Vaccinations/Create
        public IActionResult Create()
        {
            ViewData["UserDetailsId"] = new SelectList(_context.UserDetails, "UserDetailsId", "UserName");
            ViewData["VaccineId"] = new SelectList(_context.VaccineInfos, "VaccineId", "vaccineName");
            return View();
        }

        // POST: Vaccinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccinationId,VaccineId,SerialNumber,UserDetailsId")] Vaccination vaccination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserDetailsId"] = new SelectList(_context.UserDetails, "UserDetailsId", "UserName", vaccination.UserDetailsId);
            ViewData["VaccineId"] = new SelectList(_context.VaccineInfos, "VaccineId", "vaccineName", vaccination.VaccineId);
            return View(vaccination);
        }

        // GET: Vaccinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }
            ViewData["UserDetailsId"] = new SelectList(_context.UserDetails, "UserDetailsId", "UserName", vaccination.UserDetailsId);
            ViewData["VaccineId"] = new SelectList(_context.VaccineInfos, "VaccineId", "vaccineName", vaccination.VaccineId);
            return View(vaccination);
        }

        // POST: Vaccinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccinationId,VaccineId,SerialNumber,UserDetailsId")] Vaccination vaccination)
        {
            if (id != vaccination.VaccinationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationExists(vaccination.VaccinationId))
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
            ViewData["UserDetailsId"] = new SelectList(_context.UserDetails, "UserDetailsId", "UserName", vaccination.UserDetailsId);
            ViewData["VaccineId"] = new SelectList(_context.VaccineInfos, "VaccineId", "vaccineName", vaccination.VaccineId);
            return View(vaccination);
        }

        // GET: Vaccinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .Include(v => v.userDetails)
                .Include(v => v.vaccineInfo)
                .FirstOrDefaultAsync(m => m.VaccinationId == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        // POST: Vaccinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaccination = await _context.Vaccinations.FindAsync(id);
            _context.Vaccinations.Remove(vaccination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationExists(int id)
        {
            return _context.Vaccinations.Any(e => e.VaccinationId == id);
        }
    }
}
