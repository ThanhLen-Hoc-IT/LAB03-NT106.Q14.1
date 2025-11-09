using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai06
{
    public partial class FrmServer : Form
    {
        private ChatServer server;
        public FrmServer()
        {
            InitializeComponent();
            this.Text = "Chat Server Manager";

            // 1. Khởi tạo ChatServer và đăng ký sự kiện Log
            server = new ChatServer();
            server.OnLogReceived += Server_OnLogReceived;

            btnStop.Enabled = false;
            UpdateStatusLabel("Server is stopped", Color.Red);
        }

        // --- XỬ LÝ LOG (AN TOÀN ĐA LUỒNG) ---

        private void Server_OnLogReceived(string logMessage)
        {
            // Kiểm tra InvokeRequired để cập nhật TextBox an toàn
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action<string>(Server_OnLogReceived), logMessage);
                return;
            }

            rtbLog.AppendText(logMessage + Environment.NewLine);
        }

        private void UpdateStatusLabel(string status, Color color)
        {
            // Kiểm tra InvokeRequired để cập nhật Label an toàn
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action<string, Color>(UpdateStatusLabel), status, color);
            }
            else
            {
                lblStatus.Text = $"Status: {status}";
                lblStatus.ForeColor = color;
            }
        }

        // --- XỬ LÝ UI EVENTS ---
        private void btnStart_Click(object sender, EventArgs e)
        {
            // Chạy Server trên một Task nền để không chặn UI
            Task.Run(() =>
            {
                server.Start();
            });

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            UpdateStatusLabel("Server is running", Color.Green);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            UpdateStatusLabel("Server is stopped", Color.Red);
        }

        private void FrmServer_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }


    }
}
