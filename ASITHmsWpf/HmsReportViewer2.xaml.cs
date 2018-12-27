using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Common;
using System.Reflection;
using Microsoft.Reporting.WinForms;

namespace ASITHmsWpf
{
    /// <summary>
    /// Interaction logic for HmsReportViewer1.xaml
    /// </summary>
    public partial class HmsReportViewer2 : Window
    {
        public HmsReportViewer2(LocalReport Rpt1, string RptDisplayMode = "PrintLayout")
        {
            InitializeComponent();
            //this.rptViewer1.ProcessingMode = ProcessingMode.Local;
            var currentReportProperty = this.rptViewer1.GetType().GetProperty("CurrentReport", BindingFlags.NonPublic | BindingFlags.Instance);
            if (currentReportProperty != null)
            {
                var currentReport = currentReportProperty.GetValue(this.rptViewer1, null);
                var localReportField = currentReport.GetType().GetField("m_localReport", BindingFlags.NonPublic | BindingFlags.Instance);
                if (localReportField != null)
                {
                    localReportField.SetValue(currentReport, Rpt1);
                }
            }

            //double ScreenWidth1 = System.Windows.SystemParameters.VirtualScreenWidth;
            //double ScreenHeight1 = System.Windows.SystemParameters.VirtualScreenHeight;

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenhight = System.Windows.SystemParameters.PrimaryScreenHeight;

            if (RptDisplayMode == "Normal")
                this.rptViewer1.SetDisplayMode(DisplayMode.Normal);
            else
                this.rptViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            this.rptViewer1.ZoomMode = ZoomMode.PageWidth;

            //this.rptViewer1.ZoomPercent = (screenWidth <1600 ? 100 : 150);
            //this.rptViewer1.ZoomMode = ZoomMode.Percent;
            this.rptViewer1.RefreshReport();      
        }
        public HmsReportViewer2()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.rptViewer1.RefreshReport();         
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //this.rptViewer1.LocalReport = null;
        }     

    }
}
