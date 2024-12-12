using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using VehicleShowroom.Management.UI.Utils;

namespace VehicleShowroom.Management.Application.Services
{
    public class UserHub : Hub
    {
        // Dùng ConcurrentDictionary để lưu trữ kết nối và thông tin người dùng
        private static ConcurrentDictionary<string, UserConnection> _onlineUsers = new ConcurrentDictionary<string, UserConnection>();

        // Phương thức gọi khi người dùng tham gia
        public async Task Join()
        {
            var connectionId = Context.ConnectionId;
            var ipAddress = Context.GetHttpContext()?.Connection?.RemoteIpAddress?.ToString();
            if (ipAddress == "::1")
            {
                ipAddress = "127.0.0.1";
            }
            // Lấy thông tin người dùng từ context hoặc claims
            var username = Context.User.Identity.Name; 
            var fullName = Context.User.FindFirst("userFullName")?.Value; 
            var email = Context.User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var role = Context.User.FindFirst("Role")?.Value;


            if (string.IsNullOrEmpty(username))
            {
                // Nếu không đăng nhập, tạo thông tin người dùng mặc định (khách)
                username = $"Guest-{connectionId}";
                fullName = "Guest";
                email = "No Email";
                role = "Guest";
            }

            // Tạo đối tượng UserConnection với thông tin người dùng
            var userConnection = new UserConnection
            {
                UserName = username ?? "Unknown username",
                FullName = fullName ?? "Unknown full name",
                Email = email ?? "Unknown email",
                Role = role ?? "Unknown role",
                IpAddress = ipAddress,
                ConnectionTime = DateTime.Now
            };

            // Thêm thông tin người dùng vào dictionary
            _onlineUsers.TryAdd(connectionId, userConnection);

            // Gửi số người dùng online và thông tin người dùng cho tất cả client
            await Clients.All.SendAsync("UpdateUserCount", _onlineUsers.Count);
            await Clients.All.SendAsync("UpdateOnlineUsers", _onlineUsers.Values);
        }

        // Phương thức gọi khi người dùng rời đi
        public async Task Leave()
        {
            var connectionId = Context.ConnectionId;

            // Xóa kết nối của người dùng khi họ rời
            _onlineUsers.TryRemove(connectionId, out _);

            // Gửi số người dùng online và thông tin người dùng cho tất cả client
            await Clients.All.SendAsync("UpdateUserCount", _onlineUsers.Count);
            await Clients.All.SendAsync("UpdateOnlineUsers", _onlineUsers.Values);
        }

        // Lấy số lượng người dùng online
        public int GetOnlineUserCount()
        {
            return _onlineUsers.Count;
        }

        // Lấy danh sách người dùng online
        public ConcurrentDictionary<string, UserConnection> GetOnlineUsers()
        {
            return _onlineUsers;
        }
    }
}
