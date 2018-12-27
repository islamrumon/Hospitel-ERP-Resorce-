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
using System.ComponentModel;

namespace ASITHmsWpf.Accounting
{
    /// <summary>
    /// Interaction logic for frmEntryAccMgt102.xaml
    /// </summary>
    public partial class frmEntryAccMgt102 : UserControl
    {
        private List<HmsEntityGeneral.AcInfCodeBook> CactcodeList = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<vmEntryAccMgt1.ChqLeafTable> BookStatus1 = new List<vmEntryAccMgt1.ChqLeafTable>();
        vmEntryAccMgt1 vm1 = new vmEntryAccMgt1();
        public frmEntryAccMgt102()
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

            this.Objects_On_Init();  
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        
        private void Objects_On_Init()
        {
            this.xctk_dtpRegDat.Value = DateTime.Today;
            if (WpfProcessAccess.AccCodeList == null)
                WpfProcessAccess.GetAccCodeList();

            this.CactcodeList = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902") && (x.actcode.Substring(8, 4) != "0000"));

            this.CactcodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
            {
                return x.actdesc.CompareTo(y.actdesc);
            });
            this.cmbChqBank.Items.Clear();
            foreach (var itemb in CactcodeList)
            {
                if (itemb.actcode.Substring(0, 4) == "1902" || itemb.actcode.Substring(0, 4) == "2902")
                    this.cmbChqBank.Items.Add(new ComboBoxItem() { Content = itemb.actdesc, Tag = itemb.actcode });
            }
            this.cmbChqBank.SelectedIndex = 0;
            this.stkpBookList.Visibility = Visibility.Hidden;
            this.stkpNewBookReg.Visibility = Visibility.Hidden;
            this.stkpBookStatus.Visibility = Visibility.Hidden;
            // SELECT TOP (200) COMCOD, CHEQBOOKID, CHEQNUM, VOUNUM, CHEQDAT, CHEQAM, DELIVDAT, CHEQSTATUS, STATUSNOTE, ROWID FROM            dbo_acc.CHEQTRNA
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void btnExistBook_Click(object sender, RoutedEventArgs e)
        {
            string CactCode1 = ((ComboBoxItem)this.cmbChqBank.SelectedItem).Tag.ToString().Trim();
            var pap1 = vm1.SetParamShowExistingCheqBook(WpfProcessAccess.CompInfList[0].comcod, CactCode1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            var CheqBookList1 = ds1.Tables[0].DataTableToList<vmEntryAccMgt1.ChqBookRef>();
            this.cmbExistChqBook.Items.Clear();
            foreach (var item in CheqBookList1)
            {
                this.cmbExistChqBook.Items.Add(new ComboBoxItem()
                {
                    Content = "Book ID: " + item.bookid + ", Reg. Date: " + item.regdat.ToString("dd-MMM-yyyy") + ", Start No.: " + item.leafstart
                        + ", No of Leaf: " + item.leafqty + ", Used: " + item.usedleaf + ", Remains: " + item.remainleaf,
                    Tag = item.cheqbookid
                });
            }
            if (this.cmbExistChqBook.Items.Count > 0)
                this.cmbExistChqBook.SelectedIndex = 0;

            this.btnBookNext.Visibility = Visibility.Visible;
            this.btnExistBook.IsEnabled = false;
            this.btnRegBook.IsEnabled = false;
            this.cmbChqBank.IsEnabled = false;
            this.stkpBookList.Visibility = Visibility.Visible;
            this.txtLeafStart.Text = "";
        }
        private void btnBookNext_Click(object sender, RoutedEventArgs e)
        {
            this.stkpBookList.Visibility = Visibility.Hidden;
            this.stkpNewBookReg.Visibility = Visibility.Hidden;
            this.stkpBookStatus.Visibility = Visibility.Hidden;
            this.cmbExistChqBook.Items.Clear();
            this.btnBookNext.Visibility = Visibility.Hidden;
            this.btnExistBook.IsEnabled = true;
            this.btnRegBook.IsEnabled = true;
            this.cmbChqBank.IsEnabled = true;
            this.dgChqLeaf1.ItemsSource = null;
            this.cmbExistChqBook.IsEnabled = true;
            this.btnShowLeafs.Content = "Show Status";
        }
        private void btnShowLeafs_Click(object sender, RoutedEventArgs e)
        {
            if (this.cmbExistChqBook.SelectedItem == null)
                return;
            this.dgChqLeaf1.ItemsSource = null;
            this.stkpBookStatus.Visibility = Visibility.Hidden;
            if (this.btnShowLeafs.Content.ToString() == "Change")
            {
                this.cmbExistChqBook.IsEnabled = true;
                this.btnShowLeafs.Content = "Show Status";
                return;
            }

            string BookId1 = ((ComboBoxItem)this.cmbExistChqBook.SelectedItem).Tag.ToString().Trim();
            var pap1 = vm1.SetParamShowCheqBookStatus(WpfProcessAccess.CompInfList[0].comcod, BookId1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            this.BookStatus1.Clear();
            this.BookStatus1 = ds1.Tables[0].DataTableToList<vmEntryAccMgt1.ChqLeafTable>();
            this.dgChqLeaf1.ItemsSource = this.BookStatus1;
            this.stkpBookStatus.Visibility = Visibility.Visible;
            this.cmbExistChqBook.IsEnabled = false;
            this.btnShowLeafs.Content = "Change";
        }

        private void btnRegBook_Click(object sender, RoutedEventArgs e)
        {
            this.btnBookNext.Visibility = Visibility.Visible;
            this.btnExistBook.IsEnabled = false;
            this.btnRegBook.IsEnabled = false;
            this.cmbChqBank.IsEnabled = false;
            this.stkpNewBookReg.Visibility = Visibility.Visible;
            this.txtLeafStart.Text = "";
        }

        private void btnGenLeaf_Click(object sender, RoutedEventArgs e)
        {
            this.dgChqLeaf1.ItemsSource = null;
            string BookId1 = ((ComboBoxItem)this.cmbChqBank.SelectedItem).Tag.ToString() + DateTime.Parse(this.xctk_dtpRegDat.Text).ToString("yyMM") + "00";
            this.BookStatus1.Clear();

            string leafstart2 = this.txtLeafStart.Text.Trim();
            Int64 leafqty1 = Int64.Parse(((ComboBoxItem)this.cmbLeafQty.SelectedItem).Tag.ToString());
            for (Int64 i = 0; i < leafqty1; i++)
            {
                this.BookStatus1.Add(new vmEntryAccMgt1.ChqLeafTable()
                {
                    slnum = int.Parse((i + 1).ToString()),
                    comcod = WpfProcessAccess.CompInfList[0].comcod,
                    cheqbookid = BookId1,
                    cactcode = BookId1.Substring(0, 12),
                    bookid = "00",
                    cheqnum = ("00000000" + (Int64.Parse(leafstart2) + i).ToString()).Substring(("00000000" + (Int64.Parse(leafstart2) + i).ToString()).Length - 8),
                    vounum = "",
                    vounum1 = "",
                    cheqdat = DateTime.Parse("01-Jan-1900"),
                    cheqdat1 = "",
                    cheqam = 0.00m,
                    delivdat = DateTime.Parse("01-Jan-1900"),
                    delivdat1 = "",
                    cheqstatus = "B",
                    statusnote = ""
                });
            }
            this.dgChqLeaf1.ItemsSource = this.BookStatus1;
            this.stkpBookStatus.Visibility = Visibility.Visible;
        }

        private void btnUpdateNewLeaf_Click(object sender, RoutedEventArgs e)
        {
            var ChqBookRef1 = new vmEntryAccMgt1.ChqBookRef()
            {
                comcod = WpfProcessAccess.CompInfList[0].comcod,
                cheqbookid = ((ComboBoxItem)this.cmbChqBank.SelectedItem).Tag.ToString() + DateTime.Parse(this.xctk_dtpRegDat.Text).ToString("yyMM") + "00",
                cactcode = ((ComboBoxItem)this.cmbChqBank.SelectedItem).Tag.ToString(),
                bookid = DateTime.Parse(this.xctk_dtpRegDat.Text).ToString("yyMM") + "00",
                bookdesc = ((ComboBoxItem)this.cmbChqBank.SelectedItem).Content.ToString().Trim(),
                regdat = DateTime.Parse(this.xctk_dtpRegDat.Text),
                leafstart = this.txtLeafStart.Text.Trim(),
                leafqty = int.Parse(((ComboBoxItem)this.cmbLeafQty.SelectedItem).Tag.ToString()),
                booknar = this.txtChqRmrk.Text.Trim(),
                usedleaf = 0,
                remainleaf = 0
            };

            DataSet ds1 = vm1.GetDataSetForUpdateChqReg(WpfProcessAccess.CompInfList[0].comcod, ChqBookRef1, this.BookStatus1,
                          _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

            var pap1 = vm1.SetParamUpdateCheqBookReg(WpfProcessAccess.CompInfList[0].comcod, ds1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;
            System.Windows.MessageBox.Show("Cheque Book Register Updated Successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

    
    }
}
