using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } // Họ

        [NotMapped]
        public string FullName { get; set; }

        public string Password { get; set; }

        [MaxLength(200)]
        public string? Avatar { get; set; } // Đường dẫn ảnh đại diện

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } // Email (không trống và duy nhất)

        [MaxLength(15)]
        public string? PhoneNumber { get; set; } // Số điện thoại

        [MaxLength(200)]
        public string? Address { get; set; } // Địa chỉ

        public byte? Gender { get; set; } = 0; //(0-chưa xác định,1-nam, 2 nữ,3-không muốn trả lời)

        [MaxLength(100)]
        public string? Department { get; set; } // Phòng ban

        [MaxLength(100)]
        public string? JobTitle { get; set; } // Chức danh

        public DateTime? HireDate { get; set; } = DateTime.Now; // Ngày bắt đầu làm việc

        public decimal? Salary { get; set; } // Lương

        public int? Status { get; set; } = 1; // Trạng thái (1: Đang làm việc, 0: Đã nghỉ việc)

        public DateTime? DateOfBirth { get; set; } // Ngày sinh

        [MaxLength(100)]
        public string? Nationality { get; set; } // Quốc tịch

        [MaxLength(200)]
        public string? EmergencyContact { get; set; } // Liên hệ khẩn cấp

        public int? Role { get; set; } = 0; // 0 - User, 1-Admin, 2-Employee
    }
}
