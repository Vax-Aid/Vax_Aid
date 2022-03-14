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
    public class BookingDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookingDetails.Include(b => b.Location).Include(b => b.UserDetails).Include(b => b.vaccineInfo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookingDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails
                .Include(b => b.Location)
                .Include(b => b.UserDetails)
                .Include(b => b.vaccineInfo)
                .FirstOrDefaultAsync(m => m.BookingDetailsId == id);
            if (bookingDetails == null)
            {
                return NotFound();
            }

            return View(bookingDetails);
        }

        // GET: BookingDetails/Create
        public IActionResult Create()
        {
            ViewData["VendorLocationId"] = new SelectList(_context.VendorLocation, "VendorLocationId", "Municipality");
            ViewData["UserDetailsId"] = new SelectList(_context.UserDetails, "UserDetailsId", "UserName");
            ViewData["VaccineInfoId"] = new SelectList(_context.VaccineInfos, "VaccineInfoId", "vaccineName");
            return View();
        }

        // POST: BookingDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingDetailsId,VaccineInfoId,Dose,UserDetailsId,VendorLocationId,Conformation")] BookingDetails bookingDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorLocationId"] = new SelectList(_context.VendorLocation, "VendorLocationId", "Municipality", bookingDetails.VendorLocationId);
            ViewData["UserDetailsId"] = new SelectList(_context.UserDetails, "UserDetailsId", "UserName", bookingDetails.UserDetailsId);
            ViewData["VaccineInfoId"] = new SelectList(_context.VaccineInfos, "VaccineInfoId", "vaccineName", bookingDetails.VaccineInfoId);
            return View(bookingDetails);
        }

        // GET: BookingDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails.FindAsync(id);
            if (bookingDetails == null)
            {
                return NotFound();
            }
            ViewData["VendorLocationId"] = new SelectList(_context.VendorLocation, "VendorLocationId", "Municipality", bookingDetails.VendorLocationId);
            ViewData["UserDetailsId"] = new SelectList(_context.UserDetails, "UserDetailsId", "UserName", bookingDetails.UserDetailsId);
            ViewData["VaccineInfoId"] = new SelectList(_context.VaccineInfos, "VaccineInfoId", "vaccineName", bookingDetails.VaccineInfoId);
            return View(bookingDetails);
        }

        // POST: BookingDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingDetailsId,VaccineInfoId,Dose,UserDetailsId,VendorLocationId,Conformation")] BookingDetails bookingDetails)
        {
            if (id != bookingDetails.BookingDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingDetailsExists(bookingDetails.BookingDetailsId))
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
            ViewData["VendorLocationId"] = new SelectList(_context.VendorLocation, "VendorLocationId", "Municipality", bookingDetails.VendorLocationId);
            ViewData["UserDetailsId"] = new SelectList(_context.UserDetails, "UserDetailsId", "UserName", bookingDetails.UserDetailsId);
            ViewData["VaccineInfoId"] = new SelectList(_context.VaccineInfos, "VaccineInfoId", "vaccineName", bookingDetails.VaccineInfoId);
            return View(bookingDetails);
        }

        // GET: BookingDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingDetails = await _context.BookingDetails
                .Include(b => b.Location)
                .Include(b => b.UserDetails)
                .Include(b => b.vaccineInfo)
                .FirstOrDefaultAsync(m => m.BookingDetailsId == id);
            if (bookingDetails == null)
            {
                return NotFound();
            }

            return View(bookingDetails);
        }

        // POST: BookingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingDetails = await _context.BookingDetails.FindAsync(id);
            _context.BookingDetails.Remove(bookingDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingDetailsExists(int id)
        {
            return _context.BookingDetails.Any(e => e.BookingDetailsId == id);
        }
    }
}
