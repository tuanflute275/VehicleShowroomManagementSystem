using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Application.Models.ViewModels
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Supplier name is required.")]
        public string SupplierName { get; set; }

        public string? ContactPerson { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email name is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Url(ErrorMessage = "Invalid website URL format.")]
        public string? Website { get; set; }

        [StringLength(20, ErrorMessage = "Tax code cannot be longer than 20 characters.")]
        public string? TaxCode { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "Bank account number cannot be longer than 50 characters.")]
        public string? BankAccount { get; set; }

        [StringLength(100, ErrorMessage = "Bank name cannot be longer than 100 characters.")]
        public string? BankName { get; set; }

        [StringLength(50, ErrorMessage = "Contract number cannot be longer than 50 characters.")]
        public string? ContractNumber { get; set; }

        public DateTime? ContractDate { get; set; } = DateTime.Now;

        [StringLength(20, ErrorMessage = "Status cannot be longer than 20 characters.")]
        public string? Status { get; set; }

        public string? Notes { get; set; }
    }
}
