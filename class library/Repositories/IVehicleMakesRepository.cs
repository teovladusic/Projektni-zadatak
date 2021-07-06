using class_library.Helpers;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projektni_Zadatak_Project_Service.Repositories
{
    public interface IVehicleMakesRepository
    {
        Task<VehicleMake> GetVehicleMake(int id);
        Task<PagedList<VehicleMake>> GetVehicleMakes(VehicleMakesFilter vehicleMakesFilter, 
            PagingParams pagingParams, SortParams sortParams);
        public Task<List<VehicleMake>> GetAllVehicleMakes();
        public Task CreateVehicleMake(VehicleMake vehicleMake);
        public Task UpdateVehicleMake(VehicleMake vehicleMake);
        public Task DeleteItem(VehicleMake vehicleMake);
    }
}