using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;

namespace VehicleShowroomManagementSystem.Areas.Admin.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;
        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
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
            if (supplier == null)
            {
                return NotFound();
            }
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
                var result = await _supplierService.SaveOrUpdateAsync(model);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Create));
            }

            // If the model is invalid, show an error notification and re-render the form
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
                var result = await _supplierService.SaveOrUpdateAsync(model);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction("Edit", new { id = model.SupplierId });
            }
            return RedirectToAction("Edit", new { id = model.SupplierId });
        }


        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var result = await _supplierService.DeleteAsync(id);
                if(result)// Redirect to the Index page with the same page number
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
