using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleShowroom.Management.Application.Models.DTOs
{
    public class SaleOrderExportDTO
    {
        public int SalesOrderId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
