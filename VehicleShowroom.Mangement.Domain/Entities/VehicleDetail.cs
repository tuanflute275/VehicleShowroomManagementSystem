using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    [Table("VehicleDetails")]
    public class VehicleDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleDetailId { get; set; }

        // Foreign Key to Vehicles
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [StringLength(50)]
        public string EngineNumber { get; set; }

        [StringLength(50)]
        public string ChassisNumber { get; set; }

        [StringLength(30)]
        public string Color { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal Price { get; set; }

        [StringLength(20)]
        public string? FuelType { get; set; }

        public int? ManufactureYear { get; set; }

        [StringLength(20)]
        public string? TransmissionType { get; set; }

        public int? Mileage { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Weight { get; set; }

        [StringLength(50)]
        public string? Dimensions { get; set; }

        [StringLength(10)]
        public string? ColorCode { get; set; }
    }
}
