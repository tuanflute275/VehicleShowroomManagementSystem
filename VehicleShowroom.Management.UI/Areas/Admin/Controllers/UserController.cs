using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroom.Management.Infrastructure.Abstracts;
using VehicleShowroom.Management.UI.Utils;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IPDFService _pdfService;
        private readonly IUserService _userService;
        private readonly INotyfService _toastNotification;
        public UserController(IUserService userService, IMapper mapper, IPDFService pdfService, INotyfService notyfService)
        {
            _mapper = mapper;
            _pdfService = pdfService;
            _userService = userService;
            _toastNotification = notyfService;
        }
        public async Task<IActionResult> Index(string? keyword, int? page = 1)
        {
            ViewBag.keyword = keyword;
            ViewData["CurrentPage"] = page;
            var pagedSuppliers = await _userService.GetAllPaginationAsync(keyword, page ?? 1);
            return View(pagedSuppliers);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(IFormFile fileUpload,UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _userService.SaveOrUpdateAsync(model, fileUpload);
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

            // If the model is invalid, show an error notification and re-render the form
            _toastNotification.Error(Constant.InvalidForm, 3);
            return View("Create", model); // You can return the same view to show validation messages
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            var data = _mapper.Map<UserViewModel>(user);
            // Return the user to the Edit view
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(IFormFile? fileUpload, string? oldImage,UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _userService.SaveOrUpdateAsync(model, fileUpload, oldImage);
                if (isSuccess)
                {
                    _toastNotification.Success(Constant.UpdateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);  // Show error message
                    return RedirectToAction("Edit", new { id = model.UserId });
                }
            }
            _toastNotification.Error(Constant.InvalidForm, 3);
            return RedirectToAction("Edit", new { id = model.UserId });
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var (isSuccess, errorMessage) = await _userService.DeleteAsync(id);
                if (isSuccess) _toastNotification.Success(Constant.DeleteSuccess, 3);
                else _toastNotification.Warning(errorMessage ?? Constant.OperationFailed, 3);
            }
            catch (Exception ex)
            {
                _toastNotification.Error($"{Constant.OperationFailed} Error: {ex.Message}", 3);
            }
            return RedirectToAction("Index", new { page = page ?? 1 });
        }

        [HttpGet]
        public async Task<IActionResult> ExportAll()
        {
            var data = await _userService.GetAllAsync();
            string html = await this.RenderViewAsync<List<UserDTO>>(RouteData, "_TemplateReportUserAll", data, true);
            var result = _pdfService.GeneratePDF(html);
            return File(result, "application/pdf", $"{DateTime.Now.Ticks}.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> ExportPDF(int id)
        {
            var data = await _userService.GetDataExportByIdAsync(id);
            string html = await this.RenderViewAsync<UserVehicleInfoDTO>(RouteData, "_TemplateReportUser", data, true);
            var result = _pdfService.GeneratePDF(html);
            return File(result, "application/pdf", $"{DateTime.Now.Ticks}.pdf");
        }
    }
}
