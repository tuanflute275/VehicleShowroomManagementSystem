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
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private FileUploadHelper _fileUploadHelper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, FileUploadHelper fileUploadHelper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileUploadHelper = fileUploadHelper;
        }
        public async Task<(bool Success, string ErrorMessage, UserDTO user)> Login(LoginViewModel model)
        {
            var accountExist = await _unitOfWork.UserRepository.GetByUsernameOrEmailAsync(model.UsernameOrEmail);
            if (accountExist == null || !BCrypt.Net.BCrypt.Verify(model.Password, accountExist.Password))
                return (false, "Invalid username or password!", null);
            var data = _mapper.Map<UserDTO>(accountExist);
            return (true, string.Empty, data);
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                var checkUsername = await _unitOfWork.UserRepository.GetByUsernameAsync(model.UserName);
                if (checkUsername != null)
                    return (false, "The username already exists in the system!");

                var checkEmail = await _unitOfWork.UserRepository.GetByEmailAsync(model.Email);
                if (checkEmail != null)
                    return (false, "The email already exists in the system!");


                string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, 12);
                var user = new User
                {
                    Username = model.UserName,
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = passwordHash,
                    Role = 0
                };
                await _unitOfWork.UserRepository.SaveOrUpdateAsync(user);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception ex) 
            {
                return (false, "An error occurred while registering an account.");
            }
        }

        public async Task<IPagedList<UserDTO>> GetAllPaginationAsync(string keyword, int page, int pageSize = 8)
        {
            var userQuery = _unitOfWork.UserRepository.GetAllAsync(
                expression: s => string.IsNullOrEmpty(keyword) || s.Username.Contains(keyword) || s.FullName.Contains(keyword)
            );
            var users = await userQuery;
            var specialUsernames = new[] { "admin", "employee", "invoice", "user" };
            var sortedUsers = users
           .OrderBy(s => Array.IndexOf(specialUsernames, s.Username) >= 0 ? Array.IndexOf(specialUsernames, s.Username) : int.MaxValue)
           .ThenBy(s => s.Username);
            var userList = sortedUsers.ToList();
            var data = _mapper.Map<List<UserDTO>>(userList);
            return data.ToPagedList(page, pageSize); 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return false;
                }

                await _unitOfWork.UserRepository.DeleteAsync(user);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(UserViewModel model, IFormFile? fileUpload, string? oldImage = null)
        {
            try
            {
                var user = new User();
                if (model.UserId == 0)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, 12);
                    user = _mapper.Map<User>(model);
                    user.Password = passwordHash;
                    user.CreateBy = "Admin";
                    user.CreateDate = DateTime.Now;
                    if (fileUpload != null)
                    {
                        var avatar = await _fileUploadHelper.UploadFileAsync(fileUpload, "users");
                        user.Avatar = avatar;
                    }
                }
                else
                {
                    user = await _unitOfWork.UserRepository.GetByIdAsync(model.UserId);
                    if (user == null)
                        return (false, "User not found");
                    user.Username = model.Username;
                    user.FullName = model.FullName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Address = model.Address;
                    user.Gender = model.Gender;
                    user.Department = model.Department;
                    user.JobTitle = model.JobTitle;
                    user.Salary = model.Salary;
                    user.DateOfBirth = model.DateOfBirth;
                    user.Nationality = model.Nationality;
                    user.EmergencyContact = model.EmergencyContact;
                    user.Role = model.Role;
                    user.Status = model.Status;
                    user.UpdateBy = "Admin";
                    user.UpdateDate = DateTime.Now;
                    if (!string.IsNullOrWhiteSpace(model.Password))
                        user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password, 12);
                    if (fileUpload != null) // Kiểm tra xem ImageFile có tồn tại hay không
                    {
                        var avatar = await _fileUploadHelper.UploadFileAsync(fileUpload, "users");
                        user.Avatar = avatar;
                    }
                }
                bool result = await _unitOfWork.UserRepository.SaveOrUpdateAsync(user);
                await _unitOfWork.SaveChangeAsync();
                return (true, null);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var userQuery = _unitOfWork.UserRepository.GetAllAsync();
            var users = await userQuery;
            var userList = users.ToList();
            var data = _mapper.Map<List<UserDTO>>(userList);
            return data;
        }

        public async Task<UserVehicleInfoDTO> GetDataExportByIdAsync(int userId)
        {
            var salesOrderDetails = await _unitOfWork.SalesOrderDetailRepository.GetAllAsync(
                expression: x => x.SalesOrder.UserId == userId,
                include: query => query
                    .Include(x => x.SalesOrder)
                        .ThenInclude(so => so.User)
                   .Include(x => x.Vehicle)
                    .ThenInclude(v => v.Supplier)
                    .Include(x => x.Vehicle.Company)
            );

            var salesOrderDetailsList = salesOrderDetails.ToList();
            var user = salesOrderDetailsList.FirstOrDefault()?.SalesOrder?.User;
            if (user == null) return null;

            // Ánh xạ dữ liệu thành DTO
            var userVehicleInfo = new UserVehicleInfoDTO
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Gender = user.Gender == 1 ? "Male" : "Female",
                Vehicles = salesOrderDetailsList.Select(detail => new VehicleUserDTO
                {
                    ModelNumber = detail.Vehicle.ModelNumber,
                    VehicleName = detail.Vehicle.Name,
                    SupplierName = detail.Vehicle.Supplier?.SupplierName,
                    CompanyName = detail.Vehicle.Company?.CompanyName,
                    Price = (decimal)detail.Vehicle.Price
                }).ToList()
            };

            return userVehicleInfo;
        }
    }
}
