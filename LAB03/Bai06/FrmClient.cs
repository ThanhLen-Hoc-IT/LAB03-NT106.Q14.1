using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;

namespace Bai06
{
    public partial class FrmClient : Form
    {
        private ChatClient client;

        // Class để lưu trữ dữ liệu file nhận được tạm thời
        public class ReceivedFile
        {
            public string Sender { get; set; }
            public string FileName { get; set; }
            public string Base64Data { get; set; }
            public string ChatTarget { get; set; } // BROADCAST or tên chính xác
        }

        // Dictionary lưu trữ file đang chờ tải xuống
        private Dictionary<Guid, ReceivedFile> pendingFiles = new Dictionary<Guid, ReceivedFile>();

        private string pendingFilePath = null; // Đường dẫn file tạm thời
        private string pendingFileName = null; // Tên file tạm thời
        private string pendingMsgType = null;  // Loại file tạm thời

        public FrmClient()
        {
            InitializeComponent();

            client = new ChatClient();
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnConnectionLost += Client_OnConnectionLost;
            this.rtbChatLog.MouseUp += new MouseEventHandler(this.RtbChatLog_MouseUp); // Đăng ký sự kiện nhấp chuột

            SetupParticipantsListView();
            SetControlState(false);
            this.Text = "Chat Client";
            this.Load += FrmClient_Load; // Giữ lại sự kiện Load nếu bạn cần
        }

        // --- CÁC HÀM TIỆN ÍCH CHUNG VÀ UI ---

        private void SetupParticipantsListView()
        {
            lvPaticipants.View = View.Details;
            lvPaticipants.FullRowSelect = true;

            ColumnHeader colUsername = new ColumnHeader();
            colUsername.Text = "Participants";
            colUsername.Width = lvPaticipants.Width - 5;

            lvPaticipants.Columns.Add(colUsername);
        }

        private void SetControlState(bool connected)
        {
            txtClientName.Enabled = !connected;
            btnConnect.Enabled = !connected;

            txtMessage.Enabled = connected;
            btnSend.Enabled = connected;

            cboUsers.Enabled = connected;

            this.Text = connected ? $"Chat Client - {client.Username}" : "Disconnected";
        }

        private string GetSelectedChatTarget()
        {
            return cboUsers.SelectedItem?.ToString();
        }

        private RichTextBox GetChatHistoryBox(string username)
        {
            if (!client.ChatHistory.ContainsKey(username))
            {
                RichTextBox newRtb = new RichTextBox();
                newRtb.Dock = DockStyle.Fill;
                newRtb.ReadOnly = true;
                client.ChatHistory.Add(username, newRtb);

                if (username == "BROADCAST")
                {
                    AppendText(newRtb, "[System]: This is the public chat channel.", Color.Gray, false);
                }
            }
            return client.ChatHistory[username];
        }

        private void AppendText(RichTextBox rtb, string text, Color color, bool isSystem = false)
        {
            if (rtb.InvokeRequired)
            {
                this.Invoke(new Action<RichTextBox, string, Color, bool>(AppendText), rtb, text, color, isSystem);
                return;
            }

            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionLength = 0;

            rtb.SelectionColor = color;
            rtb.AppendText(text + Environment.NewLine);
            rtb.SelectionColor = rtb.ForeColor;

            rtb.SelectionStart = rtb.TextLength;
            rtb.ScrollToCaret();
        }

        private void SaveMessageToHistory(string chatTarget, string sender, string data, Color color, bool isSelfMessage = false, Guid? fileId = null)
        {
            RichTextBox rtb = GetChatHistoryBox(chatTarget);
            string displaySender = isSelfMessage ? "Me" : sender;

            // Thêm ID file vào log để kích hoạt tải xuống khi nhấp chuột
            string fileTag = (fileId.HasValue) ? $" [FileID:{fileId.Value}]" : "";

            string log = $"[{DateTime.Now:HH:mm}] {displaySender}: {data}{fileTag}";

            AppendText(rtb, log, color, false);
        }

