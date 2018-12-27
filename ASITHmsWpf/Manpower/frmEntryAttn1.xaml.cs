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

namespace ASITHmsWpf.Manpower
{
    /// <summary>
    /// Interaction logic for frmEntryAttn1.xaml
    /// </summary>
    public partial class frmEntryAttn1 : UserControl
    {
        string TitaleTag1, TitaleTag2;  // 
        public frmEntryAttn1()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            TitaleTag1 = this.Tag.ToString();   // Predefined value of Tag property set at design time
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TitaleTag2 = this.Tag.ToString(); // Dynamic value of Tag property set at design time 
            this.ActivateAuthObjects();
            if (this.tbiMachineAttn1.Visibility == Visibility.Collapsed && this.tbiManualAttn1.Visibility == Visibility.Collapsed)
            {
                this.tabPanel1.Visibility = Visibility.Hidden;
                this.lblTitle1.Visibility = Visibility.Hidden;
            }
            else
            {
                if (this.tbiMachineAttn1.Visibility == Visibility.Visible)
                    this.tabPanel1.SelectedIndex = 0;
                else if (this.tbiManualAttn1.Visibility == Visibility.Visible)
                    this.tabPanel1.SelectedIndex = 2;
            }
        }

        private void ActivateAuthObjects()
        {

            try
            {

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAttn1_frmEntryAttn102") == null)
                {
                    this.tbiMachineAttn1.Visibility = Visibility.Collapsed;
                    this.tbiMachineAttn1a.Visibility = Visibility.Collapsed;
                }
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryAttn1_frmEntryAttn103") == null)
                {
                    this.tbiManualAttn1.Visibility = Visibility.Collapsed;
                }

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCH-Attn-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }     
        private void tabPanel1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.tabPanel1.SelectedIndex < 0)
                return;

            int TabIndex1 = this.tabPanel1.SelectedIndex;
            this.ShowTabInfo(TabIndex1);
        }
        private void ShowTabInfo(int TabIndex1)
        {
            if (TabIndex1 == 0 && this.stpkTab0.Children.Count == 0)
                this.stpkTab0.Children.Add(new frmEntryAttn102());

            else if (TabIndex1 == 2 && this.stpkTab1.Children.Count == 0)
                this.stpkTab1.Children.Add(new frmEntryAttn103());

            this.tabPanel1.SelectedItem = this.tabPanel1.Items[TabIndex1];
            this.lblTitle1.Content = ((TabItem)this.tabPanel1.SelectedItem).Tag.ToString();
        }

       
    }
}
