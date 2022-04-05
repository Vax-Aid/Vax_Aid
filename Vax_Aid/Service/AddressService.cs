using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vax_Aid.Data;

namespace Vax_Aid.Service;

public class AddressService:IAddressService
{
    private readonly ApplicationDbContext _context;
    public AddressService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<dynamic> GetNearestLocationsAsync(double longitude, double latitude,int count = 5)
    {
        const double constValue = 57.2957795130823D;

        const double constValue2 = 3958.75586574D;
        var addresses = await (from l in _context.Addresses
            let temp = Math.Sin(Convert.ToDouble(l.Latitude) / constValue) *  Math.Sin(Convert.ToDouble(latitude) / constValue) +
                       Math.Cos(Convert.ToDouble(l.Latitude) / constValue) *
                       Math.Cos(Convert.ToDouble(latitude) / constValue) *
                       Math.Cos((Convert.ToDouble(longitude) / constValue) - (Convert.ToDouble(l.Longitude) / constValue))
            let distance = (constValue2 * Math.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))*1.60934
            let distanceInKm = Math.Round(distance, 2, MidpointRounding.AwayFromZero)
            orderby distance
            select new 
            {
                l.AddressId,
                l.Longitude,
                l.Latitude,
                l.AddressName,
                distanceInKm,
            }).ToListAsync();
        return addresses;
    }
}