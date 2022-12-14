using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeNA80
{
    public partial class BaseTable : Form
    {
        DataTable dt = new DataTable();

        public BaseTable()
        {
            InitializeComponent();
        }
        private void BaseTable_Load(object sender, EventArgs e)
        {
            //check the box
            if (globals.i12Ltr == 0)
                rb_1ltr.Checked = true;
            else
                rb_2ltr.Checked = true;

            //fill the datatable
            string path = globals.sequence_path;
            System.IO.StreamReader file = new System.IO.StreamReader(path + "\\BaseTable.csv");

            DataColumn dtColumn = new DataColumn();
            dtColumn.ColumnName = "1 Ltr.";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.ColumnName = "2 Ltr.";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.ColumnName = "Base Name";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.ColumnName = "Mol. Weight";
            dtColumn.DataType = System.Type.GetType("System.Double");
            dt.Columns.Add(dtColumn);

            string newline = string.Empty;
            while ((newline = file.ReadLine()) != null)
            {
                if (newline == null || newline == string.Empty)
                    break;

                DataRow dr = dt.NewRow();
                string[] values = newline.Split(',');

                for (int i = 0; i < values.Length; i++)
                {
                    if (values[2].Contains("Blank"))
                    {
                        dr[0] = dr[1] = string.Empty;
                        dr[2] = values[2];
                        dr[3] = 0.0;
                    }
                    else
                        dr[i] = values[i];
                }
                dt.Rows.Add(dr);
            }
            file.Close();
            dt_BaseTable.DataSource = dt;
            dt_BaseTable.Columns[0].Width = 50;
            dt_BaseTable.Columns[1].Width = 70;
            dt_BaseTable.Columns[2].Width = 350;
            dt_BaseTable.Columns[3].Width = 250;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            // get the data into an array
            string[] RowsOut = new string[dt_BaseTable.RowCount];
            for (int rows = 0; rows < dt_BaseTable.Rows.Count - 1; rows++)
            {
                string[] coldata = new string[4];
                for (int col = 0; col < dt_BaseTable.Rows[rows].Cells.Count; col++)
                {
                    if ((dt_BaseTable.Rows[rows].Cells[col].Value.ToString() == null || dt_BaseTable.Rows[rows].Cells[col].Value.ToString() == string.Empty) && !dt_BaseTable.Rows[rows].Cells[2].Value.ToString().Contains("Blank"))
                        break;

                    coldata[col] = dt_BaseTable.Rows[rows].Cells[col].Value.ToString();

                }
                RowsOut[rows] = coldata[0] + "," + coldata[1] + "," + coldata[2] + "," + coldata[3];
            }
            //write the data to a file
            string path = globals.sequence_path;
            System.IO.StreamWriter file = new System.IO.StreamWriter(path + "\\BaseTable.csv");
            foreach (string line in RowsOut)
            {
                file.WriteLine(line);
            }
            file.Close();

            //update properties settings
            if (rb_1ltr.Checked)
            {
                globals.i12Ltr = 0;
                Properties.Settings.Default.Ltr_12 = 0;
            }
            else
            {
                globals.i12Ltr = 1;
                Properties.Settings.Default.Ltr_12 = 1;
            }
            Properties.Settings.Default.Save();

            MessageBox.Show("Data Table Successfully Updated\n\nPress Close to Return", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dt_BaseTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //validate the information input
            //MessageBox.Show("Added" + e.RowIndex.ToString());
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Here I am ");
            foreach (DataGridViewRow item in this.dt_BaseTable.SelectedRows)
            {
                dt_BaseTable.Rows.RemoveAt(item.Index);
            }

        }

        private void dt_BaseTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            globals.bDirtyBase = true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

        }

        private void BaseTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ValidateBases())
            {
                MessageBox.Show("Duplicates are Not Allowed\n\nPlease remove duplicates", "Duplicates Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.DialogResult = DialogResult.Ignore;
                e.Cancel = true;
            }

        }
        private bool ValidateBases()
        {
            string[] RowsTest = new string[dt_BaseTable.RowCount];
            string[] amlabels = new string[dt_BaseTable.Rows.Count];
            string[] amlabels2 = new string[dt_BaseTable.Rows.Count];

            //initialize labels arrays
            for (int i = 0; i < dt_BaseTable.Rows.Count; i++)
            {
                amlabels[i] = string.Empty;
                amlabels2[i] = string.Empty;
            }
            int tablength = dt_BaseTable.Rows.Count;

            for (int rows = 0; rows < dt_BaseTable.Rows.Count - 1; rows++)
            {
                string[] coldata = new string[4];
                for (int col = 0; col < dt_BaseTable.Rows[rows].Cells.Count; col++)
                {
                    if (dt_BaseTable.Rows[rows].Cells[col].Value.ToString() == null || dt_BaseTable.Rows[rows].Cells[col].Value.ToString() == string.Empty)
                    {
                        if (!(dt_BaseTable.Rows[rows].Cells[2].Value.ToString().Contains("Blank")))
                            return (true);
                    }
                    if (col == 0)
                        amlabels[rows] = dt_BaseTable.Rows[rows].Cells[col].Value.ToString();

                    if (col == 1)
                        amlabels2[rows] = dt_BaseTable.Rows[rows].Cells[col].Value.ToString();

                    coldata[col] = dt_BaseTable.Rows[rows].Cells[col].Value.ToString();

                }
            }
            //now check each 1 letter code
            for (int j = 1; j < tablength; j++)
            {
                if (amlabels[j].Length > 0 && amlabels[j] != null)
                {
                    for (int k = j + 1; k < tablength; k++)
                    {
                        if (amlabels[k].Length > 0 && amlabels[k] != null)
                        {
                            if (amlabels[k].Equals(amlabels[j]))
                            {
                                Debug.WriteLine("Duplicates Found at " + j.ToString() + " am = " + amlabels[j] + " With- " + k.ToString() + " am = " + amlabels[k]);
                                return (true);
                            }
                        }
                    }
                }
            }
            //repeat for two letter codes
            for (int j = 1; j < tablength; j++)
            {
                if (amlabels2[j].Length > 0)
                {
                    for (int k = j + 1; k < tablength; k++)
                    {
                        if (amlabels2[k].Length > 0 && amlabels2[k] != null)
                        {
                            if (amlabels2[k].Equals(amlabels2[j]))
                            {
                                Debug.WriteLine("Duplicates Found at " + j.ToString() + " am = " + amlabels2[j] + " With- " + k.ToString() + " am = " + amlabels2[k]);
                                return (true);
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
