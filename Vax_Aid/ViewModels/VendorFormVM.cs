using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.ViewModels
{
    public class VendorFormVM
    {
        public int VendorLocationId { get; set; }
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MappedVaccines { get; set; }


        public string UserName { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
