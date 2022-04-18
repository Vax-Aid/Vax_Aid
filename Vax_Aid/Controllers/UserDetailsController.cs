using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vax_Aid.Data;
using Vax_Aid.Models;
using Vax_Aid.ViewModels;


namespace Vax_Aid.Controllers
{

    public class UserDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserDetails

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserDetails.Include(u => u.Address).Include(u => u.VaccineInfo)
                .Include(u => u.VendorLocation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails
                .Include(u => u.Address)
                .Include(u => u.VaccineInfo)
                .Include(u => u.VendorLocation)
                .FirstOrDefaultAsync(m => m.UserDetailsId == id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        // GET: UserDetails/Create
        public IActionResult Create(UserViewModelVM vm)
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName", vm.AddressId);

            var vaccineinfo = _context.VaccineInfos.Where(x => x.VaccineInfoId == vm.VaccineInfoId).FirstOrDefault();
            if (vaccineinfo != null)
            {
                ViewBag.VaccineInfoId = vaccineinfo.VaccineInfoId;
                ViewBag.VaccineName = vaccineinfo.vaccineName;
            }
            var vendorlocation = _context.VendorLocation.Where(x => x.VendorLocationId == vm.VendorLocationId).FirstOrDefault();
            if (vendorlocation != null)
            {
                ViewBag.VendorLocationId = vendorlocation.VendorLocationId;
                ViewBag.LocationName = vendorlocation.LocationName;
            }
            return View();
        }

        public ActionResult CustomCreate(int vaccineInfoID, int vendorLocationID)
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName", "");

            var vaccineinfo = _context.VaccineInfos.Where(x => x.VaccineInfoId == vaccineInfoID).FirstOrDefault();
            if (vaccineinfo != null)
            {
                ViewBag.VaccineInfoId = vaccineinfo.VaccineInfoId;
                ViewBag.VaccineName = vaccineinfo.vaccineName;
            }
            var vendorlocation = _context.VendorLocation.Where(x => x.VendorLocationId == vendorLocationID).FirstOrDefault();
            if (vendorlocation != null)
            {
                ViewBag.VendorLocationId = vendorlocation.VendorLocationId;
                ViewBag.LocationName = vendorlocation.LocationName;
            }
            return View();

        }

        // POST: UserDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserDetailsId,UserName,Ethnicity,Gender,DateofBirth,AddressId,Email,Phone,Nationality,IdentityType,IdentityNo,Occupation,MedicalConditions,DoseType,Disability")] UserDetails userDetails)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(userDetails);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Details", new { id = userDetails.UserDetailsId });
        //    }
        //    ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName", userDetails.AddressId);
        //    return View(userDetails);
        //}
        public IActionResult Create(UserFormVM vm)
        {

            UserDetails model = new UserDetails()
            {
                UserDetailsId = vm.UserDetailsId,
                UserName = vm.UserName,
                Ethnicity = vm.Ethnicity,
                Gender = vm.Gender,
                DateofBirth = vm.DateofBirth,
                AddressId = vm.AddressId,
                Email = vm.Email,
                Phone = vm.Phone,
                Nationality = vm.Nationality,
                IdentityType = vm.IdentityType,
                IdentityNo = vm.IdentityNo,
                Occupation = vm.Occupation,
                MedicalConditions = vm.MedicalConditions,
                DoseType = vm.DoseType,
                Disability = vm.Disability,
                VaccineInfoId = vm.VaccineInfoId,
                VendorLocationId = vm.VendorLocationId
            };
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = model.UserDetailsId });

            }
            return View(vm);

        }

        // GET: UserDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails.FindAsync(id);
            if (userDetails == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName", userDetails.AddressId);
            return View(userDetails);
        }

        // POST: UserDetails/Edit/5.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDetails userDetails)
        {
            if (id != userDetails.UserDetailsId)
            {
                return NotFound();
            }

            
            
                try
                {
                    _context.Update(userDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailsExists(userDetails.UserDetailsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = userDetails.UserDetailsId });
            
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName", userDetails.AddressId);
            return View(userDetails);
        }

        // GET: UserDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetails = await _context.UserDetails
                .Include(u => u.Address)
                .FirstOrDefaultAsync(m => m.UserDetailsId == id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userDetails = await _context.UserDetails.FindAsync(id);
            _context.UserDetails.Remove(userDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult UserDashboard()
        {
            ViewData["Message"] = "User Dashboard.";
            return View();
        }
        private bool UserDetailsExists(int id)
        {
            return _context.UserDetails.Any(e => e.UserDetailsId == id);
        }

        public JsonResult GetUserDetailsInfoByID(int userid)
        {
            var userDetailInfo = _context.UserDetails.Where(x => x.UserDetailsId == userid).FirstOrDefault();
            if (userDetailInfo != null)
            {
                return Json(new
                {
                    Success = true,
                    Data = userDetailInfo
                });
            }
            else
            {
                return Json(new
                {
                    Success = false,
                    Message = "Data Not Found."
                });
            }
        }
    }
}
