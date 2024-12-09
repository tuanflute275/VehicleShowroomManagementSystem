using System.ComponentModel.DataAnnotations;

namespace VehicleShowroom.Mangement.Application.Models.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; } // Họ
        public string FullName { get; set; }
        public string? Avatar { get; set; } // Đường dẫn ảnh đại diện
        public string Email { get; set; } // Email (không trống và duy nhất)
        public string? PhoneNumber { get; set; } // Số điện thoại
        public string? Address { get; set; } // Địa chỉ
        public byte? Gender { get; set; } = 0; //(0-chưa xác định,1-nam, 2 nữ,3-không muốn trả lời)
        public string? Department { get; set; } // Phòng ban
        public string? JobTitle { get; set; } // Chức danh
        public DateTime? HireDate { get; set; }
        public decimal? Salary { get; set; } // Lương
        public int? Status { get; set; } = 1; // Trạng thái (1: Đang làm việc, 0: Đã nghỉ việc)
        public DateTime? DateOfBirth { get; set; } // Ngày sinh
        public string? Nationality { get; set; } // Quốc tịch
        public string? EmergencyContact { get; set; } // Liên hệ khẩn cấp
        public int? Role { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
