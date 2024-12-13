using Microsoft.AspNetCore.Mvc;
using VehicleShowroom.Management.DataAccess.DataAccess;
using VehicleShowroom.Management.Domain.Entities;
using VehicleShowroomManagementSystem.Areas.Admin.Controllers;

namespace VehicleShowroom.Management.UI.Areas.Admin.Controllers
{
    public class StockHistoryController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public StockHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách StockHistory
        public IActionResult Index()
        {
            /*var stockHistory = _context.StockHistory.ToList();  // Lấy tất cả bản ghi từ database
            return View(stockHistory);  // Trả về View để hiển thị danh sách*/
            return View();
        }

      /*  // Thêm bản ghi mới
        [HttpPost]
        public IActionResult Add(StockHistory model)
        {
            if (ModelState.IsValid)  // Kiểm tra tính hợp lệ của model
            {
                _context.StockHistory.Add(model);  // Thêm bản ghi vào database
                _context.SaveChanges();  // Lưu thay đổi vào database
                return RedirectToAction("Index");  // Chuyển hướng về trang Index
            }
            return View(model);  // Nếu model không hợp lệ, vẫn hiển thị form nhập liệu
        }

        // Xóa bản ghi
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var stockHistory = _context.StockHistory.FirstOrDefault(x => x.StockHistoryId == id);  // Tìm bản ghi theo ID
            if (stockHistory != null)
            {
                _context.StockHistory.Remove(stockHistory);  // Xóa bản ghi
                _context.SaveChanges();  // Lưu thay đổi vào database
            }
            return RedirectToAction("Index");  // Chuyển hướng về trang Index sau khi xóa
        }*/
    }
}
