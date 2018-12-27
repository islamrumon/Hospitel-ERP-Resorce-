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
using ASITHmsViewMan.Inventory;
using ASITHmsRpt2Inventory;
using Microsoft.Reporting.WinForms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ASITHmsWpf.Inventory
{

    /// <summary>
    /// Interaction logic for frmEntryPurReq1.xaml
    /// </summary>
    /// 
    public partial class frmEntryPurReq1 : UserControl
    {
        private bool FrmInitialized = false;
        private List<vmEntryPurReq1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPurReq1.ListViewItemTable>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private List<HmsEntityGeneral.AuthorizeInf> AuthorizeTable1 = new List<HmsEntityGeneral.AuthorizeInf>();

        private vmEntryPurReq1 vm1 = new vmEntryPurReq1();
        private vmReportStore1 vm1r = new vmReportStore1();
        public int serialno = 0;
        private DataSet EditDs;
        public bool IsActiveTransListWindow { get; set; }
        private string CalcObjName = "NoName";

        private string preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
        private string prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
        private string preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
        private DateTime rowtime1 = DateTime.Now;

        private DispatcherFrame frameAuthorise;
        public frmEntryPurReq1()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;
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
            try
            {
                if (!this.FrmInitialized)
                {
                    this.FrmInitialized = true;
                    this.IsActiveTransListWindow = false;
                    this.ConstructAutoCompletionSource();
                    this.gridCalc1.Visibility = Visibility.Collapsed;
                    this.gridAuthorize.Visibility = Visibility.Collapsed;
                    this.lstItem.Items.Clear();
                    this.chkAutoTransListpr.IsChecked = this.IsActiveTransListWindow;
                    this.btnPrint2pr.Visibility = Visibility.Hidden;
                    this.btnUpdatepr.Visibility = Visibility.Hidden;
                    this.gridDetailspr.Visibility = Visibility.Collapsed;
                    this.GridItemList.Visibility = Visibility.Collapsed;
                    this.xctk_dtpreqDatpr.Value = DateTime.Today;
                    this.xctk_dtpFromDatepr.Value = DateTime.Today.AddDays(-15);
                    this.xctk_dtpToDatepr.Value = DateTime.Today;
                    this.autoReqByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoReqByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;

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

                    this.ActivateAuthObjects();
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void ActivateAuthObjects()
        {

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPurReq1_chkDateBlocked") == null)
            {
                this.chkDateBlocked.Visibility = Visibility.Collapsed;
                this.lblDateBlocked.Visibility = Visibility.Visible;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPurReq1_chkAutoTransListpr") == null)
                this.chkAutoTransListpr.Visibility = Visibility.Hidden;

            this.btnRecurring.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPurReq1_btnEditMemo") == null)
                this.btnEditMemo.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPurReq1_btnCancelMemo") == null)
                this.btnCancelMemo.Visibility = Visibility.Hidden;
        }

        private void ConstructAutoCompletionSource()
        {

            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");

            foreach (var itemd1 in deptList1)
            {
                //this.cmbSectCodpr.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                if (itemd1.sectname.ToUpper().Contains("STORE") || itemd1.sectname.ToUpper().Contains("PROJECT"))
                    this.cmbSectCodpr.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });

            }

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();
        }

        private void btnOkpr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UnCheckedAllPopups();
                this.chkShowPrevRate.IsChecked = false;
                this.btnPrint2pr.Visibility = Visibility.Hidden;
                this.btnUpdatepr.Visibility = Visibility.Hidden;
                this.txtblEditMode.Visibility = Visibility.Hidden;
                this.gridDetailspr.Visibility = Visibility.Collapsed;
                this.gridCalc1.Visibility = Visibility.Collapsed;
                this.GridItemList.Visibility = Visibility.Collapsed;
                this.ListViewItemTable1.Clear();
                this.dgReqList.ItemsSource = null;
                this.xctk_dtpreqDatpr.IsEnabled = false;

                if (this.btnOkpr.Content.ToString() == "_New")
                {
                    this.EditDs = null;
                    this.chkDateBlocked.IsChecked = false;
                    this.chkDateBlocked.IsEnabled = true;
                    this.chkAutoTransListpr.IsEnabled = true;
                    this.cmbSectCodpr.IsEnabled = true;
                    //this.stkIntropr.IsEnabled = true;

                    this.autoReqByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoReqByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                    this.preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
                    this.prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
                    this.preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
                    this.rowtime1 = DateTime.Now;
                    this.txtreqRefpr.Text = "";
                    this.lblTotaReqAmt.Content = "";
                    this.txtreqNarpr.Text = "";
                    this.txtRSirCode.Text = "";
                    this.txtRSirDescpr.Text = "";
                    this.txtreqQtypr.Text = "";
                    this.lblAmountShow.Content = "";
                    this.txtrqRatepr.Text = "";
                    this.lblUnit1pr.Content = "";
                    this.lblreqNopr.Content = "REQMM-CCCC-XXXXX";
                    this.lblreqNopr.Tag = "REQYYYYMMCCCCXXXXX";
                    if (IsActiveTransListWindow)
                    {
                        this.BuildTransactionList();
                        this.gridTransList.Visibility = Visibility.Visible;
                        this.gridTransList.IsEnabled = true;
                        this.dgvTransList.Focus();
                    }
                    this.btnOkpr.Content = "_Ok";
                    return;

                }

                if (this.checkOkValidation() == false)
                    return;

                this.stkpAddItem.IsEnabled = true;
                this.btnCalcTotal.IsEnabled = true;
                this.btnUpdatepr.Visibility = Visibility.Visible;
                this.gridTransList.Visibility = Visibility.Collapsed;
                this.gridTransList.IsEnabled = false;
                this.gridDetailspr.Visibility = Visibility.Visible;
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = false;
                this.chkAutoTransListpr.IsEnabled = false;
                this.btnUpdatepr.IsEnabled = true;
                //this.stkIntropr.IsEnabled = false;
                this.cmbSectCodpr.IsEnabled = false;
                this.dgReqList.ItemsSource = this.ListViewItemTable1;
                this.btnOkpr.Content = "_New";
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void BuildTransactionList()
        {
            try
            {
                string FromDate = this.xctk_dtpFromDatepr.Text;
                string ToDate = this.xctk_dtpToDatepr.Text;
                string sectcod1 = "%";

                if (this.chkSelectedStore.IsChecked == true)
                    sectcod1 = ((ComboBoxItem)this.cmbSectCodpr.SelectedItem).Tag.ToString();

                //var pap1 = vm1r.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "REQ", FromDate, ToDate, "%", "%");
                var pap1 = vm1r.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "REQ", FromDate, ToDate, sectcod1, "%");
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
                System.Windows.MessageBox.Show("REQ-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private bool checkOkValidation()
        {
            try
            {
                if (this.autoReqByStaffSearch.SelectedValue == null)
                    return false;

                int length1 = this.autoReqByStaffSearch.SelectedValue.ToString().Length;
                if (length1 < 0)
                    return false;
                string reqByID2 = this.autoReqByStaffSearch.SelectedValue.ToString();

                var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == reqByID2);
                return (listStaff1.Count > 0);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-04: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }
        }

        private void btnUpdatepr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.btnCalcTotal_Click(null, null);
                if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                {
                    return;
                }
                var ListViewItemTable1a = this.ListViewItemTable1.FindAll(x => x.reqqty > 0);
                if (ListViewItemTable1a.Count == 0)
                {
                    return;
                }


                string EditReqNum1 = (this.EditDs != null ? this.lblreqNopr.Tag.ToString() : "");
                string cbSectCode1 = ((ComboBoxItem)this.cmbSectCodpr.SelectedItem).Tag.ToString();
                string reqById1a = this.autoReqByStaffSearch.SelectedValue.ToString();

                if (EditReqNum1.Length == 18)
                {
                    var pap1b = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, EditReqNum1, "BACKUP", "MESSAGE");
                    DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                    if (ds1b == null)
                        return;
                }

                this.InitializeAuthorization(); // To update the prepared by record
                DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpreqDatpr.Text), EditMemoNum1: EditReqNum1,
                            cbSectCode: cbSectCode1, reqByID1: reqById1a, MemoRef1: this.txtreqRefpr.Text.Trim(), MemoNar1: this.txtreqNarpr.Text.Trim(), ListViewItemTable1: ListViewItemTable1a,
                            AuthorizeTable1: this.AuthorizeTable1, _preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);
                //_preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

                //String xx1 = ds1.GetXml().ToString();

                var pap1 = vm1.SetParamUpdatePurReq(WpfProcessAccess.CompInfList[0].comcod, ds1, EditReqNum1);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                this.lblreqNopr.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString(); ;
                this.lblreqNopr.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();
                this.EditDs = new DataSet(); // For Current Authorization
                this.btnUpdatepr.IsEnabled = false;
                this.stkpAddItem.IsEnabled = false;
                this.btnCalcTotal.IsEnabled = false;
                this.btnPrint2pr.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-05: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void chkAutoTransListpr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.GridItemList.Visibility = Visibility.Collapsed;
                this.IsActiveTransListWindow = (this.chkAutoTransListpr.IsChecked == true);
                if (this.IsActiveTransListWindow && this.gridDetailspr.Visibility == Visibility.Collapsed)
                {
                    this.BuildTransactionList();
                    this.gridTransList.Visibility = Visibility.Visible;
                    this.gridTransList.IsEnabled = true;
                    this.dgvTransList.Focus();
                }
                else if (this.IsActiveTransListWindow == false && this.gridDetailspr.Visibility == Visibility.Collapsed)
                {
                    this.gridTransList.Visibility = Visibility.Collapsed;
                    this.gridTransList.IsEnabled = false;
                }
                this.chkPrint2pr.IsChecked = false;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-06: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
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

        private void btnAddRecordpr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.GridItemList.Visibility = Visibility.Collapsed;
                decimal reqqty1a = this.validData("0" + this.txtreqQtypr.Text.Trim());
                if (reqqty1a <= 0)
                {
                    this.txtreqQtypr.Focus();
                    return;
                }
                decimal reqRat1 = this.validData("0" + this.txtrqRatepr.Text.Trim());
                decimal reqAmt1 = Math.Round(reqqty1a * reqRat1, 6);// this.validData("0" + this.lblAmountShow.Content.ToString().Trim());
                if (reqAmt1 <= 0)
                {
                    this.txtrqRatepr.Focus();
                    return;
                }

                if (this.txtRSirDescpr.Text.Trim().Length == 0)
                {
                    this.lblUnit1pr.Content = "";
                    this.txtreqQtypr.Text = "";
                    this.lblAmountShow.Content = "";
                    this.txtrqRatepr.Text = "";
                }

                int serialno1 = this.ListViewItemTable1.Count + 1;
                string rsircode1 = this.txtRSirDescpr.Tag.ToString().Trim();
                string rsirdesc1 = this.txtRSirDescpr.Text.Trim();

                string truid1a = "";// this.txtUID.Text.Trim();
                string rsirunit = this.lblUnit1pr.Content.ToString();
                if (rsircode1.Length == 0)
                    return;

                var list1a = this.ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
                if (list1a.Count > 0)
                {
                    list1a[0].reqqty = reqqty1a;
                    list1a[0].reqrate = Math.Round(reqAmt1 / reqqty1a, 6);
                    list1a[0].reqamount = reqAmt1;
                    //System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    //return;
                }
                else
                {
                    var item1a = new vmEntryPurReq1.ListViewItemTable()
                    {
                        trsl = serialno1.ToString() + ".",
                        rsircode = rsircode1,
                        trdesc = rsirdesc1,
                        reqqty = reqqty1a,
                        truid = truid1a,
                        trunit = rsirunit,
                        reqrate = Math.Round(reqAmt1 / reqqty1a, 6),
                        reqamount = reqAmt1
                    };

                    this.ListViewItemTable1.Add(item1a);

                    //this.ListViewItemTable1.Sort(delegate(vmEntryPurReq1.ListViewItemTable x, vmEntryPurReq1.ListViewItemTable y)
                    //{
                    //    return x.rsircode.CompareTo(y.rsircode);
                    //});
                }
                this.txtRSirCode.Text = "";
                this.txtRSirDescpr.Text = "";
                this.txtRSirDescpr.Tag = "";
                this.lblUnit1pr.Content = "";
                this.txtreqQtypr.Text = "";
                this.lblAmountShow.Content = "";
                this.txtrqRatepr.Text = "";
                //this.dgReqList.Focus();

                this.btnCalcTotal_Click(null, null);

                var item22 = this.ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
                if (item22.Count > 0)
                    this.dgReqList.ScrollIntoView(item22[0]);

                //this.dgReqList.Items.Refresh();
                this.gridCalc1.Visibility = Visibility.Collapsed;
                this.lstBoxPrevRate.Items.Clear();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-07: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("Please select row first", WpfProcessAccess.AppTitle, MessageBoxButton.OK);
                    return;
                }
                LocalReport rpt1 = null;
                string WindowTitle1 = "";
                if (this.rb3SelectedMemopr.IsChecked == true)
                {
                    var item1a = (HmsEntityInventory.InvTransectionList)this.dgvTransList.SelectedItem;
                    this.PrintReqMemo(item1a.memonum);
                }
                else if (this.rb3TableRecoredspr.IsChecked == true)
                {
                    var list1 = this.ListViewTransTable1;
                    var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")));

                    rpt1 = StoreReportSetup.GetLocalReport("Store.RptTransectionList", list1, null, list3); // ( R_01_RptSetup.RptSetupItemList1(ds1, ds2);          
                    WindowTitle1 = "Purchase Requisition Transaction List";
                }
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
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-08: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void UnCheckedAllPopups()
        {

            this.chkPrint2pr.IsChecked = false;
        }




        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpreqDatpr.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpreqDatpr.IsEnabled)
                this.xctk_dtpreqDatpr.Focus();
        }

        private void btnFilterpr_Click(object sender, RoutedEventArgs e)
        {
            this.gridTransList.IsEnabled = false;
            this.BuildTransactionList();
            this.gridTransList.IsEnabled = true;
        }

        private void btnPrint2pr_Click(object sender, RoutedEventArgs e)
        {
            var MemoNum1 = this.lblreqNopr.Tag.ToString();
            this.PrintReqMemo(MemoNum1);
        }

        private void PrintReqMemo(string MemoNum1 = "XXXXXXXXXXX")
        {
            try
            {
                var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, MemoNum1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurReqMemo>();
                var list2 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();
                if (this.chkPrintWithStock.IsChecked == true)
                {
                    list2[0].MemoStatus = "STOCK";
                    string date1 = list2[0].memoDate.AddDays(-7).ToString("dd-MMM-yyyy");
                    string date2 = list2[0].memoDate.ToString("dd-MMM-yyyy");
                    string StoreID1 = list2[0].sectcod;
                    var pap2 = vm1r.SetParamInvSumReport(CompCode: WpfProcessAccess.CompInfList[0].comcod, TrTyp: "STOCK01L", FromDate: date1, ToDate: date2, DeptID1: StoreID1, ItemGrp1: "%"); // "STOCKREPORT01
                    DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
                    if (ds2 == null)
                        return;

                    var list1s = ds2.Tables[0].DataTableToList<HmsEntityInventory.InvStockList>();
                    foreach (var item in list1)
                    {
                        var item1a = list1s.FindAll(x => x.sircode == item.rsircode);
                        if (item1a.Count > 0)
                            item.stockqty = item1a[0].clsqty;
                    }
                }
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


                LocalReport rpt1 = StoreReportSetup.GetLocalReport("Store.RptPurReqMemo01", list1, list2, list3);
                if (rpt1 == null)
                    return;

                this.chkPrintWithStock.IsChecked = false;
                string WindowTitle1 = "Purchase Requisition Memo";
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void txtrqRatepr_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.lblAmountShow.Content = "";
                Double quantity = Double.Parse("0" + this.txtreqQtypr.Text.ToString().Trim());
                Double Rate = Double.Parse("0" + this.txtrqRatepr.Text.ToString().Trim());
                Double Amount = quantity * Rate;
                lblAmountShow.Content = "Amt: " + Amount.ToString("#,##0").Trim();

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-10: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }



        private void btnEditMemo_Click(object sender, RoutedEventArgs e)
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
                    System.Windows.MessageBox.Show("Requisition Memo already cancelled. Edit not possible", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var pap1 = vm1.SetParamEditMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum);
                this.EditDs = null;
                this.EditDs = WpfProcessAccess.GetHmsDataSet(pap1);
                if (this.EditDs == null)
                    return;
                DataRow dr0 = this.EditDs.Tables[0].Rows[0];
                DataRow dr2 = this.EditDs.Tables[2].Rows[0];

                this.xctk_dtpreqDatpr.Value = Convert.ToDateTime(dr0["memodate"].ToString());

                int x = 0;
                foreach (ComboBoxItem item in this.cmbSectCodpr.Items)
                {
                    if (item.Tag.ToString() == dr0["sectcod"].ToString())
                        break;
                    ++x;
                }
                this.cmbSectCodpr.SelectedIndex = x;

                this.btnOkpr_Click(null, null);

                this.preparebyid1 = dr2["preparebyid"].ToString();// WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = dr2["prepareses"].ToString();// WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = dr2["preparetrm"].ToString();// WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = Convert.ToDateTime(dr2["rowtime"]);


                this.txtblEditMode.Visibility = Visibility.Visible;

                this.lblreqNopr.Content = dr0["memonum1"].ToString();
                this.lblreqNopr.Tag = dr0["memonum"].ToString();
                this.autoReqByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoReqByStaffSearch.SelectedValue = dr0["PreparById"].ToString().Trim();

                this.txtreqRefpr.Text = dr0["Referance"].ToString().Trim();
                this.txtreqNarpr.Text = dr0["Naration"].ToString().Trim();
                this.ListViewItemTable1.Clear();
                foreach (DataRow item in this.EditDs.Tables[1].Rows)
                {
                    var item1b = new vmEntryPurReq1.ListViewItemTable()
                    {
                        trsl = item["slnum"].ToString().Trim() + ".",
                        rsircode = item["rsircode"].ToString().Trim(),
                        trdesc = item["sirdesc"].ToString().Trim(),
                        reqqty = Convert.ToDecimal(item["reqqty"]),
                        truid = "",
                        trunit = item["sirunit"].ToString().Trim(),
                        reqrate = Convert.ToDecimal(item["reqrate"]),
                        reqamount = Convert.ToDecimal(item["reqqty"]) * Convert.ToDecimal(item["reqrate"])
                    };
                    this.ListViewItemTable1.Add(item1b);
                }
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
                this.gridDetailspr.Visibility = Visibility.Visible;
                this.btnUpdatepr.Visibility = Visibility.Visible;

                //-------------------------
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void btnCancelMemo_Click(object sender, RoutedEventArgs e)
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

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this Requisition " + item1a.memonum1, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;

                int itemno1 = this.dgvTransList.SelectedIndex;
                var pap1 = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum, "CANCEL", "MESSAGE");

                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.ListViewTransTable1[itemno1].MemoStatus = "C";
                this.dgvTransList.Items.Refresh();
                //AccTrnLst
                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void SelectItemInfo()
        {

            var lbi1 = (HmsEntityGeneral.SirInfCodeBook)this.lstItem.SelectedItem;

            if (lbi1 == null)
                return;
            this.txtRSirCode.Text = lbi1.sircode;
            this.txtRSirDescpr.Tag = lbi1.sircode;
            this.txtRSirDescpr.Text = lbi1.sirdesc.Trim();
            this.lblUnit1pr.Content = lbi1.sirunit.Trim();
        }
        private void lstItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectItemInfo();
            this.txtreqQtypr.Focus();
            //this.txtRSirDescpr.Focus();
        }

        private void lstItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectItemInfo();
                this.txtreqQtypr.Focus();
                //this.txtRSirDescpr.Focus();
                //this.btnAddItem_Click(null, null);
            }
        }

        private void txtRSirDescpr_TextChanged(object sender, TextChangedEventArgs e)
        {
            ////this.lstItem.Items.Clear();
            this.lstItem.ItemsSource = null;
            string StrDesc1 = this.txtRSirDescpr.Text.Trim().ToUpper();
            if (StrDesc1.Length == 0)
                return;
            var List1a = WpfProcessAccess.InvItemList.FindAll(x => x.sirdesc.ToUpper().Contains(StrDesc1));
            this.lstItem.ItemsSource = List1a;
            ////foreach (var item in List1a)
            ////    this.lstItem.Items.Add(new ListBoxItem() { Content = item.sirdesc.Trim(), Tag = item.sircode, Uid = item.sirunit });

            //------------------------------------------------------



            ////this.lstItem.Items.Clear();
            ////string StrDesc1 = this.txtRSirDescpr.Text.Trim().ToUpper();
            ////if (StrDesc1.Length == 0)
            ////    return;
            ////var List1a = WpfProcessAccess.InvItemList.FindAll(x => x.sirdesc.ToUpper().Contains(StrDesc1));

            ////foreach (var item in List1a)
            ////    this.lstItem.Items.Add(new ListBoxItem() { Content = item.sirdesc.Trim(), Tag = item.sircode, Uid = item.sirunit });
        }

        private void txtRSirDescpr_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Visible;
        }


        private void txtreqQtypr_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }

        private void txtrqRatepr_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
            if (this.chkShowPrevRate.IsChecked == true)
                this.ShowPreviousRateInfo(this.txtRSirDescpr.Tag.ToString().Trim());
        }

        private void chkShowPrevRate_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtRSirDescpr.Tag == null)
                return;

            if (this.chkShowPrevRate.IsChecked == true)
                this.ShowPreviousRateInfo(this.txtRSirDescpr.Tag.ToString().Trim());
        }

        private void ShowPreviousRateInfo(string ItemCode1)
        {
            this.lstBoxPrevRate.Items.Clear();
            if (ItemCode1.Length == 0)
                return;
            string SectCode1 = ((ComboBoxItem)this.cmbSectCodpr.SelectedItem).Tag.ToString();
            string MemoDate1 = this.xctk_dtpreqDatpr.Text.Substring(0, 11);
            string ToDate = this.xctk_dtpToDatepr.Text;
            var pap1 = vm1r.SetParamPreviousItemRate(WpfProcessAccess.CompInfList[0].comcod, SectCode1, MemoDate1, ItemCode1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                this.lstBoxPrevRate.Items.Add(new ListBoxItem()
                {
                    Content = "Rate: " + dr1["mrrrat1"].ToString().Trim() + ", Qty: " + dr1["mrrqty1"].ToString().Trim() +
                    ", Date: " + dr1["mrrdat1"].ToString().Trim(),
                    Tag = dr1["mrrrat"].ToString().Trim()
                });
            }

        }

        private void btnCalcTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dgReqList.ItemsSource = null;
                //this.dgReqList.ItemsSource = null;
                foreach (var item in this.ListViewItemTable1)
                    item.reqamount = item.reqqty * item.reqrate;
                int count1 = this.ListViewItemTable1.Count;

                this.ListViewItemTable1 = this.ListViewItemTable1.FindAll(x => x.reqamount > 0);
                if (count1 != this.ListViewItemTable1.Count)
                {
                    int i = 1;
                    foreach (var item in this.ListViewItemTable1)
                    {
                        item.trsl = i.ToString() + ".";
                        i++;
                    }
                }
                decimal sumAmt1 = this.ListViewItemTable1.Sum(x => x.reqamount);
                this.lblTotaReqAmt.Content = sumAmt1.ToString("#,##0.00;(#,##0.00); - ");
                this.dgReqList.ItemsSource = this.ListViewItemTable1;
                this.dgReqList.Items.Refresh();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void dgvlblSlNum_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string Itemid1 = ((Label)sender).Tag.ToString();
                var item1 = this.ListViewItemTable1.FindAll(x => x.rsircode == Itemid1);
                this.txtRSirCode.Text = item1[0].rsircode;
                this.txtRSirDescpr.Text = item1[0].trdesc;
                this.txtRSirDescpr.Tag = item1[0].rsircode;
                this.txtreqQtypr.Text = item1[0].reqqty.ToString("#,##0.00");
                this.lblUnit1pr.Content = item1[0].trunit;
                this.txtrqRatepr.Text = item1[0].reqrate.ToString("#,##0.00");
                this.lblAmountShow.Content = item1[0].reqamount.ToString("#,##0.00");
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("REQ-14: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            switch (this.CalcObjName)
            {
                case "TXTREQQTYPR":
                    this.txtreqQtypr.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                    this.txtreqQtypr.Focus();
                    break;
                case "TXTRQRATEPR":
                    this.txtrqRatepr.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                    this.txtrqRatepr.Focus();
                    break;
            }

            this.txtExprToCalc.Text = "";
            this.gridCalc1.Visibility = Visibility.Collapsed;
        }

        private void txt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.CalcObjName = ((WatermarkTextBox)sender).Name.ToUpper();
            this.gridCalc1.Visibility = Visibility.Visible;
            this.txtExprToCalc.Text = "";
            this.txtExprToCalc.Focus();
        }

        private void cmbSectCodpr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.chkAutoTransListpr.IsChecked == true)
                this.btnFilterpr_Click(null, null);
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

        private void autoReqByStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
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

            string memonum1 = this.lblreqNopr.Tag.ToString();
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
            autitem.auhcid = this.autoReqByStaffSearch.SelectedValue.ToString();
            autitem.auhcnam = this.autoReqByStaffSearch.SelectedText.Trim();
            autitem.austat = "Y";
            autitem.statdes = "Prepared";
            autitem.aunote = this.txtreqNarpr.Text.Trim();
            autitem.autime = this.rowtime1; //DateTime.Parse(this.xctk_dtpSrfDat.Text.Trim());
            autitem.luhcid = this.preparebyid1;
            autitem.luses = this.prepareses1;
            autitem.lutrm = this.preparetrm1;
            autitem.lutime = this.rowtime1;
        }

        private void chkAuthorize_Click(object sender, RoutedEventArgs e)
        {
            if (this.chkAuthorize.IsChecked == true)
                this.ShowHideAuthorizationPanle();
        }

        private void ShowHideAuthorizationPanle()
        {
            this.InitializeAuthorization();
            this.lblAuthorizeMemoDesc.Content = "Date: " + this.xctk_dtpreqDatpr.Text.Trim() + ", Req. No: " + this.lblreqNopr.Content.ToString();
            this.lblAuthorizeMemoDesc.Tag = this.lblreqNopr.Tag.ToString();

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

            this.frameAuthorise = new DispatcherFrame();
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

            System.Windows.Threading.Dispatcher.PushFrame(this.frameAuthorise);
            this.GridDataEntry.IsEnabled = true;
            this.gridAuthorize.IsEnabled = false;
            this.chkAuthorize.IsChecked = false;
            this.gridAuthorize.Visibility = Visibility.Collapsed;
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

        private void btnCancelAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.frameAuthorise.Continue = false; // un-blocks gui message pump
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
            this.btnUpdatepr_Click(null, null);
            //if (System.Windows.MessageBox.Show("Confirm Authorization", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
            // MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            //{
            //    return;
            //}
            //this.UpdateMemoAuthorization();
            this.frameAuthorise.Continue = false; // un-blocks gui message pump
        }

        private void btnEditAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.btnEditMemo_Click(null, null);
            if (this.EditDs == null)
                return;

            this.btnUpdatepr.IsEnabled = false;
            this.stkpAddItem.IsEnabled = false;
            this.btnPrint2pr.Visibility = Visibility.Visible;
            this.chkPrintWithStock.Visibility = Visibility.Visible;
            this.chkAuthorize.IsChecked = true;
            this.chkAuthorize_Click(null, null);
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

 
    }
}