        private void UpdateChatDisplay(string targetUsername)
        {
            if (rtbChatLog.InvokeRequired)
            {
                this.Invoke(new Action<string>(UpdateChatDisplay), targetUsername);
                return;
            }

            RichTextBox rtbHistory = GetChatHistoryBox(targetUsername);
            rtbChatLog.Rtf = rtbHistory.Rtf;

            rtbChatLog.SelectionStart = rtbChatLog.Text.Length;
            rtbChatLog.ScrollToCaret();
        }

        // --- LOGIC MÃ HÓA/GIẢI MÃ FILE VÀ DỌN DẸP ---

        private string EncodeFileToBase64(string filePath)
        {
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return Convert.ToBase64String(fileBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Encoding error: {ex.Message}", "Error");
                return null;
            }
        }

        private void ClearPendingFile()
        {
            pendingFilePath = null;
            pendingFileName = null;
            pendingMsgType = null;
        }

        private void RemovePendingFile(Guid fileId)
        {
            if (pendingFiles.ContainsKey(fileId))
            {
                pendingFiles.Remove(fileId);
            }
        }

        // ******************************************************************
        // HÀM TẢI XUỐNG VÀ LƯU FILE (ĐƯỢC GỌI KHI NHẤP CHUỘT)
        // ******************************************************************
        private void SaveReceivedFile(Message fileMsg, Guid fileId)
        {
            // Bảo vệ đa luồng
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Message, Guid>(SaveReceivedFile), fileMsg, fileId);
                return;
            }

            // Đích đến chat (để lưu lịch sử)
            string chatTarget = fileMsg.Receiver == "BROADCAST" ? "BROADCAST" : fileMsg.Sender;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = fileMsg.FileName;
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string extension = System.IO.Path.GetExtension(fileMsg.FileName);
                sfd.Filter = $"Original File (*{extension})|*{extension}|Supported Files (*.txt;*.png;*.jpg)|*.txt;*.png;*.jpg|All Files (*.*)|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        byte[] fileBytes = Convert.FromBase64String(fileMsg.Data);
                        System.IO.File.WriteAllBytes(sfd.FileName, fileBytes);

                        // THÀNH CÔNG: Xóa file khỏi bộ nhớ tạm
                        RemovePendingFile(fileId);

