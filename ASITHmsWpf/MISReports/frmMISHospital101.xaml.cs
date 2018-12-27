using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ASITHmsEntity;
using ASITHmsViewMan.MISReports;
using ASITFunLib;
using System.Data;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt6MISReports;
using ASITHmsViewMan.Commercial;
using System.Collections.ObjectModel;
using System.Threading;
using ASITHmsRpt4Commercial;
using System.Text.RegularExpressions;

namespace ASITHmsWpf.MISReports
{
    /// <summary>
    /// Interaction logic for frmMISHospital101.xaml
    /// </summary>
    public partial class frmMISHospital101 : UserControl
    {
        private List<HmsEntityMISReports.MISHospital.RefByPerformance> RefByPerfromList1 = new List<HmsEntityMISReports.MISHospital.RefByPerformance>();

        private vmEntryFrontDesk1 vm1 = new vmEntryFrontDesk1();
        private vmMISHospital1 vmr = new vmMISHospital1();
        private vmReportFrontDesk1 vmr1 = new vmReportFrontDesk1();

        private bool FrmInitialized = false;

        public frmMISHospital101()
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

            if (!this.FrmInitialized)
            {
                this.ActivateAuthObjects();
                this.Objects_On_Load();
                this.FrmInitialized = true;
            }
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }


