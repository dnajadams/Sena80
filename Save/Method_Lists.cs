using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Protocol_Editor : Form
    {
     
        /*Commands to perform before the 
         * start of the cycling protocol
         */ 
        public void Prep_FillListBox(ListBox hList)
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
                "Extra 1 Reagent\t\t,B,0, B, 3",
                "Extra 2 Reagent\t\t,B, 0,B, 3",
                "Activator 1 Reagent\t,B, 0,P, 50",
                "Activator 2 Reagent\t,B, 0,P, 50 ",
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
                "Comment\t\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=======================================",
                "Initialize Pump\t\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the box
            foreach (string line in ListItems)
            {
                hList.Items.Add(line);

            }
        }

        /* Post Synthesis Protocol 
         */
        public void Post_FillListBox(ListBox hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B,0, B, 4",
                "Wash B Reagent\t\t,B,0, B, 4",
                "Deblock Reagent\t\t,B, 0,B, 4",
                "Extra 1 Reagent\t\t,B, 0,B, 3",
                "Extra 2 Reagent\t\t,B, 0,B, 3",
                "Comment\t\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=======================================",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the box
            foreach (string line in ListItems)
            {
                hList.Items.Add(line);

            }
        }

        /* Amidite Protocol
         * Each Amidite must have its own protocol
         * provides the steps for coupling 
         */
        public void Amidite_FillListBox(ListBox hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B,0, B, 3",
                "Cap A Reagent\t\t,B, 0,B, 3",
                "Cap B Reagent\t\t,B, 0,B, 3",
                "Wash B Reagent\t\t,B, 0,B, 4",
                "Activator 1 Reagent\t,B, 0,P, 50",
                "Activator 2 Reagent\t,B, 0,P, 50 ",
                "Base Reagent\t\t,B, 0,P, 25",
                "Act1 + Base Reagent\t,B, 0,P, 25",
                "Act2 + Base Reagent\t,B, 0,P, 25",
                "Comment\t\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=======================================",
                "Initialize Pump\t\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the box
            foreach (string line in ListItems)
            {
                hList.Items.Add(line);

            }
        }

        /* Deblock Protocol Method Options
         */
        public void Deblock_FillListBox(ListBox hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B, 0,B, 4",
                "Deblock Reagent\t\t,B, 0,B, 4",
                "Extra 1 Reagent\t\t,B, 0,B, 3",
                "Extra 2 Reagent\t\t,B, 0,B, 3",
                "Comment\t\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=======================================",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the box
            foreach (string line in ListItems)
            {
                hList.Items.Add(line);

            }
        }

        /*Capping Method Commands 
         */
        public void Cap_FillListBox(ListBox hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B, 0,B, 4",
                "Cap A Reagent\t\t,B, 0,B, 3",
                "Cap B Reagent\t\t,B, 0,B, 3",
                "CapA + CapB Reagent\t,B,0 ,B, 3",
                "Extra 1 Reagent\t\t,B, 0,B, 3",
                "Extra 2 Reagent\t\t,B, 0,B, 3",
                "Comment\t\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=======================================",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the box
            foreach (string line in ListItems)
            {
                hList.Items.Add(line);

            }
        }
        /* Oxidation Protocol Editor 
         * Command Options
         */
        public void Ox_FillListBox(ListBox hList)
        {
            string[] ListItems = new string[]
            {
                "Wash A Reagent\t\t,B, 0,B, 4",
                "Oxidizer Reagent\t\t,B, 0,B, 3",
                "Ox 2 Reagent\t\t,B, 0,B, 3",
                "Extra 1 Reagent\t\t,B, 0,B, 3",
                "Extra 2 Reagent\t\t,B, 0,B, 3",
                "Comment\t\t\t,Text",
                "Wait\t\t\t,5",
                "=======Empty Line=======================================",
                "Initialize Pump\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the box
            foreach (string line in ListItems)
            {
                hList.Items.Add(line);

            }
        }
        /* Now for the loop core we simply line the
         * protocols together in the fashion that the user wants
         * at runtime we will select 3 protocols prep, loop, and post
         */
        public void Loop_FillListBox(ListBox hList)
        {
            string[] ListItems = new string[]
            {
                "Deblock Protocol\t\t\t,protocol",
                "Coupling Protocol\t\t,current base",
                "Capping Protocol\t\t\t,protocol",
                "Oxidation Protocol\t\t,protocol",
                "Comment\t\t\t\t,Text",
                "Wait\t\t\t\t,5",
                "=======Empty Line=======================================",
                "Initialize Pump\t\t\t,0"
            };
            int lines = 0;

            lines = ListItems.Length;

            //load the list into the box
            foreach (string line in ListItems)
            {
                hList.Items.Add(line);

            }
        }

    }
}