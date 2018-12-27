using System;
using System.Collections;
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
using System.ComponentModel;
using System.Windows.Data;
using ASITHmsViewMan.General;
using System.Collections.ObjectModel;
using ASITHmsViewMan.Accounting;
using ASITHmsRpt1GenAcc.Accounting;
using System.Windows.Media;


namespace ASITHmsWpf.Commercial.Trading
{
    /// <summary>
    /// Interaction logic for frmEntryGenTrPOS103.xaml
    /// </summary>
    public partial class frmEntryGenTrPOS103 : UserControl
    {

        private string TitaleTag1, TitaleTag2;  // 
        private bool FrmInitialized = false;
        private DataGrid dgRpt1;

        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemMainGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();
        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();

        private List<vmEntryPharRestPOS1.RetSaleItem> RetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();

        private List<HmsEntityGeneral.SirInfCodeBook> RegCustList = new List<HmsEntityGeneral.SirInfCodeBook>();                    // REgistered Customer List from Resource Code Book

        private List<vmEntryPharRestPOS1.ItemCustDetailsInfo> RegCustDetailsList = new List<vmEntryPharRestPOS1.ItemCustDetailsInfo>();
        //private List<vmEntryPharRestPOS1.RetSaleItem> ShortRetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();

        private List<HmsEntityCommercial.InvoiceTransList> RptList = new List<HmsEntityCommercial.InvoiceTransList>();
        private List<HmsEntityCommercial.InvoiceTransList2> RptList1 = new List<HmsEntityCommercial.InvoiceTransList2>();
        private List<HmsEntityCommercial.InvColList01> RptList2 = new List<HmsEntityCommercial.InvColList01>();//PhSalesInvoice01
        private List<HmsEntityCommercial.InvDuesList01> RptList3 = new List<HmsEntityCommercial.InvDuesList01>();
        private List<HmsEntityCommercial.PhSalesInvoice01> RptList4 = new List<HmsEntityCommercial.PhSalesInvoice01>();
        private List<HmsEntityCommercial.RetSalesTransList2> RptDailySalesList1 = new List<HmsEntityCommercial.RetSalesTransList2>();

        private List<HmsEntityAccounting.AccLedger1> AccLedgerLst = new List<HmsEntityAccounting.AccLedger1>();
        private List<HmsEntityAccounting.AccLedger1A> AccLedgerLst2 = new List<HmsEntityAccounting.AccLedger1A>();

        private List<HmsEntityCommercial.RetSalesCashRecv1> RptSalesCashRecvList1 = new List<HmsEntityCommercial.RetSalesCashRecv1>();

        private List<HmsEntityAccounting.AccTrialBalance1> RptTrialBalanceList1 = new List<HmsEntityAccounting.AccTrialBalance1>();
        private List<HmsEntityAccounting.AccTrialBalance1t> RptTrialBalanceList2 = new List<HmsEntityAccounting.AccTrialBalance1t>();



        private vmEntryPharRestPOS1 vm1a = new vmEntryPharRestPOS1();
        private vmReportPharRestPOS1 vm1 = new vmReportPharRestPOS1();
        private vmHmsGeneralList1 vmGenList1a = new vmHmsGeneralList1();
        private vmReportAccounts1 vmrptAcc = new vmReportAccounts1();

