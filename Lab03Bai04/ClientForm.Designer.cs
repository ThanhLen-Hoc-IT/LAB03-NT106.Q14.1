namespace Lab03
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpCustomer = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.grpMovie = new System.Windows.Forms.GroupBox();
            this.cboRoom = new System.Windows.Forms.ComboBox();
            this.cboMovie = new System.Windows.Forms.ComboBox();
            this.lblRoom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpSeats = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBook = new System.Windows.Forms.Button();
            this.btnC7 = new System.Windows.Forms.Button();
            this.btnC6 = new System.Windows.Forms.Button();
            this.btnC5 = new System.Windows.Forms.Button();
            this.btnC4 = new System.Windows.Forms.Button();
            this.btnC3 = new System.Windows.Forms.Button();
            this.btnC2 = new System.Windows.Forms.Button();
            this.btnC1 = new System.Windows.Forms.Button();
            this.lblC = new System.Windows.Forms.Label();
            this.bnB7 = new System.Windows.Forms.Button();
            this.btnB6 = new System.Windows.Forms.Button();
            this.btnB5 = new System.Windows.Forms.Button();
            this.btnB4 = new System.Windows.Forms.Button();
            this.btnB3 = new System.Windows.Forms.Button();
            this.btnB2 = new System.Windows.Forms.Button();
            this.btnB1 = new System.Windows.Forms.Button();
            this.lblB = new System.Windows.Forms.Label();
            this.btnA7 = new System.Windows.Forms.Button();
            this.btnA6 = new System.Windows.Forms.Button();
            this.btnA5 = new System.Windows.Forms.Button();
            this.btnA4 = new System.Windows.Forms.Button();
            this.btnA3 = new System.Windows.Forms.Button();
            this.btnA2 = new System.Windows.Forms.Button();
            this.btnA1 = new System.Windows.Forms.Button();
            this.lblA = new System.Windows.Forms.Label();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.grpCustomer.SuspendLayout();
            this.grpMovie.SuspendLayout();
            this.grpSeats.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCustomer
            // 
            this.grpCustomer.Controls.Add(this.btnConnect);
            this.grpCustomer.Controls.Add(this.txtCustomerName);
            this.grpCustomer.Controls.Add(this.lblStatus);
            this.grpCustomer.Controls.Add(this.lblName);
            this.grpCustomer.Location = new System.Drawing.Point(20, 20);
            this.grpCustomer.Name = "grpCustomer";
            this.grpCustomer.Size = new System.Drawing.Size(396, 206);
            this.grpCustomer.TabIndex = 0;
            this.grpCustomer.TabStop = false;
            this.grpCustomer.Text = "Thông tin khách hàng";
            this.grpCustomer.Enter += new System.EventHandler(this.grpCustomer_Enter);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(9, 92);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(146, 31);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Kết nối Server ";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(136, 36);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(187, 26);
            this.txtCustomerName.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(17, 150);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(177, 20);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Trạng thái: Chưa kết nối";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 36);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(81, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Họ và Tên";
            // 
            // grpMovie
            // 
            this.grpMovie.Controls.Add(this.cboRoom);
            this.grpMovie.Controls.Add(this.cboMovie);
            this.grpMovie.Controls.Add(this.lblRoom);
            this.grpMovie.Controls.Add(this.label1);
            this.grpMovie.Location = new System.Drawing.Point(552, 20);
            this.grpMovie.Name = "grpMovie";
            this.grpMovie.Size = new System.Drawing.Size(438, 206);
            this.grpMovie.TabIndex = 1;
            this.grpMovie.TabStop = false;
            this.grpMovie.Text = "Thông tin phim";
            // 
            // cboRoom
            // 
            this.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoom.FormattingEnabled = true;
            this.cboRoom.Location = new System.Drawing.Point(137, 95);
            this.cboRoom.Name = "cboRoom";
            this.cboRoom.Size = new System.Drawing.Size(222, 28);
            this.cboRoom.TabIndex = 3;
            // 
            // cboMovie
            // 
            this.cboMovie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMovie.FormattingEnabled = true;
            this.cboMovie.Location = new System.Drawing.Point(137, 36);
            this.cboMovie.Name = "cboMovie";
            this.cboMovie.Size = new System.Drawing.Size(222, 28);
            this.cboMovie.TabIndex = 2;
            this.cboMovie.SelectedIndexChanged += new System.EventHandler(this.cboMovie_SelectedIndexChanged);
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(16, 97);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(101, 20);
            this.lblRoom.TabIndex = 1;
            this.lblRoom.Text = "Phòng chiếu:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên phim: ";
            // 
            // grpSeats
            // 
            this.grpSeats.Controls.Add(this.btnClear);
            this.grpSeats.Controls.Add(this.btnBook);
            this.grpSeats.Controls.Add(this.btnC7);
            this.grpSeats.Controls.Add(this.btnC6);
            this.grpSeats.Controls.Add(this.btnC5);
            this.grpSeats.Controls.Add(this.btnC4);
            this.grpSeats.Controls.Add(this.btnC3);
            this.grpSeats.Controls.Add(this.btnC2);
            this.grpSeats.Controls.Add(this.btnC1);
            this.grpSeats.Controls.Add(this.lblC);
            this.grpSeats.Controls.Add(this.bnB7);
            this.grpSeats.Controls.Add(this.btnB6);
            this.grpSeats.Controls.Add(this.btnB5);
            this.grpSeats.Controls.Add(this.btnB4);
            this.grpSeats.Controls.Add(this.btnB3);
            this.grpSeats.Controls.Add(this.btnB2);
            this.grpSeats.Controls.Add(this.btnB1);
            this.grpSeats.Controls.Add(this.lblB);
            this.grpSeats.Controls.Add(this.btnA7);
            this.grpSeats.Controls.Add(this.btnA6);
            this.grpSeats.Controls.Add(this.btnA5);
            this.grpSeats.Controls.Add(this.btnA4);
            this.grpSeats.Controls.Add(this.btnA3);
            this.grpSeats.Controls.Add(this.btnA2);
            this.grpSeats.Controls.Add(this.btnA1);
            this.grpSeats.Controls.Add(this.lblA);
            this.grpSeats.Location = new System.Drawing.Point(20, 245);
            this.grpSeats.Name = "grpSeats";
            this.grpSeats.Size = new System.Drawing.Size(1031, 303);
            this.grpSeats.TabIndex = 2;
            this.grpSeats.TabStop = false;
            this.grpSeats.Text = "MÀN HÌNH";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(532, 242);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(181, 44);
            this.btnClear.TabIndex = 25;
            this.btnClear.Text = "Hủy chọn";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBook
            // 
            this.btnBook.Location = new System.Drawing.Point(293, 242);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(172, 44);
            this.btnBook.TabIndex = 24;
            this.btnBook.Text = "Đặt vé";
            this.btnBook.UseVisualStyleBackColor = true;
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // btnC7
            // 
            this.btnC7.Location = new System.Drawing.Point(850, 190);
            this.btnC7.Name = "btnC7";
            this.btnC7.Size = new System.Drawing.Size(79, 35);
            this.btnC7.TabIndex = 23;
            this.btnC7.Text = "C7";
            this.btnC7.UseVisualStyleBackColor = true;
            // 
            // btnC6
            // 
            this.btnC6.Location = new System.Drawing.Point(725, 190);
            this.btnC6.Name = "btnC6";
            this.btnC6.Size = new System.Drawing.Size(79, 35);
            this.btnC6.TabIndex = 22;
            this.btnC6.Text = "C6";
            this.btnC6.UseVisualStyleBackColor = true;
            // 
            // btnC5
            // 
            this.btnC5.Location = new System.Drawing.Point(606, 190);
            this.btnC5.Name = "btnC5";
            this.btnC5.Size = new System.Drawing.Size(81, 35);
            this.btnC5.TabIndex = 21;
            this.btnC5.Text = "C5";
            this.btnC5.UseVisualStyleBackColor = true;
            // 
            // btnC4
            // 
            this.btnC4.Location = new System.Drawing.Point(483, 190);
            this.btnC4.Name = "btnC4";
            this.btnC4.Size = new System.Drawing.Size(78, 35);
            this.btnC4.TabIndex = 20;
            this.btnC4.Text = "C4";
            this.btnC4.UseVisualStyleBackColor = true;
            // 
            // btnC3
            // 
            this.btnC3.Location = new System.Drawing.Point(373, 190);
            this.btnC3.Name = "btnC3";
            this.btnC3.Size = new System.Drawing.Size(75, 35);
            this.btnC3.TabIndex = 19;
            this.btnC3.Text = "C3";
            this.btnC3.UseVisualStyleBackColor = true;
            // 
            // btnC2
            // 
            this.btnC2.Location = new System.Drawing.Point(258, 190);
            this.btnC2.Name = "btnC2";
            this.btnC2.Size = new System.Drawing.Size(75, 35);
            this.btnC2.TabIndex = 18;
            this.btnC2.Text = "C2";
            this.btnC2.UseVisualStyleBackColor = true;
            // 
            // btnC1
            // 
            this.btnC1.Location = new System.Drawing.Point(148, 190);
            this.btnC1.Name = "btnC1";
            this.btnC1.Size = new System.Drawing.Size(81, 35);
            this.btnC1.TabIndex = 17;
            this.btnC1.Text = "C1";
            this.btnC1.UseVisualStyleBackColor = true;
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.Location = new System.Drawing.Point(97, 193);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(20, 20);
            this.lblC.TabIndex = 16;
            this.lblC.Text = "C";
            // 
            // bnB7
            // 
            this.bnB7.Location = new System.Drawing.Point(850, 119);
            this.bnB7.Name = "bnB7";
            this.bnB7.Size = new System.Drawing.Size(79, 35);
            this.bnB7.TabIndex = 15;
            this.bnB7.Text = "B7";
            this.bnB7.UseVisualStyleBackColor = true;
            // 
            // btnB6
            // 
            this.btnB6.Location = new System.Drawing.Point(725, 119);
            this.btnB6.Name = "btnB6";
            this.btnB6.Size = new System.Drawing.Size(79, 35);
            this.btnB6.TabIndex = 14;
            this.btnB6.Text = "B6";
            this.btnB6.UseVisualStyleBackColor = true;
            // 
            // btnB5
            // 
            this.btnB5.Location = new System.Drawing.Point(606, 119);
            this.btnB5.Name = "btnB5";
            this.btnB5.Size = new System.Drawing.Size(81, 35);
            this.btnB5.TabIndex = 13;
            this.btnB5.Text = "B5";
            this.btnB5.UseVisualStyleBackColor = true;
            // 
            // btnB4
            // 
            this.btnB4.Location = new System.Drawing.Point(483, 119);
            this.btnB4.Name = "btnB4";
            this.btnB4.Size = new System.Drawing.Size(78, 35);
            this.btnB4.TabIndex = 12;
            this.btnB4.Text = "B4";
            this.btnB4.UseVisualStyleBackColor = true;
            // 
            // btnB3
            // 
            this.btnB3.Location = new System.Drawing.Point(368, 121);
            this.btnB3.Name = "btnB3";
            this.btnB3.Size = new System.Drawing.Size(80, 33);
            this.btnB3.TabIndex = 11;
            this.btnB3.Text = "B3";
            this.btnB3.UseVisualStyleBackColor = true;
            // 
            // btnB2
            // 
            this.btnB2.Location = new System.Drawing.Point(256, 122);
            this.btnB2.Name = "btnB2";
            this.btnB2.Size = new System.Drawing.Size(77, 32);
            this.btnB2.TabIndex = 10;
            this.btnB2.Text = "B2";
            this.btnB2.UseVisualStyleBackColor = true;
            // 
            // btnB1
            // 
            this.btnB1.Location = new System.Drawing.Point(148, 121);
            this.btnB1.Name = "btnB1";
            this.btnB1.Size = new System.Drawing.Size(81, 33);
            this.btnB1.TabIndex = 9;
            this.btnB1.Text = "B1";
            this.btnB1.UseVisualStyleBackColor = true;
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(97, 122);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(20, 20);
            this.lblB.TabIndex = 8;
            this.lblB.Text = "B";
            // 
            // btnA7
            // 
            this.btnA7.Location = new System.Drawing.Point(850, 53);
            this.btnA7.Name = "btnA7";
            this.btnA7.Size = new System.Drawing.Size(79, 34);
            this.btnA7.TabIndex = 7;
            this.btnA7.Text = "A7";
            this.btnA7.UseVisualStyleBackColor = true;
            // 
            // btnA6
            // 
            this.btnA6.Location = new System.Drawing.Point(725, 53);
            this.btnA6.Name = "btnA6";
            this.btnA6.Size = new System.Drawing.Size(79, 34);
            this.btnA6.TabIndex = 6;
            this.btnA6.Text = "A6";
            this.btnA6.UseVisualStyleBackColor = true;
            // 
            // btnA5
            // 
            this.btnA5.Location = new System.Drawing.Point(606, 53);
            this.btnA5.Name = "btnA5";
            this.btnA5.Size = new System.Drawing.Size(81, 34);
            this.btnA5.TabIndex = 5;
            this.btnA5.Text = "A5";
            this.btnA5.UseVisualStyleBackColor = true;
            // 
            // btnA4
            // 
            this.btnA4.Location = new System.Drawing.Point(483, 53);
            this.btnA4.Name = "btnA4";
            this.btnA4.Size = new System.Drawing.Size(78, 34);
            this.btnA4.TabIndex = 4;
            this.btnA4.Text = "A4";
            this.btnA4.UseVisualStyleBackColor = true;
            // 
            // btnA3
            // 
            this.btnA3.Location = new System.Drawing.Point(368, 53);
            this.btnA3.Name = "btnA3";
            this.btnA3.Size = new System.Drawing.Size(80, 34);
            this.btnA3.TabIndex = 3;
            this.btnA3.Text = "A3";
            this.btnA3.UseVisualStyleBackColor = true;
            // 
            // btnA2
            // 
            this.btnA2.Location = new System.Drawing.Point(256, 53);
            this.btnA2.Name = "btnA2";
            this.btnA2.Size = new System.Drawing.Size(77, 34);
            this.btnA2.TabIndex = 2;
            this.btnA2.Text = "A2";
            this.btnA2.UseVisualStyleBackColor = true;
            // 
            // btnA1
            // 
            this.btnA1.Location = new System.Drawing.Point(148, 53);
            this.btnA1.Name = "btnA1";
            this.btnA1.Size = new System.Drawing.Size(81, 34);
            this.btnA1.TabIndex = 1;
            this.btnA1.Text = "A1";
            this.btnA1.UseVisualStyleBackColor = true;
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(97, 58);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(20, 20);
            this.lblA.TabIndex = 0;
            this.lblA.Text = "A";
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.txtPort);
            this.grpLog.Controls.Add(this.txtLog);
            this.grpLog.Controls.Add(this.txtServerIP);
            this.grpLog.Controls.Add(this.lblPort);
            this.grpLog.Controls.Add(this.lblServerIP);
            this.grpLog.Location = new System.Drawing.Point(20, 579);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(1031, 209);
            this.grpLog.TabIndex = 3;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Nhật ký hệ thống";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(680, 45);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(249, 26);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "5000";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(164, 112);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(266, 26);
            this.txtLog.TabIndex = 3;
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(164, 45);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(266, 26);
            this.txtServerIP.TabIndex = 2;
            this.txtServerIP.Text = "127.0.0.1";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(579, 45);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(55, 20);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Cổng: ";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.Location = new System.Drawing.Point(33, 45);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(74, 20);
            this.lblServerIP.TabIndex = 0;
            this.lblServerIP.Text = "ServerIP:";
            // 
            // Lab03Bai04
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 831);
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.grpSeats);
            this.Controls.Add(this.grpMovie);
            this.Controls.Add(this.grpCustomer);
            this.Name = "Lab03Bai04";
            this.Text = "Lab03Bai04";
            this.Load += new System.EventHandler(this.Lab03Bai04_Load);
            this.grpCustomer.ResumeLayout(false);
            this.grpCustomer.PerformLayout();
            this.grpMovie.ResumeLayout(false);
            this.grpMovie.PerformLayout();
            this.grpSeats.ResumeLayout(false);
            this.grpSeats.PerformLayout();
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCustomer;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.GroupBox grpMovie;
        private System.Windows.Forms.ComboBox cboMovie;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboRoom;
        private System.Windows.Forms.GroupBox grpSeats;
        private System.Windows.Forms.Button btnA7;
        private System.Windows.Forms.Button btnA6;
        private System.Windows.Forms.Button btnA5;
        private System.Windows.Forms.Button btnA4;
        private System.Windows.Forms.Button btnA3;
        private System.Windows.Forms.Button btnA2;
        private System.Windows.Forms.Button btnA1;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Button bnB7;
        private System.Windows.Forms.Button btnB6;
        private System.Windows.Forms.Button btnB5;
        private System.Windows.Forms.Button btnB4;
        private System.Windows.Forms.Button btnB3;
        private System.Windows.Forms.Button btnB2;
        private System.Windows.Forms.Button btnB1;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Button btnC7;
        private System.Windows.Forms.Button btnC6;
        private System.Windows.Forms.Button btnC5;
        private System.Windows.Forms.Button btnC4;
        private System.Windows.Forms.Button btnC3;
        private System.Windows.Forms.Button btnC2;
        private System.Windows.Forms.Button btnC1;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblServerIP;
    }
}

