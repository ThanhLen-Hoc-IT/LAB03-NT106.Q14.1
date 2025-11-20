// File: TicketInfo.cs
using System;

namespace TicketBookingApp
{
    // Cần tạo lớp này để Server có thể gửi và Client có thể nhận/phân tích dữ liệu vé
    public class TicketInfo
    {
        public string SoGhe { get; set; }
        public string LoaiVe { get; set; }
        public bool IsBooked { get; set; }
        public string ClientId { get; set; } // Ai đã đặt vé này
        public double GiaThanh { get; set; }
        // Thêm các thuộc tính cần thiết khác như PhimDangChieu, PhongChieu...
        public string PhimDangChieu { get; set; }
    }
}