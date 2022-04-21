using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Vax_Aid.Data;
using Vax_Aid.Models;
using Vax_Aid.ViewModels;

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
        public async Task<IActionResult> Create(VendorFormVM vm)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Where(x => x.UserName == vm.UserName).Any())
                {
                    ViewBag.ErrorMessage = "Username Already Exists";
                    ViewBag.VaccineList = _context.VaccineInfos.Where(x => x.Delete == false)
                   .ToList();
                    return View(vm);
                }
                else
                {
                    Microsoft.AspNetCore.Identity.IdentityUser usr = new Microsoft.AspNetCore.Identity.IdentityUser()
                    {
                        UserName = vm.UserName,
                        PasswordHash = "AQAAAAEAACcQAAAAEHBOdnKIq1a7WGsmvBiJ4Bq3miRzhbnL7PVMPzHoY7JtqZ07qZr0EAoSmStXlx58SA==",
                        NormalizedUserName = vm.UserName,
                        Email = vm.Email,
                        NormalizedEmail = vm.Email,
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnabled = true,
                        AccessFailedCount = 0
                    };
                    _context.Users.Add(usr);

                    _context.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<string>()
                    {
                        RoleId = "3",
                        UserId = usr.Id
                    });

                    VendorLocation model = new VendorLocation()
                    {
                        VendorLocationId = vm.VendorLocationId,
                        LocationName = vm.LocationName,
                        Latitude = vm.Latitude,
                        Longitude = vm.Longitude,
                        MappedVaccines = vm.MappedVaccines,
                        UserID = usr.Id
                    };
                    _context.VendorLocation.Add(model);


                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
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
        public async Task<IActionResult> Edit(int id, VendorLocation vendorLocation)
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

        public IActionResult VendorDashboard()
        {

            string vandorEmail = User.Identity.Name;
            List<UserDetails> userDetailsList = new List<UserDetails>();
            var user = _context.Users.Where(x => x.UserName == vandorEmail).FirstOrDefault();
            if (user == null)
            {
                // validate user not login case
            }
            else
            {
                string id = user.Id;
            }
            var vendor = _context.VendorLocation.Where(x => x.UserID == user.Id).FirstOrDefault();
            if (vendor == null)
            {
                // validate vendor not found case
            }
            else
            {
                userDetailsList = _context.UserDetails.Where(x => x.VendorLocationId == vendor.VendorLocationId).Include(u => u.Address).Include(u => u.VaccineInfo).ToList();

            }


            ViewData["Message"] = "Vendor Dashboard.";
            return View(userDetailsList);
        }
        public JsonResult GetDataForPieChart()
        {
            List<PieChartVM> toReturn = new List<PieChartVM>();
            string vandorEmail = User.Identity.Name;
            var user = _context.Users.Where(x => x.UserName == vandorEmail).FirstOrDefault();
            string id = user.Id;
            var vendor = _context.VendorLocation.Where(x => x.UserID == user.Id).FirstOrDefault();
            string query = @"select case when FlowStatus = 0 then 'Unchecked' when FlowStatus = 1 then 'Vaccinated' when FlowStatus = 2 then 'Unvaccinated' else '--' end as FlowStatus ,
              count(1) as TotalCount from UserDetails
                where (@isAdmin = 1 or VendorLocationId = @vendorLocationID)
                group by FlowStatus";
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                SqlParameter param1 = new SqlParameter("vendorLocationID", vendor.VendorLocationId);
                SqlParameter param2 = new SqlParameter("isAdmin", 0);

                command.Parameters.Add(param1);
                command.Parameters.Add(param2);


                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        toReturn.Add(new PieChartVM()
                        {
                            FlowStatus = result.GetString(0),
                            TotalCount = result.GetInt32(1)
                        });
                    }
                }
            }
            return Json(new
            {
                Success =true,
                Data  = toReturn
            });
        }

        private bool VendorLocationExists(int id)
        {
            return _context.VendorLocation.Any(e => e.VendorLocationId == id);
        }
    }
}
