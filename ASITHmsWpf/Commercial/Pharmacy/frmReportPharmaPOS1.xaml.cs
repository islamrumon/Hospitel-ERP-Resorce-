using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt4Commercial;
using ASITFunLib;
using ASITHmsEntity;
using System.Data;
using ASITHmsViewMan.Commercial;
using System.Windows.Data;
using System.ComponentModel;

namespace ASITHmsWpf.Commercial.Pharmacy
{
    /// <summary>
    /// Interaction logic for frmReportPharmaPOS1.xaml
    /// </summary>
    public partial class frmReportPharmaPOS1 : UserControl
    {
        string TitaleTag1, TitaleTag2;  // 

        private List<HmsEntityCommercial.InvoiceTransList> RptList = new List<HmsEntityCommercial.InvoiceTransList>();
        private List<HmsEntityCommercial.InvoiceTransList2> RptList1 = new List<HmsEntityCommercial.InvoiceTransList2>();
        private List<HmsEntityCommercial.InvColList01> RptList2 = new List<HmsEntityCommercial.InvColList01>();
        private List<HmsEntityCommercial.InvDuesList01> RptList3 = new List<HmsEntityCommercial.InvDuesList01>();
        private vmReportPharRestPOS1 vm1 = new vmReportPharRestPOS1();
        public frmReportPharmaPOS1()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            TitaleTag1 = this.Tag.ToString();   // Predefined value of Tag property set at design time
        }      

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            TitaleTag2 = this.Tag.ToString(); // Dynamic value of Tag property set at design time

            this.xctk_dtpFrom.Value = Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.xctk_dtpTo.Value = DateTime.Today;
            TreeViewItem tvi1 = new TreeViewItem() { Header = "A. SALES INVOICE SUMMARY LIST", Tag = "A00MSISUM" };
            TreeViewItem tvi2 = new TreeViewItem() { Header = "B. SALES DETAILS TRANSECTION LIST", Tag = "A00MSIDETAILS" };
            TreeViewItem tvi3 = new TreeViewItem() { Header = "C. COLLECTION DETAILS TRANSECTION LIST", Tag = "A00COLLDETAILS" };

            TreeViewItem tvi4 = new TreeViewItem() { Header = "D. GROUP WISE SUMMARY REPORTS", Tag = "000" };
            TreeViewItem tvi5 = new TreeViewItem() { Header = "E. OVERALL SUMMARY REPORTS", Tag = "000" };
            TreeViewItem tvi6 = new TreeViewItem() { Header = "F. INVOICE DUE SUMMARY LIST", Tag = "A00DUEDETAILS" };

