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
    /// Interaction logic for frmEntryPurOrder1.xaml
    /// </summary>
    public partial class frmEntryPurOrder1 : UserControl
    {
        private bool FrmInitialized = false;
        private List<vmEntryPurOrder1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPurOrder1.ListViewItemTable>();
        private List<vmEntryPurOrder1.ListViewTermsList> ListViewTermsTable1 = new List<vmEntryPurOrder1.ListViewTermsList>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        private vmEntryPurOrder1 vm1 = new vmEntryPurOrder1();
        private vmReportStore1 vm1r = new vmReportStore1();
        private bool manualTextChange = true;
        public bool IsActiveTransListWindow { get; set; }

        private List<string> orderList = new List<string>();
        public frmEntryPurOrder1()
        {
            InitializeComponent();
        }

        private void ConstructAutoCompletionSource()
        {
            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");

            foreach (var itemd1 in deptList1)
            {
                //this.cmbSectCodpr.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                if (itemd1.sectname.ToUpper().Contains("STORE"))
                    this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                
            }

            if (WpfProcessAccess.SupplierContractorList == null)
                WpfProcessAccess.GetSupplierContractorList();

            this.AtxSsircod.Items.Clear();
            foreach (var item1 in WpfProcessAccess.SupplierContractorList)
            {
                this.AtxSsircod.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());     //.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");                
            }
            

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            this.AtxordbyId.AutoSuggestionList.Clear();
            foreach (var item1 in WpfProcessAccess.StaffList)
            {
                this.AtxordbyId.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());        //.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
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
                 this.ListViewItemTable1.Clear();
                 this.ConstructAutoCompletionSource();
                 this.ListView1.Tag = "RSIRCOD";

                 orderList.Add("Approve-01");
                 orderList.Add("Approve-02");
                 orderList.Add("Approve-03");
                 orderList.Add("Approve-04");
                 orderList.Add("no Approve");

                 this.chkAutoTransListpr.IsChecked = this.IsActiveTransListWindow;
                 this.btnPrint2.Visibility = Visibility.Hidden;
                 this.btnUpdateor.Visibility = Visibility.Hidden;
                 this.gridDetails.Visibility = Visibility.Hidden;
                 this.xctk_dtpordDat.Value = DateTime.Today;
                 this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-33);
                 this.xctk_dtpToDate.Value = DateTime.Today;

                 if (IsActiveTransListWindow)
                     this.gridTransList.Visibility = Visibility.Visible;
                 else
                     this.gridTransList.Visibility = Visibility.Hidden;
             }
        }

        private void cmbSsirCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cmbordAno.IsEnabled = true;
            foreach (var itemd1 in orderList)
            {
                this.cmbordAno.Items.Add(new ComboBoxItem() { Content = itemd1, Tag = "000000000000" });
            }
            
        }
        

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpordDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpordDat.IsEnabled)
                this.xctk_dtpordDat.Focus();
        }

        private void txtAc_TextChanged(object sender, TextChangedEventArgs e)
        {
            btntxtRSirDesc.Visibility = (txtRSirDesc.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            btnRefer.Visibility = (txtordRef.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            if (manualTextChange == true)
                this.PrepareListViewData(this.ListView1.Tag.ToString().Trim());           
        }


        private void PrepareListViewData(string searchType)
        {
            ListView1.Items.Clear();
            switch (searchType)
            {
                case "RSUID":
                    this.txtRSirCode.Text = "";
                    this.txtRSirDesc.Text = "";
                    string StrUID1 = this.txtUID.Text.Trim().ToUpper();
                    if (StrUID1.Length == 0)
                        return;

                    var List1u = (from lst in WpfProcessAccess.InvItemList
                                  where lst.sirtype.ToUpper().Contains(StrUID1)
                                  select new
                                  {
                                      trcode = lst.sircode,
                                      trdesc = lst.sirdesc,
                                      truid = lst.sirtype,
                                      trunit = lst.sirunit
                                  });
                    foreach (var item1b in List1u)
                        ListView1.Items.Add(new vmEntryPurOrder1.ListViewItemSelect { trcode = item1b.trcode, trdesc = item1b.trdesc, truid = item1b.truid, trunit = item1b.trunit });
                    if (ListView1.Items.Count > 0)
                    {
                        var lvi1 = (vmEntryPurOrder1.ListViewItemSelect)ListView1.Items[0];
                        this.txtRSirDesc.Text = lvi1.trdesc;
                        this.txtRSirCode.Text = lvi1.trcode;
                        this.txtUID.Text = lvi1.truid;
                        this.lblUnitordq1.Content = lvi1.trunit;
                    }
                    break;
                case "RSIRCOD":
                    this.txtRSirCode.Text = "";
                    string StrDesc1 = this.txtRSirDesc.Text.Trim().ToUpper();
                    if (StrDesc1.Length == 0)
                        return;

                    var List1a = (from lst in WpfProcessAccess.InvItemList
                                  where lst.sirdesc.ToUpper().Contains(StrDesc1)
                                  select new
                                  {
                                      //invcode = "000000000000",        // Year(2)+Month(1)+Day(1)+Hour(2)+Minute(2)+Second(2)+Rand(2)
                                      trcode = lst.sircode,
                                      trdesc = lst.sirdesc,
                                      truid = lst.sirtype,
                                      trunit = lst.sirunit
                                  });
                    foreach (var item1b in List1a)
                        ListView1.Items.Add(new vmEntryPurOrder1.ListViewItemSelect { trcode = item1b.trcode, trdesc = item1b.trdesc, truid= item1b.truid, trunit = item1b.trunit });
                    break;
            }
        }
        

        private void btnImgV_Click(object sender, RoutedEventArgs e)
        {
            string btnNam1 = ((Button)sender).Name.ToString().Trim();
            switch (btnNam1)
            {
                case "txtRSirDesc":
                    txtRSirDesc.Clear();
                    txtRSirCode.Clear();
                    btntxtRSirDesc.Visibility = Visibility.Collapsed;
                    break;
                case "btnRefer":
                    txtordRef.Clear();
                    btnRefer.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            this.DataGr();
            this.btnPrint2.Visibility = Visibility.Hidden;
            this.btnUpdateor.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Hidden;
            this.ListViewItemTable1.Clear();
            this.ListView1.Items.Clear();
            this.ListView2.Items.Clear();
            this.xctk_dtpordDat.IsEnabled = false;
            if (this.btnOk.Content.ToString() == "_New")
            {                               
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.stkIntro.IsEnabled = true;
                this.cmbordAno.Text = "";
                this.cmbSectCod.Text = "";
                this.AtxSsircod.Text = "";
                this.AtxordbyId.Text = "";
                this.txtordRef.Text = "";
                this.txtordNar.Text = "";
                this.txtordsubject.Text = "";
                this.txtordleterdes.Text = "";
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtordQty.Text = "";
                this.lblnetAmount.Content = "";
                this.txtUID.Text = "";
                this.lblUnitordq1.Content = "";
                this.lblordNo.Content = "PORMM-CCCC-XXXXX";
                this.lblordNo.Tag = "PORYYYYMMCCCCXXXXX";
                if (IsActiveTransListWindow)
                {
                    this.BuildTransactionList();
                    this.gridTransList.Visibility = Visibility.Visible;
                    this.lvTransList.Focus();
                }
                this.btnOk.Content = "_Ok";
                return;

            }
            if (this.checkOkValidation1() == false)
                return;
            
            this.btnUpdateor.Visibility = Visibility.Visible;
            this.gridTransList.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Visible;
            this.chkDateBlocked.IsChecked = false;
            this.chkDateBlocked.IsEnabled = false;
            this.btnUpdateor.IsEnabled = true;
            this.stkItem.IsEnabled = true;
            this.stkIntro.IsEnabled = false;
            this.btnOk.Content = "_New";
        }
        public class Item
        {
            public int Id  { get; set; }
            public String Subject { get; set; }
            public String Description { get; set; }
        }
        private void DataGr()
        {
            List<vmEntryPurOrder1.ListViewTermsList> list1 = new List<vmEntryPurOrder1.ListViewTermsList>()
                {
                new vmEntryPurOrder1.ListViewTermsList(){ termsid="001", termssubj="Condition 1", termsdesc="All Purchase Order Terms and Conditions apply to both direct and indirect purchases."},
                new vmEntryPurOrder1.ListViewTermsList(){ termsid="002", termssubj="Condition 2", termsdesc="Any supplier receiving a purchase order with the following comment appended to that purchase order:"},
                new vmEntryPurOrder1.ListViewTermsList(){ termsid="003", termssubj="Condition 3", termsdesc="Any supplier receiving a purchase Purchase Order Terms and Conditions apply to both"},
                new vmEntryPurOrder1.ListViewTermsList(){ termsid="004", termssubj="Condition 4", termsdesc="Purchase Order Terms and Conditions apply to both direct a"},
                new vmEntryPurOrder1.ListViewTermsList(){ termsid="005", termssubj="Condition 5", termsdesc="Purchase Order Terms and Any supplier receiving a purchase Conditions apply to both direct a"},
                };

            dtGr.ItemsSource = list1;
        }

        private void BuildTransactionList()
        {
            string FromDate = this.xctk_dtpFromDate.Text;
            string ToDate = this.xctk_dtpToDate.Text;
            var pap1 = vm1r.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "POR", FromDate, ToDate, "%", "%");
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

        private bool checkOkValidation1()
        {
           // string orderByID1 = this.AtxordbyId.Text.Trim();
            int length1 = this.AtxordbyId.Value.Trim().Length; // orderByID1.Length - 13;
            if (length1 < 0)
                return false;
            string srfByID2 = this.AtxordbyId.Value.Trim();  // orderByID1.Substring(orderByID1.Length - 13).Replace("]", "");

            var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == srfByID2);
            return (listStaff1.Count > 0);
        }

        private void chkAutoTransList_Click(object sender, RoutedEventArgs e)
        {
            this.IsActiveTransListWindow = (this.chkAutoTransListpr.IsChecked == true);
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

        private void btnUpdateor_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
         MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            this.ListViewTermsTable1.Clear();

            foreach (var item1 in dtGr.ItemsSource)
                ListViewTermsTable1.Add((vmEntryPurOrder1.ListViewTermsList)item1);

            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            string OrderNum1a = ((ComboBoxItem)this.cmbordAno.SelectedItem).Tag.ToString();
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpordDat.Text),
                        cbSectCode: cbSectCode1, ssircode1: this.AtxSsircod.Value.Trim(), orderByID1: this.AtxordbyId.Value.Trim(),
                        OrderNum1: OrderNum1a, MemoRef1: this.txtordRef.Text.Trim(), MemoNar1: this.txtordNar.Text.Trim(), Memoletter1: this.txtordleterdes.Text.Trim(),
                        Memosub1: txtordsubject.Text.Trim(), ListViewItemTable1: this.ListViewItemTable1, ListTerms: this.ListViewTermsTable1,
                        _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            //String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamUpdateItemReceive(WpfProcessAccess.CompInfList[0].comcod, ds1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblordNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
            this.lblordNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();

            this.btnUpdateor.IsEnabled = false;
            this.stkItem.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;
        }

        private void txtCodeDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            string wtxtName1 = ((WatermarkTextBox)sender).Name.ToString().Trim();
            string tag1 = (wtxtName1 == "txtUID" || wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
            if (this.ListView1.Tag.ToString().Trim() != tag1)
                this.ListView1.Items.Clear();

            manualTextChange = true;
            this.ListView1.Tag = tag1; // (wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
        }

        private void txtUID_LostFocus(object sender, RoutedEventArgs e)
        {
            manualTextChange = false;
            this.PrepareListViewData("RSUID");
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
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
            Double quantity = Double.Parse("0" + this.txtordQty.Text.ToString());
            Double Rate = Double.Parse("0" + this.txtrate.Text.ToString());
            Double Amount = quantity * Rate;
            this.lblAmountCal.Content = Amount.ToString("#,##0.00"); 
        }
        private void btnAddRecordor_Click(object sender, RoutedEventArgs e)
        {
            decimal reqqty1a = decimal.Parse("0" + this.txtordQty.Text.Trim());
            if (reqqty1a <= 0)
            {
                this.txtordQty.Focus();
                return;
            }
            decimal reqAmt1 = decimal.Parse("0" + this.lblAmountCal.Content.ToString().Trim());
            if (reqAmt1 <= 0)
            {
                this.txtrate.Focus();
                return;
            }

            decimal disamt1 = decimal.Parse("0" + this.txtdisamt.Text.Trim());
            if (disamt1 <= 0)
            {
                this.txtdisamt.Focus();
                return;
            }

            decimal netAmt1 = decimal.Parse("0" + this.lblnetAmount.Content.ToString().Trim());

            if (this.txtRSirDesc.Text.Trim().Length == 0)
            {
                this.txtRSirCode.Text = "";
                this.txtUID.Text = "";
                this.lblUnitordq1.Content = "";               
                this.lblAmountCal.Content = "";
                this.txtordQty.Text = "";
                this.txtdisamt.Text = "";
                this.lblnetAmount.Content = "";
                this.txtrate.Text = "";
            }
           
            vmEntryPurOrder1.ListViewItemSelect lvi1 = (vmEntryPurOrder1.ListViewItemSelect)this.ListView1.SelectedItem;
            int serialno1 = ListView2.Items.Count + 1;
            string rsircode1 = this.txtRSirCode.Text.Trim();
            string rsirdesc1 = this.txtRSirDesc.Text.Trim();
            string sruid1 = this.txtUID.Text.Trim();
            string rsirunit = this.lblUnitordq1.Content.ToString();
            if (rsircode1.Length == 0)
                return;

            var list1a = ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
            if (list1a.Count > 0)
            {
                System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            var item1a = new vmEntryPurOrder1.ListViewItemTable()
            {
                trsl = serialno1.ToString() + ".",
                //orderno = "000000000000",
                aprovno = cmbordAno.SelectedIndex.ToString().Trim(), 
                //invcode = DateTime.Now.ToString("yy") + m1[DateTime.Now.Month] + d1[DateTime.Now.Day] +
                //          DateTime.Now.ToString("HHmmss") + new Random().Next(11, 99).ToString().Trim(), // "000000000000", //Year(2)+Month(1)+Day(1)+Hour(2)+Minute(2)+Second(2)+Rand(2)
                rsircode = rsircode1,
                trdesc = rsircode1 + ": " + rsirdesc1,
                orderqty = reqqty1a,
                truid = sruid1,
                trunit = rsirunit,                 
                amt = disamt1 + netAmt1,
               // = Math.Round(reqAmt1 / reqqty1a, 6),
                disamt = disamt1,
                netamt = netAmt1                
            };

            ListViewItemTable1.Add(item1a);
            ListViewItemTable1.Sort(delegate(vmEntryPurOrder1.ListViewItemTable x, vmEntryPurOrder1.ListViewItemTable y)
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

            this.cmbordAno.Text = "";
            this.txtRSirCode.Text = "";
            this.txtRSirDesc.Text = "";
            this.txtUID.Text = "";
            this.lblUnitordq1.Content = "";
            this.txtordQty.Text = "";
            this.txtrate.Text = "";
            this.lblAmountCal.Content = "";
            this.txtdisamt.Text = "";
            this.ListView2.Focus();
        }

        private void ListView1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.ListView1.Items.Count == 0)
                return;

            this.ListView1.SelectedItem = this.ListView1.Items[0];
        }

        private void ListView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ListView1_ShowData();
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.ListView1_ShowData();
            
        }
        private void ListView1_ShowData()
        {

            if (this.ListView1.SelectedItem == null)
                return;

            manualTextChange = false;
            vmEntryPurOrder1.ListViewItemSelect lvi1 = (vmEntryPurOrder1.ListViewItemSelect)this.ListView1.SelectedItem;
            if (ListView1.Tag.ToString().Trim() == "RSIRCOD")
            {
                this.txtRSirDesc.Text = lvi1.trdesc;
                this.txtRSirCode.Text = lvi1.trcode;
                this.txtUID.Text = lvi1.truid;
                this.lblUnitordq1.Content = lvi1.trunit;
                this.txtRSirCode.Focus();
            }         
        }

        private void hlDeleteRow_Click(object sender, RoutedEventArgs e)
        {

            if (!this.btnUpdateor.IsEnabled)  // After updating memo rows can't be deleted
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
            if (!this.btnUpdateor.IsEnabled)  // After updating memo rows can't be edited
                return;

            this.txtRSirCode.Text = "";
            this.txtRSirDesc.Text = "";
            this.txtUID.Text = "";
            this.lblUnitordq1.Content = "";
            this.txtordQty.Text = "";

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            var tblItm1 = ListViewItemTable1[RowIndex1 - 1];
            var tblitm2 = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == tblItm1.rsircode);

            this.txtordQty.Text = tblItm1.orderqty.ToString();
            this.txtRSirDesc.Text = tblitm2[0].sirdesc;
            this.txtUID.Text = tblitm2[0].sirtype;
            this.lblUnitordq1.Content = tblitm2[0].sirunit;
            
            this.txtRSirCode.Text = tblItm1.rsircode;
            ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.ListView2.Items.Clear();
         
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
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurOrderMemo>();
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
                rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurOrderMemo01", list1, list2, list3);
                WindowTitle1 = "Purchase Order Memo";
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
                WindowTitle1 = "Purchase Order Transaction List";
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
            var item1a = this.lblordNo.Tag.ToString();
            var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, item1a);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurOrderMemo>();
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
            LocalReport Rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurOrderMemo01", list1, list2, list3);
            if (Rpt1 == null)
                return;

            RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
            DirectPrint1.PrintReport(Rpt1);
            DirectPrint1.Dispose();
        }                
    }
}
