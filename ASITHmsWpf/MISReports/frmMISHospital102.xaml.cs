using ASITHmsEntity;
using ASITFunLib;
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
using ASITHmsViewMan.Commercial;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.ObjectModel;

namespace ASITHmsWpf.MISReports
{
    /// <summary>
    /// Interaction logic for frmMISHospital102.xaml
    /// </summary>
    public partial class frmMISHospital102 : UserControl
    {
        private string TitaleTag1, TitaleTag2;
        private DateTime StartDate, EndDate;
        private bool FrmInitialized = false;
        private List<HmsEntityCommercial.CommInvSummInf> CommInvSummList = new List<HmsEntityCommercial.CommInvSummInf>();             // Hospital/Diagnostic Centre Commercial Invoice Summary List
        private List<HmsEntityCommercial.CommInvSummInf> CommInvSummShortList = new List<HmsEntityCommercial.CommInvSummInf>();             // Hospital/Diagnostic Centre Commercial Invoice Summary List
        private List<vmEntryFrontDesk1.OrderItem> OrderItemList = new List<vmEntryFrontDesk1.OrderItem>();
        private List<HmsEntityCommercial.CommInv01.CommInv01TblCol> CollInfoList = new List<HmsEntityCommercial.CommInv01.CommInv01TblCol>();
        private List<HmsEntityCommercial.HmsRefByInf> RefByInfList = new List<HmsEntityCommercial.HmsRefByInf>();                   // Hospital/Diagnostic Centre Service Item List

