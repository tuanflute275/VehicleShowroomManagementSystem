using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Domain.Entities
{
    [Table("PurchaseOrderDetails")]
    public class PurchaseOrderDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseOrderDetailId { get; set; }

        // Foreign Key to PurchaseOrders
        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        // Foreign Key to Vehicles
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public int? Quantity { get; set; } = 0;

        [Column(TypeName = "decimal(15, 2)")]
        public decimal? UnitPrice { get; set; } = 0;
    }
}
