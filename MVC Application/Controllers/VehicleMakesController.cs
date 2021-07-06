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
using class_library.Helpers;

namespace MVC_Application.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleMakesController> _logger;

        public VehicleMakesController(IVehicleService vehicleService, IMapper mapper, ILogger<VehicleMakesController> logger)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET /vehiclemakes
        [HttpGet("[controller]")]
        public async Task<IActionResult> Index([FromQuery] VehicleMakeParams vehicleMakeParams)
        {
            ViewData["OrderBy"] = (string.IsNullOrEmpty(vehicleMakeParams.OrderBy))
                ? "Order By" : vehicleMakeParams.OrderBy;

            ViewData["PageSize"] = vehicleMakeParams.PageSize;
            ViewData["SearchQuery"] = vehicleMakeParams.SearchQuery;

            if (vehicleMakeParams.OrderBy == "Order By")
            {
                vehicleMakeParams.OrderBy = null;
            }

            VehicleMakesFilter vehicleMakesFilter = new(vehicleMakeParams.SearchQuery);
            SortParams sortParams = new(vehicleMakeParams.OrderBy);
            PagingParams pagingParams = new(vehicleMakeParams.PageNumber, vehicleMakeParams.PageSize);

            var vehicleMakes = await _vehicleService.GetVehicleMakes(vehicleMakesFilter, pagingParams, sortParams);
            var vehicleMakeViewModels = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);
            
            PagedList<VehicleMakeViewModel> pagedVehicleViewMakes = new(vehicleMakeViewModels,
                vehicleMakes.TotalCount, vehicleMakeParams.PageNumber, vehicleMakeParams.PageSize);

            return View(pagedVehicleViewMakes);
        }

        // GET /vehiclemakes/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleService.GetVehicleMake((int)id);

            if (vehicleMake is null)
            {
                return NotFound();
            }

            var vehicleMakeViewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(vehicleMakeViewModel);
        }

        // GET /vehicleMakes/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleService.GetVehicleMake((int)id);

            if (vehicleMake is null)
            {
                return NotFound();
            }

            var vehicleMakeViewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(vehicleMakeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicleMake = await _vehicleService.GetVehicleMake(id);

            if (vehicleMake is null)
            {
                return NotFound();
            }

            await _vehicleService.DeleteVehicleMake(vehicleMake);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST vehiclemakes/Create
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Name,Abrv")] VehicleMakeViewModel vehicleMakeViewModel)
        {
            if (string.IsNullOrEmpty(vehicleMakeViewModel.Name) || string.IsNullOrEmpty(vehicleMakeViewModel.Abrv))
            {
                return View(vehicleMakeViewModel);
            }

            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeViewModel);
            await _vehicleService.CreateVehicleMake(vehicleMake);
            return RedirectToAction(nameof(Index));
        }


        // GET VehicleMakes/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleService.GetVehicleMake((int)id);

            if (vehicleMake is null)
            {
                return NotFound();
            }

            var vehicleMakeViewModel = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeViewModel);
        }

        // POST vehiclemakes/Edit
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit([Bind("Id,Name,Abrv")] VehicleMakeViewModel vehicleMakeViewModel)
        {
            if (string.IsNullOrEmpty(vehicleMakeViewModel.Name) || string.IsNullOrEmpty(vehicleMakeViewModel.Abrv))
            {
                return NotFound();
            }

            var vehicleMake = _mapper.Map<VehicleMake>(vehicleMakeViewModel);
            await _vehicleService.UpdateVehicleMake(vehicleMake);
            return RedirectToAction(nameof(Index));
        }
    }
}
