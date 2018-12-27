using ASITHmsEntity;
using ASITFunLib;
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
using ASITHmsViewMan.General;

namespace ASITHmsWpf.Commercial.RealEstate
{
    /// <summary>
    /// Interaction logic for frmRealSaleMgt102.xaml
    /// </summary>
    public partial class frmRealSaleMgt102 : UserControl
    {
        private bool FrmInitialized = false;
        private List<HmsEntityGeneral.SirInfCodeBook> RegCustList = new List<HmsEntityGeneral.SirInfCodeBook>();
        private vmHmsGeneralList1 vmGenList1a = new vmHmsGeneralList1();
        public frmRealSaleMgt102()
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
                this.ActivateAuthObjects();
                this.Objects_On_Init();
                this.CleanUpScreen();
            }
        }
        private void ActivateAuthObjects()
        {

        }
        private void Objects_On_Init()
        {

        }
        private void CleanUpScreen()
        {
            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberOnlyValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9+-.,]+");
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtClientName_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrintTrans_Click(object sender, RoutedEventArgs e)
        {

        }

        private void autoStaffRefSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

        }

        private void imgMnuAddPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void imgMnuRemovePhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNewShow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTransList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPatPhoto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void xctk_dtpInvDat_LostFocus(object sender, RoutedEventArgs e)
        {

        }



        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void lblInvMode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void autoSalesUnit_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

        }

        private void btnAddUnit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {

        }
        private void GetCustomerList()
        {
            try
            {
                var pap1 = vmGenList1a.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "5101%", "5"); //"[0-4]%"
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.RegCustList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                foreach (var item in this.RegCustList)
                {
                    item.sirdesc1 = item.sirdesc1.Substring(6);
                }


                //var pap1d = vmGenList1a.SetParamGeneralDataInfo(WpfProcessAccess.CompInfList[0].comcpcod, "SIRINF", "55", "SICD001");
                //DataSet ds1d = WpfProcessAccess.GetHmsDataSet(pap1d);
                //if (ds1d == null)
                //    return;

                //this.RegCustDetailsList = ds1d.Tables[0].DataTableToList<vmEntryPharRestPOS1.ItemCustDetailsInfo>();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("GSI-2.10: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void lblCustRef_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            HmsDialogWindow1 window1 = new HmsDialogWindow1(new General.frmSirCodeBook1(MainGroup: "5101"));
            window1.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window1.Title = "CUSTOMER REFERENCE CODE BOOK ADD/EDIT SCREEN";
            //window1.Owner = Application.Current.MainWindow;
            window1.ShowDialog();
            this.GetCustomerList();
        }

        private void autoCustRefSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

        }


   
    }
}
