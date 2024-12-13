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
        public string? Slug { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [StringLength(20)]
        public string? Status { get; set; } = "Available";

        public string? Description { get; set; }

        // Foreign Key to Suppliers
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        // Foreign Key to Branches
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [StringLength(50)]
        public string? EngineNumber { get; set; } //Số máy

        [StringLength(50)]
        public string? ChassisNumber { get; set; } //Số khung

        [StringLength(50)]
        public string FuelType { get; set; } // Loại nhiên liệu

        [StringLength(50)]
        public string TransmissionType { get; set; } // Loại hộp số

        [StringLength(50)]
        public string Color { get; set; } // Màu sắc xe

        [Column(TypeName = "decimal(15,2)")]
        public decimal? Price { get; set; } // Giá xe

        [Column(TypeName = "decimal(10,2)")]
        public decimal? Mileage { get; set; } // Số km đã đi

        public int? ManufactureYear { get; set; } // Năm sản xuất
    }
}
