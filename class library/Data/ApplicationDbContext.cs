using Microsoft.EntityFrameworkCore;
using Projektni_Zadatak_Project_Service.Models;

namespace Projektni_Zadatak_Project_Service.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }

}
