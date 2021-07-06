using Projektni_Zadatak_Project_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Application.Models
{
    public class VehicleMakeParams : QueryStringParameters
    {
        public string SearchQuery { get; set; }
    }
}
