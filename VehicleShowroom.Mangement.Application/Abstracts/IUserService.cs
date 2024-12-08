using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Application.Abstracts
{
    public interface IUserService
    {
        Task<bool> Login(LoginViewModel model);
        Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model);
    }
}
