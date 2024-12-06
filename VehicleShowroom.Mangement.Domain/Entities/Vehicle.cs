using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    [Table("Vehicles")]
    public class Vehicle : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleId { get; set; }

        [StringLength(50)]
        public string ModelNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        [StringLength(20)]
        public string? Status { get; set; } = "Active";

        public DateTime? DateAdded { get; set; } = DateTime.Now;

        public string? Description { get; set; }

        // Foreign Key to Suppliers
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        // Foreign Key to Branches
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
