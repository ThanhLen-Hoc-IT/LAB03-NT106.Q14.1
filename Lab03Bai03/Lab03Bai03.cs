using System;
using System.Windows.Forms;

namespace Lab03Bai03
{
    public partial class Lab03Bai03 : Form
    {
        private ServerForm serverForm;

        public Lab03Bai03()
        {
            InitializeComponent();
        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            if (serverForm == null || serverForm.IsDisposed)
            {
                serverForm = new ServerForm();
                serverForm.Show();
            }
            else
            {
                serverForm.BringToFront();
            }
        }

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            ClientForm client = new ClientForm();
            client.Show();
        }
    }
}
