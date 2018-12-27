using ASITFunLib;
using ASITHmsEntity;
using ASITHmsRpt3Manpower;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
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
using ASITHmsViewMan.Manpower;
using System.Data;
using System.ComponentModel;
using System.Collections;
using ASITHmsViewMan.Accounting;


namespace ASITHmsWpf.Manpower
{
    /// <summary>
    /// Interaction logic for frmEntryPayroll103.xaml
    /// </summary>
    public partial class frmReportHCM1 : UserControl
    {
        private bool FrmInitialized = false;
        private DataGrid dgRpt1;
        private int TabItemIndex1 = 0;

        private List<HmsEntityManpower.HcmDayWiseAttanReport> AttanReportList = new List<HmsEntityManpower.HcmDayWiseAttanReport>();
        private List<HmsEntityManpower.RptAttnSchInfo> AttnSchInfoList = new List<HmsEntityManpower.RptAttnSchInfo>();
        private List<HmsEntityManpower.HcmMonthAttnEvalReport01> MonthlyAttnSum01 = new List<HmsEntityManpower.HcmMonthAttnEvalReport01>();
        private List<HmsEntityManpower.HcmLeaveDetailsReport01> AttnLeaveInfoList = new List<HmsEntityManpower.HcmLeaveDetailsReport01>();
        private List<HmsEntityManpower.Payslip001> MonthlySlrSht01 = new List<HmsEntityManpower.Payslip001>();

        private vmReportHCM1 vmr1 = new vmReportHCM1();
        private vmEntryPayroll1 vm1 = new vmEntryPayroll1();
        private vmEntryAttnLeav1 vm2 = new vmEntryAttnLeav1();
        private vmEntryHRGenral1 vm1gen = new vmEntryHRGenral1();
        private vmEntryVoucher1 vm1ac = new vmEntryVoucher1();

