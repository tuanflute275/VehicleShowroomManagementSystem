using Microsoft.AspNetCore.Mvc;
using NToastNotify.Helpers;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.ViewModels;

namespace VehicleShowroomManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {

            return View("Login");
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View("register");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                // Gọi service để xử lý đăng ký
                var result = await _userService.RegisterAsync(model);

                if (result.Success)
                {
                    TempData["message"] = "Account registration successful!";
                    return Redirect("/login");
                }
                else
                {
                    // Hiển thị thông báo lỗi nếu đăng ký không thành công
                    TempData["error"] = result.ErrorMessage;
                    return View(model);
                }
            }
            catch (Exception e)
            {
                // Log lỗi nếu có
                Console.WriteLine(e.Message);
                return View(model);
            }
        }
    }
}
