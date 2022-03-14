using KDSharp.KDTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vax_Aid.Models;

namespace Vax_Aid.Service
{
    public class NearestNeighbour
    {
        public List<Location> GetTopFiveNeighbours(string sourceLocationName)
        {
            var placesFromdb = new List<Location>();
            var places = placesFromdb.Where(x => x.Source == sourceLocationName)
                            .OrderBy(x => x.Distance)
                            .Take(5)
                            .ToList();

            return places;
        }

        //public void BuildTree()
        //{
        //    KDTree<Location> kd = new KDTree<Location>(2);

        //    kd.

        //}
    }
}
