using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Models.DTOs;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace VehicleShowroomManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        public AuthController(IUserService userService, INotyfService notyfService)
        {
            _userService = userService;
            _notyfService = notyfService;   
        }

        [HttpGet]
        [Route("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
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

            try
            {
                // Gọi service để thực hiện đăng nhập
                var (success, errorMessage, user) = await _userService.Login(model);

                // Xử lý kết quả đăng nhập từ Service
                if (!success)
                {
                    TempData["error"] = errorMessage;
                    return View(model);
                }

                // Lưu thông tin đăng nhập vào cookie và chuyển hướng người dùng
                await SignInUser(user);

                _notyfService.Success("Login successfully!");

                // Redirect về URL nếu có, hoặc về trang chủ
                if(user.Role != 0)
                    return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToAction("Index", "Home");
                else
                    return Redirect("/");
            }
            catch (Exception e)
            {
                // Log lỗi và hiển thị lỗi cho người dùng
                Console.WriteLine(e.Message);
                TempData["error"] = "An error occurred during login!";
                return View(model);
            }
        }

        private async Task SignInUser(UserDTO user)
        {
            string roleClaim = user.Role switch
            {
                0 => "User",
                1 => "Admin",
                2 => "Employee",
                3 => "Invoice",
                _ => "User"
            };

            var claims = new List<Claim>
        {
            new Claim("userId", user.UserId.ToString() ?? string.Empty),
            new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
            new Claim("userFullName", user.FullName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim("avatar", user.Avatar ?? string.Empty),
            new Claim(ClaimTypes.Role, roleClaim ?? "User")
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
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

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            //_toastNotification.Success("Logout successfully!", 3);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/login");
        }
    }
}
