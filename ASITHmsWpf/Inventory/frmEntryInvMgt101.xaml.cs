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
using ASITHmsEntity;
using System.Data;
using ASITHmsViewMan.Inventory;
using ASITFunLib;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt2Inventory;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using System.Drawing;

namespace ASITHmsWpf.Inventory
{
    /// <summary>
    /// Interaction logic for frmEntryInvMgt101.xaml
    /// </summary>
    public partial class frmEntryInvMgt101 : UserControl
    {
        private List<HmsEntityInventory.InvItemRateList> InvItemRateList = new List<HmsEntityInventory.InvItemRateList>();
        private List<HmsEntityInventory.InvItemRateList> InvPartItemRateList = new List<HmsEntityInventory.InvItemRateList>();

        private vmEntryInvMgt1 vm1 = new vmEntryInvMgt1();

        private int RateGrpIdx = 0;
        private string SirManinGroup = "";

        private bool FrmInitialized = false;

        public frmEntryInvMgt101()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }
        public frmEntryInvMgt101(string MainGroup = "")
        {
            InitializeComponent();
            
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            this.SirManinGroup = MainGroup;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;

                this.btnShowStdRate_Click(null, null);
            }
        }

        private void cmbItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.txtbItemSirCode.Text = "";
            this.txtbItemSirCode.Tag = "";
            if (this.cmbItemGroup.Items.Count == 0)
                return;

            this.btnShowStdRate_Click(null, null);
        }

        private void btnShowStdRate_Click(object sender, RoutedEventArgs e)
        {
            this.txtbItemSirCode.Text = "";
            this.txtbItemSirCode.Tag = "";
            this.AtxtItemCode.Text = "";
            this.txtinvCostRate.Text = "";
            this.txtinvSaleRate.Text = "";
            //this.AtxtItemCode.Value = "";
            this.lblUnit1.Content = "";

            if (this.cmbItemGroup.Items.Count == 0)
            {                
                var pap1 = vm1.SetParamInvItemRateList(WpfProcessAccess.CompInfList[0].comcpcod, "000000000000", this.SirManinGroup + "%");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                this.cmbItemGroup.Items.Clear();
                this.InvItemRateList.Clear();
                this.InvItemRateList = ds1.Tables[0].DataTableToList<HmsEntityInventory.InvItemRateList>();
                foreach (DataRow item in ds1.Tables[1].Rows)
                {
                    this.cmbItemGroup.Items.Add(new ComboBoxItem()
                    {
                        Content = item["sirtype"].ToString().Trim(),
                        Tag = item["itmgroup"].ToString().Trim() + item["sirtype"].ToString().Trim()
                    });
                }
                this.cmbItemGroup.SelectedIndex = RateGrpIdx;
            }

            this.InvPartItemRateList.Clear();
            string filterTag1 = ((ComboBoxItem)this.cmbItemGroup.SelectedItem).Tag.ToString().Trim();
            this.InvPartItemRateList = this.InvItemRateList.FindAll(x => x.rsircode.Substring(0, 9) + x.msirtype.Trim() == filterTag1);

            this.AtxtItemCode.Items.Clear();
            this.AtxtItemCode.AutoSuggestionList.Clear();
            foreach (var item1 in this.InvPartItemRateList)
            {
                string trdesc1 = item1.sirdesc.Trim() + " (" + item1.sirunit + "), Cost Price: " + item1.costprice.ToString("#,##0.00") + ", Sales Price: " + item1.saleprice.ToString("#,##0.00");
                this.AtxtItemCode.AddSuggstionItem(trdesc1, item1.rsircode.Trim());
            }
            this.dgRate1.ItemsSource = this.InvPartItemRateList;
        }



        private void btnPrintRate_Click(object sender, RoutedEventArgs e)
        {
            if (this.cmbItemGroup.Items.Count == 0)
                return;

            LocalReport rpt1 = new LocalReport();
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime:DateTime.Now);

            string filterTag1 = ((ComboBoxItem)this.cmbItemGroup.SelectedItem).Tag.ToString().Trim();
            var list1 = this.InvItemRateList.FindAll(x => x.rsircode.Substring(0, 7) + x.msirtype.Trim() == filterTag1 && (x.costprice > 0 || x.saleprice > 0));

            rpt1 = StoreReportSetup.GetLocalReport("InvMgt.RptItemRateList1", list1, null, list3);
            string WindowTitle1 = "Inventory Item Standard Rate Information";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void btnUpdateRate_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                            MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            long ItmCount1 = this.InvPartItemRateList.Count;

            // Update Records by spliting the List into multiple sub-list
            for (int i = 0; i < ItmCount1; i += 100) // Block Size 100
            {
                var InvPartItemRateList2 = this.InvPartItemRateList.Skip(i).Take(100).ToList();// sourceList.Skip(index).Take(itemsPerSet)

                DataSet ds1 = vm1.GetDataSetRateUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, sectcod1: "000000000000", RateList: InvPartItemRateList2);

                //String xx1 = ds1.GetXml().ToString();

                var pap1 = vm1.SetParamRateUpdate(WpfProcessAccess.CompInfList[0].comcod, ds1, "000000000000");
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if ((ds2 == null) || WpfProcessAccess.DatabaseErrorInfoList != null)
                {
                    System.Windows.MessageBox.Show("Could not updated information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }

            //DataSet ds1 = vm1.GetDataSetRateUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, sectcod1: "000000000000", RateList: this.InvPartItemRateList);

            ////String xx1 = ds1.GetXml().ToString();

            //var pap1 = vm1.SetParamRateUpdate(WpfProcessAccess.CompInfList[0].comcod, ds1, "000000000000");
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            //if ((ds2 == null) || WpfProcessAccess.DatabaseErrorInfoList != null)
            //{
            //    System.Windows.MessageBox.Show("Could not updated information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            //    return;
            //}

            System.Windows.MessageBox.Show("Information updated successfully", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            RateGrpIdx = this.cmbItemGroup.SelectedIndex;
            this.cmbItemGroup.Items.Clear();
            this.btnShowStdRate_Click(null, null);
        }

        private void AtxtItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.AtxtItemCode.Value.Length == 0)
                return;

            if (this.AtxtItemCode.Text.Trim().Length == 0)
                return;

            string rsircode1 = this.AtxtItemCode.Value;
            var lvi1 = this.InvPartItemRateList.Find(x => x.rsircode == AtxtItemCode.Value);
            this.lblUnit1.Content = lvi1.sirunit;
            this.txtinvCostRate.Text = lvi1.costprice.ToString("#,##0.00");
            this.txtinvSaleRate.Text = lvi1.saleprice.ToString("#,##0.00");

            var item22 = this.InvPartItemRateList.FindAll(x => x.rsircode == rsircode1);
            this.dgRate1.ScrollIntoView(item22[0]);
            this.dgRate1.SelectedItem = item22[0];


            //int z = 0;
            //foreach (var item3 in this.InvPartItemRateList)
            //{
            //    if (item3.rsircode == rsircode1)
            //        break;
            //    z++;
            //}

            //this.dgRate1.ScrollIntoView(this.InvPartItemRateList[z]);
            //this.dgRate1.SelectedIndex = z;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception exp1)
            {
                return;
            }
        }

        private void btnChangeRate_Click(object sender, RoutedEventArgs e)
        {
            if (this.AtxtItemCode.Value.Length == 0)
                return;

            if (this.AtxtItemCode.Text.Trim().Length == 0)
                return;

            decimal CostPrice1 = decimal.Parse("0" + this.txtinvCostRate.Text.Trim());
            decimal SalePrice1 = decimal.Parse("0" + this.txtinvSaleRate.Text.Trim());
            string rsircode1 = this.AtxtItemCode.Value;


            var item22 = this.InvPartItemRateList.FindAll(x => x.rsircode == rsircode1);
            item22[0].costprice = CostPrice1;
            item22[0].saleprice = SalePrice1;

            this.btnShowStdRate_Click(null, null);
            this.dgRate1.ScrollIntoView(item22[0]);
            this.dgRate1.SelectedItem = item22[0];

            //int z = 0;
            //foreach (var item3 in this.InvPartItemRateList)
            //{
            //    if (item3.rsircode == rsircode1)
            //    {
            //        item3.costprice = CostPrice1;
            //        item3.saleprice = SalePrice1;
            //        break;
            //    }
            //    z++;
            //}
            //this.btnShowStdRate_Click(null, null);
            //this.dgRate1.ScrollIntoView(this.InvPartItemRateList[z]);
            //this.dgRate1.SelectedIndex = z;
        }

        private void btnRemoveItemPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.chkShowImage.IsChecked = false;
                string sircode1 = this.txtbItemSirCode.Tag.ToString();
                //----------------------------
                string ImageFile1 = WpfProcessAccess.AppLocalImagePath + "L" + sircode1 + ".JPG";

                if (File.Exists(ImageFile1))
                {
                    File.Delete(ImageFile1);
                }
                else
                {
                    System.Windows.MessageBox.Show("No image file found to remove", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                //----------------------------


                // Remove Image file from database
                //string gencode1 = "SIGIIMG01001";

                //// string tblname1 = "SIRINF", string tblcode1 = "XXXXXXXXXXXX", string gencode1 = "SIGIIMG01001", string repeatsl = "1"
                //var pap1 = vm1.SetParamImageRemove(WpfProcessAccess.CompInfList[0].comcod, "SIRINF", sircode1, gencode1, "1");
                ////DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
                //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                //if ((ds2 == null) || WpfProcessAccess.DatabaseErrorInfoList != null)
                //{
                //    System.Windows.MessageBox.Show("Could not updated information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                //    return;
                //}

                this.imgItemPhoto.Source = null;
                this.txtImageTitle.Text = "";
                System.Windows.MessageBox.Show("Successfully updated information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("InvMgt101-01: " + exp.Message.ToString() + "\nAnother process is using this resource, can't remove image now.", 
                    WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                //System.Windows.MessageBox.Show("Another process is using this resource, can't remove image now.", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void btnUploadItemPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long threshold = 40000L;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Select a picture";
                openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";

                if (openFileDialog.ShowDialog() == true)
                {
                    var size = new FileInfo(openFileDialog.FileName).Length;    // "File size > 40Kb";
                    if (size <= threshold)
                    {
                        string txtSender = openFileDialog.FileName;
                        // image to byte Convert
                        Bitmap bmp = new Bitmap(txtSender);

                        //Bitmap bitmap2 = HmsImageManager.ResizeImaze(bmp, 320, 240);

                        TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                        //string image = Convert.ToBase64String((byte[])converter.ConvertTo(bitmap2, typeof(byte[])));
                        string image = Convert.ToBase64String((byte[])converter.ConvertTo(bmp, typeof(byte[])));

                        // byte to image Convert
                        byte[] bytes = Convert.FromBase64String(image);
                        MemoryStream mem = new MemoryStream(bytes);
                        BitmapImage bmp2 = new BitmapImage();
                        bmp2.BeginInit();
                        bmp2.StreamSource = mem;
                        bmp2.EndInit();

                        this.imgItemPhoto.Source = bmp2;
                    }
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("InvMgt101-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void btnSaveItemPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Convert image Source to byte[]
                string sircode1 = this.txtbItemSirCode.Tag.ToString();
                string gencode1 = "SIGIIMG01001";
                string imgtitle1 = this.txtImageTitle.Text.Trim();

                //byte[] pbytes = null;
                string pimage = "";
                var bmp1 = this.imgItemPhoto.Source as BitmapImage;
                if (bmp1 == null || sircode1.Length == 0)
                {
                    System.Windows.MessageBox.Show("Could not updated incomplete information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }


                if (bmp1 != null)
                {
                    //// Save Image to HDD Folder
                    string filePath = WpfProcessAccess.AppLocalImagePath + "L" + sircode1 + ".jpg";
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bmp1));

                    using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }

                    //// Save Image to Database
                    //--------------------------------------------------------
                    //MemoryStream outStream = new MemoryStream();
                    //BitmapEncoder enc = new BmpBitmapEncoder();
                    //enc.Frames.Add(BitmapFrame.Create(bmp1));
                    //enc.Save(outStream);

                    //System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap(outStream);
                    //TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                    //pimage = Convert.ToBase64String((byte[])converter.ConvertTo(bitmap1, typeof(byte[])));
                    //--------------------------------------------------------------------------------------------
                }
                System.Windows.MessageBox.Show("Successfully updated information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                #endregion

                //// Save Image Information to Database
                //--------------------------------------------------------
                //DataSet ds1 = vm1.GetDataSetImageUpdate(WpfProcessAccess.CompInfList[0].comcod, sircode1, gencode1, imgtitle1, pimage);

                //var pap1 = vm1.SetParamImageUpdate(WpfProcessAccess.CompInfList[0].comcod, ds1);            
                ////DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
                //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                //if ((ds2 == null) || WpfProcessAccess.DatabaseErrorInfoList != null)
                //{
                //    System.Windows.MessageBox.Show("Could not updated information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                //    return;
                //}
                //System.Windows.MessageBox.Show("Successfully updated information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                //--------------------------------------------------------

                /*
                 * INSERT INTO dbo.GENINF (COMCOD, TBLNAME, TBLCODE, GENCODE, REPEATSL, DATAVAL, BINDATA, ROWTIME) VALUES ('6501', 'SIRINF', 'SIGIIMG01001', '1', 'Image Title', pimage, GETDATE())
                 *  
                 *UPDATE [dbo].[GENINF] SET  [COMCOD] = '6501',  [TBLNAME] = 'SIRINF', [TBLCODE] = '4171xxxxxxx', [GENCODE] = 'SIGIIMG01001', [REPEATSL] = '1', [DATAVAL] = 'Image Title', [BINDATA] = pimage (the Image) WHERE COMCOD='6501' AND 
                 * 
                 SIGIIMG01001	DETAIL CODE IMAGE  - 001	P	Detail Code Image - 001
                    SIGIIMG01002	DETAIL CODE IMAGE  - 002	P	Detail Code Image - 002
                    SIGIIMG01003	DETAIL CODE IMAGE  - 003	P	Detail Code Image - 003
                    SIGIIMG01004	DETAIL CODE IMAGE  - 004	P	Detail Code Image - 004
                    SIGIIMG01005	DETAIL CODE IMAGE  - 005	P	Detail Code Image - 005
                 */

                //Pat1["PATPHOTO"] = pimage;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("InvMgt101-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void dgRate1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void dgRate1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.txtbItemSirCode.Text = "";
            this.txtbItemSirCode.Tag = "";
            this.imgItemPhoto.Source = null;
            this.txtImageTitle.Text = "";

            if (this.dgRate1.SelectedItem == null)
                return;

            var item1 = ((HmsEntityInventory.InvItemRateList)this.dgRate1.SelectedItem);
            this.txtbItemSirCode.Text = item1.rsircode + "\n" + item1.sirdesc.Trim();// .SelectedIndex.ToString();
            this.txtbItemSirCode.Tag = item1.rsircode;
            if (this.chkShowImage.IsChecked == true)
                this.ShowItemImage();
        }

        private void chkShowImage_Click(object sender, RoutedEventArgs e)
        {
            var chk1 = (CheckBox)sender;
            this.imgItemPhoto.Source = null;
            if (chk1 != null)
            {
                if (chk1.IsChecked == true)
                    this.ShowItemImage();
            }
        }
        private void ShowItemImage()
        {
            string sircode1 = this.txtbItemSirCode.Tag.ToString();


            string filname1 = WpfProcessAccess.AppLocalImagePath + @"L" + sircode1 + ".jpg";// @"C:\ASIT_Image\L_Image\L" + sircode1a + ".jpg";
            if (File.Exists(filname1))
            {
                //this.imgItemPhoto.Source = new BitmapImage(new System.Uri(filname1));
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(filname1);
                image.EndInit();
                this.imgItemPhoto.Source = image;
            }

            //    filname1 = WpfProcessAccess.AppLocalImagePath + @"FoodShop1.jpg";
            //BitmapImage bbb1 = new BitmapImage(new System.Uri(filname1));
            //return bbb1;




            //var pap1 = vm1.SetParamImageRetrive(WpfProcessAccess.CompInfList[0].comcod, sircode1, "SIGIIMG01001");
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            //if ((ds2 == null) || WpfProcessAccess.DatabaseErrorInfoList != null)
            //    return;

            //if (ds2.Tables[0].Rows.Count == 0)
            //    return;

            //this.txtImageTitle.Text = ds2.Tables[0].Rows[0]["dataval"].ToString().Trim();
            //if (!(ds2.Tables[0].Rows[0]["bindata"] is DBNull))
            //{
            //    byte[] byteSi = (byte[])ds2.Tables[0].Rows[0]["bindata"];
            //    MemoryStream mem1 = new MemoryStream(byteSi);
            //    if (mem1.Length > 0)
            //    {
            //        BitmapImage bmp4 = new BitmapImage();
            //        bmp4.BeginInit();
            //        bmp4.StreamSource = mem1;
            //        bmp4.EndInit();
            //        this.imgItemPhoto.Source = bmp4;
            //    }
            //}
        }
    }
}
