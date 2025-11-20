namespace TicketBookingApp
{
    partial class Dashboard
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.grpClient = new System.Windows.Forms.GroupBox();
            this.btnOpenServer = new System.Windows.Forms.Button();
            this.btnOpenClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(223, 32);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(284, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ PHÒNG VÉ";
            // 
            // grpServer
            // 
            this.grpServer.Location = new System.Drawing.Point(27, 102);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(200, 100);
            this.grpServer.TabIndex = 1;
            this.grpServer.TabStop = false;
            this.grpServer.Text = "Server Status";
            // 
            // grpClient
            // 
            this.grpClient.Location = new System.Drawing.Point(523, 102);
            this.grpClient.Name = "grpClient";
            this.grpClient.Size = new System.Drawing.Size(200, 100);
            this.grpClient.TabIndex = 2;
            this.grpClient.TabStop = false;
            this.grpClient.Text = "Client Status";
            // 
            // btnOpenServer
            // 
            this.btnOpenServer.Location = new System.Drawing.Point(27, 249);
            this.btnOpenServer.Name = "btnOpenServer";
            this.btnOpenServer.Size = new System.Drawing.Size(200, 54);
            this.btnOpenServer.TabIndex = 3;
            this.btnOpenServer.Text = "Server ";
            this.btnOpenServer.UseVisualStyleBackColor = true;
            this.btnOpenServer.Click += new System.EventHandler(this.btnOpenServer_Click);
            // 
            // btnOpenClient
            // 
            this.btnOpenClient.Location = new System.Drawing.Point(523, 249);
            this.btnOpenClient.Name = "btnOpenClient";
            this.btnOpenClient.Size = new System.Drawing.Size(200, 54);
            this.btnOpenClient.TabIndex = 4;
            this.btnOpenClient.Text = "Client";
            this.btnOpenClient.UseVisualStyleBackColor = true;
            this.btnOpenClient.Click += new System.EventHandler(this.btnOpenClient_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOpenClient);
            this.Controls.Add(this.btnOpenServer);
            this.Controls.Add(this.grpClient);
            this.Controls.Add(this.grpServer);
            this.Controls.Add(this.lblTitle);
            this.Name = "Dashboard";
            this.Text = "DashBoard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpServer;
        private System.Windows.Forms.GroupBox grpClient;
        private System.Windows.Forms.Button btnOpenServer;
        private System.Windows.Forms.Button btnOpenClient;
    }
}