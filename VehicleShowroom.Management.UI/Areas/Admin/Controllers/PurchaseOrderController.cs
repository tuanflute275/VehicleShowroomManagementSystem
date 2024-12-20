﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Services;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroom.Management.Domain.Entities;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INotyfService _toastNotification;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IVehicleService _vehicleService;

        public PurchaseOrderController(IMapper mapper, IPurchaseOrderService purchaseOrderService, INotyfService notyfService)
        {
            _mapper = mapper;
           _toastNotification = notyfService;
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
                var (isSuccess, errorMessage) = await _purchaseOrderService.SaveOrUpdateAsync(model);
                if (isSuccess)
                {
                    _toastNotification.Success(Constant.CreateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    return View("Create", model);
                }
            }

            // If the model is invalid, show an error notification and re-render the form
            ViewBag.Suppliers = new SelectList(await _purchaseOrderService.GetAllSupplierAsync(), "SupplierId", "SupplierName");
            ViewBag.Vehicles = new SelectList(await _purchaseOrderService.GetAllVehicleAsync(), "VehicleId", "Name");
            _toastNotification.Error(Constant.InvalidForm, 3);
            return View("Create", model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            var puschaseOrder = await _purchaseOrderService.GetByIdAsync(id);
            if (puschaseOrder == null) return NotFound();
            ViewBag.Suppliers = await _purchaseOrderService.GetAllSupplierAsync();
            return View(puschaseOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PurchaseOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _purchaseOrderService.SaveOrUpdateAsync(model);
                if (isSuccess)
                {
                    _toastNotification.Success(Constant.UpdateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    return View("Index", model);
                }
            }

            // If the model is invalid, show an error notification and re-render the form
            ViewBag.Suppliers = new SelectList(await _purchaseOrderService.GetAllSupplierAsync(), "SupplierId", "SupplierName");
            _toastNotification.Error(Constant.InvalidForm, 3);
            return View("Index", model);
        }
        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var (isSuccess, errorMessage) = await _purchaseOrderService.DeleteAsync(id);
                if (isSuccess) _toastNotification.Success(Constant.DeleteSuccess, 3);
                else _toastNotification.Warning(errorMessage ?? Constant.OperationFailed, 3);
            }
            catch (Exception ex)
            {
                _toastNotification.Error($"{Constant.OperationFailed} Error: {ex.Message}", 3);
            }
            return RedirectToAction("Index", new { page = page ?? 1 });
        }
        public async Task<IActionResult> Detail(int id)
        {
            var data = await _purchaseOrderService.GetDataByIdAsync(id);
            if (data == null)
            {
                _toastNotification.Warning("Purchase Order not found.", 3);
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }
    }
}