        public frmEntryGenTrPOS103()
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
                this.Objects_On_Init();
                this.FrmInitialized = true;
            }
        }

        private void Objects_On_Init()
        {
            try
            {
                TreeViewItem tvi1 = new TreeViewItem() { Header = "A. TRANSECTION LIST", Tag = "A000000000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
                TreeViewItem tvi2 = new TreeViewItem() { Header = "B. SUMMARY LIST", Tag = "B00000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };
                TreeViewItem tvi3 = new TreeViewItem() { Header = "C. SPECIAL REPORTS", Tag = "C00000000000", Foreground = Brushes.Blue, FontWeight = FontWeights.Bold };

                tvi1.Items.Add(new TreeViewItem { Header = "01. Date wise sales details", Tag = "A06A00MSIDETAILS" });

                tvi2.Items.Add(new TreeViewItem { Header = "01. Invoice summary list", Tag = "B09A00MSISUM" });
                tvi2.Items.Add(new TreeViewItem { Header = "02. Item wise sales summary", Tag = "B07A00ITEMSUMMARY" });

                tvi3.Items.Add(new TreeViewItem { Header = "01. Daily sales statement", Tag = "C01C00DAILYSALES" });
                tvi3.Items.Add(new TreeViewItem { Header = "02. Money received statement", Tag = "C02C00CUSTCASHCOL" });
                tvi3.Items.Add(new TreeViewItem { Header = "03. All customer balance", Tag = "C03C00CUSTLEDGERSUM" });
                tvi3.Items.Add(new TreeViewItem { Header = "04. Customer account ledger", Tag = "C04C00CUSTLEDGER" });

                tvi1.IsExpanded = true;
                tvi2.IsExpanded = true;
                tvi3.IsExpanded = true;

                this.tvRptRtTitle.Items.Add(tvi1);
                this.tvRptRtTitle.Items.Add(tvi2);
                this.tvRptRtTitle.Items.Add(tvi3);

                TitaleTag2 = this.Tag.ToString();
                this.xctk_dtpFrom.Value = DateTime.Today; //Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
                this.xctk_dtpTo.Value = DateTime.Today;

                this.GetBranchList();
                this.GetSectionList();
                this.GetRetailItemList();

                var pap1 = vmGenList1a.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "55%", "5"); //"[0-4]%"
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.RegCustList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                foreach (var item in this.RegCustList)
                {
                    item.sirdesc1 = item.sirdesc1.Substring(6);
                }

                // Code Goes from here -- Hafiz 23-May-2017
                //this.BasicEntry = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 7) == "SICD001" && x.actcode.Substring(9, 3) != "000");
                var pap1d = vmGenList1a.SetParamGeneralDataInfo(WpfProcessAccess.CompInfList[0].comcpcod, "SIRINF", "55", "SICD001");
                DataSet ds1d = WpfProcessAccess.GetHmsDataSet(pap1d);
                if (ds1 == null)
                    return;

                this.RegCustDetailsList = ds1d.Tables[0].DataTableToList<vmEntryPharRestPOS1.ItemCustDetailsInfo>();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-15 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void GetBranchList()
        {
            this.cmbSBrnCod.Items.Clear();
            var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            if (brnList.Count > 1)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = "ALL BRANCHES", Tag = "0000" });

            foreach (var itemb in brnList)
                this.cmbSBrnCod.Items.Add(new ComboBoxItem() { Content = itemb.brnnam, Tag = itemb.brncod });

            this.cmbSBrnCod.IsEnabled = (this.cmbSBrnCod.Items.Count == 1 ? false : true);
            this.cmbSBrnCod.SelectedIndex = 0;

        }
        private void GetSectionList()
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

        private void GetRetailItemList()
        {
            try
            {
                this.RetSaleItemList.Clear();
                //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4171", reqmfginf: "WITHOUTMFGINFO");
                //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4521", reqmfginf: "WITHMFGINFO");
                //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "0[14]51", reqmfginf: "WITHOUTMFGINFO");
                var pap = vm1a.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "0151", reqmfginf: "WITHOUTMFGINFO");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
                if (ds1 == null)
                    return;

                this.RetSaleItemGroupList = ds1.Tables[1].DataTableToList<vmEntryPharRestPOS1.RetSaleItemGroup>();
                DataRow[] dr1 = ds1.Tables[0].Select();
                DataRow[] dr2 = ds1.Tables[1].Select();
                DataRow[] dr3 = ds1.Tables[2].Select();
                foreach (DataRow row1 in dr1)
                {
                    var itm1 = new vmEntryPharRestPOS1.RetSaleItem(row1["sircode"].ToString(), row1["sircode"].ToString().Substring(6, 6) + " : " + row1["sirdesc"].ToString(), Convert.ToDecimal(row1["costprice"]),
                            Convert.ToDecimal(row1["saleprice"]), Convert.ToDecimal(row1["refscomp"]), Convert.ToDecimal(row1["salvatp"]), row1["sirtype"].ToString(), row1["sirunit"].ToString(), row1["sirunit2"].ToString(),
                            row1["sirunit3"].ToString(), decimal.Parse("0" + row1["siruconf"].ToString()), decimal.Parse("0" + row1["siruconf3"].ToString()), row1["msircode"].ToString(), row1["msirdesc"].ToString(),
                            row1["msirdesc"].ToString().Trim() + " - " + row1["sirdesc"].ToString(), row1["sircode"].ToString().Substring(6), row1["mfgid"].ToString(), row1["mfgcomnam"].ToString(),
                            (row1["mfgcomnam"].ToString().Trim().Length > 0 ? "Visible" : "Collapsed"), "Collapsed", null);
                    this.RetSaleItemList.Add(itm1);
                }
                foreach (DataRow itemr in dr3)
                {
                    this.RetSaleItemMainGroupList.Add(new vmEntryPharRestPOS1.RetSaleItemGroup() { msircode = itemr["msirtype"].ToString(), msirdesc = itemr["msirtype"].ToString(), msirtype = itemr["msirtype"].ToString() });
                }


                if (WpfProcessAccess.InvItemGroupList == null)
                    WpfProcessAccess.GetInventoryItemGroupList();

                this.cmbItemGroup.Items.Add(new ComboBoxItem() { Content = "ALL GROUP OF ITEMS", Tag = "000000000000" });
                foreach (var itemd1 in WpfProcessAccess.InvItemGroupList)
                {
                    var GrpList1 = this.RetSaleItemList.FindAll(x => x.sircode.Substring(0, 7) == itemd1.sircode.Substring(0, 7));
                    if (GrpList1.Count > 0)
                        this.cmbItemGroup.Items.Add(new ComboBoxItem() { Content = itemd1.sircode.Substring(0, 7) + ": " + itemd1.sirtype.Trim() + " - " + itemd1.sirdesc.Trim(), Tag = itemd1.sircode });
                }

                this.cmbItemGroup.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-15 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Tree Veiw Report Show

                if (dgOverall01.Columns.Count > 0)
                    dgOverall01.Columns.Clear();
                this.dgOverall01.ItemsSource = null;
                this.dgOverall01.Items.Refresh();
                this.dgOverall01.AutoGenerateColumns = false;
                if ((TreeViewItem)tvRptRtTitle.SelectedItem == null)
                {
                    return;
                }

                string fromDate = xctk_dtpFrom.Text.Trim();
                string ToDate = xctk_dtpTo.Text.ToString().Trim();
                string TrHead = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Header.ToString().ToUpper();
                string TrTyp = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Tag.ToString().Substring(3);
                //string dept01 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();

                this.lbltle1.Content = TrHead.Remove(0, 3);
                this.lbltle2.Content = " From " + fromDate + " To " + ToDate;
                string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();


                //switch (TrTyp.Substring(0, 2))
                //{
                //    case "A0": this.GetStockReport(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01); break;
                //    case "B0": this.GetSuimmaryRpt(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01); break;
                //    case "C0": this.GetTransecList(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01); break;
                //    case "D0": this.GetTransDetails(TrHead, TrTyp, PrintId, fromDate, ToDate, dept01); break;
                //}

                switch (TrTyp)
                {
                    case "A00MSISUM": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                    case "A00MSIDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                    case "A00COLLDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                    case "A00DUEDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                    case "A00ITEMSUMMARY": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                    case "A00TOPSUMMARY": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;

                    case "C00DAILYSALES": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                    case "C00CUSTLEDGER": this.CustomerAccSubLedger(TrHead, TrTyp, PrintId); break;
                    case "C00CUSTLEDGERSUM": this.GetControlSchedule(TrHead, TrTyp, PrintId); break;
                    case "C00CUSTCASHCOL": this.GetCashReceivedStatement(TrHead, TrTyp, PrintId); break;
                    //case "B0": this.GetSuimmaryRpt(TrHead, TrTyp, PrintId); break;
                    //case "C0": this.GetTransecList(TrHead, TrTyp, PrintId); break;
                    //case "D0": this.GetTransDetails(TrHead, TrTyp, PrintId); break;
                }
                #endregion

                #region ComboBox Report Show Code
                //string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //string TrHead = ((ComboBoxItem)(this.cmbReportSelOption.SelectedItem)).Content.ToString().Trim();
                //string TrTyp = ((ComboBoxItem)(this.cmbReportSelOption.SelectedItem)).Tag.ToString().Trim();

                //switch (TrTyp)
                //{
                //    case "A00MSISUM": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                //    case "A00MSIDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                //    case "A00COLLDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                //    case "A00DUEDETAILS": this.GetSumTransListReport(TrHead, TrTyp, PrintId); break;
                //    //case "B0": this.GetSuimmaryRpt(TrHead, TrTyp, PrintId); break;
                //    //case "C0": this.GetTransecList(TrHead, TrTyp, PrintId); break;
                //    //case "D0": this.GetTransDetails(TrHead, TrTyp, PrintId); break;
                //}
                #endregion
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-14 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void CustomerAccSubLedger(string TrHead, string TrTyp, string PrintId)
        {
            try
            {
                if (this.autoCustSearch.SelectedValue == null)
                    return;

                string fromDate = xctk_dtpFrom.Text.Trim();
                string ToDate = xctk_dtpTo.Text.ToString().Trim();
                string CustID = this.autoCustSearch.SelectedValue.ToString();
                var pap1 = vmrptAcc.SetParamAccSubLedger(WpfProcessAccess.CompInfList[0].comcod, fromDate, ToDate, "180100010001", CustID, "NARRATION");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;


                double balam1 = 0.00;
                for (int i = 0; i < ds1.Tables[0].Rows.Count - 2; i++)
                {
                    if (ds1.Tables[0].Rows[i]["elevel"].ToString().Trim() == "00")
                    {
                        balam1 = balam1 + Convert.ToDouble(ds1.Tables[0].Rows[i]["dram"]) - Convert.ToDouble(ds1.Tables[0].Rows[i]["cram"]);
                        if (Convert.ToDouble(ds1.Tables[0].Rows[i]["dram"]) != 0 || Convert.ToDouble(ds1.Tables[0].Rows[i]["cram"]) != 0)
                            ds1.Tables[0].Rows[i]["blancam"] = balam1;
                    }
                }

                //List<HmsEntityAccounting.AccLedger1> AccLedgerLst = new List<HmsEntityAccounting.AccLedger1>();
                //List<HmsEntityAccounting.AccLedger1A> AccLedgerLst2 = new List<HmsEntityAccounting.AccLedger1A>();


                this.AccLedgerLst.Clear();
                this.AccLedgerLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccLedger1>();
                this.AccLedgerLst2.Clear();
                this.AccLedgerLst2 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccLedger1A>();
                this.AccLedgerLst2[0].booknam = "CUSTOMER LEDGER";
                this.AccLedgerLst2[0].actdesc = this.AccLedgerLst2[0].sirdesc;
                this.AccLedgerLst2[0].sirdesc = "";


                var custDetails = this.RegCustDetailsList.FindAll(x => x.tblcode == CustID).ToList();
                var nam1 = custDetails.FindAll(x => x.tblcode == CustID && x.gencode == "SICD00101001" && x.repeatsl == "1").ToList();
                if (nam1.Count > 0)
                    this.AccLedgerLst2[0].actdesc = (nam1[0].dataval.Trim().Length > 0 ? CustID.Substring(6, 6) + " : " + nam1[0].dataval.Trim().ToUpper() : this.AccLedgerLst2[0].actdesc);

                var add1 = custDetails.FindAll(x => x.tblcode == CustID && x.gencode == "SICD00101003" && x.repeatsl == "1").ToList();
                var add2 = custDetails.FindAll(x => x.tblcode == CustID && x.gencode == "SICD00101003" && x.repeatsl == "2").ToList();
                var tel1 = custDetails.FindAll(x => x.tblcode == CustID && x.gencode == "SICD00101004" && x.repeatsl == "1").ToList();

                string CustAdd1 = (add1.Count > 0 ? add1[0].dataval.Trim() : "");
                CustAdd1 = CustAdd1 + (CustAdd1.Length > 0 && add2.Count > 0 ? ", " : "") + (add2.Count > 0 ? add2[0].dataval.Trim() : "");
                CustAdd1 = CustAdd1 + (CustAdd1.Length > 0 && tel1.Count > 0 ? ", " : "") + (tel1.Count > 0 ? tel1[0].dataval.Trim() : "");
                this.AccLedgerLst2[0].sirdesc = CustAdd1;

                var LedgMain = this.AccLedgerLst.FindAll(x => x.trcode == "000000000000").ToList();
                var LedgNarr = this.AccLedgerLst.FindAll(x => x.trtyp == "NARRATION000000000000000000000000000000000000000" && x.trdesc.Trim().Length > 0).ToList();
                foreach (var item in LedgMain)
                {
                    var LedgNarr1 = LedgNarr.FindAll(x => x.vounum == item.vounum);
                    if (LedgNarr1.Count > 0)
                        item.trdesc = LedgNarr1[0].trdesc;

                    if (item.trdesc.Trim().Length == 0 && item.vounum.Substring(0, 3) == "RVC")
                        item.trdesc = "Cash Collection";

                    if (item.drcr == "1O" || item.drcr == "3T" || item.drcr == "4C")
                        item.vounum1 = "";

                }
                this.AccLedgerLst = LedgMain.ToList();

                if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
                {
                    var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
                    LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccLedger2", this.AccLedgerLst, this.AccLedgerLst2, list3, null);

                    string WindowTitle1 = "Account Subsidiary Ledger";
                    string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                    //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
                }
                else if (PrintId == "SS")
                {
                    this.ShowGridInfo(TrTyp);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-12 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void GetControlSchedule(string TrHead1, string TrTyp, string PrintId)
        {
            try
            {
                // string PrintId, string BrnCod, string SectCod, string SirCode, string AccLevel, string LSirLevel, string Period, string DrCr
                string fromDate = xctk_dtpFrom.Text.Trim();
                string ToDate = xctk_dtpTo.Text.ToString().Trim();

                var pap1 = vmrptAcc.SetParamAccSchedule(WpfProcessAccess.CompInfList[0].comcod, fromDate, ToDate, "%", "000000000000", "180100010001", "%", "4", "5", "ALLPERIOD", "BOTHDRCR");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                //ds1.Tables[1].Rows[0]["rptLevel"] = this.TBLevelSetup(ds1.Tables[1].Rows[0]["rptLevel"].ToString().Trim(), ds1.Tables[1].Rows[0]["rptLevel2"].ToString().Trim());

                //List<HmsEntityAccounting.AccTrialBalance1> list1 = new List<HmsEntityAccounting.AccTrialBalance1>();
                //List<HmsEntityAccounting.AccTrialBalance1t> list2 = new List<HmsEntityAccounting.AccTrialBalance1t>();
                this.RptTrialBalanceList1.Clear();
                this.RptTrialBalanceList2.Clear();
                this.RptTrialBalanceList1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTrialBalance1>();
                this.RptTrialBalanceList2 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.AccTrialBalance1t>();

                string TrHead = ((TreeViewItem)(this.tvRptRtTitle.SelectedItem)).Header.ToString().ToUpper();
                if (this.RptTrialBalanceList1 == null)
                    return;
                string rptName = "SCHEDULE";
                if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
                {
                    var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);

                    var list4 = new Hashtable();
                    string fromDatep = Convert.ToDateTime(fromDate).AddDays(-1).ToString("dd-MMM-yyyy");

                    list4["ToDate"] = ToDate;
                    list4["fromDatep"] = fromDatep;
                    list4["fromDate"] = fromDate;
                    list4["RptTitle"] = TrHead.Remove(0, 3);// +" - (Level - " + list2[0].rptLevel + ")";
                    list4["Period"] = "(From " + fromDate + " To " + ToDate + ")" + (this.RptTrialBalanceList2[0].CurrPeriod.Trim().Length > 0 ? " - " + this.RptTrialBalanceList2[0].CurrPeriod.Trim() : "");
                    list4["ReportType"] = rptName;
                    //Rpt1a.SetParameters(new ReportParameter("ParamPeriod", lst4["Period"].ToString()));
                    //=Parameters!ParamTitle.Value & " - (Level - " & First(Fields!rptLevel.Value, "RptDataSet2") & ")"
                    LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccTrialBal1", this.RptTrialBalanceList1, this.RptTrialBalanceList2, list3, list4);

                    string WindowTitle1 = TrHead.Remove(0, 3);// "Account Trial Balance";
                    string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                    //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
                }
                else if (PrintId == "SS")
                {
                    this.ShowGridInfo(TrTyp);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-11 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        //private string TBLevelSetup(string Level1 = "", string Level2 = "")
        //{
        //    string Level1a = "";
        //    for (int i = 0; i < Level1.Length; i++)
        //    {
        //        Level1a += Level1.Substring(i, 1) + ", ";
        //    }
        //    Level1a = Level1a.Substring(0, Level1a.Length - 2);
        //    Level1a = Level1a + (Level2.Length > 0 ? " / " + (Level2 == "B" ? "Branch" : (Level2 == "L" ? "Location" : "Sub-" + Level2)) : "");
        //    return Level1a;
        //}

        private void GetCashReceivedStatement(string TrHead, string TrTyp, string PrintId)
        {
            try
            {
                string fromDate = xctk_dtpFrom.Text.Trim();
                string ToDate = xctk_dtpTo.Text.ToString().Trim();
                var pap1 = vm1.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, TrTyp, fromDate, ToDate);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                //List<HmsEntityCommercial.RetSalesCashRecv1> list1 = new List<HmsEntityCommercial.RetSalesCashRecv1>();
                this.RptSalesCashRecvList1.Clear();
                this.RptSalesCashRecvList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.RetSalesCashRecv1>();

                if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
                {
                    var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
                    list3[0].RptHeader1 = "Money Received Statement";
                    list3[0].RptHeader2 = " ( From  " + fromDate + "  To  " + ToDate + " )";
                    LocalReport rpt1 = CommReportSetup.GetLocalReport("RetSales.RetSalesCashRecv1", this.RptSalesCashRecvList1, null, list3);
                    string WindowTitle1 = "Money Received Statement";
                    string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                    //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
                }
                else if (PrintId == "SS")
                {
                    this.ShowGridInfo(TrTyp);
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-13 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void GetSumTransListReport(string TrHead, string TrTyp, string PrintId)
        {
            try
            {
                string fromDate = xctk_dtpFrom.Text.ToString().Trim();
                string ToDate = xctk_dtpTo.Text.ToString().Trim();

                string Dept01 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString().Trim();
                string CmbShop = ((ComboBoxItem)this.cmbShop.SelectedItem).Tag.ToString().Trim();


                string ItemGrp1 = ((ComboBoxItem)(this.cmbItemGroup.SelectedItem)).Tag.ToString().Trim().Substring(0, 7);
                string ItemGrp1des = ((ComboBoxItem)(this.cmbItemGroup.SelectedItem)).Content.ToString().Trim();
                string itemCode1 = (this.autoItemSearch.SelectedValue == null ? "" : this.autoItemSearch.SelectedValue.ToString().Trim());
                string itemCode1des = (this.autoItemSearch.SelectedValue == null ? "" : this.autoItemSearch.SelectedText.Trim());
                ItemGrp1des = (ItemGrp1 == "0000000" ? "" : ItemGrp1des);
                ItemGrp1 = (ItemGrp1 == "0000000" ? "%" : ItemGrp1);
                ItemGrp1des = (itemCode1.Length > 0 ? itemCode1des : ItemGrp1des);
                ItemGrp1 = (itemCode1.Length > 0 ? itemCode1 : ItemGrp1);

                //this.lbltle1.Content = TrHead.Remove(0, 2);
                //this.lbltle2.Content = " From " + fromDate + " To " + ToDate;
                string TransType = (TrTyp == "C00DAILYSALES" ? "GSI" : "");
                string Cust1Id = (this.autoCustSearch.SelectedValue == null ? "" : this.autoCustSearch.SelectedValue.ToString());
                string Cust1des = (this.autoCustSearch.SelectedValue == null ? "" : this.autoCustSearch.SelectedText.Trim());


                var pap1 = vm1.SetParamSalesTransList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, TrTyp: TrTyp, FromDate: fromDate, ToDate: ToDate,
                           DeptID1: "%", DeptID2: TransType, CustID1: Cust1Id, InvStatus1: "A", ItemID1: ItemGrp1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                if (ds1 == null)
                    return;

                this.RptList.Clear();
                DateTime ServerTime1 = DateTime.Now;

                if (PrintId == "PP" || PrintId == "NP" || PrintId == "PDF" || PrintId == "WORD" || PrintId == "EXCEL")
                {
                    switch (TrTyp)
                    {
                        case "A00MSISUM":
                            this.RptList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>().ToList().OrderBy(x => x.invno).ToList();
                            int sl1 = 1;
                            DateTime OldDt1 = DateTime.Parse("01-Jan-1900");
                            foreach (var item1 in this.RptList)
                            {
                                if (item1.invdat != OldDt1)
                                {
                                    OldDt1 = item1.invdat;
                                    sl1 = 1;
                                }
                                item1.slnum = sl1;
                                sl1++;
                            }
                            this.RptList = RptList.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
                            this.PrintTransecList(this.RptList, ServerTime1, Cust1des);
                            // this.prepareDtgrSlsInv();
                            break;

                        case "A00MSIDETAILS":
                        case "A00ITEMSUMMARY":
                        case "A00TOPSUMMARY":
                            this.RptList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList2>();
                            int sln2 = 1;
                            var OldMemo = "xxxxxxxxxxxx";
                            foreach (var item2 in this.RptList1)
                            {
                                if (item2.invno != OldMemo)
                                {
                                    OldMemo = item2.invno;
                                    sln2 = 1;
                                }
                                item2.slnum = sln2;
                                sln2++;
                            }

                            this.RptList1 = RptList1.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
                            this.PrintSalesDetailsList(this.RptList1, TrTyp, ServerTime1, Cust1des, ItemGrp1des);
                            //this.prepareDtgrSlsInv02();
                            break;
                        case "A00COLLDETAILS":
                            this.RptList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvColList01>();
                            this.RptList2 = RptList2.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            ServerTime1 = Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]);
                            this.PrintCollDetailsList(this.RptList2);
                            //this.prepareDtgrSlsInv03();
                            break;
                        case "A00DUEDETAILS":
                            this.RptList3 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvDuesList01>();
                            this.RptList3 = RptList3.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            this.PrintDueDetailsList(this.RptList3);
                            // this.prepareDtgrSlsInv04();
                            break;
                        case "C01SIV":
                            this.RptList4 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
                            this.RptList4 = RptList4.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            this.PrintSalesInvoiceList(this.RptList4);
                            // this.prepareDtgrSlsInv04();
                            break;
                        case "C00DAILYSALES":
                            this.RptDailySalesList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.RetSalesTransList2>();
                            this.PrintDailySalesInvoiceList01(this.RptDailySalesList1);
                            return;
                        //case "C00CUSTCASHCOL":
                        //    this.GetCashReceivedStatement(TrHead, TrTyp, PrintId); 
                        //    return;

                        default:
                            break;
                    }
                }
                else if (PrintId == "SS")
                {
                    switch (TrTyp)
                    {
                        case "A00MSISUM":
                            this.RptList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                            this.RptList = RptList.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            this.ShowGridInfo(TrTyp);
                            break;
                        case "A00MSIDETAILS":
                        case "A00ITEMSUMMARY":
                            this.RptList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList2>();
                            this.RptList1 = RptList1.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            this.ShowGridInfo(TrTyp);
                            break;
                        case "A00COLLDETAILS":
                            this.RptList2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvColList01>();
                            this.RptList2 = RptList2.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            break;
                        case "A00DUEDETAILS":
                            this.RptList3 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvDuesList01>();
                            this.RptList3 = RptList3.FindAll(x => x.invno.Substring(0, 3) == CmbShop || x.comcod == "AAAA");
                            PrintDueDetailsList(RptList3);
                            break;
                        case "C00DAILYSALES":
                            this.RptDailySalesList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.RetSalesTransList2>();
                            this.ShowGridInfo(TrTyp);
                            break;
                        //case "C00CUSTCASHCOL":
                        //    this.RptSalesCashRecvList1 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.RetSalesCashRecv1>();
                        //    this.ShowGridInfo(TrTyp);
                        //    break;
                        //case "C00CUSTLEDGERSUM":
                        //    this.RptTrialBalanceList1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccTrialBalance1>();
                        //    this.ShowGridInfo(TrTyp);
                        //    break;
                        default:
                            break;
                    }
                }
                else if (PrintId == "DP")
                {

                }
                else if (PrintId == "EXCELF")
                {

                }
                else if (PrintId == "WORD")
                {

                }
                else
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-04 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void prepareDtgrSlsInv()
        {
            try
            {
                Style style1 = new Style(typeof(DataGridCell));
                style1.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));

                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "SL No.", Binding = new Binding("slnum") });
                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Memo No.", Binding = new Binding("invno1") });

                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Bill Amount", Binding = new Binding("billam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Collection Amount", Binding = new Binding("collam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Due Amount", Binding = new Binding("dueam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "VAT", Binding = new Binding("tvatam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Discount", Binding = new Binding("tdisam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });

                this.dgOverall01.ItemsSource = this.RptList;
                this.dgOverall01.Items.Refresh();
                ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
                if (cvTasks != null && cvTasks.CanGroup == true)
                {
                    cvTasks.GroupDescriptions.Clear();
                    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("invdat", new RelativeDateValueConverter()));
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-05 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void prepareDtgrSlsInv02()
        {
            try
            {
                Style style1 = new Style(typeof(DataGridCell));
                style1.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));

                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "SL No.", Binding = new Binding("slnum") });
                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Item Description", Binding = new Binding("sirdesc"), Width = 350 });

                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Quantity", Binding = new Binding("invqty") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Amount", Binding = new Binding("inetam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });

                this.dgOverall01.ItemsSource = this.RptList1;
                this.dgOverall01.Items.Refresh();
                ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
                if (cvTasks != null && cvTasks.CanGroup == true)
                {
                    cvTasks.GroupDescriptions.Clear();
                    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("invdat1", new RelativeDateValueConverter()));
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-06 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void PrintDailySalesInvoiceList01(List<HmsEntityCommercial.RetSalesTransList2> list1)
        {
            try
            {
                string frmdat = xctk_dtpFrom.Text.ToString();
                string todat = xctk_dtpTo.Text.ToString();
                LocalReport rpt1 = null;
                var list3 = WpfProcessAccess.GetRptGenInfo();
                list3[0].RptHeader1 = "Daily Sales Statement";
                list3[0].RptHeader2 = " ( From  " + frmdat + "  To  " + todat + " )";
                if (this.autoCustSearch.SelectedValue != null)
                    list3[0].RptHeader2 = list3[0].RptHeader2 + "\n" + this.autoCustSearch.SelectedText.Trim();
                //list3[0].RptFooter1 = "User : " + WpfProcessAccess.SignedInUserList[0].signinnam;

                rpt1 = CommReportSetup.GetLocalReport("RetSales.RetSalesTransList2", list1, null, list3);

                string WindowTitle1 = "Daily Sales Statement";
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp1)
            {
                System.Windows.MessageBox.Show("GSI.RPT-01 : " + exp1.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        public void PrintSalesInvoiceList(List<HmsEntityCommercial.PhSalesInvoice01> list1)
        {
            try
            {
                //string memoNum = ((ComboBoxItem)this.cmbPrevInvList.SelectedItem).Tag.ToString();
                LocalReport rpt1 = null;
                // var pap1 = vm1.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
                var pap1 = vm1.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                //this.lblTokenSlNo.Content = ds1.Tables[2].Rows[0]["tokenid"].ToString().Trim();
                ds1.Tables[0].Rows[0]["slnum"] = Convert.ToInt32(ds1.Tables[2].Rows[0]["tokenid"]);
                //var list3 = WpfProcessAccess.GetRptGenInfo(InputSource: "Test Input Source\n");
                var list3 = WpfProcessAccess.GetRptGenInfo();
                list3[0].RptFooter1 = "User : " + WpfProcessAccess.SignedInUserList[0].signinnam;
                //var list1 = ds1.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
                var list2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                // var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();

                //rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesInv01", list1, list2, list3);
                rpt1 = CommReportSetup.GetLocalReport("RetSales.RetSalesInv01", list1, list2, list3);
                string WindowTitle1 = "Due Details List";
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

                #region Print View Comment

                //if (Duplicate.Length > 0)
                //{
                //    rpt1.SetParameters(new ReportParameter("ParamAddress1", "[Re-Print/Duplicate Invoice]"));
                //    //rpt1.SetParameters(new ReportParameter("ParamAddress2", "========================="));
                //}

                //windoeTitle1 = "Sales Memo";

                //if (ViewPrint == "View")
                //{
                //    HmsReportViewer1 window1 = new HmsReportViewer1(rpt1);
                //    window1.Title = windoeTitle1;
                //    //window1.Owner = Application.Current.MainWindow;
                //    window1.ShowDialog();
                //}
                //else if (ViewPrint == "DirectPrint")
                //{
                //    RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                //    DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                //    if (Duplicate.Length == 0)
                //    {
                //        rpt1.SetParameters(new ReportParameter("ParamTitle1", "Kitchen Order Token (KOT)"));
                //        DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                //    }
                //    DirectPrint1.Dispose();
                //}                
                #endregion
            }
            catch (Exception exp1)
            {
                System.Windows.MessageBox.Show("GSI.RPT-02 : " + exp1.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void PrintDueDetailsList(List<HmsEntityCommercial.InvDuesList01> list1)
        {
            try
            {
                string fromDate = xctk_dtpFrom.Text.ToString();
                string ToDate = xctk_dtpTo.Text.ToString();

                if (list1 == null)
                    return;
                var list3 = WpfProcessAccess.GetRptGenInfo();
                list3[0].RptHeader1 = "Due Details List ";
                list3[0].RptHeader2 = " ( From  " + fromDate + "  To  " + ToDate + " )";
                LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhDueList01", list1, null, list3);
                string WindowTitle1 = "Due Details List";
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-07 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void PrintCollDetailsList(List<HmsEntityCommercial.InvColList01> list1)
        {
            try
            {
                string frmdat = xctk_dtpFrom.Text.ToString();
                string todat = xctk_dtpTo.Text.ToString();
                if (list1 == null)
                    return;
                var list3 = WpfProcessAccess.GetRptGenInfo();
                list3[0].RptHeader1 = "Collection Details List";
                list3[0].RptHeader2 = " ( From  " + frmdat + "  To  " + todat + " )";
                LocalReport rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhInvCollList01", list1, null, list3);
                string WindowTitle1 = "Collection Details List";
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-08 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void PrintSalesDetailsList(List<HmsEntityCommercial.InvoiceTransList2> list1, string TrTyp, DateTime ServerTime1, string Cust1des1, string ItemGrp1des1)
        {
            try
            {
                string fromDate = xctk_dtpFrom.Text.ToString();
                string ToDate = xctk_dtpTo.Text.ToString();

                if (list1 == null)
                    return;
                //     case "A00MSIDETAILS":                    case "A00ITEMSUMMARY":

                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: ServerTime1);
                list3[0].RptHeader1 = (TrTyp == "A00TOPSUMMARY" ? "Overall Sales Summary" : (TrTyp == "A00ITEMSUMMARY" ? "Item wise Sales Summary" : "Sales Details List")) + (ItemGrp1des1.Length > 0 ? " - [" + ItemGrp1des1 + "]" : "");
                list3[0].RptHeader2 = " ( From  " + fromDate + "  To  " + ToDate + " )" + (Cust1des1.Length > 0 ? " - [" + Cust1des1 + "]" : "");
                list3[0].RptParVal1 = (TrTyp == "A00TOPSUMMARY" ? "TOPSUM" : (TrTyp == "A00ITEMSUMMARY" ? "ITEMSUM" : "DETAILSSUM"));
                LocalReport rpt1 = CommReportSetup.GetLocalReport("RetSales.RetSalesDetailsList1", list1, null, list3);
                string WindowTitle1 = (TrTyp == "A00TOPSUMMARY" ? "Overall Sales Summary" : (TrTyp == "A00ITEMSUMMARY" ? "Item wise Sales Summary" : "Sales Transaction Details List"));
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-09 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void PrintTransecList(List<HmsEntityCommercial.InvoiceTransList> list1, DateTime ServerTime1, string Cust1des1)
        {
            try
            {
                string fromDate = xctk_dtpFrom.Text.ToString();
                string ToDate = xctk_dtpTo.Text.ToString();

                if (list1 == null)
                    return;
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: ServerTime1);
                list3[0].RptHeader1 = "Sales Invoice List";
                list3[0].RptHeader2 = " ( From  " + fromDate + "  To  " + ToDate + " )" + (Cust1des1.Length > 0 ? " - [" + Cust1des1 + "]" : ""); ;
                LocalReport rpt1 = CommReportSetup.GetLocalReport("RetSales.RetSalesTransList1", list1, null, list3);
                string WindowTitle1 = "Sales Transaction List";
                string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
                //string RptDisplayMode = (pout1 == "PDF" ? "PDF" : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-10 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
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
            var GrpCod1 = ((ComboBoxItem)this.cmbItemGroup.SelectedItem).Tag.ToString().Trim();
            if (GrpCod1 != "000000000000")
                return new ObservableCollection<vmEntryPharRestPOS1.RetSaleItem>(
                    this.RetSaleItemList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim()) && x.sircode.Substring(0, 7) == GrpCod1.Substring(0, 7)).Take(100));
            else
                return new ObservableCollection<vmEntryPharRestPOS1.RetSaleItem>(
                    this.RetSaleItemList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
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

        private void cmbItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.autoItemSearch.ItemsSource = null;
        }

        private void cmbSBrnCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ShowGridInfo(string ItemTag)
        {

            try
            {
                //string fromDate = xctk_dtpFrom.Text.Trim();
                //string ToDate = xctk_dtpTo.Text.ToString().Trim();
                //string TrHead = ((TreeViewItem)(this.tvRptTitle.SelectedItem)).Header.ToString();
                //string From2Date = " From " + fromDate + " To " + ToDate;

                if (this.stkpDataGrid.Children.Count > 0)// && !(ItemTag == "B04CL" || ItemTag == "B05SL"))
                    this.stkpDataGrid.Children.Clear();
                switch (ItemTag)
                {
                    case "A00MSIDETAILS":
                    case "A00ITEMSUMMARY":
                        this.dgRpt1 = GridReportGenTr01.SlsInv02.GetDataGrid(this.RptList1);
                        this.stkpDataGrid.Children.Add(this.dgRpt1);
                        break;
                    case "A00MSISUM":
                        this.dgRpt1 = GridReportGenTr01.SlsInv01.GetDataGrid(this.RptList);
                        this.stkpDataGrid.Children.Add(this.dgRpt1);
                        break;
                    case "C00DAILYSALES":
                        this.dgRpt1 = GridReportGenTr01.DailySales01.GetDataGrid(this.RptDailySalesList1);
                        this.stkpDataGrid.Children.Add(this.dgRpt1);
                        break;
                    case "C00CUSTCASHCOL":
                        this.dgRpt1 = GridReportGenTr01.SalesCashRecv1.GetDataGrid(this.RptSalesCashRecvList1);
                        this.stkpDataGrid.Children.Add(this.dgRpt1);
                        break;
                    case "C00CUSTLEDGERSUM":
                        this.dgRpt1 = GridReportGenTr01.TrialBalance.GetDataGrid(this.RptTrialBalanceList1);
                        this.stkpDataGrid.Children.Add(this.dgRpt1);
                        break;
                    case "C00CUSTLEDGER":
                        this.dgRpt1 = GridReportGenTr01.CUSTLEDGER.GetDataGrid(this.AccLedgerLst);
                        this.stkpDataGrid.Children.Add(this.dgRpt1);
                        break;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI.RPT-03 : " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void chkAsonDate_Click(object sender, RoutedEventArgs e)
        {
            this.stkpDateFrom.Visibility = (this.chkAsonDate.IsChecked == true ? Visibility.Hidden : Visibility.Visible);
        }
        private void tvRptRtTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnGenerate_Click(null, null);
        }

        private void tvRptRtTitle_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            this.cmbOutputOption.ComboBox_ContextMenuOpening(null, null);
        }

        private void tvRptRtTitle_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            string yy = this.cmbOutputOption.Uid.ToString();
            if (yy != "NONE")
                this.btnGenerate_Click(null, null);
        }

        private void tvRptRtTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.btnGenerate_Click(null, null);
        }

        private void autoStaffSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

        }

    }
}
