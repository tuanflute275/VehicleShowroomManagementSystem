using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Application.Services;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IMapper _mapper;
        public PurchaseOrderController(IMapper mapper, IPurchaseOrderService purchaseOrderService)
        {
            _mapper = mapper;
            _purchaseOrderService = purchaseOrderService;
        }

        public async Task<IActionResult> Index(string? keyword, int? page = 1)
        {
            ViewBag.keyword = keyword;
            ViewData["CurrentPage"] = page;
            var data = await _purchaseOrderService.GetAllPaginationAsync(keyword, page ?? 1);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Suppliers = new SelectList(await _purchaseOrderService.GetAllSupplierAsync(), "SupplierId", "SupplierName");
            ViewBag.Vehicles = new SelectList(await _purchaseOrderService.GetAllVehicleAsync(), "VehicleId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(PurchaseOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
               /* var (isSuccess, errorMessage) = await _vehicleService.SaveOrUpdateAsync(model, fileUpload);
                if (isSuccess)
                {
                    TempData["success"] = "Vehicle created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    ModelState.AddModelError(string.Empty, errorMessage ?? "An error occurred while saving the user.");
                    return View("Create", model);
                }*/
            }

            // If the model is invalid, show an error notification and re-render the form
            ViewBag.Suppliers = new SelectList(await _purchaseOrderService.GetAllSupplierAsync(), "SupplierId", "SupplierName");
            ViewBag.Vehicles = new SelectList(await _purchaseOrderService.GetAllVehicleAsync(), "VehicleId", "Name");
            return View("Create", model);
        }

                public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var result = await _purchaseOrderService.DeleteAsync(id);
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
