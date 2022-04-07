using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class UserViewModel
    {
        public VaccineInfo vaccineInfo { get; set; }

        [Required]
        public int VaccineInfoId { get; set; }
        public Address address { get; set; }
        
        [Required]
        public int AddressId { get; set; }
    }
}
