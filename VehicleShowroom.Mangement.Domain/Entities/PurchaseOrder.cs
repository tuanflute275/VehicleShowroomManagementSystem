using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    [Table("PurchaseOrders")]
    public class PurchaseOrder : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseOrderId { get; set; }

        // Foreign Key to Suppliers
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public DateTime? OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(15, 2)")]
        public decimal? TotalAmount { get; set; } = 0;
    }
}
