using System.Threading.Tasks;

namespace Vax_Aid.Service;

public interface IAddressService
{
    Task<dynamic> GetNearestLocationsAsync(double longitude, double latitude,int count);
}