using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class CompanyController : BaseController
    {
        private readonly ICompanyServices _companyServices;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyServices companyServices, IMapper mapper)
        {
            _companyServices = companyServices;
            _mapper = mapper;
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
            if (company == null)
            {
                return NotFound();
            }
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
                var result = await _companyServices.SaveOrUpdateAsync(model);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Create));
            }
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
                var result = await _companyServices.SaveOrUpdateAsync(model);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Edit", new { id = model.CompanyId });
                }
            }
            return RedirectToAction("Edit", new { id = model.CompanyId });
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var result = await _companyServices.DeleteAsync(id);
                if (result)
                {
                    return RedirectToAction("Index", new { page = page ?? 1 });
                }
                else
                {
                    return RedirectToAction("Index", new { page = page ?? 1 });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { page = page ?? 1 });
            }
        }
    }
}
