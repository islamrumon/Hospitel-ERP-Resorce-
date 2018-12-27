using ASITHmsEntity;
using ASITHmsViewMan.Inventory;
using System;
using System.Collections.Generic;
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
using ASITFunLib;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt2Inventory;
using System.Text.RegularExpressions;

namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmEntryInvMgt102.xaml
    /// </summary>
    public partial class frmEntryInvMgt102 : UserControl
    {
        private List<HmsEntityInventory.InvStdStockList> InvItemStockList = new List<HmsEntityInventory.InvStdStockList>();
        private List<HmsEntityInventory.InvStdStockList> InvPartItemStockList = new List<HmsEntityInventory.InvStdStockList>();
        private vmEntryInvMgt1 vm1 = new vmEntryInvMgt1();
        private int SaleGrpIdx = 0;

        private bool FrmInitialized = false;
        public frmEntryInvMgt102()
        {
            InitializeComponent();

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
                var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
                foreach (var itemd1 in deptList1)
                {
                    this.cmbDept.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                }
            }
        }
        private void cmbDept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cmbItemGroup0.Items.Clear();
            this.InvItemStockList.Clear();
            this.InvPartItemStockList.Clear();
            this.dgSales1.ItemsSource = null;
            this.dgSales1.Items.Refresh();
        }

        private void cmbItemGroup0_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbItemGroup0.Items.Count == 0)
                return;

            this.BtnShowStock_OnClick(null, null);
        }

        private void BtnShowStock_OnClick(object sender, RoutedEventArgs e)
        {
            this.AtxtItemCode0.Text = "";
            this.txtMaxStock.Text = "";
            this.txtminStock.Text = "";
            this.txtReorder.Text = "";
            this.lblUnit2.Content = "";
            string sectcod = ((ComboBoxItem)this.cmbDept.SelectedItem).Tag.ToString();

            if (this.cmbItemGroup0.Items.Count == 0)
            {
                var pap1 = vm1.SetParamInvStdStockList(WpfProcessAccess.CompInfList[0].comcpcod, sectcod, "%");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                this.cmbItemGroup0.Items.Clear();
                this.InvItemStockList.Clear();
                this.InvItemStockList = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvStdStockList>();
                foreach (DataRow item in ds1.Tables[1].Rows)
                {
                    this.cmbItemGroup0.Items.Add(new ComboBoxItem()
                    {
                        Content = item["sirtype"].ToString().Trim(),
                        Tag = item["itmgroup"].ToString().Trim() + item["sirtype"].ToString().Trim()
                    });
                }
                this.cmbItemGroup0.SelectedIndex = SaleGrpIdx;
            }

            this.InvPartItemStockList.Clear();
            string filterTag1 = ((ComboBoxItem)this.cmbItemGroup0.SelectedItem).Tag.ToString().Trim();
            this.InvPartItemStockList = this.InvItemStockList.FindAll(x => x.rsircode.Substring(0, 9) + x.msirtype.Trim() == filterTag1);

            this.AtxtItemCode0.Items.Clear();
            this.AtxtItemCode0.AutoSuggestionList.Clear();
            foreach (var item1 in this.InvPartItemStockList)
            {
                string trdesc1 = item1.sirdesc.Trim() + " (" + item1.sirunit + "), Max-Stock: " + item1.maxstock.ToString("#,##0.00") + ", Min-Stock: " + item1.minstock.ToString("#,##0.00");
                this.AtxtItemCode0.AddSuggstionItem(trdesc1, item1.rsircode.Trim());
            }
            this.dgSales1.ItemsSource = this.InvPartItemStockList;
        }
        private void BtnPrintSale_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.cmbItemGroup0.Items.Count == 0)
                return;

            LocalReport rpt1 = new LocalReport();
            var list3 = WpfProcessAccess.GetRptGenInfo();

            string filterTag1 = ((ComboBoxItem)this.cmbItemGroup0.SelectedItem).Tag.ToString().Trim();
            var list1 = this.InvItemStockList.FindAll(x => x.rsircode.Substring(0, 7) + x.msirtype.Trim() == filterTag1 && (x.maxstock > 0 || x.minstock > 0));

            rpt1 = StoreReportSetup.GetLocalReport("InvMgt.RptStdStockList1", list1, null, list3);
            string WindowTitle1 = "Inventory Stock";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void AtxtItemCode0_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtItemCode0.Value.Length == 0)
                return;

            if (this.AtxtItemCode0.Text.Trim().Length == 0)
                return;

            string rsircode1 = this.AtxtItemCode0.Value;
            var lvi1 = this.InvPartItemStockList.Find(x => x.rsircode == AtxtItemCode0.Value);
            this.lblUnit2.Content = lvi1.sirunit;
            this.txtMaxStock.Text = lvi1.maxstock.ToString("#,##0.00");
            this.txtminStock.Text = lvi1.minstock.ToString("#,##0.00");
            this.txtReorder.Text = lvi1.reordrlvl.ToString("#,##0.00");

            int z = 0;
            foreach (var item3 in this.InvPartItemStockList)
            {
                if (item3.rsircode == rsircode1)
                    break;
                z++;
            }

            this.dgSales1.ScrollIntoView(this.InvPartItemStockList[z]);
            this.dgSales1.SelectedIndex = z;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception exp1)
            {
                return;
            }
        }

        private void BtnChangeStock_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.AtxtItemCode0.Value.Length == 0)
                return;

            if (this.AtxtItemCode0.Text.Trim().Length == 0)
                return;

            decimal MaxStock = decimal.Parse("0" + this.txtMaxStock.Text.Trim());
            decimal MinStock = decimal.Parse("0" + this.txtminStock.Text.Trim());
            decimal ReorderLev = decimal.Parse("0" + this.txtReorder.Text.Trim());
            string rsircode1 = this.AtxtItemCode0.Value;
            int z = 0;
            foreach (var item3 in this.InvPartItemStockList)
            {
                if (item3.rsircode == rsircode1)
                {
                    item3.maxstock = MaxStock;
                    item3.minstock = MinStock;
                    item3.reordrlvl = ReorderLev;
                    break;
                }
                z++;
            }
            this.BtnShowStock_OnClick(null, null);
            this.dgSales1.ScrollIntoView(this.InvPartItemStockList[z]);
            this.dgSales1.SelectedIndex = z;
        }

        private void BtnUpdateStock_OnClick(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                                      MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            string sectcod = ((ComboBoxItem)this.cmbDept.SelectedItem).Tag.ToString();

            DataSet ds1 = vm1.GetDataSetStdStockUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, sectcod1: sectcod, StockList: this.InvPartItemStockList);

            //String xx1 = ds1.GetXml().ToString();

            var pap1 = vm1.SetParamStdStockUpdate(WpfProcessAccess.CompInfList[0].comcod, ds1, sectcod);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if ((ds2 == null) || WpfProcessAccess.DatabaseErrorInfoList != null)
            {
                System.Windows.MessageBox.Show("Could not updated information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            System.Windows.MessageBox.Show("Information updated successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            SaleGrpIdx = this.cmbItemGroup0.SelectedIndex;
            this.cmbItemGroup0.Items.Clear();
            this.BtnShowStock_OnClick(null, null);

        }

        private void DgSales1_OnLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
       
    }
}
