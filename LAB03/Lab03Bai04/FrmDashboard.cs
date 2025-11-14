using System;
using System.Windows.Forms;

namespace Lab03
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent(); // Sửa lỗi CS0103 nếu có
            this.Text = "🎬 Điều hướng - Quản lý phòng vé";
        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            // Mở Form Server - ⚠️ ĐÃ SỬA TÊN CLASS
            FrmServer serverForm = new FrmServer();
            serverForm.Show();
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            // Mở Form Client - (Giữ nguyên ClientForm, nếu tên file là FrmClient, bạn cần sửa lại)
            ClientForm clientForm = new ClientForm();
            clientForm.Show();
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
        }
    }
}