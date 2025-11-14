using System.ComponentModel;
using System.Windows.Forms;

namespace Lab03Bai03
{
    partial class Lab03Bai03
    {
        private IContainer components = null;
        private Button btnOpenServer;
        private Button btnNewClient;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnOpenServer = new System.Windows.Forms.Button();
            this.btnNewClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpenServer
            // 
            this.btnOpenServer.Location = new System.Drawing.Point(24, 20);
            this.btnOpenServer.Name = "btnOpenServer";
            this.btnOpenServer.Size = new System.Drawing.Size(240, 40);
            this.btnOpenServer.TabIndex = 0;
            this.btnOpenServer.Text = "Open TCP Server";
            this.btnOpenServer.UseVisualStyleBackColor = true;
            this.btnOpenServer.Click += new System.EventHandler(this.btnOpenServer_Click);
            // 
            // btnNewClient
            // 
            this.btnNewClient.Location = new System.Drawing.Point(24, 80);
            this.btnNewClient.Name = "btnNewClient";
            this.btnNewClient.Size = new System.Drawing.Size(240, 40);
            this.btnNewClient.TabIndex = 1;
            this.btnNewClient.Text = "Open new TCP client";
            this.btnNewClient.UseVisualStyleBackColor = true;
            this.btnNewClient.Click += new System.EventHandler(this.btnNewClient_Click);
            // 
            // Lab03Bai03
            // 
            this.ClientSize = new System.Drawing.Size(292, 143);
            this.Controls.Add(this.btnNewClient);
            this.Controls.Add(this.btnOpenServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Lab03Bai03";
            this.Text = "Lab03_Bai03";
            this.ResumeLayout(false);
        }
    }
}
