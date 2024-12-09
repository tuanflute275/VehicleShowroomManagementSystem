using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats.Webp;
using Image = SixLabors.ImageSharp.Image;

namespace VehicleShowroom.Mangement.Application.Utils
{
    public class FileUploadHelper
    {
        private readonly IWebHostEnvironment _environment;

        public FileUploadHelper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadFileAsync(IFormFile fileUpload, string folder, string oldImage = null)
        {
            var _rootPath = _environment.ContentRootPath;
            if (fileUpload == null || fileUpload.Length == 0)
            {
                throw new ArgumentException("Tệp tải lên không hợp lệ.");
            }

            // Mặc định các định dạng tệp được chấp nhận nếu không truyền vào
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png",".webp",".jfif" };
            var fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Định dạng tệp không hợp lệ. Chỉ chấp nhận tệp hình ảnh.");
            }

            // Nếu có tệp hình ảnh cũ, xóa tệp cũ
            if (!string.IsNullOrEmpty(oldImage))
            {
                var oldFilePath = Path.Combine(_rootPath, "wwwroot", "Uploads", folder, oldImage);
                if (File.Exists(oldFilePath))
                {
                    try
                    {
                        File.Delete(oldFilePath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Không thể xóa tệp cũ: {oldFilePath}. Lỗi: {ex.Message}");
                    }
                }
            }

            // Tạo tên tệp mới (sử dụng GUID để tránh trùng lặp)
            var fileName = $"{Guid.NewGuid()}.webp"; // Đảm bảo tên tệp là .webp
            // Xác định đường dẫn lưu tệp
            var folderPath = Path.Combine(_rootPath, "wwwroot", "Uploads", folder);
            var filePath = Path.Combine(folderPath, fileName);

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Nén hình ảnh thành định dạng WebP và lưu vào hệ thống
            using (var image = Image.Load(fileUpload.OpenReadStream()))
            {
                // Cấu hình nén với chất lượng
                var encoder = new WebpEncoder()
                {
                    Quality = 75 // Điều chỉnh chất lượng (0 - 100)
                };

                // Lưu hình ảnh dưới định dạng WebP
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.SaveAsync(fileStream, encoder);
                }
            }

            return fileName; // Trả về tên tệp đã lưu
        }
    }
}
