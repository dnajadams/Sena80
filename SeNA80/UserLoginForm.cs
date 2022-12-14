using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class UserLoginForm : Form
    {
        public int noOfUsers = 0;
        public string[] names = new string[0];
        int ErroCnt = 0;
        public string[] locUsers = globals.Users;

        public UserLoginForm()
        {
            InitializeComponent();
        }

        private void UserLoginForm_Load(object sender, EventArgs e)
        {
            
            //ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            //string AppDataPath = Application.CommonAppDataPath;
            //int index = AppDataPath.IndexOf("Ltd.");
            //string newApp = AppDataPath.Substring(0, index + 4);

            string passpath = "c:\\SeNA\\SeNA80sec.bin";
            //check if file exists, if not create a default
            if (File.Exists(passpath))
            {
                DataBaseIO.ReadUsers();
            }
            else // create a starter file with 4 users (Admin, Service, User 1, User 2)
            {

                DataBaseIO.WriteInitUsers();

            }//done with create Users
            
            //now check if PS.Autologin Checked.  -- simply close
            if (globals.bQuickLogin)
            {
                globals.UserOK = true;
                this.Close();
            }
            //clear any previous contents
            tb_UserName.Text = String.Empty;
            tb_Password.Text = String.Empty;

            //count the users
            for (int k = 0; k < locUsers.Length; k++)
            {
                if (String.IsNullOrEmpty(globals.Users[k]))
                    break;

                if (globals.Users[k].Length > 15)
                    noOfUsers = noOfUsers + 1;

                //MessageBox.Show(globals.Users[k]);
            }
            
            //load the users into the Autocomplete text box
           // names = new string[noOfUsers];
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            for (int i= 0; i< noOfUsers;i++)
            {
                string[] parts = globals.Users[i].Split(',');
                names.Add(parts[0]);
               
            }
            tb_UserName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tb_UserName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //var source = new AutoCompleteStringCollection();
           
            //source.AddRange(names);
        
            tb_UserName.AutoCompleteCustomSource = names;
           
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            bool bUserFound = false;
           
            //first check User Name
            for (int j = 0; j < noOfUsers; j++)
            {
                string[] parts = globals.Users[j].Split(',');
               // MessageBox.Show("I in Name Check For - " + parts[0] + "\nCompared to "+tb_UserName.Text+"\nErrors - "+ErroCnt.ToString(), globals.Users[j]);

                if (tb_UserName.Text.Contains(parts[0].Substring(0,4))) //if User found
                {
                    //MessageBox.Show("Found Name");
                    //check password
                    if (parts[1].Trim().Equals(tb_Password.Text.Trim())) //if password matches
                    {
                        //copy the data for global visibility
                        globals.Curr_User = globals.Users[j];
                        bUserFound = true;

                        //MessageBox.Show("Passwords Match", globals.Curr_User);
                        //last check is for expiration
                        if (Convert.ToBoolean(parts[3]))
                            CheckExpiration(parts);

                        //MessageBox.Show("Passwords Match", parts[3]);
                        globals.UserOK = true;
                        DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                       
                       
                    }
                    else
                    {
                        lbl_Error.Visible = true;
                        lbl_ErrorText.Visible = true;
                        tb_Password.Clear();
                        lbl_ErrorText.Text = "Password Does Not Match...Please try again...";
                        ErroCnt++;
                        if(ErroCnt > 5)
                        {
                            MessageBox.Show("Maximum Number of Retries is 5\n\nI will exit now", "Too Many Retries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            globals.UserOK = false;
                            btn_Cancel.PerformClick();
                            return;
                        }
                        return;
                    }
                    

                }
                else
                {
                   
                    if (ErroCnt < 5)
                    {
                        if (j == noOfUsers-1)
                        {
                            lbl_Error.Visible = true;
                            lbl_ErrorText.Visible = true;
                            tb_Password.Clear();
                            tb_UserName.Clear();
                            lbl_ErrorText.Text = "User Not Found...Please try again...";
                            ErroCnt++;
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Maximum Number of Retries is 5\n\nI will exit now", "Too Many Retries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        globals.UserOK = false;
                        btn_Cancel.PerformClick();
                        return;
                    }
                }
            }
            if (!bUserFound && ErroCnt < 5)
            {
                lbl_ErrorText.Visible = true;
                tb_Password.Clear();
                lbl_ErrorText.Text = "User Name Does Not Match...Please try again...";
                ErroCnt++;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
           
        }
        public bool CheckExpiration(string [] UserToCheck)
        {
            //the expiration date is 1 year from the time they were registered on the system 
            //we use 01/01/2018 as a reference date so if they were registered on 01/01/2019 and today is 01/01/2020
            //the calculation would be date registered - reference date + 365; so if today is 1 year beyond when they registered
            //their expiration is expired...
            int daystoadd = Convert.ToInt32(UserToCheck[4]);
            DateTime dateregistered = globals.dtStart.AddDays(daystoadd - 365);

            //now simply subtract dateregistered from today and convert to days...if greater than 365 prompt for new password
            double dayssincereg =  DateTime.Now.Subtract((dateregistered)).TotalDays;
            
            MessageBox.Show("Date Registered -  " + dateregistered.ToString("D") + "\n\nYour password will expire on -  " + dateregistered.AddDays(365).ToString("D"), "Password Notification");
            if(dayssincereg > 365)
            {
                //show redo password window, check against existing password
                passchange ps = new passchange();
                ps.ShowDialog();

                if(ps.DialogResult == DialogResult.OK)
                    MessageBox.Show("Password Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                return true;
            }
            return true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            globals.UserOK = false;
            this.Close();
        }

       
        private void btn_hidden_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HERE IN CLICK");
            globals.UserOK = true;
            this.Close();
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            if (btn_show.Text.Contains("Sho"))
            {
                this.tb_Password.UseSystemPasswordChar = false;
                tb_Password.PasswordChar = '\0';
                btn_show.Text = "Hide";
            }
            else
            {
                this.tb_Password.UseSystemPasswordChar = true;
                tb_Password.PasswordChar = '*';
                btn_show.Text = "Show";

            }

        }
    }
}
