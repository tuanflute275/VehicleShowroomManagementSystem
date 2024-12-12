using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace VehicleShowroom.Management.Domain.Entities
{
    [Table("StockHistory")]
    public class StockHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockHistoryId { get; set; }

        // Foreign Key to Vehicles
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        public DateTime? ChangeDate { get; set; }

        [StringLength(20)]
        public string ChangeType { get; set; }

        public int Quantity { get; set; }
    }
}
