using ASITHmsEntity;
using ASITHmsRpt4Commercial;
using ASITHmsViewMan.Commercial;
using ASITFunLib;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit;
using ASITHmsViewMan.Manpower;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ASITHmsWpf.Commercial.FoodShop
{
    /// <summary>
    /// Interaction logic for frmEntryRestauPOS101.xaml
    /// </summary>
    public partial class frmEntryRestauPOS101 : UserControl
    {
        private int PrintCount = 0;
        private bool FrmInitialized = false;
        private List<vmEntryPharRestPOS1.RetSaleItemGroup> RetSaleItemGroupList = new List<vmEntryPharRestPOS1.RetSaleItemGroup>();
        private List<vmEntryPharRestPOS1.RetSaleItem> RetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();

        private List<vmEntryPharRestPOS1.RetSaleItem> ShortRetSaleItemList = new List<vmEntryPharRestPOS1.RetSaleItem>();

        private List<vmEntryPharRestPOS1.ListViewItemTable> ListViewItemTable1 = new List<vmEntryPharRestPOS1.ListViewItemTable>();
        private List<HmsEntityCommercial.InvoiceTransList> TransInvList = new List<HmsEntityCommercial.InvoiceTransList>();
        private bool MemoSaved = false;
        private vmEntryPharRestPOS1 vm1 = new vmEntryPharRestPOS1();
        private vmReportPharRestPOS1 vm2 = new vmReportPharRestPOS1();
        private DataSet EditDs;
        public frmEntryRestauPOS101()
        {
            InitializeComponent();


        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DesignerProperties.GetIsInDesignMode(this))
                    return;
                // Food Sales Entry 
                if (!this.FrmInitialized)
                {
                    this.ActivateAuthObjects();
                    this.Objects_On_Init();
                    this.FrmInitialized = true;
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void ActivateAuthObjects()
        {
            try
            {


                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryRestauPOS1_frmEntryRestauPOS101_chkPrintDirect") == null)
                    this.chkPrintDirect.IsEnabled = false; //this.chkPrintDirect.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryRestauPOS1_frmEntryRestauPOS101_chkPrevTransFilter") == null)
                    this.chkPrevTransFilter.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryRestauPOS1_frmEntryRestauPOS101_btnEdit1") == null)
                    this.btnEdit1.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryRestauPOS1_frmEntryRestauPOS101_chkDueList") == null)
                    this.chkDueList.Visibility = Visibility.Hidden;                   

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryRestauPOS1_frmEntryRestauPOS101_btnDelete1") == null)
                    this.btnDelete1.Visibility = Visibility.Hidden;

                if(this.chkDueList.Visibility == Visibility.Visible && this.btnEdit1.Visibility == Visibility.Hidden)
                {
                    this.btnEdit1.Visibility = Visibility.Visible;
                    this.chkDueList.IsChecked = true;
                    this.chkDueList.IsEnabled = false;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryRestauPOS1_frmEntryRestauPOS101_stkpPrevTrans") == null)
                {

                    this.stkpPrevTrans.Visibility = Visibility.Hidden;
                    this.chkPrevTransFilter.Visibility = Visibility.Hidden;
                    this.btnEdit1.Visibility = Visibility.Hidden;
                    this.btnDelete1.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void Objects_On_Init()
        {
            try
            {
                this.CleanUpScreen();

                this.GetSectionList();
                this.GetRetailItemList();
                this.dgvMemo.ItemsSource = this.ListViewItemTable1;
                this.xctk_dtpFromDate.Value = DateTime.Today.AddDays(-7);
                this.xctk_dtpToDate.Value = DateTime.Today;
                this.btnFilterPrevTrans_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void btnFilterPrevTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string FrmDate1 = this.xctk_dtpFromDate.Text;   // DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
                string ToDate1 = this.xctk_dtpToDate.Text;      // DateTime.Today.ToString("dd-MMM-yyyy");
                if (this.TransInvList == null)
                    return;

                this.TransInvList.Clear();
                this.TransInvList = this.PreviousMemoList(FrmDate1, ToDate1);
                if (this.TransInvList == null)
                    return;

                this.TransInvList = this.TransInvList.FindAll(x => x.invno.Substring(0, 3) == "FSI").ToList();
                if (this.TransInvList == null)
                    return;
                this.cmbPrevTransList.Items.Clear();
                foreach (var item1 in this.TransInvList)
                {
                    this.cmbPrevTransList.Items.Add(new ComboBoxItem()
                    {
                        Content = item1.invno1.Substring(3, 2) + "-" + item1.invno1.Substring(11, 5) + " [Tk. " + item1.billam.ToString("#,##0.00") +
                        (item1.dueam <= 0 ? "" : ", Due: Tk. " + item1.dueam.ToString("#,##0.00")) + ", " +
                        item1.invdat.ToString("dd.MM.yyyy") + "] " + (item1.invref.Trim().Length > 0 ? ", " + item1.invref.Trim() : "") +
                        (item1.invnar.Trim().Length > 0 ? ", " + item1.invnar.Trim() : ""),
                        Tag = item1.invno
                    });
                }
                if (this.cmbPrevTransList.Items.Count > 0)
                    this.cmbPrevTransList.SelectedIndex = 0;
                this.chkPrevTransFilter.IsChecked = false;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-04: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }

        private void GetSectionList()
        {
            try
            {
                this.cmbSectCod.Items.Clear();
                var deptList1 = WpfProcessAccess.CompInfList[0].SectionList.FindAll(x => x.sectcod.Substring(9, 3) != "000");
                foreach (var itemd1 in deptList1)
                {
                    if (itemd1.sectname.ToUpper().Contains("STORE"))
                    {
                        this.cmbSectCod.Items.Add(new ComboBoxItem() { Content = itemd1.sectname, Tag = itemd1.sectcod });
                    }
                }
                this.cmbSectCod.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-05: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }
        private void GetRetailItemList()
        {
            try
            {
                this.RetSaleItemList.Clear();
                var pap = vm1.SetParamRetSaleItemList(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, mitemgrp: "4171", reqmfginf: "WITHOUTMFGINFO");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap);
                if (ds1 == null)
                    return;

                this.RetSaleItemGroupList = ds1.Tables[1].DataTableToList<vmEntryPharRestPOS1.RetSaleItemGroup>();
                DataRow[] dr1 = ds1.Tables[0].Select();
                DataRow[] dr2 = ds1.Tables[1].Select();
                DataRow[] dr3 = ds1.Tables[2].Select();
                //for (int i = 0; i < dr1.Length; i++)
                foreach (DataRow row1 in dr1)
                {
                    var bimg1 = new BitmapImage();
                    //DataRow[] dr4 = ds1.Tables[3].Select("sircode = '" + row1["sircode"].ToString().Trim() + "'");
                    //if (dr4.Length > 0)
                    bimg1 = this.ConvertByteImage(row1);// (dr4[0]);
                    var itm1 = new vmEntryPharRestPOS1.RetSaleItem(row1["sircode"].ToString(), row1["sirdesc"].ToString(), Convert.ToDecimal(row1["costprice"]), Convert.ToDecimal(row1["saleprice"]), 
                               Convert.ToDecimal(row1["refscomp"]), Convert.ToDecimal(row1["salvatp"]), row1["sirtype"].ToString(), row1["sirunit"].ToString(), row1["sirunit2"].ToString(), 
                               row1["sirunit3"].ToString(), decimal.Parse("0" + row1["siruconf"].ToString()), decimal.Parse("0" + row1["siruconf3"].ToString()), row1["msircode"].ToString(),
                               row1["msirdesc"].ToString(), row1["msirdesc"].ToString().Trim() + " - " + row1["sirdesc"].ToString(), row1["sircode"].ToString().Substring(6), row1["mfgid"].ToString(), 
                               row1["mfgcomnam"].ToString(), "Collapsed", "Collapsed", bimg1);

                    this.RetSaleItemList.Add(itm1); //this.ConvertByteImage(dr2[i])));
                }
                this.RetSaleItemGroupList = ds1.Tables[1].DataTableToList<vmEntryPharRestPOS1.RetSaleItemGroup>();
                //for (int i = 0; i < dr2.Length; i++)
                foreach (DataRow row2 in dr2)
                {
                    this.cmbItemCat.Items.Add(new ComboBoxItem() { Content = row2["msirdesc"].ToString(), Tag = row2["msircode"].ToString() });
                }

                this.lstvRetailItem.ItemsSource = this.RetSaleItemList.Take(30);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-06: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private BitmapImage ConvertByteImage(DataRow dr1)
        {

            // For using local image
            try
            {

                string sircode1a = dr1["sircode"].ToString();//.Substring(6, 6);
                BitmapImage bbb1;
                string filname1 = WpfProcessAccess.AppLocalImagePath + @"L" + sircode1a + ".jpg";// @"C:\ASIT_Image\L_Image\L" + sircode1a + ".jpg";
                if (File.Exists(filname1))
                    bbb1 = new BitmapImage(new System.Uri(filname1));
                else
                {
                    filname1 = WpfProcessAccess.AppLocalImagePath + @"FoodShop1.jpg";
                    if (File.Exists(filname1))
                        bbb1 = new BitmapImage(new System.Uri(filname1));
                    else
                        bbb1 = new BitmapImage();
                }               
                return bbb1;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-07: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return null;
                //var img2 = new BitmapImage();
                //return img2;
            }


            //// For using database images
            //if (dr1["sirimage"] is DBNull)
            //{
            //    // Following statement munt not return null 
            //    //var img1 = new General.imgLib1().imgLogo1.Source as BitmapImage;

            //    //return img1;
            //    ///////
            //    return new BitmapImage();
            //}

            //byte[] byteSi = (byte[])dr1["sirimage"];
            //MemoryStream mem1 = new MemoryStream(byteSi);
            //BitmapImage bmp4 = new BitmapImage();
            //bmp4.BeginInit();
            //bmp4.StreamSource = mem1;
            //bmp4.EndInit();
            //return bmp4;
        }

        #region For Learning Resource

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //// Converting an Image Control to a Bitmap
            //RenderTargetBitmap rtBmp = new RenderTargetBitmap((int)ImgFood.ActualWidth, (int)ImgFood.ActualHeight,
            //    96.0, 96.0, PixelFormats.Pbgra32);

            //ImgFood.Measure(new System.Windows.Size((int)ImgFood.ActualWidth, (int)ImgFood.ActualHeight));
            //ImgFood.Arrange(new Rect(new System.Windows.Size((int)ImgFood.ActualWidth, (int)ImgFood.ActualHeight)));

            //rtBmp.Render(ImgFood);

            //PngBitmapEncoder encoder = new PngBitmapEncoder();
            //MemoryStream stream = new MemoryStream();
            //encoder.Frames.Add(BitmapFrame.Create(rtBmp));

            //// Save to memory stream and create Bitamp from stream
            //encoder.Save(stream);
            //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream);

            //// Demonstrate that we can do something with the Bitmap
            //bitmap.Save(@"D:\Temp\Life.png", ImageFormat.Png);

            // Optionally, if we didn't need Bitmap object, but
            // just wanted to render to file, we could:
            //encoder.Save(new FileStream(@"D:\Temp\Life-Other.png", FileMode.Create));


            //RenderTargetBitmap rtBmp = new RenderTargetBitmap((int)ImgFood.ActualWidth, (int)ImgFood.ActualHeight, 96.0, 96.0, PixelFormats.Pbgra32);

            //ImgFood.Measure(new System.Windows.Size((int)ImgFood.ActualWidth, (int)ImgFood.ActualHeight));
            //ImgFood.Arrange(new Rect(new System.Windows.Size((int)ImgFood.ActualWidth, (int)ImgFood.ActualHeight)));

            //rtBmp.Render(ImgFood);

            //PngBitmapEncoder encoder = new PngBitmapEncoder();
            //MemoryStream stream = new MemoryStream();
            //encoder.Frames.Add(BitmapFrame.Create(rtBmp));

            //// Save to memory stream and create Bitamp from stream
            //encoder.Save(stream);
            //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream);

            //using (var memory = new MemoryStream())
            //{
            //    bitmap.Save(memory, ImageFormat.Png);
            //    memory.Position = 0;

            //    var bitmapImage = new BitmapImage();
            //    bitmapImage.BeginInit();
            //    bitmapImage.StreamSource = memory;
            //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            //    bitmapImage.EndInit();

            //    return bitmapImage;
            //}


        }

        #endregion  // For Learning Resource
        private List<HmsEntityCommercial.InvoiceTransList> PreviousMemoList(string Date1, string Date2, string searchStr = "%")
        {
            try
            {
                if (this.cmbSectCod.Items.Count == 0)
                    return null;

                string sectcod1 = ((ComboBoxItem)this.cmbSectCod.Items[this.cmbSectCod.SelectedIndex]).Tag.ToString();
                var pap1 = vm2.SetParamSalesTransList(WpfProcessAccess.CompInfList[0].comcpcod, "A00MSISUM", Date1, Date2, sectcod1, "FSI");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return null;

                return ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-08: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return null;
            }
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }


        private void CleanUpScreen()
        {
            try
            {
                this.chkIntQtyOnly.IsChecked = true;
                this.PrintCount = 0;
                this.MemoSaved = false;
                this.txtContactNo.Text = "";
                this.autoItemSearch.SelectedValue = null;
                this.xcdtDeliveryDT.Value = DateTime.Parse(DateTime.Today.AddDays(0).ToString("dd-MMMM-yyyy") + " 07:00 PM");
                this.lblTransDate.Content = DateTime.Now.ToString("dd-MMM-yyyy");
                this.lblTransDate.Tag = DateTime.Now.ToString("dd-MMM-yyyy");
                this.lblGrandTotal.Content = " -  ";
                this.lblDiscTotal.Content = " -  ";
                this.lblNetTotal.Content = " -  ";
                this.lblChangeCash.Content = " -  ";
                this.dgvMemo.Items.Refresh();
                this.lblNetBalance.Content = " -  ";
                this.txtPaidAmt.Text = " 0  ";
                this.lblVATTotal.Content = " - ";
                this.lblTotalBill.Content = " - ";
                this.EditDs = null;
                this.chkDiscount.IsChecked = false;
                this.iudDisPer.Value = 0;
                this.iudDisPer.IsEnabled = false;
                this.btnSetDispPer.IsEnabled = false;
                this.btnUpdateTrans.IsEnabled = true;
                this.btnAddSearch.IsEnabled = true;
                this.btnPrintTrans.Visibility = Visibility.Hidden;
                //this.chkPrintDirect.Visibility = Visibility.Hidden;
                //this.chkConfrmSave.Visibility = Visibility.Hidden;
                this.btnUpdateTrans.Visibility = Visibility.Hidden;
                this.btnUpdateTrans.Tag = "New";
                this.ListViewItemTable1.Clear();
                this.dgvMemo.Items.Refresh();
                this.cmbPayType.SelectedIndex = 0;
                this.txtMemoNar.Text = "";
                string lastid1 = this.GetLastTransID();
                this.txtTransID.Text = (lastid1 == "FSI000000000000000" ? "" : "");
                this.txtTransID.Tag = lastid1;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-09: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private string GetLastTransID()
        {
            return "FSI000000000000000";
        }
        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string PayType1 = ((ComboBoxItem)this.cmbPayType.SelectedItem).Tag.ToString();
                string MemoNar1 = this.txtMemoNar.Text.Trim();
                string DueAmt1 = this.lblNetBalance.Content.ToString();
                string BillAmt1 = this.lblTotalBill.Content.ToString();
                decimal dueamt1 = Math.Round(decimal.Parse("0" + DueAmt1.Replace("(", "").Replace(")", "")), 0) * (DueAmt1.Contains("(") ? -1 : 1);
                decimal billamt1 = Math.Round(decimal.Parse("0" + BillAmt1.Replace("(", "").Replace(")", "")), 0) * (BillAmt1.Contains("(") ? -1 : 1);
                string PaidAmt1 = (billamt1 - dueamt1).ToString("#,##0.00");
                if ((PayType1 != "CASH" || Decimal.Parse("0" + DueAmt1) > 0) && MemoNar1.Length == 0)
                {
                    System.Windows.MessageBox.Show("Customer/Card reference is manadatory for non cash/due sale", WpfProcessAccess.AppTitle, MessageBoxButton.OK,
                        MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }


                if (this.chkConfrmSave.IsChecked == false)
                {
                    if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }

                string CustID1 = "000000000000";
                string EditTransID1 = (this.EditDs != null ? this.txtTransID.Tag.ToString() : "");

                string vouno1 = "000000000000000000"; // this.lblVouNo1.Tag.ToString().Trim();
                string vouno2 = "000000000000000000"; // this.lblVouNo2.Tag.ToString().Trim();
                string cbSectCode1 = ((ComboBoxItem)this.cmbSectCod.SelectedItem).Tag.ToString();
                DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.lblTransDate.Content.ToString().Substring(0, 11)), EditMemoNum1: EditTransID1,
                            mcode: "FSI", cbSectCode: cbSectCode1, CustID1: CustID1, InvByID1: WpfProcessAccess.SignedInUserList[0].hccode, PayType1: "CASH",
                            MemoRef1: this.txtContactNo.Text.Trim(), MemoRefDate1: DateTime.Parse(this.lblTransDate.Content.ToString().Substring(0, 11)), delivartime1: this.xcdtDeliveryDT.Text.Trim(),
                            MemoNar1: this.txtMemoNar.Text.Trim(), ListViewItemTable1a: this.ListViewItemTable1, PayType: PayType1, DueAmt: DueAmt1, PaidAmt: PaidAmt1,
                            vounum1: vouno1, vounum2: vouno2,
                            _preparebyid: WpfProcessAccess.SignedInUserList[0].hccode, InvStatus: "A", _prepareses: WpfProcessAccess.SignedInUserList[0].sessionID, _preparetrm: WpfProcessAccess.SignedInUserList[0].terminalID);

                //String xx1 = ds1.GetXml().ToString();

                var pap1 = vm1.SetParamUpdateMSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, ds1, EditTransID1);
                //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
                //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds2 == null)
                    return;

                decimal tnetam = this.ListViewItemTable1.Sum(x => x.invnetamt);
                decimal tvatam = this.ListViewItemTable1.Sum(x => x.invvatamt);
                string memonum1 = ds2.Tables[0].Rows[0]["memonum1"].ToString();
                string memonum = ds2.Tables[0].Rows[0]["memonum"].ToString();
                this.txtTransID.Text = memonum1;
                this.txtTransID.Tag = memonum;

                decimal dueAmt = decimal.Parse("0" + this.lblNetBalance.Content.ToString());
                DateTime MemoDate1 = DateTime.Parse(this.lblTransDate.Content.ToString().Substring(0, 11));
                string invref1 = this.txtContactNo.Text.Trim();
                string invnar1 = this.txtMemoNar.Text.Trim();

                //--------------------------------------
                string Content1 = memonum1.Substring(3, 2) + "-" + memonum1.Substring(11, 5) + " [Tk. " + (tnetam + tvatam).ToString("#,##0.00") +
                        (dueAmt <= 0 ? "" : ", Due: Tk. " + dueAmt.ToString("#,##0.00")) + ", " +
                        MemoDate1.ToString("dd.MM.yyyy") + "]" + (invref1.Trim().Length > 0 ? ", " + invref1.Trim() : "") +
                            (invnar1.Trim().Length > 0 ? ", " + invnar1.Trim() : "");

                bool isExist1 = false;
                foreach (ComboBoxItem item in this.cmbPrevTransList.Items)
                {
                    if (item.Tag.ToString() == memonum)
                    {
                        item.Content = Content1;
                        isExist1 = true;
                        break;
                    }
                }

                if (isExist1 == false)
                    this.cmbPrevTransList.Items.Insert(0, new ComboBoxItem() { Content = Content1, Tag = memonum });

                //--------------------------------------

                this.MemoSaved = true;
                this.btnUpdateTrans.IsEnabled = false;
                this.btnAddSearch.IsEnabled = false;
                this.btnPrintTrans.Visibility = Visibility.Visible;

                //if (this.chkPrintDirect.IsChecked == true)
                //{
                //    this.btnPrintTrans_Click(null, null);
                //}
                //else
                //{
                //    this.btnPrintTrans.Visibility = Visibility.Visible;
                //}
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-10: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnNewShow_Click(object sender, RoutedEventArgs e)
        {
            this.CleanUpScreen();
        }

        private void btnPrintTrans_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.MemoSaved == false)
                    this.btnUpdateTrans_Click(null, null);

                if (this.MemoSaved == false)
                    return;

                this.PrintCount = this.PrintCount + 1;

                if (this.PrintCount > 2)
                    this.btnPrintTrans.Visibility = Visibility.Hidden;

                string MemoNum = this.txtTransID.Tag.ToString();
                string PrnOpt1 = (this.chkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                this.ViewPrintMemo(MemoNum, PrnOpt1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-11: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ViewPrintMemo(string memoNum = "XXXXXXXX", string ViewPrint = "View", string Duplicate = "")
        {
            try
            {
                //string memoNum = ((ComboBoxItem)this.cmbPrevInvList.SelectedItem).Tag.ToString();
                LocalReport rpt1 = null;
                string WindowTitle1 = "";
                var pap1 = vm2.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;
                this.lblTokenSlNo.Content = ds1.Tables[2].Rows[0]["tokenid"].ToString().Trim();
                ds1.Tables[0].Rows[0]["slnum"] = Convert.ToInt32(ds1.Tables[2].Rows[0]["tokenid"]);
                //var list3 = WpfProcessAccess.GetRptGenInfo(InputSource: "Test Input Source\n");
                var list3 = WpfProcessAccess.GetRptGenInfo();
                list3[0].RptHeader1 = "";
                switch (WpfProcessAccess.CompInfList[0].comcod)
                {
                    case "6501":
                        list3[0].RptHeader1 = "VAT Reg. # " + "XXXXXX";
                        break;
                    case "6531":
                        list3[0].RptHeader1 = "VAT Reg. # " + "000538591";
                        break;
                    case "6535":
                        list3[0].RptHeader1 = "";
                        break;
                }

                list3[0].RptFooter1 = "User : " + WpfProcessAccess.SignedInUserList[0].signinnam;

                var list1 = ds1.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
                var list2 = ds1.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();
                // var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();

                rpt1 = CommReportSetup.GetLocalReport("Pharmacy.PhSalesInv01", list1, list2, list3);

                if (Duplicate.Length > 0 || this.PrintCount > 1)
                {
                    //rpt1.SetParameters(new ReportParameter("ParamAddress1", "[Re-Print/Duplicate Invoice]"));
                    rpt1.SetParameters(new ReportParameter("ParamAddress1", ":: D U P L I C A T E ::"));                    
                    rpt1.SetParameters(new ReportParameter("ParamAddress2", "========================="));
                }

                WindowTitle1 = "Sales Memo";

                if (ViewPrint == "View")
                {
                    WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: "PrintLayout");
                }
                else if (ViewPrint == "DirectPrint")
                {
                    RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                    DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                    if (Duplicate.Length == 0 && this.PrintCount == 1)
                    {
                        rpt1.SetParameters(new ReportParameter("ParamTitle1", "Kitchen Order Token (KOT)"));
                        DirectPrint1.PrintReport(rpt1, PrinterName: "PRNCASH");
                    }
                    DirectPrint1.Dispose();
                }
                /*
             
                rpt1.PrintOptions.PrinterName = "PRNCASH";//"\\\\proserver\\Canon LBP3300MIS";
                if (this.chkPrintDirect.Checked)
                    rpt1.PrintToPrinter(1, false, 1, 1);
                else
                {
                    frmRptVirwer frm1 = new frmRptVirwer();
                    frm1.crystalReportViewer1.ReportSource = rpt1;
                    frm1.Show();
                } 
             
                 */
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-12: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void autoItemSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
        }
        private ObservableCollection<vmEntryPharRestPOS1.RetSaleItem> GetItemSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<vmEntryPharRestPOS1.RetSaleItem>(
                this.ShortRetSaleItemList.Where((x, match) => x.sirdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }


        private void btnAddItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string srchVal1a = ((Button)sender).Tag.ToString();
                this.AddChangeItem(srchVal1: srchVal1a);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnItemAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string srchVal1a = ((Button)sender).Tag.ToString();
                this.AddChangeItem(srchVal1: srchVal1a);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-14: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnAddSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.autoItemSearch.SelectedValue == null)
                    return;

                string srchVal1a = this.autoItemSearch.SelectedValue.ToString();
                string srchTxt1a = this.autoItemSearch.SelectedText.ToString();
                this.AddChangeItem(srchVal1: srchVal1a);
                this.autoItemSearch.SelectedValue = null;
                this.autoItemSearch.Text = "";
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-15: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void AddChangeItem(string srchVal1 = "")
        {
            try
            {
                if (this.MemoSaved == true)
                    return;

                if (srchVal1.Length == 0)
                    return;
                var RetailItemList1 = this.RetSaleItemList.FindAll(x => x.sircode == srchVal1).Take(10);
                foreach (var item in RetailItemList1)
                {
                    int serialno1 = this.ListViewItemTable1.Count + 1;
                    var ListViewItemTable1a = this.ListViewItemTable1.FindAll(x => x.invcode == srchVal1);
                    if (ListViewItemTable1a.Count > 0)
                    {
                        ListViewItemTable1a[0].invqty = ListViewItemTable1a[0].invqty + 1;
                    }
                    else
                    {
                        var item1a = new vmEntryPharRestPOS1.ListViewItemTable()
                        {
                            trsl = serialno1.ToString() + ".",
                            invcode = item.sircode, //  invcode1
                            reptsl = "00",
                            rsircode = item.sircode, // rsircode1,
                            trdesc = item.sirdesc.Trim(), // rsirdesc1,
                            mfgid = item.mfgid,// "", // siruid1,
                            mfgcomnam = item.mfgcomnam,// "", //mfgByName1,
                            invqty = 1.00m, //invqty1a,
                            truid = "True", // truid1a,
                            trunit = item.sirunit, // rsirunit,
                            invrate = item.saleprice, // Math.Round(invAmt1 / invqty1a, 6),
                            invamt = item.saleprice, //invAmt1,
                            invdisamt = 0,
                            invnetamt = item.saleprice, // invAmt1,
                            invvatper = item.salvatp,
                            invvatamt = (item.salvatp / 100.00m * item.saleprice),
                            invrmrk = "",
                            batchno = item.sircode.Substring(6) + ": " + item.sirdesc.Trim(),
                            mfgdat = DateTime.Today,
                            expdat = DateTime.Today.AddDays(7),
                            mfgvisible = item.mfgvisible,
                            rmrkvisible = "Collapsed"
                        };

                        this.ListViewItemTable1.Add(item1a);
                    }
                }
                this.btnPrintTrans.Visibility = Visibility.Visible;
                //this.chkPrintDirect.Visibility = Visibility.Visible;
                //this.chkConfrmSave.Visibility = Visibility.Visible;
                this.btnUpdateTrans.Visibility = Visibility.Visible;
                this.btnTotal_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-16: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                foreach (var item in this.ListViewItemTable1)
                {
                    item.invqty = (this.chkIntQtyOnly.IsChecked == true ? Math.Floor(item.invqty) : item.invqty);
                    item.invamt = item.invrate * item.invqty;
                    item.invnetamt = item.invamt - item.invdisamt;
                    item.invvatamt = Math.Round(item.invvatper / 100.00m * item.invamt, 0);
                }
                decimal GrandTotal1 = this.ListViewItemTable1.Sum(x => x.invamt);
                decimal DiscTotal1 = this.ListViewItemTable1.Sum(x => x.invdisamt);
                decimal NetTotal = this.ListViewItemTable1.Sum(x => x.invnetamt);
                decimal VATTotal = this.ListViewItemTable1.Sum(x => x.invvatamt);
                decimal TotalBill = GrandTotal1 - DiscTotal1 + VATTotal;// +decimal.Parse("0" + this.txtCCCharge.Text.Trim());              
                decimal TotalPaid = decimal.Parse("0" + this.txtPaidAmt.Text.Trim()); // this.ListViewItemTable1.Sum(x => x.colam) + decimal.Parse("0" + this.txtCCPaid.Text.Trim());
                decimal ChangeCash = ((TotalPaid < TotalBill) ? 0.00m : (TotalPaid - TotalBill));
                decimal BalanceBill = ((TotalBill < TotalPaid) ? 0.00m : (TotalBill - TotalPaid));

                this.lblGrandTotal.Content = GrandTotal1.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblDiscTotal.Content = DiscTotal1.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblNetTotal.Content = NetTotal.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblVATTotal.Content = VATTotal.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblTotalBill.Content = TotalBill.ToString("#,##0;(#,##0); - ");// " -  ";
                //this.lblTotalPaid.Content = TotalPaid.ToString("#,##0.00;(#,##0.00); - ");// " -  ";
                this.lblNetBalance.Content = BalanceBill.ToString("#,##0;(#,##0); - ");// " -  ";            
                this.lblChangeCash.Content = ChangeCash.ToString("#,##0;(#,##0); - ");// " -  ";            
                this.ListViewItemTable1 = this.ListViewItemTable1.FindAll(x => x.invqty > 0);
                int serialno1 = 1;
                foreach (var item in this.ListViewItemTable1)
                {
                    item.trsl = serialno1.ToString() + ".";
                    ++serialno1;
                }
                this.dgvMemo.ItemsSource = this.ListViewItemTable1;
                this.dgvMemo.Items.Refresh();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-17: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void chkDiscount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //this.chkDiscount.IsChecked = !this.chkDiscount.IsChecked;
                bool isChecked1 = (this.chkDiscount.IsChecked == true);
                //this.chkDiscount.IsChecked = !isChecked1;
                this.iudDisPer.Value = 0;
                this.iudDisPer.IsEnabled = isChecked1;
                this.btnSetDispPer.IsEnabled = isChecked1;
                if (this.iudDisPer.IsEnabled == true)
                    this.iudDisPer.Focus();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-18: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnSetDispPer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string siaper1 = this.iudDisPer.Value.ToString();
                decimal disper1 = decimal.Parse("0" + siaper1) / 100.00m;
                foreach (var item in this.ListViewItemTable1)
                {
                    item.invamt = item.invrate * item.invqty;
                    item.invdisamt = Math.Round(item.invamt * disper1, 0);
                }
                this.chkDiscount.IsChecked = false;
                this.chkDiscount_Click(null, null);
                this.btnTotal_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-19: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void dgvUdQtyChange_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }

        private void dgvTxtDiscnt_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }

        private void txtPaidAmt_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string MemoNum = ((ComboBoxItem)this.cmbPrevTransList.SelectedItem).Tag.ToString();
                string PrnOpt1 = (this.chkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
                this.ViewPrintMemo(MemoNum, PrnOpt1, "Reprint");
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-20: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void cmbItemCat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbItemCat.SelectedIndex >= 0)
                    this.btnShowGroupItems_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-21: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnShowGroupItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ShortRetSaleItemList.Clear();
                string msircode1 = ((ComboBoxItem)this.cmbItemCat.SelectedItem).Tag.ToString();
                this.ShortRetSaleItemList = this.RetSaleItemList.FindAll(x => x.msircode == msircode1).ToList();
                if (msircode1 == "000000000000")
                    this.ShortRetSaleItemList = this.RetSaleItemList.ToList();

                this.autoItemSearch.ContextMenu.Items.Clear();
                foreach (var item in this.ShortRetSaleItemList)
                {
                    MenuItem mnu1 = new MenuItem { Header = item.sirdesc, Tag = item.sircode };
                    mnu1.Click += this.autoItemSearch_ContextMenu_MouseClick;
                    this.autoItemSearch.ContextMenu.Items.Add(mnu1);
                }

                this.lstvRetailItem.ItemsSource = null;
                this.lstvRetailItem.ItemsSource = this.ShortRetSaleItemList;
                this.lstvRetailItem.Items.Refresh();
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-22: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

        }
        private void autoItemSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.autoItemSearch.ItemsSource = this.ShortRetSaleItemList;
                this.autoItemSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim();
                this.btnAddSearch_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-23: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void autoItemSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoItemSearch.ContextMenu.IsOpen = true;
        }


        private void btnEdit1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.CleanUpScreen();
                if (this.cmbPrevTransList.Items.Count == 0)
                    return;

                string MemoNum = ((ComboBoxItem)this.cmbPrevTransList.SelectedItem).Tag.ToString();
                string MemoText = ((ComboBoxItem)this.cmbPrevTransList.SelectedItem).Content.ToString().Trim();

                if (!MemoText.Contains("Due: Tk.") && this.chkDueList.IsChecked == true)
                {
                    System.Windows.MessageBox.Show("Edit not possible for paid invoice", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                var pap1 = vm2.SetParamSalesInvoice(WpfProcessAccess.CompInfList[0].comcod, MemoNum);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
                this.EditDs = WpfProcessAccess.GetHmsDataSet(pap1);
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (this.EditDs == null)
                    return;

                var list1 = this.EditDs.Tables[1].DataTableToList<HmsEntityCommercial.PhSalesInvoice01>();
                var list2 = this.EditDs.Tables[0].DataTableToList<HmsEntityCommercial.InvoiceTransList>();


                if (this.chkDueList.IsChecked == true && list2[0].billam <= list2[0].collam)
                {
                    System.Windows.MessageBox.Show("Edit not possible for paid invoice", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, 
                        MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                    this.btnFilterPrevTrans_Click(null, null);
                    return;
                }


                this.lblTransDate.Content = list2[0].invdat.ToString("dd-MMM-yyyy");

                this.txtTransID.Text = list2[0].invno1;
                this.txtTransID.Tag = list2[0].invno;

                string PayType1 = list2[0].paytype.Trim().ToUpper();

                this.txtMemoNar.Text = list2[0].invnar.Trim();
                this.txtPaidAmt.Text = list2[0].collam.ToString("#,##0.00");

                this.ListViewItemTable1.Clear();
                bool intOnly = true;
                foreach (var item in list1)
                {
                    intOnly = ((Math.Floor(item.invqty) == Math.Ceiling(item.invqty)) && intOnly);
                    //this.RetSaleItemList.Add(itm1);
                    var itmcod1 = this.RetSaleItemList.FindAll(x => x.sircode == item.rsircode);

                    var item1a = new vmEntryPharRestPOS1.ListViewItemTable()
                    {
                        trsl = item.slnum.ToString() + ".",
                        invcode = item.rsircode, //  invcode1
                        reptsl = item.reptsl,
                        rsircode = item.rsircode, // rsircode1,
                        trdesc = itmcod1[0].sirdesc, // item.sirdesc.Trim(), // rsirdesc1,
                        mfgid = "", //item.mfgid,// "", // siruid1,
                        mfgcomnam = "", //item.mfgcomnam.Trim(),// "", //mfgByName1,
                        invqty = item.invqty, // itmqty1, //1.00m, //invqty1a,
                        truid = (this.chkDueList.IsChecked == true ? "False" : "True"), // truid1a,
                        trunit = item.sirunit, // rsirunit,
                        invrate = item.itmrat, // itmRate11, // item.saleprice, // Math.Round(invAmt1 / invqty1a, 6),
                        invamt = item.itmam, // itmAmt1, // item.saleprice, //invAmt1,
                        invdisamt = item.idisam,
                        invnetamt = item.inetam, //itmAmt1, // item.saleprice, // invAmt1,
                        invvatper = (item.itmam == 0 ? 0.00m : item.ivatam / item.itmam * 100.00m), // item. item.salvatp,
                        invvatamt = item.ivatam,// (item.salvatp / 100.00m * itmAmt1), //(item.salvatp / 100.00m * item.saleprice),
                        invrmrk = item.invrmrk,// this.txtItemRmrk.Text.Trim(), // Remarks Requirements : <!-- This option will required when each item special specification exist (like computer parts specifications -->
                        batchno = item.rsircode.Substring(6) + ": " + item.sirdesc.Trim(),
                        mfgdat = list2[0].invdat,// DateTime.Today,
                        expdat = list2[0].invdat.AddDays(7), //DateTime.Today.AddDays(7),
                        mfgvisible = itmcod1[0].mfgvisible, //item.mfgvisible,
                        rmrkvisible = itmcod1[0].rmrkvisible, //item.rmrkvisible,
                    };
                    this.ListViewItemTable1.Add(item1a);
                }
                this.chkIntQtyOnly.IsChecked = intOnly;
                this.dgvMemo.Items.Refresh();
                this.btnTotal_Click(null, null);
                this.btnUpdateTrans.Visibility = Visibility.Visible;
                this.btnUpdateTrans.IsEnabled = true;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-24: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnDelete1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.cmbPrevTransList.SelectedItem == null)
                {
                    System.Windows.MessageBox.Show("No record found to cancel", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel this Invoice", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;

                var item1a = (ComboBoxItem)this.cmbPrevTransList.SelectedItem;// (HmsEntityCommercial.InvoiceTransList)this.lstPrevTransList.SelectedItem;
                int itemno1 = this.cmbPrevTransList.SelectedIndex;


                var pap1a = vm1.SetParamInvoiceVouchers(WpfProcessAccess.CompInfList[0].comcod, item1a.Tag.ToString().Trim());

                DataSet ds1a = WpfProcessAccess.GetHmsDataSet(pap1a);
                if (ds1a == null)
                    return;


                var pap1 = vm1.SetParamCancelMemo(WpfProcessAccess.CompInfList[0].comcod, item1a.Tag.ToString().Trim());

                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                this.cmbPrevTransList.Items.RemoveAt(itemno1);

                this.cmbPrevTransList.Items.Refresh();
                if (this.cmbPrevTransList.Items.Count > 0)
                    this.cmbPrevTransList.SelectedIndex = 0;

                if (this.txtTransID.Tag.ToString().Trim() == item1a.Tag.ToString().Trim())
                    this.CleanUpScreen();

                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("FSI-25: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
