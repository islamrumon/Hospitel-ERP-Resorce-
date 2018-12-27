using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Media.Animation;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for mnuFlowChart1.xaml
    /// </summary>
    public partial class mnuFlowChart1 : UserControl
    {
        public mnuFlowChart1()
        {
            InitializeComponent();
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }

        public void SetTitleImage(BitmapImage bmp3a)
        {
            this.imgGoStartUp.Source = bmp3a;
        }
        public void ShowTabInformation(int SelectIndex1 = 0)
        {
            //this.mnuTabCtrl1.SelectedIndex = SelectIndex1;
            //this.gridMainCtrl.Width = this.gridMainCtrl.Width - 10;
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
            switch (SelectIndex1)
            {
                case 01:
                    this.FrontDesk();
                    break;
                case 02:
                    this.LabOperation();
                    break;
                case 03:
                    this.Inventory();
                    break;
                case 04:
                    this.Accounts();
                    break;
                case 05:
                    this.Marketing();
                    break;
                case 06:
                    this.HRManagement();
                    break;
                case 07:
                    this.AdminPanel();
                    break;
                case 08:
                    this.SetupUtility();
                    break;
                case 09:
                    this.MISReports();
                    break;
                case 10:
                    this.OthersOperation();
                    break;
                default: // 00
                    this.MenuStartUp();
                    break;
            }

            this.ShowAnimation();
        }

        private void ShowAnimation()
        {
            var fade = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(3) };

            Storyboard.SetTarget(fade, this.wrp1a);
            Storyboard.SetTargetProperty(fade, new PropertyPath(WrapPanel.OpacityProperty));

            var sb = new Storyboard();
            sb.Children.Add(fade);
            sb.Begin();

            //SolidColorBrush brush = new SolidColorBrush(Colors.LightYellow);
            //this.wrp1a.Background = brush;
            //ColorAnimation ca1 = new ColorAnimation()
            //{
            //    From = Colors.LightYellow,
            //    To = Colors.Transparent,
            //    Duration = new Duration(TimeSpan.FromSeconds(2.0)),
            //    RepeatBehavior = new RepeatBehavior(1),
            //    AutoReverse = false,
            //};

            //brush.BeginAnimation(SolidColorBrush.ColorProperty, ca1);

        }

        #region Menu Setup

        private void MenuStartUp()      // 00
        {
            this.lblMenuTite.Content = "Main Menu";
            this.Grid01.Tag = "01"; this.Grid02.Tag = "02"; this.Grid03.Tag = "03"; this.Grid04.Tag = "04"; this.Grid05.Tag = "05";
            this.Grid06.Tag = "06"; this.Grid07.Tag = "07"; this.Grid08.Tag = "08"; this.Grid09.Tag = "09"; this.Grid10.Tag = "10";

            this.lblTitle01.Content = "Front Desk";
            this.lblTitle02.Content = "Lab Reporting";
            this.lblTitle03.Content = "Inventory & SCM";
            this.lblTitle04.Content = "Accounts";
            this.lblTitle05.Content = "Marketing";
            this.lblTitle06.Content = "HCM & Payroll";
            this.lblTitle07.Content = "Admin Panel";
            this.lblTitle08.Content = "Setup & Utilities";
            this.lblTitle09.Content = "MIS Reports";
            this.lblTitle10.Content = "Others Operation";
        }
        private void FrontDesk()      // 01
        {
            this.lblMenuTite.Content = "Front Desk Menu";
            this.Grid01.Tag = "frmEntryFrontDesk101"; this.lblTitle01.Content = "Receiption";
            this.Grid02.Tag = "frmEntryFrontDesk102"; this.lblTitle02.Content = "Help Desk";
            this.Grid03.Tag = "frmEntryDocVisit1"; this.lblTitle03.Content = "Patient Visit Token";
            this.Grid04.Tag = "frmEntryFrontDesk3_Old"; this.lblTitle04.Content = "Others Entry";
            this.Grid05.Tag = "frmEntryFrontDesk103"; this.lblTitle05.Content = "Front Desk Reports";
            this.Grid06.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle06.Content = "";
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle07.Content = "";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }

        private void LabOperation()     // 02
        {
            this.lblMenuTite.Content = "Lab Operation Menu";
            this.Grid01.Tag = "frmEntryLabMagt101,Speciman Receive for Diagnosis"; this.lblTitle01.Content = "Specimen Receive";
            this.Grid02.Tag = "frmEntryLabMagt101,Diagnosis Report Document Preparation"; this.lblTitle02.Content = "Report Preparation";
            this.Grid03.Tag = "frmEntryLabMagt101,Diagnosis Report Document Submission"; this.lblTitle03.Content = "Report Submission";
            this.Grid04.Tag = "frmEntryLabReport1"; this.lblTitle04.Content = "Lab Status Reports";
            this.Grid05.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle05.Content = "";
            this.Grid06.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle06.Content = ""; ;
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle07.Content = "";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }

     
        private void Inventory()    // 03
        {
            this.lblMenuTite.Content = "Inventory & SCM Menu";
            this.Grid01.Tag = "frmEntryStoreReq1"; this.lblTitle01.Content = "Store Requisition";
            this.Grid02.Tag = "frmEntryStoreIssue1"; this.lblTitle02.Content = "Item Issue/Transfer";
            this.Grid03.Tag = "frmEntryItemRcv1"; this.lblTitle03.Content = "Item Purchase/Receive";
            this.Grid04.Tag = "frmEntryPurReq1"; this.lblTitle04.Content = "Purchase Requisition";
            this.Grid05.Tag = "frmEntryPurQuotation1"; this.lblTitle05.Content = "Purchase Quotation";
            this.Grid06.Tag = "frmEntryPurOrder1"; this.lblTitle06.Content = "Purchase Order";
            this.Grid07.Tag = "frmEntryPurLCInfo1"; this.lblTitle07.Content = "Purchase Through L/c";
            this.Grid08.Tag = "frmEntryPurBillRcv1"; this.lblTitle08.Content = "Purchase Bill Receive";
            this.Grid09.Tag = "frmEntryItemStock1"; this.lblTitle09.Content = "Physical Stock Entry";
            this.Grid10.Tag = "frmReportStore1"; this.lblTitle10.Content = "Inventory & SCM Reports";
        }
        private void Accounts()     // 04
        {
            this.lblMenuTite.Content = "Accounts Management";
            this.Grid01.Tag = "frmEntryVoucher1,Voucher"; this.lblTitle01.Content = "General Voucher";
            this.Grid02.Tag = "frmEntryVoucher1,Payment Voucher"; this.lblTitle02.Content = "Payment Voucher";
            this.Grid03.Tag = "frmEntryVoucher1,Receipt Voucher"; this.lblTitle03.Content = "Receipt Voucher";
            this.Grid04.Tag = "frmEntryVoucher1,Journal Voucher"; this.lblTitle04.Content = "Journal Voucher";
            this.Grid05.Tag = "frmEntryVoucher1,Fund Transfer Voucher"; this.lblTitle05.Content = "Fund Transfer Voucher";
            this.Grid06.Tag = "frmEntryBankRecon1"; this.lblTitle06.Content = "Bank Reconciliation";
            this.Grid07.Tag = "frmEntryPayPro1,Payment Proposal"; this.lblTitle07.Content = "Payment Proposal Entry";
            this.Grid08.Tag = "frmEntryVoucher1,Opening Voucher"; this.lblTitle08.Content = "Opening Voucher";
            this.Grid09.Tag = "frmReportAcc1"; this.lblTitle09.Content = "Accounting Reports";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }

        private void Marketing()    // 05
        {
            this.lblMenuTite.Content = "Marketing Management";
            this.Grid01.Tag = "frmEntryMarketing1,Visit Reporting"; this.lblTitle01.Content = "Visit Reporting";
            this.Grid02.Tag = "frmEntryMarketing1,Job Assigning"; this.lblTitle02.Content = "Job Assigning";
            this.Grid03.Tag = "frmEntryMarketing1,Evaluation"; this.lblTitle03.Content = "Evaluation";
            this.Grid04.Tag = "frmReportMarketing1"; this.lblTitle04.Content = "Marketing Reports";
            this.Grid05.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle05.Content = "";
            this.Grid06.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle06.Content = ""; ;
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle07.Content = "";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }    
        private void HRManagement()     // 06
        {
            this.lblMenuTite.Content = "HR & Payroll";
            this.Grid01.Tag = "frmEntryAttn101"; this.lblTitle01.Content = "Attendence Schedule";
            this.Grid02.Tag = "frmEntryAttn1"; this.lblTitle02.Content = "Attendence Entry";
            this.Grid03.Tag = "frmEntryAttn104"; this.lblTitle03.Content = "Leave Information Entry";
            this.Grid04.Tag = "frmEntryPayroll101"; this.lblTitle04.Content = "Payroll Process";
            this.Grid05.Tag = "frmEntryRecruit1"; this.lblTitle05.Content = "Recruitment";
            this.Grid06.Tag = "frmEntryHRGenral1"; this.lblTitle06.Content = "HR General Information"; 
            this.Grid07.Tag = "frmReportHCM1"; this.lblTitle07.Content = "HR & Payroll Reports";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }
   
        private void AdminPanel()   // 07
        {
            this.lblMenuTite.Content = "Admin Panel";
            this.Grid01.Tag = "frmEntryAccMgt1"; this.lblTitle01.Content = "Accounts Management";
            this.Grid02.Tag = "frmEntryInvMgt1"; this.lblTitle02.Content = "Inventory Management";
            this.Grid03.Tag = "frmEntryLabMagt107"; this.lblTitle03.Content = "Lab Report Templete";
            this.Grid04.Tag = "frmReportAdmin1"; this.lblTitle04.Content = "Admin Reports";
            this.Grid05.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle05.Content = ""; ;
            this.Grid06.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle06.Content = "";
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle07.Content = "";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }
        private void SetupUtility()     // 08
        {
            this.lblMenuTite.Content = "Setup & Utility Menu";
            this.Grid01.Tag = "frmAccCodeBook1"; this.lblTitle01.Content = "Accounts Code";
            this.Grid02.Tag = "frmSirCodeBook1"; this.lblTitle02.Content = "Resource Code";
            this.Grid03.Tag = "frmSectCodeBook1"; this.lblTitle03.Content = "Location Code";
            this.Grid04.Tag = "frmOtherCodeBook1"; this.lblTitle04.Content = "Other Details Code";
            this.Grid05.Tag = "frmConfigSetup1"; this.lblTitle05.Content = "Configuration Setup";
            this.Grid06.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle06.Content = ""; ;
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle07.Content = "";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }
        private void MISReports()   // 09
        {
            this.lblMenuTite.Content = "MIS Reports Menu";
            this.Grid01.Tag = "frmMISGeneral1"; this.lblTitle01.Content = "General MIS Reports";
            this.Grid02.Tag = "frmMISHospital1"; this.lblTitle02.Content = "Healthcare MIS Reports";
            this.Grid03.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle03.Content = "";
            this.Grid04.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle04.Content = "";
            this.Grid05.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle05.Content = "";
            this.Grid06.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle06.Content = ""; ;
            this.Grid07.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle07.Content = "";
            this.Grid08.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle08.Content = "";
            this.Grid09.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle09.Content = "";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }
        private void OthersOperation()  //  10
        {
            this.lblMenuTite.Content = "Others Operation Menu";
            this.Grid01.Tag = "frmEntryPharmaPOS1"; this.lblTitle01.Content = "Medicine Sales Invoice";
            this.Grid02.Tag = "frmEntryRestauPOS1"; this.lblTitle02.Content = "Restaurant Sales Invoice";
            this.Grid03.Tag = "frmEntryGenTrPOS1"; this.lblTitle03.Content = "General Trading Sales";
            this.Grid04.Tag = "frmEntryParkPOS1"; this.lblTitle04.Content = "Park Ticket Sales";
            this.Grid05.Tag = "frmEntryShopPOS1"; this.lblTitle05.Content = "Super Shop Sales";
            this.Grid06.Tag = "frmReportPharmaPOS1"; this.lblTitle06.Content = "Medicine Sales Reports";
            this.Grid07.Tag = "frmRealSaleMgt1"; this.lblTitle07.Content = "Real Estate Sales";
            this.Grid08.Tag = "frmRealBgd101"; this.lblTitle08.Content = "Real Estate Master Budget";
            this.Grid09.Tag = "frmMessagegMgt1"; this.lblTitle09.Content = "Message Management";
            this.Grid10.Tag = "frmXXXXXXXXXXXXXXX"; this.lblTitle10.Content = "";
        }

        //private void MedicineShop()
        //{
        //    this.Grid01.Tag = "frmEntryPharmaPOS1"; this.lblTitle01.Content = "Sales";
        //    this.Grid02.Tag = "frmReportPharmaPOS1"; this.lblTitle02.Content = "Sales Report";
        //    this.Grid03.Tag = "frmEntryItemRcv1"; this.lblTitle03.Content = "Item Receive";
        //    this.Grid04.Tag = "frmReportStore1"; this.lblTitle04.Content = "Inventory Report";
        //    this.Grid05.Tag = "frmEntryInvMgt1"; this.lblTitle05.Content = "Admin Panel";
        //    this.Grid06.Tag = "frmOtherCodeBook1"; this.lblTitle06.Content = "Setup and Utilities"; ;
        //    this.Grid07.Tag = "frmConfigSetup1"; this.lblTitle07.Content = "Configuration";
        //}

        //private void DoctorVisit()
        //{
        //    //this.Grid01.Visibility = Visibility.Visible;
        //    //this.imgBack.ImageSource = this.imgDocVisit.Source;// null;//
        //    this.Grid01.Tag = "frmEntryDocVisit1"; this.lblTitle01.Content = "Visiting Token Entry";
        //}

        #endregion Menu Setup

        public void Menu_Operation(string FormTag)
        {

        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            HmsMainWindow2 win = (HmsMainWindow2)Window.GetWindow(this);
            win.mnuOperation(((Button)sender).Tag.ToString().Trim());

        }

        private void BtnDshbd_Click(object sender, RoutedEventArgs e)
        {
            //HmsMainWindow win = (HmsMainWindow)Window.GetWindow(this);
            //win.FlowActivation(sender, e);
        }

        private void mnuiAll_Click(object sender, RoutedEventArgs e)
        {

            HmsMainWindow2 win = (HmsMainWindow2)Window.GetWindow(this);
            win.mnuiAll_Click(sender, e);
            //if (sender is Grid)
            //    win.mnuiAll_Click(sender, e);
            //else
            //    win.mnuiAll_Click(sender, e);
        }

        private void stkBd1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HmsMainWindow2 win = (HmsMainWindow2)Window.GetWindow(this);

            if (this.Grid01.Tag.ToString() != "01")
                win.mnuiAll_Click(sender, null);
            else
                win.FlowActivation(sender, e);
            // win.mnuiAll_Click(sender, e);
        }

   
        private void imgGoStartUp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HmsMainWindow2 win = (HmsMainWindow2)Window.GetWindow(this);
            win.FlowActivation(new Grid() { Tag = "0" }, null);
        }

 


    }
}
