using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ASITHmsWpf.Commercial.RealEstate
{
    /// <summary>
    /// Interaction logic for frmRealSaleMgt107.xaml
    /// </summary>
    public partial class frmRealSaleMgt107 : UserControl
    {
        private bool FrmInitialized = false;
        public frmRealSaleMgt107()
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

        private void cmbSBrnCod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void chkAsonDate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tvRptTitle_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void tvRptTitle_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void tvRptTitle_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void tvRptTitle_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void tvRptTitle_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
