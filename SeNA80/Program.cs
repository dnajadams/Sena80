using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeNA80
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public Program()
        {
            //initialize App
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

        }
        [STAThread]
        static void Main()
        {
         
            //check for running application
            if (ApplicationRunningHelper.AlreadyRunning())
            {
                return;
            }
          
            //first check for bQuickLong
            if (!globals.bQuickLogin)
            {
                UserLoginForm login = new UserLoginForm();
                if (login != null)
                {
                    login.ShowDialog();
                    while (login.DialogResult != DialogResult.OK)
                    {
                        if (!globals.UserOK)
                        {
                                login.Dispose();
                                return;
                        }

                    }
                    //now store the user and the rigths in the global variables
                    //edit system configuration
                    string[] s = globals.Curr_User.Split(',');
                    globals.Curr_User = s[0];
                    globals.Curr_Rights = s[2];
                    login.Dispose();
                    DoMain();
                }
            } //quick login

        }
        static void DoMain()
        {
            Application.Run(new Main_Form());
        }
    }
}

