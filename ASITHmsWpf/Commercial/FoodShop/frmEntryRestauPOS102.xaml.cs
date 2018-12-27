using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Commercial;
using ASITHmsRpt2Inventory;
using ASITHmsRpt4Commercial;
using Microsoft.Reporting.WinForms;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ASITHmsWpf.Commercial.FoodShop
{
    /// <summary>
    /// Interaction logic for frmEntryRestauPOS102.xaml
    /// </summary>
    public partial class frmEntryRestauPOS102 : UserControl
    {
        private bool FrmInitialized = false;
        private List<HmsEntityCommercial.InvoiceTransList> InvList = new List<HmsEntityCommercial.InvoiceTransList>();
        private List<HmsEntityCommercial.InvoiceTransList2> InvList2 = new List<HmsEntityCommercial.InvoiceTransList2>();
        //private List<vmEntryPharRestPOS1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPharRestPOS1.ListViewItemTable>();
        private List<vmEntryPharRestPOS1.StockItemList> InvItemList = new List<vmEntryPharRestPOS1.StockItemList>();
        private List<vmEntryPharRestPOS1.StockItemSumList> InvItemSumList = new List<vmEntryPharRestPOS1.StockItemSumList>();
        //private List<vmEntryPharRestPOS1.ListViewItemTable> ListViewItemTable1a = new List<vmEntryPharRestPOS1.ListViewItemTable>();

        //List<HmsEntityCommercial.InvoiceTransList> Rptlist2 = new List<HmsEntityCommercial.InvoiceTransList>();
        //List<HmsEntityCommercial.PhSalesInvoice01> Rptlist1 = new List<HmsEntityCommercial.PhSalesInvoice01>();
        private List<HmsEntityCommercial.PhSalesInvoice01> LstDueMemo1 = new List<HmsEntityCommercial.PhSalesInvoice01>();
        private List<vmEntryPharRestPOS1.PhSalesCollMemos01> LstDueMemoCol1 = new List<vmEntryPharRestPOS1.PhSalesCollMemos01>();

        private vmEntryPharRestPOS1 vm1 = new vmEntryPharRestPOS1();
        private vmReportPharRestPOS1 vm2 = new vmReportPharRestPOS1();
        public frmEntryRestauPOS102()
        {
            InitializeComponent();

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {

                this.Objects_On_Init();
                this.FrmInitialized = true;
            }
        }

        private void Objects_On_Init()
        {
            this.xctk_dtDueInvDat1.Value = DateTime.Today.AddDays(-15);
            this.xctk_dtDueInvDat2.Value = DateTime.Today;
            this.xctk_dtDuePayDat1.Value = DateTime.Today;
            string FrmDate1 = DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
            string ToDate1 = DateTime.Today.ToString("dd-MMM-yyyy");
            this.cmbDuesInvList1.Items.Clear();
            this.InvList.Clear();
            this.InvList = this.PreviousMemoList(FrmDate1, ToDate1);
            this.InvList = this.InvList.FindAll(x => x.invno.Substring(0, 3) == "FSI" && x.dueam > 0);
            if (this.InvList == null)
                return;

            foreach (var item1 in this.InvList)
            {
                this.cmbDuesInvList1.Items.Add(new ComboBoxItem()
                {
                    Content = item1.invno1.Substring(3, 2) + "-" + item1.invno1.Substring(11, 5) + " [Tk. " + item1.billam.ToString("#,##0.00") +
                    (item1.dueam <= 0 ? "" : ", Due: Tk. " + item1.dueam.ToString("#,##0.00")) + ", " +
                    item1.invdat.ToString("dd.MM.yyyy") + "] " + (item1.invref.Trim().Length > 0 ? ", " + item1.invref.Trim() : "") +
                    (item1.invnar.Trim().Length > 0 ? ", " + item1.invnar.Trim() : ""),
                    Tag = item1.invno
                });
            }
            this.cmbDuesInvList1.SelectedIndex = 0;
        }


        private List<HmsEntityCommercial.InvoiceTransList> PreviousMemoList(string Date1, string Date2, string searchStr = "%")
        {
            //string sectcod1 = ((ComboBoxItem)this.cmbSectCod.Items[this.cmbSectCod.SelectedIndex]).Tag.ToString();
            var pap1 = vm2.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, "A00MSISUM", Date1, Date2, "", "FSI");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return null;

            return ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
        }

        private void btnShowDueMemo_Click(object sender, RoutedEventArgs e)
        {
            if (this.cmbDuesInvList1.SelectedItem == null)
                return;

            this.txtDueColAmt1.Text = "";
            this.lblDueBalAmt.Visibility = Visibility.Hidden;
            this.dgDueMemo.ItemsSource = null;
            string memoNum = ((ComboBoxItem)this.cmbDuesInvList1.SelectedItem).Tag.ToString();
            var pap1 = vm2.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (double.Parse(ds1.Tables[0].Rows[0]["dueam"].ToString()) == 0)
            {
                System.Windows.MessageBox.Show("Dues amount already recovered. Please try with another memo", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                    MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            LstDueMemo1 = ds1.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
            //InvList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList2>();
            LstDueMemoCol1 = ds1.Tables[2].DataTableToList<vmEntryPharRestPOS1.PhSalesCollMemos01>();
            this.lblDueInvDate1.Content = "Date : " + ds1.Tables[0].Rows[0]["invdat1"].ToString();
            this.lblDueInvDate1.Tag = ds1.Tables[0].Rows[0]["invdat1"].ToString();
            this.lblDueInvNo1.Content = "Invoice No : " + ds1.Tables[0].Rows[0]["invno1"].ToString();

            decimal ItemTotal1 = this.LstDueMemo1.Sum(x => x.itmam);
            decimal NetTotal = this.LstDueMemo1.Sum(x => x.inetam);
            decimal VATTotal = this.LstDueMemo1.Sum(x => x.ivatam);
            decimal DiscTotal = this.LstDueMemo1.Sum(x => x.idisam);

            this.lblDueInvNo1.Tag = memoNum;

            this.lblDueAmtDes1.Content = "Due = Tk. " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["dueam"]).ToString("#,##0.00") +
                                         (Convert.ToDecimal(ds1.Tables[0].Rows[0]["collam"]) == 0 ? "" : ", Paid = Tk. " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["collam"]).ToString("#,##0.00")) +
                                         ", Bill = Tk. " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["billam"]).ToString("#,##0.00") +
                                         (Convert.ToDecimal(ds1.Tables[0].Rows[0]["tdisam"]) == 0 ? "" : ", Discount = Tk. " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["tdisam"]).ToString("#,##0.00")) +
                                         ", Total: Tk. ";
            this.lblTtlAmtBl.Content = ItemTotal1.ToString("#,##0.00");
            this.lblTtlDisctBl.Content = DiscTotal.ToString("#,##0.00");
            this.lblTtlNetAmtBl.Content = NetTotal.ToString("#,##0.00");
            this.lblTtlVtAmtBl.Content = VATTotal.ToString("#,##0.00"); //("#,##0;(#,##0); - ");
            this.lblDueAmtDes1.Tag = ds1.Tables[0].Rows[0]["dueam"].ToString();
            this.lblDueBillGrossAmt.Content = Convert.ToDecimal(ds1.Tables[0].Rows[0]["totslam"]).ToString("#,##0.00");
            this.lblDueBillGrossAmt.Tag = ds1.Tables[0].Rows[0]["billam"].ToString();
            this.dgDueMemo.ItemsSource = LstDueMemo1;
            this.dgDueCollMemo.ItemsSource = LstDueMemoCol1;
            this.lblDueInvRef1.Content = "Ref/Cell.: " + ds1.Tables[0].Rows[0]["invref"].ToString().Trim();
            this.txtbDueInvNar1.Text = "Remarks : " + ds1.Tables[0].Rows[0]["invnar"].ToString().Trim(); ;
            this.stkDueCol.Visibility = Visibility.Visible;
            this.stkpUpdateDueCol1.IsEnabled = true;
            this.btnUpdateDueCol1.IsEnabled = true;
        }

        private void btnUpdateDueCol1_Click(object sender, RoutedEventArgs e)
        {          
            string memoDate = this.lblDueInvDate1.Tag.ToString();
            string colDate = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");// this.xctk_dtDuePayDat1.Text;
            string memoNum = this.lblDueInvNo1.Tag.ToString();
            decimal DueAmt1 = decimal.Parse("0" + this.lblDueAmtDes1.Tag.ToString());
            decimal PaidAmt1 = decimal.Parse("0" + this.txtDueColAmt1.Text.Trim());
            string PayType1 = ((ComboBoxItem)this.cmbPayType.SelectedItem).Tag.ToString();
            string MemoNar1 = this.txtDueRef.Text.Trim();

            if (PayType1 != "CASH" && MemoNar1.Length == 0)
            {
                System.Windows.MessageBox.Show("Reference is manadatory for non cash collection", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                    MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
               MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            if (PaidAmt1 == 0 || PaidAmt1 > DueAmt1)
            {
                System.Windows.MessageBox.Show("Collection amount must be same as due amount", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                    MessageBoxImage.Stop, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                this.txtDueColAmt1.Focus();
                return;
            }

            string CollNote = "Bill Ammount = " + this.lblDueBillGrossAmt.Content.ToString() + ", Prev. Due = " + DueAmt1.ToString("#,##0.00") + ", Paid = " + PaidAmt1.ToString("#,##0.00") +
                    (DueAmt1 > PaidAmt1 ? ", Balance Due = " + (DueAmt1 - PaidAmt1).ToString("#,##0.00") : "") + ", Paid By : " + PayType1 + (" " + MemoNar1).Trim();
            string vouno1 = "000000000000000000";// this.lblVouNo.Tag.ToString().Trim();
            var pap1 = vm1.SetParamUpdateMSalesDueColl(WpfProcessAccess.CompInfList[0].comcod, InvNum1: memoNum, InvDate1: memoDate, DueColDate1: colDate, DueColAmt1: PaidAmt1.ToString(), CollNote1: CollNote, vounum1: vouno1,
                        _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.btnUpdateDueCol1.IsEnabled = false;
            this.dgDueCollMemo.ItemsSource = null; ;
            int sl1 = LstDueMemoCol1.Count();
            this.LstDueMemoCol1.Add(new vmEntryPharRestPOS1.PhSalesCollMemos01() { slnum = sl1 + 1, bilcolid = "aa", bilcoldat = DateTime.Parse(colDate), bilcolam = PaidAmt1, bcnote = CollNote, tokenid = "XXXXXX" });
            //this.LstDueMemoCol1.Insert(0, new vmEntryPharRestPOS1.PhSalesCollMemos01() {slnum = sl1+1, bilcolid  = "aa", bilcoldat = DateTime.Parse(colDate), bilcolam = PaidAmt1, bcnote = CollNote, tokenid="XXXXXX"});
            this.dgDueCollMemo.ItemsSource = LstDueMemoCol1;

            //lvAc.ScrollIntoView(lvAc.Items[z]);
            //lvAc.SelectedIndex = z;

            this.dgDueCollMemo.ScrollIntoView(this.dgDueCollMemo.Items[sl1]);
            //System.Windows.MessageBox.Show("Update Successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            this.stkpUpdateDueCol1.IsEnabled = false;
            this.btnFindDueList1_Click(null, null);
        }

        private void txtDueColAmt1_LostFocus(object sender, RoutedEventArgs e)
        {
            this.lblDueBalAmt.Visibility = Visibility.Visible;
            decimal DueAmt1 = decimal.Parse("0" + this.lblDueAmtDes1.Tag.ToString());
            decimal PaidAmt1 = decimal.Parse("0" + this.txtDueColAmt1.Text.Trim());
            this.lblDueBalAmt.Content = (DueAmt1 == PaidAmt1 ? "Full amount paid. Dues clear" : ((DueAmt1 < PaidAmt1) ? "Excess collection is not allowed" : "Balance Due : Tk. " + (DueAmt1 - PaidAmt1).ToString("#,##0.00")));
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnFindDueList1_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtDueInvDat1.Value = DateTime.Today.AddDays(-15);
            this.xctk_dtDueInvDat2.Value = DateTime.Today;
            this.xctk_dtDuePayDat1.Value = DateTime.Today;
            string FrmDate1 = DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
            string ToDate1 = DateTime.Today.ToString("dd-MMM-yyyy");
            this.cmbDuesInvList1.Items.Clear();
            this.InvList.Clear();
            this.InvList = this.PreviousMemoList(FrmDate1, ToDate1);
            this.InvList = this.InvList.FindAll(x => x.invno.Substring(0, 3) == "FSI" && x.dueam > 0);
            if (this.InvList == null)
                return;
            string dueref1 = this.txtDueRef.Text.Trim();
            this.txtDueRef.Text = "";
            string content1 = "";


            foreach (var item1 in this.InvList)
            {
                content1 = item1.invno1.Substring(3, 2) + "-" + item1.invno1.Substring(11, 5) + " [Tk. " + item1.billam.ToString("#,##0.00") +
                    (item1.dueam <= 0 ? "" : ", Due: Tk. " + item1.dueam.ToString("#,##0.00")) + ", " +
                    item1.invdat.ToString("dd.MM.yyyy") + "] " + (item1.invref.Trim().Length > 0 ? ", " + item1.invref.Trim() : "") +
                    (item1.invnar.Trim().Length > 0 ? ", " + item1.invnar.Trim() : "");
                if (dueref1.Length == 0 || content1.Contains(dueref1))
                {
                    this.cmbDuesInvList1.Items.Add(new ComboBoxItem()
                    {
                        Content = content1,
                        Tag = item1.invno
                    });
                }
            }
            this.cmbDuesInvList1.SelectedIndex = 0;
        }
    }
}
