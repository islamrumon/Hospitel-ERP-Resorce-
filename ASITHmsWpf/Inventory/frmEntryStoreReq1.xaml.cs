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
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Inventory;
using ASITHmsRpt2Inventory;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using Xceed.Wpf.Toolkit;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.ComponentModel;


namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmEntryStoreReq1.xaml
    /// </summary>
    public partial class frmEntryStoreReq1 : UserControl
    {

        //	Master Table : dbo_scm.storreqb, Fields: {comcod, srfno, srfdat, srfbyid, sectcod, sectcod2, srfref, srfnar, srfstatus, rowid, rowtime}
        //	Details Table: dbo_scm.storreqa, Fields: {comcod, srfno, rsircode, srfqty, rowid}
        private bool FrmInitialized = false;
        public bool IsActiveTransListWindow { get; set; }
        private List<vmEntryStoreReq1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryStoreReq1.ListViewItemTable>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private List<HmsEntityGeneral.AuthorizeInf> AuthorizeTable1 = new List<HmsEntityGeneral.AuthorizeInf>();

        private vmEntryStoreReq1 vm1 = new vmEntryStoreReq1();
        private vmReportStore1 vm1r = new vmReportStore1();
        private DataSet EditDs;

        private string preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
        private string prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
        private string preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
        private DateTime rowtime1 = DateTime.Now;

        private DispatcherFrame frameAuthorise;
        public frmEntryStoreReq1()
        {
            //	Master Table : dbo_scm.storreqb, Fields: {srfstatus, rowtime}
            //	Details Table: dbo_scm.storreqa, Fields: {rsircode, srfqty}

            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }
        private void ConstructAutoCompletionSource()
        {

            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
            foreach (var itemd1 in deptList1)
            {
                this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname.Trim(), Tag = itemd1.sectcod, ToolTip = itemd1.sectcod + " - " + itemd1.sectname.Trim() });


                if (itemd1.sectname.ToUpper().Contains("STORE"))
                {
                    this.cmbSectCod2.Items.Add(new ComboBoxItem() { Content = itemd1.sectname.Trim(), Tag = itemd1.sectcod, ToolTip = itemd1.sectcod + " - " + itemd1.sectname.Trim() });
                }
            }
            this.cmbSectCod2f.Items.Add(new ComboBoxItem() { Content = "Selected Store", Tag = "AAAAAAAAAAAA", ToolTip = "AAAAAAAAAAAA - Selected Store" });
            this.cmbSectCod2f.Items.Add(new ComboBoxItem() { Content = "All Stores", Tag = "000000000000", ToolTip = "000000000000 - All Stores" });

            this.cmbSrfByStaffFind.Items.Add(new ComboBoxItem() { Content = "All Users", Tag = "000000000000", ToolTip = "000000000000 - All Users" });
            this.cmbSrfByStaffFind.Items.Add(new ComboBoxItem() { Content = "Selected User", Tag = "AAAAAAAAAAAA", ToolTip = "AAAAAAAAAAAA - Selected User" });

            var divList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) == "000" && x.sectcod.Substring(7, 5) != "00000");
            this.cmbSectDivCod.Items.Add(new ComboBoxItem() { Content = "Selected Department", Tag = "AAAAAAAAAAAA", ToolTip = "AAAAAAAAAAAA - Selected Department" });
            this.cmbSectDivCod.Items.Add(new ComboBoxItem() { Content = "All Departments", Tag = "000000000000", ToolTip = "000000000000 - All Departments" });
            foreach (var itemv1 in divList1)
            {
                this.cmbSectDivCod.Items.Add(new ComboBoxItem() { Content = itemv1.sectname, Tag = itemv1.sectcod, ToolTip = itemv1.sectcod + " - " + itemv1.sectname.Trim() });
            }

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;
                this.IsActiveTransListWindow = false;
                this.ListViewItemTable1.Clear();
                this.ConstructAutoCompletionSource();

                this.chkAutoTransList.IsChecked = this.IsActiveTransListWindow;
                this.btnPrint2.Visibility = Visibility.Hidden;
                this.chkPrintWithStock.Visibility = Visibility.Hidden;
                this.btnUpdate.Visibility = Visibility.Hidden;
                this.gridDetails.Visibility = Visibility.Collapsed;
                this.GridItemList.Visibility = Visibility.Collapsed;
                this.gridAuthorize.Visibility = Visibility.Collapsed;
                this.lstItem.Items.Clear();
                this.xctk_dtpSrfDat.Value = DateTime.Today;
                this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-15);
                this.xctk_dtpToDate.Value = DateTime.Today;
                this.autoSrfByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoSrfByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                if (IsActiveTransListWindow)
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


        private void ActivateAuthObjects()
        {

            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreReq1", _uitype: "Form", _uidesc: "Item Requisition By Department To Store - Entry/Edit Screen"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreReq1_chkDateBlocked", _uitype: "CheckBox", _uidesc: "->> Store. Requisition Entry Date Select"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreReq1_chkAutoTransList", _uitype: "CheckBox", _uidesc: "->> Store Requisition List Display on Entry"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreReq1_btnEditSrf", _uitype: "Button", _uidesc: "->> Store Requisition Edit"));
            //uiObjInfoList.Add(new uiObjInfo(_moduleid: "Inventory", _uicode: "WPF_frmEntryStoreReq1_btnCancelSrf", _uitype: "Button", _uidesc: "->> Store Requisition Cancel (Delete)"));

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryStoreReq1_chkDateBlocked") == null)
            {
                this.chkDateBlocked.Visibility = Visibility.Collapsed;
                this.lblDateBlocked.Visibility = Visibility.Visible;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryStoreReq1_chkAutoTransList") == null)
                this.chkAutoTransList.Visibility = Visibility.Hidden;

            this.btnRecurring.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryStoreReq1_btnEditSrf") == null)
                this.btnEditSrf.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryStoreReq1_btnCancelSrf") == null)
                this.btnCancelSrf.Visibility = Visibility.Hidden;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            this.txtblEditMode.Visibility = Visibility.Hidden;
            this.btnPrint2.Visibility = Visibility.Hidden;
            this.chkPrintWithStock.Visibility = Visibility.Hidden;
            this.chkPrintWithStock.IsChecked = false;
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Collapsed;
            this.GridItemList.Visibility = Visibility.Collapsed;
            this.dgReqList.ItemsSource = null;
            this.ListViewItemTable1.Clear();
            this.xctk_dtpSrfDat.IsEnabled = false;
            if (this.btnOk.Content.ToString() == "_New")
            {
                this.EditDs = null;
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.chkAutoTransList.IsEnabled = true;
                this.cmbSectCod.IsEnabled = true;
                this.cmbSectCod2.IsEnabled = true;
                ////this.stkIntro.IsEnabled = true;
                this.autoSrfByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoSrfByStaffSearch.SelectedValue = WpfProcessAccess.SignedInUserList[0].hccode;
                this.preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = DateTime.Now;
                this.txtSrfRef.Text = "";
                this.txtSrfNar.Text = "";
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtSrfQty.Text = "";
                this.lblUnit1.Content = "";
                this.lblSrfNo.Content = "SRQMM-CCCC-XXXXX";
                this.lblSrfNo.Tag = "SRQYYYYMMCCCCXXXXX";
                if (IsActiveTransListWindow)
                {
                    this.BuildTransactionList();
                    this.gridTransList.Visibility = Visibility.Visible;
                    this.chkPrintWithStock.Visibility = Visibility.Visible;
                    this.gridTransList.IsEnabled = true;
                    this.dgvTransList.Focus();
                    //this.lvTransList.Focus();
                }
                this.btnOk.Content = "_Ok"; // new AccessText { Text = "_Ok" };//  Content = new AccessText { Text = "_Label" };
                return;
            }

            if (this.checkOkValidation() == false)
                return;

            this.btnUpdate.Visibility = Visibility.Visible;
            this.gridTransList.Visibility = Visibility.Collapsed;
            this.gridTransList.IsEnabled = false;
            this.gridDetails.Visibility = Visibility.Visible;
            this.chkDateBlocked.IsChecked = false;
            this.chkAutoTransList.IsEnabled = false;
            this.btnUpdate.IsEnabled = true;
            this.stkpAddItem.IsEnabled = true;
            this.cmbSectCod.IsEnabled = false;
            this.cmbSectCod2.IsEnabled = false;
            ////this.stkIntro.IsEnabled = false;
            this.btnOk.Content = "_New"; //new AccessText { Text = "_New" };// "_New";

            this.dgReqList.ItemsSource = this.ListViewItemTable1;

        }

        private bool checkOkValidation()
        {
            if (this.autoSrfByStaffSearch.SelectedValue == null)
                return false;

            string srfByID1 = this.autoSrfByStaffSearch.SelectedValue.ToString();
            if (srfByID1.Length < 0)
                return false;

            var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == srfByID1);
            return (listStaff1.Count > 0);
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            var item1a = this.lblSrfNo.Tag.ToString();
            this.PrintReqMemo(item1a);
        }


        private void PrintReqMemo(string MemoNum1 = "XXXXXXXXXXX")
        {
            var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, MemoNum1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.StoreReqMemo>();
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

            list3[0].RptParVal1 = (dr1.Length > 0 ? dr1[0]["auhcnam"].ToString().Replace(",","\n") : "");
            list3[0].RptParVal2 = (dr2.Length > 0 ? dr2[0]["auhcnam"].ToString().Replace(",", "\n") : "");
            list3[0].RptParVal3 = (dr3.Length > 0 ? dr3[0]["auhcnam"].ToString().Replace(",", "\n") : "");
            list3[0].RptParVal4 = (dr4.Length > 0 ? dr4[0]["auhcnam"].ToString().Replace(",", "\n") : "");

            LocalReport rpt1 = StoreReportSetup.GetLocalReport("Store.RptStoreReqMemo01", list1, list2, list3);
            if (rpt1 == null)
                return;

            string WindowTitle1 = "Store Requisition Memo";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
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
            this.GridItemList.Visibility = Visibility.Collapsed;
            decimal srfqty1a = this.validData("0" + this.txtSrfQty.Text.Trim());
            if (srfqty1a <= 0)
            {
                this.txtSrfQty.Focus();
                return;
            }


            if (this.txtRSirDesc.Text.Trim().Length == 0)
            {
                this.txtUID.Text = "";
                this.txtRSirCode.Text = "";
                this.lblUnit1.Content = "";
                this.txtSrfQty.Text = "";
            }


            int serialno1 = this.ListViewItemTable1.Count + 1;
            string truid1 = this.txtUID.Text.Trim();
            string rsircode1 = this.txtRSirDesc.Tag.ToString().Trim(); //this.txtRSirCode.Text.Trim();
            var List1a = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == rsircode1);
            if (List1a.Count == 0)
                return;

            string rsirdesc1 = List1a[0].sirdesc.Trim();  // this.txtRSirDesc.Text.Trim();

            string rsirunit = List1a[0].sirunit.Trim();// this.lblUnit1.Content.ToString();
            if (rsircode1.Length == 0)
                return;

            var list1a = this.ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
            if (list1a.Count > 0)
            {
                //System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                //return;

                list1a[0].srfqty = srfqty1a;
                list1a[0].srfqty1 = srfqty1a.ToString("#,##0.00");
            }
            else
            {
                var item1a = new vmEntryStoreReq1.ListViewItemTable()
                        {
                            trsl = serialno1.ToString() + ".",
                            rsircode = rsircode1,
                            trdesc = rsirdesc1, //rsircode1 + ": " + rsirdesc1,
                            srfqty = srfqty1a,
                            srfqty1 = srfqty1a.ToString("#,##0.00"),
                            truid = truid1,
                            trunit = rsirunit
                        };

                this.ListViewItemTable1.Add(item1a);
            }
            //this.ListViewItemTable1.Sort(delegate(vmEntryStoreReq1.ListViewItemTable x, vmEntryStoreReq1.ListViewItemTable y)
            //{
            //    return x.rsircode.CompareTo(y.rsircode);
            //});

            this.txtUID.Text = "";
            this.txtRSirCode.Text = "";
            this.txtRSirDesc.Text = "";
            this.lblUnit1.Content = "";
            this.txtSrfQty.Text = "";
            this.dgReqList.Items.Refresh();
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            if (this.dgvTransList.SelectedItem == null) //(this.lvTransList.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("No record found to view/print report", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            LocalReport rpt1 = null;

            string WindowTitle1 = "";
            if (this.rb3SelectedMemo.IsChecked == true)
            {

                var item1a = (HmsEntityInventory.InvTransectionList)this.dgvTransList.SelectedItem; //(HmsEntityInventory.InvTransectionList)this.lvTransList.SelectedItem;
                this.PrintReqMemo(item1a.memonum);
            }
            else if (this.rb3TableRecoreds.IsChecked == true)
            {

                var list1 = this.ListViewTransTable1;
                var list3 = WpfProcessAccess.GetRptGenInfo();

                rpt1 = StoreReportSetup.GetLocalReport("Store.RptTransectionList", list1, null, list3);
                WindowTitle1 = "Store Requisition Transaction List";

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
        }

        private void UnCheckedAllPopups()
        {

            this.chkPrint2.IsChecked = false;
        }

        private void chkAutoTransList_Click(object sender, RoutedEventArgs e)
        {
            this.IsActiveTransListWindow = (this.chkAutoTransList.IsChecked == true);
            if (this.IsActiveTransListWindow && this.gridDetails.Visibility == Visibility.Collapsed)
            {
                this.BuildTransactionList();
                this.gridTransList.Visibility = Visibility.Visible;
                this.chkPrintWithStock.Visibility = Visibility.Visible;
                this.gridTransList.IsEnabled = true;
                this.dgvTransList.Focus();
                //this.lvTransList.Focus();
            }
            else if (this.IsActiveTransListWindow == false && this.gridDetails.Visibility == Visibility.Collapsed)
            {
                this.gridTransList.Visibility = Visibility.Collapsed;
                this.chkPrintWithStock.Visibility = Visibility.Hidden;
                this.gridTransList.IsEnabled = false;
            }

            this.chkPrint2.IsChecked = false;
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void txtCodeDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Visible;
        }
        private void txtCodeDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lstItem.ItemsSource = null;
            string StrDesc1 = this.txtRSirDesc.Text.Trim().ToUpper();
            if (StrDesc1.Length == 0)
                return;
            var List1a = WpfProcessAccess.InvItemList.FindAll(x => x.sirdesc.ToUpper().Contains(StrDesc1));
            this.lstItem.ItemsSource = List1a;
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpSrfDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpSrfDat.IsEnabled)
                this.xctk_dtpSrfDat.Focus();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
              MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            var ListViewItemTable1a = this.ListViewItemTable1.FindAll(x => x.srfqty > 0);
            if (ListViewItemTable1a.Count == 0)
            {
                return;
            }
            string EditSrfNum1 = (this.EditDs != null ? this.lblSrfNo.Tag.ToString() : "");
            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            string cbSectCode2 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();
            string SrfById1a = this.autoSrfByStaffSearch.SelectedValue.ToString();

            if (EditSrfNum1.Length == 18)
            {
                var pap1b = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, EditSrfNum1, "BACKUP", "MESSAGE");
                DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                if (ds1b == null)
                    return;
            }

            this.InitializeAuthorization(); // To update the prepared by record
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpSrfDat.Text), EditMemoNum1: EditSrfNum1, cbSectCode: cbSectCode1,
                cbSectCode2: cbSectCode2, srfByID1: SrfById1a, MemoRef1: this.txtSrfRef.Text.Trim(), MemoNar1: this.txtSrfNar.Text.Trim(), ListViewItemTable1: ListViewItemTable1a, 
                AuthorizeTable1: this.AuthorizeTable1, _preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);
            // _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            //String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamUpdateStoreReq(WpfProcessAccess.CompInfList[0].comcod, ds1, EditSrfNum1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblSrfNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString(); ;
            this.lblSrfNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();
            this.EditDs = new DataSet(); // For Current Authorization

            this.btnUpdate.IsEnabled = false;
            this.stkpAddItem.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;
            this.chkPrintWithStock.Visibility = Visibility.Visible;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.gridTransList.IsEnabled = false;
            this.BuildTransactionList();
            this.gridTransList.IsEnabled = true;
        }
        private void BuildTransactionList()
        {
            string FromDate = this.xctk_dtpFromDate.Text;
            string ToDate = this.xctk_dtpToDate.Text;

            string cmbDept = "%";
            string cmbSt = "%";


            string DptCode11a = ((ComboBoxItem)this.cmbSectDivCod.SelectedItem).Tag.ToString().Trim();
            if (DptCode11a == "AAAAAAAAAAAA")
                cmbDept = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();

            ////if (this.chkSelectedSender.IsChecked == true)
            ////    cmbDept = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();

            string Store1a = ((ComboBoxItem)this.cmbSectCod2f.SelectedItem).Tag.ToString().Trim();
            if (Store1a == "AAAAAAAAAAAA")
                cmbSt = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();

            //if (this.chkSelectedReceiver.IsChecked == true)
            //    cmbSt = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString();

            if (cmbDept == "%" && cmbSt == "%")
            {
                this.txtTransTitle.Text = "All Transaction List From : " + FromDate + " To : " + ToDate;
            }
            else
                if (cmbDept == "%" && cmbSt != "%")
                {
                    this.txtTransTitle.Text = " Transaction List From : " + FromDate + " To : " + ToDate + " Store Filter Id : " + cmbSt.Trim();
                }
                else if (cmbDept != "%" && cmbSt == "%")
                {
                    this.txtTransTitle.Text = "Transaction List From : " + FromDate + " To : " + ToDate + " Department Filter Id : " + cmbDept.Trim();
                }
                else
                {
                    this.txtTransTitle.Text = " Transaction List From : " + FromDate + " To : " + ToDate + " Department Filter Id : " + cmbDept.Trim() + " Store Filter Id : " + cmbSt.Trim();
                }

            ASITFunParams.ProcessAccessParams pap1 = vm1r.SetParamStoreTransList(CompCode: WpfProcessAccess.CompInfList[0].comcod, TrTyp: "SRQ", FromDate: FromDate, ToDate: ToDate,
                DeptID1: cmbDept, DeptID2: cmbSt, MemoStatus1: "[0123456789A]");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            this.dgvTransList.ItemsSource = null;
            //this.lvTransList.Items.Clear();
            this.ListViewTransTable1.Clear();
            this.ListViewTransTable1 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>().ToList();
            // Following code will be performed in backend -- Hafiz -- 04-Nov-2018
            //---------------------------------------------------------------------
            string Status1a = ((ComboBoxItem)this.cmbFindStatus.SelectedItem).Tag.ToString().Trim();
            if (Status1a != "00000")
            {
                string Status1b = (Status1a == "01000" ? "0" : (Status1a == "01001" ? "1" :  (Status1a == "01010" ? "A" : "X")));
                this.ListViewTransTable1 = this.ListViewTransTable1.FindAll(x => x.MemoStatus == Status1b).ToList();
            }

            if (!(DptCode11a == "000000000000" || DptCode11a == "AAAAAAAAAAAA"))
            {
                this.ListViewTransTable1 = this.ListViewTransTable1.FindAll(x => x.sectcod.Substring(0, 9) == DptCode11a.Substring(0, 9)).ToList();
            }

            string srfbyId1f = ((ComboBoxItem)this.cmbSrfByStaffFind.SelectedItem).Tag.ToString().Trim();
            if (srfbyId1f == "AAAAAAAAAAAA" && this.autoSrfByStaffSearch.SelectedValue != null)
            {
                string srfbyId1 = this.autoSrfByStaffSearch.SelectedValue.ToString().Trim();
                this.ListViewTransTable1 = this.ListViewTransTable1.FindAll(x => x.PreparById == srfbyId1).ToList();
            }


            int i = 1;
            foreach (var item in this.ListViewTransTable1)
            {
                item.slnum = i;
                item.Naration = (item.MemoStatus == "A" ? "Approved" : (item.MemoStatus == "1" ? "Checked" : "Prepared")) + item.Naration.Trim();
                i++;
            }
            //-----------------------------------------------------------
            this.dgvTransList.ItemsSource = this.ListViewTransTable1;
            this.dgvTransList.SelectedIndex = 0;
            this.dgvTransList.Focus();

            //foreach (var itm1a in this.ListViewTransTable1)
            //{
            //    this.lvTransList.Items.Add(itm1a);
            //}
            //this.lvTransList.SelectedIndex = 0;
            //this.lvTransList.Focus();
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
        //private void dgvTransList_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return)
        //        this.btnPrint3_Click(null, null);
        //}

        private void lvTransList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.btnPrint3_Click(null, null);
        }

        private void dgvlblSlNum_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string Itemid1 = ((Label)sender).Tag.ToString();
            var item1 = this.ListViewItemTable1.FindAll(x => x.rsircode == Itemid1);
            this.txtRSirDesc.Text = item1[0].trdesc;
            this.txtRSirDesc.Tag = item1[0].rsircode;
            this.txtSrfQty.Text = item1[0].srfqty.ToString("#,##0.00");
            this.lblUnit1.Content = item1[0].trunit;
        }

        private void btnEditSrf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.EditDs = null;
                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null)// (this.lvTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to edit", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var item1a = (HmsEntityInventory.InvTransectionList)this.dgvTransList.SelectedItem; //(HmsEntityInventory.InvTransectionList)this.lvTransList.SelectedItem;
                if (item1a.MemoStatus == "C")
                {
                    System.Windows.MessageBox.Show("Requisition Memo already cancelled. Edit not possible", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var pap1 = vm1.SetParamEditMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum);

                this.EditDs = WpfProcessAccess.GetHmsDataSet(pap1);
                if (this.EditDs == null)
                    return;

                DataRow dr0 = this.EditDs.Tables[0].Rows[0];

                DataRow dr2 = this.EditDs.Tables[2].Rows[0];

                this.xctk_dtpSrfDat.Value = Convert.ToDateTime(dr0["memodate"].ToString());

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


                this.btnOk_Click(null, null);
                ///////////////
                /*
                 	select a.comcod, a.srfno, a.preparebyid, PreparByName = isnull(b.signinnam, space(20)), a.prepareses, a.preparetrm, a.rowid, a.rowtime, ServerTime = getdate() 
			from #tblSReqm01 a left join dbo_hcm.userinf b on a.comcod = b.comcod and a.preparebyid = b.hccode;

                 */
                this.preparebyid1 = dr2["preparebyid"].ToString();// WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = dr2["prepareses"].ToString();// WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = dr2["preparetrm"].ToString();// WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = Convert.ToDateTime(dr2["rowtime"]);

                this.txtblEditMode.Visibility = Visibility.Visible;

                this.lblSrfNo.Content = dr0["memonum1"].ToString();
                this.lblSrfNo.Tag = dr0["memonum"].ToString();

                this.autoSrfByStaffSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoSrfByStaffSearch.SelectedValue = dr0["PreparById"].ToString().Trim();

                this.txtSrfRef.Text = dr0["Referance"].ToString().Trim();
                this.txtSrfNar.Text = dr0["Naration"].ToString().Trim();
                this.dgReqList.ItemsSource = null;
                this.ListViewItemTable1.Clear();
                foreach (DataRow item in this.EditDs.Tables[1].Rows)
                {
                    var item1b = new vmEntryStoreReq1.ListViewItemTable()
                    {
                        trsl = item["slnum"].ToString().Trim() + ".",
                        rsircode = item["rsircode"].ToString().Trim(),
                        trdesc = item["sirdesc"].ToString().Trim(),
                        srfqty = Convert.ToDecimal(item["srfqty"]),
                        srfqty1 = Convert.ToDecimal(item["srfqty"]).ToString("#,##0.00"),
                        truid = "",
                        trunit = item["sirunit"].ToString().Trim()
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

                this.dgReqList.ItemsSource = this.ListViewItemTable1;
                this.gridDetails.Visibility = Visibility.Visible;
                this.btnUpdate.Visibility = Visibility.Visible;
                //------------------------
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Store REQ-15: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

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
            this.lblUnit1.Content = lbi1.sirunit.Trim();



        }
        private void lstItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectItemInfo();
            this.txtSrfQty.Focus();
            //this.txtRSirDesc.Focus();
        }

        private void lstItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectItemInfo();
                this.txtSrfQty.Focus();
                //this.txtRSirDesc.Focus();
                //this.btnAddItem_Click(null, null);
            }
        }

        private void txtSrfQty_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }



        private void btnCancelSrf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.UnCheckedAllPopups();
                if (this.dgvTransList.SelectedItem == null) // (this.lvTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to cancel", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                var item1a = (HmsEntityInventory.InvTransectionList)this.dgvTransList.SelectedItem; //(HmsEntityInventory.InvTransectionList)this.lvTransList.SelectedItem;

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this Requisition " + item1a.memonum1, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;

                int itemno1 = this.dgvTransList.SelectedIndex; //this.lvTransList.SelectedIndex;
                var pap1 = vm1.SetParamBackupCancelMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum, "CANCEL", "MESSAGE");

                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                //((HmsEntityInventory.InvTransectionList)this.lvTransList.Items[itemno1]).MemoStatus = "C";
                //((HmsEntityInventory.InvTransectionList)this.dgvTransList.Items[itemno1]).MemoStatus = "C";
                this.ListViewTransTable1[itemno1].MemoStatus = "C";
                //this.lvTransList.Items.Refresh();
                this.dgvTransList.Items.Refresh();
                //AccTrnLst
                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Store REQ-16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void cmbSectCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.chkAutoTransList.IsChecked == true)
                this.btnFilter_Click(null, null);

        }
        private void cmbSectCod2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.chkAutoTransList.IsChecked == true)
                this.btnFilter_Click(null, null);
        }
        private void autoSrfByStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
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
            autitem.auhcid = this.autoSrfByStaffSearch.SelectedValue.ToString();
            autitem.auhcnam = this.autoSrfByStaffSearch.SelectedText.Trim();
            autitem.austat = "Y";
            autitem.statdes = "Prepared";
            autitem.aunote = this.txtSrfNar.Text.Trim();
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
            this.lblAuthorizeMemoDesc.Content = "Date: " + this.xctk_dtpSrfDat.Text.Trim() + ", S.R.F. No: " + this.lblSrfNo.Content.ToString();
            this.lblAuthorizeMemoDesc.Tag = this.lblSrfNo.Tag.ToString();

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

        private void btnEditAuthorize_Click(object sender, RoutedEventArgs e)
        {
            this.btnEditSrf_Click(null, null);
            if (this.EditDs == null)
                return;

            this.btnUpdate.IsEnabled = false;
            this.stkpAddItem.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;
            this.chkPrintWithStock.Visibility = Visibility.Visible;
            this.chkAuthorize.IsChecked = true;
            this.chkAuthorize_Click(null, null);
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
