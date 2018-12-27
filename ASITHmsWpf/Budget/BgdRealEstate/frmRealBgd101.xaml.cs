using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Accounting;
using ASITHmsViewMan.Budget;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt1GenAcc.Accounting;


namespace ASITHmsWpf.Budget.BgdRealEstate
{
    /// <summary>
    /// Interaction logic for frmRealBgd101.xaml
    /// </summary>
    public partial class frmRealBgd101 : UserControl
    {
        string TitaleTag1, TitaleTag2, TitaleTag3;  // 

        private List<vmBgdRealEstate1.ProjectMasterBudged> LstMasterBudget = new List<vmBgdRealEstate1.ProjectMasterBudged>();

        private bool FrmInitialized = false;

        vmBgdRealEstate1 vm1 = new vmBgdRealEstate1();
        //vmEntryPrjMgt1 vm1 = new vmEntryPrjMgt1();
        public frmRealBgd101()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            this.TitaleTag1 = this.Tag.ToString();   // Predefined value of Tag property set at design time
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;
                this.ActivateAuthObjects();
                this.Objects_On_Init();
                this.CleanUpScreen();
            }
        }
        private void ActivateAuthObjects()
        {

        }
        private void Objects_On_Init()
        {
            try
            {
                var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
                foreach (var itemd1 in deptList1)
                {
                    if (itemd1.sectname.ToUpper().Contains("PROJECT")) // itemd1.sectname.ToUpper().Contains("STORE") || itemd1.sectname.ToUpper().Contains("PROJECT")
                    {
                        this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                    }
                }
                this.xctk_dtpBgdDate.Value = DateTime.Today;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("BGD-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void CleanUpScreen()
        {
            this.dgBgd1.ItemsSource = null;
            this.LstMasterBudget.Clear();
            this.btnUpdateTrans.Visibility = Visibility.Hidden;
            this.btnPrintTrans.Visibility = Visibility.Hidden;
            this.stkpEntry1.Visibility = Visibility.Collapsed;
            this.lblBgdNum.Content = "BGMMM-CCCC-XXXXX";
            this.lblBgdNum.Tag = "";
            this.xctk_dtpBgdDate.Value = DateTime.Today;
            this.stkpEntry1.IsEnabled = false;
            this.stkpTitleMaster.IsEnabled = true;
        }

        private void btnNextShow_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnNextShow.Content.ToString() == "_Next")
            {
                this.CleanUpScreen();
                this.btnNextShow.Content = "_Ok";
                return;
            }
            this.GetBudgetData();
            this.stkpTitleMaster.IsEnabled = false;
            this.btnNextShow.Content = "_Next";
        }

        private void GetBudgetData()
        {
            string SectCode1a = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            string BgdDate1a = this.xctk_dtpBgdDate.Text;
            //SectCode1 = "130100201001", string BgdNum1 = "BGM201806130100004"
            var pap1 = vm1.SetParamShowBudgetInfo(WpfProcessAccess.CompInfList[0].comcod, SectCode1: SectCode1a, BgdNum1: "BGM");
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1); //Success
            if (ds1 == null)
                return;

            this.LstMasterBudget = ds1.Tables[0].DataTableToList<vmBgdRealEstate1.ProjectMasterBudged>();
            this.dgBgd1.ItemsSource = this.LstMasterBudget;
            this.btnUpdateTrans.Visibility = Visibility.Visible;
            this.stkpEntry1.Visibility = Visibility.Visible;
            this.stkpEntry1.IsEnabled = true;
            this.btnTotal_Click(null, null);
            if (ds1.Tables[1].Rows.Count > 0)
            {
                this.lblBgdNum.Content = ds1.Tables[1].Rows[0]["memonum1"].ToString();
                this.lblBgdNum.Tag = ds1.Tables[1].Rows[0]["memonum"].ToString();
                this.xctk_dtpBgdDate.Value = Convert.ToDateTime(ds1.Tables[1].Rows[0]["bgddat"]);
                this.btnPrintTrans.Visibility = Visibility.Visible;
            }

        }

        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            //SetParamUpdateBudget(string CompCode, string bgdnum1, string BgdDate1, string BgdType1, 
            //List<ProjectMasterBudged> LstMasterBudget1, string _preparebyid, string _prepareses, string _preparetrm)
            this.btnTotal_Click(null, null);
            var BgdDate1a = this.xctk_dtpBgdDate.Text;
            var SectCode1a = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            var bgdnum1a = ((ComboBoxItem)this.cmbBgdType.SelectedItem).Tag.ToString() + DateTime.Parse(BgdDate1a).ToString("yyyyMM") + SectCode1a.Substring(0, 4);// +"00001";
            if (this.lblBgdNum.Tag.ToString().Trim().Length > 0)
                bgdnum1a = this.lblBgdNum.Tag.ToString().Trim();

            var BgdType1a = "MASTERBGD";
            var LstMasterBudget1 = this.LstMasterBudget.FindAll(x => x.bgdam > 0);
            var pap1 = vm1.SetParamUpdateBudget(WpfProcessAccess.CompInfList[0].comcod, LstMasterBudget1: LstMasterBudget1, bgdnum1: bgdnum1a,
                       BgdDate1: BgdDate1a, sectcod1: SectCode1a, BgdType1: BgdType1a, _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode,
                       _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1); //Success
            if (ds1 == null)
                return;

            this.lblBgdNum.Content = ds1.Tables[0].Rows[0]["memonum1"].ToString();
            this.lblBgdNum.Tag = ds1.Tables[0].Rows[0]["memonum"].ToString();
            this.btnPrintTrans.Visibility = Visibility.Visible;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnMarkAll_Click(object sender, RoutedEventArgs e)
        {
            bool Mark1a = (((Button)sender).Content.ToString().Contains("Un") ? false : true);
            foreach (var item in this.LstMasterBudget)
                item.Mark1 = Mark1a;

            this.dgBgd1.Items.Refresh();
        }

        private void btnNav_Click(object sender, RoutedEventArgs e)
        {

            if (this.dgBgd1.Items.Count == 0)
                return;

            if (this.dgBgd1.SelectedIndex < 0)
                this.dgBgd1.SelectedIndex = 0;

            string ActtionName = ((Button)sender).Name.ToString().Trim();
            int index1 = this.dgBgd1.SelectedIndex;
            switch (ActtionName)
            {
                case "btnTop":
                    index1 = 0;
                    break;
                case "btnPrev":
                    index1 = this.dgBgd1.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnNext":
                    index1 = this.dgBgd1.SelectedIndex + 1;
                    if (index1 >= this.dgBgd1.Items.Count)
                        index1 = this.dgBgd1.Items.Count - 1;
                    break;
                case "btnBottom":
                    index1 = this.dgBgd1.Items.Count - 1;
                    break;
            }
            this.dgBgd1.SelectedIndex = index1;

            var item21 = (vmBgdRealEstate1.ProjectMasterBudged)this.dgBgd1.Items[index1];
            this.dgBgd1.ScrollIntoView(item21);
        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            this.dgBgd1.ItemsSource = null;
            foreach (var item in this.LstMasterBudget)
                item.bgdam = item.bgdqty * item.bgdrate;

            var LstMasterBudgetGroup = this.LstMasterBudget.FindAll(x => x.sircode.Substring(9, 3) == "000").OrderBy(y => y.sircode);
            foreach (var item in LstMasterBudgetGroup)
                item.tbgdam = this.LstMasterBudget.FindAll(x => x.sircode.Substring(0, 9) == item.sircode.Substring(0, 9)).Sum(y => y.bgdam);

            this.txtTotalAmt.Text = this.LstMasterBudget.Sum(y => y.tbgdam).ToString("#,##0.00");
            this.dgBgd1.ItemsSource = this.LstMasterBudget;
        }

        private void btnPrintTrans_Click(object sender, RoutedEventArgs e)
        {
            string SectCode1a = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            string SectName1a = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Content.ToString();
            string BgdDate1a = this.xctk_dtpBgdDate.Text;
            //SectCode1 = "130100201001", string BgdNum1 = "BGM201806130100004"
            var pap1 = vm1.SetParamShowBudgetReport(WpfProcessAccess.CompInfList[0].comcod, SectCode1: SectCode1a, BgdNum1: "BGM");
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1); //Success
            if (ds1 == null)
                return;

            var list1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.MasterBgdRpt1>();
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            list3[0].RptHeader1 = "MASTER BUDGET";
            list3[0].RptHeader2 = SectName1a;
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptBgdProject01", list1, null, list3, null);
            string WindowTitle1 = "Budget Report";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

            /*             
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccVoucher1>();
                var trnsList = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTransectionList>();
                // select preparebyid, PreparByName, prepareses, preparetrm, rowid, rowtime, ServerTime = getdate() from #tblv1
                string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                                    + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
                string memoName = memoNum.Substring(0, 3).Trim();
                byte[] comlogoBytes = WpfProcessAccess.CompInfList[0].comlogo;

                HmsEntityAccounting.AccVoucher1p list4 = new HmsEntityAccounting.AccVoucher1p();
                list4.comlogo = comlogoBytes;
                list4.inWord = ASITFunLib.ASITUtility.Trans(double.Parse(list1.Sum(q => q.cramt).ToString()), 2);
                //l.inWord = ASITFunLib.ASITUtility2.UppercaseWords("");
                string rptName = (prnFrom == "VOUCHER" ? "Accounting.RptAccVou1" + PaperType : (prnFrom == "CHEQUE" ? "Accounting.RptAccPayCheq1" : (prnFrom == "MRECEIPT" ? 
                        "Accounting.RptAccMReceipt1" : "")));
                // (list1.Count > 7 ? "Accounting.RptAccVou1" : "Accounting.RptAccVou1h");
                rpt1 = AccReportSetup.GetLocalReport(rptName, list1, trnsList, list3, list4);
                //rpt1.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(bytes)));
                string WindowTitle1 = (prnFrom == "VOUCHER" ? "Accounts Voucher" : (prnFrom == "CHEQUE" ? "Payment/Transfer Cheque" : (prnFrom == "MRECEIPT" ? "Money Receipt" : "")));
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);              
             */
        }

    }
}
