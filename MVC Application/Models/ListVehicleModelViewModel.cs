using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Collections.Generic;

namespace MVC_Application.Models
{
    public class ListVehicleModelViewModel
    {
        public PagedList<VehicleModelViewModel> VehicleModelViewModels { get; set; }
        public List<VehicleMake> VehicleMakes { get; set; }
    }
}
