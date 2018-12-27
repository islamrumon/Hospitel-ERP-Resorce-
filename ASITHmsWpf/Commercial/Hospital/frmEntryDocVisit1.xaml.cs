using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Commercial;
using ASITHmsRpt4Commercial;
using Microsoft.Reporting.WinForms;
using System.Threading.Tasks;


namespace ASITHmsWpf.Commercial.Hospital
{
    /// <summary>
    /// Interaction logic for frmEntryDocVisit1.xaml
    /// </summary>
    public partial class frmEntryDocVisit1 : UserControl
    {
        private List<vmEntryReportDocVisit1.DocVisitNum> VisitList = new List<vmEntryReportDocVisit1.DocVisitNum>();
        private List<HmsEntityCommercial.DoctorToken01> TokenList = new List<HmsEntityCommercial.DoctorToken01>();
        private List<HmsEntityCommercial.DoctorToken01> gvTokenList = new List<HmsEntityCommercial.DoctorToken01>();

        vmEntryReportDocVisit1 vm1 = new vmEntryReportDocVisit1();
        public frmEntryDocVisit1()
        {
            InitializeComponent();
            this.ConstructAutoCompletionSource();
        }

        private void ConstructAutoCompletionSource()
        {

            // xxxx
            var brnList1 = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x=> x.brncod.Substring(2,2)!="00");

