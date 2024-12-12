using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Domain.Entities
{
    [Table("Suppliers")]
    public class Supplier : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(100)]
        public string SupplierName { get; set; }

        [StringLength(100)]
        public string? ContactPerson { get; set; }

        [StringLength(15)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? Website { get; set; }

        [StringLength(20)]
        public string? TaxCode { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(50)]
        public string? BankAccount { get; set; }

        [StringLength(100)]
        public string? BankName { get; set; }

        [StringLength(50)]
        public string? ContractNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        [StringLength(20)]
        public string? Status { get; set; } = "Active";

        public string? Notes { get; set; }
    }
}
