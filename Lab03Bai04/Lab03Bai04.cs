using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Lab03
{
    public partial class Lab03Bai04 : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected = false;
        private List<Movie> danhSachPhim = new List<Movie>();

        public Lab03Bai04()
        {
            InitializeComponent();
        }

        // ==========================
        // 🔹 SỰ KIỆN LOAD FORM
        // ==========================
        private void Lab03Bai04_Load(object sender, EventArgs e)
        {
            try
            {
                // 🟡 Gắn sự kiện click cho tất cả các nút ghế
                foreach (Control c in grpSeats.Controls)
                {
                    if (c is Button btn && (btn.Text.StartsWith("A") || btn.Text.StartsWith("B") || btn.Text.StartsWith("C")))
                    {
                        btn.BackColor = Color.LightGray;
                        btn.Click += SeatButton_Click;
                    }
                }

                // 🟢 Đọc dữ liệu phim
                string filePath = "input5.txt";
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"Không tìm thấy file dữ liệu: {filePath}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length < 3) continue;

                    string tenPhim = parts[0].Trim();

                    if (!int.TryParse(parts[1].Trim(), out int giaVe))
                        continue;

                    var phongList = new List<int>();
                    foreach (var p in parts[2].Split(','))
                    {
                        if (int.TryParse(p.Trim(), out int soPhong))
                            phongList.Add(soPhong);
                    }

                    if (phongList.Count == 0)
                        continue;

                    danhSachPhim.Add(new Movie
                    {
                        TenPhim = tenPhim,
                        GiaVeChuan = giaVe,
                        PhongChieu = phongList
                    });

                    cboMovie.Items.Add(tenPhim);
                }

                if (cboMovie.Items.Count > 0)
                    cboMovie.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc file input5.txt: " + ex.Message);
            }
        }

        // ==========================
        // 🔹 KHI CHỌN PHIM
        // ==========================
        private void cboMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboRoom.Items.Clear();

            int index = cboMovie.SelectedIndex;
            if (index >= 0 && index < danhSachPhim.Count)
            {
                var phim = danhSachPhim[index];
                foreach (int p in phim.PhongChieu)
                {
                    cboRoom.Items.Add($"Phòng {p}");
                }

                if (cboRoom.Items.Count > 0)
                    cboRoom.SelectedIndex = 0;
            }
        }

        // ==========================
        // 🔹 NÚT KẾT NỐI SERVER
        // ==========================
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = txtServerIP.Text.Trim();
                int port = int.Parse(txtPort.Text.Trim());

                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                isConnected = true;

                lblStatus.Text = "Đã kết nối server";
                lblStatus.ForeColor = Color.Green;
                WriteLog($"✅ Kết nối thành công đến server {ip}:{port}");

                // Gửi thông tin khách hàng
                string message = $"CONNECT|{txtCustomerName.Text}";
                SendData(message);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Kết nối thất bại";
                lblStatus.ForeColor = Color.Red;
                WriteLog($"❌ Lỗi kết nối: {ex.Message}");
            }
        }

        // ==========================
        // 🔹 SỰ KIỆN CLICK GHẾ
        // ==========================
        private void SeatButton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Vui lòng kết nối server trước!", "Thông báo");
                return;
            }

            Button btn = sender as Button;
            if (btn == null) return;

            // Đổi màu ghế
            if (btn.BackColor == Color.LightGray)
                btn.BackColor = Color.Yellow;
            else
                btn.BackColor = Color.LightGray;
        }

        // ==========================
        // 🔹 NÚT ĐẶT VÉ
        // ==========================
        private void btnBook_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                MessageBox.Show("Chưa kết nối server!", "Lỗi");
                return;
            }

            string selectedSeats = "";
            int count = 0;

            foreach (Control c in grpSeats.Controls)
            {
                if (c is Button && c.BackColor == Color.Yellow)
                {
                    selectedSeats += c.Text + ",";
                    count++;
                }
            }

            if (count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 ghế!", "Cảnh báo");
                return;
            }

            string movie = cboMovie.Text;
            string room = cboRoom.Text;
            string customer = txtCustomerName.Text;

            var phim = danhSachPhim.FirstOrDefault(p => p.TenPhim == movie);
            int giaVe = phim?.GiaVeChuan ?? 0;
            int tongTien = count * giaVe;

            string message = $"BOOK|{customer}|{movie}|{room}|{selectedSeats.TrimEnd(',')}|{tongTien}";
            SendData(message);

            WriteLog($"🎟️ {customer} đặt {count} ghế ({selectedSeats.TrimEnd(',')}) - Tổng tiền: {tongTien:N0}đ");
        }

        // ==========================
        // 🔹 NÚT HỦY CHỌN
        // ==========================
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control c in grpSeats.Controls)
            {
                if (c is Button)
                    c.BackColor = Color.LightGray;
            }

            WriteLog("🔄 Đã hủy chọn toàn bộ ghế.");
        }

        // ==========================
        // 🔹 GỬI DỮ LIỆU QUA TCP
        // ==========================
        private void SendData(string message)
        {
            try
            {
                if (stream != null && stream.CanWrite)
                {
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                WriteLog("⚠️ Lỗi gửi dữ liệu: " + ex.Message);
            }
        }

        // ==========================
        // 🔹 GHI LOG
        // ==========================
        private void WriteLog(string msg)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
        }

        // ==========================
        // 🔹 ĐÓNG FORM
        // ==========================
        private void Lab03Bai04_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (isConnected)
                {
                    SendData("DISCONNECT");
                    stream?.Close();
                    client?.Close();
                    WriteLog("🔌 Đã ngắt kết nối server.");
                }
            }
            catch { }
        }
    }
}
