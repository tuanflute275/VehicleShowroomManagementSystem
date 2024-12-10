using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Mangement.Application.Abstracts;
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
    }
}
