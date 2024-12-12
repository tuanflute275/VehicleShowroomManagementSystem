using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;

namespace VehicleShowroomManagementSystem.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _homeService.GetDashboardCountsAsync();
            return View(data);
        }
    }
}
