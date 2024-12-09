using Microsoft.AspNetCore.Http;
using VehicleShowroom.Mangement.Application.Models.DTOs;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using X.PagedList;

namespace VehicleShowroom.Mangement.Application.Abstracts
{
    public interface IUserService
    {
        Task<(bool Success, string ErrorMessage, UserDTO user)> Login(LoginViewModel model);
        Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model);
        Task<IPagedList<UserDTO>> GetPagedUsersAsync(string keyword, int page, int pageSize = 8);
        Task<bool> DeleteAsync(int id);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<(bool Success, string ErrorMessage)> SaveOrUpdateAsync(UserViewModel model, IFormFile? fileUpload, string? oldImage = null);
    }
}
