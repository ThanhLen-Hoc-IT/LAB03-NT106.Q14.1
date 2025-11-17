using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Bai06
{
    public static class MessageHelper
    {
        /// <summary>
        /// Gửi đối tượng Message qua NetworkStream (dưới dạng JSON) với Length Prefix.
        /// </summary>
        public static void SendMessage(NetworkStream stream, Message message)
        {
            try
            {
                if (stream == null || !stream.CanWrite)
                    return;

                // 1. Chuyển Message thành JSON
                string json = message.ToJson();
                byte[] data = Encoding.UTF8.GetBytes(json);

                // 2. Gửi 4 byte độ dài gói tin
                // Dùng BitConverter để đảm bảo độ dài là 4 byte (int)
                byte[] lengthPrefix = BitConverter.GetBytes(data.Length);
                stream.Write(lengthPrefix, 0, lengthPrefix.Length);

                // 3. Gửi nội dung JSON
                // NetworkStream.Write sẽ chặn cho đến khi toàn bộ data.Length byte được gửi đi.
                stream.Write(data, 0, data.Length);
                stream.Flush(); // Đảm bảo dữ liệu được đẩy ngay lập tức
            }
            catch (IOException ex)
            {
                // Xử lý lỗi khi kết nối đã bị đóng từ xa hoặc lỗi mạng
                Console.WriteLine($"SendMessage Error (Connection closed): {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SendMessage Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Nhận Message từ NetworkStream theo chuẩn Length Prefix.
        /// Hàm này đảm bảo đọc đủ số byte đã công bố.
        /// </summary>
        public static Message ReceiveMessage(NetworkStream stream)
        {
            try
            {
                if (stream == null || !stream.CanRead)
                    return null;

                // 1. Đọc 4 byte đầu tiên (Length Prefix)
                byte[] lengthPrefix = new byte[4];
                int bytesRead = stream.Read(lengthPrefix, 0, 4);

                // Nếu bytesRead = 0, kết nối đã đóng
                if (bytesRead == 0) return null;
                if (bytesRead < 4) return null; // Lỗi đọc không đủ 4 byte Length Prefix

                // Chuyển 4 byte thành số nguyên (độ dài gói tin)
                int length = BitConverter.ToInt32(lengthPrefix, 0);

                // 2. Đọc nội dung JSON dựa trên độ dài (length)
                byte[] buffer = new byte[length];
                int totalRead = 0;

                // Vòng lặp quan trọng: Đảm bảo đọc đủ tất cả byte
                while (totalRead < length)
                {
                    // Đọc phần còn lại (length - totalRead)
                    bytesRead = stream.Read(buffer, totalRead, length - totalRead);

                    if (bytesRead == 0)
                    {
                        // Lỗi: Kết nối đóng trước khi gói tin hoàn tất
                        break;
                    }
                    totalRead += bytesRead;
                }

                // Kiểm tra tính toàn vẹn (đã đọc đủ số byte đã công bố)
                if (totalRead != length)
                {
                    Console.WriteLine($"Read insufficient bytes. Requested {length}, read {totalRead}.");
                    return null;
                }

                // 3. Chuyển byte thành đối tượng Message
                string json = Encoding.UTF8.GetString(buffer, 0, totalRead);
                return Message.FromJson(json);
            }
            catch (IOException)
            {
                // Kết nối bị đóng từ phía Client/Server
                return null;
            }
            catch (Exception ex)
            {
                // Lỗi Deserialization hoặc lỗi khác
                Console.WriteLine($"ReceiveMessage Error: {ex.Message}");
                return null;
            }
        }
    }
}