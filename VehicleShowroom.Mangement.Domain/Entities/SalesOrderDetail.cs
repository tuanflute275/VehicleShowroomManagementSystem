using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    [Table("SalesOrderDetails")]
    public class SalesOrderDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesOrderDetailId { get; set; }

        // Foreign Key to SalesOrders
        [ForeignKey("SalesOrder")]
        public int SalesOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }

        // Foreign Key to Vehicles
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public int? Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(15, 2)")]
        public decimal UnitPrice { get; set; }
    }
}
