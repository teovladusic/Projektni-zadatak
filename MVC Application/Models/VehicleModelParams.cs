using Projektni_Zadatak_Project_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Application.Models
{
    public class VehicleModelParams : QueryStringParameters
    {
        public string SearchQuery { get; set; }
        public string MakeName { get; set; }
    }
}
