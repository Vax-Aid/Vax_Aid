using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vax_Aid.Data;
using Vax_Aid.Models;
using Vax_Aid.Service;
using Vax_Aid.ViewModels;

namespace Vax_Aid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                // get roles if login only
                // if roles = 3 (Vendor) go to home/VendorDashboard
                // if roels = 2 (Users) got to home/Patient User
                // else (Admin) go to home /adminIndex

               if (User.IsInRole("vendor"))
                {
                    return RedirectToAction("VendorDashboard", "VendorLocations");
                }
                if (User.IsInRole("user"))
                {
                    return RedirectToAction("UserDashboard", "UserDetails");
                }
                else
                {
                    return RedirectToAction("AdminDashboard", "Home");

                }

            }
            else
            {
                ViewData["VaccineInfoId"] = new SelectList(_context.VaccineInfos, "VaccineInfoId", "vaccineName");
                ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName");

            }
            return View();
        }

        [HttpPost]
        public IActionResult SearchNearestLocation(UserViewModelVM user)
        {
            var address = _context.Addresses.Where(x => x.AddressId == user.AddressId).FirstOrDefault();
            var vendor = _context.VendorLocation.
                Where(x => x.MappedVaccines.Contains(user.VaccineInfoId.ToString())).ToList();
            var vaccineinfo = _context.VaccineInfos.Where(x => x.VaccineInfoId == user.VaccineInfoId).FirstOrDefault();
            if (vaccineinfo != null)
            {
                ViewBag.VaccineInfoIdd = vaccineinfo.VaccineInfoId;
                ViewBag.VaccineName = vaccineinfo.vaccineName;
            }
            ViewData["VaccineInfoId"] = new SelectList(_context.VaccineInfos, "VaccineInfoId", "vaccineName");
            List<Locationinf> locationinfo = new List<Locationinf>();
            foreach (var item in vendor)
            {
                NearestNeighbour nearestNeighbour = new NearestNeighbour();
                var pointer = nearestNeighbour.getDistanceFromLatLonInKm(address.Latitude, address.Longitude, item.Latitude, item.Longitude);
                locationinfo.Add(new Locationinf
                {
                    VendorLocationId = item.VendorLocationId,
                    address = item.LocationName,
                    pointer = pointer
                });
                //get nearest location (address lat, address lon, item.lat, item.long)
                //pointer, 
            }

            locationinfo = locationinfo.OrderBy(x => x.pointer).Take(5).ToList();
            //locationinfo.Sort(x => x.pointer);

            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName");

            return View(locationinfo);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult AdminDashboard()
        {
            ViewData["Message"] = "Admin Dashboard.";
            return View();
        }
        public JsonResult GetDataForBarDiagram()
        {
            List<BarGraphVM> toReturn = new List<BarGraphVM>();
            string vandorEmail = User.Identity.Name;
            var user = _context.Users.Where(x => x.UserName == vandorEmail).FirstOrDefault();
            string id = user.Id;
            var vendor = _context.VendorLocation.Where(x => x.UserID == user.Id).FirstOrDefault();
            string query = @"select isnull(vl.LocationName,'--') as VendorName,case when FlowStatus = 0 then 'Unchecked' when FlowStatus = 1 then 'Vaccinated' when FlowStatus = 2 then 'Unvaccinated' else '--' end as FlowStatus,count(1) as TotalCount from UserDetails ud
                            left outer join VendorLocation vl on vl.VendorLocationId = ud.VendorLocationId
                            group by isnull(vl.LocationName,'--'),FlowStatus
                            order by VendorName";
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        toReturn.Add(new BarGraphVM()
                        {
                            VendorName = result.GetString(0),
                            FlowStatus = result.GetString(1),
                            TotalCount = result.GetInt32(2)
                        }) ;
                    }
                }
            }
            List<string> distinctVendorname = toReturn.Select(m => m.VendorName).Distinct().ToList();
            List<string> distinctFlowStatus = toReturn.Select(m => m.FlowStatus).Distinct().ToList();
            return Json(new
            {
                Success = true,
                Data = toReturn,
                DistinctVendorList = distinctVendorname,
                DistinctFlowStatus = distinctFlowStatus
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Locationinf
    {
        public double pointer { get; set; }
        public string address { get; set; }
        public int VendorLocationId { get; set; }
    }


}
