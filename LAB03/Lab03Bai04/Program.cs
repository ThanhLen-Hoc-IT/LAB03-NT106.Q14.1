using System;
using System.Windows.Forms;

namespace Lab03
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

          
            Application.Run(new FrmDashboard());
        }
    }
}