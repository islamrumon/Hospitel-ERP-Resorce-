using ASITFunLib;
using ASITHmsEntity;
using ASITHmsRpt3Manpower;
using ASITHmsViewMan.Manpower;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ASITHmsWpf.Manpower
{
    /// <summary>
    /// Interaction logic for frmEntryAttn104.xaml
    /// </summary>
    public partial class frmEntryAttn104 : UserControl
    {

        private List<vmEntryAttnLeav1.LeaveSummary> LeaveSumList1 = new List<vmEntryAttnLeav1.LeaveSummary>();
        private List<vmEntryAttnLeav1.LeaveAppMain> LeaveAppMainList1 = new List<vmEntryAttnLeav1.LeaveAppMain>();
        private List<vmEntryAttnLeav1.LeaveAppDetails> LeaveAppList1 = new List<vmEntryAttnLeav1.LeaveAppDetails>();
        private List<HmsEntityManpower.HcmLeaveDetailsReport01> PrevLeaveDetails1 = new List<HmsEntityManpower.HcmLeaveDetailsReport01>();
        
        private List<vmReportHCM1.Stafflist> StaffList1 = new List<vmReportHCM1.Stafflist>();
        private vmReportHCM1 vmr1 = new vmReportHCM1();
        private vmEntryHRGenral1 vm1 = new vmEntryHRGenral1();
        private vmEntryAttnLeav1 vm2 = new vmEntryAttnLeav1();

        private bool FrmInitialized = false;
        public frmEntryAttn104()
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
            try
            {
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAttn104_btnLeaveUpdate") == null)
                {
                    this.btnLeaveUpdate.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCH-Leave-102-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void Objects_On_Init()
        {


            this.stkpLeaveAppFrm.Visibility = Visibility.Hidden;

            var pap = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, "%", "EXISTSTAFFS");
            DataSet ds = WpfProcessAccess.GetHmsDataSet(pap);
            if (ds == null)
                return;
            this.StaffList1.Clear();
            this.StaffList1 = ds.Tables[0].DataTableToList<vmReportHCM1.Stafflist>();

            this.AtxtEmpAll.AutoSuggestionList.Clear();

            foreach (var item1 in this.StaffList1)
            {
                item1.hcname = item1.hccode.Trim().Substring(6) + " - " + item1.hcname.Trim() + ", " + item1.designame.Trim();
                //string end1 = item1.hccode.Trim().Substring(6) + " - " + item1.hcname.Trim() + ", " + item1.designame.Trim();
                this.AtxtEmpAll.AddSuggstionItem(item1.hcname, item1.hccode.Trim());

                var mitm1 = new MenuItem() { Header = item1.hcname, Tag = item1.hccode.Trim() };
                mitm1.Click += conMenuHCMAtnAll_MouseClick;
                this.conMenuHCMAtnAll.Items.Add(mitm1);
            }

            if (WpfProcessAccess.GenInfoTitleList == null)
                WpfProcessAccess.GetGenInfoTitleList();

            var LeaveEntry = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SIHL001" && x.actcode.Substring(9, 3) != "000");
            this.LeaveSumList1.Clear();
            ContextMenu ctmLeaveType = new ContextMenu();
            int sl1 = 1;
            foreach (var item in LeaveEntry)
            {
                string val1 = item.actdesc.Trim() + (item.acttdesc.Trim().Length > 0 ? " (" + item.acttdesc.Trim() + ")" : "");
                this.LeaveSumList1.Add(new vmEntryAttnLeav1.LeaveSummary() { slnum = sl1.ToString("00"), leavcod = item.actcode, leavdesc = val1, leavopn = 0, leavadd = 0, leavtot = 0, leavavil = 0, leavbal1 = 0, leavapp = 0, leavbal2 = 0 });
                MenuItem miLeaveType1 = new MenuItem() { Header = val1, Tag = item.actcode };
                miLeaveType1.Click += miLeaveType1_Click;
                ctmLeaveType.Items.Add(miLeaveType1);
                sl1++;
            }
            //this.lblAppliedFor.ContextMenu = ctmLeaveType;
            this.DgvLeavApp.ContextMenu = ctmLeaveType;
            this.DgvLeavSum.ItemsSource = this.LeaveSumList1;
            int yer1 = DateTime.Today.Year;
            for (int x = -2; x <= 2; x++)
            {
                string val1 = (yer1 + x).ToString("0000");
                this.cmbLeaveYear.Items.Add(new ComboBoxItem() { Content = val1, Tag = val1 });
            }
            this.cmbLeaveYear.SelectedIndex = 2;
            this.xctk_dtpApplyDate.Value = DateTime.Today;
            this.xctk_dtpApprovDate.Value = DateTime.Today;
            //this.xctk_InfoDate.Value = DateTime.Today;
        }



        private void conMenuHCMAtnAll_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtEmpAll.Text = ((MenuItem)sender).Header.ToString().Trim();
        }

        private void btnShowInfo_Click(object sender, RoutedEventArgs e)
        {
            this.CleanUpScreen();
            this.stkpMain.IsEnabled = true;
            this.stkpLeaveAppFrm.Visibility = Visibility.Collapsed;
            if (this.btnShowInfo.Content.ToString() == "Next")
            {
                this.btnShowInfo.Content = "Show";
                return;
            }
            if (this.AtxtEmpAll.Text.Trim().Length == 0)
                return;

            if (!this.ShowRequiredInfo())
                return;

            this.stkpMain.IsEnabled = false;
            this.stkpLeaveAppFrm.Visibility = Visibility.Visible;
            this.btnShowInfo.Content = "Next";
        }

        private void CleanUpScreen()
        {
            this.lblLeavID.Content = "";
            this.stkpLeaveEntry1.IsEnabled = true;
            this.stkpApproval2.IsEnabled = true;
            this.lblEditMode.Tag = "00";
            this.btnEditLeave.IsEnabled = true;
            this.DgvLeavApp.ItemsSource = null;
            this.LeaveAppList1.Clear();
            this.btnLeaveRecal.Content = "Total leave applied for 0 day";
            this.lblEditMode.Visibility = Visibility.Hidden;
            this.xctk_dtpApplyDate.Value = DateTime.Today;
            this.xctk_dtpApprovDate.Value = DateTime.Today;
            this.txtLevresn.Text = "";
            this.txttimecon.Text = "";
            this.autoLeavHcCod2.ItemsSource = null;
            this.autoLeavForwardBy.ItemsSource = null;
            this.autoLeavApprovBy.ItemsSource = null;
            this.txtnotes.Text = "";
            foreach (var item in this.LeaveSumList1)
            {
                item.leavopn = 0;
                item.leavadd = 0;
                item.leavtot = 0;
                item.leavavil = 0;
                item.leavbal1 = 0;
                item.leavapp = 0;
                item.leavbal2 = 0;
            }
            this.DgvLeavSum.Items.Refresh();
            this.DgvLeavDetails.Items.Refresh();
            // private List<vmEntryAttnLeav1.LeveSummary> LeaveSumList1 = new List<vmEntryAttnLeav1.LeveSummary>();

        }
        private bool ShowRequiredInfo()
        {
            string hccode1 = this.AtxtEmpAll.Value.ToString().Trim();
            string yearid1 = ((ComboBoxItem)this.cmbLeaveYear.SelectedItem).Tag.ToString().Trim();

            var pap = vmr1.SetParamShowLeaveDetails(WpfProcessAccess.CompInfList[0].comcpcod, hccode1, yearid1, "%");

            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
            if (ds1 == null)
                return false;

            this.PrevLeaveDetails1 = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmLeaveDetailsReport01>();

            var openList1 = this.PrevLeaveDetails1.FindAll(x => x.leavid == "00" && x.leavcod != "000000000000").ToList();

            foreach (var dr2 in openList1)
            {
                string Olc1 = dr2.leavcod;
                decimal Olq1 = dr2.totday;
                foreach (var item in this.LeaveSumList1)
                {
                    if (item.leavcod == Olc1)
                    {
                        item.leavopn = Olq1;
                        break;
                    }
                }
            }
            var maxid = this.PrevLeaveDetails1.Max(x => x.leavid);
            this.lblLeavID.Content =(maxid == null ?  "01":(int.Parse(maxid) + 1).ToString("00"));
            this.DgvLeavDetails.ItemsSource = this.PrevLeaveDetails1;
            this.DgvLeavDetails.Items.Refresh();
            this.CalculateLeaveBalance();
            return true;
        }
        private void AtxtEmpAll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtEmpAll.ContextMenu.IsOpen = true;
        }
        private void btnLeaveRecal_Click(object sender, RoutedEventArgs e)
        {
            this.CalculateLeaveBalance();
            if (this.LeaveAppList1.Count == 0)
                return;

            DateTime submitdat1 = DateTime.Parse(this.xctk_dtpApplyDate.Text);
            DateTime aprvdat1 = DateTime.Parse(this.xctk_dtpApprovDate.Text);

            submitdat1 = (submitdat1 > DateTime.Today ? DateTime.Today : submitdat1);
            aprvdat1 = (aprvdat1 > DateTime.Today ? DateTime.Today : aprvdat1);
            aprvdat1 = (submitdat1 > aprvdat1 ? submitdat1 : aprvdat1);
            this.xctk_dtpApplyDate.Value = submitdat1;
            this.xctk_dtpApprovDate.Value = aprvdat1;

            this.LeaveAppList1[0].enddat = (this.LeaveAppList1[0].enddat < this.LeaveAppList1[0].begndat ? this.LeaveAppList1[0].begndat : this.LeaveAppList1[0].enddat);
            this.LeaveAppList1[0].endstat = (this.LeaveAppList1[0].begndat == this.LeaveAppList1[0].enddat && this.LeaveAppList1[0].begnstat == "2nd" ? "2nd" : this.LeaveAppList1[0].endstat);

            if (this.LeaveAppList1.Count >= 2)
            {
                if (this.LeaveAppList1[0].endstat == "1st")
                {
                    this.LeaveAppList1[1].begndat = this.LeaveAppList1[0].enddat;
                    this.LeaveAppList1[1].begnstat = "2nd";
                }
                else
                {
                    this.LeaveAppList1[1].begndat = this.LeaveAppList1[0].enddat.AddDays(1);
                    this.LeaveAppList1[1].begnstat = "1st";
                }

                this.LeaveAppList1[1].enddat = (this.LeaveAppList1[1].enddat < this.LeaveAppList1[1].begndat ? this.LeaveAppList1[1].begndat : this.LeaveAppList1[1].enddat);
                this.LeaveAppList1[1].endstat = (this.LeaveAppList1[1].begndat == this.LeaveAppList1[1].enddat && this.LeaveAppList1[1].begnstat == "2nd" ? "2nd" : this.LeaveAppList1[1].endstat);
            }

            if (this.LeaveAppList1.Count >= 3)
            {

                if (this.LeaveAppList1[1].endstat == "1st")
                {
                    this.LeaveAppList1[2].begndat = this.LeaveAppList1[1].enddat;
                    this.LeaveAppList1[2].begnstat = "2nd";
                }
                else
                {
                    this.LeaveAppList1[2].begndat = this.LeaveAppList1[1].enddat.AddDays(1);
                    this.LeaveAppList1[2].begnstat = "1st";
                }

                this.LeaveAppList1[2].enddat = (this.LeaveAppList1[2].enddat < this.LeaveAppList1[2].begndat ? this.LeaveAppList1[2].begndat : this.LeaveAppList1[2].enddat);
                this.LeaveAppList1[2].endstat = (this.LeaveAppList1[2].begndat == this.LeaveAppList1[2].enddat && this.LeaveAppList1[2].begnstat == "2nd" ? "2nd" : this.LeaveAppList1[2].endstat);
            }

            if (this.LeaveAppList1.Count == 4)
            {
                if (this.LeaveAppList1[2].endstat == "1st")
                {
                    this.LeaveAppList1[3].begndat = this.LeaveAppList1[2].enddat;
                    this.LeaveAppList1[3].begnstat = "2nd";
                }
                else
                {
                    this.LeaveAppList1[3].begndat = this.LeaveAppList1[2].enddat.AddDays(1);
                    this.LeaveAppList1[3].begnstat = "1st";
                }

                this.LeaveAppList1[3].enddat = (this.LeaveAppList1[3].enddat < this.LeaveAppList1[3].begndat ? this.LeaveAppList1[3].begndat : this.LeaveAppList1[3].enddat);
                this.LeaveAppList1[3].endstat = (this.LeaveAppList1[3].begndat == this.LeaveAppList1[3].enddat && this.LeaveAppList1[3].begnstat == "2nd" ? "2nd" : this.LeaveAppList1[3].endstat);
            }

            foreach (var item1 in this.LeaveAppList1)
            {
                if (item1.begndat == item1.enddat && item1.begnstat == item1.endstat)
                    item1.halfday = 0.5m;
                else if (item1.begndat != item1.enddat && item1.begnstat == "1st" && item1.endstat == "1st")
                    item1.halfday = 0.5m;
                else if (item1.begndat != item1.enddat && item1.begnstat == "2nd" && item1.endstat == "1st")
                    item1.halfday = 1.0m;
                else if (item1.begndat != item1.enddat && item1.begnstat == "2nd" && item1.endstat == "2nd")
                    item1.halfday = 0.5m;
                else
                    item1.halfday = 0.0m;

                item1.totday = item1.enddat.Subtract(item1.begndat).Days + 1 - item1.halfday;
            }

            decimal leaveDays1 = this.LeaveAppList1.Sum(x => x.totday);
            DateTime minDat1 = this.LeaveAppList1.Min(x => x.begndat);
            DateTime maxDat1 = this.LeaveAppList1.Max(x => x.enddat);

            this.btnLeaveRecal.Content = "(" + minDat1.ToString("MMM-dd") + " to " + maxDat1.ToString("MMM-dd yyyy") + ")   Total leave applied for = " +
                leaveDays1.ToString("##0.0").Trim() + " day" + (leaveDays1 > 1 ? "s" : "");

            this.DgvLeavApp.Items.Refresh();

            this.CalculateLeaveBalance();
        }
        private void CalculateLeaveBalance()
        {
            DateTime aplyDat1 = DateTime.Parse(this.xctk_dtpApplyDate.Text);
            foreach (var item in this.LeaveSumList1)
            {
                item.leavadd = (item.leavcod == "SIHL00101002" ? aplyDat1.Month : (item.leavcod == "SIHL00101003" ? aplyDat1.Month / 2.0m : 0.00m));
                item.leavtot = item.leavopn + item.leavadd;

                item.leavavil = this.PrevLeaveDetails1.FindAll(x => x.leavid != "00" && x.leavcod == item.leavcod).Sum(y => y.totday);// tavail1;
                item.leavbal1 = item.leavtot - item.leavavil;

                item.leavapp = this.LeaveAppList1.FindAll(x =>x.leavcod == item.leavcod).Sum(y=>y.totday);
                item.leavbal2 = item.leavbal1 - item.leavapp;
            }
            this.DgvLeavSum.Items.Refresh();

            /*
             *  ACTCODE	        ACTDESC	                ACTELEV	ACTTYPE	ACTTDESC
                SIHL00101001	EARNED LEAVE		        N	EL
                SIHL00101002	CASUAL LEAVE		        N	CL
                SIHL00101003	SICK LEAVE		            N	SL

                SIHL00102001	MATERNITY LEAVE		        N	MATL
                SIHL00102002	COMPENSATORY LEAVE		    N	COML
                SIHL00102009	SPECIAL LEAVE		        N	SPCL
                SIHL00102019	LEAVE WITHOUT PAY		    N	WIPL
             * 
             */
        }
        private void btnPrintLeave_Click(object sender, RoutedEventArgs e)
        {
            var list1 = new List<HmsEntityManpower.HcmLeave01>();
            if (this.chkLeaveForm.IsChecked == true)
            {
                LocalReport rpt1 = HcmReportSetup.GetLocalReport("Payroll.RptLeaveForm01", list1, null, null);
                if (rpt1 == null)
                    return;
                this.ShowReportWindow(rpt1, "Human Resource Leave Information Report", false);
            }
        }
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
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
        }

        private void btnLeaveUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.LeaveAppList1.Count > 0)
                    this.btnLeaveRecal_Click(null, null);


                string hccode1 = this.AtxtEmpAll.Value.ToString().Trim();

                string yearid1 = ((ComboBoxItem)this.cmbLeaveYear.SelectedItem).Tag.ToString().Trim();
                string leavid1 = this.lblEditMode.Tag.ToString();
                this.LeaveAppMainList1.Clear();

                string hccode2a = "000000000000";
                if (this.autoLeavHcCod2.SelectedValue != null)
                    hccode2a = this.autoLeavHcCod2.SelectedValue.ToString().Trim();

                string forwrdbyid1 = "000000000000";
                if (this.autoLeavForwardBy.SelectedValue != null)
                    forwrdbyid1 = this.autoLeavForwardBy.SelectedValue.ToString().Trim();

                string approvbyid1 = "000000000000";
                if (this.autoLeavApprovBy.SelectedValue != null)
                    approvbyid1 = this.autoLeavApprovBy.SelectedValue.ToString().Trim();

                this.LeaveAppMainList1.Add(new vmEntryAttnLeav1.LeaveAppMain()
                {
                    leavid = leavid1, //"00",
                    submitdat = DateTime.Parse(this.xctk_dtpApplyDate.Text),
                    aprvdat = DateTime.Parse(this.xctk_dtpApprovDate.Text),
                    lreason = this.txtLevresn.Text.Trim(),
                    lcontact = this.txttimecon.Text.Trim(),
                    hccode2 = hccode2a,
                    forwrdbyid = forwrdbyid1,
                    approvbyid = approvbyid1,
                    leavnote = this.txtnotes.Text.Trim()
                });

                DataSet ds1 = vm2.GetDataSetForUpdateLeaveInfo(_LeaveAppMainList: this.LeaveAppMainList1, _LeaveAppList: this.LeaveAppList1, _LeveSummary: this.LeaveSumList1);

                var pap1 = vm2.SetParamUpdateLeaveInfo(WpfProcessAccess.CompInfList[0].comcod, ds1, hccode1, yearid1);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                string msg1 = "No information updated";
                if (ds2.Tables[2].Rows.Count > 0)
                    msg1 = "Leave information updated successfully";
                else if ((ds2.Tables[0].Rows.Count > 0) || (ds2.Tables[0].Rows.Count == 0 && ds2.Tables[1].Rows.Count > 0))
                    msg1 = "Only opening information updated";
                //else if (ds2.Tables[0].Rows.Count == 0 && ds2.Tables[1].Rows.Count == 0)
                //    msg1 = "No information updated";

                System.Windows.MessageBox.Show(msg1, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                if (msg1 == "No information updated")
                    return;

                this.stkpLeaveEntry1.IsEnabled = false;
                this.stkpApproval2.IsEnabled = false;
                    
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Emp.Leav-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private string ValidateReconDate(string Str1)
        {
            try
            {
                return DateTime.Parse(Str1).ToString("dd-MMM-yyyy");
            }
            catch
            {
                return "";
            }
        }

        private void DgvLeavAppTxtBeginDate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var wmt1 = (Xceed.Wpf.Toolkit.WatermarkTextBox)sender;
            wmt1.Text = (this.ValidateReconDate(wmt1.Text.Trim()).Length == 0 ? DateTime.Today.ToString("dd-MMM-yyyy") : wmt1.Text);
        }

        private void DgvLeavAppTxtBeginDate_LostFocus(object sender, RoutedEventArgs e)
        {
            var wmt1 = (Xceed.Wpf.Toolkit.WatermarkTextBox)sender;
            wmt1.Text = this.ValidateReconDate(wmt1.Text.Trim());
        }

        private void DgvLeavAppTxtEndDate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var wmt1 = (Xceed.Wpf.Toolkit.WatermarkTextBox)sender;
            wmt1.Text = (this.ValidateReconDate(wmt1.Text.Trim()).Length == 0 ? DateTime.Today.ToString("dd-MMM-yyyy") : wmt1.Text);
        }

        private void DgvLeavAppTxtEndDate_LostFocus(object sender, RoutedEventArgs e)
        {
            var wmt1 = (Xceed.Wpf.Toolkit.WatermarkTextBox)sender;
            wmt1.Text = this.ValidateReconDate(wmt1.Text.Trim());
        }

        void miLeaveType1_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi1 = (MenuItem)sender;
            string leaveCod1 = mi1.Tag.ToString();
            string leaveDesc1 = mi1.Header.ToString().Trim();
            this.AddRemoveLeave(leaveCod1, leaveDesc1);
        }
        private void AddRemoveLeave(string leaveCod1, string leaveDesc1)
        {
            //string leaveCod1 = ci1.Tag.ToString();
            //string leaveDesc1 = ci1.Content.ToString().Trim();

            string leaveid1 = this.lblEditMode.Tag.ToString();

            bool found1 = false;
            int fidx1 = -1;
            foreach (var item in this.LeaveAppList1)
            {
                fidx1++;
                if (item.leavcod == leaveCod1)
                {
                    found1 = true;

                    MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to remove " + item.leavdesc.Trim().ToLower(), WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                    if (msgresult != MessageBoxResult.Yes)
                        return;

                    this.LeaveAppList1.RemoveAt(fidx1);
                    break;
                }
            }

            if (!found1 && this.LeaveAppList1.Count < 3) // Maximum 3 times allows. Can be increased
            {
                this.LeaveAppList1.Add(new vmEntryAttnLeav1.LeaveAppDetails
                {
                    leavid = leaveid1, // "00"
                    leavidx = "00",
                    leavdesc = leaveDesc1,
                    leavcod = leaveCod1,
                    begndat = DateTime.Today,
                    enddat = DateTime.Today,
                    begnstat = "1st",
                    endstat = "2nd",
                    halfday = 0.0m,
                    totday = 1.0m
                });
            }

            int slNum1 = 1;
            foreach (var item in this.LeaveAppList1)
            {
                item.leavidx = slNum1.ToString("00");
                slNum1++;
            }
            this.btnLeaveRecal_Click(null, null);
            this.DgvLeavApp.ItemsSource = this.LeaveAppList1;
            this.DgvLeavApp.Items.Refresh();
        }


        private void autoLeavHcCod2_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStuffNameDesg(args.Pattern);
        }

        private ObservableCollection<vmReportHCM1.Stafflist> GetStuffNameDesg(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<vmReportHCM1.Stafflist>(
                this.StaffList1.Where((x, match) => (x.hccode + x.hcname).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoLeavForwardBy_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStuffNameDesg(args.Pattern);
        }

        private void autoLeavApprovBy_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStuffNameDesg(args.Pattern);
        }

        private void btnEditLeave_Click(object sender, RoutedEventArgs e)
        {
            if (this.DgvLeavDetails.SelectedItem == null)
                return;

            string leavid3 = ((HmsEntityManpower.HcmLeaveDetailsReport01)this.DgvLeavDetails.SelectedItem).leavid;// "0000000";

            if (leavid3 == "00")
                return;

            var LeaveDetails3 = this.PrevLeaveDetails1.FindAll(x => x.leavid == leavid3 && x.leavcod != "000000000000").OrderBy(x => x.leavidx);
            var leavm3 = this.PrevLeaveDetails1.FindAll(x => x.leavid == leavid3 && x.leavcod == "000000000000");
            this.lblLeavID.Content = leavid3;
            this.xctk_dtpApplyDate.Value = DateTime.Parse(leavm3[0].submitdat1);
            this.xctk_dtpApprovDate.Value = DateTime.Parse(leavm3[0].aprvdat1);

            this.txtLevresn.Text = leavm3[0].lreason.Trim();
            this.txttimecon.Text = leavm3[0].lcontact.Trim();
            this.txtnotes.Text = leavm3[0].leavnote.Trim();
            this.autoLeavHcCod2.ItemsSource = this.StaffList1;
            this.autoLeavForwardBy.ItemsSource = this.StaffList1;
            this.autoLeavApprovBy.ItemsSource = this.StaffList1;
            this.autoLeavHcCod2.SelectedValue = leavm3[0].hccode2;
            this.autoLeavForwardBy.SelectedValue = leavm3[0].forwrdbyid;
            this.autoLeavApprovBy.SelectedValue = leavm3[0].approvbyid;

            this.LeaveAppList1.Clear();
            foreach (var item in LeaveDetails3)
            {
                this.LeaveAppList1.Add(new vmEntryAttnLeav1.LeaveAppDetails
                {
                    leavid = item.leavid,
                    leavidx = item.leavidx,
                    leavdesc = this.LeaveSumList1.Find(x => x.leavcod == item.leavcod).leavdesc,
                    leavcod = item.leavcod,
                    begndat = item.begndat,
                    enddat = item.enddat,
                    begnstat = (item.begnstat == "1" ? "1st" : "2nd"), // "1st",
                    endstat = (item.endstat == "1" ? "1st" : "2nd"), // "2nd",
                    halfday = 0.0m,
                    totday = 1.0m
                });
            }
            this.lblEditMode.Tag = leavid3;
            this.btnLeaveRecal_Click(null, null);
            this.DgvLeavApp.ItemsSource = this.LeaveAppList1;
            this.DgvLeavApp.Items.Refresh();
            this.lblEditMode.Visibility = Visibility.Visible;
        }

        private void DgvLeavApp_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.DgvLeavApp.ContextMenu.IsOpen = true;
        }

        private void stkpApproval_GotFocus(object sender, RoutedEventArgs e)
        {
            this.btnLeaveRecal_Click(null, null);
        }

    }
}
