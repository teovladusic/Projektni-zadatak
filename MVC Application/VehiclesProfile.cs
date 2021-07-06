using AutoMapper;
using MVC_Application.Models;
using Projektni_Zadatak_Project_Service.Models;
using System.Collections.Generic;
using System.Linq;

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

            CreateMap<VehicleModel, VehicleModelViewModel>()
                .ForMember(
                dest => dest.MakeName,
                options => options.MapFrom(source =>
                string.Join(
                    " ",
                    source.VehicleMake.Name)));
            CreateMap<VehicleModelViewModel, VehicleModel>();
        }
    }
}
