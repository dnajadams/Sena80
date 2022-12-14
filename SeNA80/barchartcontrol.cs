using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ZedGraph;

namespace SeNA80
{

    public partial class barchartcontrol : Form
    {
        public static int cycles = 0;
        public static bool bLabelsOn = true;
        public static int iRepotOption = Properties.Settings.Default.UV_Report;
        public static double[,] iReportArray = new double[4, 200];
        public static PointPairList c1 = new PointPairList();
        public static PointPairList c2 = new PointPairList();
        public static PointPairList c3 = new PointPairList();
        public static PointPairList c4 = new PointPairList();
        public static PointPairList c5 = new PointPairList();
        public static PointPairList c6 = new PointPairList();
        public static PointPairList c7 = new PointPairList();
        public static PointPairList c8 = new PointPairList();
        public static bool bC1 = false, bC2 = false, bC3 = false, bC4 = false;
        public static bool bC5 = false, bC6 = false, bC7 = false, bC8 = false;
        public static BarItem myBar;
        public static int yMax = 0;

        public barchartcontrol()
        {
            InitializeComponent();

            bLabelsOn = Properties.Settings.Default.bView_Trityl_Labels;

            iRepotOption = Properties.Settings.Default.def_Trityl_Label;

            //uncheck them all
            Menu_NoLabels.Checked = false;
            Menu_Height.Checked = false;
            Menu_Stepwise.Checked = false;
            Menu_Total.Checked = false;

            if (iRepotOption == 0)
                Menu_NoLabels.Checked = true;
            else if (iRepotOption == 1)
                Menu_Height.Checked = true;
            else if (iRepotOption == 2)
                Menu_Stepwise.Checked = true;
            else if (iRepotOption == 3)
                Menu_Total.Checked = true;
        }
        public void CloseMe()
        {
            Debug.WriteLine("I am on the forms thread and running within the form to close itself");
            this.Close();
        }

        private void barchartcontrol_Load(object sender, EventArgs e)
        {
            this.Location = new Point(5, 5);

            {
                GraphPane myPane = zb1.GraphPane;

                string[] labels = new string[225];
                c1.Clear();
                c2.Clear();
                c3.Clear();
                c4.Clear();
                c5.Clear();
                c6.Clear();
                c7.Clear();
                c8.Clear();


                // Create a new graph with topLeft at (40,40) and size 600x400
                zb1.GraphPane.Title.Text = "Trityl Histogram Bar Graph";
                zb1.GraphPane.XAxis.Title.Text = "Cycle";
                zb1.GraphPane.YAxis.Title.Text = "UV Response";

                if (globals.bIsRunning)  //running and have at least one cycle of data
                {
                    globals.bBarCharting = true;                  //once it has been opened once, do not reinitialize 
                                                                  //the arrays, it will be updated in the background.

                    //reset the booleans
                    bC1 = bC2 = bC3 = bC4 = bC5 = bC6 = bC7 = bC8 = false;

                    //open the file and read the data
                    //also check at load time, should always be true
                    if (!File.Exists(globals.UVBarCSVfile))
                    {
                        if (SeNARun.bTerminateEndofStep || SeNARun.bTerminateEndofCycle || !SeNARun.bRunProcessing)
                        {
                            globals.sb.Close();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("The file does not exit yet, try later", "No File");
                            globals.sb.Close();
                            return;
                        }
                    }
                }
                else
                {
                    if (!File.Exists(globals.UVBarCSVfile))
                    {
                        MessageBox.Show("The file does not exit yet, try later", "No File");
                        globals.sb.Close();
                        return;
                    }

                }
                //open and read the file
                string[] readText = new string[220];

                try { readText = File.ReadAllLines(globals.UVBarCSVfile); }
                catch (Exception Joshua) { MessageBox.Show("Could Not Open the file - " + Joshua.ToString(), "File Open Error"); return; }
                cycles = readText.Length;


                //fill the initial arrays
                double[] y1 = new double[cycles];
                double[] y2 = new double[cycles];
                double[] y3 = new double[cycles];
                double[] y4 = new double[cycles];
                double[] y5 = new double[cycles];
                double[] y6 = new double[cycles];
                double[] y7 = new double[cycles];
                double[] y8 = new double[cycles];
                double[] x = new double[cycles];

                int i = 1;
                foreach (string s in readText)
                {

                    string[] pts = s.Split(',');
                    labels[i] = i.ToString("0");
                    if (i == 1)
                    {
                        //if data present then make the 
                        if (Double.Parse(pts[1]) > 0)
                            bC1 = true;
                        if (Double.Parse(pts[2]) > 0)
                            bC2 = true;
                        if (Double.Parse(pts[3]) > 0)
                            bC3 = true;
                        if (Double.Parse(pts[4]) > 0)
                            bC4 = true;
                        if (Double.Parse(pts[5]) > 0)
                            bC5 = true;
                        if (Double.Parse(pts[6]) > 0)
                            bC6 = true;
                        if (Double.Parse(pts[7]) > 0)
                            bC7 = true;
                        if (Double.Parse(pts[8]) > 0)
                            bC8 = true;
                    }

                    if (pts.Length > 8)
                    {

                        c1.Add(x[i - 1] = Convert.ToDouble(pts[0]), y1[i - 1] = Convert.ToDouble(pts[1]));
                        c2.Add(x[i - 1] = Convert.ToDouble(pts[0]), y2[i - 1] = Convert.ToDouble(pts[2]));
                        c3.Add(x[i - 1] = Convert.ToDouble(pts[0]), y3[i - 1] = Convert.ToDouble(pts[3]));
                        c4.Add(x[i - 1] = Convert.ToDouble(pts[0]), y4[i - 1] = Convert.ToDouble(pts[4]));
                        c5.Add(x[i - 1] = Convert.ToDouble(pts[0]), y5[i - 1] = Convert.ToDouble(pts[5]));
                        c6.Add(x[i - 1] = Convert.ToDouble(pts[0]), y6[i - 1] = Convert.ToDouble(pts[6]));
                        c7.Add(x[i - 1] = Convert.ToDouble(pts[0]), y7[i - 1] = Convert.ToDouble(pts[7]));
                        c8.Add(x[i - 1] = Convert.ToDouble(pts[0]), y8[i - 1] = Convert.ToDouble(pts[8]));
                        i++;

                    }

                }

                int tMax = 0;
                //determine yMax for scaling purposes
                for (int j = 0; j < (i - 1); j++)
                {
                    if (y1[j] > tMax)
                        tMax = (int)y1[j];
                    if (y2[j] > tMax)
                        tMax = (int)y2[j];
                    if (y3[j] > tMax)
                        tMax = (int)y3[j];
                    if (y4[j] > tMax)
                        tMax = (int)y4[j];
                    if (y5[j] > tMax)
                        tMax = (int)y5[j];
                    if (y6[j] > tMax)
                        tMax = (int)y6[j];
                    if (y7[j] > tMax)
                        tMax = (int)y7[j];
                    if (y8[j] > tMax)
                        tMax = (int)y8[j];
                }
                tMax = (int)(tMax * 1.15);

                if (yMax == 0)
                    yMax = tMax;
                else if (tMax != (int)(yMax * 1.15))
                    yMax = tMax;


                //MessageBox.Show("Cycles is - "+cycles.ToString("0")+"\ntMax is - "+tMax.ToString("0"));

                labels[cycles + 1] = (cycles + 1).ToString("0");

                // Set the bars and colors
                // Generate a red bar with "Col 1" in the legend
                if (globals.bUV1On || bC1)
                {
                    myBar = myPane.AddBar("Col 1", c1, Color.Red);
                    myBar.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red);
                }
                // Generate a orange bar with "Col 2" in the legend
                if (globals.bUV2On || bC2)
                {
                    myBar = myPane.AddBar("Col 2", c2, Color.Orange);
                    myBar.Bar.Fill = new Fill(Color.Orange, Color.White, Color.Orange);
                }

                // Generate a yellow (gray) bar with "Col 3" in the legend
                if (globals.bUV3On || bC3)
                {
                    myBar = myPane.AddBar("Col 3", c3, Color.YellowGreen);
                    myBar.Bar.Fill = new Fill(Color.YellowGreen, Color.White, Color.YellowGreen);
                }

                // Generate a blue bar with "Col 4" in the legend
                if (globals.bUV4On || bC4)
                {
                    myBar = myPane.AddBar("Col 4", c4, Color.Blue);
                    myBar.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);
                }

