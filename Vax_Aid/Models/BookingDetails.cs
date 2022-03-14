using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class BookingDetails
    {
        
        [Key]
        public int BookingDetailsId { get; set; }
        public VaccineInfo vaccineInfo { get; set; }
        public int VaccineInfoId { get; set; }
        public string Dose { get; set; }
        public UserDetails UserDetails { get; set; }
        public int UserDetailsId { get; set; }
        public VendorLocation Location { get; set; }
        public int VendorLocationId { get; set; }
        public bool Conformation { get; set; }
    }
}
