using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Infrastructure.Abstracts;
using VehicleShowroom.Management.UI.Utils;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IPDFService _pdfService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper, IPDFService pdfService)
        {
            _userService = userService;
            _pdfService = pdfService;
            _mapper = mapper;
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
            if (user == null)
            {
                return NotFound();
            }
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
                    TempData["success"] = "User created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    ModelState.AddModelError(string.Empty, errorMessage ?? "An error occurred while saving the user.");
                    return View("Create", model);
                }
            }

            // If the model is invalid, show an error notification and re-render the form
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
                    TempData["success"] = "User created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Thêm thông báo lỗi vào ModelState để hiển thị trên giao diện
                    ModelState.AddModelError(string.Empty, errorMessage ?? "An error occurred while saving the user.");
                    return RedirectToAction("Edit", new { id = model.UserId });
                }
            }
            return RedirectToAction("Edit", new { id = model.UserId });
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var result = await _userService.DeleteAsync(id);
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

        [HttpGet]
        public async Task<IActionResult> ExportPDF(int id)
        {
            var data = await _userService.GetUserByIdAsync(id);
            string html = await this.RenderViewAsync<UserDTO>(RouteData, "_TemplateReportUser", data, true);
            var result = _pdfService.GeneratePDF(html);
            return File(result, "application/pdf", $"{DateTime.Now.Ticks}.pdf");
        }
    }
}
