namespace Projektni_Zadatak_Project_Service.Models
{
    public class VehicleModelParameters : QueryStringParameters
    {
        public string SearchQuery { get; set; }
        public string MakeName { get; set; }

    }
}
