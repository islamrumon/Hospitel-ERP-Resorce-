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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ASITHmsWpf
{
    /// <summary>
    /// Interaction logic for HmsSplashScreenWindow1.xaml
    /// </summary>
    public partial class HmsSplashWindow1 : Window, ISplashScreen
    {
        DispatcherTimer timer1 = new DispatcherTimer();

        public HmsSplashWindow1(int SplashTabIndex = 0)
        {
            InitializeComponent();
            this.tabSplash.SelectedIndex = SplashTabIndex;
            this.timer1.Interval = TimeSpan.FromSeconds(5);
            this.timer1.Tick += this.timer1_Tick;
            this.timer1.Start();
        }

        public void AddMessage(string message)
        {
            Dispatcher.Invoke((Action)delegate() { this.lblUpdateMessage.Content = message; });
        }

        public void LoadComplete()
        {
            Dispatcher.InvokeShutdown();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
    public interface ISplashScreen
    {
        void AddMessage(string message);
        void LoadComplete();
    }
}
