using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai06
{
    public class ChatClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private bool isConnected = false;

        public string Username { get; private set; }
        public bool IsConnected => isConnected;

        // Lưu trữ lịch sử chat: Key là tên người nhận/nhóm, Value là RichTextBox lưu log
        public Dictionary<string, RichTextBox> ChatHistory { get; private set; }

        public event Action<Message> OnMessageReceived;
        public event Action<string> OnConnectionLost;

        public ChatClient()
        {
            ChatHistory = new Dictionary<string, RichTextBox>();
        }

        public bool Connect(string username, string serverIp = "172.20.10.6", int port = 8888)
        {
            try
            {
                client = new TcpClient();
                client.Connect(serverIp, port);
                stream = client.GetStream();
                isConnected = true;
                Username = username;

                // Gửi thông báo tham gia
                var joinMsg = new Message(username, "BROADCAST", $"{username} has joined!", "STATUS_JOIN");
                MessageHelper.SendMessage(stream, joinMsg);

                Task.Run(() => ListenForMessages());

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void ListenForMessages()
        {
            try
            {
                while (isConnected)
                {
                    Message msg = MessageHelper.ReceiveMessage(stream);
                    if (msg == null) break;

                    OnMessageReceived?.Invoke(msg);
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi trong quá trình nhận
            }
            finally
            {
                if (isConnected)
                {
                    isConnected = false;
                    OnConnectionLost?.Invoke("Lost connection to Server");
                    stream?.Close();
                    client?.Close();
                }
            }
        }

        // Thay đổi hàm SendMessage để nhận Message đã được tạo sẵn
        public void SendMessage(Message msg)
        {
            if (!isConnected) return;
            MessageHelper.SendMessage(stream, msg);
        }

        public void Disconnect()
        {
            if (!isConnected) return;
            isConnected = false;

            try
            {
                var leaveMsg = new Message(Username, "BROADCAST", $"{Username} has left", "STATUS_LEAVE");
                MessageHelper.SendMessage(stream, leaveMsg);
            }
            catch { /* Bỏ qua nếu gửi thất bại */ }

            stream?.Close();
            client?.Close();
            OnConnectionLost?.Invoke("You has disconnected.");
        }
    }
}