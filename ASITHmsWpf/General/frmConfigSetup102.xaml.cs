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
using System.Drawing;
using System.ComponentModel;
using System.Data;
using ASITHmsViewMan.General;
using System.IO;
using Microsoft.Win32;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmConfigSetup102.xaml
    /// </summary>
    public partial class frmConfigSetup102 : UserControl
    {
        private vmConfigSetup1 vm1 = new vmConfigSetup1();
        private bool FrmInitialized = false;

        public frmConfigSetup102()
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
                var cmpinfoList = WpfProcessAccess.CompInfList[0];
                this.cmpname.Content = cmpinfoList.comnam;
                this.cmpAddrsee.Content = cmpinfoList.comadd1;
                this.cmpPhoneno.Content = cmpinfoList.comadd3;
                this.cmpEmail.Content = cmpinfoList.comadd4;
                if ((cmpinfoList.comlogo != null))
                {
                    byte[] bytes = cmpinfoList.comlogo;
                    MemoryStream mem = new MemoryStream(bytes);
                    BitmapImage bmp3 = new BitmapImage();
                    bmp3.BeginInit();
                    bmp3.StreamSource = mem;
                    bmp3.EndInit();
                    this.imgLogo.Source = bmp3;
                }

                if ((cmpinfoList.comlabel != null))
                {
                    byte[] bytes = cmpinfoList.comlabel;
                    MemoryStream mem = new MemoryStream(bytes);
                    BitmapImage bmp3 = new BitmapImage();
                    bmp3.BeginInit();
                    bmp3.StreamSource = mem;
                    bmp3.EndInit();
                    this.imgLabel.Source = bmp3;
                }
            }
        }
        
        private void btnUpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
          MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }

            #region Update Compnay Information


            #region Convert image Source to byte[]

            byte[] Logobytes = null;
            if (txtLogo.Text.Length != 0 && txtLogo.Text != "File size > 140Kb")
            {
                Bitmap bmp = new Bitmap(txtLogo.Text);
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                string image = Convert.ToBase64String((byte[])converter.ConvertTo(bmp, typeof(byte[])));

                // byte to image Convert
                Logobytes = Convert.FromBase64String(image);
            }

            byte[] LabelSbytes = null;
            if (txtLabel.Text.Length != 0 && txtLabel.Text != "File size > 140Kb")
            {
                Bitmap bmp1 = new Bitmap(txtLabel.Text);
                TypeConverter converter1 = TypeDescriptor.GetConverter(typeof(Bitmap));
                string image1 = Convert.ToBase64String((byte[])converter1.ConvertTo(bmp1, typeof(byte[])));

                // byte to image Convert
                LabelSbytes = Convert.FromBase64String(image1);
            }

            #endregion Convert image Source to byte[]



            DataSet ds1 = vm1.GetDsCompLogoLabelUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, _comlogo: Logobytes, _comlabel: LabelSbytes);

            //String xx1 = ds1.GetXml().ToString();
            string UpType = (chkLogo.IsChecked == true && chkLabel.IsChecked == true)
                ? "LOGOLABEL"
                : (chkLogo.IsChecked == true && chkLabel.IsChecked == false)
                    ? "LOGO"
                    : (chkLogo.IsChecked == false && chkLabel.IsChecked == true) ? "LABEL" : "LOGOLABEL";

            //if (chkLogo.IsChecked == true && chkLabel.IsChecked==true)
            //{
            //    UpType = "LOGOLABEL";
            //}
            //else
            //{
            //    if (chkLogo.IsChecked == true)
            //    {
            //        UpType = "LOGO";
            //    }
            //    else
            //    {
            //        UpType = "LABEL";
            //    }
            //}

            var pap1 = vm1.SetParamUpdateCompLogoLabel(WpfProcessAccess.CompInfList[0].comcod, ds1, UpType: UpType);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;
            MessageBox.Show("Update SuccessFull!!");
            #endregion // Update values into basicinfo list
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
                        case "btnUpdateLogo":
                            txtLogo.Text = openFileDialog.FileName;
                            break;
                        case "btnUpdateLabel":
                            txtLabel.Text = openFileDialog.FileName;
                            break;                    
                    }
                }
                else
                {
                    switch (btnName)
                    {
                        case "btnUpdateLogo":
                            txtLogo.Text = "File size > 140Kb";
                            break;
                        case "btnUpdateLabel":
                            txtLabel.Text = "File size > 140Kb";
                            break;                       
                    }
                }
            }
        }

        private void txtImage_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBoxNam = (TextBox)sender;
            this.showImage(txtBoxNam);
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
                    case "txtLogo":
                        this.imgLogo.Source = bmp2;
                        break;
                    case "txtLabel":
                        this.imgLabel.Source = bmp2;
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
