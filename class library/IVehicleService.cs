using class_library.Helpers;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace class_library
{
    public interface IVehicleService
    {
        public Task<PagedList<VehicleMake>> GetVehicleMakes(VehicleMakesFilter vehicleMakeFilter,
            PagingParams pagingParams, SortParams sortParams);
        public Task<VehicleMake> GetVehicleMake(int id);
        public Task DeleteVehicleMake(VehicleMake vehicleMake);
        public Task CreateVehicleMake(VehicleMake vehicleMake);
        public Task UpdateVehicleMake(VehicleMake vehicleMake);
        public Task<List<VehicleMake>> GetAllVehicleMakes();
        

        public Task<PagedList<VehicleModel>> GetVehicleModels(VehicleModelsFilter vehicleModelsFilter, SortParams sortParams, PagingParams pagingParams);
        public Task<VehicleModel> GetVehicleModel(int id);
        public Task DeleteVehicleModel(VehicleModel vehicleModel);
        public Task CreateVehicleModel(VehicleModel vehicleModel);
        public Task UpdateVehicleModel(VehicleModel vehicleModel);
    }
}