                // Generate a green bar with "Col 5" in the legend
                if (globals.bUV5On || bC5)
                {
                    myBar = myPane.AddBar("Col 5", c5, Color.Green);
                    myBar.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green);
                }
                // Generate a indigo bar with "Col 6" in the legend
                if (globals.bUV6On || bC6)
                {
                    myBar = myPane.AddBar("Col 6", c6, Color.Indigo);
                    myBar.Bar.Fill = new Fill(Color.Indigo, Color.White, Color.Indigo);
                }
                // Generate a violet bar with "Col 7" in the legend
                if (globals.bUV7On || bC7)
                {
                    myBar = myPane.AddBar("Col 7", c7, Color.Violet);
                    myBar.Bar.Fill = new Fill(Color.Violet, Color.White, Color.Violet);
                }
                // Generate a blueshade bar with "Col 8" in the legend
                if (globals.bUV8On || bC8)
                {
                    myBar = myPane.AddBar("Col 8", c8, Color.BlueViolet);
                    myBar.Bar.Fill = new Fill(Color.BlueViolet, Color.White, Color.BlueViolet);
                }


                // Draw the X tics between the labels instead of at the labels
                myPane.XAxis.MajorTic.IsBetweenLabels = true;

                // Set the XAxis to an ordinal type
                myPane.XAxis.Type = AxisType.Ordinal;
                // draw the X axis zero line
                myPane.XAxis.MajorGrid.IsZeroLine = false;

                //This is the part that makes the bars horizontal
                myPane.BarSettings.Base = BarBase.X;


                //set the background color
                // Fill the axis area with a gradient
                myPane.Chart.Fill = new Fill(Color.White,
                    Color.FromArgb(255, 255, 166), 90F);

                // Fill the pane area with a solid color
                myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

                //add resize event handler
                zb1.Resize += new EventHandler(barchart_Resize);

                if (iRepotOption > 0)
                    CreateBarLabels(myPane);

                //set initial x axis
                globals.bZoomed = false;
                myPane.XAxis.Scale.Min = 0;
                myPane.XAxis.Scale.Max = 8;

                if (!globals.bZoomed)

                {
                    if (cycles < 8)
                    {
                        myPane.XAxis.Scale.Min = 0;
                        myPane.XAxis.Scale.Max = 8.5;
                    }
                    else
                    {
                        myPane.XAxis.Scale.Min = cycles - 8.5;
                        myPane.XAxis.Scale.Max = cycles + 0.5;

                    }

                }

                myPane.YAxis.Scale.Min = 0;
                myPane.YAxis.Scale.Max = yMax;

                zb1.IsShowHScrollBar = true;
                zb1.IsAutoScrollRange = true;


                myPane.YAxis.Scale.MaxAuto = true;


                // Draw the X tics between the labels instead of at the labels
                //   myPane.XAxis.MajorTic.IsBetweenLabels = true;

                // Enable scrollbars if needed
                zb1.IsShowHScrollBar = true;
                zb1.IsShowVScrollBar = true;
                zb1.IsAutoScrollRange = true;

                // OPTIONAL: Show tooltips when the mouse hovers over a point
                zb1.IsShowPointValues = true;
                zb1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