        string TitaleTag1, TitaleTag2;  // 
        public frmReportHCM1()
        {
            InitializeComponent();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

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
                this.Objects_On_Init();
                this.FrmInitialized = true;
            }
        }
        private void ActivateAuthObjects()
        {
            try
            {
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmReportHCM1_chkGeneralRpt") == null)
                {
                    this.chkGeneralRpt.IsChecked = false;
                }
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmReportHCM1_chkAttnRpt") == null)
                {
                    this.chkAttnRpt.IsChecked = false;
                }
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmReportHCM1_chkPayrollRpt") == null)
                {
                    this.chkPayrollRpt.IsChecked = false;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmReportHCM1_btnUpdateVoucher") == null)
                {
                    this.btnUpdateVoucher.IsEnabled = false;
                    this.btnUpdateVoucher.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCH-Report-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void Objects_On_Init()
        {
            this.TitaleTag2 = this.Tag.ToString();
            #region ReportTitles

            TreeViewItem tvi1 = new TreeViewItem() { Header = "A. GENERAL INFORMATION", Tag = "A0000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi2 = new TreeViewItem() { Header = "B. ATTENDANCE & LEAVE INFO", Tag = "B0000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
            TreeViewItem tvi3 = new TreeViewItem() { Header = "C. PAYROLL INFORMATION", Tag = "C0000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };

            tvi1.Items.Add(new TreeViewItem { Header = "01. Individual general info", Tag = "A01A00GENINF01" });
            tvi1.Items.Add(new TreeViewItem { Header = "02. Active employees list", Tag = "A02A00ACTLIST01" });
            tvi1.Items.Add(new TreeViewItem { Header = "03. Notice period employees list", Tag = "A03A00NOTLIST01" });
            tvi1.Items.Add(new TreeViewItem { Header = "04. Seperated employees list", Tag = "A04A00SEPLIST01" });

            tvi2.Items.Add(new TreeViewItem { Header = "01. Employee daily attendance", Tag = "B01B00INDATNRPT01" });
            tvi2.Items.Add(new TreeViewItem { Header = "02. Employee duty roster", Tag = "B02B00INDATNRPT02" });
            tvi2.Items.Add(new TreeViewItem { Header = "03. Employee monthly attendance", Tag = "B02B00INDATNRPT02A" });

            tvi2.Items.Add(new TreeViewItem { Header = "04. Details leave report", Tag = "B03B00LEVRPT01" });
            tvi2.Items.Add(new TreeViewItem { Header = "05. Leave summary report", Tag = "B04B00LEVRPT02" });
            tvi2.Items.Add(new TreeViewItem { Header = "06. Leave application form", Tag = "B05B00LEVAPP01" });
            tvi2.Items.Add(new TreeViewItem { Header = "07. Monthly attendance summary", Tag = "B06B00MATTNSUM01" });

            tvi3.Items.Add(new TreeViewItem { Header = "01. Salary & overtime", Tag = "C01SALARY_OT01" });
            tvi3.Items.Add(new TreeViewItem { Header = "02. Fastival bonus", Tag = "C01FAST_BONUS01" });

            //Tag="General,AttnLeave,Payroll"

            tvi1.IsExpanded = true;
            tvi2.IsExpanded = true;
            tvi3.IsExpanded = true;

            if (this.chkGeneralRpt.IsChecked == true)// (this.TitaleTag2.Trim().Contains("General"))
                this.tvRptRtTitle.Items.Add(tvi1);

            if (this.chkAttnRpt.IsChecked == true)// (this.TitaleTag2.Trim().Contains("Attnendance"))
                this.tvRptRtTitle.Items.Add(tvi2);

            if (this.chkPayrollRpt.IsChecked == true)// (this.TitaleTag2.Trim().Contains("Payroll"))
                this.tvRptRtTitle.Items.Add(tvi3);
            #endregion

            for (int i = -12; i < 12; i++)
            {
                this.cmbInfoMonth.Items.Add(new ComboBoxItem() { Content = DateTime.Today.AddMonths(i).ToString("MMMM, yyyy"), Tag = DateTime.Today.AddMonths(i).ToString("yyyyMM") });
            }
            this.cmbInfoMonth.SelectedIndex = 12;

            this.xctk_dtpFrom.Value = DateTime.Today; //Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy")).AddMonths(-1);
            this.xctk_dtpTo.Value = DateTime.Today;
            this.xctk_dtpJoin.Value = DateTime.Today.AddDays(-300);
            this.cmbSBrnCod.Items.Clear();
            var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = "ALL BRANCHES", Tag = "0000" });
            foreach (var itemb in brnList)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = itemb.brnnam, Tag = itemb.brncod });

            this.cmbSBrnCod.SelectedIndex = 0;

            var pap = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, "%", "EXISTSTAFFS");
            DataSet ds = WpfProcessAccess.GetHmsDataSet(pap);
            var tmpStaffList = ds.Tables[0].DataTableToList<vmReportHCM1.Stafflist>();



            //foreach (var item1 in WpfProcessAccess.StaffList)
            this.AtxtEmpAll.AutoSuggestionList.Clear();
            foreach (var item1 in tmpStaffList)
            {

                //this.AtxtEmpAll.AddSuggstionItem(item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
                this.AtxtEmpAll.AddSuggstionItem(item1.hccode.Trim().Substring(6) + " - " + item1.hcname.Trim() + ", " + item1.designame.Trim(), item1.hccode.Trim());
                //var mitm1 = new MenuItem() { Header = item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), Tag = item1.sircode.Trim() };
                var mitm1 = new MenuItem() { Header = item1.hccode.Trim().Substring(6) + " - " + item1.hcname.Trim() + ", " + item1.designame.Trim(), Tag = item1.hccode.Trim() };
                mitm1.Click += conMenuEmpAll_MouseClick;
                this.conMenuEmpAll.Items.Add(mitm1);
            }

            if (WpfProcessAccess.AccCodeList == null)
                WpfProcessAccess.GetAccCodeList();

            var BankList = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902")
                    && x.acttdesc.Trim().Length > 0 && (x.actcode.Substring(8, 4) != "0000")).OrderBy(x => x.actcode);
            this.cmbBankCod.Items.Clear();
            foreach (var item in BankList)
            {
                this.cmbBankCod.Items.Add(new ComboBoxItem() { Content = item.actdesc.Trim(), Tag = item.actcode + "," + item.acttdesc.Trim() });
            }
            this.stkpUpdateAccVoucher.Visibility = Visibility.Collapsed;
        }
        private void conMenuEmpAll_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtEmpAll.Text = ((MenuItem)sender).Header.ToString().Trim();
        }
        private void cmbSBrnCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbSBrnCod.SelectedItem == null)
                return;

            string brncod = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString().Trim();//.Substring(0, 4);
            var sectList = new List<HmsEntityGeneral.CompSecCodeBook>();
            if (brncod == "0000")
                sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
            else
                sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(0, 4) == brncod && x.sectcod.Substring(9, 3) != "000");

            sectList.Sort(delegate(HmsEntityGeneral.CompSecCodeBook x, HmsEntityGeneral.CompSecCodeBook y)
            {
                return x.sectname.CompareTo(y.sectname);
            });

            this.cmbSectCod.Items.Clear();
            this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = "ALL LOCATIONS", Tag = brncod + "00000000" });
            foreach (var itemc in sectList)
            {
                this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemc.sectname, Tag = itemc.sectcod });
            }
            this.cmbSectCod.SelectedIndex = 0;
        }

        private void tvRptRtTitle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            this.dgOverall01.ItemsSource = null;
            this.dgOverall01.Columns.Clear();
            string ItemTitle = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString().Trim().ToUpper();
            string ItemTag = ((TreeViewItem)((TreeView)sender).SelectedItem).Tag.ToString().Trim();
            this.ShowRequiredOptions(ItemTag);
            this.lbltle1.Content = ItemTitle;
            string Msg1 = "";
            this.lbltle2.Content = Msg1;// ItemTag;
        }

        private void ShowRequiredOptions(string ItemTag)
        {
            this.stkRptOptions.Visibility = Visibility.Hidden;
            this.stkEmpId.Visibility = Visibility.Hidden;
            this.stkpMonthInfo.Visibility = Visibility.Hidden;
            this.stkpJoinDate.Visibility = Visibility.Hidden;
            this.stkpDateFrom.Visibility = Visibility.Hidden;
            this.stkpDateTo.Visibility = Visibility.Hidden;
            this.stkpBank.Visibility = Visibility.Hidden;
            this.stkpUpdateAccVoucher.Visibility = Visibility.Collapsed;
            this.cmbRptOptions.Items.Clear();
            //this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "None", Tag = "NONE" });
            //this.cmbRptOptions.SelectedIndex = 0;

            switch (ItemTag)
            {

                case "A01A00GENINF01":      //tvi1.Items.Add(new TreeViewItem { Header = "01. INDIVIDUAL GENERAL INFO",   Tag = "A01A00GENINF01" });
                    break;
                case "A02A00ACTLIST01":     //tvi1.Items.Add(new TreeViewItem { Header = "02. ACTIVE EMPLOYEES LIST", Tag = "A02A00ACTLIST01" });
                    break;
                case "A03A00NOTLIST01":     //tvi1.Items.Add(new TreeViewItem { Header = "03. NOTICE PERIOD EMPLOYEES LIST", Tag = "A03A00NOTLIST01" });
                    break;
                case "A04A00SEPLIST01":     //tvi1.Items.Add(new TreeViewItem { Header = "04. SEPERATED EMPLOYEES LIST", Tag = "A04A00SEPLIST01" });
                    break;
                case "B01B00INDATNRPT01":   //tvi2.Items.Add(new TreeViewItem { Header = "01. EMPLOYEE DAILY ATTENDANCE",     Tag = "B01B00INDATNRPT01" });
                    this.stkpDateFrom.Visibility = Visibility.Visible;
                    this.stkpDateTo.Visibility = Visibility.Visible;
                    this.stkEmpId.Visibility = Visibility.Visible;
                    break;
                case "B02B00INDATNRPT02":   //tvi2.Items.Add(new TreeViewItem { Header = "02. EMPLOYEE DUTY ROSTER",   Tag = "B02B00INDATNRPT02" });
                case "B02B00INDATNRPT02A":   //tvi2.Items.Add(new TreeViewItem { Header = "03. EMPLOYEE MONTHLY ATTENDANCE",   Tag = "B02B00INDATNRPT02A" });
                    this.stkEmpId.Visibility = Visibility.Visible;
                    this.stkpMonthInfo.Visibility = Visibility.Visible;
                    break;
                case "B03B00LEVRPT01":      //tvi2.Items.Add(new TreeViewItem { Header = "04. DETAILS LEAVE REPORT",       Tag = "B03B00LEVRPT01" });
                case "B04B00LEVRPT02":      //tvi2.Items.Add(new TreeViewItem { Header = "05. LEAVE SUMMARY REPORT",       Tag = "B04B00LEVRPT02" });
                    this.stkpMonthInfo.Visibility = Visibility.Visible;
                    this.stkEmpId.Visibility = Visibility.Visible;
                    break;
                case "B05B00LEVAPP01":      //tvi2.Items.Add(new TreeViewItem { Header = "06. LEAVE APPLICATION FORM",        Tag = "B05B00LEVAPP01" });
                    break;
                case "B06B00MATTNSUM01":    //tvi2.Items.Add(new TreeViewItem { Header = "07. MONTHLY ATTENDANCE SUMMARY",    Tag = "B06B00MATTNSUM01" });
                    this.stkpMonthInfo.Visibility = Visibility.Visible;
                    break;
                case "C01SALARY_OT01":      //tvi3.Items.Add(new TreeViewItem { Header = "01. SALARY & OVERTIME", Tag = "C01SALARY_OT01" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details Salary Sheet", Tag = "C00MSALARY01" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Abstract of Salary Sheet", Tag = "C00SALSUM01" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details Overtime Sheet", Tag = "C00OTDETAILS01" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Abstract of Overtime Sheet", Tag = "C00OTSUMM01" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Pay Slip (All Employee)", Tag = "C00PAYSLIP01A" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Pay Slip (Bank Pay)", Tag = "C00PAYSLIP01B" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Pay Slip (No-Bank Pay)", Tag = "C00PAYSLIP01C" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Bank Letter (All Employee)", Tag = "C00BLUPTO00" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Bank Letter (1 - 10,000)", Tag = "C00BLUPTO10" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Bank Letter (10,001 - 15,000)", Tag = "C00BLUPTO15" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Bank Letter (15,001 - 20,000)", Tag = "C00BLUPTO20A" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Bank Letter (10,001 - 20,000)", Tag = "C00BLUPTO20B" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Bank Letter (20,001 - 30,000)", Tag = "C00BLUPTO30" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Bank Letter (More Than 30,000)", Tag = "C00BLMORE30" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Non-Bank Statement", Tag = "C00BLNONE00" });
                    // Digilab Salary Account : Bank Account No: 0017-0210019685 and 0017-0181000590
                    this.stkpBank.Visibility = Visibility.Visible;
                    this.stkpMonthInfo.Visibility = Visibility.Visible;
                    break;
                case "C01FAST_BONUS01":     //tvi3.Items.Add(new TreeViewItem { Header = "02. FASTIVAL BONUS",    Tag = "C01FAST_BONUS01" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Details Bonus Sheet", Tag = "C00FBONUS01" });
                    this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Abstract of Bonus Sheet", Tag = "C00FBSUM01" });
                    this.stkpMonthInfo.Visibility = Visibility.Visible;
                    this.stkpJoinDate.Visibility = Visibility.Visible;
                    break;
            }
            if (this.cmbRptOptions.Items.Count > 0)
            {
                this.stkRptOptions.Visibility = Visibility.Visible;
                this.cmbRptOptions.SelectedIndex = 0;
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if ((TreeViewItem)tvRptRtTitle.SelectedItem == null)
                return;

            string BrnCod = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString().Trim();
            BrnCod = (BrnCod == "0000" ? "%" : BrnCod);
            string BrnName = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Content.ToString().Trim();
            string SectCod = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();
            SectCod = (SectCod == "000000000000" ? "%" : (SectCod.Substring(4, 8) == "00000000" ? SectCod.Substring(0, 4) + "%" : SectCod));
            string SectName = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Content.ToString().Trim();
            string BankAcNo1 = ((ComboBoxItem)this.cmbBankCod.SelectedItem).Tag.ToString().Trim().Substring(13);
            string fromDate = this.xctk_dtpFrom.Text.Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();
            string TrHead = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Header.ToString().ToUpper();
            string TrTyp = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Tag.ToString();//.Substring(3);

            string hccode1 = this.AtxtEmpAll.Value.Trim();

            this.lbltle1.Content = TrHead.Remove(0, 3);
            this.lbltle2.Content = " From " + fromDate + " To " + ToDate;
            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            switch (TrTyp)
            {
                case "C01SALARY_OT01":
                case "C01FAST_BONUS01":
                    this.PrepareMonthlySalary(BrnCod, BrnName, SectCod, SectName, BankAcNo1, TrTyp, PrintId);
                    break;
                case "B01B00INDATNRPT01":
                    this.PrepareIndividualAttndance01(hccode1 + "%", BrnCod, SectCod, TrTyp, PrintId);
                    break;
                case "B02B00INDATNRPT02":
                case "B02B00INDATNRPT02A":
                    this.PrepareIndividualAttndance02(hccode1, TrTyp, PrintId);
                    break;
                case "B03B00LEVRPT01":
                    this.PrepareLeaveDetailsReport01(hccode1, SectCod, TrTyp, PrintId);
                    break;
                case "B04B00LEVRPT02":
                    this.PrepareLeaveSummaryReport01(hccode1, SectCod, TrTyp, PrintId);
                    break;
                case "B06B00MATTNSUM01":
                    this.PrepareMonthlyAttndanceSum01(BrnCod, SectCod, TrTyp, PrintId);
                    break;
            }
        }

        private void PrepareLeaveDetailsReport01(string hccode1, string SectCod, string TrTyp, string PrintId)
        {

            string monthName1 = ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim();
            DateTime Date1 = DateTime.Parse("01" + monthName1);
            string yearid1 = Date1.ToString("yyyy");
            var pap = vmr1.SetParamShowLeaveDetails(WpfProcessAccess.CompInfList[0].comcpcod, hccode1, yearid1, SectCod);

            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
            if (ds1 == null)
                return;

            var list1 = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmLeaveDetailsReport01>();
            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF")
            {
                Hashtable rptParam = new Hashtable();
                rptParam["Title1"] = "LEAVE INFORMATION - DETAILS";
                rptParam["Title2"] = "(For the year " + yearid1 + ")";

                //rptParam["ParmBrnDept1"] = "Department : " + Staff1[0].deptname.Trim() + ", Joining Date : " + Staff1[0].joindat.Trim() +
                //                            ", Reporting Date : " + Convert.ToDateTime(ds1r.Tables[2].Rows[0]["ServerTime"]).ToString("dd-MMM-yyyy hh:mm tt");

                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]));
                LocalReport rpt1 = HcmReportSetup.GetLocalReport("Payroll.RptLeaveDetails01", list1, rptParam, list3);
                if (rpt1 == null)
                    return;

                this.ShowReportWindow(rpt1, "Details Leave Report", false);
            }
            else if (PrintId == "SS")
            {
                this.AttnLeaveInfoList = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmLeaveDetailsReport01>();
                this.ShowGridInfo(TrTyp);
            }
        }

        private void PrepareLeaveSummaryReport01(string hccode1, string SectCod, string TrTyp, string PrintId)
        {

        }

        private void PrepareMonthlySalary(string BrnCod, string BrnName, string SectCod, string SectName, string BankAcNo1, string TrTyp, string PrintId)
        {
            string monthID1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim()).ToString("yyyyMM");

            string reportName1 = ((ComboBoxItem)this.cmbRptOptions.SelectedItem).Tag.ToString().Trim();
            string MinJoinDate = this.xctk_dtpJoin.Text.Trim();// "31-Oct-2016";
            // New code will goes here -- Hafiz 02-Jul-2017 
            var pap1 = vm1.SetParamMonthlySalaeySheet(WpfProcessAccess.CompInfList[0].comcpcod, reportName1, monthID1, BrnCod, SectCod, MinJoinDate);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            var list1 = ds1.Tables[0].DataTableToList<HmsEntityManpower.Payslip001>();
            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF")
            {
                if (reportName1 == "C00PAYSLIP01A" || reportName1 == "C00PAYSLIP01B" || reportName1 == "C00PAYSLIP01C")
                {
                    foreach (var item in list1)
                        item.netpayword = ASITUtility.Trans(double.Parse(item.netpay.ToString()), 2);

                    switch (reportName1)
                    {
                        case "C00PAYSLIP01B":
                            list1 = list1.FindAll(x => x.bankacno.Trim().Length > 0 && x.netpay > 0);
                            break;
                        case "C00PAYSLIP01C":
                            list1 = list1.FindAll(x => x.bankacno.Trim().Length == 0 && x.netpay > 0);
                            break;
                    }
                }

                var list2 = new Hashtable();
                list2["RptHead"] = ds1.Tables[1].Rows[0]["RptHead"].ToString();
                list2["RptPeriod"] = ds1.Tables[1].Rows[0]["RptPeriod"].ToString();

                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["prndate"]));
                //string RptFileName1 = (reportName1 == "C00MSALARY01" || reportName1 == "C00SALSUM01" ? "Payroll.RptSalarySheet01" :
                //    (reportName1 == "C00SALSUM02" ? "Payroll.RptSalaryDedSheet01" : (reportName1 == "C00OTDETAILS01" || reportName1 == "C00OTSUMM01" ? "Payroll.RptOverTimeSheet01" :
                //    (reportName1 == "C00FBONUS01" || reportName1 == "C00FBSUM01" ? "Payroll.RptBonusSheet01" : "Payroll.RptPaySlip001"))));

                string RptFileName1 = "";
                switch (reportName1)
                {
                    case "C00MSALARY01":
                    case "C00SALSUM01":
                        RptFileName1 = "Payroll.RptSalarySheet01";
                        break;
                    case "C00OTDETAILS01":
                    case "C00OTSUMM01":
                        RptFileName1 = "Payroll.RptOverTimeSheet01";
                        break;
                    case "C00FBONUS01":
                    case "C00FBSUM01":
                        RptFileName1 = "Payroll.RptBonusSheet01";
                        break;
                    case "C00PAYSLIP01A":
                    case "C00PAYSLIP01B":
                    case "C00PAYSLIP01C":
                        RptFileName1 = "Payroll.RptPaySlip001";
                        break;
                    case "C00BLUPTO00":
                    case "C00BLUPTO10":
                    case "C00BLUPTO15":
                    case "C00BLUPTO20A":
                    case "C00BLUPTO20B":
                    case "C00BLUPTO30":
                    case "C00BLMORE30":
                    case "C00BLNONE00":
                        RptFileName1 = "Payroll.RptBankLetter01";
                        list1 = this.PrepareBankLetter(reportName1, list1);
                        list2["RptHead"] = WpfProcessAccess.CompInfList[0].comadd1;
                        list3[0].RptParVal1 = ASITUtility.Trans(Convert.ToDouble(list1.Sum(x => x.netpay)), 2);
                        list3[0].RptParVal2 = reportName1;
                        list3[0].RptParVal3 = BankAcNo1;
                        break;
                }
                // this.cmbRptOptions.Items.Add(new ComboBoxItem { Content = "Non-Bank Statement", Tag = "C00BLNONE00" });
                if (RptFileName1.Length == 0)
                    return;

                LocalReport rpt1 = HcmReportSetup.GetLocalReport(RptFileName1, list1, list2, list3);
                if (rpt1 == null)
                    return;

                this.ShowReportWindow(rpt1, "Monthly Salary Sheet", false);
            }
            else if (PrintId == "SS")
            {
                this.MonthlySlrSht01 = ds1.Tables[0].DataTableToList<HmsEntityManpower.Payslip001>();
                this.ShowGridInfo(TrTyp);
            }
        }

        private List<HmsEntityManpower.Payslip001> PrepareBankLetter(string reportName1, List<HmsEntityManpower.Payslip001> list1)
        {
            list1 = list1.FindAll(x => x.netpay > 0);
            switch (reportName1)
            {
                case "C00BLUPTO00":
                    list1 = list1.FindAll(x => x.bankacno.Trim().Length > 0 && x.netpay > 0);
                    break;
                case "C00BLUPTO10":
                    list1 = list1.FindAll(x => x.bankacno.Trim().Length > 0 && x.netpay > 0 && x.grosspay <= 10000);
                    break;
                case "C00BLUPTO15":
                    list1 = list1.FindAll(x => x.bankacno.Trim().Length > 0 && x.netpay > 0 && x.grosspay > 10000 && x.grosspay <= 15000);
                    break;
                case "C00BLUPTO20A":
                    list1 = list1.FindAll(x => x.bankacno.Trim().Length > 0 && x.netpay > 0 && x.grosspay > 15000 && x.grosspay <= 20000);
                    break;
                case "C00BLUPTO20B":
                    list1 = list1.FindAll(x => x.bankacno.Trim().Length > 0 && x.netpay > 0 && x.grosspay > 10000 && x.grosspay <= 20000);
                    break;
                case "C00BLUPTO30":
                    list1 = list1.FindAll(x => x.bankacno.Trim().Length > 0 && x.netpay > 0 && x.grosspay > 20000 && x.grosspay <= 30000);
                    break;
                case "C00BLMORE30":
                    list1 = list1.FindAll(x => x.bankacno.Trim().Length > 0 && x.netpay > 0 && x.grosspay > 30000);
                    break;
                case "C00BLNONE00":
                    list1 = list1.FindAll(x => x.bankacno.Trim().Length == 0 && x.netpay > 0);
                    break;
            }
            int sl1 = 1;
            foreach (var item in list1)
            {
                item.comcod = sl1.ToString().Trim() + ".";
                string[] nm1 = item.hcname.Trim().Split(',');
                item.hcname = nm1[0];
                item.hcdesig = (nm1.Length > 1 ? nm1[1] : "") + (nm1.Length > 2 ? ", " + nm1[2] : "") + (nm1.Length > 3 ? ", " + nm1[3] : "");
                ++sl1;
            }
            return list1;
        }
        private void PrepareIndividualAttndance01(string hccode1, string BrnCod, string SectCod, string TrTyp, string PrintId)
        {
            string AttnDate1 = this.xctk_dtpFrom.Text.Substring(0, 11);
            string monthid1 = DateTime.Parse(AttnDate1).ToString("yyyyMM");
            string AttnDate2 = this.xctk_dtpTo.Text.Substring(0, 11);
            if (DateTime.Parse(AttnDate1).ToString("yyyyMM") != DateTime.Parse(AttnDate2).ToString("yyyyMM"))
                AttnDate2 = AttnDate1;

            var pap1 = vm2.SetParamShowActualAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, AttnDate1, AttnDate2, hccode1, SectCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF")
            {

                var ListDayWiseAttnRpt = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmDayWiseAttanReport>();
                Hashtable rptParam = new Hashtable();
                rptParam["ReportDate"] = "(" + (AttnDate1 == AttnDate2 ? "For " + AttnDate1 : "From " + AttnDate1 + " To " + AttnDate2) + ")";

                //rptParam["empId"] = this.txtblAttnSchempid.Text.ToString();
                //rptParam["empName"] = AtxtEmpAll.Text.ToString();
                //rptParam["slMnth"] = ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString();
                //var list3 = WpfProcessAccess.GetRptGenInfo();

                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
                LocalReport rpt1 = HcmReportSetup.GetLocalReport("Payroll.RptDailyAttn01", ListDayWiseAttnRpt, rptParam, list3);
                if (rpt1 == null)
                    return;

                this.ShowReportWindow(rpt1, "Daily Attendence Schedule Report", false);
            }
            else if (PrintId == "SS")
            {

                this.AttanReportList = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmDayWiseAttanReport>();
                this.ShowGridInfo(TrTyp);

            }
        }
        private void PrepareIndividualAttndance02(string hccode1, string TrTyp, string PrintId)
        {
            //--------------------------------------
            if (this.AtxtEmpAll.Value == null || this.AtxtEmpAll.Value.Length == 0)
            {
                System.Windows.MessageBox.Show("Please select an employee and try again. Thank you.", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                    MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            string hccode1a = this.AtxtEmpAll.Value;

            string monthName1 = ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim();
            DateTime Date1 = DateTime.Parse("01" + monthName1);
            string monthid1 = Date1.ToString("yyyyMM");

            var pap1r = vm2.SetParamShowScheduledAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, hccode1a, "PRINT");
            DataSet ds1r = WpfProcessAccess.GetHmsDataSet(pap1r);
            if (ds1r == null)
                return;

            if (ds1r.Tables[0].Rows.Count == 0)
            {
                System.Windows.MessageBox.Show("Attendance Schedule is not yet updated for " + monthName1 + "\nPlease try again after update. Thank you.", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            string RptType = (TrTyp == "B02B00INDATNRPT02A" ? "Attendance" : "Roster");

            //List<HmsEntityManpower.RptAttnSchInfo> Rptlst = HcmGeneralClass1.GetIndRosterAttendance(monthid1: monthid1, hccode1a: hccode1a, RptType: RptType);
            this.AttnSchInfoList = HcmGeneralClass1.GetIndRosterAttendance(monthid1: monthid1, hccode1a: hccode1a, RptType: RptType);

            if (this.AttnSchInfoList == null)
            {
                System.Windows.MessageBox.Show("Attendance Report not Generated for " + monthName1 + "\nPlease try again later. Thank you.", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                        MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }


            var pap1 = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, hccode1a, "EXISTSTAFFS");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF")
            {

                var Staff1 = ds1.Tables[0].DataTableToList<vmReportHCM1.Stafflist>();

                decimal sumLate1 = this.AttnSchInfoList.Sum(x => x.confirmlate);
                string Notes1 = (sumLate1 > 0 ? "Late Point = " + sumLate1.ToString("##") : "");
                decimal sumEout1 = this.AttnSchInfoList.Sum(x => x.confirmearly);
                Notes1 = Notes1 + (Notes1.Length > 0 && sumEout1 > 0 ? ", " : "") + (sumEout1 > 0 ? "Early Out Point = " + sumEout1.ToString("##") : "");
                Notes1 = (Notes1.Length > 0 ? "Confirm " : "") + Notes1;


                var pap2 = vm1gen.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hccode1a, "PHOTO");
                DataSet dss2 = WpfProcessAccess.GetHmsDataSet(pap2);
                if (dss2 == null)
                    return;
                byte[] bytes12 = null;
                if (!(dss2.Tables[0].Rows[0]["hcphoto"] is DBNull))
                {
                    bytes12 = (byte[])dss2.Tables[0].Rows[0]["hcphoto"];
                }
                Hashtable rptParam = new Hashtable();
                rptParam["Comlogo"] = (bytes12 == null ? "" : Convert.ToBase64String(bytes12));
                rptParam["empId"] = this.AtxtEmpAll.Value.ToString().Trim();
                rptParam["empName"] = "Employee : " + hccode1a.Substring(6, 6) + " - " + Staff1[0].hcname.Trim() + ", " + Staff1[0].designame.Trim(); //& AtxtEmpAll.Text.ToString();
                rptParam["slMnth"] = (RptType == "Attendance" ? "Monthly Attendence" : "Duty Roster") + " - " + monthName1;
                rptParam["ParmNotes1"] = Notes1;
                rptParam["ParmBrnDept1"] = "Department : " + Staff1[0].deptname.Trim() + ", Joining Date : " + Staff1[0].joindat.Trim() +
                                            ", Reporting Date : " + Convert.ToDateTime(ds1r.Tables[2].Rows[0]["ServerTime"]).ToString("dd-MMM-yyyy hh:mm tt");
                //var list3 = WpfProcessAccess.GetRptGenInfo();
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1r.Tables[2].Rows[0]["ServerTime"]));
                LocalReport rpt1 = HcmReportSetup.GetLocalReport("Payroll.RptAttenSchedule01", this.AttnSchInfoList, rptParam, list3);
                if (rpt1 == null)
                    return;

                this.ShowReportWindow(rpt1, "Attendence Schedule Report", false);
            }
            else if (PrintId == "SS")
            {
                this.ShowGridInfo(TrTyp);
            }
            //--------------------------------------
        }
        private void PrepareMonthlyAttndanceSum01(string BrnCod, string SectCod, string TrTyp, string PrintId)
        {
            string AttnDate1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim()).ToString("dd-MMM-yyyy");

            //AttnDate1 = this.xctk_dtpFrom.Text.Substring(0, 11);
            //AttnDate1 = "01-" + AttnDate1.Substring(3);
            string monthid1 = DateTime.Parse(AttnDate1).ToString("yyyyMM");
            string AttnDate2 = DateTime.Parse(AttnDate1).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            string hcBran1 = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            string hccode1 = "%";
            var pap1 = vm2.SetParamShowActualAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, AttnDate1, AttnDate2, hccode1, SectCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var ListDayWiseAttnRpt = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmDayWiseAttanReport>();

            foreach (var attnRecord1 in ListDayWiseAttnRpt)
            {
                //var actInf = vmr1.CalcActWorkHour(attnRecord1);
                var actInf = HcmGeneralClass1.CalcActWorkHourAdv(attnRecord1);// vmr1.CalcActWorkHourAdv(attnRecord1);
                attnRecord1.actworkhr = actInf.actworkhr;
                attnRecord1.actoffhr = actInf.actoffhr;
                attnRecord1.lesworkhr = actInf.lesworkhr;
                attnRecord1.otworkhr = actInf.otworkhr;
                attnRecord1.latepoint = actInf.confirmlate;
                attnRecord1.latein = (actInf.confirmlate > 0 ? actInf.latein : 0.00m);
                attnRecord1.eoutpoint = actInf.confirmearly;
                attnRecord1.earlyout = (actInf.confirmearly > 0 ? actInf.earlyout : 0.00m);
                attnRecord1.reserror = actInf.confirmerr;
                attnRecord1.Rmrks = actInf.attnrmrk;
            }

            var StaffList1 = ListDayWiseAttnRpt.GroupBy(x => x.hccode).Select(y => y.First()).OrderBy(z => z.hccode).ToList();
            var rptMonthlyAttnSum01 = new List<HmsEntityManpower.HcmMonthAttnEvalReport01>();
            foreach (var itemStaff in StaffList1)
            {
                var AttnSum1a = new HmsEntityManpower.HcmMonthAttnEvalReport01();
                //------ General Information ----------------------------------------
                AttnSum1a.comcod = itemStaff.comcod;
                AttnSum1a.monthid = itemStaff.monthid;
                AttnSum1a.sectcod = itemStaff.sectcod;
                AttnSum1a.sectname = itemStaff.sectname;
                AttnSum1a.hccode = itemStaff.hccode;
                AttnSum1a.hcnamdsg = itemStaff.hcnamdsg;
                //------ Approved Schedule/Roster Information --------------------------
                AttnSum1a.offwrkday = decimal.Parse(AttnDate2.Substring(0, 2));

                var ListDayWiseAttnRptp = ListDayWiseAttnRpt.FindAll(x => x.hccode == AttnSum1a.hccode);

                AttnSum1a.schwrkday = AttnSum1a.offwrkday - Convert.ToDecimal(ListDayWiseAttnRptp.FindAll(x => x.schworkhr == 0).Count);

                AttnSum1a.dayoffgen = Convert.ToDecimal(ListDayWiseAttnRptp.FindAll(x => x.attnstatid == "SIHA00502002").Count);
                AttnSum1a.dayoffleav = Convert.ToDecimal(ListDayWiseAttnRptp.FindAll(x => x.attnstatid == "SIHA00502003").Count)         // Full Leave
                                     + Convert.ToDecimal(ListDayWiseAttnRptp.FindAll(x => x.attnstatid == "SIHA00501008").Count) / 2.0m; // Half Leave
                AttnSum1a.dayofftotal = AttnSum1a.dayoffgen + AttnSum1a.dayoffleav;
                //----- Actual Attendance Information With Error ------------------------

                AttnSum1a.sabsentday = Convert.ToDecimal(ListDayWiseAttnRptp.FindAll(x => x.Rmrks.Contains("(Absent)")).Count);
                AttnSum1a.sabsentday = AttnSum1a.sabsentday + Convert.ToDecimal(ListDayWiseAttnRptp.Sum(x => x.reserror));
                //AttnSum1a.sabsentday = Convert.ToDecimal(ListDayWiseAttnRptp.FindAll(x => x.atndtl.Trim().Length == 0 && x.attnstatid.Substring(0, 9) == "SIHA00501").Count);

                AttnSum1a.spresentday = Convert.ToDecimal(ListDayWiseAttnRptp.FindAll(x => x.atndtl.Trim().Length > 0 && x.attnstatid.Substring(0, 9) == "SIHA00501").Count);
                AttnSum1a.cpresentday = Convert.ToDecimal(ListDayWiseAttnRptp.FindAll(x => x.actworkhr > 0.00m && x.attnstatid.Substring(0, 9) == "SIHA00501").Count);
                AttnSum1a.cschwrkhour = ListDayWiseAttnRptp.FindAll(x => x.actworkhr > 0.00m && x.attnstatid.Substring(0, 9) == "SIHA00501").Sum(y => y.schworkhr);
                AttnSum1a.cactwrkhour = ListDayWiseAttnRptp.FindAll(x => x.actworkhr > 0.00m && x.attnstatid.Substring(0, 9) == "SIHA00501").Sum(y => y.actworkhr);
                AttnSum1a.cactlateday = Convert.ToDecimal(ListDayWiseAttnRptp.Sum(x => x.latepoint));

                AttnSum1a.cactlatehour = ListDayWiseAttnRptp.FindAll(x => x.actworkhr > 0.00m && x.latein > 0 && x.attnstatid.Substring(0, 9) == "SIHA00501").Sum(y => y.latein) / 60.00m;
                AttnSum1a.cacteoutday = Convert.ToDecimal(ListDayWiseAttnRptp.Sum(x => x.eoutpoint));

                AttnSum1a.cacteouthour = ListDayWiseAttnRptp.FindAll(x => x.actworkhr > 0.00m && x.earlyout > 0 && x.attnstatid.Substring(0, 9) == "SIHA00501").Sum(y => y.earlyout) / 60.00m;
                rptMonthlyAttnSum01.Add(AttnSum1a);
                /*
                <ComboBoxItem Content="Present" Tag="SIHA00501001" />
                <ComboBoxItem Content="Absent (Day Off)" Tag="SIHA00502002" />
                <ComboBoxItem Content="Absent (On Leave)" Tag="SIHA00502003" />
                <ComboBoxItem Content="Present (Over-Time)" Tag="SIHA00501006" />
                <ComboBoxItem Content="Present (Outdoor Duty)" Tag="SIHA00501007" />
                <ComboBoxItem Content="Present (Half-Over-Time)" Tag="SIHA00501005" />
                <ComboBoxItem Content="Present (Half-Leave)" Tag="SIHA00501008" />
               
                 */

            }

            rptMonthlyAttnSum01.Sort(delegate(HmsEntityManpower.HcmMonthAttnEvalReport01 x, HmsEntityManpower.HcmMonthAttnEvalReport01 y)
            {
                return (x.sectcod + x.hccode).CompareTo(y.sectcod + y.hccode);
            });

            if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF")
            {
                Hashtable rptParam = new Hashtable();

                rptParam["ReportDate"] = AttnDate1;//  this.xctk_dtpFrom.Text;
                //rptParam["empId"] = this.txtblAttnSchempid.Text.ToString();
                //rptParam["empName"] = AtxtEmpAll.Text.ToString();
                //rptParam["slMnth"] = ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString();
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]));
                LocalReport rpt1 = HcmReportSetup.GetLocalReport("Payroll.RptMonthAttnSum01", rptMonthlyAttnSum01, rptParam, list3);
                if (rpt1 == null)
                    return;

                this.ShowReportWindow(rpt1, "Daily Attendence Schedule Report", false);

                //Code Goes from here -- Hafiz 05-Aug-2017
                //this.dgActTrmAttn.ItemsSource = this.ListDayWiseAttnRpt;
            }
            else if (PrintId == "SS")
            {
                this.MonthlyAttnSum01 = rptMonthlyAttnSum01;
                this.ShowGridInfo(TrTyp);
            }
        }

        //private void abcd()
        //{
        //    var pap1r = vm2.SetParamShowScheduledAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, hccode1a, "PRINT");
        //    DataSet ds1r = WpfProcessAccess.GetHmsDataSet(pap1r);
        //    if (ds1r == null)
        //        return;

        //    if (ds1r.Tables[0].Rows.Count == 0)
        //    {
        //        System.Windows.MessageBox.Show("Attendance Schedule is not yet updated for " + monthName1 + "\nPlease try again after update. Thank you.", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
        //        MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        //        return;
        //    }

        //    var ListSchAttn1r = ds1r.Tables[0].DataTableToList<HmsEntityManpower.RptAttnSchInfo>();

        //}


        private void ShowReportWindow(LocalReport rpt1, string WindowTitle1 = "Human Resource Information Report", bool DoDirectPrint = false)
        {
            if (DoDirectPrint)
            {
                RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                DirectPrint1.PrintReport(rpt1);
                DirectPrint1.Dispose();
            }
            else
            {
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
        }

        private void AtxtEmpAll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtEmpAll.ContextMenu.IsOpen = true;
        }
        private void CreateNewTabForReport(DataGrid dgRpt1t)
        {
            if (dgRpt1t == null)
                return;
            string fromDate = xctk_dtpFrom.Text.Trim();
            string ToDate = xctk_dtpTo.Text.ToString().Trim();

            string ItemTitle = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Header.ToString().ToUpper(); // ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString().ToUpper();
            string ItemTag = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Tag.ToString();
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
                //string fromDate = xctk_dtpFrom.Text.Trim();
                //string ToDate = xctk_dtpTo.Text.ToString().Trim();
                //string TrHead = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString();
                //string From2Date = " From " + fromDate + " To " + ToDate;

                if (this.TabUcGrid1.Items.Count > 6)
                    return;
                string cmbRPTTag = ((ComboBoxItem)(this.cmbRptOptions.SelectedItem)).Tag.ToString();

                switch (ItemTag)
                {
                    case "B01B00INDATNRPT01":
                        //this.dgRpt1 = GridReportHCM1.IndAttndance01.GetDataGrid(this.AttanReportList);
                        //this.stkpDataGrid.Children.Add(this.dgRpt1);
                        this.CreateNewTabForReport(GridReportHCM1.IndAttndance01.GetDataGrid(this.AttanReportList.ToList()));

                        break;
                    case "B02B00INDATNRPT02":
                    case "B02B00INDATNRPT02A":
                        //this.dgRpt1 = GridReportHCM1.AttenSchedule01.GetDataGrid(this.AttnSchInfoList);
                        //this.stkpDataGrid.Children.Add(this.dgRpt1);
                        this.CreateNewTabForReport(GridReportHCM1.AttenSchedule01.GetDataGrid(this.AttnSchInfoList.ToList()));
                        break;
                    case "B06B00MATTNSUM01": //B03B00LEVRPT01
                        //this.dgRpt1 = GridReportHCM1.MonthAttenSchedule01.GetDataGrid(this.MonthlyAttnSum01);
                        //this.stkpDataGrid.Children.Add(this.dgRpt1);
                        this.CreateNewTabForReport(GridReportHCM1.MonthAttenSchedule01.GetDataGrid(this.MonthlyAttnSum01.ToList()));
                        break;
                    case "B03B00LEVRPT01": //B03B00LEVRPT01  //
                        //this.dgRpt1 = GridReportHCM1.LeaveDetails01.GetDataGrid(this.AttnLeaveInfoList);
                        //this.stkpDataGrid.Children.Add(this.dgRpt1);
                        this.CreateNewTabForReport(GridReportHCM1.LeaveDetails01.GetDataGrid(this.AttnLeaveInfoList.ToList()));
                        break;
                    case "C01SALARY_OT01": //B03B00LEVRPT01  //C01SALARY_OT01
                        //this.dgRpt1 = GridReportHCM1.LeaveDetails01.GetDataGrid(this.AttnLeaveInfoList);
                        //this.stkpDataGrid.Children.Add(this.dgRpt1);
                        this.CreateNewTabForReport(GridReportHCM1.MonthlySalarySheet01.GetDataGrid(this.MonthlySlrSht01.ToList()));
                        if (cmbRPTTag == "C00MSALARY01" && this.xctk_dtpFrom.Text.Trim() == this.xctk_dtpTo.Text.Trim() &&
                            this.btnUpdateVoucher.Visibility == Visibility.Visible && WpfProcessAccess.CompInfList[0].comcod == "6521")// For Digilab Only
                            this.stkpUpdateAccVoucher.Visibility = Visibility.Visible;

                        break;
                }
            }

            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HR-Gvm-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void tvRptRtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.btnGenerate_Click(null, null);
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

        private void btnUpdateVoucher_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to update collection vouchers", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (msgresult != MessageBoxResult.Yes)
                return;

            this.UpdatProvidentFundJournalVoucher();
        }

        private void UpdatProvidentFundJournalVoucher()
        {

            try
            {
                var brnlst1 = WpfProcessAccess.CompInfList[0].BranchList.ToList();
                string vno1 = "";
                string vtag1 = "";

                foreach (var brnch1 in brnlst1)
                {
                    var pflist = this.MonthlySlrSht01.FindAll(x => x.brncod == brnch1.brncod).ToList();
                    if (pflist.Count == 0)
                        continue;

                    string VouBrn1 = brnch1.brncod.Substring(0, 4);// .grp2cod.Substring(0, 4);// "1101";
                    DateTime vouDate1 = DateTime.Parse(this.xctk_dtpFrom.Text);
                    string VouType1 = "JVA99";
                    string cactcod1 = "000000000000";
                    string EditVounum1 = "";
                    decimal pfSum1 = 0.00m;
                    var ListVouTable1u = new List<vmEntryVoucher1.VouTable>();
                    foreach (var vouinf in pflist)
                    {
                        ListVouTable1u.Add(new vmEntryVoucher1.VouTable()
                        {
                            trnsl = 1,
                            DrCrOrder = "C",
                            cactcode = cactcod1,
                            sectcod = VouBrn1 + "00101001", //"110100101001",
                            actcode = "230600010002",   // PROVIDENT FUND PAYABLE
                            sircode = vouinf.hccode, //"000000000000",
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
                            trnam = vouinf.salded02 * -1.00m,
                            trnrmrk = ""
                        });
                        pfSum1 += vouinf.salded02;
                    }
                    ListVouTable1u.Add(new vmEntryVoucher1.VouTable()
                     {
                         trnsl = 1,
                         DrCrOrder = "C",
                         cactcode = cactcod1,
                         sectcod = VouBrn1 + "00101001", //"110100101001",
                         actcode = "230600010001", // PROVIDENT FUND DEPOSITE (DHAKA BANK)
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
                         trnam = pfSum1,
                         trnrmrk = ""
                     });

                    string cheqbookid1 = "XXXXXXXXXXXXXXXXXX";
                    string cheqno1 = "";

                    string vounum1 = VouType1.Substring(0, 3) + vouDate1.ToString("yyyyMM") + VouBrn1.Substring(0, 4);
                    string RecnDate1 = vouDate1.ToString("dd-MMM-yyyy");
                    string MonthName1 = ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim().ToUpper();
                    var vouPrInfo1 = new vmEntryVoucher1.VouPrInfo()
                    {
                        vounum = vounum1,
                        voudat = DateTime.Parse(this.xctk_dtpFrom.Text),
                        vouref = "", //this.txtVouRef.Text.Trim(),
                        cheqbookid = cheqbookid1,
                        chqref = cheqno1, //((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim(),
                        advref = "", //this.txtAdvice.Text.Trim(),
                        vounar = "PROVIDENT FUND DEDUCTION AND DEPOSIT TO BANK FOR " + brnch1.brnnam.Trim() + " FOR THE MONTHE OF " + MonthName1, //this.txtVouNar.Text.Trim(),
                        curcod = "CBCICOD01001",
                        curcnv = 1.00m,
                        vstatus = "A",
                        recndt = DateTime.Parse(RecnDate1), //DateTime.Parse("01-Jan-1900"),
                        vtcode = VouType1.Substring(3, 2),
                    };

                    if (pfSum1 > 0)
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
                System.Windows.MessageBox.Show("HCM.Rpt.ACV-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop,
                        MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
