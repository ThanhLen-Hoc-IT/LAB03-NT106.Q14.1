using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Lab03Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "🎬 Lab03 - Simple TCP Server";

            int port = 5000;
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();

            Console.WriteLine($"✅ Server đang chạy tại 127.0.0.1:{port}");
            Console.WriteLine("⏳ Đang chờ client kết nối...\n");

            List<TcpClient> clients = new List<TcpClient>();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(client);
                Console.WriteLine($"📡 Client kết nối: {client.Client.RemoteEndPoint}");

                Thread t = new Thread(() => HandleClient(client, clients));
                t.IsBackground = true;
                t.Start();
            }
        }

        static void HandleClient(TcpClient client, List<TcpClient> clients)
        {
            NetworkStream ns = client.GetStream();
            byte[] buffer = new byte[4096];
            int bytesRead;

            try
            {
                while ((bytesRead = ns.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    Console.WriteLine($"📩 Nhận: {msg}");

                    if (msg.StartsWith("CONNECT|"))
                    {
                        string name = msg.Substring(8);
                        string reply = $"WELCOME|Xin chào {name}!";
                        SendMessage(ns, reply);
                        Console.WriteLine($"👋 Gửi: {reply}");
                    }
                    else if (msg.StartsWith("BOOK|"))
                    {
                        string[] parts = msg.Split('|');
                        string customer = parts.Length > 1 ? parts[1] : "Unknown";
                        string movie = parts.Length > 2 ? parts[2] : "";
                        string room = parts.Length > 3 ? parts[3] : "";
                        string seats = parts.Length > 4 ? parts[4] : "";
                        string price = parts.Length > 5 ? parts[5] : "";

                        string reply = $"BOOKED|{customer}|{movie}|{room}|{seats}|{price}";
                        SendMessage(ns, reply);
                        Console.WriteLine($"🎟️ Đặt vé thành công cho {customer}: {movie} ({room}) - {seats} ({price}đ)");
                    }
                    else if (msg == "DISCONNECT")
                    {
                        Console.WriteLine("🔌 Client đã ngắt kết nối.");
                        break;
                    }
                    else
                    {
                        SendMessage(ns, "ECHO|" + msg);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Lỗi client: " + ex.Message);
            }
            finally
            {
                ns.Close();
                client.Close();
                lock (clients) clients.Remove(client);
            }
        }

        static void SendMessage(NetworkStream ns, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            ns.Write(data, 0, data.Length);
        }
    }
}

