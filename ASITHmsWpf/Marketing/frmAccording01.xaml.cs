using ASITHmsWpf.General;
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
    /// Interaction logic for frmAccording01.xaml
    /// </summary>
    public partial class frmAccording01 : UserControl
    {
        public frmAccording01()
        {
            InitializeComponent();
        }

        private void acdPanel1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.acdPanel1.SelectedIndex < 0)
                    return;

                int tabIndex1 = this.acdPanel1.SelectedIndex;
                this.ShowTabInfo(tabIndex1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("According Test1 : " + ex.Message);
            }
        }

        private void ShowTabInfo(int tabIndex1)
        {
            try
            {
                if (((AccordionItem)this.acdPanel1.Items[tabIndex1]).Visibility != Visibility.Visible)
                    return;
                if (tabIndex1 == 0 && this.stpkAcrd0.Children.Count == 0)
                    this.stpkAcrd0.Children.Add(new frmAccCodeBook1());

                else if (tabIndex1 == 1 && this.stpkAcrd1.Children.Count == 0)
                    this.stpkAcrd1.Children.Add(new frmSirCodeBook1());

                else if (tabIndex1 == 2 && this.stpkAcrd2.Children.Count == 0)
                    this.stpkAcrd2.Children.Add(new frmSectCodeBook1());
            }
            catch (Exception ex)
            {
                MessageBox.Show("According Test2 : " + ex.Message);
            }
        }


    }
}


