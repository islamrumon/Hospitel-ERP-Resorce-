using ASITHmsEntity;
using ASITHmsViewMan.Manpower;
using ASITFunLib;
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
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Collections;
using ASITHmsRpt3Manpower;
using System.Windows.Threading;

namespace ASITHmsWpf.Manpower
{
    /// <summary>
    /// Interaction logic for frmEntryAttn105.xaml
    /// </summary>
    public partial class frmEntryAttn101 : UserControl
    {
        // TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
        // TimeSpan duration = new TimeSpan(endtime.Ticks - startTime.Ticks);

        //public List<HmsEntityManpower.HcmDayWiseAttanReport> ListDayWiseAttnRpt = new List<HmsEntityManpower.HcmDayWiseAttanReport>();
        public List<HmsEntityManpower.RptAttnSchInfo> ListSchAttn1 = new List<HmsEntityManpower.RptAttnSchInfo>();
        public List<vmEntryHRGenral1.HcmStdAttnSch1> ListGenAttnSch1 = new List<vmEntryHRGenral1.HcmStdAttnSch1>();
        private DispatcherFrame frameApprovalEntry;


        private vmReportHCM1 vmr1 = new vmReportHCM1();
        private vmEntryHRGenral1 vm1 = new vmEntryHRGenral1();
        private vmEntryAttnLeav1 vm2 = new vmEntryAttnLeav1();
        private CheckBox[] ScheduleDayChecks = new CheckBox[32];
        public byte[] bytes12;

        private bool FrmInitialized = false;
        public frmEntryAttn101()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }
        private void conMenuHCMAtnAll_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtEmpAll.Text = ((MenuItem)sender).Header.ToString().Trim();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;
                this.ActivateAuthObjects();
                this.stkpAttShdl.Visibility = Visibility.Hidden;
                this.btnUpdateInfo.Visibility = Visibility.Hidden;
                this.GridApprovalEntry.Visibility = Visibility.Collapsed;
                for (int i = -12; i < 12; i++)
                {
                    this.cmbInfoMonth.Items.Add(new ComboBoxItem() { Content = DateTime.Today.AddMonths(i).ToString("MMMM, yyyy"), Tag = DateTime.Today.AddMonths(i).ToString("yyyyMM") });
                }
                this.cmbInfoMonth.SelectedIndex = 12;

