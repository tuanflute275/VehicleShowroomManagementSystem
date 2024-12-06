using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.DTOs;

namespace VehicleShowroomManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public async Task<IActionResult> Index(string? name, int? page = 1)
        {
            RequestDatatable requestDatatable = new RequestDatatable
            {
                SkipItems = (page - 1 ?? 0) * 15, // Tính toán SkipItems dựa trên page
                PageSize = 15
            };
            if (!string.IsNullOrEmpty(name))
            {
                requestDatatable.Keyword = name; // Thêm tên vào điều kiện tìm kiếm
            }
            var data = _supplierService.GetSupplierByPaginationAsync(requestDatatable);
            ViewBag.Data = data.Result.Data;
            ViewBag.CurrentPage = page;  // Lưu lại trang hiện tại
            ViewBag.TotalPages = (int)Math.Ceiling((double)data.Result.RecordsTotal / requestDatatable.PageSize); // Tổng số trang

            return View();
        }
    }
}
