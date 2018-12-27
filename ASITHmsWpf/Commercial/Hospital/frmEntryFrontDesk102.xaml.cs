using ASITFunLib;
using ASITHmsEntity;
using ASITHmsRpt4Commercial;
using ASITHmsViewMan.Commercial;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections;
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

namespace ASITHmsWpf.Commercial.Hospital
{
    /// <summary>
    /// Interaction logic for frmEntryFrontDesk102.xaml
    /// </summary>
    public partial class frmEntryFrontDesk102 : UserControl
    {
        private DateTime StartDate, EndDate;
        private bool FrmInitialized = false;
        private List<HmsEntityCommercial.CommInvSummInf> CommInvSummList = new List<HmsEntityCommercial.CommInvSummInf>();             // Hospital/Diagnostic Centre Commercial Invoice Summary List
        private List<HmsEntityCommercial.CommInvSummInf> CommInvSummShortList = new List<HmsEntityCommercial.CommInvSummInf>();             // Hospital/Diagnostic Centre Commercial Invoice Summary List
        private List<vmEntryFrontDesk1.OrderItem> OrderItemList = new List<vmEntryFrontDesk1.OrderItem>();
        private List<HmsEntityCommercial.CommInv01.CommInv01TblCol> CollInfoList = new List<HmsEntityCommercial.CommInv01.CommInv01TblCol>();
        private List<HmsEntityGeneral.SirInfCodeBook> SirCodeList4502 = new List<HmsEntityGeneral.SirInfCodeBook>();
        //private List<HmsEntityCommercial.HmsServiceItem> ServiceItemList = new List<HmsEntityCommercial.HmsServiceItem>();             // Hospital/Diagnostic Centre Service Item List
        //private List<HmsEntityCommercial.ReportingBill01> ReportingBillList = new List<HmsEntityCommercial.ReportingBill01>();             // Hospital/Diagnostic Centre Service Item List

