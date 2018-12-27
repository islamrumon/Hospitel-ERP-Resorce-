using ASITHmsEntity;
using ASITHmsViewMan.Commercial;
using ASITFunLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
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
using Microsoft.Reporting.WinForms;
using ASITHmsRpt4Commercial;

namespace ASITHmsWpf.Commercial.ParkTicket
{
    /// <summary>
    /// Interaction logic for frmEntryParkPOS102.xaml
    /// </summary>
    public partial class frmEntryParkPOS102 : UserControl
    {
        private bool FrmInitialized = false;
        private DataGrid dgRpt1;

        private List<vmEntryPharRestPOS1.RetSaleItem> RetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();
        private List<vmEntryPharRestPOS1.RetSaleItem> ShortRetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();

        private List<HmsEntityCommercial.ParkSalesTrans01> rptTransList01 = new List<HmsEntityCommercial.ParkSalesTrans01>();
        private List<HmsEntityCommercial.ParkSalesTrans01> rptTransList02 = new List<HmsEntityCommercial.ParkSalesTrans01>();

        private vmEntryPharRestPOS1 vm1o = new vmEntryPharRestPOS1();
        private vmEntryReportPark1 vmr1 = new vmEntryReportPark1();

        public frmEntryParkPOS102()
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
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void Objects_On_Init()
        {
            this.xctk_dtpInvDat.Value = DateTime.Today; //Convert.ToDateTime("01-" + DateTime.Today.ToString("MMM-yyyy"));
            this.GetSectionList();
            this.GetRetailItemList();
            this.CleanUpScreen();
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
                System.Windows.MessageBox.Show("PSI.2-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void GetRetailItemList()
        {
            this.RetSaleItemList.Clear();
            //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4171", reqmfginf: "WITHOUTMFGINFO");
            //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4521", reqmfginf: "WITHMFGINFO");
            //var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "0[14]51", reqmfginf: "WITHOUTMFGINFO");
            //var pap = vm1a.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "0151", reqmfginf: "WITHOUTMFGINFO");
            var pap = vm1o.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4144", reqmfginf: "WITHOUTMFGINFO");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
            if (ds1 == null)
                return;

            //this.RetSaleItemGroupList = ds1.Tables[1].DataTableToList<vmEntryPharRestPOS1.RetSaleItemGroup>();
            DataRow[] dr1 = ds1.Tables[0].Select();

            foreach (DataRow row1 in dr1)
            {
                var itm1 = new vmEntryPharRestPOS1.RetSaleItem(row1["sircode"].ToString(), row1["sircode"].ToString().Substring(6, 6) + " : " + row1["sirdesc"].ToString(), Convert.ToDecimal(row1["costprice"]),
                        Convert.ToDecimal(row1["saleprice"]), Convert.ToDecimal(row1["refscomp"]), Convert.ToDecimal(row1["salvatp"]), row1["sirtype"].ToString(), row1["sirunit"].ToString(), row1["sirunit2"].ToString(),
                        row1["sirunit3"].ToString(), decimal.Parse("0" + row1["siruconf"].ToString()), decimal.Parse("0" + row1["siruconf3"].ToString()), row1["msircode"].ToString(), row1["msirdesc"].ToString(),
                        row1["msirdesc"].ToString().Trim() + " - " + row1["sirdesc"].ToString(), row1["sircode"].ToString().Substring(6), row1["mfgid"].ToString(), row1["mfgcomnam"].ToString(),
                        (row1["mfgcomnam"].ToString().Trim().Length > 0 ? "Visible" : "Collapsed"), "Collapsed", null);
                this.RetSaleItemList.Add(itm1);
            }

            this.autoItemSearch.ContextMenu.Items.Clear();
            foreach (var item in this.RetSaleItemList)
            {
                MenuItem mnu1 = new MenuItem { Header = item.sirdesc, Tag = item.sircode };
                mnu1.Click += autoItemSearch_ContextMenu_MouseClick;
                this.autoItemSearch.ContextMenu.Items.Add(mnu1);
            }

        }

        private void autoItemSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoItemSearch.ContextMenu.IsOpen = true;
        }

        private void autoItemSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.autoItemSearch.ItemsSource = this.RetSaleItemList;
                this.autoItemSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim();

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI-23: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void autoItemSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
        }

