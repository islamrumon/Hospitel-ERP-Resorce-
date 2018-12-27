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
using ASITHmsViewMan.General;
using ASITHmsViewMan.Inventory;
using ASITHmsRpt2Inventory;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Collections.ObjectModel;
using ASITHmsViewMan.Accounting;
using ASITHmsRpt1GenAcc.Accounting;
using System.Windows.Threading;
using System.ComponentModel;

namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmEntryItemRcv1.xaml
    /// </summary>
    /// 
    public partial class frmEntryItemRcv1 : UserControl
    {
        private string TitaleTag1, TitaleTag2, TitaleTag3;  // 
        private bool FrmInitialized = false;

        private List<vmEntryItemRcv1.PendingOrderItemList> POrderItemList = new List<vmEntryItemRcv1.PendingOrderItemList>();
        private List<vmEntryItemRcv1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryItemRcv1.ListViewItemTable>();
        private List<vmEntryItemRcv1.ItemMfgPriceInfo> ItemMfgPriceList;// = new List<vmEntryItemRcv1.ItemMfgPriceInfo>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private List<HmsEntityGeneral.AcInfCodeBook> AcCodeList1 = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<HmsEntityGeneral.AuthorizeInf> AuthorizeTable1 = new List<HmsEntityGeneral.AuthorizeInf>();

        private List<vmHmsGeneralList1.DraftTransactionList> DraftTransactionList1 = new List<vmHmsGeneralList1.DraftTransactionList>();

        private vmEntryItemRcv1 vm1 = new vmEntryItemRcv1();
        private vmReportStore1 vmr1 = new vmReportStore1();
        private vmEntryVoucher1 vm1acc = new vmEntryVoucher1();
        private vmReportAccounts1 vmrptAcc = new vmReportAccounts1();
        private DataSet EditDs;

        private bool IsActiveTransListWindow { get; set; }
        private string CalcObjName = "NoName";


        private string preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
        private string prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
        private string preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
        private DateTime rowtime1 = DateTime.Now;

        private DispatcherFrame DispatcherFrame1;


        private string DraftMemoNum = "";
        private Int64 DraftMemoRowID = 0;
        public frmEntryItemRcv1()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;
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

            try
            {
                if (!this.FrmInitialized)
                {
                    this.FrmInitialized = true;
                    string[] tagPart1 = this.Tag.ToString().Trim().Split(',');
                    this.TitaleTag2 = (tagPart1.Length > 0 ? tagPart1[0].Trim() : ""); //this.Tag.ToString(); // Dynamic value of Tag property set at design time
                    this.TitaleTag3 = (tagPart1.Length > 1 ? tagPart1[1].Trim() : "");
                    this.chkAddInfo.IsChecked = false;
                    this.stkpAddInfo.Visibility = Visibility.Hidden;
                    this.gridAuthorize.Visibility = Visibility.Collapsed;
                    this.gridAuthorize.IsEnabled = false;
                    this.chkShowDraft.IsChecked = false;
                    this.chkAllowDraft.IsChecked = true;
                    this.GridDraftList.IsEnabled = false;
                    this.GridDraftList.Visibility = Visibility.Collapsed;
                    this.IsActiveTransListWindow = false;
                    ListViewItemTable1.Clear();
                    this.ConstructAutoCompletionSource();
                    this.POrderItemList.Clear();
                    this.gridCalc1.Visibility = Visibility.Collapsed;
                    this.GridItemList.Visibility = Visibility.Collapsed;
                    this.chkAutoTransListpr.IsChecked = this.IsActiveTransListWindow;
                    this.btnPrint2.Visibility = Visibility.Hidden;
                    this.btnPrint2Voucher.Visibility = Visibility.Hidden;
                    this.cmbPrnForm2.Visibility = Visibility.Hidden;
                    this.btnUpdate.Visibility = Visibility.Hidden;
                    this.gridDetails.Visibility = Visibility.Hidden;
                    this.xctk_dtpMrrDat.Minimum = DateTime.Today.AddDays(-365 * 3);
                    this.xctk_dtpMrrDat.Maximum = DateTime.Today.AddDays(365 * 2);
                    this.xctk_dtpMrrDat.Value = DateTime.Today;
                    this.xctk_dtchlndat.Value = DateTime.Today;
                    this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-33);
                    this.xctk_dtpToDate.Value = DateTime.Today;
                    this.autoRecByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoRecByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                    this.AtxtssirCod.Value = "000000000000";
                    this.ActivateAuthObjects();
                    if (this.TitaleTag2 == "CellPhone")
                    {
                        this.chkMgfInfo.Visibility = Visibility.Hidden;
                        this.lblbatchno.Content = "IMEI _No :";
                    }


                    if (this.IsActiveTransListWindow)
                    {
                        this.gridTransList.Visibility = Visibility.Visible;
                        this.gridTransList.IsEnabled = true;
                    }
                    else
                    {
                        this.gridTransList.Visibility = Visibility.Hidden;
                        this.gridTransList.IsEnabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ActivateAuthObjects()
        {

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_chkDateBlocked") == null)
            {
                this.chkDateBlocked.Visibility = Visibility.Collapsed;
                this.lblDateBlocked.Visibility = Visibility.Visible;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_chkMgfInfo") == null)
                this.chkMgfInfo.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_chkAutoTransListpr") == null)
                this.chkAutoTransListpr.Visibility = Visibility.Hidden;

            this.btnRecurring.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_btnCopyMrr") == null)
                this.btnCopyMrr.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_btnEditMrr") == null)
                this.btnEditMrr.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_btnCancelMrr") == null)
                this.btnCancelMrr.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_chkAccVoucher") == null)
            {
                this.chkAccVoucher.IsChecked = false;
                this.chkAccVoucher.Visibility = Visibility.Hidden;
                this.stkpAccVoucher.Visibility = Visibility.Hidden;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_chkAccVoucher_Optional") != null)
            {
                this.chkAccVoucher.IsChecked = false;
                this.chkAccVoucher.IsEnabled = true;
                this.chkAccVoucher.Visibility = Visibility.Visible;
                this.stkpAccVoucher.Visibility = Visibility.Hidden;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryItemRcv1_chkAllowDraft") == null)
            {
                this.chkAllowDraft.IsChecked = false;
                this.stkpDraftOption.Visibility = Visibility.Hidden;
            }
        }

        private void ConstructAutoCompletionSource()
        {
            try
            {
                var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
                foreach (var itemd1 in deptList1)
                {
                    if (itemd1.sectname.ToUpper().Contains("STORE") || itemd1.sectname.ToUpper().Contains("PROJECT"))
                    {
                        this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                    }
                }

                if (WpfProcessAccess.StaffList == null)
                    WpfProcessAccess.GetCompanyStaffList();



                if (WpfProcessAccess.AccSirCodeList == null)
                    WpfProcessAccess.GetAccSirCodeList();

                if (WpfProcessAccess.SupplierContractorList == null)
                    WpfProcessAccess.GetSupplierContractorList();

                this.AtxtssirCod.AutoSuggestionList.Clear();
                this.AtxtssirCod.Items.Clear();
                this.conMenussirCod.Items.Clear();
                //this.AtxtssirCod.AutoSuggestionList.Add("RECEIVE WITHOUT P.O.: [000000000000]");
                this.AtxtssirCod.AddSuggstionItem("CASH / DIRECT PURCHASE", "000000000000");      //.AutoSuggestionList.Add("CASH / DIRECT PURCHASE : [000000000000]");
                var mitm1a = new MenuItem() { Header = "CASH / DIRECT PURCHASE", Tag = "000000000000" };
                mitm1a.Click += conMenussirCod_MouseClick;
                this.conMenussirCod.Items.Add(mitm1a);

                foreach (var item1 in WpfProcessAccess.SupplierContractorList)
                {
                    this.AtxtssirCod.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());      //.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
                    var mitm1 = new MenuItem() { Header = item1.sirdesc.Trim(), Tag = item1.sircode.Trim() };
                    mitm1.Click += conMenussirCod_MouseClick;
                    this.conMenussirCod.Items.Add(mitm1);
                }

                var lcList1 = WpfProcessAccess.AccSirCodeList.FindAll(x => (x.sircode.Substring(0, 4) == "2502" || x.sircode.Substring(0, 4) == "3699") && x.sircode.Substring(9, 3) != "000").ToList();
                foreach (var item1 in lcList1)
                {
                    this.AtxtssirCod.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());      //.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
                    var mitm1 = new MenuItem() { Header = item1.sirdesc.Trim(), Tag = item1.sircode.Trim() };
                    mitm1.Click += conMenussirCod_MouseClick;
                    this.conMenussirCod.Items.Add(mitm1);
                }

                //810200010001  GAIN/LOSS OF MATERIAL STORE VALUE

                if (WpfProcessAccess.AccCodeList == null)
                    WpfProcessAccess.GetAccCodeList();

                //this.AcCodeList1 = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 2) == "15" || x.actcode.Substring(0, 2) == "17" || x.actcode.Substring(0, 8) == "23010009" || 
                //                  x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902") && (x.actcode.Substring(8, 4) != "0000"));

                this.AcCodeList1 = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 2) == "15" || x.actcode.Substring(0, 2) == "16" || x.actcode.Substring(0, 2) == "17"
                    || x.actcode.Substring(0, 8) == "23010009" || x.actcode.Substring(0, 8) == "81020001" || x.actcode.Substring(0, 4) == "1901") && (x.actcode.Substring(8, 4) != "0000"));
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void conMenussirCod_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtssirCod.Value = ((MenuItem)sender).Tag.ToString().Trim();
        }

        private void AtxtssirCod_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.conMenussirCod.IsOpen = true;
        }

        private void BuildTransactionList()
        {
            try
            {
                //	select slnum, comcod, memonum, memonum1, memodate, memodate1, PreparById, PreparByName, recvbyid, recvbyName, 
                //	approvbyid, approvbyName, sectcod, sectname, sectcod2, sectname2, Referance, Naration, MemoStatus, ssircode, ssirname, orderno, 
                //	chlnno, chlndat, chlndat1, posubject, leterdes, refdat, refdat1 from @TrnList1 order by slnum

                string FromDate = this.xctk_dtpFromDate.Text;
                string ToDate = this.xctk_dtpToDate.Text;
                string cmbst = "%";// ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                if (this.chkSelectedStore.IsChecked == true)
                    cmbst = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();

                if (cmbst == "%")
                {
                    this.txtTransTitle.Text = "All Transaction List From : " + FromDate + " To : " + ToDate;
                }
                else
                {
                    this.txtTransTitle.Text = " Transaction List From : " + FromDate + " To : " + ToDate + " Store Filter Id : " + cmbst.Trim();
                }
                //ASITFunParams.ProcessAccessParams pap1 = vm1.SetParamTransListItemReceive(WpfProcessAccess.CompInfList[0].comcod, FromDate, ToDate, cmbst);
                var pap1 = vmr1.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcpcod, "MRR", FromDate, ToDate, cmbst, "%", "%", "%", "[0123456789A]");

                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                this.dgvTransList.ItemsSource = null;

                this.ListViewTransTable1.Clear();
                this.ListViewTransTable1 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();
                this.dgvTransList.ItemsSource = this.ListViewTransTable1;

                this.dgvTransList.SelectedIndex = 0;
                this.dgvTransList.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
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
                //------ Draft information update option is enabled (Generally for local/high avaliability of database)
                if (this.chkAllowDraft.IsChecked == true && this.DraftMemoRowID > 0)
                    this.btnCalcTotal_Click(null, null);

                if (this.txtblEditMode.Visibility == Visibility.Visible)
                {
                    this.xctk_dtpMrrDat.Minimum = DateTime.Today.AddDays(-365 * 3);
                    this.xctk_dtpMrrDat.Maximum = DateTime.Today.AddDays(365 * 2);
                    this.xctk_dtpMrrDat.Value = DateTime.Today;
                    this.xctk_dtchlndat.Value = DateTime.Today;
                }
                this.chkAddInfo.IsChecked = false;
                this.stkpAddInfo.Visibility = Visibility.Hidden;
                this.cmbQtyUnit.Items.Clear();
                this.cmbRateUnit.Items.Clear();
                this.txtblEditMode.Visibility = Visibility.Hidden;
                this.gridCalc1.Visibility = Visibility.Collapsed;
                this.GridItemList.Visibility = Visibility.Collapsed;
                this.chkAutoTransListpr.IsEnabled = true;
                this.chkMgfInfo.IsEnabled = true;
                this.dgvTrans.ItemsSource = null;
                this.POrderItemList.Clear();
                this.UnCheckedAllPopups();
                this.stkItSp.IsEnabled = true;
                this.gridDetails.IsEnabled = true;

                this.gridDetails2.IsEnabled = true;
                this.stkpEntryAccVoucher.IsEnabled = true;
                this.stkpAddInfo.IsEnabled = true;

                this.btnPrint2.Visibility = Visibility.Hidden;
                this.btnUpdate.Visibility = Visibility.Hidden;
                this.gridDetails.Visibility = Visibility.Hidden;

                this.ListViewItemTable1.Clear();
                //this.xctk_dtpMrrDat.IsEnabled = false;
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtRSirDesc.Tag = "";
                this.lblTotaNetAmt.Content = "";
                this.lblTotaDiscAmt.Content = "";
                this.lblTotaVATAmt.Content = "";
                this.lblSlNo.Content = "xxxxxxxxxxxx";
                this.lblmfgName.Content = "";
                this.lblStdRate.Content = "";
                this.txtmrrQty.Text = (this.TitaleTag2 == "CellPhone" ? "1" : "");
                this.txtAmount.Text = "";
                this.txtrqRate.Text = "";
                this.txtchlnqty.Text = this.txtmrrQty.Text;
                this.lblUnit1.Content = "";
                this.lblUnit2.Content = "";
                this.txtDiscount.Text = "";
                this.txtrqVatAmt.Text = "";
                this.lblVouNoTitle.Tag = "";
                this.lblMrrNo.Content = "MRRMM-CCCC-XXXXX";
                this.lblMrrNo.Tag = "MRRYYYYMMCCCCXXXXX";
                this.txtbatchno.Text = "";
                this.xctk_dtMfg.Value = DateTime.Today;
                this.xctk_dtExp.Value = DateTime.Today.AddDays(365 * 2);
                this.lblVouNo.Content = "XVXMM-CCCC-XXXXX";
                this.lblVouNo.Tag = "XVXYYYYMMCCCCXXXXX";
                this.lblDrAmount.Content = "";
                this.lblCrAmount.Content = "";

                this.lblTotaGrossAmt.Content = "";
                this.lblTotaDiscAmt.Content = "";
                this.lblTotaNetAmt.Content = "";
                this.lblTotaChargeAmt.Content = "";
                this.lblTotaVATAmt.Content = "";
                this.lblTotaTotAmt.Content = "";
                this.txtCarryingAmt.Text = "0.00";
                this.txtLabourAmt.Text = "0.00";
                this.txtOtherChrgAmt.Text = "0.00";
                this.iudDisPer.Value = 0;
                this.iudVATPer.Value = 4.5;


                if (this.btnOk.Content.ToString() == "_New")
                {
                    this.chkDateBlocked.IsChecked = false;
                    //this.chkDateBlocked.IsEnabled = true;
                    this.stkIntro.IsEnabled = true;
                    this.cmbSectCod.IsEnabled = true;
                    this.AtxtssirCod.IsEnabled = true;
                    this.btnOrderList.IsEnabled = true;
                    this.cmborderno.IsEnabled = true;
                    this.autoRecByStaffSearch.IsEnabled = true;
                    this.stkpDraftOption.IsEnabled = true;
                    this.DraftMemoNum = "";
                    this.DraftMemoRowID = 0;


                    this.EditDs = null;
                    this.autoRecByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoRecByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                    this.txtmrrRef.Text = "";
                    this.txtmrrNarr.Text = "";
                    this.txtchlnno.Text = "";
                    if (this.IsActiveTransListWindow)
                    {
                        this.BuildTransactionList();
                        this.gridTransList.Visibility = Visibility.Visible;
                        this.gridTransList.IsEnabled = true;
                        this.dgvTransList.Focus();
                    }
                    else
                        this.cmbSectCod.Focus();

                    this.btnOk.Content = "_Ok";
                    return;

                }

                if (this.checkOkValidation() == false)
                    return;


                this.btnUpdate.Visibility = Visibility.Visible;
                this.gridTransList.Visibility = Visibility.Hidden;
                this.gridTransList.IsEnabled = false;
                this.gridDetails.Visibility = Visibility.Visible;
                this.btnUpdateVoucher.Visibility = Visibility.Hidden;
                this.btnUpdateVoucher.IsEnabled = false;
                this.btnPrint2Voucher.Visibility = Visibility.Hidden;
                this.cmbPrnForm2.Visibility = Visibility.Hidden;
                this.cmbDrAccHead.IsEnabled = true;
                this.cmbCrAccHead.IsEnabled = true;
                this.chkDateBlocked.IsChecked = false;
                //this.chkDateBlocked.IsEnabled = false;
                this.btnUpdate.IsEnabled = true;
                this.stkItem.IsEnabled = true;
                //this.stkIntro.IsEnabled = false;
                this.cmbSectCod.IsEnabled = false;
                this.btnOrderList.IsEnabled = false;
                this.cmborderno.IsEnabled = false;
                this.autoRecByStaffSearch.IsEnabled = false;
                this.stkpDraftOption.IsEnabled = false;
                this.chkAutoTransListpr.IsEnabled = false;
                this.chkMgfInfo.IsEnabled = false;
                this.stkpMfgInfo.Visibility = (this.chkMgfInfo.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
                string orderid1 = ((ComboBoxItem)this.cmborderno.SelectedItem).Tag.ToString().Trim();
                this.AtxtssirCod.IsEnabled = (orderid1 == "000000000000000000" ? true : false);
                this.dgvTrans.ItemsSource = this.ListViewItemTable1;
                this.btnOk.Content = "_New";
                this.PrepareAccTransHead();

                // this.AtxtssirCod.AddSuggstionItem("CASH / DIRECT PURCHASE", "000000000000"); 
                this.txtRSirDesc.Focus();                
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-04: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void PrepareAccTransHead()
        {
            this.cmbDrAccHead.Items.Clear();
            this.cmbCrAccHead.Items.Clear();
            foreach (var item in this.AcCodeList1)
            {
                if (item.actcode.Substring(0, 2) == "15" || item.actcode.Substring(0, 2) == "16" || item.actcode.Substring(0, 2) == "17")
                    this.cmbDrAccHead.Items.Add(new ComboBoxItem() { Content = item.actdesc1, Tag = item.actcode });
                else if (this.AtxtssirCod.Value == "000000000000" && item.actcode.Substring(0, 8) != "23010009" && item.actcode.Substring(0, 8) != "81020001")
                    this.cmbCrAccHead.Items.Add(new ComboBoxItem() { Content = item.actdesc1, Tag = item.actcode });
                else if (this.AtxtssirCod.Value == "369900101001" && item.actcode.Substring(0, 8) == "81020001")
                    this.cmbCrAccHead.Items.Add(new ComboBoxItem() { Content = item.actdesc1, Tag = item.actcode });
                else if (this.AtxtssirCod.Value != "000000000000" && this.AtxtssirCod.Value != "369900101001" && item.actcode.Substring(0, 8) == "23010009")
                    this.cmbCrAccHead.Items.Add(new ComboBoxItem() { Content = item.actdesc1, Tag = item.actcode });
            }
            this.cmbDrAccHead.SelectedIndex = 0;
            this.cmbCrAccHead.SelectedIndex = 0;
        }

        private bool checkOkValidation()
        {
            try
            {
                if (this.autoRecByStaffSearch.SelectedValue == null)
                    return false;

                int length0 = this.AtxtssirCod.Value.Trim().Length;

                int length1 = this.autoRecByStaffSearch.SelectedValue.ToString().Length;

                if (length0 <= 0 || length1 <= 0)
                    return false;

                string srfByID2 = this.autoRecByStaffSearch.SelectedValue.ToString();
                var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == srfByID2);
                if (listStaff1.Count == 0)
                    return false;


                var item1a = this.lblMrrNo.Tag.ToString();
                string StorID1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                // string SupllierID1 = this.AtxtssirCod.Text.Trim();
                String SupllierID1 = this.AtxtssirCod.Value.Trim();  // SupllierID1.Substring(SupllierID1.Length - 13).Replace("]", "");
                string AsOnDate1 = this.xctk_dtpMrrDat.Text.Trim();
                string OrderNum1 = ((ComboBoxItem)this.cmborderno.SelectedItem).Tag.ToString();
                //if (SupllierID1 != "000000000000")
                if (OrderNum1 != "000000000000000000")
                {
                    if (this.cmborderno.Items.Count == 0)
                        return false;

                    //string OrderNum1 = ((ComboBoxItem)this.cmborderno.SelectedItem).Tag.ToString();
                    var pap1 = vm1.SetParamGetOrderList(WpfProcessAccess.CompInfList[0].comcod, StorID1, SupllierID1, AsOnDate1, OrderNum1);
                    DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                    if (ds1 == null)
                        return false;
                    this.POrderItemList.Clear();
                    this.POrderItemList = ds1.Tables[0].DataTableToList<vmEntryItemRcv1.PendingOrderItemList>();
                    var List1v = (from lst in POrderItemList
                                  where lst.orderno == OrderNum1
                                  select new
                                  {
                                      invcode = "00000000",
                                      trcode = lst.rsircode,
                                      trdesc = lst.sirdesc,
                                      truid = lst.truid,
                                      trunit = lst.sirunit,
                                      ordqty = lst.ordrqty,
                                      ordrat = lst.orderrat
                                  });

                }
                else
                {

                    if (WpfProcessAccess.InvItemList == null)
                        WpfProcessAccess.GetInventoryItemList();

                }

                if (this.ItemMfgPriceList == null)
                {
                    var pap1 = vm1.SetParamItemMfgPriceList(WpfProcessAccess.CompInfList[0].comcod, "");
                    DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                    this.ItemMfgPriceList = ds1.Tables[0].DataTableToList<vmEntryItemRcv1.ItemMfgPriceInfo>();
                }
                return true;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-05: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
        }
        private void UnCheckedAllPopups()
        {
            this.chkPrint2.IsChecked = false;
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpMrrDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpMrrDat.IsEnabled)
                this.xctk_dtpMrrDat.Focus();
        }

        private void chkAutoTransListpr_Click(object sender, RoutedEventArgs e)
        {
            this.IsActiveTransListWindow = (this.chkAutoTransListpr.IsChecked == true);
            if (this.IsActiveTransListWindow && this.gridDetails.Visibility == Visibility.Hidden)
            {
                this.BuildTransactionList();
                this.gridTransList.Visibility = Visibility.Visible;
                this.gridTransList.IsEnabled = true;
                this.dgvTransList.Focus();
            }
            else if (this.IsActiveTransListWindow == false && this.gridDetails.Visibility == Visibility.Hidden)
            {
                this.gridTransList.Visibility = Visibility.Hidden;
                this.gridTransList.IsEnabled = false;
            }
            this.chkPrint2.IsChecked = false;
        }

        private void txtrqRate_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.cmbRateUnit.Items.Count == 0)
                    return;

                if (this.cmbRateUnit.SelectedIndex < 0)
                    return;
                this.txtAmount.Text = "";
                Double quantity = Double.Parse("0" + this.txtmrrQty.Text.ToString());
                Double Rate = Double.Parse("0" + this.txtrqRate.Text.ToString());
                Double Discount = Double.Parse("0" + this.txtDiscount.Text.ToString());
                Double VatAmount = Double.Parse("0" + this.txtrqVatAmt.Text.ToString());
                int Ridx1 = this.cmbRateUnit.SelectedIndex;
                double Convfact1 = double.Parse("0" + ((ComboBoxItem)this.cmbQtyUnit.Items[Ridx1]).Tag.ToString());

                Double Amount = quantity * Rate / Convfact1;
                Double NetAmount = (Amount - Discount + VatAmount);
                Double NetRate = (quantity == 0 ? 0.00 : Math.Round(NetAmount / quantity, 6));

                this.txtAmount.Text = Amount.ToString("#,##0.00").Trim();
                this.lblNetAmtShow.Content = NetAmount.ToString("#,##0.00").Trim();
                this.txtchlnqty.Text = this.txtmrrQty.Text.ToString();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-06: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void txtAmount_LostFocus(object sender, RoutedEventArgs e)
        {

            //this.txtrqRate.Text = "";
            try
            {
                Double quantity = Double.Parse("0" + this.txtmrrQty.Text.ToString());
                Double Amount = Double.Parse("0" + this.txtAmount.Text.Trim());
                Double Rate = (quantity == 0 ? 0.00 : Math.Round(Amount / quantity, 6));
                Double Discount = Double.Parse("0" + this.txtDiscount.Text.ToString());
                Double VatAmount = Double.Parse("0" + this.txtrqVatAmt.Text.ToString());

                Double NetAmount = (Amount - Discount + VatAmount);
                Double NetRate = (quantity == 0 ? 0.00 : Math.Round(NetAmount / quantity, 6));

                this.txtrqRate.Text = Rate.ToString("#,##0.00").Trim();
                this.lblNetAmtShow.Content = NetAmount.ToString("#,##0.00").Trim();
                this.txtchlnqty.Text = this.txtmrrQty.Text.ToString();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-07: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private Decimal validData(string txtData)
        {
            try
            {
                return decimal.Parse(txtData);
            }
            catch (Exception)
            {

                return 0;
            }
        }
        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (this.txtRSirDesc.Text.Trim().Length == 0 || this.txtRSirDesc.Tag.ToString().Trim().Length == 0)
                {
                    this.txtUID.Text = "";
                    this.txtRSirCode.Text = "";
                    this.lblUnit1.Content = "";
                    this.txtmrrQty.Text = "";
                    return;
                }

                decimal mrrQty1a = this.validData("0" + this.txtmrrQty.Text.Trim());
                if (mrrQty1a <= 0)
                {
                    this.txtmrrQty.Focus();
                    return;
                }

                decimal gmrrAmt1 = this.validData("0" + this.txtAmount.Text.Trim());// Math.Round(mrrQty1a * mrrRat1, 6);// this.validData("0" + this.txtAmount.Text.Replace("Amt:", "").Trim());
                //decimal mrrRat1 = Math.Round(gmrrAmt1 / mrrQty1a, 6);// this.validData("0" + this.txtrqRate.Text.Trim());

                if (gmrrAmt1 <= 0)
                {
                    this.txtrqRate.Focus();
                    return;
                }

                decimal DiscAmt1 = this.validData("0" + this.txtDiscount.Text.Trim());
                decimal mrrAmt1 = (gmrrAmt1 - DiscAmt1);
                decimal VatAmt1 = this.validData("0" + this.txtrqVatAmt.Text.Trim());
                decimal tmrrAmt1 = (gmrrAmt1 - DiscAmt1 + VatAmt1);
                decimal chlnqty1 = this.validData("0" + this.txtchlnqty.Text.Trim());
                if (chlnqty1 <= 0)
                {
                    this.txtchlnqty.Focus();
                    return;
                }

                //--- Start of Quatity Conversion -- Addition : 20-Nov-2017
                int uidx1 = this.cmbQtyUnit.SelectedIndex;
                decimal convfac1 = decimal.Parse(((ComboBoxItem)this.cmbQtyUnit.SelectedItem).Tag.ToString());
                mrrQty1a = mrrQty1a * convfac1;
                decimal mrrRat1 = Math.Round(gmrrAmt1 / mrrQty1a, 6);
                chlnqty1 = chlnqty1 * convfac1;
                //--- End of Quatity Conversion -- Addition : 20-Nov-2017


                string batchno1 = this.txtbatchno.Text.Trim();//.ToUpper();
                string mfgDat1 = this.xctk_dtMfg.Text.Trim();
                mfgDat1 = (mfgDat1.Length == 0 ? DateTime.Today.ToString("dd-MMM-yyyy") : mfgDat1);
                string expDat1 = this.xctk_dtExp.Text.Trim();
                expDat1 = (expDat1.Length == 0 ? DateTime.Today.AddDays(365).ToString("dd-MMM-yyyy") : expDat1);

                string mfgName = this.lblmfgName.Content.ToString().Trim();
                string stdRate = this.lblStdRate.Content.ToString().Trim();
                string batchno2 = "Chl.Qty: " + chlnqty1.ToString() + ", " + (batchno1.Length == 0 ? "" : "Batch: " + batchno1 + ", ") + "Mfg: " + mfgDat1 + ", Exp: " + expDat1;
                batchno2 += (mfgName.Length > 0 ? "\nMfg.By: " + mfgName : "") + (mfgName.Length == 0 && stdRate.Length > 0 ? "\n" : (mfgName.Length > 0 && stdRate.Length > 0 ? ", " : ""))
                            + (stdRate.Length > 0 ? "Std. P.Rate: " + stdRate : "");

                if (this.txtRSirDesc.Text.Trim().Length == 0)
                {
                    this.txtRSirCode.Text = "";
                    this.cmbQtyUnit.Items.Clear();
                    this.cmbRateUnit.Items.Clear();
                    this.lblmfgName.Content = "";
                    this.lblStdRate.Content = "";
                    this.txtUID.Text = "";
                    this.lblSlNo.Content = "xxxxxxxxxxxx";
                    this.lblUnit1.Content = "";
                    this.lblUnit2.Content = "";
                    this.txtmrrQty.Text = "";
                    this.txtAmount.Text = "";
                    this.txtchlnqty.Text = "";
                    this.txtrqRate.Text = "";
                    this.txtrqVatAmt.Text = "";
                    this.lblNetAmtShow.Content = "";
                    this.txtDiscount.Text = "";
                    this.txtbatchno.Text = "";
                    this.xctk_dtMfg.Value = DateTime.Today;
                    this.xctk_dtExp.Value = DateTime.Today.AddDays(365);
                }

                int serialno1 = this.ListViewItemTable1.Count + 1;
                string rsircode1 = this.txtRSirDesc.Tag.ToString();

                var List1a = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == rsircode1);
                if (List1a.Count == 0)
                    return;

                string rsirdesc1 = List1a[0].sirdesc.Trim();

                string rsirunit = List1a[0].sirunit.Trim();

                string rsiruid1a = this.txtUID.Text.Trim();

                string InvCode1a = this.lblSlNo.Content.ToString().Trim();

                var list1a = this.ListViewItemTable1.FindAll(x => x.invcode == InvCode1a);
                if (list1a.Count > 0)
                {
                    list1a[0].rsircode = rsircode1;
                    list1a[0].trdesc = rsirdesc1 + (batchno1.Length == 0 ? "" : " [" + batchno1 + "]"); // rsircode1 + ": " + rsirdesc1,
                    list1a[0].mrrqty = mrrQty1a;
                    list1a[0].truid = rsiruid1a;
                    list1a[0].trunit = rsirunit;
                    list1a[0].mrrrate = Math.Round(gmrrAmt1 / mrrQty1a, 6); // Math.Round(reqAmt1 / reqqty1a, 6),
                    list1a[0].gmrramt = gmrrAmt1;
                    list1a[0].discamt = DiscAmt1;
                    list1a[0].mrramt = mrrAmt1;// NetAmt1,
                    list1a[0].tmrramt = tmrrAmt1;
                    list1a[0].vatamt = VatAmt1;
                    list1a[0].chrgamt = 0.00m; // Charge Amount to be accomodate with database and stored  procedure -- Hafiz 02-May-2017
                    list1a[0].chlnqty = chlnqty1;
                    list1a[0].batchno = batchno1;
                    list1a[0].mfgdat = DateTime.Parse(mfgDat1);
                    list1a[0].expdat = DateTime.Parse(expDat1);
                    list1a[0].mfgdat1 = mfgDat1;
                    list1a[0].expdat1 = expDat1;
                    list1a[0].trdesc2 = batchno2;
                }
                else
                {
                    string[] m1 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
                    string[] d1 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5" };

                    var item1a = new vmEntryItemRcv1.ListViewItemTable()
                    {
                        trsl = serialno1.ToString() + ".",
                        invcode = DateTime.Now.ToString("yy") + m1[DateTime.Now.Month] + d1[DateTime.Now.Day] +
                                  DateTime.Now.ToString("HHmmss") + new Random().Next(11, 99).ToString().Trim(), // "000000000000", //Year(2)+Month(1)+Day(1)+Hour(2)+Minute(2)+Second(2)+Rand(2)
                        rsircode = rsircode1,
                        trdesc = rsirdesc1 + (batchno1.Length == 0 ? "" : " [" + batchno1 + "]"), // rsircode1 + ": " + rsirdesc1,
                        mrrqty = mrrQty1a,
                        truid = rsiruid1a,
                        trunit = rsirunit,
                        mrrrate = Math.Round(gmrrAmt1 / mrrQty1a, 6), // Math.Round(reqAmt1 / reqqty1a, 6),
                        gmrramt = gmrrAmt1,
                        discamt = DiscAmt1,
                        mrramt = mrrAmt1,// NetAmt1,                        
                        tmrramt = tmrrAmt1,
                        vatamt = VatAmt1,
                        chrgamt = 0.00m, // Charge Amount to be accomodate with database and stored  procedure -- Hafiz 02-May-2017
                        chlnqty = chlnqty1,
                        batchno = batchno1,
                        mfgdat = DateTime.Parse(mfgDat1),
                        expdat = DateTime.Parse(expDat1),
                        mfgdat1 = mfgDat1,
                        expdat1 = expDat1,
                        trdesc2 = batchno2
                    };

                    this.ListViewItemTable1.Add(item1a);
                }

                this.txtbatchno.Text = "";
                if (this.TitaleTag2 == "CellPhone")
                {
                    this.txtmrrQty.Text = "1";
                    this.txtchlnqty.Text = "1";
                }
                else
                {
                    this.txtRSirCode.Text = "";
                    this.txtRSirDesc.Text = "";
                    this.txtRSirDesc.Tag = "";
                    this.cmbQtyUnit.Items.Clear();
                    this.cmbRateUnit.Items.Clear();
                    this.txtUID.Text = "";
                    this.lblUnit1.Content = "";
                    this.lblUnit2.Content = "";
                    this.lblmfgName.Content = "";
                    this.lblStdRate.Content = "";
                    this.lblSlNo.Content = "xxxxxxxxxxxx";
                    this.txtmrrQty.Text = "";
                    this.txtchlnqty.Text = "";
                    this.txtAmount.Text = "";
                    this.txtrqRate.Text = "";
                    this.txtDiscount.Text = "";
                    this.txtrqVatAmt.Text = "";
                    this.lblNetAmtShow.Content = "";
                    this.xctk_dtMfg.Value = DateTime.Today;
                    this.xctk_dtExp.Value = DateTime.Today.AddDays(365);
                }
                this.gridCalc1.Visibility = Visibility.Collapsed;

                this.btnCalcTotal_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-08: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.btnSetCharges_Click(null, null);

                if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                {
                    return;
                }

                string OldVouNum1 = this.lblVouNoTitle.Tag.ToString().Trim();
                string VouNum1 = "000000000000000000";
                if (sender != null)  // Blocked when update Authorization
                {
                    if (this.chkAccVoucher.IsChecked == true)
                    {
                        if (this.EditDs != null && OldVouNum1.Length > 0 && OldVouNum1 != "000000000000000000")
                        {
                            var pap1b = vm1acc.SetParamCancelVoucher(WpfProcessAccess.CompInfList[0].comcod, OldVouNum1);
                            DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                            if (ds1b == null)
                                return;
                        }
                        this.btnUpdateVoucher_Click(null, null);
                        VouNum1 = this.lblVouNo.Tag.ToString().Trim();
                    }
                }
                VouNum1 = (sender == null && this.EditDs != null ? OldVouNum1 : VouNum1);  // To update Authorization

                string OrderNum1 = "000000000000000000";
                if (this.cmborderno.Items.Count > 0)
                    OrderNum1 = ((ComboBoxItem)this.cmborderno.SelectedItem).Tag.ToString();

                string EditMrrNum1 = (this.EditDs != null ? this.lblMrrNo.Tag.ToString() : "");
                string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                string recevbyId1a = this.autoRecByStaffSearch.SelectedValue.ToString();

                if (EditMrrNum1.Length == 18)
                {
                    var pap1b = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, EditMrrNum1, "BACKUP", "MESSAGE");
                    DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                    if (ds1b == null)
                        return;
                }

                this.InitializeAuthorization(); // To update the prepared by record
                DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpMrrDat.Text), EditMemoNum1: EditMrrNum1,
                            cbSectCode: cbSectCode1, ssircode1: this.AtxtssirCod.Value.Trim(), recvByID1: recevbyId1a,
                            OrderNum1: OrderNum1, chalNum1: this.txtchlnno.Text.Trim(), chalDate1: DateTime.Parse(this.xctk_dtchlndat.Text), MemoRef1: this.txtmrrRef.Text.Trim(),
                            carramt1: this.txtCarryingAmt.Text.Trim(), labamt1: this.txtLabourAmt.Text.Trim(), othramt1: this.txtOtherChrgAmt.Text.Trim(), MemoNar1: this.txtmrrNarr.Text.Trim(),
                            ListViewItemTable1: this.ListViewItemTable1, vounum1: VouNum1,
                            AuthorizeTable1: this.AuthorizeTable1, _preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);
                //_preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

                //String xx1 = ds1.GetXml().ToString();

                var pap1 = vm1.SetParamUpdateItemReceive(WpfProcessAccess.CompInfList[0].comcod, ds1, EditMrrNum1);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                this.lblMrrNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                this.lblMrrNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();
                this.EditDs = new DataSet(); // For Current Authorization
                this.btnUpdate.IsEnabled = false;
                this.stkItem.IsEnabled = false;
                this.stkIntro.IsEnabled = false;
                this.gridDetails2.IsEnabled = false;
                this.stkpEntryAccVoucher.IsEnabled = false;
                this.stkpAddInfo.IsEnabled = false;
                this.btnPrint2.Visibility = Visibility.Visible;


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
                System.Windows.MessageBox.Show("MRR-09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void UpdateDraftMRRInformation()
        {
            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            string cbSectName1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Content.ToString().Trim();
            if (this.DraftMemoNum.Length == 0)
            {
                if (this.EditDs != null)
                    this.DraftMemoNum = this.lblMrrNo.Tag.ToString();
                else
                    this.DraftMemoNum = "MRR" + DateTime.Parse(this.xctk_dtpMrrDat.Text).ToString("yyyyMM") + cbSectCode1.Trim().Substring(0, 4) + "D0000";
            }

            DataSet ds1 = new DataSet("dsDraft");
            DataTable tbl1b = new DataTable("tblb");
            tbl1b.Columns.Add("mrrdat", typeof(String));
            tbl1b.Columns.Add("sectcod", typeof(String));
            tbl1b.Columns.Add("ssircode", typeof(String));
            tbl1b.Columns.Add("recvbyid", typeof(String));
            tbl1b.Columns.Add("mrrref", typeof(String));
            tbl1b.Columns.Add("mrrnar", typeof(String));
            tbl1b.Columns.Add("orderno", typeof(String));
            tbl1b.Columns.Add("chlnno", typeof(String));
            tbl1b.Columns.Add("chlndat", typeof(String));
            tbl1b.Columns.Add("carramt", typeof(Decimal));
            tbl1b.Columns.Add("labamt", typeof(Decimal));
            tbl1b.Columns.Add("othramt", typeof(Decimal));
            tbl1b.Columns.Add("vounum", typeof(String));

            string OrderNum1 = "000000000000000000";
            if (this.cmborderno.Items.Count > 0)
                OrderNum1 = ((ComboBoxItem)this.cmborderno.SelectedItem).Tag.ToString();

            DataRow drb1 = tbl1b.NewRow();
            drb1["mrrdat"] = this.xctk_dtpMrrDat.Text;
            drb1["sectcod"] = cbSectCode1;
            drb1["ssircode"] = this.AtxtssirCod.Value.Trim();
            drb1["recvbyid"] = "000000000000";
            if (this.autoRecByStaffSearch.SelectedValue != null)
                drb1["recvbyid"] = this.autoRecByStaffSearch.SelectedValue.ToString();

            drb1["mrrref"] = this.txtmrrRef.Text.Trim();
            drb1["mrrnar"] = this.txtmrrNarr.Text.Trim();
            drb1["orderno"] = OrderNum1;
            drb1["chlnno"] = this.txtchlnno.Text.Trim();
            drb1["chlndat"] = this.xctk_dtchlndat.Text;
            drb1["carramt"] = "0" + this.txtCarryingAmt.Text.Trim();
            drb1["labamt"] = "0" + this.txtLabourAmt.Text.Trim();
            drb1["othramt"] = "0" + this.txtOtherChrgAmt.Text.Trim();
            drb1["vounum"] = this.lblVouNoTitle.Tag.ToString().Trim();
            tbl1b.Rows.Add(drb1);
            ds1.Tables.Add(tbl1b);

            string rmrk1 = "Memo" + (this.EditDs != null ? " No: " + this.lblMrrNo.Content.ToString().Trim() + "," : "") + " Date: " + this.xctk_dtpMrrDat.Text
                            + ", Store : " + cbSectName1 + ", Supply Source : " + this.AtxtssirCod.Text.Trim();

            DataTable tbl1a = ASITUtility2.ListToDataTable<vmEntryItemRcv1.ListViewItemTable>(this.ListViewItemTable1);
            tbl1a.TableName = "tbla";
            ds1.Tables.Add(tbl1a);
            DataSet dsdraft1 = WpfProcessAccess.UpdateDeleteDraftTransaction(UpadateDelete1: "UPDATE", ds1: ds1, draftnum1: this.DraftMemoNum, rowid1: this.DraftMemoRowID,
                draftrmrk1: rmrk1, draftbyid1: this.preparebyid1, draftses1: this.prepareses1, drafttrm1: this.preparetrm1);
            if (dsdraft1 == null)
            {
                this.DraftMemoNum = "";
                this.DraftMemoRowID = 0;
                this.chkAllowDraft.IsChecked = false;
                this.stkpDraftOption.Background = Brushes.Yellow;
                System.Windows.MessageBox.Show("Draft update mode disabled. Please check the draft memo list after re-open this screen.",
                    WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            this.DraftMemoNum = dsdraft1.Tables[0].Rows[0]["draftnum"].ToString();
            this.DraftMemoRowID = Convert.ToInt64(dsdraft1.Tables[0].Rows[0]["rowid"]);
        }

        private void btnCalcTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.GridItemList.Visibility = Visibility.Collapsed;
                foreach (var item in this.ListViewItemTable1)
                {
                    item.gmrramt = (item.mrrqty <= 0 ? 0.00m : item.gmrramt);
                    item.discamt = (item.mrrqty <= 0 ? 0.00m : item.discamt);
                    item.chrgamt = (item.mrrqty <= 0 ? 0.00m : item.chrgamt);
                    item.vatamt = (item.mrrqty <= 0 ? 0.00m : item.vatamt);

                    item.mrrrate = (item.mrrqty <= 0 ? 0.00m : item.gmrramt / item.mrrqty);

                    item.mrramt = item.gmrramt - item.discamt;
                    item.tmrramt = item.mrramt + item.chrgamt + item.vatamt;
                    var item1 = WpfProcessAccess.InvItemList.Find(x => x.sircode == item.rsircode);
                    string tq1 = "", tr1 = "";
                    tq1 = (item1.siruconf > 0 && item.mrrqty > 0 ? Math.Round(item.mrrqty / item1.siruconf, 2).ToString() + " " + item1.sirunit2.Trim() : "");
                    tq1 = (tq1.Length > 0 ? tq1 + ", " : "") + (item1.siruconf3 > 0 && item.mrrqty > 0 ? Math.Round(item.mrrqty / item1.siruconf3, 2).ToString() + " " + item1.sirunit3.Trim() : "");

                    tr1 = (item1.siruconf > 0 && item.mrrqty > 0 ? "Rate /" + item1.sirunit2.Trim() + " = " + Math.Round(item.mrrrate * item1.siruconf, 2).ToString("#,##0.00") : "");
                    tr1 = (tr1.Length > 0 ? tr1 + ", " : "") + (item1.siruconf3 > 0 && item.mrrqty > 0 ? "Rate /" + item1.sirunit3.Trim() + " = " + Math.Round(item.mrrrate * item1.siruconf3, 0).ToString("#,##0.00") : "");

                    item.tooltipqty = tq1;// "0000 Ban, 00000 Ton";
                    item.tooltiprate = tr1;// "Rate/Ban : 000000";
                }

                this.txtCarryingAmt.Text = decimal.Parse("0" + this.txtCarryingAmt.Text.Trim()).ToString("#,##0.00");
                this.txtLabourAmt.Text = decimal.Parse("0" + this.txtLabourAmt.Text.Trim()).ToString("#,##0.00");
                this.txtOtherChrgAmt.Text = decimal.Parse("0" + this.txtOtherChrgAmt.Text.Trim()).ToString("#,##0.00");

                this.lblTotaGrossAmt.Content = this.ListViewItemTable1.Sum(x => x.gmrramt).ToString("#,##0.00");
                this.lblTotaNetAmt.Content = this.ListViewItemTable1.Sum(x => x.mrramt).ToString("#,##0.00");
                this.lblTotaDiscAmt.Content = this.ListViewItemTable1.Sum(x => x.discamt).ToString("#,##0.00");
                this.lblTotaChargeAmt.Content = this.ListViewItemTable1.Sum(x => x.chrgamt).ToString("#,##0.00");
                this.lblTotaVATAmt.Content = this.ListViewItemTable1.Sum(x => x.vatamt).ToString("#,##0.00");
                this.lblTotaTotAmt.Content = this.ListViewItemTable1.Sum(x => x.tmrramt).ToString("#,##0.00");
                this.lblDrAmount.Content = Math.Round(decimal.Parse(this.lblTotaTotAmt.Content.ToString()), 0).ToString("#,##0.00");
                this.lblCrAmount.Content = Math.Round(decimal.Parse(this.lblTotaTotAmt.Content.ToString()), 0).ToString("#,##0.00"); ;
                this.dgvTrans.ItemsSource = this.ListViewItemTable1;
                this.dgvTrans.Items.Refresh();

                //------ Draft information update option is enabled (Generally for local/high avaliability of database)
                if (this.chkAllowDraft.IsChecked == true)
                    this.UpdateDraftMRRInformation();

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-14: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to view/print report", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                LocalReport rpt1 = null;
                string WindowTitle1 = "";
                if (this.rb3SelectedMemopr.IsChecked == true)
                {
                    var item1a = (HmsEntityInventory.InvTransectionList)this.dgvTransList.SelectedItem;
                    this.PrintMrrMemo(item1a.memonum, false);

                }
                else if (this.rb3TableRecoredspr.IsChecked == true)
                {
                    var list1 = this.ListViewTransTable1;
                    var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")));
                    //var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();
                    //list3.Add(new HmsEntityGeneral.ReportGeneralInfo()
                    //{
                    //    RptCompName = WpfProcessAccess.CompInfList[0].comnam,
                    //    RptCompAdd1 = WpfProcessAccess.CompInfList[0].comadd1,
                    //    RptCompAdd2 = WpfProcessAccess.CompInfList[0].comadd2,
                    //    RptFooter1 = "Print Source: " + WpfProcessAccess.SignedInUserList[0].terminalID + ", " +
                    //                 WpfProcessAccess.SignedInUserList[0].signinnam + ", " +
                    //                 WpfProcessAccess.SignedInUserList[0].sessionID + ", " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")
                    //});
                    rpt1 = StoreReportSetup.GetLocalReport("Store.RptTransectionList", list1, null, list3); // ( R_01_RptSetup.RptSetupItemList1(ds1, ds2);          
                    WindowTitle1 = "Item Receive Transaction List";
                    if (rpt1 == null)
                        return;

                    if (this.rb3QuickPrintpr.IsChecked == true)
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
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-10: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item1a = this.lblMrrNo.Tag.ToString();
                //this.PrintMrrMemo(item1a, true);
                this.PrintMrrMemo(item1a, false);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        //
        private void PrintMrrMemo(string memoNum, bool DirectPrint = false)
        {
            try
            {
                LocalReport rpt1 = null;
                string WindowTitle1 = "";
                var pap1 = vmr1.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, memoNum);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurMrrMemo>();
                var list2 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();
                string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                            + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);


                // select comcod, srfno, auhcid, auhcnam, autime, aucode, aulevel, autitle, austat, statdes, aunote, luhcid, luses, lutrm, lutime from #tblstvx02 
                // CBALCOD01000,    CBALCOD01001,   CBALCOD01003,   CBALCOD01010
                //  RptParVal1, RptParVal2, RptParVal3, RptParVal4

                DataRow[] dr1 = ds1.Tables[3].Select("aucode = 'CBALCOD01000'", "aucode"); // Prepared By
                DataRow[] dr2 = ds1.Tables[3].Select("aucode = 'CBALCOD01001'", "aucode"); // Checked By
                DataRow[] dr3 = ds1.Tables[3].Select("aucode = 'CBALCOD01003'", "aucode"); // Verified By
                DataRow[] dr4 = ds1.Tables[3].Select("aucode = 'CBALCOD01010'", "aucode"); // Approved By

                list3[0].RptParVal1 = (dr1.Length > 0 ? dr1[0]["auhcnam"].ToString().Replace(",", "\n") : "");
                list3[0].RptParVal2 = (dr2.Length > 0 ? dr2[0]["auhcnam"].ToString().Replace(",", "\n") : "");
                list3[0].RptParVal3 = (dr3.Length > 0 ? dr3[0]["auhcnam"].ToString().Replace(",", "\n") : "");
                list3[0].RptParVal4 = (dr4.Length > 0 ? dr4[0]["auhcnam"].ToString().Replace(",", "\n") : "");

                rpt1 = StoreReportSetup.GetLocalReport("Store.RptMRRMemo01", list1, list2, list3);
                if (rpt1 == null)
                    return;

                //System.Windows.MessageBox.Show(item1a.reqno);
                if (DirectPrint == true)
                {
                    RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                    DirectPrint1.PrintReport(rpt1);
                    DirectPrint1.Dispose();
                }
                else
                {
                    WindowTitle1 = "Item Receive Memo";
                    string RptDisplayMode = "PrintLayout";
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        //

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.gridTransList.IsEnabled = false;
            this.BuildTransactionList();
            this.gridTransList.IsEnabled = true;
        }

        private void btnOrderList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.cmborderno.Items.Clear();
                var item1a = this.lblMrrNo.Tag.ToString();
                string StorID1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                // string SupllierID1 = this.AtxtssirCod.Text.Trim();
                string SupllierID1 = this.AtxtssirCod.Value.Trim();
                if (SupllierID1.Length < 0)
                    return;

                SupllierID1 = this.AtxtssirCod.Value.Trim(); //SupllierID1.Substring(SupllierID1.Length - 13).Replace("]", "");
                if (SupllierID1 != "000000000000")
                {
                    string AsOnDate1 = this.xctk_dtpMrrDat.Text.Trim();
                    var pap1 = vm1.SetParamGetOrderList(WpfProcessAccess.CompInfList[0].comcod, StorID1, SupllierID1, AsOnDate1, "%");
                    DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                    if (ds1 == null)
                        return;

                    //this.cmborderno.Items.Clear();
                    //this.AtxtssirCod.AutoSuggestionList.Add("RECEIVE WITHOUT P.O.: [000000000000]");
                    foreach (DataRow itemd1 in ds1.Tables[0].Rows)
                    {
                        this.cmborderno.Items.Add(new ComboBoxItem() { Content = itemd1["orderdat1"].ToString() + ", " + itemd1["orderno1"].ToString(), Tag = itemd1["orderno"].ToString() });
                    }
                }
                this.cmborderno.Items.Add(new ComboBoxItem() { Content = "RECEIVE WITHOUT PURCHASE ORDER", Tag = "000000000000000000" });
                this.cmborderno.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void StoreSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cmborderno.Items.Clear();
            this.cmborderno.Items.Add(new ComboBoxItem() { Content = "RECEIVE WITHOUT PURCHASE ORDER", Tag = "000000000000000000" });
            string SupllierID1 = this.AtxtssirCod.Value.Trim(); //this.AtxtssirCod.Text.Trim();
            if (SupllierID1.Length < 0)
                return;

            SupllierID1 = this.AtxtssirCod.Value.Trim();    //SupllierID1.Substring(SupllierID1.Length - 13).Replace("]", "");
            this.btnOrderList.IsEnabled = (SupllierID1 != "000000000000");
            this.cmborderno.IsEnabled = (SupllierID1 != "000000000000");
            this.cmborderno.SelectedIndex = 0;

            if (this.chkAutoTransListpr.IsChecked == true)
                this.btnFilter_Click(null, null);

            //if (this.ListViewItemTable1.Count > 0)
            this.PrepareAccTransHead();

        }

        private void AtxtssirCod_LostFocus(object sender, RoutedEventArgs e)
        {
            this.StoreSupplier_SelectionChanged(null, null);
        }
        private void txtrqVatPerCentpr_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal vatam = decimal.Parse("0" + this.txtrqVatPerCentpr.Text.Trim()) * 0.01m * decimal.Parse("0" + this.txtAmount.Text.Trim());
            this.txtrqVatAmt.Text = vatam.ToString("#,##0.00");
        }

        private void btnEditMrr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to edit", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var item1a = (HmsEntityInventory.InvTransectionList)this.dgvTransList.SelectedItem;
                if (item1a.MemoStatus == "C")
                {
                    System.Windows.MessageBox.Show("MRR Memo already cancelled. Edit not possible", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var pap1 = vm1.SetParamEditMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum);
                this.EditDs = null;
                this.EditDs = WpfProcessAccess.GetHmsDataSet(pap1);
                if (this.EditDs == null)
                    return;
                DataRow dr0 = this.EditDs.Tables[0].Rows[0];
                DataRow dr2 = this.EditDs.Tables[2].Rows[0];
                this.xctk_dtpMrrDat.Value = Convert.ToDateTime(dr0["memodate"].ToString());

                DateTime dat1 = Convert.ToDateTime(dr0["memodate"].ToString());
                dat1 = DateTime.Parse("01-" + dat1.ToString("MMM-yyyy"));
                DateTime dat2 = dat1.AddMonths(1).AddDays(-1);
                this.xctk_dtpMrrDat.Minimum = dat1;
                this.xctk_dtpMrrDat.Maximum = dat2;


                int x = 0;
                foreach (ComboBoxItem item in this.cmbSectCod.Items)
                {
                    if (item.Tag.ToString() == dr0["sectcod"].ToString())
                        break;
                    ++x;
                }
                this.cmbSectCod.SelectedIndex = x;
                this.AtxtssirCod.Value = dr0["ssircode"].ToString().Trim();


                this.btnOk_Click(null, null);

                this.preparebyid1 = dr2["preparebyid"].ToString();// WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = dr2["prepareses"].ToString();// WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = dr2["preparetrm"].ToString();// WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = Convert.ToDateTime(dr2["rowtime"]);

                this.txtblEditMode.Visibility = Visibility.Visible;

                this.lblMrrNo.Content = dr0["memonum1"].ToString();
                this.lblMrrNo.Tag = dr0["memonum"].ToString();

                x = 0;
                foreach (ComboBoxItem item in this.cmborderno.Items)
                {
                    if (item.Tag.ToString() == dr0["orderno"].ToString())
                        break;
                    ++x;
                }
                this.cmborderno.SelectedIndex = x;
                this.xctk_dtchlndat.Value = Convert.ToDateTime(dr0["chlndat"].ToString());
                this.txtchlnno.Text = dr0["chlnno"].ToString();
                this.autoRecByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoRecByStaffSearch.SelectedValue = dr0["recvbyid"].ToString().Trim();
                this.txtmrrRef.Text = dr0["Referance"].ToString().Trim();
                this.txtmrrNarr.Text = dr0["Naration"].ToString().Trim();
                bool mfgInfoFound = false;
                this.ListViewItemTable1.Clear();
                foreach (DataRow item in this.EditDs.Tables[1].Rows)
                {
                    if (item["batchno"].ToString().Trim().Length > 0)
                        mfgInfoFound = (mfgInfoFound == false ? true : mfgInfoFound);

                    var item1b = new vmEntryItemRcv1.ListViewItemTable()
                    {
                        trsl = item["slnum"].ToString().Trim() + ".",
                        invcode = item["invcode"].ToString().Trim(),
                        rsircode = item["rsircode"].ToString().Trim(),
                        trdesc = item["sirdesc"].ToString().Trim(),
                        mrrqty = Convert.ToDecimal(item["mrrqty"]),
                        truid = "",
                        trunit = item["sirunit"].ToString().Trim(),
                        mrrrate = Convert.ToDecimal(item["mrrrate"]),
                        //gmrramt = Convert.ToDecimal(item["mrrqty"]) * Convert.ToDecimal(item["mrrrate"]),
                        gmrramt = Convert.ToDecimal(item["mrramt"]) - Convert.ToDecimal(item["chrgamt"]) - Convert.ToDecimal(item["discamt"]),
                        discamt = Convert.ToDecimal(item["discamt"]),
                        chrgamt = Convert.ToDecimal(item["chrgamt"]), // Charge Amount to be accomodate with database and stored  procedure -- Hafiz 02-May-2017
                        mrramt = Convert.ToDecimal(item["mrramt"]),
                        vatamt = Convert.ToDecimal(item["vatamt"]),
                        tmrramt = Convert.ToDecimal(item["mrramt"]) + Convert.ToDecimal(item["vatamt"]),
                        chlnqty = Convert.ToDecimal(item["chlnqty"]),
                        batchno = item["batchno"].ToString().Trim(),
                        mfgdat = DateTime.Parse(item["mfgdat"].ToString().Trim()),
                        expdat = DateTime.Parse(item["expdat"].ToString().Trim()),
                        trdesc2 = ""
                    };
                    this.ListViewItemTable1.Add(item1b);
                }

                //select comcod, mrrno, mrrdat, carramt, labamt, othramt from #tblMRRm01e;
                this.txtCarryingAmt.Text = Convert.ToDecimal(this.EditDs.Tables[2].Rows[0]["carramt"]).ToString("#,##0.00");
                this.txtLabourAmt.Text = Convert.ToDecimal(this.EditDs.Tables[2].Rows[0]["labamt"]).ToString("#,##0.00");
                this.txtOtherChrgAmt.Text = Convert.ToDecimal(this.EditDs.Tables[2].Rows[0]["othramt"]).ToString("#,##0.00");
                this.lblVouNoTitle.Tag = this.EditDs.Tables[2].Rows[0]["vounum"].ToString();

                this.chkMgfInfo.IsChecked = (mfgInfoFound == true ? true : false);
                //-------------------------------------------
                this.AuthorizeTable1.Clear();
                this.InitializeAuthorization();
                foreach (DataRow audr in this.EditDs.Tables[3].Rows)
                {
                    var aucod1 = audr["aucode"].ToString().Trim();
                    var autitem = this.AuthorizeTable1.Find(x1 => x1.aucode == aucod1);// "CBALCOD01000");
                    if (autitem != null)
                    {
                        autitem.auhcid = audr["auhcid"].ToString().Trim();
                        autitem.auhcnam = audr["auhcnam"].ToString().Trim();
                        autitem.austat = audr["austat"].ToString().Trim();
                        autitem.statdes = (autitem.austat.Trim() == "Y" ? "Approved" : (autitem.austat.Trim() == "N" ? "Rejected" : "Pending"));   // audr["statdes"].ToString().Trim(); //"Prepared";
                        autitem.aunote = audr["aunote"].ToString().Trim();
                        autitem.notevisible = (autitem.aunote.Trim().Length == 0 ? "Collapsed" : "Visible");
                        autitem.autime = DateTime.Parse(audr["autime"].ToString().Trim());
                        autitem.luhcid = audr["luhcid"].ToString().Trim();
                        autitem.luses = audr["luses"].ToString().Trim();
                        autitem.lutrm = audr["lutrm"].ToString().Trim();
                        autitem.lutime = DateTime.Parse(audr["lutime"].ToString().Trim());
                    }
                }
                //-------------------------------------------

                this.btnCalcTotal_Click(null, null);
                this.gridDetails.Visibility = Visibility.Visible;
                this.btnUpdate.Visibility = Visibility.Visible;
                if (sender != null)
                    this.GridItemList.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-15: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnCancelMrr_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to cancel", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var item1a = (HmsEntityInventory.InvTransectionList)this.dgvTransList.SelectedItem;

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this Store Receive " + item1a.memonum1, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;

                int itemno1 = this.dgvTransList.SelectedIndex;
                var pap1 = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum, "CANCEL", "MESSAGE");

                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                //== Delete Accounts Voucher ======================================
                if (this.chkAccVoucher.IsChecked == true)
                {
                    var pap1a = vm1.SetParamEditMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum);
                    DataSet ds1a = WpfProcessAccess.GetHmsDataSet(pap1a);
                    if (ds1a == null)
                        return;

                    string OldVouNum1 = ds1a.Tables[2].Rows[0]["vounum"].ToString();// this.lblVouNoTitle.Tag.ToString().Trim();
                    if (OldVouNum1.Length > 0 && OldVouNum1 != "000000000000000000")
                    {
                        var pap1b = vm1acc.SetParamCancelVoucher(WpfProcessAccess.CompInfList[0].comcod, OldVouNum1);
                        DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                        if (ds1b == null)
                            return;
                    }
                }
                //=======================================

                this.ListViewTransTable1[itemno1].MemoStatus = "C";
                this.dgvTransList.Items.Refresh();
                //AccTrnLst
                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnCopyMrr_Click(object sender, RoutedEventArgs e)
        {
            // This option is under construction. To be develop soon --- Hafiz 04-Apr-2017
            this.btnEditMrr_Click(null, null);
            this.txtblEditMode.Visibility = Visibility.Hidden;
            this.xctk_dtpMrrDat.Minimum = DateTime.Today.AddDays(-365 * 3);
            this.xctk_dtpMrrDat.Maximum = DateTime.Today.AddDays(365 * 2);

            this.xctk_dtpMrrDat.Value = DateTime.Today;
            this.xctk_dtchlndat.Value = DateTime.Today;
            this.lblMrrNo.Content = "MRRMM-CCCC-XXXXX";
            this.lblMrrNo.Tag = "MRRYYYYMMCCCCXXXXX";
            this.txtbatchno.Text = "";
            this.xctk_dtMfg.Value = DateTime.Today;
            this.xctk_dtExp.Value = DateTime.Today.AddDays(365 * 2);
            this.lblVouNo.Content = "XVXMM-CCCC-XXXXX";
            this.lblVouNo.Tag = "XVXYYYYMMCCCCXXXXX";
            this.btnOrderList.IsEnabled = false;
            this.cmborderno.IsEnabled = false;

        }

        private void ResetUnitRateInfo(string ItemId1)
        {
            try
            {
                var item1 = WpfProcessAccess.InvItemList.Find(x => x.sircode == ItemId1);
                this.cmbQtyUnit.Items.Clear();
                this.cmbRateUnit.Items.Clear();
                this.cmbRateUnit.IsEnabled = true;
                this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1.sirunit.Trim(), Tag = "1.00" });
                this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1.sirunit.Trim(), Tag = item1.sirunit.Trim() });

                if (item1.sirunit.Trim() != item1.sirunit2.Trim() && item1.sirunit2.Trim().Length > 0 && item1.siruconf > 0)
                {
                    this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1.sirunit2.Trim(), Tag = item1.siruconf.ToString() });
                    this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1.sirunit2.Trim(), Tag = item1.sirunit2.Trim() });
                }

                if (item1.sirunit.Trim() != item1.sirunit3.Trim() && item1.sirunit3.Trim().Length > 0 && item1.siruconf3 > 0)
                {
                    this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1.sirunit3.Trim(), Tag = item1.siruconf3.ToString() });
                    this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1.sirunit3.Trim(), Tag = item1.sirunit3.Trim() });
                }

                this.cmbQtyUnit.SelectedIndex = 0;
                this.lblUnit1.Content = item1.sirunit.Trim();
                this.cmbRateUnit.SelectedIndex = 0;
                this.cmbRateUnit.IsEnabled = (this.cmbRateUnit.Items.Count > 1 ? true : false);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-31: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnUpdateVoucher_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // new AccVoucherType ( "Cash Payment Voucher", "PVC82" ),
                //new AccVoucherType ( "A/c Payable Journal Voucher", "JVP92" ),

                string CrAcCode = ((ComboBoxItem)this.cmbCrAccHead.SelectedValue).Tag.ToString();
                string VouType1 = (CrAcCode.Substring(0, 2) == "23" || CrAcCode.Substring(0, 2) == "81" ? "JVP92" : "PVC82");  // ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim()

                //--------------------

                string cactcod1 = (VouType1 == "PVC82" ? CrAcCode : "000000000000");// this.AtxtCactCode.Value.Trim();
                string cheqbookid1 = "XXXXXXXXXXXXXXXXXX";//  "190200030001151201";
                string cheqno1 = "";

                //string vounum1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim().Substring(0, 3) +
                //             DateTime.Parse(this.xctk_dtpVouDat.Text).ToString("yyyyMM") +
                //             ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Tag.ToString().Trim().Substring(0, 4);

                string vounum1 = VouType1.Substring(0, 3) +
                            DateTime.Parse(this.xctk_dtpMrrDat.Text).ToString("yyyyMM") +
                            ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim().Substring(0, 4);


                string EditVounum1 = "";
                string Challan1 = this.txtchlnno.Text.Trim();
                Challan1 = (Challan1.Length == 0 ? "" : "Challan: " + Challan1 + ", Dated: " + this.xctk_dtchlndat.Text.Trim());
                //Challan1 = "MRR # " + this.lblMrrNo.Content.ToString() + " " + Challan1;
                var vouPrInfo1 = new vmEntryVoucher1.VouPrInfo()
                {
                    vounum = (EditVounum1.Length > 0 ? EditVounum1 : vounum1),
                    voudat = DateTime.Parse(this.xctk_dtpMrrDat.Text),
                    vouref = Challan1,
                    cheqbookid = cheqbookid1,
                    chqref = cheqno1, //((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim(),
                    advref = this.txtmrrRef.Text.Trim(),
                    vounar = this.txtmrrNarr.Text.Trim(),
                    curcod = "CBCICOD01001",
                    curcnv = 1.00m,
                    vstatus = "A",
                    recndt = DateTime.Parse("01-Jan-1900"),
                    vtcode = VouType1.Substring(3, 2),
                };

                var ListVouTable1 = new List<vmEntryVoucher1.VouTable>();
                ListVouTable1.Add(new vmEntryVoucher1.VouTable()
                {
                    trnsl = 0,
                    DrCrOrder = "01",
                    cactcode = cactcod1,
                    sectcod = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim(),
                    actcode = ((ComboBoxItem)this.cmbDrAccHead.SelectedItem).Tag.ToString().Trim(),
                    sircode = "000000000000", //this.AtxtssirCod.Value,
                    reptsl = "000",
                    sircode2 = "000000000000",
                    cactcodeDesc = "",
                    sectcodDesc = "",
                    actcodeDesc = "",
                    sircodeDesc = "",
                    sircode2Desc = "",
                    trnDesc = "",
                    trnqty = 0.00m,
                    trnUnit = "",
                    trnrate = 0.00m,
                    dramt = decimal.Parse("0" + this.lblDrAmount.Content.ToString().Replace(",", "")),
                    cramt = 0.00m,
                    trnam = decimal.Parse("0" + this.lblDrAmount.Content.ToString().Replace(",", "")),
                    trnrmrk = ""
                });
                if (cactcod1 == "000000000000")
                {
                    ListVouTable1.Add(new vmEntryVoucher1.VouTable()
                    {
                        trnsl = 0,
                        DrCrOrder = "01",
                        cactcode = cactcod1,
                        sectcod = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim(),
                        actcode = ((ComboBoxItem)this.cmbCrAccHead.SelectedItem).Tag.ToString().Trim(),
                        sircode = this.AtxtssirCod.Value,
                        reptsl = "000",
                        sircode2 = "000000000000",
                        cactcodeDesc = "",
                        sectcodDesc = "",
                        actcodeDesc = "",
                        sircodeDesc = "",
                        sircode2Desc = "",
                        trnDesc = "",
                        trnqty = 0.00m,
                        trnUnit = "",
                        trnrate = 0.00m,
                        dramt = 0.00m,
                        cramt = decimal.Parse("0" + this.lblCrAmount.Content.ToString().Replace(",", "")),
                        trnam = decimal.Parse("0" + this.lblCrAmount.Content.ToString().Replace(",", "")) * -1.00m,
                        trnrmrk = ""
                    });
                }

                var ListVouTable1u = ListVouTable1.FindAll(x => x.actcode != "000000000000");
                DataSet ds1 = vm1acc.GetDataSetForUpdate(WpfProcessAccess.CompInfList[0].comcod, vouPrInfo1, ListVouTable1u,
                    _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);
                var pap1 = vm1acc.SetParamUpdateVoucher(WpfProcessAccess.CompInfList[0].comcod, ds1, EditVounum1);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                this.lblVouNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                this.lblVouNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();

                this.btnUpdateVoucher.IsEnabled = false;
                this.cmbDrAccHead.IsEnabled = false;
                this.cmbCrAccHead.IsEnabled = false;
                this.btnPrint2Voucher.Visibility = Visibility.Visible;
                this.cmbPrnForm2.Visibility = Visibility.Visible;

                //--------------------
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-17: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnPrint2Voucher_Click(object sender, RoutedEventArgs e)
        {
            string memoNum = this.lblVouNo.Tag.ToString().Trim();
            string frmname = ((ComboBoxItem)this.cmbPrnForm2.SelectedItem).Tag.ToString().Trim();
            string PaperType1 = (((ComboBoxItem)this.cmbPrnForm2.SelectedItem).Content.ToString().Trim() == "Voucher-2" ? "h" : "");

            //string frmname = "VOUCHER";

            this.PrintVoucherMemo(memoNum, true, frmname, PaperType1);
        }
        private void PrintVoucherMemo(string memoNum, bool DirectPrint = false, string prnFrom = "VOUCHER", string PaperType = "")
        {
            try
            {
                LocalReport rpt1 = null;
                string WindowTitle1 = "";
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

                var list4 = new HmsEntityAccounting.AccVoucher1p();
                list4.comlogo = comlogoBytes;
                list4.inWord = ASITFunLib.ASITUtility.Trans(double.Parse(list1.Sum(q => q.cramt).ToString()), 2);
                //l.inWord = ASITFunLib.ASITUtility2.UppercaseWords("");
                string rptName = (prnFrom == "VOUCHER" ? "Accounting.RptAccVou1" + PaperType : (prnFrom == "CHEQUE" ? "Accounting.RptAccPayCheq1" : (prnFrom == "MRECEIPT" ? "Accounting.RptAccMReceipt1" : "")));
                // (list1.Count > 7 ? "Accounting.RptAccVou1" : "Accounting.RptAccVou1h");
                rpt1 = AccReportSetup.GetLocalReport(rptName, list1, trnsList, list3, list4);
                //rpt1.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(bytes)));
                WindowTitle1 = (prnFrom == "VOUCHER" ? "Accounts Voucher" : (prnFrom == "CHEQUE" ? "Payment/Transfer Cheque" : (prnFrom == "MRECEIPT" ? "Money Receipt" : "")));
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-18: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnSetDispPer_Click(object sender, RoutedEventArgs e)
        {
            decimal discper1 = decimal.Parse("0" + this.iudDisPer.Value.ToString());
            foreach (var item in this.ListViewItemTable1)
            {
                item.gmrramt = item.mrrrate * item.mrrqty;
                item.discamt = Math.Round(item.gmrramt * discper1 / 100.00m, 0);
            }
            this.btnCalcTotal_Click(null, null);
        }

        private void btnSetVATPer_Click(object sender, RoutedEventArgs e)
        {
            decimal vatper1 = decimal.Parse("0" + this.iudVATPer.Value.ToString());
            foreach (var item in this.ListViewItemTable1)
            {
                item.gmrramt = item.mrrrate * item.mrrqty;
                item.vatamt = Math.Round(item.gmrramt * vatper1 / 100.00m, 0);
            }
            this.btnCalcTotal_Click(null, null);
        }

        private void btnSetCharges_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.ListViewItemTable1)
            {
                item.gmrramt = item.mrrrate * item.mrrqty;
                item.mrramt = item.gmrramt - item.discamt;
            }

            decimal tmrramt = this.ListViewItemTable1.Sum(x => x.mrramt);
            decimal tchrgamt = decimal.Parse("0" + this.txtCarryingAmt.Text.Trim()) + decimal.Parse("0" + this.txtLabourAmt.Text.Trim()) + decimal.Parse("0" + this.txtOtherChrgAmt.Text.Trim());
            foreach (var item in this.ListViewItemTable1)
            {
                item.chrgamt = item.mrramt / tmrramt * tchrgamt;
                //item.mrramt = item.gmrramt - item.discamt;
                //item.tmrramt = item.mrramt + item.chrgamt + item.vatamt; // Final Calculation 
            }

            this.btnCalcTotal_Click(null, null);
        }

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
                case "TXTMRRQTY":
                    this.txtmrrQty.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                    this.txtmrrQty.Focus();
                    break;
                case "TXTRQRATE":
                    this.txtrqRate.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                    this.txtrqRate.Focus();
                    break;
                case "TXTAMOUNT":
                    this.txtAmount.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                    this.txtAmount.Focus();
                    break;
            }

            this.txtExprToCalc.Text = "";
            this.gridCalc1.Visibility = Visibility.Collapsed;
        }

        private void chkAccVoucher_Click(object sender, RoutedEventArgs e)
        {
            this.stkpAccVoucher.Visibility = (this.chkAccVoucher.IsChecked == true ? Visibility.Visible : Visibility.Hidden);
        }

        private void dgvlblSlNum_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string Itemid1 = ((Label)sender).Tag.ToString();
            string InvCode1 = ((Label)sender).Uid.ToString();

            this.ResetUnitRateInfo(Itemid1);

            var item1 = this.ListViewItemTable1.FindAll(x => x.rsircode == Itemid1 && x.invcode == InvCode1);
            this.lblSlNo.Content = InvCode1;

            this.txtmrrQty.Text = item1[0].mrrqty.ToString("#,##0.00");
            this.txtrqRate.Text = item1[0].mrrrate.ToString("#,##0.00");
            this.txtAmount.Text = item1[0].mrramt.ToString("#,##0.00");
            this.txtchlnqty.Text = item1[0].chlnqty.ToString("#,##0.00");
            this.txtDiscount.Text = item1[0].discamt.ToString("#,##0.00");
            this.txtrqVatAmt.Text = item1[0].vatamt.ToString("#,##0.00");
            this.lblNetAmtShow.Content = (item1[0].mrramt - item1[0].discamt + item1[0].vatamt).ToString("#,##0.00");
        }

        private void dgvLblItemDesc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string Itemid1 = ((Label)sender).Tag.ToString();
            string InvCode1 = ((Label)sender).Uid.ToString();

            this.ResetUnitRateInfo(Itemid1);

            var item1 = this.ListViewItemTable1.FindAll(x => x.rsircode == Itemid1 && x.invcode == InvCode1);

            this.lblSlNo.Content = "xxxxxxxxxxxx";

            this.txtmrrQty.Text = "";
            this.txtrqRate.Text = "";
            this.txtAmount.Text = "";
            this.txtchlnqty.Text = "";
            this.txtDiscount.Text = "";
            this.txtrqVatAmt.Text = "";
            this.lblNetAmtShow.Content = "";
        }

        private void cmbQtyUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbQtyUnit.SelectedItem == null)
                return;

            var sunit1 = ((ComboBoxItem)this.cmbQtyUnit.SelectedItem).Content.ToString();
            this.lblUnit2.Content = sunit1;
        }

        private void cmbRateUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbRateUnit.Items.Count == 0)
                return;
        }

        private void lblssirCodTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HmsDialogWindow1 window1 = new HmsDialogWindow1(new General.frmSirCodeBook1(MainGroup: "9[89]"));
            window1.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window1.Title = "SUPPLIR CODE BOOK ADD/EDIT SCREEN";
            //window1.Owner = Application.Current.MainWindow;
            window1.ShowDialog();
            WpfProcessAccess.GetSupplierContractorList();
        }

        private void lblRSirCodeTitlepr_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HmsDialogWindow1 window1 = new HmsDialogWindow1(new General.frmSirCodeBook1(MainGroup: "[02]"));
            window1.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window1.Title = "ITEM CODE BOOK ADD/EDIT SCREEN";
            //window1.Owner = Application.Current.MainWindow;
            window1.ShowDialog();
            WpfProcessAccess.GetInventoryItemList();
        }

        private void autoRecByStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStaffRefSirdesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetStaffRefSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
               WpfProcessAccess.StaffList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoAutByStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStaffRefSirdesc(args.Pattern);
        }
        private void dgvAuthorize_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    this.dgvAuthorize.CommitEdit(DataGridEditingUnit.Cell, false);
                    this.dgvAuthorize.CommitEdit(DataGridEditingUnit.Row, false);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void btnOkAuthorize_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoAutByStaffSearch.SelectedValue == null)
                return;

            string usrid1 = this.autoAutByStaffSearch.SelectedValue.ToString();
            int cntUser1 = this.AuthorizeTable1.Count(x => x.auhcid == usrid1);
            string AutCod = this.TxtbCurAuHeader.Tag.ToString();
            if (AutCod == "CBALCOD01010")
            {
                if (cntUser1 > 0)
                {
                    System.Windows.MessageBox.Show("Duplicate Authorization User Not Allowed.\nPlease try with valid user.", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                                MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
            var aurVal = (ComboBoxItem)this.cmbCurAut.SelectedItem;
            var autitem = this.AuthorizeTable1.Find(x => x.aucode == AutCod);
            autitem.auhcid = this.autoAutByStaffSearch.SelectedValue.ToString();
            autitem.auhcnam = this.autoAutByStaffSearch.SelectedText.Trim();
            autitem.austat = aurVal.Tag.ToString().Trim();
            autitem.statdes = aurVal.Content.ToString().Trim();
            autitem.aunote = this.txtCurAutNote.Text.Trim();
            autitem.notevisible = (this.txtCurAutNote.Text.Trim().Length == 0 ? "Collapsed" : "Visible");
            autitem.autime = DateTime.Now;
            autitem.luhcid = WpfProcessAccess.SignedInUserList[0].hccode;
            autitem.luses = WpfProcessAccess.SignedInUserList[0].sessionID;
            autitem.lutrm = WpfProcessAccess.SignedInUserList[0].terminalID;
            autitem.lutime = DateTime.Now;
            this.btnUpdate_Click(null, null);
            //if (System.Windows.MessageBox.Show("Confirm Authorization", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
            // MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            //{
            //    return;
            //}
            //this.UpdateMemoAuthorization();
            this.DispatcherFrame1.Continue = false; // un-blocks gui message pump
        }

        private void btnCancelAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.DispatcherFrame1.Continue = false; // un-blocks gui message pump
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

        private void dgvTransList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnPrint3_Click(null, null);
        }

        private void dgvTransList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.btnPrint3_Click(null, null);
        }

        private void btnEditAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.btnEditMrr_Click(null, null);
            if (this.EditDs == null)
                return;

            this.btnUpdate.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;
            this.chkAuthorize.IsChecked = true;
            this.chkAuthorize_Click(null, null);
        }

        private void chkAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
            if (this.chkAuthorize.IsChecked == true)
                this.ShowHideAuthorizationPanle();
        }
        private void ShowHideAuthorizationPanle()
        {
            this.InitializeAuthorization();
            this.lblAuthorizeMemoDesc.Content = "Date: " + this.xctk_dtpMrrDat.Text.Trim() + ", M.R.R. No: " + this.lblMrrNo.Content.ToString();
            this.lblAuthorizeMemoDesc.Tag = this.lblMrrNo.Tag.ToString();

            var list1 = this.AuthorizeTable1.FindAll(x => x.austat != "U").OrderBy(y => y.aucode).ToList();
            int cntReject = this.AuthorizeTable1.Count(x => x.austat == "N");
            string usrid1 = WpfProcessAccess.SignedInUserList[0].hccode;
            int cntUser1 = 0; // this.AuthorizeTable1.Count(x => x.hccode == usrid1); // A this query if user based autherization
            var AprvCod1 = "CBALCOD01000";
            int AprvSl1 = 1;
            if (list1.Count > 0)
            {
                AprvCod1 = list1[list1.Count - 1].aucode;
                AprvSl1 = int.Parse(list1[list1.Count - 1].slnum.Replace(".", "").Trim());
            }

            this.StkpCurAut.Visibility = (AprvCod1 == "CBALCOD01010" || cntReject > 0 || cntUser1 > 0 ? Visibility.Collapsed : Visibility.Visible);
            this.txtbCurAuTime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            this.dgvAuthorize.ItemsSource = list1;

            if (this.StkpCurAut.Visibility == Visibility.Visible)
            {
                //string NextCode = this.AuthorizeTable1[AprvSl1].aucode;// "CBALCOD01" + (int.Parse(AprvCod1.Substring(9, 3)) + 1).ToString("000");
                var item21 = this.AuthorizeTable1[AprvSl1];// .Find(x => x.aucode == NextCode);
                this.TxtbCurAuHeader.Text = item21.slnum + item21.autitle.Trim() + ":";
                this.TxtbCurAuHeader.Tag = item21.aucode;// NextCode;
                this.autoAutByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoAutByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                this.txtCurAutNote.Text = "";
                this.autoAutByStaffSearch.Focus();
            }

            this.DispatcherFrame1 = new DispatcherFrame();
            this.GridDataEntry.IsEnabled = false;
            this.gridAuthorize.Visibility = Visibility.Visible;
            this.gridAuthorize.IsEnabled = true;

            if (this.dgvAuthorize.Items.Count > 0)
            {
                var item22 = (HmsEntityGeneral.AuthorizeInf)this.dgvAuthorize.Items[this.dgvAuthorize.Items.Count - 1];
                this.dgvAuthorize.ScrollIntoView(item22);
                this.dgvAuthorize.SelectedItem = item22;
                if (this.StkpCurAut.Visibility != Visibility.Visible)
                    this.dgvAuthorize.Focus();
            }

            System.Windows.Threading.Dispatcher.PushFrame(this.DispatcherFrame1);
            this.GridDataEntry.IsEnabled = true;
            this.gridAuthorize.IsEnabled = false;
            this.chkAuthorize.IsChecked = false;
            this.gridAuthorize.Visibility = Visibility.Collapsed;
        }
        private void GetAuthorizeList()
        {
            if (WpfProcessAccess.GenInfoTitleList == null)
                WpfProcessAccess.GetGenInfoTitleList();


            var lst1 = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Contains("CBALCOD010") && !x.acttdesc.Contains("XXX")
                        && int.Parse(x.actcode.Substring(9, 3)) >= 0 && int.Parse(x.actcode.Substring(9, 3)) < 11).OrderBy(y => y.actelev.Trim()).ToList();
            this.AuthorizeTable1.Clear();

            int sl1 = 1;
            foreach (var item in lst1)
            {
                var aui1 = new HmsEntityGeneral.AuthorizeInf()
                {
                    slnum = sl1.ToString() + ".",
                    comcod = WpfProcessAccess.CompInfList[0].comcod,
                    aucode = item.actcode,
                    aulevel = item.actelev,
                    autitle = item.acttype,
                    aunote = "",
                    notevisible = "Collapsed",// : "Visible"),
                    austat = "U",
                    statdes = "Pending",
                    autime = DateTime.Now,
                    auhcid = "000000000000",
                    auhcnam = "",
                    memonum = "xxxxxx",
                    luhcid = this.preparebyid1,
                    luses = this.prepareses1,
                    lutrm = this.preparetrm1,
                    lutime = DateTime.Now,
                };
                this.AuthorizeTable1.Add(aui1);
                sl1++;
            }
        }

        private void InitializeAuthorization()
        {
            if (this.AuthorizeTable1.Count == 0)
                this.GetAuthorizeList();

            string memonum1 = this.lblMrrNo.Tag.ToString();
            if (this.AuthorizeTable1.FindAll(x => x.memonum == memonum1).Count == 0 || memonum1.Contains("XXXXX"))
            {

                foreach (var item in this.AuthorizeTable1)
                {
                    item.austat = "U";
                    item.statdes = "Pending";
                    item.autime = DateTime.Now;
                    item.aunote = "";
                    item.notevisible = "Collapsed";
                    item.auhcid = "000000000000";
                    item.auhcnam = "";
                    item.memonum = memonum1;
                    item.luhcid = "000000000000";
                    item.luses = "";
                    item.lutrm = "";
                    item.lutime = DateTime.Now;
                }
            }

            var autitem = this.AuthorizeTable1.Find(x => x.aucode == "CBALCOD01000");
            autitem.auhcid = this.autoRecByStaffSearch.SelectedValue.ToString();
            autitem.auhcnam = this.autoRecByStaffSearch.SelectedText.Trim();
            autitem.austat = "Y";
            autitem.statdes = "Prepared";
            autitem.aunote = this.txtmrrNarr.Text.Trim();
            autitem.autime = this.rowtime1; //DateTime.Parse(this.xctk_dtpSrfDat.Text.Trim());
            autitem.luhcid = this.preparebyid1;
            autitem.luses = this.prepareses1;
            autitem.lutrm = this.preparetrm1;
            autitem.lutime = this.rowtime1;
        }

        private void SelectItemInfo()
        {

            //List<HmsEntityGeneral.SirInfCodeBook> InvItemList

            var lbi1 = (HmsEntityGeneral.SirInfCodeBook)this.lstItem.SelectedItem;

            if (lbi1 == null)
                return;
            this.txtRSirCode.Text = lbi1.sircode;
            this.txtRSirDesc.Tag = lbi1.sircode;
            this.txtRSirDesc.Text = lbi1.sirdesc.Trim();
            //this.lblUnit1.Content = lbi1.sirunit.Trim();

            string sircod1 = lbi1.sircode;
            this.ResetUnitRateInfo(sircod1);


        }
        private void lstItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectItemInfo();
            this.txtmrrQty.Focus();
        }

        private void lstItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectItemInfo();
                this.txtmrrQty.Focus();
                //this.txtRSirDesc.Focus();
                //this.btnAddItem_Click(null, null);
            }
        }

        private void txtRSirDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Visible;
        }

        private void txtRSirDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lstItem.ItemsSource = null;
            string StrDesc1 = this.txtRSirDesc.Text.Trim().ToUpper();
            if (StrDesc1.Length == 0)
                return;
            var List1a = WpfProcessAccess.InvItemList.FindAll(x => x.sirdesc.ToUpper().Contains(StrDesc1));
            this.lstItem.ItemsSource = List1a;
        }

        private void txtmrrQty_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }

        private void chkAddInfo_Click(object sender, RoutedEventArgs e)
        {
            this.stkpAddInfo.Visibility = (this.chkAddInfo.IsChecked == true ? Visibility.Visible : Visibility.Hidden);
        }

        private void btnCloseDraftList_Click(object sender, RoutedEventArgs e)
        {
            this.DispatcherFrame1.Continue = false; // un-blocks gui message pump
        }

        private void chkShowDraft_Click(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
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
            this.xctk_dtpDraftFrom.Value = DateTime.Parse(this.xctk_dtpMrrDat.Text).AddDays(-7);
            this.xctk_dtpDraftTo.Value = this.xctk_dtpMrrDat.Value;
            this.txtDraftTrmId.Text = this.preparetrm1;

            this.btnFilterDraftList_Click(null, null);
            System.Windows.Threading.Dispatcher.PushFrame(this.DispatcherFrame1);
            this.GridDataEntry.IsEnabled = true;
            this.GridDraftList.IsEnabled = false;
            this.chkShowDraft.IsChecked = false;
            this.GridDraftList.Visibility = Visibility.Collapsed;
        }
        private void btnFilterDraftList_Click(object sender, RoutedEventArgs e)
        {
            this.dgvDraftList.ItemsSource = null;
            string draftDesc1a = this.txtDraftDesc.Text.Trim();
            string dateFrom = this.xctk_dtpDraftFrom.Text;
            string dateTo = this.xctk_dtpDraftTo.Text;
            string drafttrm1a = this.txtDraftTrmId.Text.Trim();
            string signinnam1a = this.txtDraftUserId.Text.Trim();

            this.DraftTransactionList1 = WpfProcessAccess.GetDraftTransactionList(memohead1: "MRR", draftDesc1: draftDesc1a, signinnam1: signinnam1a, drafttrm1: drafttrm1a, 
                DateFrom1: dateFrom, DateTo1: dateTo).ToList();
            this.dgvDraftList.ItemsSource = this.DraftTransactionList1;
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

        private void dgvDraftList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.DraftGridNavigationClick(ActtionName: "btnDraftRetrive");
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
                foreach (ComboBoxItem item1 in this.cmbSectCod.Items)
                {
                    if (item1.Tag.ToString().Trim() == drb["sectcod"].ToString().Trim())
                    {
                        this.cmbSectCod.SelectedItem = item1;
                        break;
                    }
                }
                this.AtxtssirCod.Value = drb["ssircode"].ToString().Trim();
                this.AtxtssirCod_LostFocus(null, null);
                foreach (ComboBoxItem item2 in this.cmborderno.Items)
                {
                    if (item2.Tag.ToString().Trim() == drb["orderno"].ToString().Trim())
                    {
                        this.cmborderno.SelectedItem = item2;
                        break;
                    }
                }
                this.xctk_dtchlndat.Value = DateTime.Parse(drb["chlndat"].ToString());
                this.txtchlnno.Text = drb["chlnno"].ToString();
                if (drb["recvbyid"].ToString().Trim() == "000000000000")
                {
                    this.autoRecByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoRecByStaffSearch.SelectedValue = drb["recvbyid"].ToString().Trim();
                }
                this.txtmrrRef.Text = drb["mrrref"].ToString();
                this.txtmrrNarr.Text = drb["mrrnar"].ToString();
                this.btnOk_Click(null, null);

                this.ListViewItemTable1.Clear();
                foreach (DataRow dr1 in dsd2.Tables[1].Rows)
                {
                    var item1a = new vmEntryItemRcv1.ListViewItemTable()
                    {
                        trsl = dr1["trsl"].ToString().Trim(),
                        invcode = dr1["invcode"].ToString().Trim(),
                        rsircode = dr1["rsircode"].ToString().Trim(),
                        trdesc = dr1["trdesc"].ToString().Trim(),
                        mrrqty = decimal.Parse("0" + dr1["mrrqty"]),
                        truid = dr1["truid"].ToString().Trim(),
                        trunit = dr1["trunit"].ToString().Trim(),
                        mrrrate = decimal.Parse("0" + dr1["mrrrate"]),
                        gmrramt = decimal.Parse("0" + dr1["gmrramt"]),
                        discamt = decimal.Parse("0" + dr1["discamt"]),
                        chrgamt = decimal.Parse("0" + dr1["chrgamt"]),
                        mrramt = decimal.Parse("0" + dr1["mrramt"]),
                        vatamt = decimal.Parse("0" + dr1["vatamt"]),
                        tmrramt = decimal.Parse("0" + dr1["tmrramt"]),
                        chlnqty = decimal.Parse("0" + dr1["chlnqty"]),
                        batchno = dr1["batchno"].ToString().Trim(),
                        mfgdat = DateTime.Parse(dr1["mfgdat1"].ToString().Trim()),
                        expdat = DateTime.Parse(dr1["expdat1"].ToString().Trim()),
                        mfgdat1 = dr1["mfgdat1"].ToString().Trim(),
                        expdat1 = dr1["expdat1"].ToString().Trim(),
                        trdesc2 = dr1["trdesc2"].ToString().Trim(),
                        tooltipqty = dr1["tooltipqty"].ToString().Trim(),
                        tooltiprate = dr1["tooltiprate"].ToString().Trim()
                    };

                    this.ListViewItemTable1.Add(item1a);
                }

                this.txtCarryingAmt.Text = decimal.Parse("0" + drb["carramt"].ToString()).ToString("#,##0.00");
                this.txtLabourAmt.Text = decimal.Parse("0" + drb["labamt"].ToString()).ToString("#,##0.00");
                this.txtOtherChrgAmt.Text = decimal.Parse("0" + drb["othramt"].ToString()).ToString("#,##0.00");
                this.lblVouNoTitle.Tag = drb["vounum"].ToString();

                this.preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = DateTime.Now;

                this.DraftMemoNum = MemoNum1a;
                this.DraftMemoRowID = rowid1a;

                this.DispatcherFrame1.Continue = false; // un-blocks gui message pump
                this.btnCalcTotal_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MRR-28: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }

}
