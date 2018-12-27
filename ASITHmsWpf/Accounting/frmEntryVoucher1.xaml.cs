using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using ASITHmsRpt1GenAcc.Accounting;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;
using System.Globalization;
using ASITHmsViewMan.General;

namespace ASITHmsWpf.Accounting
{
    /// <summary>
    /// Interaction logic for frmEntryVoucher1.xaml
    /// </summary>
    public partial class frmEntryVoucher1 : UserControl
    {
        private string TitaleTag1, TitaleTag2, TitaleTag3;  // 
        private bool FrmInitialized = false;

        private List<vmEntryVoucher1.VouTable> ListVouTable1 = new List<vmEntryVoucher1.VouTable>();
        private List<vmEntryVoucher1.VouTableMaster> ListVouTable1All = new List<vmEntryVoucher1.VouTableMaster>();

        private List<vmEntryVoucher1.ChqBlankLeaf> ListBlnkCheq1 = new List<vmEntryVoucher1.ChqBlankLeaf>();

        private vmEntryVoucher1 vm1 = new vmEntryVoucher1();

        private List<HmsEntityGeneral.AcInfCodeBook> CactcodeList = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<HmsEntityGeneral.AcInfCodeBook> ActcodeList = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<HmsEntityGeneral.AcInfCodeBook> CurrencyCodeList = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<HmsEntityAccounting.AccTransectionList> AccTrnLst = new List<HmsEntityAccounting.AccTransectionList>();

        private List<vmHmsGeneralList1.DraftTransactionList> DraftTransactionList1 = new List<vmHmsGeneralList1.DraftTransactionList>();

        private vmReportAccounts1 vmrptAcc = new vmReportAccounts1();
        private string PrevVounum = "XXXXXXXXXXXXXXXXXX";
        private bool IsActiveTransListWindow { get; set; }

        private DataSet EditDs;
        private string CalcObjName = "NoName";

        private string preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
        private string prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
        private string preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
        private DateTime rowtime1 = DateTime.Now;

        private DispatcherFrame DispatcherFrame1;
        private string DraftMemoNum = "";
        private Int64 DraftMemoRowID = 0;