        private vmEntryFrontDesk1 vm1 = new vmEntryFrontDesk1();
        private vmReportFrontDesk1 vmr = new vmReportFrontDesk1();
        public frmEntryFrontDesk102()
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
            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;
                this.Objects_On_Init();
            }

        }

        private void Objects_On_Init()
        {
            try
            {
                this.GetBranchList();
                ////this.GetServiceItemList(itrmGroup: "4502%");
                this.xctk_dtpSrchDat1.Value = DateTime.Today.AddDays(-3);
                this.xctk_dtpSrchDat2.Value = DateTime.Today;
                this.StartDate = Convert.ToDateTime(this.xctk_dtpSrchDat1.Text);
                this.EndDate = Convert.ToDateTime(this.xctk_dtpSrchDat2.Text);

                if (WpfProcessAccess.AccSirCodeList == null)
                    WpfProcessAccess.GetAccSirCodeList();
                this.SirCodeList4502 = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(0, 4) == "4502" && x.sircode.Substring(7, 5) != "00000").ToList();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
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
        ////private void GetServiceItemList(string itrmGroup = "4502%")
        ////{
        ////    if (WpfProcessAccess.CompInfList == null)
        ////        return;

        ////    this.ServiceItemList = null;
        ////    var pap1 = vmr.SetParamServiceItemList(WpfProcessAccess.CompInfList[0].comcpcod, itrmGroup);
        ////    DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
        ////    if (ds1 == null)
        ////        return;

        ////    this.ServiceItemList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.HmsServiceItem>();
        ////}

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }


        private void autoTestItemSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetServiceItemDesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetServiceItemDesc(string Pattern)
        {
            // match on contain (could do starts with)
            if (this.chkMainGroup.IsChecked == true)
                return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                    this.SirCodeList4502.Where((x, match) => (x.sircode.Substring(9, 3) == "000") && (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                this.SirCodeList4502.Where((x, match) => (x.sircode.Substring(9, 3) != "000") && (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoStaffRefSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
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

        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoStaffRptSearch.SelectedValue == null)
                return;

            string ItemID1 = "000000000000";
            if (this.autoTestItemSearch.SelectedValue != null)// || this.autoStaffRptSearch.SelectedValue == null)          
                ItemID1 = this.autoTestItemSearch.SelectedValue.ToString().Trim();

            string rptDocID1 = this.autoStaffRptSearch.SelectedValue.ToString().Trim();


            //var MarkedList1 = this.OrderItemList.FindAll(x => x.isircode == itemID1 && x.readmode.Trim() == "True");
            int len1 = (ItemID1.Substring(9, 3) != "000" ? 12 : 9);
            var MarkedList1 = this.OrderItemList.FindAll(x => (ItemID1 == "000000000000" ? true : x.isircode.Substring(0, len1) == ItemID1.Substring(0, len1)) && x.readmode.Trim() == "True");

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
                var item2 = MarkedList1.FindAll(x => x.isircode == item.isircode && x.reptsl == item.reptsl);
                if (item2.Count > 0)
                {
                    item.rptdocid = (item2[0].delivered == true ? rptDocID1 : "000000000000");
                    item.rptlogbyid = WpfProcessAccess.SignedInUserList[0].hccode;
                    item.rptlogbyses = WpfProcessAccess.SignedInUserList[0].sessionID;
                    item.rptlogbyterm = WpfProcessAccess.SignedInUserList[0].terminalID;
                }
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

        private void ShowInvData()
        {
            try
            {
                if (this.CommInvSummShortList.Count == 0)
                    return;

                if (this.lstInvoice.SelectedItem == null)
                    return;

                this.chkRptDoct.IsChecked = false;
                string MemoNum = ((HmsEntityCommercial.CommInvSummInf)this.lstInvoice.SelectedItem).ptinvnum;
                this.MemoEditView(memoNum: MemoNum);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HS.Rpt.Con-01 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void MemoEditView(string memoNum = "CSI201611110100012")
        {
            string ItemID1 = "000000000000";
            if (this.autoTestItemSearch.SelectedValue != null)// || this.autoStaffRptSearch.SelectedValue == null)          
                ItemID1 = this.autoTestItemSearch.SelectedValue.ToString().Trim();

            string rptDocID1 = "000000000000";
            if (this.autoStaffRptSearch.SelectedValue != null)
                rptDocID1 = this.autoStaffRptSearch.SelectedValue.ToString().Trim();

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

            int len1 = (ItemID1.Substring(9, 3) != "000" ? 12 : 9);
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
                    readmode = ((row1["isircode"].ToString().Trim().Substring(0, 7) != "4502905" 
                                && (ItemID1 == "000000000000" ? true : row1["isircode"].ToString().Trim().Substring(0, len1) == ItemID1.Substring(0, len1))
                                && rptDocID1 != "000000000000") && (row1["rptdocid"].ToString().Trim() == "000000000000" || row1["rptdocid"].ToString().Trim() == rptDocID1) ? "True" : "False"),
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
                    delivered = (row1["rptdocid"].ToString().Trim() != "000000000000" ? true : false),
                    delivtime = DateTime.Parse(row1["delivtime"].ToString()),
                    icsmark = row1["icsmark"].ToString().Trim(),
                    itemrmrk = row1["itemrmrk"].ToString().Trim(),
                    newold = "OldRow"
                });
            }
            var Shortlist1 = this.OrderItemList.FindAll(x => x.rptdocid != "000000000000");
            foreach (var item in Shortlist1)
            {
                var lst2 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == item.rptdocid);
                if (lst2.Count > 0)
                    item.idisper = lst2[0].sirdesc1.Trim();
            }


            this.btnTotal_Click(null, null);
            this.dgvMemo.ItemsSource = this.OrderItemList;
            this.dgvMemo.Items.Refresh();
            int rowid1 = 0;
            foreach (var item in this.OrderItemList)
            {
                //if (item.readmode == "True")
                if (item.isircode.Substring(0, len1) == ItemID1.Substring(0, len1))
                    break;
                rowid1++;
            }
            rowid1 = (this.OrderItemList.Count <= rowid1 ? 0 : rowid1);

            var item1a = (vmEntryFrontDesk1.OrderItem)this.dgvMemo.Items[rowid1];
            this.dgvMemo.SelectedItem = item1a;
            this.dgvMemo.ScrollIntoView(item1a);
            this.dgvMemo.Focus();
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
                System.Windows.MessageBox.Show("HS.Rpt.Con-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnFilter1_Click(object sender, RoutedEventArgs e)
        {
            this.chkRptDoct.IsChecked = false;
            this.ClearInvoiceContent();
            if (this.btnFilter1.Content.ToString().Trim() == "_Next")
            {
                this.txtInvSerch.Text = "";
                this.txtSrchInvNo.Text = "";
                this.lstInvoice.ItemsSource = null;
                this.autoTestItemSearch.SelectedValue = null;
                this.btnFilter1.Content = "_Ok";
                this.stkpFilter1.IsEnabled = true;
                return;
            }

            if (!this.GetTransactionList())
                return;

            this.btnFilter1.Content = "_Next";
            this.stkpFilter1.IsEnabled = false;
        }
        private bool GetTransactionList()
        {
            string ItemID1 = "000000000000";
            if (this.autoTestItemSearch.SelectedValue != null)
                ItemID1 = this.autoTestItemSearch.SelectedValue.ToString().Trim();
            //if (ItemID1 == "000000000000" || ItemID1.Length == 0)
            //    return false;

            this.lstInvoice.ItemsSource = null;
            //this.CommInvSummList = null;
            string BrnCode1 = ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString().Substring(0, 4);
            string SignInID1 = "%"; // (this.chkFilterUser.IsChecked == true ? "%" : WpfProcessAccess.SignedInUserList[0].hccode);
            string StartDate1 = this.xctk_dtpSrchDat1.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = this.xctk_dtpSrchDat2.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = "CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = (ItemID1 == "000000000000" ? "COMMINVLIST01" : "COMMINVDETAILS01");
            this.CommInvSummShortList = null;
            this.dgvMemo.ItemsSource = null;

            //if (!(this.CommInvSummList.Count > 0 && this.StartDate == Convert.ToDateTime(StartDate1) && this.EndDate == Convert.ToDateTime(EndDate1)))
            {
                this.CommInvSummList = null;
                this.StartDate = Convert.ToDateTime(this.xctk_dtpSrchDat1.Text);
                this.EndDate = Convert.ToDateTime(this.xctk_dtpSrchDat2.Text);

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
            int len1 = (ItemID1.Substring(9, 3) != "000" ? 12 : 9);// (this.chkMainGroup.IsChecked == true ? 9 : 12);
            if (ItemID1 != "000000000000")
                this.CommInvSummShortList = this.CommInvSummList.FindAll(x => x.refbyid.Substring(0, len1) == ItemID1.Substring(0, len1));
            else
                this.CommInvSummShortList = this.CommInvSummList.ToList();

            this.CommInvSummShortList = this.CommInvSummShortList.GroupBy(x => x.ptinvnum).Select(y => y.First()).OrderBy(z => z.ptinvnum).ToList();

            string invNo1List = this.txtSrchInvNo.Text.Trim();
            if (invNo1List.Length > 0)
            {
                string[] InvArr1 = invNo1List.Split(',');
                foreach (var ae1 in InvArr1)
                {
                    string ae2 = ae1.Trim();
                    var list1 = this.CommInvSummShortList.FindAll(x => x.ptinvnum.Contains(ae2));
                    foreach (var inv1 in list1)
                        inv1.rowid = 1;
                }
                this.CommInvSummShortList = this.CommInvSummShortList.FindAll(x => x.rowid == 1).OrderBy(y => y.ptinvnum).ToList();
            }

            int slnum1 = 1;
            foreach (var item in this.CommInvSummShortList)
            {
                item.ptinvnum2 = item.brnsnam.Trim() + item.ptinvdat.ToString("yyMM") + item.ptinvnum.Substring(13);
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
        private void ClearInvoiceContent()
        {

            this.xctk_dtpInvDat.Value = DateTime.Today;
            this.xctk_dtpInvDat.Tag = "";

            this.txtTransID.Text = "";
            this.txtTransID.Tag = "";
            this.txtPatientName.Text = "";

            this.txtRefByName2.Text = "";
            this.txtRefByName2.ToolTip = null;
            this.dgvMemo.ItemsSource = null;
            this.OrderItemList.Clear();
            this.btnTotal_Click(null, null);
            this.autoStaffRptSearch.SelectedValue = null;
        }
        private void chkRptDoct_Click(object sender, RoutedEventArgs e)
        {
            this.ClearInvoiceContent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            string rptDocID1 = "%";
            if (this.autoStaffRptSearch.SelectedValue != null)
                rptDocID1 = this.autoStaffRptSearch.SelectedValue.ToString().Trim();


            string BrnCode1 = ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString().Substring(0, 4);
            if (this.chkAllBranches.IsChecked == true)
                BrnCode1 = "%";

            string SignInID1 = rptDocID1; // (this.chkFilterUser.IsChecked == true ? "%" : WpfProcessAccess.SignedInUserList[0].hccode);
            string StartDate1 = this.xctk_dtpSrchDat1.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = this.xctk_dtpSrchDat2.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = "CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = "TESTITEMBILL01";
            this.CommInvSummShortList = null;
            this.dgvMemo.ItemsSource = null;

            var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                            InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: "NONE", OrderBy: OrderBy1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var RptListc = ds1.Tables[0].DataTableToList<HmsEntityCommercial.ReportingBill01>();
            int sl1 = 1;
            string PrevDocid = "XXXXXXXXXXXX";
            foreach (var item in RptListc)
            {
                if (PrevDocid != item.rptdocid)
                    sl1 = 1;
                item.slnum = sl1;
                PrevDocid = item.rptdocid;
                sl1++;
            }


            DateTime ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
            string WindowTitle1 = ds1.Tables[1].Rows[0]["RptTitle"].ToString();
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: ServerTime1);
            list3[0].RptHeader1 = ds1.Tables[1].Rows[0]["RptTitle"].ToString();
            list3[0].RptHeader2 = ds1.Tables[1].Rows[0]["RptPeriod"].ToString();
            LocalReport rpt1 = CommReportSetup.GetLocalReport("Hospital.RptReportingBill01", RptListc, null, list3);

            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

    }
}
