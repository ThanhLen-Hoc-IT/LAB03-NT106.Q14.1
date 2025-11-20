using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TicketBookingApp
{
    public partial class Client : Form
    {

        private ComboBox cboPhong;
        private ComboBox cboPhim;
        private Label lblPhong;
        private Label lblPhim;

        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        private Dictionary<string, Button> seatButtons = new Dictionary<string, Button>();
        private List<string> selectedSeats = new List<string>();

        public Client()
        {
            InitializeComponent();
            FixUnicode();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            lblPhong = new Label()
            {
                Text = "Phòng chiếu:",
                Location = new Point(20, 50),
                AutoSize = true
            };
            this.Controls.Add(lblPhong);

            cboPhong = new ComboBox()
            {
                Location = new Point(120, 46),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboPhong.Items.AddRange(new string[] { "Phòng 1", "Phòng 2", "Phòng 3" });
            cboPhong.SelectedIndex = 0;
            this.Controls.Add(cboPhong);

            lblPhim = new Label()
            {
                Text = "Phim:",
                Location = new Point(20, 85),
                AutoSize = true
            };
            this.Controls.Add(lblPhim);

            cboPhim = new ComboBox()
            {
                Location = new Point(120, 81),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboPhim.Items.AddRange(new string[]
            {
                "Mai", "Lật mặt", "Avengers", "Conan", "Yêu lại từ đầu"
            });
            cboPhim.SelectedIndex = 0;
            this.Controls.Add(cboPhim);

            // Tạo panel chứa sơ đồ ghế
            Panel seatContainer = new Panel();
            seatContainer.Name = "seatContainer";
            seatContainer.Dock = DockStyle.Fill;
            seatContainer.AutoScroll = true;
            seatContainer.BackColor = Color.White;

            // Thêm panel vào form
            this.Controls.Add(seatContainer);

            // Đưa grpSeatMap vào panel
            seatContainer.Controls.Add(grpSeatMap);

            // Mở rộng groupbox theo số ghế để panel scroll được
            grpSeatMap.AutoSize = true;
            grpSeatMap.AutoSizeMode = AutoSizeMode.GrowAndShrink;


            GenerateSeatButtons();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = txtServerIP.Text.Trim();
                if (!int.TryParse(txtPort.Text.Trim(), out int port))
                {
                    MessageBox.Show("Port không hợp lệ.", "Lỗi");
                    return;
                }

                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();

                AddLog($"Đã kết nối tới server {ip}:{port}");

                receiveThread = new Thread(ReceiveLoop);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                btnConnect.Enabled = false;

                SendMessage("VIEW");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối Server: " + ex.Message);
            }
        }

        private void ReceiveLoop()
        {
            try
            {
                byte[] buffer = new byte[4096];

                while (client != null && client.Connected)
                {
                    int count = stream.Read(buffer, 0, buffer.Length);
                    if (count <= 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, count);

                    Invoke(new Action(() => HandleServerMessage(msg)));
                }
            }
            catch
            {
                AddLog("Mất kết nối với server.");
            }
            finally
            {
                btnConnect.Enabled = true;
            }
        }

        private void HandleServerMessage(string msg)
        {
            AddLog($"📥 Server: {msg}");

            if (msg.Contains("="))
                UpdateSeatFromServer(msg);
        }

        private void GenerateSeatButtons()
        {
            string[] rows = { "A", "B", "C" };
            int seatsPerRow = 5;

            grpSeatMap.Controls.Clear();
            seatButtons.Clear();

            int startX = 40;
            int startY = 40;
            int buttonW = 75;
            int buttonH = 55;
            int gap = 15;

            foreach (string row in rows)
            {
                for (int i = 1; i <= seatsPerRow; i++)
                {
                    string seatId = row + i;

                    Button btn = new Button();
                    btn.Text = seatId;
                    btn.Name = seatId;
                    btn.Width = buttonW;
                    btn.Height = buttonH;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.BackColor = Color.LightGreen;

                    btn.Location = new Point(
                        startX + (i - 1) * (buttonW + gap),
                        startY + Array.IndexOf(rows, row) * (buttonH + 20)
                    );

                    btn.Click += Seat_Click;

                    grpSeatMap.Controls.Add(btn);
                    seatButtons[seatId] = btn;
                }
            }
        }

        private void Seat_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string seatId = btn.Text;

            if (btn.BackColor == Color.Red)
            {
                MessageBox.Show("Ghế này đã được đặt!");
                return;
            }

            if (btn.BackColor == Color.Orange)
            {
                btn.BackColor = Color.LightGreen;
                selectedSeats.Remove(seatId);
            }
            else
            {
                btn.BackColor = Color.Orange;
                selectedSeats.Add(seatId);
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Hãy chọn ghế trước.");
                return;
            }

            foreach (string seat in selectedSeats)
            {
                SendMessage("BOOK|" + seat);
                AddLog($"📨 Gửi BOOK: {seat}");
                Thread.Sleep(50);
            }

            selectedSeats.Clear();
            SendMessage("VIEW");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (selectedSeats.Count == 0)
            {
                MessageBox.Show("Hãy chọn ghế để hủy.");
                return;
            }

            foreach (string seat in new List<string>(selectedSeats))
            {
                SendMessage("CANCEL|" + seat);
                AddLog($"📨 Gửi CANCEL: {seat}");
                Thread.Sleep(50);
            }

            selectedSeats.Clear();
            SendMessage("VIEW");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SendMessage("VIEW");
            AddLog("📨 Gửi VIEW (refresh).");
        }

        // ==============================
        // FIX LỖI GỘP LỆNH — THÊM \n
        // ==============================
        private void SendMessage(string msg)
        {
            try
            {
                if (stream != null)
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg + "\n");
                    stream.Write(data, 0, data.Length);
                }
            }
            catch
            {
                AddLog("Không thể gửi dữ liệu.");
            }
        }

        private void UpdateSeatFromServer(string seatData)
        {
            string[] items = seatData.Split(';');

            foreach (string s in items)
            {
                if (string.IsNullOrWhiteSpace(s)) continue;

                string[] p = s.Split('=');
                string seat = p[0];
                int state = int.Parse(p[1]);

                if (seatButtons.ContainsKey(seat))
                {
                    Button b = seatButtons[seat];
                    b.BackColor = (state == 1 ? Color.Red : Color.LightGreen);
                }
            }
        }

        private void FixUnicode()
        {
            this.Font = new Font("Segoe UI", 10);
        }

        private void AddLog(string text)
        {
            rtbClientLog.AppendText("[" +
                DateTime.Now.ToString("HH:mm:ss") +
                "] " + text + Environment.NewLine);
        }
    }
}