            this.tvRptTitle.Items.Add(tvi1);
            this.tvRptTitle.Items.Add(tvi2);
            this.tvRptTitle.Items.Add(tvi3);
            this.tvRptTitle.Items.Add(tvi4);
            this.tvRptTitle.Items.Add(tvi5);
            this.tvRptTitle.Items.Add(tvi6);

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
            this.cmbShop.SelectedIndex = (TitaleTag2.Contains("MediShop") ? 0 : (TitaleTag2.Contains("FoodShop") ? 1 : 2));
            //MediShop      FoodShop,  HardwareShop

        }

        private void cmbItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (dgOverall01.Columns.Count > 0)
            {
                dgOverall01.Columns.Clear();
            }
            dgOverall01.ItemsSource = null;
            dgOverall01.Items.Refresh();
            dgOverall01.AutoGenerateColumns = false;
            
            if ((TreeViewItem)tvRptTitle.SelectedItem == null)
            {
                return;
            }
            string TrHead = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString().ToUpper();
            
            string TrTyp = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Tag.ToString();

            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            switch (TrTyp)
            {
                case "A00MSISUM": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "A00MSIDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "A00COLLDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                case "A00DUEDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                //case "B0": this.GetSuimmaryRpt(TrHead, TrTyp, PrintId); break;
                //case "C0": this.GetTransecList(TrHead, TrTyp, PrintId); break;
                //case "D0": this.GetTransDetails(TrHead, TrTyp, PrintId); break;
            }
        }

        private void GetSumTransListReport(string TrHead, string TrTyp, string PrintId)
        {
            string fromDate = xctk_dtpFrom.Text.ToString().Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();
            string Dept01 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();            
            string Dept02 = ((ComboBoxItem)this.cmbSectCod2.SelectedItem).Tag.ToString().Trim();            
            string CmbShop = ((ComboBoxItem)this.cmbShop.SelectedItem).Tag.ToString();        
           
            this.lbltle1.Content = TrHead.Remove(0, 2);
            this.lbltle2.Content = " From " + fromDate + " To " + ToDate;
            var pap1 = vm1.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, TrTyp, fromDate, ToDate, Dept01, Dept02);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            if (ds1 == null)
                return;

            this.RptList.Clear();


            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF")
            {
                switch (TrTyp)
                {
                    case "A00MSISUM":
                         this.RptList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                 if (CmbShop == "MSI")                    
                    this.RptList = RptList.FindAll(x => x.invno.Substring(0, 3) == "MSI");                    
                 else
                    this.RptList = RptList.FindAll(x => x.invno.Substring(0, 3) == "FSI");
               this.PrintTransecList(RptList);
               this.prepareDtgrSlsInv();
                        break;
                    case "A00MSIDETAILS":
                        this.RptList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList2>();
                        if (CmbShop == "MSI")
                            this.RptList1 = RptList1.FindAll(x => x.invno.Substring(0, 3) == "MSI");
                        else
                            this.RptList1 = RptList1.FindAll(x => x.invno.Substring(0, 3) == "FSI");
                        this.PrintSalesDetailsList(RptList1);
                        this.prepareDtgrSlsInv02();
                        break;
                    case "A00COLLDETAILS":
                        this.RptList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvColList01>();
                        if (CmbShop == "MSI")
                            this.RptList2 = RptList2.FindAll(x => x.invno.Substring(0, 3) == "MSI");
                        else
                            this.RptList2 = RptList2.FindAll(x => x.invno.Substring(0, 3) == "FSI");
                        this.PrintCollDetailsList(RptList2);
                        this.prepareDtgrSlsInv03();
                        break;
                    case "A00DUEDETAILS":
                        this.RptList3 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvDuesList01>();
                        if (CmbShop == "MSI")
                            this.RptList3 = RptList3.FindAll(x => x.invno.Substring(0, 3) == "MSI");
                        else
                            this.RptList3 = RptList3.FindAll(x => x.invno.Substring(0, 3) == "FSI");
                        this.PrintDueDetailsList(RptList3);
                        this.prepareDtgrSlsInv04();
                        break;
                    default:
                        break;
                }
            }
            else if(PrintId == "SS")
            {
                switch (TrTyp)
                {
                    case "A00MSISUM":
                        this.RptList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                 if (CmbShop == "MSI")                    
                    this.RptList = RptList.FindAll(x => x.invno.Substring(0, 3) == "MSI");                    
                 else
                    this.RptList = RptList.FindAll(x => x.invno.Substring(0, 3) == "FSI");
                 this.prepareDtgrSlsInv();
                             
                        break;
                    case "A00MSIDETAILS":

                        this.RptList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList2>();
                        if (CmbShop == "MSI")
                            this.RptList1 = RptList1.FindAll(x => x.invno.Substring(0, 3) == "MSI");
                        else
                            this.RptList1 = RptList1.FindAll(x => x.invno.Substring(0, 3) == "FSI");
                        this.prepareDtgrSlsInv02();
                        
                    //PrintSalesDetailsList(RptList1);
                        break;
                    case "A00COLLDETAILS":
                        this.RptList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvColList01>();
                        if (CmbShop == "MSI")
                            this.RptList2 = RptList2.FindAll(x => x.invno.Substring(0, 3) == "MSI");
                        else
                            this.RptList2 = RptList2.FindAll(x => x.invno.Substring(0, 3) == "FSI");

                         this.prepareDtgrSlsInv03();
                         
                        break;
                    case "A00DUEDETAILS":
                        this.RptList3 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvDuesList01>();
                        if (CmbShop == "MSI")
                            this.RptList3 = RptList3.FindAll(x => x.invno.Substring(0, 3) == "MSI");
                        else
                            this.RptList3 = RptList3.FindAll(x => x.invno.Substring(0, 3) == "FSI");

                         this.prepareDtgrSlsInv04();
                        
                        //PrintDueDetailsList(RptList3);
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
                
        #region DataGrid Dynamic
        private void prepareDtgrSlsInv()
        {
            Style style2 = new Style(typeof(DataGridCell));
            style2.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
            //style2.Setters.Add(new Setter(TextBlock.ForegroundProperty, new RedValues()));
            string nFormat = "#,##0.00;-#,##0.00; ";
            //string dtFormat = "MMM-yyyy";
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Sl#", Binding = new Binding("slnum") });
            

            //Binding binddt = new Binding("invdat1") { StringFormat = "dd-MM-yyyy" };
            //DataGridTemplateColumn Column4 = new DataGridTemplateColumn();
            //Column4.Header = "Date";
            //FrameworkElementFactory dtFactory4 = new FrameworkElementFactory(typeof(DatePicker));
            //dtFactory4.SetValue(DatePicker.SelectedDateProperty, binddt.StringFormat = "dd-MM-yyyy");
            ////dtFactory4.SetValue(DatePicker.SelectedDateFormatProperty);
            //DataTemplate comboTemplate4 = new DataTemplate();
            //comboTemplate4.VisualTree = dtFactory4;
            //Column4.CellTemplate = comboTemplate4;
            //Column4.CellEditingTemplate = comboTemplate4;
            //dgOverall01.Columns.Add(Column4);
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Location", Binding = new Binding("sectname") });
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Date", Binding = new Binding("invdat1") { StringFormat = "dd-MM-yyyy" } });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Memo No", Binding = new Binding("invno1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Bill Amount", Binding = new Binding("billam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Collection Amount", Binding = new Binding("collam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Due Amount", Binding = new Binding("dueam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Discount Amount", Binding = new Binding("tdisam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Total Amount", Binding = new Binding("totslam") { StringFormat = nFormat }, CellStyle = style2 });
            this.dgOverall01.ItemsSource = RptList;
            ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
            if (cvTasks != null && cvTasks.CanGroup == true)
            {
                cvTasks.GroupDescriptions.Clear();
                //string title = string.Format("{0} - {1} {2}", entryViewModel.Id, entryViewModel.Numar, entryViewModel.Obiect);
                cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("sectname"));                
               cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("invdat1"));
                //cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("sectname"));
            }        

        }

        private void prepareDtgrSlsInv02()
        {
            Style style2 = new Style(typeof(DataGridCell));
            style2.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
            //style2.Setters.Add(new Setter(TextBlock.ForegroundProperty, new RedValues()));
            string nFormat = "#,##0.00;-#,##0.00; ";
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Sl#", Binding = new Binding("slnum") });
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Location", Binding = new Binding("sectname") });
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Date", Binding = new Binding("invdat1") { StringFormat = "dd-MM-yyyy" } });            
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Inv. Code", Binding = new Binding("invno1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Item Description", Binding = new Binding("sirdesc") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Quantity", Binding = new Binding("invqty") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Unit", Binding = new Binding("sirunit") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Rate", Binding = new Binding("itmrat") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Amount", Binding = new Binding("itmam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Bill Amount", Binding = new Binding("billam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Due Amount", Binding = new Binding("dueam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Discount Amount", Binding = new Binding("tdisam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Total Amount", Binding = new Binding("totslam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Prepare By", Binding = new Binding("invbyName") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Narration", Binding = new Binding("invnar") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Referance", Binding = new Binding("invref") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Customer Name", Binding = new Binding("custName"), Width = 220 });
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Other Ref.", Binding = new Binding("invno1") });

            this.dgOverall01.ItemsSource = RptList1;
            ICollectionView cvTasks1 = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
            if (cvTasks1 != null && cvTasks1.CanGroup == true)
            {
                cvTasks1.GroupDescriptions.Clear();
                cvTasks1.GroupDescriptions.Add(new PropertyGroupDescription("sectname"));
                cvTasks1.GroupDescriptions.Add(new PropertyGroupDescription("invdat1"));
                cvTasks1.GroupDescriptions.Add(new PropertyGroupDescription("invno1"));
            }
        }


        private void prepareDtgrSlsInv04()
        {
            Style style2 = new Style(typeof(DataGridCell));
            style2.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
            //style2.Setters.Add(new Setter(TextBlock.ForegroundProperty, new RedValues()));
            string nFormat = "#,##0.00;-#,##0.00; ";
            string pFormat = "#,##0.00 %;-#,##0.00 %; ";
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Sl#", Binding = new Binding("slnum") });
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Location", Binding = new Binding("sectname") });            
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Inv. Date", Binding = new Binding("invdat1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Inv. No", Binding = new Binding("invno1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Coll. Date", Binding = new Binding("maxcoldat1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Bill. Amount.", Binding = new Binding("billam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Coll. Amount", Binding = new Binding("collam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Due Amt.", Binding = new Binding("duesam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Due %", Binding = new Binding("dueper")  { StringFormat = pFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Reference", Binding = new Binding("invref") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Narration", Binding = new Binding("invnar") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Note", Binding = new Binding("bcnote") });
            this.dgOverall01.ItemsSource = RptList3;
            ICollectionView cvTasks3 = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
            if (cvTasks3 != null && cvTasks3.CanGroup == true)
            {
                cvTasks3.GroupDescriptions.Clear();
                cvTasks3.GroupDescriptions.Add(new PropertyGroupDescription("sectname"));
                cvTasks3.GroupDescriptions.Add(new PropertyGroupDescription("invdat1"));
            }
        }

        private void prepareDtgrSlsInv03()
        {
            Style style2 = new Style(typeof(DataGridCell));
            style2.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
            
            //style2.Setters.Add(new Setter(TextBlock.ForegroundProperty, new RedValues()));
            string nFormat = "#,##0.00;-#,##0.00; ";
            string dtfrm = "hh:mm:ss tt";
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Sl#", Binding = new Binding("slnum") });
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Location", Binding = new Binding("sectname") });
            //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Date", Binding = new Binding("coldat1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Inv. Date", Binding = new Binding("invdat1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Inv. No", Binding = new Binding("invno1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Coll. Time", Binding = new Binding("bilcoldat1") });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Total Coll.", Binding = new Binding("bilcolam") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Current Coll.", Binding = new Binding("currcol") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Due Coll.", Binding = new Binding("duecol") { StringFormat = nFormat }, CellStyle = style2 });
            dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Note", Binding = new Binding("bcnote") });
            this.dgOverall01.ItemsSource = RptList2;
            ICollectionView cvTasks2 = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
            if (cvTasks2 != null && cvTasks2.CanGroup == true)
            {
                cvTasks2.GroupDescriptions.Clear();
                cvTasks2.GroupDescriptions.Add(new PropertyGroupDescription("sectname"));
                cvTasks2.GroupDescriptions.Add(new PropertyGroupDescription("coldat1"));
            }
        }
        #endregion
        
        public void PrintDueDetailsList(List<HmsEntityCommercial.InvDuesList01> list1)
        {
            string frmdat = xctk_dtpFrom.Text.ToString();
            string todat = xctk_dtpTo.Text.ToString();
            if (list1 == null)
                return;
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime:DateTime.Now);
            list3[0].RptHeader1 = "Due Details List (" + frmdat + "  To  " + todat + " )";
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhDueList01", list1, null, list3);
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
            list3[0].RptHeader1 = "Collection Details List ( " + frmdat + "  To  " + todat + " )";
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhInvCollList01", list1, null, list3);
            string WindowTitle1 = "Collection Details List";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

        }

        public void PrintSalesDetailsList(List<HmsEntityCommercial.InvoiceTransList2> list1)
        {
            string frmdat = xctk_dtpFrom.Text.ToString();
            string todat = xctk_dtpTo.Text.ToString();
            if (list1 == null)
                return;
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            list3[0].RptHeader1 = "Sales Details List ( " + frmdat + "  To  " + todat + " )";
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesDetailsList1", list1, null, list3);
            string WindowTitle1 = "Sales Transaction Details List";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

        }

        public void PrintTransecList(List<HmsEntityCommercial.InvoiceTransList> list1)
        {
            string frmdat = xctk_dtpFrom.Text.ToString();
            string todat = xctk_dtpTo.Text.ToString();
            if (list1 == null)
                return;
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            list3[0].RptHeader1 = "Sales Invoice List ( " + frmdat + "  To  " + todat +" )";
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesTransList1", list1, null, list3);
            string WindowTitle1 = "Sales Transaction List";
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

        }
        private void dgOverall01_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item1a = (HmsEntityCommercial.InvoiceTransList)this.dgOverall01.SelectedItem;
                this.GetMemoList(item1a.invno);
            }
            catch (Exception)
            {
                
                return;
            }
            
        }
        private void GetMemoList(string memoNum)
        {
            LocalReport rpt1 = null;
            var pap1 = vm1.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var list1 = ds1.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
            var list2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
           // var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();
            rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesInv01", list1, list2, null);

            string WindowTitle1 = "Sales Memo";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

        }

        private void dgOverall01_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

  
    }
}
