﻿using KDSharp.KDTree;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vax_Aid.Models;

namespace Vax_Aid.Service
{
    public class NearestNeighbour
    {

        public double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = deg2rad(lat2 - lat1);  // deg2rad below
            var dLon = deg2rad(lon2 - lon1);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        public double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
    


//    for (let i = 0; i<DataInDB.length; i++)
//    {
//        let distanceInKM = getDistanceFromLatLonInKm(
//            DataInDB[i].latitude,
//            DataInDB[i].longitude,
//            UserInput.latitude,
//            UserInput.longitude
//            );
//        DataInDB[i].pointer = distanceInKM;
//    }