        private vmEntryFrontDesk1 vm1 = new vmEntryFrontDesk1();
        private vmReportFrontDesk1 vmr = new vmReportFrontDesk1();
        public frmMISHospital102()
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
            TitaleTag2 = this.Tag.ToString();
            this.xctk_dtpFrom.Value = Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy")); // DateTime.Today; //
            this.xctk_dtpTo.Value = DateTime.Today;
            this.StartDate = Convert.ToDateTime(this.xctk_dtpFrom.Text);
            this.EndDate = Convert.ToDateTime(this.xctk_dtpTo.Text);
            this.GetRefByTitleList();
            this.btnRefreshRef_Click(null, null);
        }
        private void CleanUpScreen()
        {
            //this.lblUpdateMsg.Content = "";
            //this.txtRefByID.Text = "000000000000";
            //this.txtRefByName.Text = "";
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
            this.stkpEntry.Visibility = Visibility.Collapsed;
            this.lstReferrer.Items.Clear();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void GetRefByTitleList()
        {
            this.cmbRefByTitle.Items.Clear();

            var TitleList = HmsEntityCommercial.GetRefByTitlesList();
            foreach (var item in TitleList)
                this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = item.rtitle, Tag = item.rtagid, ToolTip = item.rtooltip });

            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "DOCTOR", Tag = "DR.", ToolTip = "DOCTOR (DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "PROFESSOR", Tag = "PROF.", ToolTip = "PROFESSOR (PROF.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "PROFESSOR DOCTOR", Tag = "PROF. DR.", ToolTip = "PROFESSOR DOCTOR (PROF. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "ASSISTENT PROFESSOR DOCTOR", Tag = "ASSTT. PROF. DR.", ToolTip = "ASSISTENT PROFESSOR DOCTOR (ASSTT. PROF. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "BRIG. GEN. PROFESSOR DOCTOR", Tag = "BRIG. GEN. PROF. DR.", ToolTip = "BRIG. GEN. PROFESSOR DOCTOR (BRIG. GEN. PROF. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "BRIG. GEN. DOCTOR", Tag = "BRIG. GEN. DR.", ToolTip = "BRIG. GEN. DOCTOR (BRIG. GEN. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "LT. COL. PROFESSOR DOCTOR", Tag = "LT. COL. PROF. DR.", ToolTip = "LT. COL. PROFESSOR DOCTOR (LT. COL. PROF. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "LT. COL. DOCTOR", Tag = "LT. COL. DR.", ToolTip = "LT. COL. DOCTOR (LT. COL. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "HOSPITAL", Tag = "(HOSPITAL)", ToolTip = "HOSPITAL" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "CLINIC", Tag = "(CLINIC)", ToolTip = "CLINIC" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "PHARMACY", Tag = "(PHARMA)", ToolTip = "MEDICINE SHOP" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "MR.", Tag = "MR.", ToolTip = "INDIVIDUAL (MR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "MS.", Tag = "MS.", ToolTip = "INDIVIDUAL (MS.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "COMPANY", Tag = "(COMPANY)", ToolTip = "OTHER COMPANY" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "(NONE)", Tag = ".", ToolTip = "UNTITLED" });
        }

        private void GetRefByInfList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            this.RefByInfList = null;
            var pap1 = vmr.SetParamRefByInfList(WpfProcessAccess.CompInfList[0].comcpcod, "%");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.RefByInfList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.HmsRefByInf>();
        }

        private void txtRefByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string srchTxt = this.txtRefByName.Text.Trim().ToUpper();
            var lst1 = this.RefByInfList.FindAll(x => (x.refbyid.Substring(6, 6) + x.rfFullName.Trim().ToUpper()).Contains(srchTxt));
            this.lstReferrer.Items.Clear();
            foreach (var item in lst1)
            {
                this.lstReferrer.Items.Add(new ListBoxItem()
                {
                    Content = item.refbyid.Substring(6, 6) + " - " + item.rfFullName.Trim(),
                    Tag = item.refbyid,
                    ToolTip = item.refbyid.Substring(6, 6) + " - " + item.rfFullName.Trim()
                }
                );
            }
            if (this.lstReferrer.Items.Count > 0)
                this.lstReferrer.SelectedIndex = 0;
        }
        private void txtRefByName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                this.lstReferrer.Focus();
            else if (e.Key == Key.Return)
            {
                if (this.lstReferrer.Items.Count > 0)
                {
                    this.lstReferrer.SelectedIndex = 0;
                }
            }
        }
        private void txtRefByName_GotFocus(object sender, RoutedEventArgs e)
        {
            this.stkpRefByEntry.Visibility = Visibility.Collapsed;
            this.stkpRefByList.Visibility = Visibility.Visible;
            this.stkpRefByList0.Visibility = Visibility.Visible;
            this.btnAddRefBy.Content = "Add/Edit";
            this.GridRefrrerList.Visibility = Visibility.Visible;
        }


        private void btnUpdateRefBy1_Click(object sender, RoutedEventArgs e)
        {
            string RefById1 = this.lblRefByID1.Tag.ToString().Trim();// "000000000000";
            string RefMktId1 = "000000000000";
            if (this.autoMktStaffRefSearch.SelectedValue != null)
                RefMktId1 = this.autoMktStaffRefSearch.SelectedValue.ToString();

            Hashtable Ref1 = new Hashtable();
            Ref1["REFBYID"] = RefById1;
            Ref1["REFMKTID"] = RefMktId1;
            Ref1["NAMTITLE"] = this.lblRefByNameTitle1.Content.ToString().Trim();
            Ref1["FULLNAME"] = this.txtRefByName1.Text.Trim().ToUpper();
            Ref1["QCTITLE"] = this.txtRefByQCTitle1.Text.Trim().ToUpper();
            Ref1["ADDRESS"] = this.txtRefByAddress1.Text.Trim();
            Ref1["PHONE"] = this.txtRefByPhone1.Text.Trim();
            Ref1["EMAIL"] = this.txtRefByEMail1.Text.Trim();
            Ref1["TYPE"] = ((ComboBoxItem)this.cmbRefByType.SelectedItem).Tag.ToString().Trim();
            Ref1["REFACTIVE"] = this.txtRefActive.Text.Trim();
            Ref1["REFMARK"] = this.txtRefMark.Text.Trim();

            DataSet ds1 = vm1.GetDataDetForRefByUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, Ref1: Ref1);
            var pap1 = vm1.SetParamUpdateRefByInfo(WpfProcessAccess.CompInfList[0].comcod, ds1, RefById1);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            string memonum = ds2.Tables[0].Rows[0]["refbyid"].ToString();
            this.lblRefByID1.Content = memonum.Substring(6, 6);
            this.lblRefByID1.Tag = memonum;
            this.btnUpdateRefBy1.Tag = "Saved";
            this.btnUpdateRefBy1.IsEnabled = false;
            this.btnRefreshRef_Click(null, null);
        }
        private void cmbRefByTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnNewShow_GotFocus(object sender, RoutedEventArgs e)
        {
            this.SelectReferrerInfo();
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
        }
        private void lblRefByNameTitle1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void lstReferrer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectReferrerInfo();
        }

        private void SelectReferrerInfo()
        {
            ListBoxItem lbi1 = (ListBoxItem)this.lstReferrer.SelectedItem;
            if (lbi1 == null)
                return;

            this.txtRefByID.Text = lbi1.Tag.ToString();
            this.txtRefByName.Text = lbi1.Content.ToString().Trim();
            this.txtRefByName.ToolTip = "Ref. ID : " + lbi1.Tag.ToString().Substring(6);
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
            this.btnNewShow.Focus();
        }

        private void lstReferrer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectReferrerInfo();
                this.btnNewShow.Focus();
            }
        }

        private void btnCloseRefBy_Click(object sender, RoutedEventArgs e)
        {
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
        }

        private void btnAddRefBy_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnAddRefBy.Content.ToString() == "Add/Edit")
            {
                this.stkpRefByList.Visibility = Visibility.Collapsed;
                this.stkpRefByList0.Visibility = Visibility.Collapsed;
                this.stkpRefByEntry.Visibility = Visibility.Visible;
                this.btnAddRefBy.Content = "Show List";
                this.lblRefByID1.Content = "";
                this.lblRefByID1.Tag = "";
                this.btnUpdateRefBy1.Tag = "New";
                this.lblRefByNameTitle1.Content = "DR.";
                this.txtRefByName1.Text = "";
                this.autoMktStaffRefSearch.SelectedValue = null;
                this.txtRefByQCTitle1.Text = "";
                this.txtRefByAddress1.Text = "";
                this.txtRefByPhone1.Text = "";
                this.txtRefByEMail1.Text = "";
                this.txtRefActive.Text = "A";
                this.txtRefMark.Text = "";
                this.cmbRefByTitle.SelectedIndex = 0;
                this.cmbRefByType.SelectedIndex = 0;
                this.btnUpdateRefBy1.IsEnabled = true;
                if (this.txtRefByName.Text.Trim().Length > 0)
                {
                    this.ShowRefRecordForEdit();

                }
                return;
            }
            this.stkpRefByEntry.Visibility = Visibility.Collapsed;
            this.stkpRefByList.Visibility = Visibility.Visible;
            this.stkpRefByList0.Visibility = Visibility.Visible;
            this.btnAddRefBy.Content = "Add/Edit";
        }

        private void ShowRefRecordForEdit()
        {
            string refbyID1 = this.txtRefByID.Text.Trim();
            if (refbyID1 == "000000000000")
                return;

            this.lblRefByID1.Content = refbyID1.Substring(6, 6);
            this.lblRefByID1.Tag = refbyID1;
            this.btnUpdateRefBy1.Tag = "Edit";

            var lst1 = this.RefByInfList.FindAll(x => x.refbyid == refbyID1);
            string refmktid1 = lst1[0].refmktid;
            this.autoMktStaffRefSearch.SelectedValue = null;
            if (refmktid1 != "000000000000")
            {
                this.autoMktStaffRefSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoMktStaffRefSearch.SelectedValue = lst1[0].refmktid;
            }
            this.lblRefByNameTitle1.Content = lst1[0].refbytitle;
            this.txtRefByName1.Text = lst1[0].refbynam.Trim();
            this.txtRefByQCTitle1.Text = lst1[0].refbyqc.Trim();
            this.txtRefByAddress1.Text = lst1[0].refbyadd.Trim();
            this.txtRefByPhone1.Text = lst1[0].phoneno.Trim();
            this.txtRefByEMail1.Text = lst1[0].emailid.Trim();
            this.txtRefActive.Text = lst1[0].refactive.Trim();
            this.txtRefMark.Text = lst1[0].refmark.Trim();
            string RefTitle1 = lst1[0].refbytitle.Trim();
            string RefType1 = lst1[0].refbytype.Trim();

            // REFACTIVE, REFMARK

            int idx1 = 0;
            foreach (var item in this.cmbRefByTitle.Items)
            {
                string item1 = ((ComboBoxItem)item).Tag.ToString();
                if (item1 == RefTitle1)
                {
                    this.cmbRefByTitle.SelectedIndex = idx1;
                    break;
                }
                idx1++;
            }

            idx1 = 0;
            foreach (var item in this.cmbRefByType.Items)
            {
                string item1 = ((ComboBoxItem)item).Tag.ToString();
                if (item1 == RefType1)
                {
                    this.cmbRefByType.SelectedIndex = idx1;
                    break;
                }
                idx1++;
            }
        }
        private void btnRefreshRef_Click(object sender, RoutedEventArgs e)
        {
            this.GetRefByInfList();
        }

        private void btnNewShow_Click(object sender, RoutedEventArgs e)
        {
            //string transID1 = this.txtPrevTransID.Text.Trim().ToUpper();


            if (this.btnNewShow.Content.ToString() == "_Next")
            {
                this.CleanUpScreen();
                this.stkpEntry.Visibility = Visibility.Collapsed;
                //this.stkpTitle2.Visibility = Visibility.Hidden;
                //this.stkpPrint.Visibility = Visibility.Hidden;
                //this.stkpPhoto.Visibility = Visibility.Hidden;
                //this.btnUpdateTrans.Tag = "Ok";
                this.stkpTitle1.IsEnabled = true;
                this.btnNewShow.Content = "_Ok";
                this.btnNewShow.Focusable = true;
                //this.cmbInvMode.SelectedIndex = 0;
                return;
            }
            if (!this.GetTransactionList())
                return;

            this.stkpTitle1.IsEnabled = false;
            this.stkpEntry.Visibility = Visibility.Visible;
            this.btnNewShow.Content = "_Next";
        }

        private bool GetTransactionList()
        {

            string refbyID1 = this.txtRefByID.Text.Trim();
            if (refbyID1 == "000000000000")
                return false;

            this.lstInvoice.ItemsSource = null;
            //this.CommInvSummList = null;
            string BrnCode1 = "%"; // ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString().Substring(0, 4);
            string SignInID1 = "%"; // (this.chkFilterUser.IsChecked == true ? "%" : WpfProcessAccess.SignedInUserList[0].hccode);
            string StartDate1 = this.xctk_dtpFrom.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = this.xctk_dtpTo.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = "CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = "COMMINVLIST01";

            //var pap1 = vmr.SetParamCommInvSummList(WpfProcessAccess.CompInfList[0].comcpcod, BrnCode, StartDate, EndDate, "CSI", SignInID, "A", "%", "%");
            this.CommInvSummShortList = null;
            this.dgvMemo.ItemsSource = null;
            //this.CommInvSummList == null || 
            if (!(this.CommInvSummList.Count > 0 && this.StartDate == Convert.ToDateTime(StartDate1) && this.EndDate == Convert.ToDateTime(EndDate1)))
            {
                this.StartDate = Convert.ToDateTime(this.xctk_dtpFrom.Text);
                this.EndDate = Convert.ToDateTime(this.xctk_dtpTo.Text);

                var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                              InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: "NONE", OrderBy: OrderBy1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return false;

                this.CommInvSummList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();
            }

            ////this.txtRefByName2.Text = this.txtRefByName.Text;


            this.xctk_dtpInvDat.Value = Convert.ToDateTime(StartDate1);
            this.xctk_dtpInvDat.Tag = Convert.ToDateTime(StartDate1).ToString("dd-MMM-yyyy hh:mm tt");

            this.txtTransID.Text = "";
            this.txtTransID.Tag = "";
            this.txtPatientName.Text = "";

            this.txtRefByName2.Text = "";
            this.txtRefByName2.ToolTip = "";


            this.CommInvSummShortList = this.CommInvSummList.FindAll(x => x.refbyid == refbyID1).OrderBy(y => y.ptinvnum).ToList();

            //this.lblRefByID1.Content = refbyID1.Substring(6, 6);
            //this.lblRefByID1.Tag = refbyID1;
            //this.btnUpdateRefBy1.Tag = "Edit";


            //string paName1 = this.txtSrchPatName.Text.Trim().ToUpper();
            //string phone1 = this.txtSrchPhoneNo.Text.Trim().ToUpper();
            //string invnum2 = this.txtSrchInvNo.Text.Trim();
            //if (paName1.Length > 0)
            //    this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptname.Contains(paName1)).ToList();

            //if (phone1.Length > 0)
            //    this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptphone.Contains(phone1)).ToList();

            //if (invnum2.Length > 0)
            //    this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptinvnum2.Contains(invnum2)).ToList();

            /*
             * 
             * 
               pap1.ProcID = ProcessID; //  "COMMINVMEMO01N" // "COMMINVLIST01" // "COMMINVDETAILS01" // "GROUPDETAILS01" ;
            pap1.parm01 = BrnchCod;
            pap1.parm02 = startDate;
            pap1.parm03 = EndDate;
            pap1.parm04 = InvNum;
            pap1.parm05 = PreparedBy;
            pap1.parm06 = InvStatus;
            pap1.parm07 = TerminalName;
            pap1.parm08 = SessionID;
            pap1.parm11 = OrderBy;
             
             */


            int slnum1 = 1;
            foreach (var item in this.CommInvSummShortList)
            {
                item.slnum = slnum1;
                ++slnum1;
            }
            //this.CommInvSummList.Sort(delegate(HmsEntityCommercial.CommInvSummInf x, HmsEntityCommercial.CommInvSummInf y)
            //{
            //    return (x.ptinvnum).CompareTo(y.ptinvnum);
            //});

            this.lstInvoice.ItemsSource = this.CommInvSummShortList;
            this.lstInvoice.Items.Refresh();
            return true;
        }
     

        private void txtInvSerch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtInvSerch_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void lstInvoice_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ShowInvData();
        }


        private void lstInvoice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.ShowInvData();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ShowInvData()
        {
            try
            {
                if (this.CommInvSummShortList.Count == 0)
                    return;

                if (this.lstInvoice.SelectedItem == null)
                    return;

                string MemoNum = ((HmsEntityCommercial.CommInvSummInf)this.lstInvoice.SelectedItem).ptinvnum;
                this.MemoEditView(memoNum: MemoNum);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HS.MIS-01 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void MemoEditView(string memoNum = "CSI201611110100012")
        {
            //this.lblUpdateMsg.Content = "";
            this.dgvMemo.ItemsSource = null;
            string[] month1 = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string memoDate = "01-" + month1[int.Parse(memoNum.Substring(7, 2))] + "-" + memoNum.Substring(3, 4) + " 12:00 AM";// DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");

            string BrnCode1 = "%";
            string SignInID1 = "%";
            string StartDate1 = DateTime.Parse(memoDate).AddDays(-1).ToString("dd-MMM-yyy hh:mm tt");// this.xctk_dtpSrchDat1.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = DateTime.Parse(memoDate).AddDays(32).ToString("dd-MMM-yyy hh:mm tt"); //this.xctk_dtpSrchDat2.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = memoNum;//"CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = "COMMINVMEMO01N";

            var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                                           InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: "NONE", OrderBy: OrderBy1);

            //var pap1 = vmr.SetParamCommInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            if (ds1 == null)
                return;

            if (ds1.Tables.Count == 0)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            DataRow dr1 = ds1.Tables[1].Rows[0];

            this.xctk_dtpInvDat.Value = Convert.ToDateTime(dr1["ptinvdat"]);
            this.xctk_dtpInvDat.Tag = Convert.ToDateTime(dr1["ptinvdat"]).ToString("dd-MMM-yyyy hh:mm tt");

            this.txtTransID.Text = dr1["ptinvnum2"].ToString();
            this.txtTransID.Tag = dr1["ptinvnum"].ToString();
            this.txtPatientName.Text = dr1["ptname"].ToString();

            this.txtRefByName2.Text = dr1["refbyid"].ToString().Substring(6) + " - " + dr1["rfFullName"].ToString();
            this.txtRefByName2.ToolTip = "Ref. By ID : " + dr1["refbyid"].ToString().Substring(6) + " - " + dr1["rfFullName"].ToString();

            //---------------------------------------
            // select slnum = row_number() over (order by gsircode, isircode), comcod, ptinvnum, gsircode, gsirdesc, isircode, isirdesc, isirunit, itemqty, itmam, idisam, inetam, ivatam, 
            // rowid from #tblinvm2 order by gsircode, isircode;

            //select slnum = row_number() over(order by gsircode, isircode, reptsl), comcod, ptinvnum, gsircode, gsirdesc, isircode, reptsl, isirdesc, isirunit, isirtype, itemqty, itmrate, itmam, 
            //idisam, inetam, icomam, icdisam, refscomp, ivatam, rowid, orderbyid, orderbyses, orderbyterm, ordertime, delivbyid, delivbyses, delivbyterm, delivtime, itemrmrk from #tblinvm2 
            //order by gsircode, isircode, reptsl;
            this.OrderItemList.Clear();
            foreach (DataRow row1 in ds1.Tables[0].Rows)
            {
                this.OrderItemList.Add(new vmEntryFrontDesk1.OrderItem()
                {
                    slnum = Convert.ToInt32(row1["slnum"]).ToString("00") + ".",
                    gsircode = row1["gsircode"].ToString(),
                    isircode = row1["isircode"].ToString(),
                    reptsl = row1["reptsl"].ToString(),
                    gsirdesc = row1["gsirdesc"].ToString().Trim(),
                    sirdesc = row1["isirdesc"].ToString(),
                    sirunit = row1["isirunit"].ToString(),
                    sirtype = row1["isirtype"].ToString(),
                    itemqty = Convert.ToDecimal(row1["itemqty"]),
                    salrate = Convert.ToDecimal(row1["itmrate"]),
                    salam = Convert.ToDecimal(row1["itmam"]),
                    idisam = Convert.ToDecimal(row1["idisam"]),
                    idisper = "",
                    refscomp = Convert.ToDecimal(row1["refscomp"]),
                    refscompstd = Convert.ToDecimal(row1["refscompstd"]),
                    refpermark = (Convert.ToDecimal(row1["refscomp"]) > 0 ? "%" : ""),
                    icomam = Convert.ToDecimal(row1["icomam"]), //0.00m,
                    icdisam = Convert.ToDecimal(row1["icdisam"]), // 0.00m,
                    inetam = Convert.ToDecimal(row1["inetam"]), // serviceItem1.saleprice,
                    ivatam = Convert.ToDecimal(row1["ivatam"]), // 0.00m
                    readmode = "False",
                    orderbyid = row1["orderbyid"].ToString(),
                    orderbyses = row1["orderbyses"].ToString(),
                    orderbyterm = row1["orderbyterm"].ToString(),
                    ordertime = DateTime.Parse(row1["ordertime"].ToString()),
                    rptdocid = row1["rptdocid"].ToString(),
                    rptlogbyid = row1["rptlogbyid"].ToString(),
                    rptlogbyses = row1["rptlogbyses"].ToString(),
                    rptlogbyterm = row1["rptlogbyterm"].ToString(),
                    delivbyid = row1["delivbyid"].ToString(),
                    delivbyses = row1["delivbyses"].ToString(),
                    delivbyterm = row1["delivbyterm"].ToString().Trim(),
                    delivered = (row1["icsmark"].ToString().Trim().Length > 0 ? true : false),
                    delivtime = DateTime.Parse(row1["delivtime"].ToString()),
                    icsmark = row1["icsmark"].ToString().Trim(),
                    itemrmrk = row1["itemrmrk"].ToString().Trim(),
                    newold = "OldRow"
                });
            }
            this.btnTotal_Click(null, null);
            this.dgvMemo.ItemsSource = this.OrderItemList;
            this.dgvMemo.Items.Refresh();
        }

        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            var MarkedList1 = this.OrderItemList.FindAll(x => x.delivered == true);
            string memoNum = this.txtTransID.Tag.ToString();    // "CSI201611110100012";
            string memoDate = DateTime.Parse(this.xctk_dtpInvDat.Text).ToString("dd-MMM-yyyy");    // "01-Jan-2017";

            string BrnCode1 = "%";
            string SignInID1 = "%";
            string StartDate1 = DateTime.Parse(memoDate).AddDays(-1).ToString("dd-MMM-yyy hh:mm tt");// this.xctk_dtpSrchDat1.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = DateTime.Parse(memoDate).AddDays(1).ToString("dd-MMM-yyy hh:mm tt"); //this.xctk_dtpSrchDat2.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = memoNum;//"CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = "COMMINVMEMO01N";
            var pap1r = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: "NONE", OrderBy: OrderBy1);

            //var pap1 = vmr.SetParamCommInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1r = WpfProcessAccess.GetHmsDataSet(pap1r);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            if (ds1r == null)
                return;

            DataRow dr1 = ds1r.Tables[1].Rows[0];
            DataRow dr5 = ds1r.Tables[4].Rows[0];

            Hashtable Pat1 = new Hashtable();
            Pat1["MEMONUM"] = dr1["ptinvnum"].ToString();               //  this.txtTransID.Tag.ToString().Trim();
            Pat1["NAME"] = dr1["ptname"].ToString();                    //  this.txtPatientName.Text.Trim();
            Pat1["AGEY"] = int.Parse(dr5["ptagey"].ToString());         //  this.iudAgeY.Value;
            Pat1["AGEM"] = int.Parse(dr5["ptagem"].ToString());         //  this.iudAgeM.Value;
            Pat1["AGED"] = int.Parse(dr5["ptaged"].ToString());         //  this.iudAgeD.Value;
            Pat1["GENDRR"] = dr1["ptgender"].ToString();                // ((ComboBoxItem)this.cmbPatientGender.SelectedItem).Content.ToString();
            Pat1["PHONE"] = dr1["ptphone"].ToString();                  //  this.txtContactNo.Text.Trim();
            Pat1["MEMID"] = dr1["refcardno"].ToString();                //  this.txtMemberID.Text.Trim();
            Pat1["DELTIM"] = Convert.ToDateTime(dr1["delivartime"]).ToString("dd-MMM-yyyy hh:mm tt");    // this.xcdtDeliveryDT.Text.Trim();//.Substring(4);
            Pat1["REFBYID"] = dr1["refbyid"].ToString();                //  this.txtRefByID.Text.Trim();
            Pat1["CCAMT"] = Convert.ToDecimal(dr1["cccharge"]).ToString(); // "0" + this.txtCCCharge.Text.Trim();
            Pat1["CCPAID"] = Convert.ToDecimal(dr1["ccpaidam"]).ToString(); //"0" + this.txtCCPaid.Text.Trim();
            Pat1["RMRKS"] = dr1["ptinvnote"].ToString();                //  this.txtRemarks.Text.Trim();
            Pat1["REFRMRKS"] = dr1["ptrefnote"].ToString();             //  this.txtRefRemarks.Text.Trim();
            Pat1["DUEAM"] = "0.00";     // this.lblNetBalance.Content.ToString().Trim();
            Pat1["REFSTAFF"] = dr1["refstaffid"].ToString();            //  (this.autoStaffRefSearch.SelectedValue == null ? "000000000000" : this.autoStaffRefSearch.SelectedValue);
            Pat1["DISCTYPE"] = dr1["disctype"].ToString().Trim();       //  this.cmbDisType.Tag.ToString();
            Pat1["STATUS"] = "A";
            Pat1["PATPHOTO"] = ((dr5["ptphoto"] is DBNull) ? "" : dr5["ptphoto"].ToString());

            var OrderItemList2 = new List<vmEntryFrontDesk1.OrderItem>();
            foreach (DataRow row1 in ds1r.Tables[0].Rows)
            {
                OrderItemList2.Add(new vmEntryFrontDesk1.OrderItem()
                {
                    slnum = Convert.ToInt32(row1["slnum"]).ToString("00") + ".",
                    gsircode = row1["gsircode"].ToString(), // serviceItem1.gsircode,
                    isircode = row1["isircode"].ToString(), // serviceItem1.sircode,
                    reptsl = row1["reptsl"].ToString(),
                    gsirdesc = row1["gsirdesc"].ToString().Trim(), // + ", " + row1["isirdesc"].ToString().Trim(), // serviceItem1.gsirdesc,
                    sirdesc = row1["isirdesc"].ToString(), // serviceItem1.sirdesc,
                    sirunit = row1["isirunit"].ToString(), // serviceItem1.sirunit,
                    sirtype = row1["isirtype"].ToString(), // serviceItem1.sirtype,
                    itemqty = Convert.ToDecimal(row1["itemqty"]), // 1.00m,
                    salrate = Convert.ToDecimal(row1["itmrate"]), //serviceItem1.saleprice,
                    salam = Convert.ToDecimal(row1["itmam"]), // serviceItem1.saleprice,
                    idisam = Convert.ToDecimal(row1["idisam"]), // 0.00m,
                    idisper = "",
                    refscomp = Convert.ToDecimal(row1["refscomp"]), // serviceItem1.refscomp,
                    refscompstd = Convert.ToDecimal(row1["refscompstd"]), // serviceItem1.refscomp,
                    refpermark = (Convert.ToDecimal(row1["refscomp"]) > 0 ? "%" : ""),
                    icomam = Convert.ToDecimal(row1["icomam"]), //0.00m,
                    icdisam = Convert.ToDecimal(row1["icdisam"]), // 0.00m,
                    inetam = Convert.ToDecimal(row1["inetam"]), // serviceItem1.saleprice,
                    ivatam = Convert.ToDecimal(row1["ivatam"]), // 0.00m
                    readmode = "False",
                    orderbyid = row1["orderbyid"].ToString(),
                    orderbyses = row1["orderbyses"].ToString(),
                    orderbyterm = row1["orderbyterm"].ToString(),
                    ordertime = DateTime.Parse(row1["ordertime"].ToString()),
                    rptdocid = row1["rptdocid"].ToString(),
                    rptlogbyid = row1["rptlogbyid"].ToString(),
                    rptlogbyses = row1["rptlogbyses"].ToString(),
                    rptlogbyterm = row1["rptlogbyterm"].ToString(),
                    delivbyid = row1["delivbyid"].ToString(),
                    delivbyses = row1["delivbyses"].ToString(),
                    delivbyterm = row1["delivbyterm"].ToString().Trim(),
                    delivered = (row1["delivbyid"].ToString().Trim().Length == 12 && row1["delivbyid"].ToString() != "000000000000" ? true : false),
                    delivtime = DateTime.Parse(row1["delivtime"].ToString()),
                    icsmark = row1["icsmark"].ToString().Trim(),
                    itemrmrk = row1["itemrmrk"].ToString().Trim(),
                    newold = "OldRow"
                });
            }

            foreach (var item in OrderItemList2)
            {
                item.icsmark = "";
                var item2 = MarkedList1.FindAll(x => x.isircode == item.isircode && x.reptsl == item.reptsl);
                if (item2.Count > 0)
                    item.icsmark = "X";
            }

            this.CollInfoList.Clear();
            // select slnum, comcod, ptinvnum, bilcolid, bilcoldat, bilcolam, bcnote, preparebyid, preparebynam, prepareses, preparetrm, rowid, rowtime from #tblinvm3 order by slnum;
            this.CollInfoList = ds1r.Tables[3].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01TblCol>();
            this.CollInfoList = this.CollInfoList.FindAll(x => !x.bilcolid.Contains("CC"));
            this.CollInfoList.Sort(delegate(HmsEntityCommercial.CommInv01.CommInv01TblCol x, HmsEntityCommercial.CommInv01.CommInv01TblCol y)
            {
                return (x.bilcolid).CompareTo(y.bilcolid);
            });

            foreach (var item in this.CollInfoList)
            {
                item.comcod = "False";
                item.preparebynam = "OldRow";
            }
            this.CollInfoList = this.CollInfoList.FindAll(x => x.bilcolam > 0 || x.comcod == "True");
            int xa = 1;
            foreach (var item in this.CollInfoList)
            {
                item.bilcolid = "C" + xa.ToString("000");
                xa++;
            }

            //preparebyid, preparebynam = signinnam, prepareses, preparetrm

            BrnCode1 = memoNum.Substring(9, 4);//CSI 2018 06 110101651 substring(ptinvnum, 10, 4);// ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString();

            string preparebyid1 = dr1["preparebyid"].ToString();
            string prepareses1 = dr1["prepareses"].ToString();
            string preparetrm1 = dr1["preparetrm"].ToString();  

            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpInvDat.Text.ToString()),
                 BrnCod: BrnCode1, OrderItemList: OrderItemList2, CollInfoList: this.CollInfoList, Pat1: Pat1,
                _preparebyid: preparebyid1, _prepareses: prepareses1, _preparetrm: preparetrm1);

            string UpdateType = "EDIT_INVOICE";     // this.lblInvMode.Tag.ToString();
            var pap1 = vm1.SetParamUpdateCommInvoice(WpfProcessAccess.CompInfList[0].comcod, ds1, UpdateType);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblUpdateMsg.Content = "Invoice : " + this.txtTransID.Text + " Updated Successfully.";
            //System.Windows.MessageBox.Show("Update Successfull", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }
     
        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (this.FrmInitialized == false)
                    return;
                this.lblUpdateMsg.Content = "";
                this.OrderItemList = this.OrderItemList.FindAll(x => x.itemqty > 0);

                decimal GrandTotal1 = this.OrderItemList.Sum(x => x.salam);
                decimal DiscTotal1 = this.OrderItemList.Sum(x => x.idisam);
                decimal NetTotal = this.OrderItemList.Sum(x => x.inetam);
                decimal ProfitTotal = this.OrderItemList.Sum(x => x.icomam);

                this.lblGrandTotal.Content = GrandTotal1.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblDiscTotal.Content = DiscTotal1.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblNetTotal.Content = NetTotal.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblProfitTotal.Content = ProfitTotal.ToString("#,##0;(#,##0); - ");// " -  ";
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HS.MIS-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void autoMktStaffRefSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStaffRefSirdesc(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetStaffRefSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
               WpfProcessAccess.StaffList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }
     
    }
}
