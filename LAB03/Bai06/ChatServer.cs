using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bai06
{
    public class ChatServer
    {
        private TcpListener listener;
        // Dictionary lưu trữ Client: Key là Username, Value là TcpClient
        public Dictionary<string, TcpClient> currentClients = new Dictionary<string, TcpClient>();
        private bool isRunning = false;

        public int Port { get; private set; } = 8888;

        // Event để gửi thông báo trạng thái về Form Server
        public event Action<string> OnLogReceived;

        private void Log(string message)
        {
            OnLogReceived?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        public void Start()
        {
            if (isRunning) return;
            try
            {
                listener = new TcpListener(IPAddress.Any, Port);
                listener.Start();
                isRunning = true;
                Log($"Server is running in port {Port}");

                Task.Run(() => ListenForNewClients());
            }
            catch (Exception ex)
            {
                Log($"Startip error: {ex.Message}");
                isRunning = false;
            }
        }

        private void ListenForNewClients()
        {
            try
            {
                while (isRunning)
                {
                    // Chặn và đợi Client mới
                    TcpClient client = listener.AcceptTcpClient();

                    // Xử lý Client trên Task riêng (Task.Run là cơ chế đa luồng)
                    Task.Run(() => HandleClient(client));
                }
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
            {
                // Xảy ra khi listener.Stop() được gọi
            }
            catch (Exception ex)
            {
                Log($"Listen error: {ex.Message}");
            }
        }

        public void HandleClient(TcpClient client)
        {
            string clientUsername = "Unknown";
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    while (isRunning && client.Connected)
                    {
                        Message msg = MessageHelper.ReceiveMessage(stream);
                        if (msg == null) break;

                        switch (msg.Type)
                        {
                            case "STATUS_JOIN":
                                clientUsername = msg.Sender;
                                if (!AddClient(clientUsername, client)) return;
                                Log($"Client has  connected: {clientUsername}");

                                Broadcast(msg, clientUsername); // Broadcast tham gia
                                BroadcastUserListToAll(); // Cập nhật danh sách cho tất cả
                                break;

                            case "TEXT":
                            case "FILE_TXT":
                            case "FILE_PNG":
                            case "FILE_JPG":
                                if (msg.Receiver == "BROADCAST")
                                {
                                    Broadcast(msg, clientUsername);
                                }
                                else
                                {
                                    SendPrivateMessage(msg);
                                }
                                break;

                            case "STATUS_LEAVE":
                                Broadcast(msg, clientUsername, true); // Broadcast rời đi
                                return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Error client ({clientUsername}): {ex.Message}");
            }
            finally
            {
                RemoveClient(clientUsername);
                client.Close();
            }
        }

        // --- LOGIC QUẢN LÝ CLIENT & LOCKING ---

        private bool AddClient(string username, TcpClient client)
        {
            lock (currentClients) // Dùng lock để đảm bảo an toàn đa luồng khi sửa Dictionary
            {
                if (currentClients.ContainsKey(username))
                {
                    Log($"Warning: {username} was existed.");
                    return false;
                }
                currentClients.Add(username, client);
                return true;
            }
        }

        private void RemoveClient(string username)
        {
            if (string.IsNullOrEmpty(username) || username == "Unknown") return;

            lock (currentClients)
            {
                if (currentClients.Remove(username))
                {
                    Log($"Client disconnected: {username}");

                    // Cập nhật danh sách người dùng cho các client còn lại
                    BroadcastUserListToAll();
                }
            }
        }

        private List<string> GetUsernames()
        {
            lock (currentClients)
            {
                var usernames = currentClients.Keys.ToList();
                usernames.Insert(0, "BROADCAST"); // Thêm kênh chung vào đầu
                return usernames;
            }
        }

        // --- LOGIC GỬI TIN NHẮN VÀ DANH SÁCH ---

        public void BroadcastUserListToAll()
        {
            var usernames = GetUsernames();
            string userListJson = JsonSerializer.Serialize(usernames);

            var userListMsg = new Message("SYSTEM", "BROADCAST", userListJson, "STATUS_USERS");

            Log("Updating is successful!");

            lock (currentClients)
            {
                foreach (var c in currentClients.Values)
                {
                    if (c.Connected)
                        MessageHelper.SendMessage(c.GetStream(), userListMsg);
                }
            }
        }

        public void Broadcast(Message msg, string senderUsername, bool includeSender = false)
        {
            Log($"BROADCAST from {msg.Sender} ({msg.Type}): {msg.Data}");

            lock (currentClients)
            {
                foreach (var pair in currentClients)
                {
                    string receiverUsername = pair.Key;
                    TcpClient receiverClient = pair.Value;

                    if (!includeSender && receiverUsername == senderUsername) continue;

                    if (receiverClient.Connected)
                    {
                        MessageHelper.SendMessage(receiverClient.GetStream(), msg);
                    }
                }
            }
        }

        public void SendPrivateMessage(Message msg)
        {
            Log($" PRIVATE from {msg.Sender} to {msg.Receiver}: {msg.Data}");

            lock (currentClients)
            {
                // Gửi đến người nhận (msg.Receiver)
                if (currentClients.TryGetValue(msg.Receiver, out TcpClient receiverClient))
                {
                    if (receiverClient.Connected)
                    {
                        MessageHelper.SendMessage(receiverClient.GetStream(), msg);
                    }
                }

                // Gửi lại cho chính người gửi (msg.Sender) để họ thấy tin nhắn đã gửi
                if (currentClients.TryGetValue(msg.Sender, out TcpClient senderClient))
                {
                    if (senderClient.Connected)
                    {
                        MessageHelper.SendMessage(senderClient.GetStream(), msg);
                    }
                }
            }
        }

        public void Stop()
        {
            if (!isRunning) return;
            isRunning = false;

            var shutdownMsg = new Message("SERVER", "ALL", "Server was stopped!", "STATUS_SHUTDOWN");

            lock (currentClients)
            {
                foreach (var c in currentClients.Values)
                {
                    if (c.Connected)
                        MessageHelper.SendMessage(c.GetStream(), shutdownMsg);
                    c.Close();
                }
                currentClients.Clear();
            }

            listener?.Stop();
            Log("Server was stopped!");
        }
    }
}