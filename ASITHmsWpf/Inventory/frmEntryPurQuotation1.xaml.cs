using ASITFunLib;
using ASITHmsEntity;
using ASITHmsRpt2Inventory;
using ASITHmsViewMan.Inventory;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
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
using Xceed.Wpf.Toolkit;

namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmEntryPurQuotation1.xaml
    /// </summary>
    public partial class frmEntryPurQuotation1 : UserControl
    {
        private bool FrmInitialized = false;
        private List<vmEntryPurQuotation1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPurQuotation1.ListViewItemTable>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private vmEntryPurQuotation1 vm1 = new vmEntryPurQuotation1();
        private vmReportStore1 vm1r = new vmReportStore1();

        public bool IsActiveTransListWindow { get; set; }


        public frmEntryPurQuotation1()
        {
            InitializeComponent();
        }

        private void ConstructAutoCompletionSource()
        {
            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
            foreach (var itemd1 in deptList1)
            {
                if (itemd1.sectname.ToUpper().Contains("STORE"))
                    this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
            }


            if (WpfProcessAccess.SupplierContractorList == null)
                WpfProcessAccess.GetSupplierContractorList();
            this.AtxtSsircod.AutoSuggestionList.Clear();
            foreach (var item1s in WpfProcessAccess.SupplierContractorList)
            {
                //this.cmbSsirCod.Items.Add(new ComboBoxItem() { Content = item1s.sirdesc, Tag = item1s.sircode });
                //this.AtxtSsircod.AutoSuggestionList.Add(item1s.sirdesc.Trim() + " : [" + item1s.sircode + "]");
                this.AtxtSsircod.AddSuggstionItem(item1s.sirdesc.Trim(), item1s.sircode);
            }

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();
            this.AtxtRecById.AutoSuggestionList.Clear();

            foreach (var item1 in WpfProcessAccess.StaffList)
            {
                //this.AtxtRecById.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
                this.AtxtRecById.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode);
            }

            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;
                this.IsActiveTransListWindow = false;
                this.ConstructAutoCompletionSource();
                this.ListView1.Tag = "RSIRCOD";

                this.chkAutoTransList.IsChecked = this.IsActiveTransListWindow;
                this.btnPrint2.Visibility = Visibility.Hidden;
                this.btnUpdate.Visibility = Visibility.Hidden;
                this.gridDetails.Visibility = Visibility.Hidden;
                this.xctk_dtpQutDat.Value = DateTime.Today;
                this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-33);
                this.xctk_dtpToDate.Value = DateTime.Today;
                this.xctk_dtpQutDat.IsEnabled = false;

                if (this.IsActiveTransListWindow)
                    this.gridTransList.Visibility = Visibility.Visible;
                else
                    this.gridTransList.Visibility = Visibility.Hidden;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            this.btnPrint2.Visibility = Visibility.Hidden;
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Hidden;

            this.ListViewItemTable1.Clear();
            this.ListView1.Items.Clear();
            this.ListView2qt.Items.Clear();
            this.xctk_dtpQutDat.IsEnabled = false;
            if (this.btnOk.Content.ToString() == "_New")
            {
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.stkIntro.IsEnabled = true;
                this.AtxtSsircod.Text = "";
                this.AtxtRecById.Text = "";
                this.txtSrfRef.Text = "";
                this.txtSrfNar.Text = "";
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtQuotQty.Text = "";
                this.lblUnit1.Content = "";
                this.lblQutNo.Content = "PQTMM-CCCC-XXXXX";
                this.lblQutNo.Tag = "PQTYYYYMMCCCCXXXXX";
                if (IsActiveTransListWindow)
                {
                    this.BuildTransactionList();
                    this.gridTransList.Visibility = Visibility.Visible;
                    this.lvTransList.Focus();
                }
                this.btnOk.Content = "_Ok"; // new AccessText { Text = "_Ok" };//  Content = new AccessText { Text = "_Label" };
                return;
            }

            if (this.checkOkValidation() == false)
                return;

            if (this.checkSupOkValidation() == false)
                return;

            this.btnUpdate.Visibility = Visibility.Visible;
            this.gridTransList.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Visible;
            this.chkDateBlocked.IsChecked = false;
            this.chkDateBlocked.IsEnabled = false;
            this.btnUpdate.IsEnabled = true;
            this.stkItem.IsEnabled = true;
            this.stkIntro.IsEnabled = false;
            this.btnOk.Content = "_New"; //new AccessText { Text = "_New" };// "_New";
        }

        private bool checkSupOkValidation()
        {
            //string suplst = this.AtxtSsircod.Text.Trim();
            string suplst = this.AtxtSsircod.Value.Trim();

            if (suplst.Length < 0)
                return false;

            var listSup1 = WpfProcessAccess.SupplierContractorList.FindAll(x => x.sircode == suplst);
            return (listSup1.Count > 0);
        }

        private bool checkOkValidation()
        {
            string srfByID1 = this.AtxtRecById.Value.Trim();
            if (srfByID1.Length < 0)
                return false;

            var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == srfByID1);
            return (listStaff1.Count > 0);
        }

        private void BuildTransactionList()
        {

            string FromDate = this.xctk_dtpFromDate.Text;
            string ToDate = this.xctk_dtpToDate.Text;
            ASITFunParams.ProcessAccessParams pap1 = vm1r.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "PQT", FromDate, ToDate, "%", "%");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            this.lvTransList.Items.Clear();
            this.ListViewTransTable1.Clear();
            this.ListViewTransTable1 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();
            foreach (var itm1a in this.ListViewTransTable1)
            {
                this.lvTransList.Items.Add(itm1a);
            }
            this.lvTransList.SelectedIndex = 0;
            this.lvTransList.Focus();
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
              MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpQutDat.Text), cbSectCode: (ComboBoxItem)this.cmbSectCod.SelectedItem,
                    SsirCod: this.AtxtSsircod.Value.Trim(), reqByID1: this.AtxtRecById.Value.Trim(), MemoRef1: this.txtSrfRef.Text.Trim(), MemoNar1: this.txtSrfNar.Text.Trim(), ListViewItemTable1: this.ListViewItemTable1,
                    _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamUpdateQuot(WpfProcessAccess.CompInfList[0].comcod, ds1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblQutNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
            this.lblQutNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();


            this.btnUpdate.IsEnabled = false;
            this.stkItem.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;

        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            decimal reqqty1a = decimal.Parse("0" + this.txtQuotQty.Text.Trim());
            if (reqqty1a <= 0)
            {
                this.txtQuotQty.Focus();
                return;
            }
            decimal reqAmt1 = decimal.Parse("0" + this.lblAmount.Content.ToString().Trim());
            if (reqAmt1 <= 0)
            {
                this.txtQuotRat.Focus();
                return;
            }
            if (this.txtRSirDesc.Text.Trim().Length == 0)
            {
                this.txtRSirDesc.Text = "";
                this.txtQuotQty.Text = "";
                this.lblAmount.Content = "";
                this.txtQuotRat.Text = "";
            }
            vmEntryPurQuotation1.ListViewItemSelect lvi1 = (vmEntryPurQuotation1.ListViewItemSelect)this.ListView1.SelectedItem;
            int serialno1 = ListView2qt.Items.Count + 1;
            string rsircode1 = this.txtRSirCode.Text.Trim();
            string rsirdesc1 = this.txtRSirDesc.Text.Trim();

            string rsirunit = this.lblUnit1.Content.ToString();
            if (rsircode1.Length == 0)
                return;

            var list1a = ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
            if (list1a.Count > 0)
            {
                System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            var item1a = new vmEntryPurQuotation1.ListViewItemTable()
            {
                trsl = serialno1.ToString() + ".",
                rsircode = rsircode1,
                trdesc = rsircode1 + ": " + rsirdesc1,
                quotquantity = reqqty1a,
                quotrate = Math.Round(reqAmt1 / reqqty1a, 2),
                quotamount = reqAmt1,
                quotnote = txtQutNote.Text.ToString(),
                trunit = rsirunit
            };

            ListViewItemTable1.Add(item1a);
            ListViewItemTable1.Sort(delegate(vmEntryPurQuotation1.ListViewItemTable x, vmEntryPurQuotation1.ListViewItemTable y)
            {
                return x.rsircode.CompareTo(y.rsircode);
            });

            this.ListView2qt.Items.Clear();
            int i = 1;

            foreach (var item1a1 in ListViewItemTable1)
            {
                item1a1.trsl = i.ToString() + ".";
                ListView2qt.Items.Add(item1a1);
                i++;
            }
            this.txtRSirCode.Text = "";
            this.txtRSirDesc.Text = "";
            this.lblUnit1.Content = "";
            this.txtQuotQty.Text = "";
            this.lblAmount.Content = "";
            this.txtQuotRat.Text = "";
            this.txtQutNote.Text = "";
            this.ListView2qt.Focus();
        }

        private void AddMemo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.BuildTransactionList();
            this.chkFilter.IsChecked = false;
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            LocalReport rpt1 = null;
            string WindowTitle1 = "";
            if (this.rb3SelectedMemo.IsChecked == true)
            {
                var item1a = (HmsEntityInventory.InvTransectionList)this.lvTransList.SelectedItem;
                var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurQtnMemo>();
                var list2 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();
                var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();
                list3.Add(new HmsEntityGeneral.ReportGeneralInfo()
                {
                    RptCompName = WpfProcessAccess.CompInfList[0].comnam,
                    RptCompAdd1 = WpfProcessAccess.CompInfList[0].comadd1,
                    RptCompAdd2 = WpfProcessAccess.CompInfList[0].comadd2,
                    RptFooter1 = "Print Source: " + WpfProcessAccess.SignedInUserList[0].terminalID + ", " +
                                 WpfProcessAccess.SignedInUserList[0].signinnam + ", " +
                                 WpfProcessAccess.SignedInUserList[0].sessionID + ", " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")

                });
                rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurQuotMemo01", list1, list2, list3);
                //System.Windows.MessageBox.Show(item1a.quotref);
                WindowTitle1 = "Quotation Memo";
            }
            else if (this.rb3TableRecoreds.IsChecked == true)
            {
                var list1 = this.ListViewTransTable1;
                var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();
                list3.Add(new HmsEntityGeneral.ReportGeneralInfo()
                {
                    RptCompName = WpfProcessAccess.CompInfList[0].comnam,
                    RptCompAdd1 = WpfProcessAccess.CompInfList[0].comadd1,
                    RptCompAdd2 = WpfProcessAccess.CompInfList[0].comadd2,
                    RptFooter1 = "Print Source: " + WpfProcessAccess.SignedInUserList[0].terminalID + ", " +
                                 WpfProcessAccess.SignedInUserList[0].signinnam + ", " +
                                 WpfProcessAccess.SignedInUserList[0].sessionID + ", " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")

                });
                rpt1 = StoreReportSetup.GetLocalReport("Store.RptTransectionList", list1, null, list3); // ( R_01_RptSetup.RptSetupItemList1(ds1, ds2);          
                WindowTitle1 = "Quotation Transaction List";
            }
            if (rpt1 == null)
                return;

            if (this.rb3QuickPrint.IsChecked == true)
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

        private void UnCheckedAllPopups()
        {

            //this.chkPrint.IsChecked = false;
            this.chkFilter.IsChecked = false;
            this.chkPrint2.IsChecked = false;
        }

        private void chkAutoTransList_Click(object sender, RoutedEventArgs e)
        {
            this.IsActiveTransListWindow = (this.chkAutoTransList.IsChecked == true);
            if (this.IsActiveTransListWindow && this.gridDetails.Visibility == Visibility.Hidden)
            {
                this.BuildTransactionList();
                this.gridTransList.Visibility = Visibility.Visible;
                this.lvTransList.Focus();
            }
            else if (this.IsActiveTransListWindow == false && this.gridDetails.Visibility == Visibility.Hidden)
                this.gridTransList.Visibility = Visibility.Hidden;
            this.chkFilter.IsChecked = false;
            this.chkPrint2.IsChecked = false;

        }
        private void btntxtQuot_Click(object sender, RoutedEventArgs e)
        {
            this.txtRSirDesc.Clear();
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.ListView1_ShowData();
        }
        private void ListView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ListView1_ShowData();
        }

        private void ListView1_ShowData()
        {
            if (this.ListView1.SelectedItem == null)
                return;
            //vmEntryStoreReq1.ListViewItemSelect lvi1 = (vmEntryStoreReq1.ListViewItemSelect)this.ListView1.SelectedItem;
            var lvi1 = (vmEntryPurQuotation1.ListViewItemSelect)this.ListView1.SelectedItem;
            if (ListView1.Tag.ToString().Trim() == "RSIRCOD")
            {
                this.txtRSirDesc.Text = lvi1.trdesc;
                this.txtRSirCode.Text = lvi1.trcode;
                this.lblUnit1.Content = lvi1.trunit;
                this.txtRSirCode.Focus();

            }
        }

        private void ListView1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.ListView1.Items.Count == 0)
                return;

            this.ListView1.SelectedItem = this.ListView1.Items[0];
        }
        private void txtCodeDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            string wtxtName1 = ((WatermarkTextBox)sender).Name.ToString().Trim();
            string tag1 = (wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
            if (this.ListView1.Tag.ToString().Trim() != tag1)
                this.ListView1.Items.Clear();

            this.ListView1.Tag = (wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
        }

        private void txtCodeDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btntxtQuot.Visibility = (this.txtRSirDesc.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            this.btntxtNtCross.Visibility = (this.txtQutNote.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            this.btntxtSrfRef.Visibility = (this.txtSrfRef.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            this.ConstructAutoCompletionSource();
            this.PrepareListViewData();

        }
        private void PrepareListViewData()
        {
            ListView1.Items.Clear();
            switch (this.ListView1.Tag.ToString().Trim())
            {
                case "RSIRCOD":
                    string StrDesc1 = this.txtRSirDesc.Text.Trim().ToUpper();
                    if (StrDesc1.Length == 0)
                        return;

                    var List1a = (from lst in WpfProcessAccess.InvItemList
                                  where lst.sirdesc.ToUpper().Contains(StrDesc1)
                                  select new
                                  {
                                      trcode = lst.sircode,
                                      trdesc = lst.sirdesc,
                                      trunit = lst.sirunit
                                  });
                    foreach (var item1b in List1a)
                        ListView1.Items.Add(new vmEntryPurQuotation1.ListViewItemSelect { trcode = item1b.trcode, trdesc = item1b.trdesc, trunit = item1b.trunit });
                    break;
            }
        }

        private void txtSrfQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumeric(e.Text); //regex that allows numeric input only

        }

        private bool onlyNumeric(string text)
        {
            Regex regex = new Regex("^[0-9+-.]*$"); //regex that allows numeric input only
            return !regex.IsMatch(text); // 
        }

        private void txtSrfQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ClaculateAmt();
        }

        private void txtSrfRat_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ClaculateAmt();
        }

        private void ClaculateAmt()
        {
            Double quantity = Double.Parse("0" + this.txtQuotQty.Text.ToString());
            Double Rate = Double.Parse("0" + this.txtQuotRat.Text.ToString());
            Double Amount = quantity * Rate;
            this.lblAmount.Content = Amount.ToString("#,##0.00");
        }

        private void frmEntryStoreReq1_btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpQutDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpQutDat.IsEnabled)
                this.xctk_dtpQutDat.Focus();
        }

        private void lvTransList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnPrint3_Click(null, null);
        }

        private void lvTransList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.btnPrint3_Click(null, null);
        }

        private void hlDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (!this.btnUpdate.IsEnabled)  // After updating memo rows can't be deleted
                return;

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            if (System.Windows.MessageBox.Show("Are you sure to delete record " + RowIndex1.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.ListView2qt.Items.Clear();
            int i = 1;
            foreach (var item1a in ListViewItemTable1)
            {
                item1a.trsl = i.ToString() + ".";
                ListView2qt.Items.Add(item1a);
                i++;
            }
        }

        private void hlEditRow_Click(object sender, RoutedEventArgs e)
        {
            if (!this.btnUpdate.IsEnabled)  // After updating memo rows can't be edited
                return;

            this.txtRSirCode.Text = "";
            this.txtRSirDesc.Text = "";
            this.lblUnit1.Content = "";
            this.txtQuotQty.Text = "";

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            var tblItm1 = ListViewItemTable1[RowIndex1 - 1];
            var tblitm2 = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == tblItm1.rsircode);

            this.txtQuotQty.Text = tblItm1.quotquantity.ToString();
            this.txtRSirDesc.Text = tblitm2[0].sirdesc;
            this.lblUnit1.Content = tblitm2[0].sirunit;
            this.txtRSirCode.Text = tblItm1.rsircode;

            ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.ListView2qt.Items.Clear();
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            var item1a = this.lblQutNo.Tag.ToString();
            var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, item1a);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurQtnMemo>();
            var list2 = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();
            var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();
            list3.Add(new HmsEntityGeneral.ReportGeneralInfo()
            {
                RptCompName = WpfProcessAccess.CompInfList[0].comnam,
                RptCompAdd1 = WpfProcessAccess.CompInfList[0].comadd1,
                RptCompAdd2 = WpfProcessAccess.CompInfList[0].comadd2,
                RptFooter1 = "Print Source: " + WpfProcessAccess.SignedInUserList[0].terminalID + ", " +
                             WpfProcessAccess.SignedInUserList[0].signinnam + ", " +
                             WpfProcessAccess.SignedInUserList[0].sessionID + ", " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")

            });
            LocalReport Rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurQuotMemo01", list1, list2, list3);
            if (Rpt1 == null)
                return;

            RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
            DirectPrint1.PrintReport(Rpt1);
            DirectPrint1.Dispose();
        }

        private void btntxtNtCross_Click(object sender, RoutedEventArgs e)
        {
            this.txtQutNote.Clear();
        }

    }
}
