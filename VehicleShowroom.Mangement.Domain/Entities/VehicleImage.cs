﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    [Table("VehicleImages")]
    public class VehicleImage : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VehicleImageId { get; set; }

        [Required]
        [StringLength(200)]
        public string Path { get; set; }

        // Foreign Key to Vehicles
        [ForeignKey("Vehicle")]
        public int VehiclelId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
