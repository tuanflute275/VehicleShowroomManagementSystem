using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    [Table("Companies")]
    public class Company : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(15)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Active";
    }
}
