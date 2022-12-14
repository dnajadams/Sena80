using System;
using System.Diagnostics;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class Protocol_Editor : Form
    {
        private int indexOfItemUnderMouseToDrop;
        private Point screenOffset;
        public static int iCurList = 0;
        bool mo1_mdown = false;
        bool ml2_mdown = false;
        bool senderOptns = false;
        ListViewItem selectedItem;


        public string GetCommentParameter(string sSelected)
        {
            string sRet = "";
            string[] parts = sSelected.Split(',');
            string sParam = "";

            Text_Param ParamDialog = new Text_Param();

            ParamDialog.tb_Comments.Text = parts[1];
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (ParamDialog.ShowDialog(this) == DialogResult.OK)
            {
                sParam = ParamDialog.tb_Comments.Text;
                sRet = parts[0] + "," + sParam;
            }
            else
            {
                sRet = sSelected;

            }
            ParamDialog.Dispose();

            return sRet;
        }
        public string GetWaitParameter(string sSelected)
        {
            string sRet = "";
            string[] parts = sSelected.Split(',');
            string sParam = "";

            Wait_Form ParamDialog = new Wait_Form();
            if (sSelected.Contains("Recycle"))
            {
                ParamDialog.lbl_Wait.Text = "Recycle for:";
                ParamDialog.Text = "Recycle Parameter";
            }
            else if (sSelected.Contains("Repeat"))
            {
                ParamDialog.lbl_Wait.Text = "Repeat for:";
                ParamDialog.Text = "Times to Repeat";
                ParamDialog.n_Wait.Value = 4;
                ParamDialog.n_Wait.Maximum = 20;
                ParamDialog.n_Wait.Minimum = 1;
                ParamDialog.lbl_Sec.Text = "times";
            }
            ParamDialog.n_Wait.Value = Convert.ToInt16(parts[1]);
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (ParamDialog.ShowDialog(this) == DialogResult.OK)
            {
                sParam = ParamDialog.n_Wait.Value.ToString();
                sRet = parts[0] + "," + sParam;
            }
            else
            {
                sRet = sSelected;

            }
            ParamDialog.Dispose();

            return sRet;
        }
        public string GetPushParameter(string sSelected)
        {
            string sRet = "";
            string[] parts = sSelected.Split(',');
            string sParam = "";


            PushToCol ParamDialog = new PushToCol();
            if (sSelected.Contains("Waste"))
            {
                ParamDialog.Text = "Waste Valve Control";
                ParamDialog.lbt_Text.Text = "Select Waste Valve State (Open or Closed)";
                ParamDialog.rb_Gas.Text = "Open";
                ParamDialog.rb_Pump.Text = "Closed";

                ParamDialog.rb_Gas.Checked = true;
            }
            if (sSelected.Contains("Push"))
            {
                if (parts[1].Contains("G"))
                    ParamDialog.rb_Gas.Checked = true;
                else
                    ParamDialog.rb_Pump.Checked = true;
            }
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (ParamDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (sSelected.Contains("Push"))
                {
                    if (ParamDialog.rb_Gas.Checked)
                        sParam = "G";
                    else
                        sParam = "P";
                }
                if (sSelected.Contains("Waste"))
                {
                    if (ParamDialog.rb_Gas.Checked)
                        sParam = "1";
                    else
                        sParam = "0";
                }
                sRet = parts[0] + "," + sParam;
            }
            else
            {
                sRet = sSelected;

            }
            ParamDialog.Dispose();

            return sRet;
        }

        public string GetProtocolParameter(string sSelected)
        {
            string sRet = "";
            string[] parts = sSelected.Split(',');
            string sParam = "";

            Protocol_Selector ParamDialog = new Protocol_Selector();

            // What directory are the files in?...
            DirectoryInfo dinfo = new DirectoryInfo(@globals.protocol_path);
            // fill the list box
            if (parts[0].Contains("Debl"))
            {
                // What type of file do we want?...
                FileInfo[] Files = dinfo.GetFiles("*.dbl");
                foreach (FileInfo file in Files)
                    ParamDialog.cb_ProtocolList.Items.Add(file.Name);

                ParamDialog.lbl_FirstProtoText.Text = "Select the Deblock Protocol from the List Below:";
            }
            if (parts[0].Contains("Oxid"))
            {
                // What type of file do we want?...
                FileInfo[] Files = dinfo.GetFiles("*.oxi");
                foreach (FileInfo file in Files)
                    ParamDialog.cb_ProtocolList.Items.Add(file.Name);

                ParamDialog.lbl_FirstProtoText.Text = "Select the Oxidation Protocol from the List Below:";
            }
            if (parts[0].Contains("Thio"))
            {
                // What type of file do we want?...
                FileInfo[] Files = dinfo.GetFiles("*.thi");
                foreach (FileInfo file in Files)
                    ParamDialog.cb_ProtocolList.Items.Add(file.Name);

                ParamDialog.lbl_2ndProtocol.Text = "Select the Thiolation Protocol from the List Below:";

            }
            if (parts[0].Contains("Mix"))
            {
                ParamDialog.cb_ProtocolList2.Visible = true;
                ParamDialog.lbl_2ndProtocol.Visible = true;
                // What type of file do we want?...                
                FileInfo[] Files = dinfo.GetFiles("*.oxi");
                foreach (FileInfo file in Files)
                    ParamDialog.cb_ProtocolList.Items.Add(file.Name);
                ParamDialog.lbl_FirstProtoText.Text = "Select the Oxidation Protocol from the List Below:";
                //fill the second list
                // What type of file do we want?...
                FileInfo[] Files2 = dinfo.GetFiles("*.thi");
                foreach (FileInfo file in Files2)
                    ParamDialog.cb_ProtocolList2.Items.Add(file.Name);
                ParamDialog.lbl_2ndProtocol.Text = "Select the Thiolation Protocol from the List Below:";

            }
            if (parts[0].Contains("Capp"))
            {
                // What type of file do we want?...
                FileInfo[] Files = dinfo.GetFiles("*.cap");
                foreach (FileInfo file in Files)
                    ParamDialog.cb_ProtocolList.Items.Add(file.Name);

                ParamDialog.lbl_FirstProtoText.Text = "Select the Capping Protocol from the List Below:";
            }
            if (parts[0].Contains("Coup"))
            {
                // What type of file do we want?...
                if (MessageBox.Show("No Parameters for Coupling Protocol", "Coupling Protocol", MessageBoxButtons.OK) == DialogResult.OK)
                    return (sSelected);

            }
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (ParamDialog.ShowDialog(this) == DialogResult.OK)
            {
                string sParam2 = string.Empty;

                sParam = ParamDialog.cb_ProtocolList.Text;
                if (parts[0].Contains("Mix"))
                    sParam2 = ParamDialog.cb_ProtocolList2.Text;

                //n_Wait.Value.ToString();
                if (!parts[0].Contains("Mix"))
                    sRet = parts[0] + "," + sParam;
                else
                    sRet = parts[0] + "," + sParam + "," + sParam2;
            }
            else
            {
                sRet = sSelected;

            }
            ParamDialog.Dispose();

            return sRet;
        }
        public string GetReagentsParameter(string sSelected)
        {
            string sRet = "";
            string[] parts = sSelected.Split(',');

            Reagent_Parameters ParamDialog = new Reagent_Parameters();

            //need the number of seconds to purge, either through columns or bypass..
            if (sSelected.Contains("Gas"))
            {
                ParamDialog.gb_FlowControl.Enabled = true;
                ParamDialog.gb_PumpControl.Enabled = false;
                ParamDialog.rb_Pump.Enabled = false;
                ParamDialog.rb_Pump.Checked = false;
                ParamDialog.rb_ByPump.Checked = true;
            }
            ParamDialog.lbl_Selected.Text = sSelected;


            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (ParamDialog.ShowDialog(this) == DialogResult.OK)
            {
                //sParam = ParamDialog.tb_Comments.Text;
                sRet = parts[0] + "," + Reagent_Parameters.ColByp + "," +
                    Reagent_Parameters.sCol + "," + Reagent_Parameters.PumpByp + "," + Reagent_Parameters.sVol;
                //MessageBox.Show(sRet, "Here");
            }
            else
            {
                sRet = sSelected;

            }
            ParamDialog.Dispose();

            return sRet;
        }
        public Protocol_Editor()
        {
            InitializeComponent();
            Met_Optns.Focus();
        }

        /* All the listbox up and down and movement
         * commands -- it works via drag and drop 
         */
        private void GoBackToList()
        {
            switch (iCurList)
            {
                case 0:
                    rbStart.Select();
                    iCurList = 0;
                    break;

                case 1:
                    rbPrep.Select();
                    iCurList = 0;
                    break;

                case 2:
                    rbPost.Select();
                    iCurList = 0;
                    break;

                case 3:
                    rbAmidite.Checked = true;
                    break;

                case 4:
                    rbDeblock.Checked = true;
                    break;

                case 5:
                    rbCap.Checked = true;
                    break;

                case 6:
                    rbOx.Checked = true;
                    break;

                case 7:
                    rbThio.Checked = true;
                    break;

                case 8:
                    rbMainLoop.Checked = true;
                    break;

            }
        }
        private void Protocol_editor_Load(object sender, EventArgs e)
        {
            rbStart.Checked = true;
            SysPrep_FillListBox(Met_Optns);
            Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
            Met_Optns.Focus();
        }


        private void Met_Optns_DragEnter(object sender, DragEventArgs e)
        {
            // This event will  never be triggered unless Met_Optns.AllowDrop == true
            // Adding this event to Met_Optns's events allows the cursor to change
            // to indicate to the user that a copy dragdrop operation is underway.
            // Execution here signifies a DragDrop operation is running on this control
            e.Effect = DragDropEffects.Copy;  // The cursor changes to show Copy
        }

        private void Met_Optns_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

            ListView lb = sender as ListView;

            if (lb != null)
            {
                Form f = lb.FindForm();
                // Cancel the drag if the mouse moves off the form. The screenOffset
                // takes into account any desktop bands that may be at the top or left
                // side of the screen.
                if (((Control.MousePosition.X - screenOffset.X) < f.DesktopBounds.Left) ||
                    ((Control.MousePosition.X - screenOffset.X) > f.DesktopBounds.Right) ||
                    ((Control.MousePosition.Y - screenOffset.Y) < f.DesktopBounds.Top) ||
                    ((Control.MousePosition.Y - screenOffset.Y) > f.DesktopBounds.Bottom))

                {
                    e.Action = DragAction.Cancel;
                }
            }
        }

        private void Met_List_DragDrop(object sender, DragEventArgs e)
        {
            string line = e.Data.GetData(DataFormats.Text).ToString();
            string cmd = string.Empty;
            string param = string.Empty;

            if (selectedItem == null)
                return;

            int startIndex = line.IndexOf(',');

            if (startIndex > 0)
            {
                cmd = line.Substring(0, startIndex);
                param = line.Substring(startIndex + 1);
            }
            else
            {
                cmd = line;
                param = "";
            }

            string[] row = { cmd.Trim(), param.Trim() };

            if (cmd != null && param != null)
            {

                string[] items = new String[2];
                items[0] = cmd;
                items[1] = param;

                //if the message is from Options, just add it
                if (senderOptns)
                    Met_List.Items.Add(new ListViewItem(items, 0));

                //get where we are
                Point cp = Met_List.PointToClient(new Point(e.X, e.Y));

                //get the information
                ListViewItem dragFromItem = new ListViewItem();
                ListViewItem dragToItem = new ListViewItem();

                if (start2 != null)
                    dragFromItem = Met_List.GetItemAt(start2.X, start2.Y);
                if (cp != null)
                    dragToItem = Met_List.GetItemAt(cp.X, cp.Y);

                //MessageBox.Show("From" + dragFromItem.ToString() + "To-" + dragToItem.ToString());
                //and the indexes
                int dropIndex = 0;

                try
                {
                    dropIndex = dragToItem.Index;
                }
                catch
                {
                    dropIndex = Met_List.Items.Count;
                }
                int removeIndex = dragFromItem.Index;

                //make sure we have the current index
                ListViewItem overItem = Met_List.GetItemAt(e.X, e.Y);

                //now insert and delete the old one
                Met_List.Items.RemoveAt(removeIndex);
                if (dropIndex < Met_List.Items.Count)
                    Met_List.Items.Insert(dropIndex, new ListViewItem(items, 0));
                else
                    Met_List.Items.Add(new ListViewItem(items, 0));

            }
            Met_List.Focus();
            Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();

            senderOptns = false;
            e.Effect = DragDropEffects.None;
            mo1_mdown = false;
            ml2_mdown = false;

        }

        private void Met_List_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.StringFormat) && (e.AllowedEffect == DragDropEffects.Copy))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;
        }

        private void Met_List_DragOver(object sender, DragEventArgs e)
        {
            ListViewItem overItem = Met_List.GetItemAt(e.X, e.Y);

            indexOfItemUnderMouseToDrop = overItem.Index;

            if (e.Effect == DragDropEffects.Move)  // When moving an item within this list
            {
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
            }
        }

        private void Met_List_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            screenOffset = SystemInformation.WorkingArea.Location;

            ListView lb = sender as ListView;

            if (lb != null)
            {
                Form f = lb.FindForm();
                // Cancel the drag if the mouse moves off the form. The screenOffset
                // takes into account any desktop bands that may be at the top or left
                // side of the screen.
                if (((Control.MousePosition.X - screenOffset.X) < f.DesktopBounds.Left) ||
                    ((Control.MousePosition.X - screenOffset.X) > f.DesktopBounds.Right) ||
                    ((Control.MousePosition.Y - screenOffset.Y) < f.DesktopBounds.Top) ||
                    ((Control.MousePosition.Y - screenOffset.Y) > f.DesktopBounds.Bottom))
                {
                    e.Action = DragAction.Cancel;
                }
            }

        }
        Point start2 = new Point();
        private void Met_List_MouseDown(object sender, MouseEventArgs e)
        {
            //get the seleced item and data
            // starts a DoDragDrop operation with allowed effect  "Copy"
            selectedItem = Met_List.GetItemAt(e.X, e.Y);
            Met_List.Focus();

            if (selectedItem == null)
                return;

            string col1 = selectedItem.SubItems[0].Text;
            string col2 = selectedItem.SubItems[1].Text;

            int indexOfItem = selectedItem.Index;


            if (e.Clicks == 2)
            {
                //MessageBox.Show("White Mouse Down");
                // if it is doubleclicked then edit the parameters and return, not doing a drag drop.
                string sSelected = string.Empty;
                string sReturn = string.Empty;

                sSelected = col1 + "," + col2;
                if (sSelected == null || sSelected == String.Empty)
                    return;

                string[] parts = sSelected.Split(',');

                string Start = parts[0];

                /***************************************
                 * Now get the parameters****************
                * *******************              */

                if (String.Equals(parts[0].Substring(0, 3), "Com", StringComparison.Ordinal))
                    sReturn = GetCommentParameter(sSelected);
                else if (Start.Contains("Reagent") || Start.Contains("Purge") || Start.Contains("Block"))
                    sReturn = GetReagentsParameter(sSelected);
                else if (Start.Contains("Wait") || Start.Contains("Recycle") || Start.Equals("Repeat"))
                    sReturn = GetWaitParameter(sSelected);
                else if (Start.Contains("Push") || Start.Contains("Waste"))
                    sReturn = GetPushParameter(sSelected);
                else if (Start.Contains("Protocol"))
                    sReturn = GetProtocolParameter(sSelected);
                else
                    MessageBox.Show("No Parameters Needed for the selected command", "No Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int startIndex = sReturn.IndexOf(',');

                if (startIndex > 0)
                {
                    col1 = sReturn.Substring(0, startIndex);
                    col2 = sReturn.Substring(startIndex + 1);
                }
                else
                {
                    col1 = sReturn;
                    col2 = "";
                }

                string[] row = { col1.Trim(), col2.Trim() };

                var listViewItem = new ListViewItem(row);
                Met_List.Items.Insert(indexOfItem, listViewItem);

                Met_List.Items.RemoveAt(indexOfItem + 1);
                Met_List.Items[indexOfItem].Selected = true;
                Met_List.Select();
                Met_List.Focus();


                return;

            }

            else
            {
                start2.X = e.X;
                start2.Y = e.Y;
                ml2_mdown = true;

            }


            if (indexOfItem >= 0 && indexOfItem < Met_List.Items.Count)  // check we clicked down on a string
            {

                if (indexOfItem == Met_List.Items.Count - 1)
                    return;

                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                //MessageBox.Show(indexOfItem.ToString());
            }

        }

        private void Met_Optns_MouseDown(object sender, MouseEventArgs e)
        {
            // starts a DoDragDrop operation with allowed effect  "Copy"
            selectedItem = Met_Optns.GetItemAt(e.X, e.Y);

            if (selectedItem == null)
                return;

            string col1 = selectedItem.SubItems[0].Text;
            string col2 = selectedItem.SubItems[1].Text;

            int indexOfItem = selectedItem.Index;


            if (e.Clicks == 2)
            {

                string[] row = { col1.Trim(), col2.Trim() };

                var listViewItem = new ListViewItem(row);
                Met_List.Items.Add(listViewItem);

                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                mo1_mdown = false;
                return;
            }
            else
            {
                mo1_mdown = true;
                if (indexOfItem >= 0 && indexOfItem < Met_Optns.Items.Count)  // check that an string is selected
                {

                    string str = GetItemText(Met_Optns);
                    if (str == "") return;
                    // Set allowed DragDropEffect to Copy selected from DragDropEffects enumberation of None, Move, All etc.
                    //Met_Optns.DoDragDrop(str, DragDropEffects.Copy);

                    Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                }
            }



        }

        private void Met_List_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //double click to delete an item???
            //MessageBox.Show("Double Clicked");
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            //find out what is highlighted and move it to the Method List and insert at the bottom
            string sSelected = "";
            ListView.SelectedIndexCollection indexes = Met_Optns.SelectedIndices;

            if (indexes != null)
            {
                sSelected = GetItemText(Met_Optns);
            }
            else
            {
                MessageBox.Show("Must Select an Item", "Error", MessageBoxButtons.OK);
                return;
            }

            if (sSelected.Length < 1)
            {
                MessageBox.Show("Bad Item Selected", "Error", MessageBoxButtons.OK);
                return;
            }
            else
            {
                int startIndex = sSelected.IndexOf(',');
                string cmd = String.Empty;
                string par = String.Empty;

                if (startIndex > 0)
                {
                    cmd = sSelected.Substring(0, startIndex);
                    par = sSelected.Substring(startIndex + 1);
                }
                else
                {
                    cmd = sSelected;
                    par = "";
                }

                string[] row = { cmd.Trim(), par.Trim() };

                ListViewItem lvItem = new ListViewItem(row);
                Met_List.Items.Add(lvItem);
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Met_List.SelectedItems.Equals(lvItem);
                Met_List.Focus();
                mo1_mdown = false;
                ml2_mdown = false;
            }



        }

        private void b_mv_up_Click(object sender, EventArgs e)
        {
            //Get the selection and move it up if not at top
            string sToMove = String.Empty;
            string col1 = String.Empty, col2 = String.Empty;

            ListView.SelectedIndexCollection indexes = Met_Optns.SelectedIndices;
            int iOfSelectedItem = Met_List.SelectedItems[0].Index;  //get the index of the item in the first column

            if (!(iOfSelectedItem > 0))
            {
                MessageBox.Show("Already at the top", "Can't Move", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (indexes != null)
            {
                sToMove = GetItemText(Met_List);

                int startIndex = sToMove.IndexOf(',');

                if (startIndex > 0)
                {
                    col1 = sToMove.Substring(0, startIndex);
                    col2 = sToMove.Substring(startIndex + 1);
                }
                else
                {
                    col1 = sToMove;
                    col2 = "";
                }

                string[] row = { col1.Trim(), col2.Trim() };

                Met_List.Items.RemoveAt(iOfSelectedItem);
                if (iOfSelectedItem < Met_List.Items.Count)
                    Met_List.Items.Insert(iOfSelectedItem - 1, new ListViewItem(row, 0));
                else
                    Met_List.Items.Add(new ListViewItem(row, 0));


                ml2_mdown = false;
                mo1_mdown = false;

                this.Met_List.Items[iOfSelectedItem - 1].Focused = true;
                this.Met_List.Items[iOfSelectedItem - 1].Selected = true;
                Met_List.Focus();

            }
            else
            {
                MessageBox.Show("Must Select an Item", "Error", MessageBoxButtons.OK);
                return;
            }

        }

        private void b_mv_dn_Click(object sender, EventArgs e)
        {
            //Get the selection and move it up if not at top
            string sToMove = String.Empty;
            string col1 = String.Empty, col2 = String.Empty;

            ListView.SelectedIndexCollection indexes = Met_Optns.SelectedIndices;
            int iOfSelectedItem = Met_List.SelectedItems[0].Index;  //get the index of the item in the first column

            if ((iOfSelectedItem + 1) == Met_List.Items.Count)
            {
                MessageBox.Show("Already at the bottom", "Can't Move", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (indexes != null)
            {
                sToMove = GetItemText(Met_List);

                int startIndex = sToMove.IndexOf(',');

                if (startIndex > 0)
                {
                    col1 = sToMove.Substring(0, startIndex);
                    col2 = sToMove.Substring(startIndex + 1);
                }
                else
                {
                    col1 = sToMove;
                    col2 = "";
                }

                string[] row = { col1.Trim(), col2.Trim() };

                Met_List.Items.RemoveAt(iOfSelectedItem);
                if (iOfSelectedItem < Met_List.Items.Count)
                    Met_List.Items.Insert(iOfSelectedItem + 1, new ListViewItem(row, 0));
                else
                    Met_List.Items.Add(new ListViewItem(row, 0));

                ml2_mdown = false;
                mo1_mdown = false;

                this.Met_List.Items[iOfSelectedItem + 1].Focused = true;
                this.Met_List.Items[iOfSelectedItem + 1].Selected = true;
                Met_List.Focus();



            }
            else
            {
                MessageBox.Show("Must Select an Item", "Error", MessageBoxButtons.OK);
                return;
            }

        }


        private void b_rmv_Click(object sender, EventArgs e)
        {

            //delete the selected item
            ListView.SelectedIndexCollection indexes = Met_Optns.SelectedIndices;

            if (indexes == null)
            {

                MessageBox.Show("Must Select the item to Remove First");
                return;
            }
            else
            {

                var index = Met_List.Items.IndexOf(Met_List.SelectedItems[0]);
                string str = GetItemText(Met_List);
                Met_List.Items.RemoveAt(index);

                if (index == 0 && Met_List.Items.Count < 1)
                    return;

                if (index == 0)
                    index = 1;

                Met_List.Items[index - 1].Selected = true;
                Met_List.Select();

                this.Met_List.Focus();

                mo1_mdown = false;
                ml2_mdown = false;

                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
            }

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                if (MessageBox.Show("You have Items in your Method List Unsaved \n Are you sure you wish to exit",
                                                 "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    return;
                }

            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
                return;
            }

        }


        private void b_param_edit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("White Mouse Down");
            // if it is doubleclicked then edit the parameters and return, not doing a drag drop.
            string sSelected = string.Empty;
            string sReturn = string.Empty;
            string sToEdit = String.Empty;
            string col1 = String.Empty, col2 = String.Empty;

            int iOfSelectedItem = Met_List.SelectedItems[0].Index;  //get the index of the item in the first column

            sToEdit = GetItemText(Met_List);

            int startIndex = sToEdit.IndexOf(',');

            if (startIndex > 0)
            {
                col1 = sToEdit.Substring(0, startIndex);
                col2 = sToEdit.Substring(startIndex + 1);
            }
            else
            {
                MessageBox.Show("No Parameters to Edit", "No Params", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }


            sSelected = col1 + "," + col2;
            if (sSelected == null || sSelected == String.Empty)
                return;


            /***************************************
             * Now get the parameters****************
            * *******************              */

            if (String.Equals(col1.Substring(0, 3), "Com", StringComparison.Ordinal))
                sReturn = GetCommentParameter(sSelected);
            else if (col1.Contains("Reagent") || col1.Contains("Purge"))
                sReturn = GetReagentsParameter(sSelected);
            else if (col1.Contains("Wait") || col1.Contains("Recycle") || col1.Equals("Repeat"))
                sReturn = GetWaitParameter(sSelected);
            else if (col1.Contains("Push"))
                sReturn = GetWaitParameter(sSelected);
            else if (col1.Contains("Protocol"))
                sReturn = GetProtocolParameter(sSelected);
            else
                MessageBox.Show("No Parameters Needed for the selected Items", "No Parameters", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int returnIndex = sReturn.IndexOf(',');

            if (returnIndex > 0)
            {
                col1 = sReturn.Substring(0, startIndex);
                col2 = sReturn.Substring(startIndex + 1);
            }
            else
            {
                col1 = sReturn;
                col2 = "";
            }

            string[] row = { col1.Trim(), col2.Trim() };

            var listViewItem = new ListViewItem(row);
            Met_List.Items.Insert(iOfSelectedItem, listViewItem);

            Met_List.Items.RemoveAt(iOfSelectedItem + 1);
            Met_List.Items[iOfSelectedItem].Selected = true;
            Met_List.Select();
            Met_List.Focus();


            return;
        }
        /* When the radio button is clicked 
         * validate change then fill the list
         */
        private void rbStart_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                SysPrep_FillListBox(Met_Optns);
                iCurList = 0;
            }
            else
            {
                Met_Optns.Items.Clear();
                SysPrep_FillListBox(Met_Optns);
                iCurList = 0;
            }


        }
        private void rbPrep_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Prep_FillListBox(Met_Optns);
                iCurList = 1;
            }
            else
            {
                Met_Optns.Items.Clear();
                Prep_FillListBox(Met_Optns);
                iCurList = 1;
            }
        }

        private void rbPost_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                iCurList = 2;
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Post_FillListBox(Met_Optns);
            }
            else
            {
                Met_Optns.Items.Clear();
                Post_FillListBox(Met_Optns);
                iCurList = 2;
            }
        }

        private void rbAmidite_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                iCurList = 3;
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Amidite_FillListBox(Met_Optns);
            }
            else
            {
                Met_Optns.Items.Clear();
                Amidite_FillListBox(Met_Optns);
                iCurList = 3;
            }
        }

        private void rbDeblock_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                iCurList = 4;
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Deblock_FillListBox(Met_Optns);
            }
            else
            {
                Met_Optns.Items.Clear();
                Deblock_FillListBox(Met_Optns);
                iCurList = 4;
            }
        }

        private void rbCap_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                iCurList = 5;
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Cap_FillListBox(Met_Optns);
            }
            else
            {
                Met_Optns.Items.Clear();
                Cap_FillListBox(Met_Optns);
                iCurList = 5;
            }
        }

        private void rbOx_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                iCurList = 6;
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Ox_FillListBox(Met_Optns);
            }
            else
            {
                Met_Optns.Items.Clear();
                Ox_FillListBox(Met_Optns);
                iCurList = 6;
            }
        }
        private void rbThio_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                iCurList = 7;
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Thiol_FillListBox(Met_Optns);
            }
            else
            {
                Met_Optns.Items.Clear();
                Thiol_FillListBox(Met_Optns);
                iCurList = 7;
            }

        }

        private void rbMainLoop_CheckedChanged(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 0)
            {
                MessageBox.Show("You have Items in your Method List Unsaved \n The list will now be reset.",
                                                  "Caution", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Met_Optns.Items.Clear();
                Met_List.Items.Clear();
                iCurList = 8;
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
                Loop_FillListBox(Met_Optns);
            }
            else
            {
                Met_Optns.Items.Clear();
                Loop_FillListBox(Met_Optns);
                iCurList = 8;
            }
        }
        private bool ContReagent(string item)
        {
            if (item.Contains("Debl") || item.Contains("Wash A") || item.Contains("Wash B") || item.Contains("Wash A") || item.Contains("Cap A")
                || item.Contains("Cap B") || item.Contains("Oxidizer") || item.Contains("Ox 2") || item.Contains("DEA") || item.Contains("CapA + CapB")
                || item.Contains("Activator") || item.Contains("Base") || item.Contains("Act1 + Base") || item.Contains("Extra") || item.Contains("Thiol"))
                return true;
            else
                return false;

        }
        private string[] FindTotals(string[] reags, double[] amts)
        {
            string temp = reags[0];
            reags[0] = string.Empty;
            double totamt = amts[0];
            bool bEnd = false;
            string[] oString = new string[14];
            int cntr = 0;

            while (!bEnd)
            {
                for (int i = 1; i < reags.Length; i++)
                {
                    if (reags[i].Equals(temp))
                    {
                        totamt = totamt + amts[i];
                        reags[i] = string.Empty;
                    }
                }
                string sOut = string.Format("{0, -27},  {1:0.00}   ,mLs", temp.Trim(), totamt);
                oString[cntr] = sOut;
                Debug.WriteLine("Current Line is" + sOut);
                totamt = 0;
                cntr = cntr + 1;

                //get next string
                bool banother = false;
                for (int n = 0; n < reags.Length; n++)
                {

                    if (!reags[n].Contains(temp) && reags[n] != string.Empty)
                    {
                        temp = reags[n];
                        banother = true;
                        n = reags.Length + 5;    //break the for loop
                    }
                }
                if (!banother)
                    bEnd = true;

            }
            return oString;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            {
                //make sure there are items to save
                if (!(Met_List.Items.Count > 1))
                {
                    MessageBox.Show("Nothing to Save", "No Items");
                    return;

                }
                //Get all the items into a string to write
                //Write to a file that the user selects
                //empty the list box
                string[] lblines = new string[Met_List.Items.Count + 21];
                string[] reagent = new string[Met_List.Items.Count];
                double[] amt = new double[Met_List.Items.Count];
                int index = 0;
                int repeatX = 0;
                lblines[0] = "[Protocol]";
                double flowcorrect = 1.0;

                //initialize reagent
                for (int t = 0; t < Met_List.Items.Count; t++)
                    reagent[t] = string.Empty;

                //create the flow correction factor
                if (globals.dReagentPCalib > 0)
                    flowcorrect = globals.dReagentPCalib * 0.35;

                //get the contents from the list box and save the file
                for (int i = 0; i < Met_List.Items.Count; i++)
                {
                    //format the string
                    string line = string.Empty;
                    Met_List.Items[i].Selected = true;

                    line = GetItemText(Met_List);
                    Debug.WriteLine("Line is - " + line + "  For" + i.ToString() + "  Count " + Met_List.Items.Count.ToString());
                    int startIndex = line.IndexOf(',');
                    string col1 = line.Substring(0, startIndex);
                    string col2 = line.Substring(startIndex);
                    char[] escapeChars = new[] { '\n', '\a', '\r', '\t' }; // etc

                    if (col1.Equals("Repeat"))
                    {
                        string[] col2part = col2.Split(',');
                        repeatX = int.Parse(col2part[1]);
                    }
                    if (col1.Equals("End Repeat"))
                        repeatX = 0;

                    if (!col1.Contains("Protocol"))
                    {
                        if (ContReagent(col1))
                        {
                            string[] parts2 = col2.Split(',');
                            Debug.WriteLine("0 = " + parts2[0] + "  1 = " + parts2[1] + "  2 = " + parts2[2] + "  3 = " + parts2[3] + "  4 = " + parts2[4] + "  Flow Correct =" + flowcorrect.ToString("0.00"));
                            if (repeatX > 0)
                            {
                                if (parts2[3].Contains("B"))  //if flowing to bypass convert sec to mLs
                                {
                                    reagent[index] = col1.Trim();
                                    amt[index] = ((double)(Convert.ToInt32(parts2[4]) * repeatX) / (double)60) * flowcorrect;
                                    Debug.WriteLine("Repeat X =" + repeatX.ToString() + " Contains B Added -" + reagent[index] + "     Amount -" + amt[index].ToString("0.00") + "    Using-" + parts2[4]);

                                }
                                else //convert from uL to mL
                                {
                                    reagent[index] = col1.Trim();
                                    amt[index] = (double)(Convert.ToInt32(parts2[4]) * repeatX) / (double)1000;
                                    Debug.WriteLine("Repeat X =" + repeatX.ToString() + " Not Contain B Added -" + reagent[index] + "     Amount -" + amt[index].ToString("0.00") + "    Using-" + parts2[4]);

                                }
                            }
                            else
                            {
                                if (parts2[3].Contains("B"))
                                {
                                    reagent[index] = col1.Trim();
                                    amt[index] = ((double)Convert.ToInt32(parts2[4]) / (double)60) * flowcorrect;
                                    Debug.WriteLine("Contains B Added -" + reagent[index] + "     Amount -" + amt[index].ToString("0.00") + "    Using-" + parts2[4]);
                                }
                                else  //convert from uL to mL
                                {
                                    reagent[index] = col1.Trim();
                                    amt[index] = Convert.ToDouble(parts2[4]) / 1000;
                                    Debug.WriteLine("Not Contains B Added -" + reagent[index] + "     Amount -" + amt[index].ToString("0.00") + "    Using-" + parts2[4]);
                                }
                            }
                            Debug.WriteLine("Added Reagent = " + reagent[index] + "      Amount = " + amt[index].ToString("0"));
                            index = index + 1;

                        }
                    }

                    //string clcol1 = new string(col1.Where(c => !escapeChars.Contains(c)).ToArray());
                    string sOut = string.Format("{0, -27}  {1,-65}", col1.Trim(), col2.Trim());
                    lblines[i + 1] = sOut;
                }
                int curline = Met_List.Items.Count + 1;
                lblines[curline] = "[Volumes]";
                curline = curline + 1;
                string[] totals = FindTotals(reagent, amt);
                foreach (string line in totals)
                {
                    lblines[curline] = line;
                    curline = curline + 1;
                }

                //Next open the file Save Dialog
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = globals.protocol_path;
                saveFileDialog1.Title = "Save Protocol Files";
                saveFileDialog1.CheckFileExists = false;
                saveFileDialog1.CheckPathExists = true;

                if (rbStart.Checked)
                {
                    saveFileDialog1.DefaultExt = "str";
                    saveFileDialog1.Filter = "All files (*.*)|*.* | System Startup files (*.str)|*.str";
                }
                else if (rbPrep.Checked)
                {
                    saveFileDialog1.DefaultExt = "prp";
                    saveFileDialog1.Filter = "All files (*.*)|*.* | Prep files (*.prp)|*.prp";
                }
                else if (rbPost.Checked)
                {
                    saveFileDialog1.DefaultExt = "psy";
                    saveFileDialog1.Filter = "All files (*.*)|*.* | Post Synthesis files (*.psy)|*.psy";
                }
                else if (rbAmidite.Checked)
                {
                    saveFileDialog1.DefaultExt = "apr";
                    saveFileDialog1.Filter = "All files (*.*)|*.* | Amidite Protocol (*.apr)|*.apr";
                }
                else if (rbDeblock.Checked)
                {
                    saveFileDialog1.DefaultExt = "dbl";
                    saveFileDialog1.Filter = "All files (*.*)|*.*|Deblock files (*.dbl)|*.dbl";
                }
                else if (rbCap.Checked)
                {
                    saveFileDialog1.DefaultExt = "cap";
                    saveFileDialog1.Filter = "All files (*.*)|*.* | Capping files (*.cap)|*.cap";
                }
                else if (rbOx.Checked)
                {
                    saveFileDialog1.DefaultExt = "oxi";
                    saveFileDialog1.Filter = "All files (*.*)|*.* | Oxidation files (*.oxi)|*.oxi";
                }
                else if (rbThio.Checked)
                {
                    saveFileDialog1.DefaultExt = "thi";
                    saveFileDialog1.Filter = "All files (*.*)|*.* | Thiolation files (*.thii)|*.thi";
                }
                else if (rbMainLoop.Checked)
                {
                    saveFileDialog1.DefaultExt = "pro";
                    saveFileDialog1.Filter = "All files (*.*)|*.* | Protocol files (*.pro)|*.pro";
                }
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    String fName = "";
                    fName = saveFileDialog1.FileName;
                    //if File Name Exists prompt to overwrite else return
                    /*if (File.Exists(fName))   //the OS takes care of this...
                    {
                        if (MessageBox.Show("Files Exists, Overwrite?", "File Exists", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            File.Delete(fName);
                        else
                            return;
                    }*/
                    //now write the file - binary format
                    //this is how to write the file in ASCII if that is preferred
                    File.WriteAllLines(fName, lblines);

                    Met_List.Items.Clear();
                    Met_List_Items.Text = "Method Commands 0";

                }
            }
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            if (Met_List.Items.Count > 1)
            {
                if (MessageBox.Show("You still have items in the list...Are you sure?", "Continue", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
                else
                {
                    Met_List.Items.Clear();

                    Met_List_Items.Text = "Method Commands 0";

                    this.Text = "Syntehsis Protocol Editor - New";
                }
            }
        }

        private void btn_Open_Click(object sender, EventArgs e)
        {
            // get the file name to open
            // determine which type of file it is
            // read it into the method edit list box
            string[] lblines = new string[100];


            //Next open the file Save Dialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = globals.protocol_path;
            //MessageBox.Show(openFileDialog1.InitialDirectory);
            openFileDialog1.Title = "Open Protocol Files";
            openFileDialog1.DefaultExt = "*.*";
            openFileDialog1.Filter = "Start files (*.str)|*.str |Prep files (*.prp)|*.prp | Post Synthesis files(*.psy) | *.psy | Amidite Protocol(*.apr) | *.apr | Deblock files(*.dbl) | *.dbl | Capping files(*.cap) | *.cap | Oxidation files(*.oxi) | *.oxi | Thiolation files(*.thi) | *.thi | Protocol files(*.pro) | *.pro";
            //if you show this no files are shown in the box???
            openFileDialog1.Filter = "All Files (*.*)|*.*| Startup files (*.str)|*.str| Prep files (*.prp)|*.prp| Post Synthesis files (*.psy)|*.psy| Amidite Protocol (*.apr)|*.apr| Deblock files (*.dbl)|*.dbl| Capping files (*.cap)|*.cap| Oxidation files (*.oxi)|*.oxi| Thiolation files (*.thi)|*.thi| Protocol files (*.pro)|*.pro";
            openFileDialog1.FilterIndex = 0;

            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fName = "";
                fName = openFileDialog1.FileName;
                string ext = Path.GetExtension(fName);
                FileStream stream = null;
                try
                {
                    stream = File.Open(fName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IOException)
                {
                    MessageBox.Show("File Inaccessable, check to see if it is open in another program", "File Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                stream.Close();

                lblines = File.ReadAllLines(fName);
                //MessageBox.Show(ext, "File Opened" + fName, MessageBoxButtons.OK);

                //evaluate the extension and set the radio button
                if (ext == ".prp")
                    rbPrep.Checked = true;
                else if (ext == ".str")
                    rbStart.Checked = true;
                else if (ext == ".pst")
                    rbPost.Checked = true;
                else if (ext == ".apr")
                    rbAmidite.Checked = true;
                else if (ext == ".dbl")
                    rbDeblock.Checked = true;
                else if (ext == ".cap")
                    rbCap.Checked = true;
                else if (ext == ".oxi")
                    rbOx.Checked = true;
                else if (ext == ".thi")
                    rbThio.Checked = true;
                else if (ext == ".psy")
                    rbPost.Checked = true;
                else if (ext == ".pro")
                    rbMainLoop.Checked = true;

                int index = 0;
                int start = 0;
                //find the prtocol secton break
                foreach (string line in lblines)
                {
                    if (line.Contains("[Volumes]"))
                    {
                        start = 1;
                        break;
                    }
                    else
                        index = index + 1;
                }

                //fill the list box
                for (int x = start; x < index; x++)
                {
                    string line = lblines[x];

                    int startIndex = line.IndexOf(',');
                    string cmd = string.Empty;
                    string par = string.Empty;

                    if (startIndex > 0)
                    {
                        cmd = line.Substring(0, startIndex);
                        par = line.Substring(startIndex + 1);
                    }
                    else
                    {
                        cmd = line;
                        par = "";
                    }

                    string[] row = { cmd.Trim(), par.Trim() };

                    var listViewItem = new ListViewItem(row);
                    Met_List.Items.Add(listViewItem);
                }

                //set the title text to show the file open
                this.Text = "Synthesis Protocol Editor - " + fName;
                int items = Met_List.Items.Count;
                Met_List_Items.Text = "Method Commands " + Met_List.Items.Count.ToString();
            }
        }

        private void New_MenuItem_Click(object sender, EventArgs e)
        {
            //just click the New button
            btn_New.PerformClick();
        }

        private void File_Open_Menuitem_Click(object sender, EventArgs e)
        {
            //just click the open button
            btn_Open.PerformClick();
        }

        private void Save_MenuItem_Click(object sender, EventArgs e)
        {
            //just click the save button
            btn_Save.PerformClick();
        }

        private void Exit_MenuItem_Click(object sender, EventArgs e)
        {
            //just click the close button
            btn_OK.PerformClick();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SeNAAboutBox dlg = new SeNAAboutBox())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return;
                    // whatever you need to do with result
                }

            }
        }

        private void Menu_Help_Click(object sender, EventArgs e)
        {
            Process.Start(globals.Help_Path);
        }

        private void Met_Optns_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mo1_mdown) return;
            if (e.Button == MouseButtons.Right) return;

            string str = GetItemText(Met_Optns);
            if (str == "") return;

            senderOptns = true;
            Met_Optns.DoDragDrop(str, DragDropEffects.Copy);

        }
        private void Met_List_MouseMove(object sender, MouseEventArgs e)
        {
            if (!ml2_mdown) return;
            if (e.Button == MouseButtons.Right) return;

            string str = GetItemText(Met_List);
            if (str == "") return;


            Met_List.DoDragDrop(str, DragDropEffects.Copy | DragDropEffects.Move);
        }
        public string GetItemText(ListView LVIEW)
        {
            int nTotalSelected = LVIEW.SelectedIndices.Count;
            if (nTotalSelected <= 0) return "";
            IEnumerator selCol = LVIEW.SelectedItems.GetEnumerator();
            selCol.MoveNext();
            ListViewItem lvi = (ListViewItem)selCol.Current;
            string mDir = "";
            for (int i = 0; i < lvi.SubItems.Count; i++)
                mDir += lvi.SubItems[i].Text + ",";

            mDir = mDir.Substring(0, mDir.Length - 1);
            return mDir;
        }

        private void Met_Optns_MouseUp(object sender, MouseEventArgs e)
        {
            mo1_mdown = false;
            ml2_mdown = false;
            senderOptns = false;

        }

        private void Protocol_Editor_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                mo1_mdown = false;
                ml2_mdown = false;
            }
            if (e.KeyCode == Keys.Delete)
            {
                //get selected item from Protocol List and remove it 
            }

        }

        private void Met_Optns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                mo1_mdown = false;
                ml2_mdown = false;
            }

        }

        private void Met_Optns_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
        }

        private void Met_List_MouseUp(object sender, MouseEventArgs e)
        {
            mo1_mdown = false;
            ml2_mdown = false;
            senderOptns = false;

            Met_List.Focus();
        }

        private void Met_Optns_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(Met_Optns, "Protocol Options", "The list of commands that you may\nselect from to construct your protocol...");
        }

        private void Met_List_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(Met_List, "Protocol List", "The current command list for\nthe protocol being developed...");
        }

        private void btn_Add_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Add, "Add Selected Item", "Will copy the item in the Protocol Options List\nto the Protocol list...");
        }

        private void rbStart_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbStart, "Start Protocol", "The protocol that will be executed prior to any other\nintended to be used for system preparation");

        }

        private void rbPrep_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbPrep, "Prep Protocol", "A protocol for steps to be done prior to the cyclic protocol\nsuch as initial washes or deblock..");
        }

        private void rbPost_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbPost, "Post Run Protocol", "The protocol that will be executed after the oligo synthesis is complete\nthe most common is for trityl off or drying an oligo...");
        }

        private void rbAmidite_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbAmidite, "Amidite Protocol", "The protocol that provies specific the commands needed to couple a specific amidite\nduring the synthesis cycle...");
        }

        private void rbDeblock_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbDeblock, "Deblock Protocol", "The protocol that provies the commands need to perform the deprotection step\n during the synthesis cycle.");

        }

        private void rbCap_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbCap, "Capping Protocol", "The protocol that provies the commands needed to perform the capping step\nduring the synthesis cycle...");

        }

        private void rbOx_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbOx, "Oxidation Protocol", "The protocol that provies the commands needed to perform the oxidation step\nduring the synthesis cycle...");
        }

        private void rbThio_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbThio, "Thiolation Protocol", "The protocol that provies the commands needed to perform the thiolation step\nduring the synthesis cycle...");
        }

        private void rbMainLoop_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(rbMainLoop, "Main Loop Protocol", "The protocol that links together the step protocols into a cyclic looping protocol\nthat will be executed for each base...");
        }

        private void b_mv_up_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(b_mv_up, "Move Up", "Move the item selected up in the protocol command list...");
        }

        private void b_rmv_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(b_rmv, "Remove Item", "Delete the item selected from the protocol command list...");
        }

        private void b_param_edit_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(b_param_edit, "Edit Parameters", "Edit the run parameters for the item selected...");
        }

        private void b_mv_dn_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(b_mv_dn, "Move Down", "Move the item selected down in the protocol command list...");
        }

        private void btn_New_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_New, "New Protocol", "Empty the current command list and\nstart a new protocol of the type selected...");
        }

        private void btn_Open_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Open, "Open Protocol", "Open an exiting protocol from file for editing....");
        }

        private void btn_Save_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_Save, "Save Protocol", "Save the current commands in the command list to a disk file...");
        }

        private void btn_OK_MouseHover(object sender, EventArgs e)
        {
            if (!globals.bShowTips)
                return;
            else
                Man_Controlcs.PopToolMsg(btn_OK, "Exit", "Close the Protocol Editor.....");
        }
    }
}