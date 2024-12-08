using AutoMapper;
using System.Security.Principal;
using VehicleShowroom.Mangement.Application.Abstracts;
using VehicleShowroom.Mangement.Application.Models.ViewModels;
using VehicleShowroom.Mangement.Domain.Abstract;
using VehicleShowroom.Mangement.Domain.Entities;

namespace VehicleShowroom.Mangement.Application.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Login(LoginViewModel model)
        {
            var checkUser = await _unitOfWork.UserRepository.GetByUsernameAsync(model.UsernameOrEmail);
            if (checkUser == null) { 
            }

            return false;
        }

        public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                var checkUsername = await _unitOfWork.UserRepository.GetByUsernameAsync(model.UserName);
                var checkEmail = await _unitOfWork.UserRepository.GetByEmailAsync(model.Email);
                if (checkUsername != null)
                    return (false, "The username already exists in the system!");
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
    }
}
