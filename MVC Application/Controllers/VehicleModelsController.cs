using AutoMapper;
using class_library;
using class_library.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Application.Models;
using Projektni_Zadatak_Project_Service.Helpers;
using Projektni_Zadatak_Project_Service.Models;
using System;
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
        public async Task<IActionResult> Index([FromQuery] VehicleModelParams vehicleModelParams)
        {
            ViewData["MakeName"] = (string.IsNullOrEmpty(vehicleModelParams.MakeName))
                ? "Select Make" : vehicleModelParams.MakeName;

            ViewData["OrderBy"] = (string.IsNullOrEmpty(vehicleModelParams.OrderBy))
                ? "Order By" : vehicleModelParams.OrderBy;

            ViewData["SearchQuery"] = vehicleModelParams.SearchQuery;

            ViewData["PageSize"] = vehicleModelParams.PageSize;

            if (vehicleModelParams.OrderBy == "Order By")
            {
                vehicleModelParams.OrderBy = null;
            }

            if (vehicleModelParams.MakeName == "Select Make")
            {
                vehicleModelParams.MakeName = null;
            }

            VehicleModelsFilter vehicleModelsFilters = new(vehicleModelParams.SearchQuery, vehicleModelParams.MakeName); 
            SortParams sortParams = new(vehicleModelParams.OrderBy);
            PagingParams pagingParams = new(vehicleModelParams.PageNumber, vehicleModelParams.PageSize);

            var vehicleModels = await _vehicleService.GetVehicleModels(vehicleModelsFilters, sortParams, pagingParams);

            var vehicleModelViewModels = _mapper.Map<List<VehicleModelViewModel>>(vehicleModels);
            
            var vehicleMakes = await _vehicleService.GetAllVehicleMakes();

            PagedList<VehicleModelViewModel> pagedVehicleModelViewModels = new(vehicleModelViewModels, 
                vehicleModels.TotalCount, vehicleModelParams.PageNumber, vehicleModelParams.PageSize);

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

            var vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

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

            var vehicleModelViewModel = _mapper.Map<VehicleModelViewModel>(vehicleModel);

            return View(vehicleModelViewModel);
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
        public async Task<IActionResult> Create([Bind("Name,Abrv,VehicleMakeId")] CreateVehicleModelViewModel createVehicleModelViewModel)
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
        public async Task<IActionResult> Edit([Bind("Id,Name,Abrv,VehicleMakeId")] CreateVehicleModelViewModel createVehicleModelViewModel)
        {
            if (string.IsNullOrEmpty(createVehicleModelViewModel.Name) || string.IsNullOrEmpty(createVehicleModelViewModel.Abrv))
            {
                return NotFound();
            }

            var vehicleMake = _vehicleService.GetVehicleMake(createVehicleModelViewModel.VehicleMakeId);

            var vehicleModel = _mapper.Map<VehicleModel>(createVehicleModelViewModel);

            try
            {
                await _vehicleService.UpdateVehicleModel(vehicleModel);
            }
            catch (Exception e)
            {
                //if vehicleMakeId doesn't exist
                return NotFound("Wrong VehicleMakeId");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
