using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Services;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class SalesOrderController : BaseController
    {
        private readonly ISalesOrderService _salesOrderService;
        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        public async Task<IActionResult> Index(string? keyword, int? page = 1)
        {
            ViewBag.keyword = keyword;
            ViewData["CurrentPage"] = page;
            var data = await _salesOrderService.GetAllPaginationAsync(keyword, page ?? 1);
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var data = await _salesOrderService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = new SelectList(await _salesOrderService.GetAllUserAsync(), "UserId", "FullName");
            ViewBag.Vehicles = new SelectList(await _salesOrderService.GetAllVehicleAsync(), "VehicleId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SaleOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _salesOrderService.SaveOrUpdateAsync(model);
                if (isSuccess)
                {
                    TempData["success"] = "Sales Order created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    ModelState.AddModelError(string.Empty, errorMessage ?? "An error occurred while saving the user.");
                    ViewBag.Users = new SelectList(await _salesOrderService.GetAllUserAsync(), "UserId", "FullName");
                    ViewBag.Vehicles = new SelectList(await _salesOrderService.GetAllVehicleAsync(), "VehicleId", "Name");
                    return View("Create", model);
                }
            }
            // If the model is invalid, show an error notification and re-render the form
            ViewBag.Users = new SelectList(await _salesOrderService.GetAllUserAsync(), "UserId", "FullName");
            ViewBag.Vehicles = new SelectList(await _salesOrderService.GetAllVehicleAsync(), "VehicleId", "Name");
            return View("Create", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _salesOrderService.GetDataByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SaleOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _salesOrderService.SaveOrUpdateAsync(model);
                if (isSuccess)
                {
                    TempData["success"] = "Sales Order created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Edit", new { id = model.SalesOrderId });
                }
            }
            return RedirectToAction("Edit", new { id = model.SalesOrderId });
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var result = await _salesOrderService.DeleteAsync(id);
                if (result)// Redirect to the Index page with the same page number
                    return RedirectToAction("Index", new { page = page ?? 1 });
                else
                    return RedirectToAction("Index", new { page = page ?? 1 });
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the deletion
                return RedirectToAction("Index", new { page = page ?? 1 });
            }
        }
    }
}
