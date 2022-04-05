using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vax_Aid.Service;

namespace Vax_Aid.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public double GetDistance(double latitude, double longitude)
        {
          return  NearestNeighbour.getDistanceFromLatLonInKm(latitude, longitude, this.Latitude, this.Longitude);
        }
        
        
    }
}