                        SaveMessageToHistory(chatTarget, "[System]",
                            $"File downloaded successfully: {fileMsg.FileName}", Color.Green, false);
                    }
                    catch (Exception ex)
                    {
                        // LỖI: Giữ file trong bộ nhớ tạm để thử lại
                        SaveMessageToHistory(chatTarget, "[System]",
                            $"Error saving file {fileMsg.FileName}: {ex.Message} (Click to download)", Color.Red, false);
                    }
                }
                else
                {
                    // HỦY BỎ: Giữ file trong bộ nhớ tạm
                    SaveMessageToHistory(chatTarget, "[Hệ thống]",
                        $"Downloading was canceled: {fileMsg.FileName} (Click to download)", Color.Orange, false);
                }

                // Cập nhật giao diện sau khi SaveFileDialog đóng
                UpdateChatDisplay(chatTarget);
            }
        }

        // ******************************************************************
        // HÀM XỬ LÝ SỰ KIỆN NHẤP CHUỘT (TẢI XUỐNG THEO YÊU CẦU)
        // ******************************************************************
        private void RtbChatLog_MouseUp(object sender, MouseEventArgs e)
        {
            // Nếu không có file chờ hoặc không phải nhấp chuột trái, thoát
            if (pendingFiles.Count == 0 || e.Button != MouseButtons.Left) return;

            if (rtbChatLog.TextLength == 0) return;

            int index = rtbChatLog.GetCharIndexFromPosition(e.Location);
            int lineIndex = rtbChatLog.GetLineFromCharIndex(index);

            // KIỂM TRA BẢO VỆ CHỐNG LỖI IndexOutOfRangeException
            if (lineIndex < 0 || lineIndex >= rtbChatLog.Lines.Length)
            {
                return;
            }

            string lineText = rtbChatLog.Lines[lineIndex];

            Guid fileIdToDownload = Guid.Empty;
            ReceivedFile fileToDownload = null;

            // Duyệt qua các file đang chờ để tìm GUID tương ứng trong dòng văn bản
            foreach (var pair in pendingFiles)
            {
                string fileIdTag = $"[FileID:{pair.Key}]";

                if (lineText.Contains(fileIdTag))
                {
                    fileIdToDownload = pair.Key;
                    fileToDownload = pair.Value;
                    break;
                }
            }

            if (fileToDownload != null)
            {
                // Tạo lại đối tượng Message chứa dữ liệu đã lưu tạm
                Message downloadMsg = new Message(
                    fileToDownload.Sender,
                    fileToDownload.ChatTarget,
                    fileToDownload.Base64Data,
                    "FILE_DOWNLOAD",
                    fileToDownload.FileName
                );

                // Gọi hàm tải xuống (truyền cả ID để biết file nào cần xóa khỏi bộ nhớ tạm)
                SaveReceivedFile(downloadMsg, fileIdToDownload);
            }
        }


        // --- XỬ LÝ SỰ KIỆN TỪ CLIENT (NHẬN TIN) ---

        private void Client_OnMessageReceived(Message msg)
        {
            string chatTarget = null;
            Color color = Color.Black;

            // BẢO VỆ TRẠNG THÁI FORM
            if (this.IsDisposed || !this.IsHandleCreated) return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Message>(Client_OnMessageReceived), msg);
                return;
            }

            switch (msg.Type)
            {
                case "TEXT":
                    if (msg.Sender == client.Username) return;

                    chatTarget = msg.Receiver == "BROADCAST" ? "BROADCAST" : msg.Sender;
                    if (msg.Receiver == "BROADCAST") color = Color.DarkSlateGray;

                    SaveMessageToHistory(chatTarget, msg.Sender, msg.Data, color, false);
                    break;

                case "FILE_TXT":
                case "FILE_PNG":
                case "FILE_JPG":
                    if (msg.Sender == client.Username) return;

                    chatTarget = msg.Receiver == "BROADCAST" ? "BROADCAST" : msg.Sender;
                    color = Color.DarkCyan;

                    // 1. LƯU TRỮ DỮ LIỆU FILE VÀO BỘ NHỚ TẠM
                    Guid fileId = Guid.NewGuid();
                    pendingFiles.Add(fileId, new ReceivedFile
                    {
                        Sender = msg.Sender,
                        FileName = msg.FileName,
                        Base64Data = msg.Data,
                        ChatTarget = chatTarget
                    });

                    // 2. LƯU THÔNG BÁO VÀO LỊCH SỬ (Dùng fileId để tạo liên kết nhấp chuột)
                    SaveMessageToHistory(chatTarget, msg.Sender,
                        $"[Received File: {msg.FileName}. Click to download]", color, false, fileId);

                    break;

                case "STATUS_JOIN":
                case "STATUS_LEAVE":
                    chatTarget = "BROADCAST";
                    color = Color.Blue;
                    SaveMessageToHistory(chatTarget, "[System]", msg.Data, color, false);
                    break;

                case "STATUS_USERS":
                    UpdateUserList(msg.Data);
                    return;

                case "STATUS_SHUTDOWN":
                    Client_OnConnectionLost($"Server closed: {msg.Data}");
                    return;

                default:
                    return;
            }

            if (chatTarget != null && chatTarget == GetSelectedChatTarget())
            {
                UpdateChatDisplay(chatTarget);
            }
        }

        private void Client_OnConnectionLost(string reason)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(Client_OnConnectionLost), reason);
                return;
            }

            SaveMessageToHistory("BROADCAST", "[Connection Error]", reason, Color.Red);
            UpdateChatDisplay("BROADCAST");

            cboUsers.Items.Clear();
            lvPaticipants.Items.Clear();
            client.ChatHistory.Clear();
            SetControlState(false);
        }

        // --- XỬ LÝ UI EVENTS KHÁC ---
        private void btnConnect_Click(object sender, EventArgs e)
        {
            string username = txtClientName.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter username.", "Error");
                return;
            }

            if (client.Connect(username))
            {
                SetControlState(true);

                cboUsers.Items.Clear();
                lvPaticipants.Items.Clear();
                GetChatHistoryBox("BROADCAST");

                cboUsers.Items.Add("BROADCAST");
                cboUsers.SelectedIndex = 0;

                lvPaticipants.Items.Add(new ListViewItem("BROADCAST"));

                UpdateChatDisplay("BROADCAST");
            }
            else
            {
                MessageBox.Show("Could not connect to Server.", "Connection Error");
                return;
            }
        }

        // Nút Đính kèm File
        private void btnExtra_Click(object sender, EventArgs e)
        {
            ClearPendingFile();

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Supported Files (*.txt;*.png;*.jpg)|*.txt;*.png;*.jpg";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fileExtension = System.IO.Path.GetExtension(ofd.FileName).ToUpper();

                    switch (fileExtension)
                    {
                        case ".TXT": pendingMsgType = "FILE_TXT"; break;
                        case ".PNG": pendingMsgType = "FILE_PNG"; break;
                        case ".JPG": pendingMsgType = "FILE_JPG"; break;
                        default:
                            MessageBox.Show("Unsupported file type.", "Error");
                            return;
                    }

                    pendingFilePath = ofd.FileName;
                    pendingFileName = System.IO.Path.GetFileName(ofd.FileName);

                    txtMessage.Text = pendingFileName;
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            string receiver = GetSelectedChatTarget();

            if (receiver == null || (string.IsNullOrEmpty(message) && pendingFilePath == null)) return;

            // --- GỬI FILE ---
            if (pendingFilePath != null && pendingFileName.Equals(message, StringComparison.OrdinalIgnoreCase))
            {
                string base64Data = EncodeFileToBase64(pendingFilePath);
                if (base64Data == null)
                {
                    ClearPendingFile();
                    txtMessage.Clear();
                    return;
                }

                Message fileMsg = new Message(
                    client.Username,
                    receiver,
                    base64Data,
                    pendingMsgType,
                    pendingFileName
                );
                client.SendMessage(fileMsg);

                SaveMessageToHistory(receiver, client.Username, $"[File Sent: {pendingFileName}]", Color.Purple, true);
                ClearPendingFile();
            }
            // --- GỬI TEXT ---
            else if (!string.IsNullOrEmpty(message))
            {
                ClearPendingFile();

                Message msg = new Message(client.Username, receiver, message, "TEXT");
                client.SendMessage(msg);

                Color selfColor = (receiver == "BROADCAST") ? Color.DarkSlateGray : Color.Black;
                SaveMessageToHistory(receiver, client.Username, message, selfColor, true);
            }

            UpdateChatDisplay(receiver);
            txtMessage.Clear();
        }

        private void cboUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUser = GetSelectedChatTarget();
            if (selectedUser != null)
            {
                UpdateChatDisplay(selectedUser);
            }
        }

        private void FrmClient_FormClosing(object sender, EventArgs e) // Sửa tên hàm
        {
            client.Disconnect();
        }

        // Cần giữ lại phương thức này để đăng ký sự kiện Load (nếu cần)
        private void FrmClient_Load(object sender, EventArgs e)
        {
            // Logic khi Form Load xong (nếu có)
        }


        // --- LOGIC CẬP NHẬT DANH SÁCH NGƯỜI DÙNG ---

        private void UpdateUserList(string userListJson)
        {
            try
            {
                var usernames = JsonSerializer.Deserialize<List<string>>(userListJson);

                if (cboUsers.InvokeRequired)
                {
                    this.Invoke(new Action<string>(UpdateUserList), userListJson);
                    return;
                }

                string currentSelection = GetSelectedChatTarget();

                cboUsers.Items.Clear();
                lvPaticipants.Items.Clear();

                if (usernames.Contains("BROADCAST"))
                {
                    cboUsers.Items.Add("BROADCAST");
                    lvPaticipants.Items.Add(new ListViewItem("BROADCAST"));
                    GetChatHistoryBox("BROADCAST");
                }

                foreach (var user in usernames)
                {
                    if (user != client.Username && user != "BROADCAST")
                    {
                        cboUsers.Items.Add(user);
                        lvPaticipants.Items.Add(new ListViewItem(user));
                        GetChatHistoryBox(user);
                    }
                }

                if (cboUsers.Items.Contains(currentSelection))
                {
                    cboUsers.SelectedItem = currentSelection;
                }
                else if (cboUsers.Items.Count > 0)
                {
                    cboUsers.SelectedIndex = 0;
                }

                UpdateChatDisplay(GetSelectedChatTarget());
            }
            catch (Exception)
            {
                SaveMessageToHistory("BROADCAST", "[System error]", "Could not update user list.", Color.Red);
            }
        }
    }
}