        private void ActivateAuthObjects()
        {
            try
            {
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmMISHospital1_frmMISHospital101_btnUpdateMark") == null)
                {
                    this.btnUpdateMark.Visibility = Visibility.Hidden;
                    this.rbtnRefByMark.Visibility = Visibility.Hidden;
                }
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmMISHospital1_frmMISHospital101_btnPrintMultiLedger") == null)
                {
                    this.stkpMultiLedger1.Visibility = Visibility.Hidden;
                    this.btnPrintMultiLedger.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Health-MIS-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void Objects_On_Load()
        {
            this.xctk_dtpFrom.Value = DateTime.Today; //Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.xctk_dtpTo.Value = DateTime.Today;
            this.xctk_dtpBill.Value = DateTime.Today;

            this.cmbSBrnCod.Items.Clear();
            var zoneList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) == "00");
            var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");

            this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = "ALL BRANCHES", Tag = "0000" });
            foreach (var itemb in zoneList)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = itemb.brnnam, Tag = itemb.brncod });

            foreach (var itemb in brnList)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = itemb.brnnam, Tag = itemb.brncod });

            this.cmbSBrnCod.SelectedIndex = 0;
            this.stkpRefByList.Visibility = Visibility.Collapsed;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbtnRefByMark.IsChecked == true)
                return;
            else
                this.BillSummaryReports();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BillSummaryReports()
        {
            string RptID1 = "REFBYSUM00";
            if (this.rbtnRefBySum1.IsChecked == true)
                RptID1 = "REFBYSUM01"; //    "REFBYSUM02"    "REFBYPERFORM"   "REFBYLEDGER"   ((TreeViewItem)this.tvRptTitle.SelectedItem).Tag.ToString().Substring(3);
            else if (this.rbtnRefBySum2.IsChecked == true)
                RptID1 = "REFBYSUM02";
            else if (this.rbtnRefByStatus.IsChecked == true)
                RptID1 = "REFBYSUM03";

            string RptTitle1 = "01. Referral summary-1";// ((TreeViewItem)this.tvRptTitle.SelectedItem).Header.ToString().ToUpper();

            //string RptProcID1 = ((TreeViewItem)this.tvRptTitle.SelectedItem).Uid.ToString().ToUpper();
            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();

            string BrnCode1 = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            BrnCode1 = (BrnCode1 == "0000" ? "%" : (BrnCode1.Substring(2, 2) == "00" ? BrnCode1.Substring(0, 2) : BrnCode1));

            string StartDate1 = this.xctk_dtpFrom.Text;
            string EndDate1 = this.xctk_dtpTo.Text;
            string ExPrevDues1 = (this.chkExcludePrevDues.IsChecked == true && this.rbtnRefByStatus.IsChecked == false ? "EXCLUDEPREVDUES" : "");
            //string ExSpecial1 = (this.chkExcludeSpecial.IsChecked == true && this.rbtnRefByStatus.IsChecked == false ? "EXCLUDESPECIAL" : "");
            string ExSpecial1 = (this.chkExcludeSpecial.IsChecked == true ? "EXCLUDESPECIAL" : "");
            string Inhouse1 = ((ComboBoxItem)(this.cmbRefByType.SelectedItem)).Tag.ToString().Trim();
            Inhouse1 = (Inhouse1 == "ALLTYPES" ? "" : "TYPE" + Inhouse1);
            string Options1 = ExPrevDues1 + " " + ExSpecial1 + " " + Inhouse1;
            string Limit1 = "0" + this.txtLimit.Text.Trim();
            var pap1 = vmr.SetParamRefByPerformance(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1, RptID1: RptID1, 
                Option1: Options1, Limit1: Limit1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            //this.RefByPerfromList1 = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByPerformance>();

            if (PrintId == "DP" || PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
            {
                this.PrintReport(RptID1, ds1, PrintId);
            }
            else if (PrintId == "SS")
            {
                //this.ShowGridInfo(RptID1, ds1);
            }
        }

        private void RefByLedgerReports(string RefByID1 = "%", string TokenId1 = "0", string FileName1 = "", bool OpenFile1 = true)
        {
            string RptID1 = "REFBYLEDGER01";

            string RptTitle1 = "01. Referral Ledger-1";// ((TreeViewItem)this.tvRptTitle.SelectedItem).Header.ToString().ToUpper();
            //string RptProcID1 = ((TreeViewItem)this.tvRptTitle.SelectedItem).Uid.ToString().ToUpper();
            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();

            string BrnCode1 = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            BrnCode1 = (BrnCode1 == "0000" ? "%" : BrnCode1.Substring(0, 2));

            string StartDate1 = this.xctk_dtpFrom.Text;
            string EndDate1 = this.xctk_dtpTo.Text;
            //string RefByID1 = "%";
            string ShowDues1 = (this.chkDuesOnly.IsChecked == true ? "SHOWDUESONLY" : "");
            string ExPrevDues1 = (this.chkExcludePrevDues.IsChecked == true ? "EXCLUDEPREVDUES" : "");
            string ExSpecial1 = (this.chkExcludeSpecial.IsChecked == true ? "EXCLUDESPECIAL" : "");
            string PatintWise1 = (this.chkPatientWiseLedger.IsChecked == true ? "PATIENTWISELEDGER" : "");
            /*
              <CheckBox x:Name="chkDuesOnly" Content="Show dues only" Tag="SHOWDUESONLY" VerticalAlignment="Center" Width="150" Margin="10,0,0,0" />
              <CheckBox x:Name="chkExcludePrevDues" Content="Exclude previous dues" Tag="EXCLUDEPREVDUES" VerticalAlignment="Center" Width="160" Margin="8,0,0,0" />
              <CheckBox x:Name="chkExcludeSpecial" Content="Exclude special items" Tag="EXCLUDESPECIAL" VerticalAlignment="Center" Width="150" Margin="8,0,0,0" />
              <CheckBox x:Name="chkQuantityOnly" Content="Quantity Only" Tag="QUANTITYONLY" VerticalAlignment="Center" Width="110" Margin="10,0,0,0" />
             <CheckBox x:Name="chkPatientWiseLedger" Content="Patient wise ledger" Tag="PATIENTWISELEDGER" VerticalAlignment="Center" Width="150" Margin="57,0,0,0" />
             */

            string Options1 = ShowDues1 + " " + ExPrevDues1 + " " + ExSpecial1 + " " + PatintWise1;
            Options1 = Options1.Trim();
            var pap1 = vmr.SetParamRefByLedger(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1, RefByID1: RefByID1, Option1: Options1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            //this.RefByPerfromList1 = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByLedger>();


            if (PrintId == "DP" || PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
            {
                this.PrintReport(RptID1, ds1, PrintId, TokenId1, FileName1, OpenFile1);
            }
            else if (PrintId == "SS")
            {
                //this.ShowGridInfo(RptID1, ds1);
            }
        }


        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            this.stkpRefByList.IsEnabled = false;
            this.stkpRefByList.Visibility = Visibility.Collapsed;
            this.dgRefByList.ItemsSource = null;
            this.dgRefByList.Items.Refresh();
            this.RefByPerfromList1.Clear();
            this.GetRefByPerformance();
            if (this.RefByPerfromList1.Count > 0)
            {
                foreach (var item in this.RefByPerfromList1)
                    item.rfFullName = item.refbyid.Substring(6) + " : " + item.rfFullName;
                this.dgRefByList.ItemsSource = this.RefByPerfromList1;
                this.dgRefByList.ScrollIntoView(this.dgRefByList.Items[0]);
                this.dgRefByList.SelectedIndex = 0;
                this.stkpRefByList.Visibility = Visibility.Visible;
                this.stkpRefByList.IsEnabled = true;
            }
        }

        private void GetRefByPerformance()
        {
            string RptID1 = "REFBYSUM00";
            if (this.rbtnRefBySum1.IsChecked == true)
                RptID1 = "REFBYSUM01"; //    "REFBYSUM02"    "REFBYPERFORM"   "REFBYLEDGER"   ((TreeViewItem)this.tvRptTitle.SelectedItem).Tag.ToString().Substring(3);
            else if (this.rbtnRefBySum2.IsChecked == true)
                RptID1 = "REFBYSUM02";
            else if (this.rbtnRefByStatus.IsChecked == true)
                RptID1 = "REFBYSUM03";

            string BrnCode1 = (RptID1 == "REFBYSUM00" ? "0000" : ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString());
            BrnCode1 = (BrnCode1 == "0000" ? "%" : (BrnCode1.Substring(2, 2) == "00" ? BrnCode1.Substring(0, 2) : BrnCode1));
            string BraName1 = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Content.ToString();
            string StartDate1 = this.xctk_dtpFrom.Text;
            string EndDate1 = this.xctk_dtpTo.Text;
            string ExPrevDues1 = (this.chkExcludePrevDues.IsChecked == true && this.rbtnRefByStatus.IsChecked == false ? "EXCLUDEPREVDUES" : "");
            //string ExSpecial1 = (this.chkExcludeSpecial.IsChecked == true && this.rbtnRefByStatus.IsChecked == false ? "EXCLUDESPECIAL" : "");
            string ExSpecial1 = (this.chkExcludeSpecial.IsChecked == true ? "EXCLUDESPECIAL" : "");
            string Inhouse1 = ((ComboBoxItem)(this.cmbRefByType.SelectedItem)).Tag.ToString().Trim();
            Inhouse1 = (Inhouse1 == "ALLTYPES" ? "" : "TYPE" + Inhouse1);
            string Options1 = ExPrevDues1 + " " + ExSpecial1 + " " + Inhouse1;
            Options1 = Options1.Trim();
            string Limit1 = "0" + this.txtLimit.Text.Trim();
            var pap1 = vmr.SetParamRefByPerformance(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1, RptID1: RptID1, 
                Option1: Options1, Limit1: Limit1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            //decimal Limit1 = 00.00m;
            //var Limit1f = decimal.TryParse("0" + this.txtLimit.Text.Replace("-", ""), out Limit1);

            for (int i = 4; i <= 12; i++)
                this.dgRefByList.Columns[i].Visibility = Visibility.Visible;

            switch (RptID1)
            {
                case "REFBYSUM00":
                    //this.dgRefByList.Columns[1].Visibility = Visibility.Visible;
                    this.lblGridTitle.Content = ds1.Tables[1].Rows[0]["RptTitle"].ToString();// "01. Referral Mark";
                    this.RefByPerfromList1 = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByPerformance>();
                    //txtLimit
                    //this.RefByPerfromList1 = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByPerformance>().OrderBy(x => x.refbyid).ToList();
                    //int sl1 = 1;
                    //foreach (var item in this.RefByPerfromList1)
                    //{
                    //    item.slnum = sl1;
                    //    sl1++;
                    //}
                    break;
                case "REFBYSUM01":
                    this.dgRefByList.Columns[8].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[9].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[12].Visibility = Visibility.Collapsed;
                    this.lblGridTitle.Content = ds1.Tables[1].Rows[0]["RptTitle"].ToString();// "02. Referral summary-1";
                    this.RefByPerfromList1 = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByPerformance>();//.Where(x => x.ncomam >= Limit1).ToList();
                    break;
                case "REFBYSUM02":
                    this.dgRefByList.Columns[4].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[6].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[7].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[8].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[9].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[11].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[12].Visibility = Visibility.Collapsed;

                    this.lblGridTitle.Content = ds1.Tables[1].Rows[0]["RptTitle"].ToString();// "03. Referral summary-2";
                    this.RefByPerfromList1 = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByPerformance>();//.Where(x => x.ncomam >= Limit1).ToList();
                    break;
                case "REFBYSUM03":
                    this.dgRefByList.Columns[8].Visibility = Visibility.Collapsed;
                    this.dgRefByList.Columns[9].Visibility = Visibility.Collapsed;
                    //this.dgRefByList.Columns[12].Visibility = Visibility.Hidden;
                    this.lblGridTitle.Content = ds1.Tables[1].Rows[0]["RptTitle"].ToString();// "04. Referral performance";
                    this.RefByPerfromList1 = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByPerformance>();
                    int Marked1 = this.cmbRefByStatus.SelectedIndex;
                    if (Marked1 == 1)
                        this.RefByPerfromList1 = this.RefByPerfromList1.FindAll(x => x.mark1 == true);
                    else if (Marked1 == 2)
                        this.RefByPerfromList1 = this.RefByPerfromList1.FindAll(x => x.mark1 == false);
                    break;
            }
            this.RefByPerfromList1 = this.SetSortOrder(this.RefByPerfromList1);

            //string OrderBy1 = ((ComboBoxItem)this.cmbSortOn.SelectedItem).Tag.ToString().ToUpper();
            //switch(OrderBy1)
            //{
            //    case "INVQTY":
            //        this.RefByPerfromList1 = this.RefByPerfromList1.OrderByDescending(x => x.invqty).ToList();
            //        break;
            //    case "ITEMQTY":
            //        this.RefByPerfromList1 = this.RefByPerfromList1.OrderByDescending(x => x.itemqty).ToList();
            //        break;
            //    case "NETAM":
            //        this.RefByPerfromList1 = this.RefByPerfromList1.OrderByDescending(x => x.netam).ToList();
            //        break;
            //    case "NCOMAM":
            //        this.RefByPerfromList1 = this.RefByPerfromList1.OrderByDescending(x => x.ncomam).ToList();
            //        break;
            //    case "COLAM":
            //        this.RefByPerfromList1 = this.RefByPerfromList1.OrderByDescending(x => x.colam).ToList();
            //        break;
            //    case "DUEAM":
            //        this.RefByPerfromList1 = this.RefByPerfromList1.OrderByDescending(x => x.dueam).ToList();
            //        break;

            //}


            //int idx = 1;
            //foreach (var item in this.RefByPerfromList1)
            //{
            //    item.slnum = idx;
            //    idx++;
            //}
            DataRow dr2 = ds1.Tables[2].Rows[0];
            this.txtTotalRec.Text = Convert.ToDecimal(dr2["rcount"]).ToString("#,##0;(#,##0); ");           //00
            this.txtTotalInvqty.Text = Convert.ToDecimal(dr2["tinvqty"]).ToString("#,##0;(#,##0); ");       //03
            this.txtTotalItemqty.Text = Convert.ToDecimal(dr2["titemqty"]).ToString("#,##0;(#,##0); ");     //04
            this.txtTotalSalam.Text = Convert.ToDecimal(dr2["tsalam"]).ToString("#,##0;(#,##0); ");         //05
            this.txtTotalDisam.Text = Convert.ToDecimal(dr2["tdisam"]).ToString("#,##0;(#,##0); ");         //06
            this.txtTotalNetam.Text = Convert.ToDecimal(dr2["tnetam"]).ToString("#,##0;(#,##0); ");         //07
            this.txtTotalTcomam.Text = Convert.ToDecimal(dr2["ttcomam"]).ToString("#,##0;(#,##0); ");       //08
            this.txtTotalCdisam.Text = Convert.ToDecimal(dr2["tcdisam"]).ToString("#,##0;(#,##0); ");       //09
            this.txtTotalNcomam.Text = Convert.ToDecimal(dr2["tncomam"]).ToString("#,##0;(#,##0); ");       //10
            this.txtTotalColam.Text = Convert.ToDecimal(dr2["tcolam"]).ToString("#,##0;(#,##0); ");         //11
            this.txtTotalDueam.Text = Convert.ToDecimal(dr2["tdueam"]).ToString("#,##0;(#,##0); ");         //12

            this.txtTotalInvqty.Visibility = this.dgRefByList.Columns[3].Visibility;
            this.txtTotalItemqty.Visibility = this.dgRefByList.Columns[4].Visibility;
            this.txtTotalSalam.Visibility = this.dgRefByList.Columns[5].Visibility;
            this.txtTotalDisam.Visibility = this.dgRefByList.Columns[6].Visibility;
            this.txtTotalNetam.Visibility = this.dgRefByList.Columns[7].Visibility;
            this.txtTotalTcomam.Visibility = this.dgRefByList.Columns[8].Visibility;
            this.txtTotalCdisam.Visibility = this.dgRefByList.Columns[9].Visibility;
            this.txtTotalNcomam.Visibility = this.dgRefByList.Columns[10].Visibility;
            this.txtTotalColam.Visibility = this.dgRefByList.Columns[11].Visibility;
            this.txtTotalDueam.Visibility = this.dgRefByList.Columns[12].Visibility;
        }

        private void btnUpdateMark_Click(object sender, RoutedEventArgs e)
        {
            if (this.RefByPerfromList1.Count == 0)
                return;

            DataSet ds1 = vm1.GetDataDetForRefByMarkUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MarkedList: this.RefByPerfromList1);
            string RefById1 = "%";
            var pap1 = vm1.SetParamUpdateRefByRefMark(WpfProcessAccess.CompInfList[0].comcod, ds1, RefById1);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;
            System.Windows.MessageBox.Show("Update Successfull", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

        }

        private void dgRefByList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.RefByPerfromList1.Count == 0)
                return;

            if (this.dgRefByList.SelectedItem == null)
                return;

            var item = (HmsEntityMISReports.MISHospital.RefByPerformance)this.dgRefByList.SelectedItem;

            if (this.rbtnRefByMark.IsChecked == true || this.rbtnRefByStatus.IsChecked == true)
            {
                this.CollectionSummary(item.refbyid, item.rfFullName);
            }
            else
            {

                if (item.mark1 == false)
                {
                    MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to generate ledger for " + item.rfFullName, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                    if (msgresult != MessageBoxResult.Yes)
                        return;
                }
                this.RefByLedgerReports(RefByID1: item.refbyid, TokenId1: item.slnum.ToString().Trim());
            }
        }
        private void btnMarkAll_Click(object sender, RoutedEventArgs e)
        {
            bool Mark1a = (((Button)sender).Content.ToString().Contains("Un") ? false : true);
            foreach (var item in this.RefByPerfromList1)
                item.mark1 = Mark1a;

            this.dgRefByList.Items.Refresh();
        }

        private void autoRefBySearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetRefByNameDesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityMISReports.MISHospital.RefByPerformance> GetRefByNameDesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityMISReports.MISHospital.RefByPerformance>(
                this.RefByPerfromList1.Where((x, match) => x.rfFullName.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void btnRefBySearch_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoRefBySearch.SelectedValue == null)
                return;

            string refById1a = this.autoRefBySearch.SelectedValue.ToString();
            int z = 0;
            foreach (var item3 in this.RefByPerfromList1)
            {
                if (item3.refbyid == refById1a)
                    break;
                z++;
            }
            this.autoRefBySearch.SelectedValue = null;
            this.dgRefByList.ScrollIntoView(this.dgRefByList.Items[z]);
            this.dgRefByList.SelectedIndex = z;

        }

        private void btnPrintMultiLedger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int cont1 = this.RefByPerfromList1.Count;
                if (cont1 == 0)
                    return;

                if (!this.lblGridTitle.Content.ToString().Contains("REFERRAL BILL SUMMARY - 02"))
                    return;

                string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();

                if (!(PrintId == "DP" || PrintId == "PDF" || PrintId == "WORD"))
                    return;

                int sl1a = 0, sl1b = 0, sl1c = 0, sl1d = 0;
                bool sl2a = int.TryParse("0" + this.txtBillNo1.Text.Trim(), out sl1a);
                bool sl2b = int.TryParse("0" + this.txtBillNo2.Text.Trim(), out sl1b);

                if (sl2a == false || sl2b == false)
                    return;

                sl1c = Math.Min(sl1a, sl1b);
                sl1c = Math.Max(1, sl1c);

                sl1d = Math.Max(sl1a, sl1b);
                sl1d = Math.Min(sl1d, cont1);

                sl1c = sl1c - 1;
                sl1d = sl1d - 1;

                int delay = int.Parse(((ComboBoxItem)this.cmbDelayTime.SelectedItem).Tag.ToString());

                string brnDesc = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Content.ToString().ToUpper();
                brnDesc = brnDesc.Substring(0, brnDesc.IndexOf(' ')).Trim() + "_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt"); ;
                for (int i = sl1c; i <= sl1d; i++)
                {
                    var item = this.RefByPerfromList1[i];
                    //string FileName1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + list1c[0].ptinvnum2 + "-" + list1c[0].ptname.Trim().Replace(".", "").Replace(" ", "_");
                    string FileName1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + brnDesc + "_" + item.slnum.ToString("0000").Trim() + "_" + item.refbyid.Substring(6);
                    bool OpenFile1 = false;
                    this.RefByLedgerReports(RefByID1: item.refbyid, TokenId1: item.slnum.ToString().Trim(), FileName1: FileName1, OpenFile1: OpenFile1);
                    Thread.Sleep(delay);// 30000 Thirty Second delay
                }

                //this.RefByPerfromList1
                //var item = (HmsEntityMISReports.MISHospital.RefByPerformance)this.dgRefByList.SelectedItem;
                //this.RefByLedgerReports(RefByID1: item.refbyid, TokenId1: item.slnum.ToString().Trim());
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MIS.Hospital-05: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop,
                    MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void CollectionSummary(string refbyID1, string rfFullName1)
        {
            //tvi2.Items.Add(new TreeViewItem { Header = "03. Invoice wise collection", Tag = "B07B00INVOICESUM", Uid = "COLLSUMMARY01" }); // Existing - 5. Todays Collection
            string RptID1 = "B00INVOICESUM";
            string RptProcID1 = "COLLSUMMARY01";
            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string BrnCode1 = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            BrnCode1 = (BrnCode1 == "0000" ? "%" : BrnCode1.Substring(0, 2));

            string StartDate1 = this.xctk_dtpFrom.Text;
            string EndDate1 = this.xctk_dtpTo.Text;
            string Option1 = "";
            //PrintId, RptID1, RptProcID1, BrnCode1, StartDate1, EndDate1, TerminalName1;

            var pap1 = vmr1.SetParamFrontDeskSumReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1, 
                Option1: Option1, refbyid1 : refbyID1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);

            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
            {
                this.PrintReport(RptID1, ds1, PrintId);
            }
            else if (PrintId == "SS")
            {
                //this.ShowGridInfo(RptID1, ds1);
            }
        }
        private void PrintReport(string RptID, DataSet ds1, string pout1, string TokenID1 = "0", string FileName1 = "", bool OpenFile1 = true)
        {
            try
            {
                if (ds1 == null)
                    return;

                if (ds1.Tables.Count < 2)
                    return;

                DateTime ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
                LocalReport rpt1 = null;
                string WindowTitle1 = "Referral Bill Report";
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: ServerTime1);
                string fromDate = xctk_dtpFrom.Text.ToString();
                string ToDate = xctk_dtpTo.Text.ToString();
                list3[0].RptHeader1 = ds1.Tables[1].Rows[0]["RptTitle"].ToString();
                list3[0].RptHeader2 = ds1.Tables[1].Rows[0]["RptPeriod"].ToString();

                switch (RptID)
                {
                    case "REFBYSUM01": // "02. Invoice wise details"
                    case "REFBYSUM02":
                        decimal Limit1 = 00.00m;
                        var Limit1f = decimal.TryParse("0" + this.txtLimit.Text.Replace("-", ""), out Limit1);

                        var RptLista = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByPerformance>().FindAll(x => x.ncomam >= Limit1);
                        RptLista = this.SetSortOrder(RptLista);
                        var RptName = (RptID == "REFBYSUM02" ? "Hospital.RptRefByBillSum02" : "Hospital.RptRefByBillSum01");
                        rpt1 = MISReportSetup.GetLocalReport(RptName, RptLista, null, list3);
                        break;
                    case "REFBYSUM03":
                        var RptListb = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByPerformance>();
                        string RefByType1 = ((ComboBoxItem)this.cmbRefByType.SelectedItem).Tag.ToString();
                        if (RefByType1 != "ALLTYPES")
                            list3[0].RptHeader1 = list3[0].RptHeader1 + " [" + RefByType1 + "]";

                        int Marked1 = this.cmbRefByStatus.SelectedIndex;

                        if (Marked1 == 1)
                            RptListb = RptListb.FindAll(x => x.mark1 == true);
                        else if (Marked1 == 2)
                            RptListb = RptListb.FindAll(x => x.mark1 == false);

                        RptListb = this.SetSortOrder(RptListb);
                        //int idx = 1;
                        //if (Marked1 > 0)
                        //{
                        //    list3[0].RptHeader1 = list3[0].RptHeader1 + (Marked1 == 1 ? "  (Marked)" : " (Un-Marked)");
                        //    foreach (var item in RptListb)
                        //    {
                        //        item.slnum = idx;
                        //        idx++;
                        //    }
                        //}
                        rpt1 = MISReportSetup.GetLocalReport("Hospital.RptRefByBillSum01", RptListb, null, list3);
                        break;
                    case "REFBYLEDGER01":
                        var RptNamel = "Hospital.RptRefByLedger01";

                        var RptListl = ds1.Tables[0].DataTableToList<HmsEntityMISReports.MISHospital.RefByLedger>();
                        list3[0].RptParVal1 = TokenID1;
                        list3[0].RptParVal2 = "In-word : " + ASITFunLib.ASITUtility.Trans(double.Parse(RptListl[0].tncomam.ToString()), 2);
                        if (this.chkPatientWiseLedger.IsChecked == true)
                        {
                            RptNamel = "Hospital.RptRefByLedger02";
                            list3[0].RptParVal2 = "In-word : " + ASITFunLib.ASITUtility.Trans(double.Parse(RptListl[0].tdueam.ToString()), 2);
                        }

                        //string refbyid2 = "XXXX";
                        //foreach (var item in RptListl)
                        //{
                        //    item.inwords = "In-word : " + ASITFunLib.ASITUtility.Trans(double.Parse(item.tncomam.ToString()), 2);
                        //    if (item.refbyid == refbyid2)
                        //        item.comcod = "";
                        //    else
                        //        refbyid2 = item.refbyid;
                        //}


                        rpt1 = MISReportSetup.GetLocalReport(RptNamel, RptListl, null, list3);
                        break;
                    case "B00INVOICESUM":
                        WindowTitle1 = "Front Desk Transaction Report";
                        var RptListc2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.FDeskCollSumm01>();
                        list3[0].RptHeader1 = list3[0].RptHeader1 + " - " + RptListc2[0].refbyid.Substring(6) + " : " + RptListc2[0].rfFullName.Trim();
                        rpt1 = CommReportSetup.GetLocalReport("Hospital.RptCollectionSum01", RptListc2, null, list3);
                        break;
                    default:
                        break;
                }
                if (rpt1 == null)
                    return;

                // string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //      if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")

                //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));

                if (pout1 == "DP")
                {
                    RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                    //DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                    DirectPrint1.PrintReport(rpt1);
                    DirectPrint1.Dispose();
                }
                else
                {
                    //string FileName1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + list1c[0].ptinvnum2 + "-" + list1c[0].ptname.Trim().Replace(".", "").Replace(" ", "_");
                    string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode, RenderFileName1: FileName1, OpenFile1: OpenFile1);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("MIS.Hospital-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private List<HmsEntityMISReports.MISHospital.RefByPerformance> SetSortOrder(List<HmsEntityMISReports.MISHospital.RefByPerformance> RefByPerfromList2)
        {

            string OrderBy1 = ((ComboBoxItem)this.cmbSortOn.SelectedItem).Tag.ToString().ToUpper();
            switch (OrderBy1)
            {
                case "INVQTY":
                    RefByPerfromList2 = RefByPerfromList2.OrderByDescending(x => x.invqty).ToList();
                    break;
                case "ITEMQTY":
                    RefByPerfromList2 = RefByPerfromList2.OrderByDescending(x => x.itemqty).ToList();
                    break;
                case "SALAM":
                    RefByPerfromList2 = RefByPerfromList2.OrderByDescending(x => x.salam).ToList();
                    break;
                case "NETAM":
                    RefByPerfromList2 = RefByPerfromList2.OrderByDescending(x => x.netam).ToList();
                    break;
                case "NCOMAM":
                    RefByPerfromList2 = RefByPerfromList2.OrderByDescending(x => x.ncomam).ToList();
                    break;
                case "COLAM":
                    RefByPerfromList2 = RefByPerfromList2.OrderByDescending(x => x.colam).ToList();
                    break;
                case "DUEAM":
                    RefByPerfromList2 = RefByPerfromList2.OrderByDescending(x => x.dueam).ToList();
                    break;
            }

            int idx = 1;
            foreach (var item in RefByPerfromList2)
            {
                item.slnum = idx;
                idx++;
            }
            return RefByPerfromList2;
        }

        private void rbtnRefBy_Click(object sender, RoutedEventArgs e)
        {
            string tag1 = ((RadioButton)sender).Tag.ToString();
            if (this.lblGridTitle.Tag.ToString() == tag1)
                return;
            this.lblGridTitle.Tag = tag1;
            this.btnShowList_Click(null, null);
        }

    }
}
