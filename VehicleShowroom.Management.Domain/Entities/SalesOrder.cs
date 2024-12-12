using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Domain.Entities
{
    [Table("SalesOrders")]
    public class SalesOrder : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesOrderId { get; set; }

        // Foreign Key to Customers
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime? OrderDate { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(20)]
        public string Status { get; set; }
    }
}
