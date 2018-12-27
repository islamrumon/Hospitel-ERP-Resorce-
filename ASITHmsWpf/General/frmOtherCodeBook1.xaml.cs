using ASITFunLib;
using ASITHmsEntity;
using ASITHmsRpt1GenAcc.General;
using ASITHmsViewMan.General;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmOtherCodeBook1.xaml
    /// </summary>
    public partial class frmOtherCodeBook1 : UserControl
    {
        private bool FrmInitialized = false;
        private List<vmOtherCodeBook1.ItemMfgCompList> MfgCompanyList = new List<vmOtherCodeBook1.ItemMfgCompList>();

        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private vmOtherCodeBook1 vm1 = new vmOtherCodeBook1();
        private List<HmsEntityGeneral.SirInfCodeBook> ListSirInfMainCodeBook { get; set; }
        private List<HmsEntityGeneral.SirInfCodeBook> ListSirInfMain2CodeBook { get; set; }
        private List<vmOtherCodeBook1.MedicineItemList> ListMedicine { get; set; }
        private List<vmOtherCodeBook1.MedicineItemInfoList> ListMedicineInf { get; set; }
        public frmOtherCodeBook1()
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

                    WpfProcessAccess.AccSirCodeList = null;
                    WpfProcessAccess.StaffGroupList = null;
                    WpfProcessAccess.StaffList = null;
                    WpfProcessAccess.SupplierContractorList = null;
                    WpfProcessAccess.InvItemGroupList = null;
                    WpfProcessAccess.InvItemList = null;                   

                   var pap1 = this.vm1.SetParamItemMfgList(WpfProcessAccess.CompInfList[0].comcpcod);
                   DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                   if (ds1 == null)
                       return;
                   this.MfgCompanyList = ds1.Tables[0].DataTableToList<vmOtherCodeBook1.ItemMfgCompList>();


                   var pap2 = this.vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "%", "1234");
                   DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
                   if (ds2 == null)
                       return;

                   this.ListSirInfMainCodeBook = ds2.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                   this.ListSirInfMain2CodeBook = ListSirInfMainCodeBook.FindAll(x => x.sircode.Substring(0, 4) == "4521" && x.sircode.Substring(4, 3) != "000" && x.sircode.Substring(7, 2) == "00");
                   foreach (var item in ListSirInfMain2CodeBook)
                   {
                       this.cmbMainCat1.Items.Add(new ComboBoxItem { Content = item.sirdesc, Tag = item.sircode });
                   }
                   this.cmbMainCat1.SelectedIndex = 0;

                   this.AtxtMfgComp.Items.Clear();
                   this.AtxtMfgComp.AutoSuggestionList.Clear();
                   this.AtxtMfgComp.AddSuggstionItem("(Unknown)", "000000000000");
                   foreach (var item1m in this.MfgCompanyList)
                   {
                       this.AtxtMfgComp.AddSuggstionItem(item1m.actdesc.Trim(), item1m.actcode.Trim());
                   }
                   //this.cmbMfName.Items.Clear();
                   //this.cmbMfName.Items.Add(new ComboBoxItem {Content="(Unknown)", Tag="000000000000" });
                   //this.cmbMfName.SelectedIndex = 0;
                   this.stkedit.Visibility = Visibility.Hidden;
                   this.grListView.Visibility = Visibility.Hidden;
               }
        }

       
       

        private void cmbMainCat1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.cmbSubCat1.ItemsSource = null;
            this.cmbSubCat1.Items.Clear();
            this.cmbSubCat1.Items.Refresh();
            string mainCatTag1 = ((ComboBoxItem)this.cmbMainCat1.SelectedItem).Tag.ToString().Substring(0, 7);
            var sublist1 = ListSirInfMainCodeBook.FindAll(x => x.sircode.Substring(0, 7) == mainCatTag1);           
            var listTag = sublist1.FindAll(m => m.sircode.Substring(7,2) != "00");
            foreach (var item in listTag)
            {
                this.cmbSubCat1.Items.Add(new ComboBoxItem { Content = item.sirdesc, Tag = item.sircode });
            }
            this.cmbSubCat1.SelectedIndex = 0;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            this.clearfield();
            this.dgvItem1.ItemsSource = null;
            this.dgvItem1.Items.Refresh();

            //this.lvSir.ItemsSource = null;
            //this.lvSir.Items.Refresh();

            this.stkedit.Visibility = Visibility.Hidden;
            this.grListView.Visibility = Visibility.Hidden;
          
            string btnName = this.btnShow.Content.ToString();
            if (btnName == "Next")
            {
                this.cmbMainCat1.IsEnabled = true;
                this.cmbSubCat1.IsEnabled = true;
                this.btnShow.Content = "Show";
                return;
            }

            string mainCat1 = ((ComboBoxItem)this.cmbMainCat1.SelectedItem).Tag.ToString();
            string subCat1 = ((ComboBoxItem)this.cmbSubCat1.SelectedItem).Tag.ToString();
            ASITFunParams.ProcessAccessParams pap1 = this.vm1.SetParamMedicineList(WpfProcessAccess.CompInfList[0].comcpcod, subCat1.Substring(0, 9));
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.ListMedicine = ds1.Tables[0].DataTableToList<vmOtherCodeBook1.MedicineItemList>();
            this.ListMedicineInf = ds1.Tables[1].DataTableToList<vmOtherCodeBook1.MedicineItemInfoList>();
           
            this.AtxtItemCode.Items.Clear();
            this.AtxtItemCode.AutoSuggestionList.Clear();
            foreach (var item1 in this.ListMedicine)
            {
                string trdesc1 = item1.sirdesc.Trim() + " (" + item1.sirunit + "), Unit Cost: " + item1.costprice.ToString("#,##0.00") + ", Sales Rate: " + item1.saleprice.ToString("#,##0.00");
                this.AtxtItemCode.AddSuggstionItem(trdesc1, item1.sircode.Trim());
            }

            this.dgvItem1.ItemsSource = this.ListMedicine;
            this.cmbMainCat1.IsEnabled = false;
            this.cmbSubCat1.IsEnabled = false;
            this.stkedit.Visibility = Visibility.Visible;
            this.grListView.Visibility = Visibility.Visible;
            this.btnShow.Content = "Next";            
        }
        private void showTextBoxData(string Code1)
        {
            var row1 = this.ListMedicine.Find(x => x.sircode == Code1);

            string sirCod1 = row1.sircode.ToString();
            this.txtSirCode1.Text = sirCod1.Substring(0, 2);
            this.txtSirCode2.Text = sirCod1.Substring(2, 2);
            this.txtSirCode3.Text = sirCod1.Substring(4, 3);
            this.txtSirCode4.Text = sirCod1.Substring(7, 2);
            this.txtSirCode5.Text = sirCod1.Substring(9, 3);
            this.lblSirCode.Tag = sirCod1;

            this.wtTxtsirdesc.Text = row1.sirdesc.ToString();
            this.wtTxtsirunit.Text = row1.sirunit.ToString();
            this.AtxtMfgComp.Text = row1.mfgcomnam.ToString().Trim();    // "SIMI00010003"
            this.wtTxtcostP.Text = decimal.Parse(row1.costprice.ToString()).ToString("#,##0.00;-#,##0.00; ");
            this.wtTxtsalesP.Text = decimal.Parse(row1.saleprice.ToString()).ToString("#,##0.00;-#,##0.00; ");
            this.wtTxtGenericItems.Text = row1.genrnam.ToString().Trim();   // "SIMI00010004"

            this.wtTxtFullName.Text = this.FindValue(Code1, "SIMI00101001");
            this.wtTxtMfgCode.Text = this.FindValue(Code1, "SIMI00101002"); 
            this.wtTxtChStruc.Text = this.FindValue(Code1, "SIMI00101005");
            this.wtTxtUsage.Text = this.FindValue(Code1, "SIMI00101006");
            this.wtTxtWPackSize.Text = this.FindValue(Code1, "SIMI00101007");
            this.wtTxtRPackSize.Text = this.FindValue(Code1, "SIMI00101008");
            this.wtTxtRemakrs.Text = this.FindValue(Code1, "SIMI00101099"); 
        }

        private void clearfield()
        {
            this.txtSirCode1.Text = "";
            this.txtSirCode2.Text = "";
            this.txtSirCode3.Text = "";
            this.txtSirCode4.Text = "";
            this.txtSirCode5.Text = "";
            this.lblSirCode.Tag = "XXXXXXXXXXXX";

            this.wtTxtsirdesc.Text = "";
            this.wtTxtsirunit.Text = "";
            this.AtxtMfgComp.Text = "";
            this.wtTxtcostP.Text = "";
            this.wtTxtsalesP.Text = "";
            this.wtTxtGenericItems.Text = "";

            this.wtTxtFullName.Text = "";
            this.wtTxtMfgCode.Text = "";
            this.wtTxtChStruc.Text = "";
            this.wtTxtUsage.Text = "";
            this.wtTxtWPackSize.Text = "";
            this.wtTxtRPackSize.Text = "";
            this.wtTxtRemakrs.Text = "";
        }
        private string FindValue(string Code1, string GenCod1)
        {
            var xx = this.ListMedicineInf.Find(x => x.sircode == Code1 && x.gencode == GenCod1);
            if (xx == null)
                return "";
            return xx.dataval.Trim();
        }
        private void CodeValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string Sircode1a = this.lblSirCode.Tag.ToString().Trim();
            for (int i = 0; i < this.ListMedicine.Count; i++ )
            {
                if (this.ListMedicine[i].sircode == Sircode1a)
                {
                    this.ListMedicine[i].sirdesc = this.wtTxtsirdesc.Text.Trim();
                    this.ListMedicine[i].sirunit = this.wtTxtsirunit.Text.Trim();
                    this.ListMedicine[i].genrnam = this.wtTxtGenericItems.Text.Trim();
                    this.ListMedicine[i].mfgcomcod = this.AtxtMfgComp.Value;
                    this.ListMedicine[i].mfgcomnam = this.AtxtMfgComp.Text;
                    this.ListMedicine[i].costprice = decimal.Parse("0" + this.wtTxtcostP.Text.Trim());
                    this.ListMedicine[i].saleprice = decimal.Parse("0" + this.wtTxtsalesP.Text.Trim());
                    this.dgvItem1.Items.Refresh();
                    break;
                }
            }
            string cod_SIMI00101001 = this.wtTxtFullName.Text.Trim();       // ITEM TRADE NAME (FULL FORM)
            string cod_SIMI00101002 = this.wtTxtMfgCode.Text.Trim();        // MANUFACTURING ID (PRODUCT BAR CODE)
            string cod_SIMI00101003 = this.AtxtMfgComp.Value;               // MANUFACTURER COMPANY NAME (compnay code)
            string cod_SIMI00101004 = this.wtTxtGenericItems.Text.Trim();   // GENERIC MEDICINE NAME
            string cod_SIMI00101005 = this.wtTxtChStruc.Text.Trim();        // CHEMICAL STRUCTURE DESCRIPTION
            string cod_SIMI00101006 = this.wtTxtUsage.Text.Trim();          // USAGE TYPE
            string cod_SIMI00101007 = this.wtTxtWPackSize.Text.Trim();      // WHOLE SLSE PACK SIZE
            string cod_SIMI00101008 = this.wtTxtRPackSize.Text.Trim();      // RETAIL SALES PACK SIZE
            string cod_SIMI00101099 = this.wtTxtRemakrs.Text.Trim();        // REMARKS (IF ANY)

            this.ListMedicineInf.RemoveAll(x => x.sircode == Sircode1a && (x.gencode == "SIMI00101001" || x.gencode == "SIMI00101002" || x.gencode == "SIMI00101003" || x.gencode == "SIMI00101004" || x.gencode == "SIMI00101005"
                    || x.gencode == "SIMI00101006" || x.gencode == "SIMI00101007" || x.gencode == "SIMI00101008" || x.gencode == "SIMI00101099"));

            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101001", dataval = cod_SIMI00101001 });
            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101002", dataval = cod_SIMI00101002 });
            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101003", dataval = cod_SIMI00101003 });
            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101004", dataval = cod_SIMI00101004 });
            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101005", dataval = cod_SIMI00101005 });
            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101006", dataval = cod_SIMI00101006 });
            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101007", dataval = cod_SIMI00101007 });
            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101008", dataval = cod_SIMI00101008 });
            this.ListMedicineInf.Add(new vmOtherCodeBook1.MedicineItemInfoList() { sircode = Sircode1a, gencode = "SIMI00101099", dataval = cod_SIMI00101099 });

            DataSet ds1 = vm1.GetDataSetForUpdateMedicineInfo(WpfProcessAccess.CompInfList[0].comcod, 
                          this.ListMedicine.FindAll(x => x.sircode == Sircode1a), this.ListMedicineInf.FindAll(x => x.sircode == Sircode1a));

            var pap2 = vm1.SetParamUpdateMedicineInfo(WpfProcessAccess.CompInfList[0].comcpcod, ds1, Sircode1a, "SIMI001");
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
            if (ds2 == null)
                return;


            // Database Updating Code will be written from here  -- Hafiz 15-Jul-2016 at 5:51 AM

            return;

            /*
            string Sircode1 = this.txtSirCode1.Text.Trim() + this.txtSirCode2.Text.Trim() + this.txtSirCode3.Text.Trim() + this.txtSirCode4.Text.Trim() + this.txtSirCode5.Text.Trim();
            if (Sircode1 == "")
            {
                return;
            }

            if ((Sircode1.Substring(2, 2) == "00" && Sircode1.Substring(4, 8) != "00000000")
               || (Sircode1.Substring(4, 3) == "000" && Sircode1.Substring(7, 5) != "00000") || (Sircode1.Substring(7, 2) == "00" && Sircode1.Substring(9, 3) != "000"))
            {
                MessageBox.Show("Could not add invalid code. Please try with valid code", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (this.txtSirCode5.IsEnabled)
            {
                var list1 = this.ListMedicine.FindAll(x => x.sircode == Sircode1);
                if (list1.Count > 0)
                {
                    MessageBox.Show("Could not add code. It is already exist in the code book", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            }

            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
               MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            string mSirdesc1 = this.wtTxtsirdesc.Text.Trim();
            string mSirunit = this.wtTxtsirunit.Text.Trim();
            //string mManufac1 = this.wtTxtMfName.Text.Trim();
            string mcost1 = this.wtTxtcostP.Text.Trim();
            string msell1 = this.wtTxtsalesP.Text.Trim();
            string AddEdit1 = this.btnUpdate.Tag.ToString().Trim();

            //var pap1 = this.vm1.SetParamUpdateAcInf(WpfProcessAccess.CompInfList[0].comcpcod, Sircode1, mSirdesc1, Sirtype1, Sirtdesc1, sirUnit1, AddEdit1);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            //if (ds2 == null)
            //{
            //    MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
            //    return;
            //}
            */
        }
      
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            this.clearfield();
        }

        private void AtxtItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtItemCode.Value.Length == 0)
                return;

            if (this.AtxtItemCode.Text.Trim().Length == 0)
                return;

            string rsircode1 = this.AtxtItemCode.Value;
            var lvi1 = this.ListMedicine.Find(x => x.sircode == AtxtItemCode.Value);
            this.lblUnit1.Content = lvi1.sirunit;
            //this.txtinvCostRate.Text = lvi1.costprice.ToString("#,##0.00");
            //this.txtinvSaleRate.Text = lvi1.saleprice.ToString("#,##0.00");

            int z = 0;
            foreach (var item3 in this.ListMedicine)
            {
                if (item3.sircode == rsircode1)
                    break;
                z++;
            }
            this.dgvItem1.ScrollIntoView(this.ListMedicine[z]);
            this.dgvItem1.SelectedIndex = z;
            this.dgvItem1.Focus();
            this.showTextBoxData(rsircode1);
        }
    
        private void dgvItem1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.dgvItem1.SelectedIndex < 0)
                return;

            string Code1 = this.ListMedicine[this.dgvItem1.SelectedIndex].sircode;
            this.showTextBoxData(Code1);
        }

      

    }
}
