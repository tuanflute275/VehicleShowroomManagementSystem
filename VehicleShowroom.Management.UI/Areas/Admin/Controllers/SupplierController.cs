using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Utils;

namespace VehicleShowroomManagementSystem.Areas.Admin.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INotyfService _toastNotification;
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService, INotyfService notyfService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
            _toastNotification = notyfService;
        }

        public async Task<IActionResult> Index(string? keyword, int? page = 1)
        {
            ViewBag.keyword = keyword;
            ViewData["CurrentPage"] = page;
            var pagedSuppliers = await _supplierService.GetAllPaginationAsync(keyword, page ?? 1, 8);
            return View(pagedSuppliers);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null) return NotFound();
            var data = _mapper.Map<SupplierDTO>(supplier);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (success, errorMessage) = await _supplierService.SaveOrUpdateAsync(model);
                if (success)
                {
                    _toastNotification.Success(Constant.CreateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    return View("Create", model); // Trả lại form với thông báo lỗi
                }
            }

            // If the model is invalid, show an error notification and re-render the form
            _toastNotification.Error(Constant.InvalidForm, 3);
            return View("Create", model); // You can return the same view to show validation messages
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            var data = _mapper.Map<SupplierViewModel>(supplier);
            // Return the supplier to the Edit view
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(SupplierViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (success, errorMessage) = await _supplierService.SaveOrUpdateAsync(model);

                if (success)
                {
                    _toastNotification.Success(Constant.UpdateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    return RedirectToAction("Edit", new { id = model.SupplierId });
                }
            }
            _toastNotification.Error(Constant.InvalidForm, 3);
            return RedirectToAction("Edit", new { id = model.SupplierId });
        }


        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var (success, errorMessage) = await _supplierService.DeleteAsync(id);
                if (success) _toastNotification.Success(Constant.DeleteSuccess, 3);
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
