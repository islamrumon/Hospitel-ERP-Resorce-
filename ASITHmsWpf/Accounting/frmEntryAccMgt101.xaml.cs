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
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using ASITHmsRpt1GenAcc.Accounting;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Collections;
using Xceed.Wpf.Toolkit;
namespace ASITHmsWpf.Accounting
{
    /// <summary>
    /// Interaction logic for frmEntryAccMgt101.xaml
    /// </summary>
    public partial class frmEntryAccMgt101 : UserControl
    {
        string TitaleTag1, TitaleTag2;  // 

        private List<HmsEntityAccounting.AccCashBanRecon1> AccRecnList = new List<HmsEntityAccounting.AccCashBanRecon1>();
        private List<HmsEntityAccounting.AccCashBanRecon1> AccRecnList1 = new List<HmsEntityAccounting.AccCashBanRecon1>();
        private List<HmsEntityAccounting.AccCashBanRecon1> AccRecnList2 = new List<HmsEntityAccounting.AccCashBanRecon1>();
        private vmEntryVoucher1 vm1Entry = new vmEntryVoucher1();
        private vmReportAccounts1 vmrptAcc = new vmReportAccounts1();

        public frmEntryAccMgt101()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            this.TitaleTag1 = this.Tag.ToString();   // Predefined value of Tag property set at design time
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                this.TitaleTag2 = this.Tag.ToString().Trim();
                this.ActivateAuthObjects();

                this.xctk_dtpRecnDate.Value = DateTime.Today;
                this.HideObjects_On_Load();

