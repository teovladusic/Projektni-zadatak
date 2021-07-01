using Projektni_Zadatak_Project_Service.Models;

namespace MVC_Application.Models
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public string MakeName { get; set; }

        public static VehicleModelViewModel ToVehicleModelViewModel(VehicleModel vehicleModel, VehicleMake vehicleMake)
        {
            return new()
            {
                Id = vehicleModel.Id,
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                MakeName = vehicleMake.Name
            };
        }
    }
}
