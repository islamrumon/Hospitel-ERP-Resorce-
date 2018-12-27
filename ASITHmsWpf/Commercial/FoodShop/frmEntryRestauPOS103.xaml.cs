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
using System.Windows.Data;
using System.Windows.Media;


namespace ASITHmsWpf.Commercial.FoodShop
{
    /// <summary>
    /// Interaction logic for frmEntryRestauPOS103.xaml
    /// </summary>
    public partial class frmEntryRestauPOS103 : UserControl
    {
        private string TitaleTag1, TitaleTag2;  // 
        private bool FrmInitialized = false;
        private List<HmsEntityCommercial.InvoiceTransList> RptList = new List<HmsEntityCommercial.InvoiceTransList>();
        private List<HmsEntityCommercial.InvoiceTransList2> RptList1 = new List<HmsEntityCommercial.InvoiceTransList2>();
        private List<HmsEntityCommercial.InvColList01> RptList2 = new List<HmsEntityCommercial.InvColList01>();//PhSalesInvoice01
        private List<HmsEntityCommercial.InvDuesList01> RptList3 = new List<HmsEntityCommercial.InvDuesList01>();
        private List<HmsEntityCommercial.PhSalesInvoice01> RptList4 = new List<HmsEntityCommercial.PhSalesInvoice01>();
        private vmReportPharRestPOS1 vm1 = new vmReportPharRestPOS1();
        public frmEntryRestauPOS103()
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
                this.Objects_On_Init();
                this.FrmInitialized = true;
            }
        }

        private void Objects_On_Init()
        {
            TitaleTag2 = this.Tag.ToString();
            TreeViewItem tvi1 = new TreeViewItem() { Header = "A. TRANSECTION LIST", Tag = "A000000000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi2 = new TreeViewItem() { Header = "B. SUMMARY LIST", Tag = "B00000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi3 = new TreeViewItem() { Header = "C. SPECIAL SUMMARY LIST", Tag = "C0000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };

            tvi1.Items.Add(new TreeViewItem { Header = "01. Today's sales details", Tag = "A01A00MSIDETAILS" });
            tvi1.Items.Add(new TreeViewItem { Header = "02. Yesterday's sales details", Tag = "A02A00MSIDETAILS" });
            tvi1.Items.Add(new TreeViewItem { Header = "03. Date wise sales details", Tag = "A06A00MSIDETAILS" });


            tvi2.Items.Add(new TreeViewItem { Header = "01. Today's sales invoice", Tag = "B01A00MSISUM" });
            tvi2.Items.Add(new TreeViewItem { Header = "02. Yesterday's sales invoice", Tag = "B02A00MSISUM" });
            tvi2.Items.Add(new TreeViewItem { Header = "03. This month sales invoice", Tag = "B03A00MSISUM" });
            tvi2.Items.Add(new TreeViewItem { Header = "04. This week sales invoice", Tag = "B04A00MSISUM" });
            tvi2.Items.Add(new TreeViewItem { Header = "05. Last week sales invoice", Tag = "B05A00MSISUM" });
            tvi2.Items.Add(new TreeViewItem { Header = "06. Invoice due summary list", Tag = "B06A00DUEDETAILS" });
            tvi2.Items.Add(new TreeViewItem { Header = "07. Item wise sales summary", Tag = "B07A00ITEMSUMMARY" });
            tvi2.Items.Add(new TreeViewItem { Header = "08. Overall sales summary", Tag = "B08A00TOPSUMMARY" });


            tvi3.Items.Add(new TreeViewItem { Header = "01. Sales Invoice", Tag = "C01SIV" });
            tvi3.Items.Add(new TreeViewItem { Header = "02. Overall summary reports", Tag = "C02IS" });

            tvi1.IsExpanded = true;
            tvi2.IsExpanded = true;
            tvi3.IsExpanded = true;

            this.tvRptRtTitle.Items.Add(tvi1);
            this.tvRptRtTitle.Items.Add(tvi2);
            this.tvRptRtTitle.Items.Add(tvi3);

            this.xctk_dtpFrom.Value = Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.xctk_dtpTo.Value = DateTime.Today;
            //this.cmbShop.SelectedIndex = (TitaleTag2.Contains("MediShop") ? 0 : 1);
            //this.cmbReportSelOption.SelectedIndex = 0;        
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            #region Tree Veiw Report Show

            if (dgOverall01.Columns.Count > 0)
                dgOverall01.Columns.Clear();
            this.dgOverall01.ItemsSource = null;
            this.dgOverall01.Items.Refresh();
            this.dgOverall01.AutoGenerateColumns = false;
            if ((TreeViewItem)tvRptRtTitle.SelectedItem == null)
            {
                return;
            }

            string fromDate = xctk_dtpFrom.Text.Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();
            string TrHead = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Header.ToString();
            string TrTyp = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Tag.ToString().Substring(3);
            //string dept01 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();

            this.lbltle1.Content = TrHead.Remove(0, 3);
            this.lbltle2.Content = " From " + fromDate + " To " + ToDate;
            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();


            //switch (TrTyp.Substring(0, 2))
            //{
            //    case "A0": this.GetStockReport(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01); break;
            //    case "B0": this.GetSuimmaryRpt(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01); break;
            //    case "C0": this.GetTransecList(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01); break;
            //    case "D0": this.GetTransDetails(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01); break;
            //}

            switch (TrTyp)
            {
                case "A00MSISUM": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "A00MSIDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "A00COLLDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "A00DUEDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "A00ITEMSUMMARY": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "A00TOPSUMMARY": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "C01SIV": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                //case "B0": this.GetSuimmaryRpt(TrHead, TrTyp, PrintId); break;
                //case "C0": this.GetTransecList(TrHead, TrTyp, PrintId); break;
                //case "D0": this.GetTransDetails(TrHead, TrTyp, PrintId); break;
            }
            #endregion

            #region ComboBox Report Show Code
            //string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            //string TrHead = ((ComboBoxItem)(this.cmbReportSelOption.SelectedItem)).Content.ToString().Trim();
            //string TrTyp = ((ComboBoxItem)(this.cmbReportSelOption.SelectedItem)).Tag.ToString().Trim();

            //switch (TrTyp)
            //{
            //    case "A00MSISUM": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
            //    case "A00MSIDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
            //    case "A00COLLDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
            //    case "A00DUEDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
            //    //case "B0": this.GetSuimmaryRpt(TrHead, TrTyp, PrintId); break;
            //    //case "C0": this.GetTransecList(TrHead, TrTyp, PrintId); break;
            //    //case "D0": this.GetTransDetails(TrHead, TrTyp, PrintId); break;
            //}
            #endregion
        }

        private void GetSumTransListReport(string TrHead, string TrTyp, string PrintId)
        {
            string fromDate = xctk_dtpFrom.Text.ToString().Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();

            //string Dept01 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();
            string CmbShop = ((ComboBoxItem)this.cmbShop.SelectedItem).Tag.ToString().Trim();

            //this.lbltle1.Content = TrHead.Remove(0, 2);
            //this.lbltle2.Content = " From " + fromDate + " To " + ToDate;
            var pap1 = vm1.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, TrTyp, fromDate, ToDate);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            if (ds1 == null)
                return;

            this.RptList.Clear();


            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF")
            {
                switch (TrTyp)
                {
                    case "A00MSISUM": this.RptList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>().ToList().OrderBy(x => x.invno).ToList();
                        int sl1 = 1;
                        DateTime OldDt1 = DateTime.Parse("01-Jan-1900");
                        foreach (var item1 in this.RptList)
                        {
                            if (item1.invdat != OldDt1)
                            {
                                OldDt1 = item1.invdat;
                                sl1 = 1;
                            }
                            item1.slnum = sl1;
                            sl1++;
                        }
                        this.RptList = RptList.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                        this.PrintTransecList(RptList);
                        // this.prepareDtgrSlsInv();
                        break;

                    case "A00MSIDETAILS":
                    case "A00ITEMSUMMARY":
                    case "A00TOPSUMMARY":
                        this.RptList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList2>();
                        int sln2 = 1;
                        var OldMemo = "xxxxxxxxxxxx";
                        foreach (var item2 in this.RptList1)
                        {
                            if (item2.invno != OldMemo)
                            {
                                OldMemo = item2.invno;
                                sln2 = 1;
                            }
                            item2.slnum = sln2;
                            sln2++;
                        }

                        this.RptList1 = RptList1.FindAll(x => x.invno.Substring(0, 3) == CmbShop);// || x.comcod == "AAAA");
                        this.PrintSalesDetailsList(RptList1, TrTyp);
                        //this.prepareDtgrSlsInv02();
                        break;
                    case "A00COLLDETAILS":
                        this.RptList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvColList01>();
                        this.RptList2 = RptList2.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                        this.PrintCollDetailsList(RptList2);
                        //this.prepareDtgrSlsInv03();
                        break;
                    case "A00DUEDETAILS":
                        this.RptList3 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvDuesList01>();
                        this.RptList3 = RptList3.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                        this.PrintDueDetailsList(RptList3);
                        // this.prepareDtgrSlsInv04();
                        break;
                    case "C01SIV":
                        this.RptList4 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
                        this.RptList4 = RptList4.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                        this.PrintSalesInvoiceList(RptList4);
                        // this.prepareDtgrSlsInv04();
                        break;
                    default:
                        break;
                }
            }
            else if (PrintId == "SS")
            {
                switch (TrTyp)
                {
                    case "A00MSISUM":
                        this.RptList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                        this.RptList = RptList.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                        this.prepareDtgrSlsInv();
                        break;
                    case "A00MSIDETAILS":

                        this.RptList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList2>();
                        this.RptList1 = RptList1.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                        this.prepareDtgrSlsInv02();
                        //PrintSalesDetailsList(RptList1);
                        break;
                    case "A00COLLDETAILS":
                        this.RptList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvColList01>();
                        this.RptList2 = RptList2.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                        //this.prepareDtgrSlsInv03();

                        break;
                    case "A00DUEDETAILS":
                        this.RptList3 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvDuesList01>();
                        this.RptList3 = RptList3.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                        // this.prepareDtgrSlsInv04();

                        PrintDueDetailsList(RptList3);
                        break;
                    default:
                        break;
                }
            }
            else if (PrintId == "PDF")
            {

            }
            else if (PrintId == "DP")
            {

            }
            else if (PrintId == "EXCELF")
            {

            }
            else if (PrintId == "WORD")
            {

            }
            else
            {
                return;
            }

        }


        private void prepareDtgrSlsInv()
        {
            Style style1 = new Style(typeof(DataGridCell));
            style1.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));

            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "SL No.", Binding = new Binding("slnum") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Memo No.", Binding = new Binding("invno1") });

            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Bill Amount", Binding = new Binding("billam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Collection Amount", Binding = new Binding("collam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Due Amount", Binding = new Binding("dueam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "VAT", Binding = new Binding("tvatam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Discount", Binding = new Binding("tdisam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });

            this.dgOverall01.ItemsSource = this.RptList;
            this.dgOverall01.Items.Refresh();
            ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
            if (cvTasks != null && cvTasks.CanGroup == true)
            {
                cvTasks.GroupDescriptions.Clear();
                cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("invdat", new RelativeDateValueConverter()));
            }
        }
        private void prepareDtgrSlsInv02()
        {
            Style style1 = new Style(typeof(DataGridCell));
            style1.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));

            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "SL No.", Binding = new Binding("slnum") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Item Description", Binding = new Binding("sirdesc"), Width = 350 });

            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Quantity", Binding = new Binding("invqty") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Amount", Binding = new Binding("inetam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });

            this.dgOverall01.ItemsSource = this.RptList1;
            this.dgOverall01.Items.Refresh();
            ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
            if (cvTasks != null && cvTasks.CanGroup == true)
            {
                cvTasks.GroupDescriptions.Clear();
                cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("invdat1", new RelativeDateValueConverter()));
            }
        }

        public void PrintSalesInvoiceList(List<HmsEntityCommercial.PhSalesInvoice01> list1)
        {
            try
            {
                LocalReport rpt1 = null;
                var pap1 = vm1.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                ds1.Tables[0].Rows[0]["slnum"] = Convert.ToInt32(ds1.Tables[2].Rows[0]["tokenid"]);

                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]));

                list3[0].RptFooter1 = "User : " + WpfProcessAccess.SignedInUserList[0].signinnam;

                var list2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                rpt1 = CommReportSetup.GetLocalReport("RetSales.RetSalesInv01", list1, list2, list3);
                string WindowTitle1 = "Due Details List";
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp1)
            {
                System.Windows.MessageBox.Show(exp1.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void PrintDueDetailsList(List<HmsEntityCommercial.InvDuesList01> list1)
        {
            string fromDate = xctk_dtpFrom.Text.ToString();
            string ToDate = xctk_dtpTo.Text.ToString();

            if (list1 == null)
                return;

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            list3[0].RptHeader1 = "Due Details List ";
            list3[0].RptHeader2 = " ( From  " + fromDate + "  To  " + ToDate + " )";
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhDueList01s", list1, null, list3);
            string WindowTitle1 = "Due Details List";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

        }

        public void PrintCollDetailsList(List<HmsEntityCommercial.InvColList01> list1)
        {
            string frmdat = xctk_dtpFrom.Text.ToString();
            string todat = xctk_dtpTo.Text.ToString();
            if (list1 == null)
                return;
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            list3[0].RptHeader1 = "Collection Details List";
            list3[0].RptHeader2 = " ( From  " + frmdat + "  To  " + todat + " )";
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhInvCollList01s", list1, null, list3);

            string WindowTitle1 = "Collection Details List";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        public void PrintSalesDetailsList(List<HmsEntityCommercial.InvoiceTransList2> list1, string TrTyp)
        {
            string fromDate = xctk_dtpFrom.Text.ToString();
            string ToDate = xctk_dtpTo.Text.ToString();
            
            if (list1 == null)
                return;
            //     case "A00MSIDETAILS":                    case "A00ITEMSUMMARY":

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            list3[0].RptHeader1 = (TrTyp == "A00TOPSUMMARY" ? "Overall Sales Summary" : (TrTyp == "A00ITEMSUMMARY" ? "Item wise Sales Summary" : "Sales Details List"));
            list3[0].RptHeader2 = " ( From  " + fromDate + "  To  " + ToDate + " )";
            list3[0].RptParVal1 = (TrTyp == "A00TOPSUMMARY" ? "TOPSUM" : "DETAILSSUM");
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesDetailsList1s", list1, null, list3);
            string WindowTitle1 = (TrTyp == "A00TOPSUMMARY" ? "Overall Sales Summary" : (TrTyp == "A00ITEMSUMMARY" ? "Item wise Sales Summary" : "Sales Transaction Details List"));
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        public void PrintTransecList(List<HmsEntityCommercial.InvoiceTransList> list1)
        {
            string fromDate = xctk_dtpFrom.Text.ToString();
            string ToDate = xctk_dtpTo.Text.ToString();

            if (list1 == null)
                return;
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            list3[0].RptHeader1 = "Sales Invoice List";
            list3[0].RptHeader2 = " ( From  " + fromDate + "  To  " + ToDate + " )";
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesTransList1s", list1, null, list3);
            string WindowTitle1 = "Sales Transaction List";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }
        private void cmbItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tvRptRtTitle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.dgOverall01.ItemsSource = null;
            this.dgOverall01.Columns.Clear();
            string ItemTitle = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString();
            string ItemTag = ((TreeViewItem)((TreeView)sender).SelectedItem).Tag.ToString();
            DateTime today1 = DateTime.Today;
            DateTime fromDate = today1;
            DateTime ToDate = today1;

            switch (ItemTag.Substring(0, 3))
            {
                case "A02": //"02. YESTERDAY DETAILS":
                case "B02": //"02. YESTERDAY DETAILS":
                    fromDate = today1.AddDays(-1);
                    ToDate = today1.AddDays(-1);
                    break;
                case "A03": //"03. THIS MONTH SALES INVOICE":
                case "B03": //"03. THIS MONTH SALES INVOICE":
                    fromDate = DateTime.Parse("01-" + today1.ToString("MMM-yyyy"));
                    ToDate = today1;//fromDate.AddMonths(1).AddDays(-1);
                    break;
                case "B04": //"04. THIS WEEK SALES INVOICE":
                case "B05": //"05. LAST WEEK SALES INVOICE":
                    DateTime today2 = today1;
                    int day2 = today2.Day;
                    if (ItemTag.Substring(0, 2) == "05")
                    {
                        today2 = (day2 >= 1 && day2 <= 7 ? today2.AddDays(8) : (day2 >= 8 && day2 <= 15 ?
                                DateTime.Parse("06-" + today2.ToString("MMM-yyyy")) :
                                (day2 >= 16 && day2 <= 22 ? DateTime.Parse("14-" + today2.ToString("MMM-yyyy")) :
                                 DateTime.Parse("20-" + today2.ToString("MMM-yyyy")))));
                    }


                    int day1 = today2.Day;
                    if (day1 >= 1 && day1 <= 7)
                    {
                        fromDate = DateTime.Parse("01-" + today2.ToString("MMM-yyyy"));
                        ToDate = DateTime.Parse("07-" + today2.ToString("MMM-yyyy"));
                    }
                    else if (day1 >= 8 && day1 <= 15)
                    {
                        fromDate = DateTime.Parse("08-" + today2.ToString("MMM-yyyy"));
                        ToDate = DateTime.Parse("15-" + today2.ToString("MMM-yyyy"));
                    }
                    else if (day1 >= 16 && day1 <= 22)
                    {
                        fromDate = DateTime.Parse("16-" + today2.ToString("MMM-yyyy"));
                        ToDate = DateTime.Parse("22-" + today2.ToString("MMM-yyyy"));
                    }
                    else
                    {
                        fromDate = DateTime.Parse("23-" + today2.ToString("MMM-yyyy"));
                        ToDate = DateTime.Parse("01-" + today2.ToString("MMM-yyyy")).AddMonths(1).AddDays(-1);
                    }
                    break;
            }

            this.xctk_dtpFrom.Value = fromDate;
            this.xctk_dtpTo.Value = ToDate;
            //this.ShowRequiredOptions(ItemTag);
            this.lbltle1.Content = ItemTitle;
            string Msg1 = (ItemTag == "C01TB" || ItemTag == "C02IS" ? "(This option is under construction)" : "");
            this.lbltle2.Content = Msg1;// ItemTag;
        }

        private void cmbSBrnCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void chkAsonDate_Click(object sender, RoutedEventArgs e)
        {
            this.stkpDateFrom.Visibility = (this.chkAsonDate.IsChecked == true ? Visibility.Hidden : Visibility.Visible);
        }

        private void tvRptRtTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnGenerate_Click(null, null);
        }

        private void tvRptRtTitle_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            this.cmbOutputOption.ComboBox_ContextMenuOpening(null, null);
        }

        private void tvRptRtTitle_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            string yy = this.cmbOutputOption.Uid.ToString();
            if (yy != "NONE")
                this.btnGenerate_Click(null, null);
        }
        private void tvRptRtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.btnGenerate_Click(null, null);
        }
   
    }
}
