using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Application.Models.ViewModels
{
    public class CompanyViewModel
    {
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? Email { get; set; }
        [RegularExpression("^(Active|Inactive)$", ErrorMessage = "Status must be either 'Active' or 'Inactive'")]
        public string Status { get; set; }
    }
}
