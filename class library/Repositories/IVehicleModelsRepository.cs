using class_library.Helpers;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Threading.Tasks;

namespace Projektni_Zadatak_Project_Service.Repositories
{
    public interface IVehicleModelsRepository
    {
        Task<VehicleModel> GetVehicleModel(int id);
        Task<PagedList<VehicleModel>> GetVehicleModels(VehicleModelsFilter vehicleModelsFilter, SortParams sortParams, PagingParams pagingParams);
        public Task CreateVehicleModel(VehicleModel vehicleModel);
        public Task UpdateVehicleModel(VehicleModel vehicleModel);
        public Task DeleteVehicleModel(VehicleModel vehicleModel);
    }
}
