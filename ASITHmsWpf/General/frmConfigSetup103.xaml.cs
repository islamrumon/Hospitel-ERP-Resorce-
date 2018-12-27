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
using System.ComponentModel;
using System.IO;
using Microsoft.Win32;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt1GenAcc.General;
using System.Drawing;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmConfigSetup103.xaml
    /// </summary>
    public partial class frmConfigSetup103 : UserControl
    {

        private bool FrmInitialized = false;

        public frmConfigSetup103()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;

                var brncList = WpfProcessAccess.CompInfList[0].BranchList;
                var SectionList = WpfProcessAccess.CompInfList[0].SectionList;

                int i = 0;
                foreach (var item1 in brncList)
                {
                    this.cmbbrnch.Items.Add(new ComboBoxItem() { Content = item1.brnnam, Tag = item1.brncod });
                }
            }
        }
        private void BtnUpdateBranchInfo_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void Printinfo_OnClick(object sender, RoutedEventArgs e)
        {
            LocalReport rpt1 = new LocalReport();
            var list3 = WpfProcessAccess.GetRptGenInfo();
            var list1 = WpfProcessAccess.CompInfList;
            rpt1 = GeneralReportSetup.GetLocalReport("General.RptComSections1", list1, null, list3);
            string WindowTitle1 = "Company Info";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: "PrintLayout");
        }

        private void txtImage_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBoxNam = (TextBox)sender;
            this.showImage(txtBoxNam);
        }

        private void btnUpdateLogo_Click(object sender, RoutedEventArgs e)
        {
            string BtnSender = ((Button)sender).Name.ToString().Trim();
            this.Imgfileopen(BtnSender);
        }
        public void Imgfileopen(string btnName)
        {
            long threshold = 140000L;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";


            if (openFileDialog.ShowDialog() == true)
            {
                var size = new FileInfo(openFileDialog.FileName).Length;
                if (size <= threshold)
                {
                    switch (btnName)
                    {                        
                        case "btnBrnLabel":
                            txtbrnLabel.Text = openFileDialog.FileName;
                            break;
                        case "btnUpBrnLogo":
                            txtbrnLogo.Text = openFileDialog.FileName;
                            break;
                    }
                }
                else
                {
                    switch (btnName)
                    {                        
                        case "btnBrnLabel":
                            txtbrnLabel.Text = "File size > 140Kb";
                            break;
                        case "btnUpBrnLogo":
                            txtbrnLogo.Text = "File size > 140Kb";
                            break;
                    }
                }
            }
        }
        public void showImage(TextBox txtName)
        {
            string txtSender = txtName.Name.ToString().Trim();
            try
            {
                // image to byte Convert
                Bitmap bmp = new Bitmap(txtName.Text);
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                string image = Convert.ToBase64String((byte[])converter.ConvertTo(bmp, typeof(byte[])));

                // byte to image Convert
                byte[] bytes = Convert.FromBase64String(image);
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp2 = new BitmapImage();
                bmp2.BeginInit();
                bmp2.StreamSource = mem;
                bmp2.EndInit();
                //
                switch (txtSender)
                {                   
                    case "txtbrnLogo":
                        this.imgBrnLogo.Source = bmp2;
                        break;
                    case "txtbrnLabel":
                        this.imgbrnLabel.Source = bmp2;
                        break;
                }

            }
            catch
            {
                return;
            }
        }
    }
}
