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

namespace ASITHmsWpf.Marketing
{
    /// <summary>
    /// Interaction logic for frmEntryMarketing1.xaml
    /// </summary>
    public partial class frmEntryMarketing1 : UserControl
    {
        string TitaleTag1, TitaleTag2;
        public frmEntryMarketing1()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            TitaleTag1 = this.Tag.ToString();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TitaleTag2 = this.Tag.ToString();
        }          
    }
}
