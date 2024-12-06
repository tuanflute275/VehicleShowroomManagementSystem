using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    public class BaseEntity
    {
        [MaxLength(100)]
        public string? CreateBy { get; set; } // Người tạo bản ghi

        public DateTime? CreateDate { get; set; } = DateTime.Now; // Thời gian tạo

        [MaxLength(100)]
        public string? UpdateBy { get; set; } // Người cập nhật bản ghi

        public DateTime? UpdateDate { get; set; } // Thời gian cập nhật

        [MaxLength(100)]
        public string? DeleteBy { get; set; } // Người xóa bản ghi

        public DateTime? DeleteDate { get; set; } // Thời gian xóa

        [MaxLength(1)]
        public string? DeleteFlag { get; set; } // Cờ xóa (Y: đã xóa, N: chưa xóa)
    }
}
