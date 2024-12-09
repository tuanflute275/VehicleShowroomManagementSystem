using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Application.Services;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class VehicleController : BaseController
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        public VehicleController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
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
            if (vehicle == null)
            {
                return NotFound();
            }
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
                    TempData["success"] = "Vehicle created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    ModelState.AddModelError(string.Empty, errorMessage ?? "An error occurred while saving the user.");
                    return View("Create", model);
                }
            }
            var suppliers = await _vehicleService.GetAllSupplierAsync();
            var companies = await _vehicleService.GetAllCompanyAsync();
            ViewBag.Supplier = suppliers;
            ViewBag.Companies = companies;
            // If the model is invalid, show an error notification and re-render the form
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
                    TempData["success"] = "User created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    ModelState.AddModelError(string.Empty, errorMessage ?? "An error occurred while saving the user.");
                    return RedirectToAction("Edit", new { id = model.VehicleId });
                }
            }
            return RedirectToAction("Edit", new { id = model.VehicleId });
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var result = await _vehicleService.DeleteAsync(id);
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
