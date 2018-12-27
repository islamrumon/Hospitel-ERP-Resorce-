using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using ASITHmsEntity;
using ASITFunLib;
using ASITHmsViewMan.Accounting;
using ASITHmsViewMan.Budget;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt1GenAcc.Accounting;

namespace ASITHmsWpf.Budget.BgdAccounts
{
    /// <summary>
    /// Interaction logic for frmAccBgd102.xaml
    /// </summary>
    public partial class frmAccBgd102 : UserControl
    {
        private bool FrmInitialized = false;
        string TitaleTag1, TitaleTag2, TitaleTag3;  // 

        List<HmsEntityAccounting.ApprovalSheet> ApplyList = new List<HmsEntityAccounting.ApprovalSheet>();
        List<HmsEntityAccounting.ApprovalSheet> ApprovedList = new List<HmsEntityAccounting.ApprovalSheet>();
        List<HmsEntityAccounting.SixMonthStatus> SixMonthList = new List<HmsEntityAccounting.SixMonthStatus>();

        vmBgdAccounts1 vm1 = new vmBgdAccounts1();

        public frmAccBgd102()
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
                this.InitializeProposalInfo();
            }
        }


        private void InitializeProposalInfo()
        {
            this.btnUpdateProposal.Visibility = Visibility.Hidden;
            this.btnPrintProposal.Visibility = Visibility.Hidden;
            this.cmbRptName.Visibility = Visibility.Hidden;
            this.stkpProposalDetails.Visibility = Visibility.Collapsed;

            this.cmbBpppMonth.Items.Clear();
            DateTime setMonth = DateTime.Today.AddMonths(-6);
            for (int i = 1; i <= 12; i++)
            {
                this.cmbBpppMonth.Items.Add(new ComboBoxItem { Content = setMonth.ToString("MMM-yyyy"), Tag = setMonth.ToString("yyyyMM") });
                setMonth = setMonth.AddMonths(1);
            }
            this.cmbBpppMonth.SelectedIndex = 6;

            this.cmbBppBrn.Items.Clear();
            this.cmbBppBrn.Items.Add(new ComboBoxItem() { Content = "ALL BRANCHES", Tag = "0000" });
            var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            foreach (var itemb in brnList)
                this.cmbBppBrn.Items.Add(new ComboBoxItem() { Content = itemb.brnnam.Trim() + " (" + itemb.brnsnam.Trim() + ")", Tag = itemb.brncod });
            this.cmbBppBrn.SelectedIndex = 0;

            this.cmbPTCash.Items.Clear();
            this.cmbPTCash.Items.Add(new ComboBoxItem() { Content = "GENERAL CASH/BANK", Tag = "0000" });
            if (WpfProcessAccess.AccCodeList == null)
                WpfProcessAccess.GetAccCodeList();

            var ptcashList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(0, 8) == "19010002" && x.actcode.Substring(8, 4) != "0000");
            foreach (var itemp in ptcashList)
            {
                this.cmbPTCash.Items.Add(new ComboBoxItem() { Content = itemp.actdesc.Trim(), Tag = itemp.actcode });
            }
            this.cmbPTCash.SelectedIndex = 0;

        }


        private void btnShowProposal_Click(object sender, RoutedEventArgs e)
        {

            if (this.btnShowProposal.Content.ToString() == "Next")
            {
                this.btnUpdateProposal.Visibility = Visibility.Hidden;
                this.btnPrintProposal.Visibility = Visibility.Hidden;
                this.cmbRptName.Visibility = Visibility.Hidden;
                this.stkpProposalDetails.Visibility = Visibility.Collapsed;
                this.dgBpp1.ItemsSource = null;
                this.stkpSetup.IsEnabled = true;

                this.btnShowProposal.Content = "Ok";
                return;
            }
            this.ShowPaymentProposal();

            this.btnShowProposal.Content = "Next";
        }

        private void ShowPaymentProposal()
        {
            this.ApplyList.Clear();
            string BranchID1 = ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Tag.ToString();
            BranchID1 = (BranchID1 == "0000" ? "%" : BranchID1);
            string MonthID1 = ((ComboBoxItem)this.cmbBpppMonth.SelectedItem).Tag.ToString();
            string CashType1 = ((ComboBoxItem)this.cmbPTCash.SelectedItem).Tag.ToString();// .chkPTCash.IsChecked == true ? "19010002" : "%");
            CashType1 = (CashType1 == "0000" ? "%" : CashType1);
            var pap1 = vm1.SetParamShowPayProp(WpfProcessAccess.CompInfList[0].comcod, BranchID: BranchID1, MonthID: MonthID1, CashType: CashType1);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1); //Success
            if (ds2 == null)
                return;

            this.ApplyList = ds2.Tables[0].DataTableToList<HmsEntityAccounting.ApprovalSheet>();
            this.dgBpp1.ItemsSource = this.ApplyList;

            this.btnUpdateProposal.Visibility = Visibility.Visible;
            this.btnPrintProposal.Visibility = Visibility.Visible;
            this.stkpProposalDetails.Visibility = Visibility.Visible;
            this.cmbRptName.Visibility = Visibility.Visible;
            this.stkpSetup.IsEnabled = false;
            DateTime date1 = DateTime.Parse("01-" + ((ComboBoxItem)this.cmbBpppMonth.SelectedItem).Content.ToString()).AddMonths(-6);
            this.dgPayStatus1.Columns[3].Header = "    " + date1.ToString("MMM-yy");
            this.dgPayStatus1.Columns[4].Header = "    " + date1.AddMonths(1).ToString("MMM-yy");
            this.dgPayStatus1.Columns[5].Header = "    " + date1.AddMonths(2).ToString("MMM-yy");
            this.dgPayStatus1.Columns[6].Header = "    " + date1.AddMonths(3).ToString("MMM-yy");
            this.dgPayStatus1.Columns[7].Header = "    " + date1.AddMonths(4).ToString("MMM-yy");
            this.dgPayStatus1.Columns[8].Header = "    " + date1.AddMonths(5).ToString("MMM-yy");

            //201703
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnUpdateProposal_Click(object sender, RoutedEventArgs e)
        {
            string MonthID1 = ((ComboBoxItem)this.cmbBpppMonth.SelectedItem).Tag.ToString();
            DataSet ds1 = vm1.GetDataSetForUpdateBppApproval(WpfProcessAccess.CompInfList[0].comcod, this.ApplyList, _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode,
                    _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);
            var pap1 = vm1.SetParamUpdateBppApproval(WpfProcessAccess.CompInfList[0].comcod, MonthID1, ds1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;
            System.Windows.MessageBox.Show("Budget Approval Updated Successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void btnPrintProposal_Click(object sender, RoutedEventArgs e)
        {
            string BrnCod = ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Tag.ToString().Trim();
            string BrnName = ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Content.ToString().Trim();
            string fromDate = "01-" + ((ComboBoxItem)this.cmbBpppMonth.SelectedItem).Content.ToString().Trim();
            string ToDate = Convert.ToDateTime(fromDate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string RptID = ((ComboBoxItem)this.cmbRptName.SelectedItem).Tag.ToString(); // "RPTBPPTRANS03", "RPTBPPTRANS04"
            string TrHead = (RptID == "RPTBPPTRANS03" ? "Payment Proposal Control Summary - ".ToUpper() :
                                "Overall Payment Proposal - ".ToUpper()) + BrnName;

            BrnCod = (BrnCod == "0000" ? "%" : BrnCod);
            vmReportAccounts1 vmrptAcc = new vmReportAccounts1();
            var pap1 = vmrptAcc.SetParamBppTransList(WpfProcessAccess.CompInfList[0].comcod, RptID, fromDate, ToDate, "A", BrnCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            var PayPropTrnLst2 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.PayProTransectionList2>();

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptPayProTransList2", PayPropTrnLst2, null, list3, TrHead);
            string WindowTitle1 = TrHead;
            string date = "(From " + fromDate + " To " + ToDate + " )";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void lbldgBpp1AccHead_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string itemid = ((Label)sender).Tag.ToString().Trim();
            this.ShowSixMonthStatus(itemid);
        }


        private void ShowSixMonthStatus(string itemid = "XXXXXXXXX")
        {
            string mainDesc = "", subDesc = "";
            this.lblPayStatus1.Content = "";
            this.dgPayStatus1.ItemsSource = null;
            this.SixMonthList.Clear();


            if (itemid.Substring(12, 12) != "000000000000")
            {
                mainDesc = this.ApplyList.FindAll(x => x.trncod == itemid.Substring(0, 12) + "000000000000").ToList()[0].trndesc.Trim();
            }
            var item1 = this.ApplyList.FindAll(x => x.trncod == itemid).ToList();
            subDesc = item1[0].trndesc.Trim();
            if (item1[0].sectcod == "000000000000")
                return;

            this.lblPayStatus1.Content = mainDesc + (mainDesc.Length > 0 ? " =>> " : "") + subDesc;

            string BranchID1 = ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Tag.ToString();
            BranchID1 = (BranchID1 == "0000" ? "%" : BranchID1);
            string MonthID1 = ((ComboBoxItem)this.cmbBpppMonth.SelectedItem).Tag.ToString();
            string CashType1 = ((ComboBoxItem)this.cmbPTCash.SelectedItem).Tag.ToString();// .chkPTCash.IsChecked == true ? "19010002" : "%");
            CashType1 = (CashType1 == "0000" ? "%" : CashType1);
            var pap1 = vm1.SetParamShowSixMonthStatus(WpfProcessAccess.CompInfList[0].comcod, BranchID: BranchID1, MonthID: MonthID1, CashType: CashType1);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1); //Success
            if (ds2 == null)
                return;
            this.SixMonthList = ds2.Tables[0].DataTableToList<HmsEntityAccounting.SixMonthStatus>();
            this.dgPayStatus1.ItemsSource = this.SixMonthList;
            ///////////-----

        }

        //internal class VarianceConverter123 : IValueConverter
        //{
        //    #region IValueConverter Members

        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        var diff1 = value as vmEntryAccMgt1.ApprovalSheet;
        //        if (diff1 == null)
        //        {
        //            throw new ArgumentException("value must be a ApprovalSheet", "value");
        //        }

        //        return diff1.bppam - diff1.bapam;
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        throw new NotImplementedException();
        //    }
        //    #endregion
        //}     
    }
}
