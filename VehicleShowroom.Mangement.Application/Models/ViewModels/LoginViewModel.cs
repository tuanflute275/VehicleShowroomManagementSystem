using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Application.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username or Email is required.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }
}
