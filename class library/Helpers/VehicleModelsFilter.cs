using Projektni_Zadatak_Project_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_library.Helpers
{
    public class VehicleModelsFilter
    {
        public string SearchQuery { get; set; }
        public string MakeName { get; set; }

        public VehicleModelsFilter(string searchQuery, string makeName)
        {
            SearchQuery = searchQuery;
            MakeName = makeName;
        }
    }
}
