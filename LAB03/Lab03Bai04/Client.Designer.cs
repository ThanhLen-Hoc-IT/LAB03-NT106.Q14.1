namespace TicketBookingApp
{
    partial class Client
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpSeatMap;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RichTextBox rtbClientLog;

        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;

        /// <summary>
        ///  Clean up resources
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpSeatMap = new System.Windows.Forms.GroupBox();
            this.btnBook = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.rtbClientLog = new System.Windows.Forms.RichTextBox();

            this.lblServerIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // ===========================
            // grpSeatMap
            // ===========================
            this.grpSeatMap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSeatMap.Location = new System.Drawing.Point(20, 80);
            this.grpSeatMap.Name = "grpSeatMap";
            this.grpSeatMap.Size = new System.Drawing.Size(600, 400);
            this.grpSeatMap.TabIndex = 0;
            this.grpSeatMap.TabStop = false;
            this.grpSeatMap.Text = "Sơ đồ ghế";

            // ===========================
            // btnBook
            // ===========================
            this.btnBook.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBook.Location = new System.Drawing.Point(650, 100);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(150, 40);
            this.btnBook.TabIndex = 1;
            this.btnBook.Text = "Đặt ghế";
            this.btnBook.UseVisualStyleBackColor = true;
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);

            // ===========================
            // btnCancel
            // ===========================
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(650, 160);
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy ghế";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // ===========================
            // btnRefresh
            // ===========================
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.Location = new System.Drawing.Point(650, 220);
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // ===========================
            // rtbClientLog
            // ===========================
            this.rtbClientLog.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbClientLog.Location = new System.Drawing.Point(20, 500);
            this.rtbClientLog.Name = "rtbClientLog";
            this.rtbClientLog.Size = new System.Drawing.Size(780, 200);
            this.rtbClientLog.TabIndex = 4;
            this.rtbClientLog.Text = "";

            // ===========================
            // LABEL + TEXTBOX SERVER IP
            // ===========================
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblServerIP.Location = new System.Drawing.Point(20, 20);
            this.lblServerIP.Text = "Server IP:";

            this.txtServerIP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtServerIP.Location = new System.Drawing.Point(100, 17);
            this.txtServerIP.Size = new System.Drawing.Size(150, 25);
            this.txtServerIP.Text = "127.0.0.1";

            // ===========================
            // PORT
            // ===========================
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPort.Location = new System.Drawing.Point(300, 20);
            this.lblPort.Text = "Port:";

            this.txtPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPort.Location = new System.Drawing.Point(350, 17);
            this.txtPort.Size = new System.Drawing.Size(80, 25);
            this.txtPort.Text = "8888";

            // ===========================
            // btnConnect
            // ===========================
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnConnect.Location = new System.Drawing.Point(460, 14);
            this.btnConnect.Size = new System.Drawing.Size(120, 30);
            this.btnConnect.Text = "Kết nối";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            // ===========================
            // FORM SETTINGS
            // ===========================
            this.ClientSize = new System.Drawing.Size(850, 750);
            this.Controls.Add(this.grpSeatMap);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.rtbClientLog);

            this.Controls.Add(this.lblServerIP);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.btnConnect);

            this.Name = "Client";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quầy vé (Client)";
            this.Load += new System.EventHandler(this.Client_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
