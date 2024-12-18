using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class SalesOrderController : BaseController
    {
        private readonly INotyfService _toastNotification;
        private readonly ISalesOrderService _salesOrderService;
        public SalesOrderController(ISalesOrderService salesOrderService, INotyfService notyfService)
        {
            _toastNotification = notyfService;
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
            if (data == null) return NotFound();
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
                    _toastNotification.Success(Constant.CreateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    ViewBag.Users = new SelectList(await _salesOrderService.GetAllUserAsync(), "UserId", "FullName");
                    ViewBag.Vehicles = new SelectList(await _salesOrderService.GetAllVehicleAsync(), "VehicleId", "Name");
                    return View("Create", model);
                }
            }
            // If the model is invalid, show an error notification and re-render the form
            ViewBag.Users = new SelectList(await _salesOrderService.GetAllUserAsync(), "UserId", "FullName");
            ViewBag.Vehicles = new SelectList(await _salesOrderService.GetAllVehicleAsync(), "VehicleId", "Name");
            _toastNotification.Error(Constant.InvalidForm, 3);
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
                    _toastNotification.Success(Constant.UpdateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    return RedirectToAction("Edit", new { id = model.SalesOrderId });
                }
            }
            _toastNotification.Error(Constant.InvalidForm, 3);
            return RedirectToAction("Edit", new { id = model.SalesOrderId });
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var (isSuccess, errorMessage) = await _salesOrderService.DeleteAsync(id);
                if (isSuccess) _toastNotification.Success(Constant.DeleteSuccess, 3);
                else _toastNotification.Warning(errorMessage ?? Constant.OperationFailed, 3);
                
            }
            catch (Exception ex)
            {
                _toastNotification.Error($"{Constant.OperationFailed} Error: {ex.Message}", 3);
            }
            return RedirectToAction("Index", new { page = page ?? 1 });
        }
    }
}
