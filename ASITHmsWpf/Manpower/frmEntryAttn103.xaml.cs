using System;
using System.Collections.Generic;
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
using ASITHmsViewMan.Manpower;
using ASITHmsEntity;
using System.ComponentModel;

namespace ASITHmsWpf.Manpower
{
    /// <summary>
    /// Interaction logic for frmEntryAttn103.xaml
    /// </summary>
    public partial class frmEntryAttn103 : UserControl
    {
        private bool FrmInitialized = false;
       // private List<vmEntryAttnLeav1.GroupAttend> StuffAttnLst01 = new List<vmEntryAttnLeav1.GroupAttend>();

        private vmReportHCM1 vmr1 = new vmReportHCM1();
        //private vmEntryAttnLeav1 vm2 = new vmEntryAttnLeav1();

        public frmEntryAttn103()
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
                this.stkpApproval.Visibility = Visibility.Hidden;
                this.btnUpdateInfo.Visibility = Visibility.Hidden;
                for (int i = -12; i < 12; i++)
                {
                    this.cmbInfoMonth.Items.Add(new ComboBoxItem() { Content = DateTime.Today.AddMonths(i).ToString("MMMM, yyyy"), Tag = DateTime.Today.AddMonths(i).ToString("yyyyMM") });
                }
                this.cmbInfoMonth.SelectedIndex = 12;

                var pap = vmr1.SetHRMList(WpfProcessAccess.CompInfList[0].comcpcod, "%", "EXISTSTAFFS");
                DataSet ds = WpfProcessAccess.GetHmsDataSet(pap);
                var tmpStaffList = ds.Tables[0].DataTableToList<vmReportHCM1.Stafflist>();
                this.AtxtEmpAll.AutoSuggestionList.Clear();

                foreach (var item1 in tmpStaffList)
                {
                    //this.AtxtEmpAll.AddSuggstionItem(item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), item1.sircode.Trim());
                    this.AtxtEmpAll.AddSuggstionItem(item1.hccode.Trim().Substring(6) + " - " + item1.hcname.Trim() + ", " + item1.designame.Trim(), item1.hccode.Trim());
                    //var mitm1 = new MenuItem() { Header = item1.sircode.Trim().Substring(6) + " - " + item1.sirdesc.Trim(), Tag = item1.sircode.Trim() };
                    var mitm1 = new MenuItem() { Header = item1.hccode.Trim().Substring(6) + " - " + item1.hcname.Trim() + ", " + item1.designame.Trim(), Tag = item1.hccode.Trim() };
                    mitm1.Click += conMenuHCMAtnAll_MouseClick;
                    this.conMenuHCMAtnAll.Items.Add(mitm1);
                }
            }
        }

        private void ActivateAuthObjects()
        {

        }
        private void conMenuHCMAtnAll_MouseClick(object sender, RoutedEventArgs e)
        {
            this.AtxtEmpAll.Text = ((MenuItem)sender).Header.ToString().Trim();
        }

        private void btnShowInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AtxtEmpAll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.AtxtEmpAll.ContextMenu.IsOpen = true;
        }
      
    }
}

