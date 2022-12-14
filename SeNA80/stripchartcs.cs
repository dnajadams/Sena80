using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;


namespace SeNA80
{
    public partial class stripchartcs : Form
    {
        public bool bSymbolsHidden = false;

        public stripchartcs()
        {

            InitializeComponent();
            


        }
        private void stripchartcs_Load(object sender, EventArgs e)
        {
            // Get a reference to the GraphPane instance in the ZedGraphControl
            GraphPane myPane = zg1.GraphPane;

            
            // Set the titles and axis labels
            myPane.Title.Text = "Trityl Monitor Data";
            myPane.XAxis.Title.Text = "Time";
            myPane.YAxis.Title.Text = "UV Response";
            myPane.XAxis.Scale.IsPreventLabelOverlap = true;



            // Initialize the Point pair arrays (the x-y arrays to store the data)
            PointPairList monitor_1 = new PointPairList();
            PointPairList monitor_2 = new PointPairList();
            PointPairList monitor_3 = new PointPairList();
            PointPairList monitor_4 = new PointPairList();
            PointPairList monitor_5 = new PointPairList();
            PointPairList monitor_6 = new PointPairList();
            PointPairList monitor_7 = new PointPairList();
            PointPairList monitor_8 = new PointPairList();


            int maxpts = globals.maxtritylpts;
            XDate[] timearray = new XDate[maxpts];
            int i = 0;

            //get the existing data from the FIFO queues where the data is being stored
            foreach (DateTime date in globals.TritylDate)
            {
                XDate x = new XDate(date);
                timearray[i] = x;
                i++;
            }

            int j = 0;
            foreach (string response in globals.UVTrityResponse)
            {
               // MessageBox.Show(response, "Cycle " + j.ToString());
                string[] values = response.Split(',');
                double y = Double.Parse(values[0]);
                double y2 = Double.Parse(values[1]);
                double y3 = Double.Parse(values[2]);
                double y4 = Double.Parse(values[3]);
                double y5 = Double.Parse(values[4]);
                double y6 = Double.Parse(values[5]);
                double y7 = Double.Parse(values[6]);
                double y8 = Double.Parse(values[7]);
                if (globals.bUV1On) monitor_1.Add(timearray[j], y);
                if (globals.bUV2On) monitor_2.Add(timearray[j], y2);
                if (globals.bUV3On) monitor_3.Add(timearray[j], y3);
                if (globals.bUV4On) monitor_4.Add(timearray[j], y4);
                if (globals.bUV5On) monitor_5.Add(timearray[j], y5);
                if (globals.bUV6On) monitor_6.Add(timearray[j], y6);
                if (globals.bUV7On) monitor_7.Add(timearray[j], y7);
                if (globals.bUV8On) monitor_8.Add(timearray[j], y8);
                //increment the counter
                j++;
            }


            // Generate a red curve with diamond symbols, and "Column 1" in the legend
            // Always check to make sure the monitor is on..if not don't have the line
            LineItem myCurve = myPane.AddCurve("Col 1", monitor_1, Color.Red, SymbolType.Diamond);
            // Fill the symbols with white
            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Symbol.Size = 4;

            //Generate a blue curve with circle symbols, and "Column 2" in the legend
            LineItem myCurve2 = myPane.AddCurve("Col 2", monitor_2, Color.Blue, SymbolType.Circle);
            // Fill the symbols with white
            myCurve2.Symbol.Fill = new Fill(Color.White);  //try no fill
            myCurve2.Symbol.Size = 4;

            // Generate a violet curve with diamond symbols, and "Column 3" in the legend
            LineItem myCurve3 = myPane.AddCurve("Col 3", monitor_3, Color.Violet, SymbolType.Diamond);
            // Fill the symbols with white
            myCurve3.Symbol.Fill = new Fill(Color.White);
            myCurve3.Symbol.Size = 4;

            // Generate a green curve with square symbols, and "Column 4" in the legend
            LineItem myCurve4 = myPane.AddCurve("Col 4", monitor_4, Color.Green, SymbolType.Square);
            // Fill the symbols with white
            myCurve4.Symbol.Fill = new Fill(Color.White);
            myCurve4.Symbol.Size = 4;
            // Generate a orange curve with star symbols, and "Column 5" in the legend
            LineItem myCurve5 = myPane.AddCurve("Col 5", monitor_5, Color.Orange, SymbolType.Star);
            // Fill the symbols with white
            myCurve5.Symbol.Fill = new Fill(Color.White);
            myCurve5.Symbol.Size = 4;

            // Generate a black curve with triangle symbols, and "Column 6" in the legend
            LineItem myCurve6 = myPane.AddCurve("Col 6", monitor_6, Color.Black, SymbolType.Triangle);
            // Fill the symbols with white
            myCurve6.Symbol.Fill = new Fill(Color.White);
            myCurve6.Symbol.Size= 4;

            // Generate a indigo curve with xcross symbols, and "Column 7" in the legend
            LineItem myCurve7 = myPane.AddCurve("Col 7", monitor_7, Color.Indigo, SymbolType.XCross);
            myCurve7.Symbol.Size = 4;
               
            // Generate a pink curve with Plus symbols, and "Column 8" in the legend
            LineItem myCurve8 = myPane.AddCurve("Col 8", monitor_8, Color.DeepPink, SymbolType.Plus);
            myCurve8.Symbol.Size = 4;

            //
            globals.bStripCharting = true;

            // Show the x axis grid
            myPane.XAxis.MajorGrid.IsVisible = true;

            // Make the Y axis scale red
            myPane.YAxis.Scale.FontSpec.FontColor = Color.DarkRed;
            myPane.YAxis.Title.FontSpec.FontColor = Color.DarkRed;
            myPane.YAxis.MajorGrid.IsVisible = true;

            // turn off the opposite tics so the Y tics don't show up on the Y2 axis
            myPane.YAxis.MajorTic.IsOpposite = false;
            myPane.YAxis.MinorTic.IsOpposite = false;
                
            // Don't display the Y zero line
            myPane.YAxis.MajorGrid.IsZeroLine = false;

            // Align the Y axis labels so they are flush to the axis
            myPane.YAxis.Scale.Align = AlignP.Inside;

            // Manually set the axis range
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 1050;

            // Manually set the axis range
            myPane.XAxis.Type = AxisType.Date;
            //10 minute default window, can zoom to desired fit
            myPane.XAxis.Scale.Min = new XDate(DateTime.Now.AddSeconds(-600));
            myPane.XAxis.Scale.Max = new XDate(DateTime.Now);
            myPane.XAxis.Scale.Format = "HH:mm:ss";
            myPane.XAxis.Scale.MajorUnit = DateUnit.Second;
            myPane.XAxis.Scale.MajorStep = 60;
            myPane.XAxis.Scale.MinorUnit = DateUnit.Second;
            myPane.XAxis.Scale.MinorStep = 10;

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGray, 45.0f);

