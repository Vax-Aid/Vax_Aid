using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vax_Aid.Data;
using Vax_Aid.Models;
using Vax_Aid.Service;

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
            ViewData["VaccineInfoId"] = new SelectList(_context.VaccineInfos, "VaccineInfoId", "vaccineName");
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName");

            return View();
        }

        [HttpPost]
        public IActionResult SearchNearestLocation(UserViewModelVM user)
        {
            var address =_context.Addresses.Where(x => x.AddressId == user.AddressId).FirstOrDefault();
            var vendor = _context.VendorLocation.
                Where(x => x.MappedVaccines.Contains( user.VaccineInfoId.ToString())).ToList();
            var vaccineinfo = _context.VaccineInfos.Where(x => x.VaccineInfoId == user.VaccineInfoId).FirstOrDefault();
            if(vaccineinfo != null)
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

        public IActionResult Privacy()
        {
            return View();
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
