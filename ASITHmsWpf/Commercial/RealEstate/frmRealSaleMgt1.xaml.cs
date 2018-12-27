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
using ASITHmsWpf.Commercial.RealEstate;

namespace ASITHmsWpf.Commercial.RealEstate
{
    /// <summary>
    /// Interaction logic for frmRealSaleMgt1.xaml
    /// </summary>
    public partial class frmRealSaleMgt1 : UserControl
    {
        string TitaleTag1, TitaleTag2;  // 
        public frmRealSaleMgt1()
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
                if (this.tbiRecovery1.Visibility == Visibility.Collapsed && this.tbiSalesEntry1.Visibility == Visibility.Collapsed
                    && this.tbiSalesTarget1.Visibility == Visibility.Collapsed && this.tbiSalesReports1.Visibility == Visibility.Collapsed)
                {
                    this.tabPanel1.Visibility = Visibility.Hidden;
                    this.lblTitle1.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (this.tbiRecovery1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 0;
                    else if (this.tbiSalesEntry1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 2;
                    else if (this.tbiSalesTarget1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 4;
                    else if (this.tbiSalesReports1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 6;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("RE.Sales-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, 
                    MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ActivateAuthObjects()
        {
            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

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
                System.Windows.MessageBox.Show("RE.Sales-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, 
                    MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void ShowTabInfo(int TabIndex1)
        {
            if (TabIndex1 == 0 && this.stpkTab0.Children.Count == 0)
                this.stpkTab0.Children.Add(new frmRealSaleMgt101());

            else if (TabIndex1 == 2 && this.stpkTab1.Children.Count == 0)
                this.stpkTab1.Children.Add(new frmRealSaleMgt102());

            else if (TabIndex1 == 4 && this.stpkTab2.Children.Count == 0)
                this.stpkTab2.Children.Add(new frmRealSaleMgt103());

            else if (TabIndex1 == 6 && this.stpkTab3.Children.Count == 0)
                this.stpkTab3.Children.Add(new frmRealSaleMgt107());


            this.tabPanel1.SelectedItem = this.tabPanel1.Items[TabIndex1];
            this.lblTitle1.Content = ((TabItem)this.tabPanel1.SelectedItem).Tag.ToString();

        }
    }
}
