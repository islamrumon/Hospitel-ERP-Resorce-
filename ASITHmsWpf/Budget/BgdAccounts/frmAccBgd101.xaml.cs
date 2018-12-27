using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Accounting;
using ASITHmsViewMan.Budget;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using ASITHmsRpt1GenAcc.Accounting;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ASITHmsWpf.Budget.BgdAccounts
{
    /// <summary>
    /// Interaction logic for frmAccBgd101.xaml
    /// </summary>
    public partial class frmAccBgd101 : UserControl
    {
        private bool FrmInitialized = false;
        string TitaleTag1, TitaleTag2, TitaleTag3;  // 

        private List<vmBgdAccounts1.PropTable> ListBppTable1 = new List<vmBgdAccounts1.PropTable>();
        private vmBgdAccounts1 vm1 = new vmBgdAccounts1();


        private List<HmsEntityGeneral.AcInfCodeBook> CactcodeList = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<HmsEntityGeneral.AcInfCodeBook> ActcodeList = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<HmsEntityAccounting.PayProTransectionList> BppTrnLst = new List<HmsEntityAccounting.PayProTransectionList>();
        private vmReportAccounts1 vmrptAcc = new vmReportAccounts1();
        string PrevBppnum = "XXXXXXXXXXXXXXXXXX";
        public bool IsActiveTransListWindow { get; set; }

        DataSet EditDs;

        public frmAccBgd101()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
            this.TitaleTag1 = this.Tag.ToString();   // Predefined value of Tag property set at design time
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
       
            if (!this.FrmInitialized)
            {
                this.xctk_dtpFromDate.Value = Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
                this.xctk_dtpToDate.Value = DateTime.Today;

                // ('PBC', 'PBB')	-- PBC (Payment Budget Cash), PBB (Payment Budget Bank)
                string[] tagPart1 = this.Tag.ToString().Trim().Split(',');
                this.TitaleTag2 = (tagPart1.Length > 0 ? tagPart1[0].Trim() : ""); //this.Tag.ToString(); // Dynamic value of Tag property set at design time
                this.TitaleTag3 = (tagPart1.Length > 1 ? tagPart1[1].Trim() : "");

                this.FrmInitialized = true;
                //this.cmbBppType.Items.Add(new ComboBoxItem() { Content = "Payment Proposal - Cash".ToUpper(), Tag = "PBC84" });
                //this.cmbBppType.Items.Add(new ComboBoxItem() { Content = "Payment Proposal - Bank".ToUpper(), Tag = "PBB84" });

                this.ActivateAuthObjects();
                this.xctk_dtpBppDat.Value = DateTime.Today;
                this.HideObjects_On_Load();
            }
        }
        private void ActivateAuthObjects()
        {

            this.cmbBppType.Items.Clear();
            var ptypeList = HmsEntityAccounting.GetPayProType().FindAll(x => x.vtitle.ToUpper().Contains(TitaleTag2.ToUpper()));// && !x.vtitle.ToUpper().Contains("BUDGET"));
            if (this.TitaleTag3.Length > 0)
                ptypeList = ptypeList.FindAll(x => x.vtagid.Contains(this.TitaleTag3)).ToList();

            foreach (var item1 in ptypeList)
            {
                string uicode1 = "WPF_frmAccBgd101_cmbBppType_" + item1.vtagid;
                var findid1 = WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == uicode1);
                if (findid1 != null)
                    this.cmbBppType.Items.Add(new ComboBoxItem() { Content = item1.vtitle.ToUpper(), Tag = item1.vtagid });
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmAccBgd101_chkDateBlocked") == null)
            {
                this.chkDateBlocked.Visibility = Visibility.Collapsed;
                this.lblDateBlocked.Visibility = Visibility.Visible;
            }

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmAccBgd101_chkAutoTransList") == null)
                this.chkAutoTransList.Visibility = Visibility.Hidden;

            this.btnRecurring.Visibility = Visibility.Hidden;
            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmAccBgd101_btnBppEdit") == null)
                this.btnBppEdit.Visibility = Visibility.Hidden;

            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmAccBgd101_btnBppCancel") == null)
                this.btnBppCancel.Visibility = Visibility.Hidden;
        }


        private void HideObjects_On_Load()
        {
            this.gridTransList.Visibility = Visibility.Collapsed;
            this.gridDetails.Visibility = Visibility.Collapsed;
            this.gridDetails1.Visibility = Visibility.Collapsed;
            this.btnPrint2.Visibility = Visibility.Hidden;
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.stkpSubHead.Visibility = Visibility.Collapsed;
            this.cmbBppBrn.Items.Clear();
            var brnList = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            foreach (var itemb in brnList)
                this.cmbBppBrn.Items.Add(new ComboBoxItem()
                {
                    Content = itemb.brnnam.Trim() + " (" + itemb.brnsnam.Trim() + ")",
                    Tag = itemb.brncod + itemb.brnsnam.Trim(),
                    ToolTip = itemb.brnnam.Trim() + " (" + itemb.brnsnam.Trim() + ")"
                });


            if (WpfProcessAccess.AccCodeList == null)
                WpfProcessAccess.GetAccCodeList();

            this.CactcodeList = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902") && (x.actcode.Substring(8, 4) != "0000"));
            this.CactcodeList.Add(new HmsEntityGeneral.AcInfCodeBook
            {
                actcode = "000000000000",
                actcode1 = "00-00-0000-0000",
                actdesc = "GENERAL CASH/BANK",
                actdesc1 = "",
                actelev = "",
                acttdesc = "",
                acttype = "",
                comcod = WpfProcessAccess.CompInfList[0].comcod,
                rowid = 0,
                rowtime = DateTime.Now
            });

            this.CactcodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
            {
                return x.actdesc.CompareTo(y.actdesc);
            });

            this.ActcodeList = WpfProcessAccess.AccCodeList.FindAll(x => !(x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902") && (x.actcode.Substring(8, 4) != "0000"));
            this.ActcodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
            {
                return x.actdesc.CompareTo(y.actdesc);
            });

            this.AtxtActCode.Items.Clear();
            this.AtxtActCode.AutoSuggestionList.Clear();
            foreach (var item1 in this.ActcodeList)
                this.AtxtActCode.AddSuggstionItem(item1.actdesc.Trim(), item1.actcode);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.txtActCode.Visibility = Visibility.Hidden;
            this.btnPrint2.Visibility = Visibility.Hidden;
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.gridDetails.Visibility = Visibility.Collapsed;
            this.gridDetails1.Visibility = Visibility.Collapsed;

            this.gridDetails.IsEnabled = true;
            this.gridDetails1.IsEnabled = true;
            this.chkAutoTransList.IsEnabled = true;
            this.dgTrans.ItemsSource = null;
            this.txtBppNar.IsEnabled = true;
            this.txtBppRef.IsEnabled = true;
            this.chkSubHead.IsChecked = false;
            this.AtxtActCode.Text = "";
            this.lblLevel2.Visibility = Visibility.Hidden;
            this.CleanupControls();
            this.xctk_dtpBppDat.IsEnabled = false;

            this.lblBppNo.Content = "PBXMM-CCCC-XXXXX";
            this.lblBppNo.Tag = "PBXYYYYMMCCCCXXXXX";

            if (this.btnOk.Content.ToString() == "_New")
            {
                if (this.txtblEditMode.Visibility == Visibility.Visible)
                    this.xctk_dtpBppDat.Value = DateTime.Today;

                this.txtblEditMode.Visibility = Visibility.Hidden;
                this.chkDateBlocked.IsChecked = false;
                this.chkDateBlocked.IsEnabled = true;
                this.cmbBppType.IsEnabled = true;
                this.cmbBppBrn.IsEnabled = true;
                this.txtBppRef.Text = "";
                this.txtBppNar.Text = "";
                this.EditDs = null;

                if (IsActiveTransListWindow)
                {
                    this.BuildTransactionList();
                    this.gridTransList.Visibility = Visibility.Visible;
                    this.lvTransList.Focus();
                }
                else
                    this.cmbBppType.Focus();

                this.btnOk.Content = "_Ok";
                return;

            }
            string voutitle = ((ComboBoxItem)this.cmbBppType.SelectedItem).Tag.ToString().Trim().Substring(0, 3);


            string bppnum = voutitle + DateTime.Parse(this.xctk_dtpBppDat.Text).ToString("yyyyMM") +
                       ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Tag.ToString().Trim().Substring(0, 4);
            this.lblActCodeTitle.Content = "Account _Head:";
            this.BindControlCode(bppnum);
            this.lblBppNo.Content = bppnum.Substring(0, 3) + bppnum.Substring(7, 2) + "-" + bppnum.Substring(9, 4) + "-XXXXX";
            this.lblBppNo.Tag = bppnum;

            string epdat1 = "01-" + DateTime.Parse(this.xctk_dtpBppDat.Text).ToString("MMM-yyyy");
            this.xctk_dtpEpayDat.Value = DateTime.Parse(epdat1).AddMonths(1).AddDays(-1);
            this.chkAutoTransList.IsEnabled = false;
            this.gridDetails.Visibility = Visibility.Visible;
            this.gridTransList.Visibility = Visibility.Collapsed;
            this.chkDateBlocked.IsChecked = false;
            this.chkDateBlocked.IsEnabled = false;
            this.cmbBppType.IsEnabled = false;
            this.cmbBppBrn.IsEnabled = false;
            PrevBppnum = bppnum;

            this.btnOk.Content = "_New";
        }


        private void BindControlCode(string bppnum)
        {
            this.stkpControl.IsEnabled = true;
            this.AtxtCactCode.Items.Clear();
            this.AtxtCactCode.AutoSuggestionList.Clear();
            this.conMenuCactCode.Items.Clear();
            var CactcodeList1a = new List<HmsEntityGeneral.AcInfCodeBook>();
            switch (bppnum.Substring(0, 3))
            {
                case "PBC":
                    CactcodeList1a = this.CactcodeList.FindAll(x => x.actcode.Substring(0, 4) == "1901");
                    this.lblCactCodeTitle.Content = "_Source Cash";
                    break;
                case "PBB":
                    CactcodeList1a = this.CactcodeList.FindAll(x => x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902");
                    this.lblCactCodeTitle.Content = "_Source Bank";
                    break;
                case "PBA":
                    CactcodeList1a = this.CactcodeList.FindAll(x => x.actcode.Substring(0, 4) == "0000");
                    this.lblCactCodeTitle.Content = "_Source Cash/Bank";
                    break;

            }

            foreach (var item1 in CactcodeList1a)
            {
                this.AtxtCactCode.AddSuggstionItem(item1.actdesc.Trim(), item1.actcode);
                var mitm1 = new MenuItem() { Header = item1.actdesc.Trim(), Tag = item1.actcode.Trim() };
                mitm1.Click += conMenuCactCode_MouseClick;
                this.conMenuCactCode.Items.Add(mitm1);
            }
            if (bppnum.Substring(0, 3) == "PBA")
            {
                this.AtxtCactCode.Text = CactcodeList1a[0].actdesc;
                this.stkpControl.IsEnabled = false;
            }


            this.AtxtSectCod.Items.Clear();
            this.AtxtSectCod.AutoSuggestionList.Clear();
            this.conMenuSectCod.Items.Clear();
            string brncod1 = ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Tag.ToString().Substring(0, 4);
            var sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(0, 4) == brncod1 &&
                    (x.sectcod.Substring(9, 3) != "000" || (x.sectcod.Substring(4, 8) != "00000000" && x.sectcod.Substring(7, 5) == "00000"))).ToList().OrderBy(y => y.sectcod);

            //sectList.Sort(delegate(HmsEntityGeneral.CompSecCodeBook x, HmsEntityGeneral.CompSecCodeBook y)
            //{
            //    //return x.sectname.CompareTo(y.sectname);
            //    return x.sectcod.CompareTo(y.sectcod);
            //});

            foreach (var itemc in sectList)
            {
                this.AtxtSectCod.AddSuggstionItem(itemc.sectname, itemc.sectcod);
                var mitm1 = new MenuItem() { Header = itemc.sectname.Trim(), Tag = itemc.sectcod.Trim() };
                mitm1.Click += conMenuSectCod_MouseClick;
                this.conMenuSectCod.Items.Add(mitm1);
            }
        }

        private void CleanupControls()
        {
            this.txtBppRef.Text = "";
            this.AtxtCactCode.Text = "";
            this.AtxtSectCod.Text = "";
            this.chkSubHead.IsChecked = false;
            this.ListBppTable1.Clear();
            this.CleanupControls2();
        }

        private void CleanupControls2()
        {
            if (this.lblLevel2.Visibility == Visibility.Hidden)
                this.AtxtActCode.Text = "";

            this.AutoCompleteSirCode.SelectedValue = null;

            this.txtRmrk.Text = "";
            this.txtAmount.Text = "";
            this.stkpSubHead.Visibility = (this.chkSubHead.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
        }

        private void chkDateBlocked_Click(object sender, RoutedEventArgs e)
        {
            this.xctk_dtpBppDat.IsEnabled = (((CheckBox)sender).IsChecked == true);
            if (this.xctk_dtpBppDat.IsEnabled)
                this.xctk_dtpBppDat.Focus();
        }

        private void AtxtCactCode_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtCactCode.Value.Length > 0 && this.AtxtCactCode.Text.Trim().Length > 0)
            {
                string cactVal = this.AtxtCactCode.Value;
            }

        }
        private void AtxtCactCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtCactCode.Value.Length == 0)
                return;

            if (this.AtxtCactCode.Text.Trim().Length == 0)
                return;

            string cactVal = this.AtxtCactCode.Value;


        }
        private void AtxtActCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtActCode.Value.Length == 0)
                return;

            if (this.AtxtActCode.Text.Trim().Length == 0)
                return;

            string actVal = this.AtxtActCode.Value;

            bool level2 = false;
            var acCodeInf = this.ActcodeList.Find(x => x.actcode == actVal);
            if (acCodeInf != null)
                level2 = (acCodeInf.actelev.Trim() == "2");


            this.lblLevel2.Visibility = (level2 ? Visibility.Visible : Visibility.Hidden);
            this.chkSubHead_Click(null, null);
        }

        private void chkSubHead_Click(object sender, RoutedEventArgs e)
        {
            this.stkpSubHead.Visibility = Visibility.Collapsed;

            bool chkSubHead1a = (this.chkSubHead.IsChecked == true);

            if (chkSubHead1a || this.lblLevel2.Visibility == Visibility.Visible)
            {
                this.stkpSubHead.Visibility = Visibility.Visible;
            }

            if (WpfProcessAccess.AccSirCodeList == null)
            {
                WpfProcessAccess.GetAccSirCodeList();
            }
        }
        private void AtxtSectCod_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtSectCod.Value.Length == 0)
                return;

            if (this.AtxtSectCod.Text.Trim().Length == 0)
                return;
        }

        private void conMenuCactCode_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtCactCode.Text = ((MenuItem)sender).Header.ToString().Trim();
        }
        private void AtxtCactCode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtCactCode.ContextMenu.IsOpen = true;
        }
        private void conMenuSectCod_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtSectCod.Text = ((MenuItem)sender).Header.ToString().Trim();
        }
        private void AtxtSectCod_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtSectCod.ContextMenu.IsOpen = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string voutitle = ((ComboBoxItem)this.cmbBppType.SelectedItem).Tag.ToString().Trim().Substring(0, 3);
            decimal trnamt1 = decimal.Parse("0" + this.txtAmount.Text.Trim());
            decimal dramt1 = trnamt1;
            decimal cramt1 = 0.00m;
            decimal trnqty1 = 0.00m;
            decimal trnrate1 = 0.00m;

            var cactcode1 = (this.AtxtCactCode.Text.Trim().Length == 0 ? "000000000000" : (this.AtxtCactCode.Value.Trim().Length != 12 ? "000000000000" : this.AtxtCactCode.Value));
            var cactcodeDesc1 = (cactcode1 == "000000000000" && voutitle != "PBA" ? "" : this.AtxtCactCode.Text.Trim());
            var sectcod1 = (this.AtxtSectCod.Text.Trim().Length == 0 ? "000000000000" : (this.AtxtSectCod.Value.Trim().Length != 12 ? "000000000000" : this.AtxtSectCod.Value));
            var sectcodDesc1 = (sectcod1 == "000000000000" ? "" : this.AtxtSectCod.Text.Trim());
            var actcode1 = (this.AtxtActCode.Text.Trim().Length == 0 ? "000000000000" : (this.AtxtActCode.Value.Trim().Length != 12 ? "000000000000" : this.AtxtActCode.Value));
            var actcodeDesc1 = (actcode1 == "000000000000" ? "" : this.AtxtActCode.Text.Trim());
            var sircode1a = this.AutoCompleteSirCode.SelectedValue;
            var sircode1 = (sircode1a == null ? "000000000000" : (sircode1a.ToString().Trim().Length != 12 ? "000000000000" : sircode1a.ToString().Trim()));
            var sircodeDesc1 = (sircode1 == "000000000000" ? "" : this.AutoCompleteSirCode.SelectedText.Trim());
            var sirUnit1 = (sircode1 == "000000000000" ? "" : WpfProcessAccess.AccSirCodeList.Find(x => x.sircode == sircode1).sirunit.Trim());// this.lblUnit.Content.ToString();

            var sircode2 = "000000000000";
            var sircode2Desc1 = "";

            string epaydat1 = this.xctk_dtpEpayDat.Text;

            var rmrk1 = this.txtRmrk.Text.Trim();
            if (actcode1 == "000000000000")
                return;

            //if (this.stkpControl.Visibility == Visibility.Visible)
            //{
            //    if (cactcode1 == "000000000000")
            //        return;
            //}

            string ac1 = actcode1.Substring(0, 4);
            bool CashBank = ((ac1 == "1901" || ac1 == "1903" || ac1 == "2902") ? true : false);

            if (this.stkpLocation.Visibility == Visibility.Visible && CashBank == false)
            {
                if (sectcod1 == "000000000000")
                    return;
            }
            sectcod1 = (CashBank == true ? "000000000000" : sectcod1);
            sectcodDesc1 = (CashBank == true ? "" : sectcodDesc1);

            foreach (var itemd in this.ListBppTable1)
            {
                if (itemd.sectcod == sectcod1 && itemd.actcode == actcode1 && itemd.sircode == sircode1)
                    return;
            }

            if (sircode1 != "000000000000")
            {
                var tsirCod1 = WpfProcessAccess.AccSirCodeList.Find(x => x.sircode == sircode1);
            }

            this.gridDetails1.Visibility = Visibility.Visible;
            this.btnUpdate.Visibility = Visibility.Visible;
            this.btnUpdate.IsEnabled = true;

            string pType1 = ((ComboBoxItem)this.cmbBppType.SelectedItem).Tag.ToString();
            var Ccod1 = this.ListBppTable1.FindAll(x => x.actcode == "000000000000" && x.sectcod == "000000000000");
            if (Ccod1.Count == 0)
            {
                this.ListBppTable1.Add(new vmBgdAccounts1.PropTable()
                {
                    trnsl = this.ListBppTable1.Count() + 1,
                    cactcode = cactcode1,
                    sectcod = "000000000000",
                    actcode = "000000000000",
                    sircode = "000000000000",
                    sectcodDesc = "",
                    actcodeDesc = cactcodeDesc1,
                    sircodeDesc = "",
                    trnDesc = cactcodeDesc1,
                    epaydat = DateTime.Parse(this.xctk_dtpBppDat.Text),
                    bapdat = DateTime.Parse("01-Jan-1900"),
                    bppam = 0,
                    bpprmrk = "",
                    baprmrk = "",
                    bapam = 0,
                    bapbyid = "000000000000",
                    bapses = "",
                    bapbyName = "",
                    baptrm = "",
                    cactcodeDesc = cactcodeDesc1,
                    vepaydat = "Hidden"
                });
            }

            this.ListBppTable1.Add(new vmBgdAccounts1.PropTable()
            {
                trnsl = this.ListBppTable1.Count() + 1,
                cactcode = cactcode1,
                sectcod = sectcod1,
                actcode = actcode1,
                sircode = sircode1,
                sectcodDesc = sectcodDesc1,
                actcodeDesc = actcodeDesc1,
                sircodeDesc = sircodeDesc1,
                trnDesc = actcodeDesc1 + (sircodeDesc1.Length > 0 ? "\n\t" + sircodeDesc1 : ""),
                //trnDesc = actcodeDesc1 + (sircodeDesc1.Length > 0 ? " [" + sircodeDesc1 + "]" : ""),
                epaydat = DateTime.Parse(epaydat1),
                bapdat = DateTime.Parse("01-Jan-1900"),
                bppam = trnamt1,
                bpprmrk = rmrk1,
                baprmrk = "",
                bapam = 0,
                bapbyid = "000000000000",
                bapses = "",
                bapbyName = "",
                baptrm = "",
                cactcodeDesc = cactcodeDesc1,
                vepaydat = "Visible"
            });

            this.CleanupControls2();
            this.CalculateTotal();
            this.txtActCode.Focus();
        }
        private void CalculateTotal()
        {
            this.dgTrans.ItemsSource = null;

            string pType1 = ((ComboBoxItem)this.cmbBppType.SelectedItem).Tag.ToString();


            decimal sumDr = this.ListBppTable1.FindAll(x => x.actcode != "000000000000").Sum(x => x.bppam);
            decimal sumCr = 0.00m;

            this.ListBppTable1.Sort(delegate(vmBgdAccounts1.PropTable x, vmBgdAccounts1.PropTable y)
            {
                return (x.actcode).CompareTo(y.actcode);
            });

            int i = 1;
            foreach (var item1 in this.ListBppTable1)
            {
                item1.trnsl = i;
                ++i;
            }

            this.lblSumDram.Content = this.ListBppTable1.Sum(x => x.bppam).ToString("#,##0.00");
            this.dgTrans.ItemsSource = this.ListBppTable1;
        }

        private void txtRate_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //if(this.EditDs!=null)
            //{
            //    System.Windows.MessageBox.Show("Code required to modify for edit mode - Hafiz", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            //    return;
            //}

            this.CalculateTotal();
            string cactcod1 = this.AtxtCactCode.Value.Trim();

            string bppnum1 = ((ComboBoxItem)this.cmbBppType.SelectedItem).Tag.ToString().Trim().Substring(0, 3) +
                         DateTime.Parse(this.xctk_dtpBppDat.Text).ToString("yyyyMM") +
                         ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Tag.ToString().Trim().Substring(0, 4);

            string EditBppnum1 = (this.EditDs != null ? this.lblBppNo.Tag.ToString() : "");
            var bppPrInfo1 = new vmBgdAccounts1.PayBgdPrInfo()
            {
                bppnum = (EditBppnum1.Length > 0 ? EditBppnum1 : bppnum1),
                bppdat = DateTime.Parse(this.xctk_dtpBppDat.Text),
                bppref = this.txtBppRef.Text.Trim(),
                bppnar = this.txtBppNar.Text.Trim(),
                pstatus = "A",
                precndt = DateTime.Parse("01-Jan-1900"),
                ptcode = ((ComboBoxItem)this.cmbBppType.SelectedItem).Tag.ToString().Trim().Substring(3, 2),
            };


            var ListVouTable1u = this.ListBppTable1.FindAll(x => x.actcode != "000000000000");
            DataSet ds1 = vm1.GetDataSetForUpdate(WpfProcessAccess.CompInfList[0].comcod, bppPrInfo1, ListVouTable1u,
                _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);
            var pap1 = vm1.SetParamUpdatePayPro(WpfProcessAccess.CompInfList[0].comcod, ds1, EditBppnum1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.lblBppNo.Content = ds2.Tables[0].Rows[0]["memonum1"].ToString();
            this.lblBppNo.Tag = ds2.Tables[0].Rows[0]["memonum"].ToString();

            this.btnUpdate.IsEnabled = false;
            this.gridDetails.IsEnabled = false;
            this.gridDetails1.IsEnabled = false;
            this.txtBppNar.IsEnabled = false;
            this.txtBppRef.IsEnabled = false;
            this.btnPrint2.Visibility = Visibility.Visible;
        }
        private void chkAutoTransList_Click(object sender, RoutedEventArgs e)
        {
            this.IsActiveTransListWindow = (this.chkAutoTransList.IsChecked == true);
            if (this.IsActiveTransListWindow && this.gridDetails.Visibility == Visibility.Collapsed)
            {
                this.BuildTransactionList();
                this.gridTransList.Visibility = Visibility.Visible;
                this.lvTransList.Focus();
            }
            else if (this.IsActiveTransListWindow == false && this.gridDetails.Visibility == Visibility.Collapsed)
                this.gridTransList.Visibility = Visibility.Collapsed;

            this.chkFilter.IsChecked = false;
            this.chkPrint2.IsChecked = false;
        }
        private void BuildTransactionList()
        {
            string FromDate = this.xctk_dtpFromDate.Text;
            string ToDate = this.xctk_dtpToDate.Text;
            string brncod1 = ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Tag.ToString().Substring(0, 4);
            string pType1 = ((ComboBoxItem)this.cmbBppType.SelectedItem).Tag.ToString();
            var pap1 = vmrptAcc.SetParamBppTransList(WpfProcessAccess.CompInfList[0].comcod, "RPTBPPTRANS01", FromDate, ToDate, "A", brncod1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.BppTrnLst.Clear();
            this.BppTrnLst = ds1.Tables[0].DataTableToList<HmsEntityAccounting.PayProTransectionList>().ToList().FindAll(x => x.brncod == brncod1 && x.bppnum.Substring(0, 3) == pType1.Substring(0, 3) && x.ptcode == pType1.Substring(3, 2)); ;

            this.lvTransList.ItemsSource = BppTrnLst;
            this.lvTransList.Items.Refresh();
            lvTransList.Focus();
            this.txtTransTitle.Text = "All Transaction List From : " + FromDate + " To : " + ToDate;
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

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            this.BuildTransactionList();
            this.chkFilter.IsChecked = false;
        }

        private void btnPrint3_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            if (lvTransList.SelectedItem == null && this.rb3SelectedMemo.IsChecked == true)
            {
                System.Windows.MessageBox.Show("No record found to view/print report", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            LocalReport rpt1 = null;

            string WindowTitle1 = "";
            if (this.rb3SelectedMemo.IsChecked == true)
            {
                var item1a = (HmsEntityAccounting.PayProTransectionList)this.lvTransList.SelectedItem;
                //if (item1a.pstatus == "C")
                //{
                //    System.Windows.MessageBox.Show("Cancelled voucher can not be view/print at this moment", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                //    return;
                //}

                this.PrintProposalMemo(item1a.bppnum, false);

            }
            else if (this.rb3TableRecoreds.IsChecked == true)
            {
                if (this.BppTrnLst.Count == 0)
                    return;

                var list1 = this.BppTrnLst;
                //var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
                var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
                rpt1 = AccReportSetup.GetLocalReport("Accounting.RptPayProTransList1", list1, null, list3, null);
                WindowTitle1 = "Proposal Transaction List";
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

        private void PrintProposalMemo(string memoNum, bool DirectPrint = false)
        {
            LocalReport rpt1 = null;
            var pap1 = vmrptAcc.SetParamBppTrans(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var list1 = ds1.Tables[1].DataTableToList<HmsEntityAccounting.PayProTrans1>();
            var trnsList = ds1.Tables[0].DataTableToList<HmsEntityAccounting.PayProTransectionList>();
            // select preparebyid, PreparByName, prepareses, preparetrm, rowid, rowtime, ServerTime = getdate() from #tblv1
            string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["PreparByName"].ToString().Trim()
                                + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);
            string memoName = memoNum.Substring(0, 3).Trim();
            byte[] comlogoBytes = WpfProcessAccess.CompInfList[0].comlogo;

            HmsEntityAccounting.AccVoucher1p list4 = new HmsEntityAccounting.AccVoucher1p();
            list4.comlogo = comlogoBytes;
            list4.inWord = ASITFunLib.ASITUtility.Trans(double.Parse(list1[0].bppam.ToString()), 2);
            //l.inWord = ASITFunLib.ASITUtility2.UppercaseWords("");
            string rptName = "Accounting.RptPayProTran1";
            rpt1 = AccReportSetup.GetLocalReport(rptName, list1, trnsList, list3, list4);
            //rpt1.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(bytes)));
            string WindowTitle1 = "Budget Proposal Memo";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void UnCheckedAllPopups()
        {
            this.chkFilter.IsChecked = false;
            this.chkPrint2.IsChecked = false;
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            string memoNum = this.lblBppNo.Tag.ToString().Trim();
            this.PrintProposalMemo(memoNum, true);
        }

        private void btnBppEdit_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            if (lvTransList.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("No record found to edit", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var item1a = (HmsEntityAccounting.PayProTransectionList)this.lvTransList.SelectedItem;
            if (item1a.pstatus == "C")
            {
                System.Windows.MessageBox.Show("Proposal already cancelled. Edit not possible", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var pap1 = vm1.SetParamEditPayPro(WpfProcessAccess.CompInfList[0].comcod, item1a.bppnum);
            this.EditDs = null;
            this.EditDs = WpfProcessAccess.GetHmsDataSet(pap1);
            if (this.EditDs == null)
                return;

            var pType1 = this.EditDs.Tables[0].Rows[0]["bppnum"].ToString().Substring(0, 3) + this.EditDs.Tables[0].Rows[0]["ptcode"].ToString();
            var brncod = this.EditDs.Tables[0].Rows[0]["bppnum"].ToString().Substring(9, 4);
            int i = 0;
            bool found1 = false;
            foreach (ComboBoxItem item1b in this.cmbBppType.Items)
            {
                if (item1b.Tag.ToString().Trim() == pType1)
                {
                    found1 = true;
                    break;
                }
                i++;
            }
            if (found1 == false)
            {
                this.EditDs = null;
                return;
            }
            this.cmbBppType.SelectedIndex = i;

            int j = 0;
            foreach (ComboBoxItem item1c in this.cmbBppBrn.Items)
            {
                if (item1c.Tag.ToString().Trim().Substring(0, 4) == brncod)
                    break;
                j++;
            }
            this.cmbBppBrn.SelectedIndex = j;
            this.xctk_dtpBppDat.Value = Convert.ToDateTime(this.EditDs.Tables[0].Rows[0]["bppdat"]);

            this.btnOk_Click(null, null);

            this.txtblEditMode.Visibility = Visibility.Visible;
            this.lblBppNo.Content = this.EditDs.Tables[0].Rows[0]["bppnum1"].ToString();
            this.lblBppNo.Tag = this.EditDs.Tables[0].Rows[0]["bppnum"].ToString();

            this.txtBppRef.Text = this.EditDs.Tables[0].Rows[0]["bppref"].ToString();
            this.txtBppNar.Text = this.EditDs.Tables[0].Rows[0]["bppnar"].ToString();

            var sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");

            // select comcod, bppnum, bppdat, bppref, bppnar, ptcode, pstatus, precndt, preparebyid, prepareses, preparetrm, rowid, rowtime from dbo_acc.acbppb where comcod = @ComCod and bppnum = @Desc01;
            // select comcod, bppnum, cactcode, sectcod, actcode, sircode, bppam, bapam, epaydat, bpprmrk, baprmrk, bapdat, bapbyid, bapses, baptrm, rowid from dbo_acc.acbppa where comcod = @ComCod and bppnum = @Desc01;

            foreach (DataRow dr1a in this.EditDs.Tables[1].Rows)
            {
                decimal trnamt1 = decimal.Parse(dr1a["bppam"].ToString());
                decimal dramt1 = (trnamt1 > 0 ? trnamt1 : 0.00m);
                decimal cramt1 = (trnamt1 < 0 ? trnamt1 * -1 : 0.00m);
                string cactcode1 = dr1a["cactcode"].ToString();
                string cactcodeDesc1 = dr1a["cactcodeDesc"].ToString(); ;

                string sectcod1 = dr1a["sectcod"].ToString();
                string sectcodDesc1 = dr1a["sectcodDesc"].ToString(); ;

                string actcode1 = dr1a["actcode"].ToString();
                string actcodeDesc1 = dr1a["actcodeDesc"].ToString();
                string sircode1 = dr1a["sircode"].ToString();
                string sircodeDesc1 = dr1a["sircodeDesc"].ToString();

                string rmrk1 = dr1a["bpprmrk"].ToString();

                string ac1 = actcode1.Substring(0, 4);
                bool CashBank = ((ac1 == "1901" || ac1 == "1903" || ac1 == "2902") ? true : false);

                if (this.stkpLocation.Visibility == Visibility.Visible && CashBank == false)
                {
                    if (sectcod1 == "000000000000")
                        return;
                }

                sectcod1 = (CashBank == true ? "000000000000" : sectcod1);
                sectcodDesc1 = (CashBank == true ? "" : sectcodDesc1);
                string epaydat1 = Convert.ToDateTime(dr1a["epaydat"]).ToString("dd-MMM-yyyy");

                var Ccod1 = this.ListBppTable1.FindAll(x => x.cactcode == cactcode1 && x.actcode == "000000000000" && x.sectcod == "000000000000");
                if (Ccod1.Count == 0)
                {
                    this.ListBppTable1.Add(new vmBgdAccounts1.PropTable()
                    {
                        trnsl = this.ListBppTable1.Count() + 1,
                        cactcode = cactcode1,
                        sectcod = "000000000000",
                        actcode = "000000000000",
                        sircode = "000000000000",
                        cactcodeDesc = cactcodeDesc1,
                        sectcodDesc = "",
                        actcodeDesc = "",
                        sircodeDesc = "",
                        trnDesc = cactcodeDesc1,
                        bppam = 0,
                        epaydat = DateTime.Parse(this.xctk_dtpBppDat.Text),
                        bapdat = DateTime.Parse("01-Jan-1900"),
                        bpprmrk = "",
                        bapam = 0,
                        bapbyid = "000000000000",
                        bapbyName = "",
                        baprmrk = "",
                        bapses = "",
                        baptrm = "",
                        vbapdat = "",
                        vepaydat = "Hidden"
                    });
                }

                this.ListBppTable1.Add(new vmBgdAccounts1.PropTable()
                {
                    trnsl = this.ListBppTable1.Count() + 1,
                    cactcode = cactcode1,
                    sectcod = sectcod1,
                    actcode = actcode1,
                    sircode = sircode1,
                    cactcodeDesc = cactcodeDesc1,
                    sectcodDesc = sectcodDesc1,
                    actcodeDesc = actcodeDesc1,
                    sircodeDesc = sircodeDesc1,
                    trnDesc = actcodeDesc1 + (sircodeDesc1.Length > 0 ? "\n\t" + sircodeDesc1 : ""),
                    bppam = dramt1,
                    epaydat = DateTime.Parse(epaydat1),
                    bapdat = DateTime.Parse("01-Jan-1900"),
                    bpprmrk = rmrk1,
                    bapam = 0,
                    bapbyid = "000000000000",
                    bapbyName = "",
                    baprmrk = "",
                    bapses = "",
                    baptrm = "",
                    vbapdat = "",
                    vepaydat = "Visible"
                });
            }


            this.AtxtCactCode.Text = this.EditDs.Tables[1].Rows[0]["cactcodeDesc"].ToString();
            //-------------------------------------------

            this.dgTransColLoc.Width = 250;

            this.gridDetails1.Visibility = Visibility.Visible;
            this.btnUpdate.Visibility = Visibility.Visible;
            this.btnUpdate.IsEnabled = true;
            this.CalculateTotal();
            this.txtActCode.Focus();
        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            this.CalculateTotal();
        }

        private void cmbBppBrn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbBppBrn.SelectedItem == null)
                return;

            this.cmbBppBrn.ToolTip = ((ComboBoxItem)this.cmbBppBrn.SelectedItem).Content.ToString();
            if (this.gridTransList.Visibility == Visibility.Visible)
                this.btnFilter_Click(null, null);
        }

        private void btnBppCancel_Click(object sender, RoutedEventArgs e)
        {

            this.UnCheckedAllPopups();
            if (lvTransList.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("No record found to cancel", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this payment proposal", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (msgresult != MessageBoxResult.Yes)
                return;

            var item1a = (HmsEntityAccounting.PayProTransectionList)this.lvTransList.SelectedItem;
            int itemno1 = this.lvTransList.SelectedIndex;
            var pap1 = vm1.SetParamCancelPayPro(WpfProcessAccess.CompInfList[0].comcod, item1a.bppnum);

            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            ((HmsEntityAccounting.PayProTransectionList)this.lvTransList.Items[itemno1]).pstatus = "C";
            this.BppTrnLst[itemno1].pstatus = "C";
            this.lvTransList.Items.Refresh();
            //BppTrnLst
            System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void AutoCompleteSirCode_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetItemSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                WpfProcessAccess.AccSirCodeList.Where((x, match) => x.sircode.Substring(9, 3) != "000" && x.sirdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(200).OrderBy(m => m.sirdesc));
        }


        private void btnBppCopy_Click(object sender, RoutedEventArgs e)
        {
            this.UnCheckedAllPopups();
            if (lvTransList.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("No record found to copy", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var item1a = (HmsEntityAccounting.PayProTransectionList)this.lvTransList.SelectedItem;
            if (item1a.pstatus == "C")
            {
                System.Windows.MessageBox.Show("Proposal already cancelled. Copy not possible", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var pap1 = vm1.SetParamEditPayPro(WpfProcessAccess.CompInfList[0].comcod, item1a.bppnum);
            this.EditDs = null;
            this.EditDs = WpfProcessAccess.GetHmsDataSet(pap1);
            if (this.EditDs == null)
                return;

            var pType1 = this.EditDs.Tables[0].Rows[0]["bppnum"].ToString().Substring(0, 3) + this.EditDs.Tables[0].Rows[0]["ptcode"].ToString();
            var brncod = this.EditDs.Tables[0].Rows[0]["bppnum"].ToString().Substring(9, 4);
            int i = 0;
            foreach (ComboBoxItem item1b in this.cmbBppType.Items)
            {
                if (item1b.Tag.ToString().Trim() == pType1)
                    break;
                i++;
            }
            this.cmbBppType.SelectedIndex = i;

            int j = 0;
            foreach (ComboBoxItem item1c in this.cmbBppBrn.Items)
            {
                if (item1c.Tag.ToString().Trim().Substring(0, 4) == brncod)
                    break;
                j++;
            }
            this.cmbBppBrn.SelectedIndex = j;
            //this.xctk_dtpVouDat.Value = Convert.ToDateTime(this.EditDs.Tables[0].Rows[0]["voudat"]);

            this.btnOk_Click(null, null);

            this.txtBppRef.Text = this.EditDs.Tables[0].Rows[0]["bppref"].ToString();
            this.txtBppNar.Text = this.EditDs.Tables[0].Rows[0]["bppnar"].ToString();

            var sectList = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
        }

        private void cmbBppType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridTransList.Visibility == Visibility.Visible)
                this.btnFilter_Click(null, null);
        }

    }
}
