using AutoMapper;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;
using X.PagedList;

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

        public async Task<IPagedList<SupplierDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8)
        {
            // 1. Lấy dữ liệu với phân trang và sắp xếp từ request
            var supplierQuery = _unitOfWork.SupplierRepository.GetAllAsync(
                 expression: s => string.IsNullOrEmpty(keyword) || s.SupplierName.Contains(keyword)
             );

            // 2. Chuyển đổi dữ liệu từ Supplier sang SupplierDTO
            var suppliers = await supplierQuery;  // Execute the query to get the suppliers
            var supplierList = suppliers.ToList();  // Convert to list for paging

            // 4. Chuyển đổi dữ liệu từ Supplier sang SupplierDTO
            var data = _mapper.Map<List<SupplierDTO>>(supplierList);

            // 5. Trả về ResponseDatatable với các giá trị phân trang
            return data.ToPagedList(page, pageSize);  // Ensure data is paged
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            return await _unitOfWork.SupplierRepository.GetSupplierByIdAsync(id);
        }

        public async Task<bool> SaveOrUpdateAsync(SupplierViewModel supplierModel)
        {
            try
            {
                var supplier = new Supplier();
                if (supplierModel.SupplierId == 0)
                {
                    supplier = _mapper.Map<Supplier>(supplierModel);
                    supplier.CreateBy = "Admin";
                    supplier.CreateDate = DateTime.Now;
                    supplier.ContractDate = DateTime.Now;
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
                    supplier.Status = supplierModel.Status;
                    supplier.Notes = supplierModel.Notes;
                    supplier.UpdateBy = "Admin";
                    supplier.UpdateDate = DateTime.Now;
                }
                bool result = await _unitOfWork.SupplierRepository.SaveOrUpdateAsync(supplier);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var supplier = await _unitOfWork.SupplierRepository.GetSupplierByIdAsync(id);
                if (supplier == null)
                {
                    return false;
                }

                await _unitOfWork.SupplierRepository.DeleteAsync(supplier);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
