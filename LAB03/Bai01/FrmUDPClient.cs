using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Bai01
{
    public partial class FrmUDPClient : Form
    {
        public FrmUDPClient()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // Client tự chọn port ngẫu nhiên
                using (UdpClient udpClient = new UdpClient())
                {
                    Byte[] sendBytes = Encoding.UTF8.GetBytes(rtbMessage.Text);

                    // Gửi đến địa chỉ IP và port của server 
                    udpClient.Send(sendBytes, sendBytes.Length, txtIPHost.Text, int.Parse(txtPort.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi tin: " + ex.Message);
            }
        }
    }

}