        private ObservableCollection<vmEntryPharRestPOS1.RetSaleItem> GetItemSirdesc(string pattern)
        {
            return new ObservableCollection<vmEntryPharRestPOS1.RetSaleItem>(
                     this.RetSaleItemList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(pattern.ToLower().Trim())).Take(100));
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {

            if (this.autoItemSearch.SelectedValue == null)
                return;

            if (this.btnGenerate.Content.ToString() == "_New")
            {
                this.CleanUpScreen();
                this.xctk_dtpInvDat.IsEnabled = true;
                this.autoItemSearch.IsEnabled = true;
                this.autoItemSearch.Text = "";
                this.txtFromNo.Clear();
                this.txtToNo.Clear();
                this.dgOverall01.ItemsSource = null;
                this.dgOverall02.ItemsSource = null;
                this.btnGenerate.Content = "_Show";

                return;
            }

            this.btnUpdate.Visibility = Visibility.Visible;
            this.btnAllChecked.Visibility = Visibility.Visible;
            this.btnAllUnchecked.Visibility = Visibility.Visible;
            this.lblFrom.Visibility = Visibility.Visible;
            this.lblTo.Visibility = Visibility.Visible;
            this.txtFromNo.Visibility = Visibility.Visible;
            this.txtToNo.Visibility = Visibility.Visible;
            this.btnSet.Visibility = Visibility.Visible;
            this.btnRefresh.Visibility = Visibility.Visible;
            this.dgOverall01.Visibility = Visibility.Visible;
            this.dgOverall02.Visibility = Visibility.Visible;

            this.xctk_dtpInvDat.IsEnabled = false;
            this.autoItemSearch.IsEnabled = false;

            string Date1a = this.xctk_dtpInvDat.Text;

            string itemCode1 = "";
            //string itemCode1des = "";

            if (this.autoItemSearch.SelectedValue != null)
            {
                itemCode1 = this.autoItemSearch.SelectedValue.ToString().Trim();
                //itemCode1des = this.autoItemSearch.Text.Trim();
            }

            this.TicketSalesTrans01(ProcessID1: "POTCOUPONLIST01", Date1: Date1a, Date2: Date1a, isircode1: itemCode1, TerminalID1: "", UserID1: "", RptOpt1: "DTUC", PrintId1: "");   // TrHead, TrTyp, PrintId

            this.btnGenerate.Content = "_New";
        }

