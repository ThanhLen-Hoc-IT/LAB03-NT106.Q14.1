namespace Bai01
{
    partial class FrmUDPServer
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
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnListen = new System.Windows.Forms.Button();
            this.lvReceivedMessage = new System.Windows.Forms.ListView();
            this.lblReceivedMessages = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(42, 24);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(40, 20);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(89, 17);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(143, 27);
            this.txtPort.TabIndex = 1;
            // 
            // btnListen
            // 
            this.btnListen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListen.Location = new System.Drawing.Point(450, 17);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(102, 38);
            this.btnListen.TabIndex = 2;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // lvReceivedMessage
            // 
            this.lvReceivedMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvReceivedMessage.HideSelection = false;
            this.lvReceivedMessage.Location = new System.Drawing.Point(46, 93);
            this.lvReceivedMessage.Name = "lvReceivedMessage";
            this.lvReceivedMessage.Size = new System.Drawing.Size(506, 247);
            this.lvReceivedMessage.TabIndex = 3;
            this.lvReceivedMessage.UseCompatibleStateImageBehavior = false;
            // 
            // lblReceivedMessages
            // 
            this.lblReceivedMessages.AutoSize = true;
            this.lblReceivedMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceivedMessages.Location = new System.Drawing.Point(42, 70);
            this.lblReceivedMessages.Name = "lblReceivedMessages";
            this.lblReceivedMessages.Size = new System.Drawing.Size(160, 20);
            this.lblReceivedMessages.TabIndex = 4;
            this.lblReceivedMessages.Text = "Received messages";
            // 
            // FrmUDPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 378);
            this.Controls.Add(this.lblReceivedMessages);
            this.Controls.Add(this.lvReceivedMessage);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblPort);
            this.Name = "FrmUDPServer";
            this.Text = "UDP Server";
            this.Load += new System.EventHandler(this.FrmUDPServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.ListView lvReceivedMessage;
        private System.Windows.Forms.Label lblReceivedMessages;
    }
}