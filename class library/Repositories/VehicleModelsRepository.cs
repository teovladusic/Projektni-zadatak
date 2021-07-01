using Microsoft.Extensions.Logging;
using Projektni_Zadatak_Project_Service.Data;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Linq;
using System.Threading.Tasks;


namespace Projektni_Zadatak_Project_Service.Repositories
{
    public class VehicleModelsRepository : IVehicleModelsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISortHelper<VehicleModel> _sortHelper;
        private readonly ILogger<VehicleModelsRepository> _logger;

        public VehicleModelsRepository(ApplicationDbContext context, ISortHelper<VehicleModel> sortHelper,
            ILogger<VehicleModelsRepository> logger)
        {
            _context = context;
            _sortHelper = sortHelper;
            _logger = logger;
        }

        public async Task CreateVehicleModel(VehicleModel vehicleModel)
        {
            VehicleModel newVehicleModel = new VehicleModel
            {
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                MakeId = vehicleModel.MakeId
            };
            _context.Add(newVehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleModel(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Remove(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task<VehicleModel> GetVehicleModel(int id)
        {
            return await _context.VehicleModels.FindAsync(id);
        }

        public async Task<PagedList<VehicleModel>> GetVehicleModels(VehicleModelParameters vehicleModelParameters)
        {
            var models = from v in _context.VehicleModels select v;

            if (!string.IsNullOrWhiteSpace(vehicleModelParameters.SearchQuery))
            {
                models = models.Where(model => model.Name.Contains(vehicleModelParameters.SearchQuery)
                || model.Abrv.Contains(vehicleModelParameters.SearchQuery));
            }

            if (!string.IsNullOrEmpty(vehicleModelParameters.MakeName))
            {
                var make = _context.VehicleMakes.Where(make => make.Name == vehicleModelParameters.MakeName).FirstOrDefault();
                models = models.Where(model => model.MakeId == make.Id);
            }

            var sortedModels = _sortHelper.ApplySort(models, vehicleModelParameters.OrderBy);

            return await PagedList<VehicleModel>.ToPagedList(
                sortedModels,
                vehicleModelParameters.PageNumber,
                vehicleModelParameters.PageSize,
                sortedModels.Count());
        }

        public async Task UpdateVehicleModel(VehicleModel vehicleModel)
        {
            _context.Update(vehicleModel);
            await _context.SaveChangesAsync();
        }
    }
}