            foreach (var itemd1 in brnList1)
               this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.brnsnam, Tag = itemd1.brncod });

            this.cmbDoc.Items.Clear();
            this.cmbDoc.Items.Add(new ComboBoxItem() { Content = "PROF. DR. ABDUR RAHIM MIAH", Tag = "950600101014" });

            this.VisitList.Clear();
            this.VisitList.Add(new vmEntryReportDocVisit1.DocVisitNum() { visitnum = "01 1st Visit", visitdes = "A. 1st Visit", docfee = 800.00m });
            this.VisitList.Add(new vmEntryReportDocVisit1.DocVisitNum() { visitnum = "02 Report Show", visitdes = "B. Report Show", docfee = 300.00m });
            this.VisitList.Add(new vmEntryReportDocVisit1.DocVisitNum() { visitnum = "03 7 Days Followup", visitdes = "C. 7 Days Followup", docfee = 500.00m });
            this.VisitList.Add(new vmEntryReportDocVisit1.DocVisitNum() { visitnum = "04 15 Days Followup", visitdes = "D. 15 Days Followup", docfee = 500.00m });
            this.VisitList.Add(new vmEntryReportDocVisit1.DocVisitNum() { visitnum = "05 30 Days Followup", visitdes = "E. 30 Days Followup", docfee = 500.00m });
            this.VisitList.Add(new vmEntryReportDocVisit1.DocVisitNum() { visitnum = "06 3 Months Followup", visitdes = "F. 3 Months Followup", docfee = 600.00m });
            this.VisitList.Add(new vmEntryReportDocVisit1.DocVisitNum() { visitnum = "07 6 Months or More", visitdes = "G. 6 Months or More", docfee = 800.00m });

            this.cmbVisitNo.Items.Clear();
            foreach (var item in this.VisitList)
                this.cmbVisitNo.Items.Add(new ComboBoxItem() { Content = item.visitdes, Tag = item.visitnum });

            this.dgvDocFee.ItemsSource = this.VisitList;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.btnPrint2.Visibility = Visibility.Hidden;
            this.stkpDetails1.IsEnabled = false;
            //this.stkpDetails.Visibility = Visibility.Hidden;
            this.xctk_dtVisitDat.Value = DateTime.Today;

            string FrmDate1 = DateTime.Today.ToString("dd-MMM-yyyy"); //DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
            string ToDate1 = DateTime.Today.ToString("dd-MMM-yyyy");

            this.TokenList.Clear();
            this.TokenList = this.PreviousTokenList(FrmDate1, ToDate1);
            //this.TokenList = TokenList.FindAll(x => x.tokenid.Substring(0, 3) == "DTI");
            if (this.TokenList == null)
                return;
            if (this.TokenList.Count == 0)
                return;

            this.lblLastTokenNo.Content = int.Parse(this.TokenList[0].tokenid.Substring(15)).ToString("00");
            foreach (var item1 in this.TokenList)
            {
                this.cmbPrevTokenList.Items.Add(new ComboBoxItem()
                {
                      Content = item1.visitdat.ToString("dd-MMM-yyyy") + " [ " + item1.tokenid.Substring(15, 3) + ", " + item1.visitnum.Trim().Substring(3) + ", Tk. " + item1.totam.ToString("#,##0.00") + " ] Name: " + item1.ptname,
                        Tag = item1.dhccode + item1.tokenid
                }
                    );
            }

            this.gvTokenList.Clear();
            this.gvTokenList = this.TokenList;
            foreach (var item in this.gvTokenList)
            {
                item.slnum = int.Parse(item.tokenid.Substring(15));
                item.visitnum = item.visitnum.Substring(3);
            }
            this.dgvVisitList.ItemsSource = this.gvTokenList;
            this.CalVisitTotal();
        }
        private List<HmsEntityCommercial.DoctorToken01> PreviousTokenList(string Date1, string Date2)
        {
            string dhccode1 = ((ComboBoxItem)this.cmbDoc.SelectedItem).Tag.ToString();
            var pap1 = vm1.SetParamDocTokenList(WpfProcessAccess.CompInfList[0].comcpcod, Date1, Date2, dhccode1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return null;

            return ds1.Tables[0].DataTableToList<HmsEntityCommercial.DoctorToken01>();
        }
        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtVisitDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtVisitDat.IsEnabled)
                this.xctk_dtVisitDat.Focus();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.btnPrint2.Visibility = Visibility.Hidden;
            //this.btnUpdate.Visibility = Visibility.Hidden;
            this.stkpDetails1.IsEnabled = false;
           
            this.txtPatID.Text = "";
            this.txtPatName.Text = "";
            this.txtPatAge.Text = "";
            this.txtDocFee.Text = "0.00";
            this.txtOthrChrg.Text = "0.00";
            this.txtDiscAmt.Text = "0.00";
            this.txtNetTotal.Text = "0.00";
            this.txtOthrDesc.Text = "";
            //this.dgvDocFee.ItemsSource = null;

            this.lblTokenNo.Content = "DTIMM-CCCC-DDXXX";
            this.lblTokenNo.Tag = "DTIYYYYMMCCCCDDXXX";

            if (this.btnOk.Content.ToString() == "_New")
            {
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.stkIntro.IsEnabled = true;
                this.btnOk.Content = "_Ok";
                return;
            }

            foreach (var item in this.VisitList)
                item.docfee = decimal.Parse(item.docfee.ToString("#,##0.00"));

            this.cmbVisitNo.SelectedIndex = 0;
            this.txtDocFee.Text = this.VisitList[0].docfee.ToString("#,##0.00");
            this.dgvDocFee.Items.Refresh();
            //this.btnUpdate.Visibility = Visibility.Visible;
            this.stkpDetails1.IsEnabled = true;
            this.chkDateBlocked.IsChecked = false;
            this.chkDateBlocked.IsEnabled = false;
            this.btnUpdate.IsEnabled = true;
            this.stkIntro.IsEnabled = false;
            this.btnOk.Content = "_New";
            if (this.chkPatTrnID.IsChecked == true)
                this.txtPatID.Focus();
            else
                this.txtPatName.Focus();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.CalVisitTotal();

            if (this.ChkConfirmSave.IsChecked == true)
            {
                if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                {
                    return;
                }
            }

            string dhccode1 = ((ComboBoxItem)this.cmbDoc.SelectedItem).Tag.ToString().Trim();
            string docname1 = ((ComboBoxItem)this.cmbDoc.SelectedItem).Content.ToString().Trim();
            DateTime MemoDate1 = DateTime.Parse(this.xctk_dtVisitDat.Text);
            string CustID1 = this.txtPatID.Text.Trim();
            CustID1 = (CustID1.Length == 0 ? "000000000000" : CustID1);
            string SectCod1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            string ptname1 = this.txtPatName.Text.Trim().ToUpper();
            decimal ptage1 = decimal.Parse("0" + this.txtPatAge.Text.Trim());
            string othrdes1 = this.txtOthrDesc.Text.Trim();
            string visitnum1 = ((ComboBoxItem)this.cmbVisitNo.SelectedItem).Tag.ToString().Trim();
            decimal docfee1 = decimal.Parse("0" + this.txtDocFee.Text.Trim());
            decimal othrchrg1 = decimal.Parse("0" + this.txtOthrChrg.Text.Trim());
            decimal discam1 = decimal.Parse("0" + this.txtDiscAmt.Text.Trim());
            decimal NetAmt1 = docfee1 + othrchrg1 - discam1;

            if (ptname1.Length == 0 || ptage1 <= 0 || Math.Abs(docfee1) + Math.Abs(othrchrg1) + Math.Abs(discam1) == 0)
                return;

            
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, _dhccode: dhccode1, _visitdat: MemoDate1, _custid: CustID1, _cbSectCode: SectCod1,
                _ptname: ptname1, _ptage: ptage1, _visitnum: visitnum1, _othrdes: othrdes1, _docfee: docfee1, _othrchrg: othrchrg1, _discam: discam1,
                _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            //String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamUpdateDocToken(WpfProcessAccess.CompInfList[0].comcod, ds1);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            ////DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1); //Success
            if (ds2 == null)
                return;

            string memonum1 = ds2.Tables[0].Rows[0]["memonum1"].ToString();
            string memonum = ds2.Tables[0].Rows[0]["memonum"].ToString();
            this.lblTokenNo.Content = memonum1;
            this.lblTokenNo.Tag = memonum;
            this.lblLastTokenNo.Content = int.Parse(memonum.Substring(15)).ToString("00");
            
           
            this.cmbPrevTokenList.Items.Insert(0, new ComboBoxItem()
            {
                Content = MemoDate1.ToString("dd-MMM-yyyy") + " [ " + memonum.Substring(15, 3) + ", " + visitnum1.Substring(3) + ", Tk. " + NetAmt1.ToString("#,##0.00") + " ] Name: " + ptname1,
                Tag = dhccode1 + memonum
            });

            this.gvTokenList.Insert(0, new HmsEntityCommercial.DoctorToken01()
            {
                slnum = int.Parse(memonum.Substring(15, 3)),
                comcod = WpfProcessAccess.CompInfList[0].comcod,
                dhccode = dhccode1,
                docnam = docname1,
                visitdat = MemoDate1,
                tokenid = memonum,
                tokenid1 = memonum1,
                custid = CustID1,
                ptname = ptname1,
                ptage = ptage1,
                visitnum = visitnum1.Substring(3),
                othrdes = othrdes1,
                docfee = docfee1,
                othrchrg = othrchrg1,
                totam = docfee1 + othrchrg1,
                discam = discam1,
                Netam = NetAmt1,
                preparebyid = WpfProcessAccess.SignedInUserList[0].hccode,
                preparbyNam = "",
                prepareses = WpfProcessAccess.SignedInUserList[0].sessionID,
                preparetrm = WpfProcessAccess.SignedInUserList[0].terminalID,
                rowtime = MemoDate1,
                prndate = MemoDate1,
                copytype=""
            });
            if (this.gvTokenList.Count <= 1)
                this.dgvVisitList.ItemsSource = this.gvTokenList;

            this.dgvVisitList.Items.Refresh();

            this.btnUpdate.IsEnabled = false;
            if (this.ChkPrintDirect.IsChecked == true)
            {
                this.btnPrint2_Click(null, null);
            }
            else
            {
                this.btnPrint2.Visibility = Visibility.Visible;
            }
            if (this.ChkConfirmSave.IsChecked == false)
            {
                this.btnOk_Click(null, null);
                this.btnOk_Click(null, null);
            }
        }

        private void txtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.CalVisitTotal();
        }
        private void CalVisitTotal()
        {
            decimal DocFee = decimal.Parse("0" + this.txtDocFee.Text.Trim());
            decimal OtherFee = decimal.Parse("0" + this.txtOthrChrg.Text.Trim());
            decimal DiscAmt = decimal.Parse("0" + this.txtDiscAmt.Text.Trim());
            decimal TotalAmt = DocFee + OtherFee - DiscAmt;

            this.txtDocFee.Text = DocFee.ToString("#,##0.00;(#,##0.00);0.00");
            this.txtOthrChrg.Text = OtherFee.ToString("#,##0.00;(#,##0.00);0.00");
            this.txtDiscAmt.Text = DiscAmt.ToString("#,##0.00;(#,##0.00);0.00");
            this.txtNetTotal.Text = TotalAmt.ToString("#,##0.00;(#,##0.00);0.00");
            this.lblTota1.Content = this.gvTokenList.Sum(x => x.Netam).ToString("#,##0.00;(#,##0.00);0.00");
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {

            if (this.ChkPrintTokenList.IsChecked == true)
            {
                this.PrintTokenList();
                this.ChkPrintTokenList.IsChecked = true;
            }
            else
            {
                string MemoNum1 = ((ComboBoxItem)this.cmbPrevTokenList.SelectedItem).Tag.ToString();
                string dhccode1 = MemoNum1.Substring(0, 12);
                MemoNum1 = MemoNum1.Substring(12);
                string PrnOpt1 = (this.ChkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                this.ViewPrintMemo(MemoNum1, dhccode1, PrnOpt1);
            }
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            string MemoNum1 = this.lblTokenNo.Tag.ToString();
            string dhccode1 = ((ComboBoxItem)this.cmbDoc.SelectedItem).Tag.ToString();
            string PrnOpt1 = (this.ChkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
            this.ViewPrintMemo(MemoNum1, dhccode1, PrnOpt1, 2);
        }
        private void ViewPrintMemo(string memoNum = "XXXXXXXX", string DoctorID = "", string ViewPrint = "View", int PrnCopy=1)
        {
            LocalReport rpt1 = null;
            string WindowTitle1 = "";
            var pap1 = vm1.SetParamDocToken(WpfProcessAccess.CompInfList[0].comcod, DoctorID, memoNum);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var list3 = WpfProcessAccess.GetRptGenInfo();
            var list1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.DoctorToken01>();
            WindowTitle1 = "T O K E N";
            if (ViewPrint == "View")
            {
                rpt1 = CommReportSetup.GetLocalReport("Doctor.DocToken01", list1, null, list3);
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);


                //HmsReportViewer1 window1 = new HmsReportViewer1(rpt1);
                //window1.Title = windoeTitle1;
                ////window1.Owner = Application.Current.MainWindow;
                //window1.ShowDialog();
            }
            else if (ViewPrint == "DirectPrint")
            {
                rpt1 = CommReportSetup.GetLocalReport("Doctor.DocToken01", list1, null, list3);
                RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                DirectPrint1.PrintReport(rpt1);
                DirectPrint1.Dispose();
                if(PrnCopy==2)
                {
                    list1[0].copytype = "";
                    rpt1 = CommReportSetup.GetLocalReport("Doctor.DocToken01", list1, null, list3);
                    RdlcDirectPrint DirectPrint2 = new RdlcDirectPrint();
                    DirectPrint2.PrintReport(rpt1);
                    DirectPrint2.Dispose();
                }
            }
        }

        private void PrintTokenList()
        {
            LocalReport rpt1 = null;
            string WindowTitle1 = "";
            string DoctorID = ((ComboBoxItem)this.cmbDoc.SelectedItem).Tag.ToString();
            string TrnDate = this.xctk_dtVisitDat.Text.Trim();
            var pap1 = vm1.SetParamDocTokenList(WpfProcessAccess.CompInfList[0].comcod, _FromDate:TrnDate, _ToDate: TrnDate, _dhccode: DoctorID);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            string ViewPrint = (this.ChkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
            var list3 = WpfProcessAccess.GetRptGenInfo();
            var list1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.DoctorToken01>();

            list1.Sort(delegate(HmsEntityCommercial.DoctorToken01 x, HmsEntityCommercial.DoctorToken01 y)
            {
                return x.tokenid.CompareTo(y.tokenid);
            });


            WindowTitle1 = "T O K E N  L I S T";
            if (ViewPrint == "View")
            {
                rpt1 = CommReportSetup.GetLocalReport("Doctor.DocToken01List", list1, null, list3);
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

                //HmsReportViewer1 window1 = new HmsReportViewer1(rpt1);
                //window1.Title = windoeTitle1;
                ////window1.Owner = Application.Current.MainWindow;
                //window1.ShowDialog();
            }
            else if (ViewPrint == "DirectPrint")
            {
                rpt1 = CommReportSetup.GetLocalReport("Doctor.DocToken01List", list1, null, list3);
                RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                DirectPrint1.PrintReport(rpt1);
                DirectPrint1.Dispose();
            }
        }

        private void chkPatTrnID_Click(object sender, RoutedEventArgs e)
        {
            this.txtPatID.IsEnabled = (((CheckBox)sender).IsChecked == true);
            this.txtPatName.IsEnabled = (((CheckBox)sender).IsChecked == false);
            this.txtPatAge.IsEnabled = (((CheckBox)sender).IsChecked == false);
            if (this.txtPatID.IsEnabled)
                this.txtPatID.Focus();
            else
                this.txtPatName.Focus();

        }

        private void cmbVisitNo_LayoutUpdated(object sender, EventArgs e)
        {
            int i1 = this.cmbVisitNo.SelectedIndex;
            this.txtDocFee.Text = this.VisitList[i1].docfee.ToString("#,##0.00");
        }

        private void dgvVisitList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var item1a = (HmsEntityCommercial.DoctorToken01)this.dgvVisitList.SelectedItem;
                string MemoNum1 = item1a.tokenid;
                string dhccode1 = item1a.dhccode;
                string PrnOpt1 = (this.ChkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                this.ViewPrintMemo(MemoNum1, dhccode1, PrnOpt1, 1);
            }
            catch (Exception)
            {
                return;
            }
        }

        private void cmbVisitNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i1 = this.cmbVisitNo.SelectedIndex;
            this.txtDocFee.Text = this.VisitList[i1].docfee.ToString("#,##0.00");
        }           
    }
}
