using ASITHmsEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ASITHmsWpf
{
    /// <summary>
    /// Interaction logic for CommWin01.xaml
    /// </summary>
    public partial class CommWin01 : Window
    {
        private DispatcherTimer timer1 = new DispatcherTimer();
        private DispatcherTimer timerClick1 = new DispatcherTimer();
        private int timerClick1Counter = 0;
        private string uc1Name = "Nothing";
        private string frmTag = "Nothing";
        private string frmTag2 = "Nothing";
        private List<MenuItem> MenuItemList = new List<MenuItem>();


        private double psWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        private double psHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
        private double vsWidth = System.Windows.SystemParameters.VirtualScreenWidth;
        private double vsHeight = System.Windows.SystemParameters.VirtualScreenHeight;

        private double XpValue = 0;
        private double YpValue = 0;


        private string ModuleOption1 = "Nothing";
        public CommWin01()
        {
            InitializeComponent();
        }

        public CommWin01(string ModuleOption = "")
        {
            InitializeComponent();
            this.ModuleOption1 = ModuleOption;

            this.timer1.Interval = TimeSpan.FromSeconds(30);
            this.timer1.Tick += this.timer1_Tick;
            this.timer1.Start();
            //            this.UcGrid1.Visibility = Visibility.Hidden;

            this.lblWait1.Visibility = Visibility.Hidden;

            this.timerClick1Counter = 0;
            this.timerClick1.Interval = TimeSpan.FromSeconds(0);
            this.timerClick1.Tick += this.timerClick1_Tick;
            this.timerClick1.Stop();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (WpfProcessAccess.AppRptViewStyle != "Normal")
                this.btnMinimize.Visibility = Visibility.Visible;

            var empname1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == WpfProcessAccess.SignedInUserList[0].hccode);
            this.lblSignInNam.Content = WpfProcessAccess.SignedInUserList[0].signinnam.ToString();
            this.lblSignInNam.ToolTip = (empname1 == null ? WpfProcessAccess.SignedInUserList[0].hcname.ToString() : empname1[0].sirdesc.Trim());
            this.lblSessionId.Content = WpfProcessAccess.SignedInUserList[0].sessionID.ToString();
            this.lblTeminalId.Content = WpfProcessAccess.SignedInUserList[0].terminalID.ToString();

            this.MenuCleanUp();

            switch (ModuleOption1)
            {
                case "FrontDesk":
                    this.FrontDesk();
                    break;
                case "LabReport":
                    this.LabOperation();
                    break;
                case "Inventory":
                    this.Inventory();
                    break;
                case "Accounts":
                    this.Accounts();
                    break;
                case "MedShop":
                    this.MedicineShop();
                    break;
                case "FoodShop":
                    this.FastFoodShop();
                    break;
                case "DocVisit":
                    this.DoctorVisit();
                    break;
                case "GenTradeShop":
                    this.GenTradeShop();
                    break;
                case "ParkTicket":
                    this.ParkTicket();
                    break;
                case "SuperShop":
                    this.SuperShop();
                    break;
                case "CellPhoneShop":
                    this.CellPhoneShop();
                    break;
                case "RealEstateMgt":
                    this.RealEstateMgt();
                    break;
                case "RPGCLInvSMSMgt":
                    this.RPGCLInvSMSMgt();
                    break;

            }
            #region ObjectInitialization Option


            this.RemoveGrid(this.Grid01);
            this.RemoveGrid(this.Grid02);
            this.RemoveGrid(this.Grid03);
            this.RemoveGrid(this.Grid04);
            this.RemoveGrid(this.Grid05);
            this.RemoveGrid(this.Grid06);
            this.RemoveGrid(this.Grid07);
            this.RemoveGrid(this.Grid08);
            this.RemoveGrid(this.Grid09);
            this.RemoveGrid(this.Grid10);

            this.AddContextMenu();

            if ((WpfProcessAccess.SignedInUserList[0].hcphoto != null))
            {
                byte[] bytes = (byte[])WpfProcessAccess.SignedInUserList[0].hcphoto;
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.StreamSource = mem;
                bmp3.EndInit();
                this.imgSignInUser.Source = bmp3;
                //this.UserPhoto.Source = bmp3;
            }

            if ((WpfProcessAccess.CompInfList[0].comlabel != null))
            {
                byte[] bytes = WpfProcessAccess.CompInfList[0].comlabel;
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.StreamSource = mem;
                bmp3.EndInit();
                this.imgClTitle.Source = bmp3;
                this.imgGoStartUp.Source = bmp3;
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

            this.timer1_Tick(null, null);
            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewKeyUpEvent, new KeyEventHandler(Window_PreviewKeyUp));

            //this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            //this.Height = System.Windows.SystemParameters.VirtualScreenHeight;

            if (System.Windows.SystemParameters.VirtualScreenWidth < 900)
            {
                this.slider1.Minimum = 0.5;
                this.slider1.Maximum = 0.7;
                this.slider1.Value = 0.55;
                this.stkpContact.Visibility = Visibility.Collapsed;
                this.imgClTitle.Width = 250;
            }
            else if (System.Windows.SystemParameters.VirtualScreenWidth < 1200)
            {
                this.slider1.Minimum = 0.5;
                this.slider1.Maximum = 1.0;
                this.slider1.Value = 0.78;
                this.stkpContact.Visibility = Visibility.Collapsed;
                this.imgClTitle.Width = 450;
            }
            else if (System.Windows.SystemParameters.VirtualScreenWidth > 3000)
            {
                this.slider1.Minimum = 1.2;
                this.slider1.Maximum = 1.5;
                this.slider1.Value = 1.4;
            }
            else if (System.Windows.SystemParameters.VirtualScreenWidth > 1900)
            {
                this.slider1.Minimum = 1.0;
                this.slider1.Maximum = 1.5;
                this.slider1.Value = 1.4;
            }
            else
            {
                this.slider1.Minimum = 0.7;
                this.slider1.Maximum = 1.5;
                this.slider1.Value = 1.0;
            }
            #endregion
        }

        #region Menu Setup

        private void RemoveGrid(Grid gridx)
        {
            string[] strTag1 = gridx.Tag.ToString().Trim().Split(',');
            if (WpfProcessAccess.AppFormsList == null)
                gridx.Visibility = Visibility.Collapsed;
            else if (WpfProcessAccess.AppFormsList.FirstOrDefault(x => x.Contains(strTag1[0])) == null)
                gridx.Visibility = Visibility.Collapsed;
            else if (strTag1.Length > 1 && strTag1[0].Contains("frmEntryVoucher1"))
            {
                var vtypeList = HmsEntityAccounting.GetVoucherType().FindAll(x => x.vtitle.ToUpper().Contains(strTag1[1].ToUpper()));// && !x.vtitle.ToUpper().Contains("BUDGET"));
                if (strTag1.Length > 2)
                    vtypeList = vtypeList.FindAll(x => x.vtagid.Contains(strTag1[2])).ToList();

                int found1 = 0;
                foreach (var item1 in vtypeList)
                {
                    if (WpfProcessAccess.SignedInUserAuthList.FirstOrDefault(x => x.uicode == "WPF_frmEntryVoucher1_cmbVouType_" + item1.vtagid) != null)
                        found1++;
                }

                if (found1 == 0)
                    gridx.Visibility = Visibility.Collapsed;
            }
        }
        private void MenuCleanUp()
        {
            this.Grid01.Visibility = Visibility.Visible;
            this.Grid02.Visibility = Visibility.Visible;
            this.Grid03.Visibility = Visibility.Visible;
            this.Grid04.Visibility = Visibility.Visible;
            this.Grid05.Visibility = Visibility.Visible;
            this.Grid06.Visibility = Visibility.Visible;
            this.Grid07.Visibility = Visibility.Visible;
            this.Grid08.Visibility = Visibility.Visible;
            this.Grid09.Visibility = Visibility.Visible;
            this.Grid10.Visibility = Visibility.Visible;

            this.Grid01.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid02.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid03.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid04.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid05.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid06.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX";

            this.lblTitle01.Content = "Optional 01"; this.lblTitle02.Content = "Optional 02"; this.lblTitle03.Content = "Optional 03"; this.lblTitle04.Content = "Optional 04"; this.lblTitle05.Content = "Optional 05";
            this.lblTitle06.Content = "Optional 06"; this.lblTitle07.Content = "Optional 07"; this.lblTitle08.Content = "Optional 08"; this.lblTitle09.Content = "Optional 09"; this.lblTitle10.Content = "Optional 10";

        }
        private void FrontDesk()
        {

            this.imgBack.ImageSource = this.imgFrontDesk.Source;// this.imgDocVisit.Source;// null;//
            this.Grid01.Tag = "frmEntryFrontDesk1"; this.lblTitle01.Content = "Receiption";
            this.Grid02.Tag = "frmEntryFrontDesk3_Old"; this.lblTitle02.Content = "Others Entry";
            this.Grid03.Tag = "frmEntryHelpDesk1"; this.lblTitle03.Content = "Help Desk";
            this.Grid04.Tag = "frmEntryDocVisit1"; this.lblTitle04.Content = "Patient Visit Token";
        }

        private void LabOperation()     // 02
        {
            this.imgBack.ImageSource = this.imgFrontDesk.Source;// this.imgDocVisit.Source;// null;//
            this.Grid01.Tag = "frmEntryLabMagt101,Sample Receive"; this.lblTitle01.Content = "Sample Receive";
            this.Grid02.Tag = "frmEntryLabMagt101,Report Preparation"; this.lblTitle02.Content = "Report Preparation";
            this.Grid03.Tag = "frmEntryLabMagt101,Report Submission"; this.lblTitle03.Content = "Report Submission";
            this.Grid04.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle04.Content = "";
            this.Grid05.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle05.Content = "";
            this.Grid06.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle06.Content = ""; ;
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle07.Content = "";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }

        private void Inventory()
        {
            this.imgBack.ImageSource = this.imgGTradeShop.Source;// this.imgDocVisit.Source;// null;//
            this.Grid01.Tag = "frmEntryStoreReq1"; this.lblTitle01.Content = "Store Requisition";
            this.Grid02.Tag = "frmEntryStoreIssue1"; this.lblTitle02.Content = "Item Issue/Transfer";
            this.Grid03.Tag = "frmEntryItemRcv1"; this.lblTitle03.Content = "Item Purchase/Receive";
            this.Grid04.Tag = "frmEntryPurReq1"; this.lblTitle04.Content = "Purchase Requisition";
            this.Grid05.Tag = "frmEntryItemStock1"; this.lblTitle05.Content = "Physical Stock Entry";
            this.Grid06.Tag = "frmReportStore1"; this.lblTitle06.Content = "Store Reports";
            this.Grid07.Tag = "frmConfigSetup1"; this.lblTitle07.Content = "Configuration Setup";
            //this.Grid05.Tag = "frmEntryPurQuotation1"; this.lblTitle05.Content = "Quotation Receive";
            //this.Grid06.Tag = "frmEntryPurOrder1"; this.lblTitle06.Content = "Purchase Order";
            //this.Grid07.Tag = "frmEntryPurBillRcv1"; this.lblTitle07.Content = "Purchase Bill Receive";
            //this.Grid08.Tag = "frmEntryPurLCInfo1"; this.lblTitle08.Content = "Import L/C";
            //this.Grid09.Tag = "frmReportStore1"; this.lblTitle09.Content = "Store Reports";
            //this.Grid10.Tag = "frmConfigSetup1"; this.lblTitle10.Content = "Configuration Setup";
        }

        private void RPGCLInvSMSMgt()
        {
            this.imgBack.ImageSource = this.imgGTradeShop.Source;// this.imgDocVisit.Source;// null;//
            this.Grid01.Tag = "frmEntryStoreReq1"; this.lblTitle01.Content = "Store Requisition";
            this.Grid02.Tag = "frmEntryStoreIssue1"; this.lblTitle02.Content = "Item Issue/Transfer";
            this.Grid03.Tag = "frmEntryItemRcv1"; this.lblTitle03.Content = "Item Purchase/Receive";
            this.Grid04.Tag = "frmEntryPurReq1"; this.lblTitle04.Content = "Purchase Requisition";
            this.Grid05.Tag = "frmEntryItemStock1"; this.lblTitle05.Content = "Physical Stock Entry";
            this.Grid06.Tag = "frmReportStore1"; this.lblTitle06.Content = "Store Reports";
            this.Grid07.Tag = "frmMessagegMgt1"; this.lblTitle07.Content = "SMS Management";
            this.Grid08.Tag = "frmEntryAccMgt1"; this.lblTitle08.Content = "Accounts Management";
            this.Grid09.Tag = "frmConfigSetup1"; this.lblTitle09.Content = "Configuration Setup";

            this.MenuItemList.Clear();
            this.MenuItemList.Add(new MenuItem() { Header = "0_1." + this.lblTitle01.Content.ToString(), Uid = this.Grid01.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = "0_2." + this.lblTitle02.Content.ToString(), Uid = this.Grid02.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = "0_3." + this.lblTitle03.Content.ToString(), Uid = this.Grid03.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = "0_4." + this.lblTitle04.Content.ToString(), Uid = this.Grid04.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = "0_5." + this.lblTitle05.Content.ToString(), Uid = this.Grid05.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = "0_6." + this.lblTitle06.Content.ToString(), Uid = this.Grid06.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = "0_7." + this.lblTitle07.Content.ToString(), Uid = this.Grid07.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = "0_8." + this.lblTitle08.Content.ToString(), Uid = this.Grid08.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = "0_9." + this.lblTitle09.Content.ToString(), Uid = this.Grid09.Tag.ToString() });
        }


        private void Accounts()
        {
            //this.imgBack.ImageSource = this.imgFrontDesk.Source;// this.imgDocVisit.Source;// null;//
            this.imgBack.ImageSource = this.imgGTradeShop.Source;// this.imgDocVisit.Source;// null;//            
            this.Grid01.Tag = "frmEntryVoucher1,Voucher"; this.lblTitle01.Content = "Accounts Voucher";
            this.Grid02.Tag = "frmEntryAccMgt1"; this.lblTitle02.Content = "Accounts Management";
            this.Grid03.Tag = "frmEntryInvMgt1"; this.lblTitle03.Content = "Inventory Management";
            this.Grid04.Tag = "frmReportAcc1"; this.lblTitle04.Content = "Accounting Reports";
            this.Grid05.Tag = "frmReportStore1"; this.lblTitle05.Content = "Inventory Report";
            this.Grid06.Tag = "frmConfigSetup1"; this.lblTitle06.Content = "Configuration Setup";
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle07.Content = "";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }

        private void MedicineShop()
        {
            this.imgBack.ImageSource = this.imgMedStore.Source;// this.imgDocVisit.Source;// null;//
            this.Grid01.Tag = "frmEntryPharmaPOS1"; this.lblTitle01.Content = "Sales";
            this.Grid02.Tag = "frmEntryItemRcv1"; this.lblTitle02.Content = "Item Purchase/Receive";
            this.Grid03.Tag = "frmEntryStoreIssue1,StockOut"; this.lblTitle03.Content = "Item Transfer/Stock Out";
            this.Grid04.Tag = "frmEntryVoucher1,Voucher"; this.lblTitle04.Content = "Accounts Voucher";
            this.Grid05.Tag = "frmEntryInvMgt1"; this.lblTitle05.Content = "Inventory Admin";
            this.Grid06.Tag = "frmReportStore1"; this.lblTitle06.Content = "Inventory Report";
            this.Grid07.Tag = "frmReportAcc1"; this.lblTitle07.Content = "Accounting Reports";
            this.Grid08.Tag = "frmEntryAccMgt1"; this.lblTitle08.Content = "Accounts Admin";
            this.Grid09.Tag = "frmConfigSetup1"; this.lblTitle09.Content = "Configuration Setup";

            //this.Grid02.Tag = "frmReportPharmaPOS1,MediShop"; this.lblTitle02.Content = "Sales Report";   // to be deleted soon Hafiz 30-Jan-2018

            this.Grid10.Tag = "frmOtherCodeBook1"; this.lblTitle10.Content = "Others Code Entry";    // to be deleted soon Hafiz 30-Jan-2018
        }

        private void FastFoodShop()
        {
            WpfProcessAccess.AppTitle = "CentERPoint Foodshop Management System";
            this.imgBack.ImageSource = this.imgFoodShop.Source;// this.imgDocVisit.Source;// null;//
            this.Grid01.Tag = "frmEntryRestauPOS1"; this.lblTitle01.Content = "Sales";
            this.Grid02.Tag = "frmEntryItemRcv1"; this.lblTitle02.Content = "Item Purchase/Receive";
            this.Grid03.Tag = "frmEntryStoreIssue1,StockOut"; this.lblTitle03.Content = "Item Transfer/Stock Out";
            this.Grid04.Tag = "frmEntryVoucher1,Voucher"; this.lblTitle04.Content = "Accounts Voucher";
            this.Grid05.Tag = "frmEntryInvMgt1"; this.lblTitle05.Content = "Inventory Admin";
            this.Grid06.Tag = "frmReportStore1"; this.lblTitle06.Content = "Inventory Report";
            this.Grid07.Tag = "frmReportAcc1"; this.lblTitle07.Content = "Accounting Reports";
            this.Grid08.Tag = "frmEntryAccMgt1"; this.lblTitle08.Content = "Accounts Admin";
            this.Grid09.Tag = "frmConfigSetup1"; this.lblTitle09.Content = "Configuration";
        }

        private void DoctorVisit()
        {
            this.imgBack.ImageSource = this.imgDocVisit.Source;// null;//
            this.Grid01.Tag = "frmEntryDocVisit1"; this.lblTitle01.Content = "Visiting Token Entry";
        }
        private void GenTradeShop()
        {
            this.imgBack.ImageSource = this.imgGTradeShop.Source;// this.imgDocVisit.Source;// null;//            
            this.Grid01.Tag = "frmEntryGenTrPOS1"; this.lblTitle01.Content = "Sales";
            this.Grid02.Tag = "frmEntryItemRcv1"; this.lblTitle02.Content = "Item Purchase/Receive";
            this.Grid03.Tag = "frmEntryPur01"; this.lblTitle03.Content = "Purchease Process";
            this.Grid04.Tag = "frmEntryStoreIssue1,StockOut"; this.lblTitle04.Content = "Item Transfer/Stock Out";
            this.Grid05.Tag = "frmEntryVoucher1,Voucher"; this.lblTitle05.Content = "Accounts Voucher";
            this.Grid06.Tag = "frmEntryInvMgt1"; this.lblTitle06.Content = "Inventory Admin";
            this.Grid07.Tag = "frmReportStore1"; this.lblTitle07.Content = "Inventory Report";
            this.Grid08.Tag = "frmReportAcc1"; this.lblTitle08.Content = "Accounting Reports";
            this.Grid09.Tag = "frmEntryAccMgt1"; this.lblTitle09.Content = "Accounts Admin";
            this.Grid10.Tag = "frmConfigSetup1"; this.lblTitle10.Content = "Configuration";

            this.MenuItemList.Clear();
            //this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle01.Content.ToString(), Uid = this.Grid01.Tag.ToString() });

            var mnug1 = new MenuItem() { Header = "Sales Management", Uid = "MenuItemGroup" };
            mnug1.Items.Add(new MenuItem() { Header = "Sales Invoice Entry", Uid = "frmEntryGenTrPOS101" });
            mnug1.Items.Add(new MenuItem() { Header = "Sales Reports", Uid = "frmEntryGenTrPOS103" });
            this.MenuItemList.Add(mnug1);

            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle02.Content.ToString(), Uid = this.Grid02.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle03.Content.ToString(), Uid = this.Grid03.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle04.Content.ToString(), Uid = this.Grid04.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle05.Content.ToString(), Uid = this.Grid05.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle06.Content.ToString(), Uid = this.Grid06.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle07.Content.ToString(), Uid = this.Grid07.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle08.Content.ToString(), Uid = this.Grid08.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle09.Content.ToString(), Uid = this.Grid09.Tag.ToString() });
            this.MenuItemList.Add(new MenuItem() { Header = this.lblTitle10.Content.ToString(), Uid = this.Grid10.Tag.ToString() });

        }

        private void ParkTicket()
        {
            this.imgBack.ImageSource = this.imgParkTicket.Source;
            this.Grid01.Tag = "frmEntryParkPOS1"; this.lblTitle01.Content = "Park Ticket Sales";
            this.Grid02.Tag = "frmEntryAttn1"; this.lblTitle02.Content = "Attendence & Leave";
            this.Grid03.Tag = "frmEntryPayroll1"; this.lblTitle03.Content = "Payroll";
            this.Grid04.Tag = "frmEntryHRGenral1"; this.lblTitle04.Content = "HR General Information";
            this.Grid05.Tag = "frmSirCodeBook1"; this.lblTitle05.Content = "Resource Code Book";
            this.Grid06.Tag = "frmEntryItemRcv1"; this.lblTitle06.Content = "Item Purchase/Receive";
            this.Grid07.Tag = "frmEntryStoreIssue1,StockOut"; this.lblTitle07.Content = "Item Transfer/Stock Out";
            this.Grid08.Tag = "frmReportStore1"; this.lblTitle08.Content = "Inventory Report";
            this.Grid09.Tag = "frmEntryInvMgt1"; this.lblTitle09.Content = "Inventory Admin";
            this.Grid10.Tag = "frmConfigSetup1"; this.lblTitle10.Content = "Configuration";
        }

        private void SuperShop()
        {
            this.imgBack.ImageSource = this.imgGTradeShop.Source;// this.imgDocVisit.Source;// null;//            
            this.Grid01.Tag = "frmEntryShopPOS1"; this.lblTitle01.Content = "Sales";
            this.Grid02.Tag = "frmEntryVoucher1,Voucher"; this.lblTitle02.Content = "Accounts Voucher";
            this.Grid03.Tag = "frmEntryInvMgt1"; this.lblTitle03.Content = "Inventory Management";
            this.Grid04.Tag = "frmEntryPur01"; this.lblTitle04.Content = "Procurement";
            this.Grid05.Tag = "frmReportStore1"; this.lblTitle05.Content = "Inventory Report";
            this.Grid06.Tag = "frmReportAcc1"; this.lblTitle06.Content = "Accounting Reports";
            this.Grid07.Tag = "frmEntryAccMgt1"; this.lblTitle07.Content = "Accounts Admin";
            this.Grid08.Tag = "frmConfigSetup1"; this.lblTitle08.Content = "Configuration";
        }
        private void CellPhoneShop()
        {
            this.imgBack.ImageSource = this.imgGTradeShop.Source;// this.imgDocVisit.Source;// null;//            
            this.Grid01.Tag = "frmEntryGenTrPOS1"; this.lblTitle01.Content = "Sales";
            this.Grid02.Tag = "frmEntryItemRcv1,CellPhone"; this.lblTitle02.Content = "Item Purchase/Receive";
            this.Grid03.Tag = "frmEntryStoreIssue1,StockOut"; this.lblTitle03.Content = "Item Transfer/Stock Out";
            this.Grid04.Tag = "frmEntryInvMgt1"; this.lblTitle04.Content = "Inventory Admin";
            this.Grid05.Tag = "frmReportStore1"; this.lblTitle05.Content = "Inventory Report";
            this.Grid06.Tag = "frmEntryAccMgt1"; this.lblTitle06.Content = "Accounts Admin";
            this.Grid07.Tag = "frmConfigSetup1"; this.lblTitle07.Content = "Configuration";
        }

        private void RealEstateMgt()
        {
            this.imgBack.ImageSource = this.imgGTradeShop.Source;// this.imgDocVisit.Source;// null;//            
            this.Grid01.Tag = "frmRealSaleMgt1"; this.lblTitle01.Content = "Sales & Recovery";
            this.Grid02.Tag = "frmEntryVoucher1,Voucher"; this.lblTitle02.Content = "Accounts Voucher";
            this.Grid03.Tag = "frmEntryInvMgt1"; this.lblTitle03.Content = "Inventory Management";
            this.Grid04.Tag = "frmEntryPur01"; this.lblTitle04.Content = "Procurement";
            this.Grid05.Tag = "frmRealBgd101"; this.lblTitle05.Content = "Project Master Budget";
            this.Grid07.Tag = "frmReportAcc1"; this.lblTitle07.Content = "Accounting Reports";
            this.Grid08.Tag = "frmReportStore1"; this.lblTitle08.Content = "Inventory Report";
            this.Grid09.Tag = "frmEntryAccMgt1"; this.lblTitle09.Content = "Accounts Admin";
            this.Grid10.Tag = "frmConfigSetup1"; this.lblTitle10.Content = "Configuration";
        }
      
        #endregion Menu Setup
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.tbArrivalDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy ddd hh:mm tt");
        }

        private void timerClick1_Tick(object sender, EventArgs e)
        {
            if (timerClick1Counter == 0)
            {
                //WpfProcessAccess.ShowBalloon(this.lblBaloon1, "", "Loading information ........ ", BalloonType.Information);
                this.MMGrid1.IsEnabled = false;
                this.MMGrid1.Visibility = Visibility.Hidden;
                this.lblWait1.Visibility = Visibility.Visible;
                timerClick1Counter = 1;
                return;
            }
            this.timerClick1Counter = 0;
            this.timerClick1.Stop();
            this.frm_Show(this.uc1Name, this.frmTag, this.frmTag2);
            this.lblWait1.Visibility = Visibility.Hidden;
        }

        private void frm_Show(string uc1Name1 = "", string frmTag1 = "", string frmTag2 = "")
        {
            for (int k = 0; k < this.UcGrid1.Children.Count; k++)
            {
                string TypeName1 = this.UcGrid1.Children[k].GetType().Name.ToString();
                if (TypeName1.Contains("frm"))
                    this.UcGrid1.Children.Remove(this.UcGrid1.Children[k]);
            }

            if (uc1Name1 == "Nothing")
                return;

            UserControl uc1 = WpfProcessAccess.CreateUserControl(uc1Name1);
            if (uc1 == null)
                return;

            this.lblTitle1.Content = (uc1.Tag == null ? "" : uc1.Tag.ToString());
            uc1.Name = "frmUc1";
            uc1.Tag = (frmTag1.Length > 0 ? frmTag1 : uc1.Tag);
            uc1.Tag = (frmTag2.Length > 0 ? uc1.Tag + "," + frmTag2 : uc1.Tag);
            uc1.Height = 650;
            uc1.Width = 1200;
            uc1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            uc1.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            uc1.Margin = new Thickness((this.UcGrid1.Width - 1200) / 2, 25, 0, 0); // new Thickness((this.UcGrid1.Width - 1200) / 2, 40, 0, 0);

            int i = this.UcGrid1.Children.Count;
            this.UcGrid1.Children.Insert(i, uc1);
            this.ShowAnimation2w(uc1);
            this.UcGrid1.Visibility = Visibility.Visible;
        }

        private void ShowAnimation2w(UserControl uc1)
        {
            var sb = new Storyboard();

            var fade = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(2) };
            Storyboard.SetTarget(fade, this.UcGrid1);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Grid.OpacityProperty));
            sb.Children.Add(fade);

            Random rnd1 = new Random();
            int rnd2 = rnd1.Next(1, 100);

            if (rnd2 % 2 == 0 || rnd2 % 5 == 0 || rnd2 % 7 == 0)
            {
                var movex = new DoubleAnimation() { From = 0, To = uc1.Width, Duration = TimeSpan.FromSeconds(1) };
                Storyboard.SetTarget(movex, this.UcGrid1);
                Storyboard.SetTargetProperty(movex, new PropertyPath(Grid.WidthProperty));
                sb.Children.Add(movex);
            }
            if (rnd2 % 3 == 0 || rnd2 % 5 == 0 || rnd2 % 11 == 0)
            {
                var movey = new DoubleAnimation() { From = 0, To = uc1.Height, Duration = TimeSpan.FromSeconds(1) };
                Storyboard.SetTarget(movey, this.UcGrid1);
                Storyboard.SetTargetProperty(movey, new PropertyPath(Grid.HeightProperty));
                sb.Children.Add(movey);
            }

            sb.Begin();
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (XpValue == 0)
            {
                this.XpValue = this.Width;
                this.YpValue = this.Height;
            }
            this.ResizeZoom(this.Width / this.XpValue * 0.95, this.Height / this.YpValue * 0.95);

            //if (this.WindowState == WindowState.Maximized)
            //{
            //    //SystemParameters.VirtualScreenWidth
            //    //SystemParameters.VirtualScreenHeight
            //    this.Height = SystemParameters.PrimaryScreenHeight;
            //    this.Width = SystemParameters.PrimaryScreenWidth;
            //}


        }

        private void MainWindow1_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.Height = psHeight;// System.Windows.SystemParameters.VirtualScreenHeight;
                this.Width = psWidth;// System.Windows.SystemParameters.VirtualScreenWidth;
                this.Left = 0;
                this.Top = 0;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.Height = psHeight * 0.75;// System.Windows.SystemParameters.VirtualScreenHeight * 0.85;
                this.Width = psWidth * 0.75;// System.Windows.SystemParameters.VirtualScreenWidth * 0.85;
                Rect workArea = SystemParameters.WorkArea;
                this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
                this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IInputElement inputElement = Keyboard.FocusedElement;
                if (inputElement != null)
                {
                    System.Windows.Controls.Primitives.TextBoxBase textBoxBase = inputElement as System.Windows.Controls.Primitives.TextBoxBase;
                    if (textBoxBase != null)
                    {
                        if (!textBoxBase.AcceptsReturn)
                            MoveFocus_Next(textBoxBase);
                        return;
                    }
                    if (
                        MoveFocus_Next(inputElement as ComboBox)
                        ||
                        MoveFocus_Next(inputElement as Button)
                        ||
                        MoveFocus_Next(inputElement as DatePicker)
                        ||
                        MoveFocus_Next(inputElement as CheckBox)
                        ||
                        MoveFocus_Next(inputElement as DataGrid)
                        ||
                        MoveFocus_Next(inputElement as TabItem)
                        ||
                        MoveFocus_Next(inputElement as RadioButton)
                        ||
                        MoveFocus_Next(inputElement as ListBox)
                        ||
                        MoveFocus_Next(inputElement as ListView)
                        ||
                        MoveFocus_Next(inputElement as PasswordBox)
                        ||
                        MoveFocus_Next(inputElement as Window)
                        ||
                        MoveFocus_Next(inputElement as Page)
                        ||
                        MoveFocus_Next(inputElement as Frame)
                    )
                        return;
                }
            }
        }

        private bool MoveFocus_Next(UIElement uiElement)
        {
            if (uiElement != null)
            {
                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                return true;
            }
            return false;
        }

        private void BtnAppClode_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            string scrCaption1 = "";
            for (int k = 0; k < this.UcGrid1.Children.Count; k++)
            {
                string TypeName1 = this.UcGrid1.Children[k].GetType().Name.ToString();
                if (TypeName1.Contains("frm"))
                {
                    scrCaption1 = ((UserControl)this.UcGrid1.Children[k]).Tag.ToString().Trim();
                    this.UcGrid1.Children.Remove(this.UcGrid1.Children[k]);
                }
            }
            this.UcGrid1.Visibility = Visibility.Hidden;
            this.MMGrid1.IsEnabled = true;
            this.MMGrid1.Visibility = Visibility.Visible;
            this.timerClick1Counter = 0;
            this.timerClick1.Stop();
            //WpfProcessAccess.ShowBalloon(this.lblBaloon1, this.Title, scrCaption1 + " Has Been Closed", BalloonType.Information);
        }
        private void tbArrivalDateTime_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.dtpMW1.IsDropDownOpen = true;
        }

        private void Mailto_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:info@asit.com.bd?Subject=The%20subject%20of%20the%20mail");
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.asit.com.bd");
        }

        public void MainGridActivation(Object sender, ExecutedRoutedEventArgs e)
        {

            if (e.Parameter.ToString().Trim() == "0")
                return;
            int i = int.Parse(e.Parameter.ToString().Trim()) - 1;

            Grid[] gridMain = { this.Grid01, this.Grid02, this.Grid03, this.Grid04, this.Grid05, this.Grid06, this.Grid07, this.Grid08, this.Grid09, this.Grid10 };

            if (gridMain[i].Visibility != Visibility.Visible)
                return;

            this.stkBd1_MouseLeftButtonUp(gridMain[i], null);

            //foreach (Expander expn2 in acrMain)
            //    expn2.IsExpanded = false;

            //if (e.Parameter.ToString().Trim() == "0")
            //    return;

            //int index1 = int.Parse(e.Parameter.ToString().Trim()) - 1;
            //acrMain[index1].IsExpanded = true;
            //this.MenukeyNevigate(index1);
        }
    

        public void mnuOperation(string FormTag)
        {
            if (WpfProcessAccess.AppFormsList == null)
                return;

            //WpfProcessAccess.ShowBalloon(lblBaloon1: this.lblBaloon1, caption1:"Pleasw wait while loading information into memory");
            //List<string> List1u = WpfProcessAccess.FormsList();
            //foreach (var item1 in List1u)
            foreach (var item1 in WpfProcessAccess.AppFormsList)
            {
                //if (item1.GetType().ToString().Trim().Contains(((MenuItem)sender).Tag.ToString().Trim()))
                string[] tagPart1 = FormTag.Trim().Split(',');
                //if (item1.GetType().ToString().Trim().Contains(tagPart1[0]))
                if (item1.Contains(tagPart1[0]))
                {
                    string frmTag1 = (tagPart1.Length > 1 ? tagPart1[1] : "");
                    string frmTag2 = (tagPart1.Length > 2 ? tagPart1[2] : "");
                    this.uc1Name = item1;
                    this.frmTag = frmTag1;
                    this.frmTag2 = frmTag2;
                    this.timerClick1.Start();
                    //this.frm_Show(item1, frmTag);
                    return;
                }
            }
        }
        private void stkBd1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid)
            {
                if (((Grid)sender).Tag == null)
                {
                    return;
                }
                this.mnuOperation(((Grid)sender).Tag.ToString().Trim());
            }
            if (sender is MenuItem)
            {
                this.mnuOperation(((MenuItem)sender).Tag.ToString().Trim());
            }
        }


        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (this.UcGrid1.Visibility == Visibility.Hidden)
            //    return;

            ////var scaler = this.UcGrid1.LayoutTransform as ScaleTransform;
            ////var scaler1 = this.MMGrid1.LayoutTransform as ScaleTransform;
            ////if (scaler == null || scaler1 == null)
            ////{
            ////    this.MMGrid1.LayoutTransform = new ScaleTransform(slider1.Value, slider1.Value);
            ////    this.UcGrid1.LayoutTransform = new ScaleTransform(slider1.Value, slider1.Value);
            ////}
            ////else if (scaler.HasAnimatedProperties || scaler1.HasAnimatedProperties)
            ////{
            ////    // Do nothing because the value is being changed by animation.
            ////    // Setting scaler.ScaleX will cause infinite recursion due to the
            ////    // binding specified in the XAML.
            ////}
            ////else
            ////{
            ////    scaler.ScaleX = slider1.Value;
            ////    scaler.ScaleY = slider1.Value;
            ////    scaler1.ScaleX = slider1.Value;
            ////    scaler1.ScaleY = slider1.Value;
            ////}

            this.ResizeZoom(slider1.Value, slider1.Value);
            this.slider1.ToolTip = (slider1.Value).ToString("##0%");
            ////////this.UcGrid1.Margin = new Thickness((this.MainWindow1.Width - this.UcGrid1.Width * slider1.Value) / 2, 90, 0, 0);
            //this.MMGrid1.Margin = new Thickness((this.MainWindow1.Width - this.MMGrid1.Width * slider1.Value) / 2, 80, 0, 0);
        }

        private void ResizeZoom(double XScaleValue, double YScaleValue)
        {
            var scaler = this.UcGrid1.LayoutTransform as ScaleTransform;
            var scaler1 = this.MMGrid1.LayoutTransform as ScaleTransform;
            if (scaler == null || scaler1 == null)
            {
                this.MMGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
                this.UcGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
            }
            else if (scaler.HasAnimatedProperties)
            {
                // Do nothing because the value is being changed by animation.
                // Setting scaler.ScaleX will cause infinite recursion due to the
                // binding specified in the XAML.
            }
            else
            {
                scaler.ScaleX = XScaleValue;
                scaler.ScaleY = YScaleValue;
                scaler1.ScaleX = XScaleValue;
                scaler1.ScaleY = YScaleValue;
            }
            //this.slider1.ToolTip = (slider1.Value).ToString("##0%");
        }


        private void SliderZoom(object sender, ExecutedRoutedEventArgs e)
        {
            //int.Parse(e.Parameter.ToString().Trim()) - 1;
            if (e.Parameter.ToString().Trim() == "ZoomOut")
                slider1.Value += 0.1;// slider1.TickFrequency;
            else
                slider1.Value -= 0.1;
        }

        private void MainWindow1_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.Visibility != Visibility.Visible)
            {
                Application.Current.Shutdown();
                return;
            }

            if (System.Windows.MessageBox.Show("Are you confirm to close application", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Cancel, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
            else
                e.Cancel = true;
        }


        private void AddContextMenu()
        {

            ContextMenu cm1 = new ContextMenu() { FontSize = 14, FontWeight = FontWeights.Bold };
            foreach (MenuItem mnui1 in this.MenuItemList)
            {
                if (mnui1.Uid.ToString().Contains("MenuItemGroup"))
                {
                    var mnui2 = new MenuItem() { Header = mnui1.Header };
                    foreach (MenuItem citem1 in mnui1.Items)
                    {
                        string[] muid2 = citem1.Uid.ToString().Split(',');
                        if (WpfProcessAccess.AppFormsList.FirstOrDefault(x => x.Contains(muid2[0])) != null)
                        {
                            var mnui2c = new MenuItem() { Header = citem1.Header, Uid = citem1.Uid, Tag = "Window", Height = 25 };
                            mnui2c.Click += this.MenuItem1_Click;
                            mnui2.Items.Add(mnui2c);
                        }
                    }
                    if (mnui2.Items.Count > 0)
                        cm1.Items.Add(mnui2);
                }
                else
                {
                    string[] muid1 = mnui1.Uid.ToString().Split(',');
                    if (WpfProcessAccess.AppFormsList.FirstOrDefault(x => x.Contains(muid1[0])) != null)
                    {
                        var mnui1c = new MenuItem() { Header = mnui1.Header, Uid = mnui1.Uid, Tag = "Window", Height = 25 };
                        mnui1c.Tag = "Window";
                        mnui1c.Height = 25;
                        mnui1c.Click += this.MenuItem1_Click;
                        cm1.Items.Add(mnui1c);
                    }
                }
            }
            if (cm1.Items.Count > 0)
            {
                cm1.Items.Add(new Separator());
                MenuItem mir18 = new MenuItem() { Header = "_Close All Windows", Tag = "ClearWindows", Uid = "CLEAR_ALL_WINDOWS", Height = 25 };
                mir18.Click += this.MenuItem1_Click;
                cm1.Items.Add(mir18);
            }
            //MenuItem mir1 = new MenuItem() { Header = "New Window", Tag = "Window", Uid = "ADD_NEW_WINDOW", Height = 35 };
            //mir1.Click += this.MenuItem1_Click;
            //cm1.Items.Add(mir1);

            //MenuItem mir1e = new MenuItem() { Header = "New Extended Window", Tag = "ExtWindow", Uid = "ADD_NEW_EXT_WINDOW", Height = 35 };
            //mir1e.Click += this.MenuItem1_Click;
            //cm1.Items.Add(mir1e);

            //cm1.Items.Add(new Separator());
            //MenuItem mir3 = new MenuItem() { Header = "New Tab", Tag = "Tab", Uid = "ADD_NEW_CUSTOM_TAB", Height = 35 };
            //mir3.Click += this.MenuItem1_Click;
            //cm1.Items.Add(mir3);



            //cm1.Items.Add(new Separator());
            //MenuItem mir19 = new MenuItem() { Header = "Clear All Tabs", Tag = "ClearTabs", Uid = "CLEAR_ALL_TABS", Height = 35 };
            //mir19.Click += this.MenuItem1_Click;
            //cm1.Items.Add(mir19);

            cm1.Items.Add(new Separator());
            MenuItem mir20 = new MenuItem() { Header = "E_xit Application", Tag = "EXIT", Uid = "EXIT_APPLICATION", Height = 35 };
            mir20.Click += this.MenuItem1_Click;
            cm1.Items.Add(mir20);
            this.imgClIcon.ContextMenu = cm1;
            this.MMGrid1.ContextMenu = cm1;
            this.btnASIT.ContextMenu = cm1;
        }
        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            string ObjectTag = ((MenuItem)sender).Tag.ToString();
            string ObjUid = ((MenuItem)sender).Uid.ToString();

            switch (ObjectTag)
            {
                case "EXIT":
                    this.Close();
                    break;
                case "Window":

                    if (WpfProcessAccess.AppFormsList == null)
                        return;

                    foreach (var item1 in WpfProcessAccess.AppFormsList)
                    {
                        //if (item1.GetType().ToString().Trim().Contains(((MenuItem)sender).Tag.ToString().Trim()))
                        string[] tagPart1 = ObjUid.Trim().Split(',');
                        //if (item1.GetType().ToString().Trim().Contains(tagPart1[0]))
                        if (item1.Contains(tagPart1[0]))
                        {
                            string frmTag1 = (tagPart1.Length > 1 ? tagPart1[1] : "");
                            string frmTag2 = (tagPart1.Length > 2 ? tagPart1[2] : "");
                            this.uc1Name = item1;
                            this.frmTag = frmTag1;
                            this.frmTag2 = frmTag2;
                            break;
                            //this.timerClick1.Start();
                            //this.frm_Show(item1, frmTag);
                            //return;
                        }
                    }

                    UserControl uc1 = WpfProcessAccess.CreateUserControl(this.uc1Name); //WpfProcessAccess.CreateUserControl(uc1Name1);
                    if (uc1 == null)
                        return;

                    uc1.Tag = (this.frmTag.Length > 0 ? this.frmTag : uc1.Tag);
                    uc1.Tag = (this.frmTag2.Length > 0 ? uc1.Tag + "," + this.frmTag2 : uc1.Tag);

                    HmsChildWindow win1 = new HmsChildWindow(uc1, this.frmTag, this.frmTag2) { Owner = this };
                    win1.Top = this.Height;
                    win1.Left = this.Width * -1;
                    win1.Show();
                    win1.Height = this.Height - this.Height * 0.25;// 130;
                    win1.Width = this.Width - this.Width * 0.25;// 80;
                    //win1.Top = this.Top + this.Height * 0.25 / 4 + 90;
                    //win1.Left = this.Left + this.Width * 0.25 / 2;
                    var Top1 = this.Top + this.Height * 0.25 / 4 + 90;
                    var Left1 = this.Left + this.Width * 0.25 / 2;

                    var sb = new Storyboard();


                    //Random rnd1 = new Random();
                    //int rnd2 = rnd1.Next(1, 100);

                    //if (rnd2 % 2 == 0 || rnd2 % 5 == 0 || rnd2 % 7 == 0)
                    //{
                    //    var movex = new DoubleAnimation() { From = 0, To = win1.Width, Duration = TimeSpan.FromSeconds(1) };
                    //    Storyboard.SetTarget(movex, win1);
                    //    Storyboard.SetTargetProperty(movex, new PropertyPath(Grid.WidthProperty));
                    //    sb.Children.Add(movex);
                    //}
                    //if (rnd2 % 3 == 0 || rnd2 % 5 == 0 || rnd2 % 11 == 0)
                    //{
                    //    var movey = new DoubleAnimation() { From = 0, To = win1.Height, Duration = TimeSpan.FromSeconds(1) };
                    //    Storyboard.SetTarget(movey, win1);
                    //    Storyboard.SetTargetProperty(movey, new PropertyPath(Grid.HeightProperty));
                    //    sb.Children.Add(movey);
                    //}

                    var moveX = new DoubleAnimation(Left1, new Duration(TimeSpan.FromSeconds(2)));
                    Storyboard.SetTarget(moveX, win1);
                    Storyboard.SetTargetProperty(moveX, new PropertyPath("(Canvas.Left)"));
                    sb.Children.Add(moveX);

                    var moveY = new DoubleAnimation(Top1, new Duration(TimeSpan.FromSeconds(2)));
                    Storyboard.SetTarget(moveY, win1);
                    Storyboard.SetTargetProperty(moveY, new PropertyPath("(Canvas.Top)"));
                    sb.Children.Add(moveY);

                    var fade = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(2) };
                    Storyboard.SetTarget(fade, win1);
                    Storyboard.SetTargetProperty(fade, new PropertyPath(Grid.OpacityProperty));
                    sb.Children.Add(fade);

                    sb.Begin();


                    break;

                case "ClearWindows":
                    foreach (Window item in this.OwnedWindows)
                        item.Close();
                    break;
                default:
                    break;
            }
        }
        public void ShowContextMenue(Object sender, ExecutedRoutedEventArgs e)
        {
            this.btnASIT_Click(null, null);
        }
        private void btnASIT_Click(object sender, RoutedEventArgs e)
        {
            this.btnASIT.ContextMenu.PlacementTarget = this.btnASIT; //sender as UIElement;
            this.btnASIT.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
            this.btnASIT.ContextMenu.IsOpen = true;
        }

    }
}
