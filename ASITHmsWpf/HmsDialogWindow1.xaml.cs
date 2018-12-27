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
using System.Windows.Shapes;

namespace ASITHmsWpf
{
    /// <summary>
    /// Interaction logic for HmsDialogWindow1.xaml
    /// </summary>
    public partial class HmsDialogWindow1 : Window
    {
        private double XpValue = 0;
        private double YpValue = 0;
        private UserControl _uc1 = null;
        public HmsDialogWindow1(UserControl uc1)
        {
            InitializeComponent();
            _uc1 = uc1;
        }
        public HmsDialogWindow1()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (_uc1 == null)
                return;

            int i = this.UcGrid1.Children.Count;
            this.UcGrid1.Children.Insert(i, _uc1);
            this.UcGrid1.Visibility = Visibility.Visible;
            this.WindowState = System.Windows.WindowState.Normal;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (XpValue == 0)
            {
                this.XpValue = this.Width;
                this.YpValue = this.Height;
            }

            this.ResizeZoom(this.Width / this.XpValue, this.Height / this.YpValue);
        }
        private void ResizeZoom(double XScaleValue, double YScaleValue)
        {
            ScaleTransform scaler = this.UcGrid1.LayoutTransform as ScaleTransform;
            if (scaler == null)
            {
                this.UcGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
            }
            else if (scaler.HasAnimatedProperties)
            {
                // Do nothing because the value is being changed by animation.
                // Setting scaler.ScaleX will cause infinite recursion due to the
                // binding specified in the XAML.
            }
            else
            {
                scaler.ScaleX = XScaleValue;
                scaler.ScaleY = YScaleValue;
            }
            //this.slider1.ToolTip = (slider1.Value).ToString("##0%");
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;  // .VirtualScreenHeight;
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;    // .VirtualScreenWidth;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.Height = 650;
                this.Width = 1200;
                this.Top = (System.Windows.SystemParameters.PrimaryScreenHeight - 650) / 2;
                this.Left = (System.Windows.SystemParameters.PrimaryScreenWidth - 1200) / 2;
            }
        }

    }
}
