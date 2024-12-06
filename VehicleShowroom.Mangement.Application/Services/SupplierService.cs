using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.DTOs.Supplier;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;
using VehicleShowroom.Mangement.Domain.Enums;
using static System.Reflection.Metadata.BlobBuilder;

namespace VehicleShowroom.Mangement.Application.Services
{
    public class SupplierService : ISupplierService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(int id)
        {

            var supplier = await _unitOfWork.SupplierRepository.GetSupplierByIdAsync(id);
            if (supplier != null) 
            { 
                return false;
            }

            await _unitOfWork.SupplierRepository.DeleteAsync(supplier);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<SupplierViewModel> GetSupplierByIdAsync(int id)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetSupplierByIdAsync(id);
            return _mapper.Map<SupplierViewModel>(supplier);
        }

        public async Task<ResponseModel> SaveAsync(SupplierViewModel supplierModel)
        {
            var supplier = new Supplier();
            if(supplierModel.SupplierId == 0)
            {
                supplier = _mapper.Map<Supplier>(supplierModel);
                supplier.CreateBy = "Admin";
                supplier.CreateDate = DateTime.Now;
            }
            else
            {
                supplier = await _unitOfWork.SupplierRepository.GetSupplierByIdAsync(supplierModel.SupplierId);
                supplier.SupplierName = supplierModel.SupplierName;
                supplier.ContactPerson = supplierModel.ContactPerson;
                supplier.PhoneNumber = supplierModel.PhoneNumber;
                supplier.Email = supplierModel.Email;
                supplier.Website = supplierModel.Website;
                supplier.TaxCode = supplierModel.TaxCode;
                supplier.Address = supplierModel.Address;
                supplier.BankAccount = supplierModel.BankAccount;
                supplier.BankName = supplierModel.BankName;
                supplier.ContractNumber = supplierModel.ContractNumber;
                supplier.ContractDate = supplierModel.ContractDate;
                supplier.Status = supplierModel.Status;
                supplier.Notes = supplierModel.Notes;
            }
            bool result = await _unitOfWork.SupplierRepository.SaveOrUpdateAsync(supplier);
            await _unitOfWork.SaveChangeAsync();
           
            var actionType = supplierModel.SupplierId == 0 ? ActionType.Insert : ActionType.Update;
            var successMessage = $"{(supplierModel.SupplierId == 0 ? "Insert" : "Update")} successful.";
            var failureMessage = $"{(supplierModel.SupplierId == 0 ? "Insert" : "Update")} failed.";
            return new ResponseModel
            {
                Action = actionType,
                Message = result ? successMessage : failureMessage,
                Status = result,
            };
        }

        public async Task<ResponseDatatable<SupplierDTO>> GetSupplierByPaginationAsync(RequestDatatable request)
        {
            /* return await base.GetAllAsync(
                 expression: s => s.Status == "Active",
             include: query => query
             .Include(s => s.Products)     // Include Products
             .Include(s => s.Orders)       // Include Orders
                 .ThenInclude(o => o.OrderDetails)  // Include OrderDetails for each Order
             );*/
            // 1. Lấy dữ liệu với phân trang và sắp xếp từ request
            var supplierQuery = await _unitOfWork.SupplierRepository.GetAllAsync(
                expression: s => string.IsNullOrEmpty(request.Keyword) || s.SupplierName.Contains(request.Keyword)
            );

            var totalRecords = supplierQuery.Count();  // Tổng số bản ghi không phân trang
            var filteredQuery = supplierQuery
                .Skip(request.SkipItems)  // Bắt đầu từ chỉ số 'Start'
                .Take(request.PageSize); // Lấy số lượng bản ghi 'Length'

            // 2. Chuyển đổi dữ liệu từ Supplier sang SupplierDTO
            var suppliers = filteredQuery.ToList();
            var data = _mapper.Map<List<SupplierDTO>>(suppliers);

            // 3. Trả về ResponseDatatable với các giá trị phân trang
            return new ResponseDatatable<SupplierDTO>
            {
                RecordsTotal = totalRecords,  // Tổng số bản ghi
                RecordsFiltered = totalRecords,  // Tổng số bản ghi đã lọc (có thể thay đổi nếu có điều kiện lọc)
                Data = data  // Dữ liệu đã ánh xạ sang SupplierDTO
            };
        }
    }
}
