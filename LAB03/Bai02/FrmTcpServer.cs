using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bai02
{
    public partial class FrmTcpServer : Form
    {
        public FrmTcpServer()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            // Cho phép thread khác cập nhật giao diện
            CheckForIllegalCrossThreadCalls = false;

            Thread serverThread = new Thread(new ThreadStart(StartServer));
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        void StartServer()
        {
            Socket listenerSocket = null;

            try
            {
                // 1. Tạo socket listener
                listenerSocket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                );

                // 2. Bind IP và port
                IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
                listenerSocket.Bind(ipepServer);

                // 3. Lắng nghe
                listenerSocket.Listen(10);
                lvCommand.Items.Add(new ListViewItem("Server is running on port 8080..."));

                // 4. Vòng lặp chờ client
                while (true)
                {
                    lvCommand.Items.Add(new ListViewItem("Waiting for client..."));

                    // Chờ client kết nối
                    Socket clientSocket = listenerSocket.Accept();
                    lvCommand.Items.Add(new ListViewItem("New client connected."));

                    // 5. Xử lý client
                    byte[] recv = new byte[1024];
                    StringBuilder buffer = new StringBuilder();

                    try
                    {
                        while (true)
                        {
                            int bytesReceived = clientSocket.Receive(recv);
                            if (bytesReceived == 0)
                            {
                                // Client đóng kết nối
                                lvCommand.Items.Add(new ListViewItem("Client disconnected."));
                                clientSocket.Close();
                                break;
                            }

                            // Ghép chuỗi nhận được
                            buffer.Append(Encoding.ASCII.GetString(recv, 0, bytesReceived));

                            // Nếu kết thúc bằng newline (Enter trong Telnet gửi \r\n)
                            if (buffer.ToString().EndsWith("\n"))
                            {
                                string msg = buffer.ToString().Trim();
                                lvCommand.Items.Add(new ListViewItem("Client: " + msg));

                                buffer.Clear();
                            }
                        }
                    }
                    catch (SocketException)
                    {
                        lvCommand.Items.Add(new ListViewItem("Lost connection."));
                        try { clientSocket.Close(); } catch { }
                    }
                    catch (Exception ex)
                    {
                        lvCommand.Items.Add(new ListViewItem("Error: " + ex.Message));
                        try { clientSocket.Close(); } catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                lvCommand.Items.Add(new ListViewItem("Startup error: " + ex.Message));
                try { listenerSocket?.Close(); } catch { }
            }
        }
    }
}
