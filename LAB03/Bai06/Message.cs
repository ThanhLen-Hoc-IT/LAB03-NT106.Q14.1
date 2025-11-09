using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bai06
{
    public class Message
    {
        // Loại tin nhắn: TEXT, STATUS_JOIN, STATUS_LEAVE, STATUS_USERS, STATUS_SHUTDOWN
        [JsonPropertyName("type")]
        public string Type { get; set; } = "TEXT";

        [JsonPropertyName("sender")]
        public string Sender { get; set; } = "SYSTEM";

        // "BROADCAST" hoặc tên người nhận cụ thể
        [JsonPropertyName("receiver")]
        public string Receiver { get; set; } = "BROADCAST";

        [JsonPropertyName("data")]
        public string Data { get; set; } = string.Empty; // Nội dung tin nhắn (chuỗi text hoặc JSON UserList)

        public Message() { }


        // BỔ SUNG: Thuộc tính lưu tên file gốc khi gửi file
        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = string.Empty;


        // Constructor tiện lợi (đã cập nhật để bao gồm FileName)
        public Message(string sender, string receiver, string data, string type, string fileName = "")
        {
            this.Sender = sender;
            this.Receiver = receiver;
            this.Type = type;
            this.Data = data;
            this.FileName = fileName; // Bổ sung
        }
        public Message(string sender, string receiver, string data, string type)
        {
            this.Sender = sender;
            this.Receiver = receiver;
            this.Type = type;
            this.Data = data;
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            return JsonSerializer.Serialize(this, options);
        }

        public static Message FromJson(string json)
        {
            return JsonSerializer.Deserialize<Message>(json);
        }
    }
}