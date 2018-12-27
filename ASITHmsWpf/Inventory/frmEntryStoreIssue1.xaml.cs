using ASITFunLib;
using ASITHmsEntity;
using ASITHmsRpt2Inventory;
using ASITHmsViewMan.Inventory;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;
using Xceed.Wpf.Toolkit;

namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmEntryStoreReq1.xaml
    /// </summary>

    public partial class frmEntryStoreIssue1 : UserControl
    {

        private bool FrmInitialized = false;
        private List<vmEntryStoreIssue1.PendingSrfList> PSrfList = new List<vmEntryStoreIssue1.PendingSrfList>();
        private List<vmEntryStoreIssue1.PendingSrfItemList> PSrfItemList = new List<vmEntryStoreIssue1.PendingSrfItemList>();
        private List<vmEntryStoreIssue1.StockItemList> InvItemList = new List<vmEntryStoreIssue1.StockItemList>();
        private List<vmEntryStoreIssue1.StockItemSumList> InvItemSumList = new List<vmEntryStoreIssue1.StockItemSumList>();
        private List<vmEntryStoreIssue1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryStoreIssue1.ListViewItemTable>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private List<vmEntryStoreIssue1.cmbPendingReqList> ListPendingReq = new List<vmEntryStoreIssue1.cmbPendingReqList>();
        private List<vmEntryStoreIssue1.ListViewItemSelect2> ListviewitemSelect2 = new List<vmEntryStoreIssue1.ListViewItemSelect2>();
        private List<HmsEntityGeneral.AuthorizeInf> AuthorizeTable1 = new List<HmsEntityGeneral.AuthorizeInf>();

        private List<HmsEntityInventory.StoreIssueSummary1> ListStorIsueSumm1 = new List<HmsEntityInventory.StoreIssueSummary1>();

        private vmEntryStoreIssue1 vm1 = new vmEntryStoreIssue1();
        private vmReportStore1 vm1r = new vmReportStore1();
        private bool manualTextChange = false;
        private bool multiReqAdd = false;
        private CheckBox chkMulReqAdd = new CheckBox();

        private DataSet EditDs;
        private int serialno = 0;
        private bool IsActiveTransListWindow { get; set; }
        private string TitaleTag1, TitaleTag2;

        private string preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
        private string prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
        private string preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
        private DateTime rowtime1 = DateTime.Now;

        private DispatcherFrame frameAuthorise;
        public frmEntryStoreIssue1()
        {

            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }

       

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            this.TitaleTag1 = this.Tag.ToString();
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
                    this.TitaleTag2 = this.Tag.ToString();
                    this.IsActiveTransListWindow = false;

                    this.ListViewItemTable1.Clear();
                    this.ConstructAutoCompletionSource();
                    this.GridItemList.Visibility = Visibility.Collapsed;
                    this.lstItem.ItemsSource = null;
                    this.chkAutoTransList.IsChecked = this.IsActiveTransListWindow;
                    this.btnPrint2.Visibility = Visibility.Hidden;
                    this.btnUpdate.Visibility = Visibility.Hidden;
                    this.gridDetails.Visibility = Visibility.Hidden;
                    this.gridTransList.Visibility = Visibility.Hidden;
                    this.gridTransList.IsEnabled = false;
                    this.gridAuthorize.Visibility = Visibility.Collapsed;
                    this.xctk_dtpSrfDat.Value = DateTime.Today;
                    this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-15);
                    this.xctk_dtpToDate.Value = DateTime.Today;
                    this.autoIssueByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoIssueByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;

                    if (IsActiveTransListWindow)
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
                System.Windows.MessageBox.Show("Issue-05: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ActivateAuthObjects()
        {
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreIssue1", _uitype: "Form", _uidesc: "Item Issue/Consumption - Entry/Edit Screen"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreIssue1_chkDateBlocked", _uitype: "CheckBox", _uidesc: "->> Store Issue Entry Date Select"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreIssue1_chkAutoTransList", _uitype: "CheckBox", _uidesc: "->> Store Issue List Display on Entry"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreIssue1_cmdEditIssueMemo", _uitype: "Button", _uidesc: "->> Store Issue Edit"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreIssue1_cmdCancelIssueMemo", _uitype: "Button", _uidesc: "->> Store Issue Cancel (Delete)"));

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryStoreIssue1_chkDateBlocked") == null)
            {
                this.chkDateBlocked.Visibility = Visibility.Collapsed;
                this.lblDateBlocked.Visibility = Visibility.Visible;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryStoreIssue1_chkAutoTransList") == null)
            {
                this.chkAutoTransList.Visibility = Visibility.Hidden;
            }

            this.btnRecurring.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryStoreIssue1_cmdEditIssueMemo") == null)
                this.cmdEditIssueMemo.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryStoreIssue1_cmdCancelIssueMemo") == null)
                this.cmdCancelIssueMemo.Visibility = Visibility.Hidden;
        }

        public void ConstructAutoCompletionSource()
        {
            try
            {
                var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
                foreach (var itemd1 in deptList1)
                {
                    this.cmbSectCod2.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                    this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                }

                this.cmbSectCod2.Items.Add(new ComboBoxItem() { Content = "ITEM CONSUMPTION/WASTAGE/STOCK OUT", Tag = "000000000000" });

                int index1 = 0;
                foreach (var itemd1 in deptList1)
                {
                    if (itemd1.sectname.ToUpper().Contains("STORE"))
                        break;
                    index1++;
                }
                this.cmbSectCod.SelectedIndex = index1;

                if (WpfProcessAccess.StaffList == null)
                    WpfProcessAccess.GetCompanyStaffList();

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void cmbSectCod2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbSectCod2.Items.Count == 0)
                    return;

                string ConCod1 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString().Trim();
                this.chkDateBlocked.Content = (ConCod1 == "000000000000" ? "Cons. _Date : " : "Issue _Date : ");
                this.lblsectcod.Content = (ConCod1 == "000000000000" ? "Consum/Out _From :" : "Issue _From :");
                this.lblsectCode2Title.Content = (ConCod1 == "000000000000" ? "_Trans. Type :" : "Issue _To :");
                this.lblSirByidTitle.Content = (ConCod1 == "000000000000" ? "Consum/Out _By :" : "Issued _By :");
                this.lblrecvbyidTitle.Content = (ConCod1 == "000000000000" ? "_Verified By :" : "Recei_ved By :");
                this.lblSrfQtyTitle.Content = (ConCod1 == "000000000000" ? "Cons. _Qty:" : "Issue _Qty:");
                ////string recbyVal1 = this.autoRecByStaffSearch.SelectedValue.ToString();
                this.autoRecByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoRecByStaffSearch.SelectedValue = (ConCod1 == "000000000000" ? WpfProcessAccess.SignedInUserList[0].hccode : null);
                this.stkpReqList.Visibility = (ConCod1 == "000000000000" ? Visibility.Collapsed : Visibility.Visible);
                //this.ListView1.Height = (ConCod1 == "000000000000" ? 390 : 360);
                if (this.chkAutoTransList.IsChecked == true)
                    this.BuildTransactionList();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-04: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnNav_Click(object sender, RoutedEventArgs e)
        {

            if (this.dgvIssue.Items.Count == 0)
                return;

            if (this.dgvIssue.SelectedIndex < 0)
                this.dgvIssue.SelectedIndex = 0;

            string ActtionName = ((Button)sender).Name.ToString().Trim();
            int index1 = this.dgvIssue.SelectedIndex;
            if (ActtionName == "btnDelete")
            {
                string delmsg = "Are you confirm to delete item\n" + this.ListViewItemTable1[index1].trsl +
                    this.ListViewItemTable1[index1].rsircode + " - " + this.ListViewItemTable1[index1].trdesc.Trim();

                MessageBoxResult msgresult = System.Windows.MessageBox.Show(delmsg, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;

                this.dgvIssue.ItemsSource = null;
                var code1 = this.ListViewItemTable1[index1].rsircode;
                this.ListViewItemTable1 = this.ListViewItemTable1.FindAll(x => x.rsircode != code1);
                this.dgvIssue.ItemsSource = this.ListViewItemTable1;
                if (this.ListViewItemTable1.Count > 0)
                {
                    this.dgvIssue.SelectedIndex = (this.ListViewItemTable1.Count <= index1 ? this.ListViewItemTable1.Count - 1 : index1);
                }
                return;
            }

            switch (ActtionName)
            {
                case "btnTop":
                    index1 = 0;
                    break;
                case "btnPrev":
                    index1 = this.dgvIssue.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnNext":
                    index1 = this.dgvIssue.SelectedIndex + 1;
                    if (index1 >= this.dgvIssue.Items.Count)
                        index1 = this.dgvIssue.Items.Count - 1;
                    break;
                case "btnBottom":
                    index1 = this.dgvIssue.Items.Count - 1;
                    break;
            }
            this.dgvIssue.SelectedIndex = index1;

            var item21 = (vmEntryStoreIssue1.ListViewItemTable)this.dgvIssue.Items[index1];
            this.dgvIssue.ScrollIntoView(item21);
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbSectCod.SelectedValue.ToString() == cmbSectCod2.SelectedValue.ToString())
                    return;

                this.UnCheckedAllPopups();
                string DeptID1 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();
                this.btnPrint2.Visibility = Visibility.Hidden;
                this.btnUpdate.Visibility = Visibility.Hidden;
                this.gridDetails.Visibility = Visibility.Hidden;
                this.lblStkBalDes.Content = "";
                this.cmbSrfList.ItemsSource = null;
                this.ListPendingReq.Clear();
                this.ListViewItemTable1.Clear();
                this.lstItem.ItemsSource = null;
                this.dgvIssue.ItemsSource = null;
                this.xctk_dtpSrfDat.Minimum = null;
                this.xctk_dtpSrfDat.Maximum = null;
                this.txtblEditMode.Visibility = Visibility.Hidden;
                this.GridItemList.Visibility = Visibility.Collapsed;
                if (this.btnOk.Content.ToString() == "_New")
                {
                    this.EditDs = null;
                    this.chkMulReqAdd.IsChecked = false;
                    this.chkWithoutReq.IsChecked = false;
                    this.chkWithoutReq.IsEnabled = true;
                    this.chkWithoutReq.Visibility = Visibility.Visible;
                    this.stkItem.Visibility = Visibility.Visible;
                    this.xctk_dtpSrfDat.Value = DateTime.Today;
                    this.chkDateBlocked.IsChecked = false;
                    this.chkDateBlocked.IsEnabled = true;
                    this.chkAutoTransList.IsEnabled = true;
                    this.cmbSectCod.IsEnabled = true;
                    this.cmbSectCod2.IsEnabled = true;
                    //this.stkIntro.IsEnabled = true;
                    this.autoIssueByStaffSearch.SelectedValue = null;
                    this.autoRecByStaffSearch.SelectedValue = null;
                    this.txtissRefpr.Text = "";
                    this.txtSrfNar.Text = "";
                    this.txtRSirCode.Text = "";
                    this.txtRSirDesc.Text = "";
                    this.txtSirQty.Text = "";
                    this.txtUID.Text = "";
                    this.lblUnit1.Content = "";
                    this.lblSrfNo.Content = "SIRMM-CCCC-XXXXX";
                    this.lblSrfNo.Tag = "SIRYYYYMMCCCCXXXXX";
                    this.autoIssueByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoIssueByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                    if (DeptID1 == "000000000000")
                    {
                        this.autoRecByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                        this.autoRecByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                    }

                    this.preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
                    this.prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
                    this.preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
                    this.rowtime1 = DateTime.Now;

                    if (this.IsActiveTransListWindow)
                    {
                        this.BuildTransactionList();
                        this.gridTransList.Visibility = Visibility.Visible;
                        this.gridTransList.IsEnabled = true;
                        this.dgvTransList.Focus();
                    }
                    this.btnOk.Content = "_Ok"; // new AccessText { Text = "_Ok" };//  Content = new AccessText { Text = "_Label" };
                    return;
                }

                ////if (this.checkOkValidation() == false)
                ////    return;

                if (this.GetStockItemList() == false)
                    return;
                if (this.chkWithoutReq.IsChecked == true)
                {
                    this.chkWithoutReq.IsEnabled = false;
                    this.stkpReqList.Visibility = Visibility.Collapsed;
                    this.stkpManualItemAdd.Visibility = Visibility.Visible;
                    this.dgvIssue.Columns[1].Visibility = Visibility.Collapsed;
                    this.dgvIssue.Columns[5].Visibility = Visibility.Collapsed;
                    this.dgvIssue.Columns[6].Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (this.GetPendingSrfList() == false)
                        return;
                    this.chkWithoutReq.Visibility = Visibility.Hidden;
                    this.stkpReqList.Visibility = Visibility.Visible;
                    this.stkpManualItemAdd.Visibility = Visibility.Collapsed;
                    this.dgvIssue.Columns[1].Visibility = Visibility.Visible;
                    this.dgvIssue.Columns[5].Visibility = Visibility.Visible;
                    this.dgvIssue.Columns[6].Visibility = Visibility.Visible;
                }
                this.btnUpdate.Visibility = Visibility.Visible;
                this.gridTransList.Visibility = Visibility.Hidden;
                this.gridTransList.IsEnabled = false;
                this.gridDetails.Visibility = Visibility.Visible;
                this.chkDateBlocked.IsChecked = false;
                //this.chkDateBlocked.IsEnabled = false;
                this.chkAutoTransList.IsEnabled = false;
                this.btnUpdate.IsEnabled = true;
                this.stkItem.IsEnabled = true;
                this.cmbSectCod.IsEnabled = false;
                this.cmbSectCod2.IsEnabled = false;
                //this.stkIntro.IsEnabled = false;
                this.btnOk.Content = "_New"; //new AccessText { Text = "_New" };// "_New";
                this.cmbSrfList_SelectionChanged(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-06: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private bool GetStockItemList()
        {
            try
            {
                string StoreID1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                string StoreID2 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();
                string AsOnDate1 = this.xctk_dtpSrfDat.Text;
                var pap1 = vm1.SetParamGetStockItemList(WpfProcessAccess.CompInfList[0].comcpcod, StoreID1, AsOnDate1, "ALLITEMS", "SUMMARY");
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                {
                    WpfProcessAccess.ShowDatabaseErrorMessage();
                    //System.Windows.MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage);
                    //WpfProcessAccess.DatabaseErrorInfoList = null;
                    return false;
                }

                this.InvItemList.Clear();
                this.InvItemSumList.Clear();
                this.InvItemList = ds2.Tables[0].DataTableToList<vmEntryStoreIssue1.StockItemList>();
                this.InvItemSumList = ds2.Tables[1].DataTableToList<vmEntryStoreIssue1.StockItemSumList>();

                string fromDate = "01-" + this.xctk_dtpSrfDat.Text.Substring(3);
                string toDate = DateTime.Parse(fromDate).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
                var pap1i = vm1r.SetParamInvSumReport(WpfProcessAccess.CompInfList[0].comcod, "SIR", fromDate, toDate, StoreID1, StoreID2, "%");
                DataSet ds1i = WpfProcessAccess.GetHmsDataSet(pap1i);
                if (ds1i == null)
                {
                    WpfProcessAccess.ShowDatabaseErrorMessage();
                    return false;
                }
                this.ListStorIsueSumm1 = ds1i.Tables[0].DataTableToList<HmsEntityInventory.StoreIssueSummary1>();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-07: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
            return true;
        }

        private bool GetPendingSrfList()
        {
            try
            {

                string StoreID1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                string DeptID1 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();
                string AsOnDate1 = this.xctk_dtpSrfDat.Text;
                this.PSrfItemList.Clear();
                this.cmbSrfList.ItemsSource = null;
                this.ListPendingReq.Clear();
                //this.cmbSrfList.Items.Clear();
                if (DeptID1 != "000000000000")
                {
                    // AsOnDate1 + 90 days back validity period of a requisition 
                    var pap1 = vm1.SetParamGetPendingSrfList(WpfProcessAccess.CompInfList[0].comcpcod, StoreID1, DeptID1, AsOnDate1, "%", "A");
                    DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                    if (ds2 == null)
                    {
                        WpfProcessAccess.ShowDatabaseErrorMessage();
                        return false;
                    }

                    this.PSrfItemList = ds2.Tables[1].DataTableToList<vmEntryStoreIssue1.PendingSrfItemList>();

                    this.ListPendingReq = ds2.Tables[0].DataTableToList<vmEntryStoreIssue1.cmbPendingReqList>().ToList();
                    this.cmbSrfList.ItemsSource = this.ListPendingReq;
                    foreach (DataRow itemd1 in ds2.Tables[0].Rows)
                    {
                        //if (this.FindStockItem(itemd1["srfno"].ToString().Trim()))
                        //this.cmbSrfList.ItemsSource = this.PSrfItemList;//.Items.Add(new ComboBoxItem() { Content = "Date : " + itemd1["srfdat1"].ToString() + ", Req. No :" + itemd1["srfno1"].ToString() + ", Department : " + itemd1["sectdes2"].ToString() + ", Req. by : " + itemd1["srfbynam"].ToString(), Tag = itemd1["srfno"].ToString().Trim() });
                    }
                }
                //this.cmbSrfList.Items.Add(new ComboBoxItem() { Content = "ISSUE WITHOUT REQUISITION", Tag = "000000000000000000" });
                this.cmbSrfList.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-08: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
            return true;
        }
        private bool FindStockItem(string srfno1)
        {
            try
            {
                var ListSrfItm1 = this.PSrfItemList.FindAll(x => x.srfno == srfno1);
                foreach (var item1a in ListSrfItm1)
                {
                    var List1v = this.InvItemSumList.FindAll(a => a.sircode == item1a.rsircode);
                    if (List1v.Count() > 0)
                        return true;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
            return false;
        }
        private void UnCheckedAllPopups()
        {
            this.chkPrint2.IsChecked = false;
        }
        private bool checkOkValidation()
        {
            if (this.autoIssueByStaffSearch.SelectedValue == null || this.autoRecByStaffSearch.SelectedValue == null)
                return false;

            string srfByID1 = this.autoIssueByStaffSearch.SelectedValue.ToString();
            if (srfByID1.Length <= 0)
                return false;

            string rcvByID = this.autoRecByStaffSearch.SelectedValue.ToString();
            if (rcvByID.Length <= 0)
                return false;

            var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == srfByID1);
            return (listStaff1.Count > 0);
        }



        private void txtCodeDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Visible;

            //try
            //{

            //    string wtxtName1 = ((WatermarkTextBox)sender).Name.ToString().Trim();
            //    string tag1 = (wtxtName1 == "txtUID" || wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");

            //    if (this.ListView1.Tag.ToString().Trim() != tag1)
            //        this.ListView1.Items.Clear();

            //    this.manualTextChange = true;
            //    this.ListView1.Tag = tag1;
            //}
            //catch (Exception exp)
            //{
            //    System.Windows.MessageBox.Show("Issue-10: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            //}
        }



        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.autoIssueByStaffSearch.SelectedValue == null || this.autoRecByStaffSearch.SelectedValue == null)
                {
                    System.Windows.MessageBox.Show("Please select Issue By and Received By staff name.", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                        MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }


                if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                  MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                {
                    return;
                }

                bool IsValid = true;
                string ConCod1 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString().Trim();

                var ListViewItemTable1a = this.ListViewItemTable1.FindAll(x => x.sirqty > 0).ToList();

                if (ListViewItemTable1a == null)
                    IsValid = false;
                else if (ListViewItemTable1a.Count <= 0)
                    IsValid = false;

                if (this.EditDs == null) // For new Issue
                {
                    var list2a = ListViewItemTable1a.FindAll(x => x.sirqty > x.stokqty).ToList();
                    if (list2a.Count > 0)
                        IsValid = false;
                }

                string SirById1a = this.autoIssueByStaffSearch.SelectedValue.ToString();
                string RecvByID1a = this.autoRecByStaffSearch.SelectedValue.ToString();
                if (SirById1a.Length == 0 || RecvByID1a.Length == 0)
                    IsValid = false;

                if (!IsValid)
                {
                    System.Windows.MessageBox.Show("Validation failed. Update not possible. \nPlease try again with valid information. ", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                        MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                string EditIssuNum1 = (this.EditDs != null ? this.lblSrfNo.Tag.ToString() : "");

                if (EditIssuNum1.Length == 18)
                {
                    var pap1b = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, EditIssuNum1, "BACKUP", "MESSAGE");
                    DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                    if (ds1b == null)
                        return;
                }
                string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                string cbSectCode2 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();
                /*
                    this.InitializeAuthorization(); // To update the prepared by record
                    DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpSrfDat.Text), EditMemoNum1: EditSrfNum1, cbSectCode: cbSectCode1,
                        cbSectCode2: cbSectCode2, srfByID1: SrfById1a, MemoRef1: this.txtSrfRef.Text.Trim(), MemoNar1: this.txtSrfNar.Text.Trim(), ListViewItemTable1: ListViewItemTable1a,
                        AuthorizeTable1: this.AuthorizeTable1, _preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);
                    // _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);
                */

                this.InitializeAuthorization(); // To update the prepared by record
                DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpSrfDat.Text),
                              EditMemoNum1: EditIssuNum1, cbSectCode: cbSectCode1, cbSectCode2: cbSectCode2, sirByID1: SirById1a, recvByID1: RecvByID1a,
                              MemoRef1: this.txtissRefpr.Text.Trim(), MemoNar1: this.txtSrfNar.Text.Trim(), ListViewItemTable1a: ListViewItemTable1a,
                              AuthorizeTable1: this.AuthorizeTable1, _preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);
                //_preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

                //String xx1 = ds1.GetXml().ToString();

                var pap1 = vm1.SetParamUpdateStoreIssue(WpfProcessAccess.CompInfList[0].comcod, ds1, EditIssuNum1);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                {
                    //WpfProcessAccess.DatabaseErrorInfoList[0].errormessage.Trim();
                    System.Windows.MessageBox.Show("Failed to save data into database. \nPlease try again with valid information. ", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                        MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                this.lblSrfNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                this.lblSrfNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();
                this.EditDs = new DataSet(); // For Current Authorization
                this.btnUpdate.IsEnabled = false;
                this.stkItem.IsEnabled = false;
                this.btnPrint2.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private List<vmEntryStoreIssue1.ListViewItemTable> RedefineStockIssue()
        {
            try
            {
                // Recust Stock Issue List
                var ListViewItemTable1a = new List<vmEntryStoreIssue1.ListViewItemTable>();

                #region Recust Stock Balance
                foreach (var item1a in this.ListViewItemTable1)
                {
                    string itcod1 = item1a.rsircode;
                    decimal iqty = item1a.sirqty;

                    var lvi1 = this.InvItemSumList.Find(x => x.sircode == itcod1);
                    if (lvi1 == null)
                        return null;

                    if (iqty > lvi1.stkqty)
                    {
                        System.Windows.MessageBox.Show("Required quantity out of stock for " + lvi1.sirdesc.Trim(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                        this.txtSirQty.Focus();
                        return null;
                    }

                    var item1b = this.InvItemList.FindAll(x => x.sircode == itcod1);
                    item1b.Sort(delegate(vmEntryStoreIssue1.StockItemList x, vmEntryStoreIssue1.StockItemList y)
                    {
                        return x.invcode.CompareTo(y.invcode);
                    });

                    if (iqty > 0)
                    {
                        foreach (var item1c in item1b)
                        {
                            if (iqty <= item1c.stkqty)
                            {
                                ListViewItemTable1a.Add(new vmEntryStoreIssue1.ListViewItemTable()
                                {
                                    trsl = "",
                                    invcode = item1c.invcode,
                                    rsircode = item1c.sircode,
                                    trdesc = item1c.sirdesc,
                                    sirqty = iqty,
                                    siruid = item1c.siruid,
                                    trunit = item1c.sirunit,
                                    srfno = item1a.srfno,
                                    srfno1 = item1a.srfno1,
                                    srfqty = 0.00m,
                                    stokqty = 0.00m,
                                    prvsirqty = 0.00m
                                });
                                break;
                            }
                            else
                            {
                                ListViewItemTable1a.Add(new vmEntryStoreIssue1.ListViewItemTable()
                                {
                                    trsl = "",
                                    invcode = item1c.invcode,
                                    rsircode = item1c.sircode,
                                    trdesc = item1c.sirdesc,
                                    sirqty = item1c.stkqty,
                                    siruid = item1c.siruid,
                                    trunit = item1c.sirunit,
                                    srfno = item1a.srfno,
                                    srfno1 = item1a.srfno1,
                                    srfqty = 0.00m,
                                    stokqty = 0.00m,
                                    prvsirqty = 0.00m
                                });
                                iqty = (iqty - item1c.stkqty);
                            }
                        }
                    }
                }
                #endregion Recust Stock Balance
                ListViewItemTable1a = ListViewItemTable1a.FindAll(x => x.sirqty > 0);
                return ListViewItemTable1a;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return null;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void chkAutoTransList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsActiveTransListWindow = (this.chkAutoTransList.IsChecked == true);
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
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-14: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void BuildTransactionList()
        {
            try
            {
                if (this.cmbSectCod.Items.Count == 0 || this.cmbSectCod2.Items.Count == 0)
                    return;

                string FromDate = this.xctk_dtpFromDate.Text;
                string ToDate = this.xctk_dtpToDate.Text;
                string cmbIssfr = "%";// ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                string cmbIssfrDes = "";
                string cmbisst = "%"; // ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();
                string cmbisstDes = "%";
                if (this.chkSelectedSender.IsChecked == true)
                {
                    cmbIssfr = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                    cmbIssfrDes = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Content.ToString().Trim();// .Tag.ToString();
                }
                if (this.chkSelectedReceiver.IsChecked == true)
                {
                    cmbisst = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();
                    cmbisstDes = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Content.ToString().Trim();// .Tag.ToString();
                }

                if (cmbIssfr == "%" && cmbisst == "%")
                {
                    this.txtTransTitle.Text = "All Transaction List From : " + FromDate + " To : " + ToDate;
                }
                else
                    if (cmbIssfr == "%" && cmbisst != "%")
                    {
                        this.txtTransTitle.Text = " Transaction List From : " + FromDate + " To : " + ToDate + " Issue To : " + cmbisstDes;
                    }
                    else if (cmbIssfr != "%" && cmbisst == "%")
                    {
                        this.txtTransTitle.Text = "Transaction List From : " + FromDate + " To : " + ToDate + " Issue From : " + cmbIssfrDes;
                    }
                    else
                    {
                        this.txtTransTitle.Text = " Transaction List From : " + FromDate + " To : " + ToDate + " Issue From : " + cmbIssfrDes + ", Issue To : " + cmbisstDes;
                    }

                var pap1 = vm1r.SetParamStoreTransList(CompCode: WpfProcessAccess.CompInfList[0].comcod, TrTyp: "SIR", FromDate: FromDate, ToDate: ToDate, DeptID1: cmbIssfr,
                            DeptID2: cmbisst, MemoStatus1: "[0123456789AD]");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.dgvTransList.ItemsSource = null;
                this.ListViewTransTable1.Clear();
                this.ListViewTransTable1 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();


                foreach (var itm1a in this.ListViewTransTable1)
                {
                    if (itm1a.sectcod2 == "000000000000")
                        itm1a.sectname2 = "ITEM CONSUMPTION ENTRY";
                }
                this.dgvTransList.ItemsSource = this.ListViewTransTable1;
                this.dgvTransList.SelectedIndex = 0;
                this.dgvTransList.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-15: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void cmbSrfList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.dgvIssue1.ItemsSource = null;
                this.ListviewitemSelect2.Clear();

                if (this.cmbSrfList.Items.Count == 0)
                    return;

                this.txtSirQty.Text = "";
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtUID.Text = "";

                string srfno1 = ((vmEntryStoreIssue1.cmbPendingReqList)this.cmbSrfList.SelectedItem).srfno.Trim();
                if (srfno1 == "000000000000000000")
                {
                    return;
                }
                var ListSrfItm1 = this.PSrfItemList.FindAll(x => x.srfno == srfno1);   // no need
                ////var listviewitem = new List<vmEntryStoreIssue1.ListViewItemSelect>();   // Avoid this
                ///var listviewitem = new List<ListViewItemSelect2>();   // Avoid this
                int slno = 1;

                foreach (var item1a in ListSrfItm1)
                {
                    var stkinfo1 = this.InvItemSumList.FindAll(x => x.sircode == item1a.rsircode).ToList();
                    decimal stkqty1 = (stkinfo1.Count > 0 ? stkinfo1[0].stkqty : 0.00m);

                    var item1mi = this.ListStorIsueSumm1.FindAll(x => x.rsircode == item1a.rsircode).ToList();
                    decimal MissueQty = (item1mi.Count > 0 ? item1mi[0].trnqty : 0.00m);
                    ////listviewitem.Add(new vmEntryStoreIssue1.ListViewItemSelect
                    this.ListviewitemSelect2.Add(new vmEntryStoreIssue1.ListViewItemSelect2
                    {
                        invcode = "000000000000",
                        slnum = slno,
                        mark1 = stkqty1 <= 0 ? false : item1a.mark1,// true,
                        trcode = item1a.rsircode,
                        trdesc = item1a.sirdesc,
                        trdesc1 = item1a.rsircode + " - " + item1a.sirdesc.Trim() + " [" + item1a.sirunit.Trim() + "]\n[Stock: " + stkqty1.ToString("#,##0.00")
                                + ", Bal.Req: " + item1a.srqbalqty.ToString("#,##0.00") + " , Issue: " + item1a.prvsirqty.ToString("#,##0.00") + ", Month.Issue: "
                                + MissueQty.ToString("#,##0.00") + "]",
                        trunit = item1a.sirunit,
                        stokqty = stkqty1,
                        srfno = item1a.srfno,
                        srfno1 = item1a.srfno1,
                        srfqty = item1a.srfqty,//item1a.srfqty
                        prvsirqty = item1a.prvsirqty,
                        srqbalqty = item1a.srqbalqty,
                        MissueQty = MissueQty,
                        siruid = "",
                        markable1 = (stkqty1 > 0)
                        //invcode = "000000000000",
                        //trcode = item1a.rsircode,
                        //trdesc = item1a.sirdesc,
                        //trdesc1 = item1a.rsircode + " - " + item1a.sirdesc.Trim() + " [" + item1a.sirunit.Trim() + "]\n[Stock: " + stkqty1.ToString("#,##0.00")
                        //        + ", Bal.Req: " + item1a.srqbalqty.ToString("#,##0.00") + " , Issue: " + item1a.prvsirqty.ToString("#,##0.00") + ", Month.Issue: "
                        //        + MissueQty.ToString("#,##0.00") + "]",
                        //trunit = item1a.sirunit,
                        //stokqty = stkqty1,
                        //srfqty = item1a.srfqty,
                        //prvsirqty = item1a.prvsirqty,
                        //srqbalqty = item1a.srqbalqty
                    });
                    slno++;
                }
                this.dgvIssue1.ItemsSource = this.ListviewitemSelect2;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void cmbSrfList_SelectionChanged_Old(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbSrfList.Items.Count == 0)
                    return;

                this.txtSirQty.Text = "";
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtUID.Text = "";

                string srfno1 = ((ComboBoxItem)this.cmbSrfList.SelectedItem).Tag.ToString().Trim();
                if (srfno1 == "000000000000000000")
                {
                    return;
                }
                var ListSrfItm1 = this.PSrfItemList.FindAll(x => x.srfno == srfno1);
                var listviewitem = new List<vmEntryStoreIssue1.ListViewItemSelect>();
                this.dgvIssue1.ItemsSource = ListSrfItm1;
                foreach (var item1a in ListSrfItm1)
                {
                    var stkinfo1 = this.InvItemSumList.FindAll(x => x.sircode == item1a.rsircode).ToList();
                    decimal stkqty1 = (stkinfo1.Count > 0 ? stkinfo1[0].stkqty : 0.00m);

                    var item1mi = this.ListStorIsueSumm1.FindAll(x => x.rsircode == item1a.rsircode).ToList();
                    decimal MissueQty = (item1mi.Count > 0 ? item1mi[0].trnqty : 0.00m);
                    var item1 = new vmEntryStoreIssue1.ListViewItemSelect
                    {
                        invcode = "000000000000",
                        trcode = item1a.rsircode,
                        trdesc = item1a.sirdesc,
                        trdesc1 = item1a.rsircode + " - " + item1a.sirdesc.Trim() + " [" + item1a.sirunit.Trim() + "]\n[Stock: " + stkqty1.ToString("#,##0.00")
                                + ", Bal.Req: " + item1a.srqbalqty.ToString("#,##0.00") + " , Issue: " + item1a.prvsirqty.ToString("#,##0.00") + ", Month.Issue: "
                                + MissueQty.ToString("#,##0.00") + "]",
                        trunit = item1a.sirunit,
                        stokqty = stkqty1,
                        srfqty = item1a.srfqty,
                        prvsirqty = item1a.prvsirqty,
                        srqbalqty = item1a.srqbalqty
                    };

                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void txtCodeDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.lstItem.ItemsSource = null;
                string StrDesc1 = this.txtRSirDesc.Text.Trim().ToUpper();
                if (StrDesc1.Length == 0)
                    return;
                var List1a = this.InvItemSumList.FindAll(x => x.sirdesc.ToUpper().Contains(StrDesc1)).ToList();
                if (this.chkZeroStock.IsChecked == false)
                    List1a = List1a.FindAll(x => x.stkqty > 0).ToList();

                this.lstItem.ItemsSource = List1a;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-17: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }
        private void PrepareListViewData()
        {
            try
            {
                string ConCod1 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString().Trim();

                this.txtUID.Text = "";
                this.txtRSirCode.Text = "";
                string StrDesc1 = this.txtRSirDesc.Text.Trim().ToUpper();
                if (StrDesc1.Length == 0)
                    return;

                var InvItemSumList1a = this.InvItemSumList.FindAll(x => (x.sircode + x.sirdesc.ToUpper().Trim()).Contains(StrDesc1));
                if (this.chkZeroStock.IsChecked == false)
                {
                    InvItemSumList1a = InvItemSumList1a.FindAll(x => x.stkqty > 0);
                }

                var List1a = (from lst in InvItemSumList1a
                              where (lst.sircode + lst.sirdesc.ToUpper().Trim()).Contains(StrDesc1)
                              select new
                              {
                                  trcode = lst.sircode,
                                  trdesc = lst.sirdesc,
                                  truid = lst.siruid,
                                  trunit = lst.sirunit,
                                  stkqty = lst.stkqty,
                                  srfqty = 0,
                                  sirqty = 0,
                                  srqbalqty = 0
                              });


                foreach (var item1b in List1a)
                {
                    var item1mi = this.ListStorIsueSumm1.FindAll(x => x.rsircode == item1b.trcode);
                    decimal MissueQty = (item1mi.Count > 0 ? item1mi[0].trnqty : 0.00m);
                    var itm1a = new vmEntryStoreIssue1.ListViewItemSelect
                    {
                        trcode = item1b.trcode,
                        trdesc = item1b.trdesc,
                        //trdesc1 = item1b.trcode + " [Stock: " + item1b.stkqty.ToString("#,##0.00") + ", B.Req: " + item1b.srqbalqty.ToString("#,##0.00") + " " + item1b.trunit.Trim() + ", S.Iss: " + item1b.sirqty.ToString("#,##0.00") + "]\n" + item1b.trdesc,
                        trdesc1 = item1b.trcode + " - " + item1b.trdesc.Trim() + "\n[Stock: " + item1b.stkqty.ToString("#,##0.00") + (ConCod1 == "000000000000" ? "" : ", Bal.Req: "
                                  + item1b.srqbalqty.ToString("#,##0.00") + " " + item1b.trunit.Trim() + ", Issue: " + item1b.sirqty.ToString("#,##0.00") + ", Month.Issue: " + MissueQty.ToString("#,##0.00")) + "]",
                        siruid = item1b.truid,
                        trunit = item1b.trunit,
                        stokqty = item1b.stkqty,
                        srfqty = item1b.srfqty,
                        prvsirqty = item1b.sirqty,
                        srqbalqty = item1b.srqbalqty
                    };
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
  



        public void ListView1_DtaShow()
        {
            try
            {
                if (this.dgvIssue1.SelectedItem == null)
                    return;
                this.manualTextChange = false;
                string ConCod1 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString().Trim();


                var lvi1 = (vmEntryStoreIssue1.ListViewItemSelect2)this.dgvIssue1.SelectedItem;
                if (lvi1.stokqty <= 0 && ConCod1 != "000000000000")
                    return;

                string srfno1 = ((vmEntryStoreIssue1.cmbPendingReqList)this.cmbSrfList.SelectedItem).srfno.Trim(); //((ComboBoxItem)this.cmbSrfList.SelectedItem).Tag.ToString().Trim();

                if (lvi1.srqbalqty <= 0 && srfno1 != "000000000000000000")
                    return;

                this.txtRSirDesc.Text = lvi1.trdesc;
                this.txtRSirCode.Text = lvi1.trcode;
                this.txtUID.Text = lvi1.siruid;
                this.lblUnit1.Content = lvi1.trunit;
                this.lblStkBalDes.Content = "Stock: " + lvi1.stokqty.ToString("#,##0.00") + (ConCod1 == "000000000000" || srfno1 == "000000000000000000" ? "" : ", B.Req: "
                    + lvi1.srqbalqty.ToString("#,##0.00"));
                this.txtRSirDesc.Focus();
                //this.txtRSirCode.Focus();         
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-18: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
                this.dgvIssue.ItemsSource = null;
                var srfno1a = "000000000000000000";
                var trcode1 = this.txtRSirDesc.Tag.ToString();
                var trdesc1 = this.txtRSirDesc.Text.Trim();
                var trunit1 = this.lblUnit1.Content.ToString().Trim();
                var siruid1 = this.txtUID.Text.Trim();
                decimal stokqty1 = decimal.Parse("0" + this.txtSirQty.Tag.ToString().Trim());
                decimal sirqty1 = decimal.Parse("0" + this.txtSirQty.Text.Trim());
                int slnum1 = this.ListViewItemTable1.Count() + 1;
                if (this.ListViewItemTable1.FindAll(x => x.srfno == srfno1a && x.rsircode == trcode1).Count == 0)
                {
                    this.ListViewItemTable1.Add(new vmEntryStoreIssue1.ListViewItemTable
                    {
                        invcode = "000000000000",
                        prvsirqty = 0.00m,
                        rsircode = trcode1,
                        sirqty = (stokqty1 < sirqty1 ? stokqty1 : sirqty1),
                        siruid = siruid1,
                        srfno = srfno1a,
                        srfno1 = "",
                        srfqty = 0,
                        stokqty = stokqty1,
                        trdesc = trdesc1,
                        trsl = slnum1.ToString(),
                        trunit = trunit1
                    });
                }
                this.dgvIssue.ItemsSource = this.ListViewItemTable1;
                return;




                //--------------------------------------------------
                this.dgvIssue.ItemsSource = null;
                string ConCod1 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString().Trim();
                string rsircode1a = this.txtRSirCode.Text.Trim();
                foreach (var item1s in this.dgvIssue1.Items)
                {
                    if (((vmEntryStoreIssue1.ListViewItemSelect2)item1s).trcode == rsircode1a)
                    {
                        this.lstItem.SelectedItem = item1s;
                        break;
                    }

                }

                var lvi11 = (vmEntryStoreIssue1.ListViewItemSelect2)this.dgvIssue1.SelectedItem;


                if (lvi11 == null)
                    return;

                string lblStkBalDes1 = this.lblStkBalDes.Content.ToString();
                if (lblStkBalDes1.Length <= 0)
                {
                    return;
                }
                decimal srfqty1a = this.validData("0" + this.txtSirQty.Text.Trim());
                //string srfno1 = ((ComboBoxItem)this.cmbSrfList.SelectedItem).Tag.ToString().Trim();
                string srfno1 = ((vmEntryStoreIssue1.cmbPendingReqList)this.cmbSrfList.SelectedItem).srfno.Trim();

                if (ConCod1 != "000000000000")
                {
                    if (srfno1 != "000000000000000000" && (srfqty1a > lvi11.srqbalqty || srfqty1a <= 0))
                    {
                        this.txtSirQty.Focus();
                        return;
                    }
                    else if (srfqty1a > lvi11.stokqty)
                    {
                        return;
                    }
                }

                if (this.txtRSirDesc.Text.Trim().Length == 0)
                {
                    this.txtRSirCode.Text = "";
                    this.txtUID.Text = "";
                    this.lblUnit1.Content = "";
                    this.txtSirQty.Text = "";
                }

                int serialno1 = this.ListViewItemTable1.Count + 1;
                string invcod1 = "000000000000";
                string rsircode1 = this.txtRSirCode.Text.Trim();
                string rsirdesc1 = this.txtRSirDesc.Text.Trim();

                string srfnum1 = ((vmEntryStoreIssue1.cmbPendingReqList)this.cmbSrfList.SelectedItem).srfno.Trim();
                string siruid1a = this.txtUID.Text.Trim();
                string rsirunit = this.lblUnit1.Content.ToString();
                if (rsircode1.Length == 0)
                    return;
                bool ItemFound1 = false;
                foreach (var item1l in this.dgvIssue1.Items)
                {
                    if (((vmEntryStoreIssue1.ListViewItemSelect2)item1l).trcode == rsircode1)
                    {
                        ItemFound1 = true;
                        break;
                    }
                }

                if (!ItemFound1)
                {
                    this.txtRSirCode.Text = "";
                    this.txtUID.Text = "";
                    this.lblUnit1.Content = "";
                    this.txtSirQty.Text = "";
                    return;
                }
                var list1a = this.ListViewItemTable1.FindAll(x => x.rsircode == rsircode1 && x.srfno == srfnum1);
                if (list1a.Count > 0)
                {
                    //System.Windows.MessageBox.Show("Item : " + rsirdesc1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    //return;

                    list1a[0].invcode = rsircode1;// invcod1; // DateTime.Now.ToString("yy") + m1[DateTime.Now.Month] + d1[DateTime.Now.Day] +  DateTime.Now.ToString("HHmmss") + new Random().Next(11, 99).ToString().Trim(), // "000000000000", //Year(2)+Month(1)+Day(1)+Hour(2)+Minute(2)+Second(2)+Rand(2)
                    list1a[0].trdesc = rsirdesc1 + (ConCod1 == "000000000000" ? "" : "\n" +
                                 (srfno1 == "000000000000000000" ? "Issue Without Requisition" : ((ComboBoxItem)this.cmbSrfList.SelectedItem).Content.ToString().Substring(13) + ": [" + lblStkBalDes1 + "]"));
                    list1a[0].sirqty = srfqty1a;
                    list1a[0].srfno = srfnum1; //  "SR" + m1[DateTime.Now.Month] + d1[DateTime.Now.Day] +  DateTime.Now.ToString("HHmmss") + new Random().Next(11, 99).ToString().Trim(),
                    list1a[0].srfno1 = ((ComboBoxItem)this.cmbSrfList.SelectedItem).Content.ToString().Substring(13);
                    list1a[0].siruid = siruid1a;
                    list1a[0].trunit = rsirunit;
                }
                else
                {
                    //string[] m1 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
                    //string[] d1 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5" };

                    var item1a = new vmEntryStoreIssue1.ListViewItemTable()
                    {
                        trsl = serialno1.ToString() + ".",
                        invcode = rsircode1,//invcod1, // DateTime.Now.ToString("yy") + m1[DateTime.Now.Month] + d1[DateTime.Now.Day] +  DateTime.Now.ToString("HHmmss") + new Random().Next(11, 99).ToString().Trim(), // "000000000000", //Year(2)+Month(1)+Day(1)+Hour(2)+Minute(2)+Second(2)+Rand(2)
                        rsircode = rsircode1,
                        trdesc = rsirdesc1 + (ConCod1 == "000000000000" ? "" : "\n" +
                                 (srfno1 == "000000000000000000" ? "Issue Without Requisition" : ((ComboBoxItem)this.cmbSrfList.SelectedItem).Content.ToString().Substring(13) + ": [" + lblStkBalDes1 + "]")),
                        sirqty = srfqty1a,
                        srfno = srfnum1, //  "SR" + m1[DateTime.Now.Month] + d1[DateTime.Now.Day] +  DateTime.Now.ToString("HHmmss") + new Random().Next(11, 99).ToString().Trim(),
                        srfno1 = ((ComboBoxItem)this.cmbSrfList.SelectedItem).Content.ToString().Substring(13),
                        siruid = siruid1a,
                        trunit = rsirunit,
                        srfqty = 0.00m,
                        stokqty = 0.00m,
                        prvsirqty = 0.00m
                    };
                    var lstbcd = this.ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
                    decimal qtyst = lstbcd.Sum(x => x.sirqty) + (srfno1 != "000000000000000000" ? srfqty1a : 0);
                    if (qtyst > lvi11.stokqty)
                    {
                        return;
                    }

                    if (srfno1 != "000000000000000000")
                    {
                        var lstab = this.ListViewItemTable1.FindAll(x => x.srfno == item1a.srfno && x.rsircode == rsircode1);
                        decimal qtyy = lstab.Sum(x => x.sirqty) + srfqty1a;

                        if (qtyy > lvi11.srqbalqty)
                        {
                            return;
                        }
                    }

                    this.ListViewItemTable1.Add(item1a);
                    //ListViewItemTable1.Sort(delegate(vmEntryStoreIssue1.ListViewItemTable x, vmEntryStoreIssue1.ListViewItemTable y)
                    //{
                    //    return x.rsircode.CompareTo(y.rsircode);
                    //});
                }
                this.dgvIssue.ItemsSource = this.ListViewItemTable1;
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtUID.Text = "";
                this.lblUnit1.Content = "";
                this.txtSirQty.Text = "";
                this.lblStkBalDes.Content = "";
                this.lstItem.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-19: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
                    this.PrintReqMemo(item1a.memonum);
                }
                else if (this.rb3TableRecoredspr.IsChecked == true)
                {
                    var list1 = this.ListViewTransTable1;
                    var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();
                    list3.Add(new HmsEntityGeneral.ReportGeneralInfo()
                    {
                        RptCompName = WpfProcessAccess.CompInfList[0].comnam,
                        RptCompAdd1 = WpfProcessAccess.CompInfList[0].comadd1,
                        RptCompAdd2 = WpfProcessAccess.CompInfList[0].comadd2,
                        RptFooter1 = "Print Source: " + WpfProcessAccess.SignedInUserList[0].terminalID + ", " +
                                     WpfProcessAccess.SignedInUserList[0].signinnam + ", " +
                                     WpfProcessAccess.SignedInUserList[0].sessionID + ", " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")

                    });
                    rpt1 = StoreReportSetup.GetLocalReport("Store.RptTransectionList", list1, null, list3); // ( R_01_RptSetup.RptSetupItemList1(ds1, ds2);          
                    WindowTitle1 = "Store Issue Transaction List";
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
                System.Windows.MessageBox.Show("Issue-23: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void PrintReqMemo(string MemoNum1 = "XXXXXXXXXXX")
        {
            try
            {
                this.UnCheckedAllPopups();
                LocalReport rpt1 = null;
                string WindowTitle1 = "";

                var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, MemoNum1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.StoreIssueMemo>();
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

                rpt1 = StoreReportSetup.GetLocalReport("Store.RptIssueMemo01", list1, list2, list3);

                WindowTitle1 = "Store Issue Memo";

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
                System.Windows.MessageBox.Show("Issue-23: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            var item1a = this.lblSrfNo.Tag.ToString();
            this.PrintReqMemo(item1a);
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.gridTransList.IsEnabled = false;
            this.BuildTransactionList();
            this.gridTransList.IsEnabled = true;
        }

        private void cmdCancelIssueMemo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string mrrnum = this.lvTransList.SelectedItem
                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to cancel", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var item1a = (HmsEntityInventory.InvTransectionList)this.dgvTransList.SelectedItem;

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this Store Issue Memo " + item1a.memonum1, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;

                int itemno1 = this.dgvTransList.SelectedIndex;
                var pap1 = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum, "CANCEL", "MESSAGE");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                ((HmsEntityInventory.InvTransectionList)this.dgvTransList.Items[itemno1]).MemoStatus = "C";
                this.ListViewTransTable1[itemno1].MemoStatus = "C";
                this.dgvTransList.Items.Refresh();
                //AccTrnLst
                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-25: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void cmdEditIssueMemo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dgvIssue.ItemsSource = null;
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
                DateTime date1 = Convert.ToDateTime(dr0["memodate"]);
                ////this.xctk_dtpSrfDat.Maximum = Convert.ToDateTime("01-" + date1.ToString("MMM-yyyy")).AddMonths(1).AddDays(-1);
                ////this.xctk_dtpSrfDat.Minimum = Convert.ToDateTime("01-" + date1.ToString("MMM-yyyy"));
                this.xctk_dtpSrfDat.Value = Convert.ToDateTime(dr0["memodate"]);

                int x = 0;
                foreach (ComboBoxItem item in this.cmbSectCod.Items)
                {
                    if (item.Tag.ToString() == dr0["sectcod"].ToString())
                        break;
                    ++x;
                }
                this.cmbSectCod.SelectedIndex = x;
                int y = 0;
                foreach (ComboBoxItem item in this.cmbSectCod2.Items)
                {
                    if (item.Tag.ToString() == dr0["sectcod2"].ToString())
                        break;
                    ++y;
                }
                this.cmbSectCod2.SelectedIndex = y;

                ////this.btnOk_Click(null, null);

                this.txtblEditMode.Visibility = Visibility.Visible;

                this.lblSrfNo.Content = dr0["memonum1"].ToString();
                this.lblSrfNo.Tag = dr0["memonum"].ToString();


                this.autoIssueByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoRecByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;

                this.autoIssueByStaffSearch.SelectedValue = dr0["PreparById"].ToString().Trim();
                this.autoRecByStaffSearch.SelectedValue = dr0["recvbyid"].ToString().Trim();


                this.btnOk_Click(null, null);

                this.preparebyid1 = dr2["preparebyid"].ToString();// WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = dr2["prepareses"].ToString();// WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = dr2["preparetrm"].ToString();// WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = Convert.ToDateTime(dr2["rowtime"]);


                this.txtissRefpr.Text = dr0["Referance"].ToString().Trim();
                this.txtSrfNar.Text = dr0["Naration"].ToString().Trim();
                this.ListViewItemTable1.Clear();
                foreach (DataRow item in this.EditDs.Tables[1].Rows)
                {
                    var item1b = new vmEntryStoreIssue1.ListViewItemTable()
                    {
                        trsl = item["slnum"].ToString().Trim() + ".",
                        invcode = item["invcode"].ToString().Trim(),
                        rsircode = item["rsircode"].ToString().Trim(),
                        siruid = "",
                        trdesc = item["sirdesc"].ToString().Trim(),
                        sirqty = Convert.ToDecimal(item["sirqty"]),
                        trunit = item["sirunit"].ToString().Trim(),
                        srfno = item["srfno"].ToString().Trim(),
                        srfno1 = item["srfno1"].ToString().Trim(),
                        srfqty = 0.00m,
                        stokqty = 0.00m,
                        prvsirqty = 0.00m
                    };
                    this.ListViewItemTable1.Add(item1b);
                }

                this.dgvIssue.ItemsSource = this.ListViewItemTable1;

                foreach (var item1s in this.InvItemSumList)
                {
                    var sirqty1 = this.ListViewItemTable1.FindAll(z => z.rsircode == item1s.sircode).Sum(a => a.sirqty);
                    item1s.stkqty = item1s.stkqty + sirqty1;
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

                this.btnUpdate.Visibility = Visibility.Visible;

                //-------------------------
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-26: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void cmbSectCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbSectCod.Items.Count == 0)
                    return;

                if (this.chkAutoTransList.IsChecked == true)
                {
                    this.btnFilter_Click(null, null);
                    //   this.BuildTransactionList();
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Issue-27: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void autoRecByStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStaffRefSirdesc(args.Pattern);
        }



        private void autoIssueByStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
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

            string memonum1 = this.lblSrfNo.Tag.ToString();
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
            autitem.auhcid = this.autoIssueByStaffSearch.SelectedValue.ToString();
            autitem.auhcnam = this.autoIssueByStaffSearch.SelectedText.Trim();
            autitem.austat = "Y";
            autitem.statdes = "Prepared";
            autitem.aunote = this.txtSrfNar.Text.Trim();
            autitem.autime = this.rowtime1; //DateTime.Parse(this.xctk_dtpSrfDat.Text.Trim());
            autitem.luhcid = this.preparebyid1;
            autitem.luses = this.prepareses1;
            autitem.lutrm = this.preparetrm1;
            autitem.lutime = this.rowtime1;
            autitem.autitle = "Issued By";

            var autitem2 = this.AuthorizeTable1.Find(x => x.aucode == "CBALCOD01001");
            autitem2.auhcid = this.autoRecByStaffSearch.SelectedValue.ToString();
            autitem2.auhcnam = this.autoRecByStaffSearch.SelectedText.Trim();
            autitem2.austat = "Y";
            autitem2.statdes = "Checked";
            autitem2.aunote = this.txtSrfNar.Text.Trim();
            autitem2.autime = this.rowtime1; //DateTime.Parse(this.xctk_dtpSrfDat.Text.Trim());
            autitem2.luhcid = this.preparebyid1;
            autitem2.luses = this.prepareses1;
            autitem2.lutrm = this.preparetrm1;
            autitem2.lutime = this.rowtime1;
            autitem2.autitle = "Received By";
        }
        private void chkAuthorize_Click(object sender, RoutedEventArgs e)
        {
            if (this.chkAuthorize.IsChecked == true)
                this.ShowHideAuthorizationPanle();
        }


        private void ShowHideAuthorizationPanle()
        {
            this.InitializeAuthorization();
            this.lblAuthorizeMemoDesc.Content = "Date: " + this.xctk_dtpSrfDat.Text.Trim() + ", S.I.R. No: " + this.lblSrfNo.Content.ToString();
            this.lblAuthorizeMemoDesc.Tag = this.lblSrfNo.Tag.ToString();
            this.stkItem.Visibility = Visibility.Collapsed;

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
            this.btnUpdate_Click(null, null);
            this.frameAuthorise.Continue = false; // un-blocks gui message pump
        }

        private void btnEditAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.cmdEditIssueMemo_Click(null, null);
            if (this.EditDs == null)
                return;

            this.btnUpdate.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;
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

        private void btnAddAllRecord_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgvIssue1.ItemsSource == null || this.ListviewitemSelect2.Count == 0)
                return;

            var list2a = this.ListviewitemSelect2.FindAll(x => x.mark1 == true).ToList();
            if (list2a.Count <= 0)
                return;

            this.dgvIssue.ItemsSource = null;
            if (this.multiReqAdd == false)
                this.ListViewItemTable1.Clear();

            if (this.autoRecByStaffSearch.SelectedValue == null)
            {
                var list2b = this.ListPendingReq.FindAll(x => x.srfno == list2a[0].srfno).ToList();
                if (list2b.Count > 0)
                {
                    this.autoRecByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                    this.autoRecByStaffSearch.SelectedValue = list2b[0].srfbyid;
                }
            }
            int slnum1 = this.ListViewItemTable1.Count() + 1;
            foreach (var item1s in list2a)
            {
                if (this.ListViewItemTable1.FindAll(x => x.srfno == item1s.srfno && x.rsircode == item1s.trcode).Count == 0)
                {
                    this.ListViewItemTable1.Add(new vmEntryStoreIssue1.ListViewItemTable
                    {
                        invcode = "000000000000",
                        prvsirqty = item1s.prvsirqty,
                        rsircode = item1s.trcode,
                        sirqty = (item1s.stokqty < item1s.srqbalqty ? item1s.stokqty : item1s.srqbalqty),
                        siruid = item1s.siruid,
                        srfno = item1s.srfno,
                        srfno1 = item1s.srfno1,
                        srfqty = item1s.srfqty,
                        stokqty = item1s.stokqty,
                        trdesc = item1s.trdesc,
                        trsl = slnum1.ToString(),
                        trunit = item1s.trunit
                    });
                    slnum1++;
                }
            }
            this.dgvIssue.ItemsSource = this.ListViewItemTable1;
        }

  
        private void chkMulReqAdd_Click(object sender, RoutedEventArgs e)
        {
            this.chkMulReqAdd = ((CheckBox)sender);
            this.multiReqAdd = (this.chkMulReqAdd.IsChecked == true);
        }

        private void lstItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectItemInfo();
            this.txtSirQty.Focus();
        }

        private void lstItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectItemInfo();
                this.txtSirQty.Focus();
            }
        }
        private void SelectItemInfo()
        {
            var lbi1 = (vmEntryStoreIssue1.StockItemSumList)this.lstItem.SelectedItem;

            if (lbi1 == null)
                return;
            this.txtRSirCode.Text = lbi1.sircode;
            this.txtRSirDesc.Tag = lbi1.sircode;
            this.txtRSirDesc.Text = lbi1.sirdesc.Trim();
            this.lblUnit1.Content = lbi1.sirunit.Trim();
            this.txtSirQty.Tag = lbi1.stkqty.ToString();
            this.lblStkBalDes.Content = "Stock = " + lbi1.stkqty.ToString("#,##0.00;-#,##0.00; Nil");
        }
        private void txtSirQty_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }

        private void btnAddRecord_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
            decimal stock1 = decimal.Parse("0" + this.txtSirQty.Tag.ToString().Replace("-", "")) * (this.txtSirQty.Tag.ToString().Contains("-") ? -1 : 1);
            decimal issue1 =decimal.Parse("0" + this.txtSirQty.Text.ToString());
            decimal balanc1 = (stock1 - issue1);
            if (balanc1 <= 0 && this.chkZeroStock.IsChecked == false)
            {
                this.txtSirQty.Text = "";
                return;
            }
            this.lblStkBalDes.Content = "Stock = " + stock1.ToString("#,##0.00;-#,##0.00; Nil") + ", Issue = " + issue1.ToString("#,##0.00;-#,##0.00; Nil") + ", Balance = " + balanc1.ToString("#,##0.00;-#,##0.00; Nil");
        }
    }
   
}
