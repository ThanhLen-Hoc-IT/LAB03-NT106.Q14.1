// File: Server.cs
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using System.Windows.Forms;

namespace TicketBookingApp
{
    public partial class Server : Form
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private int Port = 8888;

        // Danh sách client
        private readonly List<TcpClient> connectedClients = new List<TcpClient>();

        // ==== BIẾN THỐNG KÊ ====
        private int totalRequests = 0;
        private DateTime serverStartTime;

        public Server()
        {
            InitializeComponent();
            this.Text = "Server Quản lý Phòng vé";

            CheckForIllegalCrossThreadCalls = false;

            TicketManager.LoadSeats();
            UpdateTicketDisplay();

            // Set trạng thái UI
            if (this.Controls.ContainsKey("lblServerStatus"))
                (this.Controls["lblServerStatus"] as Label).Text = "Đang dừng...";
        }

        private void LogMessage(string msg)
        {
            try
            {
                var rtb = this.Controls["rtbServerLog"] as RichTextBox;
                if (rtb != null)
                {
                    rtb.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\n");
                    rtb.ScrollToCaret();
                }
            }
            catch
            {
                Console.WriteLine(msg);
            }
        }

        private void LogToPanel(string msg)
        {
            try
            {
                var txt = this.Controls["txtLog"] as TextBox;
                if (txt != null)
                {
                    txt.AppendText(msg + Environment.NewLine);
                    txt.ScrollToCaret();
                    return;
                }
            }
            catch { }

            LogMessage(msg);
        }

        private void UpdateTicketDisplay()
        {
            try
            {
                var dgv = this.Controls["dgvTicketStatus"] as DataGridView;
                if (dgv == null) return;

                string status = TicketManager.GetSeatStatusString();

                dgv.Columns.Clear();
                dgv.Rows.Clear();
                dgv.Columns.Add("Seat", "Seat");
                dgv.Columns.Add("State", "State");

                var pairs = status.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var p in pairs)
                {
                    var kv = p.Split('=');
                    if (kv.Length == 2)
                        dgv.Rows.Add(kv[0], kv[1]);
                }
            }
            catch { }
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            int portToUse = Port;
            try
            {
                var txtPort = this.Controls["txtPort"] as TextBox;
                if (txtPort != null)
                    int.TryParse(txtPort.Text.Trim(), out portToUse);
            }
            catch { }

            StartServer(portToUse);
        }

        // ============ TÍNH THỐNG KÊ =============
        private void btnCalculateStats_Click(object sender, EventArgs e)
        {
            int online = connectedClients.Count;
            TimeSpan uptime = DateTime.Now - serverStartTime;

            LogToPanel("=== THỐNG KÊ SERVER ===");
            LogToPanel($"📌 Tổng request xử lý: {totalRequests}");
            LogToPanel($"📌 Số client đang online: {online}");
            LogToPanel($"⏱ Uptime: {uptime}");
            LogToPanel("========================");

            MessageBox.Show("Đã tính thống kê! Xem log.", "OK", MessageBoxButtons.OK);
        }

        private void StartServer(int port)
        {
            try
            {
                Port = port;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                tcpListener = new TcpListener(localAddr, Port);
                tcpListener.Start();

                serverStartTime = DateTime.Now;

                listenThread = new Thread(ListenForClients)
                {
                    IsBackground = true
                };
                listenThread.Start();

                LogMessage($"Server đã chạy tại {localAddr}:{Port}");

                var lbl = this.Controls["lblServerStatus"] as Label;
                if (lbl != null) lbl.Text = $"Đang chạy tại port {Port}";
            }
            catch (Exception ex)
            {
                LogMessage("Lỗi StartServer: " + ex.Message);
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            StopServer();
        }

        private void StopServer()
        {
            try
            {
                tcpListener?.Stop();
                listenThread?.Abort();

                lock (connectedClients)
                {
                    foreach (var c in connectedClients)
                        try { c.Close(); } catch { }

                    connectedClients.Clear();
                }

                LogMessage("Server đã dừng.");
            }
            catch (Exception ex)
            {
                LogMessage("Lỗi StopServer: " + ex.Message);
            }
        }

        private void ListenForClients()
        {
            while (true)
            {
                try
                {
                    var client = tcpListener.AcceptTcpClient();

                    lock (connectedClients) connectedClients.Add(client);

                    string info = client.Client.RemoteEndPoint.ToString();
                    LogMessage($"Client kết nối: {info}");

                    new Thread(() => HandleClientComm(client))
                    {
                        IsBackground = true
                    }.Start();
                }
                catch
                {
                    break;
                }
            }
        }

        // TÁCH GÓI TCP, XỬ LÝ TỪNG LỆNH
        private void HandleClientComm(TcpClient client)
        {
            NetworkStream stream = null;

            try
            {
                stream = client.GetStream();
                byte[] buffer = new byte[4096];

                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string raw = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // TÁCH CÁC LỆNH
                    string[] commands = raw.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var cmd in commands)
                    {
                        string clean = cmd.Trim();
                        if (clean == "") continue;

                        totalRequests++;
                        LogMessage($"CLIENT gửi: {clean}");

                        string response = ProcessClientRequest(clean);

                        if (stream.CanWrite)
                        {
                            var outBuf = Encoding.UTF8.GetBytes(response + "\n");
                            stream.Write(outBuf, 0, outBuf.Length);
                        }

                        if (clean.StartsWith("BOOK") || clean.StartsWith("CANCEL"))
                        {
                            Broadcast(TicketManager.GetSeatStatusString());
                            UpdateTicketDisplay();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage("Client disconnect: " + ex.Message);
            }
            finally
            {
                lock (connectedClients) connectedClients.Remove(client);
                try { stream?.Close(); client.Close(); } catch { }
            }
        }

        // ================== PROCESS COMMAND ==================
        private string ProcessClientRequest(string request)
        {
            if (request == "VIEW") return TicketManager.GetSeatStatusString();

            if (request.StartsWith("BOOK|"))
            {
                string seat = request.Substring(5);
                string res = TicketManager.BookSeat(seat);

                return res == "OK" ? $"BOOK_OK|{seat}" : $"BOOK_FAIL|{seat}|{res}";
            }

            if (request.StartsWith("CANCEL|"))
            {
                string seat = request.Substring(7);
                string res = TicketManager.CancelSeat(seat);

                return res == "OK" ? $"CANCEL_OK|{seat}" : $"CANCEL_FAIL|{seat}|{res}";
            }

            return "ERROR|UnknownCommand";
        }

        private void Broadcast(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");

            lock (connectedClients)
            {
                foreach (var c in connectedClients.ToArray())
                {
                    try
                    {
                        if (c.Connected)
                            c.GetStream().Write(data, 0, data.Length);
                    }
                    catch
                    {
                        try { c.Close(); } catch { }
                        connectedClients.Remove(c);
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopServer();
            base.OnFormClosing(e);
        }
    }
}
