using ASITHmsWpf.Marketing;
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
    /// Interaction logic for frmReportMarketing1.xaml
    /// </summary>
    public partial class frmReportMarketing1 : UserControl
    {
        public frmReportMarketing1()
        {
            InitializeComponent();
        }

        private void TabCtrl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.TabCtrl1.SelectedIndex < 0)
                return;

            int TabIndex1 = this.TabCtrl1.SelectedIndex;
            this.ShowTabInfo(TabIndex1);
        }
        private void ShowTabInfo(int TabIndex1)
        {
            if (TabIndex1 == 0 && this.stpkTab0.Children.Count == 0)
                this.stpkTab0.Children.Add(new frmSMSSedning());

            else if (TabIndex1 == 2 && this.stpkTab1.Children.Count == 0)
                this.stpkTab1.Children.Add(new frmMailSending());

            else if (TabIndex1 == 4 && this.stpkTab2.Children.Count == 0)
                //this.stpkTab2.Children.Add(new frmChartControl());           

            this.TabCtrl1.SelectedItem = this.TabCtrl1.Items[TabIndex1];
            //this.lblTitle1.Content = ((TabItem)this.TabCtrl1.SelectedItem).Tag.ToString();

        }
    }
}
