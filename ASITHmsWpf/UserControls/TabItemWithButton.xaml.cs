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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASITHmsWpf.UserControls
{
    /// <summary>
    /// Interaction logic for TabItemWithButton.xaml
    /// </summary>
    public partial class TabItemWithButton : TabItem
    {
        private bool objInitialized = false;
        private string header1 { get; set; }
        public UserControl uc1 { get; set; }
        private string tag1 { get; set; }
        private string ucName1 { get; set; }
        public TabItemWithButton()
        {
            InitializeComponent();
        }
        public TabItemWithButton(string _header1 = "0", string _userControl1 = "Nothing", UserControl _uc1 = null)
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            this.header1 =(_header1.ToString().Contains("-") ? _header1 : "Tab-" + _header1);
            this.tag1 = _header1.ToString();
            this.ucName1 = _userControl1;
            this.uc1 = _uc1;
        }

        private void TabItem_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (this.objInitialized == false)
            {
                this.txtb1.Text = this.header1;
                this.btn1.Tag = this.tag1;
                this.Tag = this.tag1;
              
                this.objInitialized = true;
                if (this.uc1 == null)
                    this.stkp1.Children.Add(WpfProcessAccess.CreateUserControl(this.ucName1));
                else
                {
                    this.stkp1.Children.Add(this.uc1);
                    if (this.uc1.Tag != null)
                    {
                        this.txtb1.ToolTip = this.uc1.Tag.ToString();
                        //this.ToolTip = this.uc1.Tag.ToString();
                    }
                }          
            }
        }
    }
}
