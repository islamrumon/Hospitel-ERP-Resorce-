using ASITHmsEntity;
using ASITFunLib;
using ASITHmsViewMan.Commercial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
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
using Microsoft.Reporting.WinForms;
using ASITHmsRpt4Commercial;
using ASITHmsViewMan.Accounting;

namespace ASITHmsWpf.Commercial.Hospital
{
    /// <summary>
    /// Interaction logic for frmEntryFrontDesk103.xaml
    /// </summary>
    public partial class frmEntryFrontDesk103 : UserControl
    {
        private string TitaleTag1, TitaleTag2;  // 
        private bool FrmInitialized = false;
        private int TabItemIndex1 = 0;
        private DataGrid dgRpt1;

        private vmReportFrontDesk1 vmr = new vmReportFrontDesk1();
        private vmEntryVoucher1 vm1ac = new vmEntryVoucher1();
        private List<HmsEntityCommercial.FDeskSalesSumm01> CollAmtList = new List<HmsEntityCommercial.FDeskSalesSumm01>();
        public frmEntryFrontDesk103()
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
                this.FrmInitialized = true;
                this.ActivateAuthObjects();
                this.Objects_On_Init();
            }
        }
        private void ActivateAuthObjects()
        {
            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk103_btnUpdateVoucher") == null)
            {
                this.btnUpdateVoucher.IsEnabled = false;
                this.btnUpdateVoucher.Visibility = Visibility.Collapsed;
            }
        }
        private void Objects_On_Init()
        {
            TreeViewItem tvi1 = new TreeViewItem() { Header = "A. TRANSECTION DETAILS", Tag = "A000000000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi2 = new TreeViewItem() { Header = "B. SUMMARY REPORTS", Tag = "B00000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };

            tvi1.Items.Add(new TreeViewItem { Header = "01. Sales invoice list", Tag = "A01A00TRANSLIST", Uid = "COMMINVLIST01" });
            tvi1.Items.Add(new TreeViewItem { Header = "02. Invoice wise sales details", Tag = "A02A00INVDETAILS", Uid = "COMMINVDETAILS01" });
            tvi1.Items.Add(new TreeViewItem { Header = "03. Group wise sales details", Tag = "A03A00GROUPDETAILS", Uid = "GROUPDETAILS01" });  // Existing - 1. Group Report Details
            tvi1.Items.Add(new TreeViewItem { Header = "04. Ref. wise dues details", Tag = "A04A00REFBYDUELIST", Uid = "DUESREFBYLIST01" });   // Existing - 3. Collection Due   // A00DUESDETAILS
            tvi1.Items.Add(new TreeViewItem { Header = "05. CC charge details ", Tag = "A05A00CCDETAILS", Uid = "CCDETAILSLIST01" });     // Existing - 7. CC Collection (Done)
            //tvi1.Items.Add(new TreeViewItem { Header = "06. Discount due collection", Tag = "A06A00DUEDISCLIST", Uid = "UNKNOWN" });  // Existing - 9. Discount (Due Coll) (Not Required - Done with DISCOUNTLIST01)
            tvi1.Items.Add(new TreeViewItem { Header = "06. Discount reference list", Tag = "A06A00REFDISCLIST", Uid = "DISCOUNTLIST01" });  // Existing - 10. Discount Reference
            tvi1.Items.Add(new TreeViewItem { Header = "07. Dues reference list", Tag = "A07A00DUEREFLIST", Uid = "DUESREFLIST01" });      // Existing - 11. Due Reference

            tvi2.Items.Add(new TreeViewItem { Header = "01. Group wise sales summary", Tag = "B09B00GROUPSUM", Uid = "GROUPSUMMARY01" });          // Existing - 2. Group Summary
            tvi2.Items.Add(new TreeViewItem { Header = "02. Collection due summary", Tag = "B07B00DUESUM", Uid = "DUESUMMARY01" });  // Existing - 4. Coll. Due Summary
            tvi2.Items.Add(new TreeViewItem { Header = "03. Invoice wise collection", Tag = "B07B00INVOICESUM", Uid = "COLLSUMMARY01" }); // Existing - 5. Todays Collection
            tvi2.Items.Add(new TreeViewItem { Header = "04. Date wise collection", Tag = "B07B00DATESUM", Uid = "DAYSALCOLSUMMARY01" });    // Existing - 6. Datewise Collection,   8. Collection Details

            tvi1.IsExpanded = true;
            tvi2.IsExpanded = true;

            this.tvRptTitle.Items.Add(tvi1);
            this.tvRptTitle.Items.Add(tvi2);

            TitaleTag2 = this.Tag.ToString();
            this.xctk_dtpFrom.Value = DateTime.Today; //Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.xctk_dtpTo.Value = DateTime.Today;

            this.cmbSBrnCod.Items.Clear();
            var zoneList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) == "00");
            var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");

            this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = "ALL BRANCHES", Tag = "0000" });
            foreach (var itemb in zoneList)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = itemb.brnnam, Tag = itemb.brncod });

            foreach (var itemb in brnList)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = itemb.brnnam, Tag = itemb.brncod });

            this.cmbSBrnCod.SelectedIndex = 0;
            this.stkpUpdateAccVoucher.Visibility = Visibility.Collapsed;

        }

        private void cmbSBrnCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
            //if (this.cmbSBrnCod.SelectedItem == null)
            //    return;

            //string brncod = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString().Trim();//.Substring(0, 4);
            //var sectList = new List<HmsEntityGeneral.CompSecCodeBook>();
            //if (brncod == "0000")
            //    sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
            //else
            //    sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(0, 4) == brncod && x.sectcod.Substring(9, 3) != "000");

            //sectList.Sort(delegate(HmsEntityGeneral.CompSecCodeBook x, HmsEntityGeneral.CompSecCodeBook y)
            //{
            //    return x.sectname.CompareTo(y.sectname);
            //});

            //this.cmbSectCod.Items.Clear();
            //this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = "ALL LOCATIONS", Tag = brncod + "00000000" });
            //foreach (var itemc in sectList)
            //{
            //    this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemc.sectname, Tag = itemc.sectcod });
            //}
            //this.cmbSectCod.SelectedIndex = 0;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void tvRptTitle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ////this.dgOverall01.ItemsSource = null;
            ////this.dgOverall01.Columns.Clear();
            string ItemTitle = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString().ToUpper();
            string ItemTag = ((TreeViewItem)((TreeView)sender).SelectedItem).Tag.ToString().Substring(3);
            this.ShowRequiredOptions(ItemTag);
            this.lbltle1.Content = ItemTitle;
            string Msg1 = (ItemTag == "B03CF" || ItemTag == "B08SS" ? "(This option is under construction)" : "");
            this.lbltle2.Content = Msg1;// ItemTag;
        }

        private void ShowRequiredOptions(string ItemTag)
        {
            this.cmbRptOptions.Items.Clear();
            this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Default", Tag = "NONE" });
            this.cmbRptOptions.SelectedIndex = 0;
            ////this.stkOptBranch.Visibility = Visibility.Hidden;
            ////this.stkpOptUser.Visibility = Visibility.Hidden;
            ////this.stkOptTerminal.Visibility = Visibility.Hidden;
            ////this.stkOptItemGroup.Visibility = Visibility.Hidden;
            ////this.stkRptOptRefBy.Visibility = Visibility.Hidden;
            ////this.stkRptOptions.Visibility = Visibility.Hidden;
            this.stkpUpdateAccVoucher.Visibility = Visibility.Collapsed;

            if (this.stkpDataGrid.Children.Count > 0)
                this.stkpDataGrid.Children.Clear();

            switch (ItemTag)
            {
                case "A00TRANSLIST":
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Due Invoices Only", Tag = "DUESUMMARY01" });
                    this.stkOptBranch.Visibility = Visibility.Visible;
                    this.stkRptOptions.Visibility = Visibility.Visible;
                    return;
                case "A00INVDETAILS":
                    return;
                case "A00GROUPDETAILS":
                    return;
                case "A00REFBYDUELIST":
                    return;
                case "A00CCDETAILS":
                    return;
                case "A00DUEDISCLIST":
                    return;
                case "A00REFDISCLIST":
                    return;
                case "A00DUEREFLIST":
                    return;
                case "B00GROUPSUM":
                    //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Main group wise summary", Tag = "MAINGROUP" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Group summary - 2", Tag = "SUBGROUP" });
                    this.stkOptBranch.Visibility = Visibility.Visible;
                    this.stkRptOptions.Visibility = Visibility.Visible;
                    return;
                case "B00DUESUM":
                    return;
                case "B00INVOICESUM":
                    return;
                case "B00DATESUM":
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Day wise summary for each branch", Tag = "BRANCHBYDAY" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Branch wise summary for each day", Tag = "DAYBYBRANCH" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Branch wise summary between dates", Tag = "BRANCHSUMM" });
                    this.stkOptBranch.Visibility = Visibility.Visible;
                    this.stkRptOptions.Visibility = Visibility.Visible;
                    return;
                default:
                    break;
            }
            /*
            tvi1.Items.Add(new TreeViewItem { Header = "01. Transection invoice list", Tag = "A01A00TRANSLIST" });             //  Existing - 8. Collection Details
            tvi1.Items.Add(new TreeViewItem { Header = "02. Invoice wise details", Tag = "A02A00INVDETAILS" });
            tvi1.Items.Add(new TreeViewItem { Header = "03. Group wise details", Tag = "A03A00GROUPDETAILS" });       // Existing - 1. Group Report Details
            tvi1.Items.Add(new TreeViewItem { Header = "04. Ref. wise dues details", Tag = "A04A00REFBYDUELIST" });   // Existing - 3. Collection Due
            tvi1.Items.Add(new TreeViewItem { Header = "05. CC charge details ", Tag = "A05A00CCDETAILS" });     // Existing - 7. CC Collection
            tvi1.Items.Add(new TreeViewItem { Header = "06. Discount due collection", Tag = "A06A00DUEDISCLIST" });  // Existing - 9. Discount (Due Coll)
            tvi1.Items.Add(new TreeViewItem { Header = "07. Discount reference list", Tag = "A07A00REFDISCLIST" });  // Existing - 10. Discount Reference
            tvi1.Items.Add(new TreeViewItem { Header = "08. Dues reference list", Tag = "A08A00DUEREFLIST" });      // Existing - 11. Due Reference

            tvi2.Items.Add(new TreeViewItem { Header = "01. Group sales summary", Tag = "B09B00GROUPSUM" });          // Existing - 2. Group Summary
            tvi2.Items.Add(new TreeViewItem { Header = "02. Collection due summary", Tag = "B07B00DUESUM" });  // Existing - 4. Coll. Due Summary
            tvi2.Items.Add(new TreeViewItem { Header = "03. Invoice wise collection", Tag = "B07B00INVOICESUM" }); // Existing - 5. Todays Collection
            tvi2.Items.Add(new TreeViewItem { Header = "04. Date wise collection", Tag = "B07B00DATESUM" });    // Existing - 6. Datewise Collection

             */

            if (ItemTag == "A01TVL")
            {
                //<TreeViewItem Header="01. VOUCHER LIST" Tag = "A01TVL"/>
                this.stkOptBranch.Visibility = Visibility.Visible;

            }
            else if (ItemTag == "A02TL")
            {
                //<TreeViewItem Header="02. TRANSACTION LIST" Tag = "A02TL"/>
                //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details With Narration", Tag = "NARRATION,TRNSDETAILS" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details Without Narration", Tag = "WITHOUTNARRATION,TRNSDETAILS" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary Without Narration", Tag = "WITHOUTNARRATION,TRNSUMMARY" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary With Narration", Tag = "NARRATION,TRNSUMMARY" });
                this.stkOptBranch.Visibility = Visibility.Visible;

                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "A03CBD")
            {
                //  <TreeViewItem Header="03. CASH BOOK DETAILS" Tag = "A03CBD"/>
                //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "With All Details Information", Tag = "NARRATION,TRNSDETAILS" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details Without Narration", Tag = "WITHOUTNARRATION,TRNSDETAILS" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Head Wise Details Transaction", Tag = "WITHOUTNARRATION,TRNSDETAILSAC" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary Without Narration", Tag = "WITHOUTNARRATION,TRNSUMMARY" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary With Narration", Tag = "NARRATION,TRNSUMMARY" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Date Wise Summary", Tag = "WITHOUTNARRATION,DATEWISESUM" });
                this.stkRptOptions.Visibility = Visibility.Visible;

            }
            else if (ItemTag == "A04RPCB" || ItemTag == "B07IVE" || ItemTag == "B01RPCB")
            {
                // <TreeViewItem Header="01. RECEIPTS &amp; PAYMENTS" Tag = "B01RPCB"/>
                // <TreeViewItem Header="07. INCOME VS EXPENSE" Tag="B07IVE"/>
                this.stkOptBranch.Visibility = Visibility.Visible;
                //this.stkOptTB.Visibility = Visibility.Visible;
                //this.stkOptMore.Visibility = Visibility.Visible;
                //this.cmbSubLevel.SelectedIndex = 0;
            }
            else if (ItemTag == "B04CL")
            {
                // <TreeViewItem Header="04. CONTROL LEDGER" Tag = "B04CL"/>
                // (3 Reports) Control Ledger, Control Ledger Voucher Summary, Control Ledger Transaction Head Summary
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary", Tag = "VOUSUMMARY,WITHOUTNARR" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Voucher Summary with Narration", Tag = "VOUSUMMARY,NARRATION" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary", Tag = "TRNSUMMARY,NOLOCATION" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Transaction Summary with Location", Tag = "TRNSUMMARY,LOCATIONWISE" });
                this.cmbRptOptions.SelectedIndex = 0;
                //this.stkOptActCode.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "B05SL")
            {
                // <TreeViewItem Header="05. SUBSIDIARY LEDGER" Tag="B05SL"/>
                //this.stkOptActCode.Visibility = Visibility.Visible;
                //this.stkOptSirCode.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "B06CS")
            {
                // <TreeViewItem Header="06. CONTROL SCHEDULE" Tag="B06CS"/>
                this.stkOptBranch.Visibility = Visibility.Visible;


            }
            else if (ItemTag == "B08SS")
            {
                // <TreeViewItem Header="06. CONTROL SCHEDULE" Tag="B06CS"/>

                // <TreeViewItem Header="08. SUBSIDIARY SCHEDULE" Tag = "B08SS"/>
                this.stkOptBranch.Visibility = Visibility.Visible;

            }
            else if (ItemTag == "C01TB")
            {
                //  <TreeViewItem Header="01. TRIAL BALANCE" Tag = "C01TB"/>
                //this.stkOptTB.Visibility = Visibility.Visible;
                //this.stkOptMore.Visibility = Visibility.Visible;
                //this.cmbSubLevel.SelectedIndex = 0;
            }
            else if (ItemTag == "C02IS")
            {
                this.stkOptBranch.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "C03BS")
            {

            }
            else if (ItemTag == "A03CBD")
            {

            }
            else if (ItemTag == "D01TPL" || ItemTag == "D02ATL" || ItemTag == "D03CWS" || ItemTag == "D04OPB")
            {
                //    <TreeViewItem Header="01. PAYMENT PROPOSAL LIST" Tag = "D01TPL"/>
                //    <TreeViewItem Header="02. ALL TRANSACTION LIST" Tag="D02ATL"/>
                //    <TreeViewItem Header="03. CATEGORY WISE SUMMARY" Tag="D03CWS"/>
                //    <TreeViewItem Header="04. OVERALL PAYMENT BUDGET" Tag="D04OPB"/>
                this.stkOptBranch.Visibility = Visibility.Visible;
                //this.stkOptLocation.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "E01HTL" || ItemTag == "E02HTS")
            {
                //tvi5.Items.Add(new TreeViewItem { Header = "01. MAIN/SUB HEAD TRANS. LIST", Tag = "E01HTL" });
                //tvi5.Items.Add(new TreeViewItem { Header = "02. MAIN/SUB HEAD TRANS. SUMMARY", Tag = "E02HTS" });
                this.stkOptBranch.Visibility = Visibility.Visible;
                //this.stkOptLocation.Visibility = Visibility.Visible;
                //this.stkOptActCode.Visibility = Visibility.Visible;
                //this.stkOptSirCode.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "E03SMTL" || ItemTag == "E04SMTS")
            {
                //tvi5.Items.Add(new TreeViewItem { Header = "03. SUB VS MAIN HEAD DETAILS", Tag = "E03SMTL" });
                //tvi5.Items.Add(new TreeViewItem { Header = "04. SUB VS MAIN HEAD SUMMARY", Tag = "E04SMTS" });
                // this.stkOptSirCode.Visibility = Visibility.Visible;
            }
            else if (ItemTag == "E05ICLDT")
            {
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details With Narration", Tag = "NARRATION" });
                this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Date Wise Summary", Tag = "DATESUM" });

                //this.cmbActGroup.SelectedIndex = 0;
                var ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(8, 4) != "0000" && x.actcode.Substring(0, 4) == "2203"); // && (x.actcode.Substring(0, 4) == "1203" || x.actcode.Substring(0, 4) == "2203"));
                //foreach (var item in ActcodeList)
                //{
                //    item.actdesc = item.actdesc.Trim().Substring(0, item.actdesc.Trim().Length - 1) + " / PAID)";
                //}
                //this.AtxtActCode.Items.Clear();
                //this.AtxtActCode.AutoSuggestionList.Clear();
                //foreach (var item1 in ActcodeList)
                //    this.AtxtActCode.AddSuggstionItem(item1.actcode.Substring(7) + " - " + item1.actdesc.Trim().Substring(0, item1.actdesc.Trim().Length - 1) + " / PAID)", item1.actcode);
                ////this.AtxtActCode.AddSuggstionItem(item1.actcode.Substring(7) + " - " + item1.actdesc.Trim(), item1.actcode);


                //this.AtxtActCode.IsEnabled = true;
                //this.stkOptActCode.Visibility = Visibility.Visible;
                this.stkRptOptions.Visibility = Visibility.Visible;
            }
        }
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (this.tvRptTitle.SelectedItem == null)
                return;

            string RptID1 = ((TreeViewItem)this.tvRptTitle.SelectedItem).Tag.ToString().Substring(3);
            string RptTitle1 = ((TreeViewItem)this.tvRptTitle.SelectedItem).Header.ToString().ToUpper();
            string RptProcID1 = ((TreeViewItem)this.tvRptTitle.SelectedItem).Uid.ToString().ToUpper();
            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();

            string BrnCode1 = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            BrnCode1 = (BrnCode1 == "0000" ? "%" : (BrnCode1.Substring(2, 2) == "00" ? BrnCode1.Substring(0, 2) : BrnCode1));
            string StartDate1 = this.xctk_dtpFrom.Text.Trim();
            string EndDate1 = this.xctk_dtpTo.Text.Trim();
            string InvNum1 = "CSI";
            string InvStatus1 = "A";
            string SignInID1 = "%";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string Options1 = ((ComboBoxItem)this.cmbRptOptions.SelectedItem).Tag.ToString();
            string OrderBy1 = "DEFAULT";
            if (RptProcID1 == "COLLSUMMARY01")
            {
                this.CollectionSummary(PrintId, RptID1, RptProcID1, BrnCode1, StartDate1, EndDate1, TerminalName1);
                return;
            }
            else if (RptProcID1 == "DAYSALCOLSUMMARY01")
            {
                this.DayWiseSalesCollectionSummary(PrintId, RptID1, RptProcID1, BrnCode1, StartDate1, EndDate1, Options1);
                return;
            }

            var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                           InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: Options1, OrderBy: OrderBy1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;


            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
            {
                this.PrintReport(RptID1, ds1, PrintId);
            }
            else if (PrintId == "SS")
            {
                this.ShowGridInfo(RptID1, ds1);
            }

            return;
        }

        #region Tobe Delete Region

        private void CollectionSummary(string PrintId, string RptID1, string RptProcID1, string BrnCode1, string StartDate1, string EndDate1, string Option1)
        {

            var pap1 = vmr.SetParamFrontDeskSumReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1, Option1: Option1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
            {
                this.PrintReport(RptID1, ds1, PrintId);
            }
            else if (PrintId == "SS")
            {
                this.ShowGridInfo(RptID1, ds1);
            }

        }

        private void DayWiseSalesCollectionSummary(string PrintId, string RptID1, string RptProcID1, string BrnCode1, string StartDate1, string EndDate1, string Option1 = "ALLBRANCH")
        {
            if (this.cmbRptOptions.SelectedIndex > 0 && BrnCode1.Length != 4)
                Option1 = ((ComboBoxItem)this.cmbRptOptions.SelectedItem).Tag.ToString();
            else
                Option1 = "ALLBRANCH";

            //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Day wise summary for each branch", Tag = "BRANCHBYDAY" });
            //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Branch wise summary for each day", Tag = "DAYBYBRANCH" });
            //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Branch wise summary between dates", Tag = "BRANCHSUMM" });

            var pap1 = vmr.SetParamFrontDeskSumReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1, Option1: Option1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
            {
                this.PrintReport(RptID1, ds1, PrintId);
            }
            else if (PrintId == "SS")
            {
                this.ShowGridInfo(RptID1, ds1);
            }

        }


        #endregion
        private void PrintReport(string RptID, DataSet ds1, string pout1)
        {
            try
            {
                if (ds1 == null)
                    return;

                if (ds1.Tables.Count < 2)
                    return;

                DateTime ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
                LocalReport rpt1 = null;
                string WindowTitle1 = "Front Desk Transaction Report";
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: ServerTime1);
                string fromDate = xctk_dtpFrom.Text.ToString();
                string ToDate = xctk_dtpTo.Text.ToString();
                list3[0].RptHeader1 = ds1.Tables[1].Rows[0]["RptTitle"].ToString();
                list3[0].RptHeader2 = ds1.Tables[1].Rows[0]["RptPeriod"].ToString();

                switch (RptID)
                {
                    case "A00TRANSLIST":
                    case "A00INVDETAILS": // "02. Invoice wise details"
                        var RptLista = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();
                        if (RptID == "A00INVDETAILS")
                        {
                            var RptLista1 = RptLista.FindAll(x => x.ptinvnum2.Trim().Length == 0);
                            int i = 1;
                            string Pinv1 = "XXXXXXXXXXXXXXXXXX";
                            foreach (var item in RptLista1)
                            {
                                if (item.ptinvnum != Pinv1)
                                    i = 1;
                                item.ptname = i.ToString("00") + ". " + item.ptname;
                                Pinv1 = item.ptinvnum;
                                i++;
                            }
                        }

                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptCommInvList1", RptLista, null, list3);
                        break;
                    case "A00GROUPDETAILS":
                    case "B00GROUPSUM": // "01. Group sales summary"       // Existing - 2. Group Summary
                        var RptListc = ds1.Tables[0].DataTableToList<HmsEntityCommercial.GroupWiseTrans01>();
                        int ii = 0;
                        foreach (var item in RptListc)
                        {
                            ii = (item.colstyle.Contains("NL") ? ii + 1 : 0);
                            item.slnum = ii;
                        }

                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptGroupWiseTrans1", RptListc, null, list3);
                        break;

                    case "A00CCDETAILS": // "05. CC charge details "    // Existing - 7. CC Collection
                        var RptCCListc2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskCollSumm01>();
                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptCCStatus01", RptCCListc2, null, list3);
                        break;
                    //case "A00DUEDISCLIST": // "06. Discount due collection"  // Existing - 9. Discount (Due Coll)
                    //    break;
                    case "A00REFBYDUELIST": // "04. Ref. wise dues details"  // Existing - 3. Collection Due
                    case "A00REFDISCLIST": // "07. Discount reference list"  // Existing - 10. Discount Reference
                    case "A00DUEREFLIST": // "08. Dues reference list"    // Existing - 11. Due Reference
                        var RptDiscountList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskDiscount01>();
                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptDiscountList01", RptDiscountList2, null, list3);
                        break;
                    case "B00DUESUM": // "02. Collection due summary"  // Existing - 4. Coll. Due Summary
                        var RptListd = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();
                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptCollDuesSum01", RptListd, null, list3);
                        //xxxxtvi2.Items.Add(new TreeViewItem { Header = "02. Collection due summary", Tag = "B07B00DUESUM", Uid = "DUESUMMARY01" }); // Under Construction  // Existing - 4. Coll. Due Summary
                        break;
                    case "B00INVOICESUM": // "03. Invoice wise collection" // Existing - 5. Todays Collection
                        var RptListc2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskCollSumm01>();
                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptCollectionSum01", RptListc2, null, list3);
                        break;
                    case "B00DATESUM": // "04. Date wise collection"    // Existing - 6. Datewise Collection
                        var RptListc3 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskSalesSumm01>();
                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptSalesSum01", RptListc3, null, list3);
                        break;
                    default:
                        break;
                }
                if (rpt1 == null)
                    return;

                // string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //      if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")


                //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FontDesk-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
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

        private void ShowGridInfo(string RptID, DataSet ds1)
        {
            try
            {
                string cmbRPTTag = ((ComboBoxItem)(this.cmbRptOptions.SelectedItem)).Tag.ToString();
                string cmbSBTag = ((ComboBoxItem)(this.cmbSBrnCod.SelectedItem)).Tag.ToString();

                if (this.TabUcGrid1.Items.Count > 9)
                    return;

                switch (RptID)
                {
                    case "A00TRANSLIST":  //"A01. Sales invoice list"
                    case "A00INVDETAILS": // "A02. Invoice wise details"
                        var RptLista = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();
                        this.CreateNewTabForReport(GridReportFrontDesk01.TransectionInvoiceList.GetDataGrid(RptLista.ToList()));
                        break;
                    case "A00GROUPDETAILS": //"A03. Group wise sales details"
                    case "B00GROUPSUM":     //"B01. Group sales summary"       // Existing - 2. Group Summary
                        var RptListb = ds1.Tables[0].DataTableToList<HmsEntityCommercial.GroupWiseTrans01>();
                        this.CreateNewTabForReport(GridReportFrontDesk01.GroupWiseTransactionList.GetDataGrid(RptListb.ToList()));
                        break;
                    case "A00CCDETAILS": // "A05. CC charge details "    // Existing - 7. CC Collection
                        var RptCCListc2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskCollSumm01>();
                        this.CreateNewTabForReport(GridReportFrontDesk01.CCChargeListc1.GetDataGrid(RptCCListc2.ToList()));
                        break;
                    case "A00DUEDISCLIST": // "A06. Discount due collection"  // Existing - 9. Discount (Due Coll)
                        break;
                    case "B00DUESUM": // "B02. Collection due summary"  // Existing - 4. Coll. Due Summary
                        var RptListd = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();
                        this.CreateNewTabForReport(GridReportFrontDesk01.CollDuesSum01.GetDataGrid(RptListd.ToList()));
                        break;
                    case "B00INVOICESUM": // "B03. Invoice wise collection" // Existing - 5. Todays Collection
                        var RptListc2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskCollSumm01>();
                        this.CreateNewTabForReport(GridReportFrontDesk01.InvWiseColl.GetDataGrid(RptListc2.ToList()));
                        break;
                    case "A00REFBYDUELIST": // "A04. Ref. wise dues details"  // Existing - 3. Collection Due
                    case "A00REFDISCLIST": // "A07. Discount reference list"  // Existing - 10. Discount Reference
                    case "A00DUEREFLIST": // "A08. Dues reference list"    // Existing - 11. Due Reference
                        var RptDiscountList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskDiscount01>();
                        this.CreateNewTabForReport(GridReportFrontDesk01.DiscountList1.GetDataGrid(RptDiscountList2.ToList()));
                        break;
                    case "B00DATESUM": // "04. Date wise collection"    // Existing - 6. Datewise Collection
                        //Tag = "BRANCHBYDAY" "NONE"
                        //Tag = "DAYBYBRANCH"
                        // Tag = "BRANCHSUMM"

                        this.CollAmtList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskSalesSumm01>();
                        //this.dgRpt1 = GridReportFrontDesk01.DateWiseCollection01.GetDataGrid(this.CollAmtList, cmbRPTTag, cmbSBTag);
                        this.CreateNewTabForReport(GridReportFrontDesk01.DateWiseCollection01.GetDataGrid(this.CollAmtList.ToList(), cmbRPTTag, cmbSBTag));
                        if (cmbRPTTag == "DAYBYBRANCH" && this.xctk_dtpFrom.Text.Trim() == this.xctk_dtpTo.Text.Trim() &&
                             this.btnUpdateVoucher.Visibility == Visibility.Visible && WpfProcessAccess.CompInfList[0].comcod == "6521")// For Digilab Only
                            this.stkpUpdateAccVoucher.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }
                //this.stkpDataGrid.Children.Add(this.dgRpt1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FontDesk-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
       
        private void chkAsonDate_Click(object sender, RoutedEventArgs e)
        {
            this.stkpDateFrom.Visibility = (this.chkAsonDate.IsChecked == true ? Visibility.Hidden : Visibility.Visible);
        }
        private void tvRptTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnGenerate_Click(null, null);
        }
        private void tvRptTitle_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            string yy = this.cmbOutputOption.Uid.ToString();
            if (yy != "NONE")
                this.btnGenerate_Click(null, null);
        }
        private void tvRptTitle_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            this.cmbOutputOption.ComboBox_ContextMenuOpening(null, null);
        }
        private void tvRptTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.btnGenerate_Click(null, null);
        }

        private void btnUpdateVoucher_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to update collection vouchers", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (msgresult != MessageBoxResult.Yes)
                return;

            this.UpdateCollectionAsAccountsVoucher();
        }

        private void UpdateCollectionAsAccountsVoucher()
        {
            //startDate: StartDate1, EndDate: EndDate1, Option1
            //DAYBYBRANCH

            try
            {
                string vno1 = "";
                string vtag1 = "";

                foreach (var vouinf in this.CollAmtList)
                {

                    string VouBrn1 = vouinf.grp2cod.Substring(0, 4);// "1101";
                    DateTime vouDate1 = DateTime.Parse(this.xctk_dtpFrom.Text);
                    string VouType1 = "RVC81";// ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString();
                    string cactcod1 = "19010003000" + (VouBrn1.Substring(0, 2) == "12" ? "2" : (VouBrn1.Substring(0, 2) == "13" ? "3" : "1"));// this.AtxtCactCode.Value.Trim();
                    string EditVounum1 = "";
                    /*
                     190100030001	REGULAR CASH  - HEAD OFFICE
                     190100030002	REGULAR CASH  - RAMPURA
                     190100030003	REGULAR CASH  - TANGAIL BRANCH                 
                     */
                    /*
                        110100102001	HO - COMMERCIAL SECTION
                        110200102001	PALLABI - COMMERCIAL SECTION
                        110300102001	KACHUKHET - COMMERCIAL SECTION
                        111200102001	RAHIMA M. - COMMERCIAL SECTION
                        120100102001	RAMPURA - COMMERCIAL SECTION
                        120200102001	ISLAM TOWER - COMMERCIAL SECTION
                        130100102001	TANGAIL - COMMERCIAL SECTION
                        140100102001	KONABARI - COMMERCIAL SECTION                     
                     */
                    var ListVouTable1u = new List<vmEntryVoucher1.VouTable>();
                    ListVouTable1u.Add(new vmEntryVoucher1.VouTable()
                    {
                        trnsl = 1,
                        DrCrOrder = "C",
                        cactcode = cactcod1,
                        sectcod = VouBrn1 + "00102001", //"110100102001",
                        actcode = "310100010001",
                        sircode = "000000000000",
                        reptsl = "001",
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
                        cramt = 0.00m,
                        trnam = vouinf.collam * -1.00m,
                        trnrmrk = ""
                    });


                    string cheqbookid1 = "XXXXXXXXXXXXXXXXXX";
                    string cheqno1 = "";
                    //string vounum1 =  ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString().Trim().Substring(0, 3) +
                    //             DateTime.Parse(this.xctk_dtpVouDat.Text).ToString("yyyyMM") +
                    //             ((ComboBoxItem)this.cmbVouBrn.SelectedItem).Tag.ToString().Trim().Substring(0, 4);

                    string vounum1 = VouType1.Substring(0, 3) + vouDate1.ToString("yyyyMM") + VouBrn1.Substring(0, 4);
                    string RecnDate1 = vouDate1.ToString("dd-MMM-yyyy");

                    var vouPrInfo1 = new vmEntryVoucher1.VouPrInfo()
                    {
                        vounum = vounum1,
                        voudat = DateTime.Parse(this.xctk_dtpFrom.Text),
                        vouref = "", //this.txtVouRef.Text.Trim(),
                        cheqbookid = cheqbookid1,
                        chqref = cheqno1, //((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim(),
                        advref = "", //this.txtAdvice.Text.Trim(),
                        vounar = ("COLLECTION FROM " + vouinf.grp2desc.Trim() + " FOR " + vouDate1.ToString("dd-MMM-yyyy dddd")).ToUpper(),//this.txtVouNar.Text.Trim(),
                        curcod = "CBCICOD01001",
                        curcnv = 1.00m,
                        vstatus = "A",
                        recndt = DateTime.Parse(RecnDate1), //DateTime.Parse("01-Jan-1900"),
                        vtcode = VouType1.Substring(3, 2),
                    };

                    if (vouinf.collam > 0)
                    {
                        DataSet ds1 = vm1ac.GetDataSetForUpdate(WpfProcessAccess.CompInfList[0].comcod, vouPrInfo1, ListVouTable1u, _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode,
                                    _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

                        var pap1 = vm1ac.SetParamUpdateVoucher(WpfProcessAccess.CompInfList[0].comcod, ds1, EditVounum1);
                        DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                        if (ds2 == null)
                            return;

                        vno1 = vno1 + (vno1.Length > 0 ? ", " : "") + ds2.Tables[0].Rows[0]["memonum1"].ToString();
                        vtag1 = vtag1 + (vtag1.Length > 0 ? "," : "") + ds2.Tables[0].Rows[0]["memonum"].ToString();
                    }
                }
                this.txtVoucherMsg.Text = vno1;
                this.txtVoucherMsg.Tag = vtag1;
                this.btnUpdateVoucher.IsEnabled = false;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Comm.Rpt.ACV-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, 
                    MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

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
