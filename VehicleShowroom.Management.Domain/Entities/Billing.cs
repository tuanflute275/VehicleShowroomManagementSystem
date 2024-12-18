using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Management.Domain.Entities
{
    [Table("Billing")]
    public class Billing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillingId { get; set; }

        [ForeignKey("SalesOrder")]
        public int SaleOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime BillingDate { get; set; }

        public decimal Amount { get; set; }

        [StringLength(255)]
        public string PaymentMethod { get; set; } // E.g., "Credit Card", "Cash", etc.

        public string Status { get; set; } // E.g., "Paid", "Pending", "Failed"

        public string Notes { get; set; } // Any additional notes regarding the billing

        public DateTime? PaidDate { get; set; }
    }
}
