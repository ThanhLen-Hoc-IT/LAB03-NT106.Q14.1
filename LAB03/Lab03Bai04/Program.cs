using System;
using System.Windows.Forms;

namespace TicketBookingApp
{
    static class Program
    {
        /// <summary>
        /// Điểm khởi động chính cho ứng dụng.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Bật Visual Styles (giúp giao diện trông hiện đại hơn)
            Application.EnableVisualStyles();

            // Thiết lập chế độ kết xuất văn bản tương thích
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi chạy Form Dashboard (Form chính)
            // Đảm bảo Form class của bạn được đặt tên là Dashboard
            Application.Run(new Dashboard());
        }
    }
}
