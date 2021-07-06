using Projektni_Zadatak_Project_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace class_library.Helpers
{
    public class VehicleMakesFilter
    {
        public string SearchQuery { get; set; }

        public VehicleMakesFilter(string SearchQuery)
        {
            this.SearchQuery = SearchQuery;
        }
       
    }
}