        // For Payment Budget Proposal Entry
        //------------------------------------------------------------------
        private DispatcherFrame frameFindPayProp;
        private List<vmEntryVoucher1.PayBgdBalance> PayBgdBalDetails = new List<vmEntryVoucher1.PayBgdBalance>();
        private List<vmEntryVoucher1.PayBgdBalance> PayBgdBalSum = new List<vmEntryVoucher1.PayBgdBalance>();
        //------------------------------------------------------------------
        public frmEntryVoucher1()
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
            try
            {
                if (DesignerProperties.GetIsInDesignMode(this))
                    return;
                if (!this.FrmInitialized)
                {
                    this.FrmInitialized = true;
                    this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-7); // Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
                    this.xctk_dtpToDate.Value = DateTime.Today;
                    // For Payment Budget Proposal Entry
                    //---------------------------------------------------------------------
                    this.GridFindPayProp.Visibility = Visibility.Collapsed;
                    this.btnFindBudget.Visibility = Visibility.Collapsed;
                    this.cmbDrCr.Visibility = Visibility.Visible;
                    //--------------------------------------------------------------------
                    this.chkShowDraft.IsChecked = false;
                    this.chkAllowDraft.IsChecked = true;
                    this.GridDraftList.IsEnabled = false;
                    this.GridDraftList.Visibility = Visibility.Collapsed;

                    //--------------------------------------------------------------------
                    string[] tagPart1 = this.Tag.ToString().Trim().Split(',');
                    this.TitaleTag2 = (tagPart1.Length > 0 ? tagPart1[0].Trim() : ""); //this.Tag.ToString(); // Dynamic value of Tag property set at design time
                    this.TitaleTag3 = (tagPart1.Length > 1 ? tagPart1[1].Trim() : "");

                    this.ActivateAuthObjects();

                    this.xctk_dtpVouDat.Value = DateTime.Today;
                    this.xctk_dtpVouDat.Minimum = DateTime.Today.AddDays(-365 * 3);
                    this.xctk_dtpVouDat.Maximum = DateTime.Today.AddDays(365 * 2);
                    this.xctk_dtpVouDat.Tag = "01-Jan-1900";
                    this.HideObjects_On_Load();
                    this.FindVouBrnchForThisTerminal();
                    this.FindCurrencyList();
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ActivateAuthObjects()
        {

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



            this.cmbVouType.Items.Clear();
            var vtypeList = HmsEntityAccounting.GetVoucherType().FindAll(x => x.vtitle.ToUpper().Contains(this.TitaleTag2.ToUpper())).ToList();// && !x.vtitle.ToUpper().Contains("BUDGET"));
            if (this.TitaleTag3.Length > 0)
                vtypeList = vtypeList.FindAll(x => x.vtagid.Contains(this.TitaleTag3)).ToList();

            foreach (var item1 in vtypeList)
            {
                string uicode1 = "WPF_frmEntryVoucher1_cmbVouType_" + item1.vtagid;
                var findid1 = WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == uicode1);
                if (findid1 != null)
                    this.cmbVouType.Items.Add(new ComboBoxItem() { Content = item1.vtitle.ToUpper(), Tag = item1.vtagid, ToolTip = item1.vtitle.ToUpper() });
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryVoucher1_chkDateBlocked") == null)
            {
                this.chkDateBlocked.Visibility = Visibility.Collapsed;
                this.lblDateBlocked.Visibility = Visibility.Visible;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryVoucher1_chkAutoTransList") == null)
                this.chkAutoTransList.Visibility = Visibility.Hidden;

            this.btnRecurring.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryVoucher1_btnVouCopy") == null)
                this.btnVouCopy.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryVoucher1_btnVouEdit") == null)
                this.btnVouEdit.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryVoucher1_btnVouCancel") == null)
                this.btnVouCancel.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryVoucher1_stkpCurrency") == null)
            {
                //this.stkpCurrency.IsEnabled = false;
                this.stkpCurrency.Visibility = Visibility.Collapsed;
                //this.txtVouNar.Height = 55;
                //this.txtVouNar.Width = this.txtVouNar.Width + 65 + 70;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryVoucher1_chkAllowDraft") == null)
            {
                this.chkAllowDraft.IsChecked = false;
                this.stkpDraftOption.Visibility = Visibility.Hidden;
            }
        }
        private void HideObjects_On_Load()
        {
            try
            {
                this.gridTransList.IsEnabled = false;
                this.gridTransList.Visibility = Visibility.Collapsed;
                this.gridDetails.Visibility = Visibility.Collapsed;
                this.gridDetails1.Visibility = Visibility.Collapsed;
                this.btnPrint2.Visibility = Visibility.Hidden;
                this.cmbPrnForm2.Visibility = Visibility.Hidden;
                this.btnUpdate.Visibility = Visibility.Hidden;

                this.stkpSubHead.Visibility = Visibility.Collapsed;
                this.stkpQty.Visibility = Visibility.Hidden;

                this.stkpSubHead2.Visibility = Visibility.Collapsed;
                this.cmbVouBrn.Items.Clear();
                var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
                foreach (var itemb in brnList)
                    this.cmbVouBrn.Items.Add(new ComboBoxItem()
                    {
                        Content = itemb.brnnam.Trim() + " (" + itemb.brnsnam.Trim() + ")",
                        Tag = itemb.brncod + itemb.brnsnam.Trim(),
                        ToolTip = itemb.brnnam.Trim() + " (" + itemb.brnsnam.Trim() + ")"
                    });

                var sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");

                sectList.Sort(delegate(HmsEntityGeneral.CompSecCodeBook x, HmsEntityGeneral.CompSecCodeBook y)
                {
                    return x.sectname.CompareTo(y.sectname);
                });

                this.AtxtSectCod.Items.Clear();
                this.AtxtSectCod.AutoSuggestionList.Clear();
                foreach (var itemc in sectList)
                {
                    string cdesc1s = itemc.sectcod + " - " + itemc.sectname.Trim();
                    this.AtxtSectCod.AddSuggstionItem(cdesc1s, itemc.sectcod);
                    var mitm1 = new MenuItem() { Header = cdesc1s, Tag = itemc.sectcod.Trim() };
                    mitm1.Click += conMenuSectCod_MouseClick;
                    this.conMenuSectCod.Items.Add(mitm1);
                }
                if (sectList.Count == 1)
                {
                    this.AtxtSectCod.Value = sectList[0].sectcod;
                    this.AtxtSectCod.IsEnabled = false;
                }

                if (WpfProcessAccess.AccCodeList == null)
                    WpfProcessAccess.GetAccCodeList();

                this.CactcodeList = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902") && (x.actcode.Substring(8, 4) != "0000"));

                this.CactcodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
                {
                    return x.actdesc.CompareTo(y.actdesc);
                });

                this.ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => !(x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902") && (x.actcode.Substring(8, 4) != "0000"));
                this.ActcodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
                {
                    return x.actdesc.CompareTo(y.actdesc);
                });


                this.conMenuActCod.Items.Clear();
                this.AtxtActCode.Items.Clear();
                this.AtxtActCode.AutoSuggestionList.Clear();
                foreach (var item1 in this.ActcodeList)
                {
                    this.AtxtActCode.AddSuggstionItem(item1.actdesc1.Trim(), item1.actcode);
                    var mitm1 = new MenuItem() { Header = item1.actdesc1.Trim(), Tag = item1.actcode.Trim() };
                    mitm1.Click += conMenuActCod_MouseClick;
                    this.conMenuActCod.Items.Add(mitm1);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void FindVouBrnchForThisTerminal()
        {
            if (WpfProcessAccess.GenInfoTitleList == null)
                WpfProcessAccess.GetGenInfoTitleList();

            string comcod1 = WpfProcessAccess.CompInfList[0].comcod;
            string TerminalID1 = WpfProcessAccess.SignedInUserList[0].terminalID.ToUpper();

            var TerminalList1 = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 8) == "CBWS" + WpfProcessAccess.CompInfList[0].comcod
                                && x.acttdesc.Trim().ToUpper() == TerminalID1 && x.actelev.Trim().Length == 4).ToList();
            int li = 0;
            if (TerminalList1.Count > 0)
            {
                string brncod1 = TerminalList1[0].actelev.Trim();
                foreach (ComboBoxItem item in this.cmbVouBrn.Items)
                {
                    if (item.Tag.ToString().ToUpper().Trim().Substring(0, 4) == brncod1)
                    {
                        this.cmbVouBrn.SelectedIndex = li;
                        break;
                    }
                    li++;
                }
            }
        }

        private void FindCurrencyList()
        {
            if (WpfProcessAccess.GenInfoTitleList == null)
                WpfProcessAccess.GetGenInfoTitleList();

            this.CurrencyCodeList = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "CBCICOD" && x.actcode.Substring(9, 3) != "000" && !x.acttdesc.Contains("XXX")).ToList();
            this.cmbCurrency.Items.Clear();
            foreach (var item in this.CurrencyCodeList)
            {
                this.cmbCurrency.Items.Add(new ComboBoxItem() { Uid = item.actcode, Content = item.acttype, Tag = item.actelev, ToolTip = item.actdesc });
            }
            this.cmbCurrency.SelectedIndex = 0;
            this.lblBaseCurrTitle.Content = this.CurrencyCodeList[0].acttype.Trim();
            this.stkpCurrency.IsEnabled = (this.cmbCurrency.Items.Count <= 1 ? false : true);
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //------ Draft information update option is enabled (Generally for local/high avaliability of database)
            if (this.chkAllowDraft.IsChecked == true && this.DraftMemoRowID > 0)
                this.CalculateTotal();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtActCode.Visibility = Visibility.Hidden;
                this.btnPrint2.Visibility = Visibility.Hidden;
                this.cmbPrnForm2.Visibility = Visibility.Hidden;
                this.btnUpdate.Visibility = Visibility.Hidden;
                this.gridDetails.Visibility = Visibility.Collapsed;
                this.gridDetails1.Visibility = Visibility.Collapsed;
                this.btnFindBudget.Visibility = Visibility.Collapsed;
                this.gridCalc1.Visibility = Visibility.Collapsed;
                this.cmbDrCr.Visibility = Visibility.Visible;
                this.gridDetails.IsEnabled = true;
                this.gridDetails1.IsEnabled = true;
                this.chkAutoTransList.IsEnabled = true;
                this.dgTrans.ItemsSource = null;
                this.txtVouNar.IsEnabled = true;
                this.txtVouRef.IsEnabled = true;
                this.chkSubHead.IsChecked = false;
                this.chkSubHead2.IsChecked = false;
                this.chkQty.IsChecked = false;
                this.AtxtActCode.Text = "";
                this.lblLevel2.Visibility = Visibility.Hidden;
                
                //------ Draft information update option is enabled (Generally for local/high avaliability of database)
                if (this.chkAllowDraft.IsChecked == true && this.DraftMemoRowID > 0)
                    this.CalculateTotal();

                this.CleanupControls();
                this.xctk_dtpVouDat.IsEnabled = false;
                this.lblVouNo.Content = "XXVMM-CCCC-XXXXX";
                this.lblVouNo.Tag = "XXVYYYYMMCCCC";
                this.PayBgdBalDetails.Clear();
                this.PayBgdBalSum.Clear();

                if (this.btnOk.Content.ToString() == "_New")
                {                 
                    if (this.txtblEditMode.Visibility == Visibility.Visible)
                    {
                        this.xctk_dtpVouDat.Minimum = DateTime.Today.AddDays(-365 * 3);
                        this.xctk_dtpVouDat.Maximum = DateTime.Today.AddDays(365 * 2);
                        this.xctk_dtpVouDat.Value = DateTime.Today;
                    }

                    this.xctk_dtpVouDat.Tag = "01-Jan-1900";
                    this.txtblEditMode.Visibility = Visibility.Hidden;
                    this.chkDateBlocked.IsChecked = false;
                    this.chkDateBlocked.IsEnabled = true;
                    this.cmbVouType.IsEnabled = true;
                    this.cmbVouBrn.IsEnabled = true;
                    this.txtVouRef.Text = "";
                    this.txtVouNar.Text = "";
                    this.stkpDraftOption.IsEnabled = true;
                    this.DraftMemoNum = "";
                    this.DraftMemoRowID = 0;

                    this.EditDs = null;

                    if (this.IsActiveTransListWindow)
                    {
                        this.BuildTransactionList();
                        this.gridTransList.Visibility = Visibility.Visible;
                        this.gridTransList.IsEnabled = true;
                        this.dgvTransList.Focus();
                    }
                    else
                        this.cmbVouType.Focus();
                    this.btnOk.Content = "_Ok";

                    return;

                }
                string voutitle = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim().Substring(0, 3);
                if (voutitle == "OPV")
                    this.FindOpeningDate();

                string vounum = voutitle + DateTime.Parse(this.xctk_dtpVouDat.Text).ToString("yyyyMM") +
                           ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Tag.ToString().Trim().Substring(0, 4);
                this.lblActCodeTitle.Content = "Account _Head:";

                string vtcode1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim().Substring(3, 2);

                if (voutitle.Substring(0, 2) == "PV" && vtcode1 == "83")
                {
                    this.btnFindBudget.Visibility = Visibility.Visible;
                    this.cmbDrCr.SelectedIndex = 0;
                    this.cmbDrCr.Visibility = Visibility.Collapsed;
                    this.CalculateBppStatus(vounum);
                }

                this.BindControlCode(vounum);
                this.ReBindAccountsCode(vounum, vtcode1);
                this.lblVouNo.Content = vounum.Substring(0, 3) + vounum.Substring(7, 2) + "-" + vounum.Substring(9, 4) + "-XXXXX";
                this.lblVouNo.Tag = vounum;

                this.stkpChk1.Visibility = (vounum.Contains("FTV") ? Visibility.Hidden : Visibility.Visible);
                this.stkpControl.Visibility = (vounum.Contains("JV") || vounum.Contains("OP") ? Visibility.Hidden : Visibility.Visible);
                this.stkpLocation.Visibility = (vounum.Contains("FTV") ? Visibility.Hidden : Visibility.Visible);
                this.stkpControl.Height = (vounum.Contains("JV") || vounum.Contains("OP") ? 0 : 26);
                this.cmbDrCr.IsEnabled = (vounum.Contains("JV") || vounum.Contains("OP"));
                this.cmbDrCr.Focusable = (vounum.Contains("JV") || vounum.Contains("OP"));
                this.cmbDrCr.SelectedIndex = (vounum.Substring(0, 1) == "R" ? 1 : 0);
                bool IsCheqNoReq = (vounum.Contains("PVB") || vounum.Contains("FT") ? true : false);
                this.stkpCheqNo.Visibility = (IsCheqNoReq ? Visibility.Visible : Visibility.Hidden);
                this.stkpCheqNo.Width = (IsCheqNoReq ? 220 : 30);
                this.txtAdvice.Width = (IsCheqNoReq ? 115 : 135);

                this.stkpDraftOption.IsEnabled = false;
                this.chkAutoTransList.IsEnabled = false;

                this.gridDetails.Visibility = Visibility.Visible;
                this.gridTransList.IsEnabled = false;
                this.gridTransList.Visibility = Visibility.Collapsed;
                this.chkDateBlocked.IsChecked = false;
                this.cmbVouType.IsEnabled = false;
                this.PrevVounum = vounum;

                this.btnOk.Content = "_New";
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
       
        private void CalculateBppStatus(string vounum)
        {
            try
            {
                this.PayBgdBalDetails.Clear();
                this.PayBgdBalSum.Clear();
                var pap1 = vm1.SetParamPayBgdBalance(WpfProcessAccess.CompInfList[0].comcod, vounum.Substring(9, 4), vounum.Substring(3, 6));
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                if (ds1.Tables[0].Rows.Count == 0)
                    return;
                this.PayBgdBalDetails = ds1.Tables[0].DataTableToList<vmEntryVoucher1.PayBgdBalance>().ToList();
                this.PayBgdBalSum = ds1.Tables[1].DataTableToList<vmEntryVoucher1.PayBgdBalance>().ToList();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-04: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void FindOpeningDate()
        {
            var pap1 = vm1.SetParamFindOpeningDate(WpfProcessAccess.CompInfList[0].comcod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            if (Convert.ToInt32(ds1.Tables[0].Rows[0]["opcount"]) == 0)
                return;

            this.xctk_dtpVouDat.Value = Convert.ToDateTime(ds1.Tables[0].Rows[0]["minopdate"]);
            this.chkDateBlocked.IsEnabled = false;

            if (Convert.ToDateTime(ds1.Tables[0].Rows[0]["minopdate"]) != Convert.ToDateTime(ds1.Tables[0].Rows[0]["maxopdate"]))
            {
                System.Windows.MessageBox.Show("Different opening date found. Please contact to system administrator", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                               MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
        }
        private void BindControlCode(string vounum)
        {
            try
            {
                this.AtxtCactCode.Items.Clear();
                this.AtxtCactCode.AutoSuggestionList.Clear();
                this.conMenuCactCode.Items.Clear();

                if (!vounum.Contains("JV") && !vounum.Contains("OP"))
                {
                    var CactcodeList1a = new List<HmsEntityGeneral.AcInfCodeBook>();
                    switch (vounum.Substring(0, 3))
                    {
                        case "RVC":
                        case "PVC":
                            CactcodeList1a = this.CactcodeList.FindAll(x => x.actcode.Substring(0, 4) == "1901");
                            this.lblCactCodeTitle.Content = (vounum.Contains("PVC") ? "_Source" : "Depo_sit") + " Cash";
                            break;
                        case "RVB":
                        case "PVB":
                            CactcodeList1a = this.CactcodeList.FindAll(x => x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902");
                            this.lblCactCodeTitle.Content = (vounum.Contains("PVB") ? "_Source" : "Depo_sit") + " Bank";
                            break;
                        case "FTV":
                            CactcodeList1a = this.CactcodeList.FindAll(x => x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902");
                            this.lblCactCodeTitle.Content = "From Cash/Bank";
                            this.lblActCodeTitle.Content = "To Cas_h/Bank";
                            break;
                    }

                    foreach (var item1 in CactcodeList1a)
                    {
                        this.AtxtCactCode.AddSuggstionItem(item1.actdesc1.Trim(), item1.actcode);
                        var mitm1 = new MenuItem() { Header = item1.actdesc1.Trim(), Tag = item1.actcode.Trim() };
                        mitm1.Click += conMenuCactCode_MouseClick;
                        this.conMenuCactCode.Items.Add(mitm1);
                    }
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-05: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ReBindAccountsCode(string vounum, string vtcode = "XX")
        {
            try
            {
                if (vounum.Substring(0, 3) == "FTV")
                {
                    this.AtxtActCode.Items.Clear();
                    this.AtxtActCode.AutoSuggestionList.Clear();
                    this.conMenuActCod.Items.Clear();
                    var ActcodeList1a = this.CactcodeList.FindAll(x => x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902");
                    foreach (var item1 in ActcodeList1a)
                    {
                        this.AtxtActCode.AddSuggstionItem(item1.actdesc1.Trim(), item1.actcode);
                        var mitm1 = new MenuItem() { Header = item1.actdesc1.Trim(), Tag = item1.actcode.Trim() };
                        mitm1.Click += conMenuActCod_MouseClick;
                        this.conMenuActCod.Items.Add(mitm1);
                    }
                }
                else if (vounum.Substring(0, 3) == "OPV")
                {
                    this.AtxtActCode.Items.Clear();
                    this.AtxtActCode.AutoSuggestionList.Clear();
                    this.conMenuActCod.Items.Clear();
                    var ActcodeList1a = this.CactcodeList.FindAll(x => x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902");
                    foreach (var item1 in ActcodeList1a)
                    {
                        this.AtxtActCode.AddSuggstionItem(item1.actdesc1.Trim(), item1.actcode);
                        var mitm1a = new MenuItem() { Header = item1.actdesc1.Trim(), Tag = item1.actcode.Trim() };
                        mitm1a.Click += conMenuActCod_MouseClick;
                        this.conMenuActCod.Items.Add(mitm1a);
                    }

                    foreach (var item1 in this.ActcodeList)
                    {
                        this.AtxtActCode.AddSuggstionItem(item1.actdesc1.Trim(), item1.actcode);
                        var mitm1b = new MenuItem() { Header = item1.actdesc1.Trim(), Tag = item1.actcode.Trim() };
                        mitm1b.Click += conMenuActCod_MouseClick;
                        this.conMenuActCod.Items.Add(mitm1b);
                    }
                }
                else if (PrevVounum.Contains("FTV") || PrevVounum.Contains("OPV"))
                {
                    this.AtxtActCode.Items.Clear();
                    this.AtxtActCode.AutoSuggestionList.Clear();
                    this.conMenuActCod.Items.Clear();
                    foreach (var item1 in this.ActcodeList)
                    {
                        this.AtxtActCode.AddSuggstionItem(item1.actdesc1.Trim(), item1.actcode);
                        var mitm1b = new MenuItem() { Header = item1.actdesc1.Trim(), Tag = item1.actcode.Trim() };
                        mitm1b.Click += conMenuActCod_MouseClick;
                        this.conMenuActCod.Items.Add(mitm1b);
                    }
                }
                else if (vounum.Substring(0, 2) == "PV" && vtcode == "83")
                {
                    this.AtxtActCode.Items.Clear();
                    this.AtxtActCode.AutoSuggestionList.Clear();
                    this.conMenuActCod.Items.Clear();
                    foreach (var item1 in this.PayBgdBalSum)
                    {
                        this.AtxtActCode.AddSuggstionItem(item1.actcodeDesc.Trim(), item1.actcode);
                        var mitm1b = new MenuItem() { Header = item1.actcodeDesc.Trim(), Tag = item1.actcode.Trim() };
                        mitm1b.Click += conMenuActCod_MouseClick;
                        this.conMenuActCod.Items.Add(mitm1b);
                    }
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-06: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void CleanupControls()
        {
            this.txtVouRef.Text = "";
            this.AtxtCactCode.Text = "";
            if (this.AtxtSectCod.IsEnabled == true)
                this.AtxtSectCod.Text = "";

            this.cmbCheqNo.Items.Clear();
            this.txtAdvice.Text = "";
            this.chkSubHead.IsChecked = false;
            this.chkQty.IsChecked = false;
            this.chkSubHead2.IsChecked = false;
            this.cmbDrCr.SelectedIndex = 0;
            this.ListVouTable1.Clear();
            this.CleanupControls2();
        }

        private void CleanupControls2()
        {
            if (this.lblLevel2.Visibility == Visibility.Hidden)
                this.AtxtActCode.Text = "";

            this.AutoCompleteSirCode.SelectedValue = null;
            this.AutoCompleteSirCode2.SelectedValue = null;

            this.txtQty.Text = "";
            this.lblUnit.Content = "";
            this.txtRate.Text = "";
            this.txtRmrk.Text = "";
            this.txtAmount.Text = "";
            this.stkpSubHead.Visibility = (this.chkSubHead.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpVouDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpVouDat.IsEnabled)
                this.xctk_dtpVouDat.Focus();
        }

        private void AtxtCactCode_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.AtxtCactCode.Value.Length > 0 && this.AtxtCactCode.Text.Trim().Length > 0)
                {
                    string cactVal = this.AtxtCactCode.Value;
                    if (this.cmbCheqNo.Items.Count > 0)
                    {
                        string strtag1 = ((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim();
                        if (strtag1.Length > 0)
                        {
                            string cactVal1 = ((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Substring(0, 12);
                            if (cactVal == cactVal1)
                                return;
                        }
                    }
                }

                this.ListBlnkCheq1.Clear();
                this.cmbCheqNo.Items.Clear();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-07: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void AtxtCactCode_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.AtxtCactCode.Value.Length == 0)
                    return;

                if (this.AtxtCactCode.Text.Trim().Length == 0)
                    return;

                string cactVal = this.AtxtCactCode.Value;
                if (this.cmbCheqNo.Items.Count > 0)
                {
                    string strtag1 = ((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim();
                    if (strtag1.Length > 0)
                    {
                        string cactVal1 = ((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Substring(0, 12);
                        if (cactVal == cactVal1)
                            return;
                    }
                }

                this.ListBlnkCheq1.Clear();
                this.cmbCheqNo.Items.Clear();

                if (this.stkpCheqNo.Visibility == Visibility.Hidden)
                    return;

                if (!(cactVal.Substring(0, 4) == "1902" || cactVal.Substring(0, 4) == "2902"))
                    return;

                // to be activated after Cheque Book Entry
                var pap1 = vm1.SetParamBlankCheque(WpfProcessAccess.CompInfList[0].comcod, cactVal);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                this.ListBlnkCheq1 = ds1.Tables[0].DataTableToList<vmEntryVoucher1.ChqBlankLeaf>();
                foreach (var item in this.ListBlnkCheq1)
                {
                    this.cmbCheqNo.Items.Add(new ComboBoxItem() { Content = item.cheqnum, Tag = item.cheqbookid + item.cheqnum.Trim() });
                }
                this.cmbCheqNo.Items.Add(new ComboBoxItem() { Content = "NONE", Tag = "" });
                if (this.cmbCheqNo.Items.Count > 0)
                    this.cmbCheqNo.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-08: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void AtxtActCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtActCode.Value.Length == 0)
                return;

            if (this.AtxtActCode.Text.Trim().Length == 0)
                return;

            string actVal = this.AtxtActCode.Value;

            bool level2 = false;
            var acCodeInf = this.ActcodeList.Find(x => x.actcode == actVal);
            if (acCodeInf != null)
                level2 = (acCodeInf.actelev.Trim() == "2");


            this.lblLevel2.Visibility = (level2 ? Visibility.Visible : Visibility.Hidden);
            this.chkSubHead_Click(null, null);
        }

        private void chkSubHead_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.stkpSubHead.Visibility = Visibility.Collapsed;

                this.stkpQty.Visibility = Visibility.Hidden;

                this.stkpSubHead2.Visibility = Visibility.Collapsed;

                bool chkSubHead1a = (this.chkSubHead.IsChecked == true);
                bool chkQty1a = (this.chkQty.IsChecked == true);
                bool chkSubHead2a = (this.chkSubHead2.IsChecked == true);

                if (chkSubHead1a || this.lblLevel2.Visibility == Visibility.Visible)
                {
                    this.stkpSubHead.Visibility = Visibility.Visible;
                }

                if (chkQty1a)
                {
                    this.chkSubHead.IsChecked = true;
                    this.stkpSubHead.Visibility = Visibility.Visible;
                    this.stkpQty.Visibility = Visibility.Visible;
                }

                if (chkSubHead2a)
                {
                    this.chkSubHead.IsChecked = true;
                    this.stkpSubHead.Visibility = Visibility.Visible;
                    this.stkpSubHead2.Visibility = Visibility.Visible;
                }

                if (WpfProcessAccess.AccSirCodeList == null)
                    WpfProcessAccess.GetAccSirCodeList();

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void AtxtSectCod_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtSectCod.Value.Length == 0)
                return;

            if (this.AtxtSectCod.Text.Trim().Length == 0)
                return;
        }

        private void conMenuCactCode_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtCactCode.Value = ((MenuItem)sender).Tag.ToString().Trim();
        }

        private void conMenuActCod_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtActCode.Value = ((MenuItem)sender).Tag.ToString().Trim();
        }

        private void AtxtCactCode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtCactCode.ContextMenu.IsOpen = true;
        }
        private void conMenuSectCod_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtSectCod.Value = ((MenuItem)sender).Tag.ToString().Trim();
        }
        private void AtxtSectCod_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtSectCod.ContextMenu.IsOpen = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string drcr = ((ComboBoxItem)this.cmbDrCr.SelectedItem).Content.ToString().Substring(0, 1);
                decimal trnamt1 = decimal.Parse("0" + this.txtAmount.Text.Trim());
                decimal dramt1 = (drcr == "D" ? trnamt1 : 0.00m);
                decimal cramt1 = (drcr == "C" ? trnamt1 : 0.00m);
                decimal trnqty1 = decimal.Parse("0" + this.txtQty.Text.Trim());
                decimal trnrate1 = (trnqty1 > 0 && trnamt1 > 0 ? (trnamt1 / trnqty1) : 0.00m);
                string vType1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString();
                var cactcode1 = (this.AtxtCactCode.Text.Trim().Length == 0 ? "000000000000" : (this.AtxtCactCode.Value.Trim().Length != 12 ? "000000000000" : this.AtxtCactCode.Value));
                cactcode1 = (vType1.Contains("JV") || vType1.Contains("OP") ? "000000000000" : cactcode1);

                var cactcodeDesc1 = (cactcode1 == "000000000000" ? "" : this.AtxtCactCode.Text.Trim());
                var sectcod1 = (this.AtxtSectCod.Text.Trim().Length == 0 ? "000000000000" : (this.AtxtSectCod.Value.Trim().Length != 12 ? "000000000000" : this.AtxtSectCod.Value));
                var sectcodDesc1 = (sectcod1 == "000000000000" ? "" : this.AtxtSectCod.Text.Trim());
                var actcode1 = (this.AtxtActCode.Text.Trim().Length == 0 ? "000000000000" : (this.AtxtActCode.Value.Trim().Length != 12 ? "000000000000" : this.AtxtActCode.Value));
                var actcodeDesc1 = (actcode1 == "000000000000" ? "" : this.AtxtActCode.Text.Trim());
                var sircode1a = this.AutoCompleteSirCode.SelectedValue;
                var sircode1 = (sircode1a == null ? "000000000000" : (sircode1a.ToString().Trim().Length != 12 ? "000000000000" : sircode1a.ToString().Trim()));
                var sircodeDesc1 = (sircode1 == "000000000000" ? "" : this.AutoCompleteSirCode.SelectedText.Trim());
                var sirUnit1 = (sircode1 == "000000000000" ? "" : WpfProcessAccess.AccSirCodeList.Find(x => x.sircode == sircode1).sirunit.Trim());// this.lblUnit.Content.ToString();

                string reptsl1 = this.lblSlNo.Content.ToString().Trim();

                var sircode2a = this.AutoCompleteSirCode2.SelectedValue;
                var sircode2 = (sircode2a == null ? "000000000000" : (sircode2a.ToString().Trim().Length != 12 ? "000000000000" : sircode2a.ToString().Trim()));
                var sircode2Desc1 = (sircode2 == "000000000000" ? "" : this.AutoCompleteSirCode2.SelectedText.Trim());

                var rmrk1 = this.txtRmrk.Text.Trim();
                if (actcode1 == "000000000000")
                    return;

                if (this.stkpControl.Visibility == Visibility.Visible)
                {
                    if (cactcode1 == "000000000000")
                        return;
                }

                string ac1 = actcode1.Substring(0, 4);
                bool CashBank = ((ac1 == "1901" || ac1 == "1902" || ac1 == "2902") ? true : false);

                if (this.stkpLocation.Visibility == Visibility.Visible && CashBank == false)
                {
                    if (sectcod1 == "000000000000")
                        return;
                }
                sectcod1 = (CashBank == true ? "000000000000" : sectcod1);
                sectcodDesc1 = (CashBank == true ? "" : sectcodDesc1);

                bool QtyFound = false;
                foreach (var itemd in this.ListVouTable1)
                {
                    QtyFound = (itemd.trnqty != 0 || QtyFound ? true : false);
                    if (itemd.cactcode == cactcode1 && itemd.sectcod == sectcod1 && itemd.actcode == actcode1 && itemd.sircode == sircode1 && itemd.reptsl == reptsl1)
                        return;
                }

                if (sircode1 != "000000000000" && this.stkpQty.Visibility == Visibility.Visible)
                {
                    var tsirCod1 = WpfProcessAccess.AccSirCodeList.Find(x => x.sircode == sircode1);
                    this.lblUnit.Content = tsirCod1.sirunit.Trim();
                }

                this.gridDetails1.Visibility = Visibility.Visible;
                this.btnUpdate.Visibility = Visibility.Visible;
                this.btnUpdate.IsEnabled = true;


                if (!vType1.Contains("JV") && !vType1.Contains("OP"))
                {

                    foreach (var item in this.ListVouTable1)
                    {
                        item.cactcode = cactcode1;
                        item.cactcodeDesc = cactcodeDesc1;
                        if (item.actcode == "000000000000" && item.sectcod == "000000000000")
                            item.trnDesc = cactcodeDesc1;
                    }

                    var Ccod1 = this.ListVouTable1.FindAll(x => x.cactcode == cactcode1 && x.actcode == "000000000000" && x.sectcod == "000000000000");
                    if (Ccod1.Count == 0)
                    {
                        this.ListVouTable1.Add(new vmEntryVoucher1.VouTable()
                        {
                            trnsl = this.ListVouTable1.Count() + 1,
                            DrCrOrder = (vType1.Substring(0, 1) == "R" ? "01" : "02"), // (vType1.Substring(1, 1) == "C" ? "01" : "02"),
                            cactcode = cactcode1,
                            sectcod = "000000000000",
                            actcode = "000000000000",
                            sircode = "000000000000",
                            reptsl = "000",
                            sircode2 = "000000000000",
                            cactcodeDesc = cactcodeDesc1,
                            sectcodDesc = "",
                            actcodeDesc = "",
                            sircodeDesc = "",
                            sircode2Desc = "",
                            trnDesc = cactcodeDesc1,
                            trnqty = 0,
                            trnUnit = "",
                            trnrate = 0,
                            dramt = 0,
                            cramt = 0,
                            trnam = 0,
                            trnrmrk = ""
                        });
                    }
                }

                var reptsl1a = (this.ListVouTable1.Count == 0 ? "000" : this.ListVouTable1.Max(x => x.reptsl));
                reptsl1 = (int.Parse(reptsl1a) + 1).ToString("000");

                this.ListVouTable1.Add(new vmEntryVoucher1.VouTable()
                    {
                        trnsl = this.ListVouTable1.Count() + 1,
                        DrCrOrder = ((dramt1 - cramt1) > 0 ? "01" : "02"),
                        cactcode = cactcode1,
                        sectcod = sectcod1,
                        actcode = actcode1,
                        sircode = sircode1,
                        reptsl = reptsl1,
                        sircode2 = sircode2,
                        cactcodeDesc = cactcodeDesc1,
                        sectcodDesc = sectcodDesc1,
                        actcodeDesc = actcodeDesc1,
                        sircodeDesc = sircodeDesc1,
                        sircode2Desc = sircode2Desc1,
                        trnDesc = actcodeDesc1 + (sircodeDesc1.Length > 0 ? "\n\t" + sircodeDesc1 + (sircode2Desc1.Length > 0 ? "\n\t\t" + sircode2Desc1 : "") : ""),
                        trnqty = trnqty1,
                        trnUnit = sirUnit1,
                        trnrate = trnrate1,
                        dramt = dramt1,
                        cramt = cramt1,
                        trnam = trnamt1,
                        trnrmrk = rmrk1
                    });

                this.lblSlNo.Content = "xxx";
                this.CleanupControls2();
                this.CalculateTotal();
                this.txtActCode.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-10: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void CalculateTotal()
        {
            try
            {
                this.dgTrans.ItemsSource = null;
                foreach (var item1 in this.ListVouTable1)
                {
                    if (item1.actcode != "000000000000")
                    {
                        item1.trnam = item1.dramt - item1.cramt;
                        item1.trnrate = (item1.trnqty != 0 ? Math.Round(item1.trnam / item1.trnqty, 2) : 0.00m);
                        item1.DrCrOrder = (item1.trnam > 0 ? "01" : "02");
                    }
                }

                string vType1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString();

                decimal sumDr = this.ListVouTable1.FindAll(x => x.actcode != "000000000000").Sum(x => x.dramt);
                decimal sumCr = this.ListVouTable1.FindAll(x => x.actcode != "000000000000").Sum(x => x.cramt);

                if (vType1.Substring(0, 1) == "R")
                {
                    foreach (var item1d in this.ListVouTable1)
                    {
                        if (item1d.actcode == "000000000000")
                        {
                            item1d.dramt = (sumCr - sumDr);
                            break;
                        }

                    }
                }
                else if (vType1.Substring(0, 1) == "P" || vType1.Substring(1, 1) == "T") //if (vType1.Substring(1, 1) == "D" || vType1.Substring(1, 1) == "T")
                {
                    foreach (var item1d in this.ListVouTable1)
                    {
                        if (item1d.actcode == "000000000000")
                        {
                            item1d.cramt = (sumDr - sumCr);
                            break;
                        }
                    }
                }

                this.ListVouTable1.Sort(delegate(vmEntryVoucher1.VouTable x, vmEntryVoucher1.VouTable y)
                {
                    return (x.DrCrOrder + x.cactcode + x.actcode).CompareTo(y.DrCrOrder + y.cactcode + y.actcode);
                });

                int i = 1;
                string prevActcode1 = "XXXXXXXXXXXX";
                foreach (var item1 in this.ListVouTable1)
                {
                    item1.trnsl = i;
                    if (item1.actcode != "000000000000")
                    {
                        string actcodeDesc1 = (item1.actcode == prevActcode1 ? "" : item1.actcodeDesc);
                        item1.trnDesc = actcodeDesc1 + (item1.sircodeDesc.Length > 0 ? (actcodeDesc1.Length > 0 ? "\n\t" : "\t") + item1.sircodeDesc + (item1.sircode2Desc.Length > 0 ? "\n\t\t" + item1.sircode2Desc : "") : "");
                    }
                    prevActcode1 = item1.actcode;
                    ++i;
                }

                this.lblSumDram.Content = this.ListVouTable1.Sum(x => x.dramt).ToString("#,##0.00");
                this.lblSumCram.Content = this.ListVouTable1.Sum(x => x.cramt).ToString("#,##0.00");
                this.dgTrans.ItemsSource = this.ListVouTable1;
                this.gridCalc1.Visibility = Visibility.Collapsed;

                bool QtyFound = (this.ListVouTable1.FindAll(x => x.trnqty != 0).Count > 0);

                this.dgTransColQty.Visibility = (QtyFound ? Visibility.Visible : Visibility.Hidden);
                this.dgTransColUnit.Visibility = (QtyFound ? Visibility.Visible : Visibility.Hidden);
                this.dgTransColRate.Visibility = (QtyFound ? Visibility.Visible : Visibility.Hidden);
                this.SeprQty.Width = (QtyFound ? 65 : 0);
                this.SeprUnit.Width = (QtyFound ? 45 : 0);
                this.SeprRate.Width = (QtyFound ? 80 : 0);
                this.dgTransColLoc.Width = (QtyFound ? 120 : 250);
                //------ Draft information update option is enabled (Generally for local/high avaliability of database)
                if (this.chkAllowDraft.IsChecked == true)
                    this.UpdateDraftVoucherInformation();

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.CalculateTotal();

                // Checking Budget vs Payment if this voucher is matched
                //----------------------------------------------------------------
                string voutitl1a = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim();
                //if (voutitl1a.Substring(0, 2) == "PV" && voutitl1a.Substring(3, 2) == "83" && this.EditDs == null) // For New Entry Only

                if (voutitl1a.Substring(0, 2) == "PV" && voutitl1a.Substring(3, 2) == "83" && this.lblVouNo.Tag.ToString().Trim().Length == 13) // For New Entry Only
                {
                    if (!IsPaymentWithinBudget())
                    {
                        System.Windows.MessageBox.Show("-:: Update not possible ::-\nInput records are not matched with payment budget",
                            WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                        return;
                    }
                }
                //----------------------------------------------------------------

                string cactcod1 = this.AtxtCactCode.Value.Trim();
                string cheqbookid1 = "XXXXXXXXXXXXXXXXXX";//  "190200030001151201";
                string cheqno1 = "";
                if (this.cmbCheqNo.Items.Count > 0)
                {
                    string strtag1 = ((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim();
                    if (strtag1.Length > 0)
                    {
                        cheqbookid1 = strtag1.Substring(0, 18);
                        cheqno1 = strtag1.Substring(18);
                    }
                }
                string vounum1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim().Substring(0, 3) +
                             DateTime.Parse(this.xctk_dtpVouDat.Text).ToString("yyyyMM") +
                             ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Tag.ToString().Trim().Substring(0, 4);

                vounum1 = (this.lblVouNo.Tag.ToString().Trim().Length == 18 ? this.lblVouNo.Tag.ToString() : vounum1);
                string EditVounum1 = (this.lblVouNo.Tag.ToString().Trim().Length == 18 ? this.lblVouNo.Tag.ToString() : "");
                string RecnDate1 = this.xctk_dtpVouDat.Tag.ToString();



                var vouPrInfo1 = new vmEntryVoucher1.VouPrInfo()
                {
                    vounum = vounum1,
                    voudat = DateTime.Parse(this.xctk_dtpVouDat.Text),
                    vouref = this.txtVouRef.Text.Trim(),
                    cheqbookid = cheqbookid1,
                    chqref = cheqno1, 
                    advref = this.txtAdvice.Text.Trim(),
                    vounar = this.txtVouNar.Text.Trim(),
                    curcod = this.DecUDCurrExRate.Uid,
                    curcnv = decimal.Parse("0" + this.DecUDCurrExRate.Value.ToString()),
                    vstatus = "A",
                    recndt = DateTime.Parse(RecnDate1),
                    vtcode = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim().Substring(3, 2),
                };

                decimal sumam1 = this.ListVouTable1.Sum(x => (x.dramt - x.cramt));
                if (sumam1 != 0)
                {
                    System.Windows.MessageBox.Show("Debit Amount must be equals with Credit Amount", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                var ListVouTable1u = this.ListVouTable1.FindAll(x => x.actcode != "000000000000");
                DataSet ds1 = vm1.GetDataSetForUpdate(WpfProcessAccess.CompInfList[0].comcod, vouPrInfo1, ListVouTable1u, _preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);
                var pap1 = vm1.SetParamUpdateVoucher(WpfProcessAccess.CompInfList[0].comcod, ds1, EditVounum1);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                this.lblVouNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                this.lblVouNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();

                this.btnUpdate.IsEnabled = false;
                this.gridDetails.IsEnabled = false;
                this.gridDetails1.IsEnabled = false;
                this.txtVouNar.IsEnabled = false;
                this.txtVouRef.IsEnabled = false;
                this.btnPrint2.Visibility = Visibility.Visible;

                //-------------------------------         
                string vtype1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Substring(0, 3);
                this.cmbPrnForm2itmC.Visibility = ((vtype1 == "PVB" || vtype1 == "FTV") ? Visibility.Visible : Visibility.Collapsed);
                this.cmbPrnForm2itmM.Visibility = ((vtype1 == "RVB" || vtype1 == "RVC") ? Visibility.Visible : Visibility.Collapsed);
                this.cmbPrnForm2.SelectedIndex = 0;

                //if (vtype1 == "RVB" || vtype1 == "RVC" || vtype1 == "PVB" || vtype1 == "PVC" || vtype1 == "FTV")
                this.cmbPrnForm2.Visibility = Visibility.Visible;

                //------ Draft information update option is enabled (Generally for local/high avaliability of database)
                if (this.chkAllowDraft.IsChecked == true)
                {
                    DataSet dsdraft1 = WpfProcessAccess.UpdateDeleteDraftTransaction(UpadateDelete1: "DELETE", ds1: null, draftnum1: this.DraftMemoNum, rowid1: this.DraftMemoRowID,
                                       draftrmrk1: "", draftbyid1: this.preparebyid1, draftses1: this.prepareses1, drafttrm1: this.preparetrm1);
                    if (dsdraft1 == null)
                        return;
                    this.DraftMemoNum = "";
                    this.DraftMemoRowID = 0;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void chkAutoTransList_Click(object sender, RoutedEventArgs e)
        {
            this.IsActiveTransListWindow = (this.chkAutoTransList.IsChecked == true);
            if (this.IsActiveTransListWindow && this.gridDetails.Visibility == Visibility.Collapsed)
            {
                string vtype1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Substring(0, 3);
                this.cmbPrnForm3itmC.Visibility = ((vtype1 == "PVB" || vtype1 == "FTV") ? Visibility.Visible : Visibility.Collapsed);
                this.cmbPrnForm3ItmM.Visibility = ((vtype1 == "RVB" || vtype1 == "RVC") ? Visibility.Visible : Visibility.Collapsed);
                //<ComboBoxItem x:Name="cmbPrnForm3itmV" Content="Voucher" Tag="VOUCHER"  />
                //<ComboBoxItem x:Name="cmbPrnForm3itmC" Content="Cheque" Tag="CHEQUE"  />
                //<ComboBoxItem x:Name="cmbPrnForm3ItmM" Content="M. Receipt" Tag="MRECEIPT" />
                this.cmbPrnForm3.SelectedIndex = 0;
                if (vtype1 == "RVB" || vtype1 == "RVC" || vtype1 == "PVB" || vtype1 == "FTV")
                {
                    this.rb3SelectedMemo.Content = "Selected Item";
                    this.cmbPrnForm3.Visibility = Visibility.Visible;
                }
                else
                {
                    this.rb3SelectedMemo.Content = "Selected Voucher";
                    this.cmbPrnForm3.Visibility = Visibility.Visible;
                    //this.cmbPrnForm3.Visibility = Visibility.Collapsed;
                }
                this.BuildTransactionList();
                this.gridTransList.Visibility = Visibility.Visible;
                this.gridTransList.IsEnabled = true;
                this.dgvTransList.Focus();
            }
            else if (this.IsActiveTransListWindow == false && this.gridDetails.Visibility == Visibility.Collapsed)
            {
                this.gridTransList.IsEnabled = false;
                this.gridTransList.Visibility = Visibility.Collapsed;
            }

            this.chkPrint2.IsChecked = false;
        }
        private void BuildTransactionList()
        {
            try
            {
                if (this.cmbVouBrn.Items.Count == 0)
                    return;
                this.dgvTransList.ItemsSource = null;
                string FromDate = this.xctk_dtpFromDate.Text;
                string ToDate = this.xctk_dtpToDate.Text;
                string brncod1 = ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Tag.ToString().Substring(0, 4);
                string vType1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString();
                var pap1 = vmrptAcc.SetParamAccTransList(WpfProcessAccess.CompInfList[0].comcod, "C0", FromDate, ToDate, "A");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.AccTrnLst.Clear();
                this.AccTrnLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTransectionList>().ToList().FindAll(x => x.brncod == brncod1
                    && x.vounum.Substring(0, 3) == vType1.Substring(0, 3) && x.vtcode == vType1.Substring(3, 2));
                int i = 1;
                foreach (var item in this.AccTrnLst)
                {
                    item.slnum = i;
                    i++;
                }

                this.dgvTransList.Columns[4].Visibility = this.stkpCurrency.Visibility;
                this.dgvTransList.Columns[5].Visibility = this.stkpCurrency.Visibility;
                this.dgvTransList.Columns[6].Width = (this.stkpCurrency.Visibility == Visibility.Visible ? 100 : 230);
                this.dgvTransList.ItemsSource = this.AccTrnLst;
                this.dgvTransList.Items.Refresh();
                this.dgvTransList.Focus();
                this.txtTransTitle.Text = "All Transaction List From : " + FromDate + " To : " + ToDate;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.gridTransList.IsEnabled = false;
            this.BuildTransactionList();
            this.gridTransList.IsEnabled = true;
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null && this.rb3SelectedMemo.IsChecked == true)
                {
                    System.Windows.MessageBox.Show("No record found to view/print report", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                LocalReport rpt1 = null;

                string WindowTitle1 = "";
                if (this.rb3SelectedMemo.IsChecked == true)
                {
                    var item1a = (HmsEntityAccounting.AccTransectionList)this.dgvTransList.SelectedItem;
                    string frmname = ((ComboBoxItem)this.cmbPrnForm3.SelectedItem).Tag.ToString().Trim();
                    string PaperType1 = (((ComboBoxItem)this.cmbPrnForm3.SelectedItem).Content.ToString().Trim() == "Voucher-2" ? "h" : "");
                    //if (item1a.vstatus == "C")
                    //{
                    //    System.Windows.MessageBox.Show("Cancelled voucher can not be view/print at this moment", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    //    return;
                    //}
                    //--------------------------
                    this.PrintVoucherMemo(item1a.vounum, false, frmname, PaperType1);

                }
                else if (this.rb3TableRecoreds.IsChecked == true)
                {
                    if (this.AccTrnLst.Count == 0)
                        return;

                    var list1 = this.AccTrnLst;
                    var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);

                    rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccTransList", list1, null, list3, null);
                    WindowTitle1 = "Transaction Voucher List";
                }
                if (rpt1 == null)
                    return;

                if (this.rb3QuickPrint.IsChecked == true)
                {
                    RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                    DirectPrint1.PrintReport(rpt1);
                    DirectPrint1.Dispose();
                }
                else
                {
                    string RptDisplayMode = "PrintLayout";
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-14: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void PrintVoucherMemo(string memoNum, bool DirectPrint = false, string prnFrom = "VOUCHER", string PaperType = "") // string prnFrom="VOUCHER", string prnFrom="CHEQUE", string prnFrom="MRECEIPT"
        {
            try
            {
                /*
                <ComboBox x:Name="cmbPrnForm2" Width="85" Background="White" IsReadOnly="True" SelectedIndex="0" Margin="7,0,0,0" Visibility="Hidden" >
                        <ComboBoxItem Content="Voucher" Tag="VOUCHER"/>
                        <ComboBoxItem Content="Cheque" Tag="CHEQUE" />
                        <ComboBoxItem Content="M. Receipt" Tag="MRECEIPT"/>             
                </ComboBox>            
                */

                LocalReport rpt1 = null;
                var pap1 = vmrptAcc.SetParamAccVoucher(WpfProcessAccess.CompInfList[0].comcod, memoNum, prnFrom);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var list1 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccVoucher1>();
                var trnsList = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTransectionList>();
                // select preparebyid, PreparByName, prepareses, preparetrm, rowid, rowtime, ServerTime = getdate() from #tblv1
                string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                                    + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
                string memoName = memoNum.Substring(0, 3).Trim();
                byte[] comlogoBytes = WpfProcessAccess.CompInfList[0].comlogo;
                decimal vouamt1 = list1.Sum(q => q.cramt);

                var list4 = new HmsEntityAccounting.AccVoucher1p();
                list4.comlogo = comlogoBytes;
                if (trnsList[0].curabvr.Trim() == "BDT")
                {
                    list4.inWord = ASITFunLib.ASITUtility.Trans(double.Parse(vouamt1.ToString()), 2);
                }
                else
                {
                    string[] amtPrts = vouamt1.ToString("###0.00").Split('.');
                    string[] curdesc2Parts = trnsList[0].curdesc2.Split(',');
                    list4.inWord = curdesc2Parts[0].Trim() + " " + ASITFunLib.ASITUtility.Trans(double.Parse(amtPrts[0]), 3) +
                            (double.Parse(amtPrts[1]) > 0 ? " AND " + curdesc2Parts[1].Trim() + " " + ASITFunLib.ASITUtility.Trans(double.Parse(amtPrts[1]), 3) : "") + " ONLY";

                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    list4.inWord = "(" + textInfo.ToTitleCase(list4.inWord.ToLower()) + ")";
                }
                //l.inWord = ASITFunLib.ASITUtility2.UppercaseWords("");
                string rptName = (prnFrom == "VOUCHER" ? "Accounting.RptAccVou1" + PaperType : (prnFrom == "CHEQUE" ? "Accounting.RptAccPayCheq1" : (prnFrom == "MRECEIPT" ? "Accounting.RptAccMReceipt1" : "")));
                // (list1.Count > 7 ? "Accounting.RptAccVou1" : "Accounting.RptAccVou1h");
                rpt1 = AccReportSetup.GetLocalReport(rptName, list1, trnsList, list3, list4);
                //rpt1.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(bytes)));
                string WindowTitle1 = (prnFrom == "VOUCHER" ? "Accounts Voucher" : (prnFrom == "CHEQUE" ? "Payment/Transfer Cheque" : (prnFrom == "MRECEIPT" ? "Money Receipt" : "")));
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-15: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void UnCheckedAllPopups()
        {
            this.chkPrint2.IsChecked = false;
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            string memoNum = this.lblVouNo.Tag.ToString().Trim();
            string frmname = ((ComboBoxItem)this.cmbPrnForm2.SelectedItem).Tag.ToString().Trim();
            string PaperType1 = (((ComboBoxItem)this.cmbPrnForm2.SelectedItem).Content.ToString().Trim() == "Voucher-2" ? "h" : "");


            /*
            <ComboBox x:Name="cmbPrnForm2" Width="85" Background="White" IsReadOnly="True" SelectedIndex="0" Margin="7,0,0,0" Visibility="Hidden" >
                    <ComboBoxItem Content="Voucher" Tag="VOUCHER"/>
                    <ComboBoxItem Content="Cheque" Tag="CHEQUE" />
                    <ComboBoxItem Content="M. Receipt" Tag="MRECEIPT"/>             
            </ComboBox>            
            */
            this.PrintVoucherMemo(memoNum, true, frmname, PaperType1);
            //string rptName = (list1.Count > 7 ? "Accounting.RptAccVou1" : "Accounting.RptAccVou1h");
        }

        private void btnVouEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to edit/copy", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var item1a = (HmsEntityAccounting.AccTransectionList)this.dgvTransList.SelectedItem;
                if (item1a.vstatus == "C")
                {
                    System.Windows.MessageBox.Show("Voucher already cancelled. Edit/Copy not possible", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var pap1 = vm1.SetParamEditVoucher(WpfProcessAccess.CompInfList[0].comcod, item1a.vounum);
                this.EditDs = null;
                this.EditDs = WpfProcessAccess.GetHmsDataSet(pap1);
                if (this.EditDs == null)
                    return;

                var vType1 = this.EditDs.Tables[0].Rows[0]["vounum"].ToString().Substring(0, 3) + this.EditDs.Tables[0].Rows[0]["vtcode"].ToString();
                var brncod = this.EditDs.Tables[0].Rows[0]["vounum"].ToString().Substring(9, 4);

                int i = 0;
                bool found1 = false;
                foreach (ComboBoxItem item1b in this.cmbVouType.Items)
                {
                    if (item1b.Tag.ToString().Trim() == vType1)
                    {
                        found1 = true;
                        break;
                    }
                    i++;
                }

                if (found1 == false)
                {
                    this.EditDs = null;
                    return;
                }

                this.cmbVouType.SelectedIndex = i;

                int j = 0;
                foreach (ComboBoxItem item1c in this.cmbVouBrn.Items)
                {
                    if (item1c.Tag.ToString().Trim().Substring(0, 4) == brncod)
                        break;
                    j++;
                }
                this.cmbVouBrn.SelectedIndex = j;
                this.cmbVouBrn.IsEnabled = false;

                this.xctk_dtpVouDat.Value = Convert.ToDateTime(this.EditDs.Tables[0].Rows[0]["voudat"]);
                this.xctk_dtpVouDat.Tag = Convert.ToDateTime(this.EditDs.Tables[0].Rows[0]["recndt"]).ToString("dd-MMM-yyyy");
                if (vType1.Contains("OPV"))
                    this.chkDateBlocked.IsEnabled = false;

                DateTime dat1 = Convert.ToDateTime(this.EditDs.Tables[0].Rows[0]["voudat"]);
                dat1 = DateTime.Parse("01-" + dat1.ToString("MMM-yyyy"));
                DateTime dat2 = dat1.AddMonths(1).AddDays(-1);
                this.xctk_dtpVouDat.Minimum = dat1;
                this.xctk_dtpVouDat.Maximum = dat2;

                this.btnOk_Click(null, null);

                this.txtblEditMode.Visibility = Visibility.Visible;
                this.lblVouNo.Content = this.EditDs.Tables[0].Rows[0]["vounum1"].ToString();
                this.lblVouNo.Tag = this.EditDs.Tables[0].Rows[0]["vounum"].ToString();

                this.txtVouRef.Text = this.EditDs.Tables[0].Rows[0]["vouref"].ToString();
                this.txtAdvice.Text = this.EditDs.Tables[0].Rows[0]["advref"].ToString();
                this.txtVouNar.Text = this.EditDs.Tables[0].Rows[0]["vounar"].ToString();

                string curcod1 = this.EditDs.Tables[0].Rows[0]["curcod"].ToString();
                decimal convFact1 = Convert.ToDecimal(this.EditDs.Tables[0].Rows[0]["curcnv"]);
                int idx1 = 0;
                foreach (ComboBoxItem cbitem in this.cmbCurrency.Items)
                {
                    if (cbitem.Uid.ToString().Trim() == curcod1)
                        break;
                    idx1++;
                }
                this.cmbCurrency.SelectedIndex = idx1;
                this.DecUDCurrExRate.Text = convFact1.ToString("##0.0000");

                var sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");

                //	select comcod, vounum, vounum1, voudat, vouref, chqref, advref, vounar, vtcode, vstatus, rowid, rowtime from dbo_acc.actrnb where comcod = @ComCod and vounum = @Desc01;
                //  select comcod, vounum, cactcode, sectcod, actcode, sircode, sircode2, trnqty, trnam, trnrmrk, rowid from dbo_acc.actrna	where comcod = @ComCod and vounum = @Desc01;

                foreach (DataRow dr1a in this.EditDs.Tables[1].Rows)
                {
                    decimal trnamt1 = decimal.Parse(dr1a["trnam"].ToString()) / convFact1;
                    decimal dramt1 = (trnamt1 > 0 ? trnamt1 : 0.00m);
                    decimal cramt1 = (trnamt1 < 0 ? trnamt1 * -1 : 0.00m);
                    decimal trnqty1 = decimal.Parse(dr1a["trnqty"].ToString()); ;
                    decimal trnrate1 = (trnqty1 > 0 && trnamt1 > 0 ? (trnamt1 / trnqty1) : 0.00m);
                    string cactcode1 = dr1a["cactcode"].ToString().Trim();
                    string cactcodeDesc1 = dr1a["cactcodeDesc"].ToString().Trim();
                    cactcodeDesc1 = (cactcodeDesc1.Length > 0 ? cactcode1 + " - " : "") + cactcodeDesc1;

                    string sectcod1 = dr1a["sectcod"].ToString().Trim();
                    string sectcodDesc1 = dr1a["sectcodDesc"].ToString().Trim();
                    sectcodDesc1 = (sectcodDesc1.Length > 0 ? sectcod1 + " - " : "") + sectcodDesc1;

                    string actcode1 = dr1a["actcode"].ToString().Trim();
                    string actcodeDesc1 = dr1a["actcodeDesc"].ToString().Trim();
                    actcodeDesc1 = (actcodeDesc1.Length > 0 ? actcode1 + " - " : "") + actcodeDesc1;

                    string sircode1 = dr1a["sircode"].ToString().Trim();
                    string sircodeDesc1 = dr1a["sircodeDesc"].ToString().Trim();
                    sircodeDesc1 = (sircodeDesc1.Length > 0 ? sircode1 + " - " : "") + sircodeDesc1;

                    string reptsl1 = dr1a["reptsl"].ToString();
                    string sircode2 = dr1a["sircode2"].ToString();
                    string sircode2Desc1 = dr1a["sircode2Desc"].ToString().Trim();
                    sircode2Desc1 = (sircode2Desc1.Length > 0 ? sircode2 + " - " : "") + sircode2Desc1;

                    string rmrk1 = dr1a["trnrmrk"].ToString();
                    string sirUnit1 = dr1a["sirunit"].ToString();

                    string ac1 = actcode1.Substring(0, 4);
                    bool CashBank = ((ac1 == "1901" || ac1 == "1903" || ac1 == "2902") ? true : false);

                    if (this.stkpLocation.Visibility == Visibility.Visible && CashBank == false)
                    {
                        if (sectcod1 == "000000000000")
                            return;
                    }

                    sectcod1 = (CashBank == true ? "000000000000" : sectcod1);
                    sectcodDesc1 = (CashBank == true ? "" : sectcodDesc1);

                    if (!vType1.Contains("JV") && !vType1.Contains("OP"))
                    {
                        var Ccod1 = this.ListVouTable1.FindAll(x => x.cactcode == cactcode1 && x.actcode == "000000000000" && x.sectcod == "000000000000");
                        if (Ccod1.Count == 0)
                        {
                            this.ListVouTable1.Add(new vmEntryVoucher1.VouTable()
                            {
                                trnsl = this.ListVouTable1.Count() + 1,
                                DrCrOrder = (vType1.Substring(0, 1) == "R" ? "01" : "02"), //(vType1.Substring(1, 1) == "C" ? "01" : "02"),
                                cactcode = cactcode1,
                                sectcod = "000000000000",
                                actcode = "000000000000",
                                sircode = "000000000000",
                                reptsl = "000",
                                sircode2 = "000000000000",
                                cactcodeDesc = cactcodeDesc1,
                                sectcodDesc = "",
                                actcodeDesc = "",
                                sircodeDesc = "",
                                sircode2Desc = "",
                                trnDesc = cactcodeDesc1,
                                trnqty = 0,
                                trnUnit = "",
                                trnrate = 0,
                                dramt = 0,
                                cramt = 0,
                                trnam = 0,
                                trnrmrk = ""
                            });
                        }
                    }

                    this.ListVouTable1.Add(new vmEntryVoucher1.VouTable()
                        {
                            trnsl = this.ListVouTable1.Count() + 1,
                            DrCrOrder = ((dramt1 - cramt1) > 0 ? "01" : "02"),
                            cactcode = cactcode1,
                            sectcod = sectcod1,
                            actcode = actcode1,
                            sircode = sircode1,
                            reptsl = reptsl1,
                            sircode2 = sircode2,
                            cactcodeDesc = cactcodeDesc1,
                            sectcodDesc = sectcodDesc1,
                            actcodeDesc = actcodeDesc1,
                            sircodeDesc = sircodeDesc1,
                            sircode2Desc = sircode2Desc1,
                            trnDesc = actcodeDesc1 + (sircodeDesc1.Length > 0 ? "\n\t" + sircodeDesc1 + (sircode2Desc1.Length > 0 ? "\n\t\t" + sircode2Desc1 : "") : ""),
                            trnqty = trnqty1,
                            trnUnit = sirUnit1,
                            trnrate = trnrate1,
                            dramt = dramt1,
                            cramt = cramt1,
                            trnam = trnamt1,
                            trnrmrk = rmrk1
                        });
                }

                bool QtyFound = false;
                foreach (var itemd in this.ListVouTable1)
                {
                    QtyFound = (itemd.trnqty != 0 || QtyFound ? true : false);
                }

                if (!(vType1.Contains("JV")))
                    this.AtxtCactCode.Text = this.EditDs.Tables[1].Rows[0]["cactcode"].ToString() + " - " + this.EditDs.Tables[1].Rows[0]["cactcodeDesc"].ToString();

                if ((vType1.Contains("PVB") || vType1.Contains("FTV")) && this.EditDs.Tables.Count > 2)
                {
                    if (this.EditDs.Tables[2].Rows.Count > 0)
                    {
                        string Content1 = this.EditDs.Tables[2].Rows[0]["cheqnum"].ToString().Trim();
                        string Tag1 = this.EditDs.Tables[2].Rows[0]["cheqbookid"].ToString() + this.EditDs.Tables[2].Rows[0]["cheqnum"].ToString().Trim();
                        this.cmbCheqNo.Items.Insert(0, new ComboBoxItem() { Content = Content1, Tag = Tag1 });
                        this.cmbCheqNo.SelectedIndex = 0;
                    }
                }
                this.EditDs = null;
                this.gridDetails1.Visibility = Visibility.Visible;
                this.btnUpdate.Visibility = Visibility.Visible;
                this.btnUpdate.IsEnabled = true;
                this.CalculateTotal();
                this.txtActCode.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            this.CalculateTotal();
        }

        private void cmbVouBrn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbVouBrn.SelectedItem == null)
                return;

            this.cmbVouBrn.ToolTip = ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Content.ToString();
            if (this.gridTransList.Visibility == Visibility.Visible)
                this.btnFilter_Click(null, null);
        }

        private void btnVouCancel_Click(object sender, RoutedEventArgs e)
        {

            this.UnCheckedAllPopups();
            if (this.dgvTransList.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("No record found to cancel", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var item1a = (HmsEntityAccounting.AccTransectionList)this.dgvTransList.SelectedItem;
            MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this voucher " + item1a.vamount1, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (msgresult != MessageBoxResult.Yes)
                return;

            int itemno1 = this.dgvTransList.SelectedIndex;
            var pap1 = vm1.SetParamCancelVoucher(WpfProcessAccess.CompInfList[0].comcod, item1a.vounum);

            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            ((HmsEntityAccounting.AccTransectionList)this.dgvTransList.Items[itemno1]).vstatus = "C";
            this.AccTrnLst[itemno1].vstatus = "C";
            this.dgvTransList.Items.Refresh();
            //AccTrnLst
            System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
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

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                WpfProcessAccess.AccSirCodeList.Where((x, match) => x.sircode.Substring(9, 3) != "000" && x.sirdesc1.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(200).OrderBy(m => m.sirdesc1));
        }

        private void AutoCompleteSirCode2_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
        }

        private void btnVouCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.btnVouEdit_Click(null, null);

                this.txtblEditMode.Visibility = Visibility.Hidden;
                this.cmbVouBrn.IsEnabled = true;

                this.xctk_dtpVouDat.Minimum = DateTime.Today.AddDays(-365 * 3);
                this.xctk_dtpVouDat.Maximum = DateTime.Today.AddDays(365 * 2);
                this.xctk_dtpVouDat.Value = DateTime.Today;
                this.EditDs = null;
                this.lblVouNo.Content = "XXVMM-CCCC-XXXXX";
                this.lblVouNo.Tag = "XXVYYYYMMCCCC";
                this.ListBlnkCheq1.Clear();
                this.cmbCheqNo.Items.Clear();
                // This option is under construction. To be developed soon --- Hafiz 04-Apr-2017

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-17: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void cmbVouType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridTransList.Visibility == Visibility.Visible)
                this.btnFilter_Click(null, null);
        }

        private void AtxtActCode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.conMenuActCod.IsOpen = true;
        }

        #region Budget Operation
        private void btnFindBudget_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var cactcode1 = (this.AtxtCactCode.Text.Trim().Length == 0 ? "000000000000" : (this.AtxtCactCode.Value.Trim().Length != 12 ? "000000000000" : this.AtxtCactCode.Value));
                if (cactcode1 == "000000000000")
                    return;
                var cactcode2 = this.PayBgdBalDetails.FirstOrDefault(x => x.cactcode == cactcode1);

                if (cactcode1.Substring(0, 8) == "19010002" || cactcode2 != null)
                    this.dgPayBgd1.ItemsSource = this.PayBgdBalDetails.FindAll(x => x.cactcode == cactcode1).ToList();
                else
                    this.dgPayBgd1.ItemsSource = this.PayBgdBalDetails.FindAll(x => x.cactcode.Substring(0, 8) != "19010002").ToList();


                this.frameFindPayProp = new DispatcherFrame();
                this.GridDataEntry.IsEnabled = false;
                this.GridFindPayProp.Visibility = Visibility.Visible;
                System.Windows.Threading.Dispatcher.PushFrame(this.frameFindPayProp);
                this.GridDataEntry.IsEnabled = true;
                this.GridFindPayProp.Visibility = Visibility.Collapsed;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-18: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnCloseFindPayProp_Click(object sender, RoutedEventArgs e)
        {
            this.dgPayBgd1.ItemsSource = null;
            this.frameFindPayProp.Continue = false; // un-blocks gui message pump
        }
        private void btnPayPropSelectForPay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgPayBgd1.SelectedIndex < 0)
                    return;
                var Item1 = ((vmEntryVoucher1.PayBgdBalance)this.dgPayBgd1.SelectedItem);
                this.AtxtActCode.Value = Item1.actcode;
                if (Item1.sircode != "000000000000")
                {
                    if (WpfProcessAccess.AccSirCodeList == null)
                        WpfProcessAccess.GetAccSirCodeList();

                    this.stkpSubHead.Visibility = Visibility.Visible;
                    var sirItemList = new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(WpfProcessAccess.AccSirCodeList.Where((x, match) => x.sircode == Item1.sircode));

                    this.AutoCompleteSirCode.ItemsSource = sirItemList;
                    this.AutoCompleteSirCode.SelectedValue = Item1.sircode;
                }
                else
                    this.stkpSubHead.Visibility = Visibility.Collapsed;

                this.txtAmount.Text = Item1.balam.ToString("###0.00");
                btnCloseFindPayProp_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-19: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnFindPayPropCode_Click(object sender, RoutedEventArgs e)
        {
            string code1 = this.txtFindCode.Text.Trim();
            this.txtFindCode.Text = "";
            if (code1.Length == 0)
                return;
            int rowIndex1 = -1;
            foreach (vmEntryVoucher1.PayBgdBalance item1a in this.dgPayBgd1.Items)
            {
                rowIndex1++;
                if (item1a.actcode.Substring(0, code1.Length) == code1)
                    break;
            }
            this.dgPayBgd1.SelectedIndex = rowIndex1;
            this.dgPayBgd1.ScrollIntoView(this.dgPayBgd1.SelectedItem);
        }

        private bool IsPaymentWithinBudget()
        {
            string voutitl1a = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim();
            if (!(voutitl1a.Substring(0, 2) == "PV" && voutitl1a.Substring(3, 2) == "83"))
                return true;

            var ListVouTable1u = this.ListVouTable1.FindAll(x => x.actcode != "000000000000");
            string cactcod1 = (ListVouTable1u[0].cactcode.Substring(0, 8) == "19010002" ? ListVouTable1u[0].cactcode : "000000000000");
            var bldList1 = this.PayBgdBalDetails.FindAll(x => x.cactcode == cactcod1 && x.sircode == "000000000000");
            foreach (var itemp in bldList1)
            {
                decimal mainSum1 = ListVouTable1u.FindAll(x => x.actcode == itemp.actcode).Sum(x => x.dramt);
                if (itemp.balam < mainSum1)
                    return false;
            }

            var bldList2 = this.PayBgdBalDetails.FindAll(x => x.cactcode == cactcod1 && x.sircode != "000000000000");
            foreach (var itemps in bldList2)
            {
                decimal subSum1 = ListVouTable1u.FindAll(x => x.actcode == itemps.actcode && x.sircode == itemps.sircode).Sum(x => x.dramt);
                if (itemps.balam < subSum1)
                    return false;
            }
            return true;
        }

        #endregion //  Budget Operation

        private void txt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.CalcObjName = ((WatermarkTextBox)sender).Name.ToUpper();
            this.gridCalc1.Visibility = Visibility.Visible;
            this.txtExprToCalc.Text = "";
            this.txtExprToCalc.Focus();
        }
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            switch (this.CalcObjName)
            {
                case "TXTAMOUNT":
                    this.txtAmount.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                    this.txtAmount.Focus();
                    break;
            }

            this.txtExprToCalc.Text = "";
            this.gridCalc1.Visibility = Visibility.Collapsed;
        }
        private void cmbCurr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbCurrency.SelectedItem == null)
                return;
            ComboBoxItem item1 = (ComboBoxItem)this.cmbCurrency.SelectedItem;

            this.cmbCurrency.ToolTip = item1.ToolTip;
            this.DecUDCurrExRate.ToolTip = item1.ToolTip;
            this.DecUDCurrExRate.Uid = item1.Uid;
            this.DecUDCurrExRate.Value = decimal.Parse("0" + item1.Tag.ToString());
            this.DecUDCurrExRate.IsEnabled = (this.cmbCurrency.SelectedIndex > 0);
            string curtxt = item1.Content.ToString().Trim();
            if (this.cmbCurrency.SelectedIndex > 0)
            {
                this.dgTrans.Columns[5].Header = "Rate [" + curtxt + "]";
                this.dgTrans.Columns[6].Header = "Debit Amt. [" + curtxt + "]";
                this.dgTrans.Columns[7].Header = "Credit Amt. [" + curtxt + "]";
                this.lblAmountTitle.Content = "Am_ount [" + curtxt + "]:";
                this.btnTotal.Content = "_Total [" + curtxt + "] :";
            }
            else
            {
                this.dgTrans.Columns[7].Header = "Rate";
                this.dgTrans.Columns[6].Header = "Debit Amount";
                this.dgTrans.Columns[7].Header = "Credit Amount";
                this.lblAmountTitle.Content = "Am_ount :";
                this.btnTotal.Content = "_Total : ";
            }
        }

        private void dgvTransList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnPrint3_Click(null, null);
        }
        private void dgvTransList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    this.dgvTransList.CommitEdit(DataGridEditingUnit.Cell, false);
                    this.dgvTransList.CommitEdit(DataGridEditingUnit.Row, false);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void dgvTransList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.btnPrint3_Click(null, null);
        }
        private void chkShowDraft_Click(object sender, RoutedEventArgs e)
        {
            ////this.GridItemList.Visibility = Visibility.Collapsed;
            if (this.chkShowDraft.IsChecked == true)
                this.ShowHideDraftListPanle();
        }
        private void ShowHideDraftListPanle()
        {
            this.dgvDraftList.ItemsSource = null;
            this.DispatcherFrame1 = new DispatcherFrame();
            this.GridDataEntry.IsEnabled = false;
            this.GridDraftList.Visibility = Visibility.Visible;
            this.GridDraftList.IsEnabled = true;
            this.xctk_dtpDraftFrom.Value = DateTime.Parse(this.xctk_dtpVouDat.Text).AddDays(-7);
            this.xctk_dtpDraftTo.Value = this.xctk_dtpVouDat.Value;
            this.txtDraftTrmId.Text = this.preparetrm1;

            this.btnFilterDraftList_Click(null, null);
            System.Windows.Threading.Dispatcher.PushFrame(this.DispatcherFrame1);
            this.GridDataEntry.IsEnabled = true;
            this.GridDraftList.IsEnabled = false;
            this.chkShowDraft.IsChecked = false;
            this.GridDraftList.Visibility = Visibility.Collapsed;
        }
        private void btnCloseDraftList_Click(object sender, RoutedEventArgs e)
        {
            this.DispatcherFrame1.Continue = false; // un-blocks gui message pump
        }
        private void btnFilterDraftList_Click(object sender, RoutedEventArgs e)
        {
            this.dgvDraftList.ItemsSource = null;
            string draftDesc1a = this.txtDraftDesc.Text.Trim();
            string dateFrom = this.xctk_dtpDraftFrom.Text;
            string dateTo = this.xctk_dtpDraftTo.Text;
            string drafttrm1a = this.txtDraftTrmId.Text.Trim();
            string signinnam1a = this.txtDraftUserId.Text.Trim();
            string voutyp1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Substring(0, 3);
            string voutyp1des = ((ComboBoxItem)this.cmbVouType.SelectedItem).Content.ToString().Trim();
            this.DraftTransactionList1 = WpfProcessAccess.GetDraftTransactionList(memohead1: voutyp1, draftDesc1: draftDesc1a, signinnam1: signinnam1a, drafttrm1: drafttrm1a, DateFrom1: dateFrom, DateTo1: dateTo).ToList();
            this.DraftTransactionList1 = this.DraftTransactionList1.FindAll(x => x.draftrmrk.Trim().Contains(voutyp1des)).ToList();
            this.dgvDraftList.ItemsSource = this.DraftTransactionList1;
        }

        private void dgvDraftList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.DraftGridNavigationClick(ActtionName: "btnDraftRetrive");
        }
        private void btnDraftNav_Click(object sender, RoutedEventArgs e)
        {
            string ActtionName = ((Button)sender).Tag.ToString().Trim();
            this.DraftGridNavigationClick(ActtionName);
        }
        private void DraftGridNavigationClick(string ActtionName = "btnDraftTop")
        {
            if (this.dgvDraftList.Items.Count == 0)
                return;

            if (this.dgvDraftList.SelectedIndex < 0)
                this.dgvDraftList.SelectedIndex = 0;

            int index1 = this.dgvDraftList.SelectedIndex;
            var item1 = (vmHmsGeneralList1.DraftTransactionList)this.dgvDraftList.SelectedItem;
            string draftsl1 = item1.slnum.ToString();
            string draftnum1x = item1.draftnum;
            Int64 rowid1x = item1.rowid;
            string draftDesc1x = item1.draftrmrk.Trim();
            string draftbyid1x = item1.draftbyid;
            string draftses1x = item1.draftses.Trim();
            string drafttrm1x = item1.drafttrm.Trim();

            switch (ActtionName)
            {
                case "btnDraftTop":
                    index1 = 0;
                    break;
                case "btnDraftPrev":
                    index1 = this.dgvDraftList.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnDraftNext":
                    index1 = this.dgvDraftList.SelectedIndex + 1;
                    if (index1 >= this.dgvDraftList.Items.Count)
                        index1 = this.dgvDraftList.Items.Count - 1;
                    break;
                case "btnDraftBottom":
                    index1 = this.dgvDraftList.Items.Count - 1;
                    break;
                case "btnDraftDismiss":
                    if (System.Windows.MessageBox.Show("Confirm Dismiss Selected Draft Sl# " + draftsl1 + ".\n" + draftDesc1x, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        DataSet dsdraft1 = WpfProcessAccess.UpdateDeleteDraftTransaction(UpadateDelete1: "DELETE", ds1: null, draftnum1: draftnum1x, rowid1: rowid1x,
                                draftrmrk1: draftDesc1x, draftbyid1: draftbyid1x, draftses1: draftses1x, drafttrm1: drafttrm1x);
                        if (dsdraft1 != null)
                            this.btnFilterDraftList_Click(null, null);
                    }
                    //return;
                    break;
                case "btnDraftRetrive":
                    if (System.Windows.MessageBox.Show("Confirm to retrive Draft Sl# " + draftsl1 + ".\n" + draftDesc1x, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                       MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        this.RetriveDraftMemo(MemoNum1a: draftnum1x, rowid1a: rowid1x);
                    }
                    break;
            }
            if (ActtionName == "btnDraftDismiss" || ActtionName == "btnDraftRetrive")
                return;

            this.dgvDraftList.SelectedIndex = index1;

            var item21 = (vmHmsGeneralList1.DraftTransactionList)this.dgvDraftList.Items[index1];
            this.dgvDraftList.ScrollIntoView(item21);
        }
        private void UpdateDraftVoucherInformation()
        {
            string brncod1 = ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Tag.ToString();
            string EditVounum1 = (this.lblVouNo.Tag.ToString().Trim().Length == 18 ? this.lblVouNo.Tag.ToString() : "");

            if (this.DraftMemoNum.Length == 0)
            {
                if (EditVounum1.Length > 0)//(this.EditDs != null)
                {
                    this.DraftMemoNum = this.lblVouNo.Tag.ToString();
                    this.txtblEditMode.Visibility = Visibility.Visible;
                }
                else
                    this.DraftMemoNum = this.lblVouNo.Tag.ToString().Substring(0, 3) + DateTime.Parse(this.xctk_dtpVouDat.Text).ToString("yyyyMM") + brncod1.Trim().Substring(0, 4) + "D0000";
            }
            DataSet ds1 = new DataSet("dsDraft");
            DataTable tbl1b = new DataTable("tblb");
            tbl1b.Columns.Add("comcod", typeof(String));
            tbl1b.Columns.Add("vounum", typeof(String));
            tbl1b.Columns.Add("voudat", typeof(String));
            tbl1b.Columns.Add("vouref", typeof(String));
            tbl1b.Columns.Add("cheqbookid", typeof(String));
            tbl1b.Columns.Add("chqref", typeof(String));
            tbl1b.Columns.Add("advref", typeof(String));
            tbl1b.Columns.Add("vounar", typeof(String));
            tbl1b.Columns.Add("curcod", typeof(String));
            tbl1b.Columns.Add("curcnv", typeof(Decimal));
            tbl1b.Columns.Add("vtcode", typeof(String));
            tbl1b.Columns.Add("vstatus", typeof(String));
            tbl1b.Columns.Add("recndt", typeof(String));
            tbl1b.Columns.Add("preparebyid", typeof(String));
            tbl1b.Columns.Add("prepareses", typeof(String));
            tbl1b.Columns.Add("preparetrm", typeof(String));
            tbl1b.Columns.Add("rowid", typeof(String));
            tbl1b.Columns.Add("rowtime", typeof(String));

            string CompCode = WpfProcessAccess.CompInfList[0].comcod;
            string cactcod1 = this.AtxtCactCode.Value.Trim();
            string cheqbookid1 = "XXXXXXXXXXXXXXXXXX";//  "190200030001151201";
            string cheqno1 = "";
            if (this.cmbCheqNo.Items.Count > 0)
            {
                string strtag1 = ((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim();
                if (strtag1.Length > 0)
                {
                    cheqbookid1 = strtag1.Substring(0, 18);
                    cheqno1 = strtag1.Substring(18);
                }
            }

            string vounum1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim().Substring(0, 3) +
                          DateTime.Parse(this.xctk_dtpVouDat.Text).ToString("yyyyMM") +
                          ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Tag.ToString().Trim().Substring(0, 4);

            vounum1 = (this.lblVouNo.Tag.ToString().Trim().Length == 18 ? this.lblVouNo.Tag.ToString() : vounum1);
            //string EditVounum1 = (this.lblVouNo.Tag.ToString().Trim().Length == 18 ? this.lblVouNo.Tag.ToString() : "");

            DataRow dr1b = tbl1b.NewRow();
            dr1b["comcod"] = CompCode;
            dr1b["vounum"] = vounum1;
            dr1b["voudat"] = this.xctk_dtpVouDat.Text;
            dr1b["vouref"] = this.txtVouRef.Text.Trim();
            dr1b["cheqbookid"] = cheqbookid1;
            dr1b["chqref"] = cheqno1;
            dr1b["advref"] = this.txtAdvice.Text.Trim();
            dr1b["vounar"] = this.txtVouNar.Text.Trim();
            dr1b["curcod"] = this.DecUDCurrExRate.Uid;
            dr1b["curcnv"] = decimal.Parse("0" + this.DecUDCurrExRate.Value.ToString());
            dr1b["vtcode"] = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim().Substring(3, 2);
            dr1b["vstatus"] = "A";
            dr1b["recndt"] = this.xctk_dtpVouDat.Tag.ToString();
            dr1b["preparebyid"] = this.preparebyid1;
            dr1b["prepareses"] = this.prepareses1;
            dr1b["preparetrm"] = this.preparetrm1;
            dr1b["rowid"] = "0";
            dr1b["rowtime"] = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            tbl1b.Rows.Add(dr1b);
            ds1.Tables.Add(tbl1b);

            DataTable tbl1a = ASITUtility2.ListToDataTable<vmEntryVoucher1.VouTable>(this.ListVouTable1);
            tbl1a.TableName = "tbla";

            ds1.Tables.Add(tbl1a);
            string brnName1 = ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Content.ToString().Trim();
            string voutype1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Content.ToString().Trim();

            string rmrk1 = voutype1 + ", Voucher" + (EditVounum1.Length > 0 ? " No: " + this.lblVouNo.Content.ToString().Trim() + "," : "") + " Date: " + this.xctk_dtpVouDat.Text
                + ", Branch : " + brnName1 + (this.AtxtCactCode.Text.Trim().Length > 0 ? ", " + this.AtxtCactCode.Text.Trim() : "");

            DataSet dsdraft1 = WpfProcessAccess.UpdateDeleteDraftTransaction(UpadateDelete1: "UPDATE", ds1: ds1, draftnum1: this.DraftMemoNum, rowid1: this.DraftMemoRowID,
                draftrmrk1: rmrk1, draftbyid1: this.preparebyid1, draftses1: this.prepareses1, drafttrm1: this.preparetrm1);
            if (dsdraft1 == null)
            {
                this.DraftMemoNum = "";
                this.DraftMemoRowID = 0;
                this.chkAllowDraft.IsChecked = false;
                this.stkpDraftOption.Background = Brushes.Yellow;
                System.Windows.MessageBox.Show("Draft update mode disabled. Please check the draft voucher list after re-open this screen.",
                    WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            this.DraftMemoNum = dsdraft1.Tables[0].Rows[0]["draftnum"].ToString();
            this.DraftMemoRowID = Convert.ToInt64(dsdraft1.Tables[0].Rows[0]["rowid"]);
        }
        private void RetriveDraftMemo(string MemoNum1a = "XXXXXXXXXXXX", Int64 rowid1a = 0)
        {
            try
            {
                DataSet dsdraft1 = WpfProcessAccess.RetriveDraftTransactionInfo(MemoNum1: MemoNum1a, rowid1: rowid1a);
                if (dsdraft1 == null)
                    return;

                StringReader strReader1 = new StringReader(dsdraft1.Tables[0].Rows[0]["draftdata"].ToString()); //new StringReader(xmlData);
                DataSet dsd2 = new DataSet();
                dsd2.ReadXml(strReader1);
                DataRow drb = dsd2.Tables[0].Rows[0];
                //-----------------------------------------------------
                if (!MemoNum1a.Contains("D"))
                    this.txtblEditMode.Visibility = Visibility.Visible;

                var vType1 = drb["vounum"].ToString().Substring(0, 3) + drb["vtcode"].ToString();
                var brncod = drb["vounum"].ToString().Substring(9, 4);

                int i = 0;
                bool found1 = false;
                foreach (ComboBoxItem item1b in this.cmbVouType.Items)
                {
                    if (item1b.Tag.ToString().Trim() == vType1)
                    {
                        found1 = true;
                        break;
                    }
                    i++;
                }

                if (found1 == false)
                {
                    this.EditDs = null;
                    return;
                }

                this.cmbVouType.SelectedIndex = i;

                int j = 0;
                foreach (ComboBoxItem item1c in this.cmbVouBrn.Items)
                {
                    if (item1c.Tag.ToString().Trim().Substring(0, 4) == brncod)
                        break;
                    j++;
                }
                this.cmbVouBrn.SelectedIndex = j;
                this.cmbVouBrn.IsEnabled = false;

                this.xctk_dtpVouDat.Value = Convert.ToDateTime(drb["voudat"]);
                this.xctk_dtpVouDat.Tag = Convert.ToDateTime(drb["recndt"]).ToString("dd-MMM-yyyy");

                if (vType1.Contains("OPV"))
                    this.chkDateBlocked.IsEnabled = false;

                this.btnOk_Click(null, null);

                string vno1 = drb["vounum"].ToString().Trim() + "XXXXX";
                string vno1a = vno1.Substring(0, 3) + vno1.Substring(7, 2) + "-" + vno1.Substring(9, 4) + "-" + vno1.Substring(13, 5);
                this.lblVouNo.Content = vno1a;
                this.lblVouNo.Tag = drb["vounum"].ToString();

                this.txtVouRef.Text = drb["vouref"].ToString();
                this.txtAdvice.Text = drb["advref"].ToString();
                this.txtVouNar.Text = drb["vounar"].ToString();

                string curcod1 = drb["curcod"].ToString();
                decimal convFact1 = Convert.ToDecimal(drb["curcnv"]);
                int idx1 = 0;
                foreach (ComboBoxItem cbitem in this.cmbCurrency.Items)
                {
                    if (cbitem.Uid.ToString().Trim() == curcod1)
                        break;
                    idx1++;
                }
                this.cmbCurrency.SelectedIndex = idx1;
                this.DecUDCurrExRate.Text = convFact1.ToString("##0.0000");

                this.dgTrans.ItemsSource = null;
                this.ListVouTable1.Clear();
                this.ListVouTable1 = dsd2.Tables[1].DataTableToList<vmEntryVoucher1.VouTable>().OrderBy(x => x.reptsl).ToList();
                string ac1 = "XXXX";
                if (!(vType1.Contains("JV")) && !(vType1.Contains("OP")))
                    ac1 = this.ListVouTable1[0].cactcode.Substring(0, 4);

                bool CashBank = ((ac1 == "1901" || ac1 == "1903" || ac1 == "2902") ? true : false);
                bool QtyFound = false;
                foreach (var itemd in this.ListVouTable1)
                {
                    QtyFound = (itemd.trnqty != 0 || QtyFound ? true : false);
                }
                if (!(vType1.Contains("JV")) && !(vType1.Contains("OP")))
                {
                    var lst1 = this.ListVouTable1.FindAll(x => x.actcode == "000000000000");
                    this.AtxtCactCode.Text = lst1[0].trnDesc;
                    if ((vType1.Contains("PVB") || vType1.Contains("FTV")))
                    {
                        string Content1 = drb["chqref"].ToString().Trim();
                        string Tag1 = drb["cheqbookid"].ToString();

                        this.cmbCheqNo.Items.Add(new ComboBoxItem() { Content = "NONE", Tag = "" });
                        if (Content1.Length > 0)
                            this.cmbCheqNo.Items.Insert(0, new ComboBoxItem() { Content = Content1, Tag = Tag1 });

                        if (this.cmbCheqNo.Items.Count > 0)
                            this.cmbCheqNo.SelectedIndex = 0;
                    }
                }

                this.DispatcherFrame1.Continue = false;
                this.EditDs = null;
                this.preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = DateTime.Now;
                this.DraftMemoNum = MemoNum1a;
                this.DraftMemoRowID = rowid1a;

                this.gridDetails1.Visibility = Visibility.Visible;
                this.btnUpdate.Visibility = Visibility.Visible;
                this.btnUpdate.IsEnabled = true;
                this.CalculateTotal();
                this.txtActCode.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("ACV-28: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

    }
}
