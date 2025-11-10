using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading; // Thêm thư viện Threading
using System.Windows.Forms;

namespace Lab03
{
   
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread; // Luồng nhận dữ liệu
        private bool isConnected = false;
        private List<Movie> danhSachPhim = new List<Movie>();

        public ClientForm()
        {
            InitializeComponent();
            // Tắt kiểm tra luồng để cho phép luồng nhận dữ liệu cập nhật UI
            // Nếu không dùng cách này, bạn phải dùng Invoke/BeginInvoke
            CheckForIllegalCrossThreadCalls = false;
        }

        // ==========================
        // 🔹 SỰ KIỆN LOAD FORM
        // ==========================
        private void Lab03Bai04_Load(object sender, EventArgs e) // ⚠️ Lưu ý: Tên hàm có thể là ClientForm_Load
        {
            try
            {
                // 🟡 Gắn sự kiện click cho tất cả các nút ghế
                // Giả định tên GroupBox chứa ghế là grpSeats
                Control grpSeats = this.Controls.Find("grpSeats", true).FirstOrDefault();

                if (grpSeats != null)
                {
                    foreach (Control c in grpSeats.Controls)
                    {
                        if (c is Button btn && (btn.Text.StartsWith("A") || btn.Text.StartsWith("B") || btn.Text.StartsWith("C")))
                        {
                            btn.BackColor = Color.LightGray;
                            btn.Click += SeatButton_Click;
                        }
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

                    // Giả định tên ComboBox Phim là cboMovie
                    ComboBox cboMovie = this.Controls.Find("cboMovie", true).FirstOrDefault() as ComboBox;
                    if (cboMovie != null)
                        cboMovie.Items.Add(tenPhim);
                }

                ComboBox cboMovieControl = this.Controls.Find("cboMovie", true).FirstOrDefault() as ComboBox;
                if (cboMovieControl != null && cboMovieControl.Items.Count > 0)
                    cboMovieControl.SelectedIndex = 0;
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
            ComboBox cboMovie = sender as ComboBox;
            ComboBox cboRoom = this.Controls.Find("cboRoom", true).FirstOrDefault() as ComboBox;

            if (cboRoom == null || cboMovie == null) return;

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
            TextBox txtServerIP = this.Controls.Find("txtServerIP", true).FirstOrDefault() as TextBox;
            TextBox txtPort = this.Controls.Find("txtPort", true).FirstOrDefault() as TextBox;
            TextBox txtCustomerName = this.Controls.Find("txtCustomerName", true).FirstOrDefault() as TextBox;
            Label lblStatus = this.Controls.Find("lblStatus", true).FirstOrDefault() as Label;

            if (txtServerIP == null || txtPort == null || txtCustomerName == null || lblStatus == null) return;

            if (isConnected)
            {
                WriteLog("Hiện tại đã kết nối, không cần kết nối lại.");
                return;
            }

            try
            {
                string ip = txtServerIP.Text.Trim();
                int port = int.Parse(txtPort.Text.Trim());

                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                isConnected = true;

                // 💡 KHỞI TẠO VÀ CHẠY LUỒNG NHẬN DỮ LIỆU
                receiveThread = new Thread(new ThreadStart(ReceiveData));
                receiveThread.IsBackground = true;
                receiveThread.Start();
                // ----------------------------------------

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
                stream?.Close();
                client?.Close();
                isConnected = false;
            }
        }

        // ==========================
        // 🔹 NHẬN VÀ XỬ LÝ DỮ LIỆU TỪ SERVER (CHẠY TRÊN LUỒNG RIÊNG)
        // ==========================
        private void ReceiveData()
        {
            byte[] buffer = new byte[4096];
            int bytesRead;

            try
            {
                while (isConnected)
                {
                    if (stream == null || !stream.CanRead)
                    {
                        Thread.Sleep(100); // Ngủ một chút nếu stream chưa sẵn sàng
                        continue;
                    }

                    bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                        HandleServerResponse(message);
                    }
                    else if (bytesRead == 0)
                    {
                        // Server đóng kết nối
                        throw new SocketException();
                    }
                }
            }
            catch (Exception ex)
            {
                if (isConnected)
                {
                    WriteLog($"⚠️ Mất kết nối Server: {ex.Message}");
                    isConnected = false;

                    // Cập nhật UI (Sử dụng CheckForIllegalCrossThreadCalls = false đã khắc phục)
                    Label lblStatus = this.Controls.Find("lblStatus", true).FirstOrDefault() as Label;
                    if (lblStatus != null)
                    {
                        lblStatus.Text = "Đã ngắt kết nối";
                        lblStatus.ForeColor = Color.Red;
                    }
                }
            }
        }

        // ==========================
        // 🔹 XỬ LÝ PHẢN HỒI TỪ SERVER
        // ==========================
        private void HandleServerResponse(string message)
        {
            // Cập nhật Log trên UI
            WriteLog($"[SERVER] Nhận: {message}");

            string[] parts = message.Split('|');
            string command = parts[0];

            switch (command)
            {
                case "WELCOME":
                    // Server xác nhận kết nối
                    WriteLog($"📢 Server: Chào mừng {parts.ElementAtOrDefault(1)}");
                    // Bạn có thể gửi yêu cầu lấy trạng thái ghế ban đầu ở đây
                    // SendData("GET_SEATS"); 
                    break;

                case "SEAT_UPDATE":
                    // Server gửi thông tin cập nhật trạng thái ghế
                    // Ví dụ: SEAT_UPDATE|A1,B2,C3|BOOKED
                    // Phần logic UpdateSeatState cần được viết để đổi màu các nút ghế
                    // UpdateSeatState(parts.ElementAtOrDefault(1), parts.ElementAtOrDefault(2)); 
                    break;

                case "BOOKED_CONFIRM":
                    // Server xác nhận đặt vé thành công
                    WriteLog($"✅ Server xác nhận: Đặt {parts.ElementAtOrDefault(1)} thành công!");
                    break;

                case "ERROR":
                    // Server thông báo lỗi (ví dụ: Ghế đã được đặt)
                    WriteLog($"❌ LỖI TỪ SERVER: {parts.ElementAtOrDefault(1)}");
                    break;
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

            // Đổi màu ghế (Nếu LightGray -> Yellow, ngược lại LightGray)
            if (btn.BackColor == Color.LightGray)
            {
                btn.BackColor = Color.Yellow;
                // Có thể gửi thông điệp PRE_BOOK|Ghế đến Server để giữ chỗ tạm thời
                // SendData($"PRE_BOOK|{btn.Text}"); 
            }
            else if (btn.BackColor == Color.Yellow)
            {
                btn.BackColor = Color.LightGray;
                // Có thể gửi thông điệp CANCEL_PRE_BOOK|Ghế đến Server
                // SendData($"CANCEL_PRE_BOOK|{btn.Text}"); 
            }
            // Không làm gì nếu ghế đã là màu Red (đã đặt)
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

            ComboBox cboMovie = this.Controls.Find("cboMovie", true).FirstOrDefault() as ComboBox;
            ComboBox cboRoom = this.Controls.Find("cboRoom", true).FirstOrDefault() as ComboBox;
            TextBox txtCustomerName = this.Controls.Find("txtCustomerName", true).FirstOrDefault() as TextBox;
            Control grpSeats = this.Controls.Find("grpSeats", true).FirstOrDefault();

            if (cboMovie == null || cboRoom == null || txtCustomerName == null || grpSeats == null) return;


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
            string room = cboRoom.Text.Replace("Phòng ", "");
            string customer = txtCustomerName.Text;

            var phim = danhSachPhim.FirstOrDefault(p => p.TenPhim == movie);
            int giaVe = phim?.GiaVeChuan ?? 0;
            int tongTien = count * giaVe;

            // Gửi yêu cầu đặt vé
            string message = $"BOOK|{customer}|{movie}|{room}|{selectedSeats.TrimEnd(',')}|{tongTien}";
            SendData(message);

            // Ghi Log trên Client (chờ Server xác nhận để biết chắc chắn)
            WriteLog($"🎟️ Gửi yêu cầu đặt {count} ghế ({selectedSeats.TrimEnd(',')}) - Tổng tiền: {tongTien:N0}đ");
        }

        // ==========================
        // 🔹 NÚT HỦY CHỌN
        // ==========================
        private void btnClear_Click(object sender, EventArgs e)
        {
            Control grpSeats = this.Controls.Find("grpSeats", true).FirstOrDefault();
            if (grpSeats == null) return;

            foreach (Control c in grpSeats.Controls)
            {
                if (c is Button && c.BackColor == Color.Yellow)
                {
                    c.BackColor = Color.LightGray;
                }
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
                if (stream != null && stream.CanWrite && isConnected)
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
            TextBox txtLog = this.Controls.Find("txtLog", true).FirstOrDefault() as TextBox;
            if (txtLog != null)
            {
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
            }
        }

        // ==========================
        // 🔹 ĐÓNG FORM
        // ==========================
        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e) // ⚠️ Lưu ý: Tên hàm có thể là Lab03Bai04_FormClosing
        {
            try
            {
                if (isConnected)
                {
                    isConnected = false; // Đặt cờ ngắt kết nối trước

                    // Gửi thông điệp DISCONNECT để Server biết
                    SendData("DISCONNECT");

                    // Dừng luồng nhận dữ liệu
                    receiveThread?.Abort();

                    stream?.Close();
                    client?.Close();
                    WriteLog("🔌 Đã ngắt kết nối server.");
                }
            }
            catch { }
        }

        // ==========================
        // 🔹 HÀM DƯ THỪA (Đã được giữ lại theo code của bạn)
        // ==========================
        private void grpCustomer_Enter(object sender, EventArgs e)
        {

        }
    }
}