                this.xctk_dtpRecnDate.Value = DateTime.Today;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("RCN-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ActivateAuthObjects()
        {
            try
            {
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAccMgt101_btnUpdate") == null)
                {
                    this.btnUpdate.Visibility = Visibility.Hidden;
                    this.btnUpdate.IsEnabled = false;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("RCN-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, 
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void HideObjects_On_Load()
        {
            try
            {
                this.ClearFormContent();
                ////this.btnUpdate.Visibility = Visibility.Hidden;
                if (WpfProcessAccess.AccCodeList == null)
                    WpfProcessAccess.GetAccCodeList();

                var CactcodeList = new List<HmsEntityGeneral.AcInfCodeBook>();
                CactcodeList = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902") && (x.actcode.Substring(8, 4) != "0000"));

                CactcodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
                {
                    return x.actdesc.CompareTo(y.actdesc);
                });

                this.cmbAcHead.Items.Clear();
                foreach (var item in CactcodeList)
                    this.cmbAcHead.Items.Add(new ComboBoxItem() { Content = item.actdesc.Trim(), Tag = item.actcode });

                this.cmbAcHead.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("RCN-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnShow.Content.ToString() == "Next")
            {
                this.ClearFormContent();
                this.stkpAccHead.IsEnabled = true;
                this.btnShow.Content = "Show";
                return;
            }

            this.stkpAccHead.IsEnabled = false;

            this.GetReconData();
            this.btnShow.Content = "Next";
            ////this.btnUpdate.Visibility = Visibility.Visible;
        }

        private void GetReconData()
        {
            try
            {
                string FromDate = this.xctk_dtpRecnDate.Text.Trim();
                string ToDate = FromDate;
                string accCode = ((ComboBoxItem)this.cmbAcHead.SelectedItem).Tag.ToString().Trim();
                var pap1 = vmrptAcc.SetParamCashBankRecon(WpfProcessAccess.CompInfList[0].comcod, FromDate, ToDate, accCode);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.txtAcHead1.Text = ds1.Tables[1].Rows[0]["actdesc"].ToString().Trim();
                this.AccRecnList = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccCashBanRecon1>().ToList();
                
                foreach (var item in this.AccRecnList)
                   item.recndat = null;
                
                var bal1 = this.AccRecnList.FindAll(x => x.drcr == "1C00");
                if (bal1.Count > 0)
                {
                    this.txtBalTitle1.Text = bal1[0].trdesc.Trim();
                    this.txtBalAmt1.Text = bal1[0].cram.ToString("#,##0.00;(#,##0.00);Nil");
                }

                var bal2 = this.AccRecnList.FindAll(x => x.drcr == "4C00");
                if (bal2.Count > 0)
                {
                    this.txtBalTitle2.Text = bal2[0].trdesc.Trim();
                    this.txtBalAmt2.Text = bal2[0].cram.ToString("#,##0.00;(#,##0.00);Nil");
                }

                var txtVouAmt1t = this.AccRecnList.FindAll(x => x.drcr == "2P00");
                if (txtVouAmt1t.Count > 0)
                    this.txtVouAmt1Title.Text = txtVouAmt1t[0].trdesc.Trim();

                var txtVouAmt2t = this.AccRecnList.FindAll(x => x.drcr == "3D00");
                if (txtVouAmt2t.Count > 0)
                    this.txtVouAmt2Title.Text = txtVouAmt2t[0].trdesc.Trim();

                var txtVouAmt1 = this.AccRecnList.FindAll(x => x.drcr == "2P99");
                if (txtVouAmt1.Count > 0)
                    this.txtVouAmt1.Text = txtVouAmt1[0].cram.ToString("#,##0.00;(#,##0.00);Nil");

                var txtVouAmt2 = this.AccRecnList.FindAll(x => x.drcr == "3D99");
                if (txtVouAmt2.Count > 0)
                    this.txtVouAmt2.Text = txtVouAmt2[0].cram.ToString("#,##0.00;(#,##0.00);Nil");

                this.AccRecnList1 = this.AccRecnList.FindAll(x => x.drcr == "2P01");
                this.AccRecnList2 = this.AccRecnList.FindAll(x => x.drcr == "3D01");

                this.dgRecon1.ItemsSource = this.AccRecnList1;
                this.dgRecon2.ItemsSource = this.AccRecnList2;
                this.btnUpdate.IsEnabled = (this.AccRecnList1.Count >0 || this.AccRecnList2.Count > 0);
                this.stkpRecon.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("RCN-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ClearFormContent()
        {
            this.stkpRecon.Visibility = Visibility.Collapsed;
            this.dgRecon1.ItemsSource = null;
            this.dgRecon2.ItemsSource = null;

            this.AccRecnList.Clear();
            this.AccRecnList1.Clear();
            this.AccRecnList2.Clear();

            this.txtAcHead1.Text = "";
            this.txtBalTitle1.Text = "";
            this.txtBalAmt1.Text = "";
            this.txtBalTitle2.Text = "";
            this.txtBalAmt2.Text = "";

            this.txtVouAmt1Title.Text = "";
            this.txtVouAmt2Title.Text = "";
            this.txtVouAmt1.Text = "";
            this.txtVouAmt2.Text = "";
        }
        private void dgRecon1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString() + ".";
        }
        private void dgRecon2_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString() + ".";
        }
            
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            ////var AccRecnList3 = this.AccRecnList1.FindAll(x => ((DateTime)x.recndat).Year > 1900).Union(this.AccRecnList2.FindAll(x => x.recndat.Year > 1900)).ToList();

            var AccRecnList3 = this.AccRecnList1.FindAll(x => x.recndat != null).Union(this.AccRecnList2.FindAll(x => x.recndat != null)).ToList();
            foreach (var item in AccRecnList3)
            {
                var pap1 = vm1Entry.SetParamUpdateReconVoucher(WpfProcessAccess.CompInfList[0].comcod, item.vounum, ((DateTime)item.recndat).ToString("dd-MMM-yyyy"));
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
            }
            this.btnUpdate.IsEnabled = false;
            ////this.btnUpdate.Visibility = Visibility.Hidden;
            System.Windows.MessageBox.Show("Update Successfull", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            string FromDate = this.xctk_dtpRecnDate.Text.Trim();
            string ToDate = FromDate;
            string accCode = ((ComboBoxItem)this.cmbAcHead.SelectedItem).Tag.ToString().Trim();
            var pap1 = vmrptAcc.SetParamCashBankRecon(WpfProcessAccess.CompInfList[0].comcod, FromDate, ToDate, accCode);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.txtAcHead1.Text = ds1.Tables[1].Rows[0]["actdesc"].ToString().Trim();
            var list1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccCashBanRecon1>().ToList();
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["prndate"]));
            Hashtable Params = new Hashtable();
            Params["Title1"] = ds1.Tables[1].Rows[0]["booknam"].ToString().Trim();
            Params["Title2"] = ds1.Tables[1].Rows[0]["actdesc"].ToString().Trim();
            Params["AsOnDate"] = "(As on " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["ToDate"]).ToString("dd-MMM-yyyy") + ")";
            LocalReport rpt1 = AccReportSetup.GetLocalReport("Accounting.RptAccRecon1", list1, Params, list3, null);
            string WindowTitle1 = Params["Title1"].ToString();
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void btnReconDate_Click(object sender, RoutedEventArgs e)
        {
            string tagstr1 = ((ComboBoxItem)this.cmbVouType.SelectedItem).Tag.ToString();
            string tagstr2 = (tagstr1=="PVC_RVC" ? "RVC" : "XXX");
            tagstr1 = (tagstr1 == "PVC_RVC" ? "PVC" : "FTV");
            foreach (var item in this.AccRecnList1)
            {
                if (item.vounum.Contains(tagstr1) || item.vounum.Contains(tagstr2))
                {
                    item.recndat = item.voudat;
                    item.trdesc = item.voudat.ToString("dd-MMM-yyyy");
                }
            }

            foreach (var item in this.AccRecnList2)
            {
                if (item.vounum.Contains(tagstr1) || item.vounum.Contains(tagstr2))
                {
                    item.recndat = item.voudat;
                    item.trdesc = item.voudat.ToString("dd-MMM-yyyy");
                }
            }
            this.dgRecon1.Items.Refresh();
            this.dgRecon2.Items.Refresh();
        }

        private void dgxctk_dtpRecn1Date_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((Xceed.Wpf.Toolkit.DateTimePicker)sender).IsOpen = true;
        }

        private void dgxctk_dtpRecn2Date_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((Xceed.Wpf.Toolkit.DateTimePicker)sender).IsOpen = true;
        }      
    }
}
