using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Inventory;
using ASITHmsRpt2Inventory;
using Microsoft.Reporting.WinForms;
using System.ComponentModel;
using ASITHmsViewMan.General;
using System.Collections.ObjectModel;
using ASITHmsViewMan.Accounting;
using System.Collections;


namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmEntryPurLCInfo1.xaml
    /// </summary>
    public partial class frmEntryPurLCInfo1 : UserControl
    {
        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        //private List<vmHmsGeneralList1.GenDetailsListInfo> ListGenDetailsInfo = new List<vmHmsGeneralList1.GenDetailsListInfo>();

        private List<vmEntryPur01.LcGenInfo> LCGeneralInfoList = new List<vmEntryPur01.LcGenInfo>();
        private List<vmEntryPur01.LcPayInfo> LCPaymentInfoList = new List<vmEntryPur01.LcPayInfo>();
        private List<vmEntryPur01.LcCostInfo> LCCostInfoList = new List<vmEntryPur01.LcCostInfo>();

        private vmReportAccounts1 vmrptAcc = new vmReportAccounts1();
        private vmEntryPur01 vmp1 = new vmEntryPur01();

        private vmReportStore1 vm1r = new vmReportStore1();
        private DataSet EditDs;
        private bool FrmInitialized = false;
        private bool IsActiveTransListWindow { get; set; }

        public frmEntryPurLCInfo1()
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
                this.CleanUpObjects();
                this.FrmInitialized = true;
                this.IsActiveTransListWindow = false;
                this.ConstructAutoCompletionSource();
            }
        }

        private void CleanUpObjects()
        {
            this.stkpLCDetailsInfo.Visibility = Visibility.Collapsed;
            this.btnPrint.Visibility = Visibility.Hidden;
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.dgLCGenInfo.ItemsSource = null;
            this.dgLcPayInfo.ItemsSource = null;
            this.dgLCItemInfo.ItemsSource = null;


            this.LCGeneralInfoList.Clear();
            this.LCPaymentInfoList.Clear();
            //foreach (var item in this.LCPaymentInfoList)
            //{
            //    item.payamt = 0.00m;
            //    item.payper = 0.00m;
            //}
            this.LCCostInfoList.Clear();
        }
        private void ConstructAutoCompletionSource()
        {

            if (WpfProcessAccess.AccSirCodeList == null)
                WpfProcessAccess.GetAccSirCodeList();

            var lcinfo = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(0, 4) == "2502" && x.sircode.Substring(9, 3) != "000");

            this.AtxtPlcId.AutoSuggestionList.Clear();
            foreach (var item1 in lcinfo)
            {
                this.AtxtPlcId.AddSuggstionItem(item1.sircode + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());   //.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
                var mitm1b = new MenuItem() { Header = item1.sircode + " - " + item1.sirdesc.Trim(), Tag = item1.sircode.Trim() };
                mitm1b.Click += conMenuPlcId_MouseClick;
                this.conMenuPlcId.Items.Add(mitm1b);
            }

            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();

            if (WpfProcessAccess.AccCodeList == null)
                WpfProcessAccess.GetAccCodeList();
            var list1 = WpfProcessAccess.AccCodeList.FindAll(x => x.actcode.Substring(0, 8) == "14020001" && (Convert.ToInt64(x.actcode) >= 140200010001 && Convert.ToInt64(x.actcode) <= 140200010099));
            int sl1 = 1;
            foreach (var item in list1)
            {
                this.LCPaymentInfoList.Add(new vmEntryPur01.LcPayInfo()
               {
                   lsircode = "000000000000",
                   slnum = sl1,
                   actcode = item.actcode,
                   actdesc = item.actdesc,
                   payamt = 0.00m,
                   payper = 0
               });
                sl1++;
            }
        }

        private void conMenuPlcId_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtPlcId.Value = ((MenuItem)sender).Tag.ToString().Trim();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AtxtPlcId_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.conMenuPlcId.IsOpen = true;
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.CleanUpObjects();
            if (this.btnOk.Content.ToString() == "_Next")
            {
                this.lblLCDesc1.Visibility = Visibility.Collapsed;
                this.AtxtPlcId.Visibility = Visibility.Visible;
                this.btnPrint.Visibility = Visibility.Hidden;
                this.btnUpdate.Visibility = Visibility.Hidden;
                this.btnOk.Content = "_Ok";
                return;
            }


            if (this.AtxtPlcId.Text.Trim().Length == 0)
                return;
            string sirCod1 = this.AtxtPlcId.Value.ToString();// this.lblSirCode.Tag.ToString();

            if (sender == null)
                return;
            string sirCod2 = sirCod1.Substring(0, 4);

            //if (WpfProcessAccess.GenInfoTitleList == null)
            //    WpfProcessAccess.GetGenInfoTitleList();



            //string gcodeGroup = ((sirCod2.Substring(0, 1) == "5" || sirCod2.Substring(0, 2) == "98" || sirCod2.Substring(0, 2) == "99") ? "SICD" : (sirCod2.Substring(0, 4) == "2502" ? "SILC" : "YYYY"));

            //var pap1 = this.vmGenList1.SetParamGenDetailsInf(WpfProcessAccess.CompInfList[0].comcpcod, "SIRINF", sirCod1, gcodeGroup);
            var pap1 = this.vmp1.SetParamToGetLCCostSheetInfo(WpfProcessAccess.CompInfList[0].comcpcod, sirCod1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            this.LCGeneralInfoList = ds1.Tables[0].DataTableToList<vmEntryPur01.LcGenInfo>();
            string actcod1 = "XXXXXXXXXXXX";
            foreach (var item in this.LCGeneralInfoList)
            {
                if (item.actcode == actcod1)
                    item.actdesc = "          Do";
                else
                    actcod1 = item.actcode;
            }

            this.LCCostInfoList = ds1.Tables[1].DataTableToList<vmEntryPur01.LcCostInfo>();//.Add(new vmEntryPur01.LcCostInfo()

            //actdesc = "          Do"
            //this.LCGeneralInfoList.Clear();
            //this.LCPaymentInfoList.Clear();
            //foreach (DataRow dr1 in ds1.Tables[0].Rows)
            //{
            //    this.LCGeneralInfoList.Add(new vmEntryPur01.LcGenInfo()
            //    {
            //        lsircode = dr1["tblcode"].ToString().Trim(),
            //        slnum = Convert.ToInt32(dr1["slnum"]),
            //        actcode = dr1["gencode"].ToString().Trim(),
            //        actdesc = dr1["gendesc"].ToString().Trim(),
            //        repeatsl = dr1["repeatsl"].ToString().Trim(),
            //        lcgendata = dr1["dataval"].ToString().Trim()
            //    });
            //}

            string fromDate1 = DateTime.Today.AddDays(-720).ToString("dd-MMM-yyyy");
            string ToDate1 = DateTime.Today.ToString("dd-MMM-yyyy");

            var pap1p = vmrptAcc.SetParamSubVsMainTrans1(WpfProcessAccess.CompInfList[0].comcod, ReportType: "Summary", FromDate: fromDate1, ToDate: ToDate1, BrnCod: "",
                        SectCod: "", ActCode: "", SirCode: sirCod1, VouType: "%");
            DataSet ds1p = WpfProcessAccess.GetHmsDataSet(pap1p);
            if (ds1p == null)
                return;

            foreach (DataRow dr1p in ds1p.Tables[0].Rows)
            {
                if (dr1p["trcode"].ToString().Substring(0, 4) == "1402" &&
                    (Convert.ToInt64(dr1p["trcode"].ToString()) >= 140200010001 && Convert.ToInt64(dr1p["trcode"].ToString()) <= 140200090099))
                {
                    //var item1 = this.LCPaymentInfoList.FindAll(x => x.actcode == dr1p["trcode"].ToString());
                    //if(item1.Count>0)
                    //    item1[0].payamt = Convert.ToDecimal(dr1p["dram"]);

                    this.LCPaymentInfoList.Add(new vmEntryPur01.LcPayInfo()
                    {
                        lsircode = sirCod1,
                        slnum = 0,
                        actcode = dr1p["trcode"].ToString().Trim(),
                        actdesc = dr1p["trdesc"].ToString().Trim(),
                        payamt = Convert.ToDecimal(dr1p["dram"]),
                        payper = 0
                    });
                }
            }

            this.stkpLCDetailsInfo.Visibility = Visibility.Visible;
            this.btnRefresh_Click(null, null);
            this.lblLCDesc1.Content = this.AtxtPlcId.Text.Trim();
            this.AtxtPlcId.Visibility = Visibility.Collapsed;
            this.lblLCDesc1.Visibility = Visibility.Visible;
            this.btnPrint.Visibility = Visibility.Visible;
            this.btnUpdate.Visibility = Visibility.Visible;
            this.btnOk.Content = "_Next";
        }

        private void lbldgExtraInfoRptSlno_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm to add space", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
               MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            string sirCode1 = this.AtxtPlcId.Value.ToString();
            string genCode1 = ((Label)sender).Tag.ToString();
            this.dgLCGenInfo.ItemsSource = null;
            this.dgLcPayInfo.ItemsSource = null;
            this.dgLCItemInfo.ItemsSource = null;

            int index1 = this.LCGeneralInfoList.FindLastIndex(x => x.actcode == genCode1);
            string gentype1 = this.LCGeneralInfoList[index1].acttype;
            string newRptNo = (int.Parse(this.LCGeneralInfoList[index1].repeatsl.Trim()) + 1).ToString();

            this.LCGeneralInfoList.Add(new vmEntryPur01.LcGenInfo()
            {
                slnum = 0,
                actcode = genCode1,
                actdesc = "          Do",
                acttype = gentype1,
                repeatsl = newRptNo,
                lsircode = sirCode1,
                lcgendata = "",
                lcgendata2 = "",
                gendataw1 = 420,
                gendataw2 = 0

            });
            /*
                         public string lcgendata2 { get; set; }
            public int gendataw1 { get; set; }
            public int gendataw2 { get; set; }
             
             */
            this.LCGeneralInfoList.Sort(delegate(vmEntryPur01.LcGenInfo x, vmEntryPur01.LcGenInfo y)
            {
                return (x.actcode + x.repeatsl.Trim()).CompareTo(y.actcode + y.repeatsl.Trim());
            });
            index1 = 1;
            foreach (var item in this.LCGeneralInfoList)
            {
                item.slnum = index1;
                ++index1;
            }
            this.btnRefresh_Click(null, null);
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {

            if (this.autoRSirDescSearch.SelectedValue == null)
                return;
            //this.dgLCItemInfo.ItemsSource = null;
            string LcCod1 = this.AtxtPlcId.Value.ToString();
            string sircode1 = this.autoRSirDescSearch.SelectedValue.ToString();
            string sirdesc1 = this.autoRSirDescSearch.SelectedText.Trim();
            string sirunit1 = ((ComboBoxItem)this.cmbQtyUnit.SelectedItem).Content.ToString();
            decimal LCQty1 = decimal.Parse("0" + this.txtLCQty.Text.Trim());
            decimal FCRate1 = decimal.Parse("0" + this.txtrqRate.Text.Trim());

            this.autoRSirDescSearch.SelectedValue = null;
            this.txtLCQty.Text = "";
            this.txtrqRate.Text = "";
            int sl1 = this.LCCostInfoList.Count + 1;
            this.LCCostInfoList.Add(new vmEntryPur01.LcCostInfo()
            {
                slnum = sl1,
                lsircode = LcCod1,
                sircode = sircode1,
                sirdesc = sirdesc1,
                sirrmrk = sirdesc1.Substring(15).Trim(),
                sirunit = sirunit1,
                lcqty = LCQty1,
                fcrat1 = FCRate1,
                fcval1 = LCQty1 * FCRate1,
                dcval1 = 0.00m,
                dcrat1 = 0.00m,
                dcover = 0.00m,
                overper = 0.00m,
                dcval2 = 0.00m,
                dcrat2 = 0.00m,
                fcrat2 = FCRate1,
                dcval3act = 0.00m,
                dcrat3act = 0.00m
            });
            this.btnRefresh_Click(null, null);
            //this.dgLCItemInfo.ItemsSource = this.LCCostInfoList;
            /*
              public class LcCostInfo
        {
            public int slnum { get; set; }          // Serial No.
            public string lsircode { get; set; }    // LC Code
            public string sircode { get; set; }     // Item Code
            public string sirdesc { get; set; }     // Item Description
            public string sirrmrk { get; set; }     // Additional Information
            public string sirunit { get; set; }     // Item Unit
            public decimal lcqty { get; set; }      // LC Item Quantity
            public decimal fcrat1 { get; set; }     // Item Rate in Foreign Currency (FC)
            public decimal fcval1 { get; set; }     // Item Value in Foreign Currency (FC)
            public decimal dcval1 { get; set; }     // Item Value in Domestic Currency (DC)
            public decimal dcrat1 { get; set; }     // Item Rate in Domestic Currency (DC)
            public decimal dcover { get; set; }     // Other Overhead Cost
            public decimal dcval2 { get; set; }     // Total Cost Value
            public decimal dcrat2 { get; set; }     // Costing Rate

        }
             
             */
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.dgLCGenInfo.ItemsSource = null;
            this.dgLcPayInfo.ItemsSource = null;
            this.dgLCItemInfo.ItemsSource = null;


            int sl1 = 1;
            decimal tpay1act = this.LCPaymentInfoList.Sum(x => x.payamt);

            foreach (var item in this.LCPaymentInfoList)
            {
                item.slnum = sl1;
                item.payper = (tpay1act == 0.00M ? 0.00M : Math.Round(item.payamt / tpay1act * 100.00m, 2));
                sl1++;
            }

            string FC1 = this.LCGeneralInfoList.Find(x => x.actcode == "SILC00101009").lcgendata.Trim();
            string LCurr1 = this.LCGeneralInfoList.Find(x => x.actcode == "SILC00101010").actdesc.Trim();
            LCurr1 = LCurr1.Replace("CONVERSION RATE", "").Replace("(", "").Replace(")", "").Trim();
            LCurr1 = (LCurr1.Length == 0 ? "TAKA" : LCurr1);

            decimal FCRate1 = decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00101010").lcgendata.Trim());
            FCRate1 = (FCRate1 <= 0 ? 1 : FCRate1);
            decimal lcFcValue1 = (decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00101003").lcgendata.Trim()));
            decimal lcLocalValue1 = (lcFcValue1 * FCRate1);

            decimal Margin1 = decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00105001").lcgendata.Trim().Replace("%", ""));
            decimal MarkUP1 = decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00105003").lcgendata.Trim().Replace("%", ""));
            decimal CRFValFc1 = decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00105007").lcgendata.Trim());
            decimal CRFValLocal1 = CRFValFc1 * FCRate1;
            decimal OverP1 = decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00105009").lcgendata.Trim().Replace("%", ""));

            decimal BankLoanP1 = decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00105011").lcgendata.Trim().Replace("%", ""));

            decimal BankLoanFc1 = (BankLoanP1 * lcFcValue1 / 100.00m);
            decimal BankLoanLocal1 = (BankLoanP1 * lcLocalValue1 / 100.00m);

            decimal BankLoanIntP1 = decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00105013").lcgendata.Trim().Replace("%", ""));
            decimal BankLoanPeriod1 = decimal.Parse("0" + this.LCGeneralInfoList.Find(x => x.actcode == "SILC00105014").lcgendata.Trim());

            decimal BankLoanIntFc1 = BankLoanFc1 * (BankLoanPeriod1 / 12.00m) * (BankLoanIntP1 / 100.00m);
            decimal BankLoanIntLocal1 = BankLoanLocal1 * (BankLoanPeriod1 / 12.00m) * (BankLoanIntP1 / 100.00m);

            decimal EstTotalOverhead1 = (MarkUP1 * lcLocalValue1 / 100.00m) + CRFValLocal1 + (OverP1 * lcLocalValue1 / 100.00m) + BankLoanIntLocal1;


            this.dgLCItemInfo.Columns[4].Header = "Rate-1 (" + FC1 + ")";
            this.dgLCItemInfo.Columns[5].Header = "Amount (" + FC1 + ")";

            this.dgLCItemInfo.Columns[6].Header = "Amount-1 (" + LCurr1 + ")";
            this.dgLCItemInfo.Columns[7].Header = "Rate-1 (" + LCurr1 + ")";
            this.dgLCItemInfo.Columns[9].Header = "Overhead (" + LCurr1 + ")";
            this.dgLCItemInfo.Columns[10].Header = "Amount-2 (" + LCurr1 + ")";
            this.dgLCItemInfo.Columns[11].Header = "Rate-2 (" + LCurr1 + ")";

            this.dgLCItemInfo.Columns[12].Header = "Rate-2 (" + FC1 + ")";
            decimal tmargin1act = this.LCPaymentInfoList.FindAll(y => y.actcode == "140200010001").Sum(x => x.payamt);
            decimal tlcval1act = this.LCPaymentInfoList.FindAll(y => y.actcode.Substring(0, 8) == "14020001").Sum(x => x.payamt);

            decimal tover1act = this.LCPaymentInfoList.FindAll(y => y.actcode.Substring(0, 8) != "14020001").Sum(x => x.payamt);


            foreach (var item in this.LCCostInfoList)
            {
                item.fcval1 = item.lcqty * item.fcrat1;
                item.dcval1 = item.fcval1 * FCRate1;
                item.dcrat1 = (item.lcqty != 0.00m ? item.dcval1 / item.lcqty : 0.00m);
                //item.dcover = (tover1act != 0 ? item.dcval1 / tover1act : 0.00m);
                item.dcover = (EstTotalOverhead1 != 0 ? item.dcval1 / EstTotalOverhead1 : 0.00m);
                item.overper = 0.00m;
            }

            decimal tfcval1 = this.LCCostInfoList.Sum(x => x.fcval1);
            decimal tdcval1 = this.LCCostInfoList.Sum(x => x.dcval1);
            //decimal tmuVal1 = tdcval1 * (MarkUP1 / 100.00m);

            foreach (var item in this.LCCostInfoList)
            {
                //item.dcover = (tover1act != 0.00m ? (item.dcval1 / tdcval1) * (tover1act + tmuVal1) : 0.00m);
                //item.overper = (tover1act != 0.00m ? (item.dcval1 / tdcval1) * 100.00m : 0.00m);

                item.dcover = (EstTotalOverhead1 != 0.00m ? (item.dcval1 / tdcval1) * EstTotalOverhead1 : 0.00m);
                item.overper = (EstTotalOverhead1 != 0.00m ? (item.dcval1 / tdcval1) * 100.00m : 0.00m);
                item.dcval2 = item.dcval1 + item.dcover;
                item.dcrat2 = (item.lcqty != 0.00m ? item.dcval2 / item.lcqty : 0.00m);
                item.fcrat2 = (FCRate1 != 0 ? item.dcrat2 / FCRate1 : 0.00m);
            }

            decimal tdcval2 = this.LCCostInfoList.Sum(x => x.dcval2);
            foreach (var item in this.LCCostInfoList)
            {
                item.dcval3act = (item.dcval2 / tdcval2) * tpay1act;
                item.dcrat3act = (item.lcqty != 0.00m ? item.dcval3act / item.lcqty : 0.00m);
            }

            this.txtTotalPayment.Text = tpay1act.ToString("#,##0.00;(#,##0.00); ");
            this.txtTotalLCMargin.Text = tmargin1act.ToString("#,##0.00;(#,##0.00);0.00");
            this.txtTotalLCMarginPer.Text = (tpay1act==0 ? 0 : Math.Round(tmargin1act / tpay1act * 100, 2)).ToString("#,##0.00;(#,##0.00);0.00") + " %";
            this.txtTotalLCInvValuPaid.Text = (tlcval1act - tmargin1act).ToString("#,##0.00;(#,##0.00);0.00");
            this.txtTotalLCInvValuPaidPer.Text = (tpay1act==0 ? 0 : Math.Round((tlcval1act - tmargin1act) / tpay1act * 100, 2)).ToString("#,##0.00;(#,##0.00);0.00") + " %";

            this.txtLCValuePaid.Text = tlcval1act.ToString("#,##0.00;(#,##0.00);0.00");
            this.txtLCValuePaidPer.Text = (tpay1act==0 ? 0 : Math.Round(tlcval1act / tpay1act * 100, 2)).ToString("#,##0.00;(#,##0.00);0.00") + " %";
            this.txtEstTotalLCOverheadPaid.Text = tover1act.ToString("#,##0.00;(#,##0.00);0.00");
            this.txtEstTotalLCOverheadPaidPer.Text = (tpay1act==0 ? 0 : Math.Round(tover1act / tpay1act * 100, 2)).ToString("#,##0.00;(#,##0.00);0.00") + " %";
            this.lblTotalFcAmt1.Content = tfcval1.ToString("#,##0.00;(#,##0.00);0.00");
            this.lblTotalDcAmt1.Content = tdcval1.ToString("#,##0.00;(#,##0.00);0.00");
            this.lblTotalDcOverAmt1.Content = EstTotalOverhead1.ToString("#,##0.00;(#,##0.00);0.00");
            this.lblTotalDcAmt2.Content = tdcval2.ToString("#,##0.00;(#,##0.00);0.00");


            var LCGeneralInfoList2 = this.LCGeneralInfoList.FindAll(x => (x.actcode == "SILC00101003" || x.actcode == "SILC00105001" || x.actcode == "SILC00105003"
              || x.actcode == "SILC00105007" || x.actcode == "SILC00105009" || x.actcode == "SILC00105011" || x.actcode == "SILC00105013")).ToList();
            foreach (var item in LCGeneralInfoList2)
            {
                item.gendataw1 = 150;
                item.gendataw2 = 270;
                item.lcgendata2 = "";
                switch (item.actcode)
                {
                    case "SILC00101003":
                        item.lcgendata2 = lcFcValue1.ToString("#,##0.00") + " " + FC1 + " = " + lcLocalValue1.ToString("#,##0.00") + " " + LCurr1;
                        break;
                    case "SILC00105001":    // L/C MARGIN %
                        item.lcgendata2 = (Margin1 * lcFcValue1 / 100.00m).ToString("#,##0.00") + " " + FC1 + " = " + (Margin1 * lcLocalValue1 / 100.00m).ToString("#,##0.00") + " " + LCurr1;
                        break;
                    case "SILC00105003":    // COST MARK-UP %
                        item.lcgendata2 = (MarkUP1 * lcFcValue1 / 100.00m).ToString("#,##0.00") + " " + FC1 + " = " + (MarkUP1 * lcLocalValue1 / 100.00m).ToString("#,##0.00") + " " + LCurr1;
                        break;
                    case "SILC00105007":    // CRF PORT CHARGES
                        item.lcgendata2 = CRFValFc1.ToString("#,##0.00") + " " + FC1 + " = " + CRFValLocal1.ToString("#,##0.00") + " " + LCurr1;
                        break;
                    case "SILC00105009":    // OTHER OVERHEAD %
                        item.lcgendata2 = (OverP1 * lcFcValue1 / 100.00m).ToString("#,##0.00") + " " + FC1 + " = " + (OverP1 * lcLocalValue1 / 100.00m).ToString("#,##0.00") + " " + LCurr1;
                        break;
                    case "SILC00105011":    // BANK LOAN %
                        item.lcgendata2 = BankLoanFc1.ToString("#,##0.00") + " " + FC1 + " = " + BankLoanLocal1.ToString("#,##0.00") + " " + LCurr1;
                        break;
                    case "SILC00105013":    // INTETEST % ON BANK LOAN
                        item.lcgendata2 = BankLoanIntFc1.ToString("#,##0.00") + " " + FC1 + " = " + BankLoanIntLocal1.ToString("#,##0.00") + " " + LCurr1;
                        break;
                }
            }
            this.dgLCGenInfo.ItemsSource = this.LCGeneralInfoList;
            this.dgLcPayInfo.ItemsSource = this.LCPaymentInfoList;//.FindAll(x=>x.payamt!=0.00m);
            this.dgLCItemInfo.ItemsSource = this.LCCostInfoList;

        }





        private void autoRSirDescSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetInvItemSirdesc(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetInvItemSirdesc(string Pattern)
        {
            // match on contain (could do starts with)
            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
               WpfProcessAccess.InvItemList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoRSirDescSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.autoRSirDescSearch.SelectedValue == null)
                return;

            string sircod1 = this.autoRSirDescSearch.SelectedValue.ToString();
            this.ResetUnitRateInfo(sircod1);
        }
        private void ResetUnitRateInfo(string ItemId1)
        {
            try
            {
                var item1 = WpfProcessAccess.InvItemList.Find(x => x.sircode == ItemId1);
                this.cmbQtyUnit.Items.Clear();
                this.cmbRateUnit.Items.Clear();
                this.cmbRateUnit.IsEnabled = true;
                this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1.sirunit.Trim(), Tag = "1.00" });
                this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1.sirunit.Trim(), Tag = item1.sirunit.Trim() });

                if (item1.sirunit.Trim() != item1.sirunit2.Trim() && item1.sirunit2.Trim().Length > 0 && item1.siruconf > 0)
                {
                    this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1.sirunit2.Trim(), Tag = item1.siruconf.ToString() });
                    this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1.sirunit2.Trim(), Tag = item1.sirunit2.Trim() });
                }

                if (item1.sirunit.Trim() != item1.sirunit3.Trim() && item1.sirunit3.Trim().Length > 0 && item1.siruconf3 > 0)
                {
                    this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1.sirunit3.Trim(), Tag = item1.siruconf3.ToString() });
                    this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1.sirunit3.Trim(), Tag = item1.sirunit3.Trim() });
                }

                this.cmbQtyUnit.SelectedIndex = 0;
                this.lblUnit1.Content = item1.sirunit.Trim();
                this.cmbRateUnit.SelectedIndex = 0;
                this.cmbRateUnit.IsEnabled = (this.cmbRateUnit.Items.Count > 1 ? true : false);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("L/C-31: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string LcCod1 = this.AtxtPlcId.Value.ToString();
            var pap1 = vmp1.SetParamForLcCostSheetUpdate(WpfProcessAccess.CompInfList[0].comcpcod, LcCod1, this.LCGeneralInfoList, this.LCCostInfoList);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            if (ds1 == null)
            {
                System.Windows.MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            System.Windows.MessageBox.Show("Update SuccessFull!!", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information,
                          MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            this.btnRefresh_Click(null, null);
            LocalReport rpt1 = null;
            var list3 = WpfProcessAccess.GetRptGenInfo();
            list3[0].RptHeader1 = "L/C Information Sheet - " + this.lblLCDesc1.Content.ToString().Trim() + " [As of " + DateTime.Today.ToString("dd-MMM-yyyy, dddd") + " ]";

            string FC1 = this.LCGeneralInfoList.Find(x => x.actcode == "SILC00101009").lcgendata.Trim();
            list3[0].RptHeader2 = FC1;

            string LCurr1 = this.LCGeneralInfoList.Find(x => x.actcode == "SILC00101010").actdesc.Trim();
            LCurr1 = LCurr1.Replace("CONVERSION RATE", "").Replace("(", "").Replace(")", "").Trim();
            LCurr1 = (LCurr1.Length == 0 ? "TAKA" : LCurr1);
            list3[0].RptFooter2 = LCurr1;

            list3[0].RptParVal1 = "N.B.: L/C Overhead = Cost Mark-up + CRF Port Charges + Other Overhead + Intetest on Bank Loan";

            var rptLCGeneralInfoList = new List<HmsEntityInventory.InvLcCostInfo01>();
            var rptLCPaymentInfoList = new List<HmsEntityInventory.InvLcCostInfo01>();
            var rptLCPaymentInfoList2 = new List<HmsEntityInventory.InvLcCostInfo01>();
            var rptLCCostInfoList = new List<HmsEntityInventory.InvLcCostInfo01>();       

            foreach (var item in this.LCGeneralInfoList)
            {

                //if (int.Parse(item.actcode.Substring(8, 4)) <= 1028 || item.lcgendata.Trim().Length > 0)
                if (item.lcgendata.Trim().Length > 0)
                    rptLCGeneralInfoList.Add(new HmsEntityInventory.InvLcCostInfo01()
                    {
                        grp1 = "A001",
                        lsircode = "",
                        slnum = item.slnum,
                        sircode = item.actcode,
                        sirdesc = item.actdesc,
                        repeatsl = item.repeatsl,
                        sirrmrk = item.lcgendata.Trim() + (item.lcgendata2.Trim().Length > 0 ? "          [ " + item.lcgendata2.Trim() + " ]"  +
                                  (item.actcode == "SILC00105003" || item.actcode == "SILC00105007" || item.actcode == "SILC00105009" || item.actcode == "SILC00105013" ? "  ---- Overhead" : "") : ""),
                        sirunit = "",
                        lcqty = 0.00m,
                        fcrat1 = 0.00m,
                        fcval1 = 0.00m,
                        dcval1 = 0.00m,
                        dcrat1 = 0.00m,
                        dcover = 0.00m,
                        overper = 0.00m,
                        dcval2 = 0.00m,
                        dcrat2 = 0.00m,
                        fcrat2 = 0.00m,
                        dcrat3act = 0.00m,
                        dcval3act = 0.00m 
                    });
            }

            foreach (var item in this.LCPaymentInfoList)
            {
                rptLCPaymentInfoList.Add(new HmsEntityInventory.InvLcCostInfo01()
                {
                    grp1 = "B001",
                    lsircode = "",
                    slnum = item.slnum,
                    sircode = item.actcode,
                    sirdesc = item.actdesc,
                    repeatsl = "",
                    sirrmrk = "",
                    sirunit = "",
                    lcqty = 0.00m,
                    fcrat1 = 0.00m,
                    fcval1 = 0.00m,
                    dcval1 = item.payamt,
                    dcrat1 = item.payper,
                    dcover = 0.00m,
                    overper = 0.00m,
                    dcval2 = 0.00m,
                    dcrat2 = 0.00m,
                    fcrat2 = 0.00m,
                    dcrat3act = 0.00m,
                    dcval3act = 0.00m 
                });
            }

            rptLCPaymentInfoList2.Add(new HmsEntityInventory.InvLcCostInfo01()
            {
                grp1 = "B002",
                lsircode = "",
                slnum = 0,
                sircode = "",
                sirdesc = "TOTAL PAYMENT [ A + B ]",
                repeatsl = "",
                sirrmrk = "",
                sirunit = "",
                lcqty = 0.00m,
                fcrat1 = 0.00m,
                fcval1 = 0.00m,
                dcval1 = decimal.Parse("0" + this.txtTotalPayment.Text.Replace(",", "")),
                dcrat1 = 100.00m,
                dcover = 0.00m,
                overper = 0.00m,
                dcval2 = 0.00m,
                dcrat2 = 0.00m,
                fcrat2 = 0.00m,
                dcrat3act = 0.00m,
                dcval3act = 0.00m 
            });

            rptLCPaymentInfoList2.Add(new HmsEntityInventory.InvLcCostInfo01()
            {
                grp1 = "B002",
                lsircode = "",
                slnum = 1,
                sircode = "",
                sirdesc = "TOTAL L/C VALUE PAID [ A = a1 + a2 ]",
                repeatsl = "",
                sirrmrk = "",
                sirunit = "",
                lcqty = 0.00m,
                fcrat1 = 0.00m,
                fcval1 = 0.00m,
                dcval1 = decimal.Parse("0" + this.txtLCValuePaid.Text.Replace(",", "")),
                dcrat1 = decimal.Parse("0" + this.txtLCValuePaidPer.Text.Replace("%", "")),
                dcover = 0.00m,
                overper = 0.00m,
                dcval2 = 0.00m,
                dcrat2 = 0.00m,
                fcrat2 = 0.00m,
                dcrat3act = 0.00m,
                dcval3act = 0.00m 
            });

            rptLCPaymentInfoList2.Add(new HmsEntityInventory.InvLcCostInfo01()
            {
                grp1 = "B002",
                lsircode = "",
                slnum = 2,
                sircode = "",
                sirdesc = "L/C MARGIN - a1 ",
                repeatsl = "",
                sirrmrk = "",
                sirunit = "",
                lcqty = 0.00m,
                fcrat1 = 0.00m,
                fcval1 = 0.00m,
                dcval1 = decimal.Parse(this.txtTotalLCMargin.Text.Replace(",", "")),
                dcrat1 = decimal.Parse(this.txtTotalLCMarginPer.Text.Replace("%", "")),
                dcover = 0.00m,
                overper = 0.00m,
                dcval2 = 0.00m,
                dcrat2 = 0.00m,
                fcrat2 = 0.00m,
                dcrat3act = 0.00m,
                dcval3act = 0.00m 
            });
            rptLCPaymentInfoList2.Add(new HmsEntityInventory.InvLcCostInfo01()
            {
                grp1 = "B002",
                lsircode = "",
                slnum = 3,
                sircode = "",
                sirdesc = "INVOICE VALUE - a2 ",
                repeatsl = "",
                sirrmrk = "",
                sirunit = "",
                lcqty = 0.00m,
                fcrat1 = 0.00m,
                fcval1 = 0.00m,
                dcval1 = decimal.Parse(this.txtTotalLCInvValuPaid.Text.Replace(",", "")),
                dcrat1 = decimal.Parse(this.txtTotalLCInvValuPaidPer.Text.Replace("%", "")),
                dcover = 0.00m,
                overper = 0.00m,
                dcval2 = 0.00m,
                dcrat2 = 0.00m,
                fcrat2 = 0.00m,
                dcrat3act = 0.00m,
                dcval3act = 0.00m 
            });
            rptLCPaymentInfoList2.Add(new HmsEntityInventory.InvLcCostInfo01()
            {
                grp1 = "B002",
                lsircode = "",
                slnum = 4,
                sircode = "",
                sirdesc = "TOTAL OVERHEAD PAID [ B ]",
                repeatsl = "",
                sirrmrk = "",
                sirunit = "",
                lcqty = 0.00m,
                fcrat1 = 0.00m,
                fcval1 = 0.00m,
                dcval1 = decimal.Parse(this.txtEstTotalLCOverheadPaid.Text.Replace(",", "")),
                dcrat1 = decimal.Parse(this.txtEstTotalLCOverheadPaidPer.Text.Replace("%", "")),
                dcover = 0.00m,
                overper = 0.00m,
                dcval2 = 0.00m,
                dcrat2 = 0.00m,
                fcrat2 = 0.00m,
                dcrat3act = 0.00m,
                dcval3act = 0.00m 
            });

            foreach (var item in this.LCCostInfoList)
            {
                rptLCCostInfoList.Add(new HmsEntityInventory.InvLcCostInfo01()
                {
                    grp1 = "C001",
                    lsircode = "",
                    slnum = item.slnum,
                    sircode = item.sircode,
                    sirdesc = item.sirrmrk,
                    repeatsl = "",
                    sirrmrk = item.sirdesc,
                    sirunit = item.sirunit,
                    lcqty = item.lcqty,
                    fcrat1 = item.fcrat1,
                    fcval1 = item.fcval1,
                    dcval1 = item.dcval1,
                    dcrat1 = item.dcrat1,
                    dcover = item.dcover,
                    overper = item.overper,
                    dcval2 = item.dcval2,
                    dcrat2 = item.dcrat2,
                    fcrat2 = item.fcrat2,
                    dcrat3act = item.dcrat3act,
                    dcval3act = item.dcval3act 
                });
            }
            /*
               public class InvLcCostInfo01
        {
             public string grp1 { get; set; }          // Report Group 
            public string lsircode { get; set; }    // LC Code
            public int slnum { get; set; }          // Serial No.-1
            public string sircode { get; set; }     // Item Code
            public string sirdesc { get; set; }     // Item Description
            public string repeatsl { get; set; }
            public string sirrmrk { get; set; }     // Additional Information
            public string sirunit { get; set; }     // Item Unit
            public decimal lcqty { get; set; }      // LC Item Quantity
            public decimal fcrat1 { get; set; }     // Item Rate-1 in Foreign Currency (FC)
            public decimal fcval1 { get; set; }     // Item Value in Foreign Currency (FC)
            public decimal dcval1 { get; set; }     // Item Value in Domestic Currency (DC)
            public decimal dcrat1 { get; set; }     // Item Rate-1 in Domestic Currency (DC)
            public decimal dcover { get; set; }     // Other Overhead Cost
            public decimal overper { get; set; }     // Other Overhead %
            public decimal dcval2 { get; set; }     // Total Cost Value in Domestic Currency (DC)
            public decimal dcrat2 { get; set; }     // Costing Rate-2 in Domestic Currency (DC)
            public decimal fcrat2 { get; set; }     // Item Rate-2 in Foreign Currency (FC)

        }
             
             */

            if (rptLCGeneralInfoList.Count == 0 && rptLCPaymentInfoList.Count == 0)
            {
                System.Windows.MessageBox.Show("Nothing to print.\nPlease try again after updating some information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information,
                    MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }


            Hashtable list1 = new Hashtable();
            list1["GenInfo"] = rptLCGeneralInfoList;
            list1["PayInfo"] = rptLCPaymentInfoList;
            list1["PayInfo2"] = rptLCPaymentInfoList2;
            list1["CostInfo"] = rptLCCostInfoList;
            rpt1 = StoreReportSetup.GetLocalReport("Procurement.RptLcCostSheet01", list1, null, list3);
            string WindowTitle1 = "L/C Cost Analysis Sheet";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }
    }
}
