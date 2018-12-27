using ASITHmsEntity;
using ASITHmsViewMan.Manpower;
using ASITHmsRpt3Manpower;
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
using System.Collections;
using Microsoft.Reporting.WinForms;

namespace ASITHmsWpf.Manpower
{
    /// <summary>
    /// Interaction logic for frmEntryAttn101.xaml
    /// </summary>
    public partial class frmEntryAttn102 : UserControl
    {

        public List<HmsEntityManpower.HcmDayWiseAttanReport> ListDayWiseAttnRpt = new List<HmsEntityManpower.HcmDayWiseAttanReport>();
        private vmReportHCM1 vmr1 = new vmReportHCM1();
        private vmEntryHRGenral1 vm1 = new vmEntryHRGenral1();
        private vmEntryAttnLeav1 vm2 = new vmEntryAttnLeav1();

        private bool FrmInitialized = false;

        public frmEntryAttn102()
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

                var brnlist = WpfProcessAccess.CompInfList[0].BranchList.FindAll(d => d.brncod.Substring(2, 2) != "00");
                foreach (var itembrn in brnlist)
                {
                    this.cmbSectCodAll.Items.Add(new ComboBoxItem() { Content = itembrn.brnnam, Tag = itembrn.brncod });

                }
                var deptlist = WpfProcessAccess.CompInfList[0].SectionList.FindAll(d => d.sectcod.Substring(9, 3) == "000" && d.sectcod.Substring(7, 5) != "00000");
                foreach (var itemdpt in deptlist)
                {
                    this.cmbSectCodAll.Items.Add(new ComboBoxItem() { Content = itemdpt.sectname, Tag = itemdpt.sectcod });
                }

                var sectlist = WpfProcessAccess.CompInfList[0].SectionList.FindAll(d => d.sectcod.Substring(9, 3) != "000");
                foreach (var itemsect in sectlist)
                {
                    this.cmbSectCodAll.Items.Add(new ComboBoxItem() { Content = itemsect.sectname, Tag = itemsect.sectcod });
                }
                this.cmbSectCodAll.SelectedIndex = 0;

                this.stkpMacAtt.Visibility = Visibility.Hidden;
                for (int i = -12; i < 12; i++)
                {
                    this.cmbInfoMonth.Items.Add(new ComboBoxItem() { Content = DateTime.Today.AddMonths(i).ToString("MMMM, yyyy"), Tag = DateTime.Today.AddMonths(i).ToString("yyyyMM") });
                }
                this.cmbInfoMonth.SelectedIndex = 12;
                this.xctk_InfoDate.Value = DateTime.Today;

                var pap = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, "%", "EXISTSTAFFS");
                DataSet ds = WpfProcessAccess.GetHmsDataSet(pap);
                var tmpStaffList = ds.Tables[0].DataTableToList<vmReportHCM1.Stafflist>();
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
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAttn102_btnUpdateMacData") == null)
                {
                    this.btnUpdateMacData.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCH-Attn-102-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void conMenuHCMAtnAll_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtEmpAll.Text = ((MenuItem)sender).Header.ToString().Trim();
        }

        private void cmbSectCodAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cmbSectCodAll.ToolTip = ((ComboBoxItem)this.cmbSectCodAll.SelectedItem).Content.ToString().Trim();
        }

        private void cmbInfoMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbInfoMonth.SelectedIndex < 0)
                return;

            this.xctk_InfoDate.Value = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim());         
        }

        private void btnShowInfo_Click(object sender, RoutedEventArgs e)
        {
            this.stkpMain.IsEnabled = true;
            this.stkpMacAtt.Visibility = Visibility.Hidden;
            if (this.btnShowInfo.Content.ToString() == "Next")
            {
                this.btnShowInfo.Content = "Show";
                return;
            }
            if (!this.ShowRequiredInfo())
                return;

            this.stkpMain.IsEnabled = false;
            this.stkpMacAtt.Visibility = Visibility.Visible;

            this.btnShowInfo.Content = "Next";
        }
        private bool ShowRequiredInfo()
        {
            this.ShowMachineAttendance();
         
            return true;
        }

          private void AtxtEmpAll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtEmpAll.ContextMenu.IsOpen = true;
        }

        private void btnUpdateMacData_Click(object sender, RoutedEventArgs e)
        {
            DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim());
            DateTime Date2 = Date1.AddMonths(1).AddDays(-1);
            string monthid1 = Date1.ToString("yyyyMM");
            var pap1 = vm2.SetParamUpdateMachineAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, Date1.ToString("dd-MMM-yyyy"), Date2.ToString("dd-MMM-yyyy"), "%");
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (this.btnShowInfo.Content.ToString() == "Show")
                    this.btnShowInfo_Click(null, null);
                else
                    this.ShowMachineAttendance();
            }
        }



        private void btnPrintAttSchdlDay_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListDayWiseAttnRpt == null || this.ListDayWiseAttnRpt.Count == 0)
                this.ShowMachineAttendance();

            Hashtable rptParam = new Hashtable();


            rptParam["ReportDate"] = this.xctk_InfoDate.Text;
            //rptParam["empId"] = this.txtblAttnSchempid.Text.ToString();
            //rptParam["empName"] = AtxtEmpAll.Text.ToString();
            //rptParam["slMnth"] = ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString();
            //var list3 = WpfProcessAccess.GetRptGenInfo();


            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            LocalReport rpt1 = HcmReportSetup.GetLocalReport("Payroll.RptDailyAttn01", this.ListDayWiseAttnRpt, rptParam, list3);
            if (rpt1 == null)
                return;
            this.ShowReportWindow(rpt1, "Daily Attendence Schedule Report", false);
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
        private void ShowMachineAttendance()
        {
            //this.cmbInfoMonth.SelectedItem
            DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim());
            //DateTime Date2 = Date1.AddMonths(1).AddDays(-1);
            string monthid1 = Date1.ToString("yyyyMM");
            string AttnDate1 = (chkMacAttnDate.IsChecked == true ? "%" : this.xctk_InfoDate.Text.Substring(0, 11));
            string hcDept1 = ((ComboBoxItem)this.cmbSectCodAll.SelectedItem).Tag.ToString();

            string hccode1 = this.AtxtEmpAll.Value + "%";
            var pap1 = vm2.SetParamShowActualAttnInfo1(WpfProcessAccess.CompInfList[0].comcpcod, monthid1, AttnDate1, AttnDate1, hccode1, hcDept1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.ListDayWiseAttnRpt = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmDayWiseAttanReport>();

            this.dgActTrmAttn.ItemsSource = this.ListDayWiseAttnRpt;
        }
    }
}
