using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INotyfService _toastNotification;
        private readonly ICompanyServices _companyServices;
        public CompanyController(ICompanyServices companyServices, IMapper mapper, INotyfService notyfService)
        {
            _mapper = mapper;
            _toastNotification = notyfService;
            _companyServices = companyServices;
        }

        public async Task<IActionResult> Index(string? keyword, int? page = 1)
        {
            ViewBag.keyword = keyword;
            ViewData["CurrentPage"] = page;
            var data = await _companyServices.GetAllPaginationAsync(keyword, page ?? 1, 8);
            return View(data);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var company = await _companyServices.GetByIdAsync(id);
            if (company == null) return NotFound();
            var data = _mapper.Map<CompanyDTO>(company);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _companyServices.SaveOrUpdateAsync(model);
                if (isSuccess)
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
            _toastNotification.Error(Constant.InvalidForm, 3);
            return View("Create", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyServices.GetByIdAsync(id);
            var data = _mapper.Map<CompanyViewModel>(company);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _companyServices.SaveOrUpdateAsync(model);
                if (isSuccess)
                {
                    _toastNotification.Success(Constant.UpdateSuccess, 3);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toastNotification.Error(errorMessage ?? Constant.OperationFailed, 3);
                    return RedirectToAction("Edit", new { id = model.CompanyId });
                }
            }
            _toastNotification.Error(Constant.InvalidForm, 3);
            return RedirectToAction("Edit", new { id = model.CompanyId });
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var (isSuccess, errorMessage) = await _companyServices.DeleteAsync(id);
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
