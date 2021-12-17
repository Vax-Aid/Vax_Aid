using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class VaccineInfo
    {
        [Key]
        public int VaccineId { get; set; }
        [Required]
        [StringLength(255)]
        public string vaccineName { get; set; }
        public string CountryMade { get; set; }
        public string DoseType { get; set; }
        public string ManufacturedBy { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public bool Delete { get; set; }
    }
}
