using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.DTOs.Supplier;
using X.PagedList;

namespace VehicleShowroom.Mangement.Application.Abstracts
{
    public interface ICompanyServices
    {
        Task<IPagedList<CompanyDTO>> GetPagedCompanyAsync(string keyword, int page, int pageSize = 10);
        Task<bool> DeleteAsync(int id);
        Task<bool> GetCompanyByIdAsync(int id);
    }
}
