using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt2Inventory;
using ASITFunLib;
using ASITHmsEntity;
using System.Data;
using ASITHmsViewMan.Inventory;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;


namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmReportStore1.xaml
    /// </summary>
    public partial class frmReportStore1 : UserControl
    {
        private bool FrmInitialized = false;
        private int TabItemIndex1 = 0;
        private DataGrid dgRpt1;
        private List<HmsEntityInventory.InvTransectionList> RptList = new List<HmsEntityInventory.InvTransectionList>();
        private List<HmsEntityInventory.InvStockList> RptStockList = new List<HmsEntityInventory.InvStockList>();
        private List<HmsEntityInventory.InvStockList02> RptStockList02 = new List<HmsEntityInventory.InvStockList02>();
        private List<HmsEntityInventory.MrrMemoDetails> mrrMemoDetailsList = new List<HmsEntityInventory.MrrMemoDetails>();
        private List<HmsEntityInventory.IssueMemoDetails> issueDetailsList = new List<HmsEntityInventory.IssueMemoDetails>();
        private List<HmsEntityInventory.StoreReqMemoDetails> storeReqDetailsList = new List<HmsEntityInventory.StoreReqMemoDetails>();
        private List<HmsEntityInventory.PurReqMemoDetails> purReqDetailsList = new List<HmsEntityInventory.PurReqMemoDetails>();
        private List<HmsEntityInventory.MStockMemoDetails> mstMemoDetailsList = new List<HmsEntityInventory.MStockMemoDetails>();

        private List<HmsEntityInventory.PurMrrSummary1> purMrrLst = new List<HmsEntityInventory.PurMrrSummary1>();
        private List<HmsEntityInventory.PurReqSummary1> purReqLst = new List<HmsEntityInventory.PurReqSummary1>();
        private List<HmsEntityInventory.StoreIssueSummary1> storeSumList = new List<HmsEntityInventory.StoreIssueSummary1>();
        private List<HmsEntityInventory.ItemStatusDetails> itemStatusList = new List<HmsEntityInventory.ItemStatusDetails>();

        private vmReportStore1 vm1 = new vmReportStore1();

        public frmReportStore1()
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
                this.FrmInitialized = true;
            }
        }
        private void InitializeOptions()
        {
            this.xctk_dtpFrom.Value = DateTime.Today; // Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.xctk_dtpTo.Value = DateTime.Today;

            TreeViewItem tvi1 = new TreeViewItem() { Header = "A. OVERALL SUMMARY REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi2 = new TreeViewItem() { Header = "B. GROUP WISE SUMMARY REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi3 = new TreeViewItem() { Header = "C. TRANSACTION MEMO LIST", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi4 = new TreeViewItem() { Header = "D. TRANSACTION DETAILS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi5 = new TreeViewItem() { Header = "E. SPECIAL REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            //TreeViewItem tvi6 = new TreeViewItem() { Header = "F. APROVAL STATUS REPORTS", Tag = "000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };

            tvi1.Items.Add(new TreeViewItem { Header = "01. Stock balance summary", Tag = "A01STOCK01" });
            tvi1.Items.Add(new TreeViewItem { Header = "02. Stock value summary", Tag = "A01STOCK01VAL" });
            tvi1.Items.Add(new TreeViewItem { Header = "03. Stock balance with level", Tag = "A01STOCK01L" });
            tvi1.Items.Add(new TreeViewItem { Header = "04. Stock value with level", Tag = "A01STOCK01LVAL" });
            tvi1.Items.Add(new TreeViewItem { Header = "05. Stock balance summary-2", Tag = "A02STOCK02" });
            tvi1.Items.Add(new TreeViewItem { Header = "06. Stock value summary-2", Tag = "A02STOCK02VAL" });

            tvi2.Items.Add(new TreeViewItem { Header = "01. Store requisition", Tag = "B01SRQ" });
            tvi2.Items.Add(new TreeViewItem { Header = "02. Store issue/transfer", Tag = "B02SIR" });
            tvi2.Items.Add(new TreeViewItem { Header = "03. Purchase requisition", Tag = "B03REQ" });
            tvi2.Items.Add(new TreeViewItem { Header = "04. Item receive summary", Tag = "B04MRR" });

            tvi3.Items.Add(new TreeViewItem { Header = "01. Store requisition", Tag = "C01SRQ" });
            tvi3.Items.Add(new TreeViewItem { Header = "02. Store issue/transfer", Tag = "C02SIR" });
            tvi3.Items.Add(new TreeViewItem { Header = "03. Purchase requisition", Tag = "C03REQ" });
            tvi3.Items.Add(new TreeViewItem { Header = "04. Item receive (MRR)", Tag = "C04MRR" });
            tvi3.Items.Add(new TreeViewItem { Header = "05. Physical Stock Info (MST)", Tag = "C05MST" });

            tvi4.Items.Add(new TreeViewItem { Header = "01. Store requisition", Tag = "D01SRQ" });
            tvi4.Items.Add(new TreeViewItem { Header = "02. Store issue/transfer", Tag = "D02SIR" });
            tvi4.Items.Add(new TreeViewItem { Header = "03. Purchase requisition", Tag = "D03REQ" });
            tvi4.Items.Add(new TreeViewItem { Header = "04. Item receive (MRR)", Tag = "D04MRR" });
            tvi4.Items.Add(new TreeViewItem { Header = "05. MRR with batch info", Tag = "D05MRR" });
            tvi4.Items.Add(new TreeViewItem { Header = "06. Physical Stock Info (MST)", Tag = "D06MST" });

            tvi5.Items.Add(new TreeViewItem { Header = "01. Item status - details", Tag = "E01ISTAT01" });
            tvi5.Items.Add(new TreeViewItem { Header = "02. Item status - summary", Tag = "E02ISTAT02" }); // Will be constructed soon -- Hafiz 19-Oct-2017
            tvi5.Items.Add(new TreeViewItem { Header = "03. L/C status - details", Tag = "E03LCSTAT01" });

            //tvi6.Items.Add(new TreeViewItem { Header = "01. Store Req. Approval Status", Tag = "F01APROV01" });


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
            //this.tvRptTitle.Items.Add(tvi6);

            this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = "ALL LOCATIONS", Tag = "%" });
            this.cmbSectCod2.Items.Add(new ComboBoxItem() { Content = "ALL LOCATIONS", Tag = "%" });

            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
            foreach (var item in deptList1)
            {
                this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = item.sectname, Tag = item.sectcod });
            }
            foreach (var itemd1 in deptList1)
            {
                //if (itemd1.sectname.ToUpper().Contains("STORE"))
                this.cmbSectCod2.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
            }

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();


            if (WpfProcessAccess.InvItemGroupList == null)
                WpfProcessAccess.GetInventoryItemGroupList();


            if (WpfProcessAccess.AccSirCodeList == null)
                WpfProcessAccess.GetAccSirCodeList();

            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();

            if (WpfProcessAccess.SupplierContractorList == null)
                WpfProcessAccess.GetSupplierContractorList();

            this.cmbItemGroup.Items.Add(new ComboBoxItem() { Content = "ALL GROUP OF ITEMS", Tag = "000000000000" });
            foreach (var itemd1 in WpfProcessAccess.InvItemGroupList)
            {
                var GrpList1 = WpfProcessAccess.InvItemList.FindAll(x => x.sircode.Substring(0, 7) == itemd1.sircode.Substring(0, 7));
                if (GrpList1.Count > 0)
                    this.cmbItemGroup.Items.Add(new ComboBoxItem() { Content = itemd1.sircode.Substring(0, 7) + ": " + itemd1.sirtype + " - " + itemd1.sirdesc.Trim(), Tag = itemd1.sircode });
            }

            this.AtxtSupId.Items.Clear();
            this.AtxtSupId.AutoSuggestionList.Clear();
            foreach (var item1 in WpfProcessAccess.SupplierContractorList)
            {
                this.AtxtSupId.AddSuggstionItem(item1.sircode.Trim().Substring(6, 6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
            }
            var lcList1 = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(0, 4) == "2502" && x.sircode.Substring(9, 3) != "000").ToList();
            foreach (var item1 in lcList1)
            {
                this.AtxtSupId.AddSuggstionItem(item1.sircode.Trim().Substring(6, 6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
            }

            this.AtxtSupId.AddSuggstionItem("CASH / DIRECT PURCHASE", "000000000000");      //.AutoSuggestionList.Add("CASH / DIRECT PURCHASE : [000000000000]");
            this.AtxtSupId.AddSuggstionItem("ADJUSTMENT OF STORE ITEMS", "369900101001");      //.AutoSuggestionList.Add("CASH / DIRECT PURCHASE : [000000000000]");


            this.AtxtStaffId.Items.Clear();
            this.AtxtStaffId.AutoSuggestionList.Clear();
            this.AtxtStaffId1.Items.Clear();
            this.AtxtStaffId1.AutoSuggestionList.Clear();
            foreach (var item1 in WpfProcessAccess.StaffList)
            {
                this.AtxtStaffId.AddSuggstionItem(item1.sircode.Trim().Substring(6, 6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
                this.AtxtStaffId1.AddSuggstionItem(item1.sircode.Trim().Substring(6, 6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
            }
        }


        private void tvRptTitle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string ItemTitle = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString().ToUpper();
            string ItemTag = ((TreeViewItem)((TreeView)sender).SelectedItem).Tag.ToString();
            this.ShowRequiredOptions(ItemTag);
           
        }
        private void ShowRequiredOptions(string ItemTag)
        {
            this.stkpSup.Visibility = Visibility.Visible;
            this.stkpStaff1.Visibility = Visibility.Collapsed;
            switch (ItemTag)
            {
                case "B02SIR":
                case "C02SIR":
                case "D02SIR":
                    this.stkpSup.Visibility = Visibility.Collapsed;
                    this.stkpStaff1.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void cmbItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string grpid = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString();
            var list1i = WpfProcessAccess.InvItemList.FindAll(x => x.sircode.Substring(0, 7) == grpid.Substring(0, 7));
            this.AtxtItemId.Items.Clear();
            this.AtxtItemId.AutoSuggestionList.Clear();
            foreach (var item1 in list1i)
            {
                this.AtxtItemId.AddSuggstionItem(item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if ((TreeViewItem)tvRptTitle.SelectedItem == null)
            {
                return;
            }

            string fromDate = xctk_dtpFrom.Text.Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();
            string TrHead = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString().ToUpper();
            string TrTyp = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            string dept01 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();
            string dept02 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString().Trim();

            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();

            string ItemGrp1 = ((ComboBoxItem)(this.cmbItemGroup.SelectedItem)).Tag.ToString().Trim().Substring(0, 7);
            string ItemGrp1des = ((ComboBoxItem)(this.cmbItemGroup.SelectedItem)).Content.ToString().Trim();
            string itemCode1 = this.AtxtItemId.Value.Trim();
            string itemCode1des = this.AtxtItemId.Text.Trim();
            ItemGrp1 = (ItemGrp1 == "0000000" ? "%" : (itemCode1.Length > 0 ? itemCode1 : ItemGrp1));

            ItemGrp1des = (ItemGrp1 == "%" ? "" : (itemCode1.Length > 0 ? itemCode1des : ItemGrp1des));
            string SupId1 = this.AtxtSupId.Value.Trim() + "%";
            if (this.stkpStaff1.Visibility == Visibility.Visible)
                SupId1 = this.AtxtStaffId1.Value.Trim() + "%";

            string StaffId1 = this.AtxtStaffId.Value.Trim() + "%";

            switch (TrTyp.Substring(0, 2))
            {
                case "A0": this.GetStockReport(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01, dept02, ItemGrp1, ItemGrp1des); break;
                case "B0": this.GetSummaryReport(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01, dept02, ItemGrp1, ItemGrp1des, SupId1, StaffId1); break;
                case "C0": this.GetTransecList(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01, dept02, SupId1, StaffId1); break;
                case "D0": this.GetTransDetails(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01, dept02, ItemGrp1, ItemGrp1des, SupId1, StaffId1); break;
                case "E0": this.GetItemSpecialTrans(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01, ItemGrp1, ItemGrp1des, SupId1, StaffId1); break;
            }
        }
        #region Summary Report

        private void GetItemSpecialTrans(string trHead, string trTyp, string printId, string fromDate, string toDate, string dept01, string ItemGrp1, string ItemGrp1des, string SupId1, string StaffId1)
        {
            var pap1 = vm1.SetParamItemSpecialTrans(WpfProcessAccess.CompInfList[0].comcod, trTyp.Substring(3), fromDate, toDate, dept01, ItemGrp1, SupId1, StaffId1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            var list3 = WpfProcessAccess.GetRptGenInfo();
            if (trTyp.Substring(0, 3) == "E01" || trTyp.Substring(0, 3) == "E02")
            {
                string storeid1 = "XXXXXXXXXXXX";
                string itemd1 = "YYYYYYYYYYYY";
                int slnum1 = 0;
                decimal blnc1 = 0.00m;

                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    if (dr1["sectcod"].ToString().Trim() != storeid1 || dr1["sircode"].ToString().Trim() != itemd1)
                    {
                        storeid1 = dr1["sectcod"].ToString().Trim();
                        itemd1 = dr1["sircode"].ToString().Trim();
                        slnum1 = 0;
                        blnc1 = 0.00m;
                    }
                    ++slnum1;
                    blnc1 = blnc1 + Convert.ToDecimal(dr1["inqty"]) - Convert.ToDecimal(dr1["outqty"]);
                    dr1["slnum"] = slnum1;
                    dr1["balqty"] = blnc1;
                }

                list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]));
                list3[0].RptHeader1 = fromDate;
                list3[0].RptHeader2 = toDate;
                list3[0].RptParVal1 = (ItemGrp1des.Length > 0 ? "(Filter Item : " + ItemGrp1des + ")" : "");
            }
            if (printId == "PP" || printId == "NP" || printId == "PDF" || printId == "WORD" || printId == "EXCEL")
            {
                switch (trTyp.Substring(0, 3))
                {
                    case "E01":
                        list3[0].RptHeader1 = "Item Status Details ( " + fromDate + " To  " + toDate + " )";
                        var storeItemSpclList1 = ds1.Tables[0].DataTableToList<HmsEntityInventory.ItemStatusDetails>();
                        LocalReport rpt1 = StoreReportSetup.GetLocalReport("Store.RptItemStatus1", storeItemSpclList1, null, list3);
                        this.ShowRptWindow("Item Status Details", rpt1);
                        break;
                    case "E02":
                        list3[0].RptHeader1 = "Item Status Summary ( " + fromDate + " To  " + toDate + " )";
                        var storeItemSpclList2 = ds1.Tables[0].DataTableToList<HmsEntityInventory.ItemStatusDetails>();
                        LocalReport rpt2 = StoreReportSetup.GetLocalReport("Store.RptItemStatus1", storeItemSpclList2, null, list3);
                        this.ShowRptWindow("Item Status Summary", rpt2);
                        break;
                }
            }
            else if (printId == "SS")
            {
                switch (trTyp.Substring(0, 3))
                {
                    case "E01":
                        this.itemStatusList = ds1.Tables[0].DataTableToList<HmsEntityInventory.ItemStatusDetails>();
                        this.ShowGridInfo(trTyp); // Store Requision
                        break;
                    //case "E02":
                    //    this.storeSumList = ds1.Tables[0].DataTableToList<HmsEntityInventory.StoreIssueSummary1>();
                    //    this.prepareGridStrReq();// Store Issue
                    //    break;                                      
                }
            }
        }

        private void GetSummaryReport(string trHead, string trTyp, string printId, string fromDate, string toDate, string dept01, string dept02, string ItemGrp1, string ItemGrp1des, string SupId1, string StaffId1)
        {
            var pap1 = vm1.SetParamInvSumReport(WpfProcessAccess.CompInfList[0].comcod, trTyp.Substring(3), fromDate, toDate, dept01, dept02, ItemGrp1, SupId1, StaffId1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]));
            list3[0].RptHeader1 = fromDate;
            list3[0].RptHeader2 = toDate;
            list3[0].RptParVal1 = (ItemGrp1des.Length > 0 ? "(Filter Item : " + ItemGrp1des + ")" : "");

            if (printId == "PP" || printId == "NP" || printId == "PDF" || printId == "WORD" || printId == "EXCEL")
            {
                switch (trTyp.Substring(0, 3))
                {
                    case "B01":
                        list3[0].RptHeader1 = "Store Requsition Summary ( " + fromDate + " To  " + toDate + " )";
                        list3[0].RptParVal1 = list3[0].RptParVal1 + (StaffId1.Length > 5 ? " [Req. By : " + this.AtxtStaffId.Text.Trim() + " ]" : "");
                        var storeSumList = ds1.Tables[0].DataTableToList<HmsEntityInventory.StoreIssueSummary1>();
                        LocalReport rpt1 = StoreReportSetup.GetLocalReport("Store.RptStoreIssueSum1", storeSumList, null, list3);
                        this.ShowRptWindow("Store Requsition Summary", rpt1);
                        break;
                    case "B02":
                        list3[0].RptHeader1 = "Store Issue Summary ( " + fromDate + " To  " + toDate + " )";
                        var storIsuueSumLIist = ds1.Tables[0].DataTableToList<HmsEntityInventory.StoreIssueSummary1>();
                        LocalReport rpt2 = StoreReportSetup.GetLocalReport("Store.RptStoreIssueSum1", storIsuueSumLIist, null, list3);
                        this.ShowRptWindow("Store Issue Summary", rpt2);
                        break;
                    case "B04":
                        list3[0].RptParVal1 = list3[0].RptParVal1 + (SupId1.Length > 5 ? " [Supply Source : " + this.AtxtSupId.Text.Trim() + " ]" : "");
                        var purMrrLst = ds1.Tables[0].DataTableToList<HmsEntityInventory.PurMrrSummary1>();
                        LocalReport rpt3 = StoreReportSetup.GetLocalReport("Store.RptPurMrrSum1", purMrrLst, null, list3);
                        this.ShowRptWindow("Purchase Mrr Summary", rpt3);
                        break;
                    case "B03":
                        var purReqLst = ds1.Tables[0].DataTableToList<HmsEntityInventory.PurReqSummary1>();
                        LocalReport rpt4 = StoreReportSetup.GetLocalReport("Store.RptPurReqSum1", purReqLst, null, list3);
                        this.ShowRptWindow("Purchase Requsition Summary", rpt4);
                        break;
                }
            }
            else if (printId == "SS")
            {
                switch (trTyp.Substring(0, 3))
                {
                    case "B01":
                        this.storeSumList = ds1.Tables[0].DataTableToList<HmsEntityInventory.StoreIssueSummary1>();
                        this.prepareGridStrReq();// Store Requision
                        break;
                    case "B02":
                        this.storeSumList = ds1.Tables[0].DataTableToList<HmsEntityInventory.StoreIssueSummary1>();
                        this.prepareGridStrReq();// Store Issue
                        break;
                    case "B04":
                        this.purMrrLst = ds1.Tables[0].DataTableToList<HmsEntityInventory.PurMrrSummary1>();
                        this.prepareGridMRR01();  // MRR Summary

                        break;
                    case "B03":
                        this.purReqLst = ds1.Tables[0].DataTableToList<HmsEntityInventory.PurReqSummary1>();
                        this.prepareGridPurReq();  // Purchase Requision

                        break;
                    default:
                        break;
                }
            }

        }

        #region Datagrid

        private void prepareGridStrReq()// Store Requision
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }

        private void prepareGridMRR01()// MRR Summary
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }

        private void prepareGridPurReq()  // Purchase Requision
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }
        #endregion

        #endregion

        #region Transaction Details
        private void GetTransDetails(string trHead, string trTyp, string printId, string fromDate, string toDate, string dept01, string dept02, string ItemGrp1, string ItemGrp1des, string SupId1, string StaffId1)
        {

            var pap1 = vm1.SetParamStoreTransDetails(WpfProcessAccess.CompInfList[0].comcod, trTyp.Substring(3), fromDate, toDate, dept01, dept02, ItemGrp1, SupId1, StaffId1, "[0123456789A]");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]));
            list3[0].RptHeader1 = fromDate;
            list3[0].RptHeader2 = toDate;
            list3[0].RptParVal1 = (ItemGrp1des.Length > 0 ? "(Filter Item : " + ItemGrp1des + ")" : "");

            if (printId == "PP" || printId == "NP" || printId == "PDF" || printId == "WORD" || printId == "EXCEL")
            {
                switch (trTyp.Substring(0, 3))
                {
                    case "D04":
                        var mrrMemoDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.MrrMemoDetails>();
                        LocalReport rpt1 = StoreReportSetup.GetLocalReport("Store.RptMrrDetails1", mrrMemoDetailsList, null, list3);
                        this.ShowRptWindow("MRR Details Report", rpt1);
                        break;
                    case "D05":
                        var mrrMemoDetailsList12 = ds1.Tables[0].DataTableToList<HmsEntityInventory.MrrMemoDetails>();
                        LocalReport rpt12 = StoreReportSetup.GetLocalReport("Store.RptMrrDetails2", mrrMemoDetailsList12, null, list3);
                        this.ShowRptWindow("MRR Report With Batch Info", rpt12);
                        break;

                    case "D06":
                        var mstMemoDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.MStockMemoDetails>();
                        LocalReport rpt6 = StoreReportSetup.GetLocalReport("Store.RptMStockDetails1", mstMemoDetailsList, null, list3);
                        this.ShowRptWindow("Physical Stock Details Report", rpt6);
                        break;

                    case "D02":
                        var issueDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.IssueMemoDetails>();
                        LocalReport rpt2 = StoreReportSetup.GetLocalReport("Store.RptIssueDetails1", issueDetailsList, null, list3);
                        this.ShowRptWindow("Issue Details Window", rpt2);
                        break;
                    case "D01":
                        var storeReqDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.StoreReqMemoDetails>();
                        LocalReport rpt3 = StoreReportSetup.GetLocalReport("Store.RptStoreReqDetails1", storeReqDetailsList, null, list3);
                        this.ShowRptWindow("Store Requsition Details Report", rpt3);
                        break;
                    case "D03":
                        var purReqDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.PurReqMemoDetails>();
                        LocalReport rpt4 = StoreReportSetup.GetLocalReport("Store.RptPurReqDetails1", purReqDetailsList, null, list3);
                        this.ShowRptWindow("Purchase Requsition Details report", rpt4);
                        break;
                }
            }
            else if (printId == "SS")
            {
                switch (trTyp.Substring(0, 3))
                {
                    case "D04":
                    case "D05":
                        this.mrrMemoDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.MrrMemoDetails>();
                        this.prepareGridStrReqT04();// MRR Memo Details List
                        break;
                    case "D06":
                        this.mstMemoDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.MStockMemoDetails>();
                        this.prepareGridStrMst06();// Physical Stock Details List
                        break;
                    case "D02":
                        this.issueDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.IssueMemoDetails>();
                        this.prepareGridStrReqT02();// Issue Memo Details List                       
                        break;
                    case "D01":
                        this.storeReqDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.StoreReqMemoDetails>();
                        this.prepareGridStrReqT01();// Store Requision Transaction List
                        break;
                    case "D03":
                        this.purReqDetailsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.PurReqMemoDetails>();
                        this.prepareGridStrReqT03();// Purchase Requision Memo Details
                        break;
                    default:
                        break;
                }
            }
            //////////////////////
        }

        #region Transaction Details DataGrid

        private void prepareGridStrMst06() // Physical Stock Details List
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }
        private void prepareGridStrReqT04() // MRR Memo Details
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }


        private void prepareGridStrReqT03() // Purchase Requision Memo Details
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }

        private void prepareGridStrReqT02()
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }

        private void prepareGridStrReqT01()// Store Requision Transaction List
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }
        #endregion

        #endregion

        private void ShowRptWindow(string wtitle, LocalReport rpt1)
        {
            string WindowTitle1 = wtitle;
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        #region Stock Report

        public void GetStockReport(string TrHead, string TrTyp, string PrintId, string fromDate, string toDate, string dept01, string dept02, string ItemGrp1, string ItemGrp1des)
        {
            var pap1 = vm1.SetParamInvSumReport(WpfProcessAccess.CompInfList[0].comcod, TrTyp.Substring(3), fromDate, toDate, dept01, dept02, ItemGrp1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            string ShowOpt1 = ((ComboBoxItem)this.cmbShowOptions.SelectedItem).Tag.ToString();

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]));
            list3[0].RptParVal1 = TrTyp;
            list3[0].RptHeader1 = fromDate;
            list3[0].RptHeader2 = toDate;
            list3[0].RptParVal1 = (ItemGrp1des.Length > 0 ? "(Filter Item : " + ItemGrp1des + ")" : "");
            list3[0].RptParVal2 = (TrTyp.Contains("VAL") ? "(Amount in Taka)" : "");    // Will be programmed when multicurrency enabled
            this.RptStockList.Clear();
            this.RptStockList02.Clear();
            if ((PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL") && TrTyp.Substring(0, 3) == "A01")
            {
                list3[0].RptHeader1 = "Stock" + (TrTyp.Contains("VAL") ? " Value" : "") + " Report ( " + fromDate + " To  " + toDate + " )";

              
                if(this.cmbShowOptions.SelectedIndex > 0)
                    list3[0].RptHeader1 = list3[0].RptHeader1 + " - " + ((ComboBoxItem)this.cmbShowOptions.SelectedItem).Content.ToString() + " Items Only";

                this.RptStockList = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvStockList>();
                if (ShowOpt1 == "CURRALL")
                  this.RptStockList = this.RptStockList.FindAll(x => x.recvqty > 0 || x.isuqty > 0).ToList();
                else if (ShowOpt1 == "CURRIN")
                    this.RptStockList = this.RptStockList.FindAll(x => x.recvqty > 0).ToList();
                else if (ShowOpt1 == "CURROUT")
                    this.RptStockList = this.RptStockList.FindAll(x => x.isuqty > 0).ToList();
                else if (ShowOpt1 == "REVIOUSALL")
                    this.RptStockList = this.RptStockList.FindAll(x => x.recvqty <=0 && x.isuqty <= 0).ToList();
                else if (ShowOpt1 == "OPENING")
                    this.RptStockList = this.RptStockList.FindAll(x => x.opnqty > 0).ToList();
                else if (ShowOpt1 == "CLOSING")
                    this.RptStockList = this.RptStockList.FindAll(x => x.clsqty > 0).ToList();

                string RptName = (TrTyp.Contains("A01STOCK01L") ? "Store.RptClosingStock1L" : "Store.RptClosingStock1");
                LocalReport Rpt1 = StoreReportSetup.GetLocalReport(RptName, this.RptStockList, null, list3);
                this.ShowRptWindow("Stock Report List", Rpt1);
                return;
            }
            else if ((PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL") && TrTyp.Substring(0, 3) == "A02")
            {
                list3[0].RptHeader1 = "Stock" + (TrTyp.Contains("VAL") ? " Value" : "") + " Report - 2 ( " + fromDate + " To  " + toDate + " )";
                if (this.cmbShowOptions.SelectedIndex > 0)
                    list3[0].RptHeader1 = list3[0].RptHeader1 + " - " + ((ComboBoxItem)this.cmbShowOptions.SelectedItem).Content.ToString() + " Items Only";

                // =Trim("Stock Report ( " & Parameters!RptHeader1.Value & " To " & Parameters!RptHeader2.Value &" )" + "   " + Parameters!ParmCurr1.Value)
                this.RptStockList02 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvStockList02>();
                if (ShowOpt1 == "CURRALL")
                    this.RptStockList02 = this.RptStockList02.FindAll(x => x.trcvqty > 0 || x.tisuqty > 0).ToList();
                else if (ShowOpt1 == "CURRIN")
                    this.RptStockList02 = this.RptStockList02.FindAll(x => x.trcvqty > 0).ToList();
                else if (ShowOpt1 == "CURROUT")
                    this.RptStockList02 = this.RptStockList02.FindAll(x => x.tisuqty > 0).ToList();
                if (ShowOpt1 == "REVIOUSALL")
                    this.RptStockList02 = this.RptStockList02.FindAll(x => x.trcvqty <= 0 && x.tisuqty <= 0).ToList();
                else if (ShowOpt1 == "OPENING")
                    this.RptStockList02 = this.RptStockList02.FindAll(x => x.opnqty > 0).ToList();
                else if (ShowOpt1 == "CLOSING")
                    this.RptStockList02 = this.RptStockList02.FindAll(x => x.clsqty > 0).ToList();

                LocalReport Rpt1 = StoreReportSetup.GetLocalReport("Store.RptClosingStock2", RptStockList02, null, list3);
                this.ShowRptWindow("Stock Report List", Rpt1);
                return;
            }

            switch (TrTyp.Substring(0, 3))
            {
                case "A01":
                    this.RptStockList = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvStockList>();
                    if (ShowOpt1 == "CURRALL")
                      this.RptStockList = this.RptStockList.FindAll(x => x.recvqty > 0 || x.isuqty > 0).ToList();
                    else if (ShowOpt1 == "CURRIN")
                        this.RptStockList = this.RptStockList.FindAll(x => x.recvqty > 0).ToList();
                    else if (ShowOpt1 == "CURROUT")
                        this.RptStockList = this.RptStockList.FindAll(x => x.isuqty > 0).ToList();
                    else if (ShowOpt1 == "REVIOUSALL")
                        this.RptStockList = this.RptStockList.FindAll(x => x.recvqty <= 0 && x.isuqty <= 0).ToList();
                    else if (ShowOpt1 == "OPENING")
                        this.RptStockList = this.RptStockList.FindAll(x => x.opnqty > 0).ToList();
                    else if (ShowOpt1 == "CLOSING")
                        this.RptStockList = this.RptStockList.FindAll(x => x.clsqty > 0).ToList();
                    this.PrepareGridForStock01(); // Stock Balance With Level

                    break;
                case "A02":
                    this.RptStockList02 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvStockList02>();
                    if (ShowOpt1 == "CURRALL")
                        this.RptStockList02 = this.RptStockList02.FindAll(x => x.trcvqty > 0 || x.tisuqty > 0).ToList();
                    else if (ShowOpt1 == "CURRIN")
                        this.RptStockList02 = this.RptStockList02.FindAll(x => x.trcvqty > 0).ToList();
                    else if (ShowOpt1 == "CURROUT")
                        this.RptStockList02 = this.RptStockList02.FindAll(x => x.tisuqty > 0).ToList();
                    if (ShowOpt1 == "REVIOUSALL")
                        this.RptStockList02 = this.RptStockList02.FindAll(x => x.trcvqty <= 0 && x.tisuqty <= 0).ToList();
                    else if (ShowOpt1 == "OPENING")
                        this.RptStockList02 = this.RptStockList02.FindAll(x => x.opnqty > 0).ToList();
                    else if (ShowOpt1 == "CLOSING")
                        this.RptStockList02 = this.RptStockList02.FindAll(x => x.clsqty > 0).ToList();
                    this.PrepareGridForStock02(); //Stock Balance Status
                    break;
                default:
                    break;
            }
        }

        private void PrepareGridForStock01() //  Stock Balance With Level
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }

        private void PrepareGridForStock02() //Stock Balance Status
        {

            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }

        #endregion



        #region Transaction Memo List

        public void GetTransecList(string TrHead, string TrTyp, string PrintId, string fromDate, string toDate, string dept01, string dept02, string SupId1, string StaffId1)
        {
            var pap1 = vm1.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, TrTyp.Substring(3), fromDate, toDate, dept01, dept02, SupId1, StaffId1, "[0123456789A]");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.RptList.Clear();
            this.RptList = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();
            string ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            switch (PrintId)
            {
                case "PP":
                case "NP":
                case "PDF":
                case "WORD":
                case "EXCEL":
                    this.PrintTransecList(RptList, ServerTime1);
                    break;
                case "SS":
                    this.memoTranList();
                    break;
                case "DP":
                    break;
                case "EXCELF":
                    break;
                default:
                    return;
            }
        }

        private void memoTranList()
        {
            string ItemTag = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();
            this.ShowGridInfo(ItemTag);
        }

        #endregion
        public void PrintTransecList(List<HmsEntityInventory.InvTransectionList> list1, string ServerTime1 = "")
        {
            if (list1 == null)
                return;

            ServerTime1 = (ServerTime1.Length == 0 ? DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") : ServerTime1);

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ServerTime1));
            LocalReport Rpt1 = StoreReportSetup.GetLocalReport("Store.RptTransectionList", list1, null, list3);
            this.ShowRptWindow("Transaction List", Rpt1);
        }

        private void GetMemoList(string memoNum)
        {
            LocalReport rpt1 = null;
            var pap1 = vm1.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var trnsList = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();
            //var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]));

            string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
              + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
            
            string memoName = memoNum.Substring(0, 3).Trim();
            if (memoName == "SRQ" || memoName == "SIR" || memoName == "REQ" || memoName == "MRR" || memoName == "MST")
            {

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
            }

    
            switch (memoName)
            {
                case "SRQ": rpt1 = StoreReportSetup.GetLocalReport("Store.RptStoreReqMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.StoreReqMemo>(), trnsList, list3); break;
                case "MRR": rpt1 = StoreReportSetup.GetLocalReport("Store.RptMRRMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.PurMrrMemo>(), trnsList, list3); ; break;
                case "MST": rpt1 = StoreReportSetup.GetLocalReport("Store.RptMStockMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.MStockMemo>(), trnsList, list3); ; break;
                case "BIL": rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurBillRcvMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.PurBillMemo>(), trnsList, list3); break;
                case "POR": rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurOrderMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.PurOrderMemo>(), trnsList, list3); break;
                case "PQT": rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurQuotMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.PurQtnMemo>(), trnsList, list3); break;
                case "REQ": rpt1 = StoreReportSetup.GetLocalReport("Store.RptPurReqMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.PurReqMemo>(), trnsList, list3); break;
                case "PAP": rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurReqApprMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.PurApprovMemo>(), trnsList, list3); break;
                case "QRA": rpt1 = StoreReportSetup.GetLocalReport("Procurement.RateFixMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.PurRateMemo>(), trnsList, list3); break;
                case "SIR": rpt1 = StoreReportSetup.GetLocalReport("Store.RptIssueMemo01", ds1.Tables[1].DataTableToList<HmsEntityInventory.StoreIssueMemo>(), trnsList, list3); break;
                default: return;
            }

            this.ShowRptWindow("Memo", rpt1);


            //string WindowTitle1 = "Memo";
            //string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            //string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            //WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
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
        private void ShowGridInfo(string ItemTag)
        {

            try
            {
                if (this.TabUcGrid1.Items.Count > 6)
                    return;
                string ItemTitle = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString().ToUpper();
                this.ShowRequiredOptions(ItemTag);
                string Msg1 = (ItemTag == "C05PAP" || ItemTag == "D07POR" ? "(This option is under construction)" : "");

                switch (ItemTag)
                {
                    case "A01STOCK01":
                    case "A01STOCK01VAL":
                        this.CreateNewTabForReport(GridReportInv1.StockBalance.GetDataGrid(this.RptStockList.ToList()));
                        break;
                    case "A01STOCK01L":
                    case "A01STOCK01LVAL":
                        this.CreateNewTabForReport(GridReportInv1.StockBalanceWithLbl.GetDataGrid(this.RptStockList.ToList()));
                        break;
                    case "A02STOCK02":
                    case "A02STOCK02VAL":
                        this.CreateNewTabForReport(GridReportInv1.StockBalanceStatus.GetDataGrid(this.RptStockList02.ToList()));
                        break;
                    case "B01SRQ":
                    case "B02SIR":
                        this.CreateNewTabForReport(GridReportInv1.StrReq01.GetDataGrid(this.storeSumList.ToList()));
                        break;
                    case "B03REQ":
                        this.CreateNewTabForReport(GridReportInv1.PurReq01.GetDataGrid(this.purReqLst.ToList()));
                        break;
                    case "B04MRR":
                        this.CreateNewTabForReport(GridReportInv1.MRR01.GetDataGrid(this.purMrrLst.ToList()));
                        break;
                    case "C01SRQ":
                    case "C02SIR":
                    case "C03REQ":
                    case "C04MRR":
                    case "C05MST":
                        this.CreateNewTabForReport(GridReportInv1.memoTranList.GetDataGrid(this.RptList.ToList()));
                        break;
                    case "D01SRQ":
                        this.CreateNewTabForReport(GridReportInv1.StrReqT01.GetDataGrid(this.storeReqDetailsList.ToList()));
                        break;
                    case "D02SIR":
                        this.CreateNewTabForReport(GridReportInv1.StrReqT02.GetDataGrid(this.issueDetailsList.ToList()));
                        break;
                    case "D03REQ":
                        this.CreateNewTabForReport(GridReportInv1.StrReqT03.GetDataGrid(this.purReqDetailsList.ToList()));
                        break;
                    case "D04MRR":
                        this.CreateNewTabForReport(GridReportInv1.StrReqT04.GetDataGrid(this.mrrMemoDetailsList.ToList()));
                        break;
                    case "D05MRR":
                        this.CreateNewTabForReport(GridReportInv1.StrReqT05.GetDataGrid(this.mrrMemoDetailsList.ToList()));
                        break;
                    case "D06MST":
                        this.CreateNewTabForReport(GridReportInv1.StrMst06.GetDataGrid(this.mstMemoDetailsList.ToList()));
                        break;
                    case "E01ISTAT01":
                    case "E02ISTAT02":
                        this.CreateNewTabForReport(GridReportInv1.ItemStatusD.GetDataGrid(this.itemStatusList.ToList()));
                        break;
                    case "E03LCSTAT03":
                        break;
                }
                this.TabUcGrid1.SelectedIndex = this.TabUcGrid1.Items.Count - 1;
            }

            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Inv-Gvm-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
      

        private void dgRpt1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Currently Disabled but need to activate ASAP -- Hafiz -- 06-Nov-2018
            try
            {
                if (((DataGrid)sender).Tag == null)
                    return;

                string ItemTag = ((DataGrid)sender).Tag.ToString();
                switch (ItemTag)
                {
                    case "C01SRQ":
                    case "C02SIR":
                    case "C03REQ":
                    case "C04MRR":
                    case "C05MST":
                        var item1a = (HmsEntityInventory.InvTransectionList)this.dgRpt1.SelectedItem;
                        this.GetMemoList(item1a.memonum);
                        break;
                    case "D01SRQ":
                        var item1r = (HmsEntityInventory.StoreReqMemoDetails)this.dgRpt1.SelectedItem;
                        this.GetMemoList(item1r.srfno);
                        break;
                    case "D02SIR":
                        var item1s = (HmsEntityInventory.IssueMemoDetails)this.dgRpt1.SelectedItem;
                        this.GetMemoList(item1s.sirno);
                        break;
                    case "D03REQ":
                        var item1q = (HmsEntityInventory.PurReqMemoDetails)this.dgRpt1.SelectedItem;
                        this.GetMemoList(item1q.reqno);
                        break;
                    case "D04MRR":
                    case "D05MRR":
                        var item1m = (HmsEntityInventory.MrrMemoDetails)this.dgRpt1.SelectedItem;
                        this.GetMemoList(item1m.mrrno);
                        break;
                    case "D06MST":
                        var item1h = (HmsEntityInventory.MStockMemoDetails)this.dgRpt1.SelectedItem;
                        this.GetMemoList(item1h.mstkno);
                        break;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Inv.Rpt-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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

    }
}
