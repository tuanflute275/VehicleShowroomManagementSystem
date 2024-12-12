namespace VehicleShowroom.Management.UI.Utils
{
    public class UserConnection
    {
        public string UserName { get; set; }  // Tên người dùng
        public string IpAddress { get; set; }  // Địa chỉ IP
        public string FullName { get; set; }   // Tên đầy đủ
        public string Email { get; set; }      // Email của người dùng
        public string Role { get; set; }       // Vai trò của người dùng (ví dụ: Admin, User, v.v.)
        public DateTime ConnectionTime { get; set; }  // Thời gian kết nối của người dùng
    }
}
