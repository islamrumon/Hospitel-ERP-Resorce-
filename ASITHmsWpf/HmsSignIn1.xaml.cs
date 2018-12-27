using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan;
using ASITHmsViewMan.General;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.IO;

namespace ASITHmsWpf
{
    /// <summary>
    /// Interaction logic for HmsLogin1.xaml
    /// </summary>
    public partial class HmsSignIn1 : Window
    {
        public Window hmsMain1 = new Window();
        //public HmsMainWindow hmsMain1 = new HmsMainWindow();
        //public CommWin01 hmsMain1 = new CommWin01("MedShop");
        //public CommWin01 hmsMain1 = new CommWin01("DocVisit");
        public string ExitType = "";
        public HmsSignIn1(string ModuleID = "NOTHING", string WindowStyle = "SingleBorderWindow")
        {
            try
            {
                InitializeComponent();
                this.txblMsg1.Visibility = Visibility.Hidden;
                switch (ModuleID.ToUpper())
                {
                    case "FRONTDESK":
                        this.hmsMain1 = new CommWin01("FrontDesk");
                        break;
                    case "LABREPORT":
                        this.hmsMain1 = new CommWin01("LabReport");
                        break;
                    case "INVENTORY":
                        this.hmsMain1 = new CommWin01("Inventory");
                        break;
                    case "ACCOUNTS":
                        WpfProcessAccess.AppTitle = "CentERPoint Easy Accounting System";
                        this.lblAppTitle2.Content = "Easy Accounting System";
                        //this.hmsMain1 = new CommWin01("Accounts");
                        this.hmsMain1 = new HmsMainWindow("Accounts");
                        break;
                    case "MEDSHOP":
                        this.hmsMain1 = new CommWin01("MedShop");
                        break;
                    case "FOODSHOP":
                        WpfProcessAccess.AppTitle = "CentERPoint Foodshop Management System";
                        this.lblAppTitle2.Content = "Foodshop Management System";
                        //this.hmsMain1 = new CommWin01("FoodShop");
                        this.hmsMain1 = new HmsMainWindow("FoodShop");
                        break;
                    case "DOCVISIT":
                        this.hmsMain1 = new CommWin01("DocVisit");
                        break;
                    case "GENTRADESHOP":
                        WpfProcessAccess.AppTitle = "CentERPoint Trading Management System";
                        this.lblAppTitle2.Content = "Trading Management System";
                        //this.hmsMain1 = new CommWin01("GenTradeShop");
                        this.hmsMain1 = new HmsMainWindow("GenTradeShop");
                        break;
                    case "PARKTICKET":
                        WpfProcessAccess.AppTitle = "CentERPoint Park Management System";
                        this.lblAppTitle2.Content = "Park Management System";
                        ////this.hmsMain1 = new CommWin01("ParkTicket");
                        this.hmsMain1 = new HmsMainWindow("ParkTicket");
                        break;
                    case "SUPERSHOP":
                        WpfProcessAccess.AppTitle = "CentERPoint Super Shop Management System";
                        this.lblAppTitle2.Content = "Super Shop Management System";
                        //this.hmsMain1 = new CommWin01("SuperShop");
                        this.hmsMain1 = new HmsMainWindow("SuperShop");
                        break;
                    case "CELLPHONESHOP":
                        WpfProcessAccess.AppTitle = "CentERPoint Trading Management System";
                        this.lblAppTitle2.Content = "Trading Management System";
                        this.hmsMain1 = new CommWin01("CellPhoneShop");
                        break;
                    case "REALESTATE":
                        WpfProcessAccess.AppTitle = "CentERPoint Easy Real Estate Management System";
                        this.lblAppTitle2.Content = "Easy Real Estate Management System";
                        //this.hmsMain1 = new CommWin01("RealEstateMgt");
                        this.hmsMain1 = new HmsMainWindow("RealEstateMgt");
                        break;
                    case "RPGCLINVSMS":
                        // Online Inventory Management Systems
                        WpfProcessAccess.AppTitle = "Online Inventory Management & Smart SMS Utility System";
                        this.lblAppTitle2.Content = "Online Inventory Management & SMS Utility";
                        ////this.hmsMain1 = new CommWin01("RPGCLInvSMSMgt");
                        this.hmsMain1 = new HmsMainWindow("RPGCLInvSMSMgt");
                        break;
                    case "DIAGNOSTIC":
                        this.hmsMain1 = new HmsMainWindow("Diagnostic");
                        //this.hmsMain1 = new HmsMainWindow2();
                        break;
                    default:
                        this.hmsMain1 = new HmsMainWindow();
                        break;
                }

                this.hmsMain1.Title = WpfProcessAccess.AppTitle;

                //WpfProcessAccess.GetCompanyInfoList();
                //if (WpfProcessAccess.CompInfList != null)
                //{
                //    WpfProcessAccess.GetCompanyStaffList();
                //    if (WpfProcessAccess.VersionType == "11") // Development & Testing Version (0 for Published Version)
                //    {
                //        // For Temporary Use. For Distribution block the following If block
                //        if (WpfProcessAccess.StaffList != null)
                //            this.btnSingIn_Click(null, null);
                //    }
                //}

                //if (WpfProcessAccess.DatabaseErrorInfoList != null)
                //{
                //    WpfProcessAccess.ShowDatabaseErrorMessage();
                //    Application.Current.Shutdown();
                //}

                //if (WpfProcessAccess.StaffList.Count == 0)
                //{
                //    WpfProcessAccess.ShowDatabaseErrorMessage("Database configuration error occured.\nPlease contact to System Administrator");
                //    Application.Current.Shutdown();
                //}
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show(exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                Application.Current.Shutdown();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ChkPass_Click(null, null);
            if (WpfProcessAccess.VersionType == "1") // Development & Testing Version (0 for Published Version)
            {
                // For Temporary Use. For Distribution block the following If block
                if (WpfProcessAccess.StaffList != null)
                    this.btnSingIn_Click(null, null);
            }

            if ((WpfProcessAccess.CompInfList[0].comlogo != null))
            {
                byte[] bytes = WpfProcessAccess.CompInfList[0].comlogo;
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.StreamSource = mem;
                bmp3.EndInit();
                this.imgClIcon.Source = bmp3;
            }
        }

        private void btnSingIn_Click(object sender, RoutedEventArgs e)
        {
            this.ExitType = "GoMainWindow";
            string UserSignInName = this.txtSignInName.Text.Trim().ToUpper();
            string UserSignInPass = this.PasswordBox1.Password.Trim();
            string TerminalID = Environment.MachineName.ToString().Trim().ToUpper();
            string TerminalMAC = WpfProcessAccess.GetMacAddress();
            bool ChkPass1 = (this.ChkPass.IsChecked == true);
            string NewUserSignInPass1 = this.NewPasswordBox1.Password.Trim();
            string NewUserSignInPass2 = this.NewPasswordBox2.Password.Trim();

            //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            ////for each j you can get the MAC
            //PhysicalAddress address = nics[j].GetPhysicalAddress();
            //byte[] bytes = address.GetAddressBytes();
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    // Display the physical address in hexadecimal.
            //    Console.Write("{0}", bytes[i].ToString("X2"));
            //    // Insert a hyphen after each byte, unless we are at the end of the
            //    // address.
            //    if (i != bytes.Length - 1)
            //    {
            //        Console.Write("-");
            //    }
            //}


            if (UserSignInName.Length < 4 || UserSignInPass.Length < 4)
            {
                if (WpfProcessAccess.VersionType == "0") // Published Version (1 for Development & Testing Version)
                {
                    this.txtSignInName.Clear();
                    this.PasswordBox1.Clear();
                    this.txtSignInName.Focus();
                    this.txblMsg1.Visibility = Visibility.Visible;
                    return;
                }
                // For Temporary Use
                UserSignInName = vmHmsGeneral1.DevUserAuth.DevUserID;      
                UserSignInPass = vmHmsGeneral1.DevUserAuth.DevUserPass;    
            }

            string encodedPw = ASITUtility.EncodePassword(UserSignInName + UserSignInPass);
            string encodedNewPw1 = (ChkPass1 && NewUserSignInPass1.Length > 0 ? ASITUtility.EncodePassword(UserSignInName + NewUserSignInPass1) : "");
            string encodedNewPw2 = (ChkPass1 && NewUserSignInPass2.Length > 0 ? ASITUtility.EncodePassword(UserSignInName + NewUserSignInPass2) : "");

            WpfProcessAccess.GetSignedInUserList(UserSignInName, encodedPw, TerminalID, encodedNewPw1, encodedNewPw2);

            if (WpfProcessAccess.SignedInUserList == null)
            {
                this.txblMsg1.Visibility = Visibility.Visible;
                return;
            }
            if (WpfProcessAccess.SignedInUserList.Count == 0)
            {
                this.txblMsg1.Visibility = Visibility.Visible;
                return;
            }
            if(WpfProcessAccess.AppRptViewStyle != "Normal")
            {
                this.hmsMain1.WindowStyle = WindowStyle.None;
                this.hmsMain1.ResizeMode = ResizeMode.NoResize;
            }

            if (WpfProcessAccess.AppUserLogLevel > 0)
                WpfProcessAccess.UpdateUserLogInfo(logref1: "Sign-In Successfull");

            this.hmsMain1.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.ExitType.Length == 0)
                Application.Current.Shutdown();
            //hmsMain1.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            //this.Close();
        }

        private void btnCancel_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtSignInName.Focus();
        }
    
        private void ChkPass_Click(object sender, RoutedEventArgs e)
        {
            this.stkbutton.Visibility = (this.ChkPass.IsChecked == true ? Visibility.Hidden : Visibility.Visible);
            this.stkpChangePass.Visibility = (this.ChkPass.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
            this.Height = 210 + (this.ChkPass.IsChecked == true ? 100 : 0) + 20;
        }

        private void Mailto_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:info@asit.com.bd?Subject=The%20subject%20of%20the%20mail");
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.asit.com.bd");
        }

    }
}
