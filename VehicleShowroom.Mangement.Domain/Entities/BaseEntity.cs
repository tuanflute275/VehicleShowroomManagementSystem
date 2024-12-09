using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    public class BaseEntity
    {
        [MaxLength(100)]
        public string? CreateBy { get; set; } = "Admin"; // Người tạo bản ghi

        public DateTime? CreateDate { get; set; } = DateTime.Now; // Thời gian tạo

        [MaxLength(100)]
        public string? UpdateBy { get; set; } // Người cập nhật bản ghi

        public DateTime? UpdateDate { get; set; } // Thời gian cập nhật

        [MaxLength(100)]
        public string? DeleteBy { get; set; } // Người xóa bản ghi

        public DateTime? DeleteDate { get; set; } // Thời gian xóa

        public bool? DeleteFlag { get; set; } = false; // Cờ xóa (true: đã xóa, false: chưa xóa)
    }
}
