using DVLD.Applications;
using DVLD.Applications.International_License;
using DVLD.Login;
using DVLD.System_Logs;
using DVLD.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmDeleteLogs());


            bool IsReLogin = false;
            do
            {
                frmLogin frmLogin = new frmLogin();

                Application.Run(frmLogin);

                int UserID = Convert.ToInt16(frmLogin.Tag);
                frmMain frmMain = new frmMain(UserID);
                if (frmLogin.Tag != null)
                {
                    Application.Run(frmMain);
                    IsReLogin = (Convert.ToInt16(frmMain.Tag)) == 1;
                }
            } while (IsReLogin);

        }
    }
}
