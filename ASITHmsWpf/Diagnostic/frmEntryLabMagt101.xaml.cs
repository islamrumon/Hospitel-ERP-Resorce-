using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.General;
using ASITHmsViewMan.Diagnostic;
using System;
using System.Collections;
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
using ASITHmsWpf.UserControls;
using ASITHmsRpt5Diagnostic;
using Microsoft.Reporting.WinForms;
using System.IO;
using ASITHmsViewMan.Commercial;


namespace ASITHmsWpf.Diagnostic
{
    /// <summary>
    /// Interaction logic for frmEntryLabMagt101.xaml
    /// </summary>
    public partial class frmEntryLabMagt101 : UserControl
    {
        private bool FrmInitialized = false;
        private List<HmsEntityCommercial.CommInvSummInf> CommInvSummList = new List<HmsEntityCommercial.CommInvSummInf>();             // Hospital/Diagnostic Centre Commercial Invoice Summary List
        private List<HmsEntityGeneral.AcInfCodeBook> TestElementList = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<HmsEntityGeneral.AcInfCodeBook> ListSpeciman = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<vmEntryLabMgt1.DiagSpecimanReceive> ListSpecimanRecv = new List<vmEntryLabMgt1.DiagSpecimanReceive>();
        private List<vmEntryLabMgt1.DiagTemplateDetails> ListDiagTemplateDetails = new List<vmEntryLabMgt1.DiagTemplateDetails>();
        private List<vmEntryLabMgt1.DiagRptTemplate> ListRptDgTemplate = new List<vmEntryLabMgt1.DiagRptTemplate>();
        private List<vmEntryLabMgt1.DiagRptTemplate> ListRptSavedTemplate = new List<vmEntryLabMgt1.DiagRptTemplate>();
        private List<vmEntryLabMgt1.DiagTestTitle> ListTestTitle = new List<vmEntryLabMgt1.DiagTestTitle>();

        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private vmReportFrontDesk1 vmr = new vmReportFrontDesk1();
        private vmEntryLabMgt1 vm1 = new vmEntryLabMgt1();
        public frmEntryLabMagt101()
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
                this.CleanUpScreen();
            }
        }

        private void ActivateAuthObjects()
        {

        }
        private void Objects_On_Init()
        {
            try
            {
                string tag1 = this.Tag.ToString();
                ////this.Uid = (tag1.Contains("Receive") ? "SAMPLERECEIVE" : (tag1.Contains("Preparation") ? "REPORTDOCS" : (tag1.Contains("Submission") ?  "REPORTSUBMIT" : "NOTHING")));
                
                this.lblSubTitle.Content = this.Tag.ToString();
                this.lblSubTitle.Uid = this.Uid.ToString();

                this.GetBranchList();
                ////this.GetServiceItemList(itrmGroup: "4502%");

                this.xctk_dtpSrchDat1.Value = DateTime.Today.AddDays(-3);
                this.xctk_dtpSrchDat2.Value = DateTime.Today;
                this.cmbPrevYearMon.Items.Clear();

                DateTime dt1 = DateTime.Parse("01-" + DateTime.Today.ToString("MMM-yyyy"));
                for (int i = 0; i > -10; i--)
                    this.cmbPrevYearMon.Items.Add(new ComboBoxItem()
                    {
                        Content = dt1.AddMonths(i).ToString("yyMM"),
                        Tag = dt1.AddMonths(i).ToString("yyyyMM"),
                        Width = 50,
                        HorizontalContentAlignment = HorizontalAlignment.Left
                    });

                this.cmbPrevYearMon.SelectedIndex = 0;

                var pap1 = this.vmGenList1.SetParamGeneralInfoCodeBook("SILBRPT");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.TestElementList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.AcInfCodeBook>().FindAll(x => x.actcode.Substring(9, 3) != "000");
                foreach (var item in this.TestElementList)
                    item.actdesc = item.actdesc.Trim() + (item.actcode.Substring(0, 9) == "SILBRPT03" ? " " + item.acttdesc.Trim() : "");

                this.TestElementList = this.TestElementList.OrderBy(x => x.actdesc).ToList();

                this.ListSpeciman = this.TestElementList.FindAll(x => x.actcode.Substring(9, 3) != "000" && x.actcode.Substring(0, 9) == "SILBRPT02" && x.actcode != "SILBRPT02001").OrderBy(y => y.actdesc).ToList();

                this.stkpPrint.Visibility = Visibility.Collapsed;
                if (this.Uid.ToString().Contains("SAMPLERECEIVE"))
                {
                    this.stkpSampleRecv.Visibility = Visibility.Visible;
                    this.chkSpecReceiveInfo.IsChecked = false;
                    this.chkSpecReceiveInfo.Visibility = Visibility.Hidden;
                    this.stkpSampleRecv.Orientation = Orientation.Horizontal;
                    this.stkpReportWrite.Visibility = Visibility.Collapsed;
                    this.dgvSpRecv.Width = 310 + 135;
                    this.dgvSpRecv.Columns[2].Width = 130 + 135;
                    ContextMenu ctmSpecCodeType = new ContextMenu() { Height = 400 };
                    foreach (var item1 in this.ListSpeciman)
                    {
                        MenuItem miSpecType1 = new MenuItem() { Header = item1.actdesc.Trim(), Tag = item1.actcode };
                        miSpecType1.Click += miSpecType1_Click;
                        ctmSpecCodeType.Items.Add(miSpecType1);
                    }
                    this.autoSpecimanSearch.ContextMenu = ctmSpecCodeType;
                }
                else //if(this.Uid.ToString().Contains("REPORTDOCS"))  // if(this.Uid.ToString().Contains("REPORTSUBMIT"))
                {
                    //this.stkpAddSpeciman.Visibility = (this.Uid.ToString().Contains("REPORTDOCS") ? Visibility.Hidden : Visibility.Visible);
                    this.chkSpecReceiveInfo.IsChecked = true;
                    this.stkpSampleRecv.Visibility = (this.chkSpecReceiveInfo.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
                    this.stkpSampleRecv.IsEnabled = (this.chkSpecReceiveInfo.IsChecked == true ? true : false);
                    this.stkpAddSpeciman.Visibility = Visibility.Hidden;

                    this.autoSpecimanSearch2_MenuBind();
                    this.autoTitleSearch_MenuBind();
                }

                if (WpfProcessAccess.StaffList.Count == 0)
                    WpfProcessAccess.GetCompanyStaffList();

                this.cmbRptVerifiedBy.Items.Add(new ComboBoxItem() { Content = "(Manual Seal of Consultant)", Tag = "000000000000" });
                var LabConsultantList = WpfProcessAccess.StaffList.FindAll(x => x.sircode.Substring(0, 2) == "95" && x.sirtype.ToUpper().Contains("REPORTING CONSULTANT"));
                foreach (var item in LabConsultantList)
                {
                    this.cmbRptVerifiedBy.Items.Add(new ComboBoxItem() { Content = item.sirdesc.Trim(), Tag = item.sircode });
                }
                this.cmbRptVerifiedBy.SelectedIndex = 0;

                this.cmbRptLabStaff1.Items.Add(new ComboBoxItem() { Content = "(Manual Seal of Staff)", Tag = "000000000000" });
                this.cmbRptLabStaff2.Items.Add(new ComboBoxItem() { Content = "(Manual Seal of Staff)", Tag = "000000000000" });
                var LabStaffist = WpfProcessAccess.StaffList.FindAll(x => x.sircode.Substring(0, 2) == "95" && x.sirtype.ToUpper().Contains("REPORTING LAB STAFF"));
                foreach (var item in LabStaffist)
                {
                    this.cmbRptLabStaff1.Items.Add(new ComboBoxItem() { Content = item.sirdesc.Trim(), Tag = item.sircode });
                    this.cmbRptLabStaff2.Items.Add(new ComboBoxItem() { Content = item.sirdesc.Trim(), Tag = item.sircode });
                }
                this.cmbRptLabStaff1.SelectedIndex = 0;
                this.cmbRptLabStaff2.SelectedIndex = 0;
                this.FindBrnchForThisTerminal();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("DSR-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void FindBrnchForThisTerminal()
        {
            if (WpfProcessAccess.GenInfoTitleList == null)
                WpfProcessAccess.GetGenInfoTitleList();

            string comcod1 = WpfProcessAccess.CompInfList[0].comcod;
            string TerminalID1 = WpfProcessAccess.SignedInUserList[0].terminalID.ToUpper();

            var TerminalList1 = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 8) == "CBWS" + WpfProcessAccess.CompInfList[0].comcod
                                && x.acttdesc.Trim().ToUpper() == TerminalID1 && x.actelev.Trim().Length == 4).ToList();
            int li = 0;
            if (TerminalList1.Count > 0)
            {
                string brncod1 = TerminalList1[0].actelev.Trim();
                foreach (ComboBoxItem item in this.cmbBranch.Items)
                {
                    if (item.Tag.ToString().ToUpper().Trim() == brncod1)
                    {
                        this.cmbBranch.SelectedIndex = li;
                        break;
                    }
                    li++;
                }
            }
        }
        private void miSpecType1_Click(object sender, RoutedEventArgs e)
        {

            MenuItem mi1 = (MenuItem)sender;
            string specCod1 = mi1.Tag.ToString();
            string specDesc1 = mi1.Header.ToString().Trim();
            this.AddSpecimanToGrid(specCod1, specDesc1);
        }

        private void AddSpecimanToGrid(string specCod1, string specDesc1)
        {
            this.dgvSpRecv.ItemsSource = null;
            int cnt1 = this.ListSpecimanRecv.Count;
            this.ListSpecimanRecv.Add(new vmEntryLabMgt1.DiagSpecimanReceive()
            {
                slnum = cnt1 + 1,
                specid = specCod1,
                specdesc = specDesc1,
                volsize = "",
                sprcvtime = DateTime.Now,
                sprmrks = "",
                isenabled = true,
                rcvbyid = WpfProcessAccess.SignedInUserList[0].hccode,
                rcvses = WpfProcessAccess.SignedInUserList[0].sessionID,
                rcvtrm = WpfProcessAccess.SignedInUserList[0].terminalID
            });
            this.dgvSpRecv.ItemsSource = this.ListSpecimanRecv;
            var item22 = this.ListSpecimanRecv.FindAll(x => x.slnum == (cnt1 + 1));
            if (item22.Count > 0)
            {
                this.dgvSpRecv.ScrollIntoView(item22[0]);
            }
            this.btnUpdateTrans.Visibility = Visibility.Visible;
        }


        private void GetBranchList()
        {
            this.cmbBranch.Items.Clear();
            var brnlist1 = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            foreach (var item in brnlist1)
                this.cmbBranch.Items.Add(new ComboBoxItem()
                {
                    Content = item.brnnam.Trim() + " (" + item.brnsnam.Trim() + ")",
                    Tag = item.brncod,
                    Uid = item.brnsnam.Trim(),
                    ToolTip = item.brnnam.Trim() + " (" + item.brnsnam.Trim() + ")"
                });

            this.cmbBranch.SelectedIndex = 0;
        }
        private void CleanUpScreen()
        {
            this.txtInvDate.Text = DateTime.Today.ToString("dd-MMM-yyyy hh:mm tt");
            this.stkpHeader.ToolTip = null;
            this.stkpTransList.Visibility = Visibility.Collapsed;
            this.stkpTransList.IsEnabled = false;
            this.btnUpdateTrans.Visibility = Visibility.Hidden;
            this.ListSpecimanRecv.Clear();
            this.ListTestTitle.Clear();
            this.ListRptSavedTemplate.Clear();
            this.ListDiagTemplateDetails.Clear();
            this.txtPrevTransID.Text = "";
            this.chkElement.IsChecked = false;
            this.stkpAddElement.Visibility = Visibility.Collapsed;
            this.chkBranchName.IsChecked = false;
            this.chkInvoiceNo.IsChecked = true;
            this.txtInvNum.Text = "";
            this.txtInvNum.Tag = "";
            this.txtMachine.Text = "";
            this.txtPrevTransID.Text = "";
            this.imgPatPhoto.Source = this.imgEmptyPhoto.Source;
            this.stkpEntrySpecimanRcv.Visibility = Visibility.Collapsed;
            this.stkpEntrySpecimanRcv.IsEnabled = false;
            this.stkpTitleMaster.IsEnabled = true;
            this.stkpTitle2.Visibility = Visibility.Hidden;
            this.stkpPhoto.Visibility = Visibility.Hidden;
            this.stkpRptWrite.Visibility = Visibility.Hidden;
            this.stkpRptWrite.IsEnabled = false;
            this.dgvRpt.ItemsSource = null;
            this.ListRptDgTemplate.Clear();

        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnTransList_Click(object sender, RoutedEventArgs e)
        {
            ////this.HideRefItemList1();
            if (this.stkpTransList.Visibility == Visibility.Visible)
            {
                this.stkpTransList.Visibility = Visibility.Collapsed;
                this.stkpTransList.IsEnabled = false;
                if (this.btnNextShow.Content.ToString() == "_Next")
                {
                    this.stkpEntrySpecimanRcv.Visibility = Visibility.Visible;
                    this.stkpEntrySpecimanRcv.IsEnabled = true;
                    ////this.stkpTitle2.Visibility = Visibility.Visible;
                    ////this.stkpPhoto.Visibility = Visibility.Visible;
                }
                this.btnTransList.Content = "Show Transaction List";
                return;
            }
            this.stkpEntrySpecimanRcv.Visibility = Visibility.Collapsed;
            this.stkpEntrySpecimanRcv.IsEnabled = false;
            ////this.stkpTitle2.Visibility = Visibility.Hidden;
            ////this.stkpPhoto.Visibility = Visibility.Hidden;
            this.stkpTransList.Visibility = Visibility.Visible;
            this.stkpTransList.IsEnabled = true;
            this.btnTransList.Content = "Hide Transaction List";
            this.btnFilter1_Click(null, null);
        }

        private void btnNextShow_Click(object sender, RoutedEventArgs e)
        {
            this.btnShowReport.Content = "Show";
            this.cmbRptTitle.IsEnabled = true;
            this.stkpAddNewReport.Visibility = Visibility.Visible;
            this.stkpPrint.Visibility = Visibility.Collapsed;
            if (this.btnNextShow.Content.ToString() == "_Next")
            {
                ////this.btnUpdateTrans.Tag = "Ok";
                this.CleanUpScreen();
                this.stkpTitle1.IsEnabled = true;
                this.btnNextShow.Content = "_Ok";
                this.btnNextShow.Focusable = true;
                this.txtInvNum.Focus();
                return;
            }
            if (!this.ShowSpecimanReceiveInfo())
                return;

            this.btnNextShow.Content = "_Next";
            this.btnNextShow.Focusable = false;
            this.stkpTitleMaster.IsEnabled = false;
            this.stkpTitle2.Visibility = Visibility.Visible;
            this.stkpPhoto.Visibility = Visibility.Visible;
            this.stkpTransList.Visibility = Visibility.Collapsed;
            this.stkpTransList.IsEnabled = false;
            this.btnTransList.Content = "Show Transaction List";
        }


        private bool ShowSpecimanReceiveInfo()
        {
            //var pap1 = vm1.SetParamToGetSpecimanInfo(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: "SPECIMANRECEIVE01", BrnchCod: BrnchCod1,
            //    InvNum: "CSI201802110100001", startDate: "01-Feb-2018 12:00 AM", EndDate: "28-Feb-2018 11:59 PM", OrderBy: "DEFAULT");

            string BrnchCod1 = ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString().Trim();
            string ym1 = ((ComboBoxItem)this.cmbPrevYearMon.SelectedItem).Tag.ToString().Trim();
            string InvNum1 = "CSI" + ym1 + BrnchCod1 + this.txtPrevTransID.Text.Trim();
            string startDate1 = "01-Jan-" + ym1.Substring(0, 4) + " 12:00 AM";
            string EndDate1 = "31-Dec-" + ym1.Substring(0, 4) + " 11:59 PM";

            if (InvNum1.Length < 18)
                return false;

            var pap1 = vm1.SetParamToGetSpecimanInfo(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: "SPECIMANRECEIVE01", BrnchCod: BrnchCod1,
                InvNum: InvNum1, startDate: startDate1, EndDate: EndDate1, OrderBy: "DEFAULT");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return false;

            if (ds1.Tables.Count == 0)
                return false;

            if (ds1.Tables[2].Rows.Count == 0)
                return false;

            this.ShowInvoiceGenInfo(ds1.Tables[0], ds1.Tables[1]);

            var list1 = ds1.Tables[2].DataTableToList<vmEntryLabMgt1.DiagTestItemDetails>();
            string gc1 = "XXXXXXXXXXXX";
            foreach (var item in list1)
            {
                item.gsirdesc = item.gsirdesc.Trim();
                item.sirdesc = "     " + item.slnum.ToString("00") + ". " + item.sirdesc.Trim();
                if (item.gisircode != gc1)
                {
                    gc1 = item.gisircode;
                    item.comcod = "Visible";
                }
                else
                {
                    item.gsirdesc = "";
                    item.comcod = "Collapsed"; // "Visible";// 
                }
            }

            this.dgvSpDetails.ItemsSource = list1;// ds1.Tables[2].DefaultView;
            this.dgvSpRecv.ItemsSource = null;
            this.ListSpecimanRecv.Clear();
            this.ListTestTitle.Clear();
            this.ListSpecimanRecv = ds1.Tables[3].DataTableToList<vmEntryLabMgt1.DiagSpecimanReceive>();
            this.ListTestTitle = ds1.Tables[4].DataTableToList<vmEntryLabMgt1.DiagTestTitle>();


            if (this.Uid.ToString().Contains("SAMPLERECEIVE"))
            {
                foreach (var item in this.ListSpecimanRecv)
                    item.isenabled = true;

                if (this.ListSpecimanRecv.Count > 0)
                    this.btnUpdateTrans.Visibility = Visibility.Visible;
            }


            this.dgvSpRecv.ItemsSource = this.ListSpecimanRecv;

            this.cmbRptTitle.Items.Clear();
            foreach (var item in this.ListTestTitle)
            {
                item.machdesc = (item.machcod == "SILBRPT03001" ? "" : item.machdesc.Trim());
                this.cmbRptTitle.Items.Add(new ComboBoxItem()
                {
                    Content = item.mtitledes.Trim(),
                    Tag = item.mtitlecod.Trim(),
                    Uid = item.machcod.Trim(),
                    ToolTip = "Machine Used : " + item.machdesc.Trim()
                });
            }

            if (this.cmbRptTitle.Items.Count > 0)
                this.cmbRptTitle.SelectedIndex = 0;

            this.ListRptSavedTemplate = ds1.Tables[5].DataTableToList<vmEntryLabMgt1.DiagRptTemplate>();
            this.ListDiagTemplateDetails = ds1.Tables[6].DataTableToList<vmEntryLabMgt1.DiagTemplateDetails>();

            this.stkpEntrySpecimanRcv.Visibility = Visibility.Visible;
            this.stkpEntrySpecimanRcv.IsEnabled = true;
            return true;
        }

        private void ShowInvoiceGenInfo(DataTable tbl1, DataTable tbl2)
        {

            DataRow dr1 = tbl1.Rows[0];
            string memoNum = dr1["ptinvnum"].ToString();
            string ptinvnum2 = dr1["ptinvnum2"].ToString();
            this.txtInvNum.Text = ptinvnum2;
            this.txtInvNum.Tag = memoNum;

            this.txtPatientName.Text = dr1["ptname"].ToString().Trim();
            this.txtContactNo.Text = dr1["ptphone"].ToString().Trim();
            this.txtPatientAge.Text = dr1["ptage"].ToString().Trim();
            this.txtPatientGender.Text = dr1["ptgender"].ToString().Trim();
            this.txtMemberID.Text = dr1["refcardno"].ToString().Trim();
            this.txtDeliveryTime.Text = Convert.ToDateTime(dr1["delivartime"]).ToString("dd-MMM-yyyy hh:mm tt");
            this.txtRefByName.Text = dr1["rfFullName"].ToString().Trim() + dr1["ptrefnote"].ToString().Trim();
            this.stkpHeader.ToolTip = "Ref. By Name : " + this.txtRefByName.Text;

            //  this.txtPrevTransID.Text = ptinvnum2.Substring(6, 5);

            this.txtInvDate.Text = Convert.ToDateTime(dr1["ptinvdat"]).ToString("dd-MMM-yyyy hh:mm tt");

            foreach (var item in this.cmbBranch.Items)
            {
                if (((ComboBoxItem)item).Tag.ToString().Trim() == memoNum.Substring(9, 4))
                {
                    this.cmbBranch.SelectedItem = item;
                    break;
                }
            }
            this.txtPrevTransID.Text = memoNum.Substring(13, 5);

            bool foundYM1 = false;
            foreach (var item in this.cmbPrevYearMon.Items)
            {
                if (((ComboBoxItem)item).Tag.ToString().Trim() == memoNum.Substring(3, 6))
                {
                    foundYM1 = true;
                    this.cmbPrevYearMon.SelectedItem = item;
                    break;
                }
            }

            if (foundYM1 == false)
            {
                ComboBoxItem cbi1 = new ComboBoxItem() { Content = memoNum.Substring(5, 4), Tag = memoNum.Substring(3, 6), Width = 50, HorizontalContentAlignment = HorizontalAlignment.Left };
                this.cmbPrevYearMon.Items.Add(cbi1);
                this.cmbPrevYearMon.SelectedItem = cbi1;
            }

            if (!(tbl2.Rows[0]["ptphoto"] is DBNull))
            {
                byte[] byteSi = (byte[])tbl2.Rows[0]["ptphoto"];
                MemoryStream mem1 = new MemoryStream(byteSi);
                if (mem1.Length > 0)
                {
                    BitmapImage bmp4 = new BitmapImage();
                    bmp4.BeginInit();
                    bmp4.StreamSource = mem1;
                    bmp4.EndInit();
                    this.imgPatPhoto.Source = bmp4;
                }
            }
        }

        private void cmbBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (this.cmbBranch.SelectedItem == null || this.stkpTransList == null)
                return;

            this.cmbBranch.ToolTip = ((ComboBoxItem)this.cmbBranch.SelectedItem).Content.ToString();

            if (this.stkpTransList.Visibility == Visibility.Visible)
                this.btnFilter1_Click(null, null);
        }


        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateSpecimanReceive();

            if (sender != null)
                System.Windows.MessageBox.Show("Information Updated Successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                        MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void UpdateSpecimanReceive()
        {
            //--- ToDo List For Data Update -- Tobe Done When Finalized
            // Retrive Saved Data First
            // Removed Target Date From Retrived Data
            // Insert Corrected/New Data With Retrived Data
            // Finally Send The Corrected Data for Update
            //System.Windows.MessageBox.Show("!! Warning !! Final Update Pending\nHafiz -- 16-Mar-2018", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
            //    MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

            this.BackupCancelInvoice(this.txtInvNum.Tag.ToString().Trim(), "BACKUP", this.Uid.ToString());

            if (this.Uid.ToString().Contains("SAMPLERECEIVE"))
                this.btndgvSpRecvHeader_Click(null, null);

            else if (this.Uid.ToString().Contains("REPORTDOCS"))
            {
                var ListRptDgTemplate2 = this.ListRptDgTemplate.ToList();

                string mtitlecod1 = ((ComboBoxItem)this.cmbRptTitle.SelectedItem).Tag.ToString();


                //   var test1a = this.ListTestTitle.Find(x => x.mtitlecod == mtitlecod1);

                string TitleId1 = mtitlecod1.Substring(0, 12); //this.txtRptTitle.Tag.ToString(); // mtitlecod1.Substring(0, 12);
                string Titledesc1 = this.txtRptTitle.Text.Trim();

                string SpecmId1 = mtitlecod1.Substring(12, 12); //this.txtSpecimen.Tag.ToString();// 
                string Spedesc1 = this.txtSpecimen.Text.Trim();

                string Machcod1 = this.txtMachine.Tag.ToString();
                string Machdesc1 = this.txtMachine.Text.Trim();

                string CombCod1 = TitleId1.Substring(7, 5) + SpecmId1.Substring(7, 5) + "00";

                string RptVerifyID = ((ComboBoxItem)this.cmbRptVerifiedBy.SelectedItem).Tag.ToString();
                
                RptVerifyID = RptVerifyID + "," + this.xctk_dtpRptTime.Text;
                RptVerifyID = RptVerifyID + "," + WpfProcessAccess.SignedInUserList[0].hccode + "," + WpfProcessAccess.SignedInUserList[0].sessionID + "," + WpfProcessAccess.SignedInUserList[0].terminalID;
                string RptAssistID = ((ComboBoxItem)this.cmbRptLabStaff1.SelectedItem).Tag.ToString();  // WpfProcessAccess.SignedInUserList[0].hccode;
                RptAssistID = RptAssistID + ((ComboBoxItem)this.cmbRptLabStaff2.SelectedItem).Tag.ToString();

                //ListRptDgTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(CombCod1, 0, 1, test1a.titlecod, test1a.titledesc.Trim(), RptVerifyID, RptAssistID, "", "N", true, "B"));
                //ListRptDgTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(CombCod1, 0, 2, test1a.specode, test1a.spedesc.Trim(), "", "", "", "N", true, "B"));
                //ListRptDgTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(CombCod1, 0, 3, test1a.machcod, test1a.machdesc.Trim(), "", "", "", "N", true, "B"));

                ListRptDgTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(CombCod1, 0, 1, TitleId1, Titledesc1, RptVerifyID, RptAssistID, "", "N", true, "B"));
                ListRptDgTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(CombCod1, 0, 2, SpecmId1, Spedesc1, "", "", "", "N", true, "B"));
                ListRptDgTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(CombCod1, 0, 3, Machcod1, Machdesc1, "", "", "", "N", true, "B"));


                ListRptDgTemplate2.Sort(delegate(vmEntryLabMgt1.DiagRptTemplate x, vmEntryLabMgt1.DiagRptTemplate y)
                {
                    return (x.elgrpsl.ToString("00") + x.elressl.ToString("00")).CompareTo(y.elgrpsl.ToString("00") + y.elressl.ToString("00"));
                });

                this.ListRptSavedTemplate = this.ListRptSavedTemplate.FindAll(x => x.isircode != CombCod1).ToList();
                this.ListRptSavedTemplate = this.ListRptSavedTemplate.Union(ListRptDgTemplate2).ToList();
            }

            DataSet ds1 = vm1.GetDataSetForUpdatePatientReport01(this.ListSpecimanRecv, this.ListRptSavedTemplate);

            //DateTime Date1 = DateTime.Parse("01" + ((ComboBoxItem)this.cmbInfoMonth.SelectedItem).Content.ToString().Trim());
            //string monthid1 = Date1.ToString("yyyyMM");
            //string hccode1 = this.AtxtEmpAll.Value;
            string memoDate1 = this.txtInvDate.Text.Trim();
            string NewEdit = "Edit";// this.ListSchAttn1[0].newedit.Trim();

            string memoNum1 = this.txtInvNum.Tag.ToString();
            var pap1 = vm1.SetParamForPatientRptUpdate(WpfProcessAccess.CompInfList[0].comcpcod, memoNum1, memoDate1, ds1, NewEdit);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
            {
                System.Windows.MessageBox.Show("Information Updated fail", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                               MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void dgvTransList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (this.CommInvSummList.Count == 0)
                    return;

                if (this.dgvTransList.SelectedItem == null)
                    return;

                string MemoNum = ((HmsEntityCommercial.CommInvSummInf)this.dgvTransList.SelectedItem).ptinvnum;
                string MemoNum2 = ((HmsEntityCommercial.CommInvSummInf)this.dgvTransList.SelectedItem).ptinvnum2;
                //((ListBoxItem)this.lstPrevTransList.SelectedItem).Tag.ToString();
                //string memoType1 = ((ComboBoxItem)this.cmbPrnMemoType2.SelectedItem).Tag.ToString();
                string memoDate1 = ((HmsEntityCommercial.CommInvSummInf)this.dgvTransList.SelectedItem).ptinvdat.ToString("dd-MMM-yyyy hh:mm tt");

                this.CleanUpScreen();
                this.btnNextShow.Content = "_Ok";
                this.txtInvNum.Text = MemoNum2;
                this.txtInvNum_LostFocus(null, null);

                this.btnNextShow_Click(null, null);
                //this.ViewPrintMemo(memoNum: MemoNum, memoDate: memoDate1);
                //this.ViewPrintMemo(MemoNum, PrnOpt1, "", memoType1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop,
                               MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberOnlyValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9+-.,]+");
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtPrevTransID_LostFocus(object sender, RoutedEventArgs e)
        {
            this.txtPrevTransID.Text = (this.txtPrevTransID.Text.Trim().Length > 0 ? ASITUtility.Right("00000" + this.txtPrevTransID.Text.Trim(), 5) : "");
        }

        private void txtInvNum_LostFocus(object sender, RoutedEventArgs e)
        {
            string InvNum1a = this.txtInvNum.Text.Trim();
            if (InvNum1a.Length != 11)
                return;

            foreach (var item in this.cmbBranch.Items)
            {
                if (((ComboBoxItem)item).Uid.ToString().Trim() == InvNum1a.Substring(0, 2))
                {
                    this.cmbBranch.SelectedItem = item;
                    break;
                }
            }

            bool foundYM1 = false;
            foreach (var item in this.cmbPrevYearMon.Items)
            {
                if (((ComboBoxItem)item).Tag.ToString().Trim() == InvNum1a.Substring(2, 6))
                {
                    foundYM1 = true;
                    this.cmbPrevYearMon.SelectedItem = item;
                    break;
                }
            }

            if (foundYM1 == false)
            {
                ComboBoxItem cbi1 = new ComboBoxItem() { Content = InvNum1a.Substring(2, 4), Tag = "20" + InvNum1a.Substring(2, 4), Width = 50, HorizontalContentAlignment = HorizontalAlignment.Left };
                this.cmbPrevYearMon.Items.Add(cbi1);
                this.cmbPrevYearMon.SelectedItem = cbi1;
            }
            this.txtPrevTransID.Text = InvNum1a.Substring(6, 5);

            string InvNum1b = ((ComboBoxItem)this.cmbBranch.SelectedItem).Uid.ToString().Trim() + ((ComboBoxItem)this.cmbPrevYearMon.SelectedItem).Content.ToString().Trim() + this.txtPrevTransID.Text.Trim();
            if (InvNum1a != InvNum1b)
            {
                this.txtInvNum.Text = "";
                this.txtPrevTransID.Text = "";
            }
        }


        private void btndgvSpRecvHeader_Click(object sender, RoutedEventArgs e)
        {
            this.dgvSpRecv.ItemsSource = null;
            this.ListSpecimanRecv = this.ListSpecimanRecv.FindAll(x => x.volsize.Trim().Length > 0);
            this.ListSpecimanRecv.Sort(delegate(vmEntryLabMgt1.DiagSpecimanReceive x, vmEntryLabMgt1.DiagSpecimanReceive y)
            {
                return (x.slnum.ToString("00")).CompareTo(y.slnum.ToString("00"));
            });
            int sl1 = 1;
            foreach (var item in this.ListSpecimanRecv)
            {
                item.slnum = sl1;
                ++sl1;
            }
            this.dgvSpRecv.ItemsSource = this.ListSpecimanRecv;
        }

        private void btnAddSpeciman_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoSpecimanSearch.SelectedValue == null)
                return;

            string specCod1 = this.autoSpecimanSearch.SelectedValue.ToString().Trim();
            string specDesc1 = this.autoSpecimanSearch.SelectedText.Trim();
            this.AddSpecimanToGrid(specCod1, specDesc1);
            this.autoSpecimanSearch.SelectedValue = null;
        }
        private void btnRemoveSpeciman_Click(object sender, RoutedEventArgs e)
        {
            int index1 = this.dgvSpRecv.SelectedIndex;
            if (index1 < 0)
                return;
            var item1 = this.ListSpecimanRecv[index1];
            MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to delete item\n" + item1.slnum.ToString("00") + ". " + item1.specdesc.Trim(),
                                  WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (msgresult != MessageBoxResult.Yes)
                return;

            this.ListSpecimanRecv.RemoveAt(index1);
            this.dgvSpRecv.Items.Refresh();

        }
        private void autoSpecimanSearch_PatternChanged(object sender, AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetSpecimanDesc(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.AcInfCodeBook> GetSpecimanDesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityGeneral.AcInfCodeBook>(
                this.ListSpeciman.Where((x, match) => (x.actdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void btnShowReport_Click(object sender, RoutedEventArgs e)
        {
            this.dgvRpt.ItemsSource = null;
            this.ListRptDgTemplate.Clear();
            this.stkpRptWrite.Visibility = Visibility.Hidden;
            this.stkpRptWrite.IsEnabled = false;
            this.btnUpdateTrans.Visibility = Visibility.Hidden;
            this.stkpPrint.Visibility = Visibility.Collapsed;
            this.stkpAddNewReport.Visibility = Visibility.Collapsed;
            this.cmbRptVerifiedBy.SelectedIndex = 0;
            this.cmbRptLabStaff1.SelectedIndex = 0;
            this.cmbRptLabStaff2.SelectedIndex = 0;
            this.cmbRptTitle.IsEnabled = true;
            if (this.btnShowReport.Content.ToString() == "Change")
            {
                this.cmbRptTitle.IsEnabled = true;
                this.stkpAddNewReport.Visibility = Visibility.Visible;
                this.btnShowReport.Content = "Show";
                return;
            }

            string mtitlecod1 = ((ComboBoxItem)this.cmbRptTitle.SelectedItem).Tag.ToString();
            var test1a = this.ListTestTitle.Find(x => x.mtitlecod == mtitlecod1);
            this.txtMachine.Text = test1a.machdesc;// ((ComboBoxItem)this.cmbRptTitle.SelectedItem).ToolTip.ToString();

            string TitleId1 = mtitlecod1.Substring(0, 12);
            string SpecmId1 = mtitlecod1.Substring(12, 12);
            string CombCod1 = TitleId1.Substring(7, 5) + SpecmId1.Substring(7, 5) + "00";

            foreach (ComboBoxItem item in this.cmbElementType.Items)
            {
                if (item.Tag.ToString() == "SILBRPT02")
                {
                    item.Content = (int.Parse(TitleId1.Substring(8, 4)) <= 1199 ? "Specimen" : "Part Scanned");
                    this.lblSpecimenTitle.Content = (int.Parse(TitleId1.Substring(8, 4)) <= 1199 ? "Specimen :" : "Part Scanned :");
                    this.lblSpecimenTitle2.Content = this.lblSpecimenTitle.Content;
                    break;
                }
            }

            this.txtRptTitle.Text = test1a.titledesc.Trim();
            this.txtRptTitle.Tag = test1a.titlecod;
            this.txtSpecimen.Text = test1a.spedesc.Trim();
            this.txtSpecimen.Tag = test1a.specode;
            this.txtMachine.Text = test1a.machdesc.Trim();
            this.txtMachine.Tag = test1a.machcod;

            var Title1 = this.ListRptSavedTemplate.FindAll(x => x.isircode == CombCod1 && x.elgrpsl == 0 && x.elressl == 1).ToList();
            if (Title1.Count > 0)
            {
                this.txtRptTitle.Text = Title1[0].eldesc.Trim();
                this.txtRptTitle.Tag = Title1[0].elcode;
            }

            var Specm1 = this.ListRptSavedTemplate.FindAll(x => x.isircode == CombCod1 && x.elgrpsl == 0 && x.elressl == 2).ToList();
            if (Specm1.Count > 0)
            {
                this.txtSpecimen.Text = Specm1[0].eldesc.Trim();
                this.txtSpecimen.Tag = Specm1[0].elcode;
            }
            var Machine1 = this.ListRptSavedTemplate.FindAll(x => x.isircode == CombCod1 && x.elgrpsl == 0 && x.elressl == 3).ToList();
            if (Machine1.Count > 0)
            {
                this.txtMachine.Text = Machine1[0].eldesc.Trim();
                this.txtMachine.Tag = Machine1[0].elcode;
            }

            this.xctk_dtpRptTime.Value = DateTime.Parse(this.txtDeliveryTime.Text);
            string ShowBtnName1 = ((Button)sender).Name.ToString().ToUpper();
            var ListRptSavedTemplate2 = this.ListRptSavedTemplate.FindAll(x => x.isircode == CombCod1 && x.elgrpsl > 0).ToList();
            if (ShowBtnName1 == "BTNSHOWREPORT" && ListRptSavedTemplate2.Count > 0)
            {
                this.ListRptDgTemplate = this.ListRptDgTemplate.Union(ListRptSavedTemplate2).ToList();
                this.cmbRptVerifiedBy.SelectedIndex = 0;
                this.cmbRptLabStaff1.SelectedIndex = 0;
                this.cmbRptLabStaff2.SelectedIndex = 0;

                var ListRptPrepareBy1 = this.ListRptSavedTemplate.Find(x => x.isircode == CombCod1 && x.elgrpsl == 0 && x.elressl == 1);
                string[] RptVerify1 = ListRptPrepareBy1.elresval.Split(',');
                //06-Jun-2018 05:00 PM
                //var cmbRptVerifiedBy1 = (ListRptPrepareBy1.elresval.Trim() + "000000000000").Substring(0, 12);
                var cmbRptVerifiedBy1 = (RptVerify1[0].Trim() + "000000000000").Substring(0, 12);
                foreach (ComboBoxItem item in this.cmbRptVerifiedBy.Items)
                {
                    if (item.Tag.ToString().Trim() == cmbRptVerifiedBy1)
                    {
                        this.cmbRptVerifiedBy.SelectedItem = item;
                        break;
                    }
                }
                if (RptVerify1.Length > 1)
                {
                    DateTime RptTime1;
                    if (DateTime.TryParse(RptVerify1[1], out RptTime1))
                         this.xctk_dtpRptTime.Value = RptTime1;
                }

                string Staff1 = (ListRptPrepareBy1.elrefval.Trim() + "000000000000000000000000").Substring(0, 12);
                string Staff2 = (ListRptPrepareBy1.elrefval.Trim() + "000000000000000000000000").Substring(12, 12);
                foreach (ComboBoxItem item in this.cmbRptLabStaff1.Items)
                {
                    if (item.Tag.ToString().Trim() == Staff1)
                    {
                        this.cmbRptLabStaff1.SelectedItem = item;
                        break;
                    }
                }
                foreach (ComboBoxItem item in this.cmbRptLabStaff2.Items)
                {
                    if (item.Tag.ToString().Trim() == Staff2)
                    {
                        this.cmbRptLabStaff2.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                var list1 = this.ListDiagTemplateDetails.FindAll(x => x.elcode == TitleId1);
                foreach (var item in list1)
                {
                    var list2 = this.ListDiagTemplateDetails.FindAll(x => x.sircode == item.sircode);
                    if (list2.FindAll(y => y.elcode == SpecmId1).Count > 0)
                        this.BuildTestReport(item.sircode, CombCod1);
                }
            }
    

            this.btndgvRptHeader_Click(null, null);
            this.cmbRptTitle.IsEnabled = false;
            this.stkpRptWrite.Visibility = Visibility.Visible;
            this.stkpRptWrite.IsEnabled = true;
            this.btnUpdateTrans.Visibility = Visibility.Visible;
            this.stkpPrint.Visibility = Visibility.Visible;
            this.btnShowReport.Content = "Change";
        }

        private void BuildTestReport(string tsircode1, string CombCod1)
        {
            //var list1t = this.ListDiagTemplateDetails.FindAll(x => x.sircode == tsircode1 && (x.elcode.Substring(0, 9) != "SILBRPT01" && x.elcode.Substring(0, 9) != "SILBRPT02")).OrderBy(y => y.elgrpsl.ToString("00") + y.elressl.ToString("00")).ToList();
            var list1t = this.ListDiagTemplateDetails.FindAll(x => x.sircode == tsircode1 && (x.elcode.Substring(7, 2) != "01" && x.elcode.Substring(7, 2) != "02") && x.elcode.Substring(7, 2) != "03").OrderBy(y => y.elgrpsl.ToString("00") + y.elressl.ToString("00")).ToList();
            foreach (var item in list1t)
            {
                bool isGrp = (item.elcode.Substring(0, 9) == "SILBRPT06");
                this.ListRptDgTemplate.Add(new vmEntryLabMgt1.DiagRptTemplate()
                {
                    elcode = item.elcode,
                    eldesc = item.eldesc,
                    elgrpsl = item.elgrpsl,
                    elressl = item.elressl,
                    isircode = CombCod1,
                    elrefval = item.elrefval,
                    elresval = item.elresval,
                    elSlEnabled = (isGrp ? false : true),
                    elFontBold = (isGrp ? "Bold" : "Normal"),
                    elstyle = (isGrp ? "BU" : "N"),
                    elVisible = "Visible"//(isGrp ? "Collapsed" : "Visible")
                });

            }


        }

        private void chkElement_Click(object sender, RoutedEventArgs e)
        {
            this.stkpAddElement.Visibility = (this.chkElement.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
            //            this.stkpRptVerified.Visibility = (this.chkElement.IsChecked == true ? Visibility.Collapsed : Visibility.Visible);
            this.autoElementSearch.SelectedValue = null;

            if (this.chkElement.IsChecked == true)
                this.cmbElementType_SelectionChanged(null, null);
        }

        private void autoElementSearch_PatternChanged(object sender, AutoComplete.AutoCompleteArgs args)
        {
            string tag1 = ((ComboBoxItem)this.cmbElementType.SelectedItem).Tag.ToString();
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetElementActDesc(args.Pattern, tag1);
            this.autoElementSearch.ToolTip = null;
        }
        private ObservableCollection<HmsEntityGeneral.AcInfCodeBook> GetElementActDesc(string Pattern, string tag1)
        {
            return new ObservableCollection<HmsEntityGeneral.AcInfCodeBook>(
                this.TestElementList.Where((x, match) => x.actcode.Substring(9, 3) != "000" && x.actcode.Substring(0, 9) == tag1 && x.actdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void btnSelectElement_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoElementSearch.SelectedValue == null)
                return;

            string mtitlecod1 = ((ComboBoxItem)this.cmbRptTitle.SelectedItem).Tag.ToString();
            string TitleId1 = mtitlecod1.Substring(0, 12);
            string SpecmId1 = mtitlecod1.Substring(12, 12);
            string CombCod1 = TitleId1.Substring(7, 5) + SpecmId1.Substring(7, 5) + "00";


            string isircod = CombCod1;  // "000000000000";// this.lblSelectedTest.Tag.ToString().Trim();
            string elcode = this.autoElementSearch.SelectedValue.ToString();
            string eldesc = this.autoElementSearch.SelectedText.Trim();
            int grpcod = (int)this.udGroup.Value;
            int slncod = (int)this.udSlNum.Value;
            this.autoElementSearch.SelectedValue = null;
            this.autoElementSearch.ToolTip = null;
            eldesc = (elcode.Substring(9, 3) == "001" ? "" : eldesc);

            switch (elcode.Substring(0, 9))
            {
                case "SILBRPT01":
                    this.txtRptTitle.Text = eldesc;
                    this.txtRptTitle.Tag = elcode;
                    break;
                case "SILBRPT02":
                    this.txtSpecimen.Text = eldesc;
                    this.txtSpecimen.Tag = elcode;
                    break;
                case "SILBRPT03":
                    this.txtMachine.Text = eldesc;
                    this.txtMachine.Tag = elcode;
                    break;
                case "SILBRPT06":
                case "SILBRPT08":
                    this.dgvRpt.ItemsSource = null;
                    bool isGrp = (elcode.Substring(0, 9) == "SILBRPT06");
                    eldesc = (elcode == "SILBRPT06001" ? "" : eldesc);
                    this.ListRptDgTemplate.Add(new vmEntryLabMgt1.DiagRptTemplate(isircod, grpcod, (isGrp ? 0 : slncod), elcode, eldesc, "", "",
                        "Visible", (isGrp ? "Bold" : "Normal"), (isGrp ? false : true), (isGrp ? "BU" : "N")));

                    //this.ListRptDgTemplate.Add(new vmEntryLabMgt1.DiagRptTemplate(isircod, grpcod, (isGrp ? 0 : slncod), elcode, eldesc, "", "",
                    //                        (isGrp ? "Collapsed" : "Visible"), (isGrp ? "Bold" : "Normal"), (isGrp ? false : true), (isGrp ? "BU" : "N")));
                    this.btndgvRptHeader_Click(null, null);
                    var selitm1 = this.ListRptDgTemplate.Find(x => x.elgrpsl == grpcod && x.elcode == elcode);
                    this.dgvRpt.ScrollIntoView(selitm1);
                    break;
            }
        }

        private void cmbElementType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.autoElementSearch == null)
                return;

            this.autoElementSearch.SelectedValue = null;
            this.autoElementSearch.ContextMenu.Items.Clear();
            string tag1 = ((ComboBoxItem)this.cmbElementType.SelectedItem).Tag.ToString();
            var ElemList1 = this.TestElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9));
            foreach (var ditem in ElemList1)
            {
                MenuItem mnui1 = new MenuItem() { Header = ditem.actdesc.Trim(), Tag = ditem.actcode };
                mnui1.Click += autoElementSearch_ContextMenu_MouseClick;
                this.autoElementSearch.ContextMenu.Items.Add(mnui1);
            }
        }

        private void autoElementSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            string tag1 = ((ComboBoxItem)this.cmbElementType.SelectedItem).Tag.ToString();
            this.autoElementSearch.ItemsSource = this.TestElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9) && x.actcode.Substring(9, 3) != "000");
            this.autoElementSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim(); ;
            this.autoElementSearch.ToolTip = this.autoElementSearch.SelectedValue.ToString().Trim().Substring(7, 5) + " - " + this.autoElementSearch.SelectedText.Trim();
        }

        private void autoElementSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoElementSearch.ContextMenu.IsOpen = true;
        }
        private void autoSpecimanSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoSpecimanSearch.ContextMenu.IsOpen = true;
        }

        private void btndgvRptHeader_Click(object sender, RoutedEventArgs e)
        {
            this.dgvRpt.ItemsSource = null;
            this.ListRptDgTemplate.Sort(delegate(vmEntryLabMgt1.DiagRptTemplate x, vmEntryLabMgt1.DiagRptTemplate y)
            {
                return (x.elgrpsl.ToString("00") + x.elressl.ToString("00")).CompareTo(y.elgrpsl.ToString("00") + y.elressl.ToString("00"));
            });
            this.dgvRpt.ItemsSource = this.ListRptDgTemplate;
        }

        private void chkSpecReceiveInfo_Click(object sender, RoutedEventArgs e)
        {
            this.stkpEntrySpecimanRcv.IsEnabled = false;
            this.stkpSampleRecv.IsEnabled = false;
            var isHide = (this.chkSpecReceiveInfo.IsChecked == true ? false : true);
            this.stkpSampleRecv.Visibility = (isHide ? Visibility.Collapsed : Visibility.Visible);
            this.stkpSampleRecv.IsEnabled = true;
            this.dgvRpt.Width = (isHide ? 1170 : 875);
            this.dgvRpt.Columns[1].Width = (isHide ? 280 : 200);
            this.dgvRpt.Columns[2].Width = (isHide ? 500 : 340);
            this.dgvRpt.Columns[3].Width = (isHide ? 250 : 200);
            this.stkpEntrySpecimanRcv.IsEnabled = true;
        }

        private void btnNav_Click(object sender, RoutedEventArgs e)
        {

            if (this.dgvRpt.Items.Count == 0)
                return;

            if (this.dgvRpt.SelectedIndex < 0)
                this.dgvRpt.SelectedIndex = 0;

            string ActtionName = ((Button)sender).Name.ToString().Trim();
            int index1 = this.dgvRpt.SelectedIndex;
            if (ActtionName == "btnDelete" || ActtionName == "btnDelete2" || ActtionName == "btnDelete3")
            {
                string delmsg = "Are you confirm to delete item\n" + this.ListRptDgTemplate[index1].elgrpsl.ToString("00") + "." +
                    this.ListRptDgTemplate[index1].elressl.ToString("00") + ". " + this.ListRptDgTemplate[index1].eldesc.Trim();

                if (ActtionName == "btnDelete2")
                    delmsg = "Are you confirm to delete all 'X' marked item(s)";

                else if (ActtionName == "btnDelete3")
                    delmsg = "Are you confirm to delete item(s) of " + this.ListRptDgTemplate[index1].elgrpsl.ToString("00") + " group" ;

                MessageBoxResult msgresult = System.Windows.MessageBox.Show(delmsg, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;
                this.dgvRpt.ItemsSource = null;

                if (ActtionName == "btnDelete2")
                {
                    this.ListRptDgTemplate = this.ListRptDgTemplate.FindAll(x => x.elstyle.Trim() != "X");
                }
                else if (ActtionName == "btnDelete3")
                {
                    var gc1 = this.ListRptDgTemplate[index1].elgrpsl;
                    this.ListRptDgTemplate = this.ListRptDgTemplate.FindAll(x => x.elgrpsl != gc1);
                }
                else 
                {
                    this.ListRptDgTemplate[index1].elstyle = "XXXX";
                    this.ListRptDgTemplate = this.ListRptDgTemplate.FindAll(x => x.elstyle != "XXXX");
                }
                this.dgvRpt.ItemsSource = this.ListRptDgTemplate;
                if (this.ListRptDgTemplate.Count > 0)
                {
                    this.dgvRpt.SelectedIndex = (this.ListRptDgTemplate.Count <= index1 ? this.ListRptDgTemplate.Count - 1 : index1);
                }
                return;
            }
            switch (ActtionName)
            {
                case "btnTop":
                    index1 = 0;
                    break;
                case "btnPrev":
                    index1 = this.dgvRpt.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnNext":
                    index1 = this.dgvRpt.SelectedIndex + 1;
                    if (index1 >= this.dgvRpt.Items.Count)
                        index1 = this.dgvRpt.Items.Count - 1;
                    break;
                case "btnBottom":
                    index1 = this.dgvRpt.Items.Count - 1;
                    break;
            }
            this.dgvRpt.SelectedIndex = index1;

            var item21 = (vmEntryLabMgt1.DiagRptTemplate)this.dgvRpt.Items[index1];
            this.dgvRpt.ScrollIntoView(item21);
        }

        private void btnFilter1_Click(object sender, RoutedEventArgs e)
        {
            this.dgvTransList.ItemsSource = null;
            this.CommInvSummList = null;
            string BrnCode1 = ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString().Substring(0, 4);
            string SignInID1 = "%"; // (this.chkFilterUser.IsChecked == true ? "%" : WpfProcessAccess.SignedInUserList[0].hccode);
            string StartDate1 = this.xctk_dtpSrchDat1.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = this.xctk_dtpSrchDat2.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = "CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = "COMMINVLIST01";

            //var pap1 = vmr.SetParamCommInvSummList(WpfProcessAccess.CompInfList[0].comcpcod, BrnCode, StartDate, EndDate, "CSI", SignInID, "A", "%", "%");

            var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                                             InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: "NONE", OrderBy: OrderBy1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.CommInvSummList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();

            string paName1 = this.txtSrchPatName.Text.Trim().ToUpper();
            string phone1 = this.txtSrchPhoneNo.Text.Trim().ToUpper();
            string invnum2 = this.txtSrchInvNo.Text.Trim();
            if (paName1.Length > 0)
                this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptname.Contains(paName1)).ToList();

            if (phone1.Length > 0)
                this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptphone.Contains(phone1)).ToList();

            if (invnum2.Length > 0)
                this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptinvnum2.Contains(invnum2)).ToList();


            int slnum1 = 1;
            foreach (var item in this.CommInvSummList)
            {
                item.slnum = slnum1;
                ++slnum1;
            }
            this.dgvTransList.ItemsSource = this.CommInvSummList;
            this.dgvTransList.Items.Refresh();
        }

        private void btnPrintTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.btnUpdateTrans_Click(null, null);
                string BrnchCod1 = ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString().Trim();
                string ym1 = ((ComboBoxItem)this.cmbPrevYearMon.SelectedItem).Tag.ToString().Trim();
                string InvNum1 = "CSI" + ym1 + BrnchCod1 + this.txtPrevTransID.Text.Trim();
                string startDate = this.txtInvDate.Text.Trim();
                string EndDate = startDate;

                string mtitlecod1 = ((ComboBoxItem)this.cmbRptTitle.SelectedItem).Tag.ToString();
                string TitleId1 = mtitlecod1.Substring(0, 12);
                string SpecmId1 = mtitlecod1.Substring(12, 12);
                string CombCod1 = TitleId1.Substring(7, 5) + SpecmId1.Substring(7, 5) + "00";
                //--------------------------------------

                // string CompCode, string ProcessID = "RPTDIAGNOSIS01", string isircode = "XXXXXXXXXXXXXXXXXX", string InvNum = "XXXXXXXXXXXXXXXXXX", string OrderBy = "DEFAULT"

                var pap1 = vm1.SetParamToPrintDiagRpt(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: "RPTDIAGNOSIS01", isircode: "%", InvNum: InvNum1,
                    startDate: startDate, EndDate: EndDate, OrderBy: CombCod1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var list1a = ds1.Tables[0].DataTableToList<HmsEntityDiagnostic.DiagnosticReport>();
                var list1b = ds1.Tables[1].DataTableToList<HmsEntityDiagnostic.DiagnosticReport>();
                var list1c = ds1.Tables[2].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01GenInf>();

                var list1d = "";
                if (ds1.Tables[3].Rows.Count > 0)
                {
                    if (!(ds1.Tables[3].Rows[0]["ptphoto"] is DBNull))
                    {
                        byte[] imge1 = (byte[])ds1.Tables[3].Rows[0]["ptphoto"];
                        list1d = Convert.ToBase64String(imge1);
                    }
                }

                // Start of Rearrange For Culture Test
                #region Start of Rearrange For Culture Test
                var listCulture1 = list1b.FindAll(x => x.eldesc1.ToUpper().Contains("CULTURE OF") || x.elresval.ToUpper().Contains("CULTURE"));
                if (listCulture1.Count > 0) // (list1a[0].sircode.Substring(0, 9) == "450211166")
                {
                    var list1b1 = list1b.FindAll(x => x.elgrpsl == 2 && x.elressl > 0).OrderBy(y => y.elressl).ToList();
                    int cnt1 = list1b1.Count;
                    if (cnt1 > 0)
                    {
                        for (int u = cnt1; u < 36; u++)
                        {
                            var cc1 = new HmsEntityDiagnostic.DiagnosticReport()
                            {
                                sircode = "000000000000",
                                elgrpsl = 0,
                                elressl = 0,
                                elcode = "",
                                eldesc1 = "",
                                eldesc2 = "",
                                elrefval = "",
                                elresval = "",
                                elstyle = "",
                                sirdesc = ""
                            };

                            list1b1.Add(cc1);
                        }

                        var list1bb = new List<HmsEntityDiagnostic.DiagnosticReport>();
                        for (int k = 0; k < 12; k++)
                        {
                            var newitem = new HmsEntityDiagnostic.DiagnosticReport();
                            newitem.sircode = list1b1[k].sircode;
                            newitem.elgrpsl = list1b1[k].elgrpsl;
                            newitem.elressl = list1b1[k].elressl;

                            newitem.sirdesc = (k + 1).ToString("00") + ". " + list1b1[k].eldesc1.Trim();
                            newitem.elcode = ": " + list1b1[k].elresval.Trim();
                            newitem.eldesc2 = "CULTURE";

                            newitem.eldesc1 = (list1b1[k + 12].eldesc1.Trim().Length > 0 ? (k + 13).ToString("00") + ". " + list1b1[k + 12].eldesc1.Trim() : "");
                            newitem.elresval = (list1b1[k + 12].eldesc1.Trim().Length > 0 ? ": " + list1b1[k + 12].elresval.Trim() : "");

                            newitem.elrefval = (list1b1[k + 24].eldesc1.Trim().Length > 0 ? (k + 25).ToString("00") + ". " + list1b1[k + 24].eldesc1.Trim() : "");
                            newitem.elstyle = (list1b1[k + 24].eldesc1.Trim().Length > 0 ? ": " + list1b1[k + 24].elresval.Trim() : "");

                            list1bb.Add(newitem);

                        }

                        ////string[] desc2a = { "", "", "" };
                        ////string[] desc2b = { "", "", "" };
                        ////int sl = 0;
                        ////int cnt2 = 1;
                        ////foreach (var item in list1b1)
                        ////{
                        ////    desc2a[sl] = item.eldesc1.Trim();
                        ////    desc2b[sl] = ": " + item.elresval.Trim();
                        ////    if (sl == 2 || list1b1.Count == cnt2)
                        ////    {
                        ////        var newitem = new HmsEntityDiagnostic.DiagnosticReport();
                        ////        newitem.sircode = item.sircode;
                        ////        newitem.elgrpsl = item.elgrpsl;
                        ////        newitem.elressl = item.elressl;

                        ////        newitem.sirdesc = desc2a[0];
                        ////        newitem.elcode = desc2b[0];

                        ////        newitem.eldesc2 = "CULTURE";
                        ////        newitem.eldesc1 = desc2a[1];
                        ////        newitem.elresval = desc2b[1];

                        ////        newitem.elrefval = desc2a[2];
                        ////        newitem.elstyle = desc2b[2];

                        ////        desc2a[0] = desc2a[1] = desc2a[2] = "";
                        ////        desc2b[0] = desc2b[1] = desc2b[2] = "";
                        ////        list1bb.Add(newitem);
                        ////        sl = -1;
                        ////    }
                        ////    sl++;
                        ////    cnt2++;
                        ////}

                        ////cnt2 = list1bb.Count;
                        ////sl = 1;
                        ////foreach (var item in list1bb)
                        ////{
                        ////    item.sirdesc = (item.sirdesc.Trim().Length > 0 ?  sl.ToString("00") + ". " : "") + item.sirdesc.Trim();
                        ////    item.eldesc1 = (item.eldesc1.Trim().Length > 0 ?  (sl + cnt2).ToString("00") + ". " : "") + item.eldesc1.Trim();
                        ////    item.elrefval = (item.elrefval.Trim().Length > 0 ? (sl + cnt2 + cnt2).ToString("00") + ". " : "") + item.elrefval.Trim();
                        ////    sl++;
                        ////}

                        list1b = list1b.FindAll(x => x.elgrpsl != 2 || (x.elgrpsl == 2 && x.elressl == 0));
                        list1b = list1b.Concat(list1bb).ToList().OrderBy(y => y.elgrpsl.ToString("00") + y.elressl.ToString("00")).ToList();
                    }
                }

                #endregion End of Rearrange For Culture Test

                //string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["preparebynam"].ToString().Trim() + ", " +
                //                     ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                //var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]));

                //string docid1 = (list1a[0].elresval.Trim() + "000000000000").Substring(0, 12);
                string[] RptVerify1 = list1a[0].elresval.Trim().Split(',');

                string docid1 = (RptVerify1[0] + "000000000000").Substring(0, 12);
                DateTime RptTime1;
                if (RptVerify1.Length > 1)
                {
                    if (DateTime.TryParse(RptVerify1[1], out RptTime1))
                        list1c[0].delivartime = RptTime1;
                }

                string labhr1 = (list1a[0].elrefval.Trim() + "000000000000000000000000").Substring(0, 12);
                string labhr2 = (list1a[0].elrefval.Trim() + "000000000000000000000000").Substring(12, 12);

                string[] Retval1 = this.GetSealInfo(docid1);
                list3[0].RptParVal1 = Retval1[0];
                list3[0].RptParVal2 = Retval1[1];

                Retval1 = this.GetSealInfo(labhr1);
                list3[0].RptParVal3 = Retval1[0];
                list3[0].RptParVal4 = Retval1[1];

                Retval1 = this.GetSealInfo(labhr2);
                list3[0].RptParVal5 = Retval1[0];
                list3[0].RptParVal6 = Retval1[1];

                var list1 = new List<Object>();
                list1.Add(list1a);
                list1.Add(list1b);
                list1.Add(list1c);
                list1.Add(list1d);

                LocalReport rpt1 = DiagReportSetup.GetLocalReport("Lab.RptLabDiag01", list1, null, list3);

                string WindowTitle1 = "Diagnosis Report";
                string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                if (PrintId == "DP")
                {
                    RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                    //DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                    DirectPrint1.PrintReport(rpt1);
                    DirectPrint1.Dispose();
                }
                else
                {
                    string FileName1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + list1c[0].ptinvnum2 + "-" + list1c[0].ptname.Trim().Replace(".", "").Replace(" ", "_");
                    string RptDisplayMode = (PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL" ? PrintId : "PrintLayout");
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode, RenderFileName1: FileName1);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Lab.Mgt-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private string[] GetSealInfo(string isircode1)
        {
            string[] Retval1 = { "", "" };
            if (isircode1.Substring(0, 2) != "95")
                return Retval1;

            var pap1l = vm1.SetParamToGetRptTemplate(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, isircode1: isircode1);
            DataSet ds1l = WpfProcessAccess.GetHmsDataSet(pap1l);
            if (ds1l == null)
                return Retval1;
            if (ds1l.Tables[0].Rows.Count == 0)
                return Retval1;

            DataRow[] dr0 = ds1l.Tables[0].Select("actcode='SIHI00102001' and repeatsl = '001'");
            if (dr0 != null)
            {
                if (dr0.Length >= 0)
                    Retval1[0] = dr0[0]["dataval"].ToString().Trim();
            }
            DataRow[] dr1 = ds1l.Tables[0].Select("actcode='SIHI00102010' and repeatsl = '001'");
            if (dr1 != null)
            {
                if (dr1.Length >= 0)
                    Retval1[1] = dr1[0]["dataval"].ToString().Trim();
            }
            return Retval1;
        }



        private void autoTitleSearch_PatternChanged(object sender, AutoComplete.AutoCompleteArgs args)
        {
            string tag1 = "SILBRPT01";
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetElementActDesc(args.Pattern, tag1);
            this.autoTitleSearch.ToolTip = null;
        }

        private void autoSpecimanSearch2_MenuBind()
        {
            if (this.autoSpecimanSearch2 == null)
                return;

            ContextMenu ctmSpecCodeType2 = new ContextMenu() { Height = 400 };
            foreach (var item1 in this.ListSpeciman)
            {
                MenuItem miSpecType2 = new MenuItem() { Header = item1.actdesc.Trim(), Tag = item1.actcode };
                miSpecType2.Click += autoSpecimanSearch2_ContextMenu_MouseClick;
                ctmSpecCodeType2.Items.Add(miSpecType2);
            }
            this.autoSpecimanSearch2.ContextMenu = ctmSpecCodeType2;

        }
        private void autoSpecimanSearch2_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            this.autoSpecimanSearch2.ItemsSource = this.ListSpeciman;//.TestElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9) && x.actcode.Substring(9, 3) != "000");
            this.autoSpecimanSearch2.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim(); ;


            //string specCod1 = this.autoSpecimanSearch.SelectedValue.ToString().Trim();
            //string specDesc1 = this.autoSpecimanSearch.SelectedText.Trim();


            //string tag1 = "SILBRPT01";
            //this.autoTitleSearch.ItemsSource = this.TestElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9) && x.actcode.Substring(9, 3) != "000");
            //this.autoTitleSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim(); ;
            //this.autoTitleSearch.ToolTip = this.autoTitleSearch.SelectedValue.ToString().Trim().Substring(7, 5) + " - " + this.autoTitleSearch.SelectedText.Trim();
        }
        private void autoTitleSearch_MenuBind()
        {
            if (this.autoTitleSearch == null)
                return;

            this.autoTitleSearch.SelectedValue = null;
            this.autoTitleSearch.ContextMenu.Items.Clear();
            string tag1 = "SILBRPT01";
            var ElemList1 = this.TestElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9));
            foreach (var ditem in ElemList1)
            {
                MenuItem mnui1 = new MenuItem() { Header = ditem.actdesc.Trim(), Tag = ditem.actcode };
                mnui1.Click += autoTitleSearch_ContextMenu_MouseClick;
                this.autoTitleSearch.ContextMenu.Items.Add(mnui1);
            }
        }

        private void autoTitleSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            string tag1 = "SILBRPT01";
            this.autoTitleSearch.ItemsSource = this.TestElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9) && x.actcode.Substring(9, 3) != "000");
            this.autoTitleSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim(); ;
            this.autoTitleSearch.ToolTip = this.autoTitleSearch.SelectedValue.ToString().Trim().Substring(7, 5) + " - " + this.autoTitleSearch.SelectedText.Trim();
        }

        private void autoTitleSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoTitleSearch.ContextMenu.IsOpen = true;
        }

        private void btnSelectNewTitle_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //// to be handeled soon -- Hafiz 02-May-2018
                //if (this.ListTestTitle.Count == 0)
                //    this.ListTestTitle.Add(new vmEntryLabMgt1.DiagTestTitle() { machcod = "SILBRPT03001", machdesc = "", mtitlecod = "SILBRPT02001SILBRPT01001", mtitledes = "", 
                //        specode = "SILBRPT02001", spedesc = "", titlecod = "SILBRPT01001", titledesc = "" });
                if (this.autoTitleSearch.SelectedValue == null)
                    return;

                var TitleCode1 = this.autoTitleSearch.SelectedValue.ToString();
                var TitleDesc1 = this.autoTitleSearch.SelectedText.Trim();

                var TitleCode2 = this.autoSpecimanSearch2.SelectedValue.ToString();
                var TitleDesc2 = this.autoSpecimanSearch2.SelectedText.Trim();

                var newTitle1 = new vmEntryLabMgt1.DiagTestTitle()
                {
                    machcod = "SILBRPT03001",
                    machdesc = "",
                    mtitlecod = TitleCode1 + TitleCode2,    // "SILBRPT02001"
                    mtitledes = TitleDesc1 + " (Specimen : " + TitleDesc2 + ")",  // (UNSPECIFIED)
                    specode = TitleCode2,   // "SILBRPT02001",
                    spedesc = TitleDesc2,   // "(UNSPECIFIED)",
                    titlecod = TitleCode1,
                    titledesc = TitleDesc1// + " (UNSPECIFIED)"
                };

                var List2 = this.ListTestTitle.FindAll(x => x.mtitlecod == TitleCode1 + TitleCode2).ToList();
                if (List2.Count > 0)
                {
                    System.Windows.MessageBox.Show("This titile already exist. Addition not possible", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                       MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                }
                {
                    this.ListTestTitle.Add(newTitle1);
                    this.cmbRptTitle.Items.Add(new ComboBoxItem()
                   {
                       Content = newTitle1.mtitledes.Trim(),
                       Tag = newTitle1.mtitlecod.Trim(),
                       Uid = newTitle1.machcod.Trim(),
                       ToolTip = "Machine Used : " + newTitle1.machdesc.Trim()
                   });
                    this.cmbRptTitle.SelectedIndex = this.cmbRptTitle.Items.Count - 1;
                }
                ////this.cmbRptTitle.Items.Clear();
                //foreach (var item in this.ListTestTitle)
                //{
                //    item.machdesc = (item.machcod == "SILBRPT03001" ? "" : item.machdesc.Trim());
                //    this.cmbRptTitle.Items.Add(new ComboBoxItem()
                //    {
                //        Content = item.mtitledes.Trim(),
                //        Tag = item.mtitlecod.Trim(),
                //        Uid = item.machcod.Trim(),
                //        ToolTip = "Machine Used : " + item.machdesc.Trim()
                //    });
                //}

                //if (this.cmbRptTitle.Items.Count > 0)
                //    this.cmbRptTitle.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Lab.Mgt-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void autoSpecimanSearch2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoSpecimanSearch2.ContextMenu.IsOpen = true;
        }

        private void autoSpecimanSearch2_PatternChanged(object sender, AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetSpecimanDesc(args.Pattern);
        }
        private void BackupCancelInvoice(string InvoiceID, string exetype, string exermrk)
        {
            var pap1 = vmr.SetParamToBackupCancelInvoice(CompCode: WpfProcessAccess.CompInfList[0].comcod, InvoiceID: InvoiceID, exetype: exetype, exebyid: WpfProcessAccess.SignedInUserList[0].hccode,
                exebynam: WpfProcessAccess.SignedInUserList[0].signinnam, exeses: WpfProcessAccess.SignedInUserList[0].sessionID, exetrm: WpfProcessAccess.SignedInUserList[0].terminalID, exermrk: exermrk);

            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

        }

    }
}
