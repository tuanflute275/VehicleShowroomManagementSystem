using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class VehicleDetailController : BaseController
    {
        private readonly IVehicleDetailServices _vehicleDetailServices;
        private readonly IMapper _mapper;
        public VehicleDetailController(IVehicleDetailServices vehicleDetailServices, IMapper mapper)
        {
            _vehicleDetailServices = vehicleDetailServices;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? page = 1)
        {
            ViewData["CurrentPage"] = page;
            var data = await _vehicleDetailServices.GetAllPaginationAsync(page ?? 1, 8);
            return View(data);
        }
    }
}
