using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.ComponentModel;
using ASITHmsViewMan.General;
using ASITHmsViewMan.Accounting;
using ASITHmsRpt1GenAcc.Accounting;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Collections;
using System.Windows.Threading;

namespace ASITHmsWpf.Commercial.Trading
{
    /// <summary>
    /// Interaction logic for frmEntryGenTrPOS101.xaml
    /// </summary>

    public partial class frmEntryGenTrPOS101 : UserControl
    {
        private bool FrmInitialized = false;
        private string CalcObjName = "NoName";
        private bool MemoSaved = false;
        
        private DataSet EditDs;

        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemMainGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();
        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();
        private List<vmEntryPharRestPOS1.RetSaleItem> RetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();

        private List<vmEntryPharRestPOS1.RetSaleItem> ShortRetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();
        private List<HmsEntityGeneral.SirInfCodeBook> RegCustList = new List<HmsEntityGeneral.SirInfCodeBook>();                    // REgistered Customer List from Resource Code Book
        private List<vmEntryPharRestPOS1.ItemCustDetailsInfo> RegCustDetailsList = new List<vmEntryPharRestPOS1.ItemCustDetailsInfo>();

        private List<HmsEntityCommercial.InvoiceTransList> TransInvList = new List<HmsEntityCommercial.InvoiceTransList>();

        private List<vmEntryPharRestPOS1.StockItemList> InvStockItemList = new List<vmEntryPharRestPOS1.StockItemList>();
        private List<vmEntryPharRestPOS1.StockItemSumList> InvStockItemSumList = new List<vmEntryPharRestPOS1.StockItemSumList>();

        private List<vmEntryPharRestPOS1.ListViewItemTableDetails> ListViewItemTable1 = new List<vmEntryPharRestPOS1.ListViewItemTableDetails>();

        private List<vmHmsGeneralList1.DraftTransactionList> DraftTransactionList1 = new List<vmHmsGeneralList1.DraftTransactionList>();

        private List<HmsEntityGeneral.AcInfCodeBook> AcCodeList1 = new List<HmsEntityGeneral.AcInfCodeBook>();
        private vmEntryVoucher1 vm1acc = new vmEntryVoucher1();
        private vmReportAccounts1 vmrptAcc = new vmReportAccounts1();

        private vmEntryPharRestPOS1 vm1 = new vmEntryPharRestPOS1();
        private vmReportPharRestPOS1 vm2 = new vmReportPharRestPOS1();
        private vmHmsGeneralList1 vmGenList1a = new vmHmsGeneralList1();


        private string preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
        private string prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
        private string preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
        private DateTime rowtime1 = DateTime.Now;

        private DispatcherFrame DispatcherFrame1;


        private string DraftMemoNum = "";
        private Int64 DraftMemoRowID = 0;

