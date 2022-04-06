using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
