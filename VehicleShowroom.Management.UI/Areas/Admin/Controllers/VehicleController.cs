﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class VehicleController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INotyfService _toastNotification;
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService, IMapper mapper, INotyfService notyfService)
        {
            _mapper = mapper;
            _vehicleService = vehicleService;
            _toastNotification = notyfService;
        }

        public async Task<IActionResult> Index(string? keyword, int? page = 1)
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
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var suppliers = await _vehicleService.GetAllSupplierAsync();
            var companies = await _vehicleService.GetAllCompanyAsync();
            ViewBag.Supplier = suppliers;   
            ViewBag.Companies = companies;   
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(IFormFile fileUpload, VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _vehicleService.SaveOrUpdateAsync(model, fileUpload);
                if (isSuccess)
                {
                    _toastNotification.Success(Constant.CreateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    return View("Create", model);
                }
            }
            var suppliers = await _vehicleService.GetAllSupplierAsync();
            var companies = await _vehicleService.GetAllCompanyAsync();
            ViewBag.Supplier = suppliers;
            ViewBag.Companies = companies;
            // If the model is invalid, show an error notification and re-render the form
            _toastNotification.Error(Constant.InvalidForm, 3);
            return View("Create", model); // You can return the same view to show validation messages
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(id);
            var data = _mapper.Map<VehicleViewModel>(vehicle);
            var suppliers = await _vehicleService.GetAllSupplierAsync();
            var companies = await _vehicleService.GetAllCompanyAsync();
            ViewBag.Suppliers = suppliers;
            ViewBag.Companies = companies;
            // Return the vehicle to the Edit view
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(IFormFile? fileUpload, string? oldImage, VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _vehicleService.SaveOrUpdateAsync(model, fileUpload, oldImage);
                if (isSuccess)
                {
                    _toastNotification.Success(Constant.UpdateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    return RedirectToAction("Edit", new { id = model.VehicleId });
                }
            }
            _toastNotification.Error(Constant.InvalidForm, 3);
            return RedirectToAction("Edit", new { id = model.VehicleId });
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var (isSuccess, errorMessage) = await _vehicleService.DeleteAsync(id);
                if (isSuccess) _toastNotification.Success(Constant.DeleteSuccess, 3);
                else _toastNotification.Warning(errorMessage ?? Constant.OperationFailed, 3);
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the deletion
                _toastNotification.Error($"{Constant.OperationFailed} Error: {ex.Message}", 3);
            }
            return RedirectToAction("Index", new { page = page ?? 1 });
        }

        //VehicleImage
        public async Task<IActionResult> ListImage(int id, int? page = 1)
        {
            var data = await _vehicleService.GetAllImagePaginationAsync(id, page ?? 1, 8);
            return View(data);
        }

        public async Task<IActionResult> CreateImage(int id)
        {

            return View();
        }
    }
}
