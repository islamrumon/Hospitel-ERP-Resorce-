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
    /// Interaction logic for frmEntryPurReqAppr1.xaml
    /// </summary>

    public partial class frmEntryPurReqAppr1 : UserControl
    {
        private bool FrmInitialized = false;
        private List<vmEntryPurReqAppr1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPurReqAppr1.ListViewItemTable>();
        private List<HmsEntityInventory.InvTransectionList> ListViewTransTable1 = new List<HmsEntityInventory.InvTransectionList>();
        vmEntryPurReqAppr1 vm1 = new vmEntryPurReqAppr1();
        vmReportStore1 vm1r = new vmReportStore1();
        public int serialno = 0;
        bool manualTextChange = true;
        public bool IsActiveTransListWindow { get; set; }
        
        public frmEntryPurReqAppr1()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
             if (DesignerProperties.GetIsInDesignMode(this))
                return;

             if (!this.FrmInitialized)
             {
                 this.FrmInitialized = true;
                 IsActiveTransListWindow = false;
                 ConstructAutoCompletionSource();
                 this.ListView1.Tag = "RSIRCOD";     

                 this.chkAutoTransList.IsChecked = this.IsActiveTransListWindow;
                 this.xctk_dtpApprovDat.IsEnabled = false;
                 this.btnPrint2.Visibility = Visibility.Hidden;
                 this.btnUpdate.Visibility = Visibility.Hidden;
                 this.gridDetails.Visibility = Visibility.Hidden;
                 this.xctk_dtpApprovDat.Value = DateTime.Today;
                 this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-33);
                 this.xctk_dtpToDate.Value = DateTime.Today;

                 if (IsActiveTransListWindow)
                     this.gridTransList.Visibility = Visibility.Visible;
                 else
                     this.gridTransList.Visibility = Visibility.Hidden;
             }
        }

        private void ConstructAutoCompletionSource()
        {
            this.cmbfDept.Items.Add(new ComboBoxItem() { Content = "ALL", Tag = "%" });//31
            var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");

            foreach (var itemd1 in deptList1)
            {
                //this.cmbSectCodpr.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                if (itemd1.sectname.ToUpper().Contains("STORE"))
                    this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                this.cmbfDept.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
            }

            if (WpfProcessAccess.SupplierContractorList == null)
                WpfProcessAccess.GetSupplierContractorList();


            foreach (var item1 in WpfProcessAccess.SupplierContractorList)
            {
                // this.AtxtSsirlst.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
                this.AtxtSsirlst.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());
            }

            this.cmbfDept.SelectedIndex = 0;

            if (WpfProcessAccess.StaffList == null)
                WpfProcessAccess.GetCompanyStaffList();

            this.AtxtApprovById.AutoSuggestionList.Clear();
            foreach (var item1 in WpfProcessAccess.StaffList)
            {
                //this.AtxtApprovById.AutoSuggestionList.Add(item1.sirdesc.Trim() + " : [" + item1.sircode + "]");
                this.AtxtApprovById.AddSuggstionItem(item1.sirdesc.Trim(), item1.sircode.Trim());
            }


            if (WpfProcessAccess.InvItemList == null)
                WpfProcessAccess.GetInventoryItemList();


            string FromDate = DateTime.Today.AddDays(-33).ToString();
            string ToDate = DateTime.Today.ToString();

            ASITFunParams.ProcessAccessParams pap1 = vm1r.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "REQ", FromDate, ToDate, "%", "%");
            DataSet ds5 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds5 == null)
                return;

            var reqno = ds5.Tables[0].DataTableToList<HmsEntityInventory.InvTransectionList>();


            foreach (var item in reqno)
            {
                cmbreqno.Items.Add(new ComboBoxItem() { Content = item.memonum.ToString() });
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
            this.ListView2.Items.Clear();
            this.xctk_dtpApprovDat.IsEnabled = false;
            if (this.btnOk.Content.ToString() == "_New")
            {
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.stkIntro.IsEnabled = true;
                this.cmbSectCod.Text = "";
                this.AtxtApprovById.Text = "";
                this.btnReferAp.Visibility = Visibility.Hidden;
                this.AtxtSsirlst.Text = "";
                this.txtApprovRef.Text = "";
                this.txtApprovNar.Text = "";
                this.txtUID.Text = "";
                this.txtRSirCode.Text = "";
                this.txtRSirDesc.Text = "";
                this.txtApprovQty.Text = "";
                this.lblAmount.Content = "";
                this.txtApprovRat.Text = "";
                this.lblUnit1.Content = "";
                this.lblaprovno.Content = "PAPMM-CCCC-XXXXX";
                this.lblaprovno.Tag = "PAPYYYYMMCCCCXXXXX";
                if (IsActiveTransListWindow)
                {
                    this.BuildTransactionList();
                    this.gridTransList.Visibility = Visibility.Visible;
                    this.lvTransList.Focus();
                }
                this.btnOk.Content = "_Ok";
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
            this.stkIntro.IsEnabled = false;
            this.btnOk.Content = "_New"; 

        }

        private bool checkOkValidation()
        {
            //string ApprovByID1 = this.AtxtApprovById.Text.Trim();
            string ApprovByID1 = this.AtxtApprovById.Value.Trim();
            //int length1 = ApprovByID1.Length - 13;
            if (ApprovByID1.Length < 0)
                return false;
            //string ApprovByID2 = ApprovByID1.Substring(ApprovByID1.Length - 13).Replace("]", "");

            var listStaff1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == ApprovByID1);
            return (listStaff1.Count > 0);
        }
        private void BuildTransactionList()
        {
            string FromDate = this.xctk_dtpFromDate.Text;
            string ToDate = this.xctk_dtpToDate.Text;

            string cmbDept = ((ComboBoxItem)this.cmbfDept.SelectedItem).Tag.ToString();           
            if (cmbDept == "%")
            {
                this.txtTransTitle.Text = "All Transaction List From : " + FromDate + " To : " + ToDate;
            }
            else
                {
                    this.txtTransTitle.Text = " Transaction List From : " + FromDate + " To : " + ToDate + " Store Id : " + cmbDept.Trim();
                }
            ASITFunParams.ProcessAccessParams pap1 = vm1r.SetParamStoreTransList(WpfProcessAccess.CompInfList[0].comcod, "PAP", FromDate, ToDate, cmbDept);
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
        private void btnImgV_Click(object sender, RoutedEventArgs e)
        {
            string btnNam1 = ((Button)sender).Name.ToString().Trim();
            switch (btnNam1)
            {
                case "btntxtCross":
                    this.txtUID.Text = "";
                    txtRSirDesc.Clear();
                    txtRSirCode.Clear();
                    btntxtCross.Visibility = Visibility.Collapsed;
                    break;
                case "btntxtNtCross":
                    txtApprovNote.Clear();
                    btntxtNtCross.Visibility = Visibility.Collapsed;
                    break;
                case "btnReferAp":
                    txtApprovRef.Clear();
                    btnReferAp.Visibility = Visibility.Collapsed;
                    break;
            }
         }
        private void txtCodeDesc_TextChanged(object sender, TextChangedEventArgs e)
        {
            btntxtCross.Visibility = (txtRSirDesc.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            btntxtNtCross.Visibility = (txtApprovNote.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
            btnReferAp.Visibility = (txtApprovRef.Text.Length > 0 ? Visibility.Visible : Visibility.Hidden);
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
                        ListView1.Items.Add(new vmEntryPurReqAppr1.ListViewItemSelect { trcode = item1b.trcode, trdesc = item1b.trdesc, truid = item1b.truid, trunit = item1b.trunit });
                    if (ListView1.Items.Count > 0)
                    {
                        var lvi1 = (vmEntryPurReqAppr1.ListViewItemSelect)ListView1.Items[0];
                        this.txtRSirDesc.Text = lvi1.trdesc;
                        this.txtRSirCode.Text = lvi1.trcode;
                        this.txtUID.Text = lvi1.truid;
                        this.lblUnit1.Content = lvi1.trunit;
                    }

                    break;
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
                                      truid = lst.sirtype,
                                      trunit = lst.sirunit
                                  });
                    foreach (var item1b in List1a)
                        ListView1.Items.Add(new vmEntryPurReqAppr1.ListViewItemSelect { trcode = item1b.trcode, trdesc = item1b.trdesc, truid = item1b.truid, trunit = item1b.trunit });
                    break;
            }
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
           MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpApprovDat.Text),
                cbSectCode: cbSectCode1, SsirCod: this.AtxtSsirlst.Value.Trim(), approvByID1: this.AtxtApprovById.Value.Trim(),
                MemoRef1: this.txtApprovRef.Text.Trim(), MemoNar1: this.txtApprovNar.Text.Trim(), ListViewItemTable1: this.ListViewItemTable1,
                _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            //String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamUpdatePurReqApproval(WpfProcessAccess.CompInfList[0].comcod, ds1);

            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblaprovno.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString(); ;
            this.lblaprovno.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();

            this.btnUpdate.IsEnabled = false;
            this.stkItem.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;
           
        }

        private void txtCodeDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            string wtxtName1 = ((WatermarkTextBox)sender).Name.ToString().Trim();
            //string tag1 = (wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
            string tag1 = (wtxtName1 == "txtUID" || wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
            if (this.ListView1.Tag.ToString().Trim() != tag1)
                this.ListView1.Items.Clear();
            manualTextChange = true;
            this.ListView1.Tag = tag1;// (wtxtName1 == "txtRSirCode" || wtxtName1 == "txtRSirDesc" ? "RSIRCOD" : "UNKNOWN");
        }

        private void txtUID_LostFocus(object sender, RoutedEventArgs e)
        {
            manualTextChange = false;
            this.PrepareListViewData("RSUID");
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
           manualTextChange = false;

            vmEntryPurReqAppr1.ListViewItemSelect lvi1 = (vmEntryPurReqAppr1.ListViewItemSelect)this.ListView1.SelectedItem;
            //if (ListView1.Tag.ToString().Trim() == "RSIRCOD")
            if (ListView1.Tag.ToString().Trim() == "RSUID" || ListView1.Tag.ToString().Trim() == "RSIRCOD")
            {
                this.txtRSirDesc.Text = lvi1.trdesc;
                this.txtRSirCode.Text = lvi1.trcode;
                this.txtUID.Text = lvi1.truid;
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

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.BuildTransactionList();
            this.chkFilter.IsChecked = false;
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            if (lvTransList.SelectedItem == null)
            {
                return;
            }
            LocalReport rpt1 = null;
            string WindowTitle1 = "";
            if (this.rb3SelectedMemo.IsChecked == true)
            {
                var item1a = (HmsEntityInventory.InvTransectionList)this.lvTransList.SelectedItem;
                var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.memonum);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurApprovMemo>();
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
                rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurReqApprMemo01", list1, list2, list3);
                WindowTitle1 = "Purchase Approval Memo";
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
                WindowTitle1 = "Purchase Approval Transaction List";
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

        private void txtSrfQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumeric(e.Text);
        }
        public static bool onlyNumeric(string text)
        {
            Regex regex = new Regex("^[0-9]*$"); //regex that allows numeric input only
            return !regex.IsMatch(text); // 
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpApprovDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpApprovDat.IsEnabled)
                this.xctk_dtpApprovDat.Focus();
        }

        private void btntxtCross_Click(object sender, RoutedEventArgs e)
        {
            this.txtRSirDesc.Clear();
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
        private void btnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            decimal reqqty1a = this.validData("0" + this.txtApprovQty.Text.Trim());
            if (reqqty1a <= 0)
            {
                this.txtApprovQty.Focus();
                return;
            }
            decimal reqRat1 = this.validData("0" + this.txtApprovRat.Text.Trim());
            decimal reqAmt1 = Math.Round(reqqty1a * reqRat1, 6) ;
            if (reqAmt1 <= 0)
            {
                this.txtApprovRat.Focus();
                return;
            }


            if (this.txtRSirDesc.Text.Trim().Length == 0)
            {
                this.txtRSirCode.Text = "";
                this.txtUID.Text = "";
                this.lblUnit1.Content = "";
                this.txtApprovQty.Text = "";
                this.lblAmount.Content = "";
                this.txtApprovRat.Text = "";
                this.txtApprovNote.Text = "";
                this.cmbpaytype.Text = "";
            }

            vmEntryPurReqAppr1.ListViewItemSelect lvi1 = (vmEntryPurReqAppr1.ListViewItemSelect)this.ListView1.SelectedItem;
            int serialno1 = ListView2.Items.Count + 1;
            string rsircode1 = this.txtRSirCode.Text.Trim();
            string rsirdesc1 = this.txtRSirDesc.Text.Trim();
            string rsuid1 = this.txtUID.Text.Trim();
            string rsirunit = this.lblUnit1.Content.ToString();
            if (rsircode1.Length == 0)
                return;

            var list1a = ListViewItemTable1.FindAll(x => x.rsircode == rsircode1);
            if (list1a.Count > 0)
            {
                System.Windows.MessageBox.Show("Item ID: " + rsircode1 + " already exist in data table", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            var item1a = new vmEntryPurReqAppr1.ListViewItemTable()
            {
                trsl = serialno1.ToString() + ".",
                reqno = cmbreqno.Text.ToString(),
                rsircode = rsircode1,
                trdesc = rsircode1 + ": " + rsirdesc1,
                aprovqty = reqqty1a,
                 truid = rsuid1,
                trunit = rsirunit,
                aprovrate = reqRat1,
                aprovamt = reqAmt1,
                paytype = cmbpaytype.Text.Trim(),
                aprovnote = txtApprovNote.Text
            };

            ListViewItemTable1.Add(item1a);
            ListViewItemTable1.Sort(delegate(vmEntryPurReqAppr1.ListViewItemTable x, vmEntryPurReqAppr1.ListViewItemTable y)
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
            this.txtRSirCode.Text = "";
            this.txtRSirDesc.Text = "";
            this.txtUID.Text = "";
            this.lblUnit1.Content = "";
            this.txtApprovQty.Text = "";
            this.lblAmount.Content = "";
            this.txtApprovRat.Text = "";
            this.txtApprovNote.Text = "";
            this.cmbpaytype.Text = "";
            this.cmbreqno.Text = "";
            this.ListView2.Focus();
        }

        private void btnAddAllRecords_Click(object sender, RoutedEventArgs e)
        {
            // Tobe Add all records in specific Requisition at a time
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
            this.txtUID.Text = "";
            this.lblUnit1.Content = "";
            this.txtApprovQty.Text = "";
            this.lblAmount.Content = "";
            this.txtApprovRat.Text = "";

            int RowIndex1 = int.Parse(((Hyperlink)sender).Tag.ToString().Replace(".", "").Trim());

            var tblItm1 = ListViewItemTable1[RowIndex1 - 1];
            var tblitm2 = WpfProcessAccess.InvItemList.FindAll(x => x.sircode == tblItm1.rsircode);

            this.txtApprovQty.Text = tblItm1.aprovqty.ToString();
            this.txtApprovRat.Text = tblItm1.aprovrate.ToString();          

            this.txtRSirDesc.Text = tblitm2[0].sirdesc;
            this.txtUID.Text = tblitm2[0].sirtype;
            this.lblUnit1.Content = tblitm2[0].sirunit;
            this.txtRSirCode.Text = tblItm1.rsircode;
            ListViewItemTable1.RemoveAt(RowIndex1 - 1);
            this.ListView2.Items.Clear();
            int i = 1;

            ListViewItemTable1.Sort(delegate(vmEntryPurReqAppr1.ListViewItemTable x, vmEntryPurReqAppr1.ListViewItemTable y)
            {
                return x.rsircode.CompareTo(y.rsircode);
            });


            foreach (var item1a in ListViewItemTable1)
            {
                item1a.trsl = i.ToString() + ".";
                ListView2.Items.Add(item1a);
                i++;
            }
        }

        
        private void txtApprovQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtApprovRat_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ClaculateAmt();
        }

        private void txtApprovQty_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.ClaculateAmt();
        }
        private Double validData1(string txtData)
        {
            try
            {
                return double.Parse(txtData);
            }
            catch (Exception)
            {
                return 0;
            }

        }
        private void ClaculateAmt()
        {
            Double quantity = this.validData1("0" + this.txtApprovQty.Text.ToString());
            Double Rate = this.validData1("0" + this.txtApprovRat.Text.ToString());
            Double Amount = quantity * Rate;
            this.lblAmount.Content = Amount.ToString("#,##0.00"); 
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            var item1a = this.lblaprovno.Tag.ToString();
            var pap1 = vm1r.SetParamStoreTransMemo(WpfProcessAccess.CompInfList[0].comcod, item1a);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            var list1 = ds1.Tables[1].DataTableToList<HmsEntityInventory.PurApprovMemo>();
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
            LocalReport Rpt1 = StoreReportSetup.GetLocalReport("Procurement.PurReqApprMemo01", list1, list2, list3);
            if (Rpt1 == null)
                return;

            RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
            DirectPrint1.PrintReport(Rpt1);
            DirectPrint1.Dispose();
        }

        private void btntxtNtCross_Click(object sender, RoutedEventArgs e)
        {
            this.txtApprovNote.Clear();
        }      
    }
}
