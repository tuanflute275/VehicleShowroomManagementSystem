namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class SupplierDTO
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        public string? ContactPerson { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public string? TaxCode { get; set; }

        public string Address { get; set; }

        public string? BankAccount { get; set; }

        public string? BankName { get; set; }

        public string? ContractNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        public string? Status { get; set; }

        public string? Notes { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
