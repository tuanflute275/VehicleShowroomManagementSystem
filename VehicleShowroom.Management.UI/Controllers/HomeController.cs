using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;

namespace VehicleShowroomManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly INotyfService _toastNotification;
        private readonly IVehicleService _vehicleService;

        public HomeController(ILogger<HomeController> logger, IVehicleService vehicleService, IMapper mapper, INotyfService notyfService)
        {
            _logger = logger;
            _mapper = mapper;
            _vehicleService = vehicleService;
            _toastNotification = notyfService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _vehicleService.GetAllPaginationAsync(null, 1, 8);
            return View(data);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> Product(string? keyword, int? page = 1)
        {
            ViewBag.keyword = keyword;
            ViewData["CurrentPage"] = page;
            var data = await _vehicleService.GetAllPaginationAsync(keyword, page ?? 1, 8);
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            if (vehicle == null) return NotFound();
            var data = _mapper.Map<VehicleDTO>(vehicle);
            var dataImage = await _vehicleService.GetAllImagePaginationAsync(id, 1, 99);
            ViewBag.ListImage = dataImage;
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
