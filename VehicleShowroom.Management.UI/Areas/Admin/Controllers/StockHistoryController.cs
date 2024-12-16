using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroom.Management.Infrastructure.Abstracts;
using VehicleShowroom.Management.UI.Utils;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class StockHistoryController : BaseController
    {
        private readonly IPDFService _pdfService;
        private readonly IStockHistoryService _stockHistoryService;
        public StockHistoryController(IStockHistoryService stockHistoryService, IPDFService pdfService)
        {
            _pdfService = pdfService;
            _stockHistoryService = stockHistoryService;
        }

        public async Task<IActionResult> Index(string? keyword, int? page = 1)
        {
            ViewBag.keyword = keyword;
            ViewData["CurrentPage"] = page;
            var data = await _stockHistoryService.GetAllPaginationAsync(keyword, page ?? 1, 8);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> ExportAll()
        {
            var data = await _stockHistoryService.GetAllAsync(Constant.STOCKIN);
            string html = await this.RenderViewAsync<List<StockHistoryDTO>>(RouteData, "_TemplateReportStockHistory", data, true);
            var result = _pdfService.GeneratePDF(html);
            return File(result, "application/pdf", $"{DateTime.Now.Ticks}.pdf");
        }

        public async Task<IActionResult> Delete(int id, int? page)
        {
            try
            {
                var result = await _stockHistoryService.DeleteAsync(id);
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
