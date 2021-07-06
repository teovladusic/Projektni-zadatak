using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projektni_Zadatak_Project_Service.Models
{
    public class VehicleMake
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }

        public List<VehicleModel> VehicleModels { get; set; }
    }
}
