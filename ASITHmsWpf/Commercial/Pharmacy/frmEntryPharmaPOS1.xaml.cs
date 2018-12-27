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
using ASITHmsRpt2Inventory;
using ASITHmsRpt4Commercial;
using Microsoft.Reporting.WinForms;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace ASITHmsWpf.Commercial.Pharmacy
{
    /// <summary>
    /// Interaction logic for frmEntryPharmaPOS1.xaml
    /// </summary>
    public partial class frmEntryPharmaPOS1 : UserControl
    {
        private bool SearchWithInvCode = false;// true; // if "true" Inventory code wise search and sales 
        private List<HmsEntityCommercial.InvoiceTransList> InvList = new List<HmsEntityCommercial.InvoiceTransList>();
        private List<vmEntryPharRestPOS1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPharRestPOS1.ListViewItemTable>();
        private List<vmEntryPharRestPOS1.StockItemList> InvStockItemList = new List<vmEntryPharRestPOS1.StockItemList>();
        private List<vmEntryPharRestPOS1.StockItemSumList> InvStockItemSumList = new List<vmEntryPharRestPOS1.StockItemSumList>();
        private List<vmEntryPharRestPOS1.ListViewItemTable> ListViewItemTable1a = new List<vmEntryPharRestPOS1.ListViewItemTable>();

        //List<HmsEntityCommercial.InvoiceTransList> Rptlist2 = new List<HmsEntityCommercial.InvoiceTransList>();
        //List<HmsEntityCommercial.PhSalesInvoice01> Rptlist1 = new List<HmsEntityCommercial.PhSalesInvoice01>();
        List<HmsEntityCommercial.PhSalesInvoice01> LstDueMemo1 = new List<HmsEntityCommercial.PhSalesInvoice01>();
        List<vmEntryPharRestPOS1.PhSalesCollMemos01> LstDueMemoCol1 = new List<vmEntryPharRestPOS1.PhSalesCollMemos01>();

        vmEntryPharRestPOS1 vm1 = new vmEntryPharRestPOS1();
        vmReportPharRestPOS1 vm2 = new vmReportPharRestPOS1();
        DataSet EditDs;
        public frmEntryPharmaPOS1()
        {
            InitializeComponent();
            ConstructAutoCompletionSource();
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
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.stkpItemUI.Visibility = Visibility.Collapsed;  // If required this option can be visible for special option
            //------------------------------------------------

            this.btnPrint2.Visibility = Visibility.Hidden;
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Hidden;
            this.xctk_dtSalesDat.Value = DateTime.Today;

            string FrmDate1 = DateTime.Today.AddDays(-360).ToString("dd-MMM-yyyy");
            string ToDate1 = DateTime.Today.ToString("dd-MMM-yyyy");

            this.InvList.Clear();
            this.InvList = PreviousMemoList(FrmDate1, ToDate1);
            this.InvList = InvList.FindAll(x => x.invno.Substring(0, 3) == "MSI");
            if (this.InvList == null)
                return;

            foreach (var item1 in InvList)
            {
                this.cmbPrevInvList.Items.Add(new ComboBoxItem()
                    {
                        Content = item1.invno1.Substring(3, 2) + "-" + item1.invno1.Substring(11, 5) + " [Tk. " + item1.billam.ToString("#,##0.00") +
                        (item1.dueam <= 0 ? "" : ", Due: Tk. " + item1.dueam.ToString("#,##0.00")) + ", " +
                        item1.invdat.ToString("dd.MM.yyyy") + "] " + (item1.invref.Trim().Length > 0 ? ", " + item1.invref.Trim() : "") +
                        (item1.invnar.Trim().Length > 0 ? ", " + item1.invnar.Trim() : ""),
                        Tag = item1.invno
                    }
                    );
            }
        }

        private List<HmsEntityCommercial.InvoiceTransList> PreviousMemoList(string Date1, string Date2, string searchStr = "%")
        {
            string sectcod1 = ((ComboBoxItem)this.cmbSectCod.Items[this.cmbSectCod.SelectedIndex]).Tag.ToString();
            var pap1 = vm2.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, "A00MSISUM", Date1, Date2, sectcod1, "MSI");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return null;

            return ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtSalesDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtSalesDat.IsEnabled)
                this.xctk_dtSalesDat.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.btnPrint2.Visibility = Visibility.Hidden;
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Hidden;
            this.dgvMemo.ItemsSource = null;
            this.ListViewItemTable1.Clear();
            this.xctk_dtSalesDat.IsEnabled = false;
            this.autoItemSearch.SelectedValue = null;
            this.lblTotalAmt.Content = "";
            this.lblTDiscAmt.Content = "";
            this.txtRcvAmt.Text = "";
            this.lblTotalNetAmt.Content = "";
            this.lblPaidAmt.Content = "";
            this.lblReturnAmt.Content = "";
            this.lblDueAmt.Content = "";
            this.txtDisPer.Text = "0.00";
            this.txtinvRef.Text = "";
            this.txtinvNar.Text = "";
            this.txtinvQty.Text = "";
            this.AtxtItemCode.Text = "";
            this.lblMfgByName.Content = "";
            this.lblAmountShow.Content = "";
            this.lblSalesRate.Content = "";
            this.lblUnit1.Content = "";
            this.lblinvNo.Content = "INVMM-CCCC-XXXXX";
            this.lblinvNo.Tag = "INVYYYYMMCCCCXXXXX";
            this.EditDs = null;

            if (this.btnOk.Content.ToString() == "_New")
            {
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.stkIntro.IsEnabled = true;
                this.btnOk.Content = "_Ok";
                return;
            }

            if (this.GetStockItemList() == false)
                return;

            this.btnUpdate.Visibility = Visibility.Visible;
            this.gridDetails.Visibility = Visibility.Visible;
            this.btnAddRecord.Visibility = Visibility.Visible;
            this.chkDateBlocked.IsChecked = false;
            this.chkDateBlocked.IsEnabled = false;
            this.btnUpdate.IsEnabled = true;
            this.stkIntro.IsEnabled = false;
            this.btnOk.Content = "_New";
            this.txtUID.Focus();
        }

        private bool GetStockItemList()
        {
            string StoreID1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            string AsOnDate1 = this.xctk_dtSalesDat.Text;
            string StockItemType1 = "ALLITEMS"; // Show Item with Zero or Negative Stocks
            // string StockItemType1 = "STOCKITEMS"; // Show Item with Non Zero or Positive Stocks
            var pap1 = vm1.SetParamGetStockItemList(WpfProcessAccess.CompInfList[0].comcpcod, StoreID1, AsOnDate1, StockItemType1, "SUMDETAILS");
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
            {
                WpfProcessAccess.ShowDatabaseErrorMessage();
                return false;
            }

            this.InvStockItemList.Clear();
            this.InvStockItemSumList.Clear();
            this.InvStockItemList = ds2.Tables[0].DataTableToList<vmEntryPharRestPOS1.StockItemList>().FindAll(x => x.sircode.Substring(0, 4) == "4521" && x.saleprice > 0);
            this.InvStockItemSumList = ds2.Tables[1].DataTableToList<vmEntryPharRestPOS1.StockItemSumList>().FindAll(x => x.sircode.Substring(0, 4) == "4521" && x.saleprice > 0);

            // Blocked on 03-Sep-2016 -- by Hafiz
            //this.AtxtItemCode.Items.Clear();
            //this.AtxtItemCode.AutoSuggestionList.Clear();
            //if (SearchWithInvCode == true) // When Invcode Considered on Sales
            //{
            //    foreach (var item1 in this.InvStockItemList)
            //    {
            //        if (item1.saleprice > 0)
            //        {
            //            string trdesc1 = item1.sirdesc.Trim() + ", Inv.Code: " + item1.invcode + ", Exp.Date: " + item1.expdat.ToString("dd-MMM-yyy") +
            //                            ", Rate: " + item1.saleprice.ToString("#,##0.00") + ", C.Stock: " + item1.stkqty.ToString("#,##0.00") + " " + item1.sirunit;
            //            this.AtxtItemCode.AddSuggstionItem(trdesc1, item1.sircode.Trim() + item1.invcode.Trim());
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (var item1 in this.InvStockItemSumList)
            //    {
            //        if (item1.saleprice > 0)
            //        {
            //            string trdesc1 = item1.sirdesc.Trim() + ", Rate: " + item1.saleprice.ToString("#,##0.00") + ", C.Stock: " + item1.stkqty.ToString("#,##0.00") + " " + item1.sirunit;
            //            this.AtxtItemCode.AddSuggstionItem(trdesc1, item1.sircode.Trim() + "000000000000");
            //        }
            //    }
            //}
            // End of Blocked on 03-Sep-2016 -- by Hafiz
            return true;
        }

        private void txtSearchItem1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string srchItem1 = txtSearchItem1.Text.Trim().ToUpper();
            if (SearchWithInvCode == true) // When Invcode Considered on Sales
            {
                var InvStockItemList1 = this.InvStockItemList.FindAll(x => (x.sirdesc.Trim() + ", " + x.invcode + ", " + x.expdat.ToString("dd-MMM-yyy") +
                                            ", " + x.saleprice.ToString("#,##0.00") + ", " + x.stkqty.ToString("#,##0.00")).ToUpper().Contains(srchItem1) && x.saleprice > 0);
                this.lstItem1.Items.Clear();
                foreach (var item1 in InvStockItemList1)
                {
                    this.lstItem1.Items.Add(new ListBoxItem()
                    {
                        Content = (item1.sirdesc.Trim() + ", Inv.Code: " + item1.invcode + ", Exp.Date: " + item1.expdat.ToString("dd-MMM-yyy") +
                                            ", Rate: " + item1.saleprice.ToString("#,##0.00") + ", C.Stock: " + item1.stkqty.ToString("#,##0.00") + " " + item1.sirunit),
                        Tag = item1.sircode.Trim() + item1.invcode.Trim(),
                        ToolTip = (item1.mfgcomnam.Trim().Length==0 ? "<< Manufacturer name not found >>" : "Mfg By: " + item1.mfgcomnam.Trim())
                    });
                }
            }
            else
            {
                var InvStockItemList1s = this.InvStockItemSumList.FindAll(x => (x.sirdesc.Trim() + ", " + x.saleprice.ToString("#,##0.00") + ", " + x.stkqty.ToString("#,##0.00")).ToUpper().Contains(srchItem1) && x.saleprice > 0);
                this.lstItem1.Items.Clear();
                foreach (var item1 in InvStockItemList1s)
                {
                    this.lstItem1.Items.Add(new ListBoxItem()
                    {
                        Content = (item1.sirdesc.Trim() + ", Rate: " + item1.saleprice.ToString("#,##0.00") + ", C.Stock: " + item1.stkqty.ToString("#,##0.00") + " " + item1.sirunit),
                        Tag = item1.sircode.Trim() + item1.sircode.Trim() + "000000000000",
                        ToolTip = (item1.mfgcomnam.Trim().Length == 0 ? "<< Manufacturer name not found >>" : "Mfg By: " + item1.mfgcomnam.Trim())
                    });
                }
            }

            if (this.lstItem1.Items.Count > 0)
                this.lstItem1.SelectedIndex = 0;
        }
        private void txtSearchItem1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                this.lstItem1.Focus();
            else if (e.Key == Key.Return)
            {
                if (this.lstItem1.Items.Count > 0)
                {
                    this.lstItem1.SelectedIndex = 0;
                    this.ListItem1_ShowData();
                }
            }
        }
        private void txtSearchItem1_LostFocus(object sender, RoutedEventArgs e)
        {
            //this.ListItem1_ShowData();
        }

        private void lstItem1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.ListItem1_ShowData();
            this.txtSearchItem1.Focus();
        }
        private void lstItem1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.ListItem1_ShowData();
                this.txtSearchItem1.Focus();
            }
        }

        private void ListItem1_ShowData()
        {
            if (this.lstItem1.SelectedItem == null)
                return;
            string rsircode1 = ((ListBoxItem)this.lstItem1.SelectedItem).Tag.ToString();// this.txtSearchItem1.Tag.ToString().Trim();

            this.txtSearchItem1.Tag = rsircode1;
            this.txtSearchItem1.ToolTip = rsircode1;
            this.txtSearchItem1.Text = ((ListBoxItem)this.lstItem1.SelectedItem).Content.ToString();

            if (SearchWithInvCode == true)
            {
                var lvi1 = this.InvStockItemList.Find(x => x.sircode + x.invcode == rsircode1);
                this.txtUID.Text = lvi1.siruid;
                this.lblUnit1.Content = lvi1.sirunit;
                this.lblMfgByName.Content = lvi1.mfgcomnam.Trim();
                this.lblSalesRate.Content = lvi1.saleprice.ToString("#,##0.00");
            }
            else
            {
                var lvi1 = this.InvStockItemSumList.Find(x => x.sircode + "000000000000" == rsircode1);
                this.txtUID.Text = lvi1.siruid;
                this.lblUnit1.Content = lvi1.sirunit;
                this.lblMfgByName.Content = lvi1.mfgcomnam;
                this.lblSalesRate.Content = lvi1.saleprice.ToString("#,##0.00");
            }
            this.txtinvQty.Text = "";
            this.lblAmountShow.Content = "";// lvi1.saleprice.ToString("#,##0.00");            
        }
        private void txtinvRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.lblAmountShow.Content = "";
            try
            {
                Double quantity = Double.Parse("0" + this.txtinvQty.Text.ToString().Trim());
                Double Rate = Double.Parse("0" + this.lblSalesRate.Content.ToString().Trim());
                Double Amount = quantity * Rate;
                this.lblAmountShow.Content = "Amt: " + Amount.ToString("#,##0.00").Trim();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoItemSearch.SelectedValue == null)
                return;

            decimal invqty1a = this.validData("0" + this.txtinvQty.Text.Trim());
            if (invqty1a <= 0)
            {
                this.txtinvQty.Focus();
                return;
            }

            decimal invRat1 = this.validData("0" + this.lblSalesRate.Content.ToString().Trim());
            decimal invAmt1 = Math.Round(invqty1a * invRat1, 6);// this.validData("0" + this.lblAmountShow.Content.ToString().Trim());
            int serialno1 = ListViewItemTable1.Count + 1;
            string invcode1 = "000000000000";
            string rsircode1 = "000000000000";
            string rsircode1s = this.autoItemSearch.SelectedValue.ToString();
            string rsirdesc1 = "";
            string siruid1 = "";
            string mfgByName1 = "";
            string batchno1 = "";
            DateTime mfgdat1 = DateTime.Parse("01-Jan-1900");
            DateTime expdat1 = DateTime.Parse("01-Jan-1900");
            decimal stkqty1 = 0.00m;
            if (SearchWithInvCode == true) // When Invcode Considered on Sales
            {
                //var lvi1a = this.InvStockItemList.Find(x => x.sircode + x.invcode == this.AtxtItemCode.Value);
                var lvi1a = this.InvStockItemList.Find(x => x.sircode + x.invcode == this.txtSearchItem1.Tag.ToString());
                invcode1 = lvi1a.invcode;                
                rsircode1 = lvi1a.sircode;
                rsirdesc1 = lvi1a.sirdesc.Trim() + (lvi1a.batchno.Trim().Length > 0 ? ", Batch No: " + lvi1a.batchno.Trim() : "") + ", Exp.Date: " + lvi1a.expdat.ToString("dd-MMM-yyyy");
                siruid1 = lvi1a.mfgid;
                mfgByName1 = lvi1a.mfgcomnam;
                batchno1 = lvi1a.batchno;
                mfgdat1 = lvi1a.mfgdat;
                expdat1 = lvi1a.expdat;
                stkqty1 = lvi1a.stkqty;
            }
            else
            {
                //var lvi1 = this.InvStockItemSumList.Find(x => x.sircode + "000000000000" == this.AtxtItemCode.Value);
                //var lvi1 = this.InvStockItemSumList.Find(x => x.sircode + "000000000000" == this.txtSearchItem1.Tag.ToString());

                var lvi1 = this.InvStockItemSumList.Find(x => x.sircode == rsircode1s);               
                rsircode1 = lvi1.sircode;
                rsirdesc1 = lvi1.sirdesc;
                siruid1 = lvi1.mfgid;
                mfgByName1 = lvi1.mfgcomnam;
                stkqty1 = lvi1.stkqty;
            }
            string truid1a = this.txtUID.Text.Trim();
            string rsirunit = this.lblUnit1.Content.ToString();
            if (rsircode1.Length == 0)
                return;

            //if (invqty1a > stkqty1)
            //{
            //    System.Windows.MessageBox.Show("Required quantity out of stock for " + rsirdesc1, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
            //    this.txtinvQty.Focus();
            //    return;
            //}

            //var list1a = ListViewItemTable1.FindAll(x => x.rsircode + x.invcode == rsircode1 + invcode1);
            //if (list1a.Count > 0)
            //{
            //    System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
            //    return;
            //}

            bool ItemExist = false;

            for(int i=0; i<ListViewItemTable1.Count; i++)
            {
               if(this.ListViewItemTable1[i].rsircode + this.ListViewItemTable1[i].invcode == rsircode1 + invcode1)
               {

                   this.ListViewItemTable1[i].invqty = invqty1a;
                   this.ListViewItemTable1[i].invrate = Math.Round(invAmt1 / invqty1a, 6);
                   this.ListViewItemTable1[i].invamt = invAmt1;
                   this.ListViewItemTable1[i].invdisamt = 0.00m;
                   this.ListViewItemTable1[i].invnetamt = invAmt1;
                   this.ListViewItemTable1[i].invvatper = 0.00m;
                   this.ListViewItemTable1[i].invvatamt = 0.00m;
                   this.ListViewItemTable1[i].batchno = batchno1;
                   this.ListViewItemTable1[i].mfgdat = mfgdat1;
                   this.ListViewItemTable1[i].expdat = expdat1;
                   ItemExist = true;
                   break;
               }
            }
            if (!ItemExist)
            {
                var item1a = new vmEntryPharRestPOS1.ListViewItemTable()
                {
                    trsl = serialno1.ToString() + ".",
                    invcode = invcode1,
                    reptsl = "00",
                    rsircode = rsircode1,
                    trdesc = rsirdesc1,
                    mfgid = siruid1,
                    mfgcomnam = mfgByName1,
                    invqty = invqty1a,
                    truid = truid1a,
                    trunit = rsirunit,
                    invrate = Math.Round(invAmt1 / invqty1a, 6),
                    invamt = invAmt1,
                    invdisamt = 0.00m,
                    invnetamt = invAmt1,
                    invvatper = 0.00m,
                    invvatamt = 0.00m,
                    invrmrk = "",
                    batchno = batchno1,
                    mfgdat = mfgdat1,
                    expdat = expdat1,
                    mfgvisible = "Collapsed",
                    rmrkvisible = "Collapsed"
                };
                this.dgvMemo.ItemsSource = null;
                this.ListViewItemTable1.Add(item1a);
            }

            //ListViewItemTable1.Sort(delegate(vmEntryPharRestPOS1.ListViewItemTable x, vmEntryPharRestPOS1.ListViewItemTable y)
            //{
            //    return x.trdesc.CompareTo(y.trdesc);
            //    //return x.rsircode.CompareTo(y.rsircode);
            //});

            this.dgvMemo.ItemsSource = this.ListViewItemTable1;

            this.AtxtItemCode.Text = "";
            this.txtUID.Text = "";
            this.lblMfgByName.Content = "";
            this.lblUnit1.Content = "";
            this.txtinvQty.Text = "";
            this.lblAmountShow.Content = "";
            this.lblSalesRate.Content = "";
            this.RecalCulateInfo();
            this.txtUID.Focus();
        }

        private Decimal validData(string txtData)
        {
            try
            {
                return decimal.Parse(txtData);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            this.ListViewItemTable1a.Clear();
            if (SearchWithInvCode == true) // When Invcode Considered on Sales
            {
                foreach (var item1a in this.ListViewItemTable1)
                {
                    string itcod1 = item1a.rsircode;
                    string invcod1 = item1a.invcode;
                    decimal itrat = item1a.invrate;
                    decimal iqty = item1a.invqty;
                    decimal disam1 = item1a.invdisamt / iqty;
                    //var lvi1 = this.InvStockItemList.Find(x => x.sircode + x.invcode == itcod1 + invcod1);
                    //if (iqty > lvi1.stkqty)
                    //{
                    //    System.Windows.MessageBox.Show("Required quantity out of stock for " + lvi1.sirdesc.Trim(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    //    this.txtinvQty.Focus();
                    //    return;
                    //}

                    ListViewItemTable1a.Add(new vmEntryPharRestPOS1.ListViewItemTable()
                    {
                        trsl = "",
                        invcode = item1a.invcode,
                        rsircode = item1a.rsircode,
                        trdesc = item1a.trdesc,
                        mfgid = item1a.mfgid,
                        mfgcomnam = item1a.mfgcomnam,
                        invqty = item1a.invqty,
                        truid = item1a.truid,
                        trunit = item1a.trunit,
                        invrate = item1a.invrate,
                        invamt = item1a.invqty * item1a.invrate,
                        invdisamt = disam1 * item1a.invqty,
                        invnetamt = (item1a.invqty * item1a.invrate) - (disam1 * item1a.invqty),
                        invvatper = 0.00m,
                        invvatamt = 0.00m,
                        invrmrk = "",
                        batchno = item1a.batchno,
                        mfgdat = item1a.mfgdat,
                        expdat = item1a.expdat,
                        mfgvisible = "Collapsed",
                        rmrkvisible = "Collapsed"
                    });
                }
            }
            else
            {
                foreach (var item1a in this.ListViewItemTable1)
                {
                    string itcod1 = item1a.rsircode;
                    decimal itrat = item1a.invrate;
                    decimal iqty = item1a.invqty;
                    decimal disam1 = item1a.invdisamt / iqty;
                    //var lvi1 = this.InvStockItemSumList.Find(x => x.sircode == itcod1);
                    //if (iqty > lvi1.stkqty)
                    //{
                    //    System.Windows.MessageBox.Show("Required quantity out of stock for " + lvi1.sirdesc.Trim(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    //    this.txtinvQty.Focus();
                    //    return;
                    //}
                    var item1b = this.InvStockItemList.FindAll(x => x.sircode == itcod1);
                    item1b.Sort(delegate(vmEntryPharRestPOS1.StockItemList x, vmEntryPharRestPOS1.StockItemList y)
                    {
                        return x.invcode.CompareTo(y.invcode);
                    });
                    if (iqty > 0)
                    {
                        foreach (var item1c in item1b)
                        {
                            if (iqty <= item1c.stkqty)
                            {
                                ListViewItemTable1a.Add(new vmEntryPharRestPOS1.ListViewItemTable()
                                {
                                    trsl = "",
                                    invcode = item1c.invcode,
                                    rsircode = item1c.sircode,
                                    trdesc = item1c.sirdesc,
                                    mfgid = item1c.mfgid,
                                    mfgcomnam = item1c.mfgcomnam,
                                    invqty = iqty,
                                    truid = item1c.siruid,
                                    trunit = item1c.sirunit,
                                    invrate = item1c.saleprice,
                                    invamt = iqty * item1c.saleprice,
                                    invdisamt = disam1 * iqty,
                                    invnetamt = (iqty * item1c.saleprice) - (disam1 * iqty),
                                    invvatper = 0.00m,
                                    invvatamt = 0.00m,
                                    invrmrk = "",
                                    batchno = "",
                                    mfgdat = DateTime.Parse("01-Jan-1900"),
                                    expdat = DateTime.Parse("01-Jan-1900"),
                                    mfgvisible = "Collapsed",
                                    rmrkvisible = "Collapsed"
                                });
                                break;
                            }
                            else
                            {
                                ListViewItemTable1a.Add(new vmEntryPharRestPOS1.ListViewItemTable()
                                {
                                    trsl = "",
                                    invcode = item1c.invcode,
                                    rsircode = item1c.sircode,
                                    trdesc = item1c.sirdesc,
                                    mfgid = item1c.mfgid,
                                    mfgcomnam = item1c.mfgcomnam,
                                    invqty = item1c.stkqty,
                                    truid = item1c.siruid,
                                    trunit = item1c.sirunit,
                                    invrate = item1c.saleprice,
                                    invamt = item1c.stkqty * item1c.saleprice,
                                    invdisamt = disam1 * item1c.stkqty,
                                    invnetamt = (item1c.stkqty * item1c.saleprice) - (disam1 * item1c.stkqty),
                                    invvatper = 0.00m,
                                    invvatamt = 0.00m,
                                    invrmrk = "", 
                                    batchno = "",
                                    mfgdat = DateTime.Parse("01-Jan-1900"),
                                    expdat = DateTime.Parse("01-Jan-1900"),
                                    mfgvisible = "Collapsed",
                                    rmrkvisible = "Collapsed"
                                });
                                iqty = (iqty - item1c.stkqty);
                            }
                        }
                    }
                }
            }

            string CustID1 = "000000000000";
            string EditTransID1 = (this.EditDs != null ? this.lblinvNo.Tag.ToString() : "");

            string vouno1 = "000000000000000000"; // this.lblVouNo1.Tag.ToString().Trim();
            string vouno2 = "000000000000000000"; // this.lblVouNo2.Tag.ToString().Trim();
            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtSalesDat.Text), EditMemoNum1: EditTransID1,
                mcode: "MSI", cbSectCode: cbSectCode1, CustID1: CustID1, InvByID1: WpfProcessAccess.SignedInUserList[0].hccode, PayType1: "CASH",
                MemoRef1: this.txtinvRef.Text.Trim(), MemoRefDate1: DateTime.Parse(this.xctk_dtSalesDat.Text), delivartime1: DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"),
                        MemoNar1: this.txtinvNar.Text.Trim(), ListViewItemTable1a: ListViewItemTable1a, PayType: "By Cash", DueAmt: this.lblDueAmt.Content.ToString(), PaidAmt: this.txtRcvAmt.Text.Trim(), 
                        vounum1: vouno1, vounum2 : vouno2, InvStatus: "A",
                        _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID , 
                        _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            //String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamUpdateMSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, ds1, EditTransID1);           
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;
            decimal tnetam = ListViewItemTable1a.Sum(x => x.invnetamt);
            string memonum1 = ds2.Tables[0].Rows[0]["memonum1"].ToString();
            string memonum = ds2.Tables[0].Rows[0]["memonum"].ToString();
            this.lblinvNo.Content = memonum1;
            this.lblinvNo.Tag = memonum;

            //Rptlist2.Add(new HmsEntityCommercial.InvoiceTransList() { invdat1 = DateTime.Parse(this.xctk_dtSalesDat.Text).ToString("dd-MMM-yyy") });

            //int i = 1;
            //foreach (var item1 in ListViewItemTable1)
            //{
            //    Rptlist1.Add(new HmsEntityCommercial.PhSalesInvoice01()
            //    {
            //        slnum = i,
            //        comcod = WpfProcessAccess.CompInfList[0].comcod,
            //        idisam = item1.invdisamt,
            //        inetam = item1.invnetamt,
            //        invno = memonum,
            //        invqty = item1.invqty,
            //        itmam = item1.invamt,
            //        itmrat = item1.invrate,
            //        rsircode = item1.rsircode,
            //        sirdesc = item1.trdesc,
            //        sirunit = item1.trunit
            //    });
            //    i++;
            //}

            decimal dueAmt = decimal.Parse("0" + this.lblDueAmt.Content.ToString());
            DateTime MemoDate1 = DateTime.Parse(this.xctk_dtSalesDat.Text);
            string invref1 = this.txtinvRef.Text.Trim();
            string invnar1 = this.txtinvNar.Text.Trim();
            this.cmbPrevInvList.Items.Insert(0, new ComboBoxItem()
            {
                Content = memonum1.Substring(3, 2) + "-" + memonum1.Substring(11, 5) + " [Tk. " + tnetam.ToString("#,##0.00") +
                    (dueAmt <= 0 ? "" : ", Due: Tk. " + dueAmt.ToString("#,##0.00")) + ", " +
                    MemoDate1.ToString("dd.MM.yyyy") + "]" + (invref1.Trim().Length > 0 ? ", " + invref1.Trim() : "") +
                        (invnar1.Trim().Length > 0 ? ", " + invnar1.Trim() : ""),
                Tag = memonum
            });

            this.btnUpdate.IsEnabled = false;
            this.btnAddRecord.Visibility = Visibility.Hidden;
            if (this.ChkPrintDirect.IsChecked == true)
            {
                this.btnPrint2_Click(null, null);
            }
            else
            {
                this.btnPrint2.Visibility = Visibility.Visible;
            }
        }

        //private string GetInvDate(string invcod1)
        //{
        //    string InvDate="";
        //    string[] m1 = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
        //    string[] m2 = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        //    string[] d1 = { "", "A",  "B",  "C",  "D",  "E",  "F",  "G",  "H",  "I",  "J",  "K",  "L",  "M",  "N",  "O",  "P",  "Q",  "R",  "S",  "T",  "U",  "V",  "W",  "X",  "Y",  "Z",  "1",  "2",  "3",  "4",  "5" };
        //    string[] d2 = { "", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
        //    string year1 = "20" + invcod1.Substring(0, 2);
        //    string month1 = invcod1.Substring(2, 1);
        //    string day1 = invcod1.Substring(3, 1);

        //    for(int i=1; i<=12; i++)
        //    {
        //        if(month1==m1[i])
        //        {
        //            month1 = m2[i];
        //            break;
        //        }
        //    }
        //    for (int j = 1; j <= 31; j++)
        //    {
        //        if (day1 == d1[j])
        //        {
        //            day1 = d2[j];
        //            break;
        //        }
        //    }
        //    InvDate = day1 + "-" + month1 + "-" + year1;
        //    return InvDate;
        //}
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AtxtItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtItemCode.Value.Length == 0)
                return;

            if (this.AtxtItemCode.Text.Trim().Length == 0)
                return;

            string rsircode1 = this.AtxtItemCode.Value;

            if (SearchWithInvCode == true)
            {
                var lvi1 = this.InvStockItemList.Find(x => x.sircode + x.invcode == rsircode1);
                this.txtUID.Text = lvi1.siruid;
                this.lblUnit1.Content = lvi1.sirunit;
                this.lblMfgByName.Content = lvi1.mfgcomnam;
                this.lblSalesRate.Content = lvi1.saleprice.ToString("#,##0.00");
            }
            else
            {
                var lvi1 = this.InvStockItemSumList.Find(x => x.sircode + "000000000000" == rsircode1);
                this.txtUID.Text = lvi1.siruid;
                this.lblUnit1.Content = lvi1.sirunit;
                this.lblMfgByName.Content = lvi1.mfgcomnam;
                this.lblSalesRate.Content = lvi1.saleprice.ToString("#,##0.00");
            }
            this.txtinvQty.Text = "";
            this.lblAmountShow.Content = "";// lvi1.saleprice.ToString("#,##0.00"); 
        }

        private void btnRecal_Click(object sender, RoutedEventArgs e)
        {
            this.RecalCulateInfo();
        }
        private void RecalCulateInfo()
        {
            this.dgvMemo.ItemsSource = null;
            this.ListViewItemTable1 = this.ListViewItemTable1.FindAll(x => x.invqty > 0 && x.invamt > 0);
            double disper1 = double.Parse("0" + this.txtDisPer.Text.Trim());
            this.txtDisPer.Text = disper1.ToString("#,##0.00");
            int i = 1;
            foreach (var item1a in this.ListViewItemTable1)
            {
                item1a.trsl = i.ToString().Trim() + ".";
                item1a.invamt = Math.Round(item1a.invqty * item1a.invrate, 2);
                if (this.ckhDiscountPer.IsChecked == true)
                    item1a.invdisamt = Convert.ToDecimal(Math.Round((Convert.ToDouble(item1a.invamt) * disper1 / 100.00), 0));

                item1a.invnetamt = item1a.invamt - item1a.invdisamt;
                i++;
            }
            decimal TotalAmt1 = this.ListViewItemTable1.Sum(x => x.invamt);
            decimal TDiscAmt1 = this.ListViewItemTable1.Sum(x => x.invdisamt);
            decimal TotalNetAmt1 = this.ListViewItemTable1.Sum(x => x.invnetamt);
            decimal RevAmt1 = decimal.Parse("0" + this.txtRcvAmt.Text.Trim());

            this.lblTotalAmt.Content = TotalAmt1.ToString("#,##0.00;(#,##0.00); ");
            this.lblTDiscAmt.Content = TDiscAmt1.ToString("#,##0.00;(#,##0.00); ");
            this.lblTotalNetAmt.Content = TotalNetAmt1.ToString("#,##0.00;(#,##0.00); ");


            this.lblPaidAmt.Content = (TotalNetAmt1 > RevAmt1 ? RevAmt1.ToString("#,##0.00;(#,##0.00); ") : TotalNetAmt1.ToString("#,##0.00;(#,##0.00); "));
            this.lblDueAmt.Content = (TotalNetAmt1 > RevAmt1 ? (TotalNetAmt1 - RevAmt1).ToString("#,##0.00;(#,##0.00); ") : "");
            this.lblReturnAmt.Content = (RevAmt1 > TotalNetAmt1 ? (RevAmt1 - TotalNetAmt1).ToString("#,##0.00;(#,##0.00); ") : "");
            this.txtRcvAmt.Text = RevAmt1.ToString("#,##0.00;(#,##0.00); ");
            this.dgvMemo.ItemsSource = this.ListViewItemTable1;

        }

        private void txtRcvAmt_LostFocus(object sender, RoutedEventArgs e)
        {
            this.RecalCulateInfo();
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            string MemoNum = ((ComboBoxItem)this.cmbPrevInvList.SelectedItem).Tag.ToString();
            string PrnOpt1 = (this.ChkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
            this.ViewPrintMemo(MemoNum, PrnOpt1);

        }
        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            string MemoNum = this.lblinvNo.Tag.ToString();
            string PrnOpt1 = (this.ChkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
            this.ViewPrintMemo(MemoNum, PrnOpt1);           
        }
        private void ViewPrintMemo(string memoNum = "XXXXXXXX", string ViewPrint = "View")
        {
            //string memoNum = ((ComboBoxItem)this.cmbPrevInvList.SelectedItem).Tag.ToString();
            LocalReport rpt1 = null;
            string WindowTitle1 = "";
            var pap1 = vm2.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            //var list3 = WpfProcessAccess.GetRptGenInfo(InputSource: "Test Input Source\n");
            var list3 = WpfProcessAccess.GetRptGenInfo();
            var list1 = ds1.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
            var list2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
            // var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();

            rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesInv01", list1, list2, list3);
            WindowTitle1 = "Sales Memo";

            if (ViewPrint == "View")
            {
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            else if (ViewPrint == "DirectPrint")
            {
                RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                DirectPrint1.PrintReport(rpt1);
                DirectPrint1.Dispose();
            }
        }
        private void ChkDuecollection_Checked(object sender, RoutedEventArgs e)
        {
            this.xctk_dtDueInvDat1.Value = DateTime.Today.AddDays(-15);
            this.xctk_dtDueInvDat2.Value = DateTime.Today;
            this.xctk_dtDuePayDat1.Value = DateTime.Today;
            this.stkDueCol.Visibility = Visibility.Hidden;

            this.txtDueRef.Text = "";
            this.txtDueColAmt1.Text = "";
            this.lblDueBalAmt.Content = "";
            this.dgDueMemo.ItemsSource = null;

            this.cmbDuesInvList.Items.Clear();

            foreach (ComboBoxItem citem1 in this.cmbPrevInvList.Items)
            {
                if (citem1.Content.ToString().Contains("Due"))
                    this.cmbDuesInvList.Items.Add(new ComboBoxItem() { Content = citem1.Content, Tag = citem1.Tag });
            }
            this.cmbDuesInvList.SelectedIndex = 0;
        }
        private void btnFindDueList1_Click(object sender, RoutedEventArgs e)
        {
            string FrmDate1 = this.xctk_dtDueInvDat1.Text.Trim();
            string ToDate1 = this.xctk_dtDueInvDat2.Text.Trim();

            this.InvList.Clear();
            this.InvList = PreviousMemoList(FrmDate1, ToDate1);
            this.InvList = InvList.FindAll(x => x.invno.Substring(0, 3) == "MSI" && x.dueam > 0);
            if (this.InvList == null)
                return;
            this.cmbDuesInvList.Items.Clear();

            foreach (var item1 in InvList)
            {
                this.cmbDuesInvList.Items.Add(new ComboBoxItem()
                {
                    Content = item1.invno1.Substring(3, 2) + "-" + item1.invno1.Substring(11, 5) + " [Tk. " + item1.billam.ToString("#,##0.00") +
                    (item1.dueam <= 0 ? "" : ", Due: Tk. " + item1.dueam.ToString("#,##0.00")) + ", " +
                    item1.invdat.ToString("dd.MM.yyyy") + "] " + (item1.invref.Trim().Length > 0 ? ", " + item1.invref.Trim() : "") +
                    (item1.invnar.Trim().Length > 0 ? ", " + item1.invnar.Trim() : ""),
                    Tag = item1.invno
                });
            }
            this.cmbDuesInvList.SelectedIndex = 0;

        }

        private void btnShowDueMemo_Click(object sender, RoutedEventArgs e)
        {
            this.btnUpdateDueCol1.IsEnabled = true;
            this.txtDueColAmt1.Text = "";
            this.lblDueBalAmt.Visibility = Visibility.Hidden;
            this.dgDueMemo.ItemsSource = null;
            string memoNum = ((ComboBoxItem)this.cmbDuesInvList.SelectedItem).Tag.ToString();
            var pap1 = vm2.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (double.Parse(ds1.Tables[0].Rows[0]["dueam"].ToString()) == 0)
            {
                System.Windows.MessageBox.Show("Dues amount already recovered. Please try with another memo", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                    MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            LstDueMemo1 = ds1.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
            //var list2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
            LstDueMemoCol1 = ds1.Tables[2].DataTableToList<vmEntryPharRestPOS1.PhSalesCollMemos01>();
            this.lblDueInvDate1.Content = "Date : " + ds1.Tables[0].Rows[0]["invdat1"].ToString();
            this.lblDueInvDate1.Tag = ds1.Tables[0].Rows[0]["invdat1"].ToString();
            this.lblDueInvNo1.Content = "Invoice No : " + ds1.Tables[0].Rows[0]["invno1"].ToString();
            this.lblDueInvNo1.Tag = memoNum;

            this.lblDueAmtDes1.Content = "Due = Tk. " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["dueam"]).ToString("#,##0.00") +
                                         (Convert.ToDecimal(ds1.Tables[0].Rows[0]["collam"]) == 0 ? "" : ", Paid = Tk. " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["collam"]).ToString("#,##0.00")) +
                                         ", Bill = Tk. " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["billam"]).ToString("#,##0.00") +
                                         (Convert.ToDecimal(ds1.Tables[0].Rows[0]["tdisam"]) == 0 ? "" : ", Discount = Tk. " + Convert.ToDecimal(ds1.Tables[0].Rows[0]["tdisam"]).ToString("#,##0.00")) +
                                         ", Total: Tk. ";

            this.lblDueAmtDes1.Tag = ds1.Tables[0].Rows[0]["dueam"].ToString();
            this.lblDueBillGrossAmt.Content = Convert.ToDecimal(ds1.Tables[0].Rows[0]["totslam"]).ToString("#,##0.00");
            this.lblDueBillGrossAmt.Tag = ds1.Tables[0].Rows[0]["billam"].ToString();
            this.dgDueMemo.ItemsSource = LstDueMemo1;
            this.dgDueCollMemo.ItemsSource = LstDueMemoCol1;
            this.lblDueInvRef1.Content = "Ref/Cell.: " + ds1.Tables[0].Rows[0]["invref"].ToString().Trim();
            this.txtbDueInvNar1.Text = "Remarks : " + ds1.Tables[0].Rows[0]["invnar"].ToString().Trim(); ;
            this.stkDueCol.Visibility = Visibility.Visible;
        }

        private void txtDueColAmt1_LostFocus(object sender, RoutedEventArgs e)
        {
            this.lblDueBalAmt.Visibility = Visibility.Visible;
            decimal DueAmt1 = decimal.Parse("0" + this.lblDueAmtDes1.Tag.ToString());
            decimal PaidAmt1 = decimal.Parse("0" + this.txtDueColAmt1.Text.Trim());
            this.lblDueBalAmt.Content = (DueAmt1 == PaidAmt1 ? "Full amount paid. Dues clear" : ((DueAmt1 < PaidAmt1) ? "Excess collection is not allowed" : "Balance Due : Tk. " + (DueAmt1 - PaidAmt1).ToString("#,##0.00")));
        }

        private void btnUpdateDueCol1_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            string memoDate = this.lblDueInvDate1.Tag.ToString();
            string colDate = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");// this.xctk_dtDuePayDat1.Text;
            string memoNum = this.lblDueInvNo1.Tag.ToString();
            decimal DueAmt1 = decimal.Parse("0" + this.lblDueAmtDes1.Tag.ToString());
            decimal PaidAmt1 = decimal.Parse("0" + this.txtDueColAmt1.Text.Trim());

            if (PaidAmt1 == 0 || PaidAmt1 > DueAmt1)
            {
                System.Windows.MessageBox.Show("Collection amount must be same as due amount", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                    MessageBoxImage.Stop, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly);
                this.txtDueColAmt1.Focus();
                return;
            }

            string CollNote = "Bill Ammount = " + this.lblDueBillGrossAmt.Content.ToString() + ", Prev. Due = " + DueAmt1.ToString("#,##0.00") + ", Paid = " + PaidAmt1.ToString("#,##0.00") + (DueAmt1 > PaidAmt1 ? ", Balance Due = " + (DueAmt1 - PaidAmt1).ToString("#,##0.00") : "");

            string vouno1 = "000000000000000000";// this.lblVouNo.Tag.ToString().Trim();
            var pap1 = vm1.SetParamUpdateMSalesDueColl(WpfProcessAccess.CompInfList[0].comcod, InvNum1: memoNum, InvDate1: memoDate, DueColDate1: colDate, DueColAmt1: PaidAmt1.ToString(), CollNote1: CollNote, vounum1: vouno1,
                        _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.btnUpdateDueCol1.IsEnabled = false;
            this.dgDueCollMemo.ItemsSource = null; ;
            int sl1 = LstDueMemoCol1.Count();
            this.LstDueMemoCol1.Add(new vmEntryPharRestPOS1.PhSalesCollMemos01() { slnum = sl1 + 1, bilcolid = "aa", bilcoldat = DateTime.Parse(colDate), bilcolam = PaidAmt1, bcnote = CollNote, tokenid="XXXXXX" });
            //this.LstDueMemoCol1.Insert(0, new vmEntryPharRestPOS1.PhSalesCollMemos01() {slnum = sl1+1, bilcolid  = "aa", bilcoldat = DateTime.Parse(colDate), bilcolam = PaidAmt1, bcnote = CollNote, tokenid="XXXXXX"});
            this.dgDueCollMemo.ItemsSource = LstDueMemoCol1;

            //lvAc.ScrollIntoView(lvAc.Items[z]);
            //lvAc.SelectedIndex = z;

            this.dgDueCollMemo.ScrollIntoView(this.dgDueCollMemo.Items[sl1]);
            //System.Windows.MessageBox.Show("Update Successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

        }

        private void autoItemSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);

        }

        private ObservableCollection<vmEntryPharRestPOS1.StockItemSumList> GetItemSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<vmEntryPharRestPOS1.StockItemSumList>(
                this.InvStockItemSumList.Where((x, match) => x.sirdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoItemSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoItemSearch.ContextMenu.IsOpen = true;
        }

        private void autoItemSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if(this.autoItemSearch.SelectedValue == null)
                return;
            
            string itmcod1 = this.autoItemSearch.SelectedValue.ToString().Trim();
            var itm1 = this.InvStockItemSumList.Find(x => x.sircode == itmcod1);
            this.lblUnit1.Content = itm1.sirunit.Trim();
            this.lblSalesRate.Content = itm1.saleprice.ToString("#,##0.00");
        }
    }
}
