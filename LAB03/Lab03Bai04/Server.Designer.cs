namespace TicketBookingApp
{
    partial class Server
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
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.rtbServerLog = new System.Windows.Forms.RichTextBox();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.dgvTicketStatus = new System.Windows.Forms.DataGridView();
            this.grpStatistics = new System.Windows.Forms.GroupBox();
            this.pbStatExport = new System.Windows.Forms.ProgressBar();
            this.btnCalculateStats = new System.Windows.Forms.Button();
            this.rtbStatistics = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTicketStatus)).BeginInit();
            this.grpStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(253, 40);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 26);
            this.txtPort.TabIndex = 0;
            this.txtPort.Text = "8888";
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(381, 40);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(140, 26);
            this.btnStartServer.TabIndex = 1;
            this.btnStartServer.Text = "Start Listen";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // btnStopServer
            // 
            this.btnStopServer.Location = new System.Drawing.Point(560, 40);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(142, 26);
            this.btnStopServer.TabIndex = 2;
            this.btnStopServer.Text = "Stop";
            this.btnStopServer.UseVisualStyleBackColor = true;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // rtbServerLog
            // 
            this.rtbServerLog.Location = new System.Drawing.Point(620, 123);
            this.rtbServerLog.Name = "rtbServerLog";
            this.rtbServerLog.ReadOnly = true;
            this.rtbServerLog.Size = new System.Drawing.Size(246, 150);
            this.rtbServerLog.TabIndex = 3;
            this.rtbServerLog.Text = "";
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.Location = new System.Drawing.Point(754, 43);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(100, 20);
            this.lblServerStatus.TabIndex = 4;
            this.lblServerStatus.Text = "Đang dừng...";
            // 
            // dgvTicketStatus
            // 
            this.dgvTicketStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTicketStatus.Location = new System.Drawing.Point(129, 123);
            this.dgvTicketStatus.Name = "dgvTicketStatus";
            this.dgvTicketStatus.ReadOnly = true;
            this.dgvTicketStatus.RowHeadersWidth = 62;
            this.dgvTicketStatus.RowTemplate.Height = 28;
            this.dgvTicketStatus.Size = new System.Drawing.Size(240, 150);
            this.dgvTicketStatus.TabIndex = 5;
            // 
            // grpStatistics
            // 
            this.grpStatistics.Controls.Add(this.pbStatExport);
            this.grpStatistics.Controls.Add(this.btnCalculateStats);
            this.grpStatistics.Controls.Add(this.rtbStatistics);
            this.grpStatistics.Location = new System.Drawing.Point(129, 303);
            this.grpStatistics.Name = "grpStatistics";
            this.grpStatistics.Size = new System.Drawing.Size(830, 205);
            this.grpStatistics.TabIndex = 6;
            this.grpStatistics.TabStop = false;
            this.grpStatistics.Text = "Thống kê doanh thu";
            // 
            // pbStatExport
            // 
            this.pbStatExport.Location = new System.Drawing.Point(431, 34);
            this.pbStatExport.Name = "pbStatExport";
            this.pbStatExport.Size = new System.Drawing.Size(183, 42);
            this.pbStatExport.TabIndex = 9;
            // 
            // btnCalculateStats
            // 
            this.btnCalculateStats.Location = new System.Drawing.Point(26, 34);
            this.btnCalculateStats.Name = "btnCalculateStats";
            this.btnCalculateStats.Size = new System.Drawing.Size(198, 42);
            this.btnCalculateStats.TabIndex = 7;
            this.btnCalculateStats.Text = "Tính toán và Xuất Stats";
            this.btnCalculateStats.UseVisualStyleBackColor = true;
            this.btnCalculateStats.Click += new System.EventHandler(this.btnCalculateStats_Click);
            // 
            // rtbStatistics
            // 
            this.rtbStatistics.Location = new System.Drawing.Point(26, 82);
            this.rtbStatistics.Name = "rtbStatistics";
            this.rtbStatistics.ReadOnly = true;
            this.rtbStatistics.Size = new System.Drawing.Size(281, 96);
            this.rtbStatistics.TabIndex = 8;
            this.rtbStatistics.Text = "";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 559);
            this.Controls.Add(this.grpStatistics);
            this.Controls.Add(this.dgvTicketStatus);
            this.Controls.Add(this.lblServerStatus);
            this.Controls.Add(this.rtbServerLog);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.txtPort);
            this.Name = "Server";
            this.Text = "Server";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTicketStatus)).EndInit();
            this.grpStatistics.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.RichTextBox rtbServerLog;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.DataGridView dgvTicketStatus;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.Button btnCalculateStats;
        private System.Windows.Forms.RichTextBox rtbStatistics;
        private System.Windows.Forms.ProgressBar pbStatExport;
    }
}