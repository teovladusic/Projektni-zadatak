using class_library.Helpers;
using Microsoft.EntityFrameworkCore;
using Projektni_Zadatak_Project_Service.Data;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Collections.Generic;
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

        public async Task<List<VehicleMake>> GetAllVehicleMakes()
        {
            return await _context.VehicleMakes.ToListAsync();
        }

        public async Task<PagedList<VehicleMake>> GetVehicleMakes(VehicleMakesFilter vehicleMakesFilter, 
            PagingParams pagingParams, SortParams sortParams)
        {
            var vehicleMakes = from v in _context.VehicleMakes select v;

            if (!string.IsNullOrWhiteSpace(vehicleMakesFilter.SearchQuery))
            {
                vehicleMakes = _context.VehicleMakes.Where(make => make.Name.Contains(vehicleMakesFilter.SearchQuery)
            || make.Abrv.Contains(vehicleMakesFilter.SearchQuery));
            }

            var sortedMakes = _sortHelper.ApplySort(vehicleMakes, sortParams.OrderBy);

            return await PagedList<VehicleMake>.ToPagedList(
                sortedMakes,
                pagingParams.CurrentPage,
                pagingParams.PageSize,
                sortedMakes.Count());
        }

        public async Task<VehicleMake> GetVehicleMake(int id)
        {
            return await _context.VehicleMakes.FindAsync(id);
        }

        public async Task CreateVehicleMake(VehicleMake vehicleMake)
        {
            _context.VehicleMakes.Add(vehicleMake);
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