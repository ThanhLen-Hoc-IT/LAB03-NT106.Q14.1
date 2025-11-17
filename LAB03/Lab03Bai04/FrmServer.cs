using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace Lab03
{
    public partial class FrmServer : Form
    {
        private TcpListener listener;
        private Thread listenThread;
        private List<TcpClient> clients = new List<TcpClient>();
        private bool isRunning = false;
        private readonly int PORT = 5000;
        private readonly IPAddress IP = IPAddress.Any;

        // KHÔNG KHAI BÁO CÁC CONTROLS Ở ĐÂY (txtLog, btnStart, btnStop),
        // Vì chúng đã được khai báo trong FrmServer.Designer.cs (để tránh lỗi trùng lặp CS0102/CS0229).

        public FrmServer()
        {
            InitializeComponent();
            this.Text = "🎬 TCP Server - Quản lý phòng vé";
            // Cho phép gọi UI từ Thread khác
            Control.CheckForIllegalCrossThreadCalls = false;

            // Thiết lập trạng thái ban đầu (sử dụng tên controls trực tiếp)
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                try
                {
                    listener = new TcpListener(IP, PORT);
                    listenThread = new Thread(new ThreadStart(StartListening));
                    listenThread.IsBackground = true;
                    listenThread.Start();

                    isRunning = true;
                    WriteLog($"✅ Server đang chạy tại {PORT}");
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                catch (Exception ex)
                {
                    WriteLog($"❌ Lỗi khởi động Server (Có thể cổng {PORT} đã bị chiếm): {ex.Message}");
                    isRunning = false;
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                }
            }
        }

        private void StartListening()
        {
            try
            {
                listener.Start();
                WriteLog("⏳ Đang chờ client kết nối...");

                while (isRunning)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    lock (clients) clients.Add(client);
                    WriteLog($"📡 Client kết nối: {client.Client.RemoteEndPoint}");

                    // Gửi lời chào ngay sau khi kết nối
                    SendMessage(client.GetStream(), $"WELCOME|Server chào mừng bạn!");

                    Thread t = new Thread(() => HandleClient(client));
                    t.IsBackground = true;
                    t.Start();
                }
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted || ex.ErrorCode == 10004)
            {
                // Lỗi khi listener.Stop() được gọi
                WriteLog("🔌 Server đã dừng lắng nghe.");
            }
            catch (Exception ex)
            {
                WriteLog("❌ Lỗi Server: " + ex.Message);
            }
            finally
            {
                if (listener != null) listener.Stop();
                isRunning = false;
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] buffer = new byte[4096];
            int bytesRead;

            try
            {
                while (client.Connected && (bytesRead = ns.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    WriteLog($"📩 Nhận từ {client.Client.RemoteEndPoint}: {msg}");

                    ProcessCommand(ns, client, msg);
                }
            }
            catch (Exception ex)
            {
                WriteLog($"⚠️ Mất kết nối client {client.Client.RemoteEndPoint}: {ex.Message}");
            }
            finally
            {
                lock (clients) clients.Remove(client);
                ns.Close();
                client.Close();
            }
        }

        private void ProcessCommand(NetworkStream ns, TcpClient client, string msg)
        {
            string[] parts = msg.Split('|');
            string command = parts[0];

            switch (command)
            {
                case "CONNECT":
                    // Xử lý thông tin client nếu cần
                    break;

                case "BOOK":
                    // Xử lý logic đặt vé, kiểm tra trùng lặp
                    string reply = "BOOKED_CONFIRM|Đặt vé của bạn đã được xác nhận.";
                    SendMessage(ns, reply);
                    break;

                case "DISCONNECT":
                    WriteLog($"🔌 Client ngắt kết nối: {client.Client.RemoteEndPoint}");
                    client.Close();
                    break;

                default:
                    SendMessage(ns, "ERROR|Lệnh không hợp lệ.");
                    break;
            }
        }

        private void SendMessage(NetworkStream ns, string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                ns.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                WriteLog("⚠️ Lỗi gửi dữ liệu: " + ex.Message);
            }
        }

        private void WriteLog(string msg)
        {
            // Sử dụng txtLog trực tiếp
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                WriteLog("🛑 Đang dừng server...");
                listener?.Stop();
            }
        }

        private void FrmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRunning)
            {
                listener?.Stop();
                lock (clients)
                {
                    foreach (var client in clients) client.Close();
                }
                listenThread?.Join(500);
            }
        }

        private void FrmServer_Load(object sender, EventArgs e)
        {
        }
    }
}