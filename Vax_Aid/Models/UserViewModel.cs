using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class UserViewModel
    {
        public VaccineInfo vaccineInfo { get; set; }
        public int VaccineInfoId { get; set; }
        public Address address { get; set; }
        public int AddressId { get; set; }
    }
}
