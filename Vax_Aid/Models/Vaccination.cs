using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class Vaccination
    {
        public int VaccinationId { get; set; }
        public VaccineInfo vaccineInfo { get; set; }
        public int VaccineInfoId { get; set; }
        public string SerialNumber { get; set; }
        public UserDetails userDetails { get; set; }
        public int UserDetailsId { get; set; }
    }
}
