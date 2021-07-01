using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Threading.Tasks;

namespace Projektni_Zadatak_Project_Service.Repositories
{
    public interface IVehicleMakesRepository
    {
        Task<VehicleMake> GetVehicleMake(int id);
        Task<PagedList<VehicleMake>> GetVehicleMakes(VehicleMakeParameters vehicleMakeParameters);
        public Task CreateVehicleMake(VehicleMake vehicleMake);
        public Task UpdateVehicleMake(VehicleMake vehicleMake);
        public Task DeleteItem(VehicleMake vehicleMake);
    }
}