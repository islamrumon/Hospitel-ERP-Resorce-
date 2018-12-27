using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsRpt2Inventory;
using ASITHmsViewMan.Inventory;
using Microsoft.Reporting.WinForms;
using Xceed.Wpf.Toolkit;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmEntryItemStock1.xaml
    /// </summary>
    public partial class frmEntryItemStock1 : UserControl
    {
        private bool FrmInitialized = false;
        private List<vmEntryItemStock1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryItemStock1.ListViewItemTable>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private List<HmsEntityGeneral.AuthorizeInf> AuthorizeTable1 = new List<HmsEntityGeneral.AuthorizeInf>();

        private vmEntryItemStock1 vm1 = new vmEntryItemStock1();
        private vmReportStore1 vm2 = new vmReportStore1();
        public int serialno = 0;
        private DataSet EditDs;
        public bool IsActiveTransListWindow { get; set; }

        private string preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
        private string prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
        private string preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
        private DateTime rowtime1 = DateTime.Now;

        private DispatcherFrame frameAuthorise;
        public frmEntryItemStock1()
        {
            InitializeComponent();

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
                    this.chkAutoTransListpr.IsChecked = this.IsActiveTransListWindow;
                    this.btnPrint2pr.Visibility = Visibility.Hidden;
                    this.btnUpdatepr.Visibility = Visibility.Hidden;
                    this.gridDetailspr.Visibility = Visibility.Hidden;
                    this.gridAuthorize.Visibility = Visibility.Collapsed;
                    this.xctk_dtpMstDate.Value = DateTime.Today;
                    this.xctk_dtpFromDatepr.Value = DateTime.Today.AddDays(-15);
                    this.xctk_dtpToDatepr.Value = DateTime.Today;
                    this.GridItemList.Visibility = Visibility.Collapsed;
                    this.autoMstByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoMstByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                    if (this.IsActiveTransListWindow)
                    {
                        this.gridTransList.Visibility = Visibility.Visible;
                        this.gridTransList.IsEnabled = true;
                    }
                    else
                    {
                        this.gridTransList.Visibility = Visibility.Collapsed;
                        this.gridTransList.IsEnabled = false;
                    }
                    this.ActivateAuthObjects();
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MSTK-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ActivateAuthObjects()
        {

            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryPur01_frmEntryPurReq1_chkDateBlocked", _uitype: "CheckBox", _uidesc: "->> Pur. Requisition Entry Date Select"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryPur01_frmEntryPurReq1_chkAutoTransListpr", _uitype: "CheckBox", _uidesc: "->> Pur. Requisition List Display on Entry"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryPur01_frmEntryPurReq1_btnEditMemo", _uitype: "Button", _uidesc: "->> Pur. Requisition Edit"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryPur01_frmEntryPurReq1_btnCancelMemo", _uitype: "Button", _uidesc: "->> Pur. Requisition Cancel (Delete)"));


            //if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPur01_frmEntryPurReq1_chkDateBlocked") == null)
            //{
            //    this.chkDateBlocked.Visibility = Visibility.Collapsed;
            //    this.lblDateBlocked.Visibility = Visibility.Visible;
            //}

            //if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPur01_frmEntryPurReq1_chkAutoTransListpr") == null)
            //    this.chkAutoTransListpr.Visibility = Visibility.Hidden;

            //this.btnRecurring.Visibility = Visibility.Hidden;

            //if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPur01_frmEntryPurReq1_btnEditMemo") == null)
            //    this.btnEditMemo.Visibility = Visibility.Hidden;

            //if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPur01_frmEntryPurReq1_btnCancelMemo") == null)
            //    this.btnCancelMemo.Visibility = Visibility.Hidden;
        }

        private void ConstructAutoCompletionSource()
        {
            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");

            foreach (var itemd1 in deptList1)
            {
                //this.cmbSectCodpr.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                if (itemd1.sectname.ToUpper().Contains("STORE"))
                    this.cmbSectCodpr.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
            }

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();
        }
        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpMstDate.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpMstDate.IsEnabled)
                this.xctk_dtpMstDate.Focus();
        }

        private void txtCodeDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Visible;

            if (this.chkShowPrevRate.IsChecked == true)
                this.ShowPreviousRateInfo(this.txtRSirCodepr.Text.Trim());
        }

        private void txtAc_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lstItem.ItemsSource = null;
            string StrDesc1 = this.txtRSirDescpr.Text.Trim().ToUpper();
            if (StrDesc1.Length == 0)
                return;
            var List1a = WpfProcessAccess.InvItemList.FindAll(x => x.sirdesc.ToUpper().Contains(StrDesc1));
            this.lstItem.ItemsSource = List1a;
        }
        private void dgvlblSlNum_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string Itemid1 = ((Label)sender).Tag.ToString();
                var item1 = this.ListViewItemTable1.FindAll(x => x.rsircode == Itemid1);
                this.txtRSirCodepr.Text = item1[0].rsircode;
                this.txtRSirDescpr.Text = item1[0].trdesc;
                this.txtRSirDescpr.Tag = item1[0].rsircode;
                this.txtMstQty.Text = item1[0].mstkqty.ToString("#,##0.00");
                this.lblUnit1pr.Content = item1[0].trunit;
                this.txtrqRatepr.Text = item1[0].mstkrate.ToString("#,##0.00");
                this.lblAmountShow.Content = item1[0].mstkamt.ToString("#,##0.00");
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MSTK-14: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnOkpr_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            this.btnPrint2pr.Visibility = Visibility.Hidden;
            this.btnUpdatepr.Visibility = Visibility.Hidden;
            this.gridDetailspr.Visibility = Visibility.Hidden;
            this.GridItemList.Visibility = Visibility.Collapsed;
            this.dgvStockList.ItemsSource = null;
            this.ListViewItemTable1.Clear();
            this.xctk_dtpMstDate.IsEnabled = false;
            if (this.btnOkpr.Content.ToString() == "_New")
            {
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                //this.stkIntropr.IsEnabled = true;
                this.cmbSectCodpr.IsEnabled = true;

                this.autoMstByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoMstByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                this.preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = DateTime.Now;

                this.chkAutoTransListpr.IsEnabled = true;
                this.txtMstRef.Text = "";
                this.txtMstNar.Text = "";
                this.txtRSirCodepr.Text = "";
                this.txtRSirDescpr.Text = "";
                this.txtMstQty.Text = "";
                this.lblAmountShow.Content = "";
                this.txtrqRatepr.Text = "";
                this.lblUnit1pr.Content = "";
                this.lblMstNo.Content = "MSTMM-CCCC-XXXXX";
                this.lblMstNo.Tag = "MSTYYYYMMCCCCXXXXX";
                if (this.IsActiveTransListWindow)
                {
                    this.gridTransList.IsEnabled = false;
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

            this.btnUpdatepr.Visibility = Visibility.Visible;
            this.gridTransList.Visibility = Visibility.Collapsed;
            this.gridTransList.IsEnabled = false;
            this.gridDetailspr.Visibility = Visibility.Visible;
            this.chkDateBlocked.IsChecked = false;
            this.chkDateBlocked.IsEnabled = false;
            this.btnUpdatepr.IsEnabled = true;
            //this.stkItempr.IsEnabled = true;
            //this.stkIntropr.IsEnabled = false;
            this.cmbSectCodpr.IsEnabled = false;
            this.chkAutoTransListpr.IsEnabled = false;
            this.btnOkpr.Content = "_New";
        }

        private void BuildTransactionList()
        {
            string FromDate = this.xctk_dtpFromDatepr.Text;
            string ToDate = this.xctk_dtpToDatepr.Text;
            ASITFunParams.ProcessAccessParams pap1 = vm2.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "MST", FromDate, ToDate, "%", "%", "%", "%", "[0123456789A]");
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

        private bool checkOkValidation()
        {
            int length1 = this.autoMstByStaffSearch.SelectedValue.ToString().Trim().Length;
            if (length1 < 0)
                return false;

            string reqByID2 = this.autoMstByStaffSearch.SelectedValue.ToString().Trim();
            var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == reqByID2);
            return (listStaff1.Count > 0);
        }

        private void chkAutoTransListpr_Click(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
            this.IsActiveTransListWindow = (this.chkAutoTransListpr.IsChecked == true);
            if (this.IsActiveTransListWindow && this.gridDetailspr.Visibility == Visibility.Hidden)
            {
                this.BuildTransactionList();
                this.gridTransList.Visibility = Visibility.Visible;
                this.gridTransList.IsEnabled = true;
                this.dgvTransList.Focus();
            }
            else if (this.IsActiveTransListWindow == false && this.gridDetailspr.Visibility == Visibility.Hidden)
            {
                this.gridTransList.Visibility = Visibility.Collapsed;
                this.gridTransList.IsEnabled = false;
            }
            this.chkPrint2pr.IsChecked = false;
        }

        private void txtUID_LostFocus(object sender, RoutedEventArgs e)
        {

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

            decimal mstqty1a = this.validData("0" + this.txtMstQty.Text.Trim());
            if (mstqty1a <= 0)
            {
                this.txtMstQty.Focus();
                return;
            }

            decimal mstRat1 = this.validData("0" + this.txtrqRatepr.Text.Trim());
            decimal mstAmt1 = Math.Round(mstqty1a * mstRat1, 6);// this.validData("0" + this.lblAmountShow.Content.ToString().Trim());
            if (mstAmt1 <= 0)
            {
                this.txtrqRatepr.Focus();
                return;
            }

            if (this.txtRSirDescpr.Text.Trim().Length == 0)
            {
                this.txtRSirCodepr.Text = "";
                this.txtUID.Text = "";
                this.lblUnit1pr.Content = "";
                this.txtMstQty.Text = "";
                this.lblAmountShow.Content = "";
                this.txtrqRatepr.Text = "";
            }

            int serialno1 = this.ListViewItemTable1.Count + 1;
            string rsircode1 = this.txtRSirCodepr.Text.Trim();
            var List1a = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == rsircode1);
            if (List1a.Count == 0)
                return;

            string rsirdesc1 = List1a[0].sirdesc.Trim();  // this.txtRSirDesc.Text.Trim();
            string rsirunit = List1a[0].sirunit.Trim();// this.lblUnit1.Content.ToString();
            string truid1a = this.txtUID.Text.Trim();
            if (rsircode1.Length == 0)
                return;

            var list1a = this.ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
            if (list1a.Count > 0)
            {
                System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var item1a = new vmEntryItemStock1.ListViewItemTable()
            {
                trsl = serialno1.ToString() + ".",
                rsircode = rsircode1,
                trdesc = rsirdesc1,
                mstkqty = mstqty1a,
                truid = truid1a,
                trunit = rsirunit,
                mstkrate = Math.Round(mstAmt1 / mstqty1a, 6),
                mstkamt = mstAmt1
            };

            this.dgvStockList.ItemsSource = null;
            this.ListViewItemTable1.Add(item1a);
            //this.ListViewItemTable1.Sort(delegate(vmEntryItemStock1.ListViewItemTable x, vmEntryItemStock1.ListViewItemTable y)
            //{
            //    return x.rsircode.CompareTo(y.rsircode);
            //});

            this.txtRSirCodepr.Text = "";
            this.txtRSirDescpr.Text = "";
            this.txtUID.Text = "";
            this.lblUnit1pr.Content = "";
            this.txtMstQty.Text = "";
            this.lblAmountShow.Content = "";
            this.txtrqRatepr.Text = "";
            this.dgvStockList.ItemsSource = this.ListViewItemTable1;

            this.btnCalcTotal_Click(null, null);

            var item22 = this.ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
            if (item22.Count > 0)
                this.dgvStockList.ScrollIntoView(item22[0]);

            this.dgvStockList.Focus();
        }


        private void hlDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (!this.btnUpdatepr.IsEnabled)  // After updating memo rows can't be deleted
                return;

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            if (System.Windows.MessageBox.Show("Are you sure to delete record " + RowIndex1.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.dgvStockList.Items.Refresh();
            this.dgvStockList.Focus();
        }


        private void hlEditRow_Click(object sender, RoutedEventArgs e)
        {
            if (!this.btnUpdatepr.IsEnabled)  // After updating memo rows can't be edited
                return;

            this.txtRSirCodepr.Text = "";
            this.txtRSirDescpr.Text = "";
            this.txtUID.Text = "";
            this.lblUnit1pr.Content = "";
            this.txtMstQty.Text = "";
            this.lblAmountShow.Content = "";
            this.txtrqRatepr.Text = "";

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            var tblItm1 = this.ListViewItemTable1[RowIndex1 - 1];
            var tblitm2 = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == tblItm1.rsircode);

            this.txtMstQty.Text = tblItm1.mstkqty.ToString();
            this.txtrqRatepr.Text = tblItm1.mstkrate.ToString();
            this.lblAmountShow.Content = tblItm1.mstkamt.ToString();
            this.txtRSirDescpr.Text = tblitm2[0].sirdesc;
            this.txtUID.Text = tblitm2[0].sirtype;
            this.lblUnit1pr.Content = tblitm2[0].sirunit;
            this.txtRSirCodepr.Text = tblItm1.rsircode;

            this.ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.dgvStockList.Items.Refresh();
            //ListViewItemTable1.Sort(delegate(vmEntryItemStock1.ListViewItemTable x, vmEntryItemStock1.ListViewItemTable y)
            //{
            //    return x.rsircode.CompareTo(y.rsircode);
            //});

        }

        private void btnUpdatepr_Click(object sender, RoutedEventArgs e)
        {

            this.btnCalcTotal_Click(null, null);
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            string EditMstNum1 = (this.EditDs != null ? this.lblMstNo.Tag.ToString() : "");
            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCodpr.SelectedItem).Tag.ToString();
            string TakenBy1 = this.autoMstByStaffSearch.SelectedValue.ToString();

            if (EditMstNum1.Length == 18)
            {
                var pap1b = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, EditMstNum1, "BACKUP", "MESSAGE");
                DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                if (ds1b == null)
                    return;
            }

            this.InitializeAuthorization(); // To update the prepared by record
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpMstDate.Text), cbSectCode: cbSectCode1,
                          mstkByID1: TakenBy1, MemoRef1: this.txtMstRef.Text.Trim(), MemoNar1: this.txtMstNar.Text.Trim(), ListViewItemTable1: this.ListViewItemTable1,
                          AuthorizeTable1: this.AuthorizeTable1, _preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);

            //_preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            //String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamUpdateItemStock(WpfProcessAccess.CompInfList[0].comcod, ds1, EditMstNum1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblMstNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString(); ;
            this.lblMstNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();
            this.EditDs = new DataSet(); // For Current Authorization
            this.btnUpdatepr.IsEnabled = false;
            this.stkItempr.IsEnabled = false;
            this.btnPrint2pr.Visibility = Visibility.Visible;
        }

        private void btnFilterpr_Click(object sender, RoutedEventArgs e)
        {
            this.gridTransList.IsEnabled = false;
            this.BuildTransactionList();
            this.gridTransList.IsEnabled = true;
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
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
                var list3 = WpfProcessAccess.GetRptGenInfo();
                rpt1 = StoreReportSetup.GetLocalReport("Store.RptTransectionList", list1, null, list3); // ( R_01_RptSetup.RptSetupItemList1(ds1, ds2);          
                WindowTitle1 = "Item Stock Transaction List";
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
        private void UnCheckedAllPopups()
        {
            this.chkPrint2pr.IsChecked = false;
        }

        private void btnPrint2pr_Click(object sender, RoutedEventArgs e)
        {
            var item1a = this.lblMstNo.Tag.ToString();
            this.PrintReqMemo(item1a);
        }

        private void PrintReqMemo(string MemoNum1 = "XXXXXXXXXXX")
        {
            try
            {
                var pap1 = vm2.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, MemoNum1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.MStockMemo>();
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
                LocalReport rpt1 = StoreReportSetup.GetLocalReport("Store.RptMStockMemo01", list1, list2, list3);
                if (rpt1 == null)
                    return;

                string WindowTitle1 = "Physical Stock  Memo";
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MSTK-09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void txtrqRatepr_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblAmountShow.Content = "";
            try
            {
                Double quantity = Double.Parse("0" + this.txtMstQty.Text.ToString().Trim());
                Double Rate = Double.Parse("0" + this.txtrqRatepr.Text.ToString().Trim());
                Double Amount = quantity * Rate;
                lblAmountShow.Content = "Amt: " + Amount.ToString("#,##0").Trim();

            }
            catch (Exception)
            {

                return;
            }
        }

        private void chkShowPrevRate_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtRSirCodepr.Text.Trim().Length == 0)
                return;

            if (this.chkShowPrevRate.IsChecked == true)
            {
                this.ShowPreviousRateInfo(this.txtRSirCodepr.Text.Trim());
            }
        }

        private void ShowPreviousRateInfo(string ItemCode1)
        {
            this.lstBoxPrevRate.Items.Clear();
            if (ItemCode1.Length == 0)
                return;
            string SectCode1 = ((ComboBoxItem)this.cmbSectCodpr.SelectedItem).Tag.ToString();
            string MemoDate1 = this.xctk_dtpMstDate.Text.Substring(0, 11);
            string ToDate = this.xctk_dtpToDatepr.Text;
            var pap1 = vm2.SetParamPreviousItemRate(WpfProcessAccess.CompInfList[0].comcod, SectCode1, MemoDate1, ItemCode1);
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

        private void lstItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectItemInfo();
            this.txtMstQty.Focus();
            //this.txtRSirDescpr.Focus();
        }

        private void lstItem_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                this.SelectItemInfo();
                this.txtMstQty.Focus();
                //this.txtRSirDescpr.Focus();
                //this.btnAddItem_Click(null, null);
            }
        }
        private void SelectItemInfo()
        {
            var lbi1 = (HmsEntityGeneral.SirInfCodeBook)this.lstItem.SelectedItem;

            if (lbi1 == null)
                return;
            this.txtRSirCodepr.Text = lbi1.sircode;
            this.txtRSirDescpr.Tag = lbi1.sircode;
            this.txtRSirDescpr.Text = lbi1.sirdesc.Trim();
            this.lblUnit1pr.Content = lbi1.sirunit.Trim();
        }
        private void txtMstQty_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }
        private void txtrqRatepr_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }
        private void btnCalcTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dgvStockList.ItemsSource = null;
                //this.dgReqList.ItemsSource = null;
                foreach (var item in this.ListViewItemTable1)
                    item.mstkamt = item.mstkqty * item.mstkrate;
                int count1 = this.ListViewItemTable1.Count;

                this.ListViewItemTable1 = this.ListViewItemTable1.FindAll(x => x.mstkamt > 0);
                if (count1 != this.ListViewItemTable1.Count)
                {
                    int i = 1;
                    foreach (var item in this.ListViewItemTable1)
                    {
                        item.trsl = i.ToString() + ".";
                        i++;
                    }
                }
                decimal sumAmt1 = this.ListViewItemTable1.Sum(x => x.mstkamt);
                this.lblTotalMstAmt.Content = sumAmt1.ToString("#,##0.00;(#,##0.00); - ");
                this.dgvStockList.ItemsSource = this.ListViewItemTable1;
                this.dgvStockList.Items.Refresh();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MSTK-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop,
                    MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void autoMstByStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
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

                this.xctk_dtpMstDate.Value = Convert.ToDateTime(dr0["memodate"].ToString());

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

                this.lblMstNo.Content = dr0["memonum1"].ToString();
                this.lblMstNo.Tag = dr0["memonum"].ToString();
                this.autoMstByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoMstByStaffSearch.SelectedValue = dr0["PreparById"].ToString().Trim();

                this.txtMstRef.Text = dr0["Referance"].ToString().Trim();
                this.txtMstNar.Text = dr0["Naration"].ToString().Trim();
                this.ListViewItemTable1.Clear();
                foreach (DataRow item in this.EditDs.Tables[1].Rows)
                {
                    var item1b = new vmEntryItemStock1.ListViewItemTable()
                    {
                        trsl = item["slnum"].ToString().Trim() + ".",
                        rsircode = item["rsircode"].ToString().Trim(),
                        trdesc = item["sirdesc"].ToString().Trim(),
                        mstkqty = Convert.ToDecimal(item["mstkqty"]),
                        truid = "",
                        trunit = item["sirunit"].ToString().Trim(),
                        mstkrate = Convert.ToDecimal(item["mstkrate"]),
                        mstkamt = Convert.ToDecimal(item["mstkqty"]) * Convert.ToDecimal(item["mstkrate"])
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

        private void btnEditAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.btnEditMemo_Click(null, null);
            if (this.EditDs == null)
                return;

            this.btnUpdatepr.IsEnabled = false;
            this.btnPrint2pr.Visibility = Visibility.Visible;
            this.chkAuthorize.IsChecked = true;
            this.chkAuthorize_Click(null, null);
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

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this Memo " + item1a.memonum1, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
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
                System.Windows.MessageBox.Show("MSTK-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
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

            string memonum1 = this.lblMstNo.Tag.ToString();
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
            autitem.auhcid = this.autoMstByStaffSearch.SelectedValue.ToString();
            autitem.auhcnam = this.autoMstByStaffSearch.SelectedText.Trim();
            autitem.austat = "Y";
            autitem.statdes = "Prepared";
            autitem.aunote = this.txtMstNar.Text.Trim();
            autitem.autime = this.rowtime1; //DateTime.Parse(this.xctk_dtpSrfDat.Text.Trim());
            autitem.luhcid = this.preparebyid1;
            autitem.luses = this.prepareses1;
            autitem.lutrm = this.preparetrm1;
            autitem.lutime = this.rowtime1;
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

        private void chkAuthorize_Click(object sender, RoutedEventArgs e)
        {
            if (this.chkAuthorize.IsChecked == true)
                this.ShowHideAuthorizationPanle();
        }

        private void ShowHideAuthorizationPanle()
        {
            this.InitializeAuthorization();
            this.lblAuthorizeMemoDesc.Content = "Date: " + this.xctk_dtpMstDate.Text.Trim() + ", Req. No: " + this.lblMstNo.Content.ToString();
            this.lblAuthorizeMemoDesc.Tag = this.lblMstNo.Tag.ToString();

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

        private void btnCancelAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.frameAuthorise.Continue = false; // un-blocks gui message pump
        }

        private void btnOkAuthorize_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoAutByStaffSearch.SelectedValue == null)
                return;

            string usrid1 = this.autoAutByStaffSearch.SelectedValue.ToString();
            string AutCod = this.TxtbCurAuHeader.Tag.ToString();
            if (AutCod == "CBALCOD01010")
            {
                int cntUser1 = this.AuthorizeTable1.Count(x => x.auhcid == usrid1);
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

    }
}