        private void TicketSalesTrans01(string ProcessID1 = "POTCOUPONLIST01", string Date1 = "01-Apr-2018", string Date2 = "02-Apr-2018", string isircode1 = "", string TerminalID1 = "",
           string UserID1 = "", string RptOpt1 = "DTUC", string PrintId1 = "PP")
        {
            try
            {
                this.dgOverall01.ItemsSource = null;
                //LocalReport rpt1 = null;
                this.rptTransList01.Clear();
                //string WindowTitle1 = "";
                var pap = vmr1.SetParamParkTicketTrans01(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: ProcessID1, MemoDate1: Date1, MemoDate2: Date2, isircode: isircode1,
                    TerminalID: TerminalID1, UserID: UserID1, RptOption: RptOpt1, Status1: "A");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
                if (ds1 == null)
                    return;
                this.rptTransList01 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.ParkSalesTrans01>();

                this.ChangeMark(0);
                this.ShowGridInfo();


            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("PSI.Rpt-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ShowGridInfo()//string ProcessID1
        {

            this.dgOverall01.ItemsSource = this.rptTransList01;
        }

        private void btnAllChecked_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeMark(1);
        }

        private void btnAllUnchecked_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeMark(0);
        }

        private void ChangeMark(int Mark1)
        {
            if (this.rptTransList01.Count == 0)
                return;

            this.dgOverall01.ItemsSource = null;
            foreach (var item in rptTransList01)
                item.tokencnt1 = Mark1;

            this.dgOverall01.ItemsSource = this.rptTransList01;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

            this.dgOverall01.ItemsSource = null;
            this.dgOverall02.ItemsSource = null;
            var list01 = this.rptTransList01.FindAll(x => x.tokencnt1 == 1).ToList();
            var list02 = this.rptTransList02.FindAll(x => x.tokencnt1 == 0).ToList();
      
            if (list01.Count > 0)
            {
                //this.rptTransList02.Clear();
                var itemlist01 = this.rptTransList01.FindAll(x => x.tokencnt1 == 1).ToList();

                this.rptTransList02.AddRange(itemlist01);

                this.rptTransList01.RemoveAll(x => x.tokencnt1 == 1);
            }

            if (list02.Count > 0)
            {
                //this.rptTransList02.Clear();
                var itemlist02 = this.rptTransList02.FindAll(x => x.tokencnt1 == 0).ToList();
                this.rptTransList01.AddRange(itemlist02);

                this.rptTransList02.RemoveAll(x => x.tokencnt1 == 0);
            }
            this.rptTransList01 = this.rptTransList01.OrderBy(x => x.maxtnum).ToList();
            this.rptTransList02 = this.rptTransList02.OrderBy(x => x.maxtnum).ToList();

            this.dgOverall01.ItemsSource = this.rptTransList01;
            this.dgOverall02.ItemsSource = this.rptTransList02;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string tsircode1 = this.autoItemSearch.SelectedValue.ToString();
            string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
            DateTime invdat1 = DateTime.Parse(this.xctk_dtpInvDat.Text.Trim().Substring(0, 11));
            /*
             * 
             string CompCode, string _tsircode, DateTime _tokendat, string _tokehead, string _cbSectCode,
             * 
             */
            //if (this.rptTransList02.Count == 0)
            //    return;

            DataSet ds1 = vmr1.GetDataSetForCancelUpdateCoupon(_rptTransList02a: this.rptTransList02);
            // CompCode: WpfProcessAccess.CompInfList[0].comcod, _tsircode: tsircode1, _tokendat: invdat1,  _tokehead: "POT", _cbSectCode: cbSectCode1, 
            if (ds1.Tables[0].Rows.Count == 0)
                return;

            var pap1 = vmr1.SetParamCancelUpdateCoupon(WpfProcessAccess.CompInfList[0].comcod, _tsircode: tsircode1, _tokendat: invdat1, _tokehead: "POT", _cbSectCode: cbSectCode1, ds1: ds1);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            this.rptTransList02.Clear();
            this.dgOverall02.ItemsSource = null;
            MessageBox.Show("Successfully Updated", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);           
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtFromNo.Text.Trim() == string.Empty || this.txtToNo.Text.Trim() == string.Empty)
                return;

            this.dgOverall01.ItemsSource = null;

            string TNum1a = this.txtFromNo.Text;
            string TNum2a = this.txtToNo.Text;

            Int64 ii1 = Int64.Parse(TNum1a);
            Int64 ii2 = Int64.Parse(TNum2a);
            var list1 = this.rptTransList01.FindAll(x => Int64.Parse(x.maxtnum) >= ii1 && Int64.Parse(x.maxtnum) <= ii2);

            foreach (var item in list1)
            {
                item.tokencnt1 = 1;
                //if (ii1 > ii2)
                //    break;

                //if (ii1.ToString() == item.maxtnum)
                //    item.tokencnt1 = 1;

                //ii1++;
            }

            this.dgOverall01.ItemsSource = this.rptTransList01;

        }
        private void CleanUpScreen()
        {
            this.btnUpdate.Visibility = Visibility.Hidden;
            this.btnAllChecked.Visibility = Visibility.Hidden;
            this.btnAllUnchecked.Visibility = Visibility.Hidden;
            this.lblFrom.Visibility = Visibility.Hidden;
            this.lblTo.Visibility = Visibility.Hidden;
            this.txtFromNo.Visibility = Visibility.Hidden;
            this.txtToNo.Visibility = Visibility.Hidden;
            this.btnSet.Visibility = Visibility.Hidden;
            this.btnRefresh.Visibility = Visibility.Hidden;
            this.dgOverall01.Visibility = Visibility.Collapsed;
            this.dgOverall02.Visibility = Visibility.Collapsed;
        }

    }
}
