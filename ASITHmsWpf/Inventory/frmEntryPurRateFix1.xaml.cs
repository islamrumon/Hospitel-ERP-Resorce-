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
    /// Interaction logic for frmEntryRateFix1.xaml
    /// </summary>
    public partial class frmEntryPurRateFix1 : UserControl
    {
        private bool FrmInitialized = false;
        private List<vmEntryRateFix1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryRateFix1.ListViewItemTable>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private vmEntryRateFix1 vm1 = new vmEntryRateFix1();
        private vmReportStore1 vm1r = new vmReportStore1();
        private bool IsActiveTransListWindow { get; set; }
        public frmEntryPurRateFix1()
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            string TitaleTag1 = this.Tag.ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

             if (DesignerProperties.GetIsInDesignMode(this))
                return;

             if (!this.FrmInitialized)
             {
                 this.FrmInitialized = true;
                 IsActiveTransListWindow = false;
                 this.ConstructAutoCompletionSource();
                 this.ListView1pr.Tag = "RSIRCOD";

                 this.chkAutoTransListpr.IsChecked = this.IsActiveTransListWindow;
                 this.btnPrint2pr.Visibility = Visibility.Hidden;
                 this.btnUpdatepr.Visibility = Visibility.Hidden;
                 this.gridDetailspr.Visibility = Visibility.Hidden;
                 this.xctk_dtpreqDatpr.Value = DateTime.Today;
                 this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-33);
                 this.xctk_dtpToDate.Value = DateTime.Today;
                 this.xctk_dtprexpire.Value = DateTime.Today;

                 if (IsActiveTransListWindow)
                     this.gridTransList.Visibility = Visibility.Visible;
                 else
                     this.gridTransList.Visibility = Visibility.Hidden;
             }
        }

        private void ConstructAutoCompletionSource()
        {

            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");

            foreach (var itemd1 in deptList1)
            {
                if (itemd1.sectname.ToUpper().Contains("STORE"))
                    this.cmbSectCodpr.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
            }

            if (WpfProcessAccess.SupplierContractorList == null)
                WpfProcessAccess.GetSupplierContractorList();

            this.Atxtssircode.AutoSuggestionList.Clear();
            foreach (var item1 in WpfProcessAccess.SupplierContractorList)
            {
                this.Atxtssircode.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());    //.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
            }

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            this.AtxtapproveById.AutoSuggestionList.Clear();
            foreach (var item1 in WpfProcessAccess.StaffList)
            {
                this.AtxtapproveById.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());    //.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
            }

            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();
        }


        private void btnOkpr_Click(object sender, RoutedEventArgs e)
        {

             this.UnCheckedAllPopups();
            this.btnPrint2pr.Visibility = Visibility.Hidden;
            this.btnUpdatepr.Visibility = Visibility.Hidden;
            this.gridDetailspr.Visibility = Visibility.Hidden;
           this.ListViewItemTable1.Clear();
            this.ListView1pr.Items.Clear();
            this.ListView2pr.Items.Clear();
            this.xctk_dtpreqDatpr.IsEnabled = false;
            if (this.btnOkpr.Content.ToString() == "_New")
            {
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.stkIntropr.IsEnabled = true;
                this.AtxtapproveById.Text = "";
                this.txtqrRefpr.Text = "";
                this.txtqrNarpr.Text = "";
                this.txtRSirCodepr.Text = "";
                this.txtRSirDescpr.Text = "";
                this.txtrqRatepr.Text = "";

                this.lblreqNopr.Content = "QRAMM-CCCC-XXXXX";
                this.lblreqNopr.Tag = "QRAYYYYMMCCCCXXXXX";
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

            this.btnUpdatepr.Visibility = Visibility.Visible;
           this.gridTransList.Visibility = Visibility.Hidden;
            this.gridDetailspr.Visibility = Visibility.Visible;
            this.chkDateBlocked.IsChecked = false;
            this.chkDateBlocked.IsEnabled = false;
            this.btnUpdatepr.IsEnabled = true;
            this.stkItempr.IsEnabled = true;
            this.stkIntropr.IsEnabled = false;
            this.btnOkpr.Content = "_New"; 

        }

        private bool checkOkValidation()
        {
            //string suplierID1 = this.Atxtssircode.Text.Trim();
            int length0 = this.Atxtssircode.Text.Trim().Length;  // suplierID1.Length - 13;
           // string reqByID1 = this.AtxtapproveById.Text.Trim();
            int length1 = this.AtxtapproveById.Text.Trim().Length; // reqByID1.Length - 13;

            if (length0 < 0 || length1 < 0)
                return false;

            string reqByID2 = this.AtxtapproveById.Value.Trim();  // reqByID1.Substring(reqByID1.Length - 13).Replace("]", "");

            var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == reqByID2);
            if (listStaff1.Count == 0)
                return false;

            string suplierID2 = this.Atxtssircode.Value.Trim();    // suplierID1.Substring(suplierID1.Length - 13).Replace("]", "");
            var listSup1 = WpfProcessAccess.SupplierContractorList.FindAll(x => x.sircode == suplierID2);
            if (listSup1.Count == 0)
                return false;

            return true;
        }

        private void btnImgV_Click(object sender, RoutedEventArgs e)
        {
            string btnNam1 = ((Button)sender).Name.ToString().Trim();
            switch (btnNam1)
            {
                case "btntxtRSirDescpr":
                    txtRSirDescpr.Clear();
                    txtRSirCodepr.Clear();
                    btntxtRSirDescpr.Visibility = Visibility.Collapsed;
                    break;
                case "btnReferpr":
                    txtqrRefpr.Clear();
                    btnReferpr.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void txtAc_TextChanged(object sender, TextChangedEventArgs e)
        {
            btntxtRSirDescpr.Visibility = (txtRSirDescpr.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            btnReferpr.Visibility = (txtqrRefpr.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            PrepareListViewData();
        }

        private void txtCodeDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            string wtxtName1 = ((WatermarkTextBox)sender).Name.ToString().Trim();
            string tag1 = (wtxtName1 == "txtRSirCodepr" || wtxtName1 == "txtRSirDescpr" ? "RSIRCOD" : "UNKNOWN");
            if (this.ListView1pr.Tag.ToString().Trim() != tag1)
                this.ListView1pr.Items.Clear();

            this.ListView1pr.Tag = (wtxtName1 == "txtRSirCodepr" || wtxtName1 == "txtRSirDescpr" ? "RSIRCOD" : "UNKNOWN");
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void PrepareListViewData()
        {
            ListView1pr.Items.Clear();
            switch (this.ListView1pr.Tag.ToString().Trim())
            {
                case "RSIRCOD":
                    this.txtRSirCodepr.Text = "";
                    string StrDesc1 = this.txtRSirDescpr.Text.Trim().ToUpper();
                    if (StrDesc1.Length == 0)
                        return;

                    var List1a = (from lst in WpfProcessAccess.InvItemList
                                  where lst.sirdesc.ToUpper().Contains(StrDesc1)
                                  select new
                                  {

                                      trcode = lst.sircode,
                                      trdesc = lst.sirdesc,
                                      trunit =lst.sirunit
                                      
                                      
                                  });
                    foreach (var item1b in List1a)
                        ListView1pr.Items.Add(new vmEntryRateFix1.ListViewItemSelect { trcode = item1b.trcode, trdesc = item1b.trdesc, trunit=item1b.trunit });
                    break;
            }
        }

        private void ListView1_ShowData()
        {

            if (this.ListView1pr.SelectedItem == null)
                return;

            vmEntryRateFix1.ListViewItemSelect lvi1 = (vmEntryRateFix1.ListViewItemSelect)this.ListView1pr.SelectedItem;
            if (ListView1pr.Tag.ToString().Trim() == "RSIRCOD")
            {
                this.txtRSirDescpr.Text = lvi1.trdesc;
                this.txtRSirCodepr.Text = lvi1.trcode;
                this.lblUnit1pr.Content = lvi1.trunit;
                this.txtRSirCodepr.Focus();
            }
        }

        private void ListView1pr_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.ListView1pr.Items.Count == 0)
                return;

            this.ListView1pr.SelectedItem = this.ListView1pr.Items[0];
        }

        private void ListView1pr_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ListView1_ShowData();
        }
        private void ListView1pr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.ListView1_ShowData();
        }

        private void btnAddRecordpr_Click(object sender, RoutedEventArgs e)
        {
            decimal qrRate1 = decimal.Parse("0" + this.txtrqRatepr.Text.ToString().Trim());
            if (qrRate1 <= 0)
            {
                this.txtrqRatepr.Focus();
                return;
            }


            if (this.txtRSirDescpr.Text.Trim().Length == 0)
            {
                this.txtRSirCodepr.Text = "";
                this.txtaprvnote.Text = "";
                this.txtrqRatepr.Text = "";
                this.lblUnit1pr.Content = "";
                this.txtrqRatepr.Text = "";
            }

            vmEntryRateFix1.ListViewItemSelect lvi1 = (vmEntryRateFix1.ListViewItemSelect)this.ListView1pr.SelectedItem;
            int serialno1 = ListView2pr.Items.Count + 1;
            string rsircode1 = this.txtRSirCodepr.Text.Trim();
            string rsirdesc1 = this.txtRSirDescpr.Text.Trim();
            string qrexpiDate1= this.xctk_dtprexpire.Text.ToString();
            string qraprvnote =this.txtaprvnote.Text.ToString();
            string qrunit = this.lblUnit1pr.Content.ToString();
            if (rsircode1.Length == 0)
                return;

            var list1a = ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
            if (list1a.Count > 0)
            {
                System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            var item1a = new vmEntryRateFix1.ListViewItemTable()
            {
                trsl = serialno1.ToString() + ".",
                rsircode = rsircode1,
                trdesc = rsircode1 + ": " + rsirdesc1,
                aprvrate = qrRate1,
                expiryDate = qrexpiDate1,
                aprvnote = qraprvnote,
                sirunit=qrunit             
            };

            ListViewItemTable1.Add(item1a);
            ListViewItemTable1.Sort(delegate(vmEntryRateFix1.ListViewItemTable x, vmEntryRateFix1.ListViewItemTable y)
            {
                return x.rsircode.CompareTo(y.rsircode);
            });

            this.ListView2pr.Items.Clear();
            int i = 1;

            foreach (var item1a1 in ListViewItemTable1)
            {
                item1a1.trsl = i.ToString() + ".";
                ListView2pr.Items.Add(item1a1);
                i++;
            }
            this.txtRSirCodepr.Text = "";
            this.txtRSirDescpr.Text = "";
            this.lblUnit1pr.Content = "";
            this.txtaprvnote.Text = "";
            this.txtrqRatepr.Text = "";
            this.ListView2pr.Focus();        
        }

        private void hlDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (!this.btnUpdatepr.IsEnabled)  // After updating memo rows can't be deleted
                return;

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            if (System.Windows.MessageBox.Show("Are you sure to delete record " + RowIndex1.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.ListView2pr.Items.Clear();
            int i = 1;
            foreach (var item1a in ListViewItemTable1)
            {
                item1a.trsl = i.ToString() + ".";
                ListView2pr.Items.Add(item1a);
                i++;
            }
        }

        private void btnUpdatepr_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
           MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCodpr.SelectedItem).Tag.ToString();
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpreqDatpr.Text),
                cbSectCode: cbSectCode1, ssircode1: this.Atxtssircode.Value.Trim(), sirByID1: this.AtxtapproveById.Value.Trim(), 
                MemoRef1: this.txtqrRefpr.Text.Trim(), MemoNar1: this.txtqrNarpr.Text.Trim(), ListViewItemTable1: this.ListViewItemTable1,
                _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            //String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamUpdatePurRate(WpfProcessAccess.CompInfList[0].comcod, ds1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblreqNopr.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString(); ;
            this.lblreqNopr.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();

            this.btnUpdatepr.IsEnabled = false;
            this.stkItempr.IsEnabled = false;
            this.btnPrint2pr.Visibility = Visibility.Visible;

        }
        private void hlEditRow_Click(object sender, RoutedEventArgs e)
        {
            if (!this.btnUpdatepr.IsEnabled)  // After updating memo rows can't be edited
                return;

            this.txtRSirCodepr.Text = "";
            this.txtRSirDescpr.Text = "";
            this.txtrqRatepr.Text = "";
            this.txtaprvnote.Text = "";


            this.txtrqRatepr.Text = "";

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            var tblItm1 = ListViewItemTable1[RowIndex1 - 1];
            var tblitm2 = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == tblItm1.rsircode);

            this.txtaprvnote.Text = tblItm1.aprvnote.ToString();
            this.txtrqRatepr.Text = tblItm1.aprvrate.ToString();
            this.xctk_dtprexpire.Text = tblItm1.expiryDate.ToString();
            this.txtRSirDescpr.Text = tblitm2[0].sirdesc;         
            this.txtRSirCodepr.Text = tblItm1.rsircode;
            ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.ListView2pr.Items.Clear();
            int i = 1;
            ListViewItemTable1.Sort(delegate(vmEntryRateFix1.ListViewItemTable x, vmEntryRateFix1.ListViewItemTable y)
            {
                return x.rsircode.CompareTo(y.rsircode);
            });


            foreach (var item1a in ListViewItemTable1)
            {
                item1a.trsl = i.ToString() + ".";
                ListView2pr.Items.Add(item1a);
                i++;
            }
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {

            this.xctk_dtpreqDatpr.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpreqDatpr.IsEnabled)
                this.xctk_dtpreqDatpr.Focus();
        }

        private void chkAutoTransListpr_Click(object sender, RoutedEventArgs e)
        {
            this.IsActiveTransListWindow = (this.chkAutoTransListpr.IsChecked == true);
            if (this.IsActiveTransListWindow && this.gridDetailspr.Visibility == Visibility.Hidden)
            {
                this.BuildTransactionList();
                this.gridTransList.Visibility = Visibility.Visible;
                this.lvTransList.Focus();
            }
            else if (this.IsActiveTransListWindow == false && this.gridDetailspr.Visibility == Visibility.Hidden)
               this.gridTransList.Visibility = Visibility.Hidden;
            this.chkFilter.IsChecked = false;
            this.chkPrint2.IsChecked = false;
        }

        private void BuildTransactionList()
        {
            string FromDate = this.xctk_dtpFromDate.Text;
            string ToDate = this.xctk_dtpToDate.Text;
            ASITFunParams.ProcessAccessParams pap1 = vm1r.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "QRA", FromDate, ToDate, "%");
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
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurRateMemo>();
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
                rpt1 = StoreReportSetup.GetLocalReport("Procurement.RateFixMemo01", list1, list2, list3);
                WindowTitle1 = "Rate Fix Memo";
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
                WindowTitle1 = "Rate Fix Transaction List";
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
            this.chkFilter.IsChecked = false;
            this.chkPrint2.IsChecked = false;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.BuildTransactionList();
            this.chkFilter.IsChecked = false;
        }

        private void btnPrint2pr_Click(object sender, RoutedEventArgs e)
        {
            var item1a = this.lblreqNopr.Tag.ToString();
            var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, item1a);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurRateMemo>();
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
            LocalReport Rpt1 = StoreReportSetup.GetLocalReport("Procurement.RateFixMemo01", list1, list2, list3);
            if (Rpt1 == null)
                return;

            RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
            DirectPrint1.PrintReport(Rpt1);
            DirectPrint1.Dispose();
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
    }
}
