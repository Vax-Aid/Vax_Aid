using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class VendorLocation
    {
        [Key]
        public int VendorLocationId { get; set; }
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string MappedVaccines { get; set; }

        public string UserID { get; set; }

    }
}
