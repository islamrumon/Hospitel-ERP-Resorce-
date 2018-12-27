using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Manpower;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for frmEntryPayroll101.xaml
    /// </summary>
    public partial class frmEntryPayroll101 : UserControl
    {
        private bool FrmInitialized = false;
        string TitaleTag1, TitaleTag2;  // 

        private vmEntryPayroll1 vm1 = new vmEntryPayroll1();

        private List<HmsEntityManpower.Payslip001> SalaryInfoList = new List<HmsEntityManpower.Payslip001>();
        private List<HmsEntityManpower.Payslip001> OldSalaryInfoList = new List<HmsEntityManpower.Payslip001>();
        private bool UpdateMode = true;
        public frmEntryPayroll101()
        {
            InitializeComponent();
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
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryPayroll101_btnUpdateData") == null)
                {
                    this.btnUpdateData.Visibility = Visibility.Collapsed;
                    this.UpdateMode = false;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCH-Payroll-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void Objects_On_Init()
        {
            this.btnNextData.Visibility = Visibility.Collapsed;
            this.btnUpdateData.Visibility = Visibility.Collapsed;
            this.btnShowNewData.Visibility = Visibility.Visible;
            this.btnShowData.Visibility = Visibility.Visible;

            this.lblEntryMode.Tag = "XXXXXXXX";
            this.stkpOptions.IsEnabled = true;

            this.stkpPayData.Visibility = Visibility.Collapsed;

            for (int i = -6; i < 6; i++)
            {
                this.cmbInfoMonth.Items.Add(new ComboBoxItem() { Content = DateTime.Today.AddMonths(i).ToString("MMMM, yyyy"), Tag = "01-" + DateTime.Today.AddMonths(i).ToString("MMM-yyyy") });
            }
            this.cmbInfoMonth.SelectedIndex = 5;


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
        private void btnShowNewData_Click(object sender, RoutedEventArgs e)
        {
            this.RetriveSalarySheet(RetriveType: "NEWSALARY01");
        }
        private void btnShowData_Click(object sender, RoutedEventArgs e)
        {
            this.RetriveSalarySheet(RetriveType: "EDITSALARY01");
        }

        private void RetriveSalarySheet(string RetriveType = "EDITSALARY01")
        {
            this.SalaryInfoList.Clear();
            this.OldSalaryInfoList.Clear();
            string MonthID1 = DateTime.Parse(((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Tag.ToString()).ToString("yyyyMM");// "201705";
            string BrnCod = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            BrnCod = (BrnCod == "0000" ? "%" : BrnCod);
            string SectCod = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            SectCod = (SectCod.Substring(4, 8) == "00000000" ? "%" : SectCod);
            //RetriveType = "NEWSALARY01" // RetriveType = "EDITSALARY01"
            var pap1 = vm1.SetParamPrepareMonthlySalaey(WpfProcessAccess.CompInfList[0].comcpcod, RetriveType, MonthID1, BrnCod, SectCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.SalaryInfoList = ds1.Tables[0].DataTableToList<HmsEntityManpower.Payslip001>();
            if (this.SalaryInfoList.Count == 0)
            {
                System.Windows.MessageBox.Show("No record found. Please with appropriate options.", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            if (RetriveType == "NEWSALARY01")
            {
                var pap1o = vm1.SetParamPrepareMonthlySalaey(WpfProcessAccess.CompInfList[0].comcpcod, "EDITSALARY01", MonthID1, BrnCod, SectCod);
                DataSet ds1o = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1o == null)
                    return;
                this.OldSalaryInfoList = ds1.Tables[0].DataTableToList<HmsEntityManpower.Payslip001>();
            }
            //foreach (var item in this.SalaryInfoList)
            //{
            //    item.hcdesig = item.hccode.Substring(6, 6) + " - " + item.hcname;
            //}

            this.dgPayInfo.ItemsSource = this.SalaryInfoList;
            this.dgPayInfo.Items.Refresh();
            this.lblEntryMode.Text = (RetriveType == "EDITSALARY01" ? "EDIT SAVED ENTRY" : "NEW ENTRY");
            this.lblEntryMode.Tag = RetriveType;
            this.stkpOptions.IsEnabled = false;

            this.btnShowNewData.Visibility = Visibility.Collapsed;
            this.btnShowData.Visibility = Visibility.Collapsed;

            this.btnNextData.Visibility = Visibility.Visible;
            if (this.UpdateMode == true)
                this.btnUpdateData.Visibility = Visibility.Visible;

            this.stkpPayData.Visibility = Visibility.Visible;
        }


        private void btnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgPayInfo.SelectedItem == null)
                return;

            var item1 = ((HmsEntityManpower.Payslip001)this.dgPayInfo.SelectedItem);

            if (System.Windows.MessageBox.Show("Confirm Delete : " + item1.hcname.Trim(), WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
               MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            string hccode1 = item1.hccode.Trim();

            string MonthID1 = DateTime.Parse(((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Tag.ToString()).ToString("yyyyMM");// "201705";
            var pap1d = vm1.SetParamDeleteIndMonthlySalInf(WpfProcessAccess.CompInfList[0].comcpcod, MonthID1, hccode1);
            DataSet ds1d = WpfProcessAccess.GetHmsDataSet(pap1d);
            if (ds1d == null)
                return;

            this.SalaryInfoList.RemoveAll(x => x.hccode == item1.hccode);
            this.OldSalaryInfoList.RemoveAll(x => x.hccode == item1.hccode);
            this.dgPayInfo.Items.Refresh();
        }
        private void btnUpdateData_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            this.btnRecalc_Click(null, null);

            string MonthID1 = DateTime.Parse(((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Tag.ToString()).ToString("yyyyMM");// "201705";
            if (this.OldSalaryInfoList.Count > 0)
            {
                var pap1d = vm1.SetParamDeleteMonthlySalInf(WpfProcessAccess.CompInfList[0].comcpcod, this.OldSalaryInfoList, MonthID1);
                DataSet ds1d = WpfProcessAccess.GetHmsDataSet(pap1d);
                if (ds1d == null)
                    return;
                this.OldSalaryInfoList.Clear();
            }

            var lst1s = new List<HmsEntityManpower.Payslip001Saved>();
            foreach (var item in this.SalaryInfoList)
            {
                lst1s.Add(new HmsEntityManpower.Payslip001Saved()
                    {
                        comcod = item.comcod,
                        deptid = item.deptid,
                        hccode = item.hccode,
                        hcname = item.hcname,
                        workdays = item.workdays,
                        otdays = item.otdays,
                        absdays = item.absdays,
                        saladd01 = item.saladd01,
                        saladd02 = item.saladd02,
                        saladd08 = item.saladd08,
                        salded02 = item.salded02,
                        salded03 = item.salded03,
                        salded04 = item.salded04,
                        salded05 = item.salded05,
                        salded06 = item.salded06,
                        salded07 = item.salded07,
                        salded08 = item.salded08,
                        cashpay = item.cashpay,
                        bankacno = item.bankacno,
                        salrmrk = item.salrmrk,
                        saldate = item.saldate
                    });
            }

            foreach (var item1s in lst1s)
            {
                var lst2 = new List<HmsEntityManpower.Payslip001Saved>();
                lst2.Add(item1s);
                DataTable tbl1 = ASITFunLib.ASITUtility2.ListToDataTable(lst2);

                tbl1.TableName = "t1";
                DataSet DsSal1 = new DataSet("ds");
                DsSal1.Tables.Add(tbl1);
                string hccode1 = item1s.hccode;
                var pap1 = vm1.SetParamUpdateMonthlySalInf(WpfProcessAccess.CompInfList[0].comcpcod, DsSal1, MonthID1, hccode1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
            }

            System.Windows.MessageBox.Show("Update Successfull!!", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            //cmd.Parameters.Add("@dxml01", SqlDbType.Xml).Value = (pap1.parmXml01 == null ? null : pap1.parmXml01.GetXml());

            //string xmlStr1 = (DsSal1 == null ? null : DsSal1.GetXml().ToString());
            //byte[] xmlStr2 = System.Text.ASCIIEncoding.Default.GetBytes(xmlStr1); ;
        }

        private void dgPayInfo_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString() + ".";
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnRecalc_Click(object sender, RoutedEventArgs e)
        {
            string monthid1 = DateTime.Parse(((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Tag.ToString()).ToString("yyyyMM");// "201705";
            foreach (var item in this.SalaryInfoList)
            {
                if (this.chkIncAttnInfo.IsChecked == true)
                {
                    List<HmsEntityManpower.RptAttnSchInfo> Rptlst = HcmGeneralClass1.GetIndRosterAttendance(monthid1: monthid1, hccode1a: item.hccode, RptType: "Attendance");

                    if(Rptlst == null)
                        item.salrmrk = "MANUAL CHECK";
                    else
                    {
                        var Rptlst2 = Rptlst.FindAll(x => x.attnstatid == "SIHA00501001" || x.attnstatid == "SIHA00501005" || x.attnstatid == "SIHA00501006" || x.attnstatid == "SIHA00501008");
                        decimal otCount1 = Rptlst2.Count(x => x.attnstatid == "SIHA00501006");
                        decimal otCount2 = Rptlst2.Count(x => x.attnstatid == "SIHA00501005");
                        item.otdays = otCount1 * 1.00m + otCount2 / 2.00m;
                        decimal sumLate1 = Rptlst2.Sum(x => x.confirmlate);
                        decimal sumEout1 = Rptlst2.Sum(x => x.confirmearly); //  * 0.00m // Active from Jan-2018
                        decimal sumErr1 = Rptlst2.Sum(x => x.confirmerr);
                        int AbsCount1 = Rptlst2.Count(x => x.attnrmrk.Contains("(Absent)"));
                        item.absdays = AbsCount1 + sumErr1 + Math.Floor(sumLate1 / 3.00m) + Math.Floor(sumEout1 / 3.00m);
                    }
                }

                /*
                        <ComboBoxItem Content="Present" Tag="SIHA00501001" />
                        <ComboBoxItem Content="Absent (Day Off)" Tag="SIHA00502002" />
                        <ComboBoxItem Content="Absent (On Leave)" Tag="SIHA00502003" />
                        <ComboBoxItem Content="Present (Over-Time)" Tag="SIHA00501006" />
                        <ComboBoxItem Content="Present (Outdoor Duty)" Tag="SIHA00501007" />
                        <ComboBoxItem Content="Present (Half-Over-Time)" Tag="SIHA00501005" />
                        <ComboBoxItem Content="Present (Half-Leave)" Tag="SIHA00501008" />                                  
                 */

                item.paydays = item.workdays - item.absdays;                                    // Payment Days
                item.grosspay = item.saladd01 + item.saladd02 + item.saladd08;                  // saladd01: Cons. Pay, saladd02: Other Allowance, saladd08: Other Adjustment Payment
                item.saladd09 = Math.Round((item.saladd01 + item.saladd02) / item.workdays * item.otdays, 0);     // Over Time Amount
                item.salded01 = Math.Round((item.saladd01 + item.saladd02) / item.workdays * item.absdays, 0);    // Absent Deduction
                // salded02 : P.F., salded03: P.F. Loan, salded04: P.F. Int., salded05: Income Tax, salded06: Mobile Ded, salded07: Adv. Salary, salded08: Other Deduction
                item.salded10 = item.salded01 + item.salded02 + item.salded03 + item.salded04 + item.salded05 + item.salded06 + item.salded07 + item.salded08;
                item.netpay = item.grosspay - item.salded10;
            }
            this.dgPayInfo.Items.Refresh();
        }

        private void UpdateAttenInfo()
        {
            string MonthID1 = DateTime.Parse(((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Tag.ToString()).ToString("yyyyMM");// "201705";
            string BrnCod = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            BrnCod = (BrnCod == "0000" ? "%" : BrnCod);
            string SectCod = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            SectCod = (SectCod.Substring(4, 8) == "00000000" ? "%" : SectCod);
        }

        private void btnFindStaff_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoStaffSearch.SelectedValue == null)
                return;

            string srchVal1 = this.autoStaffSearch.SelectedValue.ToString();
            this.autoStaffSearch.SelectedValue = null;

            var emp22 = this.SalaryInfoList.FindAll(x => x.hccode == srchVal1);
            if (emp22.Count > 0)
            {
                this.dgPayInfo.ScrollIntoView(emp22[0]);
                this.dgPayInfo.SelectedItem = emp22[0];
                this.dgPayInfo.Focus();
            }

        }

        private void autoStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStaffName(args.Pattern);
        }

        private ObservableCollection<HmsEntityManpower.Payslip001> GetStaffName(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityManpower.Payslip001>(
                this.SalaryInfoList.Where((x, match) => x.hcdesig.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void btnNextData_Click(object sender, RoutedEventArgs e)
        {
            this.dgPayInfo.ItemsSource = null;
            this.SalaryInfoList.Clear();
            this.dgPayInfo.Items.Refresh();
            this.lblEntryMode.Text = "";
            this.lblEntryMode.Tag = "XXXXXXXX";
            this.stkpOptions.IsEnabled = true;

            this.btnNextData.Visibility = Visibility.Collapsed;
            this.btnUpdateData.Visibility = Visibility.Collapsed;

            this.btnShowNewData.Visibility = Visibility.Visible;
            this.btnShowData.Visibility = Visibility.Visible;

            this.stkpPayData.Visibility = Visibility.Collapsed;
        }

        private void btnUnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            string srchVal1 = this.wmtxtEmpID.Text.Trim();
            string srchtxt1 = "";
            srchtxt1 = "Confirm Retrive" + (srchtxt1.Length > 0 ? " : " + srchVal1 : " All Deleted Record") + " ?";
            if (System.Windows.MessageBox.Show("Confirm Retrive : " + srchtxt1.Trim(), WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
               MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            this.wmtxtEmpID.Text = "";
            string MonthID1 = DateTime.Parse(((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Tag.ToString()).ToString("yyyyMM");// "201705";
            string BrnCod = ((ComboBoxItem)this.cmbSBrnCod.SelectedItem).Tag.ToString();
            BrnCod = (BrnCod == "0000" ? "%" : BrnCod);
            string SectCod = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            SectCod = (SectCod.Substring(4, 8) == "00000000" ? "%" : SectCod);
            var pap1 = vm1.SetParamPrepareMonthlySalaey(WpfProcessAccess.CompInfList[0].comcpcod, "NEWSALARY01", MonthID1, BrnCod, SectCod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var UndeleteSalaryInfoList = ds1.Tables[0].DataTableToList<HmsEntityManpower.Payslip001>().ToList();
            if (UndeleteSalaryInfoList.Count == 0)
            {
                System.Windows.MessageBox.Show("No record found. Please with appropriate options.", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            if (srchVal1.Length > 0)
                UndeleteSalaryInfoList = UndeleteSalaryInfoList.FindAll(x => ASITUtility.Right(x.hccode, srchVal1.Length) == srchVal1).ToList();

            if (UndeleteSalaryInfoList.Count == 0)
            {
                System.Windows.MessageBox.Show("No record found. Please with appropriate options.", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }


            foreach (var item in UndeleteSalaryInfoList)
            {

                item.salrmrk = item.salrmrk + " (Undo)";
                if ((this.SalaryInfoList.FindAll(x => x.hccode == item.hccode)).Count == 0)
                    this.SalaryInfoList.Add(item);
            }


            this.SalaryInfoList.Sort(delegate(HmsEntityManpower.Payslip001 x, HmsEntityManpower.Payslip001 y)
            {
                return (x.deptid + x.hccode).CompareTo(y.deptid + y.hccode);
            });
            this.dgPayInfo.Items.Refresh();
            //order by deptid, hccode;
        }
    }
}
