#region Library Declaration
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Manpower;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using System.Collections;
using ASITHmsRpt3Manpower;
using Xceed.Wpf.Toolkit;
using System.Net;
using System.Diagnostics;
using System.Windows.Threading;
#endregion  // Library Declaration

namespace ASITHmsWpf.Manpower
{
    /// <summary>
    /// Interaction logic for frmEntryHRGenral1.xaml
    /// </summary>
    public partial class frmEntryHRGenral1 : UserControl
    {
        private bool FrmInitialized = false;
        private bool IsNewRecord = true;
        private bool AllowNewRecord = true;
        private System.Windows.Controls.Image imgPhoto1 = new System.Windows.Controls.Image();
        private System.Windows.Controls.Image signPhoto1 = new System.Windows.Controls.Image();
        private System.Windows.Controls.Image subsignPhoto1 = new System.Windows.Controls.Image();

        private vmEntryHRGenral1 vm1 = new vmEntryHRGenral1();
        private vmReportHCM1 vmr1 = new vmReportHCM1();
        private List<HmsEntityManpower.hcphoto> LstEmpphoto = new List<HmsEntityManpower.hcphoto>();



        private List<vmReportHCM1.Stafflist> StuffLst01 = new List<vmReportHCM1.Stafflist>();
        private DispatcherTimer timerClick1 = new DispatcherTimer();
        private int timerClick1Counter = 0;

        public frmEntryHRGenral1()
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
                this.ActivateAuthObjects();
                if (this.tbiBasicInfo1.Visibility == Visibility.Collapsed && this.tbiQual1.Visibility == Visibility.Collapsed
                        && this.tbiSalInf1.Visibility == Visibility.Collapsed && this.tbiReport1.Visibility == Visibility.Collapsed)
                {
                    this.stkpTitleInfo.Visibility = Visibility.Collapsed;
                    this.tabPanel1.Visibility = Visibility.Collapsed;
                    return;
                }
                this.Objects_On_Init();


