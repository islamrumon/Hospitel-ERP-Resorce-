
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


namespace ASITHmsWpf.Commercial.SuperShop
{
    /// <summary>
    /// Interaction logic for frmEntryShopPOS103.xaml
    /// </summary>
    public partial class frmEntryShopPOS103 : UserControl
    {
        private string TitaleTag1, TitaleTag2;  // 
        private bool FrmInitialized = false;
        private DataGrid dgRpt1;
        private vmReportFrontDesk1 vmr = new vmReportFrontDesk1();
        public frmEntryShopPOS103()
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
                this.Objects_On_Init();
            }
        }

        private void Objects_On_Init()
        {
            TreeViewItem tvi1 = new TreeViewItem() { Header = "A. TRANSECTION DETAILS", Tag = "A000000000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi2 = new TreeViewItem() { Header = "B. SUMMARY REPORTS", Tag = "B00000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };

            tvi1.Items.Add(new TreeViewItem { Header = "01. Transection invoice list", Tag = "A01A00TRANSLIST", Uid = "COMMINVLIST01" });             //  Existing - 8. Collection Details
            tvi1.Items.Add(new TreeViewItem { Header = "02. Invoice wise details", Tag = "A03A00GROUPDETAILS", Uid = "GROUPDETAILS01" });       // Existing - 1. Group Report Details

            tvi2.Items.Add(new TreeViewItem { Header = "01. Invoice wise summary", Tag = "B09B00GROUPSUM", Uid = "GROUPSUMMARY01" });          // Existing - 2. Group Summary
            tvi2.Items.Add(new TreeViewItem { Header = "02. Item wise summary", Tag = "B07B00DUESUM", Uid = "UNKNOWN" });  // Existing - 4. Coll. Due Summary
            tvi2.Items.Add(new TreeViewItem { Header = "03. Terminal wise summary", Tag = "B07B00INVOICESUM", Uid = "UNKNOWN" }); // Existing - 5. Todays Collection
            tvi2.Items.Add(new TreeViewItem { Header = "04. Date wise top summary", Tag = "B07B00DATESUM", Uid = "UNKNOWN" });    // Existing - 6. Datewise Collection


            tvi1.IsExpanded = true;
            tvi2.IsExpanded = true;

            this.tvRptTitle.Items.Add(tvi1);
            this.tvRptTitle.Items.Add(tvi2);

            TitaleTag2 = this.Tag.ToString();
            this.xctk_dtpFrom.Value = DateTime.Today; //Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.xctk_dtpTo.Value = DateTime.Today;


            this.cmbSBrnCod.Items.Clear();
            var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = "ALL BRANCHES", Tag = "0000" });
            foreach (var itemb in brnList)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = itemb.brnnam, Tag = itemb.brncod });

            this.cmbSBrnCod.SelectedIndex = 0;

        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void cmbSBrnCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            switch (ItemTag)
            {
                case "A00TRANSLIST":
                    return;
                case "A00INVDETAILS":
                    return;
                case "A00GROUPDETAILS":
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details Without Narration", Tag = "WITHOUTNARRATION,TRNSDETAILS" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary Without Narration", Tag = "WITHOUTNARRATION,TRNSUMMARY" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Summary With Narration", Tag = "NARRATION,TRNSUMMARY" });
                    this.stkOptBranch.Visibility = Visibility.Visible;
                    this.stkRptOptions.Visibility = Visibility.Visible;
                    return;
                case "A00DUESDETAILS":
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
                    return;
                case "B00DUESUM":
                    return;
                case "B00INVOICESUM":
                    return;
                case "B00DATESUM":
                    return;
                default:
                    break;
            }
            /*
            tvi1.Items.Add(new TreeViewItem { Header = "01. Transection invoice list", Tag = "A01A00TRANSLIST" });             //  Existing - 8. Collection Details
            tvi1.Items.Add(new TreeViewItem { Header = "02. Invoice wise details", Tag = "A02A00INVDETAILS" });
            tvi1.Items.Add(new TreeViewItem { Header = "03. Group wise details", Tag = "A03A00GROUPDETAILS" });       // Existing - 1. Group Report Details
            tvi1.Items.Add(new TreeViewItem { Header = "04. Ref. wise dues details", Tag = "A04A00DUESDETAILS" });   // Existing - 3. Collection Due
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
            BrnCode1 = (BrnCode1 == "0000" ? "%" : BrnCode1);
            string StartDate1 = this.xctk_dtpFrom.Text.Trim();
            string EndDate1 = this.xctk_dtpTo.Text.Trim();
            string InvNum1 = "CSI";
            string InvStatus1 = "A";
            string SignInID1 = "%";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string Options1 = ((ComboBoxItem)this.cmbRptOptions.SelectedItem).Tag.ToString();
            string OrderBy1 = "DEFAULT";
            var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                                                   InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: Options1, OrderBy: OrderBy1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF")
            {
                this.PrintReport(RptID1, ds1, PrintId);
            }
            else if (PrintId == "SS")
            {
                this.ShowGridInfo(RptID1, ds1);
            }

            return;

            //switch (RptID1)
            //{
            //    case "A00TRANSLIST": // "01. Transection invoice list"        //  Existing - 8. Collection Details
            //        ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();
            //        this.TransectionInvoiceList(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "A00INVDETAILS": // "02. Invoice wise details"
            //        this.InvoiceWiseDetailsList(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "A00GROUPDETAILS": // "03. Group wise details"        //  Existing - 1. Group Report Details
            //        this.GroupWiseDetailsList(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "A00DUESDETAILS": // "04. Ref. wise dues details"  // Existing - 3. Collection Due
            //        this.ReferenceWiseDueDetails(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "A00CCDETAILS": // "05. CC charge details "    // Existing - 7. CC Collection
            //        this.CollectionChargeDetails(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "A00DUEDISCLIST": // "06. Discount due collection"  // Existing - 9. Discount (Due Coll)
            //        this.DiscountDueCollectionList(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "A00REFDISCLIST": // "07. Discount reference list"  // Existing - 10. Discount Reference
            //        this.DiscountReferenceList(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "A00DUEREFLIST": // "08. Dues reference list"    // Existing - 11. Due Reference
            //        this.DiscountReferenceList(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;

            //    case "B00GROUPSUM": // "01. Group sales summary"       // Existing - 2. Group Summary
            //        this.GroupSalesSummary(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "B00DUESUM": // "02. Collection due summary"  // Existing - 4. Coll. Due Summary
            //        this.CollectionDueSummary(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "B00INVOICESUM": // "03. Invoice wise collection" // Existing - 5. Todays Collection
            //        this.InvoiceWiseCollection(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    case "B00DATESUM": // "04. Date wise collection"    // Existing - 6. Datewise Collection
            //        this.DateWiseCollection(RptID1, BrnCode1, StartDate1, EndDate1, SignInID1, TerminalName1, SessionID1);
            //        break;
            //    default:
            //        break;
            //}
        }

        #region Tobe Delete Region


        private void TransectionInvoiceList(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            // case "A00TRANSLIST": // "01. Transection invoice list"        //  Existing - 8. Collection Details


        }
        private void InvoiceWiseDetailsList(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //  case "A00INVDETAILS": // "02. Invoice wise details"

        }
        private void GroupWiseDetailsList(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //   case "A00GROUPDETAILS": // "03. Group wise details"        //  Existing - 1. Group Report Details


        }
        private void ReferenceWiseDueDetails(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //    case "A00DUESDETAILS": // "04. Ref. wise dues details"  // Existing - 3. Collection Due

        }
        private void CollectionChargeDetails(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //     case "A00CCDETAILS": // "05. CC charge details "    // Existing - 7. CC Collection

        }
        private void DiscountDueCollectionList(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //      case "A00DUEDISCLIST": // "06. Discount due collection"  // Existing - 9. Discount (Due Coll)

        }
        private void DiscountReferenceList(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //      case "A00REFDISCLIST": // "07. Discount reference list"  // Existing - 10. Discount Reference
        }
        private void DuesReferenceList(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //      case "A00DUEREFLIST": // "08. Dues reference list"    // Existing - 11. Due Reference
        }
        private void GroupSalesSummary(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //       case "B00GROUPSUM": // "01. Group sales summary"       // Existing - 2. Group Summary
        }
        private void CollectionDueSummary(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //      case "B00DUESUM": // "02. Collection due summary"  // Existing - 4. Coll. Due Summary
        }
        private void InvoiceWiseCollection(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //     case "B00INVOICESUM": // "03. Invoice wise collection" // Existing - 5. Todays Collection
        }
        private void DateWiseCollection(string RptID, string BrnCode, string StartDate, string EndDate, string SignInID, string TerminalName, string SessionID)
        {
            //     case "B00DATESUM": // "04. Date wise collection"    // Existing - 6. Datewise Collection
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
                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptCommInvList1", RptLista, null, list3);
                        break;
                    case "A00GROUPDETAILS":
                    case "B00GROUPSUM": // "01. Group sales summary"       // Existing - 2. Group Summary
                        var RptListc = ds1.Tables[0].DataTableToList<HmsEntityCommercial.GroupWiseTrans01>();
                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptGroupWiseTrans1", RptListc, null, list3);
                        break;
                    case "A00DUESDETAILS": // "04. Ref. wise dues details"  // Existing - 3. Collection Due
                        break;
                    case "A00CCDETAILS": // "05. CC charge details "    // Existing - 7. CC Collection
                        break;
                    case "A00DUEDISCLIST": // "06. Discount due collection"  // Existing - 9. Discount (Due Coll)
                        break;
                    case "A00REFDISCLIST": // "07. Discount reference list"  // Existing - 10. Discount Reference
                        break;
                    case "A00DUEREFLIST": // "08. Dues reference list"    // Existing - 11. Due Reference
                        break;
                    case "B00DUESUM": // "02. Collection due summary"  // Existing - 4. Coll. Due Summary
                        break;
                    case "B00INVOICESUM": // "03. Invoice wise collection" // Existing - 5. Todays Collection
                        break;
                    case "B00DATESUM": // "04. Date wise collection"    // Existing - 6. Datewise Collection
                        break;
                    default:
                        break;
                }
                if (rpt1 == null)
                    return;

                // string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FontDesk-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void ShowGridInfo(string RptID, DataSet ds1)
        {
            try
            {
                //string fromDate = xctk_dtpFrom.Text.Trim();
                //string ToDate = xctk_dtpTo.Text.ToString().Trim();
                //string TrHead = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString();
                //string From2Date = " From " + fromDate + " To " + ToDate;

                if (this.stkpDataGrid.Children.Count > 0)// && !(ItemTag == "B04CL" || ItemTag == "B05SL"))
                    this.stkpDataGrid.Children.Clear();

                switch (RptID)
                {
                    case "A00TRANSLIST":
                    case "A00INVDETAILS": // "02. Invoice wise details"
                        var RptLista = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();
                        this.dgRpt1 = GridReportFrontDesk01.TransectionInvoiceList.GetDataGrid(RptLista);
                        break;
                    case "A00GROUPDETAILS":
                    case "B00GROUPSUM": // "01. Group sales summary"       // Existing - 2. Group Summary
                        var RptListb = ds1.Tables[0].DataTableToList<HmsEntityCommercial.GroupWiseTrans01>();
                        int ii = 0;
                        foreach (var item in RptListb)
                        {
                            ii = (item.colstyle.Contains("NL") ? ii + 1 : 0);
                            item.slnum = ii;
                        }

                        this.dgRpt1 = GridReportFrontDesk01.GroupWiseTransactionList.GetDataGrid(RptListb);
                        break;
                    case "A00DUESDETAILS": // "04. Ref. wise dues details"  // Existing - 3. Collection Due
                        break;
                    case "A00CCDETAILS": // "05. CC charge details "    // Existing - 7. CC Collection
                        break;
                    case "A00DUEDISCLIST": // "06. Discount due collection"  // Existing - 9. Discount (Due Coll)
                        break;
                    case "A00REFDISCLIST": // "07. Discount reference list"  // Existing - 10. Discount Reference
                        break;
                    case "A00DUEREFLIST": // "08. Dues reference list"    // Existing - 11. Due Reference
                        break;
                    case "B00DUESUM": // "02. Collection due summary"  // Existing - 4. Coll. Due Summary
                        break;
                    case "B00INVOICESUM": // "03. Invoice wise collection" // Existing - 5. Todays Collection
                        break;
                    case "B00DATESUM": // "04. Date wise collection"    // Existing - 6. Datewise Collection
                        break;
                    default:
                        break;
                }
                this.stkpDataGrid.Children.Add(this.dgRpt1);
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
    }
}
