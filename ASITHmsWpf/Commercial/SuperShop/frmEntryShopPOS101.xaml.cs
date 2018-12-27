using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Commercial;
using ASITHmsRpt2Inventory;
using ASITHmsRpt4Commercial;
using Microsoft.Reporting.WinForms;
using System.ComponentModel;
using ASITHmsViewMan.General;
using ASITHmsViewMan.Accounting;
using ASITHmsRpt1GenAcc.Accounting;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASITHmsWpf.Commercial.SuperShop
{
    /// <summary>
    /// Interaction logic for frmEntryShopPOS101.xaml    
    /// </summary>
    public partial class frmEntryShopPOS101 : UserControl
    {
        private int PrintCount = 0;
        private bool FrmInitialized = false;
        private string CalcObjName = "NoName";
        private bool MemoSaved = false;
        private DataSet EditDs;

        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemMainGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();
        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();
        private List<vmEntryPharRestPOS1.RetSaleItem> RetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();
        private List<vmEntryPharRestPOS1.RetSaleItem> ShortRetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();
        
        private List<HmsEntityCommercial.InvoiceTransList> TransInvList = new List<HmsEntityCommercial.InvoiceTransList>();

        private List<HmsEntityGeneral.SirInfCodeBook> RegCustList = new List<HmsEntityGeneral.SirInfCodeBook>();                    // REgistered Customer List from Resource Code Book
        private List<vmEntryPharRestPOS1.ItemCustDetailsInfo> RegCustDetailsList = new List<vmEntryPharRestPOS1.ItemCustDetailsInfo>();


        private List<vmEntryPharRestPOS1.ListViewItemTableDetails> ListViewItemTable1 = new List<vmEntryPharRestPOS1.ListViewItemTableDetails>();

        private vmEntryPharRestPOS1 vm1 = new vmEntryPharRestPOS1();
        private vmReportPharRestPOS1 vm2 = new vmReportPharRestPOS1();

        public frmEntryShopPOS101()
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
                this.stkpFreeItems.Visibility = Visibility.Collapsed;
                this.btnAddItem.Visibility = Visibility.Hidden;
                this.dgvSalesMemo.Height = 430;
                this.btnNewShow_Click(null, null);
                this.txtItemCode.Focus();

            }
        }

        private void ActivateAuthObjects()
        {
            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryShopPOS101_chkAllowDraft") == null)
            {
                this.chkAllowDraft.IsChecked = false;
                this.stkpDraftOption.Visibility = Visibility.Hidden;
            }

            ////try
            ////{
            ////    if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS1_frmEntryGenTrPOS101_chkDateBlocked") == null)
            ////        this.chkDateBlocked.Visibility = Visibility.Hidden;

            ////    if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS1_frmEntryGenTrPOS101_chkAutoSaveAc") == null)
            ////        this.chkAutoSaveAc.IsChecked = false;

            ////    if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS1_frmEntryGenTrPOS101_chkVerifyStock") == null)
            ////        this.chkVerifyStock.IsChecked = false;

            ////    if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS1_frmEntryGenTrPOS101_stkpAccVoucher0") == null)
            ////        this.stkpAccVoucher0.Visibility = Visibility.Collapsed;



            ////    if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS1_frmEntryGenTrPOS101_btnFilter1") == null)
            ////    {
            ////        this.btnFilter1.Visibility = Visibility.Hidden;
            ////        this.stkpFilter1.Visibility = Visibility.Collapsed;
            ////    }

            ////    if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS1_frmEntryGenTrPOS101_btnEdit1") == null)
            ////        this.btnEdit1.Visibility = Visibility.Hidden;

            ////    if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS1_frmEntryGenTrPOS101_btnDelete1") == null)
            ////    {
            ////        this.btnDelete1.Visibility = Visibility.Hidden;
            ////        //this.btnDeleteTrans.Visibility = Visibility.Hidden;
            ////    }
            ////}
            ////catch (Exception exp)
            ////{
            ////    System.Windows.MessageBox.Show("SSI-2.08: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            ////}
        }

        private void Objects_On_Init()
        {
            try
            {
                this.GetSectionList();
                this.GetRetailItemList();

                ////this.dgvMemo.ItemsSource = this.ListViewItemTable1;
                ////this.xctk_dtpSrchDat1.Value = DateTime.Today.AddDays(-3);
                ////this.xctk_dtpSrchDat2.Value = DateTime.Today;
                ////this.btnFilter1_Click(null, null);
                ////this.GetCustomerList();

                if (WpfProcessAccess.AccCodeList == null)
                    WpfProcessAccess.GetAccCodeList();

                ////this.AcCodeList1 = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 8) == "31010001" || x.actcode.Substring(0, 2) == "18" ||
                ////              x.actcode.Substring(0, 4) == "1901") && (x.actcode.Substring(8, 4) != "0000")).OrderBy(x => x.actcode).ToList();

                ////this.cmbPayType.SelectedIndex = 2;
                ////this.cmbPayType_SelectionChanged(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void GetSectionList()
        {
            try
            {
                this.cmbSectCod.Items.Clear();
                var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
                foreach (var itemd1 in deptList1)
                {
                    if (itemd1.sectname.ToUpper().Contains("STORE"))
                    {
                        this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                    }
                }
                //this.cmbSectCod.IsEnabled = (this.cmbSectCod.Items.Count == 1 ? false : true);
                this.cmbSectCod.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }
        private void GetRetailItemList()
        {
            try
            {
                this.RetSaleItemList.Clear();
                this.ShortRetSaleItemList.Clear();
                //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4171", reqmfginf: "WITHOUTMFGINFO");
                //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4521", reqmfginf: "WITHMFGINFO");
                //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "0[14]51", reqmfginf: "WITHOUTMFGINFO");
                var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4171", reqmfginf: "WITHOUTMFGINFO");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
                if (ds1 == null)
                    return;

                this.RetSaleItemGroupList = ds1.Tables[1].DataTableToList<vmEntryPharRestPOS1.RetSaleItemGroup>();
                DataRow[] dr1 = ds1.Tables[0].Select();
                DataRow[] dr2 = ds1.Tables[1].Select();
                DataRow[] dr3 = ds1.Tables[2].Select();
                string rmrkvisibl1 = "Collapsed";
                //for (int i = 0; i < dr1.Length; i++)

                //public class RetSaleItem  {sircode, sirdesc, costprice, saleprice, refscomp, salvatp, sirtype, sirunit, sirunit2, siruconf, msircode, msirdesc, srchdesc, sircode1, mfgid, mfgcomnam, mfgvisible, rmrkvisible, sirimage

                foreach (DataRow row1 in dr1)
                {
                    var itm1 = new vmEntryPharRestPOS1.RetSaleItem(row1["sircode"].ToString(), row1["sirdesc"].ToString(), Convert.ToDecimal(row1["costprice"]),
                        Convert.ToDecimal(row1["saleprice"]), Convert.ToDecimal(row1["refscomp"]), Convert.ToDecimal(row1["salvatp"]), row1["sirtype"].ToString(), row1["sirunit"].ToString(), row1["sirunit2"].ToString(),
                        row1["sirunit3"].ToString(), decimal.Parse("0" + row1["siruconf"].ToString()), decimal.Parse("0" + row1["siruconf3"].ToString()), row1["msircode"].ToString(), row1["msirdesc"].ToString(),
                        row1["msirdesc"].ToString().Trim() + " - " + row1["sirdesc"].ToString(), row1["sircode"].ToString().Substring(6), row1["mfgid"].ToString(), row1["mfgcomnam"].ToString(),
                        (row1["mfgcomnam"].ToString().Trim().Length > 0 ? "Visible" : "Collapsed"), rmrkvisibl1, null);

                    itm1.sirunit2 = (itm1.sirunit2.Trim().Length == 0 ? itm1.sirunit : itm1.sirunit2);
                    itm1.sirunit3 = (itm1.sirunit3.Trim().Length == 0 ? itm1.sirunit : itm1.sirunit3);
                    itm1.siruconf = (itm1.siruconf == 0 ? 1 : itm1.siruconf);
                    itm1.siruconf3 = (itm1.siruconf3 == 0 ? 1 : itm1.siruconf3);


                    if (itm1.saleprice > 0)
                        this.RetSaleItemList.Add(itm1);

                    this.ShortRetSaleItemList = this.RetSaleItemList.ToList();

                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void CleanUpScreen()
        {
            try
            {               
                this.stkpPrevTrans.Visibility = Visibility.Collapsed;
                this.stkpDataEntry.Visibility = Visibility.Visible;
                this.txtItemCode0.Visibility = Visibility.Hidden;
                this.chkConfrmSave.Visibility = Visibility.Hidden;
                this.gridCalc1.Visibility = Visibility.Collapsed;
                this.stkpPrevTrans.Visibility = Visibility.Collapsed;
                this.GridItemList.Visibility = Visibility.Collapsed;
                this.txtblEditMode.Visibility = Visibility.Hidden;
                this.PrintCount = 0;
                this.EditDs = null;

                this.txtPaidAmt.IsEnabled = true;
                this.cmbPayType.SelectedIndex = 0;
                this.btnUpdateTrans.IsEnabled = false;
                this.btnAddItem.IsEnabled = true;
                this.dgvSalesMemo.IsEnabled = true;
                this.MemoSaved = false;

                this.xctk_dtpSalDat.Minimum = DateTime.Today.AddDays(-365 * 3);
                this.xctk_dtpSalDat.Maximum = DateTime.Today.AddDays(365 * 2);
                this.xctk_dtpSalDat.Value = DateTime.Today;

                this.xctk_dtpSrchDat1.Value = DateTime.Today;
                this.xctk_dtpSrchDat2.Value = DateTime.Today;

                this.lstItem.Items.Clear();
                ////this.txtbCustName.Text = "";
                ////this.txtbCustAddress.Text = "";
                ////this.txtbCustPhone.Text = "";

                this.lblGrandTotal.Content = " 0.00 ";
                ////this.lblNetTotal.Content = " -  ";
                this.lblChangeCash.Content = " 0.00  ";
                ////this.lblNetBalance.Content = " -  ";
                this.txtPaidAmt.Text = " 0.00 ";
                this.lblVATTotal.Content = " 0.00 ";
                this.lblTotalBill.Content = " 0.00 ";

                this.btnUpdateTrans.Tag = "New";
                this.ListViewItemTable1.Clear();
                this.dgvSalesMemo.Items.Refresh();
                this.txtMemoNar.Text = "";
                string lastid1 = this.GetLastTransID();
                this.txtTransID.Text = (lastid1 == "SSI000000000000000" ? "" : "");
                this.txtTransID.Tag = lastid1;
                this.txtItemCode.Focus();
                this.TransInvList.Clear();
                ////this.SetStockBackGround();
                ////this.txtSrchInvNo.Text = "";
                ////this.GetStockItemList();
                ////this.btnClearRecord_Click(null, null);

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private string GetLastTransID()
        {
            return "SSI000000000000000";
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
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            // this.ListViewItemTable1;
            try
            {               
                ////this.GridItemList.Visibility = Visibility.Collapsed;
                string srchTxt1a = this.txtItemCode.Text.Trim();
                srchTxt1a = "417100" + srchTxt1a;
                this.txtItemCode.Text = "";
                this.AddProductItem(srchVal1: srchTxt1a);
                //this.btnClearRecord_Click(null, null);
                this.txtItemCode0.Visibility = Visibility.Visible;
                this.txtItemCode0.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void AddProductItem(string srchVal1 = "")
        {
            try
            {
                if (this.MemoSaved == true)
                    return;

                if (srchVal1.Length == 0)
                    return;

                int rowidx1 = this.dgvSalesMemo.SelectedIndex;

                decimal itmqty1 = 1.00m;
                decimal itmRate1 = 1.00m;
                decimal itmAmt1 = itmqty1 * itmRate1;

                string custid1 = "000000000000";
                string reptsl1 = "000";

               //// srchVal1 = this.RetSaleItemList[0].sircode; // For temporary 

                var RetailItemList1 = this.RetSaleItemList.FindAll(x => x.sircode == srchVal1).Take(10).ToList();

                foreach (var item in RetailItemList1)
                {
                    #region MyRegion
                    int serialno1 = this.ListViewItemTable1.Count + 1;
                    var ListViewItemTable1a = this.ListViewItemTable1.FindAll(x => x.rsircode == item.sircode);
                    if (ListViewItemTable1a.Count > 0)
                    {
                        ListViewItemTable1a[0].invqty = ListViewItemTable1a[0].invqty + itmqty1;
                        ListViewItemTable1a[0].trunit = item.sirunit;
                        ListViewItemTable1a[0].invamt = item.saleprice * ListViewItemTable1a[0].invqty;
                        ListViewItemTable1a[0].invdisamt = 0;
                        ListViewItemTable1a[0].invnetamt = item.saleprice * ListViewItemTable1a[0].invqty;
                        ListViewItemTable1a[0].invvatper = item.salvatp;
                        ListViewItemTable1a[0].invvatamt = (item.salvatp / 100.00m * (item.saleprice * ListViewItemTable1a[0].invqty));                      
                    }
                    else
                    {
                        var reptsl1a = (this.ListViewItemTable1.Count == 0 ? "000" : this.ListViewItemTable1.Max(x => x.reptsl));
                        reptsl1 = (int.Parse(reptsl1a) + 1).ToString("000");
                        var item1a = new vmEntryPharRestPOS1.ListViewItemTableDetails()
                        {
                            trsl = serialno1.ToString() + ".",
                            invcode = item.sircode,
                            rsircode = item.sircode,
                            trdesc = item.sirdesc.Trim(),
                            mfgid = item.mfgid,
                            mfgcomnam = item.mfgcomnam.Trim(),
                            invqty = itmqty1,
                            truid = "",
                            trunit = item.sirunit,
                            invrate = item.saleprice,
                            invamt = item.saleprice * itmqty1,
                            invdisamt = 0,
                            invnetamt = item.saleprice * itmqty1,
                            invvatper = item.salvatp,
                            invvatamt = (item.salvatp / 100.00m * (item.saleprice * itmqty1)),
                            invrmrk = "",
                            batchno = item.sircode.Substring(6) + ": " + item.sirdesc.Trim(),
                            mfgdat = DateTime.Today,
                            expdat = DateTime.Today.AddDays(7),
                            mfgvisible = item.mfgvisible,
                            rmrkvisible = item.rmrkvisible,
                            invqty2 = 0.00m,
                            invqty3 = 0.00m,
                            invrate2 = 0.00m,
                            invrate3 = 0.00m,
                            invweight = 0.00m,
                            trunit2 = item.sirunit2,
                            trunit3 = item.sirunit3,
                            siruconf = item.siruconf,
                            reptsl = reptsl1
                        };                     
                        this.ListViewItemTable1.Add(item1a);
                        rowidx1 = this.ListViewItemTable1.Count - 1;
                    }
                    #endregion
                }

             
                this.btnUpdateTrans.IsEnabled = true;
                
                this.btnTotal_Click(null, null);
                this.txtPaidAmt.Text = this.lblTotalBill.Content.ToString();
                this.btnTotal_Click(null, null);
                this.dgvSalesMemo.SelectedIndex = rowidx1;
                var item22 = this.ListViewItemTable1.FindAll(x => x.invcode == srchVal1 && x.reptsl == reptsl1);
                if (item22.Count > 0)
                {
                    this.dgvSalesMemo.ScrollIntoView(item22[0]);
                }
                this.GridItemList.Visibility = Visibility.Collapsed;

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.23: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

      

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                btnAddItem_Click(null, null);
        }

        private void btnNav_Click(object sender, RoutedEventArgs e)
        {

            if (this.dgvSalesMemo.Items.Count == 0)
                return;

            if (this.dgvSalesMemo.SelectedIndex < 0)
                this.dgvSalesMemo.SelectedIndex = 0;
            string ActtionName = ((Button)sender).Name.ToString().Trim();

            int index1 = this.dgvSalesMemo.SelectedIndex;
            if (ActtionName == "btnDelete")
            {

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to delete item\n" + this.ListViewItemTable1[index1].trsl + " " + this.ListViewItemTable1[index1].trdesc.Trim(),
                                    WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;
                this.ListViewItemTable1[index1].invqty = 0;
                this.btnTotal_Click(null, null);
                if (this.ListViewItemTable1.Count > 0)
                {
                    this.dgvSalesMemo.SelectedIndex = (this.ListViewItemTable1.Count <= index1 ? this.ListViewItemTable1.Count - 1 : index1);
                }
                return;
            }
            switch (ActtionName)
            {
                case "btnTop":
                    index1 = 0;
                    break;
                case "btnPrev":
                    index1 = this.dgvSalesMemo.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnNext":
                    index1 = this.dgvSalesMemo.SelectedIndex + 1;
                    if (index1 >= this.dgvSalesMemo.Items.Count)
                        index1 = this.dgvSalesMemo.Items.Count - 1;
                    break;
                case "btnBottom":
                    index1 = this.dgvSalesMemo.Items.Count - 1;
                    break;
            }
            this.dgvSalesMemo.SelectedIndex = index1;

            var item21 = (vmEntryPharRestPOS1.ListViewItemTableDetails)this.dgvSalesMemo.Items[index1];
            this.dgvSalesMemo.ScrollIntoView(item21);

        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.FrmInitialized == false)
                    return;

                if (this.MemoSaved == true)
                    return;

                this.ListViewItemTable1 = this.ListViewItemTable1.FindAll(x => x.invqty > 0);

                decimal DiscTotal1 = 0.00m; // decimal.Parse("0" + this.txtDiscAmt.Text.Trim());
                decimal Carring1 = 0.00m;    // "045100101001"
                decimal Labour1 = 0.00m;      // "045100102001"

                foreach (var item in this.ListViewItemTable1)
                    item.invamt = Math.Round(item.invrate * item.invqty, 6);


                decimal GrandTotal1 = Math.Round(this.ListViewItemTable1.Sum(x => x.invamt), 6);

                foreach (var item in this.ListViewItemTable1)
                {
                    item.invdisamt = (GrandTotal1 == 0 ? 0 : Math.Round(DiscTotal1 / GrandTotal1 * item.invamt, 6));
                    item.invnetamt = Math.Round(item.invamt - item.invdisamt, 6);
                    item.invvatamt = Math.Round(item.invvatper / 100.00m * item.invamt, 6);
                }

                decimal TotalQty = this.ListViewItemTable1.Sum(x => x.invqty);
                //0151049

                decimal TotalWeight = this.ListViewItemTable1.Sum(x => x.invweight);

                //decimal DiscTotal1 = this.ListViewItemTable1.Sum(x => x.invdisamt);
                decimal NetTotal = this.ListViewItemTable1.Sum(x => x.invnetamt); // Math.Round(ListViewItemTable1a.Sum(x => x.invnetamt), 0);
                decimal VATTotal = this.ListViewItemTable1.Sum(x => x.invvatamt);
                decimal TotalBill = GrandTotal1 - DiscTotal1 + VATTotal + Carring1 + Labour1;   // +decimal.Parse("0" + this.txtCCCharge.Text.Trim());              

                decimal RoundUp1 = Math.Round(TotalBill, 2) - Math.Round(TotalBill, 0);
                TotalBill = Math.Round(TotalBill, 0) + (RoundUp1 < 0.50m ? 0.00m : 1.00m);


                decimal TotalPaid = decimal.Parse("0" + this.txtPaidAmt.Text.Trim());           // this.ListViewItemTable1.Sum(x => x.colam) + decimal.Parse("0" + this.txtCCPaid.Text.Trim());
                decimal ChangeCash = ((TotalPaid < TotalBill) ? 0.00m : (TotalPaid - TotalBill));
                decimal BalanceBill = ((TotalBill < TotalPaid) ? 0.00m : (TotalBill - TotalPaid));

                //----Correction Request by Client ----------------
                // For Accounting Entry and Trading House ChangeCash must be alwais 0.00 So
                ChangeCash = (TotalPaid - TotalBill);
                BalanceBill = 0.00m; // (TotalBill - TotalPaid);
                //------------------------------------------------


                ////this.lblTotalQty.Content = TotalQty.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                ////this.lblTotalQty2.Content = TotalQty2.ToString("#,##0.00;(#,##0.00); - ");// " -  ";

                this.lblGrandTotal.Content = GrandTotal1.ToString("#,##0.00;(#,##0.00); 0.00");// " -  ";
                this.lblVATTotal.Content = VATTotal.ToString("#,##0.00;(#,##0.00); 0.00");// " -  ";
                this.lblRoundUpAmt.Content = RoundUp1.ToString("#,##0.00;(#,##0.00); 0.00");// " -  ";
                this.lblTotalBill.Content = TotalBill.ToString("#,##0.00;(#,##0.00); 0.00"); //TotalBill.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                ////this.lblNetBalance.Content = BalanceBill.ToString("#,##0;(#,##0); - ");// " -  ";            
                this.lblChangeCash.Content = ChangeCash.ToString("#,##0.00;(#,##0.00); 0.00");// " -  ";            

                int serialno1 = 1;
                foreach (var item in this.ListViewItemTable1)
                {
                    item.trsl = serialno1.ToString() + ".";
                    ++serialno1;
                }
                this.dgvSalesMemo.ItemsSource = this.ListViewItemTable1;
                this.dgvSalesMemo.Items.Refresh();


            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.20: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        

   

        private void btnNewShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (this.ListViewItemTable1.Count > 0 && this.btnUpdateTrans.IsEnabled == true)
                {
                    this.btnTotal_Click(null, null);
                }

                this.CleanUpScreen();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.14: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.MemoSaved == true)
                    return;
                
                this.btnTotal_Click(null, null);             

                string InvStatus1 = "A";
                string PayType1 = ((ComboBoxItem)this.cmbPayType.SelectedItem).Tag.ToString();
                string MemoNar1 = this.txtMemoNar.Text.Trim();
                string DueAmt1 = "0.00";// this.lblNetBalance.Content.ToString();
                string PaidAmt1 = this.lblTotalBill.Content.ToString().Trim();// this.txtPaidAmt.Text.Trim();

                if (this.chkConfrmSave.IsChecked == false)
                {
                    if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }

                string EditTransID1 = this.txtTransID.Tag.ToString();
                EditTransID1 = (EditTransID1 == "SSI000000000000000" ? "" : EditTransID1);

                string CustID1 = "000000000000";
                string CustName1 = "";

                var ListViewItemTable1s = new List<vmEntryPharRestPOS1.ListViewItemTable>();
                foreach (var item in this.ListViewItemTable1)
                {
                    ListViewItemTable1s.Add(new vmEntryPharRestPOS1.ListViewItemTable()
                    {
                        trsl = item.trsl,
                        invcode = item.invcode,
                        reptsl = item.reptsl,
                        rsircode = item.rsircode,
                        trdesc = item.trdesc,
                        mfgid = item.mfgid,
                        mfgcomnam = item.mfgcomnam,
                        invqty = item.invqty,
                        truid = item.truid,
                        trunit = item.trunit,
                        invrate = item.invrate,
                        invamt = item.invamt,
                        invdisamt = item.invdisamt,
                        invnetamt = item.invnetamt,
                        invvatper = item.invvatper,
                        invvatamt = item.invvatamt,
                        batchno = item.batchno,
                        invrmrk = item.invrmrk,
                        mfgdat = item.mfgdat,
                        expdat = item.expdat,
                        mfgvisible = item.mfgvisible,
                        rmrkvisible = item.mfgvisible
                    });
                }

                decimal Carring1 = 0.00m;   //decimal.Parse("0" + this.txtCarrAmt.Text.Trim());    // "045100101001"
                decimal Labour1 = 0.00m;    // decimal.Parse("0" + this.txtLabAmt.Text.Trim());      // "045100102001"

                if (Carring1 > 0)
                {
                    ListViewItemTable1s.Add(new vmEntryPharRestPOS1.ListViewItemTable()
                    {
                        trsl = "000",
                        invcode = "045100101001",
                        reptsl = "000",
                        rsircode = "045100101001",
                        trdesc = "",
                        mfgid = "",
                        mfgcomnam = "",
                        invqty = 1,
                        truid = "",
                        trunit = "",
                        invrate = Carring1,
                        invamt = Carring1,
                        invdisamt = 0.00m,
                        invnetamt = Carring1,
                        invvatper = 0.00m,
                        invvatamt = 0.00m,
                        batchno = "",
                        invrmrk = "",
                        mfgdat = DateTime.Today,
                        expdat = DateTime.Today,
                        mfgvisible = "",
                        rmrkvisible = ""
                    });
                }

                if (Labour1 > 0)
                {
                    ListViewItemTable1s.Add(new vmEntryPharRestPOS1.ListViewItemTable()
                    {
                        trsl = "000",
                        invcode = "045100102001",
                        reptsl = "000",
                        rsircode = "045100102001",
                        trdesc = "",
                        mfgid = "",
                        mfgcomnam = "",
                        invqty = 1,
                        truid = "",
                        trunit = "",
                        invrate = Labour1,
                        invamt = Labour1,
                        invdisamt = 0.00m,
                        invnetamt = Labour1,
                        invvatper = 0.00m,
                        invvatamt = 0.00m,
                        batchno = "",
                        invrmrk = "",
                        mfgdat = DateTime.Today,
                        expdat = DateTime.Today,
                        mfgvisible = "",
                        rmrkvisible = ""
                    });
                }

                string PayType1a = ((ComboBoxItem)this.cmbPayType.SelectedItem).Tag.ToString();
                    //(this.rbtnPayTypeCash.IsChecked == true ? "CASH" : (this.rbtnPayTypeCredit.IsChecked == true ? "CREDIT" : "CHEQUE"));

                string vouno1 = "000000000000000000";
                string vouno2 = "000000000000000000";
                string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();

                DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpSalDat.Text.Substring(0, 11)), EditMemoNum1: EditTransID1,
                            mcode: "SSI", cbSectCode: cbSectCode1, CustID1: CustID1, InvByID1: WpfProcessAccess.SignedInUserList[0].hccode, PayType1: PayType1a,
                            MemoRef1: "", MemoRefDate1: DateTime.Parse(this.xctk_dtpSalDat.Text.Substring(0, 11)), delivartime1: this.xctk_dtpSalDat.Text.Trim(),
                            MemoNar1: this.txtMemoNar.Text.Trim(), ListViewItemTable1a: ListViewItemTable1s, PayType: PayType1, DueAmt: DueAmt1, PaidAmt: PaidAmt1, vounum1: vouno1, vounum2: vouno2,
                            _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, InvStatus: InvStatus1, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

                //String xx1 = ds1.GetXml().ToString();

                var pap1 = vm1.SetParamUpdateMSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, ds1, EditTransID1);
                //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
                //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                decimal tnetam = this.ListViewItemTable1.Sum(x => x.invnetamt);
                string memonum1 = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                string memonum = ds2.Tables[0].Rows[0]["memonum"].ToString();
                this.txtTransID.Text = memonum1;
                this.txtTransID.Tag = memonum;

                this.MemoSaved = true;
                this.btnUpdateTrans.IsEnabled = false;

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.17: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
     
    
       
        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpSalDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpSalDat.IsEnabled)
                this.xctk_dtpSalDat.Focus();
        }

        private void txtPaidAmt_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }

        private void btnPrintTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.MemoSaved == false)
                    this.btnUpdateTrans_Click(null, null);

                if (this.MemoSaved == false)
                    return;

                this.PrintCount = this.PrintCount + 1;

                //if (this.PrintCount > 2)
                //    this.btnPrintTrans.Visibility = Visibility.Hidden;

                string MemoNum = this.txtTransID.Tag.ToString();
                string PrnOpt1 = (this.chkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                this.ViewPrintMemo(MemoNum, PrnOpt1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ViewPrintMemo(string memoNum = "XXXXXXXX", string ViewPrint = "View", string Duplicate = "")
        {
            try
            {
                //string memoNum = ((ComboBoxItem)this.cmbPrevInvList.SelectedItem).Tag.ToString();
                LocalReport rpt1 = null;
                string WindowTitle1 = "";
                var pap1 = vm2.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                ////this.lblTokenSlNo.Content = ds1.Tables[2].Rows[0]["tokenid"].ToString().Trim();
                ds1.Tables[0].Rows[0]["slnum"] = Convert.ToInt32(ds1.Tables[2].Rows[0]["tokenid"]);
                //var list3 = WpfProcessAccess.GetRptGenInfo(InputSource: "Test Input Source\n");
                var list3 = WpfProcessAccess.GetRptGenInfo();
                list3[0].RptHeader1 = "";
                switch (WpfProcessAccess.CompInfList[0].comcod)
                {
                    case "6501":
                        list3[0].RptHeader1 = "VAT Reg. # " + "XXXXXX";
                        break;
                    case "6531":
                        list3[0].RptHeader1 = "VAT Reg. # " + "000538591";
                        break;
                    case "6535":
                        list3[0].RptHeader1 = "";
                        break;
                }

                list3[0].RptFooter1 = "User : " + WpfProcessAccess.SignedInUserList[0].signinnam;

                var list1 = ds1.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
                var list2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                // var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();

                rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesInv01", list1, list2, list3);

                if (Duplicate.Length > 0 || this.PrintCount > 1)
                {
                    //rpt1.SetParameters(new ReportParameter("ParamAddress1", "[Re-Print/Duplicate Invoice]"));
                    rpt1.SetParameters(new ReportParameter("ParamAddress1", ":: D U P L I C A T E ::"));
                    rpt1.SetParameters(new ReportParameter("ParamAddress2", "========================="));
                }

                WindowTitle1 = "Sales Memo";

                if (ViewPrint == "View")
                {
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: "PrintLayout");
                }
                else if (ViewPrint == "DirectPrint")
                {
                    RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                    DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                    if (Duplicate.Length == 0 && this.PrintCount == 1)
                    {
                        rpt1.SetParameters(new ReportParameter("ParamTitle1", "Kitchen Order Token (KOT)"));
                        DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                    }
                    DirectPrint1.Dispose();
                }
                /*
             
                rpt1.PrintOptions.PrinterName = "PRNCASH";//"\\\\proserver\\Canon LBP3300MIS";
                if (this.chkPrintDirect.Checked)
                    rpt1.PrintToPrinter(1, false, 1, 1);
                else
                {
                    frmRptVirwer frm1 = new frmRptVirwer();
                    frm1.crystalReportViewer1.ReportSource = rpt1;
                    frm1.Show();
                } 
             
                 */
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectItemInfo();
        }
        private void SelectItemInfo()
        {
            ListBoxItem lbi1 = (ListBoxItem)this.lstItem.SelectedItem;
            if (lbi1 == null)
            {
                if (this.lstItem.Items.Count > 0)
                {
                    lbi1 = (ListBoxItem)this.lstItem.Items[0];
                }
                else
                    return;
            }
            this.txtItemName.Tag = lbi1.Tag.ToString();
            this.txtItemName.Text = lbi1.Content.ToString().Trim();
            this.txtItemName.ToolTip = this.txtItemName.Tag.ToString() + " - " + this.txtItemName.Text;
            string ItemId1 = lbi1.Tag.ToString();
            this.txtItemCode.Text = ItemId1.Substring(6, 6);
            this.txtItemCode.Focus();
        }

        private void lstItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectItemInfo();
            }
        }

        private void dgvUdQtyChange_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }

        private void chkSectBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.cmbSectCod.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.cmbSectCod.IsEnabled)
                this.cmbSectCod.Focus();
        }

        private void txtItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                this.txtItemName.Tag = "";
                this.txtItemName.ToolTip = string.Empty;
                this.lstItem.Items.Clear();
                string srchTxt = this.txtItemName.Text.Trim().ToUpper();
                if (srchTxt.Length == 0)
                    return;

                var lst1 = this.ShortRetSaleItemList.FindAll(x => (x.sircode.Substring(6, 6) + x.sirdesc.Trim()).ToUpper().Contains(srchTxt));
                foreach (var item in lst1)
                {
                    this.lstItem.Items.Add(new ListBoxItem()
                    {
                        Content = item.sircode.Substring(6, 6) + " - " + item.sirdesc,
                        Tag = item.sircode,
                        ToolTip = item.sirdesc.Trim() + "Rate: " + item.saleprice.ToString("#,##0") + ", Main Group: " + item.msirdesc.Trim()
                    }
                    );
                }
                if (lst1.Count > 0)
                {
                    this.txtItemName.Tag = lst1[0].sircode;
                    this.txtItemName.ToolTip = lst1[0].sircode + " - " + lst1[0].sirdesc.Trim();
                    //this.GridItemList.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.43: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                this.lstItem.Focus();
            else if (e.Key == Key.Return)
            {
                if (this.lstItem.Items.Count > 0)
                {
                    this.lstItem.SelectedIndex = 0;
                }
            }
        }

        private void txtItemCode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Visible;
            this.txtItemName.Focus();
        }

        private void btnCloseGridItemList_Click(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }

        private void btnPrevTrans_Click(object sender, RoutedEventArgs e)
        {
            this.stkpDataEntry.Visibility = Visibility.Collapsed;
            if (this.stkpPrevTrans.Visibility == Visibility.Visible)
            {
                this.stkpPrevTrans.Visibility = Visibility.Collapsed;
                this.stkpDataEntry.Visibility = Visibility.Visible;
                return;
            }
            this.stkpPrevTrans.Visibility = Visibility.Visible;
            if (this.TransInvList.Count == 0)
                this.btnFilter1_Click(null, null);
        }

        private void btnDelete1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgvPrevTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to cancel", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                int invidx1 = this.dgvPrevTransList.SelectedIndex;

                var invItem1 = (HmsEntityCommercial.InvoiceTransList)this.dgvPrevTransList.SelectedItem;
                string MemoNum = invItem1.invno; //((ListBoxItem)this.lstPrevTransList.SelectedItem).Tag.ToString();
                int itemno1 = this.dgvPrevTransList.SelectedIndex;

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel the Invoice # " + invItem1.invno1 + "\nInvoice Date : " +
                                             invItem1.invdat1 + ", Bill Amount : " + invItem1.billam.ToString("#,##0.00") + "\nFor " + invItem1.custName.Trim(),
                                             WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;



                var pap1 = vm1.SetParamCancelMemo(WpfProcessAccess.CompInfList[0].comcod, MemoNum);

                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;


                this.btnFilter1_Click(null, null);
                //this.TransInvList.RemoveAt(itemno1);
                //this.dgvPrevTransList.Items.Refresh();

                ////if (this.txtTransID.Tag.ToString().Trim() == MemoNum)
                ////    this.CleanUpScreen();

                this.dgvPrevTransList.SelectedIndex = (this.dgvPrevTransList.Items.Count <= invidx1 ? this.dgvPrevTransList.Items.Count - 1 : invidx1);

                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.40: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnFilter1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.TransInvList == null)
                    return;

                string FrmDate1 = this.xctk_dtpSrchDat1.Text.Trim();
                string ToDate1 = this.xctk_dtpSrchDat2.Text.Trim();
                string InvNo1 = this.txtSrchInvNo.Text.Trim();
                string InvStatus1 = "A";
                string Cust1 =  "%";

                this.TransInvList.Clear();

                string sectcod1 = ((ComboBoxItem)this.cmbSectCod.Items[this.cmbSectCod.SelectedIndex]).Tag.ToString();
                var pap1 = vm2.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, "A00MSISUM", FrmDate1, ToDate1, sectcod1, "SSI%" + InvNo1, InvStatus1, Cust1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.TransInvList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();


                foreach (var item1 in this.TransInvList)
                {

                    item1.preparetrm = (item1.custName.Trim().Length > 0 ? item1.custName.Trim() : "") +
                                       (item1.invnar.Trim().Length > 0 && item1.custName.Trim().Length > 0 ? ", " + item1.invnar.Trim() : "");
                }

                this.TransInvList = this.TransInvList.FindAll(x => x.invno.Substring(0, 3) == "SSI").ToList();
                if (this.TransInvList == null)
                    return;

                this.txtSrchInvNo.Text = "";

                this.dgvPrevTransList.ItemsSource = this.TransInvList;
                this.dgvPrevTransList.Items.Refresh();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.41: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnEdit1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.TransInvList.Count == 0)
                    return;

                if (this.dgvPrevTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to edit", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                this.CleanUpScreen();

                this.stkpPrevTrans.Visibility = Visibility.Collapsed;
                this.stkpDataEntry.Visibility = Visibility.Visible;
                this.stkpDataEntry.IsEnabled = true;

                //string MemoNum = ((ListBoxItem)this.lstPrevTransList.SelectedItem).Tag.ToString();
                //string custNam1a = ((ListBoxItem)this.lstPrevTransList.SelectedItem).Uid.ToString().Trim();

                var Memoitem1 = (HmsEntityCommercial.InvoiceTransList)this.dgvPrevTransList.SelectedItem;

                string MemoNum = Memoitem1.invno;
                string custNam1a = Memoitem1.custid.Substring(6, 6) + " - " + Memoitem1.custName.Trim();

                

                var pap1 = vm2.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, MemoNum);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                this.EditDs = WpfProcessAccess.GetHmsDataSet(pap1);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (this.EditDs == null)
                    return;

                var list1 = this.EditDs.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
                var list2 = this.EditDs.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();

                this.xctk_dtpSalDat.Value = list2[0].invdat;
                this.txtTransID.Text = list2[0].invno1;
                this.txtTransID.Tag = list2[0].invno;

                this.txtblEditMode.Visibility = (list2[0].invno != "SSI000000000000000" ? Visibility.Visible : Visibility.Hidden);

                //------------------------------------
                DateTime dat1 = list2[0].invdat;
                dat1 = DateTime.Parse("01-" + dat1.ToString("MMM-yyyy"));
                DateTime dat2 = dat1.AddMonths(1).AddDays(-1);
                this.xctk_dtpSalDat.Minimum = dat1;
                this.xctk_dtpSalDat.Maximum = dat2;
                //---------------------------

                string PayType1 = list2[0].paytype.Trim().ToUpper();

                ////this.rbtnPayTypeCash.IsChecked = (PayType1 == "CASH" ? true : false);
                ////this.rbtnPayTypeCheque.IsChecked = (PayType1 == "CHEQUE" ? true : false);
                ////this.rbtnPayTypeCredit.IsChecked = (PayType1 == "CREDIT" ? true : false);
                ////this.txtChequeNo.Text = list2[0].invref;
                ////this.xctk_dtpChqDat.Value = list2[0].invrefdat;
                this.txtMemoNar.Text = list2[0].invnar.Trim();

                ////this.stkpChequeInfo.Visibility = (PayType1 == "CHEQUE" ? Visibility.Visible : Visibility.Hidden);

                var Carr1 = list1.FindAll(x => x.rsircode == "045100101001");
                var Lab1 = list1.FindAll(x => x.rsircode == "045100102001");

                
                decimal DiscAmt1 = list1.Sum(x => x.idisam);
                ////this.txtDiscAmt.Text = DiscAmt1.ToString("#,##0.00");
                this.txtPaidAmt.Text = list2[0].collam.ToString("#,##0");
                string custid1 = list2[0].custid;
                ////this.autoCustSearch.ItemsSource = this.RegCustList;
                ////this.autoCustSearch.SelectedValue = list2[0].custid;
                ////this.autoCustSearch_LostFocus(null, null);
                var list1a = list1.FindAll(x => x.rsircode.Substring(0, 2) != "04").OrderBy(x => x.reptsl).ToList();

                string fval1 = list1a[0].reptsl;
                if (list1a.FindAll(x => x.reptsl == fval1).Count > 1)
                {
                    int i = 1;
                    foreach (var item2 in list1a)
                    {
                        item2.reptsl = i.ToString("000");
                        ++i;
                    }
                }

                this.ListViewItemTable1.Clear();
                foreach (var item in list1a)
                {
                    var itmcod1 = this.RetSaleItemList.FindAll(x => x.sircode == item.rsircode);

                    var item1a = new vmEntryPharRestPOS1.ListViewItemTableDetails()
                    {
                        trsl = item.slnum.ToString() + ".",
                        invcode = item.rsircode,
                        reptsl = item.reptsl,
                        rsircode = item.rsircode,
                        trdesc = itmcod1[0].sirdesc,
                        mfgid = "",
                        mfgcomnam = "",
                        invqty = item.invqty,
                        truid = "",
                        trunit = item.sirunit,
                        invrate = item.itmrat,
                        invamt = item.itmam,
                        invdisamt = item.idisam,
                        invnetamt = item.inetam,
                        invvatper = (item.itmam == 0 ? 0.00m : item.ivatam / item.itmam * 100.00m),
                        invvatamt = item.ivatam,
                        invrmrk = item.invrmrk,
                        batchno = item.rsircode.Substring(6) + ": " + item.sirdesc.Trim(),
                        mfgdat = list2[0].invdat,
                        expdat = list2[0].invdat.AddDays(7),
                        mfgvisible = itmcod1[0].mfgvisible,
                        rmrkvisible = itmcod1[0].rmrkvisible,
                        invqty2 = (itmcod1[0].siruconf == 0 ? 1.00m : Math.Round(item.invqty / itmcod1[0].siruconf, 6)),
                        invrate2 = (item.invqty == 0 ? 0.00m : Math.Round(item.itmam / (itmcod1[0].siruconf == 0 ? 1.00m : item.invqty / itmcod1[0].siruconf), 0)),
                        invweight = item.invqty * itmcod1[0].refscomp,
                        trunit2 = itmcod1[0].sirunit2,
                        siruconf = itmcod1[0].siruconf
                    };
                    this.ListViewItemTable1.Add(item1a);
                }              
                this.EditDs = null;

                this.dgvSalesMemo.Items.Refresh();
                this.btnTotal_Click(null, null);
                this.txtPaidAmt.Text = this.lblTotalBill.Content.ToString();
                this.btnTotal_Click(null, null);
                this.btnUpdateTrans.IsEnabled = true;
                this.stkpFinalUpdate.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.38: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void dgvPrevTransList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (this.TransInvList.Count == 0)
                    return;

                if (this.dgvPrevTransList.SelectedItem == null)
                    return;

                string MemoNum = ((HmsEntityCommercial.InvoiceTransList)this.dgvPrevTransList.SelectedItem).invno; //((ListBoxItem)this.lstPrevTransList.SelectedItem).Tag.ToString();
                string PrnOpt1 = "View";//  (this.chkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                this.ViewPrintMemo(MemoNum, PrnOpt1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("SSI-2.13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            this.dgvPrevTransList_MouseDoubleClick(null, null);
        }

        private void chkShowDraft_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
