using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VehicleShowroom.Management.Application.Abstracts;
using VehicleShowroom.Management.Application.Models.DTOs;
using VehicleShowroom.Management.Application.Models.ViewModels;
using VehicleShowroom.Management.Application.Utils;
using VehicleShowroom.Management.Domain.Abstract;
using VehicleShowroom.Management.Domain.Entities;
using X.PagedList;

namespace VehicleShowroom.Management.Application.Services
{
    public class VehicleService : IVehicleService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private FileUploadHelper _fileUploadHelper;
        public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, FileUploadHelper fileUploadHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileUploadHelper = fileUploadHelper;
        }

        public async Task<IPagedList<VehicleDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8)
        {
            var vehicleQuery = _unitOfWork.VehicleRepository.GetAllAsync(
               expression: s => string.IsNullOrEmpty(keyword) || s.Name.Contains(keyword),
               include: query => query
               .Include(x => x.Supplier)   
               .Include(x => x.Company)       
             );
            var vehicle = await vehicleQuery;
            var vehicleList = vehicle.ToList();
            var data = _mapper.Map<List<VehicleDTO>>(vehicleList);
            return data.ToPagedList(page, pageSize);
        }

        public async Task<List<Supplier>> GetAllSupplierAsync()
        {
            var supplierQuery = _unitOfWork.SupplierRepository.GetAllAsync();
            var suppliers = await supplierQuery;
            var supplierList = suppliers.ToList();
            return supplierList;
        }
        public async Task<List<Company>> GetAllCompanyAsync()
        {
            var companyQuery = _unitOfWork.CompanyRepository.GetAllAsync();
            var companies = await companyQuery;
            var companyList = companies.ToList();
            return companyList;
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            var vehicle = await _unitOfWork.VehicleRepository.GetAllAsync(
             expression: v => v.VehicleId == id,
             include: query => query
                 .Include(x => x.Supplier)
                 .Include(x => x.Company)
            );

            return vehicle.FirstOrDefault();
        }

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(VehicleViewModel model, IFormFile? fileUpload, string? oldImage = null)
        {
            try
            {
                var vehicle = new Vehicle();
                if (model.VehicleId == 0)
                {
                    vehicle.ModelNumber = model.ModelNumber;
                    vehicle.Name = model.Name;
                    vehicle.Slug = Util.GenerateSlug(model.Name);
                    vehicle.EngineNumber = model.EngineNumber;
                    vehicle.ChassisNumber = model.ChassisNumber;
                    vehicle.FuelType = model.FuelType;
                    vehicle.TransmissionType = model.TransmissionType;
                    vehicle.Color = model.Color;
                    vehicle.Price = model.Price;
                    vehicle.Mileage = model.Mileage;
                    vehicle.ManufactureYear = model.ManufactureYear;
                    vehicle.Status = model.Status;
                    vehicle.Description = model.Description;
                    vehicle.SupplierId = model.SupplierId;
                    vehicle.CompanyId = model.CompanyId;
                    vehicle.CreateBy = "Admin";
                    vehicle.CreateDate = DateTime.Now;
                    if (fileUpload != null)
                    {
                        var image = await _fileUploadHelper.UploadFileAsync(fileUpload, "vehicles");
                        vehicle.Image = image;
                    }
                }
                else
                {
                    vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(model.VehicleId);
                    if (vehicle == null) return (false, "Vehicle not found");
                    vehicle.ModelNumber = model.ModelNumber;
                    vehicle.Name = model.Name;
                    vehicle.Slug = Util.GenerateSlug(model.Name);
                    vehicle.EngineNumber = model.EngineNumber;
                    vehicle.ChassisNumber = model.ChassisNumber;
                    vehicle.FuelType = model.FuelType;
                    vehicle.TransmissionType = model.TransmissionType;
                    vehicle.Color = model.Color;
                    vehicle.Price = model.Price;
                    vehicle.Mileage = model.Mileage;
                    vehicle.ManufactureYear = model.ManufactureYear;
                    vehicle.Status = model.Status;
                    vehicle.Description = model.Description;
                    vehicle.SupplierId = model.SupplierId;
                    vehicle.CompanyId = model.CompanyId;
                    vehicle.UpdateBy = "Admin";
                    vehicle.UpdateDate = DateTime.Now;
                    if (fileUpload != null) // Kiểm tra xem ImageFile có tồn tại hay không
                    {
                        var image = await _fileUploadHelper.UploadFileAsync(fileUpload, "vehicles");
                        vehicle.Image = image;
                    }
                }
                bool result = await _unitOfWork.VehicleRepository.SaveOrUpdateAsync(vehicle);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int id)
        {
            if (id <= 0) return (false, "Invalid ID.");
            try
            {
                var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
                if (vehicle == null) return (false, "Vehicle not found.");
                await _unitOfWork.VehicleRepository.DeleteAsync(vehicle);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IPagedList<VehicleImageDTO>> GetAllImagePaginationAsync(int vehicleId, int page, int pageSize = 8)
        {
            var vehicleImageQuery = _unitOfWork.VehicleImageRepository.GetAllAsync(
               expression: s => vehicleId != null || s.VehicleId == vehicleId,
                include: query => query.Include(x => x.Vehicle)
             );
            var vehicleImage = await vehicleImageQuery;
            var vehicleImageList = vehicleImage.ToList();
            var data = _mapper.Map<List<VehicleImageDTO>>(vehicleImageList);
            return data.ToPagedList(page, pageSize);
        }

        public async Task<(bool Success, string ErrorMessage)> SaveImageAsync(VehicleImageViewModel model)
        {
            try
            {
                // validate input data
                if (model.VehicleId <= 0) return (false, "Invalid Vehicle ID.");
                if (model.fileUploads == null || !model.fileUploads.Any()) return (false, "No files to upload.");
                
                // businness add image
                foreach (var item in model.fileUploads)
                {
                    var vehicleImage = new VehicleImage
                    {
                        VehicleId = model.VehicleId
                    };
                    var imagePath = await _fileUploadHelper.UploadFileAsync(item, "vehicleImages");
                    vehicleImage.Path = imagePath;
                    await _unitOfWork.VehicleImageRepository.SaveOrUpdateAsync(vehicleImage);
                }
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateImageAsync(VehicleImageEditViewModel model)
        {
            try
            {
                // validate input data
                if (model.VehicleImageId <= 0) return (false, "Invalid Vehicle Image ID.");
                if (model.Path != null) return (false, "No files to upload.");

                // businness add image
                var vehicleImage = new VehicleImage
                {
                    VehicleId = model.VehicleId,
                    VehicleImageId = model.VehicleImageId
                };
                var imagePath = await _fileUploadHelper.UploadFileAsync(model.ImageFile, "vehicleImages");
                vehicleImage.Path = imagePath;
                await _unitOfWork.VehicleImageRepository.SaveOrUpdateAsync(vehicleImage);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteImageAsync(int id)
        {
            if (id <= 0) return (false, "Invalid ID.");
            try
            {
                var vehicleImage = await _unitOfWork.VehicleImageRepository.GetDataByIdAsync(id);
                if (vehicleImage == null) return (false, "Vehicle Image not found.");
                await _unitOfWork.VehicleImageRepository.DeleteAsync(vehicleImage);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<VehicleImageEditViewModel> GetImageByIdAsync(int id)
        {
            var query = await _unitOfWork.VehicleImageRepository.GetDataByIdAsync(id);
            var data = _mapper.Map<VehicleImageEditViewModel>(query);
            return data;
        }

        public async Task<List<VehicleDTO>> GetAllExportAsync()
        {
            try
            {
                var vehicleQuery = _unitOfWork.VehicleRepository.GetAllAsync(null,
               include: query => query
               .Include(x => x.Supplier)
               .Include(x => x.Company)
             );
                var vehicle = await vehicleQuery;
                var vehicleList = vehicle.ToList();
                var data = _mapper.Map<List<VehicleDTO>>(vehicleList);
                return data;
            }
            catch (Exception ex) {
                return null;
            }
        }
    }
}
