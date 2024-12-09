using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Application.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
        public string FullName { get; set; }
        public string Avatar { get; set; }

        [Display(Name = "Password")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string? Address { get; set; }
        public byte? Gender { get; set; } = 0; // 0: Unspecified, 1: Male, 2: Female, 3: Prefer not to say
        public string? Department { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? HireDate { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal? Salary { get; set; }
        public int? Status { get; set; } = 1; // 1: Active, 0: Inactive
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        [StringLength(50, ErrorMessage = "Emergency contact cannot exceed 50 characters.")]
        public string? EmergencyContact { get; set; }
        public int? Role { get; set; } = 0; // 0: User, 1: Admin, etc.
    }
}
