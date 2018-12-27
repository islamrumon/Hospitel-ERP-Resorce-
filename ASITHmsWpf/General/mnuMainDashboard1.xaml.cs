using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for mnuMainDashboard1.xaml
    /// </summary>
    public partial class mnuMainDashboard1 : UserControl
    {
        private bool FrmInitialized = false;
       // private List<TreeViewItem> tvItemList = new List<TreeViewItem>();
        public mnuMainDashboard1()
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
                //this.tvMenu1.Height = this.Height;
            }
        }

    

        private void btnTvManage_Click(object sender, RoutedEventArgs e)
        {
            string btnName = ((Button)sender).Name.ToString();
            switch(btnName)
            {
                case "btnTvExpand":
                    foreach (TreeViewItem tvi1 in this.tvMenu1.Items)
                    {
                        foreach (TreeViewItem tvi2 in tvi1.Items)
                            tvi2.IsExpanded = true;

                        tvi1.IsExpanded = true;
                    }
                    break;
                case "btnTvCollapsed":
                    foreach (TreeViewItem tvi1 in this.tvMenu1.Items)
                    {
                        foreach (TreeViewItem tvi2 in tvi1.Items)
                            tvi2.IsExpanded = false;

                        tvi1.IsExpanded = false;
                    }
                    break;
                case "btnTvFind":
                    break;

            }
        }

        private void frmDashboardG1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string ucTag = ((Control)sender).Tag.ToString();
            HmsMainWindow win01 = new HmsMainWindow();
            HmsChildWindow win02 = new HmsChildWindow();
            var wHeight = win01.Height;
            var wWidth = win01.Width;
            var wTop = win01.Top;
            var wLeft = win01.Left;

            var HideLegendStyle = new Style(typeof(Legend));
            HideLegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 80.0));
            HideLegendStyle.Setters.Add(new Setter(Legend.MarginProperty, new Thickness(5, 0, 0, 0)));
            HideLegendStyle.Setters.Add(new Setter(Legend.MinHeightProperty, 25.0));

            UserControl uc1 = null;
            switch (ucTag)
            {
                case "G1":
                case "G7":
                    var uc01 = new Marketing.frmDashboardG1();
                    uc01.dgvChart.Visibility = Visibility.Visible;
                    uc01.dgvChart.Width = wWidth - wWidth * 0.5;

                    uc01.dgvChart.Height = 150;
                    uc01.dgvChart.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                    uc01.chrtLine.LegendStyle = HideLegendStyle;
                    uc01.chrtLine.Height = wHeight - wHeight * 0.55;
                    uc01.chrtLine.Width = wWidth - wWidth * 0.30;
                    uc01.chrtLine.BorderThickness = new Thickness(0);

                    uc01.txtTitle1.Visibility = Visibility.Visible;
                    uc01.txtTitle1.Width = wWidth - wWidth * 0.5;
                    uc1 = uc01;
                    break;
                case "G2":
                case "G8":
                    var uc02 = new Marketing.frmDashboardG2();
                    uc02.dgvChart.Visibility = Visibility.Visible;
                    uc02.dgvChart.Width = wWidth - wWidth * 0.5;

                    uc02.dgvChart.Height = 150;
                    uc02.dgvChart.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                    uc02.chrtLine.LegendStyle = HideLegendStyle;
                    uc02.chrtLine.Height = wHeight - wHeight * 0.55;
                    uc02.chrtLine.Width = wWidth - wWidth * 0.30;
                    uc02.chrtLine.BorderThickness = new Thickness(0);

                    uc02.txtTitle1.Visibility = Visibility.Visible;
                    uc02.txtTitle1.Width = wWidth - wWidth * 0.5;
                    uc1 = uc02;
                    break;
                case "G3":
                case "G9":
                    var uc03 = new Marketing.frmDashboardG3();
                    uc03.dgvChart.Visibility = Visibility.Visible;
                    uc03.dgvChart.Width = wWidth - wWidth * 0.5;

                    uc03.dgvChart.Height = 150;
                    uc03.dgvChart.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                    uc03.chrtColumnS.LegendStyle = HideLegendStyle;
                    uc03.chrtColumnS.Height = wHeight - wHeight * 0.55;
                    uc03.chrtColumnS.Width = wWidth - wWidth * 0.30;
                    uc03.chrtColumnS.BorderThickness = new Thickness(0);

                    uc03.txtTitle1.Visibility = Visibility.Visible;
                    uc03.txtTitle1.Width = wWidth - wWidth * 0.5;
                    uc1 = uc03;
                    break;
                case "G4":
                case "G10":
                    var uc04 = new Marketing.frmDashboardG4();
                    uc04.dgvChart.Visibility = Visibility.Visible;
                    uc04.dgvChart.Width = wWidth - wWidth * 0.5;

                    uc04.dgvChart.Height = 150;
                    uc04.dgvChart.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                    uc04.chrtBarS.LegendStyle = HideLegendStyle;
                    uc04.chrtBarS.Height = wHeight - wHeight * 0.55;
                    uc04.chrtBarS.Width = wWidth - wWidth * 0.30;
                    uc04.chrtBarS.BorderThickness = new Thickness(0);

                    uc04.txtTitle1.Visibility = Visibility.Visible;
                    uc04.txtTitle1.Width = wWidth - wWidth * 0.5;
                    uc1 = uc04;
                    break;
                case "G5":
                case "G11":
                    var uc05 = new Marketing.frmDashboardG5();
                    uc05.dgvChart.Visibility = Visibility.Visible;
                    uc05.dgvChart.Width = wWidth - wWidth * 0.5;

                    uc05.dgvChart.Height = 150;
                    uc05.dgvChart.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                    uc05.chrtColumnS.LegendStyle = HideLegendStyle;
                    uc05.chrtColumnS.Height = wHeight - wHeight * 0.55;
                    uc05.chrtColumnS.Width = wWidth - wWidth * 0.30;
                    uc05.chrtColumnS.BorderThickness = new Thickness(0);

                    uc05.txtTitle1.Visibility = Visibility.Visible;
                    uc05.txtTitle1.Width = wWidth - wWidth * 0.5;
                    uc1 = uc05;
                    break;
                case "G6":
                case "G12":
                    var uc06 = new Marketing.frmDashboardG6();
                    uc06.dgvChart.Visibility = Visibility.Visible;
                    uc06.dgvChart.Width = wWidth - wWidth * 0.5;

                    uc06.dgvChart.Height = 150;
                    uc06.dgvChart.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                    uc06.chrtColumnS.LegendStyle = HideLegendStyle;
                    uc06.chrtColumnS.Height = wHeight - wHeight * 0.55;
                    uc06.chrtColumnS.Width = wWidth - wWidth * 0.30;
                    uc06.chrtColumnS.BorderThickness = new Thickness(0);

                    uc06.txtTitle1.Visibility = Visibility.Visible;
                    uc06.txtTitle1.Width = wWidth - wWidth * 0.5;
                    uc1 = uc06;
                    break;
            }
            if (uc1 == null)
                return;

            HmsDialogWindow1 win1 = new HmsDialogWindow1(uc1);// { Owner = this };    
            //win1.WindowState = WindowState.Maximized;
            ////win1.Top = wHeight;
            ////win1.Left = wWidth * -1;
            ////win1.Height = wHeight - wHeight * 0.25;// 130;
            ////win1.Width = wWidth - wWidth * 0.25;// 80;
            ////var Top1 = -8.0 + wHeight * 0.25 / 4 + 90;
            ////var Left1 = -8.0 + wWidth * 0.25 / 2;

            ////var sb = new Storyboard();
            ////var moveX = new DoubleAnimation(Left1, new Duration(TimeSpan.FromSeconds(2)));
            ////Storyboard.SetTarget(moveX, win1);
            ////Storyboard.SetTargetProperty(moveX, new PropertyPath("(Canvas.Left)"));
            ////sb.Children.Add(moveX);

            ////var moveY = new DoubleAnimation(Top1, new Duration(TimeSpan.FromSeconds(2)));
            ////Storyboard.SetTarget(moveY, win1);
            ////Storyboard.SetTargetProperty(moveY, new PropertyPath("(Canvas.Top)"));
            ////sb.Children.Add(moveY);

            ////var fade = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(2) };
            ////Storyboard.SetTarget(fade, win1);
            ////Storyboard.SetTargetProperty(fade, new PropertyPath(Grid.OpacityProperty));
            ////sb.Children.Add(fade);
            ////sb.Begin();

            win1.ShowDialog();
        }

        private void chkDashboardItems_Click(object sender, RoutedEventArgs e)
        {
            this.scrlvGrapg.Visibility = (this.chkDashboardItems.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
        }  

       
    }
}
