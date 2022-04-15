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
    public class VendorLocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorLocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorLocations
        public async Task<IActionResult> Index()
        {
            return View(await _context.VendorLocation.ToListAsync());
        }

        // GET: VendorLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorLocation = await _context.VendorLocation
                .FirstOrDefaultAsync(m => m.VendorLocationId == id);
            if (vendorLocation == null)
            {
                return NotFound();
            }
            ViewBag.VaccineList = _context.VaccineInfos.Where(x => x.Delete == false)
                    .ToList();

            return View(vendorLocation);
        }

        // GET: VendorLocations/Create
        public IActionResult Create()
        {
            ViewBag.VaccineList = _context.VaccineInfos.Where(x => x.Delete == false)
                    .ToList();
            return View();
        }

        // POST: VendorLocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorLocationId,LocationName,Latitude,Longitude,MappedVaccines")] VendorLocation vendorLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendorLocation);
        }

        // GET: VendorLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorLocation = await _context.VendorLocation.FindAsync(id);
            if (vendorLocation == null)
            {
                return NotFound();
            }
            ViewBag.VaccineList = _context.VaccineInfos.Where(x => x.Delete == false)
                    .ToList();
            return View(vendorLocation);
        }

        // POST: VendorLocations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorLocationId,LocationName,Latitude,Longitude,MappedVaccines")] VendorLocation vendorLocation)
        {
            if (id != vendorLocation.VendorLocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorLocationExists(vendorLocation.VendorLocationId))
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
            return View(vendorLocation);
        }

        // GET: VendorLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorLocation = await _context.VendorLocation
                .FirstOrDefaultAsync(m => m.VendorLocationId == id);
            if (vendorLocation == null)
            {
                return NotFound();
            }

            return View(vendorLocation);
        }

        // POST: VendorLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorLocation = await _context.VendorLocation.FindAsync(id);
            _context.VendorLocation.Remove(vendorLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorLocationExists(int id)
        {
            return _context.VendorLocation.Any(e => e.VendorLocationId == id);
        }
    }
}
