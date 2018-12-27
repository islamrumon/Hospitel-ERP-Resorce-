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
    /// Interaction logic for frmDashboardG6.xaml
    /// </summary>
    public partial class frmDashboardG6 : UserControl
    {
        private List<Testdata> chartlist = new List<Testdata>().ToList();
        public frmDashboardG6()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                chartlist.Add(new Testdata { name = "A", value1 = 25, value2 = 30 });
                chartlist.Add(new Testdata { name = "B", value1 = 20, value2 = 35 });
                chartlist.Add(new Testdata { name = "C", value1 = 15, value2 = 55 });
                chartlist.Add(new Testdata { name = "D", value1 = 35, value2 = 44 });
                chartlist.Add(new Testdata { name = "E", value1 = 30, value2 = 20 });
                chartlist.Add(new Testdata { name = "F", value1 = 49, value2 = 87 });
                chartlist.Add(new Testdata { name = "G", value1 = 60, value2 = 33 });
                this.chrtColumnS.DataContext = this.chartlist;
                this.dgvChart.ItemsSource = this.chartlist;
                this.DataContext = this;
            }
            catch
            {

            }
        }

    }
}
