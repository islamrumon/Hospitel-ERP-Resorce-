using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmConfigSetup1.xaml
    /// </summary>
    public partial class frmConfigSetup1 : UserControl
    {
        string TitaleTag1, TitaleTag2;  // 
        public frmConfigSetup1()
        {
            InitializeComponent();
        }


        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            TitaleTag1 = this.Tag.ToString();   // Predefined value of Tag property set at design time
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
            try
            {
                TitaleTag2 = this.Tag.ToString(); // Dynamic value of Tag property set at design time
                this.ActivateAuthObjects();
                if (this.tbiUserConfig1.Visibility == Visibility.Collapsed && this.tbiCompanyInfo1.Visibility == Visibility.Collapsed
                    && this.tbiBranchInfo1.Visibility == Visibility.Collapsed && this.tbiTerminalInfo1.Visibility == Visibility.Collapsed
                    && this.tbiDatabaseInfo1.Visibility == Visibility.Collapsed)
                {
                    this.tabPanel1.Visibility = Visibility.Hidden;
                    this.lblTitle1.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (this.tbiUserConfig1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 0;
                    else if (this.tbiCompanyInfo1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 2;
                    else if (this.tbiBranchInfo1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 4;
                    else if (this.tbiTerminalInfo1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 6;
                    else if (this.tbiDatabaseInfo1.Visibility == Visibility.Visible)
                        this.tabPanel1.SelectedIndex = 8;
                }

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Config-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ActivateAuthObjects()
        {

            try
            {
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmConfigSetup1_frmConfigSetup101") == null)
                {
                    this.tbiUserConfig1.Visibility = Visibility.Collapsed;
                    this.tbiUserConfig1a.Visibility = Visibility.Collapsed;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmConfigSetup1_frmConfigSetup102") == null)
                {
                    this.tbiCompanyInfo1.Visibility = Visibility.Collapsed;
                    this.tbiCompanyInfo1a.Visibility = Visibility.Collapsed;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmConfigSetup1_frmConfigSetup103") == null)
                {
                    this.tbiBranchInfo1.Visibility = Visibility.Collapsed;
                    this.tbiBranchInfo1a.Visibility = Visibility.Collapsed;
                }


                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmConfigSetup1_frmConfigSetup104") == null)
                {
                    this.tbiTerminalInfo1.Visibility = Visibility.Collapsed;
                    this.tbiTerminalInfo1a.Visibility = Visibility.Collapsed;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmConfigSetup1_frmConfigSetup105") == null)
                {
                    this.tbiDatabaseInfo1.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Config-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
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
                System.Windows.MessageBox.Show("Config-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void ShowTabInfo(int TabIndex1)
        {
            try
            {
                if (((TabItem)this.tabPanel1.Items[TabIndex1]).Visibility != Visibility.Visible)
                    return;

                if (TabIndex1 == 0 && this.stpkTab0.Children.Count == 0)
                    this.stpkTab0.Children.Add(new frmConfigSetup101());

                else if (TabIndex1 == 2 && this.stpkTab1.Children.Count == 0)
                    this.stpkTab1.Children.Add(new frmConfigSetup102());

                else if (TabIndex1 == 4 && this.stpkTab2.Children.Count == 0)
                    this.stpkTab2.Children.Add(new frmConfigSetup103());

                else if (TabIndex1 == 6 && this.stpkTab3.Children.Count == 0)
                    this.stpkTab3.Children.Add(new frmConfigSetup104());

                else if (TabIndex1 == 8 && this.stpkTab4.Children.Count == 0)
                    this.stpkTab4.Children.Add(new frmConfigSetup105());

                this.tabPanel1.SelectedItem = this.tabPanel1.Items[TabIndex1];
                this.lblTitle1.Content = ((TabItem)this.tabPanel1.SelectedItem).Tag.ToString();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("Config-04: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
