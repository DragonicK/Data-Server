using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Server {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmMain frmMain = new FrmMain();
            frmMain.Show();

            Application.Idle += frmMain.OnApplicationIdle;
            Application.Run(frmMain);
        }
    }
}
