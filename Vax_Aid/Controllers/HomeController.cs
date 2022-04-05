using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vax_Aid.Data;
using Vax_Aid.Models;
using Vax_Aid.Service;

namespace Vax_Aid.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAddressService _addressService;

        public HomeController(ApplicationDbContext context,IAddressService addressService)
        {
            _context = context;
            _addressService = addressService;
        }
        public IActionResult Index()
        {
            ViewData["VaccineInfoId"] = new SelectList(_context.VaccineInfos, "VaccineInfoId", "vaccineName");
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchNearestLocation(UserViewModel user)
        {

            var address = await _context.Addresses.Where(x => x.AddressId == user.AddressId).SingleOrDefaultAsync();
            var nearestAddresses = await _addressService.GetNearestLocationsAsync(address.Longitude, address.Latitude, 5);
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressName");
            return View(nearestAddresses);
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
