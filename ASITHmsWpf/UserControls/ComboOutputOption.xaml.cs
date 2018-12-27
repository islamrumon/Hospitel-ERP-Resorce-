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

namespace ASITHmsWpf.UserControls
{
    /// <summary>
    /// Interaction logic for ComboOutputOption.xaml
    /// </summary>
    public partial class ComboOutputOption : ComboBox
    {
        public ComboOutputOption()
        {
            InitializeComponent();

        }

        private void ComboBox_Initialized(object sender, EventArgs e)
        {
            ContextMenu ctm1 = new ContextMenu();
            int i = 0;
            foreach (ComboBoxItem item in this.Items)
            {
                MenuItem mi1 = new MenuItem() { Header = item.Content, Tag = item.Tag, Uid = i.ToString() };
                mi1.Click += mi1_Click;
                ctm1.Items.Add(mi1);
                i++;
            }
            this.ContextMenu = ctm1;
        }

        private void mi1_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedIndex = int.Parse(((MenuItem)sender).Uid.ToString());
            this.Uid = ((MenuItem)sender).Tag.ToString();
        }

        public void ComboBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            this.Uid = "NONE";
        }

    }
}