                //-----For EmpList Report------------------
                var BranchList1 = WpfProcessAccess.CompInfList[0].BranchList;
                foreach (var item in BranchList1)
                {
                    if (item.brncod.ToString().Substring(2, 2) != "00") // .ToString().ToUpper().Contains("BRANCH"))
                    {
                        this.CmbBranch.Items.Add(new ComboBoxItem { Content = item.brnsnam.ToString(), Tag = item.brncod.ToString().Trim() });
                    }
                }
                this.timerClick1Counter = 0;
                timerClick1.Interval = TimeSpan.FromSeconds(0);
                timerClick1.Tick += this.timerClick1_Tick;
                timerClick1.Stop();
                //-----------------------------------------
                this.FrmInitialized = true;
                this.AtxtEmployeeNam.Focus();
            }
        }
        private void ActivateAuthObjects()
        {
            try
            {
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryHRGenral1_BasicInfo") == null)
                {
                    this.tbiBasicInfo1.Visibility = Visibility.Collapsed;
                    this.tbiBasicInfo1a.Visibility = Visibility.Collapsed;
                    this.btnNewRecord.Visibility = Visibility.Hidden;
                }
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryHRGenral1_btnNewRecord") == null)
                {
                    this.AllowNewRecord = false;
                    this.btnNewRecord.Visibility = Visibility.Hidden;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryHRGenral1_btnUpdateInfo") == null)
                    this.btnUpdateInfo.Visibility = Visibility.Hidden;


                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryHRGenral1_Qualification") == null)
                {
                    this.tbiQual1.Visibility = Visibility.Collapsed;
                    this.tbiQual1a.Visibility = Visibility.Collapsed;
                }
            
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryHRGenral1_Salary") == null)
                {
                    this.tbiSalInf1.Visibility = Visibility.Collapsed;
                    this.tbiSalInf1a.Visibility = Visibility.Collapsed;
                }          

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryHRGenral1_Reports") == null)
                {
                    this.tbiReport1.Visibility = Visibility.Collapsed;
                }

                if (this.tbiBasicInfo1.Visibility == Visibility.Visible)
                    this.tabPanel1.SelectedIndex = 0;
                else if (this.tbiQual1.Visibility == Visibility.Visible)
                    this.tabPanel1.SelectedIndex = 2;
                else if (this.tbiSalInf1.Visibility == Visibility.Visible)
                    this.tabPanel1.SelectedIndex = 4;
                else if (this.tbiReport1.Visibility == Visibility.Visible)
                    this.tabPanel1.SelectedIndex = 6;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCH-Gen-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void Objects_On_Init()
        {

            #region Initialize form objects by putting default values

            this.imgPhoto1.Source = this.imgPhoto.Source;
            this.signPhoto1.Source = this.signPhoto.Source;
            this.subsignPhoto1.Source = this.subsignPhoto.Source;

            if (WpfProcessAccess.StaffGroupList == null)
                WpfProcessAccess.GetCompanyStaffGroupList();

            foreach (var item1 in WpfProcessAccess.StaffGroupList)
            {
                this.cmbhrgroup.Items.Add(new ComboBoxItem() { Content = item1.sirdesc, Tag = item1.sircode });
            }

            WpfProcessAccess.StaffList = null;
            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            //this.AtxtEmployeeNam.AutoSuggestionList.Clear();

            //var tmpStaffList = WpfProcessAccess.StaffList.ToList();

            //tmpStaffList.Sort(delegate(HmsEntityGeneral.SirInfCodeBook x, HmsEntityGeneral.SirInfCodeBook y)
            //{
            //    return x.sirdesc.CompareTo(y.sirdesc);
            //});


            ////foreach (var item1 in WpfProcessAccess.StaffList)
            //foreach (var item1 in tmpStaffList)
            //{
            //    this.AtxtEmployeeNam.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());
            //    var mitm1 = new MenuItem() { Header = item1.sirdesc.Trim(), Tag = item1.sircode.Trim() };
            //    mitm1.Click += conMenuHCMItem_MouseClick;
            //    this.conMenuHCM.Items.Add(mitm1);
            //}


            this.AtxtSupCod.AutoSuggestionList.Clear();
            foreach (var item1 in WpfProcessAccess.StaffList)
            {
                this.AtxtSupCod.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());
            }

            if (WpfProcessAccess.GenInfoTitleList == null)
                WpfProcessAccess.GetGenInfoTitleList();

            #region Initializing Basic Information
            var grade11 = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 4) == "SIHG" && x.actcode.Substring(9, 3) != "000");
            foreach (var item11 in grade11)
            {
                this.cmbHRGrade.Items.Add(new ComboBoxItem() { Content = item11.actdesc, Tag = item11.actcode });
            }
            this.cmbHRGrade.SelectedIndex = 0;

            var deptlist = WpfProcessAccess.CompInfList[0].SectionList.FindAll(d => d.sectcod.Substring(9, 3) != "000");
            foreach (var itemdpt in deptlist)
            {
                this.cmbWrkDept.Items.Add(new ComboBoxItem() { Content = itemdpt.sectname, Tag = itemdpt.sectcod });
            }
            this.cmbWrkDept.SelectedIndex = 0;

            var hmdesig = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 4) == "SIHD" && x.actcode.Substring(9, 3) != "000");

            this.txtDesig.AutoSuggestionList.Clear();
            foreach (var item1 in hmdesig)
            {
                //this.txtDesig.AutoSuggestionList.Add(item1.actdesc.Trim() + " : [" + item1.actcode + "]");
                this.txtDesig.AddSuggstionItem(item1.actdesc.Trim(), item1.actcode.Trim());
            }

            var BasicEntry = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SIHI001" && x.actcode.Substring(9, 3) != "000");

            // Following Items are the primary information and entered with strict controll without using Datatable
            List<string> ListInf1 = new List<string>()
            {   
                "SIHI00101001", "SIHI00101004", "SIHI00101005", "SIHI00101006", "SIHI00101007", "SIHI00101011", "SIHI00101012", "SIHI00101021", "SIHI00101022", 
                "SIHI00102001", "SIHI00102002", "SIHI00102003", "SIHI00102004", "SIHI00102005", "SIHI00102008", "SIHI00102021", "SIHI00102022", "SIHI00102023"                 
            };

            this.dgBasicInfo1.Items.Clear();
            int srl1b = 1;
            foreach (var item in BasicEntry)
            {
                string xx = item.actcode;
                if (!ListInf1.Contains(item.actcode))
                {
                    string TitleDesc1 = item.actdesc;
                    if (item.actcode == "SIHI00102032" || item.actcode == "SIHI00102033" || item.actcode == "SIHI00102042" || item.actcode == "SIHI00102043")
                        TitleDesc1 = "";

                    this.dgBasicInfo1.Items.Add(new vmEntryHRGenral1.HcmInfo1 { slnum = srl1b.ToString("00") + ".", Code = item.actcode, TitleDesc = TitleDesc1, repeatsl = "001", ValueDesc = "", ValueType = item.acttype.Trim() });

                    srl1b++;
                }
            }

            this.dgBasicInfo1.Items.Refresh();
            #endregion //Initializing Basic Information

            #region Initializing Payroll Information

            var SalaryEntry = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SIHS001");

            var GAddition = SalaryEntry.FindAll(x => x.actcode.Substring(0, 9) == "SIHS00101" && x.actcode.Substring(9, 3) != "000" && x.actcode != "SIHS00101099");

            var GDeduction = SalaryEntry.FindAll(x => x.actcode.Substring(0, 9) == "SIHS00102" && x.actcode.Substring(9, 3) != "000" && x.actcode != "SIHS00102099");


            int srl1 = 1;
            foreach (var item in GAddition)
            {

                this.dgvPayAdd.Items.Add(new vmEntryHRGenral1.HcmInfo1 { slnum = srl1 + ".", Code = item.actcode, TitleDesc = item.actdesc, repeatsl = "001", ValueDesc = "", ValueType = item.acttype.Trim() });
                srl1++;
            }

            int srl2 = 1;
            foreach (var item in GDeduction)
            {

                this.dgvPayDed.Items.Add(new vmEntryHRGenral1.HcmInfo1 { slnum = srl2 + ".", TitleDesc = item.actdesc, Code = item.actcode, repeatsl = "001", ValueDesc = "", ValueType = item.acttype.Trim() });
                srl2++;
            }

            var SalOtherInfo = SalaryEntry.FindAll(x => (x.actcode.Substring(0, 9) == "SIHS00103" || x.actcode.Substring(0, 9) == "SIHS00109") && x.actcode.Substring(9, 3) != "000");
            int srl3 = 1;
            foreach (var item in SalOtherInfo)
            {

                this.dgvPayOther.Items.Add(new vmEntryHRGenral1.HcmInfo1 { slnum = srl3 + ".", TitleDesc = item.actdesc, Code = item.actcode, repeatsl = "001", ValueDesc = "", ValueType = item.acttype.Trim() });
                srl3++;
            }
            //SIHS00103001

            var AttnInfo = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SIHA001" && x.actcode.Substring(9, 3) != "000").OrderBy(y => y.acttdesc);

            int srl4 = 1;
            foreach (var item in AttnInfo)
            {
                this.dgvAttn.Items.Add(new vmEntryHRGenral1.HcmStdAttnSch1
                {
                    slnum = srl4 + ".",
                    attndesc = item.actdesc,
                    attncod = item.actcode,
                    attndata = "",
                    attnfri = "",
                    attnsat = "",
                    attnsun = "",
                    attnmon = "",
                    attntue = "",
                    attnwed = "",
                    attnthu = ""
                });
                srl4++;
            }
            #endregion //Initializing Payroll Information

            #region Initializing Qualification Information

            var Edu = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SIHE001" && x.actcode.Substring(9, 3) == "000" && x.actcode.Substring(7, 5) != "00000");
            foreach (var item11 in Edu)
            {

                this.dgViewEdu.Items.Add(new vmEntryHRGenral1.HcmEduInfo { eduCode = item11.actcode, degreenam = item11.actdesc, examnam = "", examinst = "", eduperiod = "", examresult = "", examrmrk = "", examyear = "" });
            }
            var Edu1 = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SIHE002" && x.actcode.Substring(9, 3) == "000" && x.actcode.Substring(7, 5) != "00000");
            foreach (var item11 in Edu1)
            {
                this.dgViewHrEd.Items.Add(new vmEntryHRGenral1.HcmEduInfo { eduCode = item11.actcode, degreenam = item11.actdesc, examnam = "", examinst = "", eduperiod = "", examresult = "", examrmrk = "", examyear = "" });
            }
            var Edu2 = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SIHE003" && x.actcode.Substring(9, 3) == "000" && x.actcode.Substring(7, 5) != "00000");
            foreach (var item11 in Edu2)
            {
                this.dgViewExtEd.Items.Add(new vmEntryHRGenral1.HcmEduInfo { eduCode = item11.actcode, degreenam = item11.actdesc, examnam = "", examinst = "", eduperiod = "", examresult = "", examrmrk = "", examyear = "" });
            }

            var Job = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SIHJ001" && x.actcode.Substring(9, 3) == "000" && x.actcode.Substring(7, 5) != "00000");
            int srl2j = 1;
            foreach (var item11 in Job)
            {
                this.dgvJob1.Items.Add(new vmEntryHRGenral1.HcmJobInfo { jobCode = item11.actcode, jobsl = srl2j + ".", jobcom = "", jobdept = "", jobdsg = "", sdate = DateTime.Parse("01-Jan-1900"), edate = DateTime.Parse("01-Jan-1900"), jobdesc = "", jobrmrks = "" });
                srl2j++;
            }
            #endregion Initializing Qualification Information
            #endregion //Initialize form objects by putting default values
            this.btnUpdateInfo.Visibility = Visibility.Hidden;
            this.stkpEmpinfo.Visibility = Visibility.Hidden;
            this.StackUserinfo.Visibility = Visibility.Hidden;
            this.xctk_joinDate.Value = DateTime.Today;
            this.xctk_joinConfarm.Value = DateTime.Today;
            this.xctk_SepexeDate.Value = DateTime.Parse("01-Jan-1900");
            this.xctk_SepinfDat.Value = DateTime.Parse("01-Jan-1900");
        }



        private void FillAtxtEmployeeNam()
        {
            this.AtxtEmployeeNam.AutoSuggestionList.Clear();
            this.conMenuHCM.Items.Clear();
            var StaffGroup1 = ((ComboBoxItem)this.cmbhrgroup.Items[this.cmbhrgroup.SelectedIndex]).Tag.ToString() + "XXXXXXXXXXXX";

            if (WpfProcessAccess.StaffGroupList == null)
                WpfProcessAccess.GetCompanyStaffGroupList();

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            var tmpStaffList = WpfProcessAccess.StaffList.FindAll(x => x.sircode.Substring(0, 7) == StaffGroup1.Substring(0, 7));// .ToList();

            //tmpStaffList.Sort(delegate(HmsEntityGeneral.SirInfCodeBook x, HmsEntityGeneral.SirInfCodeBook y)
            //{
            //    return x.sirdesc.CompareTo(y.sirdesc);
            //});


            //foreach (var item1 in WpfProcessAccess.StaffList)
            foreach (var item1 in tmpStaffList)
            {
                this.AtxtEmployeeNam.AddSuggstionItem(item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
                var mitm1 = new MenuItem() { Header = item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), Tag = item1.sircode.Trim() };
                mitm1.Click += conMenuHCMItem_MouseClick;
                this.conMenuHCM.Items.Add(mitm1);
            }

        }

        private void CmbBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CmbDept.Items.Clear();
            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList;
            string cod1 = ((ComboBoxItem)this.CmbBranch.SelectedItem).Tag.ToString().Trim();
            var dept1 = deptList1.FindAll(x => x.sectcod.Substring(0, 4) == cod1 && x.sectcod.Substring(9, 3) == "000");
            foreach (var item in dept1)
            {
                this.CmbDept.Items.Add(new ComboBoxItem { Content = item.sectname.ToString(), Tag = item.sectcod.Substring(0, 9).ToString().Trim() });
            }
            this.CmbDept.SelectedIndex = 0;            
        }

        private void CmbDept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CmbSec.Items.Clear();
            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList;
            try
            {
                // string brncCod = ((ComboBoxItem)this.CmbBranch.SelectedItem).Tag.ToString().Trim();
                string DeptCod = ((ComboBoxItem)this.CmbDept.SelectedItem).Tag.ToString().Trim();
                var Sect1 = deptList1.FindAll(x => x.sectcod.Substring(0, 9) == DeptCod && x.sectcod.Substring(9, 3) != "000");

                if (DeptCod.Substring(7, 2) != "00")
                {
                    this.CmbSec.Items.Add(new ComboBoxItem { Content = "ALL SECTIONS", Tag = DeptCod + "000" });
                }

                foreach (var item in Sect1)
                {
                    this.CmbSec.Items.Add(new ComboBoxItem { Content = item.sectname.ToString(), Tag = item.sectcod.ToString().Trim() });
                }
            }
            catch (Exception)
            {

            }
            this.CmbSec.SelectedIndex = 0;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            this.ClearContents();
            string hcid1 = this.AtxtEmployeeNam.Value.Trim();
            string nameDsg = this.AtxtEmployeeNam.Text.Trim();
            if (hcid1.Length <= 0 || nameDsg.Length <= 0)
                return;

            if (!this.ShowHCInfo(hcid1, nameDsg))
                return;

            this.IsNewRecord = false;
            this.AtxtEmployeeNam.IsEnabled = false;
            this.StackUserinfo.Visibility = Visibility.Visible;
            this.cmbhrgroup.IsEnabled = false;
            this.stkpEmpinfo.Visibility = Visibility.Visible;
            this.btnUpdateInfo.Visibility = Visibility.Visible;
            //this.btnFind.IsEnabled = false;
            this.btnFind.Visibility = Visibility.Hidden;

            this.btnNewRecord.Content = "_Next";
            this.btnNewRecord.Visibility = Visibility.Visible;

            if (this.tbiBasicInfo1.Visibility == Visibility.Visible)
                this.tabPanel1.SelectedIndex = 0;
            else if (this.tbiQual1.Visibility == Visibility.Visible)
                this.tabPanel1.SelectedIndex = 2;
            else if (this.tbiSalInf1.Visibility == Visibility.Visible)
                this.tabPanel1.SelectedIndex = 4;
            else if (this.tbiReport1.Visibility == Visibility.Visible)
                this.tabPanel1.SelectedIndex = 6;

        }

        private void btnNewRecord_Click(object sender, RoutedEventArgs e)
        {

            this.AtxtEmployeeNam.Text = "";
            if (this.btnNewRecord.Content.ToString() == "_Next")
            {
                this.stkpEmpinfo.Visibility = Visibility.Hidden;
                this.btnUpdateInfo.Visibility = Visibility.Hidden;
                this.StackUserinfo.Visibility = Visibility.Hidden;
                //this.btnFind.IsEnabled = true;
                this.btnFind.Visibility = Visibility.Visible;
                this.cmbhrgroup.IsEnabled = true;
                this.AtxtEmployeeNam.IsEnabled = true;
                this.AtxtEmployeeNam.ToolTip = "Right Click or Double Click to view existing staff list";
                this.AtxtEmployeeNam.Focus();
                if (this.AllowNewRecord == true)
                    this.btnNewRecord.Content = "_Add New";
                else
                    this.btnNewRecord.Visibility = Visibility.Hidden;
                return;

            }
            this.ClearContents();
            this.btnNewRecord.Content = "_Next";
            this.AtxtEmployeeNam.IsEnabled = false;
            this.stkpEmpinfo.Visibility = Visibility.Visible;
            this.btnUpdateInfo.Visibility = Visibility.Visible;
            this.IsNewRecord = true;
            //this.btnFind.IsEnabled = false;
            this.btnFind.Visibility = Visibility.Hidden;
            this.cmbhrgroup.IsEnabled = false;
            //this.cmbhrgroup.SelectedIndex = 2;
        }

        private void ClearContents()
        {
            #region Form Controls Cleanup Code
            this.cmbhrgroup.ItemsSource = null;
            this.lblHcmID.Content = "000000";
            this.lblHcmID.Tag = "000000000000";
            this.txtIdCardNo.Text = "";
            this.cmbGender.ItemsSource = null;
            this.cmbTname.ItemsSource = null;
            this.txtFname.Text = "";
            this.txtMname.Text = "";
            this.txtLnamer.Text = "";
            this.txtSurname.Text = "";
            this.lblFullName.Content = "";
            this.cmbBldGr.ItemsSource = null;
            this.txtDesig.Text = "";
            this.txtDesig.Tag = "";
            this.cmbHRGrade.ItemsSource = null;
            this.cmbWrkDept.ItemsSource = null;
            this.AtxtSupCod.Text = "";
            this.AtxtSupCod.Tag = "";
            this.xctk_joinDate.Value = DateTime.Today;
            this.xctk_SepinfDat.Value = DateTime.Parse("01-Jan-1900");
            this.xctk_joinConfarm.Value = DateTime.Today;
            this.xctk_SepexeDate.Value = DateTime.Parse("01-Jan-1900");
            this.xctk_Date.Value = DateTime.Parse("01-Jan-1900");
            this.lblage.Content = "Age :";

            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgBasicInfo1.Items)
                item.ValueDesc = "";

            this.dgBasicInfo1.Items.Refresh();

            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgvPayAdd.Items)
                item.ValueDesc = "";

            this.dgvPayAdd.Items.Refresh();


            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgvPayDed.Items)
                item.ValueDesc = "";

            foreach (vmEntryHRGenral1.HcmStdAttnSch1 item in this.dgvAttn.Items)
            {
                item.attndata = ""; item.attnfri = ""; item.attnsat = ""; item.attnsun = ""; item.attnmon = ""; item.attntue = ""; item.attnwed = ""; item.attnthu = "";
            }
            this.rbatnfri.IsChecked = false;
            this.rbatnsat.IsChecked = false;
            this.rbatnsun.IsChecked = false;
            this.rbatnmon.IsChecked = false;
            this.rbatntue.IsChecked = false;
            this.rbatnwed.IsChecked = false;
            this.rbatnthu.IsChecked = false;

            this.dgvPayDed.Items.Refresh();
            this.lblGTotal.Content = "-";
            this.lblGDed.Content = "-";
            this.lblNetPay.Content = "-";

            this.chkSeperateNoticeDate.IsChecked = false;
            this.chkSeperationDate.IsChecked = false;

            foreach (vmEntryHRGenral1.HcmEduInfo item in this.dgViewEdu.Items)
            {
                item.examnam = ""; item.examinst = ""; item.eduperiod = ""; item.examyear = ""; item.examresult = ""; item.examrmrk = "";
            }
            this.dgViewEdu.Items.Refresh();

            foreach (vmEntryHRGenral1.HcmEduInfo item in this.dgViewHrEd.Items)
            {
                item.examnam = ""; item.examinst = ""; item.eduperiod = ""; item.examyear = ""; item.examresult = ""; item.examrmrk = "";
            }
            this.dgViewHrEd.Items.Refresh();

            foreach (vmEntryHRGenral1.HcmEduInfo item in this.dgViewExtEd.Items)
            {
                item.examnam = ""; item.examinst = ""; item.eduperiod = ""; item.examyear = ""; item.examresult = ""; item.examrmrk = "";
            }
            this.dgViewExtEd.Items.Refresh();

            foreach (vmEntryHRGenral1.HcmJobInfo item in this.dgvJob1.Items)
            {
                item.jobcom = ""; item.jobdept = ""; item.jobdsg = ""; item.sdate = DateTime.Parse("01-Jan-1900"); item.edate = DateTime.Parse("01-Jan-1900"); item.jobdesc = ""; item.jobrmrks = "";
            }
            this.dgvJob1.Items.Refresh();

            this.UserPhoto.Source = this.imgPhoto1.Source;
            this.imgPhoto.Source = this.imgPhoto1.Source;
            this.signPhoto.Source = this.signPhoto1.Source;
            this.subsignPhoto.Source = this.subsignPhoto1.Source;
            this.tabPanel1.SelectedIndex = 0;
            #endregion  //Form Controls Cleanup Code
        }

        private bool ShowHCInfo(string hcid1, string nameDsg)
        {
            #region Data Populate into Form Controls

            var pap1 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "BASICINFO1");

            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return false;

            for (int i = 0; i < this.cmbhrgroup.Items.Count; i++)
            {
                if (((ComboBoxItem)cmbhrgroup.Items[i]).Tag.ToString() == hcid1.Substring(0, 7) + "00000")
                {
                    this.cmbhrgroup.SelectedIndex = i;
                    break;
                }
            }

            nameDsg = (!nameDsg.Contains(',') ? nameDsg + "," : nameDsg);
            this.lblShowfullname.Content = nameDsg.Substring(0, nameDsg.IndexOf(',', 1)).Substring(8).Trim();
            this.lblShowdesig.Content = nameDsg.Substring(nameDsg.IndexOf(',', 1) + 1).Trim();
            ////this.AtxtEmployeeNam.Text = "";

            var EmpInfoList = ds1.Tables[0].DataTableToList<vmEntryHRGenral1.HcmInfoTable>();

            var emdetails = EmpInfoList.FindAll(x => (x.actcode.Substring(0, 9) == "SIHI00102") || (x.actcode.Substring(0, 9) == "SIHI00101"));

            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgBasicInfo1.Items)
            {
                item.ValueDesc = "";
                item.ValueDesc = emdetails.Find(x => x.actcode == item.Code).dataval.ToString().Trim();
            }
            this.dgBasicInfo1.Items.Refresh();
            ////////////
            //var emEdu = EmpInfoList.FindAll(x => (x.actcode.Substring(0, 7) == "SIHE001"));           
            foreach (vmEntryHRGenral1.HcmEduInfo item in dgViewEdu.Items)
            {
                item.examnam = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "001").dataval.ToString().Trim();
                item.examinst = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "002").dataval.ToString().Trim();
                item.eduperiod = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "003").dataval.ToString().Trim();
                item.examyear = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "004").dataval.ToString().Trim();
                item.examresult = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "005").dataval.ToString().Trim();
                item.examrmrk = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "006").dataval.ToString().Trim();
            }

            this.dgViewEdu.Items.Refresh();
            foreach (vmEntryHRGenral1.HcmEduInfo item in dgViewHrEd.Items)
            {
                item.examnam = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "001").dataval.ToString().Trim();
                item.examinst = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "002").dataval.ToString().Trim();
                item.eduperiod = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "003").dataval.ToString().Trim();
                item.examyear = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "004").dataval.ToString().Trim();
                item.examresult = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "005").dataval.ToString().Trim();
                item.examrmrk = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "006").dataval.ToString().Trim();
            }

            this.dgViewHrEd.Items.Refresh();
            foreach (vmEntryHRGenral1.HcmEduInfo item in dgViewExtEd.Items)
            {
                item.examnam = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "001").dataval.ToString().Trim();
                item.examinst = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "002").dataval.ToString().Trim();
                item.eduperiod = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "003").dataval.ToString().Trim();
                item.examyear = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "004").dataval.ToString().Trim();
                item.examresult = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "005").dataval.ToString().Trim();
                item.examrmrk = EmpInfoList.Find(x => x.actcode == item.eduCode.Substring(0, 9) + "006").dataval.ToString().Trim();
            }

            this.dgViewExtEd.Items.Refresh();
            foreach (vmEntryHRGenral1.HcmJobInfo item in dgvJob1.Items)
            {
                item.jobcom = EmpInfoList.Find(x => x.actcode == item.jobCode.Substring(0, 9) + "001").dataval.ToString().Trim();
                item.jobdsg = EmpInfoList.Find(x => x.actcode == item.jobCode.Substring(0, 9) + "002").dataval.ToString().Trim();
                item.jobdept = EmpInfoList.Find(x => x.actcode == item.jobCode.Substring(0, 9) + "003").dataval.ToString().Trim();
                string sdat1 = EmpInfoList.Find(x => x.actcode == item.jobCode.Substring(0, 9) + "004").dataval.ToString().Trim();
                sdat1 = (sdat1.Length == 0 ? "01-Jan-1900" : sdat1);
                item.sdate = DateTime.Parse(sdat1);
                string edat1 = EmpInfoList.Find(x => x.actcode == item.jobCode.Substring(0, 9) + "005").dataval.ToString().Trim();
                edat1 = (edat1.Length == 0 ? "01-Jan-1900" : edat1);
                item.edate = DateTime.Parse(edat1);
                item.jobdesc = EmpInfoList.Find(x => x.actcode == item.jobCode.Substring(0, 9) + "006").dataval.ToString().Trim();
                item.jobrmrks = EmpInfoList.Find(x => x.actcode == item.jobCode.Substring(0, 9) + "007").dataval.ToString().Trim();
            }

            this.dgvJob1.Items.Refresh();

            // Populat Salary Addition Grid
            var GAddition = EmpInfoList.FindAll(x => x.actcode.Substring(0, 9) == "SIHS00101" && x.actcode.Substring(9, 3) != "000" && x.actcode != "SIHS00101099");
            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgvPayAdd.Items)
            {
                item.ValueDesc = "";
                item.ValueDesc = GAddition.Find(x => x.actcode == item.Code).dataval.ToString().Trim();
            }
            this.dgvPayAdd.Items.Refresh();

            //var BankInf1 = EmpInfoList.FindAll(x => x.actcode == "SIHS00109001");
            //this.txtBankName.Text = (BankInf1.Count > 0 ? BankInf1[0].dataval.ToString().Trim() : "");
            //var BankInf2 = EmpInfoList.FindAll(x => x.actcode == "SIHS00109002");
            //this.txtBankAccNo.Text = (BankInf2.Count > 0 ? BankInf2[0].dataval.ToString().Trim() : ""); ;


            // SIHS00109001 SIHS00109002

            // Populat Salary Deduction Grid

            var GDeduction = EmpInfoList.FindAll(x => x.actcode.Substring(0, 9) == "SIHS00102" && x.actcode.Substring(9, 3) != "000" && x.actcode != "SIHS00102099");
            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgvPayDed.Items)
            {
                item.ValueDesc = "";
                item.ValueDesc = GDeduction.Find(x => x.actcode == item.Code).dataval.ToString().Trim();
            }
            this.dgvPayDed.Items.Refresh();

            // Populat Salary Others Information Grid

            var SalOtherInfo = EmpInfoList.FindAll(x => (x.actcode.Substring(0, 9) == "SIHS00103" || x.actcode.Substring(0, 9) == "SIHS00109") && x.actcode.Substring(9, 3) != "000");
            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgvPayOther.Items)
            {
                item.ValueDesc = "";
                item.ValueDesc = SalOtherInfo.Find(x => x.actcode == item.Code).dataval.ToString().Trim();
            }
            this.dgvPayOther.Items.Refresh();

            // Populat Attendence Information Grid

            var lstAttn = EmpInfoList.FindAll(x => x.actcode.Substring(0, 7) == "SIHA001" && x.actcode.Substring(9, 3) != "000");

            foreach (vmEntryHRGenral1.HcmStdAttnSch1 item in this.dgvAttn.Items)
            {
                item.attndata = "";
                item.attndata = lstAttn.Find(x => x.actcode == item.attncod).dataval.ToString().Trim();
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
            this.dgvAttn.Items.Refresh();
            var offday1 = (vmEntryHRGenral1.HcmStdAttnSch1)this.dgvAttn.Items[0];
            this.rbatnfri.IsChecked = (offday1.attnfri.Contains("OFF") ? true : false);
            this.rbatnsat.IsChecked = (offday1.attnsat.Contains("OFF") ? true : false);
            this.rbatnsun.IsChecked = (offday1.attnsun.Contains("OFF") ? true : false);
            this.rbatnmon.IsChecked = (offday1.attnmon.Contains("OFF") ? true : false);
            this.rbatntue.IsChecked = (offday1.attntue.Contains("OFF") ? true : false);
            this.rbatnwed.IsChecked = (offday1.attnwed.Contains("OFF") ? true : false);
            this.rbatnthu.IsChecked = (offday1.attnthu.Contains("OFF") ? true : false);

            this.btnCalculate_Click(null, null);

            this.txtIdCardNo.Text = EmpInfoList.Find(x => x.actcode == "SIHI00101001").dataval.ToString().Trim();

            this.lblShowidCardnum.Content = this.txtIdCardNo.Text;
            this.lblHcmID.Content = hcid1.Substring(6);
            this.lblHcmID.Tag = hcid1;
            this.cmbGender.Text = EmpInfoList.Find(x => x.actcode == "SIHI00102021").dataval.ToString().Trim();

            this.txtFname.Text = EmpInfoList.Find(x => x.actcode == "SIHI00102003").dataval.ToString().Trim();
            this.txtMname.Text = EmpInfoList.Find(x => x.actcode == "SIHI00102004").dataval.ToString().Trim();
            this.txtLnamer.Text = EmpInfoList.Find(x => x.actcode == "SIHI00102005").dataval.ToString().Trim();
            this.txtSurname.Text = EmpInfoList.Find(x => x.actcode == "SIHI00102008").dataval.ToString().Trim();
            this.lblFullName.Content = EmpInfoList.Find(x => x.actcode == "SIHI00102001").dataval.ToString().Trim();

            this.cmbBldGr.Text = EmpInfoList.Find(x => x.actcode == "SIHI00102023").dataval.ToString().Trim();

            string dsgid1 = EmpInfoList.Find(x => x.actcode == "SIHI00101004").dataval.ToString().Trim();
            if (dsgid1.Length > 0)
            {
                var dsgdsc = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode == dsgid1);
                if (dsgdsc.Count > 0)
                {
                    this.txtDesig.Text = dsgdsc[0].actdesc.Trim();
                    this.txtDesig.Tag = dsgdsc[0].actcode.Trim();
                }
            }

            var deptcod1 = EmpInfoList.Find(x => x.actcode == "SIHI00101005").dataval.ToString().Trim();

            int depti = 0;
            foreach (var item in this.cmbWrkDept.Items)
            {
                if (((ComboBoxItem)item).Tag.ToString() == deptcod1)
                {
                    this.cmbWrkDept.SelectedIndex = depti;
                    break;
                }
                ++depti;
            }

            string supid1 = EmpInfoList.Find(x => x.actcode == "SIHI00101007").dataval.ToString().Trim();
            string supdesc1 = (supid1.Length < 12 ? "" : WpfProcessAccess.StaffList.Find(x => x.sircode == supid1).sirdesc.ToString().Trim());
            this.AtxtSupCod.Text = (supdesc1.Length < 12 ? "" : supdesc1);
            this.AtxtSupCod.Tag = supid1;
            this.lblShowSuper.Content = this.AtxtSupCod.Text;

            string grad11 = EmpInfoList.Find(x => x.actcode == "SIHI00101006").dataval.ToString().Trim();

            int gr1 = 0;
            foreach (var item in this.cmbHRGrade.Items)
            {
                if (((ComboBoxItem)item).Tag.ToString().Trim() == grad11)
                {
                    this.cmbHRGrade.SelectedIndex = gr1;
                    break;
                }
                ++gr1;
            }

            string findStr1 = EmpInfoList.Find(x => x.actcode == "SIHI00101011").dataval.ToString().Trim(); // sdat1 = (sdat1.Length == 0 ? "01-Jan-1900" : sdat1);

            this.xctk_joinDate.Text = (findStr1.Length == 0 ? "01-Jan-1900" : findStr1); // EmpInfoList.Find(x => x.actcode == "SIHI00101011").dataval.ToString().Trim();
            this.lblTJoinDate.Content = this.xctk_joinDate.Text;


            findStr1 = EmpInfoList.Find(x => x.actcode == "SIHI00101021").dataval.ToString().Trim();
            this.xctk_SepinfDat.Text = (findStr1.Length == 0 ? "01-Jan-1900" : findStr1); // EmpInfoList.Find(x => x.actcode == "SIHI00101021").dataval.ToString().Trim();

            findStr1 = EmpInfoList.Find(x => x.actcode == "SIHI00101022").dataval.ToString().Trim();
            this.xctk_SepexeDate.Text = (findStr1.Length == 0 ? "01-Jan-1900" : findStr1); // EmpInfoList.Find(x => x.actcode == "SIHI00101022").dataval.ToString().Trim();

            findStr1 = EmpInfoList.Find(x => x.actcode == "SIHI00101012").dataval.ToString().Trim();
            this.xctk_joinConfarm.Text = (findStr1.Length == 0 ? "01-Jan-1900" : findStr1); // EmpInfoList.Find(x => x.actcode == "SIHI00101012").dataval.ToString().Trim();

            findStr1 = EmpInfoList.Find(x => x.actcode == "SIHI00102022").dataval.ToString().Trim();
            this.xctk_Date.Text = (findStr1.Length == 0 ? "01-Jan-1900" : findStr1); // EmpInfoList.Find(x => x.actcode == "SIHI00102022").dataval.ToString().Trim();

            this.lblShowEMPID.Content = hcid1.Substring(6);
            this.lblShowEMPID.Tag = hcid1;

            if (!this.ShowPhotoSign(hcid1))
                return false;

            return true;
            #endregion  // Data Populate into Form Controls
        }

        private bool ShowPhotoSign(string hcid1)
        {

            var pap2 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "PHOTO");
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
            if (ds2 == null)
                return true;

            if (ds2.Tables[0].Rows.Count <= 0)
                return true;

            if (!(ds2.Tables[0].Rows[0]["hcphoto"] is DBNull))
            {
                byte[] bytes = (byte[])ds2.Tables[0].Rows[0]["hcphoto"];
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.StreamSource = mem;
                bmp3.EndInit();
                this.imgPhoto.Source = bmp3;
                this.UserPhoto.Source = bmp3;
            }

            var pap3 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "SIGN1");
            DataSet ds3 = WpfProcessAccess.GetHmsDataSet(pap3);
            if (ds3 == null)
                return true;

            if (ds3.Tables[0].Rows.Count <= 0)
                return true;

            if (!(ds3.Tables[0].Rows[0]["hcinisign"] is DBNull))
            {
                byte[] byteSi = (byte[])ds3.Tables[0].Rows[0]["hcinisign"];
                MemoryStream mem1 = new MemoryStream(byteSi);
                BitmapImage bmp4 = new BitmapImage();
                bmp4.BeginInit();
                bmp4.StreamSource = mem1;
                bmp4.EndInit();
                this.subsignPhoto.Source = bmp4;
            }

            var pap4 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "SIGN2");
            DataSet ds4 = WpfProcessAccess.GetHmsDataSet(pap4);
            if (ds4 == null)
                return true;

            if (ds4.Tables[0].Rows.Count <= 0)
                return true;

            if (!(ds4.Tables[0].Rows[0]["hcfullsign"] is DBNull))
            {

                byte[] byteSub = (byte[])ds4.Tables[0].Rows[0]["hcfullsign"];
                MemoryStream mem2 = new MemoryStream(byteSub);
                BitmapImage bmp5 = new BitmapImage();
                bmp5.BeginInit();
                bmp5.StreamSource = mem2;
                bmp5.EndInit();
                this.signPhoto.Source = bmp5;
            }

            return true;
        }


        #region Photo upload And show
        private void uploadphoto_Click(object sender, RoutedEventArgs e)
        {
            string BtnSender = ((Button)sender).Name.ToString().Trim();
            this.Imgfileopen(BtnSender);

        }
        private void txtPhoto_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBoxNam = (TextBox)sender;
            this.showImage(txtBoxNam);
        }

        public void Imgfileopen(string btnName)
        {

            long threshold = 60000L;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                var size = new FileInfo(openFileDialog.FileName).Length;
                if (size <= threshold)
                {
                    switch (btnName)
                    {
                        case "uploadphoto":
                            txtPhoto.Text = openFileDialog.FileName;
                            break;
                        case "uploadSign":
                            txtSign.Text = openFileDialog.FileName;
                            break;
                        case "uploadsubSign":
                            txtsubSign.Text = openFileDialog.FileName;
                            break;
                    }
                }
                else
                {
                    switch (btnName)
                    {
                        case "uploadphoto":
                            txtPhoto.Text = "File size > 60Kb";
                            break;
                        case "uploadSign":
                            txtSign.Text = "File size > 60Kb";
                            break;
                        case "uploadsubSign":
                            txtsubSign.Text = "File size > 60Kb";
                            break;
                    }
                }
            }
        }


        public void showImage(TextBox txtName)
        {
            string txtSender = txtName.Name.ToString().Trim();
            try
            {
                // image to byte Convert
                Bitmap bmp = new Bitmap(txtName.Text);
                //Bitmap bitmap2 = HmsImageManager.ResizeImaze(bmp, 320, 240);
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                //string image = Convert.ToBase64String((byte[])converter.ConvertTo(bitmap2, typeof(byte[])));
                string image = Convert.ToBase64String((byte[])converter.ConvertTo(bmp, typeof(byte[])));

                // byte to image Convert
                byte[] bytes = Convert.FromBase64String(image);
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp2 = new BitmapImage();
                bmp2.BeginInit();
                bmp2.StreamSource = mem;
                bmp2.EndInit();
                //
                switch (txtSender)
                {

                    case "txtPhoto":
                        this.imgPhoto.Source = bmp2;
                        this.UserPhoto.Source = bmp2;
                        break;
                    case "txtSign":
                        this.signPhoto.Source = bmp2;
                        break;
                    case "txtsubSign":
                        this.subsignPhoto.Source = bmp2;
                        break;
                }

            }
            catch
            {
                return;
            }
        }

        #endregion

        private void txtLnamer_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblFullName.Content = (this.txtFname.Text.Trim() + " " + this.txtMname.Text.Trim() + " " + this.txtLnamer.Text.Trim()).ToUpper();
        }

        #region Payroll Information
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Populat Salary Addition Grid
            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgvPayAdd.Items)
            {
                item.ValueDesc = (item.ValueDesc.Trim().Length == 0 ? "" : decimal.Parse("0" + item.ValueDesc.Trim().Replace("-", "")).ToString("#,##0.00"));
                //item.ValueDesc = GAddition.Find(x => x.actcode == item.Code).dataval.ToString().Trim();
            }
            this.dgvPayAdd.Items.Refresh();

            // Populat Salary Deduction Grid

            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgvPayDed.Items)
            {
                item.ValueDesc = (item.ValueDesc.Trim().Length == 0 ? "" : decimal.Parse("0" + item.ValueDesc.Trim().Replace("-", "")).ToString("#,##0.00"));
                //item.ValueDesc = GDeduction.Find(x => x.actcode == item.Code).dataval.ToString().Trim();
            }
            this.dgvPayDed.Items.Refresh();

            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgvPayOther.Items)
            {
                if (item.ValueType.Trim() == "N")
                    item.ValueDesc = (item.ValueDesc.Trim().Length == 0 ? "" : decimal.Parse("0" + item.ValueDesc.Trim().Replace("-", "")).ToString("#,##0.00"));
                //item.ValueDesc = SalOtherInfo.Find(x => x.actcode == item.Code).dataval.ToString().Trim();
            }
            this.dgvPayOther.Items.Refresh();


            //Total of General Addition

            double totalA = this.dgvPayAdd.Items.Cast<vmEntryHRGenral1.HcmInfo1>().ToList().Sum(item => Convert.ToDouble("0" + item.ValueDesc));
            this.lblGTotal.Content = totalA.ToString("#,##0.00;-#,##0.00; -  ");

            //Total of General Deduction
            double totalD = this.dgvPayDed.Items.Cast<vmEntryHRGenral1.HcmInfo1>().ToList().Sum(item => Convert.ToDouble("0" + item.ValueDesc));
            this.lblGDed.Content = totalD.ToString("#,##0.00;-#,##0.00; -  ");
            this.lblNetPay.Content = (totalA - totalD).ToString("#,##0.00;-#,##0.00; -  ");
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception exp1)
            {
                return;
            }
        }

        #endregion //Payroll Information
        private void xctk_Date_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.xctk_Date.Text.Trim().Length == 0)
                return;

            DateTime currentDate = DateTime.Parse(DateTime.Now.Date.ToShortDateString());
            DateTime birthdate = DateTime.Parse(this.xctk_Date.Text);
            int ageInDays = currentDate.Day - birthdate.Day;
            int ageInMonths = currentDate.Month - birthdate.Month;
            int ageInYears = currentDate.Year - birthdate.Year;
            if (ageInDays < 0)
            {
                ageInDays += DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                ageInMonths = ageInMonths--;

                if (ageInMonths < 0)
                {
                    ageInMonths += 12;
                    ageInYears--;
                }
            }
            if (ageInMonths < 0)
            {
                ageInMonths += 12;
                ageInYears--;
            }

            this.lblage.Content = ageInYears.ToString() + " Years " + ageInMonths.ToString() + " Month " + ageInDays.ToString() + " Day";
        }
    
        private void btnGenInfo1_Click(object sender, RoutedEventArgs e)
        {
            string hcid1 = this.lblShowEMPID.Tag.ToString().Trim();
            var pap1 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "BASICINFO1");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var EmpInfoList = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmInfoTable>();
            var pap2 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "PHOTO");
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
            if (ds2 == null)
                return;

            if (!(ds2.Tables[0].Rows[0]["hcphoto"] is DBNull))
                LstEmpphoto.Add(new HmsEntityManpower.hcphoto() { photo = (byte[])ds2.Tables[0].Rows[0]["hcphoto"] });

            var pap3 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "SIGN1");
            DataSet ds3 = WpfProcessAccess.GetHmsDataSet(pap3);
            if (ds3 == null)
                return;

            if (!(ds3.Tables[0].Rows[0]["hcinisign"] is DBNull))
                LstEmpphoto.Add(new HmsEntityManpower.hcphoto() { photo = (byte[])ds3.Tables[0].Rows[0]["hcinisign"] });

            var pap4 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "SIGN2");
            DataSet ds4 = WpfProcessAccess.GetHmsDataSet(pap4);
            if (ds4 == null)
                return;

            if (!(ds4.Tables[0].Rows[0]["hcfullsign"] is DBNull))
                LstEmpphoto.Add(new HmsEntityManpower.hcphoto() { photo = (byte[])ds4.Tables[0].Rows[0]["hcfullsign"] });

            int x = (ChkEdu.IsChecked == true ? 0 : 1);
            int y = (ChkJob.IsChecked == true ? 0 : 1);
            int[] chk = new int[2] { x, y };

            LocalReport rpt1 = HcmReportSetup.GetLocalReport("HcmInfo.RptHcmGenInf01", EmpInfoList, LstEmpphoto, null, chk);
            if (rpt1 == null)
                return;

            this.ShowReportWindow(rpt1, "Human Resource General Information Report", false);
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

        private void conMenuHCMItem_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtEmployeeNam.Text = ((MenuItem)sender).Header.ToString().Trim();
        }

        private void AtxtEmployeeNam_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AtxtEmployeeNam.ContextMenu.IsOpen = true;
        }

        private void cmbhrgroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.FillAtxtEmployeeNam();
        }

        private void chkSeperateNoticeDate_Click(object sender, RoutedEventArgs e)
        {
            //if (this.chkSeperateNoticeDate.IsChecked == true && this.xctk_SepexeDate.Text.Contains("01-Jan-1900"))
            //    this.xctk_SepinfDat.Value = DateTime.Today;

        }

        private void chkSeperationDate_Click(object sender, RoutedEventArgs e)
        {
            //if (this.chkSeperationDate.IsChecked == true && this.xctk_SepexeDate.Text.Contains("01-Jan-1900"))
            //    this.xctk_SepexeDate.Value = DateTime.Today;
        }

        private void xctk_Date_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dtp1 = (Xceed.Wpf.Toolkit.DateTimePicker)sender;
            dtp1.Text = (dtp1.Text.Contains("1900") ? DateTime.Today.ToString("dd-MMM-yyyy") : "01-Jan-1900");
        }

        private void rbatnAll_Click(object sender, RoutedEventArgs e)
        {
            string objName1 = ((RadioButton)sender).Name.ToString().ToUpper();
            foreach (vmEntryHRGenral1.HcmStdAttnSch1 item in this.dgvAttn.Items)
            {
                item.attnfri = (item.attnfri.Contains("OFF") ? "" : item.attnfri);
                item.attnsat = (item.attnsat.Contains("OFF") ? "" : item.attnsat);
                item.attnsun = (item.attnsun.Contains("OFF") ? "" : item.attnsun);
                item.attnmon = (item.attnmon.Contains("OFF") ? "" : item.attnmon);
                item.attntue = (item.attntue.Contains("OFF") ? "" : item.attntue);
                item.attnwed = (item.attnwed.Contains("OFF") ? "" : item.attnwed);
                item.attnthu = (item.attnthu.Contains("OFF") ? "" : item.attnthu);
            }

            foreach (vmEntryHRGenral1.HcmStdAttnSch1 item in this.dgvAttn.Items)
            {
                switch (objName1.Substring(5, 3))
                {
                    case "FRI": item.attnfri = "OFF"; break;
                    case "SAT": item.attnsat = "OFF"; break;
                    case "SUN": item.attnsun = "OFF"; break;
                    case "MON": item.attnmon = "OFF"; break;
                    case "TUE": item.attntue = "OFF"; break;
                    case "WED": item.attnwed = "OFF"; break;
                    case "THU": item.attnthu = "OFF"; break;
                }
            }
            this.dgvAttn.Items.Refresh();
            //for(int t1 = 2; t1<=8; t1++)
            //    this.dgvAttn.Columns[t1].IsReadOnly = false;

            //this.dgvAttn.Columns[tagid].IsReadOnly = true;
        }

        private void DgvAttnTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var item1 = ((WatermarkTextBox)sender);
            item1.IsReadOnly = (item1.Text.Contains("OFF") ? true : false);
        }

        private void btnUploadGenDocs1_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            string hcid1 = "H" + this.lblShowEMPID.Tag.ToString().Trim();
            OpenFileDialog op = new OpenFileDialog();
            op.DefaultExt = ".pdf";

            Nullable<bool> result = op.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                filePath = op.FileName;
            }

            string upload_file = filePath;//txtFilePath.Text;//
            string filExtension = System.IO.Path.GetExtension(upload_file);// GetFileName(upload_file);
            string filFileName = hcid1.Trim(); ;
            try
            {
                string ftpAddress = "ftp://59.152.97.179/";
                string username = "ftpuser001";
                string password = "123321";


                using (WebClient wclient = new WebClient())
                {
                    wclient.Credentials = new NetworkCredential(username, password);
                    wclient.UploadFile(ftpAddress + "/HmsDocs/" + filFileName + filExtension, "STOR", upload_file);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnShowGenDocs1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string hcid1 = "H" + this.lblShowEMPID.Tag.ToString().Trim();
                string FilePath = Environment.GetEnvironmentVariable("TEMP") + "\\" + hcid1 + ".pdf";

                string filname1 = "http://59.152.97.179/asitftpdata/HmsDocs/" + hcid1 + ".pdf";

                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(new System.Uri(filname1), FilePath);
                }
                Process myProcess = new Process();
                //myProcess.StartInfo.FileName = "AcroRd32"; //"AcroRd32.exe";//not the full application path
                myProcess.StartInfo.FileName = "chrome"; //"chrome.exe";//not the full application path
                //myProcess.StartInfo.FileName = "firefox"; //"firefox.exe";//not the full application path
                myProcess.StartInfo.Arguments = FilePath;// ToString();// @"C:\Temps\test.pdf"; //"/A \"page=2=OpenActions\" C:\\example.pdf";
                myProcess.Start();

            }
            catch (Exception)
            {
                return;
            }
        }



        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (System.Windows.MessageBox.Show("Confirm update all information except images", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                {
                    return;
                }

                //SetParamUpdateHCPrimaryInfor(string CompCode, DataSet ds1)

                string hcode1 = this.lblHcmID.Tag.ToString().Trim();
                hcode1 = (hcode1 == "000000000000" ? ((ComboBoxItem)this.cmbhrgroup.SelectedItem).Tag.ToString().Trim().Substring(0, 7) + "00000" : hcode1);

                #region Convert image Source to byte[]

                //imgPhoto.Source = new BitmapImage(new Uri(@"/Images/NoUser.jpg", UriKind.Relative)); //Source="..\Images\NoUser.jpg"
                //signPhoto.Source = new BitmapImage(new Uri(@"/Images/blue-bckgrd.jpg", UriKind.Relative));
                //subsignPhoto.Source = new BitmapImage(new Uri(@"/Images/blue-bckgrd.jpg", UriKind.Relative));
                byte[] pbytes = null;
                var bmp = this.imgPhoto.Source as BitmapImage;
                if (bmp != null)
                {
                    MemoryStream outStream = new MemoryStream();
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bmp));
                    enc.Save(outStream);


                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                    string pimage = Convert.ToBase64String((byte[])converter.ConvertTo(bitmap, typeof(byte[])));
                    pbytes = Convert.FromBase64String(pimage);

                }


                byte[] FSbytes = null;
                var bmp1 = signPhoto.Source as BitmapImage;
                if (bmp1 != null)
                {
                    MemoryStream outStream1 = new MemoryStream();
                    BitmapEncoder enc1 = new BmpBitmapEncoder();
                    enc1.Frames.Add(BitmapFrame.Create(bmp1));
                    enc1.Save(outStream1);
                    System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap(outStream1);
                    TypeConverter converter01 = TypeDescriptor.GetConverter(typeof(Bitmap));
                    string FSimage = Convert.ToBase64String((byte[])converter01.ConvertTo(bitmap1, typeof(byte[])));
                    FSbytes = Convert.FromBase64String(FSimage);

                }


                byte[] ISbytes = null;
                var bmp3 = subsignPhoto.Source as BitmapImage;
                if (bmp3 != null)
                {

                    MemoryStream outStream3 = new MemoryStream();
                    BitmapEncoder enc3 = new BmpBitmapEncoder();
                    enc3.Frames.Add(BitmapFrame.Create(bmp3));
                    enc3.Save(outStream3);
                    System.Drawing.Bitmap bitmap3 = new System.Drawing.Bitmap(outStream3);

                    TypeConverter converter02 = TypeDescriptor.GetConverter(typeof(Bitmap));
                    string ISimage = Convert.ToBase64String((byte[])converter02.ConvertTo(bitmap3, typeof(byte[])));
                    ISbytes = Convert.FromBase64String(ISimage);

                }
                #endregion Convert image Source to byte[]

                DataSet ds1i = vm1.GetDataSetHCPrimaryInfo(CompCode: WpfProcessAccess.CompInfList[0].comcod, _hccode: hcode1, _hcname: this.lblFullName.Content.ToString(),
                             _hcphoto: pbytes, _hcinisign: ISbytes, _hcfullsign: FSbytes);

                // string CompCode, DataSet ds1, string _hccode, string _hcname, string hcdsg, string updateGroup
                var pap1i = vm1.SetParamUpdateHCPrimaryInfo(WpfProcessAccess.CompInfList[0].comcod, ds1i, hcode1, this.lblFullName.Content.ToString(), this.txtDesig.Text.Trim());
                DataSet ds2i = WpfProcessAccess.GetHmsDataSet(pap1i);
                if (ds2i == null)
                {
                    System.Windows.MessageBox.Show("Could not update primary Information\nPlease update basic information correctly then try it.", WpfProcessAccess.AppTitle,
                        MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                #region Start of Generating Employee Basic Information
                

                hcode1 = ds2i.Tables[0].Rows[0]["hccode"].ToString();
                this.lblHcmID.Content = ds2i.Tables[0].Rows[0]["hccode"].ToString().Substring(6);
                this.lblHcmID.Tag = ds2i.Tables[0].Rows[0]["hccode"].ToString();

                this.lblShowEMPID.Content = ds2i.Tables[0].Rows[0]["hccode"].ToString().Substring(6);
                this.lblShowfullname.Content = ds2i.Tables[0].Rows[0]["hcname"].ToString();

                var BasicInfo1 = this.dgBasicInfo1.Items.Cast<vmEntryHRGenral1.HcmInfo1>().ToList();

                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101001", TitleDesc = "ID CARD NO", repeatsl = "001", ValueDesc = this.txtIdCardNo.Text.Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00102021", TitleDesc = "GENDER", repeatsl = "001", ValueDesc = this.cmbGender.Text.ToString().Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00102003", TitleDesc = "FIRST NAME", repeatsl = "001", ValueDesc = this.txtFname.Text.ToString().Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00102004", TitleDesc = "MIDDILE NAME", repeatsl = "001", ValueDesc = this.txtMname.Text.ToString().Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00102005", TitleDesc = "LAST NAME", repeatsl = "001", ValueDesc = this.txtLnamer.Text.ToString().Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00102008", TitleDesc = "SURNAME", repeatsl = "001", ValueDesc = this.txtSurname.Text.ToString().Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00102001", TitleDesc = "FULL NAME", repeatsl = "001", ValueDesc = this.lblFullName.Content.ToString().Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00102023", TitleDesc = "BLOOD GROUP", repeatsl = "001", ValueDesc = this.cmbBldGr.Text.ToString().Trim() });

                string DesigCode1 = this.txtDesig.Tag.ToString().Trim();
                DesigCode1 = (DesigCode1.Length == 0 ? "" : DesigCode1);

                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101004", TitleDesc = "DESIGNATION", repeatsl = "001", ValueDesc = DesigCode1 });
                string deptCod1 = ((ComboBoxItem)this.cmbWrkDept.SelectedItem).Tag.ToString();
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101005", TitleDesc = "DEPARTMENT", repeatsl = "001", ValueDesc = deptCod1.Trim() });

                string gradCod1 = ((ComboBoxItem)this.cmbHRGrade.SelectedItem).Tag.ToString();
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101006", TitleDesc = "GRADE CODE", repeatsl = "001", ValueDesc = gradCod1.Trim() });
                string SupCod1 = this.AtxtSupCod.Tag.ToString().Trim();
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101007", TitleDesc = "REPORTING SUPERVISOR", repeatsl = "001", ValueDesc = SupCod1 });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101011", TitleDesc = "JOINING DATE", repeatsl = "001", ValueDesc = this.xctk_joinDate.Text.Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101021", TitleDesc = "SEPERATION INFORM DATE", repeatsl = "001", ValueDesc = this.xctk_SepinfDat.Text.Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101012", TitleDesc = "CONFIRMATION DATE", repeatsl = "001", ValueDesc = this.xctk_joinConfarm.Text.Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00101022", TitleDesc = "SEPERATION DATE", repeatsl = "001", ValueDesc = this.xctk_SepexeDate.Text.Trim() });
                BasicInfo1.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHI00102022", TitleDesc = "DATE OF BIRTH", repeatsl = "001", ValueDesc = this.xctk_Date.Text.Trim() });
                #endregion End of Generating Employee Basic Information

                #region Start of Payroll Info

                this.btnCalculate_Click(null, null);

                var lstAddipayroll = this.dgvPayAdd.Items.Cast<vmEntryHRGenral1.HcmInfo1>().ToList();
                var lstDeducroll = this.dgvPayDed.Items.Cast<vmEntryHRGenral1.HcmInfo1>().ToList();
                var lstOtherPayroll = this.dgvPayOther.Items.Cast<vmEntryHRGenral1.HcmInfo1>().ToList();
                lstAddipayroll = lstAddipayroll.FindAll(x => double.Parse("0" + x.ValueDesc.Trim().Replace(",", "").Replace("-", "")) > 0);
                lstDeducroll = lstDeducroll.FindAll(x => double.Parse("0" + x.ValueDesc.Trim().Replace(",", "").Replace("-", "")) > 0);

                foreach (var item in lstAddipayroll)
                    item.ValueDesc = item.ValueDesc.Trim().Replace(",", "").Replace("-", "");

                foreach (var item in lstDeducroll)
                    item.ValueDesc = item.ValueDesc.Trim().Replace(",", "").Replace("-", "");



                var lstAttn = new List<vmEntryHRGenral1.HcmInfo1>();
                foreach (vmEntryHRGenral1.HcmStdAttnSch1 item in this.dgvAttn.Items)
                {
                    item.attndata = "FRI-" + item.attnfri.Trim() + "|" + "SAT-" + item.attnsat.Trim() + "|" + "SUN-" + item.attnsun.Trim() + "|" + "MON-" + item.attnmon.Trim() + "|" +
                                    "TUE-" + item.attntue.Trim() + "|" + "WED-" + item.attnwed.Trim() + "|" + "THU-" + item.attnthu.Trim();

                    lstAttn.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = item.attncod, ValueDesc = item.attndata, repeatsl = "001", TitleDesc = item.attndesc, ValueType = "T" });
                }

                List<vmEntryHRGenral1.HcmInfo1> lstpayrollinfo = lstAddipayroll.Concat(lstDeducroll).Concat(lstOtherPayroll).Concat(lstAttn).ToList();
                lstpayrollinfo.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHS00101099", repeatsl = "001", ValueDesc = this.lblGTotal.Content.ToString().Trim().Replace(",", "").Replace("-", "") });
                lstpayrollinfo.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", Code = "SIHS00102099", repeatsl = "001", ValueDesc = this.lblGDed.Content.ToString().Trim().Replace(",", "").Replace("-", "") });
                #endregion End of Payroll Info 

                BasicInfo1 = BasicInfo1.Concat(lstpayrollinfo).ToList();

                #region Start of Qualification Information


                var lstEdu1 = this.dgViewEdu.Items.Cast<vmEntryHRGenral1.HcmEduInfo>().ToList();
                var lsteduHr1 = this.dgViewHrEd.Items.Cast<vmEntryHRGenral1.HcmEduInfo>().ToList();
                var lsteduExt1 = this.dgViewExtEd.Items.Cast<vmEntryHRGenral1.HcmEduInfo>().ToList();
                var lstjob1 = this.dgvJob1.Items.Cast<vmEntryHRGenral1.HcmJobInfo>().ToList();

                var lstEduA = new List<vmEntryHRGenral1.HcmInfo1>();
                foreach (var item in lstEdu1)
                {
                    lstEduA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "001", repeatsl = "001", ValueDesc = item.examnam.Trim() });
                    lstEduA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "002", repeatsl = "001", ValueDesc = item.examinst.Trim() });
                    lstEduA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "003", repeatsl = "001", ValueDesc = item.eduperiod.Trim() });
                    lstEduA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "004", repeatsl = "001", ValueDesc = item.examyear.Trim() });
                    lstEduA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "005", repeatsl = "001", ValueDesc = item.examresult.Trim() });
                    lstEduA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "006", repeatsl = "001", ValueDesc = item.examrmrk.Trim() });
                }

                var lstEduB = new List<vmEntryHRGenral1.HcmInfo1>();
                foreach (var item in lsteduHr1)
                {
                    lstEduB.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "001", repeatsl = "001", ValueDesc = item.examnam.Trim() });
                    lstEduB.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "002", repeatsl = "001", ValueDesc = item.examinst.Trim() });
                    lstEduB.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "003", repeatsl = "001", ValueDesc = item.eduperiod.Trim() });
                    lstEduB.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "004", repeatsl = "001", ValueDesc = item.examyear.Trim() });
                    lstEduB.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "005", repeatsl = "001", ValueDesc = item.examresult.Trim() });
                    lstEduB.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "006", repeatsl = "001", ValueDesc = item.examrmrk.Trim() });
                }

                var lstEduC = new List<vmEntryHRGenral1.HcmInfo1>();
                foreach (var item in lsteduExt1)
                {
                    lstEduC.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "001", repeatsl = "001", ValueDesc = item.examnam.Trim() });
                    lstEduC.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "002", repeatsl = "001", ValueDesc = item.examinst.Trim() });
                    lstEduC.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "003", repeatsl = "001", ValueDesc = item.eduperiod.Trim() });
                    lstEduC.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "004", repeatsl = "001", ValueDesc = item.examyear.Trim() });
                    lstEduC.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "005", repeatsl = "001", ValueDesc = item.examresult.Trim() });
                    lstEduC.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.eduCode.Substring(0, 9) + "006", repeatsl = "001", ValueDesc = item.examrmrk.Trim() });
                }

                var lstJobA = new List<vmEntryHRGenral1.HcmInfo1>();
                foreach (var item in lstjob1)
                {
                    lstJobA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.jobCode.Substring(0, 9) + "001", repeatsl = "001", ValueDesc = item.jobcom.Trim() });
                    lstJobA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.jobCode.Substring(0, 9) + "002", repeatsl = "001", ValueDesc = item.jobdsg.Trim() });
                    lstJobA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.jobCode.Substring(0, 9) + "003", repeatsl = "001", ValueDesc = item.jobdept.Trim() });

                    if (item.sdate.Year > 1900)
                        lstJobA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.jobCode.Substring(0, 9) + "004", repeatsl = "001", ValueDesc = item.sdate.ToString("dd-MMM-yyyy").Trim() });

                    if (item.edate.Year > 1900)
                        lstJobA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.jobCode.Substring(0, 9) + "005", repeatsl = "001", ValueDesc = item.edate.ToString("dd-MMM-yyyy").Trim() });

                    lstJobA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.jobCode.Substring(0, 9) + "006", repeatsl = "001", ValueDesc = item.jobdesc.Trim(), ValueType = "T" });
                    lstJobA.Add(new vmEntryHRGenral1.HcmInfo1() { slnum = "", TitleDesc = "", Code = item.jobCode.Substring(0, 9) + "007", repeatsl = "001", ValueDesc = item.jobrmrks.Trim(), ValueType = "T" });
                }
                #endregion End of Qualification Information

                BasicInfo1 = BasicInfo1.Concat(lstEduA).Concat(lstEduB).Concat(lstEduC).Concat(lstJobA).ToList();


                BasicInfo1 = BasicInfo1.OrderBy(x => x.Code + x.repeatsl).ToList();


                var pap1 = vm1.SetParamForHRGenInfoUpdate(WpfProcessAccess.CompInfList[0].comcod, hcode1, BasicInfo1);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;


                this.StackUserinfo.Visibility = Visibility.Visible;
                //string hcdesS = this.txtDesig.Text.ToString().Trim();
                //hcdesS = (hcdesS.Length < 16 ? "" : hcdesS.Substring(0, txtDesig.Text.ToString().Length - 16));
                string hcdesS = this.txtDesig.Text.ToString().Trim();
                if (this.IsNewRecord)
                {
                    WpfProcessAccess.StaffList.Add(new HmsEntityGeneral.SirInfCodeBook() { comcod = "", rowid = 0, rowtime = DateTime.Today, sircode = hcode1, sircode1 = "",
                        sirdesc = this.lblFullName.Content.ToString().Trim(), sirdesc1 = "", sirtdes = "", sirtype = "", sirunit = "", siruconf = 0, siruconf3 = 0, sirunit2 = "", sirunit3 = "" });

                    this.AtxtEmployeeNam.AddSuggstionItem((hcode1.Substring(6) + " - " + this.lblFullName.Content.ToString().Trim() + "," + hcdesS), hcode1);
                }
                else
                {
                    var staff1a = WpfProcessAccess.StaffList.FindAll(x => x.sircode == hcode1);
                    if (staff1a.Count > 0)
                        staff1a[0].sirdesc = this.lblFullName.Content.ToString().Trim();

                    //foreach (var item in WpfProcessAccess.StaffList)
                    //{
                    //    if (item.sircode == hcode1)
                    //    {
                    //        item.sirdesc = this.lblFullName.Content.ToString().Trim();
                    //        break;
                    //    }
                    //}
                    int i = 0;
                    foreach (var item in this.AtxtEmployeeNam.AutoSuggestionList)
                    {
                        if (item.itemvalue.Contains(hcode1))
                        {

                            this.AtxtEmployeeNam.AutoSuggestionList[i].itemtxt = hcode1.Substring(6) + " - " + this.lblFullName.Content.ToString().Trim() + ", " + hcdesS;
                            this.AtxtEmployeeNam.AutoSuggestionList[i].itemvalue = hcode1;//   .AddSuggstionItem((this.lblFullName.Content.ToString().Trim() + ", " + hcdesS), hcode1);  //.AutoSuggestionList[i] = this.lblFullName.Content.ToString().Trim() + ", " + hcdesS + " : [" + hcode1 + "]";
                            break;
                        }
                        i++;
                    }
                }

                this.IsNewRecord = false;
                System.Windows.MessageBox.Show("Successfully Updated HR Information!!", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information,
                               MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCM.Gen-2: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void lbldgBasicInfo1Slno_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm to add space", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                          MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            //string sirCode1 = this.AtxtPlcId.Value.ToString();
            string genCode1 = ((Label)sender).Tag.ToString();

            int index1 = this.dgBasicInfo1.SelectedIndex;// this.LCGeneralInfoList.FindLastIndex(x => x.actcode == genCode1);
            string gentype1 = ((vmEntryHRGenral1.HcmInfo1)this.dgBasicInfo1.Items[index1]).ValueType;
            string newRptNo = (int.Parse(((vmEntryHRGenral1.HcmInfo1)this.dgBasicInfo1.Items[index1]).repeatsl) + 1).ToString("000");

            this.dgBasicInfo1.Items.Insert(index1 + 1, new vmEntryHRGenral1.HcmInfo1()
            {
                slnum = "0",
                Code = genCode1,
                TitleDesc = "          Do",
                repeatsl = newRptNo,
                ValueType = gentype1,
                ValueDesc = "",

            });
            index1 = 1;
            foreach (vmEntryHRGenral1.HcmInfo1 item in this.dgBasicInfo1.Items)
            {
                item.slnum = index1.ToString("00") + ".";
                ++index1;
            }
            this.dgBasicInfo1.Items.Refresh();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.timerClick1.Start();
            this.stkId.Children.Clear();
        }

        private void timerClick1_Tick(object sender, EventArgs e)
        {
            if (timerClick1Counter == 0)
            {
                this.pgrsbar.Visibility = Visibility.Visible;
                timerClick1Counter = 1;
                return;
            }
            this.timerClick1Counter = 0;
            this.timerClick1.Stop();

            var pap = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, "%", "EXISTSTAFFS");
            DataSet ds = WpfProcessAccess.GetHmsDataSet(pap);
            if (ds == null)
                return;
            var stufflst = ds.Tables[0].DataTableToList<vmReportHCM1.Stafflist>();
            if (this.CmbSec.Items.Count > 0)
            {
                StuffLst01.Clear();
                string SectCod = ((ComboBoxItem)this.CmbSec.SelectedItem).Tag.ToString();
                if (SectCod.Substring(9, 3) != "000")
                {
                    StuffLst01 = stufflst.FindAll(x => x.deptcod == SectCod.ToString().Trim());
                    this.CreateGridData(StuffLst01);
                    // this.timerClick1.Start();                   
                }
                else
                {
                    StuffLst01 = stufflst.FindAll(x => x.deptcod.ToString().Substring(0, 9) == SectCod.ToString().Substring(0, 9));
                    this.CreateGridData(StuffLst01);
                    // this.timerClick1.Start(); 
                }
            }
            else
            {
                StuffLst01.Clear();
                string DeptCod = ((ComboBoxItem)this.CmbDept.SelectedItem).Tag.ToString();
                StuffLst01 = stufflst.FindAll(x => x.deptcod.Substring(0, 4) == DeptCod.Substring(0, 4).ToString().Trim());
                this.CreateGridData(StuffLst01);
                // this.timerClick1.Start();
            }
            // this.CreateGridData(StuffLst01);
            this.pgrsbar.Visibility = Visibility.Hidden;
        }


        private void CreateGridData(List<vmReportHCM1.Stafflist> stufflst)
        {
            //var pap2 =WpfProcessAccess.SetHRMList(id);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
            //var pap3 = WpfProcessAccess.SetHRMIMGList(id, id, "PHOTOSIGN1SIGN2");
            //DataSet ds3 = WpfProcessAccess.GetHmsDataSet(pap3);

            //WpfProcessAccess.GetCompanyStaffList();
            //var list = WpfProcessAccess.StaffList;
            //var pap = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, "%" );
            //DataSet ds = WpfProcessAccess.GetHmsDataSet(pap);
            //var stufflst = ds.Tables[0].DataTableToList<HmsEntityManpower.HCMStafflist>();

            //string id1 = stufflst[0].hccode;
            //string id2 = stufflst[20].hccode;
            //var pap1 = vmr1.SetHRMIMGList(id1, id2, "PHOTO");
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            //List<HmsEntityManpower.HRMIMG> imglist = ds1.Tables[0].DataTableToList<HmsEntityManpower.HRMIMG>();
            //int i = 0;
            //foreach (var x in imglist)
            //{
            //    stufflst[i].img = x.hcphoto;
            //    i++;
            //}
            int i = 1;
            foreach (var item in stufflst)
            {
                // byte[] hcphoto = imglist.Find(x => x.hccode == item.hccode).hcphoto;

                var pap2 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, item.hccode, "PHOTO");
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
                if (ds2 == null)
                    return;

                //Create Grid
                Grid DynamicGrid = new Grid();
                ////DynamicGrid.ShowGridLines = true;
                DynamicGrid.Style = (Style)FindResource("IdgrdStyle");

                DynamicGrid.Width = 360;
                DynamicGrid.Height = 200;
                DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
                DynamicGrid.VerticalAlignment = VerticalAlignment.Top;
                //  DynamicGrid.ShowGridLines = true;
                //  DynamicGrid.Background = new SolidColorBrush(Colors.White);
                ////DynamicGrid.MouseLeftButtonUp += new MouseButtonEventHandler(DynamicGrid_Mdown);
                //create border
                Border border = new Border();
                border.BorderBrush = System.Windows.Media.Brushes.Black;
                border.CornerRadius = new CornerRadius(5, 5, 5, 5);
                border.Margin = new Thickness(10, 10, 10, 10);
                border.BorderThickness = new Thickness(1, 1, 1, 1);
                ////border.Child = DynamicGrid;
                Label lbl1 = new Label() { Padding = new Thickness(0), BorderThickness = new Thickness(0) };
                lbl1.MouseDoubleClick += lbl1_MouseDoubleClick;
                lbl1.Content = DynamicGrid;
                border.Child = lbl1;
                // Create Colomn
                ColumnDefinition gridCol1 = new ColumnDefinition();
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol1.Width = new GridLength(270);
                DynamicGrid.ColumnDefinitions.Add(gridCol1);
                DynamicGrid.ColumnDefinitions.Add(gridCol2);              

                StackPanel stk1 = new StackPanel();
                stk1.Width = 270;
                stk1.HorizontalAlignment = HorizontalAlignment.Left;
                stk1.VerticalAlignment = VerticalAlignment.Center;
                stk1.Orientation = Orientation.Vertical;
                Grid.SetColumn(stk1, 0);



                StackPanel stk2 = new StackPanel();
                stk2.Width = 90;
                stk1.HorizontalAlignment = HorizontalAlignment.Right;
                //  stk2.VerticalAlignment = VerticalAlignment.Center;
                stk2.Orientation = Orientation.Vertical;
                Grid.SetColumn(stk2, 1);

                // Text box 
                TextBlock tblock6 = new TextBlock();
                tblock6.Style = (Style)FindResource("IdTxBOxStyle");
                tblock6.Foreground = new SolidColorBrush(Colors.Blue);
                tblock6.Height = 22;
                tblock6.FontSize = 13;
                tblock6.HorizontalAlignment = HorizontalAlignment.Left;
                tblock6.Text = " HR ID : " + item.hccode.Substring(6, 6);
                tblock6.Tag = item.hccode.ToString().Trim();
                // Text box 
                TextBlock tblock8 = new TextBlock();
                tblock8.Style = (Style)FindResource("IdTxBOxStyle");
                tblock8.Height = 22;
                tblock8.FontSize = 12;
                tblock8.HorizontalAlignment = HorizontalAlignment.Left;
                tblock8.Text = " ID Card No : " + item.idcardno;
                // Text box 
                TextBlock tblock1 = new TextBlock();
                tblock1.Style = (Style)FindResource("IdTxBOxStyle");
                //  tblock1.Foreground = new SolidColorBrush(Colors.Black);
                tblock1.Height = 22;
                tblock1.FontSize = 12;
                tblock1.HorizontalAlignment = HorizontalAlignment.Left;
                tblock1.Text = " Name : " + item.hcname;
                // Grid.SetColumn(tblock1, 0);
                // Text box 
                TextBlock tblock2 = new TextBlock();
                tblock2.Style = (Style)FindResource("IdTxBOxStyle");
                tblock2.Height = 22;
                tblock2.FontSize = 12;
                tblock2.HorizontalAlignment = HorizontalAlignment.Left;
                tblock2.Text = " Designation : " + item.designame;
                // Text box 
                TextBlock tblock3 = new TextBlock();
                tblock3.Style = (Style)FindResource("IdTxBOxStyle");
                tblock3.Height = 22;
                tblock3.FontSize = 12;
                tblock3.HorizontalAlignment = HorizontalAlignment.Left;
                tblock3.Text = " Department : " + item.deptname;
                // Text box 
                TextBlock tblock7 = new TextBlock();
                tblock7.Style = (Style)FindResource("IdTxBOxStyle");
                tblock7.Height = 22;
                tblock7.FontSize = 12;
                tblock7.HorizontalAlignment = HorizontalAlignment.Left;
                tblock7.Text = " Joining Date : " + item.joindat;
                // Text box 
                TextBlock tblock4 = new TextBlock();
                tblock4.Style = (Style)FindResource("IdTxBOxStyle");
                tblock4.Height = 22;
                tblock4.FontSize = 12;
                tblock4.HorizontalAlignment = HorizontalAlignment.Left;
                tblock4.Text = " Date Of Birth : " + item.birthdat;
                // Text box 
                TextBlock tblock5 = new TextBlock();
                tblock5.Style = (Style)FindResource("IdTxBOxStyle");
                tblock5.Height = 22;
                tblock5.FontSize = 12;
                tblock5.HorizontalAlignment = HorizontalAlignment.Left;
                tblock5.Text = " Blood Group : " + item.blodgrp;

                // Text box 
                TextBlock tblock9 = new TextBlock();
                tblock9.Style = (Style)FindResource("IdTxBOxStyle");
                tblock9.Height = 22;
                tblock9.Width = 60;
                tblock9.FontSize = 12;
                tblock9.HorizontalAlignment = HorizontalAlignment.Left;
                tblock9.Text = i.ToString() + " / " + stufflst.Count.ToString();

                // Text box 
                TextBlock tblock11 = new TextBlock();
                // tblock11.Style = (Style)FindResource("IdTxBOxStyle");
                tblock11.Height = 22;
                tblock11.Width = 70;
                tblock11.FontSize = 10;
                tblock11.HorizontalAlignment = HorizontalAlignment.Center;
                tblock11.Text = "Signature :";

                stk1.Children.Add(tblock9);
                stk1.Children.Add(tblock6);
                stk1.Children.Add(tblock8);
                stk1.Children.Add(tblock1);
                stk1.Children.Add(tblock2);
                stk1.Children.Add(tblock3);
                stk1.Children.Add(tblock7);
                stk1.Children.Add(tblock4);
                stk1.Children.Add(tblock5);

                DynamicGrid.Children.Add(stk1);
                DynamicGrid.Children.Add(stk2);

                //BitmapImage img = new BitmapImage(new Uri("/ASITHmsWpf;component/Images/NoUser.jpg", UriKind.Relative));
                System.Windows.Controls.Image Hcimg = new System.Windows.Controls.Image();
                Hcimg.Height = 80;
                Hcimg.Width = 70;
                Hcimg.ToolTip = "photo";


                if (!(ds2.Tables[0].Rows[0]["hcphoto"] is DBNull))
                {
                    byte[] bytes = (byte[])ds2.Tables[0].Rows[0]["hcphoto"];
                    MemoryStream mem = new MemoryStream(bytes);
                    BitmapImage bmp3 = new BitmapImage();
                    bmp3.BeginInit();
                    bmp3.StreamSource = mem;
                    bmp3.EndInit();
                    Hcimg.Source = bmp3;
                }


                stk2.Children.Add(Hcimg);
                //     stk2.Children.Add(tblock11);


                this.stkId.Children.Add(border);
                i++;
            }
        }

        void lbl1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DynamicGrid_Mdown(((Label)sender).Content, null);
        }

        private void DynamicGrid_Mdown(object sender, EventArgs e)
        {
            Grid detailGrid = (sender as Grid);
            StackPanel stk = (StackPanel)detailGrid.Children[0];
            TextBlock textBlock = stk.Children[1] as TextBlock;//. .OfType<TextBlock>().FirstOrDefault();
            //string txt = textBlock.Text;
            //string[] str = txt.Split(':');

            //MessageBox.Show(txt);

            //string hcid1 = str[1].ToString().Trim();
            string hcid1 = textBlock.Tag.ToString().Trim();
            LstEmpphoto.Clear();

            var pap1 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "BASICINFO1");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var EmpInfoList = ds1.Tables[0].DataTableToList<HmsEntityManpower.HcmInfoTable>();

            var pap2 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "PHOTO");
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
            if (ds2 == null)
                return;

            if (!(ds2.Tables[0].Rows[0]["hcphoto"] is DBNull))
            {
                this.LstEmpphoto.Add(new HmsEntityManpower.hcphoto() { photo = (byte[])ds2.Tables[0].Rows[0]["hcphoto"] });    // { photo = (byte[])ds2.Tables[0].Rows[0]["hcphoto"] });
            }

            var pap3 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "SIGN1");
            DataSet ds3 = WpfProcessAccess.GetHmsDataSet(pap3);
            if (ds3 == null)
                return;

            if (!(ds3.Tables[0].Rows[0]["hcinisign"] is DBNull))
            {
                this.LstEmpphoto.Add(new HmsEntityManpower.hcphoto() { photo = (byte[])ds3.Tables[0].Rows[0]["hcinisign"] });
            }
            var pap4 = vm1.SetParamShowHCInfo(WpfProcessAccess.CompInfList[0].comcpcod, hcid1, "SIGN2");
            DataSet ds4 = WpfProcessAccess.GetHmsDataSet(pap4);
            if (ds4 == null)
                return;
            if (!(ds4.Tables[0].Rows[0]["hcfullsign"] is DBNull))
            {
                this.LstEmpphoto.Add(new HmsEntityManpower.hcphoto() { photo = (byte[])ds4.Tables[0].Rows[0]["hcfullsign"] });
            }

            //int x = (ChkEdu.IsChecked == true ? 0 : 1);
            //int y = (ChkJob.IsChecked == true ? 0 : 1);
            int[] chk = new int[2] { 0, 0 };

            LocalReport rpt1 = HcmReportSetup.GetLocalReport("HcmInfo.RptHcmGenInf01", EmpInfoList, LstEmpphoto, null, chk);
            if (rpt1 == null)
                return;

            string WindowTitle1 = "Human Resource General Information Report";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode, IsTopMost: true);

        }
    }
}
