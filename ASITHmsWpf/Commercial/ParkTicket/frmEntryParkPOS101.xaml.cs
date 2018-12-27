using ASITHmsEntity;
using ASITHmsRpt4Commercial;
using ASITHmsViewMan.Commercial;
using ASITFunLib;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ASITHmsViewMan.Manpower;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace ASITHmsWpf.Commercial.ParkTicket
{
    /// <summary>
    /// Interaction logic for frmEntryParkPOS101.xaml
    /// </summary>
    public partial class frmEntryParkPOS101 : UserControl
    {
        int PrintCount = 0;
        private bool FrmInitialized = false;
        private List<vmEntryReportPark1.ParkTicketTemplate> ListTicketTemplate = new List<vmEntryReportPark1.ParkTicketTemplate>();
        private vmEntryReportPark1 vm1 = new vmEntryReportPark1();

        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();
        private List<vmEntryPharRestPOS1.RetSaleItem> RetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();
        private List<vmEntryPharRestPOS1.RetSaleItem> ShortRetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();
        private List<vmEntryPharRestPOS1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPharRestPOS1.ListViewItemTable>();

        private List<HmsEntityCommercial.InvoiceTransList> TransInvList = new List<HmsEntityCommercial.InvoiceTransList>();
        private bool MemoSaved = false;
        private vmEntryPharRestPOS1 vm1o = new vmEntryPharRestPOS1();
        private vmReportPharRestPOS1 vm2 = new vmReportPharRestPOS1();
        private DataSet EditDs;

        public frmEntryParkPOS101()
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
                this.ActivateAuthObjects();
                this.Objects_On_Init();
                this.FrmInitialized = true;
            }
        }

        private void ActivateAuthObjects()
        {

            try
            {
                this.stkpPrevTrans.Visibility = Visibility.Hidden;
                this.stkpControlButton.Visibility = Visibility.Collapsed;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryParkPOS101_chkDateBlocked") == null)
                    this.chkDateBlocked.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryParkPOS101_chkConfrmSave") == null)
                    this.chkConfrmSave.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryParkPOS101_chkPrintDirect") == null)
                    this.chkPrintDirect.Visibility = Visibility.Hidden;

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Entry-POT-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }


        }

        private void Objects_On_Init()
        {
            try
            {
                this.CleanUpScreen();

                this.GetSectionList();
                this.GetTestTicketTemplateInfo(isircode: "4144");
                this.GetRetailItemList();
                ////this.dgvMemo.ItemsSource = this.ListViewItemTable1;
                this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-7);
                this.xctk_dtpToDate.Value = DateTime.Today;
                this.btnFilterPrevTrans_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
                    if (itemd1.sectname.ToUpper().Contains("PARK")) //if (itemd1.sectname.ToUpper().Contains("STORE"))
                    {
                        this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                    }
                }
                this.cmbSectCod.SelectedIndex = 0;
                this.cmbSectCod.IsEnabled = (this.cmbSectCod.Items.Count > 1 ? true : false);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-05: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }
        private void GetTestTicketTemplateInfo(string isircode = "XXXXXXXXXXXX")
        {
            var pap1 = vm1.SetParamToGetTicketTemplate(WpfProcessAccess.CompInfList[0].comcpcod, isircode);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            //this.dgvTkt.ItemsSource = null;
            this.ListTicketTemplate.Clear();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                string elcode = dr1["elcode"].ToString().Trim();

                switch (elcode.Substring(0, 9))
                {
                    //case "SIPOTKT01":
                    //    this.lblRptTitle.Content = dr1["eldesc"].ToString().Trim();
                    //    this.lblRptTitle.Tag = elcode;
                    //    break;
                    //case "SIPOTKT02":
                    //    this.lblSubTitle.Content = dr1["eldesc"].ToString().Trim();
                    //    this.lblSubTitle.Tag = elcode;
                    //    break;
                    //case "SIPOTKT03":
                    //    this.lblTicketNote.Content = dr1["eldesc"].ToString().Trim();
                    //    this.lblTicketNote.Tag = elcode;
                    //    break;
                    case "SIPOTKT06":
                    case "SIPOTKT08":
                        bool isGrp = (elcode.Substring(0, 9) == "SIPOTKT06");
                        this.ListTicketTemplate.Add(new vmEntryReportPark1.ParkTicketTemplate(dr1["sircode"].ToString().Trim(), int.Parse(dr1["elgrpsl"].ToString().Trim()),
                                (isGrp ? 0 : int.Parse(dr1["elressl"].ToString().Trim())), elcode, dr1["eldesc"].ToString().Trim(), dr1["elstyle"].ToString().Trim(), (isGrp ? "Bold" : "Normal")));
                        break;
                }
            }
            //if (this.ListTicketTemplate.Count > 0)
            //    this.btndgvTktHeader_Click(null, null);


        }


        private void GetRetailItemList()
        {
            try
            {
                this.RetSaleItemList.Clear();
                //var pap = vm1o.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4171", reqmfginf: "WITHOUTMFGINFO");
                var pap = vm1o.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4144", reqmfginf: "WITHOUTMFGINFO");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
                if (ds1 == null)
                    return;

                this.RetSaleItemGroupList = ds1.Tables[1].DataTableToList<vmEntryPharRestPOS1.RetSaleItemGroup>();
                DataRow[] dr1 = ds1.Tables[0].Select();
                DataRow[] dr2 = ds1.Tables[1].Select();
                DataRow[] dr3 = ds1.Tables[2].Select();
                //for (int i = 0; i < dr1.Length; i++)
                foreach (DataRow row1 in dr1)
                {
                    string isircode1 = row1["sircode"].ToString();
                    if (this.ListTicketTemplate.FindAll(x => x.isircode == isircode1).Count > 0)
                    {
                        var bimg1 = new BitmapImage();
                        //DataRow[] dr4 = ds1.Tables[3].Select("sircode = '" + row1["sircode"].ToString().Trim() + "'");
                        //if (dr4.Length > 0)
                        bimg1 = this.ConvertByteImage(row1);// (dr4[0]);
                        var itm1 = new vmEntryPharRestPOS1.RetSaleItem(row1["sircode"].ToString(), row1["sirdesc"].ToString(), Convert.ToDecimal(row1["costprice"]), Convert.ToDecimal(row1["saleprice"]),
                                   Convert.ToDecimal(row1["refscomp"]), Convert.ToDecimal(row1["salvatp"]), row1["sirtype"].ToString(), row1["sirunit"].ToString(), row1["sirunit2"].ToString(),
                                   row1["sirunit3"].ToString(), decimal.Parse("0" + row1["siruconf"].ToString()), decimal.Parse("0" + row1["siruconf3"].ToString()), row1["msircode"].ToString(),
                                   row1["msirdesc"].ToString(), row1["msirdesc"].ToString().Trim() + " - " + row1["sirdesc"].ToString(), row1["sircode"].ToString().Substring(6), row1["mfgid"].ToString(),
                                   row1["mfgcomnam"].ToString(), "Collapsed", "Collapsed", bimg1);

                        this.RetSaleItemList.Add(itm1); //this.ConvertByteImage(dr2[i])));
                    }
                }
                this.RetSaleItemGroupList = ds1.Tables[1].DataTableToList<vmEntryPharRestPOS1.RetSaleItemGroup>();
                //for (int i = 0; i < dr2.Length; i++)
                foreach (DataRow row2 in dr2)
                {
                    this.cmbItemCat.Items.Add(new ComboBoxItem() { Content = row2["msirdesc"].ToString(), Tag = row2["msircode"].ToString() });
                }

                this.lstvRetailItem.ItemsSource = this.RetSaleItemList.Take(30);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-06: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void CleanUpScreen()
        {
            try
            {
                this.PrintCount = 0;
                this.MemoSaved = false;
                this.autoItemSearch.SelectedValue = null;
                this.ListViewItemTable1.Clear();
                this.xctk_dtpInvDat.Value = DateTime.Today;
                this.xctk_dtpInvDat.Tag = DateTime.Today.ToString("dd-MMM-yyyy");
                this.lblGrandTotal.Content = "";
                this.txtDisAmt.Text = "";
                this.lblNetTotal.Content = "";
                this.lblVATTotal.Content = "";
                this.lblUnitPrice.Content = "";

                this.EditDs = null;
                this.chkDiscount.IsChecked = false;
                this.iudDisPer.Value = 0;
                this.btnUpdateTrans.IsEnabled = true;
                this.btnAddSearch.IsEnabled = true;
                this.btnPrintTrans.Visibility = Visibility.Hidden;
                this.btnUpdateTrans.Visibility = Visibility.Hidden;
                this.btnUpdateTrans.Tag = "New";
                string lastid1 = this.GetLastTransID();
                this.txtTransID.Text = (lastid1 == "PSI000000000000000" ? "" : "");
                this.txtTransID.Tag = lastid1;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private string GetLastTransID()
        {
            return "PSI000000000000000";
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal TotalQty1 = this.ListViewItemTable1.Sum(x => x.invqty);
                if (TotalQty1 <= 0)
                    return;

                ////decimal disper1 = decimal.Parse(this.iudDisPer.Value.ToString());
                ////decimal GrandTotal1a = this.ListViewItemTable1.Sum(x => x.invamt);
                ////decimal disamt1 = Math.Round(GrandTotal1a * disper1 / 100.00m, 0);
                ////foreach (var item1b in this.ListViewItemTable1)
                ////    item1b.invdisamt = disamt1;
                decimal disamt1 = decimal.Parse("0" + this.txtDisAmt.Text.Trim());
                foreach (var item in this.ListViewItemTable1)
                {
                    item.invqty = Math.Floor(item.invqty); // (this.chkIntQtyOnly.IsChecked == true ? Math.Floor(item.invqty) : item.invqty);
                    item.invamt = item.invrate * item.invqty;
                    item.invdisamt = disamt1;
                    item.invnetamt = item.invamt - disamt1; // item.invdisamt;
                    item.invvatamt = Math.Round(item.invvatper / 100.00m * item.invamt, 0);
                }


                decimal GrandTotal1 = this.ListViewItemTable1.Sum(x => x.invamt);
                decimal DiscTotal1 = this.ListViewItemTable1.Sum(x => x.invdisamt);
                decimal NetTotal = this.ListViewItemTable1.Sum(x => x.invnetamt);
                decimal VATTotal = this.ListViewItemTable1.Sum(x => x.invvatamt);
                decimal TotalBill = GrandTotal1 - DiscTotal1 + VATTotal;// +decimal.Parse("0" + this.txtCCCharge.Text.Trim());              

                this.lblGrandTotal.Content = GrandTotal1.ToString("#,##0;(#,##0); - ");// " -  ";
                //this.txtDisAmt.Text = DiscTotal1.ToString("#,##0;(#,##0); ");// " -  ";
                this.lblNetTotal.Content = NetTotal.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblVATTotal.Content = VATTotal.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblUnitPrice.Content = (GrandTotal1 / TotalQty1).ToString("#,##0;(#,##0); - ");// " -  ";

                this.ListViewItemTable1 = this.ListViewItemTable1.FindAll(x => x.invqty > 0);
                int serialno1 = 1;
                foreach (var item in this.ListViewItemTable1)
                {
                    item.trsl = serialno1.ToString() + ".";
                    ++serialno1;
                }
                //// this.dgvMemo.ItemsSource = this.ListViewItemTable1;
                //// this.dgvMemo.Items.Refresh();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-17: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }


    


        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string DueAmt1 = "0";
                string BillAmt1 = this.lblNetTotal.Content.ToString();
                decimal dueamt1 = Math.Round(decimal.Parse("0" + DueAmt1.Replace("(", "").Replace(")", "")), 0) * (DueAmt1.Contains("(") ? -1 : 1);
                decimal billamt1 = Math.Round(decimal.Parse("0" + BillAmt1.Replace("(", "").Replace(")", "")), 0) * (BillAmt1.Contains("(") ? -1 : 1);
                string PaidAmt1 = (billamt1 - dueamt1).ToString("#,##0.00");


                if (this.chkConfrmSave.IsChecked == false)
                {
                    this.MemoSaved = false;
                    if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }

                string tsircode1 = this.ListViewItemTable1[0].rsircode;// "000000000000";
                string EditTransID1 = (this.EditDs != null ? this.txtTransID.Tag.ToString() : "");

                string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                decimal tgtotal1 = decimal.Parse("0" + this.lblGrandTotal.Content.ToString().Replace("-", ""));
                decimal tnetam1 = decimal.Parse("0" + this.lblNetTotal.Content.ToString().Replace("-", ""));
                decimal tvatam1 = decimal.Parse("0" + this.lblVATTotal.Content.ToString().Replace("-", ""));
                decimal taddam1 = 0.00m;
                decimal tdisam1 = tgtotal1 - tnetam1 - tvatam1;
                string trmrks1 = this.txtMemoNar.Text.Trim();

                Int32 tokenpcs1 = Int32.Parse(this.intUdQty.Value.ToString());// this.ListTicketTemplate.FindAll(x => x.isircode == tsircode1 && x.elressl == 0).Count;

                DataSet ds1 = vm1.GetDataSetForUpdateCoupon(CompCode: WpfProcessAccess.CompInfList[0].comcod, _tsircode: tsircode1, _tokendat: DateTime.Parse(this.xctk_dtpInvDat.Text.Trim().Substring(0, 11)),
                              EditMemoNum1: EditTransID1, _tokehead: "POT", _cbSectCode: cbSectCode1, _tokenpcs: tokenpcs1, _tdisam: tdisam1, _tnetam: tnetam1, _tvatam: tvatam1, _taddam: taddam1, _trmrks: trmrks1,
                              _salstatus: "A", _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);


                //String xx1 = ds1.GetXml().ToString();

                var pap1 = vm1.SetParamUpdateCoupon(WpfProcessAccess.CompInfList[0].comcod, _tsircode: tsircode1, ds1: ds1, EditTransID1: EditTransID1);
                //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
                //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                decimal tnetam = this.ListViewItemTable1.Sum(x => x.invnetamt);
                decimal tvatam = this.ListViewItemTable1.Sum(x => x.invvatamt);
                string memonum1 = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                string memonum = ds2.Tables[0].Rows[0]["memonum"].ToString();
                this.txtTransID.Text = memonum1;
                this.txtTransID.Tag = memonum;

                DateTime MemoDate1 = DateTime.Parse(this.xctk_dtpInvDat.Text.Trim().Substring(0, 11));

                //--------------------------------------
                if (this.stkpPrevTrans.Visibility == Visibility.Visible)
                {
                    string Content1 = "xxxxxx";
                    bool isExist1 = false;
                    foreach (ComboBoxItem item in this.cmbPrevTransList.Items)
                    {
                        if (item.Tag.ToString() == memonum)
                        {
                            item.Content = Content1;
                            isExist1 = true;
                            break;
                        }
                    }

                    if (isExist1 == false)
                        this.cmbPrevTransList.Items.Insert(0, new ComboBoxItem() { Content = Content1, Tag = memonum });
                }
                //--------------------------------------

                this.MemoSaved = true;
                this.btnUpdateTrans.IsEnabled = false;
                this.btnAddSearch.IsEnabled = false;
                this.btnPrintTrans.Visibility = Visibility.Visible;
                this.txtMemoNar.Text = "";
                this.intUdQty.Value = 1;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-10: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnDelete1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.cmbPrevTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to cancel", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this Invoice", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;

                var item1a = (ComboBoxItem)this.cmbPrevTransList.SelectedItem;// (HmsEntityCommercial.InvoiceTransList)this.lstPrevTransList.SelectedItem;
                int itemno1 = this.cmbPrevTransList.SelectedIndex;


                var pap1a = vm1o.SetParamInvoiceVouchers(WpfProcessAccess.CompInfList[0].comcod, item1a.Tag.ToString().Trim());

                DataSet ds1a = WpfProcessAccess.GetHmsDataSet(pap1a);
                if (ds1a == null)
                    return;


                var pap1 = vm1o.SetParamCancelMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.Tag.ToString().Trim());

                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.cmbPrevTransList.Items.RemoveAt(itemno1);

                this.cmbPrevTransList.Items.Refresh();
                if (this.cmbPrevTransList.Items.Count > 0)
                    this.cmbPrevTransList.SelectedIndex = 0;

                if (this.txtTransID.Tag.ToString().Trim() == item1a.Tag.ToString().Trim())
                    this.CleanUpScreen();

                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-25: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnAddSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.autoItemSearch.SelectedValue == null)
                    return;

                ////string srchVal1a = this.autoItemSearch.SelectedValue.ToString();
                ////string srchTxt1a = this.autoItemSearch.SelectedText.ToString();
                ////this.AddChangeItem(srchVal1: srchVal1a);
                ////this.autoItemSearch.SelectedValue = null;
                ////this.autoItemSearch.Text = "";
                this.btnPrintTrans_Click(null, null);

                this.btnNewShow_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-15: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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

                this.PrintCount = this.PrintCount + 1;

                if (this.PrintCount > 2)
                    this.btnPrintTrans.Visibility = Visibility.Hidden;
                string tsircode1 = this.ListViewItemTable1[0].rsircode;
                string MemoNum = this.txtTransID.Tag.ToString();
                string MomoDate = this.xctk_dtpInvDat.Text;
                string PrnOpt1 = (this.chkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                this.ViewPrintMemo(tsircode1, MemoNum, MomoDate, PrnOpt1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void dgvUdQtyChange_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }

        private void dgvTxtDiscnt_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }

        private void autoItemSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
        }
        private ObservableCollection<vmEntryPharRestPOS1.RetSaleItem> GetItemSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<vmEntryPharRestPOS1.RetSaleItem>(
                this.ShortRetSaleItemList.Where((x, match) => x.sirdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoItemSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoItemSearch.ContextMenu.IsOpen = true;
        }

        private void btnAddItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string srchVal1a = ((Button)sender).Tag.ToString();
                this.autoItemSearch.ItemsSource = this.ShortRetSaleItemList;
                this.autoItemSearch.SelectedValue = srchVal1a;
                this.autoItemSearch_LostFocus(null, null);
                this.btnAddSearch_Click(null, null);

                ////this.btnNewShow_Click(null, null);

                ////string srchVal1a = ((Button)sender).Tag.ToString();
                ////this.AddChangeItem(srchVal1: srchVal1a);
                ////this.btnPrintTrans_Click(null, null);

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
                var RetailItemList1 = this.RetSaleItemList.FindAll(x => x.sircode == srchVal1).Take(10);
                foreach (var item in RetailItemList1)
                {
                    this.ListViewItemTable1.Clear(); // for park only

                    int serialno1 = this.ListViewItemTable1.Count + 1;
                    var ListViewItemTable1a = this.ListViewItemTable1.FindAll(x => x.invcode == srchVal1);
                    if (ListViewItemTable1a.Count > 0)
                    {
                        ListViewItemTable1a[0].invqty = ListViewItemTable1a[0].invqty + 1;
                    }
                    else
                    {
                        var item1a = new vmEntryPharRestPOS1.ListViewItemTable()
                        {
                            trsl = serialno1.ToString() + ".",
                            invcode = item.sircode, //  invcode1
                            reptsl = "00",
                            rsircode = item.sircode, // rsircode1,
                            trdesc = item.sirdesc.Trim(), // rsirdesc1,
                            mfgid = item.mfgid,// "", // siruid1,
                            mfgcomnam = item.mfgcomnam,// "", //mfgByName1,
                            invqty = decimal.Parse(this.intUdQty.Value.ToString()) * 1.00m, //invqty1a,
                            truid = "True", // truid1a,
                            trunit = item.sirunit, // rsirunit,
                            invrate = item.saleprice, // Math.Round(invAmt1 / invqty1a, 6),
                            invamt = item.saleprice, //invAmt1,
                            invdisamt = 0,
                            invnetamt = item.saleprice, // invAmt1,
                            invvatper = item.salvatp,
                            invvatamt = (item.salvatp / 100.00m * item.saleprice),
                            invrmrk = "",
                            batchno = item.sircode.Substring(6) + ": " + item.sirdesc.Trim(),
                            mfgdat = DateTime.Today,
                            expdat = DateTime.Today.AddDays(7),
                            mfgvisible = item.mfgvisible,
                            rmrkvisible = "Collapsed"
                        };

                        this.ListViewItemTable1.Add(item1a);
                    }
                }
                this.btnPrintTrans.Visibility = Visibility.Visible;
                //this.chkPrintDirect.Visibility = Visibility.Visible;
                //this.chkConfrmSave.Visibility = Visibility.Visible;
                this.btnUpdateTrans.Visibility = Visibility.Visible;
                this.btnTotal_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private BitmapImage ConvertByteImage(DataRow dr1)
        {

            // For using local image
            try
            {

                string sircode1a = dr1["sircode"].ToString();//.Substring(6, 6);
                BitmapImage bbb1;
                string filname1 = WpfProcessAccess.AppLocalImagePath + @"L" + sircode1a + ".jpg";// @"C:\ASIT_Image\L_Image\L" + sircode1a + ".jpg";
                if (File.Exists(filname1))
                    bbb1 = new BitmapImage(new System.Uri(filname1));
                else
                {
                    filname1 = WpfProcessAccess.AppLocalImagePath + @"FoodShop1.jpg";
                    if (File.Exists(filname1))
                        bbb1 = new BitmapImage(new System.Uri(filname1));
                    else
                        bbb1 = new BitmapImage();
                }
                return bbb1;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-07: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return null;
                //var img2 = new BitmapImage();
                //return img2;
            }


            //// For using database images
            //if (dr1["sirimage"] is DBNull)
            //{
            //    // Following statement munt not return null 
            //    //var img1 = new General.imgLib1().imgLogo1.Source as BitmapImage;

            //    //return img1;
            //    ///////
            //    return new BitmapImage();
            //}

            //byte[] byteSi = (byte[])dr1["sirimage"];
            //MemoryStream mem1 = new MemoryStream(byteSi);
            //BitmapImage bmp4 = new BitmapImage();
            //bmp4.BeginInit();
            //bmp4.StreamSource = mem1;
            //bmp4.EndInit();
            //return bmp4;
        }
        private List<HmsEntityCommercial.InvoiceTransList> PreviousMemoList(string Date1, string Date2, string searchStr = "%")
        {
            try
            {
                if (this.cmbSectCod.Items.Count == 0)
                    return null;

                string sectcod1 = ((ComboBoxItem)this.cmbSectCod.Items[this.cmbSectCod.SelectedIndex]).Tag.ToString();
                var pap1 = vm2.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, "A00MSISUM", Date1, Date2, sectcod1, "PSI");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return null;

                return ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-08: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return null;
            }
        }
        private void btnFilterPrevTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FrmDate1 = this.xctk_dtpFromDate.Text;   // DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
                string ToDate1 = this.xctk_dtpToDate.Text;      // DateTime.Today.ToString("dd-MMM-yyyy");
                if (this.TransInvList == null)
                    return;

                this.TransInvList.Clear();
                this.TransInvList = this.PreviousMemoList(FrmDate1, ToDate1);
                if (this.TransInvList == null)
                    return;

                this.TransInvList = this.TransInvList.FindAll(x => x.invno.Substring(0, 3) == "FSI").ToList();
                if (this.TransInvList == null)
                    return;
                this.cmbPrevTransList.Items.Clear();
                foreach (var item1 in this.TransInvList)
                {
                    this.cmbPrevTransList.Items.Add(new ComboBoxItem()
                    {
                        Content = item1.invno1.Substring(3, 2) + "-" + item1.invno1.Substring(11, 5) + " [Tk. " + item1.billam.ToString("#,##0.00") +
                        (item1.dueam <= 0 ? "" : ", Due: Tk. " + item1.dueam.ToString("#,##0.00")) + ", " +
                        item1.invdat.ToString("dd.MM.yyyy") + "] " + (item1.invref.Trim().Length > 0 ? ", " + item1.invref.Trim() : "") +
                        (item1.invnar.Trim().Length > 0 ? ", " + item1.invnar.Trim() : ""),
                        Tag = item1.invno
                    });
                }
                if (this.cmbPrevTransList.Items.Count > 0)
                    this.cmbPrevTransList.SelectedIndex = 0;
                this.chkPrevTransFilter.IsChecked = false;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-04: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string MemoNum = ((ComboBoxItem)this.cmbPrevTransList.SelectedItem).Tag.ToString();
                string PrnOpt1 = (this.chkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                this.ViewPrintMemo(MemoNum, PrnOpt1, "Reprint");
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-20: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void ViewPrintMemo(string isircode1, string memoNum = "XXXXXXXX", string memoDate = "01-Jan-2018", string ViewPrint = "View", string Duplicate = "")
        {
            try
            {
                //string memoNum = ((ComboBoxItem)this.cmbPrevInvList.SelectedItem).Tag.ToString();
                LocalReport rpt1 = null;
                string WindowTitle1 = "";
                var pap1 = vm1.SetParamParkTicketPrint(WpfProcessAccess.CompInfList[0].comcod, isircode1, memoNum, memoDate);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var list3 = WpfProcessAccess.GetRptGenInfo();
                list3[0].RptHeader1 = "";
                switch (WpfProcessAccess.CompInfList[0].comcod)
                {
                    case "6501":
                        list3[0].RptHeader1 = "VAT Reg. # " + "XXXXXXXXXX";
                        break;
                    case "6531":
                        list3[0].RptHeader1 = "VAT Reg. # " + "000538591";
                        break;
                    case "6535":
                    case "6572":
                        list3[0].RptHeader1 = "";
                        break;
                }

                list3[0].RptFooter1 = "User : " + WpfProcessAccess.SignedInUserList[0].signinnam;

                var list1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.ParkTicketCoupon01>();
                //var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();

                if (WpfProcessAccess.CompInfList[0].comcod == "6572" || WpfProcessAccess.CompInfList[0].comcod == "6501")
                    list3[0].RptParVal1 = (list1[0].tsircode.Substring(0, 9) == "414410102" ? "Adventure Water Land" : "Adventure Land");

                Int32 cnt1 = list1.Count;
                foreach (var item in list1)
                {
                    item.eldesc = (item.eldesc.Contains("(EMPTY)") ? "" : item.eldesc);
                    item.rowid = cnt1;
                }
                rpt1 = CommReportSetup.GetLocalReport("ParkSales.ParkTicket01", list1, null, list3);

                ////if (Duplicate.Length > 0 || this.PrintCount > 1)
                ////{
                ////    //rpt1.SetParameters(new ReportParameter("ParamAddress1", "[Re-Print/Duplicate Invoice]"));
                ////    rpt1.SetParameters(new ReportParameter("ParamAddress1", ":: D U P L I C A T E ::"));
                ////    rpt1.SetParameters(new ReportParameter("ParamAddress2", "========================="));
                ////}

                WindowTitle1 = "Park Ticket / Coupon";

                if (ViewPrint == "View")
                {
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: "PrintLayout");
                }
                else if (ViewPrint == "DirectPrint")
                {
                    RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                    DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                    //if (Duplicate.Length == 0 && this.PrintCount == 1)
                    //{
                    //    rpt1.SetParameters(new ReportParameter("ParamTitle1", "Kitchen Order Token (KOT)"));
                    //    DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                    //}
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
                System.Windows.MessageBox.Show("PSI-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }
        private void cmbItemCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbItemCat.SelectedIndex >= 0)
                    this.btnShowGroupItems_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-21: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnShowGroupItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ShortRetSaleItemList.Clear();
                string msircode1 = ((ComboBoxItem)this.cmbItemCat.SelectedItem).Tag.ToString();
                this.ShortRetSaleItemList = this.RetSaleItemList.FindAll(x => x.msircode == msircode1).ToList();
                if (msircode1 == "000000000000")
                    this.ShortRetSaleItemList = this.RetSaleItemList.ToList();

                this.autoItemSearch.ContextMenu.Items.Clear();
                foreach (var item in this.ShortRetSaleItemList)
                {
                    MenuItem mnu1 = new MenuItem { Header = item.sirdesc, Tag = item.sircode };
                    mnu1.Click += autoItemSearch_ContextMenu_MouseClick;
                    this.autoItemSearch.ContextMenu.Items.Add(mnu1);
                }

                this.lstvRetailItem.ItemsSource = null;
                this.lstvRetailItem.ItemsSource = this.ShortRetSaleItemList;
                this.lstvRetailItem.Items.Refresh();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-22: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void autoItemSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.autoItemSearch.ItemsSource = this.ShortRetSaleItemList;
                this.autoItemSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim();
                this.btnAddSearch_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-23: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnNewShow_Click(object sender, RoutedEventArgs e)
        {
            this.CleanUpScreen();
        }

        private void btnEdit1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void autoItemSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.autoItemSearch.SelectedValue == null)
                    return;

                string srchVal1a = this.autoItemSearch.SelectedValue.ToString();
                string srchTxt1a = this.autoItemSearch.SelectedText.ToString();
                this.AddChangeItem(srchVal1: srchVal1a);
                ////this.autoItemSearch.SelectedValue = null;
                ////this.autoItemSearch.Text = "";
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-25: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void intUdQty_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.autoItemSearch.SelectedValue == null)
                return;

            string itemcod1 = this.autoItemSearch.SelectedValue.ToString();
            var itm1a = this.ListViewItemTable1.FindAll(x => x.rsircode == itemcod1);
            foreach (var item1b in itm1a)
                item1b.invqty = decimal.Parse(this.intUdQty.Value.ToString());

            this.btnTotal_Click(null, null);
        }

        private void iudDisPer_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                decimal disper1 = decimal.Parse(this.iudDisPer.Value.ToString());
                decimal GrandTotal1a = this.ListViewItemTable1.Sum(x => x.invamt);
                decimal disamt1 = Math.Round(GrandTotal1a * disper1 / 100.00m, 0);
                foreach (var item1b in this.ListViewItemTable1)
                    item1b.invdisamt = disamt1;
                this.txtDisAmt.Text = disamt1.ToString("###0;-#,##0;0");// " -  ";       
            }
            catch
            {

            }
        }

        private void txtDisAmt_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }

    }
}
