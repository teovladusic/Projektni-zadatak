using AutoMapper;
using MVC_Application.Models;
using Projektni_Zadatak_Project_Service.Models;

namespace MVC_Application
{
    public class VehiclesProfile : Profile
    {
        public VehiclesProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<VehicleMakeViewModel, VehicleMake>();

            CreateMap<CreateVehicleModelViewModel, VehicleModel>();
            CreateMap<VehicleModel, CreateVehicleModelViewModel>();
        }
    }
}