                // OPTIONAL: Add a custom context menu item
                zb1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(
                                MyContextMenuBuilder);

                // OPTIONAL: Handle the Zoom Event
                zb1.ZoomEvent += new ZedGraphControl.ZoomEventHandler(MyZoomEvent);

                // Set the XAxis labels
                //myPane.XAxis.Scale.Format = "0";
                myPane.XAxis.Scale.TextLabels = labels;
                // Set the XAxis to Text type
                //myPane.XAxis.Type = AxisType.Text;


                // Size the control to fit the window
                SetSize();

                barchart_Resize(myPane, null);

                zb1.AxisChange();
                zb1.Invalidate();

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] readText = new string[220];

            // first check if running
            if (globals.bIsRunning)
            {
                MessageBox.Show("Review Not Allowed While Running", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            //if the port is closed or not streaming anymore just return
            /*string test = string.Empty;
            if (Main_Form.UV_Trityl_Arduino.IsOpen)
            {
                test = Main_Form.UV_Trityl_Arduino.ReadExisting();
                int cntr = 0;
                while (cntr < 7 && test.Length < 5)
                {
                    test = Main_Form.UV_Trityl_Arduino.ReadExisting();
                    Man_Controlcs.SyncWait(100);
                    if (cntr > 6)
                    {
                        Man_Controlcs.WriteStatus("Trityl Monitor", "Not Streaming");
                        return;
                    }
                    cntr++;
                }
            }
            else
            {
                Man_Controlcs.WriteStatus("Trityl Monitor", "Not Gone Offline");
                return;
            }*/


            //next open file
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Properties.Settings.Default.CSV_Path;
            openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = "UV_BAR*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Select a chart file";

            string fname = String.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = openFileDialog1.FileName;

                //make sure it is a Bar Chart and not strip chart
                if (!(fname.Contains("BAR")))
                {
                    MessageBox.Show("Must Be a Bar chart", "Bar only", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    return;
                }

                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            //open and read the file

                            try { readText = File.ReadAllLines(fname); }
                            catch (Exception Joshua) { MessageBox.Show("Could Not Open the file - " + Joshua.ToString(), "File Open Error"); return; }
                            cycles = readText.Length;

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            //clear the graph objects
            zb1.GraphPane.CurveList.Clear();
            zb1.GraphPane.GraphObjList.Clear();
            zb1.GraphPane = new GraphPane();




            //now clear the chart and load the data
            GraphPane myReviewPane = zb1.GraphPane;

            int k = 0;
            k = myReviewPane.CurveList.IndexOf("Col 1");
            if (k > 0) { myReviewPane.CurveList.RemoveAt(k); }
            k = myReviewPane.CurveList.IndexOf("Col 2");
            if (k > 0) { myReviewPane.CurveList.RemoveAt(k); }
            k = myReviewPane.CurveList.IndexOf("Col 3");
            if (k > 0) { myReviewPane.CurveList.RemoveAt(k); }
            k = myReviewPane.CurveList.IndexOf("Col 4");
            if (k > 0) { myReviewPane.CurveList.RemoveAt(k); }
            k = myReviewPane.CurveList.IndexOf("Col 5");
            if (k > 0) { myReviewPane.CurveList.RemoveAt(k); }
            k = myReviewPane.CurveList.IndexOf("Col 6");
            if (k > 0) { myReviewPane.CurveList.RemoveAt(k); }
            k = myReviewPane.CurveList.IndexOf("Col 7");
            if (k > 0) { myReviewPane.CurveList.RemoveAt(k); }
            k = myReviewPane.CurveList.IndexOf("Col 8");
            if (k > 0) { myReviewPane.CurveList.RemoveAt(k); }


            //myReviewPane.Legend.Em = false;
            //clear the point pair lists
            string[] labels = new string[225];
            c1.Clear();
            c2.Clear();
            c3.Clear();
            c4.Clear();
            c5.Clear();
            c6.Clear();
            c7.Clear();
            c8.Clear();



            // Create a new graph with topLeft at (40,40) and size 600x400
            zb1.GraphPane.Title.Text = "Trityl Histogram Bar Graph";
            zb1.GraphPane.XAxis.Title.Text = "Cycle";
            zb1.GraphPane.YAxis.Title.Text = "UV Response";

            //fill the initial arrays
            double[] y1 = new double[cycles];
            double[] y2 = new double[cycles];
            double[] y3 = new double[cycles];
            double[] y4 = new double[cycles];
            double[] y5 = new double[cycles];
            double[] y6 = new double[cycles];
            double[] y7 = new double[cycles];
            double[] y8 = new double[cycles];
            double[] x = new double[cycles];

            int i = 1;
            if (readText.Length < 1)
            { MessageBox.Show(fname + "has no data"); return; }

            foreach (string s in readText)
            {
                //MessageBox.Show(s, "i - "+readText.Length.ToString());

                string[] pts = s.Split(',');
                labels[i] = i.ToString("0");
                if (i == 1)
                {
                    //if data present then make the 
                    if (Double.Parse(pts[1]) > 0)
                        bC1 = true;
                    if (Double.Parse(pts[2]) > 0)
                        bC2 = true;
                    if (Double.Parse(pts[3]) > 0)
                        bC3 = true;
                    if (Double.Parse(pts[4]) > 0)
                        bC4 = true;
                    if (Double.Parse(pts[5]) > 0)
                        bC5 = true;
                    if (Double.Parse(pts[6]) > 0)
                        bC6 = true;
                    if (Double.Parse(pts[7]) > 0)
                        bC7 = true;
                    if (Double.Parse(pts[8]) > 0)
                        bC8 = true;
                }

                if (pts.Length > 8)
                {

                    if (bC1)
                        c1.Add(x[i - 1] = Convert.ToDouble(pts[0]), y1[i - 1] = Convert.ToDouble(pts[1]));
                    if (bC2)
                        c2.Add(x[i - 1] = Convert.ToDouble(pts[0]), y2[i - 1] = Convert.ToDouble(pts[2]));
                    if (bC3)
                        c3.Add(x[i - 1] = Convert.ToDouble(pts[0]), y3[i - 1] = Convert.ToDouble(pts[3]));
                    if (bC4)
                        c4.Add(x[i - 1] = Convert.ToDouble(pts[0]), y4[i - 1] = Convert.ToDouble(pts[4]));
                    if (bC5)
                        c5.Add(x[i - 1] = Convert.ToDouble(pts[0]), y5[i - 1] = Convert.ToDouble(pts[5]));
                    if (bC6)
                        c6.Add(x[i - 1] = Convert.ToDouble(pts[0]), y6[i - 1] = Convert.ToDouble(pts[6]));
                    if (bC7)
                        c7.Add(x[i - 1] = Convert.ToDouble(pts[0]), y7[i - 1] = Convert.ToDouble(pts[7]));
                    if (bC8)
                        c8.Add(x[i - 1] = Convert.ToDouble(pts[0]), y8[i - 1] = Convert.ToDouble(pts[8]));

                    i++;

                }

            }

            int tMax = 0;
            //determine yMax for scaling purposes
            for (int j = 0; j < (i - 1); j++)
            {
                if (y1[j] > tMax)
                    tMax = (int)y1[j];
                if (y2[j] > tMax)
                    tMax = (int)y2[j];
                if (y3[j] > tMax)
                    tMax = (int)y3[j];
                if (y4[j] > tMax)
                    tMax = (int)y4[j];
                if (y5[j] > tMax)
                    tMax = (int)y5[j];
                if (y6[j] > tMax)
                    tMax = (int)y6[j];
                if (y7[j] > tMax)
                    tMax = (int)y7[j];
                if (y8[j] > tMax)
                    tMax = (int)y8[j];
            }
            tMax = (int)(tMax * 1.15);

            if (yMax == 0)
                yMax = tMax;
            else if (tMax != (int)(yMax * 1.15))
                yMax = tMax;


            //MessageBox.Show("yMax is - "+yMax.ToString("0")+"\ntMax is - "+tMax.ToString("0"));

            labels[cycles + 1] = (cycles + 1).ToString("0");

            // Set the bars and colors
            // Generate a red bar with "Col 1" in the legend/*
            if (bC1)
            {
                myBar = myReviewPane.AddBar("Col 1", c1, Color.Red);
                myBar.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red);
            }
            // Generate a orange bar with "Col 2" in the legend
            if (bC2)
            {
                myBar = myReviewPane.AddBar("Col 2", c2, Color.Orange);
                myBar.Bar.Fill = new Fill(Color.Orange, Color.White, Color.Orange);
            }
            // Generate a yellow (gray) bar with "Col 3" in the legend
            if (bC3)
            {
                myBar = myReviewPane.AddBar("Col 3", c3, Color.YellowGreen);
                myBar.Bar.Fill = new Fill(Color.YellowGreen, Color.White, Color.YellowGreen);
            }
            // Generate a blue bar with "Col 4" in the legend
            if (bC4)
            {
                myBar = myReviewPane.AddBar("Col 4", c4, Color.Blue);
                myBar.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);
            }
            // Generate a green bar with "Col 5" in the legend
            if (bC5)
            {
                myBar = myReviewPane.AddBar("Col 5", c5, Color.Green);
                myBar.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green);
            }
            // Generate a indigo bar with "Col 6" in the legend
            if (bC6)
            {
                myBar = myReviewPane.AddBar("Col 6", c6, Color.Indigo);
                myBar.Bar.Fill = new Fill(Color.Indigo, Color.White, Color.Indigo);
            }
            // Generate a violet bar with "Col 7" in the legend
            if (bC7)
            {
                myBar = myReviewPane.AddBar("Col 7", c7, Color.Violet);
                myBar.Bar.Fill = new Fill(Color.Violet, Color.White, Color.Violet);
            }
            // Generate a blueshade bar with "Col 8" in the legend
            if (bC8)
            {
                myBar = myReviewPane.AddBar("Col 8", c8, Color.BlueViolet);
                myBar.Bar.Fill = new Fill(Color.BlueViolet, Color.White, Color.BlueViolet);
            }



            // Draw the X tics between the labels instead of at the labels
            myReviewPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Set the XAxis to an ordinal type
            myReviewPane.XAxis.Type = AxisType.Ordinal;
            // draw the X axis zero line
            myReviewPane.XAxis.MajorGrid.IsZeroLine = false;

            //This is the part that makes the bars horizontal
            myReviewPane.BarSettings.Base = BarBase.X;


            //set the background color
            // Fill the axis area with a gradient
            myReviewPane.Chart.Fill = new Fill(Color.White,
                Color.FromArgb(255, 255, 166), 90F);

            // Fill the pane area with a solid color
            myReviewPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            //add resize event handler
            zb1.Resize += new EventHandler(barchart_Resize);

            //set initial x axis
            globals.bZoomed = false;
            myReviewPane.XAxis.Scale.Min = 0;
            myReviewPane.XAxis.Scale.Max = 8;

            //label the bars
            if (iRepotOption > 0)
                CreateBarLabels(myReviewPane);

            if (!globals.bZoomed)

            {
                if (cycles < 8)
                {
                    myReviewPane.XAxis.Scale.Min = 0;
                    myReviewPane.XAxis.Scale.Max = 8.5;
                }
                else
                {
                    myReviewPane.XAxis.Scale.Min = cycles - 8.5;
                    myReviewPane.XAxis.Scale.Max = cycles + 0.5;

                }

            }

            myReviewPane.YAxis.Scale.Min = 0;
            myReviewPane.YAxis.Scale.Max = yMax;

            zb1.IsShowHScrollBar = true;
            zb1.IsAutoScrollRange = true;


            myReviewPane.YAxis.Scale.MaxAuto = true;


            // Draw the X tics between the labels instead of at the labels
            //   myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Enable scrollbars if needed
            zb1.IsShowHScrollBar = true;
            zb1.IsShowVScrollBar = true;
            zb1.IsAutoScrollRange = true;

            // OPTIONAL: Show tooltips when the mouse hovers over a point
            zb1.IsShowPointValues = true;
            zb1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);

            // OPTIONAL: Add a custom context menu item
            zb1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(
                            MyContextMenuBuilder);

            // OPTIONAL: Handle the Zoom Event
            zb1.ZoomEvent += new ZedGraphControl.ZoomEventHandler(MyZoomEvent);

            // Set the XAxis labels
            //myPane.XAxis.Scale.Format = "0";
            myReviewPane.XAxis.Scale.TextLabels = labels;
            // Set the XAxis to Text type
            //myPane.XAxis.Type = AxisType.Text;


            // Size the control to fit the window
            SetSize();

            barchart_Resize(myReviewPane, null);

            zb1.AxisChange();
            zb1.Invalidate();
        }

        private void MyContextMenuBuilder(ZedGraphControl control, ContextMenuStrip menuStrip,
                            Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            //ToolStripMenuItem item = new ToolStripMenuItem();
            //item.Name = "Change Labels";
            //item.Tag = "add-Tick-Mark";
            //item.Text = "Add a Tick Mark to Chart";
            //item.Click += new System.EventHandler(AddColumnTickMark);

            //menuStrip.Items.Add(item);
        }
        private void MyZoomEvent(ZedGraphControl control, ZoomState oldState,
                    ZoomState newState)
        {
            globals.bZoomed = true;

            // Here we get notification everytime the user zooms
            // only for unzoom and set to default size
            if (oldState != null && newState != null)
            {

                globals.bZoomed = false;
                //MessageBox.Show("I am here - new state" + newState.ToString() + "\nold state" + oldState.ToString());
            }
        }


        private string MyPointValueHandler(ZedGraphControl control, GraphPane pane,
                       CurveItem curve, int iPt)
        {
            // Get the PointPair that is under the mouse
            PointPair pt = curve[iPt];
            // For some reason when cycle > 1 it shows cycle - 1
            int temp = 1;
            if (globals.iCycle > 1)
                temp = Convert.ToInt16(pt.X) + 1;

            return curve.Label.Text + " Trityl Response is " + pt.Y.ToString("f0") + " AU for cycle " + temp.ToString("0");
        }
        private void barchart_Resize(object sender, EventArgs e)
        {
            SetSize();
        }
        private void frmGraph_VisibleChanged(object sender, EventArgs e)
        {
            zb1.GraphPane.XAxis.Scale.Max = cycles;
            zb1.RestoreScale(zb1.GraphPane);
        }
        private void mypane_Resize(object sender, EventArgs e)
        {
            SetSize();

        }
        private void SetSize()
        {

            zb1.Location = new Point(8, 22);
            // Leave a small margin around the outside of the control

            zb1.Size = new Size(this.ClientRectangle.Width - 10,
                    this.ClientRectangle.Height - 20);
        }
        private void CreateBarLabels(GraphPane mylocalPane)
        {
            ValueHandler valueHandler = null;

            //set scales
            //settings
            try { mylocalPane.XAxis.Scale.Min = 0; } catch (Exception e) { MessageBox.Show("Error Setting Scales " + e.ToString()); }

            //height of label shift
            float shift = 55;
            double dmax = 0.0;

            int ord = 0;
            int cl = mylocalPane.CurveList.Count;

            CurveItem firstcurve = mylocalPane.CurveList[0];
            if (cl == 0 || firstcurve == null)
            {
                Man_Controlcs.WriteStatus("Bar Chart", "No Curves or Curve Items in list");
                return;
            }
            //clear all previous labels
            mylocalPane.GraphObjList.Clear();

            // The ValueHandler is a helper that does some position calculations for us.
            try { valueHandler = new ValueHandler(mylocalPane, true); } catch { valueHandler = new ValueHandler(mylocalPane, false); }
            foreach (CurveItem curve in mylocalPane.CurveList)
            {
                BarItem bar = curve as BarItem;

                if (bar != null)
                {
                    IPointList points = curve.Points;

                    for (int i = 0; i < points.Count; i++)
                    {
                        double yVal = 0.0;
                        double dVal = points[i].Y;
                        double dStepYield = 0.0;
                        double dTotalYield = 0.0;

                        //MessageBox.Show("iCycle" + globals.iCycle.ToString());
                        if (globals.bIsRunning && i > globals.iCycle)
                        {
                            break;
                        }

                        if (points[i].Y > dmax)
                            dmax = points[i].Y;

                        shift = (float)(dmax * 0.1);

                        //calculate stepwise yield
                        if (i > 0)
                        {
                            if (points[i].Y > 0)  //the detector can report 0, let's catch it
                                dStepYield = (1 - ((points[i - 1].Y - points[i].Y) / points[i].Y)) * 100;
                        }
                        else
                            dStepYield = points[i].Y;

                        //calculate total yield
                        if (i > 0)
                        {
                            double sum = 0;
                            for (int j = 0; j < points.Count; j++)
                                sum = sum + points[j].Y;

                            double avg = sum / points.Count;

                            if (points[i].Y > 0) //the detector can report 0, let's catch it
                                dTotalYield = (1 - ((points[0].Y - points[i].Y) / points[0].Y)) * 100;
                            else
                                dTotalYield = 0;
                        }

                        else
                            dTotalYield = points[i].Y;

                        //now decide what to report
                        if (iRepotOption == 1)
                            yVal = points[i].Y;
                        else if (iRepotOption == 2)
                            yVal = dStepYield;
                        else if (iRepotOption == 3)
                            yVal = dTotalYield;

                        //store in array for end of run reporting
                        if (i > 0 && iRepotOption == 1)
                        {
                            iReportArray[0, i] = (double)i;
                            iReportArray[1, i] = dVal;
                            iReportArray[2, i] = dStepYield;
                            iReportArray[3, i] = dTotalYield;
                        }
                        else
                        {
                            iReportArray[0, i] = (double)i;
                            iReportArray[1, i] = dVal;
                            iReportArray[2, i] = 100.0;
                            iReportArray[3, i] = 100.0;
                        }

                        //check trityl alarm
                        if (globals.bTritylAlarOn)
                        {
                            if (i > 0 && iRepotOption > 1)
                            {
                                if (iRepotOption == 2) //Stepwise Yield
                                {
                                    if (dStepYield < (double)globals.iTritylAlarmAmt)
                                        globals.bTritylAlarOn = true;
                                    else
                                        globals.bTritylAlarOn = false;
                                }
                                if (iRepotOption == 3 || iRepotOption == 1 || iRepotOption == 0) //no labels, height or Total Yield
                                {
                                    if (dTotalYield < (double)globals.iTritylAlarmAmt)
                                        globals.bTritylAlarOn = true;
                                    else
                                        globals.bTritylAlarOn = false;

                                }
                            }
                        }
                        // Calculate the Y value at the center of each bar
                        //MessageBox.Show("Here + cycle" + i.ToString("0")+ "  yVal" + yVal.ToString());
                        double xVal = valueHandler.BarCenterValue(curve, curve.GetBarWidth(mylocalPane),
                                  i, points[i].X, ord);

                        // format the label string to have 1 decimal place
                        string lab = "";

                        if (i > 0 && iRepotOption > 1)
                        {
                            if (yVal > 0)
                                lab = yVal.ToString("F1") + "%";
                            else
                                lab = "";
                        }
                        else
                            lab = yVal.ToString("F0");

                        // create the text item (assumes the x axis is ordinal or text)
                        // for negative bars, the label appears just above the zero value
                        TextObj text = new TextObj();
                        if (bLabelsOn)
                        {

                            //   MessageBox.Show(yVal.ToString());
                            text = new TextObj(lab, (float)xVal,
                                                   (float)dVal + (dVal > 0 ? shift : -shift));
                        }
                        else
                        {
                            mylocalPane.GraphObjList.Clear();
                        }
                        // tell Zedgraph to use user scale units for locating the TextObj
                        text.Location.CoordinateFrame = CoordType.AxisXYScale;
                        text.FontSpec.Size = 10;
                        // AlignH the left-center of the text to the specified point
                        text.Location.AlignH = xVal > 0 ? AlignH.Center : AlignH.Right;
                        text.Location.AlignV = AlignV.Top;
                        text.FontSpec.Border.IsVisible = false;

                        // rotate the text 90 degrees
                        text.FontSpec.Angle = 0;
                        text.FontSpec.Fill.IsVisible = false;
                        // add the TextObj to the list
                        mylocalPane.GraphObjList.Add(text);
                    }
                }


                ord++;
            }


        }
        public delegate void SetControlCallback(ZedGraph.ZedGraphControl c);
        public static void UpdateUVTrityBar(double[] inArray)
        {
            DateTime dt = DateTime.Now;

            if (globals.sb == null)
                globals.sb = new barchartcontrol();

            if (globals.sb.zb1 == null)
                globals.sb.zb1 = new ZedGraphControl();

            if (globals.sb.zb1.GraphPane == null)
                globals.sb.zb1.GraphPane = new GraphPane();

            //determine why this sometimes says "Object reference not set to an instance of an object" System.Null exception
            if (globals.sb.zb1.GraphPane == null)
                return;

            // Get a reference to the "Curve List" curve IPointListEdit
            try
            {
                if (globals.sb == null)
                    return;

                // Get a reference to the GraphPane instance in the ZedGraphControl
                GraphPane mylocalPane = globals.sb.zb1.GraphPane;

                //make sure we got the data
                if (myBar != null)
                {
                    cycles = cycles + 1;

                    //now update the data
                    //to get the data 
                    double x = (double)globals.iCycle;


                    double y1 = inArray[0];
                    double y2 = inArray[1];
                    double y3 = inArray[2];
                    double y4 = inArray[3];
                    double y5 = inArray[4];
                    double y6 = inArray[5];
                    double y7 = inArray[6];
                    double y8 = inArray[7];

                    //add to chart
                    if (SeNARun.runSequences[1].Length > 0)
                    {
                        if (globals.bUV1On) { c1.Add(x, y1); }
                        else { c1.Add(x, 0.0); };
                    }
                    if (SeNARun.runSequences[2].Length > 0)
                    {
                        if (globals.bUV2On) { c2.Add(x, y2); }
                        else { c2.Add(x, 0.0); };
                    }
                    if (SeNARun.runSequences[3].Length > 0)
                    {
                        if (globals.bUV3On) { c3.Add(x, y3); }
                        else { c3.Add(x, 0.0); };
                    }
                    if (SeNARun.runSequences[4].Length > 0)
                    {
                        if (globals.bUV4On) { c4.Add(x, y4); }
                        else { c4.Add(x, 0.0); };
                    }
                    if (SeNARun.runSequences[5].Length > 0)
                    {
                        if (globals.bUV5On) { c5.Add(x, y5); }
                        else { c5.Add(x, 0.0); };
                    }
                    if (SeNARun.runSequences[6].Length > 0)
                    {
                        if (globals.bUV6On) { c6.Add(x, y6); }
                        else { c6.Add(x, 0.0); };
                    }
                    if (SeNARun.runSequences[7].Length > 0)
                    {
                        if (globals.bUV7On) { c1.Add(x, y7); }
                        else { c7.Add(x, 0.0); };
                    }
                    if (SeNARun.runSequences[8].Length > 0)
                    {
                        if (globals.bUV8On) { c1.Add(x, y8); }
                        else { c8.Add(x, 0.0); };
                    }

                    //update the axis
                    if (y1 > yMax)
                        yMax = (int)y1;
                    if (y2 > yMax)
                        yMax = (int)y2;
                    if (y3 > yMax)
                        yMax = (int)y3;
                    if (y4 > yMax)
                        yMax = (int)y4;
                    if (y5 > yMax)
                        yMax = (int)y5;
                    if (y6 > yMax)
                        yMax = (int)y6;
                    if (y7 > yMax)
                        yMax = (int)y7;
                    if (y8 > yMax)
                        yMax = (int)y8;
                    //set initial
                    yMax = (int)(yMax * 1.15);

                    // Set the XAxis to an ordinal type
                    mylocalPane.XAxis.Type = AxisType.Ordinal;
                    // draw the X axis zero line
                    mylocalPane.XAxis.MajorGrid.IsZeroLine = false;

                    //This is the part that makes the bars horizontal
                    mylocalPane.BarSettings.Base = BarBase.X;

                    mylocalPane.XAxis.MajorTic.IsBetweenLabels = true;


                    //set the background color
                    // Fill the axis area with a gradient
                    mylocalPane.Chart.Fill = new Fill(Color.White,
                        Color.FromArgb(255, 255, 166), 90F);

                    // Fill the pane area with a solid color
                    mylocalPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

                    //add scroll bars if larger than 7 
                    mylocalPane.YAxis.Scale.Min = 0;
                    mylocalPane.YAxis.Scale.Max = yMax;

                    mylocalPane.YAxis.Scale.Min = 0;
                    mylocalPane.YAxis.Scale.Max = yMax;

                    //mylocalPane.XAxis.Scale.MaxAuto = true;
                    mylocalPane.YAxis.Scale.MaxAuto = true;

                    //update the labels
                    globals.sb.CreateBarLabels(mylocalPane);

                    //update the title to reflect the date of the last coupling
                    mylocalPane.Title.Text = "Trityl Histogram Bar Graph " + dt.ToString("MMMM dd, yyyy h:mm tt", CultureInfo.CreateSpecificCulture("en-US"));
                    if (!globals.bZoomed)

                    {
                        if (cycles < 8)
                        {
                            mylocalPane.XAxis.Scale.Min = 0;
                            mylocalPane.XAxis.Scale.Max = 8.5;
                        }
                        else
                        {
                            mylocalPane.XAxis.Scale.Min = cycles - 8.5;
                            mylocalPane.XAxis.Scale.Max = cycles + 0.5;

                        }

                    }


                    //set up scroll bars
                    if (globals.iCycle > 6)
                        globals.sb.zb1.IsShowHScrollBar = true;
                    else
                        globals.sb.zb1.IsShowHScrollBar = false;

                    globals.sb.zb1.IsAutoScrollRange = true;

                    mylocalPane.YAxis.Scale.MaxAuto = true;


                    // Draw the X tics between the labels instead of at the labels
                    //   myPane.XAxis.MajorTic.IsBetweenLabels = true;

                    // Enable scrollbars if needed
                    globals.sb.zb1.IsShowHScrollBar = true;
                    globals.sb.zb1.IsShowVScrollBar = true;
                    globals.sb.zb1.IsAutoScrollRange = true;

                    //add resize event handler
                    globals.sb.SetSize();


                    //refresh the chart if not zoomed
                    ZedGraph.ZedGraphControl c = globals.sb.zb1;
                    if (c != null && c.InvokeRequired)
                    {
                        try
                        {
                            //MessageBox.Show("In Callback" + text, c.ToString());
                            c.Invoke(new MethodInvoker(delegate
                            {
                                c.AxisChange();
                                c.Invalidate();

                                if (!globals.bZoomed) { c.Refresh(); }


                            }));
                        }
                        catch (Exception e) //(System.Reflection.TargetInvocationException tie)
                        {
                            Man_Controlcs.WriteStatus("Bar Graph", "Invoke Error -" + e.ToString());
                        }
                    }
                    else
                    {
                        if (c != null)
                        {
                            try { c.AxisChange(); c.Invalidate(); if (!globals.bZoomed) { c.Refresh(); } }
                            catch (Exception e) { Man_Controlcs.WriteStatus("Bar Graph", "Could not update the chart - " + e.ToString()); }
                        }
                    }
                }

            }

            catch (Exception x) { Man_Controlcs.WriteStatus("Bar Graph", "Error Getting Curve List -" + x.ToString()); }


        }
        public static void SavePict()
        {
            DateTime d = DateTime.Now;
            GraphPane savePane = globals.sb.zb1.GraphPane;

            savePane.XAxis.Scale.Min = 0;
            savePane.XAxis.Scale.Max = globals.iCycle + 1;

            string path = globals.CSV_path + "trit_" + d.ToString("ddMMyy") + "__" + d.ToString("h_mm") + ".png";

            Bitmap bm = savePane.GetImage();

            bm.Save(path);
        }
        private void Menu_axisLabels_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Exit_Click(object sender, EventArgs e)
        {
            SeNARun.bMonitoringUV = false;
            this.Close();
        }

        private void Menu_NoLabels_Click(object sender, EventArgs e)
        {
            //Uncheck the others
            if (Menu_Stepwise.Checked)
                Menu_Stepwise.Checked = false;
            if (Menu_Height.Checked)
                Menu_Height.Checked = false;
            if (Menu_Total.Checked)
                Menu_Total.Checked = false;

            //check this one
            Menu_NoLabels.Checked = true;

            //set report option
            iRepotOption = 0;
            bLabelsOn = false;
            Properties.Settings.Default.def_Trityl_Label = 0;
            Properties.Settings.Default.Save();

            zb1.GraphPane.GraphObjList.Clear();
            GraphPane mylocalPane = globals.sb.zb1.GraphPane;

            // Set the XAxis to an ordinal type
            mylocalPane.XAxis.Type = AxisType.Ordinal;
            // draw the X axis zero line
            mylocalPane.XAxis.MajorGrid.IsZeroLine = false;

            //This is the part that makes the bars horizontal
            mylocalPane.BarSettings.Base = BarBase.X;

            mylocalPane.XAxis.MajorTic.IsBetweenLabels = true;

            mylocalPane.BarSettings.ClusterScaleWidth = 8f;

            //set the background color
            // Fill the axis area with a gradient
            mylocalPane.Chart.Fill = new Fill(Color.White,
                Color.FromArgb(255, 255, 166), 90F);

            // Fill the pane area with a solid color
            mylocalPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            //add scroll bars if larger than 7 
            mylocalPane.YAxis.Scale.Min = 0;
            mylocalPane.YAxis.Scale.Max = yMax;


            //set initial
            mylocalPane.XAxis.Scale.Min = 0;
            mylocalPane.XAxis.Scale.Max = cycles + 1;

            mylocalPane.YAxis.Scale.Min = -1;
            mylocalPane.YAxis.Scale.Max = cycles + 1;

            //mylocalPane.XAxis.Scale.MaxAuto = true;
            mylocalPane.YAxis.Scale.MaxAuto = true;

            //set up scroll bars
            if (globals.iCycle > 7)
                globals.sb.zb1.IsShowHScrollBar = true;
            else
                globals.sb.zb1.IsShowHScrollBar = false;

            globals.sb.zb1.IsAutoScrollRange = true;


            //mylocalPane.XAxis.Scale.MaxAuto = true;
            mylocalPane.YAxis.Scale.MaxAuto = true;


            // Draw the X tics between the labels instead of at the labels
            //   myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Enable scrollbars if needed
            globals.sb.zb1.IsShowHScrollBar = true;
            globals.sb.zb1.IsShowVScrollBar = true;
            globals.sb.zb1.IsAutoScrollRange = true;

            //add resize event handler
            globals.sb.SetSize();


            zb1.AxisChange();
            zb1.Refresh();

        }
        private void Menu_Height_Click(object sender, EventArgs e)
        {
            if (Menu_Stepwise.Checked)
                Menu_Stepwise.Checked = false;
            if (Menu_NoLabels.Checked)
                Menu_NoLabels.Checked = false;
            if (Menu_Total.Checked)
                Menu_Total.Checked = false;

            //check this one
            Menu_Height.Checked = true;
            bLabelsOn = true;

            //set report option
            iRepotOption = 1;

            //save in app config file
            Properties.Settings.Default.def_Trityl_Label = 1;
            Properties.Settings.Default.Save();

            zb1.GraphPane.GraphObjList.Clear();
            CreateBarLabels(zb1.GraphPane);

            //zb1.GraphPane.XAxis.Scale.Min = cycles - 7;
            //zb1.GraphPane.XAxis.Scale.Max = cycles + 1;

            zb1.AxisChange();
            zb1.Refresh();


        }

        private void Menu_Stepwise_Click_1(object sender, EventArgs e)
        {
            if (Menu_NoLabels.Checked)
                Menu_NoLabels.Checked = false;
            if (Menu_Height.Checked)
                Menu_Height.Checked = false;
            if (Menu_Total.Checked)
                Menu_Total.Checked = false;

            //check this one
            Menu_Stepwise.Checked = true;

            //set report option
            iRepotOption = 2;

            //save in app config file
            Properties.Settings.Default.def_Trityl_Label = 2;
            Properties.Settings.Default.Save();
            bLabelsOn = true;


            zb1.GraphPane.GraphObjList.Clear();
            CreateBarLabels(zb1.GraphPane);

            //zb1.GraphPane.XAxis.Scale.Min = cycles - 7;
            //zb1.GraphPane.XAxis.Scale.Max = cycles + 1;

            zb1.AxisChange();
            zb1.Refresh();

        }
        private void Menu_Total_Click(object sender, EventArgs e)
        {
            if (Menu_Stepwise.Checked)
                Menu_Stepwise.Checked = false;
            if (Menu_Height.Checked)
                Menu_Height.Checked = false;
            if (Menu_NoLabels.Checked)
                Menu_NoLabels.Checked = false;

            //check this one
            Menu_Total.Checked = true;

            //set report option
            iRepotOption = 3;
            bLabelsOn = true;

            //save in app config file
            Properties.Settings.Default.def_Trityl_Label = 3;
            Properties.Settings.Default.Save();

            zb1.GraphPane.GraphObjList.Clear();
            CreateBarLabels(zb1.GraphPane);
            //zb1.GraphPane.XAxis.Scale.Min = cycles - 7;
            //zb1.GraphPane.XAxis.Scale.Max = cycles + 1;

            zb1.AxisChange();
            zb1.Refresh();

        }

        private void barchartcontrol_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void barchartcontrol_FormClosing(object sender, FormClosingEventArgs e)
        {
            SeNARun.bMonitoringUV = false;
            //we just hide the bar chart instead of close it, it will 
            //continually be updated, but until the user clicks open again
            // he will not see the updates.  By keeping bBarcharting true
            // we will not have to update from the CSV file.
            //Properties.Settings.Default.UV_Report = iRepotOption;
            //Properties.Settings.Default.Save();
            //MessageBox.Show("Here");
            zb1.GraphPane.GraphObjList.Clear();
            //this.Close();

            /*            if (e.CloseReason == CloseReason.UserClosing)
                        {
                            Properties.Settings.Default.UV_Report = iRepotOption;
                            Properties.Settings.Default.Save();
                            e.Cancel = false;
                            // if we just hide it it won't go through form load
                            // that will keep the chart from reloading
                            zb1.GraphPane.GraphObjList.Clear();
                            this.Hide();  
                        }*/

        }
    }
}
