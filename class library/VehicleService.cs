using Microsoft.Extensions.Logging;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using Projektni_Zadatak_Project_Service.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace class_library
{
    public class VehicleService : IVehicleService
    {

        private readonly IVehicleMakesRepository _vehicleMakesRepository;
        private readonly IVehicleModelsRepository _vehicleModelsRepository;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(IVehicleMakesRepository vehicleMakesRepository, IVehicleModelsRepository vehicleModelsRepository,
            ILogger<VehicleService> logger)
        {
            _vehicleMakesRepository = vehicleMakesRepository;
            _vehicleModelsRepository = vehicleModelsRepository;
            _logger = logger;
        }

        public async  Task<PagedList<VehicleMake>> GetVehicleMakes(VehicleMakeParameters vehicleMakeParameters)
        {
            var vehicleMakes = await _vehicleMakesRepository.GetVehicleMakes(vehicleMakeParameters);
            return vehicleMakes;
        }

        public async Task<VehicleMake> GetVehicleMake(int id)
        {
            return await _vehicleMakesRepository.GetVehicleMake(id);
        }

        public async Task DeleteVehicleMake(VehicleMake vehicleMake)
        {
            await _vehicleMakesRepository.DeleteItem(vehicleMake);
        }

        public async Task CreateVehicleMake(VehicleMake vehicleMake)
        {
            await _vehicleMakesRepository.CreateVehicleMake(vehicleMake);
        }

        public async Task UpdateVehicleMake(VehicleMake vehicleMake)
        {
            await _vehicleMakesRepository.UpdateVehicleMake(vehicleMake);
        }




        public async Task<PagedList<VehicleModel>> GetVehicleModels(VehicleModelParameters vehicleModelParameters)
        {
            var vehicleModels = await _vehicleModelsRepository.GetVehicleModels(vehicleModelParameters);
            return vehicleModels;
        }

        public async Task<VehicleModel> GetVehicleModel(int id)
        {
            return await _vehicleModelsRepository.GetVehicleModel(id);
        }

        public async Task DeleteVehicleModel(VehicleModel vehicleModel)
        {
            await _vehicleModelsRepository.DeleteVehicleModel(vehicleModel);
        }

        public async Task CreateVehicleModel(VehicleModel vehicleModel)
        {
            await _vehicleModelsRepository.CreateVehicleModel(vehicleModel);
        }

        public async Task UpdateVehicleModel(VehicleModel vehicleModel)
        {
            await _vehicleModelsRepository.UpdateVehicleModel(vehicleModel);
        }
    }
}
