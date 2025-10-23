using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai01
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
           
        }
       

        private void btnServer_Click(object sender, EventArgs e)
        {
            FrmUDPServer frm = new FrmUDPServer();
            frm.Show();
        }

       

        private void btnClient_Click(object sender, EventArgs e)
        {
            FrmUDPClient frm = new FrmUDPClient();
            frm.Show();
        }
    }
}
