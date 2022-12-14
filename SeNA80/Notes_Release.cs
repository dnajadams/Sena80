using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeNA80
{
    class Notes_Release
    {
        public static string rel_Notes =


         @"
                                     This is a NotePad to keep track of release notes   
          
         There are many little changes being made so it is best to log them all for user reference
         

        11-10-17  - rearrange the bottles and other UI improvements\n
        
        11-14-17  - fix a couple of small bugs noticed, i.e. don't let users
                          select two valves open at the same time, flow will back flow
        
        11-20-17  - redo the entire method editor to use ListView instead of listboxes so that it is easier
                           to edit methods.  Also fixed all the drag and drop issues with previous version

        11-27-17  - have the recycle valve close during pumping cycles for base and act+base.  This will hopefully keep
                          the system from 'dripping' amidite during the amidite cycle.  The check valves are not properly sealing
                          so need help!!!

        11-17-17  - Fixed the timer problem (I hope), nested the timer within a timer so that timer one posts the update to the 
                          status and timer 2 keeps the time (get rid of async wait).

        11-29-17  - Fixed the timer problems completely, it is tested and works, went to a synchronise timer that does App.DoEvents
                           to keep UI open, works great, also track amidite used and close the recycle valve to do check valve assist for
                           all train B reagents, even wash.
                           Also added push to column, it is a based for pressure on a 3mL/minute calibration, and measured value.
                          user inputs the measured value, the software does everything else..now available in protocol
                          NOTE - this week I want to add file openn in trityl monitor for historical trityl viewing...then, other than 
                          bug fixes this software is complete...

        11-29-17  - Added File open, there are still some fixes that need to be done for proper display and viewing, but it works,
                          so for now will be O.K.

        11-29-17  - Swapped Amidite and Reagent Presssures, they were exactly backwards...

        11-30-17  - Added Close Waste Valve to protocol editor, removed earlier try to close recycle valve during train B actions
                           don't understand why, but it did not work.  
                         - Also added File open to Bar Chart Viewer.  Now, when it opens it will display the latest, but, the user can open
                           any historical bar chart CSV file
                        - Cleaned up additional bugs with Log File open and other minor things....

        11-30-17  - Added secret button in about box to view release notes...

        12-1-17   - Completely rewrote the RunDoCommands program to simplify it and get rid of the risk of errors in multiple
                          places, now all do commands process the same set of functions...

        12-2-17   - Added comments to command so that we can read the log to determine errors..

        12-3-17   - Added Valves Viewer to Run Program, cleaned up all valve operations during real time run

        12-3-17   - Added memorize the last view to the Real Time Run program

        06-21-18 - Added Pressure and Flow calibration constants 
                       - For flow correction a constant is used to correct the steps sent to the pump (Vol Pumped = Vol Selected * PCF)
                       - For pressure, the flows are calibrated 3mL/min for amidites and 4mL/minute reagents, the pressure is input into 
                          the calibration field and then the software will monitor a 20% high/low window to make sure the pressure stays with-
                          in, the run will continue with only the labels being red, if too low though, the run will trip a pressure alarm and pause....

       06-22-18   -NOTE: Later I will add full pressure and flow calibration based on a multipoint calibration algorithm, then correct flows 
                          and pressures dynamically in real time to make sure the flow, whether by pressure or pump is extremely accurate.

       06-22-18   -Added a Splash Screen at Startup...it takes a few seconds for the controllers to initialize...will optimize the time later.
      
       07-01-18   -Major release - Added system security.  3 User levels 1) Administrator - System wide access 2) Advanced User -
                          Can change protocols at Run time, Can Edit Protocols, Can not change sytem administration 3) Basic User -
                          Can only run the default protocols, can change and modify sequences, can not edit protocols...

       07-02-18  -Added status bar to the Run Program to show continuous status updates for the current step.  Now have valve view, 
                         Message View (mostly command level language) and status bar that is text view.  So, there is ample run-time status 
                         information

      07-02-18  -Cleaned up run time bugs and switched to off thread waits for smoother operation.  Also cleaned up some main menu bugs
                        This will be an on-going process as the software is used more and more.

      07-03-18  -Added a hide the main menu when running function.  This way the main menu does not show when the Run program is 
                        displayed.  This will make it easier to show both the trityl monitor and run window at the same time. 

      07-03-18  -Added a mass calculator to the Sequence Editor.  Must use the letters provided.  Will add modifiers later once I am provided
                        the commonly used modifiers and the desired codes

     07-05-18   -1) Autoscroll list boxes during Run - active step will always show 2) Added database name search and Enter for login box, 3) 
                      added default Trityl Chart Label Selection, it is in the Config Editor and will automatically use the configured label selection.  
                      4) Changed one Xtra bottle to DEA so that it can be added and documented in the protocol 5) Expanded status to be shown in
                      status bar, also leave longer.  6) fixed bugs, in Sequence Editor, Run, Tritly Chart...note: 1 remaining bug will test and find over 
                      the weekend.

     07-10-18    - Added AutoRecycle function.  During the run the recycle valve will be controlled by the firmware to be closed during the 'pull' 
                        and open during 'push' for the amidite pump.  Also put in waste valve control in the firmware.  This way the pump will not leak
                        to waste during pumping operations.  We need to schedule to install a waste valve for both pumps later...(check the pin 
                        assignment).  Will in the near future add a dedicated waste valves so that both pumps work well.

     07-12-18   -Added Pump Initialization for both pumps to the Manual Control Program.  When recycling should empty the pump, then
                        do inject amidite, then recycle...

     07-13-18    -Added scale amidite flow based on a for loop - will be implemented very soon.  Can now select different scales for each
                        column being synthesized.  The algorithm bases scales on scaling factors from the System Configuration.  NOTE:  It only
                        scales amidites!  It does not scale the reagent delivery protocols.  And, it scales based on using the same column for the
                        various scales, so if you base the Amidtie protocol off of a 100uL Expedite column, you can scale that to the various scales
                        offered.  For now, you will have to base the reagent protocol on the largest scale

     07-13-18   -Later we will add reagent protocol scaling and the concept of Universal Protocol.  Will expand to soon let each column be 
                        at a different scale for both amidites and reagents...

     07-13-18   -the Amidite Scaling and Universal Protocols were all added at the requiest of Xie....Select the scale and with amidites, the looping
                        section will be scaled up or down based on the scale selected.  So, say you loop 4X at 1uMole, at 5uMole you will loop 20X.
                      -Scaling factors were also added to the configuration program.  These factors are used with the scale to correct for up and down
                        scaling, for example at 1 uMole you loop 4X, but at 5uMole you will only loop 16X (loop * scaling factor).

     07-15-18   -Added close all function so that when the run is terminated all valves are closed to the 'safe' position.  Added 'Edit on Run'
                        feature, you can now type sequences directly in on the Run window.  No need to create files and open them..  Fixed Menus
                        in trityl monitor, they now respond correctly.  Fixed No Chart Yet, window will not show.  Fixed LEDs turned off for columns not 
                        in use.  Fixed status lables so that there are not lots of extra spaces.  Added longer time when initilizing the pump, this is a 
                        very long operation. Fixed login screen, would trap correctly for password, now it traps correctly for login or pass.  Later will
                        change Terminate to provide options 1) at end of current step, 2) at end of current cycle 3) Immediately to provide a more 
                        graceful termination.  Last feature I will add and then only fix bugs is the ability to scale protocols based on the use of 
                        'Universal' protocols with looping steps for extra addition.  The protocols will scale based on total scale and number of columns.
                        So, if you are running 4 columns, it will scale by adding 2X as much of each reagent.  If you are running an average scale that is
                        2X the Universal scale, it will scale up, likewise if your average scale is 0.5 it will scale down to the average.  I will also break 
                        the Run program out to be standalone so that if there are users you want to ONLY RUN THE SYSTEM and not see all the other
                        stuff, you can create preset prep protocols to ready the system, have preset Universal protocols and post protocols so that 
                        all the user will have to do is type in a sequence, select a scale and press start.  

     07-19-18   -Added scale Deblock, Cap, Ox protocols for number of columns and average scale.  You simply need more reagent to 
                        synthesize additional columns and larger scales.  So, you can now create an 'Universal' protocol and then regardless 
                        of what scale you synthesize at the protocol will automatically scale to the scale and number of columns.

     07-20-18   - Added Graceful termination.  Three modes, 1) immediately, it will finish the current action, then terminate the protocol, it is 
                        recommended to use only in the case of emergency 2) At the end of the current step in the cycle (i.e. Deblocking), it will finish the 
                        cycle step and then terminate or 3) at the end of the cycle, the program will finsih all of the currently running protocol steps and 
                        then terminate.  So, if you are currently Deblocking, it will finish Coupling, Capping and Oxidation prior to termination.
                        
     07-31-18    - Added a 'Base Table' the base table stores the following 1) one letter base code 2) two letter base code 3) base chemical 
                      name and 4) mass of the base.  You can store up to 100 bases in the base table, but you will be limited by the one 
                      character possibilities A-Z, a-z, 0-9 and !@#~$%^&*()_+=-?/>< are about all the possible single letter options.  But,
                      you do have to input a one leter code, two letter code, name and mass for the base to be added to the table.  This
                      makes it more convenient to build protocols and view them as they run.  Plus, it reduces the chance for typing in the
                      wrong sequence.  ONLY ADMINISTRATORS CAN CHANGE CODE VIEWS, to switch from 1 letter views to 2 
                      letter views you must be an administrator.  To edit the base table, you must be an administrator (or have Excel).
                      - Other little bugs were fixed, a couple of small enhancements were also made.
     
     08-03-18   -Added Reagent Consumption Calculation - ALL PROTOCOLS MUST BE UPDATED TO THE NEW FORMAT.  Now
                    at Run-time, the system will estimate the total consumption for all reagents and amdites to be used during the
                    synthesis.  It counts all oligos and accomodates for average scale.  But, the process for synthesizing is:
                    (assume the Universal protocols preloaded) 1) Create a sequence(s) 2) Open the Run Program 3) Select the 
                    Sequence 4) Verify Reagent Volumes 3) Press start...no need for priming, washing column.  
                    ************************Just load and go!****************

    08-12-18    -Added 1) Thiol Protocols so that you can perform both oxidation and thiolation 2) broth the Prep protocol into
                        two a) a system startup protocol that does system priming, pressurize, etc.  and b) prep protocol do do 
                        initial wash, initial deblock, etc.  3) The ability to WHEN USING 2Letter codes do mixed P=S and P=O
                        oligo backbones 4) The ability to run in Serial (i.e. one at a time) or Parallel, all at the same time, but 
                        coupling still done one at a time.  5) Added the ability to reload without leaving the Run program. 
                        6) Added individual oligo finished so you know which ones can be removed because their synthesis
                        is complete.  Fixed many small annoying bugs....7) Added automatically show to the trityl chart so
                        it will automatically come up right after the first deblock and automatically close when the synthesis
                        is complete.
    
    08-13-18    -Got rid of duplicates and made the Consumption calculator support the new protocols.  Note: it does
                        not include the word 'amidite' so when you purge amidite 1, amiite 2, it will not be included in the 
                        volumes calculation.  It seems to work very well and very accurately.  

                      2 MORE FEATURES COMING!!!!!
                      PLAN TO ADD SMART COUPLING - when in parallel, if two bases the same it will couple them at the 
                      same time....Will also add SMART OXIDIZING at the same time, so if two bases require oxidation and
                      one requires thiolation when using the Mixed protcol we will first do oxidation with for the oligos needing
                      oxidized then tiolation with the oligos needing thiolated.-Done....see below
    
    08-28-18    -Added the ability for the Run program to select between PS and PO oxidation cycles automatically 
                        based on the codes used.  If the Run program sees a '-' as the third character, it will perform 
                        oxidation, if it sees a '+' or 's' it will perform thiolation during the oxidation ste.  I also added the 
                        ability to run multiple oligos and it will oxidize some while thiolating others in the same cycle.
                        (Wei Wang Function)
                     - Added 'Smart' coupling.  When the software sees two or more oligos during a cycle that require the
                        same base, it will couple them both at the same time if this option is selected.  Using smart coup-
                        ling saves about 30% synthesis time when synthesizing multiple oligos. (Bill Zhang function)
                     - Added popup tips for all programs except manual control.  If you hover the mouse over a button or
                        control, a balloon will appear with a brief decription of the controls function.  These tips can be 
                        turned on or off in the Run program or the Main Menu by selecting the Tools menus and select 
                        Enable/Disable Tips....
                    - User Interface Upgrade - changed the look and feel of the Main Menu and Run Programs, using 
                        more color to provide a better user experience.
                    - Added AutoOpen functionality for the trityl monitor.  It will now automatically display after the first 
                        deprotection is complete.

     12-24-18   -Fixed bugs in Amidite Configuration, there is still one left, will fix later
                        made combobox searching case sensitive for amidite lettters
                    -Fixed bugs in run...
                        fixed bug where column 7 wouldn't work, a for loop did not go to 8, now it does
                        fixed scaling factor bug, it now works
                    -Other minor bugs fixed and enhanced....

    01-21-18    Fixed major bugs 1) the stopwatch was being lost after a few cycles and if the program did not see
                       Pumping Done it would go into an infinite loop, fixed 2) The loop counter in the Run program was 
                       fixed along with several other bugs in the smart mode 3) When the Run Program was closed it would
                       keep in memory the MaxCycles variable and when you went back into Run it would miscalculate 
                       the number of cycles to run.  4) Other little bugs.


    01/22/19    - Did many changes to fix all of the pump issues, hopefully finally.  We changed the firmware to
                        slow the pump way down, put on larger outlet tubing, put in pump control valves, it seems to be
                        perfect now.  We also put the two amidite blocks in parallel.  We discovered the dead volume
                        from the valve blocks was ~500uL; so, with two blocks in parallel the dead volume was 
                        a lot.  To accomplish this we had to 'T' the Wash B reagent and installed a second valve to
                        allow Wash B to flow to both valve blocks in a controllable fashion.  We removed the recycle
                        valve, will put it back later once everything is validated as working.  

    01/23/19    -Fixed a few more software bugs.  Rewrote the software to accomodate for the hardware changes
                       fixed many minor bugs.  Also optimized the protocols to accomodate for the dead volume issues
                       with the valve blocks.  There are several new commands for protocols now to select an amidite 
                       push from the correct valve block.  The dead volume ranges from 390 to 500uL depending on 
                       the position.  Reconfigured amidite blocks to be in parallel with 7 amidites per block so that the
                       dead volume for 1 is the same as 8, 2 the same as 9, etc.  Fixed bug in step yield calculation.
                       Fixed bug in pump correction for amidites and made ONLY THE AMOUNT OF BASE variable
                       in Smart mode.  So, all other values are the same, just more base is added for multiple columns
                       coupled at the same time.
    
    01/28/19    -Put the looping steps in a list view instead of listbox, much cleaner to look at, make the high-
                       light bar in the list view green to show which step is the current step.
                        - will add trityl monitoring on final deblock to make sure we can see the trityl for all coupling
                          steps very soon (currently only during the looping steps, not post).

    01/29/19    -Added trityl monitoring for final POST step.  If the file name for POST contains 'Deblock' or
                       the file name contains 'Off' it will start the trityl monitor and register the trityl yield for the final
                       trityl and add it to the spreadsheet summary.  
    
    02/20/19    - Added long oligo handling, it will add  0.5% additional base per cycle and also add an 
                    additional coupling loop every 25 cycles to help drive coupling during a long synthesis...
                    NOTE: There is a checkbox in the System Configuration that must be checked for this
                    feature to be applied.
    04/03/2019  - Added the ability to save files (strip CSV, bar CSV and bar histogram), fixed end 
                        of Run bugs.  The software was not waiting until the post cycle to deblock the 
                        final trityl when shorter oligos are run with longer.  Fixed.  Also fixed a histogram
                        display bug and added the date to the histogram trityl so that when it is saved it
                        will show the date.  Fixed Consumption calc bugs.  Fixed batch file loading and 
                        saving bugs.  Fixed a few other small bugs...software is an on-going project!!!
    04/04/2020  - Added a new icon and went through all screens to make sure the same icon is used.
                - Checked out a reported bug, can't find it assume working with wrong version??
                - Fixedd the Pressurize program to pressurize all of the reagents...

    11/27/2022  - Fixed reported bugs (I think) and added a Radio Button for simutaneous wait.



                      THE SOFTWARE IS COMPLETE NOW AND WILL NO LONGER BE UPGRADED........
     ";       


    }
}
