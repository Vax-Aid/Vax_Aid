﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class VendorDetails
    {
        [Key]
        public int VendorDetailsId { get; set; }
        public string VendorName { get; set; }
        public string VendorLocation { get; set; }
        public bool VaccineAvailability { get; set; }
        public VaccineInfo vaccineInfo { get; set; }
        public int VaccineId { get; set; }
    }
}
