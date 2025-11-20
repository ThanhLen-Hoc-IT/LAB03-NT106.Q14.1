
using System;
using System.Windows.Forms;

namespace TicketBookingApp
{
    // Đảm bảo Form này được đặt tên là Dashboard
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            this.Text = "Quản lý Phòng vé (V3) - Dashboard";
        }

        // Sự kiện cho nút Khởi động Server
        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo Form Server
                Server serverForm = new Server();
                serverForm.Show(); // Mở Form Server
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở Form Server: {ex.Message}\nHãy đảm bảo Form class tên là 'Server' đã được tạo.", "Lỗi Khởi tạo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện cho nút Mở Quầy vé (Client)
        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo Form Client
                Client clientForm = new Client();
                clientForm.Show(); // Mở Form Client
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở Form Client: {ex.Message}\nHãy đảm bảo Form class tên là 'Client' đã được tạo.", "Lỗi Khởi tạo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}