                var pap1 = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, "%", "EXISTSTAFFS");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var tmpStaffList = ds1.Tables[0].DataTableToList<vmReportHCM1.Stafflist>();
                this.AtxtEmpAll.AutoSuggestionList.Clear();

                foreach (var item1 in tmpStaffList)
                {
                    //this.AtxtEmpAll.AddSuggstionItem(item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
                    this.AtxtEmpAll.AddSuggstionItem(item1.hccode.Trim().Substring(6) + " - " + item1.hcname.Trim() + ", " + item1.designame.Trim(), item1.hccode.Trim());
                    //var mitm1 = new MenuItem() { Header = item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), Tag = item1.sircode.Trim() };
                    var mitm1 = new MenuItem() { Header = item1.hccode.Trim().Substring(6) + " - " + item1.hcname.Trim() + ", " + item1.designame.Trim(), Tag = item1.hccode.Trim() };
                    mitm1.Click += conMenuHCMAtnAll_MouseClick;
                    this.conMenuHCMAtnAll.Items.Add(mitm1);
                }
            }
        }

        private void ActivateAuthObjects()
        {
            try
            {


                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAttn101_btnRemoveInfo") == null)
                {
                    this.btnRemoveInfo.Visibility = Visibility.Hidden;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAttn101_btnUpdateInfo") == null)
                {
                    this.btnUpdateInfo.Visibility = Visibility.Hidden;
                    this.btnUpdateInfo.IsEnabled = false;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAttn101_btnSetAttnDayOffSch") == null)
                {
                    this.btnSetAttnDayOffSch.Visibility = Visibility.Hidden;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAttn101_btnPrintAttSchdl") == null)
                {
                    this.btnPrintAttSchdl.Visibility = Visibility.Hidden;
                    this.chkPrintActAttnWithSch.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCH-Attn-101-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void AtxtEmpAll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtEmpAll.ContextMenu.IsOpen = true;
        }
        private void btnShowInfo_Click(object sender, RoutedEventArgs e)
        {
            this.chkPrintActAttnWithSch.IsChecked = false;
            this.btnUpdateInfo.Visibility = Visibility.Hidden;
            this.stkpMain.IsEnabled = true;
            this.stkpAttShdl.Visibility = Visibility.Hidden;
            this.chkSchDayOff.IsChecked = true;
            //this.imgAttnSchEmpimg.Source = this.imgUserPhoto.Source;
            this.bytes12 = null;

            if (this.btnShowInfo.Content.ToString() == "Next")
            {
                this.btnShowInfo.Content = "Show";
                return;
            }
            if (!this.ShowRequiredInfo())
                return;

            this.stkpMain.IsEnabled = false;
            if (this.btnUpdateInfo.IsEnabled == true)
                this.btnUpdateInfo.Visibility = Visibility.Visible;

            this.stkpAttShdl.Visibility = Visibility.Visible;

            this.btnShowInfo.Content = "Next";
        }
        private bool ShowRequiredInfo()
        {
            string empID = this.AtxtEmpAll.Value.Trim();
            if (this.stkpMain.Visibility == Visibility.Visible && this.AtxtEmpAll.Text.Trim().Length == 0 && empID.Length == 0)
                return false;

            this.ShowScheduleAttendance();

            return true;
        }
        private void ShowScheduleAttendance()
        {
            this.ScheduleDateInitialized();
            this.cmbAssignOption.SelectedIndex = 0;
            DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim());
            string monthid1 = Date1.ToString("yyyyMM");
            string hccode1 = this.AtxtEmpAll.Value;
            string hcname1 = this.AtxtEmpAll.Text;
            this.dgAttnSch.ItemsSource = null;
            this.ListSchAttn1.Clear();
            this.ListGenAttnSch1.Clear();
            var pap1 = vm2.SetParamShowScheduledAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, hccode1, "ENTRY");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.ListSchAttn1 = ds1.Tables[0].DataTableToList<HmsEntityManpower.RptAttnSchInfo>();
            this.ListGenAttnSch1 = ds1.Tables[1].DataTableToList<vmEntryHRGenral1.HcmStdAttnSch1>();
            foreach (var item in this.ListGenAttnSch1)
            {
                string[] daydat1 = item.attndata.Split('|');
                foreach (var daydat2 in daydat1)
                {
                    if (daydat2.Length > 0)
                    {
                        switch (daydat2.Substring(0, 3))
                        {
                            case "FRI": item.attnfri = daydat2.Substring(4).Trim(); break;
                            case "SAT": item.attnsat = daydat2.Substring(4).Trim(); break;
                            case "SUN": item.attnsun = daydat2.Substring(4).Trim(); break;
                            case "MON": item.attnmon = daydat2.Substring(4).Trim(); break;
                            case "TUE": item.attntue = daydat2.Substring(4).Trim(); break;
                            case "WED": item.attnwed = daydat2.Substring(4).Trim(); break;
                            case "THU": item.attnthu = daydat2.Substring(4).Trim(); break;
                        }
                    }
                }
            }
            if (this.ListGenAttnSch1.Count > 0)
            {
                var lo1 = this.ListGenAttnSch1.Find(x => x.attncod == "SIHA00102002");
                foreach (var item in this.ListGenAttnSch1)
                {
                    if (item.attncod == "SIHA00101002" || item.attncod == "SIHA00102001")
                    {
                        item.attnfri = (item.attnfri.Length == 0 ? lo1.attnfri : item.attnfri);
                        item.attnsat = (item.attnsat.Length == 0 ? lo1.attnsat : item.attnsat);
                        item.attnsun = (item.attnsun.Length == 0 ? lo1.attnsun : item.attnsun);
                        item.attnmon = (item.attnmon.Length == 0 ? lo1.attnmon : item.attnmon);
                        item.attntue = (item.attntue.Length == 0 ? lo1.attntue : item.attntue);
                        item.attnwed = (item.attnwed.Length == 0 ? lo1.attnwed : item.attnwed);
                        item.attnthu = (item.attnthu.Length == 0 ? lo1.attnthu : item.attnthu);
                    }
                }
            }
            int maxDay = this.ListSchAttn1[this.ListSchAttn1.Count - 1].attndate.Day; // Date2.Day; // 
            this.iudSchDayFrom.Maximum = maxDay;
            this.iudSchDayTo.Maximum = maxDay;

            this.iudSchDayFrom.Value = 1;
            this.iudSchDayTo.Value = maxDay;
            this.dgAttnSch.ItemsSource = this.ListSchAttn1;

            /*
                SIHA00101001    FIRST IN-TIME (DAY START)
                SIHA00101002    SECOND IN-TIME (BREAK END)
                SIHA00102001    FIRST OUT-TIME (BREAK START)
                SIHA00102002    SECOND OUT-TIME (DAY END)
             */
        }

        private void ScheduleDateInitialized()
        {
            this.dtpAttnSchIn1s.Value = DateTime.Parse("08:00 AM");
            this.dtpAttnSchOut1s.Value = DateTime.Parse("12:00 PM");
            this.dtpAttnSchIn2s.Value = DateTime.Parse("05:00 PM");
            this.dtpAttnSchOut2s.Value = DateTime.Parse("11:00 PM");

            DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim());
            DateTime Date2 = Date1.AddMonths(1).AddDays(-1);
            this.ScheduleDayChecks = ScheduleDayChecks1();
            foreach (var item in this.ScheduleDayChecks)
            {
                item.Visibility = Visibility.Hidden;
                item.IsChecked = false;
            }
            for (int i = 1; i <= Date2.Day; i++)
            {
                this.ScheduleDayChecks[i].Content = i.ToString("00 ") + DateTime.Parse(i.ToString() + Date1.ToString("-MMM-yyyy")).ToString("ddd").Substring(0, 2);
                this.ScheduleDayChecks[i].Visibility = Visibility.Visible;
            }
        }
        private CheckBox[] ScheduleDayChecks1()
        {
            CheckBox[] ScheduleDayChecks2 = {new CheckBox(), this.chkDay01, this.chkDay02, this.chkDay03, this.chkDay04, this.chkDay05, this.chkDay06, this.chkDay07, this.chkDay08, this.chkDay09, this.chkDay10, 
                                          this.chkDay11, this.chkDay12, this.chkDay13, this.chkDay14, this.chkDay15, this.chkDay16, this.chkDay17, this.chkDay18, this.chkDay19, this.chkDay20, 
                                          this.chkDay21, this.chkDay22, this.chkDay23, this.chkDay24, this.chkDay25, this.chkDay26, this.chkDay27, this.chkDay28, this.chkDay29, this.chkDay30, this.chkDay31 };
            return ScheduleDayChecks2;
        }
        private void UpdateScheduleAttendance()
        {
            foreach (var item in this.ListSchAttn1)
            {
                //if (item.attnstatid == "SIHA00501001" || item.attnstatid == "SIHA00501005" || item.attnstatid == "SIHA00501006" || item.attnstatid == "SIHA00501007" || item.attnstatid == "SIHA00501008")
                if (item.attnstatid.Substring(0, 9) == "SIHA00501")
                {
                    item.attnhour = ((item.outtime2.Subtract(item.intime1).TotalMinutes - item.intime2.Subtract(item.outtime1).TotalMinutes) / 60.00).ToString("##.0") + " hrs";
                    item.schworkhr = Convert.ToDecimal((item.outtime2.Subtract(item.intime1).TotalMinutes - item.intime2.Subtract(item.outtime1).TotalMinutes) / 60.00);
                }
            }
            this.dgAttnSch.Items.Refresh();

            DataSet ds1 = vm2.GetDataSetForUpdateScheduleAttendance01(this.ListSchAttn1);
            DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim());
            string monthid1 = Date1.ToString("yyyyMM");
            string hccode1 = this.AtxtEmpAll.Value;
            string NewEdit = this.ListSchAttn1[0].newedit.Trim();
            var pap1 = vm2.SetParamForUpdateScheduleAttendance01(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, hccode1, ds1, NewEdit);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            System.Windows.MessageBox.Show("Attendance Schedule Information Updated Successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }


        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateScheduleAttendance();
        }

        private void btnPrintAttSchdl_Click(object sender, RoutedEventArgs e)
        {
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
            string RptType = "Roster";
            if (this.chkPrintActAttnWithSch.IsChecked == true)
            {
                this.chkPrintActAttnWithSch.IsChecked = false;
                RptType = "Attendance";
            }

            List<HmsEntityManpower.RptAttnSchInfo> Rptlst = HcmGeneralClass1.GetIndRosterAttendance(monthid1: monthid1, hccode1a: hccode1a, RptType: RptType);

            if (Rptlst == null)
            {
                System.Windows.MessageBox.Show("Attendance Report not Generated for " + monthName1 + "\nPlease try again later. Thank you.", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                        MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            var pap1 = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, hccode1a, "EXISTSTAFFS");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var Staff1 = ds1.Tables[0].DataTableToList<vmReportHCM1.Stafflist>();

            decimal sumLate1 = Rptlst.Sum(x => x.confirmlate);
            string Notes1 = (sumLate1 > 0 ? "Late Point = " + sumLate1.ToString("##") : "");

            decimal sumEout1 = Rptlst.Sum(x => x.confirmearly);
            Notes1 = Notes1 + (Notes1.Length > 0 && sumEout1 > 0 ? ", " : "") + (sumEout1 > 0 ? "Early Out Point = " + sumEout1.ToString("##") : "");
            Notes1 = (Notes1.Length > 0 ? "Confirm " : "") + Notes1;

            var pap2 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hccode1a, "PHOTO");
            DataSet dss2 = WpfProcessAccess.GetHmsDataSet(pap2);
            if (dss2 == null)
                return;

            if (!(dss2.Tables[0].Rows[0]["hcphoto"] is DBNull))
            {
                this.bytes12 = (byte[])dss2.Tables[0].Rows[0]["hcphoto"];
            }

            Hashtable rptParam = new Hashtable();
            rptParam["Comlogo"] = (this.bytes12 == null ? "" : Convert.ToBase64String(this.bytes12));
            rptParam["empId"] = this.AtxtEmpAll.Value.ToString().Trim();
            rptParam["empName"] = "Employee : " + hccode1a.Substring(6, 6) + " - " + Staff1[0].hcname.Trim() + ", " + Staff1[0].designame.Trim(); //& AtxtEmpAll.Text.ToString();
            rptParam["slMnth"] = (RptType == "Attendance" ? "Monthly Attendence" : "Duty Roster") + " - " + monthName1;
            rptParam["ParmNotes1"] = Notes1;
            rptParam["ParmBrnDept1"] = "Department : " + Staff1[0].deptname.Trim() +", Joining Date : " + Staff1[0].joindat.Trim() +
                                       ", Reporting Date : " + Convert.ToDateTime(ds1r.Tables[2].Rows[0]["ServerTime"]).ToString("dd-MMM-yyyy hh:mm tt"); 
            
            //var list3 = WpfProcessAccess.GetRptGenInfo();
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1r.Tables[2].Rows[0]["ServerTime"]));
            LocalReport rpt1 = HcmReportSetup.GetLocalReport("Payroll.RptAttenSchedule01", Rptlst, rptParam, list3);
            if (rpt1 == null)
                return;

            this.ShowReportWindow(rpt1, "Attendence Schedule Report", false);
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

        private void btnSetAttnDayOffSch_Click(object sender, RoutedEventArgs e)
        {
            if (ListSchAttn1.Count == 0)
                return;

            if (System.Windows.MessageBox.Show("Confirm Change Schedule", WpfProcessAccess.AppTitle, MessageBoxButton.OKCancel,
               MessageBoxImage.Question, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.OK)
                return;

            /*
                ACTCODE	ACTDESC
                SIHA00501001	PRECFECTLY PRESENT
                SIHA00501002	PRESENT BUT LATE
                SIHA00501003	PRESENT AND EARLY LEAVE
                SIHA00501004	PRESENT, LATE AND EARLY LEAVE
                SIHA00501005	PRESENT FOR HALF OVER-TIME DUTY
                SIHA00501006	PRESENT FOR OVER-TIME DUTY
                SIHA00501007	PRESENT OUTDOOR DUTY
                SIHA00501008	PRESENT AND HALF LEAVE
             
                SIHA00502001	ABSULUATELY ABSENT
                SIHA00502002	ABSENT DUE TO DAY-OFF
                SIHA00502003	ABSENT DUE TO LEAVE
             */

            string AssignOption1 = ((ComboBoxItem)this.cmbAssignOption.SelectedItem).Tag.ToString().Trim();

            switch (AssignOption1)
            {
                case "ASGENERAL":
                    this.AssignAsGeneralSchedule();
                    break;
                case "ASMARKED":
                    this.AssignAsMarkedSchedule();
                    break;
                case "ASRANGE":
                    this.AssignAsAssignedRangeSchedule();
                    break;
                case "FIRSTWEEK":
                    this.AssignLikeThisMonthFirstWeek();
                    break;
                case "LASTMONTHFW":
                    this.AssignLikeLastMonthFirstWeek();
                    break;
                case "LASTMONTHLW":
                    this.AssignLikeLastMonthLastWeek();
                    break;
                case "SATURDAY":
                case "SUNDAY":
                case "MONDAY":
                case "TUESDAY":
                case "WEDNESDAY":
                case "THURSDAY":
                case "FRIDAY":
                    this.AssignSpecificDaySchedule(AssignOption1);
                    break;
            }
            this.dgAttnSch.Items.Refresh();
        }

        private void AssignAsGeneralSchedule()
        {
            if (this.ListGenAttnSch1.Count() == 0)
                return;
            
            this.chkSchDayOff.IsChecked = false;

            var of1 = this.ListGenAttnSch1[0];
            string OffDay = (of1.attnfri.Contains("OFF") ? "Fri" : (of1.attnsat.Contains("OFF") ? "Sat" : (of1.attnsun.Contains("OFF") ? "Sun" : (of1.attnmon.Contains("OFF") ? "Mon" :
                            (of1.attntue.Contains("OFF") ? "Tue" : (of1.attnwed.Contains("OFF") ? "Wed" : (of1.attnthu.Contains("OFF") ? "Thu" : "Unk")))))));
                            // ((ComboBoxItem)cmbSchDayOff.SelectedItem).Content.ToString().Substring(0, 3);

            var InTime1 = this.ListGenAttnSch1.Find(x => x.attncod == "SIHA00101001");
            var OutTime1 = this.ListGenAttnSch1.Find(x => x.attncod == "SIHA00102001");
            var InTime2 = this.ListGenAttnSch1.Find(x => x.attncod == "SIHA00101002");
            var OutTime2 = this.ListGenAttnSch1.Find(x => x.attncod == "SIHA00102002");

            foreach (var item in this.ListSchAttn1)
            {
                string dt1 = item.attndate.ToString("dd-MMM-yyyy");
                if (item.attnstatid != "SIHA00502003")
                {
                    if (item.attndate.ToString("dd-MMM-yyyy ddd").Contains(OffDay))
                    {
                        item.intime1 = DateTime.Parse(dt1 + " 12:00 AM");
                        item.outtime1 = DateTime.Parse(dt1 + " 12:00 AM");
                        item.intime2 = DateTime.Parse(dt1 + " 12:00 AM");
                        item.outtime2 = DateTime.Parse(dt1 + " 12:00 AM");
                        item.attnstatid = "SIHA00502002";
                        item.attnrmrk = "Day off";
                        item.attnstat = "Absent (Day Off)";
                        item.visibletime = "Hidden";
                    }
                    else
                    {
                        string i1 = "", o1 = "", i2 = "", o2 = "";
                        switch (item.attndate.ToString("ddd").ToUpper())
                        {
                            case "FRI": i1 = InTime1.attnfri; o1 = OutTime1.attnfri; i2 = InTime2.attnfri; o2 = OutTime2.attnfri; break;
                            case "SAT": i1 = InTime1.attnsat; o1 = OutTime1.attnsat; i2 = InTime2.attnsat; o2 = OutTime2.attnsat; break;
                            case "SUN": i1 = InTime1.attnsun; o1 = OutTime1.attnsun; i2 = InTime2.attnsun; o2 = OutTime2.attnsun; break;
                            case "MON": i1 = InTime1.attnmon; o1 = OutTime1.attnmon; i2 = InTime2.attnmon; o2 = OutTime2.attnmon; break;
                            case "TUE": i1 = InTime1.attntue; o1 = OutTime1.attntue; i2 = InTime2.attntue; o2 = OutTime2.attntue; break;
                            case "WED": i1 = InTime1.attnwed; o1 = OutTime1.attnwed; i2 = InTime2.attnwed; o2 = OutTime2.attnwed; break;
                            case "THU": i1 = InTime1.attnthu; o1 = OutTime1.attnthu; i2 = InTime2.attnthu; o2 = OutTime2.attnthu; break;
                        }
                        DateTime dateval1;
                        item.intime1 = (DateTime.TryParse(dt1 + " " + i1, out dateval1) ? DateTime.Parse(dt1 + " " + i1) : DateTime.Parse(dt1 + " 12:00 AM"));
                        item.outtime1 = (DateTime.TryParse(dt1 + " " + o1, out dateval1) ? DateTime.Parse(dt1 + " " + o1) : DateTime.Parse(dt1 + " 12:00 AM"));
                        item.intime2 = (DateTime.TryParse(dt1 + " " + i2, out dateval1) ? DateTime.Parse(dt1 + " " + i2) : DateTime.Parse(dt1 + " 12:00 AM"));
                        item.outtime2 = (o2.Contains("AM") ? (DateTime.TryParse(item.attndate.AddDays(1).ToString("dd-MMM-yyyy") + " " + o2, out dateval1) ? 
                                    DateTime.Parse(item.attndate.AddDays(1).ToString("dd-MMM-yyyy") + " " + o2) : DateTime.Parse(item.attndate.AddDays(1).ToString("dd-MMM-yyyy") + " 12:00 AM" )) : 
                                    (DateTime.TryParse(dt1 + " " + o2, out dateval1) ? DateTime.Parse(dt1 + " " + o2) : DateTime.Parse(dt1 + " 12:00 AM")));
                        item.attnstatid = "SIHA00501001";
                        item.attnhour = ((item.outtime2.Subtract(item.intime1).TotalMinutes - item.intime2.Subtract(item.outtime1).TotalMinutes) / 60.00).ToString("##.0") + " hrs";
                        item.attnrmrk = "";
                        item.attnstat = "Present";
                        item.visibletime = "Visible";
                    }
                }
            }
            //this.ListGenAttnSch1

            /*
                SIHA00101001    FIRST IN-TIME (DAY START)
                SIHA00101002    SECOND IN-TIME (BREAK END)
                SIHA00102001    FIRST OUT-TIME (BREAK START)
                SIHA00102002    SECOND OUT-TIME (DAY END)
            */
        }
        private void AssignAsMarkedSchedule()
        {
            // <ComboBoxItem Content="Assign as described above" Tag="ASRANGE" />

            string OffDay = ((ComboBoxItem)cmbSchDayOff.SelectedItem).Content.ToString().Substring(0, 3);
            string InTime1 = ((DateTime)this.dtpAttnSchIn1s.Value).ToString("hh:mm tt");
            string OutTime1 = ((DateTime)this.dtpAttnSchOut1s.Value).ToString("hh:mm tt");
            string InTime2 = ((DateTime)this.dtpAttnSchIn2s.Value).ToString("hh:mm tt");
            string OutTime2 = ((DateTime)this.dtpAttnSchOut2s.Value).ToString("hh:mm tt");
            //int dayStart = (int)this.iudSchDayFrom.Value;
            //int dayEnd = (int)this.iudSchDayTo.Value;

            // this.ListSchAttn1[x].attnstatid = (y.Contains("Leave") ? "SIHA00502003" : (y.Contains("Day Off") ? "SIHA00502002" : (y.Contains("Over-Time") ? "SIHA00501006" : "SIHA00501001")));
            foreach (var item in this.ListSchAttn1)
            {
                //if (item.attndate.Day >= dayStart && item.attndate.Day <= dayEnd)
                {
                    if (item.attnstatid != "SIHA00502003")
                    {
                        if (item.attndate.ToString("dd-MMM-yyyy ddd").Contains(OffDay) && this.chkSchDayOff.IsChecked == true)
                        {
                            item.intime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.outtime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.intime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.outtime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.attnstatid = "SIHA00502002";
                            item.attnrmrk = "Day off";
                            item.attnstat = "Absent (Day Off)";
                            item.visibletime = "Hidden";
                        }
                        else
                        {
                            int i = item.attndate.Day;
                            if (this.ScheduleDayChecks[i].IsChecked == true)
                            {
                                item.intime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + InTime1);
                                item.outtime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + OutTime1);
                                item.intime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + InTime2);
                                item.outtime2 = (OutTime2.Contains("AM") ? DateTime.Parse(item.attndate.AddDays(1).ToString("dd-MMM-yyyy") + " " + OutTime2) :
                                                 DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + OutTime2));
                                item.attnstatid = "SIHA00501001";
                                item.attnhour = ((item.outtime2.Subtract(item.intime1).TotalMinutes - item.intime2.Subtract(item.outtime1).TotalMinutes) / 60.00).ToString("##.0") + " hrs";
                                item.attnrmrk = "";
                                item.attnstat = "Present";
                                item.visibletime = "Visible";
                            }
                        }
                    }
                }
            }
        }

        private void AssignSpecificDaySchedule(string AssignOption1)
        {
            // <ComboBoxItem Content="Assign as described above" Tag="ASRANGE" />

            string OffDay = ((ComboBoxItem)cmbSchDayOff.SelectedItem).Content.ToString().Substring(0, 3);
            string InTime1 = ((DateTime)this.dtpAttnSchIn1s.Value).ToString("hh:mm tt");
            string OutTime1 = ((DateTime)this.dtpAttnSchOut1s.Value).ToString("hh:mm tt");
            string InTime2 = ((DateTime)this.dtpAttnSchIn2s.Value).ToString("hh:mm tt");
            string OutTime2 = ((DateTime)this.dtpAttnSchOut2s.Value).ToString("hh:mm tt");
            //int dayStart = (int)this.iudSchDayFrom.Value;
            //int dayEnd = (int)this.iudSchDayTo.Value;

            // this.ListSchAttn1[x].attnstatid = (y.Contains("Leave") ? "SIHA00502003" : (y.Contains("Day Off") ? "SIHA00502002" : "SIHA00501001"));
            foreach (var item in this.ListSchAttn1)
            {
                //if (item.attndate.Day >= dayStart && item.attndate.Day <= dayEnd)
                if (item.attndate.ToString("dddd").ToUpper().Trim() == AssignOption1)
                {
                    if (item.attnstatid != "SIHA00502003")
                    {
                        if (item.attndate.ToString("dd-MMM-yyyy ddd").Contains(OffDay) && this.chkSchDayOff.IsChecked == true)
                        {
                            item.intime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.outtime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.intime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.outtime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.attnstatid = "SIHA00502002";
                            item.attnrmrk = "Day off";
                            item.attnstat = "Absent (Day Off)";
                            item.visibletime = "Hidden";
                        }
                        else
                        {
                            item.intime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + InTime1);
                            item.outtime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + OutTime1);
                            item.intime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + InTime2);
                            item.outtime2 = (OutTime2.Contains("AM") ? DateTime.Parse(item.attndate.AddDays(1).ToString("dd-MMM-yyyy") + " " + OutTime2) :
                                             DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + OutTime2));
                            item.attnstatid = "SIHA00501001";
                            item.attnhour = ((item.outtime2.Subtract(item.intime1).TotalMinutes - item.intime2.Subtract(item.outtime1).TotalMinutes) / 60.00).ToString("##.0") + " hrs";
                            item.attnrmrk = "";
                            item.attnstat = "Present";
                            item.visibletime = "Visible";
                        }
                    }
                }
            }
        }

        private void AssignAsAssignedRangeSchedule()
        {
            // <ComboBoxItem Content="Assign as described above" Tag="ASRANGE" />

            string OffDay = ((ComboBoxItem)cmbSchDayOff.SelectedItem).Content.ToString().Substring(0, 3);
            string InTime1 = ((DateTime)this.dtpAttnSchIn1s.Value).ToString("hh:mm tt");
            string OutTime1 = ((DateTime)this.dtpAttnSchOut1s.Value).ToString("hh:mm tt");
            string InTime2 = ((DateTime)this.dtpAttnSchIn2s.Value).ToString("hh:mm tt");
            string OutTime2 = ((DateTime)this.dtpAttnSchOut2s.Value).ToString("hh:mm tt");
            int dayStart = (int)this.iudSchDayFrom.Value;
            int dayEnd = (int)this.iudSchDayTo.Value;

            // this.ListSchAttn1[x].attnstatid = (y.Contains("Leave") ? "SIHA00502003" : (y.Contains("Day Off") ? "SIHA00502002" : "SIHA00501001"));
            foreach (var item in this.ListSchAttn1)
            {
                if (item.attndate.Day >= dayStart && item.attndate.Day <= dayEnd)
                {
                    if (item.attnstatid != "SIHA00502003")
                    {
                        if (item.attndate.ToString("dd-MMM-yyyy ddd").Contains(OffDay) && this.chkSchDayOff.IsChecked == true)
                        {
                            item.intime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.outtime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.intime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.outtime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " 12:00 AM");
                            item.attnstatid = "SIHA00502002";
                            item.attnrmrk = "Day off";
                            item.attnstat = "Absent (Day Off)";
                            item.visibletime = "Hidden";
                        }
                        else
                        {
                            item.intime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + InTime1);
                            item.outtime1 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + OutTime1);
                            item.intime2 = DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + InTime2);
                            item.outtime2 = (OutTime2.Contains("AM") ? DateTime.Parse(item.attndate.AddDays(1).ToString("dd-MMM-yyyy") + " " + OutTime2) :
                                             DateTime.Parse(item.attndate.ToString("dd-MMM-yyyy") + " " + OutTime2));
                            item.attnstatid = "SIHA00501001";
                            item.attnhour = ((item.outtime2.Subtract(item.intime1).TotalMinutes - item.intime2.Subtract(item.outtime1).TotalMinutes) / 60.00).ToString("##.0") + " hrs";
                            item.attnrmrk = "";
                            item.attnstat = "Present";
                            item.visibletime = "Visible";
                        }
                    }
                }
            }
            this.dgAttnSch.Items.Refresh();
        }

        private void AssignLikeThisMonthFirstWeek()
        {
            // <ComboBoxItem Content="Assign like first week" Tag="FIRSTWEEK" />

            var ListSchAttn1w = this.ListSchAttn1.FindAll(x => x.attndate.Day <= 7);

            for (int i = 7; i < this.ListSchAttn1.Count; i++)
            {
                var item = ListSchAttn1w.Find(x => x.attndate.DayOfWeek == this.ListSchAttn1[i].attndate.DayOfWeek);
                if (item == null)
                    break;

                this.ListSchAttn1[i].intime1 = item.intime1;
                this.ListSchAttn1[i].outtime1 = item.outtime1;
                this.ListSchAttn1[i].intime2 = item.intime2;
                this.ListSchAttn1[i].outtime2 = item.outtime2;
                this.ListSchAttn1[i].attnstatid = item.attnstatid;
                this.ListSchAttn1[i].attnrmrk = item.attnrmrk;
                this.ListSchAttn1[i].attnstat = item.attnstat;
                this.ListSchAttn1[i].visibletime = item.visibletime;
                this.ListSchAttn1[i].attnhour = item.attnhour;
            }

            this.dgAttnSch.Items.Refresh();
        }
        private void AssignLikeLastMonthFirstWeek()
        {
            // <ComboBoxItem Content="Assign like last month (first week)" Tag="LASTMONTHFW" />

            DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim()).AddDays(-1);
            string monthid1 = Date1.ToString("yyyyMM");
            string hccode1 = this.AtxtEmpAll.Value;
            var pap1 = vm2.SetParamShowScheduledAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, hccode1, "ENTRY");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var ListSchAttn1p = ds1.Tables[0].DataTableToList<HmsEntityManpower.RptAttnSchInfo>();
            if (ListSchAttn1p[0].newedit == "New")
                return;

            var ListSchAttn1wp = ListSchAttn1p.FindAll(x => x.attndate.Day <= 7);
            for (int i = 0; i < this.ListSchAttn1.Count; i++)
            {
                var item = ListSchAttn1wp.Find(x => x.attndate.DayOfWeek == this.ListSchAttn1[i].attndate.DayOfWeek);
                if (item == null)
                    break;

                this.ListSchAttn1[i].intime1 = DateTime.Parse(this.ListSchAttn1[i].attndate.ToString("dd-MMM-yyyy") + " " + item.intime1.ToString("hh:mm tt")); // item.intime1
                this.ListSchAttn1[i].outtime1 = DateTime.Parse(this.ListSchAttn1[i].attndate.ToString("dd-MMM-yyyy") + " " + item.outtime1.ToString("hh:mm tt")); // item.outtime1;
                this.ListSchAttn1[i].intime2 = DateTime.Parse(this.ListSchAttn1[i].attndate.ToString("dd-MMM-yyyy") + " " + item.intime2.ToString("hh:mm tt")); // item.intime2;
                this.ListSchAttn1[i].outtime2 = DateTime.Parse(this.ListSchAttn1[i].attndate.ToString("dd-MMM-yyyy") + " " + item.outtime2.ToString("hh:mm tt")); // item.outtime2;
                this.ListSchAttn1[i].attnstatid = item.attnstatid;
                this.ListSchAttn1[i].attnrmrk = item.attnrmrk;
                this.ListSchAttn1[i].attnstat = item.attnstat;
                this.ListSchAttn1[i].visibletime = item.visibletime;
                this.ListSchAttn1[i].attnhour = item.attnhour;
            }

            this.dgAttnSch.Items.Refresh();
        }

        private void AssignLikeLastMonthLastWeek()
        {
            // <ComboBoxItem Content="Like last month (last week)" Tag="LASTMONTHLW" />

            DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim()).AddDays(-1);
            string monthid1 = Date1.ToString("yyyyMM");
            string hccode1 = this.AtxtEmpAll.Value;
            var pap1 = vm2.SetParamShowScheduledAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, hccode1, "ENTRY");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var ListSchAttn1p = ds1.Tables[0].DataTableToList<HmsEntityManpower.RptAttnSchInfo>();
            if (ListSchAttn1p[0].newedit == "New")
                return;

            //var ListSchAttn1wp = ListSchAttn1p.FindAll(x => x.attndate.Day <= 7);
            int Days1 = Date1.AddMonths(1).AddDays(-1).Day - 7;
            var ListSchAttn1wp = ListSchAttn1p.FindAll(x => x.attndate.Day > Days1);
            for (int i = 0; i < this.ListSchAttn1.Count; i++)
            {
                var item = ListSchAttn1wp.Find(x => x.attndate.DayOfWeek == this.ListSchAttn1[i].attndate.DayOfWeek);
                if (item == null)
                    break;

                this.ListSchAttn1[i].intime1 = DateTime.Parse(this.ListSchAttn1[i].attndate.ToString("dd-MMM-yyyy") + " " + item.intime1.ToString("hh:mm tt")); // item.intime1
                this.ListSchAttn1[i].outtime1 = DateTime.Parse(this.ListSchAttn1[i].attndate.ToString("dd-MMM-yyyy") + " " + item.outtime1.ToString("hh:mm tt")); // item.outtime1;
                this.ListSchAttn1[i].intime2 = DateTime.Parse(this.ListSchAttn1[i].attndate.ToString("dd-MMM-yyyy") + " " + item.intime2.ToString("hh:mm tt")); // item.intime2;
                this.ListSchAttn1[i].outtime2 = DateTime.Parse(this.ListSchAttn1[i].attndate.ToString("dd-MMM-yyyy") + " " + item.outtime2.ToString("hh:mm tt")); // item.outtime2;
                this.ListSchAttn1[i].attnstatid = item.attnstatid;
                this.ListSchAttn1[i].attnrmrk = item.attnrmrk;
                this.ListSchAttn1[i].attnstat = item.attnstat;
                this.ListSchAttn1[i].visibletime = item.visibletime;
                this.ListSchAttn1[i].attnhour = item.attnhour;
            }

            this.dgAttnSch.Items.Refresh();
        }
        private void btnResetChecks_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.ScheduleDayChecks)
                item.IsChecked = false;
        }

        private void cmbdgAttnSch_DropDownClosed(object sender, EventArgs e)
        {
            int x = this.dgAttnSch.SelectedIndex;
            if (x < 0)
                return;
            string y = ((ComboBox)sender).Text.Trim();
            //string id1 = ((ComboBoxItem)((ComboBox)sender).SelectedItem).Tag.ToString().Trim();
            //this.ListSchAttn1[x].attnstatid = id1;

            this.ListSchAttn1[x].attnstatid = (y.Contains("(On Leave)") ? "SIHA00502003" : (y.Contains("(Day Off)") ? "SIHA00502002" : (y.Contains("(Half-Over-Time)") ? "SIHA00501005" :
                 (y.Contains("(Over-Time)") ? "SIHA00501006" : (y.Contains("(Outdoor Duty)") ? "SIHA00501007" : (y.Contains("(Half-Leave)") ? "SIHA00501008" : "SIHA00501001"))))));

            if (!y.Contains("Present"))
            {
                this.ListSchAttn1[x].intime1 = this.ListSchAttn1[x].attndate;
                this.ListSchAttn1[x].intime2 = this.ListSchAttn1[x].attndate;
                this.ListSchAttn1[x].outtime1 = this.ListSchAttn1[x].attndate;
                this.ListSchAttn1[x].outtime2 = this.ListSchAttn1[x].attndate;
                this.ListSchAttn1[x].visibletime = "Hidden";
            }
            else
            {
                this.ListSchAttn1[x].intime1 = (this.ListSchAttn1[x].attndate == this.ListSchAttn1[x].intime1 ? DateTime.Parse(this.ListSchAttn1[x].attndate.ToString("dd-MMM-yyyy") + " 08:00 AM") : this.ListSchAttn1[x].intime1);
                this.ListSchAttn1[x].intime2 = (this.ListSchAttn1[x].attndate == this.ListSchAttn1[x].intime2 ? DateTime.Parse(this.ListSchAttn1[x].attndate.ToString("dd-MMM-yyyy") + " 05:00 PM") : this.ListSchAttn1[x].intime2);
                this.ListSchAttn1[x].outtime1 = (this.ListSchAttn1[x].attndate == this.ListSchAttn1[x].outtime1 ? DateTime.Parse(this.ListSchAttn1[x].attndate.ToString("dd-MMM-yyyy") + " 12:00 PM") : this.ListSchAttn1[x].outtime1);
                this.ListSchAttn1[x].outtime2 = (this.ListSchAttn1[x].attndate == this.ListSchAttn1[x].outtime2 ? DateTime.Parse(this.ListSchAttn1[x].attndate.ToString("dd-MMM-yyyy") + " 11:00 PM") : this.ListSchAttn1[x].outtime2);
                this.ListSchAttn1[x].visibletime = "Visible";
            }
            this.dgAttnSch.Items.Refresh();

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

        private void lbldgAttnSchDate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.frameApprovalEntry = new DispatcherFrame();
            this.GridShceduleEntry.IsEnabled = false;
            this.GridApprovalEntry.Visibility = Visibility.Visible;
            this.GetApprovalData(((Label)sender).Uid.ToString().Trim());
            System.Windows.Threading.Dispatcher.PushFrame(this.frameApprovalEntry);
            this.GridShceduleEntry.IsEnabled = true;
            this.GridApprovalEntry.Visibility = Visibility.Collapsed;
        }

        private void btnCloseApprovalEntry_Click(object sender, RoutedEventArgs e)
        {
            this.frameApprovalEntry.Continue = false;
        }

        private void GetApprovalData(string SchDate1)
        {
            // Code Goes from here
            this.lblApprovalDate.Content = SchDate1;
            DateTime SchDat = DateTime.Parse(SchDate1.Substring(0, 11));
            var sch1 = this.ListSchAttn1.FindAll(x => x.attndate == SchDat);
            string fResult = sch1[0].approvals.Trim();

            this.txtbtime1.Text = sch1[0].intime1.ToString("dd-MMM-yyyy hh:mm tt");
            this.txtbtime2.Text = sch1[0].outtime1.ToString("dd-MMM-yyyy hh:mm tt");
            this.txtbtime3.Text = sch1[0].intime2.ToString("dd-MMM-yyyy hh:mm tt");
            this.txtbtime4.Text = sch1[0].outtime2.ToString("dd-MMM-yyyy hh:mm tt");

            this.rbtnNot1.IsChecked = true;
            this.rbtnNot2.IsChecked = true;
            this.rbtnNot3.IsChecked = true;
            this.rbtnNot4.IsChecked = true;

            this.rbtnErr1.IsChecked = false;
            this.rbtnErr2.IsChecked = false;
            this.rbtnErr3.IsChecked = false;
            this.rbtnErr4.IsChecked = false;

            this.rbtnAio1.IsChecked = false;
            this.rbtnAio2.IsChecked = false;
            this.rbtnAio3.IsChecked = false;
            this.rbtnAio4.IsChecked = false;

            this.txtApr1.Text = "";
            this.txtApr2.Text = "";
            this.txtApr3.Text = "";
            this.txtApr4.Text = "";

            this.dtpApr1.Value = SchDat;
            this.dtpApr2.Value = SchDat;
            this.dtpApr3.Value = SchDat;
            this.dtpApr4.Value = SchDat;

            if (fResult.Length == 0)
                return;

            string[] results1 = fResult.Split('|');

            for (int i = 0; i < results1.Length; i++)
            {
                results1[i] = results1[i].Trim() + "  ";
                switch (results1[i].Substring(0, 2))
                {
                    case "1E":
                        this.rbtnNot1.IsChecked = false;
                        this.rbtnErr1.IsChecked = true;
                        this.rbtnAio1.IsChecked = false;
                        this.dtpApr1.Value = DateTime.Parse(results1[i].Substring(2, 11));
                        this.txtApr1.Text = results1[i].Substring(13).Trim();
                        break;
                    case "1L":
                        this.rbtnNot1.IsChecked = false;
                        this.rbtnErr1.IsChecked = false;
                        this.rbtnAio1.IsChecked = true;
                        this.dtpApr1.Value = DateTime.Parse(results1[i].Substring(2, 11));
                        this.txtApr1.Text = results1[i].Substring(13).Trim();
                        break;
                    case "2E":
                        this.rbtnNot2.IsChecked = false;
                        this.rbtnErr2.IsChecked = true;
                        this.rbtnAio2.IsChecked = false;
                        this.dtpApr2.Value = DateTime.Parse(results1[i].Substring(2, 11));
                        this.txtApr2.Text = results1[i].Substring(13).Trim();
                        break;
                    case "2O":
                        this.rbtnNot2.IsChecked = false;
                        this.rbtnErr2.IsChecked = false;
                        this.rbtnAio2.IsChecked = true;
                        this.dtpApr2.Value = DateTime.Parse(results1[i].Substring(2, 11));
                        this.txtApr2.Text = results1[i].Substring(13).Trim();
                        break;
                    case "3E":
                        this.rbtnNot3.IsChecked = false;
                        this.rbtnErr3.IsChecked = true;
                        this.rbtnAio3.IsChecked = false;
                        this.dtpApr3.Value = DateTime.Parse(results1[i].Substring(2, 11));
                        this.txtApr3.Text = results1[i].Substring(13).Trim();
                        break;
                    case "3L":
                        this.rbtnNot3.IsChecked = false;
                        this.rbtnErr3.IsChecked = false;
                        this.rbtnAio3.IsChecked = true;
                        this.dtpApr3.Value = DateTime.Parse(results1[i].Substring(2, 11));
                        this.txtApr3.Text = results1[i].Substring(13).Trim();
                        break;
                    case "4E":
                        this.rbtnNot4.IsChecked = false;
                        this.rbtnErr4.IsChecked = true;
                        this.rbtnAio4.IsChecked = false;
                        this.dtpApr4.Value = DateTime.Parse(results1[i].Substring(2, 11));
                        this.txtApr4.Text = results1[i].Substring(13).Trim();
                        break;
                    case "4O":
                        this.rbtnNot4.IsChecked = false;
                        this.rbtnErr4.IsChecked = false;
                        this.rbtnAio4.IsChecked = true;
                        this.dtpApr4.Value = DateTime.Parse(results1[i].Substring(2, 11));
                        this.txtApr4.Text = results1[i].Substring(13).Trim();
                        break;
                }
            }

        }
        private void btnUpdateApp_Click(object sender, RoutedEventArgs e)
        {
            string Result1 = (this.rbtnErr1.IsChecked == true ? "E" : this.rbtnAio1.IsChecked == true ? "L" : "");
            Result1 = (Result1.Length == 0 ? "" : "1" + Result1 + this.dtpApr1.Text.Trim() + this.txtApr1.Text.Trim());

            string Result2 = (this.rbtnErr2.IsChecked == true ? "E" : this.rbtnAio2.IsChecked == true ? "O" : "");
            Result2 = (Result2.Length == 0 ? "" : "2" + Result2 + this.dtpApr2.Text.Trim() + this.txtApr2.Text.Trim());

            string Result3 = (this.rbtnErr3.IsChecked == true ? "E" : this.rbtnAio3.IsChecked == true ? "L" : "");
            Result3 = (Result3.Length == 0 ? "" : "3" + Result3 + this.dtpApr3.Text.Trim() + this.txtApr3.Text.Trim());

            string Result4 = (this.rbtnErr4.IsChecked == true ? "E" : this.rbtnAio4.IsChecked == true ? "O" : "");
            Result4 = (Result4.Length == 0 ? "" : "4" + Result4 + this.dtpApr4.Text.Trim() + this.txtApr4.Text.Trim());

            string fResult = Result1 + (Result2.Length > 0 && Result2.Length > 0 ? " | " + Result2 : "");
            fResult = fResult + (fResult.Length > 0 && Result3.Length > 0 ? " | " + Result3 : Result3);
            fResult = fResult + (fResult.Length > 0 && Result4.Length > 0 ? " | " + Result4 : Result4);

            string SchDate1 = this.lblApprovalDate.Content.ToString().Trim();
            DateTime SchDat = DateTime.Parse(SchDate1.Substring(0, 11));
            var sch1 = this.ListSchAttn1.FindAll(x => x.attndate == SchDat);
            sch1[0].approvals = fResult;
            this.btnCloseApprovalEntry_Click(null, null);
            this.btnUpdateInfo_Click(null, null);
        }

        private void btnRemoveInfo_Click(object sender, RoutedEventArgs e)
        {
            string empID = this.AtxtEmpAll.Value.Trim();
            if (this.stkpMain.Visibility == Visibility.Visible && this.AtxtEmpAll.Text.Trim().Length == 0 && empID.Length == 0)
                return;

            if (System.Windows.MessageBox.Show("Are you sure to remove Attendance Schedule", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
               MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            DataSet ds1 = null;// vm2.GetDataSetForUpdateScheduleAttendance01(this.ListSchAttn1);
            DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim());
            string monthid1 = Date1.ToString("yyyyMM");
            string hccode1 = this.AtxtEmpAll.Value;
            string NewEdit = "REMOVE";
            var pap1 = vm2.SetParamForUpdateScheduleAttendance01(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, hccode1, ds1, NewEdit);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            System.Windows.MessageBox.Show("Attendance Schedule Information Removed Successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

        }




        /*

        private string ValidateSchTime(string Str1) // For validating Schedule Time
        {
            try
            {
                //return DateTime.Parse(Str1).ToString("dd-MMM-yyyy");
                return DateTime.Parse(Str1).ToString("hh:mm tt");
            }
            catch
            {
                return "";
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


        private void dgRecon2TxtRecDate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var wmt1 = (Xceed.Wpf.Toolkit.WatermarkTextBox)sender;
            string vounum1 = wmt1.Tag.ToString();
            var item1 = this.AccRecnList2.FindAll(x => x.vounum == vounum1);
            item1[0].recndat = (item1[0].recndat.Year == 1900 ? DateTime.Today : DateTime.Parse("01-Jan-1900"));
            wmt1.Text = (item1[0].recndat.Year == 1900 ? "" : item1[0].recndat.ToString("dd-MMM-yyyy"));
        }

        private void dgRecon2TxtRecDate_LostFocus(object sender, RoutedEventArgs e)
        {
            var wmt1 = (Xceed.Wpf.Toolkit.WatermarkTextBox)sender;
            string vounum1 = wmt1.Tag.ToString();
            string DateVal = wmt1.Text.Trim();
            var item1 = this.AccRecnList2.FindAll(x => x.vounum == vounum1);
            string wmt2 = this.ValidateReconDate(DateVal);
            wmt1.Text = wmt2;
            item1[0].recndat = DateTime.Parse(wmt2.Length == 0 ? "01-Jan-1900" : wmt2);
        }

        */
    }
}
