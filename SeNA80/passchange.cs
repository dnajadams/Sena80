using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class passchange : Form
    {
        //three timers when a user starts typing in a field the timer
        //will start, it will reset with each character, but if no typing occurs for 
        //1 second it will validate the field...when all three validations pass
        //the Accept button will highlight
        private Timer _OPtimer;
        private Timer _NPtimer;
        private Timer _CPtimer;
        bool bOldPassOK = false;
        bool bNewPassOK = false;
        bool bConfirmPassOK = false;
        string curPass = string.Empty;
        string newPass = string.Empty;
        string[] UserParts = new string[7] { "", "", "", "","","","" };
        public passchange()
        {
            InitializeComponent();
            
        }

        private void passchange_Load(object sender, EventArgs e)
        {
            //get the current pass
            UserParts = globals.Curr_User.Split(',');
            curPass = UserParts[1];

            //INITIALIZE EACH OF THE TIMERS
            _OPtimer = new Timer();
            _OPtimer.Interval = 1050;
            _OPtimer.Tick += new EventHandler(_OPtimer_Done);

            _NPtimer = new Timer();
            _NPtimer.Interval = 1050;
            _NPtimer.Tick += new EventHandler(_NPtimer_Done);

            _CPtimer = new Timer();
            _CPtimer.Interval = 1050;
            _CPtimer.Tick += new EventHandler(_CPtimer_Done);

        }
        //note: until all validation passes the button won't become active
        private void tb_OldPass_TextChanged(object sender, EventArgs e)
        {
            //fist make sure it is stopped, so every keystroke refreshes the timer
            _OPtimer.Stop();
            _OPtimer.Start();

        }
        void _OPtimer_Done(object sender, EventArgs e)
        {
            //if the user quits typing or changes focus the timer elapses
            //now validate and if not valid post message...
            if (curPass.Equals(tb_OldPass.Text))
                bOldPassOK = true;
            else
            {
                lbl_EText.Visible = true;
                lbl_PassError.Visible = true;
                lbl_PassError.Text = "Existing Passwords Do Not Match...";
            }

            if (bOldPassOK && bNewPassOK && bConfirmPassOK)
                btn_Accept.Enabled = true;
        }
        
        private void tb_NewPass_TextChanged(object sender, EventArgs e)
        {
            //fist make sure it is stopped, so every keystroke refreshes the timer
            _NPtimer.Stop();
            _NPtimer.Start();
        }
        void _NPtimer_Done(object sender, EventArgs e)
        {
            newPass = tb_NewPass.Text;
            if(!(curPass.Equals(newPass)) && newPass.Length > 5 )
                bNewPassOK = true;
            else
            {
                lbl_EText.Visible = true;
                lbl_PassError.Visible = true;
                lbl_PassError.Text = "New Password Fails Validation...";
            }

            if (bOldPassOK && bNewPassOK && bConfirmPassOK)
                btn_Accept.Enabled = true;
        }
        private void tb_ConfirmPass_TextChanged(object sender, EventArgs e)
        {
            _CPtimer.Stop();
            _CPtimer.Stop();

        }
        void _CPtimer_Done(object sender, EventArgs e)
        {
            if (newPass.Equals(tb_ConfirmPass.Text))
                bConfirmPassOK = true;
            else
            {
                lbl_EText.Visible = true;
                lbl_PassError.Visible = true;
                lbl_PassError.Text = "Confirmation Password Fails Validation...";
            }

            if (bOldPassOK && bNewPassOK && bConfirmPassOK)
                btn_Accept.Enabled = true;
        }
        private void btn_Accept_Click(object sender, EventArgs e)
        {
            //if all validation passes
            //write update the global users and save the database
            //find the user in globals.Users
            int foundat = 0;
            double newExp = (DateTime.Now - globals.dtStart).TotalDays + 365;
            string userOut = UserParts[0] + ',' + newPass + ','+ UserParts[2]+ ',' + UserParts[3]+',' +newExp.ToString("0");
            foreach (string user in globals.Users)
            {
                if (String.IsNullOrEmpty(user))
                    break;

                //compare until we match
                if (user.Equals(globals.Curr_User))
                {
                    //replace the data
                    globals.Users[foundat] = userOut;
                   
                }
                foundat++;
            }
            //rewrite the database
            DataBaseIO.WriteFinalUsers(globals.Users);
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }
    }
}
