namespace VehicleShowroomManagementSystem.Models
{
    public class ResponseDatatable<T>
    {
        public int RecordsTotal { get; set; }  // Tổng số bản ghi
        public int RecordsFiltered { get; set; }  // Tổng số bản ghi đã lọc
        public List<T> Data { get; set; }  // Dữ liệu của bảng (SupplierDTO)
    }
}
