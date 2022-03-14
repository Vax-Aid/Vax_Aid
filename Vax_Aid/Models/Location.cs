using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vax_Aid.Models
{
    public class LocationList
    {
        public static List<Location> Places { get; set; }
        public Location ReferenceLocation { get; set; }
    }

    public class Location
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public float Distance { get; set; }
    }
}
