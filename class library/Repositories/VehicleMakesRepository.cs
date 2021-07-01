using Projektni_Zadatak_Project_Service.Data;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Projektni_Zadatak_Project_Service.Repositories
{
    public class VehicleMakesRepository : IVehicleMakesRepository
    {
        private readonly ApplicationDbContext _context;
        private ISortHelper<VehicleMake> _sortHelper;

        public VehicleMakesRepository(ApplicationDbContext context, ISortHelper<VehicleMake> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<VehicleMake>> GetVehicleMakes(VehicleMakeParameters vehicleMakeParameters)
        {
            var vehicleMakes = from v in _context.VehicleMakes select v;

            if (!string.IsNullOrWhiteSpace(vehicleMakeParameters.SearchQuery))
            {
                vehicleMakes = _context.VehicleMakes.Where(make => make.Name.Contains(vehicleMakeParameters.SearchQuery)
            || make.Abrv.Contains(vehicleMakeParameters.SearchQuery));
            }

            var sortedMakes = _sortHelper.ApplySort(vehicleMakes, vehicleMakeParameters.OrderBy);

            return await PagedList<VehicleMake>.ToPagedList(
                sortedMakes,
                vehicleMakeParameters.PageNumber,
                vehicleMakeParameters.PageSize,
                sortedMakes.Count());
        }

        public async Task<VehicleMake> GetVehicleMake(int id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task CreateVehicleMake(VehicleMake vehicleMake)
        {
            var newVehicleMake = new VehicleMake
            {
                Name = vehicleMake.Name,
                Abrv = vehicleMake.Abrv
            };
            _context.VehicleMakes.Add(newVehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVehicleMake(VehicleMake vehicleMake)
        {
            _context.Update(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(VehicleMake vehicleMake)
        {
            _context.Remove(vehicleMake);
            await _context.SaveChangesAsync();
        }
    }
}