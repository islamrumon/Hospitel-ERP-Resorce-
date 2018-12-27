using ASITHmsEntity;
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
using ASITHmsViewMan.Accounting;
using System.Data;
using System.ComponentModel;
using ASITFunLib;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt0Main;
using ASITHmsRpt1GenAcc.Accounting;
using System.Windows.Controls.Primitives;
using System.Collections;
using System.Collections.ObjectModel;

namespace ASITHmsWpf.Accounting
{
    /// <summary>
    /// Interaction logic for frmReportAcc1.xaml
    /// </summary>
    public partial class frmReportAcc1 : UserControl
    {
        private bool FrmInitialized = false;
        private int TabItemIndex1 = 0;
        private DataGrid dgRpt1;
        private string LastSelectedItem = "NONE";
        private vmReportAccounts1 vmrptAcc = new vmReportAccounts1();
        private List<HmsEntityAccounting.AccTransectionList> AccTrnLst = new List<HmsEntityAccounting.AccTransectionList>();
        private List<HmsEntityAccounting.AccLedger1> AccTrnLst2 = new List<HmsEntityAccounting.AccLedger1>();
        private List<HmsEntityAccounting.AccTrialBalance1> AccTrialBlncLst = new List<HmsEntityAccounting.AccTrialBalance1>();
        private List<HmsEntityAccounting.AccTrialBalance1t> AccTrialBlncTtlLst = new List<HmsEntityAccounting.AccTrialBalance1t>();
        private List<HmsEntityAccounting.AccLedger1> AccLedgerLst = new List<HmsEntityAccounting.AccLedger1>();
        private List<HmsEntityAccounting.AccLedger1A> AccLedgerLst2 = new List<HmsEntityAccounting.AccLedger1A>();

        private List<HmsEntityAccounting.AccIncomeStatement1> AccIncomeStLst = new List<HmsEntityAccounting.AccIncomeStatement1>();
        private List<HmsEntityAccounting.AccIncomeStatement1t> AccIncomeStLstTtlLst = new List<HmsEntityAccounting.AccIncomeStatement1t>();