            // Add a text box with instructions
            TextObj text = new TextObj( "Use left mouse button for Drag Zoom\nUse the Right mouse button for the Menu",
                    0.01f, 0.98f, CoordType.ChartFraction, AlignH.Left, AlignV.Bottom);
            text.FontSpec.Size = 8;
            text.FontSpec.StringAlignment = StringAlignment.Near;
            myPane.GraphObjList.Add(text);

            // Enable scrollbars if needed
            zg1.IsShowHScrollBar = true;
            zg1.IsShowVScrollBar = true;
            zg1.IsAutoScrollRange = true;

            // Add a custom context menu item
            zg1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(
                            MyContextMenuBuilder);

            // Show tooltips when the mouse hovers over a point
            zg1.IsShowPointValues = true;
            zg1.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);
        
            // Call back to Handle the Zoom Event
            zg1.ZoomEvent += new ZedGraphControl.ZoomEventHandler(MyZoomEvent);

            // Size the control to fit the window
            SetSize();
           
            // Tell ZedGraph to calculate the axis ranges
            // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
            // up the proper scrolling parameters
            zg1.AxisChange();
            
            // Make sure the Graph gets redrawn
            zg1.Invalidate();
        }

        /// <summary>
        /// On resize action, resize the ZedGraphControl to fill most of the Form, with a small
        /// margin around the outside
        /// </summary>
        private void stripchartcs_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            zg1.Location = new Point(10, 10);
            
            // Leave a small margin around the outside of the control
            zg1.Size = new Size(this.ClientRectangle.Width - 10,
                    this.ClientRectangle.Height - 10);
        }
        /// <summary>
        /// Add a menu item to add or remove line symbolx
        /// </summary>
        private void MyContextMenuBuilder(ZedGraphControl control, ContextMenuStrip menuStrip,
                        Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = "show-symbol";
            item.Tag = "show-symbol";
            item.Text = "Show/Hide Symbols";
            item.Click += new System.EventHandler(ShowHideSymbol);

            menuStrip.Items.Add(item);
        }
        private void ShowHideSymbol(object sender, EventArgs args)
        {
            // Get a reference to the GraphPane instance in the ZedGraphControl
            GraphPane myPane = zg1.GraphPane;
            CurveList c1 = myPane.CurveList;

            if (!bSymbolsHidden)
            {
                foreach (LineItem l in c1)
                {
                    l.Symbol.IsVisible = false;
                    bSymbolsHidden = true;
                }
               
            }
            else
            {
                foreach (LineItem l in c1)
                {
                    l.Symbol.IsVisible = true;
                    bSymbolsHidden = false;
                }
            }
        }

        /// <summary>
        /// Display customized tooltips when the mouse hovers over a point
        /// </summary>
        private string MyPointValueHandler(ZedGraphControl control, GraphPane pane,
                        CurveItem curve, int iPt)
        {
            // Get the PointPair that is under the mouse
            PointPair pt = curve[iPt];

            XDate dx = new XDate(pt.X);

            return curve.Label.Text + " is " + pt.Y.ToString("f2") + " RFU at " + dx.ToString("G");
        }

        //Update the chart in real time with data from the serial port.
        public delegate void SetControlCallback(ZedGraph.ZedGraphControl c);
        public static void UpdateStripChart(string instring)
        {
            // Get a reference to the "Curve List" curve IPointListEdit
            try
            {
                if (globals.sc == null)
                    return;

                // Get a reference to the GraphPane instance in the ZedGraphControl
                GraphPane mylocalPane = globals.sc.zg1.GraphPane;
                 
                if(globals.bStripCharting)
                {
                IPointListEdit ip1 = globals.sc.zg1.GraphPane.CurveList["Col 1"].Points as IPointListEdit;
                IPointListEdit ip2 = globals.sc.zg1.GraphPane.CurveList["Col 2"].Points as IPointListEdit;
                IPointListEdit ip3 = globals.sc.zg1.GraphPane.CurveList["Col 3"].Points as IPointListEdit;
                IPointListEdit ip4 = globals.sc.zg1.GraphPane.CurveList["Col 4"].Points as IPointListEdit;
                IPointListEdit ip5 = globals.sc.zg1.GraphPane.CurveList["Col 5"].Points as IPointListEdit;
                IPointListEdit ip6 = globals.sc.zg1.GraphPane.CurveList["Col 6"].Points as IPointListEdit;
                IPointListEdit ip7 = globals.sc.zg1.GraphPane.CurveList["Col 7"].Points as IPointListEdit; 
                IPointListEdit ip8 = globals.sc.zg1.GraphPane.CurveList["Col 8"].Points as IPointListEdit;
                    //make sure we got the data
                    if (ip1 != null || ip2 != null || ip3 != null || ip4 != null || ip5 != null
                        || ip6 != null || ip7 != null || ip8 != null)
                    {
                        // Get a reference to the GraphPane instance in the ZedGraphControl
                        //GraphPane mylocalPane = globals.sc.zg1.GraphPane;

                        //now update the data
                        //note: we use datetime now instead of transferring x because it should only be 5ms or less
                        //to get the data 
                        XDate x = new XDate(DateTime.Now);
                        string[] ptvalues = instring.Split(',');

                        if (ptvalues.Count() < 7)
                            //|| ptvalues[0] == null || ptvalues[1] == null || ptvalues[2] == null || ptvalues[3] != null)
                            return;

                        double y1 = Convert.ToDouble(ptvalues[0]);
                        double y2 = Convert.ToDouble(ptvalues[1]);
                        double y3 = Convert.ToDouble(ptvalues[2]);
                        double y4 = Convert.ToDouble(ptvalues[3]);
                        double y5 = Convert.ToDouble(ptvalues[4]);
                        double y6 = Convert.ToDouble(ptvalues[5]);
                        double y7 = Convert.ToDouble(ptvalues[6]);
                        double y8 = Convert.ToDouble(ptvalues[7]);

                        if (globals.bUV1On) ip1.Add(x, y1);
                        if (globals.bUV2On) ip2.Add(x, y2);
                        if (globals.bUV3On) ip3.Add(x, y3);
                        if (globals.bUV4On) ip4.Add(x, y4);
                        if (globals.bUV5On) ip5.Add(x, y5);
                        if (globals.bUV6On) ip6.Add(x, y6);
                        if (globals.bUV7On) ip7.Add(x, y7);
                        if (globals.bUV8On) ip8.Add(x, y8);


                    }   
                    //update the axis
                    mylocalPane.XAxis.Scale.Min = new XDate(DateTime.Now.AddSeconds(-600));
                    mylocalPane.XAxis.Scale.Max = new XDate(DateTime.Now);

                    globals.sc.zg1.AxisChange();
               

                    //refresh the chart if not zoomed
                    ZedGraph.ZedGraphControl c = globals.sc.zg1;
                    if (!globals.bFluidicsBusy && !c.IsDisposed)
                    {
                        if (c != null && c.InvokeRequired)
                        {
                            try
                            {
                                //MessageBox.Show("In Callback" + text, c.ToString());
                                c.Invoke(new MethodInvoker(delegate
                                {
                                    if (!globals.bZoomed) { c.Invalidate(); }


                            }));
                            }
                            catch (Exception e) //(System.Reflection.TargetInvocationException tie)
                            {
                                MessageBox.Show("Invoke Error \n" + e.ToString(), "Invoke Issue");
                            }
                        }
                        else
                        {
                            if (c != null)
                            {
                                try {  if (!globals.bZoomed) { c.Invalidate(); } }
                                catch (Exception e) { MessageBox.Show("Could not update the chart \nReason - " + e.ToString()); }
                            }
                        }
                    }
               }
                
            }

            catch (Exception x) { MessageBox.Show("Error Getting Curve List -" + x.ToString(), "Curve List Error"); }


        }

        // Respond to a Zoom Event
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
        
       
        private void stripchartcs_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }

        }
    }
}
