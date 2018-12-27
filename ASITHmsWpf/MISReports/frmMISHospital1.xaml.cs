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
using ASITHmsWpf.MISReports;
namespace ASITHmsWpf.MISReports
{
    /// <summary>
    /// Interaction logic for frmMISHospital1.xaml
    /// </summary>
    public partial class frmMISHospital1 : UserControl
    {
        string TitaleTag1, TitaleTag2;  // 
        public frmMISHospital1()
        {
            InitializeComponent();
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            TitaleTag1 = this.Tag.ToString();   // Predefined value of Tag property set at design time
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                TitaleTag2 = this.Tag.ToString(); // Dynamic value of Tag property set at design time
                this.ActivateAuthObjects();
                if (this.tbiRferralLedger1.Visibility == Visibility.Collapsed && this.tbiInvoiceEdit1.Visibility == Visibility.Collapsed)
                {
                    this.tabPanel1.Visibility = Visibility.Hidden;
                    this.lblTitle1.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (this.tbiRferralLedger1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 0;
                    else if (this.tbiInvoiceEdit1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 2;                 
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Health-MIS-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ActivateAuthObjects()
        {
            try
            {
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmMISHospital1_frmMISHospital101") == null)
                {
                    this.tbiRferralLedger1.Visibility = Visibility.Collapsed;
                    this.tbiRferralLedger1a.Visibility = Visibility.Collapsed;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmMISHospital1_frmMISHospital102") == null)
                {
                    this.tbiInvoiceEdit1.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Health-MIS-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void tabPanel1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.tabPanel1.SelectedIndex < 0)
                    return;

                int TabIndex1 = this.tabPanel1.SelectedIndex;
                this.ShowTabInfo(TabIndex1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Health-MIS-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ShowTabInfo(int TabIndex1)
        {
            try
            {
                if (((TabItem)this.tabPanel1.Items[TabIndex1]).Visibility != Visibility.Visible)
                    return;

                if (TabIndex1 == 0 && this.stpkTab0.Children.Count == 0)
                    this.stpkTab0.Children.Add(new frmMISHospital101());

                else if (TabIndex1 == 2 && this.stpkTab1.Children.Count == 0)
                    this.stpkTab1.Children.Add(new frmMISHospital102());

                this.tabPanel1.SelectedItem = this.tabPanel1.Items[TabIndex1];
                this.lblTitle1.Content = ((TabItem)this.tabPanel1.SelectedItem).Tag.ToString();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Health-MIS-04: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