        private List<HmsEntityAccounting.PayProTransectionList> PayPropTrnLst = new List<HmsEntityAccounting.PayProTransectionList>();
        private List<HmsEntityAccounting.PayProTransectionList2> PayPropTrnLst2 = new List<HmsEntityAccounting.PayProTransectionList2>();
        private List<HmsEntityAccounting.AccCashBook1> CashBookTransList = new List<HmsEntityAccounting.AccCashBook1>();
        private List<HmsEntityAccounting.AccIntComLoanStat1> InterComLoanStat1 = new List<HmsEntityAccounting.AccIntComLoanStat1>();
        private List<HmsEntityAccounting.AccIntComLoanSum1> InterComLoanSum1 = new List<HmsEntityAccounting.AccIntComLoanSum1>();
        private object RecPaydtset;
        public frmReportAcc1()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.InitializeOptions();
                this.Objects_On_Load();
                this.FrmInitialized = true;
            }
        }
        private void InitializeOptions()
        {
            TreeViewItem tvi1 = new TreeViewItem() { Header = "A. TRANSACTION REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi2 = new TreeViewItem() { Header = "B. GENERAL ACCOUNTING REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi3 = new TreeViewItem() { Header = "C. FINAL ACCOUNTING REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi4 = new TreeViewItem() { Header = "D. PAYMENT BUDGET REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi5 = new TreeViewItem() { Header = "E. SPECIAL PURPOSE REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };

            tvi1.Items.Add(new TreeViewItem { Header = "01. Transaction voucher list", Tag = "A01TVL" });
            tvi1.Items.Add(new TreeViewItem { Header = "02. Details transaction list", Tag = "A02TL" });
            tvi1.Items.Add(new TreeViewItem { Header = "03. Cash book details", Tag = "A03CBD" });
            tvi1.Items.Add(new TreeViewItem { Header = "04. Receipts & Payments details", Tag = "A04RPCB" });

            tvi2.Items.Add(new TreeViewItem { Header = "01. Receipts & Payments", Tag = "B01RPCB" });
            tvi2.Items.Add(new TreeViewItem { Header = "02. Cash & Bank position", Tag = "B02CB" });
            tvi2.Items.Add(new TreeViewItem { Header = "03. Cash flow statement", Tag = "B03CF" });
            tvi2.Items.Add(new TreeViewItem { Header = "04. Control ledger", Tag = "B04CL" });
            tvi2.Items.Add(new TreeViewItem { Header = "05. Subsidiary ledger", Tag = "B05SL" });
            tvi2.Items.Add(new TreeViewItem { Header = "06. Control schedule", Tag = "B06CS" });
            tvi2.Items.Add(new TreeViewItem { Header = "07. Income Vs Expense", Tag = "B07IVE" });


            tvi3.Items.Add(new TreeViewItem { Header = "01. Trial balance", Tag = "C01TB" });
            tvi3.Items.Add(new TreeViewItem { Header = "02. Income statement", Tag = "C02IS" });
            tvi3.Items.Add(new TreeViewItem { Header = "03. Balance sheet", Tag = "C03BS" });

            tvi4.Items.Add(new TreeViewItem { Header = "01. Payment proposal list", Tag = "D01TPL" });
            tvi4.Items.Add(new TreeViewItem { Header = "02. All transaction list", Tag = "D02ATL" });
            tvi4.Items.Add(new TreeViewItem { Header = "03. Category wise summary", Tag = "D03CWS" });
            tvi4.Items.Add(new TreeViewItem { Header = "04. Overall payment budget", Tag = "D04OPB" });

            tvi5.Items.Add(new TreeViewItem { Header = "01. Main/sub head trans. list", Tag = "E01HTL" });
            tvi5.Items.Add(new TreeViewItem { Header = "02. Main/sub head trans. sum.", Tag = "E02HTS" });
            tvi5.Items.Add(new TreeViewItem { Header = "03. Sub vs main head details", Tag = "E03SMTL" });
            tvi5.Items.Add(new TreeViewItem { Header = "04. Sub vs main head summary", Tag = "E04SMTS" });
            tvi5.Items.Add(new TreeViewItem { Header = "05. Inter-company loan details", Tag = "E05ICLDT" });
            tvi5.Items.Add(new TreeViewItem { Header = "06. Inter-company loan summary", Tag = "E06ICLSU" });
            tvi5.Items.Add(new TreeViewItem { Header = "07. Main vs Sub vs Location summary", Tag = "E07SMTS" });
            tvi5.Items.Add(new TreeViewItem { Header = "08. Main vs Location vs Sub summary", Tag = "E08SMTS" });
            tvi1.IsExpanded = true;
            tvi2.IsExpanded = true;
            tvi3.IsExpanded = true;
            tvi4.IsExpanded = true;
            tvi5.IsExpanded = true;

            this.tvRptTitle.Items.Add(tvi1);
            this.tvRptTitle.Items.Add(tvi2);
            this.tvRptTitle.Items.Add(tvi3);
            this.tvRptTitle.Items.Add(tvi4);
            this.tvRptTitle.Items.Add(tvi5);

        }

        private void Objects_On_Load()
        {
            this.xctk_dtpFrom.Value = DateTime.Today; //Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.xctk_dtpTo.Value = DateTime.Today;

            this.VouType();

            this.cmbSBrnCod.Items.Clear();
            var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = "ALL BRANCHES", Tag = "0000" });
            foreach (var itemb in brnList)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = itemb.brnnam, Tag = itemb.brncod });

            this.cmbSBrnCod.SelectedIndex = 0;
            //var sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
            //sectList.Sort(delegate(HmsEntityGeneral.CompSecCodeBook x, HmsEntityGeneral.CompSecCodeBook y)
            //{
            //    return x.sectname.CompareTo(y.sectname);
            //});

            //this.cmbSectCod.Items.Clear();
            //this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = "ALL LOCATIONS", Tag = "000000000000" });
            //foreach (var itemc in sectList)
            //{
            //    this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemc.sectname, Tag = itemc.sectcod });
            //}

            if (WpfProcessAccess.AccCodeList == null)
            {
                WpfProcessAccess.GetAccCodeList();
                WpfProcessAccess.AccCodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
                {
                    return x.actdesc.CompareTo(y.actdesc);
                });
            }

            this.cmbActGroup.Items.Clear();
            var ActGrpList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(4, 8) == "00000000");
            ActGrpList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
            {
                return x.actcode.CompareTo(y.actcode);
            });

            this.cmbActGroup.Items.Add(new ComboBoxItem() { Content = "DETAILS", ToolTip = "ALL DETAILS HEADS OF ACCOUNTS", Tag = "DETAILS" });
            this.cmbActGroup.Items.Add(new ComboBoxItem() { Content = "MAIN", ToolTip = "ALL MAIN GROUP HEAD OF ACCOUNTS", Tag = "MAIN" });
            this.cmbActGroup.Items.Add(new ComboBoxItem() { Content = "LEVEL-2", ToolTip = "ALL LEVEL-2 HEADS OF ACCOUNTS", Tag = "LEVEL2" });
            this.cmbActGroup.Items.Add(new ComboBoxItem() { Content = "LEVEL-3", ToolTip = "ALL LEVEL-3 HEADS OF ACCOUNTS", Tag = "LEVEL3" });


            //foreach (var itemg1 in ActGrpList)
            //    this.cmbActGroup.Items.Add(new ComboBoxItem() { Content = itemg1.actcode.Substring(0, 4), ToolTip = itemg1.actdesc, Tag = itemg1.actcode });

            if (WpfProcessAccess.AccSirCodeList == null)
            {
                WpfProcessAccess.GetAccSirCodeList();
                WpfProcessAccess.AccSirCodeList.Sort(delegate(HmsEntityGeneral.SirInfCodeBook x, HmsEntityGeneral.SirInfCodeBook y)
                {
                    return x.sirdesc.CompareTo(y.sirdesc);
                });
            }

            this.cmbSirGroup.Items.Clear();
            var SirGrpList = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(7, 5) == "00000");

            SirGrpList.Sort(delegate(HmsEntityGeneral.SirInfCodeBook x, HmsEntityGeneral.SirInfCodeBook y)
            {
                return x.sircode.CompareTo(y.sircode);
            });

            this.cmbSirGroup.Items.Add(new ComboBoxItem() { Content = "DETAILS", ToolTip = "ALL DETAILS HEADS OF SUB.ACCOUNTS", Tag = "DETAILS" });
            this.cmbSirGroup.Items.Add(new ComboBoxItem() { Content = "MAIN", ToolTip = "ALL MAIN GROUP HEAD OF SUB.ACCOUNTS", Tag = "MAIN" });
            this.cmbSirGroup.Items.Add(new ComboBoxItem() { Content = "LEVEL-2", ToolTip = "ALL LEVEL-2 HEADS OF SUB.ACCOUNTS", Tag = "LEVEL2" });
            this.cmbSirGroup.Items.Add(new ComboBoxItem() { Content = "LEVEL-3", ToolTip = "ALL LEVEL-3 HEADS OF SUB.ACCOUNTS", Tag = "LEVEL3" });
            this.cmbSirGroup.Items.Add(new ComboBoxItem() { Content = "LEVEL-4", ToolTip = "ALL LEVEL-4 HEADS OF SUB.ACCOUNTS", Tag = "LEVEL4" });

            foreach (var itemg2 in SirGrpList)
                this.cmbSirGroup.Items.Add(new ComboBoxItem() { Content = itemg2.sircode.Substring(0, 7), ToolTip = itemg2.sirdesc, Tag = itemg2.sircode });


            WpfProcessAccess.AccSirCodeList.Sort(delegate(HmsEntityGeneral.SirInfCodeBook x, HmsEntityGeneral.SirInfCodeBook y)
            {
                return x.sirdesc.CompareTo(y.sirdesc);
            });
        }



        private void cmbSBrnCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbSBrnCod.SelectedItem == null)
                return;

            string brncod = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString().Trim();//.Substring(0, 4);
            var sectList = new List<HmsEntityGeneral.CompSecCodeBook>();
            //if (brncod == "0000")
            //    sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
            //else
            //    sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(0, 4) == brncod && x.sectcod.Substring(9, 3) != "000");


            if (brncod == "0000")
                sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(7, 5) != "00000");
            else
                sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(0, 4) == brncod && x.sectcod.Substring(7, 5) != "00000");

            //sectList.Sort(delegate(HmsEntityGeneral.CompSecCodeBook x, HmsEntityGeneral.CompSecCodeBook y)
            //{
            //    return x.sectname.CompareTo(y.sectname);
            //});

            this.cmbSectCod.Items.Clear();
            this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = "ALL LOCATIONS", Tag = brncod + "00000000", ToolTip = brncod + "00000000 - ALL LOCATIONS" });
            foreach (var itemc in sectList)
            {
                var cbi1 = new ComboBoxItem() { Content = itemc.sectname, Tag = itemc.sectcod, ToolTip = itemc.sectcod + " - " + itemc.sectname };
                cbi1.Foreground = (itemc.sectcod.Substring(9, 3) == "000" ? Brushes.Blue : Brushes.Black);
                cbi1.FontWeight = (itemc.sectcod.Substring(9, 3) == "000" ? FontWeights.Bold : FontWeights.Normal);
                this.cmbSectCod.Items.Add(cbi1);
            }
            this.cmbSectCod.SelectedIndex = 0;
        }

        private void VouType()
        {
            this.cmbVouType.Items.Clear();
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "All Type of Vouchers".ToUpper(), Tag = "00000", IsSelected = true });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "All Payment Vouchers".ToUpper(), Tag = "PV000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "All Receipt Vouchers".ToUpper(), Tag = "RV000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "All Receipt & Payment Vouchers".ToUpper(), Tag = "[RP]V000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "All Journal Vouchers".ToUpper(), Tag = "JV000" });

            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "Cash Payment Voucher".ToUpper(), Tag = "PVC000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "Bank Payment Voucher".ToUpper(), Tag = "PVB000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "Fund Transfer Voucher".ToUpper(), Tag = "FTV000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "Cash Receipt Voucher".ToUpper(), Tag = "RVC000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "Bank Receipt Voucher".ToUpper(), Tag = "RVB000" });

            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "A/c Receivable Journal Voucher".ToUpper(), Tag = "JVR000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "A/c Receivable Journal Voucher".ToUpper(), Tag = "JVP000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "Adjustment Journal Voucher".ToUpper(), Tag = "JVA000" });
            this.cmbVouType.Items.Add(new ComboBoxItem() { Content = "Accounts Opening Voucher".ToUpper(), Tag = "OPV000" });

            /*
              var vlist = new List<AccVoucherType>() {
                new AccVoucherType ( "Cash Payment Voucher", "PVC82" ),
                new AccVoucherType ( "Bank Payment Voucher", "PVB82" ),
                new AccVoucherType ( "Cash Payment Against Budget Voucher", "PVC83" ),
                new AccVoucherType ( "Bank Payment Against Budget Voucher", "PVB83" ),
                new AccVoucherType ( "Fund Transfer Voucher", "FTV88" ),
                new AccVoucherType ( "Cash Receipt Voucher", "RVC81" ),
                new AccVoucherType ( "Bank Receipt Voucher", "RVB81" ),
                new AccVoucherType ( "A/c Receivable Journal Voucher", "JVR91" ),
                new AccVoucherType ( "A/c Payable Journal Voucher", "JVP92" ),
                //new AccVoucherType ( "Bills Receivable Journal Voucher", "JVR93" ),
                //new AccVoucherType ( "Bills Payable Journal Voucher", "JVP94" ),
                new AccVoucherType ( "Adjustment Journal Voucher", "JVA99" ),
                new AccVoucherType ( "Accounts Opening Voucher", "OPV00" )
            };

             
             */


            //var vtypeList = HmsEntityAccounting.GetVoucherType();
            //foreach (var item1 in vtypeList)
            //    this.cmbVouType.Items.Add(new ComboBoxItem() { Content = item1.vtitle.ToUpper(), Tag = item1.vtagid });
        }
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if ((TreeViewItem)tvRptTitle.SelectedItem == null)
                return;

            string BrnCod = "0000";
            string BrnName = "";
            if (this.stkOptBranch.Visibility == Visibility.Visible)
            {
                BrnCod = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString().Trim();
                BrnName = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Content.ToString().Trim();
            }
            BrnCod = (BrnCod == "0000" ? "%" : BrnCod);

            string SectCod = "000000000000";
            if (this.stkOptLocation.Visibility == Visibility.Visible)
                SectCod = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();

            SectCod = (SectCod.Substring(9, 3) == "000" ? SectCod.Substring(0, 9) : SectCod);
            SectCod = (SectCod.Length == 9 && SectCod.Substring(7, 2) == "00" ? SectCod.Substring(0, 7) : SectCod);
            SectCod = (SectCod.Length == 7 && SectCod.Substring(4, 3) == "000" ? SectCod.Substring(0, 4) : SectCod);
            SectCod = (SectCod == "0000" ? "%" : SectCod);

            //SectCod = (SectCod == "000000000000" ? "%" : SectCod);

            if (this.stkOptActCode.Visibility != Visibility.Visible)
            {
                this.AutoCompleteActCode.SelectedValue = null;
            }

            if (this.stkOptSirCode.Visibility != Visibility.Visible)
            {
                this.AutoCompleteSirCode.SelectedValue = null;
            }

            string VouType = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim();
            VouType = (VouType == "00000" ? "%" : (VouType.Substring(VouType.Length - 2, 2) == "00" ? VouType.Substring(0, VouType.Length - 3) : VouType));
            string fromDate = xctk_dtpFrom.Text.Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();
            string TrHead = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString().ToUpper();
            string TrTyp = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            string dept01 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();

            string ActCode = "";// .Tag.ToString(); 00-00-0000-0000
            if (this.AutoCompleteActCode.SelectedValue != null)
                ActCode = this.AutoCompleteActCode.SelectedValue.ToString().Trim();

            string SirCode = "";
            if (this.AutoCompleteSirCode.SelectedValue != null)
                SirCode = this.AutoCompleteSirCode.SelectedValue.ToString().Trim();// .Tag.ToString();

            if (this.stkOptActCode.Visibility == Visibility.Visible && this.stkOptTB.Visibility == Visibility.Visible)// (TrTyp == "B06CS")
            {
                string ACode1 = (ActCode.Trim() + "000000000000").Substring(0, 12);
                ACode1 = (ACode1.Substring(8, 4) == "0000" ? ACode1.Substring(0, 8) : ACode1);
                ACode1 = (ACode1.Substring(4, 4) == "0000" && ACode1.Length == 8 ? ACode1.Substring(0, 4) : ACode1);
                ACode1 = (ACode1.Substring(2, 2) == "00" && ACode1.Length == 4 ? ACode1.Substring(0, 2) : ACode1);
                this.chkLevel1.IsChecked = (ACode1.Length <= 2 && this.chkLevel1.IsChecked == true ? true : false);
                this.chkLevel2.IsChecked = (ACode1.Length <= 4 && this.chkLevel2.IsChecked == true ? true : false);
                this.chkLevel3.IsChecked = (ACode1.Length <= 8 && this.chkLevel3.IsChecked == true ? true : false);
            }

            string AccLevel = (this.chkLevel1.IsChecked == true ? "1" : "") + (this.chkLevel2.IsChecked == true ? "2" : "") +
                              (this.chkLevel3.IsChecked == true ? "3" : "") + (this.chkLevel4.IsChecked == true ? "4" : "");// "1234";
            if (AccLevel.Length == 0)
            {
                this.chkLevel4.IsChecked = true;
                AccLevel = "4";
            }
            int OptMore = this.cmbSubLevel.SelectedIndex;
            string LSirLevel = (OptMore == 1 ? "B" : (OptMore == 2 ? "T" : (OptMore == 3 ? "L" : (OptMore > 3 ? (OptMore - 3).ToString() : ""))));

            // 01OCCLB="Opening, Current &amp; Closing Balance", 02CPSO="Current Period Summary Only", 03MWS06="Month Wise Summary (6 Months)", 04MWS12="Month Wise Summary (12 Months)"
            string Period = ((ComboBoxItem)this.cmbPeriod.SelectedItem).Tag.ToString().Trim();// "WITHOPENING"; // "CURPERIOD"
            Period = (Period == "01OCCLB" ? "ALLPERIOD" : (Period == "02CPSO" ? "CURPERIOD" : (Period == "03MWS06" ? "06MONTHS" : (Period == "04MWS12" ? "12MONTHS" : "UNKNOWN"))));
            // DRCR="Both Debit &amp; Credit Amount",  DRO1="Debit Amount Only", CRO1="Credit Amount Only", DRB1="Debit Balance Only", CRB1="Credit Balance Only"
            string DrCr = ((ComboBoxItem)this.cmbDrCr.SelectedItem).Tag.ToString().Trim();
            DrCr = (DrCr == "DRCR" ? "BOTHDRCR" : (DrCr == "DRO1" ? "DEBITONLY" : (DrCr == "CRO1" ? "CREDITONLY" : (DrCr == "DRB1" ? "DEBITBALANCE" : (DrCr == "CRB1" ? "CREDITBALANCE" : "UNKNOWN")))));

            string[] RptOption1 = ((ComboBoxItem)this.cmbRptOptions.SelectedItem).Tag.ToString().Trim().Split(',');

            string SummLedger = "";
            string NarLoc = "NARRATION";
            if (RptOption1.Length > 1)
            {
                SummLedger = RptOption1[0].ToString();
                NarLoc = RptOption1[1].ToString();
            }

            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();

            switch (TrTyp)
            {
                case "B04CL": // (3 Reports) Control Ledger, Control Ledger Voucher Summary, Control Ledger Transaction Head Summary
                    this.GetAccLedger(PrintId, fromDate, ToDate, ActCode, NarLoc, SummLedger, BrnCod, SectCod);
                    break;
                case "B05SL":
                    this.GetAccSubLedger(PrintId, fromDate, ToDate, ActCode, SirCode, NarLoc, BrnCod, SectCod);
                    break;
                case "A04RPCB":
                case "B01RPCB":
                case "B02CB":
                case "B03CF":
                case "B07IVE":
                    AccLevel = AccLevel.Substring(AccLevel.Length - 1, 1);
                    this.chkLevel1.IsChecked = (AccLevel == "1" ? true : false);
                    this.chkLevel2.IsChecked = (AccLevel == "2" ? true : false);
                    this.chkLevel3.IsChecked = (AccLevel == "3" ? true : false);
                    this.chkLevel4.IsChecked = (AccLevel == "4" ? true : false);
                    string TrTyp1 = (RptOption1[0] == "NONE" && TrTyp.Contains("RPCB") ? TrTyp.Replace("CB", "") : TrTyp);
                    this.GetRecptPayCashFlow(PrintId, fromDate, ToDate, TrTyp1, BrnCod, SectCod, AccLevel, LSirLevel); // RptOption1[0] = "WITHBALANCE"/ "NONE"
                    break;
                case "A01TVL":
                    this.GetAllTranslst(PrintId, fromDate, ToDate, TrHead);
                    break;
                case "A02TL":   //       //<TreeViewItem Header="02. TRANSACTION LIST" Tag = "A02TL"/>
                    string WithNarr = (RptOption1[0] == "NONE" ? "NARRATION" : RptOption1[0]);
                    string TransSumm = (RptOption1.Length == 1 ? "TRNSDETAILS" : RptOption1[1]);
                    TrHead = "ACCOUNTS " + TrHead.Substring(3).Trim() + " - " + ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Content.ToString().Trim()
                        + (this.cmbVouType.SelectedIndex == 0 ? "" : " - " + ((ComboBoxItem)this.cmbVouType.SelectedItem).Content.ToString().Trim());
                    this.GetAllTranslst2(PrintId, fromDate, ToDate, TrHead, VouType, BrnCod, SectCod, WithNarr, TransSumm);
                    break;

                case "C01TB":
                    this.GetTrialBalance(PrintId, fromDate, ToDate, AccLevel, LSirLevel, Period);
                    break;
                case "C02IS":
                    this.GetIncomeStatement(PrintId, fromDate, ToDate, TrHead, BrnCod, AccLevel);
                    break;
                case "C03BS":
                    this.GetBalanceSheet(PrintId, fromDate, ToDate, TrHead, AccLevel);
                    break;
                case "B06CS":
                    this.GetControlSchedule(PrintId, fromDate, ToDate, BrnCod, SectCod, ActCode, SirCode, AccLevel, LSirLevel, Period, DrCr);
                    break;
                case "D01TPL":
                    this.GetAllPropTranslst1(PrintId, fromDate, ToDate, TrHead, BrnCod);
                    break;
                case "D02ATL":
                    TrHead = "Payment Proposal Details - ".ToUpper() + BrnName;
                    this.GetAllPropTranslst2("RPTBPPTRANS02", PrintId, fromDate, ToDate, TrHead, BrnCod);
                    break;
                case "D03CWS":
                    TrHead = "Payment Proposal Control Summary - ".ToUpper() + BrnName;
                    this.GetAllPropTranslst2("RPTBPPTRANS03", PrintId, fromDate, ToDate, TrHead, BrnCod);
                    break;
                case "D04OPB":
                    TrHead = "Overall Payment Proposal - ".ToUpper() + BrnName;
                    this.GetAllPropTranslst2("RPTBPPTRANS04", PrintId, fromDate, ToDate, TrHead, BrnCod);
                    break;
                case "A03CBD":
                    string WithNarr2 = (RptOption1[0] == "NONE" ? "NARRATION" : RptOption1[0]);
                    string TransSumm2 = (RptOption1.Length == 1 ? "TRNSDETAILS" : RptOption1[1]);
                    TrHead = "Cash Book ".ToUpper();
                    this.GetDateWiseCashBook1(PrintId, fromDate, ToDate, WithNarr2, TransSumm2);
                    break;
                case "E01HTL":  //  { Header = "01. MAIN/SUB HEAD TRANS. LIST", Tag = "E01HTL" }
                    this.GetMainSubTrans1(PrintId, "Details", fromDate, ToDate, BrnCod, SectCod, ActCode, SirCode, VouType);
                    break;
                case "E02HTS":  //  { Header = "02. MAIN/SUB HEAD TRANS. SUMMARY", Tag = "E02HTS" }
                    this.GetMainSubTrans1(PrintId, "Summary", fromDate, ToDate, BrnCod, SectCod, ActCode, SirCode, VouType);
                    break;
                case "E03SMTL": //  { Header = "03. SUB VS MAIN HEAD DETAILS", Tag = "E03SMTL" }
                    this.GetSubVsMainTrans1(PrintId, "Details", fromDate, ToDate, BrnCod, SectCod, ActCode, SirCode, VouType);
                    break;
                case "E04SMTS": //  { Header = "04. SUB VS MAIN HEAD SUMMARY", Tag = "E04SMTS" }
                    this.GetSubVsMainTrans1(PrintId, "Summary", fromDate, ToDate, BrnCod, SectCod, ActCode, SirCode, VouType);
                    break;
                case "E05ICLDT": //  { Header = "05. INTER-COMPANY LOAN DETAILS", Tag = "E05ICLDT" }
                    this.GetInterCompanyLoanDetails1(PrintId, "Details", fromDate, ToDate, ActCode, RptOption1[0]);
                    break;
                case "E06ICLSU": //  { Header = "06. INTER-COMPANY LOAN SUMMARY", Tag = "E06ICLSU" }
                    this.GetInterCompanyLoanSummary1(PrintId, "Summary", fromDate, ToDate);
                    break;
                case "E07SMTS":
                case "E08SMTS":
                    System.Windows.MessageBox.Show("This option is under construction. Please report to system admin", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                        MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    break;


            }

        }

        private void GetInterCompanyLoanSummary1(string PrintId, string ReportType, string fromDate, string ToDate)
        {
            var pap1 = vmrptAcc.SetParamInterCompanyLoan1(WpfProcessAccess.CompInfList[0].comcod, ReportType, fromDate, ToDate);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DateTime ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
            this.InterComLoanSum1.Clear();
            this.InterComLoanSum1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccIntComLoanSum1>();

            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintInterCompLoanSum1(this.InterComLoanSum1, "INTER COMPANY LOAN RECEIVED / PAID SUMMARY", fromDate, ToDate, ServerTime1); break;
                case "SS":
                    this.ViewTranList2(); break;
            }
        }

        private void PrintInterCompLoanSum1(List<HmsEntityAccounting.AccIntComLoanSum1> InterComLoanSum1, string title, string fromDate, string ToDate, DateTime ServerTime1)
        {
            //  Content = "Date Wise Summary", Tag = "DATESUM" 
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: ServerTime1);
            list3[0].RptHeader1 = title;
            list3[0].RptHeader2 = "(From " + fromDate + " To " + ToDate + " )";
            list3[0].RptParVal1 = "Balance As Of " + DateTime.Parse(fromDate).AddDays(-1).ToString("dd-MMM-yyyy");
            list3[0].RptParVal2 = "Balance As Of " + ToDate;
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccIntComLon2", InterComLoanSum1, null, list3, null);
            string WindowTitle1 = title.Remove(0, 4);
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void GetInterCompanyLoanDetails1(string PrintId, string ReportType, string fromDate, string ToDate, string ActCode, string RptOption1)
        {
            var pap1 = vmrptAcc.SetParamInterCompanyLoan1(WpfProcessAccess.CompInfList[0].comcod, ReportType, fromDate, ToDate, ActCode, RptOption1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);

            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DateTime ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
            this.InterComLoanStat1.Clear();
            this.InterComLoanStat1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccIntComLoanStat1>();
            decimal lblnc1 = 0.00m;
            foreach (var item in this.InterComLoanStat1)
            {
                if ((Math.Abs(item.lonrcv) + Math.Abs(item.lonpay)) > 0)
                {
                    lblnc1 = lblnc1 + item.lonrcv - item.lonpay;
                    item.netlon = lblnc1;
                }
            }
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintInterCompLoanStatus1(this.InterComLoanStat1, this.AutoCompleteActCode.SelectedText.Trim(), fromDate, ToDate, RptOption1, ServerTime1); break;
                case "SS":
                    this.ViewTranList2(); break;
            }


        }

        private void PrintInterCompLoanStatus1(List<HmsEntityAccounting.AccIntComLoanStat1> InterCompLoanStatus1, string title, string fromDate, string ToDate, string RptOption1, DateTime ServerTime1)
        {
            //  Content = "Date Wise Summary", Tag = "DATESUM" 
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: ServerTime1);
            list3[0].RptHeader1 = title;
            list3[0].RptHeader2 = (RptOption1 == "DATESUM" ? "Date Wise Summary - " : "") + "(From " + fromDate + " To " + ToDate + " )";
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccIntComLon1", InterCompLoanStatus1, null, list3, null);
            string WindowTitle1 = title.Remove(0, 4);
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void GetMainSubTrans1(string PrintId, string ReportType, string fromDate, string ToDate, string BrnCod, string SectCod, string ActCode, string SirCode, string VouType)
        {
            if (ActCode.Length < 5 && SirCode.Length < 5)
                return;

            var pap1 = vmrptAcc.SetParamMainSubTrans1(WpfProcessAccess.CompInfList[0].comcod, ReportType, fromDate, ToDate, BrnCod, SectCod, ActCode, SirCode, VouType);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);

            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.AccTrnLst.Clear();
            this.AccTrnLst2 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccLedger1>();
            string mainAc = "";
            if (this.AutoCompleteActCode.SelectedValue != null)
                mainAc = this.AutoCompleteActCode.SelectedValue.ToString().Trim();

            string subAc = "";
            if (this.AutoCompleteSirCode.SelectedValue != null)
                subAc = this.AutoCompleteSirCode.SelectedValue.ToString().Trim();
            string Title = (mainAc.Length > 0 ? "Main" : "") + (mainAc.Length > 0 && subAc.Length > 0 ? " & " : "") + (subAc.Length > 0 ? "Sub." : "") + " A/c (Special " + ReportType + ") - " +
                (mainAc.Length > 0 ? "[" + mainAc + "]" : "") + (mainAc.Length > 0 && subAc.Length > 0 ? "\n" : "") + (subAc.Length > 0 ? "[" + subAc + "]" : "");
            if (this.cmbVouType.SelectedIndex != 0)
            {
                string VType1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Content.ToString().Trim();
                Title = Title + "\n(" + VType1 + ")";
            }
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintallTranlst2(this.AccTrnLst2, Title, fromDate, ToDate); break;
                case "SS":
                    this.ViewTranList2(); break;
            }

        }
        private void GetSubVsMainTrans1(string PrintId, string ReportType, string fromDate, string ToDate, string BrnCod, string SectCod, string ActCode, string SirCode, string VouType)
        {
            if (SirCode.Length < 5)
                return;

            BrnCod = "";
            SectCod = "";
            ActCode = "";

            var pap1 = vmrptAcc.SetParamSubVsMainTrans1(WpfProcessAccess.CompInfList[0].comcod, ReportType, fromDate, ToDate, BrnCod, SectCod, ActCode, SirCode, VouType);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);

            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.AccTrnLst.Clear();
            this.AccTrnLst2 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccLedger1>();
            string SirDesc1 = "";
            if (this.AutoCompleteSirCode.SelectedValue != null)
                SirDesc1 = this.AutoCompleteSirCode.SelectedText.Trim();
            string Title = "Sub. Vs Main A/c (Special " + ReportType + ") - " + SirDesc1;
            if (this.cmbVouType.SelectedIndex != 0)
            {
                string VType1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Content.ToString().Trim();
                Title = Title + "\n(" + VType1 + ")";
            }

            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintallTranlst2(this.AccTrnLst2, Title, fromDate, ToDate); break;
                case "SS":
                    this.ViewTranList2(); break;
            }
        }

        private void GetDateWiseCashBook1(string PrintId, string fromDate, string ToDate, string WithNarr2, string TransSumm2)
        {
            var pap1 = vmrptAcc.SetParamCashBook(WpfProcessAccess.CompInfList[0].comcod, fromDate, ToDate, WithNarr2, TransSumm2);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.CashBookTransList.Clear();
            this.CashBookTransList = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccCashBook1>();

            DateTime ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintallCashBookTranlst1(this.CashBookTransList, "Cash Book", fromDate, ToDate, ServerTime1, TransSumm2);
                    break;
                case "SS":
                    this.ViewCashBookTranList1();
                    break;
            }
        }


        private void ViewCashBookTranList1()
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);

        }

        private void PrintallCashBookTranlst1(List<HmsEntityAccounting.AccCashBook1> CashBookTransLst, string title, string fromDate, string ToDate, DateTime ServerTime1, string TransSumm2)
        {

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: ServerTime1);
            list3[0].RptHeader1 = "Cash Book - " + (TransSumm2.Contains("TRNSUMMARY") ? "[Voucher Wise Summary]" : (TransSumm2.Contains("DATEWISESUM") ? "[Date Wise Summary]" :
                (TransSumm2.Contains("TRNSDETAILSACSUM") ? "[A/c Head Wise Summary]" : (TransSumm2.Contains("TRNSDETAILSAC") ? "[Transaction Head Wise Details]" : "Details"))));

            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccCashBook1", CashBookTransLst, null, list3, null);
            string WindowTitle1 = title.Remove(0, 4);
            string date = "(From " + fromDate + " To " + ToDate + " )";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void GetAllPropTranslst1(string PrintId, string fromDate, string ToDate, string title, string BrnCod)
        {
            BrnCod = (BrnCod == "0000" ? "%" : BrnCod);
            var pap1 = vmrptAcc.SetParamBppTransList(WpfProcessAccess.CompInfList[0].comcod, "RPTBPPTRANS01", fromDate, ToDate, "A", BrnCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.PayPropTrnLst.Clear();
            this.PayPropTrnLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.PayProTransectionList>();

            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintallProTranlst1(this.PayPropTrnLst, title, fromDate, ToDate); break;

                case "SS":
                    this.ViewPayProTranList1(); break;
            }
        }

        private void GetAllPropTranslst2(string ReportId, string PrintId, string fromDate, string ToDate, string title, string BrnCod)
        {
            // ReportId = "RPTBPPTRANS02" // "RPTBPPTRANS03" // "RPTBPPTRANS04"
            BrnCod = (BrnCod == "0000" ? "%" : BrnCod);
            var pap1 = vmrptAcc.SetParamBppTransList(WpfProcessAccess.CompInfList[0].comcod, ReportId, fromDate, ToDate, "A", BrnCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.PayPropTrnLst2.Clear();
            this.PayPropTrnLst2 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.PayProTransectionList2>();

            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintallProTranlst2(this.PayPropTrnLst2, title, fromDate, ToDate); break;

                case "SS":
                    this.ViewPayProTranList1(); break;
            }
        }

        private void PrintallProTranlst1(List<HmsEntityAccounting.PayProTransectionList> PayProTrnLst, string title, string fromDate, string ToDate)
        {
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptPayProTransList1", PayProTrnLst, null, list3, null);
            string WindowTitle1 = title.Remove(0, 4);
            string date = "(From " + fromDate + " To " + ToDate + " )";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void PrintallProTranlst2(List<HmsEntityAccounting.PayProTransectionList2> PayProTrnLst2, string title, string fromDate, string ToDate)
        {
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptPayProTransList2", PayProTrnLst2, null, list3, title);
            string WindowTitle1 = title.Remove(0, 4);
            string date = "(From " + fromDate + " To " + ToDate + " )";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void ViewPayProTranList1()
        {

            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }
        //private void GetCashBankP(string PrintId, string fromDate, string ToDate, string TrTyp)
        //{
        //    var pap1 = vmrptAcc.SetParamReceiptPayment(WpfProcessAccess.CompInfList[0].comcod, "RPCB", fromDate, ToDate, "1234");
        //    DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
        //    if (ds1 == null)
        //        return;
        //}

        private void GetAllTranslst(string PrintId, string fromDate, string ToDate, string title)
        {
            var pap1 = vmrptAcc.SetParamAccTransList(WpfProcessAccess.CompInfList[0].comcod, "C0", fromDate, ToDate, "A", "WITHSUM");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.AccTrnLst.Clear();
            this.AccTrnLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTransectionList>();

            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintallTranlst(this.AccTrnLst, title, fromDate, ToDate); break;
                case "SS":
                    this.ViewTranList1(); break;
            }
        }

        private void GetAllTranslst2(string PrintId, string fromDate, string ToDate, string title, string VouType, string BrnCod, string SectCod, string WithNarr, string TransSumm)
        {

            var pap1 = vmrptAcc.SetParamAccTransList2(WpfProcessAccess.CompInfList[0].comcod, "C2", fromDate, ToDate, "A", VouType, BrnCod, WithNarr, TransSumm);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.AccTrnLst.Clear();
            this.AccTrnLst2 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccLedger1>();

            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintallTranlst2(this.AccTrnLst2, title, fromDate, ToDate); break;
                case "SS":
                    this.ShowGridInfo(ItemTag); break;
            }
        }
        private void PrintallTranlst(List<HmsEntityAccounting.AccTransectionList> AccTrnLst, string title, string fromDate, string ToDate)
        {
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccTransList", AccTrnLst, null, list3, null);
            string WindowTitle1 = title.Remove(0, 4);
            string date = "(From " + fromDate + " To " + ToDate + " )";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void PrintallTranlst2(List<HmsEntityAccounting.AccLedger1> AccTrnLst, string title, string fromDate, string ToDate)
        {
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            Hashtable list4 = new Hashtable();
            list4["Title"] = title;

            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccTransList2", AccTrnLst, null, list3, list4);
            string WindowTitle1 = title.Remove(0, 4);
            string date = "(From " + fromDate + " To " + ToDate + " )";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void GetRecptPayCashFlow(string PrintId, string fromDate, string ToDate, string TrTyp, string BrnId, string SectCod, string AccLevel, string LSirLevel)
        {
            BrnId = (SectCod.Length > 3 ? SectCod : BrnId); //(TrTyp == "A04RPCB" && SectCod.Length > 3 ? SectCod : BrnId);
            string RptName = (TrTyp == "B07IVE" ? "INCVSEXP01" : (TrTyp == "A04RP" ? "DATEWISERP" : (TrTyp == "A04RPCB" ? "DATEWISERPCB" : (TrTyp == "B01RP" ? "RP"
                : (TrTyp == "B01RPCB" ? "RPCB" : (TrTyp == "B02CB" ? "CB" : "CASHFLOW01")))))); //INCVSEXP01//"RP" ;// "CB"; // CASHFLOW01

            var pap1 = vmrptAcc.SetParamReceiptPayment(WpfProcessAccess.CompInfList[0].comcod, RptName, fromDate, ToDate, AccLevel, BrnId, LSirLevel);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
                    string rptFileName1 = (RptName == "CASHFLOW01" ? "Accounting.RptAccCashFlow1" : "Accounting.RptAccRecPay1");
                    //LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccRecPay1", ds1, null, list3, null);
                    LocalReport rpt1 = AccReportSetup.GetLocalReport(rptFileName1, ds1, null, list3, null);
                    string WindowTitle1 = "Accounting Report";// "Account Ledger";
                    string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                    string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
                    break;
                case "SS":
                    this.AccRecPayDg(ds1);
                    break;
            }
        }

        private void GetTrialBalance(string PrintId, string fromDate, string ToDate, string AccLevel, string LSirLevel, string Period)
        {

            var pap1 = vmrptAcc.SetParamTrialBalance(WpfProcessAccess.CompInfList[0].comcod, fromDate, ToDate, AccLevel, LSirLevel, Period);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            ds1.Tables[1].Rows[0]["rptLevel"] = this.TBLevelSetup(ds1.Tables[1].Rows[0]["rptLevel"].ToString().Trim(), ds1.Tables[1].Rows[0]["rptLevel2"].ToString().Trim());
            this.AccTrialBlncLst.Clear();
            this.AccTrialBlncLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTrialBalance1>();
            this.AccTrialBlncTtlLst.Clear();
            this.AccTrialBlncTtlLst = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccTrialBalance1t>();
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintTrialBalance(AccTrialBlncLst, AccTrialBlncTtlLst, "TRIALBALANCE");
                    break;
                case "SS":
                    this.TrialBalanceDg();
                    break;
                case "DP":
                    break;
                case "EXCELF":
                    break;
                default:
                    return;
            }
        }

        private void GetIncomeStatement(string PrintId, string fromDate, string ToDate, string TrHead, string BrnCod, string AccLevel)
        {
            var pap1 = vmrptAcc.SetParamIncomeStatementBS(WpfProcessAccess.CompInfList[0].comcod, "INCOMEST01", fromDate, ToDate, BrnCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            this.AccIncomeStLst.Clear();
            this.AccIncomeStLstTtlLst.Clear();
            this.AccIncomeStLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccIncomeStatement1>();
            this.AccIncomeStLstTtlLst = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccIncomeStatement1t>();
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();//this is test
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintIncomeStatement(this.AccIncomeStLst, this.AccIncomeStLstTtlLst);
                    break;
                case "SS":
                    this.ShowGridInfo(ItemTag);
                    break;
            }
        }
        private void GetBalanceSheet(string PrintId, string fromDate, string ToDate, string TrHead, string AccLevel)
        {
            var pap1 = vmrptAcc.SetParamIncomeStatementBS(WpfProcessAccess.CompInfList[0].comcod, "BALANSHT01", fromDate, ToDate, AccLevel);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            this.AccIncomeStLst.Clear();
            this.AccIncomeStLstTtlLst.Clear();
            this.AccIncomeStLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccIncomeStatement1>();
            this.AccIncomeStLstTtlLst = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccIncomeStatement1t>();
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();//this is test
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintIncomeStatement(this.AccIncomeStLst, this.AccIncomeStLstTtlLst);
                    break;
                case "SS":
                    this.ShowGridInfo(ItemTag);
                    break;
            }

        }

        private void PrintIncomeStatement(List<HmsEntityAccounting.AccIncomeStatement1> IncomeStLst1, List<HmsEntityAccounting.AccIncomeStatement1t> IncomeStLst1t)
        {
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: IncomeStLst1t[0].prndate);
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccIncomeSt1", IncomeStLst1, IncomeStLst1t, list3, null);
            string WindowTitle1 = IncomeStLst1t[0].RptTile;
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }
        private void GetControlSchedule(string PrintId, string fromDate, string ToDate, string BrnCod, string SectCod, string ActCode, string SirCode, string AccLevel, string LSirLevel, string Period, string DrCr)
        {
            var pap1 = vmrptAcc.SetParamAccSchedule(WpfProcessAccess.CompInfList[0].comcod, fromDate, ToDate, BrnCod, SectCod, ActCode, SirCode, AccLevel, LSirLevel, Period, DrCr);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            ds1.Tables[1].Rows[0]["rptLevel"] = this.TBLevelSetup(ds1.Tables[1].Rows[0]["rptLevel"].ToString().Trim(), ds1.Tables[1].Rows[0]["rptLevel2"].ToString().Trim());
            this.AccTrialBlncLst.Clear();
            this.AccTrialBlncLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTrialBalance1>();
            this.AccTrialBlncTtlLst.Clear();
            this.AccTrialBlncTtlLst = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccTrialBalance1t>();

            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintTrialBalance(AccTrialBlncLst, AccTrialBlncTtlLst, "SCHEDULE");
                    break;
                case "SS":
                    this.TrialBalanceDg();
                    break;
                case "DP":
                    break;
                case "EXCELF":
                    break;
                default:
                    return;
            }
        }

        private void AccRecPayDg(object RecPaydtset)
        {

            this.RecPaydtset = RecPaydtset;
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();//this is test
            this.ShowGridInfo(ItemTag);
        }

        private void GetAccLedger(string PrintId, string fromDate, string ToDate, string AccCodeHead, string Nar, string SummLedger, string BrnCod, string SectCod)
        {
            BrnCod = (SectCod.Length > 3 ? SectCod : BrnCod);
            var pap1 = vmrptAcc.SetParamAccLedger(WpfProcessAccess.CompInfList[0].comcod, fromDate, ToDate, AccCodeHead, Nar, SummLedger, BrnCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            double balam1 = 0.00;
            for (int i = 0; i < ds1.Tables[0].Rows.Count - 2; i++)
            {
                if (ds1.Tables[0].Rows[i]["elevel"].ToString().Trim() == "00" || SummLedger == "TRNSUMMARY")
                {
                    balam1 = balam1 + Convert.ToDouble(ds1.Tables[0].Rows[i]["dram"]) - Convert.ToDouble(ds1.Tables[0].Rows[i]["cram"]);
                    if (Convert.ToDouble(ds1.Tables[0].Rows[i]["dram"]) != 0 || Convert.ToDouble(ds1.Tables[0].Rows[i]["cram"]) != 0)
                        ds1.Tables[0].Rows[i]["blancam"] = balam1;
                }
            }

            this.AccLedgerLst.Clear();
            this.AccLedgerLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccLedger1>();
            this.AccLedgerLst2.Clear();
            this.AccLedgerLst2 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccLedger1A>();
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintAccLedger(AccLedgerLst, AccLedgerLst2, "Account Ledger");
                    break;
                case "SS":
                    this.AccledgerDg();
                    break;
            }

        }

        private void AccledgerDg()
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();//this is test
            this.ShowGridInfo(ItemTag);
        }

        private void GetAccSubLedger(string PrintId, string fromDate, string ToDate, string AccCodeHead, string AccSubCodeHead, string Nar, string BrnCod, string SectCod)
        {
            BrnCod = (SectCod.Length > 3 ? SectCod : BrnCod);
            var pap1 = vmrptAcc.SetParamAccSubLedger(WpfProcessAccess.CompInfList[0].comcod, fromDate, ToDate, AccCodeHead, AccSubCodeHead, Nar, BrnCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            double balam1 = 0.00;
            for (int i = 0; i < ds1.Tables[0].Rows.Count - 2; i++)
            {
                if (ds1.Tables[0].Rows[i]["elevel"].ToString().Trim() == "00")
                {
                    balam1 = balam1 + Convert.ToDouble(ds1.Tables[0].Rows[i]["dram"]) - Convert.ToDouble(ds1.Tables[0].Rows[i]["cram"]);
                    if (Convert.ToDouble(ds1.Tables[0].Rows[i]["dram"]) != 0 || Convert.ToDouble(ds1.Tables[0].Rows[i]["cram"]) != 0)
                        ds1.Tables[0].Rows[i]["blancam"] = balam1;
                }
            }


            this.AccLedgerLst.Clear();
            this.AccLedgerLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccLedger1>();
            this.AccLedgerLst2.Clear();
            this.AccLedgerLst2 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccLedger1A>();

            string[] rptOption1 = ((ComboBoxItem)this.cmbRptOptions.SelectedItem).Tag.ToString().Split(',');
            if (rptOption1[0] == "SHORTLEDGER")
            {
                this.AccLedgerLst2[0].booknam = this.AccLedgerLst2[0].booknam.Trim() + " (SHORT FORM)";
                var LedgMain = this.AccLedgerLst.FindAll(x => x.trcode == "000000000000").ToList();
                var LedgNarr = this.AccLedgerLst.FindAll(x => x.trtyp == "NARRATION000000000000000000000000000000000000000" && x.trdesc.Trim().Length > 0).ToList();
                foreach (var item in LedgMain)
                {
                    var LedgNarr1 = LedgNarr.FindAll(x => x.vounum == item.vounum);
                    if (LedgNarr1.Count > 0)
                        item.trdesc = LedgNarr1[0].trdesc;

                    if (item.trdesc.Trim().Length == 0 && item.vounum.Substring(0, 3) == "RVC")
                        item.trdesc = "Cash Collection";

                    if (item.trdesc.Trim().Length == 0 && item.vounum.Substring(0, 3) == "PVC")
                        item.trdesc = "Cash Payment";

                    if (item.drcr == "1O" || item.drcr == "3T" || item.drcr == "4C")
                        item.vounum1 = "";

                }
                this.AccLedgerLst = LedgMain.ToList();
            }


            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintAccLedger(AccLedgerLst, AccLedgerLst2, "Account Subsidiary Ledger");
                    break;
                case "SS":
                    this.AccledgerDg();
                    break;
            }

        }
        private void PrintAccLedger(List<HmsEntityAccounting.AccLedger1> AccLedgerLst, List<HmsEntityAccounting.AccLedger1A> AccLedgerLst2, string winTitle)
        {
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            string[] rptOption1 = ((ComboBoxItem)this.cmbRptOptions.SelectedItem).Tag.ToString().Split(',');
            string rptName1 = (rptOption1[0] == "SHORTLEDGER" ? "Accounting.RptAccLedger2" : "Accounting.RptAccLedger1");
            LocalReport rpt1 = AccReportSetup.GetLocalReport(rptName1, AccLedgerLst, AccLedgerLst2, list3, null);
            string WindowTitle1 = winTitle;// "Account Ledger";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private string TBLevelSetup(string Level1 = "", string Level2 = "")
        {
            string Level1a = "";
            for (int i = 0; i < Level1.Length; i++)
            {
                Level1a += Level1.Substring(i, 1) + ", ";
            }
            Level1a = Level1a.Substring(0, Level1a.Length - 2);
            Level1a = Level1a + (Level2.Length > 0 ? " / " + (Level2 == "B" ? "Branch" : (Level2 == "L" ? "Location" : "Sub-" + Level2)) : "");
            return Level1a;
        }

        private void PrintTrialBalance(List<HmsEntityAccounting.AccTrialBalance1> list1, List<HmsEntityAccounting.AccTrialBalance1t> list2, string rptName = "TRIALBALANCE")
        {
            string TrHead = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString().ToUpper();
            string AcDesc1 = "";
            if (this.AutoCompleteActCode.SelectedValue != null)
                AcDesc1 = this.AutoCompleteActCode.SelectedText.Trim(); ;

            string AcHead = (rptName == "SCHEDULE" && AcDesc1.Length > 0 ? " [" + AcDesc1 + "] " : "");
            if (list1 == null)
                return;

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);

            var list4 = new Hashtable();
            string fromDate = xctk_dtpFrom.Text.Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();
            string fromDatep = Convert.ToDateTime(fromDate).AddDays(-1).ToString("dd-MMM-yyyy");

            list4["ToDate"] = ToDate;
            list4["fromDatep"] = fromDatep;
            list4["fromDate"] = fromDate;
            list4["RptTitle"] = TrHead.Remove(0, 3) + AcHead + " - (Level - " + list2[0].rptLevel + ")";
            list4["Period"] = "(From " + fromDate + " To " + ToDate + ")" + (list2[0].CurrPeriod.Trim().Length > 0 ? " - " + list2[0].CurrPeriod.Trim() : "");
            list4["ReportType"] = rptName;
            string brncod1 = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            string sectcod1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            if (rptName == "SCHEDULE" && sectcod1 != "000000000000")
            {
                string brnname1 = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Content.ToString().Trim();
                string sectname1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Content.ToString().Trim();
                list4["RptTitle"] = list4["RptTitle"] + " - " + (sectcod1.Substring(7, 5) == "00000" ? brnname1 : sectname1);

            }

            //=Parameters!ParamTitle.Value & " - (Level - " & First(Fields!rptLevel.Value, "RptDataSet2") & ")"
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccTrialBal1", list1, list2, list3, list4);

            string WindowTitle1 = TrHead.Remove(0, 3);// "Account Trial Balance";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }
        private void TrialBalanceDg()
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();//this is test
            this.ShowGridInfo(ItemTag);
        }
        private void ViewTranList1()
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();//this is test
            this.ShowGridInfo(ItemTag);
        }

        private void ViewTranList2()
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();//this is test
            this.ShowGridInfo(ItemTag);

        }
        private void dgOverall01_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void dgOverall01_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ////try
            ////{
            ////    string TrTyp = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            ////    if (TrTyp == "D01TPL")
            ////    {
            ////        var item1p = (HmsEntityAccounting.PayProTransectionList)this.dgOverall01.SelectedItem;
            ////        this.PrintProposalMemo(item1p.bppnum);
            ////    }
            ////    else
            ////    {
            ////        var item1a = (HmsEntityAccounting.AccTransectionList)this.dgOverall01.SelectedItem;
            ////        this.PrintVoucherMemo(item1a.vounum);
            ////    }
            ////}
            ////catch (Exception)
            ////{
            ////    return;
            ////}
        }

        private void PrintVoucherMemo(string memoNum)
        {
            try
            {
                LocalReport rpt1 = null;
                var pap1 = vmrptAcc.SetParamAccVoucher(WpfProcessAccess.CompInfList[0].comcod, memoNum);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var list1 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccVoucher1>();
                var trnsList = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTransectionList>();


                // select preparebyid, PreparByName, prepareses, preparetrm, rowid, rowtime, ServerTime = getdate() from #tblv1
                string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                                    + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);

                //var list3 = WpfProcessAccess.GetRptGenInfo();
                string memoName = memoNum.Substring(0, 3).Trim();
                byte[] comlogoBytes = WpfProcessAccess.CompInfList[0].comlogo;

                var list4 = new HmsEntityAccounting.AccVoucher1p();
                list4.comlogo = comlogoBytes;
                list4.inWord = ASITFunLib.ASITUtility.Trans(double.Parse(list1.Sum(q => q.cramt).ToString()), 2);
                //l.inWord = ASITFunLib.ASITUtility2.UppercaseWords("");
                string rptName = "Accounting.RptAccVou1"; // (list1.Count > 7 ? "Accounting.RptAccVou1" : "Accounting.RptAccVou1h");
                rpt1 = AccReportSetup.GetLocalReport(rptName, list1, trnsList, list3, list4);

                string WindowTitle1 = "Accounts Voucher";
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Acc-Gvm-111: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void PrintProposalMemo(string memoNum)
        {
            LocalReport rpt1 = null;
            var pap1 = vmrptAcc.SetParamBppTrans(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var list1 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.PayProTrans1>();
            var trnsList = ds1.Tables[0].DataTableToList<HmsEntityAccounting.PayProTransectionList>();
            // select preparebyid, PreparByName, prepareses, preparetrm, rowid, rowtime, ServerTime = getdate() from #tblv1
            string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                                + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
            string memoName = memoNum.Substring(0, 3).Trim();
            byte[] comlogoBytes = WpfProcessAccess.CompInfList[0].comlogo;

            var list4 = new HmsEntityAccounting.AccVoucher1p();
            list4.comlogo = comlogoBytes;
            list4.inWord = ASITFunLib.ASITUtility.Trans(double.Parse(list1[0].bppam.ToString()), 2);
            //l.inWord = ASITFunLib.ASITUtility2.UppercaseWords("");
            string rptName = "Accounting.RptPayProTran1";
            rpt1 = AccReportSetup.GetLocalReport(rptName, list1, trnsList, list3, list4);
            //rpt1.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(bytes)));
            string WindowTitle1 = "Budget Proposal Memo";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void cmbActGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cmbActGroup.ToolTip = ((ComboBoxItem)((ComboBox)sender).SelectedItem).ToolTip;
            this.AutoCompleteActCode.SelectedValue = null;
        }

        private void cmbSirGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cmbSirGroup.ToolTip = ((ComboBoxItem)((ComboBox)sender).SelectedItem).ToolTip;
            this.AutoCompleteSirCode.SelectedValue = null;
        }

        private void tvRptTitle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ////this.dgOverall01.ItemsSource = null;
            ////this.dgOverall01.Columns.Clear();
            string ItemTitle = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString();
            string ItemTag = ((TreeViewItem)((TreeView)sender).SelectedItem).Tag.ToString();
            this.ShowRequiredOptions(ItemTag);
            ////this.lbltle1.Content = ItemTitle;
            string Msg1 = (ItemTag == "B03CF" ? "(This option is under construction)" : "");
            ////this.lbltle2.Content = Msg1;// ItemTag;
        }

        private void ShowRequiredOptions(string ItemTag)
        {
            this.cmbRptOptions.Items.Clear();
            this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Default", Tag = "NONE" });
            this.cmbRptOptions.SelectedIndex = 0;
            this.stkOptBranch.Visibility = Visibility.Hidden;
            this.stkOptVouType.Visibility = Visibility.Hidden;
            this.stkOptLocation.Visibility = Visibility.Hidden;
            this.stkOptActCode.Visibility = Visibility.Hidden;
            this.stkOptSirCode.Visibility = Visibility.Hidden;

            this.stkOptTB.Visibility = Visibility.Hidden;
            this.stkOptMore.Visibility = Visibility.Hidden;
            this.stkOptSchedule.Visibility = Visibility.Hidden;
            this.stkOptDrCrColumns.Visibility = Visibility.Hidden;
            this.stkRptOptions.Visibility = Visibility.Hidden;

            if (ItemTag == "A01TVL")
            {
                //<TreeViewItem Header="01. VOUCHER LIST" Tag = "A01TVL"/>
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkOptVouType.Visibility = Visibility.Visible;

                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "A02TL")
            {
                //<TreeViewItem Header="02. TRANSACTION LIST" Tag = "A02TL"/>
                //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details With Narration", Tag = "NARRATION,TRNSDETAILS" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details Without Narration", Tag = "WITHOUTNARRATION,TRNSDETAILS" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary Without Narration", Tag = "WITHOUTNARRATION,TRNSUMMARY" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary With Narration", Tag = "NARRATION,TRNSUMMARY" });
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkOptVouType.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "A03CBD")
            {
                //  <TreeViewItem Header="03. CASH BOOK DETAILS" Tag = "A03CBD"/>
                //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "With All Details Information", Tag = "NARRATION,TRNSDETAILS" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details Without Narration", Tag = "WITHOUTNARRATION,TRNSDETAILS" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Head Wise Details", Tag = "WITHOUTNARRATION,TRNSDETAILSAC" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "A/c Head Wise Summary", Tag = "WITHOUTNARRATION,TRNSDETAILSACSUM" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary Without Narration", Tag = "WITHOUTNARRATION,TRNSUMMARY" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary With Narration", Tag = "NARRATION,TRNSUMMARY" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Date Wise Summary", Tag = "WITHOUTNARRATION,DATEWISESUM" });
                this.stkRptOptions.Visibility = Visibility.Visible;

            }
            else if (ItemTag == "A04RPCB")
            {
                // <TreeViewItem Header="01. RECEIPTS &amp; PAYMENTS" Tag = "B01RPCB"/>
                // <TreeViewItem Header = "04. Cash book details (R/P form)", Tag = "A04RPCB"/>;
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "With Cash & Bank Balance", Tag = "WITHBALANCE" });
                this.cmbRptOptions.SelectedIndex = 1;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "B01RPCB")
            {
                // <TreeViewItem Header="01. RECEIPTS &amp; PAYMENTS" Tag = "B01RPCB"/>
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "With Cash & Bank Balance", Tag = "WITHBALANCE" });
                //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary with Narration", Tag = "VOUSUMMARY,NARRATION" });
                //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary", Tag = "TRNSUMMARY,NOLOCATION" });
                //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary with Location", Tag = "TRNSUMMARY,LOCATIONWISE" });
                this.cmbRptOptions.SelectedIndex = 1;
                this.cmbSubLevel.SelectedIndex = 0;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptTB.Visibility = Visibility.Visible;
                this.stkOptMore.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;

            }
            else if (ItemTag == "B04CL")
            {
                // <TreeViewItem Header="04. CONTROL LEDGER" Tag = "B04CL"/>
                // (3 Reports) Control Ledger, Control Ledger Voucher Summary, Control Ledger Transaction Head Summary
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary", Tag = "VOUSUMMARY,WITHOUTNARR" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary with Narration", Tag = "VOUSUMMARY,NARRATION" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary", Tag = "TRNSUMMARY,NOLOCATION" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary with Location", Tag = "TRNSUMMARY,LOCATIONWISE" });
                this.cmbRptOptions.SelectedIndex = 0;
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkOptActCode.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "B05SL")
            {
                // <TreeViewItem Header="05. SUBSIDIARY LEDGER" Tag="B05SL"/>
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Subsidiary Ledger - Short Form", Tag = "SHORTLEDGER,NARRATION" });
                ////this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary", Tag = "VOUSUMMARY,WITHOUTNARR" });
                ////this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary with Narration", Tag = "VOUSUMMARY,NARRATION" });
                ////this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary", Tag = "TRNSUMMARY,NOLOCATION" });
                ////this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary with Location", Tag = "TRNSUMMARY,LOCATIONWISE" });
                this.cmbRptOptions.SelectedIndex = 0;
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkOptActCode.Visibility = Visibility.Visible;
                this.stkOptSirCode.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "B06CS")
            {
                // <TreeViewItem Header="06. CONTROL SCHEDULE" Tag="B06CS"/>
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkOptActCode.Visibility = Visibility.Visible;
                this.stkOptTB.Visibility = Visibility.Visible;
                this.stkOptMore.Visibility = Visibility.Visible;
                this.stkOptSchedule.Visibility = Visibility.Visible;
                this.stkOptDrCrColumns.Visibility = Visibility.Visible;
                this.cmbPeriod.SelectedIndex = 0;
                this.cmbDrCr.SelectedIndex = 0;
            }
            else if (ItemTag == "B07IVE")
            {
                //TreeViewItem { Header = "07. Income Vs Expense", Tag = "B07IVE" };
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "C01TB")
            {
                //  <TreeViewItem Header="01. TRIAL BALANCE" Tag = "C01TB"/>
                this.stkOptTB.Visibility = Visibility.Visible;
                this.stkOptMore.Visibility = Visibility.Visible;
                this.cmbSubLevel.SelectedIndex = 0;
                this.stkOptSchedule.Visibility = Visibility.Visible;
                this.cmbPeriod.SelectedIndex = 0;
            }
            else if (ItemTag == "C02IS")
            {
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptTB.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "C03BS")
            {
                this.chkLevel1.IsChecked = true;
                this.chkLevel2.IsChecked = true;
                this.chkLevel3.IsChecked = false;
                this.chkLevel4.IsChecked = false;
                this.stkOptTB.Visibility = Visibility.Visible;

            }
            else if (ItemTag == "A03CBD")
            {

            }
            else if (ItemTag == "D01TPL" || ItemTag == "D02ATL" || ItemTag == "D03CWS" || ItemTag == "D04OPB")
            {
                //    <TreeViewItem Header="01. PAYMENT PROPOSAL LIST" Tag = "D01TPL"/>
                //    <TreeViewItem Header="02. ALL TRANSACTION LIST" Tag="D02ATL"/>
                //    <TreeViewItem Header="03. CATEGORY WISE SUMMARY" Tag="D03CWS"/>
                //    <TreeViewItem Header="04. OVERALL PAYMENT BUDGET" Tag="D04OPB"/>
                this.stkOptBranch.Visibility = Visibility.Visible;
                //this.stkOptLocation.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "E01HTL" || ItemTag == "E02HTS")
            {
                //tvi5.Items.Add(new TreeViewItem { Header = "01. MAIN/SUB HEAD TRANS. LIST", Tag = "E01HTL" });
                //tvi5.Items.Add(new TreeViewItem { Header = "02. MAIN/SUB HEAD TRANS. SUMMARY", Tag = "E02HTS" });
                this.stkOptBranch.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkOptActCode.Visibility = Visibility.Visible;
                this.stkOptSirCode.Visibility = Visibility.Visible;
                this.stkOptVouType.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "E03SMTL" || ItemTag == "E04SMTS")
            {
                //tvi5.Items.Add(new TreeViewItem { Header = "03. SUB VS MAIN HEAD DETAILS", Tag = "E03SMTL" });
                //tvi5.Items.Add(new TreeViewItem { Header = "04. SUB VS MAIN HEAD SUMMARY", Tag = "E04SMTS" });
                this.stkOptSirCode.Visibility = Visibility.Visible;
                this.stkOptVouType.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "E05ICLDT")
            {
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details With Narration", Tag = "NARRATION" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Date Wise Summary", Tag = "DATESUM" });

                this.cmbActGroup.SelectedIndex = 0;

                this.stkOptActCode.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            if (this.LastSelectedItem == "E05ICLDT" || this.stkOptActCode.Visibility != Visibility.Visible)
                this.AutoCompleteActCode.SelectedValue = null;

            if (this.stkOptSirCode.Visibility != Visibility.Visible)
                this.AutoCompleteSirCode.SelectedValue = null;

            this.LastSelectedItem = ItemTag;

        }

        private void CreateNewTabForReport(DataGrid dgRpt1t)
        {
            if (dgRpt1t == null)
                return;
            string fromDate = xctk_dtpFrom.Text.Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();

            string ItemTitle = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString().ToUpper(); // ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString().ToUpper();
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowRequiredOptions(ItemTag);
            string Msg1 = (ItemTag == "C05PAP" || ItemTag == "D07POR" ? "(This option is under construction)" : "");

            dgRpt1t.HorizontalAlignment = HorizontalAlignment.Center;
            dgRpt1t.MouseDoubleClick += GridInfoClick;
            dgRpt1t.PreviewKeyDown += dgRpt1t_PreviewKeyDown;
            dgRpt1t.PreviewKeyUp += dgRpt1t_PreviewKeyUp;
            var uc2 = new UserControls.TabItemGridReport();// ASITHmsWpf.Manpower.frmMessagegMgt103();
            uc2.lbltle1.Content = ItemTitle;
            uc2.lbltle2.Content = " From " + fromDate + " To " + ToDate; //Msg1;
            uc2.stkpDataGrid.Children.Add(dgRpt1t);
            TabItemIndex1++;
            UserControls.TabItemWithButton tbi1c = new UserControls.TabItemWithButton(_header1: "Report - " + TabItemIndex1.ToString("00"), _uc1: uc2);
            tbi1c.txtb1.ToolTip = ItemTitle;
            tbi1c.btn1.MouseDoubleClick += Btn1_MouseDoubleClick;
            tbi1c.btn1.KeyUp += Btn1_KeyUp;
            this.TabUcGrid1.Items.Add(tbi1c);
            this.TabUcGrid1.SelectedIndex = this.TabUcGrid1.Items.Count - 1;
            this.TabUcGrid1.Visibility = Visibility.Visible;
        }

        void dgRpt1t_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.GridInfoClick(sender, e);
        }

        void dgRpt1t_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var dgRpt1t = (DataGrid)sender;
            switch (e.Key)
            {
                case Key.Enter:
                    dgRpt1t.CommitEdit(DataGridEditingUnit.Cell, false);
                    dgRpt1t.CommitEdit(DataGridEditingUnit.Row, false);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void ShowGridInfo(string ItemTag)
        {
            try
            {
                if (this.TabUcGrid1.Items.Count > 8)
                    return;

                switch (ItemTag)
                {
                    case "A01TVL":

                        this.CreateNewTabForReport(GridReportAcc1.ViewTranList1.GetDataGrid(this.AccTrnLst.ToList()));
                        break;
                    case "A02TL":
                        this.CreateNewTabForReport(GridReportAcc1.ViewTranList2.GetDataGrid(this.AccTrnLst2.ToList()));
                        break;
                    case "A03CBD":
                        this.CreateNewTabForReport(GridReportAcc1.CashBookTranList1.GetDataGrid(this.CashBookTransList.ToList()));
                        break;
                    case "A04RPCB":
                    case "B01RPCB":
                    case "B02CB":
                    case "B07IVE":
                        this.CreateNewTabForReport(GridReportAcc1.AccRecPay.GetDataGrid(this.RecPaydtset));
                        break;
                    case "B04CL":
                    case "B05SL":
                        this.CreateNewTabForReport(GridReportAcc1.AccLedger.GetDataGrid(this.AccLedgerLst.ToList()));
                        break;
                    case "B06CS":
                    case "C01TB":
                        this.CreateNewTabForReport(GridReportAcc1.TrialBalance.GetDataGrid(this.AccTrialBlncLst.ToList()));
                        break;
                    case "C02IS":
                    case "C03BS":
                        this.CreateNewTabForReport(GridReportAcc1.IncomeStatement.GetDataGrid(this.AccIncomeStLst.ToList()));
                        break;
                    case "D04OPB":
                    case "D03CWS":
                        this.CreateNewTabForReport(GridReportAcc1.IncomeStatement.GetDataGrid(this.AccIncomeStLst.ToList()));
                        break;
                    case "E01HTL":
                    case "E02HTS":
                    case "E03SMTL":
                    case "E04SMTS":
                        this.CreateNewTabForReport(GridReportAcc1.IncomeStatement.GetDataGrid(this.AccIncomeStLst.ToList()));
                        break;
                    case "E05ICLDT":
                        this.CreateNewTabForReport(GridReportAcc1.InterCompLoanStatus1.GetDataGrid(this.InterComLoanStat1.ToList()));
                        break;
                    case "E06ICLSU":
                        this.CreateNewTabForReport(GridReportAcc1.InterCompLoanSum1.GetDataGrid(this.InterComLoanSum1.ToList()));
                        break;
                    default:
                        break;
                }
                //this.dgRpt1.MouseDoubleClick += this.GridInfoClick;
            }

            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Acc-Gvm-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void GridInfoClick(object sender, RoutedEventArgs e)
        {
            var datagrid = (DataGrid)sender;

            if (datagrid.SelectedItem == null)
                return;

            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            switch (ItemTag)
            {
                case "A01TVL":
                    var item1 = (HmsEntityAccounting.AccTransectionList)datagrid.SelectedItem;
                    string memoNum = item1.vounum;
                    this.PrintVoucherMemo(memoNum);
                    break;
                case "A02TL":
                    var item2 = (HmsEntityAccounting.AccLedger1)datagrid.SelectedItem;
                    string memoNum2 = item2.vounum;
                    this.PrintVoucherMemo(memoNum2);
                    break;
                default:
                    break;
            }
        }

        private void tvRptTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnGenerate_Click(null, null);
        }

        private void tvRptTitle_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            this.cmbOutputOption.ComboBox_ContextMenuOpening(null, null);
        }

        private void tvRptTitle_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            string yy = this.cmbOutputOption.Uid.ToString();
            if (yy != "NONE")
                this.btnGenerate_Click(null, null);
        }

        private void tvRptTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.btnGenerate_Click(null, null);
        }



        private void cmbSectCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbSectCod.SelectedItem == null)
                return;
            var cbi1 = (ComboBoxItem)this.cmbSectCod.SelectedItem;
            //this.cmbSectCod.Foreground = cbi1.Foreground;
            //this.cmbSectCod.FontWeight = cbi1.FontWeight;

            this.cmbSectCod.ToolTip = cbi1.ToolTip.ToString();
        }

        private void Btn1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.RemoveTabItem(((Button)sender).Tag.ToString());
        }
        private void RemoveTabItem(string tag1 = "Nothing")
        {
            foreach (TabItem item1 in this.TabUcGrid1.Items)
            {
                if (tag1 == item1.Tag.ToString())
                {
                    this.TabUcGrid1.Items.Remove(item1);
                    break;
                }
            }
            if (this.TabUcGrid1.Items.Count == 0)
                this.TabUcGrid1.Visibility = Visibility.Collapsed;
        }
        private void Btn1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.RemoveTabItem(((Button)sender).Tag.ToString());
        }
        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            TabItem item = (TabItem)sender;
            if (item != null && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(item, item, DragDropEffects.All);
        }
        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            TabItem target = (TabItem)sender;
            TabItem source = (TabItem)e.Data.GetData(typeof(TabItem));
            if (source != null && target != null && !source.Equals(target))
            {
                TabControl tab = (TabControl)source.Parent;
                int sourceIndex = tab.Items.IndexOf(source);
                int targetIndex = tab.Items.IndexOf(target);
                tab.Items.Remove(source);
                tab.Items.Insert(targetIndex, source);

                // For Place Swiping of tab items
                //tab.Items.Remove(target);
                //tab.Items.Insert(sourceIndex, target);
            }
        }

        private void AutoCompleteActCode_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetAcoountsDesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityGeneral.AcInfCodeBook> GetAcoountsDesc(string Pattern)
        {


            if (WpfProcessAccess.AccCodeList == null)
            {
                WpfProcessAccess.GetAccCodeList();
                WpfProcessAccess.AccCodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
                {
                    return x.actdesc.CompareTo(y.actdesc);
                });
            }

            // match on contain (could do starts with) 
            string TrTyp = "";
            if (this.tvRptTitle.SelectedItem != null)
                TrTyp = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            //"B06CS":

            var ActcodeList = new List<HmsEntityGeneral.AcInfCodeBook>();
            string actg0 = ((ComboBoxItem)this.cmbActGroup.SelectedItem).Tag.ToString().Trim();

            switch (actg0)
            {
                case "DETAILS":
                    if (TrTyp == "E05ICLDT")
                    {
                        var ActcodeList2 = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(8, 4) != "0000" && x.actcode.Substring(0, 4) == "2203").ToList();
                        foreach (var item1 in ActcodeList2)
                            ActcodeList.Add(new HmsEntityGeneral.AcInfCodeBook()
                            {
                                comcod = item1.comcod,
                                actcode = item1.actcode,
                                actcode1 = item1.actcode1,
                                actdesc = item1.actdesc,
                                actdesc1 = item1.actcode.Substring(7) + " - " + item1.actdesc.Trim().Substring(0, item1.actdesc.Trim().Length - 1) + " / PAID)",
                                actelev = item1.actelev,
                                acttdesc = item1.acttdesc,
                                acttype = item1.acttype,
                                rowid = item1.rowid,
                                rowtime = item1.rowtime
                            });

                        //item1.actdesc1 = item1.actcode.Substring(7) + " - " + item1.actdesc.Trim().Substring(0, item1.actdesc.Trim().Length - 1) + " / PAID)";
                    }
                    else
                        ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(8, 4) != "0000").ToList();
                    break;
                case "MAIN":
                    ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(2, 10) == "0000000000").ToList();
                    break;
                case "LEVEL2":
                    ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(4, 8) == "00000000" && x.actcode.Substring(2, 10) != "0000000000").ToList();
                    break;
                case "LEVEL3":
                    ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(8, 4) == "0000" && x.actcode.Substring(4, 8) != "00000000").ToList();
                    break;
                default:
                    string actg1 = actg0.Substring(0, 4);
                    actg1 = (actg1.Substring(2, 2) == "00" ? actg1.Substring(0, 2) : actg1);
                    if (TrTyp == "B06CS")
                        ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(0, actg1.Length) == actg1).ToList();
                    else
                        ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(0, actg1.Length) == actg1 && x.actcode.Substring(8, 4) != "0000").ToList();
                    break;
            }

            /*
                         else if (ItemTag == "E05ICLDT")
            {
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details With Narration", Tag = "NARRATION" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Date Wise Summary", Tag = "DATESUM" });

                this.cmbActGroup.SelectedIndex = 0;
                this.ActcodeList1234 = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(8, 4) != "0000" && x.actcode.Substring(0, 4) == "2203").ToList();
                foreach (var item1 in this.ActcodeList1234)
                    item1.actdesc1 = item1.actcode.Substring(7) + " - " + item1.actdesc.Trim().Substring(0, item1.actdesc.Trim().Length - 1) + " / PAID)";

             */



            return new ObservableCollection<HmsEntityGeneral.AcInfCodeBook>(
                ActcodeList.Where((x, match) => x.actdesc1.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(200).OrderBy(m => m.actdesc1));
        }

        private void AutoCompleteSirCode_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetItemSirdesc(string Pattern)
        {
            // match on contain (could do starts with) 

            if (WpfProcessAccess.AccSirCodeList == null)
            {
                WpfProcessAccess.GetAccSirCodeList();
                WpfProcessAccess.AccSirCodeList.Sort(delegate(HmsEntityGeneral.SirInfCodeBook x, HmsEntityGeneral.SirInfCodeBook y)
                {
                    return x.sirdesc.CompareTo(y.sirdesc);
                });
            }

            var SirCodeList = new List<HmsEntityGeneral.SirInfCodeBook>();
            string sirg0 = ((ComboBoxItem)this.cmbSirGroup.SelectedItem).Tag.ToString().Trim();
            //00-00-000-00-000
            switch (sirg0)
            {
                case "DETAILS":
                    SirCodeList = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(9, 3) != "000");
                    break;
                case "MAIN":
                    SirCodeList = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(2, 10) == "0000000000");
                    break;
                case "LEVEL2":
                    SirCodeList = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(4, 8) == "00000000" && x.sircode.Substring(2, 10) != "0000000000");
                    break;
                case "LEVEL3":
                    SirCodeList = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(7, 5) == "00000" && x.sircode.Substring(4, 8) != "00000000");
                    break;
                case "LEVEL4":
                    SirCodeList = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(9, 3) == "000" && x.sircode.Substring(7, 5) != "00000");
                    break;
                default:
                    string sirg1 = sirg0.Substring(0, 7);
                    sirg1 = (sirg1.Substring(4, 3) == "000" ? sirg1.Substring(0, 4) : sirg1);
                    sirg1 = (sirg1.Substring(2, 2) == "00" ? sirg1.Substring(0, 2) : sirg1);
                    SirCodeList = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(0, sirg1.Length) == sirg1 && x.sircode.Substring(9, 3) != "000");
                    break;
            }

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                SirCodeList.Where((x, match) => x.sirdesc1.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(200).OrderBy(m => m.sirdesc1));
        }

    }
}
