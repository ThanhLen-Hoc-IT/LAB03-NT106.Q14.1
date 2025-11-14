using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab03Bai03
{
    public partial class ServerForm : Form
    {
        private TcpListener listener;
        private Thread listenerThread;
        private bool running = false;
        private List<TcpClient> connectedClients = new List<TcpClient>();

        public ServerForm()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                listenerThread = new Thread(StartListening);
                listenerThread.IsBackground = true;
                listenerThread.Start();
                btnListen.Enabled = false;
            }
        }

        private void StartListening()
        {
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(ip, 8080);
                listener.Start();
                running = true;
                UpdateLog("Server started! Listening on 127.0.0.1:8080");

                while (running)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    lock (connectedClients)
                    {
                        connectedClients.Add(client);
                    }
                    UpdateLog("Connection accepted from " + ((IPEndPoint)client.Client.RemoteEndPoint).ToString());

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (SocketException)
            {
                UpdateLog("Server stopped listening.");
            }
            catch (Exception ex)
            {
                UpdateLog("Error: " + ex.Message);
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                NetworkStream ns = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = ns.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string msg = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                    string from = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    UpdateLog($"From {from}: {msg}");
                }
            }
            catch (Exception ex)
            {
                UpdateLog("Client handler error: " + ex.Message);
            }
            finally
            {
                lock (connectedClients)
                {
                    connectedClients.Remove(client);
                }
                try { client.Close(); } catch { }
                UpdateLog("Client disconnected.");
            }
        }

        private void UpdateLog(string text)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => txtLog.AppendText(text + Environment.NewLine)));
            }
            else
            {
                txtLog.AppendText(text + Environment.NewLine);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            running = false;
            try { listener?.Stop(); } catch { }

            lock (connectedClients)
            {
                foreach (var c in connectedClients)
                {
                    try { c.Close(); } catch { }
                }
                connectedClients.Clear();
            }

            base.OnFormClosing(e);
        }
    }
}
