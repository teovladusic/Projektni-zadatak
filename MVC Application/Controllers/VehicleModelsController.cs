using AutoMapper;
using class_library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Application.Models;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Application.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly ILogger<VehicleModelsController> _logger;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleModelsController(IVehicleService vehicleService, IMapper mapper, ILogger<VehicleModelsController> logger)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET vehiclemodels
        [HttpGet("[controller]")]
        public async Task<IActionResult> Index([FromQuery] VehicleModelParameters vehicleModelParameters)
        {
            ViewData["MakeName"] = (string.IsNullOrEmpty(vehicleModelParameters.MakeName))
                ? "Select Make" : vehicleModelParameters.MakeName;

            ViewData["OrderBy"] = (string.IsNullOrEmpty(vehicleModelParameters.OrderBy))
                ? "Order By" : vehicleModelParameters.OrderBy;

            ViewData["SearchQuery"] = vehicleModelParameters.SearchQuery;

            ViewData["PageSize"] = vehicleModelParameters.PageSize;

            if (vehicleModelParameters.OrderBy == "Order By")
            {
                vehicleModelParameters.OrderBy = null;
            }

            if (vehicleModelParameters.MakeName == "Select Make")
            {
                vehicleModelParameters.MakeName = null;
            }

            var vehicleModels = await _vehicleService.GetVehicleModels(vehicleModelParameters);

            List<VehicleModelViewModel> vehicleModelViewModels = new();
            var vehicleMakes = await _vehicleService.GetVehicleMakes(new());

            foreach (VehicleModel vehicleModel in vehicleModels)
            {
                var vehicleMake = await _vehicleService.GetVehicleMake(vehicleModel.MakeId);
                if (vehicleMake is null)
                {
                    continue;
                }
                VehicleModelViewModel vehicleModelViewModel =
                    VehicleModelViewModel.ToVehicleModelViewModel(vehicleModel, vehicleMake);
                vehicleModelViewModels.Add(vehicleModelViewModel);
            }

            PagedList<VehicleModelViewModel> pagedVehicleModelViewModels = new(vehicleModelViewModels, 
                vehicleModels.TotalCount, vehicleModelParameters.PageNumber, vehicleModelParameters.PageSize);

            ListVehicleModelViewModel listVehicleModelViewModel = new()
            {
                VehicleMakes = vehicleMakes.ToList(),
                VehicleModelViewModels = pagedVehicleModelViewModels
            };

            return View(listVehicleModelViewModel);
        }

        // GET /vehiclemodels/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleService.GetVehicleModel((int)id);

            if (vehicleModel is null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleService.GetVehicleMake(vehicleModel.MakeId);

            if (vehicleMake is null)
            {
                return NotFound();
            }

            VehicleModelViewModel vehicleModelViewModel =
                VehicleModelViewModel.ToVehicleModelViewModel(vehicleModel, vehicleMake);

            return View(vehicleModelViewModel);
        }

        // GET /vehicleModels/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleService.GetVehicleModel((int)id);

            if (vehicleModel is null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleService.GetVehicleMake(vehicleModel.MakeId);

            if (vehicleMake is null)
            {
                return NotFound();
            }

            var vehicleMakeViewModel = VehicleModelViewModel.ToVehicleModelViewModel(vehicleModel, vehicleMake);

            return View(vehicleMakeViewModel);
        }

        // POST 
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleModel = await _vehicleService.GetVehicleModel(id);

            if (vehicleModel is null)
            {
                return NotFound();
            }

            await _vehicleService.DeleteVehicleModel(vehicleModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST vehicleModels/Create
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Name,Abrv,MakeId")] CreateVehicleModelViewModel createVehicleModelViewModel)
        {
            if (string.IsNullOrEmpty(createVehicleModelViewModel.Name) || string.IsNullOrEmpty(createVehicleModelViewModel.Abrv))
            {
                return View(createVehicleModelViewModel);
            }

            var vehicleModel = _mapper.Map<VehicleModel>(createVehicleModelViewModel);

            await _vehicleService.CreateVehicleModel(vehicleModel);
            return RedirectToAction(nameof(Index));
        }

        // GET vehicleModels/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleService.GetVehicleModel((int)id);

            if (vehicleModel is null)
            {
                return NotFound();
            }

            var createVehicleMakeViewModel = _mapper.Map<CreateVehicleModelViewModel>(vehicleModel);

            return View(createVehicleMakeViewModel);
        }

        // POST vehicleModels/Edit
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit([Bind("Id,Name,Abrv,MakeId")] CreateVehicleModelViewModel createVehicleModelViewModel)
        {
            if (string.IsNullOrEmpty(createVehicleModelViewModel.Name) || string.IsNullOrEmpty(createVehicleModelViewModel.Abrv))
            {
                return NotFound();
            }

            var vehicleModel = _mapper.Map<VehicleModel>(createVehicleModelViewModel);
            await _vehicleService.UpdateVehicleModel(vehicleModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
