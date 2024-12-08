using Microsoft.AspNetCore.Mvc;

namespace VehicleShowroomManagementSystem.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
