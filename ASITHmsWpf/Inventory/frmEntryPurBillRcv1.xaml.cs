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
    /// Interaction logic for frmEntryPurBillRcv1.xaml
    /// </summary>
    public partial class frmEntryPurBillRcv1 : UserControl
    {

        private bool FrmInitialized = false;
        private bool IsActiveTransListWindow { get; set; }
        private List<vmEntryPurBillRcv1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPurBillRcv1.ListViewItemTable>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private vmEntryPurBillRcv1 vm1 = new vmEntryPurBillRcv1();
        private vmReportStore1 vm1r = new vmReportStore1();

        private List<string> orderList = new List<string>();

        private List<string> mrrrnoList = new List<string>();

        public frmEntryPurBillRcv1()
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
            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            if (WpfProcessAccess.SupplierContractorList == null)
                WpfProcessAccess.GetSupplierContractorList();

            this.AtxSsircod.Items.Clear();
            foreach (var item1 in WpfProcessAccess.SupplierContractorList)
            {
                //this.AtxSsircod.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
                this.AtxSsircod.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());
            }

            this.AtxtRcvById.AutoSuggestionList.Clear();
            foreach (var item1 in WpfProcessAccess.StaffList)
            {
               // this.AtxtRcvById.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
                this.AtxtRcvById.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());
            }

            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();
          
            foreach (var itemd1 in orderList)
            {

                this.cmborderno.Items.Add(new ComboBoxItem() { Content = itemd1, Tag = "000000000000000000" });

            }

            foreach (var itemd1 in mrrrnoList)
            {

                this.cmbmrrno.Items.Add(new ComboBoxItem() { Content = itemd1, Tag = "000000000000000000" });

            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
             if (DesignerProperties.GetIsInDesignMode(this))
                return;

             if (!this.FrmInitialized)
             {
                 this.FrmInitialized = true;
                 orderList.Add("order-01");
                 orderList.Add("order-02");
                 orderList.Add("order-03");
                 orderList.Add("order-04");
                 orderList.Add("no order");

                 mrrrnoList.Add("MRRno-01");
                 mrrrnoList.Add("MRRno-02");
                 mrrrnoList.Add("MRRno-03");
                 mrrrnoList.Add("MRRno-04");

                 this.IsActiveTransListWindow = false;
                 this.ListViewItemTable1.Clear();
                 this.ConstructAutoCompletionSource();
                 this.ListView1.Tag = "RSIRCOD";                    

                 this.chkAutoTransList.IsChecked = this.IsActiveTransListWindow;
                 this.btnPrint2.Visibility = Visibility.Hidden;
                 this.btnUpdate.Visibility = Visibility.Hidden;
                 this.gridDetails.Visibility = Visibility.Hidden;
                 this.xctk_dtbillDat.Value = DateTime.Today;
                 this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-33);
                 this.xctk_dtpToDate.Value = DateTime.Today;
                 this.xctk_dtrefdatDatpr.Value = DateTime.Today;

                 if (IsActiveTransListWindow)
                     this.gridTransList.Visibility = Visibility.Visible;
                 else
                     this.gridTransList.Visibility = Visibility.Hidden;
             }
        }


        private void btnOkpr_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            this.btnPrint2.Visibility = Visibility.Hidden;
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Hidden;
            this.ListViewItemTable1.Clear();
            this.ListView1.Items.Clear();
            this.ListView2.Items.Clear();
            this.xctk_dtbillDat.IsEnabled = false;
            if (this.btnOkpr.Content.ToString() == "_New")
            {
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.stkIntropr.IsEnabled = true;
                this.AtxtRcvById.Text = "";
                this.txtbillRefpr.Text = "";
                this.txtqrNarpr.Text = "";
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtbillQty.Text = "";
                this.lblUnitordq1.Content = "";
                this.lblbillNo.Content = "BILMM-CCCC-XXXXX";
                this.lblbillNo.Tag = "BILYYYYMMCCCCXXXXX";
                if (IsActiveTransListWindow)
                {
                    this.BuildTransactionList();
                    this.gridTransList.Visibility = Visibility.Visible;
                    this.lvTransList.Focus();
                }
                this.btnOkpr.Content = "_Ok"; 
                return;
            }

            if (this.checkOkValidation() == false)
                return;

            this.btnUpdate.Visibility = Visibility.Visible;
            this.gridTransList.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Visible;
            this.chkDateBlocked.IsChecked = false;
            this.chkDateBlocked.IsEnabled = false;
            this.btnUpdate.IsEnabled = true;
            this.stkItem.IsEnabled = true;
            this.stkIntropr.IsEnabled = false;
            this.btnOkpr.Content = "_New";


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

        private void BuildTransactionList()
        {
            string FromDate = this.xctk_dtpFromDate.Text;
            string ToDate = this.xctk_dtpToDate.Text;
            ASITFunParams.ProcessAccessParams pap1 = vm1r.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "BIL", FromDate, ToDate, "%", "%");
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


        private void UnCheckedAllPopups()
        {
            this.chkFilter.IsChecked = false;
            this.chkPrint2.IsChecked = false;
        }


        private bool checkOkValidation()
        {
            //string srfByID1 = this.AtxtRcvById.Text.Trim();
            string srfByID1 = this.AtxtRcvById.Value.Trim();
            //int length1 = srfByID1.Length;
            if (srfByID1.Length < 0)
                return false;
           // string srfByID2 = srfByID1.Substring(srfByID1.Length - 13).Replace("]", "");

            var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == srfByID1);
            return (listStaff1.Count > 0);
        }


   

        private void txtCodeDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            string wtxtName1 = ((WatermarkTextBox)sender).Name.ToString().Trim();
            string tag1 = (wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
            if (this.ListView1.Tag.ToString().Trim() != tag1)
                this.ListView1.Items.Clear();

            this.ListView1.Tag = (wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
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

            var lvi1 = (vmEntryPurBillRcv1.ListViewItemSelect)this.ListView1.SelectedItem;
            if (ListView1.Tag.ToString().Trim() == "RSIRCOD")
            {
                this.txtRSirDesc.Text = lvi1.trdesc;
                this.txtRSirCode.Text = lvi1.trcode;
                this.lblUnitordq1.Content = lvi1.trunit;
                this.txtRSirCode.Focus();
            }       
        }


        private void ListView1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.ListView1.Items.Count == 0)
                return;

            this.ListView1.SelectedItem = this.ListView1.Items[0];
        }




        private void btnImgV_Click(object sender, RoutedEventArgs e)
        {
            string btnNam1 = ((Button)sender).Name.ToString().Trim();
            switch (btnNam1)
            {
                case "btntxtSrfRef":
                    txtbillRefpr.Clear();
                    btnReferpr.Visibility = Visibility.Collapsed;
                    break;
                case "btntxtRSirDesc":
                    txtRSirDesc.Clear();
                    txtRSirCode.Clear();
                    btntxtRSirDesc.Visibility = Visibility.Collapsed;
                    break;               
            }
        }

        private void txtCodeDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnReferpr.Visibility = (this.txtbillRefpr.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            this.btntxtRSirDesc.Visibility = (this.txtRSirDesc.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            this.PrepareListViewData();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PrepareListViewData()
        {
            ListView1.Items.Clear();
            switch (this.ListView1.Tag.ToString().Trim())
            {
                case "RSIRCOD":
                    this.txtRSirCode.Text = "";
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
                        ListView1.Items.Add(new vmEntryPurBillRcv1.ListViewItemSelect { trcode = item1b.trcode, trdesc = item1b.trdesc, trunit = item1b.trunit });
                    break;
            }
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {

            this.xctk_dtbillDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtbillDat.IsEnabled)
                this.xctk_dtbillDat.Focus();
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {


            decimal srfqty1a = decimal.Parse("0" + this.txtbillQty.Text.Trim());
            if (srfqty1a <= 0)
            {
                this.txtbillQty.Focus();
                return;
            }
            decimal reqAmt1 = decimal.Parse("0" + this.lblAmountCal.Content.ToString().Trim());
            if (reqAmt1 <= 0)
            {
                this.txtrate.Focus();
                return;
            }
            decimal netAmt1 = decimal.Parse("0" + this.lblnetAmount.Content.ToString().Trim());
            decimal disamt1 = decimal.Parse("0" + this.txtdisamt.Text.Trim());
            if (disamt1 <= 0)
            {
                this.txtdisamt.Focus();
                return;
            }
            if (this.txtRSirDesc.Text.Trim().Length == 0)
            {
                this.txtRSirCode.Text = "";
                this.lblUnitordq1.Content = "";
                this.txtbillQty.Text = "";
            }

            vmEntryPurBillRcv1.ListViewItemSelect lvi1 = (vmEntryPurBillRcv1.ListViewItemSelect)this.ListView1.SelectedItem;
            int serialno1 = ListView2.Items.Count + 1;
            string rsircode1 = this.txtRSirCode.Text.Trim();
            string rsirdesc1 = this.txtRSirDesc.Text.Trim();          

            string rsirunit = this.lblUnitordq1.Content.ToString();
            if (rsircode1.Length == 0)
                return;

            var list1a = ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
            if (list1a.Count > 0)
            {
                System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            var item1a = new vmEntryPurBillRcv1.ListViewItemTable()
            {
                trsl = serialno1.ToString() + ".",
                rsircode = rsircode1,
                trdesc = rsircode1 + ": " + rsirdesc1,
                billqty = srfqty1a,
                trunit = rsirunit,
                mrrno="MRR000000000",
                disamt = disamt1,
                mrrqty=0,
                ordrqty=0,
                netamt= netAmt1,

            };

            ListViewItemTable1.Add(item1a);
            ListViewItemTable1.Sort(delegate(vmEntryPurBillRcv1.ListViewItemTable x, vmEntryPurBillRcv1.ListViewItemTable y)
            {
                return x.rsircode.CompareTo(y.rsircode);
            });

            this.ListView2.Items.Clear();
            int i = 1;

            foreach (var item1a1 in ListViewItemTable1)
            {
                item1a1.trsl = i.ToString() + ".";
                ListView2.Items.Add(item1a1);
                i++;
            }
            //this.cmbmrrno.Text = "";
            //this.cmborderno.Text = "";
            this.txtRSirCode.Text = "";
            this.txtRSirDesc.Text = "";
            this.lblUnitordq1.Content = "";
            this.txtbillQty.Text = "";
            this.txtdisamt.Text = "";
            this.txtrate.Text = "";
            this.ListView2.Focus();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
             MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            string cbOrderID1 = ((ComboBoxItem)this.cmborderno.SelectedItem).Tag.ToString();
            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtbillDat.Text), cbOrderID: cbOrderID1, cbSectCode: cbSectCode1, 
                        ssircode1: this.AtxSsircod.Value.Trim(), recvByID1: this.AtxtRcvById.Value.Trim(), MemoRef1: this.txtbillRefpr.Text.Trim(), 
                        MemoNar1: this.txtqrNarpr.Text.Trim(),refdate: DateTime.Parse(this.xctk_dtrefdatDatpr.Text), ListViewItemTable1: this.ListViewItemTable1,
                        _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            var pap1 = vm1.SetParamUpdateBillRecv(WpfProcessAccess.CompInfList[0].comcod, ds1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblbillNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString(); ;
            this.lblbillNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();
            this.btnUpdate.IsEnabled = false;
            this.stkItem.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;
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
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurBillMemo>();
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

                rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurBillRcvMemo01", list1, list2, list3);
                WindowTitle1 = "Bill Receive Memo";
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
                rpt1 = StoreReportSetup.GetLocalReport("Store.RptTransectionList", list1, null, list3);
                WindowTitle1 = "Bill Receive Transaction List";
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

        private void txtQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ClaculateAmt();
        }

        private void txtRate_TextChanged(object sender, TextChangedEventArgs e)
        {
             this.ClaculateAmt();
        }
        private void txtDisAmt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Double CalAmount = Double.Parse("0" + this.lblAmountCal.Content.ToString());
            Double Discount = Double.Parse("0" + this.txtdisamt.Text.ToString());
            Double NetAmount = CalAmount - Discount;
            this.lblnetAmount.Content = NetAmount.ToString("#,##0.00");
        }

        private void ClaculateAmt()
        {
            Double quantity = Double.Parse("0" + this.txtbillQty.Text.ToString());
            Double Rate = Double.Parse("0" + this.txtrate.Text.ToString());
            Double Amount = quantity * Rate;
            this.lblAmountCal.Content = Amount.ToString("#,##0.00"); 
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

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            var item1a = this.lblbillNo.Tag.ToString();
            var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, item1a);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurBillMemo>();
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
            LocalReport Rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurBillRcvMemo01", list1, list2, list3);
            if (Rpt1 == null)
                return;

            RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
            DirectPrint1.PrintReport(Rpt1);
            DirectPrint1.Dispose();
        

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
            this.ListView2.Items.Clear();
            int i = 1;
            foreach (var item1a in ListViewItemTable1)
            {
                item1a.trsl = i.ToString() + ".";
                ListView2.Items.Add(item1a);
                i++;
            }
        }

        private void hlEditRow_Click(object sender, RoutedEventArgs e)
        {
            if (!this.btnUpdate.IsEnabled)  // After updating memo rows can't be edited
                return;

            this.txtRSirCode.Text = "";
            this.txtRSirDesc.Text = "";
            this.lblUnitordq1.Content = "";
            this.txtbillQty.Text = "";

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            var tblItm1 = ListViewItemTable1[RowIndex1 - 1];
            var tblitm2 = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == tblItm1.rsircode);

            this.txtbillQty.Text = tblItm1.billqty.ToString();
            this.txtRSirDesc.Text = tblitm2[0].sirdesc;
            this.lblUnitordq1.Content = tblitm2[0].sirunit;

            this.txtRSirCode.Text = tblItm1.rsircode;
            ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.ListView2.Items.Clear();
         
        }

        private void txtUID_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
