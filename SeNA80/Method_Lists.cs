using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Protocol_Editor : Form
    {

        /*Commands to perform before the 
         * start of the cycling protocol
         */
        //Commands for System Preparation
        public void SysPrep_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Pressurize System\t\t,",
                "Wash A Reagent\t\t,B,0, B, 3",
                "Wash B Reagent\t\t,B,0, B, 4",
                "Deblock Reagent\t\t,B,0, B, 4",
                "Cap A Reagent\t\t,B, 0,B, 3",
                "Cap B Reagent\t\t,B, 0,B, 3",
                "Oxidizer Reagent\t\t,B, 0,B, 3",
                "Ox 2 Reagent\t\t,B, 0,B, 3",
                "DEA Reagent\t\t,B,0, B, 3",
                "Gas Purge\t\t,B, 0,B, 3",
                "Activator 1 Reagent     \t,B, 0,P, 50",
                "Activator 2 Reagent     \t,B, 0,P, 50 ",
                "Amidite 1 Reagent\t\t,B, 0,P, 25",
                "Amidite 2 Reagent\t\t,B, 0,P, 25",
                "Amidite 3 Reagent\t\t,B, 0,P, 25",
                "Amidite 4 Reagent\t\t,B, 0,P, 25",
                "Amidite 5 Reagent\t\t,B, 0,P, 25",
                "Amidite 6 Reagent\t\t,B, 0,P, 25",
                "Amidite 7 Reagent\t\t,B, 0,P, 25",
                "Amidite 8 Reagent\t\t,B, 0,P, 25",
                "Amidite 9 Reagent\t\t,B, 0,P, 25",
                "Amidite 10 Reagent\t\t,B, 0,P, 25",
                "Amidite 11 Reagent\t\t,B, 0,P, 25",
                "Amidite 12 Reagent\t\t,B, 0,P, 25",
                "Amidite 13 Reagent\t\t,B, 0,P, 25",
                "Amidite 14 Reagent\t\t,B, 0,P, 25",
                "Comment\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.HideSelection = false;
            hList.Items[0].Selected = true;
            hList.Items[0].Focused = true;
            hList.FocusedItem = hList.Items[0];
            hList.Focus();
        }
        // Commands for Prep Protocols
        public void Prep_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B,0, B, 3",
                "Wash B Reagent\t\t,B,0, B, 4",
                "Deblock Reagent\t\t,B,0, B, 4",
                "Cap A Reagent\t\t,B, 0,B, 3",
                "Cap B Reagent\t\t,B, 0,B, 3",
                "Oxidizer Reagent\t\t,B, 0,B, 3",
                "Ox 2 Reagent\t\t,B, 0,B, 3",
                "DEA Reagent\t\t,B,0, B, 3",
                "Gas Purge\t\t,B, 0,B, 3",
                "Comment\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.HideSelection = false;
            hList.Items[0].Selected = true;
            hList.Items[0].Focused = true;
            hList.FocusedItem = hList.Items[0];
            hList.Focus();
        }

        /* Post Synthesis Protocol 
         */
        public void Post_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B,0, B, 4",
                "Wash B Reagent\t\t,B,0, B, 4",
                "Both AB Reagent\t\t,B,0, B, 4",
                "Deblock Reagent\t\t,B, 0,B, 4",
                "DEA Reagent\t\t,B, 0,B, 3",
                "Gas Purge\t\t,B, 0,B, 3",
                "Comment\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.Items[0].Selected = true;
            hList.Focus();
        }

        /* Amidite Protocol
         * Each Amidite must have its own protocol
         * provides the steps for coupling 
         */
        public void Amidite_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t\t,B,0, B, 3",
                "Wash B Reagent\t\t\t,B, 0,B, 4",
                "Amidite Block Wash\t\t\t,B, 0,B, 4",
                "Both AB Reagent\t\t,B,0, B, 4",
                "Act+B Reagent\t\t,B, 0,P, 25",
                "Activator 1 Reagent     \t\t,B, 0,P, 50",
                "Activator 2 Reagent     \t\t,B, 0,P, 50 ",
                "Base Reagent\t\t\t,B, 0,P, 25",
                "Act1 + Base Reagent\t\t,B, 0,P, 25",
                "Act2 + Base Reagent\t\t,B, 0,P, 25",
                "Push To Column, P",
                "Repeat\t\t\t,4",
                "End Repeat",
                "Recycle\t\t\t\t,90",
                "Waste Valve,1",
                "Comment\t\t\t,Text",
                "Wait\t\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.Items[0].Selected = true;
            hList.Focus();
        }

        /* Deblock Protocol Method Options
         */
        public void Deblock_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B, 0,B, 4",
                "Deblock Reagent\t\t,B, 0,B, 4",
                "Gas Purge\t\t,B, 0,B, 3",
                "Waste Valve,1",
                "Repeat\t\t\t,4",
                "End Repeat",
                "Comment\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.Items[0].Selected = true;
            hList.Focus();
        }

        /*Capping Method Commands 
         */
        public void Cap_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B, 0,B, 4",
                "Wash B  Reagent\t\t,B, 0,B, 4",
                "Both AB Reagent\t\t,B,0, B, 4",
                "Cap A Reagent\t\t,B, 0,B, 3",
                "Cap B Reagent\t\t,B, 0,B, 3",
                "CapA + CapB Reagent\t,B,0 ,B, 3",
                "Gas Purge\t\t,B, 0,B, 3",
                "Waste Valve,1",
                "Repeat\t\t\t,4",
                "End Repeat",
                "Comment\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.Items[0].Selected = true;
            hList.Focus();
        }
        /* Oxidation Protocol Editor 
         * Command Options
         */
        public void Ox_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B, 0,B, 4",
                "Oxidizer Reagent\t\t,B, 0,B, 3",
                "Ox 2 Reagent\t\t,B, 0,B, 3",
                "Gas Purge\t\t,B, 0,B, 3",
                "Waste Valve,1",
                "Repeat\t\t\t,4",
                "End Repeat",
                "Comment\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.Items[0].Selected = true;
            hList.Focus();
        }
        /* Thiolation Protocol Editor 
         * Command Options
         */
        public void Thiol_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B, 0,B, 4",
                "Oxidizer Reagent\t\t,B, 0,B, 3",
                "Ox 2 (Thiolatin) Reagent\t\t,B, 0,B, 3",
                "Gas Purge\t\t,B, 0,B, 3",
                "Waste Valve,1",
                "Repeat\t\t\t,4",
                "End Repeat",
                "Comment\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.Items[0].Selected = true;
            hList.Focus();
        }
        /* Now for the loop core we simply line the
         * protocols together in the fashion that the user wants
         * at runtime we will select 3 protocols prep, loop, and post
         */
        public void Loop_FillListBox(ListView hList)
        {
            string[] ListItems = new string[]
            {
                "Deblock Protocol\t\t\t,protocol",
                "Coupling Protocol\t\t\t,current base",
                "Capping Protocol\t\t\t,protocol",
                "Oxidation ONLY Protocol\t\t\t,protocol",
                 "Mixed PS-PO Protocol   \t,protocol,protcol",
                "Thiolation ONLY Protocol\t\t\t,protocol",
                "Comment\t\t\t,Text",
                "Wait\t\t\t\t,5",
                "=======Empty Line=========",
                "Initialize Pump\t\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the listview box
            foreach (string line in ListItems)
            {
                string cmd = string.Empty;
                string parameters = String.Empty;

                int startIndex = line.IndexOf(',');

                if (startIndex > 0)
                {
                    cmd = line.Substring(0, startIndex);
                    parameters = line.Substring(startIndex + 1);
                }
                else
                {
                    cmd = line;
                    parameters = "";
                }

                string[] row = { cmd.Trim(), parameters.Trim() };

                var listViewItem = new ListViewItem(row);
                hList.Items.Add(listViewItem);
            }
            hList.Items[0].Selected = true;
            hList.Focus();
        }

    }
}