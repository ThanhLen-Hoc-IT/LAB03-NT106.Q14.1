using System;
using System.Windows.Forms;

namespace Bai06
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            FrmServer server = new FrmServer();
            server.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            FrmClient client = new FrmClient();
            client.Show();
        }
    }
}
