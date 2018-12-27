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
    /// Interaction logic for HmsChildWindow.xaml
    /// </summary>
    public partial class HmsChildWindow : Window
    {
        private double XpValue = 0;
        private double YpValue = 0;
        private UserControl _uc1 = null;
        private string frmTag = "Nothing";
        private string frmTag2 = "Nothing";
        public HmsChildWindow()
        {
            InitializeComponent();
        }
        public HmsChildWindow(UserControl uc1 = null, string FrmTag = "", string FrmTag2 = "")
        {
            InitializeComponent();
            this._uc1 = uc1;
            this.frmTag = FrmTag;
            this.frmTag2 = FrmTag2;
            this.Title = (uc1.Tag == null ? "" : uc1.Tag.ToString());
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
                this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;  // System.Windows.SystemParameters.VirtualScreenHeight;
                this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;    //System.Windows.SystemParameters.VirtualScreenWidth;

                //this.Height = System.Windows.SystemParameters.VirtualScreenHeight;
                //this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.Height = this.Owner.Height - this.Owner.Height * 0.25; // 130;
                this.Width = this.Owner.Width - this.Owner.Width * 0.25; // 80;
                this.Top = this.Owner.Height * 0.25 / 4 + 90;
                this.Left = this.Owner.Width * 0.25 / 2; // 40;
            }
            //if (this.WindowState == WindowState.Maximized)
            //{
            //    double sw1 = System.Windows.SystemParameters.VirtualScreenWidth;
            //    double sh1 = System.Windows.SystemParameters.VirtualScreenHeight;
            //    this.ResizeZoom(sw1 / this.XpValue, sh1 / this.YpValue);
            //}

            //this.ResizeZoom(this.Width / this.XpValue, this.Height / this.YpValue);
        }
    }
}
