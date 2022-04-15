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
    public class VaccineInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VaccineInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VaccineInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.VaccineInfos.ToListAsync());
        }

        // GET: VaccineInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccineInfo = await _context.VaccineInfos
                .FirstOrDefaultAsync(m => m.VaccineInfoId == id);
            if (vaccineInfo == null)
            {
                return NotFound();
            }

            return View(vaccineInfo);
        }

        // GET: VaccineInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccineInfoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VaccineInfoId,vaccineName,CountryMade,DoseType,ManufacturedBy,ManufacturedDate,Delete")] VaccineInfo vaccineInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccineInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vaccineInfo);
        }

        // GET: VaccineInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccineInfo = await _context.VaccineInfos.FindAsync(id);
            if (vaccineInfo == null)
            {
                return NotFound();
            }
            return View(vaccineInfo);
        }

        // POST: VaccineInfoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VaccineInfoId,vaccineName,CountryMade,DoseType,ManufacturedBy,ManufacturedDate,Delete")] VaccineInfo vaccineInfo)
        {
            if (id != vaccineInfo.VaccineInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccineInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineInfoExists(vaccineInfo.VaccineInfoId))
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
            return View(vaccineInfo);
        }

        // GET: VaccineInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccineInfo = await _context.VaccineInfos
                .FirstOrDefaultAsync(m => m.VaccineInfoId == id);
            if (vaccineInfo == null)
            {
                return NotFound();
            }

            return View(vaccineInfo);
        }

        // POST: VaccineInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaccineInfo = await _context.VaccineInfos.FindAsync(id);
            _context.VaccineInfos.Remove(vaccineInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool VaccineInfoExists(int id)
        {
            return _context.VaccineInfos.Any(e => e.VaccineInfoId == id);
        }

        public JsonResult GetVendorMappedWithVaccine(int vaccineid)
        {
            bool isOk = false;
            List<VendorLocation> locs = new List<VendorLocation>();
            try
            {
                locs = _context.VendorLocation.Where(p => p.MappedVaccines.Contains(vaccineid.ToString())).ToList();
                isOk = true;
                if (locs.Count == 0)
                {
                    locs = new List<VendorLocation>();
                }
            }
            catch (Exception ex)
            {
                isOk = false;
            }
            var json = Json(new
            {
                Success = isOk,
                Data = locs
            });

            return json;
        }
    }
}
