using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeNA80
{
    class valves
    {
        //pump
        public static string reagPumpInit = "P,0\n";
       
        //3 way valve blocks
        public static string tocol = "V,B,1\n";
        public static string tocolbypass = "V,B,0\n";
        public static string topump = "V,P,1\n";
        public static string topumpbypass = "V,P,0\n";
        public static string pressuretop = "V,A,0\n";
        public static string pressurebot = "V,A,1\n";


       //reagents
        //-deblock
        public static string deblon = "V,R,1,1\n";
        public static string debloff = "V,R,1,0\n";
        public static string deblgason = "V,S,1,1\n";
        public static string deblgasoff = "V,S,1,0\n";
        
        //-caps
        public static string capaon = "V,R,2,1\n";
        public static string capaoff = "V,R,2,0\n";
        public static string capagason = "V,S,2,1\n";
        public static string capagasoff = "V,S,2,0\n";
        public static string capbon = "V,A,16,1\n";
        public static string capboff = "V,A,16,0\n";
        public static string capbgason = "V,S,2,1\n";
        public static string capbgasoff = "V,S,2,0\n";
        
        //-ox
        public static string ox1on = "V,R,3,1\n";
        public static string ox1off = "V,R,3,0\n";
        public static string ox1gason = "V,S,3,1\n";
        public static string ox1gasoff = "V,S,3,0\n";
        public static string ox2on = "V,R,4,1\n";
        public static string ox2off = "V,R,4,0\n";
        public static string ox2gason = "V,S,4,1\n";
        public static string ox2gasoff = "V,S,4,0\n";
        
        //-xtra
        public static string xtra1on = "V,R,6,1\n";  //DEA
        public static string xtra1off = "V,R,6,0\n";  //DEA
        public static string xtra1gason = "V,S,7,1\n"; //DEA
        public static string xtra1gasoff = "V,S,7,0\n"; //DEA
        public static string gaspurgeon = "V,R,7,1\n";
        public static string gaspurgeoff = "V,R,7,0\n";
        public static string xtra2gason = "V,S,6,1\n";
        public static string xtra2gasoff = "V,S,6,0\n";

        //wash solutions
        //- wash b on it's 2 way own valve
        public static string washaon = "V,R,8,1\n";
        public static string washaoff = "V,R,8,0\n";
        public static string washagason = "V,S,7,1\n";
        public static string washagasoff = "V,S,7,0\n";

        //public static string washbon = "V,A,16,1\n";
        //public static string washboff = "V,A,16,0\n";
        public static string washbon = "V,A,16,1\n";
        public static string washboff = "V,A,16,0\n";
        //got the valves B1 and B2 are the same as RB code
        //public static string washb1on = "V,A,16,1\n";
        //public static string washb1off = "V,A,16,0\n";
        //public static string washb2on = "V,A,16,1\n";
        //public static string washb2off = "V,A,16,0\n";
        public static string washb1on = "V,W,1,1\n";
        public static string washb1off = "V,W,1,0\n";
        public static string washb2on = "V,W,2,1\n";
        public static string washb2off = "V,W,2,0\n";

        public static string washbgason = "V,S,8,1\n";
        public static string washbgasoff = "V,S,8,0\n";

        //activators
        public static string act1on = "V,R,5,1\n";
        public static string act1off = "V,R,5,0\n";
        public static string act1gason = "V,S,5,1\n";
        public static string act1gasoff = "V,S,5,0\n";

        public static string act2on = "V,R,6,1\n";
        public static string act2off = "V,R,6,0\n";
        public static string act2gason = "V,S,5,1\n";
        public static string act2gasoff = "V,S,5,0\n";

        //amidites
        public static string am14on = "V,A,14,1\n";
        public static string am14off = "V,A,14,0\n";
        public static string am13on = "V,A,13,1\n";
        public static string am13off = "V,A,13,0\n";
        public static string am12on = "V,A,12,1\n";
        public static string am12off = "V,A,12,0\n";
        public static string am11on = "V,A,11,1\n";
        public static string am11off = "V,A,11,0\n";
        public static string am10on = "V,A,10,1\n";
        public static string am10off = "V,A,10,0\n";
        public static string am9on = "V,A,9,1\n";
        public static string am9off = "V,A,9,0\n";
        public static string am8on = "V,A,8,1\n";
        public static string am8off = "V,A,8,0\n";
        public static string am7on = "V,A,7,1\n";
        public static string am7off = "V,A,7,0\n";
        public static string am6on = "V,A,6,1\n";
        public static string am6off = "V,A,6,0\n";
        public static string am5on = "V,A,5,1\n";
        public static string am5off = "V,A,5,0\n";
        public static string am4on = "V,A,4,1\n";
        public static string am4off = "V,A,4,0\n";
        public static string am3on = "V,A,3,1\n";
        public static string am3off = "V,A,3,0\n";
        public static string am2on = "V,A,2,1\n";
        public static string am2off = "V,A,2,0\n";
        public static string am1on = "V,A,1,1\n";
        public static string am1off = "V,A,1,0\n";

        //columns
        public static string col1on = "V,C,1,1\n";
        public static string col1off = "V,C,1,0\n";
        public static string col2on = "V,C,2,1\n";
        public static string col2off = "V,C,2,0\n";
        public static string col3on = "V,C,3,1\n";
        public static string col3off = "V,C,3,0\n";
        public static string col4on = "V,C,4,1\n";
        public static string col4off = "V,C,4,0\n";
        public static string col5on = "V,C,5,1\n";
        public static string col5off = "V,C,5,0\n";
        public static string col6on = "V,C,6,1\n";
        public static string col6off = "V,C,6,0\n";
        public static string col7on = "V,C,7,1\n";
        public static string col7off = "V,C,7,0\n";
        public static string col8on = "V,C,8,1\n";
        public static string col8off = "V,C,8,0\n";

        //LEDS for UV monitors
        public static string UV1on = "L,1,1\n";
        public static string UV2on = "L,2,1\n";
        public static string UV3on = "L,3,1\n";
        public static string UV4on = "L,4,1\n";
        public static string UV5on = "L,5,1\n";
        public static string UV6on = "L,6,1\n";
        public static string UV7on = "L,7,1\n";
        public static string UV8on = "L,8,1\n";
        public static string UV1off = "L,1,0\n";
        public static string UV2off = "L,2,0\n";
        public static string UV3off = "L,3,0\n";
        public static string UV4off = "L,4,0\n";
        public static string UV5off = "L,5,0\n";
        public static string UV6off = "L,6,0\n";
        public static string UV7off = "L,7,0\n";
        public static string UV8off = "L,8,0\n";
        
        public static void AllValvesOff()
        {
            // make all red
            globals.runform.rb_3WBC.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_3WBP.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_3WWR.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_3WRA.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am1.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am2.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am3.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am4.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am5.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am6.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am7.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am8.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am9.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am10.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am11.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am12.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Am13.BackColor = System.Drawing.Color.Red;
          //  globals.runform.rb_Am14.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_WashA.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_WashB.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Dbl.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_CapA.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_CapB.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Ox1.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Ox2.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Xtra1.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Xtra2.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Act1.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_Act2.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_C1.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_C2.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_C3.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_C4.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_C5.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_C6.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_C7.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_C8.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_AGas.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_AGas.Text = "top";
            globals.runform.rb_RGas.BackColor = System.Drawing.Color.Red;
            globals.runform.rb_RGas.Text = "none";

            //now close all
            //3WayVavles
            Man_Controlcs.SendControllerMsg(1, topumpbypass);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, tocolbypass);
            globals.runform.SyncWait(20);


            //big bottles
            Man_Controlcs.SendControllerMsg(1, debloff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, deblgasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, capaoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, capboff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, capagasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, capbgasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, ox1off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, ox1gasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, ox2off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, ox2gasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, xtra1off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, xtra1gasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, gaspurgeoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, xtra2gasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1,  washaoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, washagasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, washboff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, washbgasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, act1off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, act1gasoff);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, act2off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, act2gasoff);
            globals.runform.SyncWait(20);

            //columns
            Man_Controlcs.SendControllerMsg(1, col1off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, col2off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, col3off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, col4off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, col5off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, col6off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, col7off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, col8off);
            globals.runform.SyncWait(20);

            //amidites
            Man_Controlcs.SendControllerMsg(1, am1off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am2off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am3off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am4off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am5off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am6off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am7off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am8off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am9off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am10off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am11off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am12off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am13off);
            globals.runform.SyncWait(20);
            Man_Controlcs.SendControllerMsg(1, am14off);
            globals.runform.SyncWait(20);
        }
    }
}