        public frmEntryGenTrPOS101()
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
                this.chkShowDraft.IsChecked = false;
                this.chkAllowDraft.IsChecked = true;
                this.GridDraftList.IsEnabled = false;
                this.GridDraftList.Visibility = Visibility.Collapsed;
                this.xctk_dtpSalDat.Value = DateTime.Today;
                this.ActivateAuthObjects();
                this.Objects_On_Init();
                this.CleanUpScreen();
                this.autoCustSearch.Focus();
            }
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //------ Draft information update option is enabled (Generally for local/high avaliability of database)
            if (this.chkAllowDraft.IsChecked == true && this.DraftMemoRowID > 0)
                this.btnTotal_Click(null, null);
        }

        private void ActivateAuthObjects()
        {
            try
            {
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS101_chkDateBlocked") == null)
                    this.chkDateBlocked.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS101_chkAutoSaveAc") == null)
                    this.chkAutoSaveAc.IsChecked = false;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS101_chkVerifyStock") == null)
                    this.chkVerifyStock.IsChecked = false;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS101_stkpAccVoucher0") == null)
                    this.stkpAccVoucher0.Visibility = Visibility.Collapsed;



                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS101_btnFilter1") == null)
                {
                    this.btnFilter1.Visibility = Visibility.Hidden;
                    this.stkpFilter1.Visibility = Visibility.Collapsed;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS101_btnEdit1") == null)
                    this.btnEdit1.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS101_btnDelete1") == null)
                {
                    this.btnDelete1.Visibility = Visibility.Hidden;
                    //this.btnDeleteTrans.Visibility = Visibility.Hidden;
                }

                
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryGenTrPOS101_chkAllowDraft") == null)
                {
                    this.chkAllowDraft.IsChecked = false;
                    this.stkpDraftOption.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.08: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void Objects_On_Init()
        {
            try
            {

                this.GetSectionList();
                this.GetRetailItemList();

                this.dgvMemo.ItemsSource = this.ListViewItemTable1;
                this.xctk_dtpSrchDat1.Value = DateTime.Today.AddDays(-3);
                this.xctk_dtpSrchDat2.Value = DateTime.Today;
                this.btnFilter1_Click(null, null);
                this.GetCustomerList();

                if (WpfProcessAccess.AccCodeList == null)
                    WpfProcessAccess.GetAccCodeList();

                this.AcCodeList1 = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 8) == "31010001" || x.actcode.Substring(0, 2) == "18" ||
                              x.actcode.Substring(0, 4) == "1901") && (x.actcode.Substring(8, 4) != "0000")).OrderBy(x => x.actcode).ToList();


                this.cmbPayType.SelectedIndex = 2;
                this.cmbPayType_SelectionChanged(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
                var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "0151", reqmfginf: "WITHOUTMFGINFO");
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
                        row1["msirdesc"].ToString().Trim() + " - " + row1["sirdesc"].ToString(), row1["sircode"].ToString().Substring(6), row1["mfgid"].ToString().Trim(), row1["mfgcomnam"].ToString(),
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
                System.Windows.MessageBox.Show("GSI-2.12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void CleanUpScreen()
        {
            try
            {
                this.txtDummy1.Visibility = Visibility.Hidden;
                this.stkpPrint.Visibility = Visibility.Hidden;
                this.stkpFinalUpdate.Visibility = Visibility.Hidden;
                this.gridCalc1.Visibility = Visibility.Collapsed;
                this.stkpDataEntry.Visibility = Visibility.Collapsed;
                this.stkpDataEntry.IsEnabled = false;
                this.stkpPrevTrans.Visibility = Visibility.Collapsed;
                this.stkpdgvPrevTransList.IsEnabled = false;
                this.GridAccVoucher.Visibility = Visibility.Collapsed;
                this.stkpVAT.Visibility = Visibility.Hidden;
                this.stkpChequeInfo.Visibility = Visibility.Hidden;
                this.GridItemList.Visibility = Visibility.Collapsed;
                this.btnUpdateVoucher1.Visibility = Visibility.Hidden;
                this.btnPrint2Voucher1.Visibility = Visibility.Hidden;
                this.btnUpdateVoucher2.Visibility = Visibility.Hidden;
                this.btnPrint2Voucher2.Visibility = Visibility.Hidden;
                this.txtblEditMode.Visibility = Visibility.Hidden;
                ////this.stkpAccVoucher0.Visibility = Visibility.Hidden;
                ////this.stkpChequeInfo.Visibility = Visibility.Hidden;

                this.EditDs = null;
                //--this.stkpDataEntry.IsEnabled = true;
                //this.stkpCustInf1.IsEnabled = true;
                this.cmbSectCod.IsEnabled = true;
                this.txtPaidAmt.IsEnabled = true;
                this.rbtnPayTypeCash.IsChecked = false;
                this.rbtnPayTypeCredit.IsChecked = true;
                this.rbtnPayTypeCheque.IsChecked = false;
                this.chkDiscount.IsChecked = false;
                this.iudDisPer.IsEnabled = false;
                this.btnSetDispPer.IsEnabled = false;
                this.btnUpdateTrans.IsEnabled = false;
                this.btnAddRecord.IsEnabled = true;
                this.stkpEntryHeader.IsEnabled = true;
                this.stkpEntryFooter.IsEnabled = true;
                this.stkpTitleEntry.IsEnabled = true;
                this.dgvMemo.IsEnabled = true;
                this.cmbDrAccHead1.IsEnabled = true;
                this.cmbCrAccHead1.IsEnabled = true;
                this.autoCustSearch.IsEnabled = true;
                this.btnUpdateVoucher1.IsEnabled = true;
                this.btnUpdateVoucher2.IsEnabled = true;
                this.cmbDrAccHead2.IsEnabled = true;
                this.cmbCrAccHead2.IsEnabled = true;
                this.chkSrchCust.IsChecked = false;
                this.MemoSaved = false;

                this.xcdtDeliveryDT.Value = DateTime.Parse(DateTime.Today.AddDays(0).ToString("dd-MMMM-yyyy") + " 07:00 PM");
                this.xctk_dtpSalDat.Minimum = DateTime.Today.AddDays(-365 * 3);
                this.xctk_dtpSalDat.Maximum = DateTime.Today.AddDays(365 * 2);
                this.xctk_dtpSalDat.Value = DateTime.Today;

                this.xctk_dtpChqDat.Value = this.xctk_dtpSalDat.Value;
                this.txtChequeNo.Text = "";
                this.lstItem.Items.Clear();
                this.txtbCustName.Text = "";
                this.txtbCustAddress.Text = "";
                this.txtbCustPhone.Text = "";
                this.txtLabAmt.Text = "";
                this.txtCarrAmt.Text = "";
                this.txtDiscAmt.Text = "";
                this.lblTotalQty.Content = "";
                this.lblTotalQty2.Content = "";
                this.lblTinTotalBan.Content = "";
                this.lblTotalWeight.Content = "";
                this.lblPaidPercent.Content = "";
                this.lblNetBalPercent.Content = "";
                this.lblGrandTotal.Content = " -  ";
                this.lblDiscTotal.Content = " -  ";
                this.lblNetTotal.Content = " -  ";
                this.lblChangeCash.Content = " -  ";
                this.lblNetBalance.Content = " -  ";
                this.txtPaidAmt.Text = "0";
                this.lblVATTotal.Content = " - ";
                this.lblTotalBill.Content = " - ";

                this.iudDisPer.Value = 0;
                this.btnUpdateTrans.Tag = "New";
                this.ListViewItemTable1.Clear();
                this.dgvMemo.Items.Refresh();
                this.cmbPayType.SelectedIndex = 2;
                this.txtMemoNar.Text = "";
                string lastid1 = this.GetLastTransID();
                this.txtTransID.Text = (lastid1 == "GSI000000000000000" ? "" : "");
                this.txtTransID.Tag = lastid1;
                this.autoCustSearch.SelectedValue = null;
                this.lblDrAmount1.Content = "0.00";
                this.lblCrAmount1.Content = "0.00";
                this.lblDrAmount2.Content = "0.00";
                this.lblCrAmount2.Content = "0.00";
                this.SetStockBackGround();

                this.lblVouNo1.Content = "XVXMM-CCCC-XXXXX";
                this.lblVouNo1.Tag = "XVXYYYYMMCCCCXXXXX";
                this.lblVouNo2.Content = "XVXMM-CCCC-XXXXX";
                this.lblVouNo2.Tag = "XVXYYYYMMCCCCXXXXX";

                this.txtSrchInvNo.Text = "";

                this.cmbPayType_SelectionChanged(null, null);


                this.GetStockItemList();

                this.btnClearRecord_Click(null, null);

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private string GetLastTransID()
        {
            return "GSI000000000000000";
        }

        private bool GetStockItemList()
        {
            try
            {
                string StoreID1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();
                string AsOnDate1 = this.xctk_dtpSalDat.Text;// this.xctk_dtSalesDat.Text;
                string StockItemType1 = "ALLITEMS"; // Show Item with Zero or Negative Stocks
                // string StockItemType1 = "STOCKITEMS"; // Show Item with Non Zero or Positive Stocks
                var pap1 = vm1.SetParamGetStockItemList(WpfProcessAccess.CompInfList[0].comcpcod, StoreID1, AsOnDate1, StockItemType1, "SUMMARY");
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                {
                    WpfProcessAccess.ShowDatabaseErrorMessage();
                    return false;
                }

                this.InvStockItemList.Clear();
                this.InvStockItemSumList.Clear();
                this.InvStockItemList = ds2.Tables[0].DataTableToList<vmEntryPharRestPOS1.StockItemList>();
                this.InvStockItemSumList = ds2.Tables[1].DataTableToList<vmEntryPharRestPOS1.StockItemSumList>();

                return true;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return false;
            }

        }

        private void GetCustomerList()
        {
            try
            {
                var pap1 = vmGenList1a.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "55%", "5"); //"[0-4]%"
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.RegCustList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                foreach (var item in this.RegCustList)
                {
                    item.sirdesc1 = item.sirdesc1.Substring(6);
                }


                var pap1d = vmGenList1a.SetParamGeneralDataInfo(WpfProcessAccess.CompInfList[0].comcpcod, "SIRINF", "55", "SICD001");
                DataSet ds1d = WpfProcessAccess.GetHmsDataSet(pap1d);
                if (ds1d == null)
                    return;

                this.RegCustDetailsList = ds1d.Tables[0].DataTableToList<vmEntryPharRestPOS1.ItemCustDetailsInfo>();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.10: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
                this.cmbSectCod.IsEnabled = (this.cmbSectCod.Items.Count == 1 ? false : true);
                this.cmbSectCod.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (this.CalcObjName)
                {
                    case "TXTITEMQTY":
                        this.txtItemQty.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                        this.txtItemQty.Focus();
                        break;
                    case "TXTITEMRATE":
                        this.txtItemRate.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                        this.txtItemRate.Focus();
                        break;
                    case "LBLLABAMT":
                    case "TXTLABAMT":
                        this.txtLabAmt.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                        this.txtLabAmt.Focus();
                        break;
                    case "LBLCARRAMT":
                    case "TXTCARRAMT":
                        this.txtCarrAmt.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                        this.txtCarrAmt.Focus();
                        break;
                    case "LBLDISCAMT":
                    case "TXTDISCAMT":
                        this.txtDiscAmt.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                        this.txtDiscAmt.Focus();
                        break;
                    case "LBLPAIDAMT":
                    case "TXTPAIDAMT":
                        this.txtPaidAmt.Text = HmsCalculator.Text2Value(this.txtExprToCalc.Text.Trim());
                        this.txtPaidAmt.Focus();
                        break;
                }

                this.txtExprToCalc.Text = "";
                this.gridCalc1.Visibility = Visibility.Collapsed;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.42: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void lstItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectItemInfo();
                this.txtItemQty.Focus();
                //this.txtItemName.Focus();
            }
        }

        private void lstItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectItemInfo();
            this.txtItemQty.Focus();
            //this.txtItemName.Focus();
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
            this.ShowInHandStock(ItemId1);
        }
     
        private void btnAddCust_Click(object sender, RoutedEventArgs e)
        {
            HmsDialogWindow1 window1 = new HmsDialogWindow1(new General.frmSirCodeBook1(MainGroup: "55"));
            window1.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window1.Title = "CUSTOMER CODE BOOK ADD/EDIT SCREEN";
            //window1.Owner = Application.Current.MainWindow;
            window1.ShowDialog();
            this.GetCustomerList();
        }

        private void btnNewShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //------ Draft information update option is enabled (Generally for local/high avaliability of database)
                if (this.chkAllowDraft.IsChecked == true && this.DraftMemoRowID > 0)
                    this.btnTotal_Click(null, null);

                if (this.btnNewShow.Content.ToString() == "_New")
                {
                    this.stkpDraftOption.IsEnabled = true;
                    this.DraftMemoNum = "";
                    this.DraftMemoRowID = 0;

                    this.CleanUpScreen();
                    this.btnNewShow.Content = "_Ok";
                    this.autoCustSearch.Focus();
                    return;
                }

                if (this.autoCustSearch.SelectedValue == null)
                    return;

                this.btnNewShow.Content = "_New";
                this.stkpFinalUpdate.Visibility = Visibility.Hidden;
                this.cmbSectCod.IsEnabled = false;
                this.stkpDraftOption.IsEnabled = false;
                this.stkpPrevTrans.Visibility = Visibility.Collapsed;
                this.stkpdgvPrevTransList.IsEnabled = false;
                this.stkpDataEntry.Visibility = Visibility.Visible;
                this.stkpDataEntry.IsEnabled = true;
                this.chkAutoSaveAc_Click(null, null);
                this.btnClearRecord_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.14: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.btnTotal_Click(null, null);               

                string InvStatus1 = "A";
                string PayType1 = ((ComboBoxItem)this.cmbPayType.SelectedItem).Tag.ToString();
                string MemoNar1 = this.txtMemoNar.Text.Trim();
                string DueAmt1 = this.lblNetBalance.Content.ToString();
                string PaidAmt1 = this.txtPaidAmt.Text.Trim();

                if (this.chkConfrmSave.IsChecked == false)
                {
                    if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }

                if (this.autoCustSearch.SelectedValue == null)
                    return;

                #region Accounts Voucher Related Updates

                //string EditTransID1 = (this.EditDs != null ? this.txtTransID.Tag.ToString() : "");
                //if (this.EditDs != null)

                string EditTransID1 = this.txtTransID.Tag.ToString();
                EditTransID1 = (EditTransID1 == "GSI000000000000000" ? "" : EditTransID1);
                if (EditTransID1.Length > 0)
                {
                    var pap1a = vm1.SetParamInvoiceVouchers(WpfProcessAccess.CompInfList[0].comcod, EditTransID1);

                    DataSet ds1a = WpfProcessAccess.GetHmsDataSet(pap1a);
                    if (ds1a == null)
                        return;

                    foreach (DataRow dr1 in ds1a.Tables[0].Rows)
                    {
                        string vounum1 = dr1["vounum"].ToString().Trim();
                        var pap1b = vm1acc.SetParamCancelVoucher(WpfProcessAccess.CompInfList[0].comcod, vounum1);
                        DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                        if (ds1b == null)
                            return;
                    }
                }

                //if (this.chkAutoSaveAc.IsChecked == true)
                {
                    if (this.stkpAccVoucher1.Visibility == Visibility.Visible)
                        this.btnUpdateVoucher1_Click(null, null);
                    else if (this.stkpAccVoucher2.Visibility == Visibility.Visible)
                        this.btnUpdateVoucher2_Click(null, null);
                }

                #endregion // End of Accounts Voucher Related Updates

                string CustID1 = this.autoCustSearch.SelectedValue.ToString();
                string CustName1 = this.autoCustSearch.SelectedText.Trim().Substring(9);

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

                decimal Carring1 = decimal.Parse("0" + this.txtCarrAmt.Text.Trim());    // "045100101001"
                decimal Labour1 = decimal.Parse("0" + this.txtLabAmt.Text.Trim());      // "045100102001"

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

                string PayType1a = (this.rbtnPayTypeCash.IsChecked == true ? "CASH" : (this.rbtnPayTypeCredit.IsChecked == true ? "CREDIT" : "CHEQUE"));

                string vouno1 = this.lblVouNo1.Tag.ToString().Trim();
                string vouno2 = this.lblVouNo2.Tag.ToString().Trim();
                string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();

                DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpSalDat.Text.Substring(0, 11)), EditMemoNum1: EditTransID1,
                            mcode: "GSI", cbSectCode: cbSectCode1, CustID1: CustID1, InvByID1: this.preparebyid1, PayType1: PayType1a,
                            MemoRef1: this.txtChequeNo.Text.Trim(), MemoRefDate1: DateTime.Parse(this.xctk_dtpChqDat.Text.Substring(0, 11)), delivartime1: this.xcdtDeliveryDT.Text.Trim(),
                            MemoNar1: this.txtMemoNar.Text.Trim(), ListViewItemTable1a: ListViewItemTable1s, PayType: PayType1, DueAmt: DueAmt1, PaidAmt: PaidAmt1, vounum1: vouno1, vounum2: vouno2,
                            _preparebyid: this.preparebyid1, InvStatus: InvStatus1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);

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

                DateTime MemoDate1 = DateTime.Parse(this.xctk_dtpSalDat.Text.Substring(0, 11));
                string invref1 = this.xctk_dtpChqDat.Text.Trim();
                string invnar1 = this.txtMemoNar.Text.Trim();

                string Content1 = memonum1.Substring(3, 2) + "-" + memonum1.Substring(11, 5) + " [" + MemoDate1.ToString("dd.MM.yyyy") + "] " + CustName1.Trim();
                string Content2 = memonum1.Substring(3, 2) + "-" + memonum1.Substring(11, 5) + " [Tk. " + tnetam.ToString("#,##0.00") +
                        (DueAmt1.Contains("(") ? "" : ", Due: Tk. " + DueAmt1) + ", " +
                        MemoDate1.ToString("dd.MM.yyyy") + "]" + (invref1.Trim().Length > 0 ? ", " + invref1.Trim() : "") +
                        (CustName1.Trim().Length > 0 ? ",\n\t" + CustName1.Trim() : "") +
                        (invnar1.Trim().Length > 0 ? ", " + invnar1.Trim() : "");

                this.MemoSaved = true;
                this.btnUpdateTrans.IsEnabled = false;
                this.stkpDataEntry.IsEnabled = false;
                this.stkpPrint.Visibility = Visibility.Visible;
                this.TransInvList.Clear();
                //------ Draft information update option is enabled (Generally for local/high avaliability of database)
                if (this.chkAllowDraft.IsChecked == true)
                {
                    DataSet dsdraft1 = WpfProcessAccess.UpdateDeleteDraftTransaction(UpadateDelete1: "DELETE", ds1: null, draftnum1: this.DraftMemoNum, rowid1: this.DraftMemoRowID,
                                       draftrmrk1: "", draftbyid1: this.preparebyid1, draftses1: this.prepareses1, drafttrm1: this.preparetrm1);
                    if (dsdraft1 == null)
                        return;
                    this.DraftMemoNum = "";
                    this.DraftMemoRowID = 0;
                }
                //------------------------------------------------------
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.17: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpSalDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpSalDat.IsEnabled)
                this.xctk_dtpSalDat.Focus();
        }

        private void rbtnPayType_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rbtnObj1 = (RadioButton)sender;
            this.stkpChequeInfo.Visibility = Visibility.Hidden;
            //this.stkpChequeInfo.Visibility = Visibility.Collapsed;
            this.txtPaidAmt.IsEnabled = true;
            switch (rbtnObj1.Name)
            {
                //case "rbtnPayTypeCredit":
                //    this.txtPaidAmt.Text = "0.00";
                //    this.txtPaidAmt.IsEnabled = false;
                //    break;
                case "rbtnPayTypeCheque":
                    this.stkpChequeInfo.Visibility = Visibility.Visible;
                    break;
            }

            // rbtnPayTypeCash
            // rbtnPayTypeCredit
            // rbtnPayTypeCheque

        }

        private void autoCustSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetCustSirdesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetCustSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                this.RegCustList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoCustSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.autoCustSearch.SelectedValue == null)
                    return;

                this.txtbCustName.Text = "";
                this.txtbCustAddress.Text = "";
                this.txtbCustPhone.Text = "";

                string CustID1 = this.autoCustSearch.SelectedValue.ToString();
                var CustDetails = this.RegCustDetailsList.FindAll(x => x.tblcode == CustID1);
                if (CustDetails.Count > 0)
                {
                    var Custnam1 = CustDetails.FindAll(x => x.gencode == "SICD00101001");
                    if (Custnam1.Count > 0)
                        this.txtbCustName.Text = Custnam1[0].dataval.Trim();

                    var Custadd1 = CustDetails.FindAll(x => x.gencode == "SICD00101003");
                    string Address1 = "  ";
                    foreach (var item in Custadd1)
                    {
                        Address1 = Address1 + item.dataval.Trim() + ", ";
                    }
                    Address1 = Address1.Substring(0, Address1.Length - 2).Trim();
                    this.txtbCustAddress.Text = Address1;

                    this.txtbCustName.Text = this.txtbCustName.Text + (Address1.Length > 0 ? ", " + Address1 : ""); // New Addition

                    var Custphon1 = CustDetails.FindAll(x => x.gencode == "SICD00101004");
                    string Phone1 = "  ";
                    foreach (var item in Custphon1)
                    {
                        Phone1 = Phone1 + item.dataval.Trim() + ", ";
                    }
                    Phone1 = Phone1.Substring(0, Phone1.Length - 2).Trim();
                    this.txtbCustPhone.Text = Phone1;

                    this.txtbCustName.Text = this.txtbCustName.Text + (Phone1.Length > 0 ? ", Ph." + Phone1 : ""); // New Addition

                    //if (Custnam1.Count > 0)
                    //    this.txtbCustAddress.Text = Custadd1[0].dataval.Trim();
                    // SICD00101001     Full Name
                    // SICD00101003     Address
                    // SICD00101004     Phone No

                    //SIRCODE	SIRDESC
                    //045100101001	CARRING CHARGE
                    //045100102000	LABOUR COST
                    //045100102001	LABOUR CHARGE
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.31: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
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

                var lst1 = this.ShortRetSaleItemList.FindAll(x => x.sirdesc.Trim().ToUpper().Contains(srchTxt));
                foreach (var item in lst1)
                {
                    this.lstItem.Items.Add(new ListBoxItem()
                    {
                        Content = item.sirdesc,
                        Tag = item.sircode,
                        ToolTip = item.sirdesc.Trim() + "Rate: " + item.saleprice.ToString("#,##0") + ", Main Group: " + item.msirdesc.Trim()
                    }
                    );
                }
                if (lst1.Count > 0)
                {
                    this.txtItemName.Tag = lst1[0].sircode;
                    this.txtItemName.ToolTip = lst1[0].sircode + " - " + lst1[0].sirdesc.Trim();
                    this.GridItemList.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.43: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void txtItemName_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtDummy1.Visibility = Visibility.Hidden;
            // this.GridItemList.Visibility = Visibility.Visible;
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

        private void txtItemQty_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtItemName.Tag == null)
                    return;

                string ItemId1 = this.txtItemName.Tag.ToString();   // this.autoItemSearch.SelectedValue.ToString();
                this.ShowInHandStock(ItemId1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.32: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ShowInHandStock(string ItemId1)
        {
            try
            {
                var item1 = this.RetSaleItemList.FindAll(x => x.sircode == ItemId1);

                if (item1.Count == 0)
                    return;

                // Start of Current Stock Calculation
                var itmStk1 = this.InvStockItemSumList.FindAll(x => x.sircode == ItemId1);
                if (itmStk1.Count == 0)
                    return;

                int roundF1 = (item1[0].siruconf < 1 ? 0 : 6);
                int roundF3 = (item1[0].siruconf3 < 1 ? 0 : 6);
                decimal stku1 = itmStk1[0].stkqty;
                decimal stku2 = Math.Round(stku1 / item1[0].siruconf, roundF1);
                decimal stku3 = Math.Round(stku1 / item1[0].siruconf3, roundF3);
                this.lblStockU1a.Content = stku1.ToString("#,##0.00") + " " + item1[0].sirunit.Trim();
                this.lblStockU2a.Content = stku2.ToString("#,##0.00") + " " + item1[0].sirunit2.Trim();
                this.lblStockU3a.Content = stku3.ToString("#,##0.00") + " " + item1[0].sirunit3.Trim();

                this.lblStockU2a.Content = (stku1 == stku2 ? "" : this.lblStockU2a.Content);
                this.lblStockU3a.Content = (stku1 == stku3 ? "" : this.lblStockU3a.Content);

                // End of Current Stock Calculation

                decimal qty1 = decimal.Parse("0" + this.txtItemQty.Text.Trim());
                decimal qty2 = (item1[0].siruconf == 1.00m ? qty1 : Math.Round(qty1 / item1[0].siruconf, roundF1));
                decimal qty3 = (item1[0].sirunit3.Trim().Length == 0 ? 1 : (item1[0].siruconf3 <= 1.00m ? qty1 : Math.Round(qty1 / item1[0].siruconf3, roundF3)));

                this.txtItemQty2.Text = qty2.ToString("#,##0.00");
                this.txtItemQty2.Tag = qty2.ToString();

                this.txtItemQty3.Text = qty3.ToString("#,##0.00");
                this.txtItemQty3.Tag = qty3.ToString();

                this.txtItemQty2.Visibility = (qty1 == qty2 ? Visibility.Hidden : Visibility.Visible);
                this.txtItemQty3.Visibility = (qty1 == qty3 ? Visibility.Hidden : Visibility.Visible);
                this.lblUnit2.Visibility = (qty1 == qty2 ? Visibility.Hidden : Visibility.Visible);
                this.lblUnit3.Visibility = (qty1 == qty3 ? Visibility.Hidden : Visibility.Visible);


                int Uidx1 = this.cmbRateUnit.SelectedIndex;
                decimal Rate1 = decimal.Parse("0" + this.txtItemRate.Text.Trim());
                switch (Uidx1)
                {
                    case 0:
                        this.txtItemAmount.Text = Math.Round(qty1 * Rate1, 2).ToString("#,##0.00");
                        break;
                    case 1:
                        this.txtItemAmount.Text = Math.Round(qty2 * Rate1, 2).ToString("#,##0.00");
                        break;
                    case 2:
                        this.txtItemAmount.Text = Math.Round(qty3 * Rate1, 2).ToString("#,##0.00");
                        break;
                }

                var itmSal1 = this.ListViewItemTable1.FindAll(x => x.rsircode == ItemId1);

                decimal qty1h = qty1;
                if (itmSal1.Count > 0)
                    qty1h = qty1 + itmSal1.Sum(x => x.invqty);

                this.lblStockU1h.Content = (stku1 - qty1h).ToString("#,##0.00") + " " + item1[0].sirunit.Trim();
                this.lblStockU2h.Content = (stku2 - Math.Round(qty1h / item1[0].siruconf, roundF1)).ToString("#,##0.00") + " " + item1[0].sirunit2.Trim();
                this.lblStockU3h.Content = (stku3 - Math.Round(qty1h / item1[0].siruconf3, roundF3)).ToString("#,##0.00") + " " + item1[0].sirunit3.Trim();

                this.lblStockU2h.Content = (this.lblStockU2h.Content.ToString() == this.lblStockU1h.Content.ToString() ? "" : this.lblStockU2h.Content);
                this.lblStockU3h.Content = (this.lblStockU3h.Content.ToString() == this.lblStockU1h.Content.ToString() ? "" : this.lblStockU3h.Content);

                this.lblStockU3.Content = "Weight : " + (qty1 * item1[0].refscomp).ToString("#,##0.00") + " Kg";// *"00.0";
                this.SetStockBackGround();
                this.RecalculateRateInfo();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.30: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void RecalculateRateInfo()
        {
            decimal qty1 = decimal.Parse("0" + this.txtItemQty.Text.Trim());
            decimal qty2 = decimal.Parse("0" + this.txtItemQty2.Tag.ToString().Trim());
            decimal qty3 = decimal.Parse("0" + this.txtItemQty3.Tag.ToString().Trim());

            decimal amt1 = decimal.Parse("0" + this.txtItemAmount.Text.Trim());
            int Uidx1 = this.cmbRateUnit.SelectedIndex;

            switch (Uidx1)
            {
                case 0:
                    this.txtItemRate.Text = Math.Round(amt1 / qty1, 2).ToString("#,##0.00");
                    this.txtItemRate2.Text = Math.Round(amt1 / qty2, 0).ToString("#,##0.00");
                    this.txtItemRate3.Text = Math.Round(amt1 / qty3, 0).ToString("#,##0.00");
                    this.lblItemRate2.Content = ((ComboBoxItem)((ComboBox)this.cmbRateUnit).Items[1]).Content;
                    this.lblItemRate3.Content = ((ComboBoxItem)((ComboBox)this.cmbRateUnit).Items[2]).Content;
                    break;
                case 1:
                    this.txtItemRate.Text = Math.Round(amt1 / qty2, 0).ToString("#,##0.00");
                    this.txtItemRate2.Text = Math.Round(amt1 / qty1, 2).ToString("#,##0.00");
                    this.txtItemRate3.Text = Math.Round(amt1 / qty3, 0).ToString("#,##0.00");
                    this.lblItemRate2.Content = ((ComboBoxItem)((ComboBox)this.cmbRateUnit).Items[0]).Content;
                    this.lblItemRate3.Content = ((ComboBoxItem)((ComboBox)this.cmbRateUnit).Items[2]).Content;
                    break;
                case 2:
                    this.txtItemRate.Text = Math.Round(amt1 / qty3, 0).ToString("#,##0.00");
                    this.txtItemRate2.Text = Math.Round(amt1 / qty1, 2).ToString("#,##0.00");
                    this.txtItemRate3.Text = Math.Round(amt1 / qty2, 0).ToString("#,##0.00");
                    this.lblItemRate2.Content = ((ComboBoxItem)((ComboBox)this.cmbRateUnit).Items[0]).Content;
                    this.lblItemRate3.Content = ((ComboBoxItem)((ComboBox)this.cmbRateUnit).Items[1]).Content;
                    break;
            }
            this.lblItemRate2.Visibility = (this.txtItemRate.Text == this.txtItemRate2.Text ? Visibility.Collapsed : Visibility.Visible);
            this.lblItemRate3.Visibility = (this.txtItemRate.Text == this.txtItemRate3.Text ? Visibility.Hidden : Visibility.Visible);
            this.lblItemRate3.Visibility = (this.txtItemRate2.Text == this.txtItemRate3.Text ? Visibility.Hidden : this.lblItemRate3.Visibility);
            this.txtItemRate2.Visibility = this.lblItemRate2.Visibility;
            this.txtItemRate3.Visibility = this.lblItemRate3.Visibility;

        }

        private void SetStockBackGround()
        {
            //var bc = new BrushConverter();
            var bcc = (Brush)(new BrushConverter()).ConvertFrom("#FFDBFBF8");
            var bcc1 = (Brush)(new BrushConverter()).ConvertFrom("#FFDBFBF8");

            if (this.lblStockU1a.Content.ToString().Contains("-"))
                bcc = (Brush)(new BrushConverter()).ConvertFrom("#FFF3A8A8");

            if (this.lblStockU1h.Content.ToString().Contains("-"))
                bcc1 = (Brush)(new BrushConverter()).ConvertFrom("#FFF3A8A8");

            this.stkpStockU1a.Background = bcc;
            this.stkpStockU1h.Background = bcc1;
        }
        private void txtItemQty_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.GridItemList.Visibility == Visibility.Visible)
            {
                if (this.lstItem.Items.Count > 0)
                {
                    this.txtItemName.Text = ((ListBoxItem)this.lstItem.Items[0]).Content.ToString();
                    this.txtItemName.Tag = ((ListBoxItem)this.lstItem.Items[0]).Tag.ToString();
                    this.txtItemName.ToolTip = this.txtItemName.Tag.ToString() + " - " + this.txtItemName.Text;
                    string ItemId1 = ((ListBoxItem)this.lstItem.Items[0]).Tag.ToString();
                    this.ResetUnitRateInfo(ItemId1);
                }
                this.GridItemList.Visibility = Visibility.Collapsed;
            }
        }
        private void ResetUnitRateInfo(string ItemId1)
        {
            try
            {
                this.cmbRateUnit.IsEnabled = true;
                var item1 = this.RetSaleItemList.FindAll(x => x.sircode == ItemId1);

                this.cmbQtyUnit.Items.Clear();
                this.cmbRateUnit.Items.Clear();
                this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1[0].sirunit.Trim(), Tag = item1[0].sirunit.Trim().ToUpper(), Uid = "1.00", ContentStringFormat = item1[0].refscomp.ToString() });
                this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1[0].sirunit.Trim(), Tag = item1[0].sirunit.Trim().ToUpper(), Uid = item1[0].saleprice.ToString() });

                this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1[0].sirunit2.Trim(), Tag = item1[0].sirunit2.Trim().ToUpper(), Uid = item1[0].siruconf.ToString() });
                this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1[0].sirunit2.Trim(), Tag = item1[0].sirunit2.Trim().ToUpper(), Uid = (item1[0].saleprice * item1[0].siruconf).ToString("") });

                if (item1[0].sirunit3.Trim().Length > 0)
                {
                    this.cmbQtyUnit.Items.Add(new ComboBoxItem() { Content = item1[0].sirunit3.Trim(), Tag = item1[0].sirunit3.Trim().ToUpper(), Uid = item1[0].siruconf3.ToString() });
                    this.cmbRateUnit.Items.Add(new ComboBoxItem() { Content = "Rate /" + item1[0].sirunit3.Trim(), Tag = item1[0].sirunit3.Trim().ToUpper(), Uid = (item1[0].saleprice * item1[0].siruconf3).ToString("") });
                }

                this.lblUnit2.Content = item1[0].sirunit2.Trim();
                this.lblUnit3.Content = item1[0].sirunit3.Trim();

                this.cmbQtyUnit.SelectedIndex = 0;
                if (this.txtItemQty.Text.Trim().Length == 0)
                    this.txtItemQty.Text = ((ComboBoxItem)this.cmbQtyUnit.Items[0]).Uid;
                this.txtItemQty.SelectAll();

                string u2 = item1[0].sirunit2.Trim().ToUpper();
                this.cmbRateUnit.SelectedIndex = (u2 == "BAN" || u2 == "HOND" || (ItemId1.Substring(0, 9) == "015100806" && u2 == "FIT") ? 1 : 0);

                if (item1[0].sirunit.Trim().ToUpper() == item1[0].sirunit2.Trim().ToUpper())
                    this.cmbRateUnit.IsEnabled = false;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.31: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void OpenGridCalc1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.CalcObjName = ((Label)sender).Name.ToUpper();
            this.gridCalc1.Visibility = Visibility.Visible;
            this.txtExprToCalc.Text = "";
            this.txtExprToCalc.Focus();
        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.FrmInitialized == false)
                    return;
                this.dgvMemo.ItemsSource = null;
                this.ListViewItemTable1 = this.ListViewItemTable1.FindAll(x => x.invqty > 0);
                //foreach (var item in this.ListViewItemTable1)
                //{
                //    //item.invamt = Math.Round(item.invrate * item.invqty, 0);
                //    item.invamt = Math.Round(item.invrate2 / item.siruconf * item.invqty, 0);
                //}

                decimal DiscTotal1 = decimal.Parse("0" + this.txtDiscAmt.Text.Trim());
                decimal GrandTotal1 = Math.Round(this.ListViewItemTable1.Sum(x => x.invamt), 0);
                decimal Carring1 = decimal.Parse("0" + this.txtCarrAmt.Text.Trim());    // "045100101001"
                decimal Labour1 = decimal.Parse("0" + this.txtLabAmt.Text.Trim());      // "045100102001"

                foreach (var item in this.ListViewItemTable1)
                {
                    //item.invamt = Math.Round(item.invrate * item.invqty, 2);
                    item.invrate = Math.Round(item.invamt / item.invqty, 6);
                    item.invrate2 = Math.Round(item.invamt / item.invqty2, 0);
                    item.invdisamt = (GrandTotal1 == 0 ? 0 : Math.Round(DiscTotal1 / GrandTotal1 * item.invamt, 6));
                    item.invnetamt = Math.Round(item.invamt - item.invdisamt, 6);
                    item.invvatamt = Math.Round(item.invvatper / 100.00m * item.invamt, 6);
                    if (item.trunit.Trim().ToUpper() == item.trunit2.Trim().ToUpper())
                    {
                        item.truid = string.Empty;
                        item.invrmrk = string.Empty;
                    }
                    else
                    {
                        item.truid = item.invqty2.ToString("#,##0.00") + " " + item.trunit2 + ", " + item.invqty3.ToString("#,##0.00") + " " + item.trunit3;// (item.invqty / item.siruconf).ToString("#,##0.00") + " " + item.trunit2;
                        item.invrmrk = "Rate /" + item.trunit2 + " = " + item.invrate2.ToString("#,##0.00") + ", " + "Rate /" + item.trunit3 + " = " + item.invrate3.ToString("#,##0.00");// Math.Round((item.invamt / (item.invqty / item.siruconf)), 0).ToString("#,##0.00");
                    }
                }

                decimal TotalQty = this.ListViewItemTable1.Sum(x => x.invqty);
                decimal TotalQty2 = this.ListViewItemTable1.Sum(x => x.invqty2);
                decimal TotalQty2Tin = this.ListViewItemTable1.FindAll(x => x.rsircode.Substring(0, 7) == "0151008" && x.trdesc.ToUpper().Trim().Contains("TIN")).Sum(x => x.invqty2);
                decimal TotalQty3Redg = this.ListViewItemTable1.FindAll(x => x.rsircode.Substring(0, 7) == "0151009" && x.trdesc.ToUpper().Trim().Contains("REDGING")).Sum(x => x.invqty2);
                decimal TotalQty4Hond = this.ListViewItemTable1.FindAll(x => x.rsircode.Substring(0, 7) == "0151049" && x.trunit2.ToUpper().Trim().Contains("HOND")).Sum(x => x.invqty2);
                decimal TotalQty5Sheet = this.ListViewItemTable1.FindAll(x => x.rsircode.Substring(0, 9) == "015100806" && x.trdesc.ToUpper().Trim().Contains("SHEET")).Sum(x => x.invqty);
                decimal TotalQty6Rod = this.ListViewItemTable1.FindAll(x => x.rsircode.Substring(0, 7) == "0151001" && x.trdesc.ToUpper().Trim().Contains("M.M")).Sum(x => x.invqty);
                TotalQty3Redg = (TotalQty3Redg / 25.00m);
                //REDGING - 0.21 mm (Elephant) NOF 

                decimal TotalWeight = this.ListViewItemTable1.Sum(x => x.invweight);

                decimal NetTotal = this.ListViewItemTable1.Sum(x => x.invnetamt);
                decimal VATTotal = this.ListViewItemTable1.Sum(x => x.invvatamt);
                decimal TotalBill = GrandTotal1 - DiscTotal1 + VATTotal + Carring1 + Labour1;
                decimal TotalPaid = decimal.Parse("0" + this.txtPaidAmt.Text.Trim());
                decimal ChangeCash = ((TotalPaid < TotalBill) ? 0.00m : (TotalPaid - TotalBill));
                decimal BalanceBill = ((TotalBill < TotalPaid) ? 0.00m : (TotalBill - TotalPaid));

                //----Correction Request by Client ----------------
                // For Accounting Entry and Trading House ChangeCash must be alwais 0.00 So
                ChangeCash = 0.00m;
                BalanceBill = (TotalBill - TotalPaid);
                //------------------------------------------------

                this.lblPaidPercent.Content = Math.Round((TotalBill == 0 ? 0 : (TotalPaid - ChangeCash) / TotalBill * 100.00m), 2).ToString("##0.00") + " %";
                this.lblNetBalPercent.Content = Math.Round((TotalBill == 0 ? 0 : BalanceBill / TotalBill * 100.00m), 2).ToString("##0.00") + " %";

                this.lblTotalQty.Content = TotalQty.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                this.lblTotalQty2.Content = TotalQty2.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                string TotalTinRedg = (TotalQty2Tin > 0 || TotalQty3Redg > 0 ? "Total " : "");
                TotalTinRedg = (TotalQty2Tin > 0 ? "Tin. " + TotalQty2Tin.ToString("#,##0.00;(#,##0.00); - ") + " Ban" : "");
                TotalTinRedg = TotalTinRedg + (TotalTinRedg.Length > 0 && TotalQty3Redg > 0 ? ", " : "");
                TotalTinRedg = TotalTinRedg + (TotalQty3Redg > 0 ? "Red. " + TotalQty3Redg.ToString("#,##0.00;(#,##0.00); - ") + " Ban" : "");
                TotalTinRedg = TotalTinRedg + (TotalTinRedg.Length > 0 && TotalQty4Hond > 0 ? ", " : "");
                TotalTinRedg = TotalTinRedg + (TotalQty4Hond > 0 ? "Screw/T.k. " + TotalQty4Hond.ToString("#,##0.00;(#,##0.00); - ") + " Hond" : "");
                TotalTinRedg = TotalTinRedg + (TotalTinRedg.Length > 0 && TotalQty5Sheet > 0 ? ", " : "");
                TotalTinRedg = TotalTinRedg + (TotalQty5Sheet > 0 ? "Datex " + TotalQty5Sheet.ToString("#,##0;(#,##0); - ") + " Pcs" : "");
                TotalTinRedg = TotalTinRedg + (TotalTinRedg.Length > 0 && TotalQty6Rod > 0 ? ", " : "");
                TotalTinRedg = TotalTinRedg + (TotalQty6Rod > 0 ? "Rod " + TotalQty6Rod.ToString("#,##0.00;(#,##0.00); - ") + " Kg" : "");

                this.lblTinTotalBan.Content = TotalTinRedg;

                //this.lblTinTotalBan.Content = TotalQty2Tin.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                //this.lblRedgTotalBan.Content = TotalQty3Redg.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                this.lblTotalWeight.Content = TotalWeight.ToString("#,##0.00;(#,##0.00); - ");// " -  ";

                this.lblGrandTotal.Content = GrandTotal1.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                this.lblDiscTotal.Content = DiscTotal1.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                this.lblNetTotal.Content = NetTotal.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                this.lblVATTotal.Content = VATTotal.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                this.lblTotalBill.Content = TotalBill.ToString("#,##0;(#,##0); - "); //TotalBill.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                this.stkpVAT.Visibility = (VATTotal > 0 ? Visibility.Visible : Visibility.Hidden);

                this.lblDrAmount1.Content = "0.00";
                this.lblCrAmount1.Content = "0.00";
                this.lblDrAmount2.Content = "0.00";
                this.lblCrAmount2.Content = "0.00";
                if (TotalPaid >= TotalBill)
                    this.cmbPayType.SelectedIndex = 0;
                else if (TotalPaid > 0)
                    this.cmbPayType.SelectedIndex = 1;
                else if (TotalPaid == 0)
                    this.cmbPayType.SelectedIndex = 2;


                string Tag1 = ((ComboBoxItem)this.cmbPayType.SelectedItem).Tag.ToString();
                switch (Tag1)
                {
                    //case "FULLPAY": // For Wholesale Trading House All Customer Should be under A/c Receible First the Cash Collection
                    //    this.lblDrAmount1.Content = (TotalPaid - ChangeCash).ToString("#,##0;(#,##0);0.00");   // " -  ";
                    //    this.lblCrAmount1.Content = (TotalPaid - ChangeCash).ToString("#,##0;(#,##0);0.00");   // " -  ";
                    //    break;
                    case "FULLPAY":
                    case "PARTPAY":
                        this.lblDrAmount1.Content = TotalPaid.ToString("#,##0;(#,##0);0.00");   // " -  ";
                        this.lblCrAmount1.Content = TotalPaid.ToString("#,##0;(#,##0);0.00");   // " -  ";
                        this.lblDrAmount2.Content = TotalBill.ToString("#,##0;(#,##0);0.00");   // " -  ";
                        this.lblCrAmount2.Content = TotalBill.ToString("#,##0;(#,##0);0.00");   // " -  ";
                        break;
                    case "CREDIT":
                        this.lblDrAmount2.Content = TotalBill.ToString("#,##0;(#,##0);0.00");   // " -  ";
                        this.lblCrAmount2.Content = TotalBill.ToString("#,##0;(#,##0);0.00");   // " -  ";
                        break;
                }

                this.lblNetBalance.Content = BalanceBill.ToString("#,##0;(#,##0); - ");// " -  ";            
                this.lblChangeCash.Content = ChangeCash.ToString("#,##0.00;(#,##0.00); - ");// " -  ";            

                int serialno1 = 1;
                foreach (var item in this.ListViewItemTable1)
                {
                    item.trsl = serialno1.ToString() + ".";
                    ++serialno1;
                }
                this.dgvMemo.ItemsSource = this.ListViewItemTable1;
                this.dgvMemo.Items.Refresh();

                
                //------ Draft information update option is enabled (Generally for local/high avaliability of database)
                if (this.chkAllowDraft.IsChecked == true)
                    this.UpdateDraftGSIInformation();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.20: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnPrint2Voucher1_Click(object sender, RoutedEventArgs e)
        {
            string memoNum = this.lblVouNo1.Tag.ToString().Trim();
            string frmname = "VOUCHER";

            this.PrintVoucherMemo(memoNum, true, frmname);
        }

        private void btnPrint2Voucher2_Click(object sender, RoutedEventArgs e)
        {
            string memoNum = this.lblVouNo2.Tag.ToString().Trim();
            string frmname = "VOUCHER";
            this.PrintVoucherMemo(memoNum, true, frmname);
        }

        private void btnUpdateVoucher1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //new AccVoucherType ( "Cash Receipt Voucher", "RVC81" ),
                //new AccVoucherType ( "Bank Receipt Voucher", "RVB81" ),

                string cactcod1 = ((ComboBoxItem)this.cmbDrAccHead1.SelectedValue).Tag.ToString();
                string VouType1 = (cactcod1.Substring(0, 4) == "1901" ? "RVC81" : "RVB81");
                string CrAcCode1 = ((ComboBoxItem)this.cmbCrAccHead1.SelectedValue).Tag.ToString();
                string CustCode1 = this.autoCustSearch.SelectedValue.ToString();
                string sectcod1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();
                //--------------------

                string cheqbookid1 = "XXXXXXXXXXXXXXXXXX";//  "190200030001151201";
                string cheqno1 = "";

                string vounum1 = VouType1.Substring(0, 3) + DateTime.Parse(this.xctk_dtpSalDat.Text).ToString("yyyyMM") +
                                ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim().Substring(0, 4);

                string EditVounum1 = "";
                string Challan1 = "";
                //Challan1 = (Challan1.Length == 0 ? "" : "Challan: " + Challan1 + ", Dated: " + this.xctk_dtchlndat.Text.Trim());
                //Challan1 = "MRR # " + this.txtTransID.Text.ToString() + " " + Challan1;

                var vouPrInfo1 = new vmEntryVoucher1.VouPrInfo()
                {
                    vounum = (EditVounum1.Length > 0 ? EditVounum1 : vounum1),
                    voudat = DateTime.Parse(this.xctk_dtpSalDat.Text),
                    vouref = Challan1,
                    cheqbookid = cheqbookid1,
                    chqref = cheqno1, //((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim(),
                    advref = this.txtChequeNo.Text.Trim(),
                    vounar = this.txtMemoNar.Text.Trim(),
                    curcod = "CBCICOD01001",
                    curcnv = 1.00m,
                    vstatus = "A",
                    recndt = DateTime.Parse("01-Jan-1900"),
                    vtcode = VouType1.Substring(3, 2),
                };

                var ListVouTable1 = new List<vmEntryVoucher1.VouTable>();
                ListVouTable1.Add(new vmEntryVoucher1.VouTable()
                {
                    trnsl = 0,
                    DrCrOrder = "01",
                    cactcode = cactcod1,
                    sectcod = sectcod1, //((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim(),
                    actcode = CrAcCode1, //((ComboBoxItem)this.cmbDrAccHead1.SelectedItem).Tag.ToString().Trim(),
                    sircode = CustCode1, //"000000000000", //this.AtxtssirCod.Value,
                    reptsl = "000",
                    sircode2 = "000000000000",
                    cactcodeDesc = "",
                    sectcodDesc = "",
                    actcodeDesc = "",
                    sircodeDesc = "",
                    sircode2Desc = "",
                    trnDesc = "",
                    trnqty = 0.00m,
                    trnUnit = "",
                    trnrate = 0.00m,
                    dramt = 0.00m,
                    cramt = decimal.Parse("0" + this.lblDrAmount1.Content.ToString().Replace(",", "")),
                    trnam = decimal.Parse("0" + this.lblDrAmount1.Content.ToString().Replace(",", "")) * -1.00m,
                    trnrmrk = ""
                });

                var ListVouTable1u = ListVouTable1.FindAll(x => x.actcode != "000000000000");
                DataSet ds1 = vm1acc.GetDataSetForUpdate(WpfProcessAccess.CompInfList[0].comcod, vouPrInfo1, ListVouTable1u, _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode,
                            _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);
                var pap1 = vm1acc.SetParamUpdateVoucher(WpfProcessAccess.CompInfList[0].comcod, ds1, EditVounum1);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                this.lblVouNo1.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                this.lblVouNo1.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();

                this.cmbDrAccHead1.IsEnabled = false;
                this.cmbCrAccHead1.IsEnabled = false;
                this.btnUpdateVoucher1.IsEnabled = false;
                this.btnPrint2Voucher1.Visibility = Visibility.Visible;

                if (this.stkpAccVoucher1.Visibility == Visibility.Visible && this.stkpAccVoucher2.Visibility == Visibility.Visible)
                    this.btnUpdateVoucher2_Click(null, null);

                //--------------------
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.26: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnUpdateVoucher2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //new AccVoucherType ( "A/c Receivable Journal Voucher", "JVR91" ),
                string VouType2 = "JVR91";

                string cactcod2 = "000000000000";
                string DrAcCode2 = ((ComboBoxItem)this.cmbDrAccHead2.SelectedValue).Tag.ToString();
                string CrAcCode2 = ((ComboBoxItem)this.cmbCrAccHead2.SelectedValue).Tag.ToString();
                string CustCode2 = this.autoCustSearch.SelectedValue.ToString();
                string sectcod2 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();

                //--------------------

                string cheqbookid2 = "XXXXXXXXXXXXXXXXXX";//  "190200030001151201";
                string cheqno2 = "";


                string vounum2 = VouType2.Substring(0, 3) + DateTime.Parse(this.xctk_dtpSalDat.Text).ToString("yyyyMM") +
                                ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim().Substring(0, 4);

                string EditVounum2 = "";
                string Challan2 = "";
                //Challan1 = (Challan1.Length == 0 ? "" : "Challan: " + Challan1 + ", Dated: " + this.xctk_dtchlndat.Text.Trim());
                //Challan1 = "MRR # " + this.txtTransID.Text.ToString() + " " + Challan1;

                var vouPrInfo2 = new vmEntryVoucher1.VouPrInfo()
                {
                    vounum = (EditVounum2.Length > 0 ? EditVounum2 : vounum2),
                    voudat = DateTime.Parse(this.xctk_dtpSalDat.Text),
                    vouref = Challan2,
                    cheqbookid = cheqbookid2,
                    chqref = cheqno2, //((ComboBoxItem)this.cmbCheqNo.SelectedItem).Tag.ToString().Trim(),
                    advref = "",
                    vounar = this.txtMemoNar.Text.Trim(),
                    curcod = "CBCICOD01001",
                    curcnv = 1.00m,
                    vstatus = "A",
                    recndt = DateTime.Parse("01-Jan-1900"),
                    vtcode = VouType2.Substring(3, 2),
                };

                var ListVouTable2 = new List<vmEntryVoucher1.VouTable>();
                ListVouTable2.Add(new vmEntryVoucher1.VouTable()
                {
                    trnsl = 0,
                    DrCrOrder = "01",
                    cactcode = cactcod2,
                    sectcod = sectcod2, //((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim(),
                    actcode = DrAcCode2, //((ComboBoxItem)this.cmbDrAccHead1.SelectedItem).Tag.ToString().Trim(),
                    sircode = CustCode2, //"000000000000", //this.AtxtssirCod.Value,
                    reptsl = "000",
                    sircode2 = "000000000000",
                    cactcodeDesc = "",
                    sectcodDesc = "",
                    actcodeDesc = "",
                    sircodeDesc = "",
                    sircode2Desc = "",
                    trnDesc = "",
                    trnqty = 0.00m,
                    trnUnit = "",
                    trnrate = 0.00m,
                    dramt = decimal.Parse("0" + this.lblDrAmount2.Content.ToString().Replace(",", "")),
                    cramt = 0.00m,
                    trnam = decimal.Parse("0" + this.lblDrAmount2.Content.ToString().Replace(",", "")),
                    trnrmrk = ""
                });

                if (cactcod2 == "000000000000")
                {
                    ListVouTable2.Add(new vmEntryVoucher1.VouTable()
                    {
                        trnsl = 0,
                        DrCrOrder = "01",
                        cactcode = cactcod2,
                        sectcod = sectcod2, //((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim(),
                        actcode = CrAcCode2, //((ComboBoxItem)this.cmbCrAccHead1.SelectedItem).Tag.ToString().Trim(),
                        sircode = this.autoCustSearch.SelectedValue.ToString(),
                        reptsl = "000",
                        sircode2 = "000000000000",
                        cactcodeDesc = "",
                        sectcodDesc = "",
                        actcodeDesc = "",
                        sircodeDesc = "",
                        sircode2Desc = "",
                        trnDesc = "",
                        trnqty = 0.00m,
                        trnUnit = "",
                        trnrate = 0.00m,
                        dramt = 0.00m,
                        cramt = decimal.Parse("0" + this.lblCrAmount2.Content.ToString().Replace(",", "")),
                        trnam = decimal.Parse("0" + this.lblCrAmount2.Content.ToString().Replace(",", "")) * -1.00m,
                        trnrmrk = ""
                    });
                }

                var ListVouTable2u = ListVouTable2.FindAll(x => x.actcode != "000000000000");
                DataSet ds1 = vm1acc.GetDataSetForUpdate(WpfProcessAccess.CompInfList[0].comcod, vouPrInfo2, ListVouTable2u,
                    _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);
                var pap1 = vm1acc.SetParamUpdateVoucher(WpfProcessAccess.CompInfList[0].comcod, ds1, EditVounum2);
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                this.lblVouNo2.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                this.lblVouNo2.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();

                this.btnUpdateVoucher2.IsEnabled = false;
                this.cmbDrAccHead2.IsEnabled = false;
                this.cmbCrAccHead2.IsEnabled = false;
                this.btnPrint2Voucher2.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.27: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void PrintVoucherMemo(string memoNum, bool DirectPrint = false, string prnFrom = "VOUCHER")
        {
            try
            {
                LocalReport rpt1 = null;
                string WindowTitle1 = "";
                var pap1 = vmrptAcc.SetParamAccVoucher(WpfProcessAccess.CompInfList[0].comcod, memoNum, prnFrom);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var list1 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccVoucher1>();
                var trnsList = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTransectionList>();
                // select preparebyid, PreparByName, prepareses, preparetrm, rowid, rowtime, ServerTime = getdate() from #tblv1
                string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                                    + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
                string memoName = memoNum.Substring(0, 3).Trim();
                byte[] comlogoBytes = WpfProcessAccess.CompInfList[0].comlogo;

                var list4 = new HmsEntityAccounting.AccVoucher1p();
                list4.comlogo = comlogoBytes;
                list4.inWord = ASITFunLib.ASITUtility.Trans(double.Parse(list1.Sum(q => q.cramt).ToString()), 2);
                //l.inWord = ASITFunLib.ASITUtility2.UppercaseWords("");
                string rptName = (prnFrom == "VOUCHER" ? "Accounting.RptAccVou1" : (prnFrom == "CHEQUE" ? "Accounting.RptAccPayCheq1" : (prnFrom == "MRECEIPT" ? "Accounting.RptAccMReceipt1" : "")));
                // (list1.Count > 7 ? "Accounting.RptAccVou1" : "Accounting.RptAccVou1h");
                rpt1 = AccReportSetup.GetLocalReport(rptName, list1, trnsList, list3, list4);
                //rpt1.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(bytes)));
                WindowTitle1 = (prnFrom == "VOUCHER" ? "Accounts Voucher" : (prnFrom == "CHEQUE" ? "Payment/Transfer Cheque" : (prnFrom == "MRECEIPT" ? "Money Receipt" : "")));
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-29: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnSetDispPer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.btnTotal_Click(null, null);

                string siaper1 = this.iudDisPer.Value.ToString();
                decimal disper1 = decimal.Parse("0" + siaper1) / 100.00m;
                this.txtDiscAmt.Text = Math.Round(decimal.Parse("0" + this.lblGrandTotal.Content.ToString()) * disper1, 0).ToString("#,##0.00");
                this.chkDiscount.IsChecked = false;
                this.chkDiscount_Click(null, null);
                this.btnTotal_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.22: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void chkDiscount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isChecked1 = (this.chkDiscount.IsChecked == true);
                this.iudDisPer.Value = 0;
                this.iudDisPer.IsEnabled = isChecked1;
                this.btnSetDispPer.IsEnabled = isChecked1;
                if (this.iudDisPer.IsEnabled == true)
                    this.iudDisPer.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.21: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void cmbPayType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!this.FrmInitialized)
                    return;

                this.cmbDrAccHead1.Items.Clear();
                this.cmbCrAccHead1.Items.Clear();
                this.cmbDrAccHead2.Items.Clear();
                this.cmbCrAccHead2.Items.Clear();
                this.cmbDrAccHead1.Items.Clear();
                this.cmbCrAccHead1.Items.Clear();
                this.stkpAccVoucher1.Visibility = Visibility.Hidden;
                this.stkpAccVoucher2.Visibility = Visibility.Hidden;

                string Tag1 = ((ComboBoxItem)this.cmbPayType.SelectedItem).Tag.ToString();
                switch (Tag1)
                {
                    //case "FULLPAY": // For Wholesale Trading House All Customer Should be under A/c Receible First the Cash Collection
                    //    foreach (var item in this.AcCodeList1)
                    //    {
                    //        if (item.actcode.Substring(0, 2) == "19")
                    //            this.cmbDrAccHead1.Items.Add(new ComboBoxItem() { Content = item.actdesc, Tag = item.actcode });
                    //        else if (item.actcode.Substring(0, 8) == "31010001")
                    //            this.cmbCrAccHead1.Items.Add(new ComboBoxItem() { Content = item.actdesc, Tag = item.actcode });
                    //    }
                    //    this.stkpAccVoucher1.Visibility = Visibility.Visible;
                    //    break;
                    case "FULLPAY":
                    case "PARTPAY":
                        foreach (var item in this.AcCodeList1)
                        {
                            if (item.actcode.Substring(0, 4) == "1901")
                                this.cmbDrAccHead1.Items.Add(new ComboBoxItem() { Content = item.actdesc, Tag = item.actcode });
                            else if (item.actcode.Substring(0, 2) == "18")
                            {
                                this.cmbCrAccHead1.Items.Add(new ComboBoxItem() { Content = item.actdesc, Tag = item.actcode });
                                this.cmbDrAccHead2.Items.Add(new ComboBoxItem() { Content = item.actdesc, Tag = item.actcode });
                            }
                            else if (item.actcode.Substring(0, 8) == "31010001")
                                this.cmbCrAccHead2.Items.Add(new ComboBoxItem() { Content = item.actdesc, Tag = item.actcode });
                        }

                        this.stkpAccVoucher1.Visibility = Visibility.Visible;
                        this.stkpAccVoucher2.Visibility = Visibility.Visible;
                        break;
                    case "CREDIT":
                        foreach (var item in this.AcCodeList1)
                        {
                            if (item.actcode.Substring(0, 2) == "18")
                            {
                                this.cmbDrAccHead2.Items.Add(new ComboBoxItem() { Content = item.actdesc, Tag = item.actcode });
                            }
                            else if (item.actcode.Substring(0, 8) == "31010001")
                                this.cmbCrAccHead2.Items.Add(new ComboBoxItem() { Content = item.actdesc, Tag = item.actcode });
                        }
                        this.stkpAccVoucher2.Visibility = Visibility.Visible;
                        break;
                }

                this.cmbDrAccHead1.SelectedIndex = 0;
                this.cmbCrAccHead1.SelectedIndex = 0;

                this.cmbDrAccHead2.SelectedIndex = 0;
                this.cmbCrAccHead2.SelectedIndex = 0;
                this.btnTotal_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.28: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void OpenGridCalc2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.CalcObjName = ((Xceed.Wpf.Toolkit.WatermarkTextBox)sender).Name.ToUpper();
            this.gridCalc1.Visibility = Visibility.Visible;
            this.txtExprToCalc.Text = "";
            this.txtExprToCalc.Focus();
        }

        private void txtPaidAmt_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
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
                string memoType1 = ((ComboBoxItem)this.cmbPrnMemoType2.SelectedItem).Tag.ToString();
                this.ViewPrintMemo(MemoNum, PrnOpt1, "", memoType1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnPrintTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.MemoSaved == false)
                    this.btnUpdateTrans_Click(null, null);

                if (this.MemoSaved == false)
                    return;

                string MemoNum = this.txtTransID.Tag.ToString();
                string PrnOpt1 = "View";// (this.chkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                string memoType1 = ((ComboBoxItem)this.cmbPrnMemoType.SelectedItem).Tag.ToString();
                this.ViewPrintMemo(MemoNum, PrnOpt1, "", memoType1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.18: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void ViewPrintMemo(string memoNum = "XXXXXXXX", string ViewPrint = "View", string Duplicate = "", string memoType1 = "Sales Invoice")
        {
            try
            {
                //string memoNum = ((ComboBoxItem)this.cmbPrevInvList.SelectedItem).Tag.ToString();

                //string memoType1 = ((ComboBoxItem)this.cmbPrnMemoType.SelectedItem).Tag.ToString();
                LocalReport rpt1 = null;
                string WindowTitle1 = (memoType1 == "CHALLAN" ? "Challan" : "Sales Invoice");

                var pap1 = vm2.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                this.lblTokenSlNo.Content = ds1.Tables[2].Rows[0]["tokenid"].ToString().Trim();
                ds1.Tables[0].Rows[0]["slnum"] = Convert.ToInt32(ds1.Tables[2].Rows[0]["tokenid"]);
                //var list3 = WpfProcessAccess.GetRptGenInfo(InputSource: "Test Input Source\n");

                /*
                    string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                                    + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                    var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
                 */

                string inputSource = ds1.Tables[0].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[0].Rows[0]["invbyName"].ToString().Trim()
                                + ", " + ds1.Tables[0].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[0].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);

                //list3[0].RptFooter1 = "User : " + WpfProcessAccess.SignedInUserList[0].signinnam;

                //string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                //                    + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                //var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);

                var list1 = ds1.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
                var list2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                // var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();

                // For POS Printer
                //rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesInv01", list1, list2, list3);

                // For A4 Printer
                byte[] comlogoBytes = WpfProcessAccess.CompInfList[0].comlogo;

                Hashtable list4 = new Hashtable();
                list4["comlogo"] = comlogoBytes;
                list4["inWord"] = ASITFunLib.ASITUtility.Trans(double.Parse(Math.Round(list1.Sum(q => q.inetam), 0).ToString()), 2);

                decimal TotalWeight = 0.00m;//  list1.Sum(x => x.invweight);
                foreach (var item in list1)
                {
                    var itmcod1 = this.RetSaleItemList.FindAll(x => x.sircode == item.rsircode);
                    if (itmcod1.Count > 0)
                        TotalWeight = TotalWeight + item.invqty * itmcod1[0].refscomp;
                }


                //-- Start of Weight Calculation
                decimal TotalQty2Tin = list1.FindAll(x => x.rsircode.Substring(0, 7) == "0151008" && x.sirdesc.ToUpper().Trim().Contains("TIN")).Sum(x => x.invqty / (x.siruconf == 0 ? 1 : x.siruconf));
                decimal TotalQty3Redg = list1.FindAll(x => x.rsircode.Substring(0, 7) == "0151009" && x.sirdesc.ToUpper().Trim().Contains("REDGING")).Sum(x => x.invqty);
                decimal TotalQty4Hond = list1.FindAll(x => x.rsircode.Substring(0, 7) == "0151049" && x.sirunit2.ToUpper().Trim().Contains("HOND")).Sum(x => x.invqty / (x.siruconf == 0 ? 1 : x.siruconf));
                decimal TotalQty5Sheet = list1.FindAll(x => x.rsircode.Substring(0, 9) == "015100806" && x.sirdesc.ToUpper().Trim().Contains("SHEET")).Sum(x => x.invqty);
                decimal TotalQty6Rod = list1.FindAll(x => x.rsircode.Substring(0, 7) == "0151001" && x.sirdesc.ToUpper().Trim().Contains("M.M")).Sum(x => x.invqty);

                TotalQty3Redg = (TotalQty3Redg / 25.00m);    //0151049         //REDGING - 0.21 mm (Elephant) NOF 

                string TotalTinRedg = (TotalQty2Tin > 0 || TotalQty3Redg > 0 ? "Total " : "");
                TotalTinRedg = (TotalQty2Tin > 0 ? "Tin. " + TotalQty2Tin.ToString("#,##0.00;(#,##0.00); - ") + " Ban" : "");
                TotalTinRedg = TotalTinRedg + (TotalTinRedg.Length > 0 && TotalQty3Redg > 0 ? ", " : "");
                TotalTinRedg = TotalTinRedg + (TotalQty3Redg > 0 ? "Red. " + TotalQty3Redg.ToString("#,##0.00;(#,##0.00); - ") + " Ban" : "");
                TotalTinRedg = TotalTinRedg + (TotalTinRedg.Length > 0 && TotalQty4Hond > 0 ? ", " : "");
                TotalTinRedg = TotalTinRedg + (TotalQty4Hond > 0 ? "Screw/T.k. " + TotalQty4Hond.ToString("#,##0.00;(#,##0.00); - ") + " Hond" : "");
                TotalTinRedg = TotalTinRedg + (TotalTinRedg.Length > 0 && TotalQty5Sheet > 0 ? ", " : "");
                TotalTinRedg = TotalTinRedg + (TotalQty5Sheet > 0 ? "Datex " + TotalQty5Sheet.ToString("#,##0;(#,##0); - ") + " Pcs" : "");
                TotalTinRedg = TotalTinRedg + (TotalTinRedg.Length > 0 && TotalQty6Rod > 0 ? ", " : "");
                TotalTinRedg = TotalTinRedg + (TotalQty6Rod > 0 ? "Rod " + TotalQty6Rod.ToString("#,##0.00;(#,##0.00); - ") + " Kg" : "");


                list2[0].invnar = list2[0].invnar + (list2[0].invnar.Trim().Length > 0 && TotalTinRedg.Length > 0 ? "\n" : "") +
                                  (TotalWeight > 0.00m ? "Total Weight : " + TotalWeight.ToString("#,##0.00;(#,##0.00); - ") + " Kg\n" : "") + TotalTinRedg;
                //-- End of Weight Calculation
                //-------------------------------------------------

                //var pap1a = vmrptAcc.SetParamAccSubLedger(WpfProcessAccess.CompInfList[0].comcod, fromDate, ToDate, AccCodeHead, AccSubCodeHead, Nar);

                var pap1a = vmrptAcc.SetParamAccSubLedger(WpfProcessAccess.CompInfList[0].comcod, list2[0].invdat.ToString("dd-MMM-yyyy"), list2[0].invdat.ToString("dd-MMM-yyyy"), "180100010001", list2[0].custid, "XXXXX");
                DataSet ds1a = WpfProcessAccess.GetHmsDataSet(pap1a);
                if (ds1a == null)
                    return;

                decimal PrevBal1 = 0.00m;
                int recored1 = ds1a.Tables[0].Rows.Count - 1;
                if (recored1 >= 0)
                    PrevBal1 = Convert.ToDecimal(ds1a.Tables[0].Rows[recored1]["dram"]) - Convert.ToDecimal(ds1a.Tables[0].Rows[recored1]["cram"]);

                //-------------------------------------------------

                string CustID1 = list2[0].custid;// this.autoCustSearch.SelectedValue.ToString();
                list4["cuatnam"] = list2[0].custName.Trim();
                list4["cuatAdd"] = list2[0].invnar.Trim();
                list4["cuatTel"] = "";
                list4["prevbal"] = PrevBal1;
                var CustDetails = this.RegCustDetailsList.FindAll(x => x.tblcode == CustID1);
                if (CustDetails.Count > 0)
                {
                    var Custnam1 = CustDetails.FindAll(x => x.gencode == "SICD00101001");
                    if (Custnam1.Count > 0)
                        list4["cuatnam"] = Custnam1[0].dataval.Trim();

                    var Custadd1 = CustDetails.FindAll(x => x.gencode == "SICD00101003");
                    string Address1 = "  ";
                    foreach (var item in Custadd1)
                    {
                        Address1 = Address1 + item.dataval.Trim() + ", ";
                    }
                    Address1 = Address1.Substring(0, Address1.Length - 2).Trim();
                    if (Address1.Length > 0)
                        list4["cuatAdd"] = Address1;

                    var Custphon1 = CustDetails.FindAll(x => x.gencode == "SICD00101004");
                    string Phone1 = "  ";
                    foreach (var item in Custphon1)
                    {
                        Phone1 = Phone1 + item.dataval.Trim() + ", ";
                    }
                    Phone1 = Phone1.Substring(0, Phone1.Length - 2).Trim();
                    list4["cuatTel"] = Phone1;
                }

                if (list4["cuatAdd"].ToString().Trim() == list2[0].invnar.Trim())
                    list2[0].invnar = "";

                //list4["cuatAdd"] = (list4["cuatAdd"].ToString().Trim().Length ==0 ?  "" : list4["cuatAdd"]);

                //HmsEntityAccounting.AccVoucher1p list4a = new HmsEntityAccounting.AccVoucher1p();
                //list4a.comlogo = comlogoBytes;
                //list4a.inWord = ASITFunLib.ASITUtility.Trans(double.Parse(list1.Sum(q => q.inetam).ToString()), 2);

                list4["memoType"] = memoType1;
                list4["memoTitle"] = WindowTitle1;
                if (memoType1 == "CHALLAN")
                    rpt1 = CommReportSetup.GetLocalReport("RetSales.RetSalesChallan01", list1, list2, list3, list4);
                else
                    rpt1 = CommReportSetup.GetLocalReport("RetSales.RetSalesInv01", list1, list2, list3, list4);

                if (Duplicate.Length > 0)
                {
                    rpt1.SetParameters(new ReportParameter("ParamAddress1", "[Re-Print/Duplicate Invoice]"));
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
                    if (Duplicate.Length == 0)
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
                System.Windows.MessageBox.Show("GSI-2.19: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
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


                var pap1a = vm1.SetParamInvoiceVouchers(WpfProcessAccess.CompInfList[0].comcod, MemoNum);

                DataSet ds1a = WpfProcessAccess.GetHmsDataSet(pap1a);
                if (ds1a == null)
                    return;

                foreach (DataRow dr1 in ds1a.Tables[0].Rows)
                {
                    string vounum1 = dr1["vounum"].ToString().Trim();
                    var pap1b = vm1acc.SetParamCancelVoucher(WpfProcessAccess.CompInfList[0].comcod, vounum1);
                    DataSet ds1b = WpfProcessAccess.GetHmsDataSet(pap1b);
                    if (ds1b == null)
                        return;
                }

                var pap1 = vm1.SetParamCancelMemo(WpfProcessAccess.CompInfList[0].comcod, MemoNum);

                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;


                this.btnFilter1_Click(null, null);
                //this.TransInvList.RemoveAt(itemno1);
                //this.dgvPrevTransList.Items.Refresh();

                if (this.txtTransID.Tag.ToString().Trim() == MemoNum)
                    this.CleanUpScreen();

                this.dgvPrevTransList.SelectedIndex = (this.dgvPrevTransList.Items.Count <= invidx1 ? this.dgvPrevTransList.Items.Count - 1 : invidx1);

                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.40: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
                this.stkpdgvPrevTransList.IsEnabled = false;
                this.btnPrevTrans.Content = "Prev.Trans.";
                this.btnNewShow.Content = "_New";
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
                this.xcdtDeliveryDT.Value = list2[0].delivartime;
                this.txtTransID.Text = list2[0].invno1;
                this.txtTransID.Tag = list2[0].invno;

                this.txtblEditMode.Visibility = (list2[0].invno != "GSI000000000000000" ? Visibility.Visible : Visibility.Hidden);

                //------------------------------------
                DateTime dat1 = list2[0].invdat;
                dat1 = DateTime.Parse("01-" + dat1.ToString("MMM-yyyy"));
                DateTime dat2 = dat1.AddMonths(1).AddDays(-1);
                this.xctk_dtpSalDat.Minimum = dat1;
                this.xctk_dtpSalDat.Maximum = dat2;
                //---------------------------

                string PayType1 = list2[0].paytype.Trim().ToUpper();

                this.rbtnPayTypeCash.IsChecked = (PayType1 == "CASH" ? true : false);
                this.rbtnPayTypeCheque.IsChecked = (PayType1 == "CHEQUE" ? true : false);
                this.rbtnPayTypeCredit.IsChecked = (PayType1 == "CREDIT" ? true : false);
                this.txtChequeNo.Text = list2[0].invref;
                this.xctk_dtpChqDat.Value = list2[0].invrefdat;
                this.txtMemoNar.Text = list2[0].invnar.Trim();

                this.stkpChequeInfo.Visibility = (PayType1 == "CHEQUE" ? Visibility.Visible : Visibility.Hidden);

                var Carr1 = list1.FindAll(x => x.rsircode == "045100101001");
                var Lab1 = list1.FindAll(x => x.rsircode == "045100102001");

                this.txtCarrAmt.Text = (Carr1.Count > 0 ? Carr1[0].itmam.ToString("#,##0.00") : "");
                this.txtLabAmt.Text = (Lab1.Count > 0 ? Lab1[0].itmam.ToString("#,##0.00") : "");

                decimal DiscAmt1 = list1.Sum(x => x.idisam);
                this.txtDiscAmt.Text = DiscAmt1.ToString("#,##0.00");
                this.txtPaidAmt.Text = list2[0].collam.ToString("#,##0");
                string custid1 = list2[0].custid;
                this.autoCustSearch.ItemsSource = this.RegCustList;
                this.autoCustSearch.SelectedValue = list2[0].custid;
                this.autoCustSearch_LostFocus(null, null);
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

                this.dgvMemo.Items.Refresh();
                this.chkAutoSaveAc_Click(null, null);
                this.btnTotal_Click(null, null);
                
                this.stkpPrint.Visibility = Visibility.Hidden;
                this.btnUpdateTrans.IsEnabled = true;
                this.stkpFinalUpdate.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.38: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnFilter1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.TransInvList == null)
                    return;
                this.stkpdgvPrevTransList.IsEnabled = false;
                string FrmDate1 = this.xctk_dtpSrchDat1.Text.Trim();
                string ToDate1 = this.xctk_dtpSrchDat2.Text.Trim();
                string InvNo1 = this.txtSrchInvNo.Text.Trim();
                string InvStatus1 = "A";
                string Cust1 = (this.chkSrchCust.IsChecked == true && this.autoCustSearch.SelectedValue != null ? this.autoCustSearch.SelectedValue.ToString() : "") + "%";

                this.TransInvList.Clear();

                string sectcod1 = ((ComboBoxItem)this.cmbSectCod.Items[this.cmbSectCod.SelectedIndex]).Tag.ToString();
                var pap1 = vm2.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, "A00MSISUM", FrmDate1, ToDate1, sectcod1, "GSI%" + InvNo1, InvStatus1, Cust1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.TransInvList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();


                foreach (var item1 in this.TransInvList)
                {

                    item1.preparetrm = (item1.custName.Trim().Length > 0 ? item1.custName.Trim() : "") +
                                       (item1.invnar.Trim().Length > 0 && item1.custName.Trim().Length > 0 ? ", " + item1.invnar.Trim() : "");
                }

                this.TransInvList = this.TransInvList.FindAll(x => x.invno.Substring(0, 3) == "GSI").ToList();
                if (this.TransInvList == null)
                    return;

                this.chkSrchCust.IsChecked = false;
                this.txtSrchInvNo.Text = "";

                this.dgvPrevTransList.ItemsSource = this.TransInvList;
                this.dgvPrevTransList.Items.Refresh();
                this.stkpdgvPrevTransList.IsEnabled = true;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.41: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            this.dgvPrevTransList_MouseDoubleClick(null, null);
        }

        private void btnPrevTrans_Click(object sender, RoutedEventArgs e)
        {
            this.stkpDataEntry.Visibility = Visibility.Collapsed;
            this.stkpDataEntry.IsEnabled = false;
            this.GridAccVoucher.Visibility = Visibility.Collapsed;
            this.gridCalc1.Visibility = Visibility.Collapsed;
            if (this.stkpPrevTrans.Visibility == Visibility.Visible)
            {
                this.stkpPrevTrans.Visibility = Visibility.Collapsed;
                this.stkpdgvPrevTransList.IsEnabled = false;
                this.btnPrevTrans.Content = "Prev.Trans.";
                if (this.btnNewShow.Content.ToString() == "_New")
                {
                    this.stkpDataEntry.Visibility = Visibility.Visible;
                    this.stkpDataEntry.IsEnabled = true;
                    this.chkAutoSaveAc_Click(null, null);
                }
                return;
            }
            this.btnPrevTrans.Content = "Hide Trans.";
            this.stkpPrevTrans.Visibility = Visibility.Visible;
            this.stkpdgvPrevTransList.IsEnabled = true;
            if (this.TransInvList.Count == 0)
                this.btnFilter1_Click(null, null);
        }

        private void txtItemRate_LostFocus(object sender, RoutedEventArgs e)
        {
            string ItemId1 = this.txtItemName.Tag.ToString();
            this.ShowInHandStock(ItemId1);
        }
        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Button)sender).Name.ToString() == "btnAddRecord")
                    this.lblSlNo.Content = "xxx";

                this.GridItemList.Visibility = Visibility.Collapsed;

                // Change made on 21-May-2018 --- Hafiz
                //if (this.txtTransID.Text.Trim().Length == 0 && this.chkVerifyStock.IsChecked == true) 
                // this.txtTransID.Text.Trim().Length == 0 means ignore in edit mode

                if (this.chkVerifyStock.IsChecked == true)
                {
                    if (this.lblStockU1h.Content.ToString().Contains("-"))
                    {
                        this.txtDummy1.Visibility = Visibility.Visible;
                        this.txtDummy1.Focus();
                        return;
                    }
                }

                string srchVal1a = this.txtItemName.Tag.ToString().Trim();
                string srchTxt1a = this.txtItemName.Text.Trim();
                this.AddChangeItem(srchVal1: srchVal1a);
                //this.btnClearRecord_Click(null, null);
                this.lblSlNo.Content = "xxx";
                this.btnUpdateRecord.IsEnabled = false;
                this.txtDummy1.Visibility = Visibility.Visible;
                this.txtDummy1.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void AddChangeItem(string srchVal1 = "")
        {
            try
            {
                if (this.MemoSaved == true)
                    return;

                if (srchVal1.Length == 0)
                    return;

                if (this.autoCustSearch.SelectedValue == null)
                    return;

                int rowidx1 = this.dgvMemo.SelectedIndex;

                decimal itmqty1 = decimal.Parse("0" + this.txtItemQty.Text.Trim());
                decimal itmAmt1 = decimal.Parse("0" + this.txtItemAmount.Text.Trim());
                decimal itmRate1 = Math.Round(itmAmt1 / itmqty1, 6);

                decimal siruconf1 = decimal.Parse("0" + ((ComboBoxItem)this.cmbQtyUnit.Items[1]).Uid.ToString());
                decimal siruconf3 = decimal.Parse("0" + ((ComboBoxItem)this.cmbQtyUnit.Items[2]).Uid.ToString());

                decimal itmqty2 = Math.Round(itmqty1 / siruconf1, 6);
                decimal itmqty3 = Math.Round(itmqty1 / siruconf3, 6);

                decimal itmRate2 = Math.Round(itmAmt1 / itmqty2, 6);
                decimal itmRate3 = Math.Round(itmAmt1 / itmqty3, 6);

                string custid1 = this.autoCustSearch.SelectedValue.ToString();
                string reptsl1 = this.lblSlNo.Content.ToString();

                var RetailItemList1 = this.RetSaleItemList.FindAll(x => x.sircode == srchVal1).Take(10);

                foreach (var item in RetailItemList1)
                {
                    #region MyRegion
                    int serialno1 = this.ListViewItemTable1.Count + 1;
                    var ListViewItemTable1a = this.ListViewItemTable1.FindAll(x => x.reptsl == reptsl1);
                    if (ListViewItemTable1a.Count > 0)
                    {
                        ListViewItemTable1a[0].invcode = item.sircode;
                        ListViewItemTable1a[0].rsircode = item.sircode;
                        ListViewItemTable1a[0].trdesc = item.sirdesc.Trim();
                        ListViewItemTable1a[0].mfgid = item.mfgid.Trim();
                        ListViewItemTable1a[0].mfgcomnam = item.mfgcomnam.Trim();
                        ListViewItemTable1a[0].invqty = itmqty1;
                        ListViewItemTable1a[0].truid = "";
                        ListViewItemTable1a[0].trunit = item.sirunit;
                        ListViewItemTable1a[0].invamt = itmAmt1;
                        ListViewItemTable1a[0].invdisamt = 0;
                        ListViewItemTable1a[0].invnetamt = itmAmt1;
                        ListViewItemTable1a[0].invvatper = item.salvatp;
                        ListViewItemTable1a[0].invvatamt = (item.salvatp / 100.00m * itmAmt1);
                        ListViewItemTable1a[0].invrmrk = "";
                        ListViewItemTable1a[0].batchno = item.sircode.Substring(6) + ": " + item.sirdesc.Trim();
                        ListViewItemTable1a[0].mfgdat = DateTime.Today;
                        ListViewItemTable1a[0].expdat = DateTime.Today.AddDays(7);
                        ListViewItemTable1a[0].mfgvisible = item.mfgvisible;
                        ListViewItemTable1a[0].rmrkvisible = item.rmrkvisible;
                        ListViewItemTable1a[0].invqty2 = itmqty2;
                        ListViewItemTable1a[0].invqty3 = itmqty3;
                        ListViewItemTable1a[0].invrate2 = itmRate2;
                        ListViewItemTable1a[0].invrate3 = itmRate3;
                        ListViewItemTable1a[0].invweight = itmqty1 * item.refscomp;
                        ListViewItemTable1a[0].trunit2 = item.sirunit2;
                        ListViewItemTable1a[0].trunit3 = item.sirunit3;
                        ListViewItemTable1a[0].siruconf = item.siruconf;
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
                            mfgid = item.mfgid.Trim(),
                            mfgcomnam = item.mfgcomnam.Trim(),
                            invqty = itmqty1,
                            truid = "",
                            trunit = item.sirunit,
                            invrate = itmRate1,
                            invamt = itmAmt1,
                            invdisamt = 0,
                            invnetamt = itmAmt1,
                            invvatper = item.salvatp,
                            invvatamt = (item.salvatp / 100.00m * itmAmt1),
                            invrmrk = "",
                            batchno = item.sircode.Substring(6) + ": " + item.sirdesc.Trim(),
                            mfgdat = DateTime.Today,
                            expdat = DateTime.Today.AddDays(7),
                            mfgvisible = item.mfgvisible,
                            rmrkvisible = item.rmrkvisible,
                            invqty2 = itmqty2,
                            invqty3 = itmqty3,
                            invrate2 = itmRate2,
                            invrate3 = itmRate3,
                            invweight = itmqty1 * item.refscomp,
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
                this.stkpFinalUpdate.Visibility = Visibility.Visible;
                this.btnTotal_Click(null, null);

                this.dgvMemo.SelectedIndex = rowidx1;
                var item22 = this.ListViewItemTable1.FindAll(x => x.invcode == srchVal1 && x.reptsl == reptsl1);
                if (item22.Count > 0)
                {
                    this.dgvMemo.ScrollIntoView(item22[0]);
                }

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.23: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void dgvMemo_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }

        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgvMemo.SelectedIndex < 0)
                return;

            if (this.ListViewItemTable1.Count == 0)
                return;

            int itemIndex1 = this.dgvMemo.SelectedIndex;

            MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to delete item " + this.ListViewItemTable1[itemIndex1].trsl + " " + this.ListViewItemTable1[itemIndex1].trdesc.Trim(),
                                         WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (msgresult != MessageBoxResult.Yes)
                return;

            this.ListViewItemTable1[itemIndex1].invqty = 0;
            this.btnTotal_Click(null, null);
            if (this.ListViewItemTable1.Count > 0)
            {
                this.dgvMemo.SelectedIndex = (this.ListViewItemTable1.Count <= itemIndex1 ? this.ListViewItemTable1.Count - 1 : itemIndex1);
            }
            this.btnClearRecord_Click(null, null);
        }

        private void btnClearRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtItemName.Text = "";
                this.txtItemName.Tag = "";
                this.txtItemName.ToolTip = "";
                this.lblStockU1a.Content = "";
                this.lblStockU2a.Content = "";
                this.lblStockU3a.Content = "";
                this.txtItemQty.Text = "";
                this.cmbQtyUnit.Items.Clear();
                this.txtItemQty2.Text = "";
                this.txtItemQty2.Tag = "";
                this.txtItemQty3.Text = "";
                this.txtItemQty3.Tag = "";

                this.lblUnit2.Content = "";
                this.lblUnit3.Content = "";
                this.lblStockU3.Content = "";
                this.lblStockU1h.Content = "";
                this.lblStockU2h.Content = "";
                this.lblStockU3h.Content = "";
                this.cmbRateUnit.Items.Clear();
                this.txtItemRate.Text = "";
                this.lblItemRate2.Content = "";
                this.txtItemRate2.Text = "";
                this.lblItemRate3.Content = "";
                this.txtItemRate3.Text = "";
                this.txtItemAmount.Text = "";
                this.lblSlNo.Content = "xxx";
                this.btnUpdateRecord.IsEnabled = false;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.25: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void cmbRateUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbRateUnit.Items.Count == 0)
                return;
            if (this.txtItemName.Tag == null)
                return;

            string ItemId1 = this.txtItemName.Tag.ToString();
            if (this.txtItemRate.Text.Trim().Length == 0)
            {
                this.txtItemRate.Text = decimal.Parse("0" + ((ComboBoxItem)this.cmbRateUnit.SelectedItem).Uid.ToString()).ToString("#,##0.00");
                this.ShowInHandStock(ItemId1);
            }
            else
            {
                this.RecalculateRateInfo();
            }
        }


        private void btnNave_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgvMemo.Items.Count == 0)
                return;
            if (this.dgvMemo.SelectedIndex < 0)
                this.dgvMemo.SelectedIndex = 0;

            string Nav1 = ((Button)sender).Tag.ToString().ToUpper();
            int index1 = this.dgvMemo.SelectedIndex;
            switch (Nav1)
            {
                case "TOP":
                    index1 = 0;
                    break;
                case "PREVIOUS":
                    index1 = this.dgvMemo.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "NEXT":
                    index1 = this.dgvMemo.SelectedIndex + 1;
                    if (index1 >= this.dgvMemo.Items.Count)
                        index1 = this.dgvMemo.Items.Count - 1;
                    break;
                case "BOTTOM":
                    index1 = this.dgvMemo.Items.Count - 1;
                    break;
            }
            this.dgvMemo.SelectedIndex = index1;

            var item21 = (vmEntryPharRestPOS1.ListViewItemTableDetails)this.dgvMemo.Items[index1];
            this.dgvMemo.ScrollIntoView(item21);
        }

        private void btnShowRecord_Click(object sender, RoutedEventArgs e)
        {
            if (this.dgvMemo.Items.Count == 0)
                return;

            if (this.dgvMemo.SelectedIndex < 0)
                return;

            int index1 = this.dgvMemo.SelectedIndex;
            var item1 = this.ListViewItemTable1[index1];
            this.lblSlNo.Content = item1.reptsl;
            this.txtItemName.Text = item1.trdesc;
            this.txtItemName.Tag = item1.rsircode;
            this.txtItemName.ToolTip = this.txtItemName.Tag + " - " + this.txtItemName.Text;
            this.txtItemQty.Text = item1.invqty.ToString("#,##0.00");
            this.txtItemAmount.Text = item1.invamt.ToString("#,##0.00");
            this.ResetUnitRateInfo(item1.rsircode);
            this.txtItemRate.Text = Math.Round((this.cmbRateUnit.SelectedIndex == 0 ? item1.invrate : item1.invrate * item1.siruconf), 0).ToString("#,##0.00");
            this.ShowInHandStock(item1.rsircode);
            this.btnUpdateRecord.IsEnabled = true;
            this.txtItemQty.Focus();
            // Other show code will goes here -- Hafiz 15-Nov-2017

        }

        private void dgvMemo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnShowRecord_Click(null, null);
        }

        private void stkpEntryFooter_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
        }

        private void chkAutoSaveAc_Click(object sender, RoutedEventArgs e)
        {
            if (this.chkAutoSaveAc.IsChecked == true)
                this.GridAccVoucher.Visibility = Visibility.Collapsed;
            else
                this.GridAccVoucher.Visibility = Visibility.Visible;

            if (this.ListViewItemTable1.Count > 0)
                this.btnTotal_Click(null, null);
        }

        private void chkShowDraft_Click(object sender, RoutedEventArgs e)
        {
            this.GridItemList.Visibility = Visibility.Collapsed;
            if (this.chkShowDraft.IsChecked == true)
                this.ShowHideDraftListPanle();

        }
        private void ShowHideDraftListPanle()
        {
            this.dgvDraftList.ItemsSource = null;
            this.DispatcherFrame1 = new DispatcherFrame();
            this.GridDataEntry.IsEnabled = false;
            this.GridDraftList.Visibility = Visibility.Visible;
            this.GridDraftList.IsEnabled = true;
            this.xctk_dtpDraftFrom.Value = DateTime.Parse(this.xctk_dtpSalDat.Text).AddDays(-7);
            this.xctk_dtpDraftTo.Value = this.xctk_dtpSalDat.Value;
            this.txtDraftTrmId.Text = this.preparetrm1;

            this.btnFilterDraftList_Click(null, null);
            System.Windows.Threading.Dispatcher.PushFrame(this.DispatcherFrame1);
            this.GridDataEntry.IsEnabled = true;
            this.GridDraftList.IsEnabled = false;
            this.chkShowDraft.IsChecked = false;
            this.GridDraftList.Visibility = Visibility.Collapsed;
        }

        private void btnCloseDraftList_Click(object sender, RoutedEventArgs e)
        {
            this.DispatcherFrame1.Continue = false; // un-blocks gui message pump
        }

        private void btnFilterDraftList_Click(object sender, RoutedEventArgs e)
        {
            this.dgvDraftList.ItemsSource = null;
            string draftDesc1a = this.txtDraftDesc.Text.Trim();
            string dateFrom = this.xctk_dtpDraftFrom.Text;
            string dateTo = this.xctk_dtpDraftTo.Text;
            string drafttrm1a = this.txtDraftTrmId.Text.Trim();
            string signinnam1a = this.txtDraftUserId.Text.Trim();

            this.DraftTransactionList1 = WpfProcessAccess.GetDraftTransactionList(memohead1: "GSI", draftDesc1: draftDesc1a, signinnam1: signinnam1a, drafttrm1: drafttrm1a,
                DateFrom1: dateFrom, DateTo1: dateTo).ToList();
            this.dgvDraftList.ItemsSource = this.DraftTransactionList1;
        }

        private void btnDraftNav_Click(object sender, RoutedEventArgs e)
        {
            string ActtionName = ((Button)sender).Tag.ToString().Trim();
            this.DraftGridNavigationClick(ActtionName);
        }
        private void DraftGridNavigationClick(string ActtionName = "btnDraftTop")
        {
            if (this.dgvDraftList.Items.Count == 0)
                return;

            if (this.dgvDraftList.SelectedIndex < 0)
                this.dgvDraftList.SelectedIndex = 0;

            int index1 = this.dgvDraftList.SelectedIndex;
            var item1 = (vmHmsGeneralList1.DraftTransactionList)this.dgvDraftList.SelectedItem;
            string draftsl1 = item1.slnum.ToString();
            string draftnum1x = item1.draftnum;
            Int64 rowid1x = item1.rowid;
            string draftDesc1x = item1.draftrmrk.Trim();
            string draftbyid1x = item1.draftbyid;
            string draftses1x = item1.draftses.Trim();
            string drafttrm1x = item1.drafttrm.Trim();

            switch (ActtionName)
            {
                case "btnDraftTop":
                    index1 = 0;
                    break;
                case "btnDraftPrev":
                    index1 = this.dgvDraftList.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnDraftNext":
                    index1 = this.dgvDraftList.SelectedIndex + 1;
                    if (index1 >= this.dgvDraftList.Items.Count)
                        index1 = this.dgvDraftList.Items.Count - 1;
                    break;
                case "btnDraftBottom":
                    index1 = this.dgvDraftList.Items.Count - 1;
                    break;
                case "btnDraftDismiss":
                    if (System.Windows.MessageBox.Show("Confirm Dismiss Selected Draft Sl# " + draftsl1 + ".\n" + draftDesc1x, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        DataSet dsdraft1 = WpfProcessAccess.UpdateDeleteDraftTransaction(UpadateDelete1: "DELETE", ds1: null, draftnum1: draftnum1x, rowid1: rowid1x,
                                draftrmrk1: draftDesc1x, draftbyid1: draftbyid1x, draftses1: draftses1x, drafttrm1: drafttrm1x);
                        if (dsdraft1 != null)
                            this.btnFilterDraftList_Click(null, null);
                    }
                    //return;
                    break;
                case "btnDraftRetrive":
                    if (System.Windows.MessageBox.Show("Confirm to retrive Draft Sl# " + draftsl1 + ".\n" + draftDesc1x, WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                       MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        this.RetriveDraftMemo(MemoNum1a: draftnum1x, rowid1a: rowid1x);
                    }
                    break;
            }
            if (ActtionName == "btnDraftDismiss" || ActtionName == "btnDraftRetrive")
                return;

            this.dgvDraftList.SelectedIndex = index1;

            var item21 = (vmHmsGeneralList1.DraftTransactionList)this.dgvDraftList.Items[index1];
            this.dgvDraftList.ScrollIntoView(item21);
        }

        private void dgvDraftList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.DraftGridNavigationClick(ActtionName: "btnDraftRetrive");
        }     
        
        private void UpdateDraftGSIInformation()
        {
            if (this.autoCustSearch.SelectedValue == null)
                return;

            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();
            string cbSectName1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Content.ToString().Trim();
            if (this.DraftMemoNum.Length == 0)
            {
                if (this.EditDs != null)
                    this.DraftMemoNum = this.txtTransID.Tag.ToString();
                else
                    this.DraftMemoNum = "GSI" + DateTime.Parse(this.xctk_dtpSalDat.Text).ToString("yyyyMM") + cbSectCode1.Trim().Substring(0, 4) + "D0000";
            }

            DataSet ds1 = new DataSet("dsDraft");
            DataTable tbl1b = new DataTable("tblb");
            tbl1b.Columns.Add("invno", typeof(String));
            tbl1b.Columns.Add("invdat", typeof(String));
            tbl1b.Columns.Add("sectcod", typeof(String));
            tbl1b.Columns.Add("paytype", typeof(String));
            tbl1b.Columns.Add("paytype1", typeof(String));
            tbl1b.Columns.Add("invref", typeof(String));
            tbl1b.Columns.Add("invrefdat", typeof(String));
            tbl1b.Columns.Add("custid", typeof(String));
            tbl1b.Columns.Add("delivartime", typeof(String));
            tbl1b.Columns.Add("invnar", typeof(String));
            tbl1b.Columns.Add("labamt", typeof(Decimal));
            tbl1b.Columns.Add("carramt", typeof(Decimal));
            tbl1b.Columns.Add("preparebyid", typeof(String));
            tbl1b.Columns.Add("prepareses", typeof(String));
            tbl1b.Columns.Add("preparetrm", typeof(String));
            /*
             *      string PayType1a = (this.rbtnPayTypeCash.IsChecked == true ? "CASH" : (this.rbtnPayTypeCredit.IsChecked == true ? "CREDIT" : "CHEQUE"));
               <RadioButton x:Name="rbtnPayTypeCash" Content="Cash" Focusable="False" Width="55" VerticalContentAlignment="Center" FontWeight="Bold" Click="rbtnPayType_Click" />
                            <RadioButton x:Name="rbtnPayTypeCredit" Content="Credit" Focusable="False" Width="60" VerticalContentAlignment="Center" FontWeight="Bold" IsChecked="True" Click="rbtnPayType_Click"/>
                            <RadioButton x:Name="rbtnPayTypeCheque" Content="Cheque" Focusable="False" Width="65" VerticalContentAlignment="Center" FontWeight="Bold" Click="rbtnPayType_Click"/>
            */

            DataRow dr1b = tbl1b.NewRow();
            dr1b["invno"] = this.txtTransID.Tag.ToString().Trim();
            dr1b["invdat"] = this.xctk_dtpSalDat.Text.Trim();
            dr1b["sectcod"] = cbSectCode1.Substring(0, 12);
            dr1b["paytype"] = ((ComboBoxItem)this.cmbPayType.SelectedItem).Tag.ToString().Trim();
            string PayType1a = (this.rbtnPayTypeCash.IsChecked == true ? "CASH" : (this.rbtnPayTypeCredit.IsChecked == true ? "CREDIT" : "CHEQUE"));
            dr1b["paytype1"] = PayType1a;
            dr1b["invref"] = this.txtChequeNo.Text.Trim();
            dr1b["invrefdat"] = DateTime.Parse(this.xctk_dtpChqDat.Text.Substring(0, 11));
            dr1b["custid"] = this.autoCustSearch.SelectedValue.ToString();
            dr1b["delivartime"] = this.xcdtDeliveryDT.Text.Trim();
            dr1b["invnar"] = this.txtMemoNar.Text.Trim();
            dr1b["labamt"] = decimal.Parse("0" + this.txtLabAmt.Text.Trim());
            dr1b["carramt"] = decimal.Parse("0" + this.txtCarrAmt.Text.Trim());
            dr1b["preparebyid"] = this.preparebyid1;
            dr1b["prepareses"] = this.prepareses1;
            dr1b["preparetrm"] = this.preparetrm1;

            tbl1b.Rows.Add(dr1b);
            ds1.Tables.Add(tbl1b);

            string rmrk1 = "Invoice" + (this.EditDs != null ? " No: " + this.txtTransID.Text.Trim() + "," : "") + " Date: " + this.xctk_dtpSalDat.Text
                           + ", Customer : " + this.autoCustSearch.SelectedText.Trim() + ", Store : " + cbSectName1;
            
            DataTable tbl1a = ASITUtility2.ListToDataTable<vmEntryPharRestPOS1.ListViewItemTableDetails>(this.ListViewItemTable1);
            tbl1a.TableName = "tbla";
            ds1.Tables.Add(tbl1a);
            DataSet dsdraft1 = WpfProcessAccess.UpdateDeleteDraftTransaction(UpadateDelete1: "UPDATE", ds1: ds1, draftnum1: this.DraftMemoNum, rowid1: this.DraftMemoRowID,
                draftrmrk1: rmrk1, draftbyid1: this.preparebyid1, draftses1: this.prepareses1, drafttrm1: this.preparetrm1);
            if (dsdraft1 == null)
            {
                this.DraftMemoNum = "";
                this.DraftMemoRowID = 0;
                this.chkAllowDraft.IsChecked = false;
                this.stkpDraftOption.Background = Brushes.Yellow;
                System.Windows.MessageBox.Show("Draft update mode disabled. Please check the draft invoice list after re-open this screen.",
                    WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            this.DraftMemoNum = dsdraft1.Tables[0].Rows[0]["draftnum"].ToString();
            this.DraftMemoRowID = Convert.ToInt64(dsdraft1.Tables[0].Rows[0]["rowid"]);
        }

        private void RetriveDraftMemo(string MemoNum1a = "XXXXXXXXXXXX", Int64 rowid1a = 0)
        {
            try
            {
                this.CleanUpScreen();
                DataSet dsdraft1 = WpfProcessAccess.RetriveDraftTransactionInfo(MemoNum1: MemoNum1a, rowid1: rowid1a);
                if (dsdraft1 == null)
                    return;

                StringReader strReader1 = new StringReader(dsdraft1.Tables[0].Rows[0]["draftdata"].ToString()); //new StringReader(xmlData);
                DataSet dsd2 = new DataSet();
                dsd2.ReadXml(strReader1);
                DataRow drb = dsd2.Tables[0].Rows[0];

                this.txtTransID.Tag = drb["invno"].ToString().Trim();
                this.xctk_dtpSalDat.Value = DateTime.Parse(drb["invdat"].ToString());
                string sectcod1 = drb["sectcod"].ToString().Trim();
                int idx1 = 0;
                foreach (ComboBoxItem item1 in this.cmbPayType.Items)
                {
                    if (item1.Tag.ToString().Trim() == sectcod1)
                        break;

                    idx1++;
                }
                this.cmbPayType.SelectedIndex = idx1;

                string paytype1 = drb["paytype"].ToString().Trim();
                int idx2 = 0;
                foreach (ComboBoxItem item2 in this.cmbPayType.Items)
                {
                    if (item2.Tag.ToString().Trim() == paytype1)
                        break;

                    idx2++;
                }

                this.cmbPayType.SelectedIndex = idx2;
                string PayType1a =  drb["paytype1"].ToString().Trim();
                this.rbtnPayTypeCash.IsChecked = PayType1a.Contains("CASH");
                this.rbtnPayTypeCredit.IsChecked = PayType1a.Contains("CREDIT");
                this.rbtnPayTypeCheque.IsChecked = PayType1a.Contains("CHEQUE");

                this.txtChequeNo.Text = drb["invref"].ToString().Trim();
                this.xctk_dtpChqDat.Value = DateTime.Parse(drb["invrefdat"].ToString());

                this.stkpChequeInfo.Visibility = (this.rbtnPayTypeCheque.IsChecked == true ? Visibility.Visible : Visibility.Hidden);
                this.txtPaidAmt.IsEnabled = true;

                this.autoCustSearch.ItemsSource = this.RegCustList;
                this.autoCustSearch.SelectedValue = drb["custid"].ToString();
                this.xcdtDeliveryDT.Text = drb["delivartime"].ToString().Trim();
                this.txtMemoNar.Text = drb["invnar"].ToString().Trim();
                this.txtLabAmt.Text = drb["labamt"].ToString().Trim();
                this.txtCarrAmt.Text = drb["carramt"].ToString().Trim();

                this.preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
                this.prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
                this.preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;
                this.rowtime1 = DateTime.Now;

                this.DraftMemoNum = MemoNum1a;
                this.DraftMemoRowID = rowid1a;
                this.ListViewItemTable1 = dsd2.Tables[1].DataTableToList<vmEntryPharRestPOS1.ListViewItemTableDetails>().ToList();
                this.DispatcherFrame1.Continue = false; // un-blocks gui message pump

                this.EditDs = null;

                this.dgvMemo.Items.Refresh();
                this.chkAutoSaveAc_Click(null, null);
                this.btnTotal_Click(null, null);


                this.stkpPrevTrans.Visibility = Visibility.Collapsed;
                this.stkpdgvPrevTransList.IsEnabled = false;
                this.btnPrevTrans.Content = "Prev.Trans.";
                this.btnNewShow.Content = "_New";
                this.stkpDataEntry.Visibility = Visibility.Visible;
                this.stkpDataEntry.IsEnabled = true;
                this.stkpPrint.Visibility = Visibility.Hidden;
                this.btnUpdateTrans.IsEnabled = true;
                this.stkpFinalUpdate.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-28: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


    }
}
