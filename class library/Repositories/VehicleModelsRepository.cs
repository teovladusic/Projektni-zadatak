using class_library.Helpers;
using Microsoft.EntityFrameworkCore;
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
            _context.Add(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleModel(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Remove(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task<VehicleModel> GetVehicleModel(int id)
        {
            return await _context.VehicleModels.Include(m => m.VehicleMake).FirstAsync(m => m.Id == id);
        }

        public async Task<PagedList<VehicleModel>> GetVehicleModels(VehicleModelsFilter vehicleModelsFilter,
            SortParams sortParams, PagingParams pagingParams)
        {
            var models = _context.VehicleModels.Include(m => m.VehicleMake).AsQueryable();

            if (!string.IsNullOrWhiteSpace(vehicleModelsFilter.SearchQuery))
            {
                models = models.Where(model => model.Name.Contains(vehicleModelsFilter.SearchQuery)
                || model.Abrv.Contains(vehicleModelsFilter.SearchQuery));
            }

            if (!string.IsNullOrEmpty(vehicleModelsFilter.MakeName))
            {
                var make = _context.VehicleMakes.Where(make => make.Name == vehicleModelsFilter.MakeName).FirstOrDefault();
                models = models.Where(model => model.VehicleMakeId == make.Id);
            }

            var sortedModels = _sortHelper.ApplySort(models, sortParams.OrderBy);

            return await PagedList<VehicleModel>.ToPagedList(
                sortedModels,
                pagingParams.CurrentPage,
                pagingParams.PageSize,
                sortedModels.Count());
        }

        public async Task UpdateVehicleModel(VehicleModel vehicleModel)
        {
            _context.Update(vehicleModel);
            await _context.SaveChangesAsync();
        }
    }
}
