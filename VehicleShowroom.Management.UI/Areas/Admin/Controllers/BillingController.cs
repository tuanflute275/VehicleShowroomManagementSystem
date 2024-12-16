using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Services;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class BillingController : BaseController
    {
        private readonly IBillingService _billingService;
        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;   
        }

        public async Task<IActionResult> Index(string? keyword, int? page = 1)
        {
            ViewBag.keyword = keyword;
            ViewData["CurrentPage"] = page;
            var data = await _billingService.GetAllPaginationAsync(keyword, page ?? 1, 8);
            return View(data);
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var (isSuccess, errorMessage) = await _billingService.DeleteAsync(id);
                if (isSuccess)
                {
                    TempData["success"] = "Vehicle created successfully!";
                    return RedirectToAction("Index", new { page = page ?? 1 });
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    ModelState.AddModelError(string.Empty, errorMessage ?? "An error occurred while saving the user.");
                    return RedirectToAction("Index", new { page = page ?? 1 });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { page = page ?? 1 });
            }
        }
    }
}
