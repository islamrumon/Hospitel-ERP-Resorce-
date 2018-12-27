using System;
using System.Collections;
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
using ASITHmsViewMan.General;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ASITHmsWpf.Commercial.ParkTicket
{
    /// <summary>
    /// Interaction logic for frmEntryParkPOS103.xaml
    /// </summary>
    public partial class frmEntryParkPOS103 : UserControl
    {
        private string TitaleTag1, TitaleTag2;  // 
        private bool FrmInitialized = false;
        private DataGrid dgRpt1;

        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private List<HmsEntityGeneral.UserInterfaceAuth.AppUserList> UserList1 = new List<HmsEntityGeneral.UserInterfaceAuth.AppUserList>();
        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemMainGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();
        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();

        private List<vmEntryPharRestPOS1.RetSaleItem> RetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();
        private List<vmEntryPharRestPOS1.RetSaleItem> ShortRetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();

        private List<HmsEntityCommercial.ParkSalesTrans01> rptTransList01 = new List<HmsEntityCommercial.ParkSalesTrans01>();
        private vmEntryPharRestPOS1 vm1o = new vmEntryPharRestPOS1();
        private vmEntryReportPark1 vmr1 = new vmEntryReportPark1();
        public frmEntryParkPOS103()
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

            TreeViewItem tvi1 = new TreeViewItem() { Header = "A. TRANSECTION LIST", Tag = "A000000000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi2 = new TreeViewItem() { Header = "B. SUMMARY LIST", Tag = "B00000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };

            tvi1.Items.Add(new TreeViewItem { Header = "01. Ticket sales details", Tag = "A01SALESDETAILS" });
            tvi1.Items.Add(new TreeViewItem { Header = "02. Ticket cancel details", Tag = "A02CANCELDETAILS" });

            tvi2.Items.Add(new TreeViewItem { Header = "01. Ticket sales summary", Tag = "B01SALESSUM01" });
            tvi2.Items.Add(new TreeViewItem { Header = "01. Ticket cancel summary", Tag = "B02CANCELSUM01" });
            
            tvi1.IsExpanded = true;
            tvi2.IsExpanded = true;

            this.tvRptRtTitle.Items.Add(tvi1);
            this.tvRptRtTitle.Items.Add(tvi2);

            TitaleTag2 = this.Tag.ToString();
            this.xctk_dtpFrom.Value = DateTime.Today; //Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.xctk_dtpTo.Value = DateTime.Today;

            this.GetSectionList();
            this.GetTerminalList();
            this.GetRetailItemList();
        }


        private void GetSectionList()
        {
            this.cmbSectCod.Items.Clear();
            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
            foreach (var itemd1 in deptList1)
            {
                if (itemd1.sectname.ToUpper().Contains("PARK"))
                {
                    this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                }
            }
            this.cmbSectCod.IsEnabled = (this.cmbSectCod.Items.Count == 1 ? false : true);
            this.cmbSectCod.SelectedIndex = 0;
        }
        private void GetTerminalList()
        {
            if (WpfProcessAccess.GenInfoTitleList == null)
                WpfProcessAccess.GetGenInfoTitleList();

            string comcod1 = WpfProcessAccess.CompInfList[0].comcod;
            var TerminalList1 = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 8) == "CBWS" + WpfProcessAccess.CompInfList[0].comcod).ToList();

            this.cmbTrmID.Items.Clear();
            foreach (var item in TerminalList1)
            {
                this.cmbTrmID.Items.Add(new ComboBoxItem() { Content = item.actdesc.Trim(), Tag = item.acttdesc });
            }
            var pap1 = vmGenList1.SetParamAppUserList(WpfProcessAccess.CompInfList[0].comcpcod, "%");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.UserList1 = ds1.Tables[0].DataTableToList<HmsEntityGeneral.UserInterfaceAuth.AppUserList>();
            this.autoUserSearch.ContextMenu.Items.Clear();
            foreach (var item in this.UserList1)
            {
                item.userrmrk = item.hccode.Substring(6, 6) + " - " + item.signinnam.Trim();
                MenuItem mnu1 = new MenuItem { Header = item.userrmrk, Tag = item.hccode };
                mnu1.Click += autoUserSearch_ContextMenu_MouseClick;
                this.autoUserSearch.ContextMenu.Items.Add(mnu1);
            }
        }

        private void autoUserSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.autoUserSearch.ItemsSource = this.UserList1;
                this.autoUserSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim();

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-25: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }



        private void GetRetailItemList()
        {
            this.RetSaleItemList.Clear();
            //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4171", reqmfginf: "WITHOUTMFGINFO");
            //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4521", reqmfginf: "WITHMFGINFO");
            //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "0[14]51", reqmfginf: "WITHOUTMFGINFO");
            //var pap = vm1a.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "0151", reqmfginf: "WITHOUTMFGINFO");
            var pap = vm1o.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4144", reqmfginf: "WITHOUTMFGINFO");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
            if (ds1 == null)
                return;

            this.RetSaleItemGroupList = ds1.Tables[1].DataTableToList<vmEntryPharRestPOS1.RetSaleItemGroup>();
            DataRow[] dr1 = ds1.Tables[0].Select();
            DataRow[] dr2 = ds1.Tables[1].Select();
            DataRow[] dr3 = ds1.Tables[2].Select();
            foreach (DataRow row1 in dr1)
            {
                var itm1 = new vmEntryPharRestPOS1.RetSaleItem(row1["sircode"].ToString(), row1["sircode"].ToString().Substring(6, 6) + " : " + row1["sirdesc"].ToString(), Convert.ToDecimal(row1["costprice"]),
                        Convert.ToDecimal(row1["saleprice"]), Convert.ToDecimal(row1["refscomp"]), Convert.ToDecimal(row1["salvatp"]), row1["sirtype"].ToString(), row1["sirunit"].ToString(), row1["sirunit2"].ToString(),
                        row1["sirunit3"].ToString(), decimal.Parse("0" + row1["siruconf"].ToString()), decimal.Parse("0" + row1["siruconf3"].ToString()), row1["msircode"].ToString(), row1["msirdesc"].ToString(),
                        row1["msirdesc"].ToString().Trim() + " - " + row1["sirdesc"].ToString(), row1["sircode"].ToString().Substring(6), row1["mfgid"].ToString(), row1["mfgcomnam"].ToString(),
                        (row1["mfgcomnam"].ToString().Trim().Length > 0 ? "Visible" : "Collapsed"), "Collapsed", null);
                this.RetSaleItemList.Add(itm1);
            }
            foreach (DataRow itemr in dr3)
            {
                this.RetSaleItemMainGroupList.Add(new vmEntryPharRestPOS1.RetSaleItemGroup()
                {
                    msircode = itemr["msirtype"].ToString(),
                    msirdesc = itemr["msirtype"].ToString(),
                    msirtype = itemr["msirtype"].ToString()
                });
            }

            foreach (DataRow row2 in dr2)
            {
                this.cmbItemGroup.Items.Add(new ComboBoxItem() { Content = row2["msirdesc"].ToString(), Tag = row2["msircode"].ToString() });
            }

            this.cmbItemGroup.SelectedIndex = 0;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if ((TreeViewItem)tvRptRtTitle.SelectedItem == null)
                return;

            string TrHead = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Header.ToString().ToUpper();
            string TrTyp = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Tag.ToString();

            string Date1a = this.xctk_dtpFrom.Text;
            string Date2a = this.xctk_dtpTo.Text;

            string termID1 = ((ComboBoxItem)this.cmbTrmID.SelectedItem).Tag.ToString().Trim();
            string appUser1 = "";
            if (this.autoUserSearch.SelectedValue != null)
                appUser1 = this.autoUserSearch.SelectedValue.ToString();

            string ItemGrp1 = ((ComboBoxItem)(this.cmbItemGroup.SelectedItem)).Tag.ToString().Trim().Substring(0, 7);
            string ItemGrp1des = ((ComboBoxItem)(this.cmbItemGroup.SelectedItem)).Content.ToString().Trim();

            string itemCode1 = "";
            string itemCode1des = "";

            if (this.autoItemSearch.SelectedValue != null)
            {
                itemCode1 = this.autoItemSearch.SelectedValue.ToString().Trim();
                itemCode1des = this.autoItemSearch.Text.Trim();
            }
            ItemGrp1 = (ItemGrp1 == "0000000" ? "%" : (itemCode1.Length > 0 ? itemCode1 : ItemGrp1));

            ItemGrp1des = (ItemGrp1 == "%" ? "" : (itemCode1.Length > 0 ? itemCode1des : ItemGrp1des));
            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();

            string prtOpt1 = ""; // "DCTU"
            prtOpt1 = prtOpt1 + (this.chkDate1.IsChecked == true ? "D" : "");
            prtOpt1 = prtOpt1 + (this.chkCoupon1.IsChecked == true ? "C" : "");
            prtOpt1 = prtOpt1 + (this.chkTerminal1.IsChecked == true ? "T" : "");
            prtOpt1 = prtOpt1 + (this.chkUser1.IsChecked == true ? "U" : "");
            string Status1 = (TrTyp == "A02CANCELDETAILS" || TrTyp == "B02CANCELSUM01" ? "C" : "A");
            switch (TrTyp)
            {
                case "A01SALESDETAILS":
                case "A02CANCELDETAILS": this.TicketSalesTrans01(ProcessID1: "POTCOUPONLIST01", Date1: Date1a, Date2: Date1a, isircode1: itemCode1, TerminalID1: termID1, UserID1: appUser1, 
                    RptOpt1: prtOpt1, Status1: Status1, PrintId1: PrintId); break;  // TrHead, TrTyp, PrintId
                case "B01SALESSUM01":
                case "B02CANCELSUM01": 
                    this.TicketSalesTrans01(ProcessID1: "POTCOUPONSUM01", Date1: Date1a, Date2: Date2a, isircode1: itemCode1, TerminalID1: termID1, UserID1: appUser1,
                    RptOpt1: prtOpt1, Status1: Status1, PrintId1: PrintId); break; // TrHead, TrTyp, PrintId
            }
        }


        private void TicketSalesTrans01(string ProcessID1 = "POTCOUPONLIST01", string Date1 = "01-Apr-2018", string Date2 = "02-Apr-2018", string isircode1 = "", string TerminalID1 = "",
            string UserID1 = "", string RptOpt1 = "DCTU", string Status1 = "A", string PrintId1 = "PP")
        {
            try
            {
                this.dgOverall01.ItemsSource = null;
                LocalReport rpt1 = null;
                this.rptTransList01.Clear();
                string WindowTitle1 = "";
                var pap = vmr1.SetParamParkTicketTrans01(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: ProcessID1, MemoDate1: Date1, MemoDate2: Date2, isircode: isircode1,
                               TerminalID: TerminalID1, UserID: UserID1, RptOption: RptOpt1, Status1: Status1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
                if (ds1 == null)
                    return;
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]));
                list3[0].RptHeader1 = (Status1 == "C" ? "Ticket Cancel " : "Sales ") + (ProcessID1 == "POTCOUPONLIST01" ? "Details List" : "Summary") + 
                    " (" + (Date1 == Date2 ? "For " + Date1 : "From " + Date1 + " To " + Date2) + ")";

                //list3[0].RptFooter1 = "User : " + WpfProcessAccess.SignedInUserList[0].signinnam;

                this.rptTransList01 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.ParkSalesTrans01>();

                if (ProcessID1 == "POTCOUPONLIST01")
                {
                    rpt1 = CommReportSetup.GetLocalReport("ParkSales.RptParkSalesList01", this.rptTransList01, null, list3);
                    WindowTitle1 = "Park Ticket " + (Status1 == "C" ? "Cancel " : "Sales ") + "Details-01";
                }
                else
                {
                    rpt1 = CommReportSetup.GetLocalReport("ParkSales.RptParkSalesSum01", this.rptTransList01, null, list3);
                    WindowTitle1 = "Park Ticket " + (Status1 == "C" ? "Cancel " : "Sales ") + " Summary-01";
                }


                if (PrintId1 == "PP" || PrintId1 == "NP" || PrintId1 == "PDF")
                {
                    string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                    string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
                }
                else if (PrintId1 == "SS")
                {
                    this.ShowGridInfo(ProcessID1);
                }


                //WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: "PrintLayout");

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI.Rpt-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ShowGridInfo(string ProcessID1)
        {
            this.dgOverall01.ItemsSource = this.rptTransList01;
        }
        private void tvRptRtTitle_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            this.cmbOutputOption.ComboBox_ContextMenuOpening(null, null);
        }

        private void cmbItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.autoItemSearch.ItemsSource = null;
                string msircode1 = ((ComboBoxItem)this.cmbItemGroup.SelectedItem).Tag.ToString();
                this.ShortRetSaleItemList = this.RetSaleItemList.FindAll(x => x.msircode == msircode1).ToList();
                if (msircode1 == "000000000000")
                    ShortRetSaleItemList = this.RetSaleItemList.ToList();

                this.autoItemSearch.ContextMenu.Items.Clear();
                foreach (var item in this.ShortRetSaleItemList)
                {
                    MenuItem mnu1 = new MenuItem { Header = item.sirdesc, Tag = item.sircode };
                    mnu1.Click += autoItemSearch_ContextMenu_MouseClick;
                    this.autoItemSearch.ContextMenu.Items.Add(mnu1);
                }

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-22: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void autoItemSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.autoItemSearch.ItemsSource = this.ShortRetSaleItemList;
                this.autoItemSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim();

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-23: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void autoItemSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
        }
        private ObservableCollection<vmEntryPharRestPOS1.RetSaleItem> GetItemSirdesc(string Pattern)
        {
            var GrpCod1 = ((ComboBoxItem)this.cmbItemGroup.SelectedItem).Tag.ToString().Trim();
            if (GrpCod1 != "000000000000")
                return new ObservableCollection<vmEntryPharRestPOS1.RetSaleItem>(
                    this.RetSaleItemList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim()) && x.sircode.Substring(0, 7) == GrpCod1.Substring(0, 7)).Take(100));
            else
                return new ObservableCollection<vmEntryPharRestPOS1.RetSaleItem>(
                    this.RetSaleItemList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }
        private void chkAsonDate_Click(object sender, RoutedEventArgs e)
        {
            this.stkpDateFrom.Visibility = (this.chkAsonDate.IsChecked == true ? Visibility.Hidden : Visibility.Visible);
        }


        private void tvRptRtTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnGenerate_Click(null, null);
        }
        private void tvRptRtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.btnGenerate_Click(null, null);
        }
        private void tvRptRtTitle_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            string yy = this.cmbOutputOption.Uid.ToString();
            if (yy != "NONE")
                this.btnGenerate_Click(null, null);
        }

        private void autoStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

        }

        private void autoItemSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoItemSearch.ContextMenu.IsOpen = true;
        }

        private void tvRptRtTitle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string ItemTitle = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString();
            string ItemTag = ((TreeViewItem)((TreeView)sender).SelectedItem).Tag.ToString();
            this.ShowRequiredOptions(ItemTag);
            this.lbltle1.Content = ItemTitle;
            string Msg1 = "";
            this.lbltle2.Content = Msg1;// ItemTag;
        }

        private void ShowRequiredOptions(string ItemTag)
        {
            this.stkOptLocation.Visibility = Visibility.Hidden;
            this.stkOptTerminal.Visibility = Visibility.Hidden;
            this.stkpOptItemGroup.Visibility = Visibility.Hidden;
            this.stkpOptItem.Visibility = Visibility.Hidden;
            this.stkOptUser.Visibility = Visibility.Hidden;
            this.stkRptOptions.Visibility = Visibility.Hidden;

            this.chkDate1.IsChecked = true;
            this.chkCoupon1.IsChecked = true;
            this.chkTerminal1.IsChecked = false;
            this.chkUser1.IsChecked = false;
            
            this.chkDate1.IsEnabled = true;
            this.chkCoupon1.IsEnabled = true;
            this.chkTerminal1.IsEnabled = true;
            this.chkUser1.IsEnabled = true;

            if (ItemTag == "A01SALESDETAILS" || ItemTag == "A02CANCELDETAILS")
            {
                this.chkDate1.IsEnabled = false;
                this.chkCoupon1.IsEnabled = false;
                this.xctk_dtpTo.Value = this.xctk_dtpFrom.Value;
                this.stkOptTerminal.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkpOptItemGroup.Visibility = Visibility.Visible;
                this.stkpOptItem.Visibility = Visibility.Visible;
                this.stkOptUser.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "B01SALESSUM01" || ItemTag == "B02CANCELSUM01")
            {
                this.stkOptTerminal.Visibility = Visibility.Visible;
                this.stkOptLocation.Visibility = Visibility.Visible;
                this.stkpOptItemGroup.Visibility = Visibility.Visible;
                this.stkpOptItem.Visibility = Visibility.Visible;
                this.stkOptUser.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }

            //else if (ItemTag == "A04RPCB")
            //{
            //    // <TreeViewItem Header="01. RECEIPTS &amp; PAYMENTS" Tag = "B01RPCB"/>
            //    // <TreeViewItem Header = "04. Cash book details (R/P form)", Tag = "A04RPCB"/>;
            //    this.stkOptLocation.Visibility = Visibility.Visible;
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //}
            //else if (ItemTag == "B01RPCB")
            //{
            //    // <TreeViewItem Header="01. RECEIPTS &amp; PAYMENTS" Tag = "B01RPCB"/>
            //    this.stkOptLocation.Visibility = Visibility.Visible;
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //    this.stkOptTB.Visibility = Visibility.Visible;
            //    this.stkOptMore.Visibility = Visibility.Visible;
            //    this.cmbSubLevel.SelectedIndex = 0;
            //}
            //else if (ItemTag == "B04CL")
            //{
            //    // <TreeViewItem Header="04. CONTROL LEDGER" Tag = "B04CL"/>
            //    // (3 Reports) Control Ledger, Control Ledger Voucher Summary, Control Ledger Transaction Head Summary
            //    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary", Tag = "VOUSUMMARY,WITHOUTNARR" });
            //    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary with Narration", Tag = "VOUSUMMARY,NARRATION" });
            //    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary", Tag = "TRNSUMMARY,NOLOCATION" });
            //    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary with Location", Tag = "TRNSUMMARY,LOCATIONWISE" });
            //    this.cmbRptOptions.SelectedIndex = 0;
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //    this.stkOptLocation.Visibility = Visibility.Visible;
            //    this.stkOptActCode.Visibility = Visibility.Visible;
            //    this.stkRptOptions.Visibility = Visibility.Visible;
            //}
            //else if (ItemTag == "B05SL")
            //{
            //    // <TreeViewItem Header="05. SUBSIDIARY LEDGER" Tag="B05SL"/>
            //    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Subsidiary Ledger - Short Form", Tag = "SHORTLEDGER,NARRATION" });
            //    ////this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary", Tag = "VOUSUMMARY,WITHOUTNARR" });
            //    ////this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary with Narration", Tag = "VOUSUMMARY,NARRATION" });
            //    ////this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary", Tag = "TRNSUMMARY,NOLOCATION" });
            //    ////this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary with Location", Tag = "TRNSUMMARY,LOCATIONWISE" });
            //    this.cmbRptOptions.SelectedIndex = 0;
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //    this.stkOptLocation.Visibility = Visibility.Visible;
            //    this.stkOptActCode.Visibility = Visibility.Visible;
            //    this.stkOptSirCode.Visibility = Visibility.Visible;
            //    this.stkRptOptions.Visibility = Visibility.Visible;
            //}
            //else if (ItemTag == "B06CS")
            //{
            //    // <TreeViewItem Header="06. CONTROL SCHEDULE" Tag="B06CS"/>
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //    this.stkOptLocation.Visibility = Visibility.Visible;
            //    this.stkOptActCode.Visibility = Visibility.Visible;
            //    this.stkOptTB.Visibility = Visibility.Visible;
            //    this.stkOptMore.Visibility = Visibility.Visible;
            //    this.stkOptSchedule.Visibility = Visibility.Visible;
            //    this.stkOptDrCrColumns.Visibility = Visibility.Visible;
            //    this.cmbPeriod.SelectedIndex = 0;
            //    this.cmbDrCr.SelectedIndex = 0;
            //}
            //else if (ItemTag == "B07IVE")
            //{
            //    //TreeViewItem { Header = "07. Income Vs Expense", Tag = "B07IVE" };
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //    this.stkOptLocation.Visibility = Visibility.Visible;
            //}
            //else if (ItemTag == "C01TB")
            //{
            //    //  <TreeViewItem Header="01. TRIAL BALANCE" Tag = "C01TB"/>
            //    this.stkOptTB.Visibility = Visibility.Visible;
            //    this.stkOptMore.Visibility = Visibility.Visible;
            //    this.cmbSubLevel.SelectedIndex = 0;
            //    this.stkOptSchedule.Visibility = Visibility.Visible;
            //    this.cmbPeriod.SelectedIndex = 0;
            //}
            //else if (ItemTag == "C02IS")
            //{
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //}
            //else if (ItemTag == "C03BS")
            //{

            //}
            //else if (ItemTag == "A03CBD")
            //{

            //}
            //else if (ItemTag == "D01TPL" || ItemTag == "D02ATL" || ItemTag == "D03CWS" || ItemTag == "D04OPB")
            //{
            //    //    <TreeViewItem Header="01. PAYMENT PROPOSAL LIST" Tag = "D01TPL"/>
            //    //    <TreeViewItem Header="02. ALL TRANSACTION LIST" Tag="D02ATL"/>
            //    //    <TreeViewItem Header="03. CATEGORY WISE SUMMARY" Tag="D03CWS"/>
            //    //    <TreeViewItem Header="04. OVERALL PAYMENT BUDGET" Tag="D04OPB"/>
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //    //this.stkOptLocation.Visibility = Visibility.Visible;
            //}
            //else if (ItemTag == "E01HTL" || ItemTag == "E02HTS")
            //{
            //    //tvi5.Items.Add(new TreeViewItem { Header = "01. MAIN/SUB HEAD TRANS. LIST", Tag = "E01HTL" });
            //    //tvi5.Items.Add(new TreeViewItem { Header = "02. MAIN/SUB HEAD TRANS. SUMMARY", Tag = "E02HTS" });
            //    this.stkOptBranch.Visibility = Visibility.Visible;
            //    this.stkOptLocation.Visibility = Visibility.Visible;
            //    this.stkOptActCode.Visibility = Visibility.Visible;
            //    this.stkOptSirCode.Visibility = Visibility.Visible;
            //    this.stkOptVouType.Visibility = Visibility.Visible;
            //}
            //else if (ItemTag == "E03SMTL" || ItemTag == "E04SMTS")
            //{
            //    //tvi5.Items.Add(new TreeViewItem { Header = "03. SUB VS MAIN HEAD DETAILS", Tag = "E03SMTL" });
            //    //tvi5.Items.Add(new TreeViewItem { Header = "04. SUB VS MAIN HEAD SUMMARY", Tag = "E04SMTS" });
            //    this.stkOptSirCode.Visibility = Visibility.Visible;
            //    this.stkOptVouType.Visibility = Visibility.Visible;
            //}
            //else if (ItemTag == "E05ICLDT")
            //{
            //    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details With Narration", Tag = "NARRATION" });
            //    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Date Wise Summary", Tag = "DATESUM" });

            //    this.cmbActGroup.SelectedIndex = 0;
            //    var ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(8, 4) != "0000" && x.actcode.Substring(0, 4) == "2203"); // && (x.actcode.Substring(0, 4) == "1203" || x.actcode.Substring(0, 4) == "2203"));
            //    //foreach (var item in ActcodeList)
            //    //{
            //    //    item.actdesc = item.actdesc.Trim().Substring(0, item.actdesc.Trim().Length - 1) + " / PAID)";
            //    //}
            //    this.AtxtActCode.Items.Clear();
            //    this.AtxtActCode.AutoSuggestionList.Clear();
            //    foreach (var item1 in ActcodeList)
            //        this.AtxtActCode.AddSuggstionItem(item1.actcode.Substring(7) + " - " + item1.actdesc.Trim().Substring(0, item1.actdesc.Trim().Length - 1) + " / PAID)", item1.actcode);
            //    //this.AtxtActCode.AddSuggstionItem(item1.actcode.Substring(7) + " - " + item1.actdesc.Trim(), item1.actcode);


            //    this.AtxtActCode.IsEnabled = true;
            //    this.stkOptActCode.Visibility = Visibility.Visible;
            //    this.stkRptOptions.Visibility = Visibility.Visible;
            //}
        }

        private void autoUserSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetUserInfo(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.UserInterfaceAuth.AppUserList> GetUserInfo(string Pattern)
        {
            return new ObservableCollection<HmsEntityGeneral.UserInterfaceAuth.AppUserList>(
                    this.UserList1.Where((x, match) => (x.userrmrk).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));       
        }

        private void autoUserSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoUserSearch.ContextMenu.IsOpen = true;
        }
    }
}
