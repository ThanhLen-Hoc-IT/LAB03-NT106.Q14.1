using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Bai01
{
    public partial class FrmUDPServer : Form
    {
        private Thread thdUDPServer;
        private UdpClient udpClient;
        private bool isRunning = false;
        public FrmUDPServer()
        {
            InitializeComponent();
            
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                thdUDPServer = new Thread(ServerThread);
                thdUDPServer.IsBackground = true;
                thdUDPServer.Start();
                btnListen.Enabled = false;
            }

        }

        private void ServerThread()
        {
            try
            {
                int port = int.Parse(txtPort.Text) ;
                udpClient = new UdpClient(port);

                while (isRunning)
                {
                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                    string message = Encoding.UTF8.GetString(receiveBytes); // Hỗ trợ tiếng Việt
                    string ip = RemoteIpEndPoint.Address.ToString();

                    //Viết hàm InfoMessage để hiển thị thông điệp lên List View
                    InfoMessage(ip,message);
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Không thể khởi động server: " + ex.Message);
            }
        }

        private void InfoMessage(string ip, string message)
        {
            if (lvReceivedMessage.InvokeRequired)
            {
                // Gọi lại trên UI thread
                lvReceivedMessage.Invoke(new Action<string, string>(InfoMessage), ip, message);
            }
            else
            {
                // Tạo dòng mới hiển thị
                ListViewItem item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
                item.SubItems.Add(ip);
                item.SubItems.Add(message);
                lvReceivedMessage.Items.Add(item);

                // Cuộn xuống dòng mới nhất
                lvReceivedMessage.EnsureVisible(lvReceivedMessage.Items.Count - 1);
            }
        }

        private void FrmUDPServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            udpClient?.Close();
            thdUDPServer?.Join(500); // chờ thread dừng nhẹ nhàng
        }

        private void FrmUDPServer_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false; // cho phép update UI từ thread khác

            lvReceivedMessage.View = View.Details;          
            lvReceivedMessage.FullRowSelect = true;         
            lvReceivedMessage.GridLines = true;             
            lvReceivedMessage.Columns.Add("Time", 80);
            lvReceivedMessage.Columns.Add("IP", 120);
            lvReceivedMessage.Columns.Add("Message", 300);
        }
    }
}
