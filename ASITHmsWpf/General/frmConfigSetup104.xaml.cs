using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.Data;
using ASITHmsViewMan.General;
using System.IO;
using Microsoft.Win32;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmConfigSetup104.xaml
    /// </summary>
    public partial class frmConfigSetup104 : UserControl
    {

        vmConfigSetup1 vm1 = new vmConfigSetup1();

        private bool FrmInitialized = false;
        public frmConfigSetup104()
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
            }
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {

        }
    }
}
