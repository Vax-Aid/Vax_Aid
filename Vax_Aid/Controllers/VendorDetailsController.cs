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
    public class VendorDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VendorDetails.Include(v => v.VendorLocation).Include(v => v.vaccineInfo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VendorDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorDetails = await _context.VendorDetails
                .Include(v => v.VendorLocation)
                .Include(v => v.vaccineInfo)
                .FirstOrDefaultAsync(m => m.VendorDetailsId == id);
            if (vendorDetails == null)
            {
                return NotFound();
            }

            return View(vendorDetails);
        }

        // GET: VendorDetails/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.VendorLocation, "LocationId", "Municipality");
            ViewData["VaccineId"] = new SelectList(_context.VaccineInfos, "VaccineId", "vaccineName");
            return View();
        }

        // POST: VendorDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VendorDetailsId,VendorName,LocationId,VaccineAvailability,VaccineId")] VendorDetails vendorDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.VendorLocation, "LocationId", "Municipality", vendorDetails.LocationId);
            ViewData["VaccineId"] = new SelectList(_context.VaccineInfos, "VaccineId", "vaccineName", vendorDetails.VaccineId);
            return View(vendorDetails);
        }

        // GET: VendorDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorDetails = await _context.VendorDetails.FindAsync(id);
            if (vendorDetails == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.VendorLocation, "LocationId", "Municipality", vendorDetails.LocationId);
            ViewData["VaccineId"] = new SelectList(_context.VaccineInfos, "VaccineId", "vaccineName", vendorDetails.VaccineId);
            return View(vendorDetails);
        }

        // POST: VendorDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VendorDetailsId,VendorName,LocationId,VaccineAvailability,VaccineId")] VendorDetails vendorDetails)
        {
            if (id != vendorDetails.VendorDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorDetailsExists(vendorDetails.VendorDetailsId))
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
            ViewData["LocationId"] = new SelectList(_context.VendorLocation, "LocationId", "Municipality", vendorDetails.LocationId);
            ViewData["VaccineId"] = new SelectList(_context.VaccineInfos, "VaccineId", "vaccineName", vendorDetails.VaccineId);
            return View(vendorDetails);
        }

        // GET: VendorDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorDetails = await _context.VendorDetails
                .Include(v => v.VendorLocation)
                .Include(v => v.vaccineInfo)
                .FirstOrDefaultAsync(m => m.VendorDetailsId == id);
            if (vendorDetails == null)
            {
                return NotFound();
            }

            return View(vendorDetails);
        }

        // POST: VendorDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorDetails = await _context.VendorDetails.FindAsync(id);
            _context.VendorDetails.Remove(vendorDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorDetailsExists(int id)
        {
            return _context.VendorDetails.Any(e => e.VendorDetailsId == id);
        }
    }
}
