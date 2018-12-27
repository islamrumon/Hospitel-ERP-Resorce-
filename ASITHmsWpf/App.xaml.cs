using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ASITHmsWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //------ For Splash Screen -----------------------------------------
        public static ISplashScreen splashScreen;

        private ManualResetEvent ResetSplashCreated;
        private Thread SplashThread;
        private string startParm;
        private string AppWindowType;
        protected override void OnStartup(StartupEventArgs e)
        {
            //-------------------------------------------------------
            string[] args1 = Environment.GetCommandLineArgs();

            this.startParm = (args1.Length > 1 ? args1[1] : "NOTHING");   // NOTHING  DOCVISIT  MEDSHOP
            this.AppWindowType = (args1.Length > 2 ? args1[2] : "Fixed");   // Normal, Fixed
            // FOR SPECIAL PUBLISHED ONLY
            this.AppWindowType = "Normal";  // Normal, Fixed

            //this.AppWindowType = "Fixed";// "Normal";  // Normal, Fixed

            startParm = "Diagnostic";
            //startParm = "foodshop";
            //startParm = "medshop";
            //startParm = "accounts";
            //startParm = "inventory";
            //startParm = "gentradeshop";
            //startParm = "cellphoneshop";
            //startParm = "parkticket";
            //startParm = "supershop";
            //startParm = "realestate";
            //startParm = "RPGCLInvSMS";
            //------------------------------------------------------------------

            WpfProcessAccess.AppRptViewStyle = (this.AppWindowType == "Normal" ? "Normal" : "Dialog");
            WpfProcessAccess.GetAppConfigInfo();
            if (WpfProcessAccess.VersionType == "0")
            {
                // ManualResetEvent acts as a block. It waits for a signal to be set.
                ResetSplashCreated = new ManualResetEvent(false);

                // Create a new thread for the splash screen to run on
                SplashThread = new Thread(ShowSplash);
                SplashThread.SetApartmentState(ApartmentState.STA);
                SplashThread.IsBackground = true;
                SplashThread.Name = "Splash Screen";
                SplashThread.Start();

                // Wait for the blocker to be signaled before continuing. This is essentially the same as: while(ResetSplashCreated.NotSet) {}
                ResetSplashCreated.WaitOne();
            }
            base.OnStartup(e);
        }

        private void ShowSplash()
        {
            // Create the window
            int SplashIndex = (startParm == "foodshop" ? 1 : (startParm == "gentradeshop" || startParm == "supershop" || startParm == "cellphoneshop" ? 2 :
                (startParm == "accounts" ? 3 : (startParm == "parkticket" ? 4 : (startParm == "realestate" ? 5 : (startParm == "RPGCLInvSMS" ? 6 : 0))))));
            
            HmsSplashWindow1 animatedSplashScreenWindow = new HmsSplashWindow1(SplashTabIndex: SplashIndex);
            splashScreen = animatedSplashScreenWindow;

            // Show it
            animatedSplashScreenWindow.Show();

            // Now that the window is created, allow the rest of the startup to run
            ResetSplashCreated.Set();
            System.Windows.Threading.Dispatcher.Run();
        }

        //-----------------------------------------------

        //protected override void OnStartup(StartupEventArgs e)
        //{

        //    //StartupUri="HmsSignIn1.xaml"
        //    string[] args = Environment.GetCommandLineArgs();

        //    foreach (string arg in e.Args)
        //    {
        //        // TODO: whatever
        //    }

        //    base.OnStartup(e);
        //    HmsSignIn1 swin1 = new HmsSignIn1();
        //    if (swin1.IsLoaded)
        //        swin1.Show();
        //}

        private void App_Startup(object sender, StartupEventArgs e)
        {
            WpfProcessAccess.GetCompanyInfoList();

            if (WpfProcessAccess.CompInfList != null)
                WpfProcessAccess.GetCompanyStaffList();

            if (WpfProcessAccess.DatabaseErrorInfoList != null)
            {
                WpfProcessAccess.ShowDatabaseErrorMessage(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage);
                Environment.Exit(0);
                //Application.Current.Shutdown();
                return;
            }

            if (WpfProcessAccess.StaffList == null)
            {
                WpfProcessAccess.ShowDatabaseErrorMessage("Database configuration error occured.\nPlease contact to System Administrator");
                Environment.Exit(0);
                //Application.Current.Shutdown();
                return;
            }

            if (WpfProcessAccess.StaffList.Count == 0)
            {
                WpfProcessAccess.ShowDatabaseErrorMessage("Database configuration error occured.\nPlease contact to System Administrator");
                Environment.Exit(0);
                //Application.Current.Shutdown();
                return;
            }
       
            HmsSignIn1 swin1 = new HmsSignIn1(startParm);

            if (!swin1.IsVisible)
            {
                swin1.Show();
            }
            swin1.WindowState = WindowState.Normal;
            swin1.Activate();
            swin1.Topmost = true;
            swin1.Topmost = false;
            swin1.Focus();
           
            /*
            // Application is running
            // Process command line args
            bool startMinimized = false;
            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i] == "/StartMinimized")
                {
                    startMinimized = true;
                }
            }
            // Create main application window, starting minimized if specified
            HmsMainWindow mainWindow = new HmsMainWindow();
            if (startMinimized)
            {
                mainWindow.WindowState = WindowState.Minimized;
            }
            mainWindow.Show();
             */



        }
    }
}
