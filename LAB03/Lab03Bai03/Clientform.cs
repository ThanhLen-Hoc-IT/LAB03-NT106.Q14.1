using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Lab03Bai03
{
    public partial class ClientForm : Form
    {
        private TcpClient tcpClient;
        private NetworkStream ns;

        public ClientForm()
        {
            InitializeComponent();
            btnSend.Enabled = false;
            btnDisconnect.Enabled = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint endPoint = new IPEndPoint(ip, 8080);

                tcpClient = new TcpClient();
                tcpClient.Connect(endPoint);
                ns = tcpClient.GetStream();

                Log("Connected to server.");
                btnSend.Enabled = true;
                btnDisconnect.Enabled = true;
                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                Log("Connect error: " + ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                Log("Not connected to server!");
                return;
            }

            try
            {
                string text = txtInput.Text;
                if (string.IsNullOrWhiteSpace(text)) return;

                byte[] data = Encoding.UTF8.GetBytes(text + "\n");
                ns.Write(data, 0, data.Length);
                Log("Sent: " + text);
                txtInput.Clear();
            }
            catch (Exception ex)
            {
                Log("Send error: " + ex.Message);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void Disconnect()
        {
            try
            {
                ns?.Close();
                tcpClient?.Close();
                Log("Disconnected from server.");
            }
            catch { }

            btnSend.Enabled = false;
            btnDisconnect.Enabled = false;
            btnConnect.Enabled = true;
        }

        private void Log(string message)
        {
            if (txtClientLog.InvokeRequired)
            {
                txtClientLog.Invoke(new Action(() => txtClientLog.AppendText(message + Environment.NewLine)));
            }
            else
            {
                txtClientLog.AppendText(message + Environment.NewLine);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Disconnect();
            base.OnFormClosing(e);
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }
    }
}

