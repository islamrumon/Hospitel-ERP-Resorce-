using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data;
using System.Windows.Threading;
using ASITHmsEntity;
using ASITHmsViewMan.Commercial;
using ASITHmsRpt4Commercial;
using Microsoft.Reporting.WinForms;
using ASITFunLib;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Globalization;
using Microsoft.Win32;
using System.Diagnostics;
using ASITHmsViewMan.Manpower;
using System.Collections.ObjectModel;

namespace ASITHmsWpf.Commercial.Hospital
{
    /// <summary>
    /// Interaction logic for frmEntryFrontDesk101.xaml
    /// </summary>
    public partial class frmEntryFrontDesk101 : UserControl
    {
        private bool FrmInitialized = false;
        private int cmbInvMode_DefaultIndex = 0;
        private DispatcherFrame frameFindPatient;
        private DispatcherFrame framePatientPhoto;
        private List<vmEntryFrontDesk1.OrderItem> OrderItemList = new List<vmEntryFrontDesk1.OrderItem>();
        private List<HmsEntityCommercial.CommInv01.CommInv01TblCol> CollInfoList = new List<HmsEntityCommercial.CommInv01.CommInv01TblCol>();
        private List<HmsEntityCommercial.CommInvSummInf> CommInvSummList = new List<HmsEntityCommercial.CommInvSummInf>();             // Hospital/Diagnostic Centre Commercial Invoice Summary List
        private List<HmsEntityCommercial.HmsServiceItem> ServiceItemList = new List<HmsEntityCommercial.HmsServiceItem>();             // Hospital/Diagnostic Centre Service Item List
        private List<HmsEntityCommercial.HmsRefByInf> RefByInfList = new List<HmsEntityCommercial.HmsRefByInf>();                   // Hospital/Diagnostic Centre Service Item List

        private vmEntryFrontDesk1 vm1 = new vmEntryFrontDesk1();
        private vmReportFrontDesk1 vmr = new vmReportFrontDesk1();
        private DrCapture DrCam1;

        private string preparebyid1, prepareses1, preparetrm1;

        IntPtr m_ip = IntPtr.Zero;
        System.Windows.Forms.PictureBox picturebox1 = new System.Windows.Forms.PictureBox();
        const int VIDEODEVICE = 0; // zero based index of video capture device to use
        const int VIDEOWIDTH = 640; //  640; // Depends on video device caps
        const int VIDEOHEIGHT = 480; // 480; // Depends on video device caps
        const int VIDEOBITSPERPIXEL = 24; // 24 //BitsPerPixel values determined by device

        public frmEntryFrontDesk101()
        {
            InitializeComponent();
        }
        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;
                this.windowsFormsHost1.Child = picturebox1;
                this.ActivateAuthObjects();
                this.Objects_On_Init();
                this.CleanUpScreen();
            }
        }

        private void ActivateAuthObjects()
        {
            try
            {
                //this.btnFindPatient.Visibility = Visibility.Hidden; // For Temporary Hidden -- Hafiz 30-May-2018

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_chkDateBlocked") == null)
                    this.chkDateBlocked.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_chkBranchName") == null)
                    this.chkBranchName.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_btnTransList") == null)
                    this.btnTransList.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_chkFilterUser") == null)
                    this.chkFilterUser.Visibility = Visibility.Hidden;

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_btnDelete1") == null)
                    this.btnDelete1.Visibility = Visibility.Hidden;



                this.btnEdit1.Visibility = Visibility.Hidden;
                this.btnEdit2.Visibility = Visibility.Hidden;
                this.btnEdit3.Visibility = Visibility.Hidden;
                this.btnEdit4.Visibility = Visibility.Hidden;

                this.cmbInvMode.Items.Clear();
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_cmbInvMode_NEW_INVOICE") != null)
                {
                    this.cmbInvMode.Items.Add(new TextBlock()
                    {
                        Text = "New Invoice Entry",
                        Tag = "NEW_INVOICE",
                        Background = System.Windows.Media.Brushes.Transparent,
                        Width = 145
                    });
                }

                ContextMenu cm1 = new ContextMenu() { FontSize = 14 };
                int i = 1;
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_cmbInvMode_REPORT_DELIVERY_INVOICE") != null)
                {
                    this.cmbInvMode.Items.Add(new TextBlock()
                    {
                        Text = "Report Delivery",
                        Tag = "REPORT_DELIVERY_INVOICE",
                        Foreground = System.Windows.Media.Brushes.Maroon,
                        FontWeight = FontWeights.Bold,
                        Background = System.Windows.Media.Brushes.White,
                        Width = 145
                    });

                    this.btnEdit4.Tag = i.ToString().Trim();

                    var mir1 = new MenuItem() { Header = "Report Delivery", Tag = i.ToString().Trim(), Uid = "REPORT_DELIVERY_INVOICE", Foreground = System.Windows.Media.Brushes.Maroon, Background = System.Windows.Media.Brushes.White };
                    mir1.Click += this.dgvTransList_MenuItem_Click;
                    cm1.Items.Add(mir1);
                    this.btnEdit4.Visibility = Visibility.Visible;
                    i++;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_cmbInvMode_DUE_COLL_INVOICE") != null)
                {
                    this.cmbInvMode.Items.Add(new TextBlock()
                    {
                        Text = "Due-Coll. Invoice",
                        Tag = "DUE_COLL_INVOICE",
                        Foreground = System.Windows.Media.Brushes.Green,
                        FontWeight = FontWeights.Bold,
                        Background = System.Windows.Media.Brushes.White,
                        Width = 145
                    });

                    if (cm1.Items.Count > 0)
                        cm1.Items.Add(new Separator());

                    this.btnEdit3.Tag = i.ToString().Trim();
                    var micol1 = new MenuItem() { Header = "Due-Coll. Invoice", Tag = i.ToString().Trim(), Uid = "DUE_COLL_INVOICE", Foreground = System.Windows.Media.Brushes.Green, Background = System.Windows.Media.Brushes.White };
                    micol1.Click += this.dgvTransList_MenuItem_Click;
                    cm1.Items.Add(micol1);
                    this.btnEdit3.Visibility = Visibility.Visible;
                    i++;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_cmbInvMode_PART_EDIT_INVOICE") != null)
                {
                    this.cmbInvMode.Items.Add(new TextBlock()
                    {
                        Text = "Part-Edit Invoice",
                        Tag = "PART_EDIT_INVOICE",
                        Foreground = System.Windows.Media.Brushes.Blue,
                        FontWeight = FontWeights.Bold,
                        Background = System.Windows.Media.Brushes.White,
                        Width = 145
                    });

                    if (cm1.Items.Count > 0)
                        cm1.Items.Add(new Separator());

                    this.btnEdit2.Tag = i.ToString().Trim();
                    var mied1 = new MenuItem() { Header = "Part-Edit Invoice", Tag = i.ToString().Trim(), Uid = "PART_EDIT_INVOICE", Foreground = System.Windows.Media.Brushes.Blue, Background = System.Windows.Media.Brushes.White };
                    mied1.Click += this.dgvTransList_MenuItem_Click;
                    cm1.Items.Add(mied1);
                    this.btnEdit2.Visibility = Visibility.Visible;
                    i++;
                }

                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmEntryFrontDesk101_cmbInvMode_FULL_EDIT_INVOICE") != null)
                {
                    this.cmbInvMode.Items.Add(new TextBlock()
                    {
                        Text = "Full-Edit Invoice",
                        Tag = "FULL_EDIT_INVOICE",
                        Foreground = System.Windows.Media.Brushes.Red,
                        FontWeight = FontWeights.Bold,
                        Background = System.Windows.Media.Brushes.White,
                        Width = 145
                    });

                    if (cm1.Items.Count > 0)
                        cm1.Items.Add(new Separator());
                    this.btnEdit1.Tag = i.ToString().Trim();
                    var mied2 = new MenuItem() { Header = "Full-Edit Invoice", Tag = i.ToString().Trim(), Uid = "FULL_EDIT_INVOICE", Foreground = System.Windows.Media.Brushes.Red, Background = System.Windows.Media.Brushes.White };
                    mied2.Click += this.dgvTransList_MenuItem_Click;
                    cm1.Items.Add(mied2);
                    this.cmbInvMode_DefaultIndex = this.cmbInvMode.Items.Count - 1;
                    this.btnEdit1.Visibility = Visibility.Visible;
                }

                if (this.cmbInvMode.Items.Count > 0)
                    this.cmbInvMode.SelectedIndex = this.cmbInvMode_DefaultIndex; // 0;

                // Following code will be changed as per authentication
                this.chkRefBy.Visibility = Visibility.Hidden;
                this.chkRefBy.IsChecked = true;
                this.chkRefBy_Click(null, null);

                cm1.Items.Add(new Separator());
                var mip1 = new MenuItem() { Header = "Invoice Print Preview ", Tag = "0", Uid = "PRINT_PREVIEW", Background = System.Windows.Media.Brushes.White };
                mip1.Click += this.dgvTransList_MenuItem_Click;
                cm1.Items.Add(mip1);

                if (this.btnDelete1.Visibility == Visibility.Visible)
                {
                    cm1.Items.Add(new Separator());
                    var mic1 = new MenuItem() { Header = "Cancel Invoice", Tag = "0", Uid = "CANCEL_DELETE", Background = System.Windows.Media.Brushes.White };
                    mic1.Click += this.dgvTransList_MenuItem_Click;
                    cm1.Items.Add(mic1);
                }

                //for (int ii = 0; ii < cm1.Items.Count; ii++)
                //{
                //    if ((MenuItem)cm1.Items[ii] != null)
                //    {
                //        ((MenuItem)cm1.Items[ii]).Click += this.dgvTransList_MenuItem_Click;
                //    }
                //}
                this.dgvTransList.ContextMenu = cm1;

                if (this.cmbInvMode.Items.Count == 0)
                    this.Visibility = Visibility.Collapsed;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

            ////this.stkpAddEditItem.Visibility = Visibility.Hidden;
        }

        private void Objects_On_Init()
        {
            try
            {
                this.GetBranchList();
                this.GetServiceItemList(itrmGroup: "4502%");
                this.GetRefByTitleList();
                this.GetDiscountTypeList();
                //this.dgvMemo.ItemsSource = this.OrderItemList;
                //this.dgvColInfo.ItemsSource = this.CollInfoList;

                this.xctk_dtpSrchDat1.Value = DateTime.Today.AddDays(-3);
                this.xctk_dtpSrchDat2.Value = DateTime.Today;
                this.cmbPrevYearMon.Items.Clear();

                DateTime dt1 = DateTime.Parse("01-" + DateTime.Today.ToString("MMM-yyyy"));
                for (int i = 0; i > -10; i--)
                    this.cmbPrevYearMon.Items.Add(new ComboBoxItem()
                    {
                        Content = dt1.AddMonths(i).ToString("yyMM"),
                        Tag = dt1.AddMonths(i).ToString("yyyyMM"),
                        Width = 50,
                        HorizontalContentAlignment = HorizontalAlignment.Left
                    });

                this.cmbPrevYearMon.SelectedIndex = 0;

                this.FindBrnchForThisTerminal();
                //this.cmbBranch.Items.Add(new ComboBoxItem()
                //{
                //    Content = item.brnnam.Trim() + " (" + item.brnsnam.Trim() + ")",
                //    Tag = item.brncod,
                //    Uid = item.brnsnam.Trim(),
                //    ToolTip = item.brnnam.Trim() + " (" + item.brnsnam.Trim() + ")"
                //});

                this.btnRefreshRef_Click(null, null);
                //this.btnFilter1_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-02: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void FindBrnchForThisTerminal()
        {
            if (WpfProcessAccess.GenInfoTitleList == null)
                WpfProcessAccess.GetGenInfoTitleList();

            string comcod1 = WpfProcessAccess.CompInfList[0].comcod;
            string TerminalID1 = WpfProcessAccess.SignedInUserList[0].terminalID.ToUpper();

            var TerminalList1 = WpfProcessAccess.GenInfoTitleList.FindAll(x => x.actcode.Substring(0, 8) == "CBWS" + WpfProcessAccess.CompInfList[0].comcod
                                && x.acttdesc.Trim().ToUpper() == TerminalID1 && x.actelev.Trim().Length == 4).ToList();
            int li = 0;
            if (TerminalList1.Count > 0)
            {
                string brncod1 = TerminalList1[0].actelev.Trim();
                foreach (ComboBoxItem item in this.cmbBranch.Items)
                {
                    if (item.Tag.ToString().ToUpper().Trim() == brncod1)
                    {
                        this.cmbBranch.SelectedIndex = li;
                        break;
                    }
                    li++;
                }
            }
        }
        private void GetBranchList()
        {
            this.cmbBranch.Items.Clear();
            var brnlist1 = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            foreach (var item in brnlist1)
                this.cmbBranch.Items.Add(new ComboBoxItem()
                {
                    Content = item.brnnam.Trim() + " (" + item.brnsnam.Trim() + ")",
                    Tag = item.brncod,
                    Uid = item.brnsnam.Trim(),
                    ToolTip = item.brnnam.Trim() + " (" + item.brnsnam.Trim() + ")"
                });

            this.cmbBranch.SelectedIndex = 0;
        }

        private void GetRefByTitleList()
        {
            this.cmbRefByTitle.Items.Clear();
            var TitleList = HmsEntityCommercial.GetRefByTitlesList();
            foreach (var item in TitleList)
                this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = item.rtitle, Tag = item.rtagid, ToolTip = item.rtooltip });

            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "DOCTOR", Tag = "DR.", ToolTip = "DOCTOR (DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "PROFESSOR", Tag = "PROF.", ToolTip = "PROFESSOR (PROF.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "PROFESSOR DOCTOR", Tag = "PROF. DR.", ToolTip = "PROFESSOR DOCTOR (PROF. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "ASSISTENT PROFESSOR DOCTOR", Tag = "ASSTT. PROF. DR.", ToolTip = "ASSISTENT PROFESSOR DOCTOR (ASSTT. PROF. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "BRIG. GEN. PROFESSOR DOCTOR", Tag = "BRIG. GEN. PROF. DR.", ToolTip = "BRIG. GEN. PROFESSOR DOCTOR (BRIG. GEN. PROF. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "BRIG. GEN. DOCTOR", Tag = "BRIG. GEN. DR.", ToolTip = "BRIG. GEN. DOCTOR (BRIG. GEN. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "LT. COL. PROFESSOR DOCTOR", Tag = "LT. COL. PROF. DR.", ToolTip = "LT. COL. PROFESSOR DOCTOR (LT. COL. PROF. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "LT. COL. DOCTOR", Tag = "LT. COL. DR.", ToolTip = "LT. COL. DOCTOR (LT. COL. DR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "HOSPITAL", Tag = "(HOSPITAL)", ToolTip = "HOSPITAL" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "CLINIC", Tag = "(CLINIC)", ToolTip = "CLINIC" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "PHARMACY", Tag = "(PHARMA)", ToolTip = "MEDICINE SHOP" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "MR.", Tag = "MR.", ToolTip = "INDIVIDUAL (MR.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "MS.", Tag = "MS.", ToolTip = "INDIVIDUAL (MS.)" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "COMPANY", Tag = "(COMPANY)", ToolTip = "OTHER COMPANY" });
            //this.cmbRefByTitle.Items.Add(new ComboBoxItem() { Content = "(NONE)", Tag = ".", ToolTip = "UNTITLED" });
        }

        private void GetDiscountTypeList()
        {
            this.cmbDisType.Items.Clear();
            this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "General Discount", Tag = "0GEND", ToolTip = "Applicable General Discount Formula" });
            this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Ref./Doctor's Part", Tag = "1REFD", ToolTip = "Discount From Ref./Doctor's Part" });
            this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Full (100 %) Free", Tag = "2FREE", ToolTip = "Full Free Diagnosis Facilities" });
            this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Special Purpose", Tag = "3SPCD", ToolTip = "Special Purpose Discount" });
        }
        private void CleanUpScreen()
        {
            this.stkpPhoto.Visibility = Visibility.Hidden;
            this.GridPatientPhoto.Visibility = Visibility.Collapsed;
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
            this.GridTestItem.Visibility = Visibility.Collapsed;
            this.btnUpdateTrans.Visibility = Visibility.Hidden;
            this.btnPrintTrans.Visibility = Visibility.Hidden;
            this.GridFindPatient.Visibility = Visibility.Collapsed;
            this.stkpNav1.Visibility = Visibility.Hidden;
            this.stkpEntry.Visibility = Visibility.Collapsed;
            this.stkpEntry.IsEnabled = false;
            this.stkpTransList.Visibility = Visibility.Collapsed;
            this.stkpGridTransList.IsEnabled = false;
            this.stkpTitle2.Visibility = Visibility.Hidden;
            this.stkpPrint.Visibility = Visibility.Hidden;

            this.btnUpdateTrans.IsEnabled = true;
            this.stkpTitle2.IsEnabled = true;
            this.stkpFooter1.IsEnabled = true;
            this.stkpItemAdd.IsEnabled = true;
            this.stkpCCCharge.IsEnabled = true;
            this.stkpFooter2.IsEnabled = true;
            //this.chkFilterUser.IsChecked = false;
            this.chkMemberID.IsChecked = false;
            this.iudDisPer.IsEnabled = false;
            this.cmbDisType.SelectedIndex = 1;//  0;
            this.cmbDisType.Tag = "1REFD";// "0GEND";
            this.cmbDisType.IsEnabled = false;
            //this.chkDisDoctor.IsEnabled = false;
            this.btnSetDispPer.IsEnabled = false;
            this.btnPatPhoto.IsEnabled = false;
            this.chkDghDelivered.IsChecked = false;
            this.btnTransList.Content = "Trans. List";
            this.txtPrevTransID.Text = "";
            this.txtPatientName.Text = "";
            this.iudAgeY.Value = 0;
            this.iudAgeM.Value = 0;
            this.iudAgeD.Value = 0;
            this.chkStaffRef.IsChecked = false;
            this.autoStaffRefSearch.IsEnabled = false;
            this.autoStaffRefSearch.SelectedValue = null;
            this.cmbPatientGender.SelectedIndex = 0;
            this.txtContactNo.Text = "";

            this.chkRefBy.IsChecked = (this.chkRefBy.Visibility == Visibility.Visible ? true : this.chkRefBy.IsChecked);
            this.chkRefBy_Click(null, null);

            this.txtMemberID.Text = "";
            this.txtRefByID.Text = "000000000000";
            this.txtRefByName.Text = "";
            this.txtRefRemarks.Text = "";
            this.txtItemName0.Focusable = false;
            this.txtRefByName.ToolTip = null;

            this.chkDeliveryTime.IsChecked = true;  // false;
            this.xcdtDeliveryDT.Value = DateTime.Parse(DateTime.Today.ToString("dd-MMMM-yyyy") + " 07:00 PM");

            if (this.chkDateBlocked.IsChecked == false)
            {
                this.xctk_dtpInvDat.Value = DateTime.Now;
                //this.xctk_dtpInvDat.Tag = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            }
            this.txtDiscTotal.Text = "";
            this.lblGrandTotal.Content = " -  ";
            this.lblDiscTotal.Content = " -  ";
            this.lblNetTotal.Content = " -  ";

            this.lblNetBalance.Content = " - ";
            this.lblTotalPaid.Content = " -  ";

            this.chkDiscount.IsChecked = false;
            this.iudDisPer.Value = 0;
            //this.chkDisDoctor.IsChecked = false;

            this.imgPatPhoto.Source = this.imgEmptyPhoto.Source;
            this.imgPatPhotoCapture.Source = this.imgEmptyPhoto.Source;

            this.btnUpdateTrans.Tag = "New";
            this.txtItemName.Text = "";
            this.txtItemName.Tag = "";
            this.dgvMemo.ItemsSource = null;
            this.OrderItemList.Clear();
            this.dgvMemo.Items.Refresh();
            this.dgvColInfo.ItemsSource = null;
            this.CollInfoList.Clear();
            this.dgvColInfo.Items.Refresh();
            this.lstReferrer.Items.Clear();
            this.txtCCCharge.Text = "";
            this.txtCCPaid.Text = "";
            this.txtRemarks.Text = "";
            string lastid1 = this.GetLastTransID();
            this.txtTransID.Text = (lastid1 == "CSI000000000000000" ? "" : lastid1);
            this.txtTransID.Tag = "CSI000000000000000";// lastid1;
            
            if(this.lblInvMode.Tag.ToString().Trim() == "FULL_EDIT_INVOICE")
                this.txtPrevTransID.Text = (lastid1 == "CSI000000000000000" ? "" : lastid1.Substring(6, 5));

            this.preparebyid1 = WpfProcessAccess.SignedInUserList[0].hccode;
            this.prepareses1 = WpfProcessAccess.SignedInUserList[0].sessionID;
            this.preparetrm1 = WpfProcessAccess.SignedInUserList[0].terminalID;

        }
        private string GetLastTransID()
        {
            try
            {
                string InvYearBrn = "CSI" + DateTime.Parse(this.xctk_dtpInvDat.Text).ToString("yyyyMM") + ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString();
                var pap1 = vmr.SetParamToGerLastTransID(WpfProcessAccess.CompInfList[0].comcpcod, InvYearBrn);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return "CSI000000000000000";

                return ds1.Tables[0].Rows[0]["memonum2"].ToString();// "CSI000000000000000";
            }
            catch(Exception exp1)
            {
                return "CSI000000000000000";
            }
        }

        private void GetServiceItemList(string itrmGroup = "4502%")
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            this.ServiceItemList = null;
            var pap1 = vmr.SetParamServiceItemList(WpfProcessAccess.CompInfList[0].comcpcod, itrmGroup);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.ServiceItemList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.HmsServiceItem>();
        }

        private void GetRefByInfList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            this.RefByInfList = null;
            var pap1 = vmr.SetParamRefByInfList(WpfProcessAccess.CompInfList[0].comcpcod, "%");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.RefByInfList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.HmsRefByInf>();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void NumberOnlyValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9+-.,]+");
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void btnFindPatient_Click(object sender, RoutedEventArgs e)
        {
            this.frameFindPatient = new DispatcherFrame();
            this.GridDataEntry.IsEnabled = false;
            this.GridFindPatient.Visibility = Visibility.Visible;
            System.Windows.Threading.Dispatcher.PushFrame(this.frameFindPatient);
            this.GridDataEntry.IsEnabled = true;
            this.GridFindPatient.Visibility = Visibility.Collapsed;
        }

        private void btnCloseFindPatient_Click(object sender, RoutedEventArgs e)
        {
            this.frameFindPatient.Continue = false; // un-blocks gui message pump
        }

        private void txtRefByName_GotFocus(object sender, RoutedEventArgs e)
        {
            this.stkpRefByEntry.Visibility = Visibility.Collapsed;
            this.stkpRefByList.Visibility = Visibility.Visible;
            this.stkpRefByList0.Visibility = Visibility.Visible;
            this.btnAddRefBy.Content = "Add/Edit";

            this.GridTestItem.Visibility = Visibility.Collapsed;
            this.GridRefrrerList.Visibility = Visibility.Visible;
        }

        private void lstReferrer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectReferrerInfo();
        }
        private void lstReferrer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectReferrerInfo();
                this.txtItemName.Focus();
            }
        }
        private void SelectReferrerInfo()
        {
            ListBoxItem lbi1 = (ListBoxItem)this.lstReferrer.SelectedItem;
            if (lbi1 == null)
                return;

            this.txtRefByID.Text = lbi1.Tag.ToString();
            this.txtRefByName.Text = lbi1.Content.ToString().Trim();
            this.txtRefByName.ToolTip = "Ref. ID : " + lbi1.Tag.ToString().Substring(6);
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
            this.txtItemName.Focus();
        }
        private void btnNewShow_Click(object sender, RoutedEventArgs e)
        {
            string transID1 = this.txtPrevTransID.Text.Trim().ToUpper();

            this.CleanUpScreen();
            if (this.btnNewShow.Content.ToString() == "_Next")
            {
                this.stkpEntry.Visibility = Visibility.Collapsed;
                this.stkpEntry.IsEnabled = false;
                this.stkpTitle2.Visibility = Visibility.Hidden;
                this.stkpPrint.Visibility = Visibility.Hidden;
                this.stkpPhoto.Visibility = Visibility.Hidden;
                //this.btnFindPatient.Visibility = Visibility.Visible;
                this.btnUpdateTrans.Tag = "Ok";
                this.stkpTitle1.IsEnabled = true;
                this.btnNewShow.Content = "_Ok";
                this.btnNewShow.Focusable = true;
                this.cmbInvMode.SelectedIndex = this.cmbInvMode_DefaultIndex; // 0;
                return;
            }

            this.cmbInvMode_DefaultIndex = this.cmbInvMode.SelectedIndex;

            //if (this.cmbInvMode.SelectedIndex > 0 && transID1.Length < 5)
            if (this.lblInvMode.Tag.ToString().Trim() != "NEW_INVOICE" && transID1.Length < 5)
            {
                this.txtPrevTransID.Focus();

                return;
            }

            // Start of -- If Edit Existing Invoice
            //string transID1 = this.txtPrevTransID.Text.Trim().ToUpper();
            if (transID1.Length > 0)
            {
                string brnid1 = this.txtPrevBrn.Tag.ToString().Trim();
                string yermon1 = ((ComboBoxItem)this.cmbPrevYearMon.SelectedItem).Tag.ToString().Trim();
                string transID2 = "CSI" + yermon1 + brnid1 + transID1;
                this.MemoEditView(memoNum: transID2);
                return;
            }
            // End of -- If Edit Existing Invoice
            this.dgvMemo.Columns[10].Width = 190;
            this.dgvMemo.Columns[11].Visibility = Visibility.Collapsed;

            this.xcdtDeliveryDT.Value = DateTime.Parse(DateTime.Parse(this.xctk_dtpInvDat.Text).ToString("dd-MMM-yyyy") + " 07:00 PM");
            this.xctk_dtpInvDat_LostFocus(null, null);
            this.dgvColInfo.ItemsSource = null;
            this.CollInfoList.Clear();
            //_preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1
            this.CollInfoList.Add(new HmsEntityCommercial.CommInv01.CommInv01TblCol()
            {
                comcod = "False",
                slnum = 0,
                ptinvnum = "",
                bilcolid = "C001",
                bilcoldat = DateTime.Parse(this.xctk_dtpInvDat.Text),// DateTime.Now,
                bcnote = "",
                paidby = "CASH",
                vounum = "",
                bilcolam = 0.00m,
                preparebyid = this.preparebyid1,
                preparebynam = "NewRow",
                prepareses = this.prepareses1,
                preparetrm = this.preparetrm1,
                rowid = 0
            });
            this.CollInfoList.Add(new HmsEntityCommercial.CommInv01.CommInv01TblCol()
            {
                comcod = "False",
                slnum = 1,
                ptinvnum = "",
                bilcolid = "C002",
                bilcoldat = DateTime.Parse(this.xctk_dtpInvDat.Text),// DateTime.Now,
                bcnote = "",
                paidby = "CASH",
                vounum = "",
                bilcolam = 0.00m,
                preparebyid = this.preparebyid1,
                preparebynam = "NewRow",
                prepareses = this.prepareses1,
                preparetrm = this.preparetrm1,
                rowid = 0
            });
            this.dgvColInfo.ItemsSource = this.CollInfoList;
            this.dgvColInfo.Items.Refresh();


            this.lstServiceItem.Items.Clear();
            this.stkpTitle1.IsEnabled = false;
            this.stkpTitle2.Visibility = Visibility.Visible;
            this.stkpPrint.Visibility = Visibility.Visible;
            this.stkpEntry.Visibility = Visibility.Visible;
            this.stkpEntry.IsEnabled = true;
            this.stkpPhoto.Visibility = Visibility.Visible;
            this.btnPatPhoto.IsEnabled = true;
            //this.btnFindPatient.Visibility = Visibility.Hidden;
            this.btnNewShow.Content = "_Next";
            this.btnNewShow.Focusable = false;
            this.txtPatientName.Focus();
        }


    

        private void txtItemName_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtItemName0.Focusable = false;
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
            this.GridTestItem.Visibility = Visibility.Visible;
        }


        private void txtRefByName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                this.lstReferrer.Focus();
            else if (e.Key == Key.Return)
            {
                if (this.lstReferrer.Items.Count > 0)
                {
                    this.lstReferrer.SelectedIndex = 0;
                }
            }
        }
        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                this.lstServiceItem.Focus();

            else if (e.Key == Key.Return)
            {
                if (this.lstServiceItem.Items.Count > 0)
                {
                    this.lstServiceItem.SelectedIndex = 0;
                    if (this.txtItemName.Text.Trim().Length > 0)
                    {
                        string itemcod1 = this.SelectItemInfo(this.txtItemName.Tag.ToString());
                        this.AddItemToDataGrid(itemcod1);
                        //this.btnAddItem_Click(null, null);
                        this.txtItemName0.Focusable = true;
                        this.txtItemName0.Focus();
                    }
                }
            }
        }

        private void lstServiceItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem lbi1 = (ListBoxItem)this.lstServiceItem.SelectedItem;
            if (lbi1 == null)
            {
                if (this.lstServiceItem.Items.Count > 0)
                {
                    lbi1 = (ListBoxItem)this.lstServiceItem.Items[0];
                }
                else
                    return;
            }
            this.AddItemToDataGrid(lbi1.Tag.ToString());

            //this.SelectItemInfo();
            //this.btnAddItem_Click(null, null);
        }

        private void lstServiceItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.AddItemToDataGrid(this.SelectItemInfo("Return"));
                //this.btnAddItem_Click(null, null);
            }
            else if (e.Key == Key.Space)
            {
                this.AddItemToDataGrid(this.SelectItemInfo("Space"));
                //this.btnAddItem_Click(null, null);
            }

        }
        private string SelectItemInfo(string KeyId = "")
        {
            ListBoxItem lbi1 = (ListBoxItem)this.lstServiceItem.SelectedItem;
            if (lbi1 == null)
            {
                if (this.lstServiceItem.Items.Count > 0)
                {
                    lbi1 = (ListBoxItem)this.lstServiceItem.Items[0];
                }
                else
                    return "";
            }
            if (KeyId.Contains("Return"))
            {
                this.txtItemName.Tag = lbi1.Tag.ToString();
                this.txtItemName.Text = lbi1.Content.ToString().Trim();
                this.txtItemName.Focus();
            }
            return lbi1.Tag.ToString();
        }

        private void chkDiscount_Click(object sender, RoutedEventArgs e)
        {
            //this.chkDiscount.IsChecked = !this.chkDiscount.IsChecked;

            bool isChecked1 = (this.chkDiscount.IsChecked == true);
            //this.chkDiscount.IsChecked = !isChecked1;
            this.txtDiscTotal.Text = "";
            this.iudDisPer.Value = 0;
            if (this.cmbDisType.Tag != null)
                this.cmbDisType.SelectedIndex = int.Parse("0" + this.cmbDisType.Tag.ToString().Substring(0, 1));// = "0GEND"; 0;

            //this.chkDisDoctor.IsChecked = false;
            this.txtDiscTotal.IsEnabled = isChecked1;
            this.iudDisPer.IsEnabled = isChecked1;
            this.cmbDisType.IsEnabled = isChecked1;
            //this.chkDisDoctor.IsEnabled = isChecked1;
            this.btnSetDispPer.IsEnabled = isChecked1;
            if (this.txtDiscTotal.IsEnabled == true)
                this.txtDiscTotal.Focus();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            string sircode1 = this.txtItemName.Tag.ToString().Trim();
            this.AddItemToDataGrid(sircode1);
            this.txtItemName.Focus();

            //if (sircode1.Length == 0)
            //{
            //    this.txtItemName.Focus();
            //    return;
            //}

            //var ExistItem1 = this.OrderItemList.FindAll(x => x.isircode == sircode1);
            //if (ExistItem1.Count > 0)
            //{
            //    this.txtItemName.Focus();
            //    return;
            //}


            //var serviceItem1 = this.ServiceItemList.Find(x => x.sircode == sircode1);
            //if (serviceItem1 == null)
            //{
            //    this.txtItemName.Focus();
            //    return;
            //}
            //// (SIRCODE LIKE '450211166%')
            //string itmrmrk1 = (serviceItem1.sircode.Substring(0, 9).Equals("450211166") ? 
            //       "(Deliver on " + DateTime.Parse(this.xcdtDeliveryDT.Text).AddDays(3).ToString("dd.MM.yyyy hh:mm tt") + ") " : "");
            //this.OrderItemList.Add(new vmEntryFrontDesk1.OrderItem()
            //        {
            //            slnum = "00.",
            //            gsircode = serviceItem1.gsircode,
            //            isircode = serviceItem1.sircode,
            //            reptsl = "001",
            //            gsirdesc = serviceItem1.gsirdesc.Trim(),// + ", " + serviceItem1.sirdesc.Trim(),
            //            sirdesc = serviceItem1.sirdesc,
            //            sirunit = serviceItem1.sirunit,
            //            sirtype = serviceItem1.sirtype,
            //            itemqty = 1.00m,
            //            salrate = serviceItem1.saleprice,
            //            salam = serviceItem1.saleprice,
            //            idisam = 0.00m,
            //            idisper = "",
            //            refscomp = serviceItem1.refscomp,
            //            refscompstd = serviceItem1.refscomp,
            //            refpermark = (serviceItem1.refscomp > 0 ? "%" : ""),
            //            icomam = Math.Round(serviceItem1.saleprice * serviceItem1.refscomp / 100.00m, 2),
            //            icdisam = 0.00m,
            //            inetam = serviceItem1.saleprice,
            //            ivatam = 0.00m,
            //            icsmark = "",
            //            itemrmrk = itmrmrk1,
            //            orderbyid = WpfProcessAccess.SignedInUserList[0].hccode,
            //            orderbyses = WpfProcessAccess.SignedInUserList[0].sessionID,
            //            orderbyterm = WpfProcessAccess.SignedInUserList[0].terminalID,
            //            ordertime = DateTime.Now,
            //            delivered = false,
            //            delivbyid = "000000000000",
            //            delivbyses = "000000",
            //            delivbyterm = "",
            //            delivtime = DateTime.Now,   // DateTime.Parse("01-Jan-1900"),
            //            readmode = "False",
            //            newold = "NewRow"
            //        }
            //    );

            //this.dgvMemo.Items.Refresh();
            //this.btnTotal_Click(null, null);

            //this.dgvMemo.SelectedIndex = this.OrderItemList.Count - 1;
            //var item22 = this.OrderItemList.FindAll(x => x.isircode == serviceItem1.sircode);
            //if (item22.Count > 0)
            //{
            //    this.dgvMemo.ScrollIntoView(item22[0]);
            //}
            //this.txtItemName.Focus();
        }

        private void AddItemToDataGrid(string sircode1 = "XXXXXXXXXXXX")
        {
            //string sircode1 = this.txtItemName.Tag.ToString().Trim();
            if (sircode1.Length == 0)
            {
                this.txtItemName.Focus();
                return;
            }

            var ExistItem1 = this.OrderItemList.FindAll(x => x.isircode == sircode1);
            if (ExistItem1.Count > 0)
            {
                this.txtItemName.Focus();
                return;
            }


            var serviceItem1 = this.ServiceItemList.Find(x => x.sircode == sircode1);
            if (serviceItem1 == null)
            {
                this.txtItemName.Focus();
                return;
            }
            // (SIRCODE LIKE '450211166%')
            string itmrmrk1 = (serviceItem1.sircode.Substring(0, 9).Equals("450211166") ?
                   "(Deliver on " + DateTime.Parse(this.xcdtDeliveryDT.Text).AddDays(3).ToString("dd.MM.yyyy hh:mm tt") + ") " : "");
            this.OrderItemList.Add(new vmEntryFrontDesk1.OrderItem()
            {
                slnum = "00.",
                gsircode = serviceItem1.gsircode,
                isircode = serviceItem1.sircode,
                reptsl = "001",
                gsirdesc = serviceItem1.gsirdesc.Trim(),// + ", " + serviceItem1.sirdesc.Trim(),
                sirdesc = serviceItem1.sirdesc,
                sirunit = serviceItem1.sirunit,
                sirtype = serviceItem1.sirtype,
                itemqty = 1.00m,
                salrate = serviceItem1.saleprice,
                salam = serviceItem1.saleprice,
                idisam = 0.00m,
                idisper = "",
                refscomp = serviceItem1.refscomp,
                refscompstd = serviceItem1.refscomp,
                refpermark = (serviceItem1.refscomp > 0 ? "%" : ""),
                icomam = Math.Round(serviceItem1.saleprice * serviceItem1.refscomp / 100.00m, 2),
                icdisam = 0.00m,
                inetam = serviceItem1.saleprice,
                ivatam = 0.00m,
                icsmark = "",
                itemrmrk = itmrmrk1,
                orderbyid = WpfProcessAccess.SignedInUserList[0].hccode,
                orderbyses = WpfProcessAccess.SignedInUserList[0].sessionID,
                orderbyterm = WpfProcessAccess.SignedInUserList[0].terminalID,
                ordertime = DateTime.Now,
                rptdocid = "000000000000",
                rptlogbyid = "000000000000",
                rptlogbyses = "000000",
                rptlogbyterm = "",
                delivered = false,
                delivbyid = "000000000000",
                delivbyses = "000000",
                delivbyterm = "",
                delivtime = DateTime.Now,   // DateTime.Parse("01-Jan-1900"),
                readmode = "False",
                newold = "NewRow"
            }
                );

            this.dgvMemo.Items.Refresh();
            this.btnTotal_Click(null, null);

            this.dgvMemo.SelectedIndex = this.OrderItemList.Count - 1;
            var item22 = this.OrderItemList.FindAll(x => x.isircode == serviceItem1.sircode);
            if (item22.Count > 0)
            {
                this.dgvMemo.ScrollIntoView(item22[0]);
            }
            //this.txtItemName.Focus();
        }

        private void btnAddRefBy_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnAddRefBy.Content.ToString() == "Add/Edit")
            {
                this.stkpRefByList.Visibility = Visibility.Collapsed;
                this.stkpRefByList0.Visibility = Visibility.Collapsed;
                this.stkpRefByEntry.Visibility = Visibility.Visible;
                this.btnAddRefBy.Content = "Show List";
                this.lblRefByID1.Content = "";
                this.lblRefByID1.Tag = "";
                this.btnUpdateRefBy1.Tag = "New";
                this.lblRefByNameTitle1.Content = "DR.";
                this.txtRefByName1.Text = "";
                this.autoMktStaffRefSearch.SelectedValue = null;
                this.txtRefByQCTitle1.Text = "";
                this.txtRefByAddress1.Text = "";
                this.txtRefByPhone1.Text = "";
                this.txtRefByEMail1.Text = "";
                this.txtRefActive.Text = "A";
                this.txtRefMark.Text = "";
                this.cmbRefByTitle.SelectedIndex = 0;
                this.cmbRefByType.SelectedIndex = 0;
                this.btnUpdateRefBy1.IsEnabled = true;
                if (this.txtRefByName.Text.Trim().Length > 0)
                {
                    this.ShowRefRecordForEdit();

                }
                return;
            }
            this.stkpRefByEntry.Visibility = Visibility.Collapsed;
            this.stkpRefByList.Visibility = Visibility.Visible;
            this.stkpRefByList0.Visibility = Visibility.Visible;
            this.btnAddRefBy.Content = "Add/Edit";
        }

        private void ShowRefRecordForEdit()
        {
            string refbyID1 = this.txtRefByID.Text.Trim();
            if (refbyID1 == "000000000000")
                return;

            this.lblRefByID1.Content = refbyID1.Substring(6, 6);
            this.lblRefByID1.Tag = refbyID1;
            this.btnUpdateRefBy1.Tag = "Edit";
            var lst1 = this.RefByInfList.FindAll(x => x.refbyid == refbyID1);
            string refmktid1 = lst1[0].refmktid;
            this.autoMktStaffRefSearch.SelectedValue = null;
            if (refmktid1 != "000000000000")
            {
                this.autoMktStaffRefSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoMktStaffRefSearch.SelectedValue = lst1[0].refmktid;
            }
            this.lblRefByNameTitle1.Content = lst1[0].refbytitle;
            this.txtRefByName1.Text = lst1[0].refbynam.Trim();
            this.txtRefByQCTitle1.Text = lst1[0].refbyqc.Trim();
            this.txtRefByAddress1.Text = lst1[0].refbyadd.Trim();
            this.txtRefByPhone1.Text = lst1[0].phoneno.Trim();
            this.txtRefByEMail1.Text = lst1[0].emailid.Trim();
            this.txtRefActive.Text = lst1[0].refactive.Trim();
            this.txtRefMark.Text = lst1[0].refmark.Trim();
            string RefTitle1 = lst1[0].refbytitle.Trim();
            string RefType1 = lst1[0].refbytype.Trim();

            int idx1 = 0;
            foreach (var item in this.cmbRefByTitle.Items)
            {
                string item1 = ((ComboBoxItem)item).Tag.ToString();
                if (item1 == RefTitle1)
                {
                    this.cmbRefByTitle.SelectedIndex = idx1;
                    break;
                }
                idx1++;
            }

            idx1 = 0;
            foreach (var item in this.cmbRefByType.Items)
            {
                string item1 = ((ComboBoxItem)item).Tag.ToString();
                if (item1 == RefType1)
                {
                    this.cmbRefByType.SelectedIndex = idx1;
                    break;
                }
                idx1++;
            }
        }

        private void btnUpdateRefBy1_Click(object sender, RoutedEventArgs e)
        {
            string RefById1 = this.lblRefByID1.Tag.ToString().Trim();// "000000000000";
            string RefMktId1 = "000000000000";
            if(this.autoMktStaffRefSearch.SelectedValue != null)
                RefMktId1 = this.autoMktStaffRefSearch.SelectedValue.ToString();

            Hashtable Ref1 = new Hashtable();
            Ref1["REFBYID"] = RefById1;
            Ref1["REFMKTID"] = RefMktId1;
            Ref1["NAMTITLE"] = this.lblRefByNameTitle1.Content.ToString().Trim();
            Ref1["FULLNAME"] = this.txtRefByName1.Text.Trim().ToUpper();
            Ref1["QCTITLE"] = this.txtRefByQCTitle1.Text.Trim().ToUpper();
            Ref1["ADDRESS"] = this.txtRefByAddress1.Text.Trim();
            Ref1["PHONE"] = this.txtRefByPhone1.Text.Trim();
            Ref1["EMAIL"] = this.txtRefByEMail1.Text.Trim();
            Ref1["TYPE"] = ((ComboBoxItem)this.cmbRefByType.SelectedItem).Tag.ToString().Trim();
            Ref1["REFACTIVE"] = this.txtRefActive.Text.Trim();
            Ref1["REFMARK"] = this.txtRefMark.Text.Trim();

            DataSet ds1 = vm1.GetDataDetForRefByUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, Ref1: Ref1);
            var pap1 = vm1.SetParamUpdateRefByInfo(WpfProcessAccess.CompInfList[0].comcod, ds1, RefById1);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            string memonum = ds2.Tables[0].Rows[0]["refbyid"].ToString();
            this.lblRefByID1.Content = memonum.Substring(6, 6);
            this.lblRefByID1.Tag = memonum;
            this.btnUpdateRefBy1.Tag = "Saved";
            this.btnUpdateRefBy1.IsEnabled = false;
            this.btnRefreshRef_Click(null, null);
        }

        private void btnCloseRefBy_Click(object sender, RoutedEventArgs e)
        {
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
        }


        private void txtItemName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string srchTxt = this.txtItemName.Text.Trim().ToUpper();
            var lst1 = new List<HmsEntityCommercial.HmsServiceItem>();
            if (this.rbtnItemSearchStartpos.IsChecked == true)
                lst1 = this.ServiceItemList.FindAll(x => x.sirdesc.Trim().ToUpper().Substring(0, Math.Min(x.sirdesc.Trim().Length, srchTxt.Length)) == srchTxt).ToList();
            else
                lst1 = this.ServiceItemList.FindAll(x => x.sirdesc.Trim().ToUpper().Contains(srchTxt)).ToList();

            this.lstServiceItem.Items.Clear();
            foreach (var item in lst1)
            {
                this.lstServiceItem.Items.Add(new ListBoxItem()
                {
                    Content = item.sirdesc,
                    Tag = item.sircode,
                    ToolTip = item.sirdesc.Trim() + "Rate: " + item.saleprice.ToString("#,##0") + ", Main Group: " + item.gsirdesc.Trim()
                }
                );
            }
        }


        private bool ValidateForUpdate()
        {
            bool visible = true;
            visible = visible & (this.txtPatientName.Text.Trim().Length > 4);
            visible = visible & (this.iudAgeY.Value + this.iudAgeM.Value + this.iudAgeD.Value > 0);
            visible = visible & (this.txtContactNo.Text.Trim().Length > 3);
            if (this.chkRefBy.IsChecked == true)
                visible = visible & (this.txtRefByID.Text.Trim() != "000000000000");
            //else
            //    visible = visible & (this.txtRefRemarks.Text.Trim().Length > 0);

            return visible;
        }

        private void btnRefreshRef_Click(object sender, RoutedEventArgs e)
        {
            this.GetRefByInfList();
        }

        private void txtRefByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string srchTxt = this.txtRefByName.Text.Trim().ToUpper();
            var lst1 = this.RefByInfList.FindAll(x => (x.refbyid.Substring(6, 6) + x.rfFullName.Trim().ToUpper()).Contains(srchTxt));
            this.lstReferrer.Items.Clear();
            foreach (var item in lst1)
            {
                this.lstReferrer.Items.Add(new ListBoxItem()
                {
                    Content = item.refbyid.Substring(6, 6) + " - " + item.rfFullName.Trim(),
                    Tag = item.refbyid,
                    ToolTip = item.refbyid.Substring(6, 6) + " - " + item.rfFullName.Trim()
                }
                );
            }
            if (this.lstReferrer.Items.Count > 0)
                this.lstReferrer.SelectedIndex = 0;
        }


        private void btnPrintTrans_Click(object sender, RoutedEventArgs e)
        {
            string MemoNum1 = this.txtTransID.Tag.ToString().Trim();
            if (MemoNum1.Substring(13, 5) == "00000")
                return;

            if (this.btnUpdateTrans.IsEnabled == true)
                this.btnUpdateTrans_Click(null, null);

            string memoDate1 = this.xctk_dtpInvDat.Text.Trim();
            string PrnStat = (this.chkPrintDirect.IsChecked == true ? "DirectPrint" : "View");
            this.ViewPrintMemo(memoNum: MemoNum1, memoDate: memoDate1, ViewPrint: PrnStat);
        }

        private void btnUpdateTrans_Click(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
            Hashtable Pat1 = new Hashtable();
            Pat1["MEMONUM"] = this.txtTransID.Tag.ToString().Trim();
            Pat1["NAME"] = this.txtPatientName.Text.Trim();
            Pat1["AGEY"] = this.iudAgeY.Value;
            Pat1["AGEM"] = this.iudAgeM.Value;
            Pat1["AGED"] = this.iudAgeD.Value;
            Pat1["GENDRR"] = ((ComboBoxItem)this.cmbPatientGender.SelectedItem).Content.ToString();
            Pat1["PHONE"] = this.txtContactNo.Text.Trim();
            Pat1["MEMID"] = this.txtMemberID.Text.Trim();
            Pat1["DELTIM"] = this.xcdtDeliveryDT.Text.Trim();//.Substring(4);
            Pat1["REFBYID"] = this.txtRefByID.Text.Trim();
            Pat1["CCAMT"] = "0" + this.txtCCCharge.Text.Trim();
            Pat1["CCPAID"] = "0" + this.txtCCPaid.Text.Trim();
            Pat1["RMRKS"] = this.txtRemarks.Text.Trim();
            Pat1["REFRMRKS"] = this.txtRefRemarks.Text.Trim();
            Pat1["DUEAM"] = this.lblNetBalance.Content.ToString().Trim();
            Pat1["REFSTAFF"] = (this.autoStaffRefSearch.SelectedValue == null ? "000000000000" : this.autoStaffRefSearch.SelectedValue);
            Pat1["DISCTYPE"] = this.cmbDisType.Tag.ToString();
            Pat1["STATUS"] = "A";

            #region Convert image Source to byte[]
            //byte[] pbytes = null;
            string pimage = "";
            var bmp1 = this.imgPatPhoto.Source as BitmapImage;
            if (bmp1 != null)
            {
                MemoryStream outStream = new MemoryStream();
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bmp1));
                enc.Save(outStream);

                System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap(outStream);
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                pimage = Convert.ToBase64String((byte[])converter.ConvertTo(bitmap1, typeof(byte[])));
                //pbytes = Convert.FromBase64String(pimage);
            }
            #endregion
            Pat1["PATPHOTO"] = pimage;

            this.CollInfoList = this.CollInfoList.FindAll(x => x.bilcolam > 0 || x.comcod == "True");
            int xa = 1;
            foreach (var item in this.CollInfoList)
            {
                item.bilcolid = "C" + xa.ToString("000");
                xa++;
            }

            if (this.lblInvMode.Tag.ToString().Trim() != "NEW_INVOICE")
            {
                this.BackupCancelInvoice(this.txtTransID.Tag.ToString().Trim(), "BACKUP", this.lblInvMode.Tag.ToString().Trim());
                foreach (var item in this.OrderItemList)
                    this.SetDeliveryInfo(item);
            }

            string BrnCode1 = ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString();
            DataSet ds1 = vm1.GetDataSetForUpdate(CompCode: WpfProcessAccess.CompInfList[0].comcod, MemoDate1: DateTime.Parse(this.xctk_dtpInvDat.Text.ToString()),
                 BrnCod: BrnCode1, OrderItemList: this.OrderItemList, CollInfoList: this.CollInfoList, Pat1: Pat1,
                _preparebyid: this.preparebyid1, _prepareses: this.prepareses1, _preparetrm: this.preparetrm1);

            string UpdateType = this.lblInvMode.Tag.ToString();
            var pap1 = vm1.SetParamUpdateCommInvoice(WpfProcessAccess.CompInfList[0].comcod, ds1, UpdateType);
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON"); //Success
            //DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1, "XML");  //Success
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds2 == null)
                return;

            string memonum1 = ds2.Tables[0].Rows[0]["memonum1"].ToString();
            string memonum2 = ds2.Tables[0].Rows[0]["memonum2"].ToString();
            string memonum = ds2.Tables[0].Rows[0]["memonum"].ToString();
            this.txtTransID.Text = memonum2;
            this.txtTransID.Tag = memonum;

            xa = this.CollInfoList.Count();
            //this.CollInfoList.Add(new HmsEntityCommercial.CommInv01.CommInv01TblCol()
            //{
            //    comcod = "False",
            //    slnum = 0,
            //    ptinvnum = "",
            //    bilcolid = "C" + xa.ToString("000"),
            //    bilcoldat = DateTime.Now,
            //    bcnote = "",
            //    bilcolam = 0.00m,
            //    preparebyid = "",
            //    preparebynam = "NewRow",
            //    prepareses = "",
            //    preparetrm = "",
            //    rowid = 0
            //});

            this.dgvColInfo.ItemsSource = this.CollInfoList;
            this.dgvColInfo.Items.Refresh();

            this.btnUpdateTrans.Tag = "Saved";
            this.btnUpdateTrans.IsEnabled = false;
            this.stkpTitle2.IsEnabled = false;
            this.stkpFooter1.IsEnabled = false;
            this.stkpFooter2.IsEnabled = false;

            //this.btnPrintTrans.Visibility = Visibility.Visible;           
            //MessageBox.Show("Save Button Clicked");

            //if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
            //MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            //{
            //    return;
            //}


            if (sender != null && this.chkPrintDirect.IsChecked == true)
                this.btnPrintTrans_Click(null, null);
            else
                this.btnPrintTrans.Visibility = Visibility.Visible;

        }

        private void btnClosePatPhoto_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            //timer1.Stop();
            try
            {
                DrCam1.Dispose();
            }
            catch (Exception Exp)
            {

            }

            //this.windowsFormsHost1.Visibility = Visibility.Hidden;

            this.framePatientPhoto.Continue = false; // un-blocks gui message pump
        }

        private void btnPatPhoto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.imgPatPhotoCapture.Source = this.imgEmptyPhoto.Source;
                this.GridRefrrerList.Visibility = Visibility.Collapsed;
                this.GridTestItem.Visibility = Visibility.Collapsed;
                this.framePatientPhoto = new DispatcherFrame();
                this.GridDataEntry.IsEnabled = false;
                this.GridPatientPhoto.Visibility = Visibility.Visible;
                DrCam1 = new DrCapture(VIDEODEVICE, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, picturebox1);
                System.Windows.Threading.Dispatcher.PushFrame(this.framePatientPhoto);
                this.GridDataEntry.IsEnabled = true;
                this.GridPatientPhoto.Visibility = Visibility.Collapsed;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-03: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ViewPrintMemo(string memoNum = "CSI201611110100012", string memoDate = "01-Jan-2017", string ViewPrint = "View")
        {
            LocalReport rpt1 = null;
            string WindowTitle1 = "";

            string BrnCode1 = "%";
            string SignInID1 = "%";
            string StartDate1 = DateTime.Parse(memoDate).AddDays(-1).ToString("dd-MMM-yyy hh:mm tt");// this.xctk_dtpSrchDat1.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = DateTime.Parse(memoDate).AddDays(1).ToString("dd-MMM-yyy hh:mm tt"); //this.xctk_dtpSrchDat2.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = memoNum;//"CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = "COMMINVMEMO01N";
            var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: "NONE", OrderBy: OrderBy1);

            //var pap1 = vmr.SetParamCommInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            if (ds1 == null)
                return;

            string inputSource = ds1.Tables[1].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[1].Rows[0]["preparebynam"].ToString().Trim()
                              + ", " + ds1.Tables[1].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[1].Rows[0]["ServerTime"]), InputSource: inputSource);
            list3[0].RptFooter1 = list3[0].RptFooter1.Replace(" Source", "");

            var list1a = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01TblItem>();
            var list1b = ds1.Tables[1].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01GenInf>();
            var list1c = ds1.Tables[2].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01TblSum>();
            var list1d = ds1.Tables[3].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01TblCol>();

            var list1e = "";
            if (ds1.Tables[4].Rows.Count > 0)
            {
                if (!(ds1.Tables[4].Rows[0]["ptphoto"] is DBNull))
                {
                    byte[] imge1 = (byte[])ds1.Tables[4].Rows[0]["ptphoto"];
                    list1e = Convert.ToBase64String(imge1);
                }
            }

            var list1 = new List<Object>();
            list1.Add(list1a);
            list1.Add(list1b);
            list1.Add(list1c);
            list1.Add(list1d);
            list1.Add(list1e);

            rpt1 = CommReportSetup.GetLocalReport("Hospital.CommInv01", list1, null, list3);
            WindowTitle1 = "Commercial Invoice";

            if (ViewPrint == "View")
            {
                string RptDisplayMode = "PrintLayout";
                WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
            }
            else if (ViewPrint == "DirectPrint")
            {
                RdlcDirectPrint DirectPrint1 = new RdlcDirectPrint();
                DirectPrint1.PrintReport(rpt1);
                DirectPrint1.Dispose();
            }
        }

        private void cmbBranch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cmbBranch.SelectedItem == null || this.stkpTransList == null)
                return;

            this.cmbBranch.ToolTip = ((ComboBoxItem)this.cmbBranch.SelectedItem).Content.ToString();

            if (this.stkpTransList.Visibility == Visibility.Visible)
                this.btnFilter1_Click(null, null);
        }

        private void cmbRefByTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.lblRefByNameTitle1 == null)
                return;

            if (this.cmbRefByTitle.SelectedItem == null)
                return;
            this.lblRefByNameTitle1.Content = ((ComboBoxItem)this.cmbRefByTitle.SelectedItem).Tag.ToString().Trim();
        }

        private void btnTakePatPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DrCam1 == null)
                    return;
                if (DrCam1.Width <= 0)
                    return;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                // Release any previous buffer
                if (m_ip != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(m_ip);
                    m_ip = IntPtr.Zero;
                }

                // capture image
                m_ip = DrCam1.Click();
                //System.Drawing.Bitmap b = new System.Drawing.Bitmap(DrCam1.Width, DrCam1.Height, DrCam1.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, m_ip);
                System.Drawing.Bitmap bitmap1 = new System.Drawing.Bitmap(DrCam1.Width, DrCam1.Height, DrCam1.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, m_ip);

                // If the image is upsidedown
                bitmap1.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
                //------------
                Bitmap bitmap2 = HmsImageManager.ResizeImaze(bitmap1, 320, 240);

                //// This following lines transfer to the  function "HmsImageManager.ResizeImaze"
                ////Bitmap bitmap2 = new Bitmap(320, 240); // Bitmap(640,480);

                ////double ratioX = (double)bitmap2.Width / (double)bitmap1.Width;
                ////double ratioY = (double)bitmap2.Height / (double)bitmap1.Height;
                ////double ratio = ratioX < ratioY ? ratioX : ratioY;

                ////int newHeight = Convert.ToInt32(bitmap1.Height * ratio);
                ////int newWidth = Convert.ToInt32(bitmap1.Width * ratio);

                ////using (Graphics g = Graphics.FromImage(bitmap2))
                ////{
                ////    g.DrawImage(bitmap1, 0, 0, newWidth, newHeight);
                ////}


                BitmapImage bitmapImage = new BitmapImage();

                using (var stream = new MemoryStream())
                {
                    //bitmap1.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);      
                    //bitmap1.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    bitmap2.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    stream.Seek(0, SeekOrigin.Begin);

                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }

                bitmapImage.Freeze();


                this.imgPatPhotoCapture.Dispatcher.Invoke((Action)(() => this.imgPatPhotoCapture.Source = bitmapImage));

                //bitmapImage.SetResolution(200, 200);
                this.imgPatPhoto.Source = this.imgPatPhotoCapture.Source;
                if (this.chkAutoClose.IsChecked == true)
                    this.btnClosePatPhoto_Click(null, null);

                //System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                ////timer1.Stop();
                //DrCam1.Dispose();
                ////this.windowsFormsHost1.Visibility = Visibility.Hidden;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-05: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DrCam1 != null)
                    DrCam1.Dispose();
            }
            catch (Exception Exp)
            {

            }
        }

        #region CommercialMemoEdit

        private void MemoEditView(string memoNum = "CSI201611110100012")
        {
            string[] month1 = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string memoDate = "01-" + month1[int.Parse(memoNum.Substring(7, 2))] + "-" + memoNum.Substring(3, 4) + " 12:00 AM";// DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");

            string BrnCode1 = "%";
            string SignInID1 = "%";
            string StartDate1 = DateTime.Parse(memoDate).AddDays(-1).ToString("dd-MMM-yyy hh:mm tt");// this.xctk_dtpSrchDat1.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = DateTime.Parse(memoDate).AddDays(32).ToString("dd-MMM-yyy hh:mm tt"); //this.xctk_dtpSrchDat2.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = memoNum;//"CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = "COMMINVMEMO01N";

            var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                                           InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: "NONE", OrderBy: OrderBy1);

            //var pap1 = vmr.SetParamCommInvoice(WpfProcessAccess.CompInfList[0].comcod, memoNum);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            if (ds1 == null)
                return;

            if (ds1.Tables.Count == 0)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            this.CleanUpScreen();
            this.dgvMemo.Columns[10].Width = 120;
            this.dgvMemo.Columns[11].Visibility = Visibility.Visible;
            this.stkpPrint.Visibility = Visibility.Visible;
            foreach (var item in this.cmbBranch.Items)
            {
                if (((ComboBoxItem)item).Tag.ToString().Trim() == memoNum.Substring(9, 4))
                {
                    this.cmbBranch.SelectedItem = item;
                    break;
                }
            }
            this.txtPrevTransID.Text = memoNum.Substring(13, 5);

            bool foundYM1 = false;
            foreach (var item in this.cmbPrevYearMon.Items)
            {
                if (((ComboBoxItem)item).Tag.ToString().Trim() == memoNum.Substring(3, 6))
                {
                    foundYM1 = true;
                    this.cmbPrevYearMon.SelectedItem = item;
                    break;
                }
            }

            if (foundYM1 == false)
            {
                ComboBoxItem cbi1 = new ComboBoxItem() { Content = memoNum.Substring(5, 4), Tag = memoNum.Substring(3, 6), Width = 50, HorizontalContentAlignment = HorizontalAlignment.Left };
                this.cmbPrevYearMon.Items.Add(cbi1);
                this.cmbPrevYearMon.SelectedItem = cbi1;
            }

            this.dgvColInfo.ItemsSource = null;

            this.chkBranchName.IsChecked = false;
            this.btnUpdateTrans.Tag = "Edit";

            this.lstServiceItem.Items.Clear();
            DataRow dr1 = ds1.Tables[1].Rows[0];
            DataRow dr5 = ds1.Tables[4].Rows[0];

            this.xctk_dtpInvDat.Value = Convert.ToDateTime(dr1["ptinvdat"]);
            //this.xctk_dtpInvDat.Tag = Convert.ToDateTime(dr1["ptinvdat"]).ToString("dd-MMM-yyyy hh:mm tt");

            this.txtTransID.Text = dr1["ptinvnum2"].ToString();
            this.txtTransID.Tag = dr1["ptinvnum"].ToString();
            this.txtPatientName.Text = dr1["ptname"].ToString();
            this.iudAgeY.Value = int.Parse(dr5["ptagey"].ToString());
            this.iudAgeM.Value = int.Parse(dr5["ptagem"].ToString());
            this.iudAgeD.Value = int.Parse(dr5["ptaged"].ToString());
            string ptgen1 = dr1["ptgender"].ToString().ToUpper().Substring(0, 1);
            this.cmbPatientGender.SelectedIndex = (ptgen1 == "M" ? 0 : (ptgen1 == "F" ? 1 : 2));
            this.txtContactNo.Text = dr1["ptphone"].ToString();
            this.txtMemberID.Text = dr1["refcardno"].ToString(); 
            this.txtRefByID.Text = dr1["refbyid"].ToString();
            this.txtRefByName.Text = dr1["refbyid"].ToString().Substring(6) + " - " + dr1["rfFullName"].ToString();
            this.txtRefByName.ToolTip = "Ref. By ID : " + dr1["refbyid"].ToString().Substring(6) + " - " + dr1["rfFullName"].ToString();
            this.xcdtDeliveryDT.Value = Convert.ToDateTime(dr1["delivartime"]);
            this.txtCCCharge.Text = Convert.ToDecimal(dr1["cccharge"]).ToString("#,##0;(#,##0); ");
            this.txtCCPaid.Text = Convert.ToDecimal(dr1["ccpaidam"]).ToString("#,##0;(#,##0); ");
            this.txtRemarks.Text = dr1["ptinvnote"].ToString();
            this.txtRefRemarks.Text = dr1["ptrefnote"].ToString();
            this.preparebyid1 = dr1["preparebyid"].ToString();
            this.prepareses1 = dr1["prepareses"].ToString();
            this.preparetrm1 = dr1["preparetrm"].ToString();

            this.cmbDisType.SelectedIndex = int.Parse(dr1["disctype"].ToString().Trim().Substring(0, 1));
            this.cmbDisType.Tag = dr1["disctype"].ToString().Trim();

            string RefStaff1 = dr1["refstaffid"].ToString();
            if (RefStaff1 != "000000000000")
            {
                this.chkStaffRef.IsChecked = true;
                this.autoStaffRefSearch.IsEnabled = true;
                this.autoStaffRefSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoStaffRefSearch.SelectedValue = RefStaff1;
            }

            this.chkRefBy.IsChecked = (this.txtRefByID.Text == "000000000000" ? false : true);
            this.chkRefBy_Click(null, null);

            if (!(dr5["ptphoto"] is DBNull))
            {
                byte[] byteSi = (byte[])dr5["ptphoto"];
                MemoryStream mem1 = new MemoryStream(byteSi);
                if (mem1.Length > 0)
                {
                    BitmapImage bmp4 = new BitmapImage();
                    bmp4.BeginInit();
                    bmp4.StreamSource = mem1;
                    bmp4.EndInit();
                    this.imgPatPhoto.Source = bmp4;
                }
            }

            //---------------------------------------
            // select slnum = row_number() over (order by gsircode, isircode), comcod, ptinvnum, gsircode, gsirdesc, isircode, isirdesc, isirunit, itemqty, itmam, idisam, inetam, ivatam, rowid from #tblinvm2 order by gsircode, isircode;

            //select slnum = row_number() over(order by gsircode, isircode, reptsl), comcod, ptinvnum, gsircode, gsirdesc, isircode, reptsl, isirdesc, isirunit, isirtype, itemqty, itmrate, itmam, 
            //idisam, inetam, icomam, icdisam, refscomp, ivatam, rowid, orderbyid, orderbyses, orderbyterm, ordertime, rptdocid, rptlogbyid, rptlogbyses, rptlogbyterm,
            //delivbyid, delivbyses, delivbyterm, delivtime, icsmark, itemrmrk from #tblinvm2 
            //order by gsircode, isircode, reptsl;

            foreach (DataRow row1 in ds1.Tables[0].Rows)
            {
                this.OrderItemList.Add(new vmEntryFrontDesk1.OrderItem()
                {
                    slnum = Convert.ToInt32(row1["slnum"]).ToString("00") + ".",
                    gsircode = row1["gsircode"].ToString(), // serviceItem1.gsircode,
                    isircode = row1["isircode"].ToString(), // serviceItem1.sircode,
                    reptsl = row1["reptsl"].ToString(),
                    gsirdesc = row1["gsirdesc"].ToString().Trim(), // + ", " + row1["isirdesc"].ToString().Trim(), // serviceItem1.gsirdesc,
                    sirdesc = row1["isirdesc"].ToString(), // serviceItem1.sirdesc,
                    sirunit = row1["isirunit"].ToString(), // serviceItem1.sirunit,
                    sirtype = row1["isirtype"].ToString(), // serviceItem1.sirtype,
                    itemqty = Convert.ToDecimal(row1["itemqty"]), // 1.00m,
                    salrate = Convert.ToDecimal(row1["itmrate"]), //serviceItem1.saleprice,
                    salam = Convert.ToDecimal(row1["itmam"]), // serviceItem1.saleprice,
                    idisam = Convert.ToDecimal(row1["idisam"]), // 0.00m,
                    idisper = "",
                    refscomp = Convert.ToDecimal(row1["refscomp"]), // serviceItem1.refscomp,
                    refscompstd = Convert.ToDecimal(row1["refscompstd"]), // serviceItem1.refscomp,
                    refpermark = (Convert.ToDecimal(row1["refscomp"]) > 0 ? "%" : ""),
                    icomam = Convert.ToDecimal(row1["icomam"]), //0.00m,
                    icdisam = Convert.ToDecimal(row1["icdisam"]), // 0.00m,
                    inetam = Convert.ToDecimal(row1["inetam"]), // serviceItem1.saleprice,
                    ivatam = Convert.ToDecimal(row1["ivatam"]), // 0.00m
                    readmode = (this.lblInvMode.Tag.ToString().Trim() == "FULL_EDIT_INVOICE" ? "False" : "True"),
                    orderbyid = row1["orderbyid"].ToString(),
                    orderbyses = row1["orderbyses"].ToString(),
                    orderbyterm = row1["orderbyterm"].ToString(),
                    ordertime = DateTime.Parse(row1["ordertime"].ToString()),
                    rptdocid = row1["rptdocid"].ToString(),
                    rptlogbyid = row1["rptlogbyid"].ToString(),
                    rptlogbyses = row1["rptlogbyses"].ToString(),
                    rptlogbyterm = row1["rptlogbyterm"].ToString(),
                    delivbyid = row1["delivbyid"].ToString(),
                    delivbyses = row1["delivbyses"].ToString(),
                    delivbyterm = row1["delivbyterm"].ToString().Trim(),
                    delivered = (row1["delivbyid"].ToString().Trim().Length == 12 && row1["delivbyid"].ToString() != "000000000000" ? true : false),
                    delivtime = DateTime.Parse(row1["delivtime"].ToString()),
                    icsmark = row1["icsmark"].ToString().Trim(),
                    itemrmrk = row1["itemrmrk"].ToString().Trim(),
                    newold = "OldRow"
                });
            }

            if (this.OrderItemList.FindAll(x => x.delivered == false).Count == 0)
                this.chkDghDelivered.IsChecked = true;

            if (this.lblInvMode.Tag.ToString().Trim() == "DUE_COLL_INVOICE")
            {
                this.dgvMemo.Columns[10].Width = 190;
                this.dgvMemo.Columns[11].Visibility = Visibility.Collapsed;
            }

            this.dgvMemo.Items.Refresh();
            this.CollInfoList.Clear();
            // select slnum, comcod, ptinvnum, bilcolid, bilcoldat, bilcolam, bcnote, preparebyid, preparebynam, prepareses, preparetrm, rowid, rowtime from #tblinvm3 order by slnum;
            this.CollInfoList = ds1.Tables[3].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01TblCol>();
            this.CollInfoList = this.CollInfoList.FindAll(x => !x.bilcolid.Contains("CC"));
            this.CollInfoList.Sort(delegate(HmsEntityCommercial.CommInv01.CommInv01TblCol x, HmsEntityCommercial.CommInv01.CommInv01TblCol y)
            {
                return (x.bilcolid).CompareTo(y.bilcolid);
            });

            //this.CollInfoList = this.CollInfoList.FindAll(x => !x.bilcolid.Contains("CC")).OrderBy(y=> y.bilcolid).ToList();
            foreach (var item in this.CollInfoList)
            {
                item.comcod = (this.lblInvMode.Tag.ToString().Trim() == "FULL_EDIT_INVOICE" ? "False" : "True");
                item.preparebynam = "OldRow";
            }
            string collid1 = "C" + (this.CollInfoList.Count + 1).ToString("000");
            this.CollInfoList.Add(new HmsEntityCommercial.CommInv01.CommInv01TblCol()
            {
                comcod = "False",
                slnum = this.CollInfoList.Count,
                ptinvnum = memoNum,
                bilcolid = collid1,
                bilcoldat = DateTime.Now,
                bcnote = "",
                paidby = "CASH",
                vounum = "",
                bilcolam = 0.00m,
                preparebyid = WpfProcessAccess.SignedInUserList[0].hccode,
                preparebynam = "NewRow",
                prepareses = WpfProcessAccess.SignedInUserList[0].sessionID,
                preparetrm = WpfProcessAccess.SignedInUserList[0].terminalID,
                rowid = 0
            });

            this.dgvColInfo.ItemsSource = this.CollInfoList;
            this.dgvColInfo.Items.Refresh();



            this.btnTotal_Click(null, null);
            this.txtItemName.Focus();

            this.stkpTitle1.IsEnabled = false;
            this.stkpTitle2.Visibility = Visibility.Visible;
            this.stkpEntry.Visibility = Visibility.Visible;
            this.stkpEntry.IsEnabled = true;
            this.stkpPhoto.Visibility = Visibility.Visible;
            this.btnPatPhoto.IsEnabled = true;
            //this.btnFindPatient.Visibility = Visibility.Hidden;
            this.btnUpdateTrans.Visibility = Visibility.Visible;
            this.btnPrintTrans.Visibility = Visibility.Visible;

            this.btnNewShow.Content = "_Next";
            /*
            // Report Delivery Mode // Due Collection Entry Mode
            if ((this.lblInvMode.Tag.ToString().Trim() == "REPORT_DELIVERY_INVOICE") || (this.lblInvMode.Tag.ToString().Trim() == "DUE_COLL_INVOICE")) 
            {
                this.stkpTitle2.IsEnabled = false;
                this.stkpItemAdd.IsEnabled = false;
                this.stkpFooter1.IsEnabled = true;
                this.stkpFooter2.IsEnabled = true;
            }
            */
            //            this.txtPatientName.Focus();
        }
        #endregion

        private void btnUploadPatPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long threshold = 400000L;
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

                        Bitmap bitmap2 = HmsImageManager.ResizeImaze(bmp, 320, 240);

                        TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                        string image = Convert.ToBase64String((byte[])converter.ConvertTo(bitmap2, typeof(byte[])));

                        // byte to image Convert
                        byte[] bytes = Convert.FromBase64String(image);
                        MemoryStream mem = new MemoryStream(bytes);
                        BitmapImage bmp2 = new BitmapImage();
                        bmp2.BeginInit();
                        bmp2.StreamSource = mem;
                        bmp2.EndInit();

                        this.imgPatPhotoCapture.Source = bmp2;
                        this.imgPatPhoto.Source = this.imgPatPhotoCapture.Source;
                        if (this.chkAutoClose.IsChecked == true)
                            this.btnClosePatPhoto_Click(null, null);
                    }
                }
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-06: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void btnSavePatPhoto_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"C:\Temps\PatientPhoto_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".jpg";// ".png"; // ".tif";//
            var encoder = new JpegBitmapEncoder(); //TiffBitmapEncoder();//  PngBitmapEncoder(); //
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)this.imgPatPhotoCapture.Source));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                encoder.Save(stream);
        }

        private void btnNav_Click(object sender, RoutedEventArgs e)
        {

            if (this.dgvMemo.Items.Count == 0)
                return;

            if (this.dgvMemo.SelectedIndex < 0)
                this.dgvMemo.SelectedIndex = 0;

            string InvMode1 = this.lblInvMode.Tag.ToString();
            string ActtionName = ((Button)sender).Name.ToString().Trim();
            int index1 = this.dgvMemo.SelectedIndex;
            var item1 = (vmEntryFrontDesk1.OrderItem)this.dgvMemo.SelectedItem;

            //this.OrderItemList.Add(new vmEntryFrontDesk1.OrderItem()   newold = "NewRow"
            if (ActtionName == "btnDelete" && (InvMode1 == "NEW_INVOICE" || InvMode1 == "FULL_EDIT_INVOICE" || item1.newold == "NewRow"))
            {

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to delete item\n" + this.OrderItemList[index1].slnum + " " + this.OrderItemList[index1].sirdesc.Trim(),
                                    WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;
                this.OrderItemList[index1].itemqty = 0;
                this.btnTotal_Click(null, null);
                if (this.OrderItemList.Count > 0)
                {
                    this.dgvMemo.SelectedIndex = (this.OrderItemList.Count <= index1 ? this.OrderItemList.Count - 1 : index1);
                }
                return;
            }
            switch (ActtionName)
            {
                case "btnTop":
                    index1 = 0;
                    break;
                case "btnPrev":
                    index1 = this.dgvMemo.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnNext":
                    index1 = this.dgvMemo.SelectedIndex + 1;
                    if (index1 >= this.dgvMemo.Items.Count)
                        index1 = this.dgvMemo.Items.Count - 1;
                    break;
                case "btnBottom":
                    index1 = this.dgvMemo.Items.Count - 1;
                    break;
            }
            this.dgvMemo.SelectedIndex = index1;

            var item21 = (vmEntryFrontDesk1.OrderItem)this.dgvMemo.Items[index1];
            this.dgvMemo.ScrollIntoView(item21);
        }

        private void dgvMemo_GotFocus(object sender, RoutedEventArgs e)
        {
            this.HideRefItemList1();
        }

        private void HideRefItemList1()
        {
            this.GridRefrrerList.Visibility = Visibility.Collapsed;
            this.GridTestItem.Visibility = Visibility.Collapsed;
        }
        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            this.HideRefItemList1();
        }

        private void btnTransList_Click(object sender, RoutedEventArgs e)
        {
            this.txtSrchPatName.Text = "";
            this.txtSrchPhoneNo.Text = "";
            this.txtSrchInvNo.Text = "";

            this.HideRefItemList1();
            if (this.stkpTransList.Visibility == Visibility.Visible)
            {
                this.stkpTransList.Visibility = Visibility.Collapsed;
                this.stkpGridTransList.IsEnabled = false;
                if (this.btnNewShow.Content.ToString() == "_Next")
                {
                    this.stkpEntry.Visibility = Visibility.Visible;
                    this.stkpEntry.IsEnabled = true;
                    this.stkpTitle2.Visibility = Visibility.Visible;
                    this.stkpPhoto.Visibility = Visibility.Visible;
                }
                this.btnTransList.Content = "Trans. List";
                return;
            }
            this.stkpEntry.Visibility = Visibility.Collapsed;
            this.stkpEntry.IsEnabled = false;
            this.stkpTitle2.Visibility = Visibility.Hidden;
            this.stkpPhoto.Visibility = Visibility.Hidden;
            this.stkpTransList.Visibility = Visibility.Visible;
            this.stkpGridTransList.IsEnabled = true;
            this.btnTransList.Content = "Hide Tr.List";
            this.btnFilter1_Click(null, null);
        }

        private void btnFilter1_Click(object sender, RoutedEventArgs e)
        {
            this.stkpGridTransList.IsEnabled = false;
            this.dgvTransList.ItemsSource = null;
            this.CommInvSummList = null;
            string BrnCode1 = ((ComboBoxItem)this.cmbBranch.SelectedItem).Tag.ToString().Substring(0, 4);
            string SignInID1 = (this.chkFilterUser.IsChecked == true ? "%" : WpfProcessAccess.SignedInUserList[0].hccode);
            string StartDate1 = this.xctk_dtpSrchDat1.Text; // DateTime.Today.AddDays(-60).ToString("dd-MMM-yyyy");
            string EndDate1 = this.xctk_dtpSrchDat2.Text; // DateTime.Today.ToString("dd-MMM-yyyy");
            string InvNum1 = "CSI";
            string InvStatus1 = "A";
            string TerminalName1 = "%";
            string SessionID1 = "%";
            string OrderBy1 = "DESCENDING";
            string RptProcID1 = "COMMINVLIST01";

            //var pap1 = vmr.SetParamCommInvSummList(WpfProcessAccess.CompInfList[0].comcpcod, BrnCode, StartDate, EndDate, "CSI", SignInID, "A", "%", "%");

            var pap1 = vmr.SetParamFrontDeskReport(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: RptProcID1, BrnchCod: BrnCode1, startDate: StartDate1, EndDate: EndDate1,
                                             InvNum: InvNum1, PreparedBy: SignInID1, InvStatus: InvStatus1, TerminalName: TerminalName1, SessionID: SessionID1, Options: "NONE", OrderBy: OrderBy1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.CommInvSummList = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInvSummInf>();

            string paName1 = this.txtSrchPatName.Text.Trim().ToUpper();
            string phone1 = this.txtSrchPhoneNo.Text.Trim().ToUpper();
            string invnum2 = this.txtSrchInvNo.Text.Trim();
            if (paName1.Length > 0)
                this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptname.Contains(paName1)).ToList();

            if (phone1.Length > 0)
                this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptphone.Contains(phone1)).ToList();

            if (invnum2.Length > 0)
                this.CommInvSummList = this.CommInvSummList.FindAll(x => x.ptinvnum2.Contains(invnum2)).ToList();

            /*
             * 
             * 
               pap1.ProcID = ProcessID; //  "COMMINVMEMO01N" // "COMMINVLIST01" // "COMMINVDETAILS01" // "GROUPDETAILS01" ;
            pap1.parm01 = BrnchCod;
            pap1.parm02 = startDate;
            pap1.parm03 = EndDate;
            pap1.parm04 = InvNum;
            pap1.parm05 = PreparedBy;
            pap1.parm06 = InvStatus;
            pap1.parm07 = TerminalName;
            pap1.parm08 = SessionID;
            pap1.parm11 = OrderBy;
             
             */


            int slnum1 = 1;
            foreach (var item in this.CommInvSummList)
            {
                item.slnum = slnum1;
                ++slnum1;
            }
            //this.CommInvSummList.Sort(delegate(HmsEntityCommercial.CommInvSummInf x, HmsEntityCommercial.CommInvSummInf y)
            //{
            //    return (x.ptinvnum).CompareTo(y.ptinvnum);
            //});

            this.dgvTransList.ItemsSource = this.CommInvSummList;
            this.dgvTransList.Items.Refresh();
            this.stkpGridTransList.IsEnabled = true;
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            this.dgvTransList_MouseDoubleClick(null, null);
        }

        private void dgvTransList_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).Uid.ToString().Contains("_INVOICE"))
            {
                Button btn1 = new Button() { Tag = ((MenuItem)sender).Tag.ToString() };
                this.btnEdit1_Click(btn1, e);
            }
            else if (((MenuItem)sender).Uid.ToString().Contains("_PREVIEW"))
            {
                this.dgvTransList_MouseDoubleClick(null, null);
            }
            else if (((MenuItem)sender).Uid.ToString().Contains("_DELETE"))
            {
                this.btnDelete1_Click(null, null);
            }
        }
        private void btnEdit1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.CommInvSummList.Count == 0)
                    return;

                if (this.dgvTransList.SelectedItem == null)
                    return;
                int tag1 = int.Parse("0" + ((Button)sender).Tag.ToString());
                this.cmbInvMode.SelectedIndex = tag1;// 4; // FULL_EDIT_INVOICE
                string MemoNum = ((HmsEntityCommercial.CommInvSummInf)this.dgvTransList.SelectedItem).ptinvnum;
                this.MemoEditView(memoNum: MemoNum);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-12.B: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, 
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnDelete1_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (this.CommInvSummList.Count == 0)
                    return;

                if (this.dgvTransList.SelectedItem == null)
                    return;

                var item1 = (HmsEntityCommercial.CommInvSummInf)this.dgvTransList.SelectedItem;
                string MemoNum = item1.ptinvnum; //((ListBoxItem)this.lstPrevTransList.SelectedItem).Tag.ToString();

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to permanently cancel the\n" +
                       "Invoice No : " + item1.ptinvnum2 + ", Date : " + item1.ptinvdat.ToString("dd-MMM-yyyy ddd hh:mm tt"),
                                 WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;

                this.BackupCancelInvoice(MemoNum, "CANCEL", "CANCEL_INVOICE");
                this.btnFilter1_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-15: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, 
                    MessageBoxOptions.DefaultDesktopOnly);
            }

            //System.Windows.MessageBox.Show("Delete operation is under construction\n--- Thank you\n    System Admin", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, 
            //    MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }
        private void dgvTransList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (this.CommInvSummList.Count == 0)
                    return;

                if (this.dgvTransList.SelectedItem == null)
                    return;

                string MemoNum = ((HmsEntityCommercial.CommInvSummInf)this.dgvTransList.SelectedItem).ptinvnum; //((ListBoxItem)this.lstPrevTransList.SelectedItem).Tag.ToString();
                string memoType1 = ((ComboBoxItem)this.cmbPrnMemoType2.SelectedItem).Tag.ToString();
                string memoDate1 = ((HmsEntityCommercial.CommInvSummInf)this.dgvTransList.SelectedItem).ptinvdat.ToString("dd-MMM-yyyy hh:mm tt");
                this.ViewPrintMemo(memoNum: MemoNum, memoDate: memoDate1);
                //this.ViewPrintMemo(MemoNum, PrnOpt1, "", memoType1);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-13: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void lblRefByNameTitle1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.cmbRefByTitle.IsDropDownOpen = true;
        }

        private void cmbInvMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.lblInvMode == null)
                return;

            if (this.cmbInvMode.SelectedItem == null)
                return;

            TextBlock tbMode1 = (TextBlock)this.cmbInvMode.SelectedItem;
            this.lblInvMode.Content = tbMode1.Text.Trim() + " : ";
            this.lblInvMode.Tag = tbMode1.Tag.ToString();
            // Tag Value 0 = "NEW_INVOICE, 1 = "REPORT_DELIVERY_INVOICE", 2 = "DUE_COLL_INVOICE", 3 = "PART_EDIT_INVOICE", 4 = "FULL_EDIT_INVOICE"
            this.lblInvMode.Background = tbMode1.Background;
            this.lblInvMode.Foreground = tbMode1.Foreground;

            this.cmbPrevYearMon.SelectedIndex = 0;
            this.stkpEditPrevInv.IsEnabled = (this.lblInvMode.Tag.ToString().Trim() != "NEW_INVOICE");// (this.cmbInvMode.SelectedIndex > 0);
            this.txtPrevTransID.Text = (this.lblInvMode.Tag.ToString().Trim() == "NEW_INVOICE" ? "" : this.txtPrevTransID.Text.Trim());

            if (this.txtPrevTransID.IsEnabled == true)
                this.txtPrevTransID.Focus();
            else
                this.txtPatientName.Focus();

        }

        private void lblInvMode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.cmbInvMode.IsDropDownOpen = true;
        }
        private void chkRefBy_Click(object sender, RoutedEventArgs e)
        {
            this.HideRefItemList1();
            this.txtRefByName.Visibility = (this.chkRefBy.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
            this.txtRefRemarks.Visibility = (this.chkRefBy.IsChecked == false ? Visibility.Visible : Visibility.Collapsed);
        }
        private void txtRefRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.txtItemName0.Focusable = true;
                this.txtItemName0.Focus();
            }
            else if (e.Key == Key.Tab)
                this.txtItemName.Focus();
        }
        private void txtPrevTransID_LostFocus(object sender, RoutedEventArgs e)
        {
            this.txtPrevTransID.Text = (this.txtPrevTransID.Text.Trim().Length > 0 ? ASITUtility.Right("00000" + this.txtPrevTransID.Text.Trim(), 5) : "");
        }
        private void btnPaidAmt_Click(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }
        private void dgvColInfo_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }
        private void imgMnuAddPhoto_Click(object sender, RoutedEventArgs e)
        {
            this.btnPatPhoto_MouseDoubleClick(null, null);
        }
        private void imgMnuRemovePhoto_Click(object sender, RoutedEventArgs e)
        {
            this.imgPatPhoto.Source = this.imgEmptyPhoto.Source;
            this.imgPatPhotoCapture.Source = this.imgEmptyPhoto.Source;
        }
        private void xctk_dtpInvDat_LostFocus(object sender, RoutedEventArgs e)
        {
            bool foundYM2 = false;
            string ym1 = DateTime.Parse(this.xctk_dtpInvDat.Text).ToString("yyyyMM");
            foreach (var item in this.cmbPrevYearMon.Items)
            {
                if (((ComboBoxItem)item).Tag.ToString().Trim() == ym1)
                {
                    foundYM2 = true;
                    this.cmbPrevYearMon.SelectedItem = item;
                    break;
                }
            }

            if (foundYM2 == false)
            {
                ComboBoxItem cbi2 = new ComboBoxItem() { Content = ym1.Substring(2, 4), Tag = ym1, Width = 50, HorizontalContentAlignment = HorizontalAlignment.Left };
                this.cmbPrevYearMon.Items.Add(cbi2);
                this.cmbPrevYearMon.SelectedItem = cbi2;
            }
        }
        private void txtbdgPaidBy_Click(object sender, RoutedEventArgs e)
        {
            this.dgvColInfo.ItemsSource = null;
            MenuItem mi1 = (MenuItem)sender;
            int index1 = int.Parse(mi1.Tag.ToString());
            this.CollInfoList[index1].paidby = mi1.Header.ToString();
            this.dgvColInfo.ItemsSource = this.CollInfoList;
        }
        private void chkDghDelivered_Click(object sender, RoutedEventArgs e)
        {
            bool chkVal1 = (((CheckBox)sender).IsChecked == true);
            foreach (var item in this.OrderItemList)
            {
                item.delivered = chkVal1;
                this.SetDeliveryInfo(item);
                //item.delivbyid = (chkVal1 ? WpfProcessAccess.SignedInUserList[0].hccode : "000000000000");
                //item.delivbyses = (chkVal1 ? WpfProcessAccess.SignedInUserList[0].sessionID : "000000");
                //item.delivbyterm = (chkVal1 ? WpfProcessAccess.SignedInUserList[0].terminalID : "");
                //item.delivtime = (chkVal1 ? DateTime.Now : DateTime.Parse("01-Jan-1900"));
            }
            this.dgvMemo.Items.Refresh();
        }
        private void SetDeliveryInfo(vmEntryFrontDesk1.OrderItem item)
        {
            if (item.delivered)
            {
                if (item.delivbyid == "000000000000")
                {
                    item.delivbyid = WpfProcessAccess.SignedInUserList[0].hccode;
                    item.delivbyses = WpfProcessAccess.SignedInUserList[0].sessionID;
                    item.delivbyterm = WpfProcessAccess.SignedInUserList[0].terminalID;
                    item.delivtime = DateTime.Now;
                }
            }
            else
            {
                item.delivbyid = "000000000000";
                item.delivbyses = "";
                item.delivbyterm = "";
                item.delivtime = DateTime.Parse("01-Jan-1900");
            }
        }
        private void dgDudQty_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }
        private void dgtxtDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }
        private void txtPatientName_LostFocus(object sender, RoutedEventArgs e)
        {
            string namt1 = this.txtPatientName.Text.Trim();
            if (namt1.Length > 5)
                this.cmbPatientGender.SelectedIndex = (namt1.Substring(0, 3) == "MS." || namt1.Substring(0, 4) == "MRS." || namt1.Substring(0, 4) == "BABY" ? 1 : 0);
        }
        private void chkStaffRef_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked1 = (this.chkStaffRef.IsChecked == true);
            this.autoStaffRefSearch.IsEnabled = isChecked1;
            if (this.chkStaffRef.IsEnabled == true)
                this.autoStaffRefSearch.Focus();

        }
        private void autoStaffRefSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStaffRefSirdesc(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetStaffRefSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
               WpfProcessAccess.StaffList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }
        private void txtDiscTotal_LostFocus(object sender, RoutedEventArgs e)
        {
            this.iudDisPer.Value = 0;
            decimal disamt1 = decimal.Parse("0" + this.txtDiscTotal.Text.Trim());
            var DiscItems = this.OrderItemList.FindAll(x => x.isircode.Substring(0, 7) != "4502905").ToList();
            decimal sumVal = 0.00m;
            if (DiscItems.Count > 0)
            {
                sumVal = DiscItems.Sum(y => Math.Round(y.salrate * y.itemqty, 0));
                this.iudDisPer.Value = double.Parse(Math.Round((disamt1 / sumVal) * 100m, 0).ToString());
            }

        }
        private void iudDisPer_Spinned(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            if (this.iudDisPer.Value == null)
                this.iudDisPer.Value = 0;
            decimal spinval1 = decimal.Parse("0" + this.iudDisPer.Value.ToString());
            var DiscItems = this.OrderItemList.FindAll(x => x.isircode.Substring(0, 7) != "4502905").ToList();
            decimal sumVal = 0.00m;
            if (DiscItems.Count > 0)
            {
                sumVal = DiscItems.Sum(y => Math.Round(y.salrate * y.itemqty, 0));
                this.txtDiscTotal.Text = Math.Round((spinval1 / 100.00m) * sumVal, 0).ToString("#");
            }
        }
        private void iudDisPer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.iudDisPer_Spinned(null, null);
            this.txtDiscTotal_MouseDoubleClick(null, null);
        }
        private void txtDiscTotal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.btnSetDispPer_Click(null, null);
        }
        private void btnSetDispPer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*
                this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "General Discount", Tag = "0GEND", ToolTip = "Applicable General Discount Formula" });
                this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Ref./Doctor's Part", Tag = "1REFD", ToolTip = "Discount From Ref./Doctor's Part" });
                this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Full (100 %) Free", Tag = "2FREE", ToolTip = "Full Free Diagnosis Facilities" });
                this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Special Purpose", Tag = "3SPCD", ToolTip = "Special Purpose Discount" });
                 */

                if (this.cmbDisType.SelectedItem == null)
                    return;

                ComboBoxItem disType1 = (ComboBoxItem)this.cmbDisType.SelectedItem;
                this.cmbDisType.Tag = disType1.Tag;

                decimal sumVal1 = this.OrderItemList.Sum(y => y.salam);

                decimal disamt1 = decimal.Parse("0" + this.txtDiscTotal.Text.Trim());
                disamt1 = (this.cmbDisType.SelectedIndex == 2 ? sumVal1 : Math.Min(disamt1, sumVal1));
                foreach (var item in this.OrderItemList)
                {
                    item.idisam = (disamt1 == 0.00m ? 0.00m : item.idisam);
                    item.refscomp = (disamt1 == 0.00m ? item.refscompstd : item.refscomp);
                    item.salam = Math.Round(item.salrate * item.itemqty, 0);
                }

                if (this.cmbDisType.SelectedIndex < 2) // Applicable for General Discount (Tag = "0GEND") and Ref./Doctor's Part Discount (Tag = "1REFD")
                {
                    var DiscItems = this.OrderItemList.FindAll(x => x.isircode.Substring(0, 7) != "4502905").OrderBy(y => y.salam).ToList();

                    if (DiscItems.Count == 0)
                        return;

                    decimal sumMaxDisnt = DiscItems.Sum(y => y.salam * y.refscompstd / 100.00m);
                    disamt1 = Math.Min(disamt1, sumMaxDisnt);
                    decimal disamt2 = disamt1;

                    foreach (var item in DiscItems)
                    {
                        decimal disamt4 = (item.salam * item.refscompstd / 100.00m);
                        decimal disamt3 = Math.Round(disamt1 / sumMaxDisnt * disamt4, 0);
                        item.idisam = disamt3;
                        disamt2 = disamt2 - disamt3;
                        item.idisam = item.idisam + (disamt2 < 5 ? disamt2 : 0);
                        item.icdisam = (this.cmbDisType.SelectedIndex == 1 ? item.idisam : Math.Ceiling(item.idisam / 2.00m));
                        item.refscomp = (item.refscompstd - (item.icdisam / item.salam * 100.00m));

                        if (disamt2 < 5)
                            break;
                    }
                }
                else //if (this.cmbDisType.SelectedIndex >= 2) 
                {
                    decimal disamt21 = disamt1;
                    string siaper1 = this.iudDisPer.Value.ToString();
                    foreach (var item in this.OrderItemList)
                    {
                        item.salam = Math.Round(item.salrate * item.itemqty, 0);
                        decimal disamt41 = Math.Round(item.salam * disamt1 / sumVal1, 0);
                        item.idisam = disamt41;
                        disamt21 = disamt21 - disamt41;
                        item.idisam = item.idisam + (disamt21 < 3 ? disamt21 : 0);
                        item.inetam = item.salam - item.idisam;
                        item.icdisam = Math.Min(item.idisam, Math.Ceiling(item.salam * item.refscompstd / 100.00m));
                        item.refscomp = (item.refscompstd - (item.icdisam / item.salam * 100.00m));
                        if (disamt21 < 3)
                            break;
                    }
                }

                this.chkDiscount.IsChecked = false;
                this.chkDiscount_Click(null, null);
                this.btnTotal_Click(null, null);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-19: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            //if (this.cmbDisType.SelectedIndex < 2) // Applicable for General Discount (Tag = "0GEND") and Ref./Doctor's Part Discount (Tag = "1REFD")

            /*
            this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "General Discount", Tag = "0GEND", ToolTip = "Applicable General Discount Formula" });
            this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Ref./Doctor's Part", Tag = "1REFD", ToolTip = "Discount From Ref./Doctor's Part" });
            this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Full (100 %) Free", Tag = "2FREE", ToolTip = "Full Free Diagnosis Facilities" });
            this.cmbDisType.Items.Add(new ComboBoxItem() { Content = "Special Purpose", Tag = "3SPCD", ToolTip = "Special Purpose Discount" });
             */


            try
            {
                if (this.FrmInitialized == false)
                    return;

                //this.dgvMemo.ItemsSource = null;
                this.OrderItemList = this.OrderItemList.FindAll(x => x.itemqty > 0);

                int serialno1 = 1;
                foreach (var item in this.OrderItemList)
                {
                    item.refscomp = Math.Max(0, Math.Min(item.refscomp, item.refscompstd));

                    if (item.isircode.Substring(0, 7) == "4502905" && this.cmbDisType.SelectedIndex < 2)
                        item.idisam = 0.00m;

                    item.slnum = serialno1.ToString() + ".";
                    item.salam = Math.Round(item.salrate * item.itemqty, 0);
                    var icomp1 = (item.refscompstd - item.refscomp);
                    var idisam1 = Math.Floor(item.refscompstd / 100.00m * item.salam);
                    if (this.cmbDisType.SelectedIndex < 2)
                        item.idisam = Math.Max(0, Math.Round((item.idisam > idisam1 ? idisam1 : item.idisam), 0));
                    else
                        item.idisam = Math.Max(0, Math.Min(item.idisam, item.salam));

                    item.icdisam = Math.Ceiling(icomp1 / 100.00m * item.salam);
                    item.icdisam = (item.icdisam > item.idisam ? item.idisam : item.icdisam);

                    var icomp2 = (item.refscompstd - Math.Round((item.salam == 0 ? 0 : item.icdisam / item.salam) * 100.00m, 0));
                    item.refscomp = (item.refscomp < icomp2 ? icomp2 : item.refscomp);

                    item.inetam = item.salam - item.idisam;
                    item.idisper = (item.idisam == 0.00m ? "" : Math.Round((item.salam == 0 ? 0 : item.idisam / item.salam) * 100.00m, 2).ToString("00.0") + " %");
                    item.icomam = Math.Max(0, (Math.Floor(item.salam * item.refscompstd / 100.00m) - item.icdisam));

                    if (this.cmbDisType.SelectedIndex == 1)
                    {
                        decimal adjm1 = (item.idisam - item.icdisam);
                        if (adjm1 > 0 && item.icomam >= adjm1)
                        {
                            item.icdisam = item.icdisam + adjm1;
                            item.icomam = item.icomam - adjm1;
                        }
                    }

                    item.refpermark = (item.refscomp > 0 ? "%" : "");
                    ++serialno1;
                }


                decimal GrandTotal1 = this.OrderItemList.Sum(x => x.salam);
                decimal DiscTotal1 = this.OrderItemList.Sum(x => x.idisam);
                decimal NetTotal = this.OrderItemList.Sum(x => x.inetam);
                decimal TotalBill = GrandTotal1 - DiscTotal1 + decimal.Parse("0" + this.txtCCCharge.Text.Trim());
                decimal TotalPaid = this.CollInfoList.Sum(x => x.bilcolam) + decimal.Parse("0" + this.txtCCPaid.Text.Trim());
                decimal BalanceBill = TotalBill - TotalPaid;

                this.lblGrandTotal.Content = GrandTotal1.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblDiscTotal.Content = DiscTotal1.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblDiscPerTotal.Content = ((GrandTotal1 == 0 ? 0 : DiscTotal1 / GrandTotal1) * 100.00m).ToString("00.0") + "%";// " -  ";
                this.lblNetTotal.Content = NetTotal.ToString("#,##0;(#,##0); - ");// " -  ";
                ////this.lblTotalBill.Content = TotalBill.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblTotalPaid.Content = TotalPaid.ToString("#,##0;(#,##0); - ");// " -  ";
                this.lblNetBalance.Content = BalanceBill.ToString("#,##0;(#,##0); - ");// " -  ";
                this.dgvMemo.ItemsSource = this.OrderItemList;
                this.dgvMemo.Items.Refresh();
                this.stkpNav1.Visibility = (this.OrderItemList.Count > 0 ? Visibility.Visible : Visibility.Hidden);
                this.btnUpdateTrans.Visibility = (this.ValidateForUpdate() ? Visibility.Visible : Visibility.Hidden);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("CSI-20: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void dgtxtRefsComp_LostFocus(object sender, RoutedEventArgs e)
        {
            this.btnTotal_Click(null, null);
        }
        private void btnAddEditItem_Click(object sender, RoutedEventArgs e)
        {
            HmsDialogWindow1 window1 = new HmsDialogWindow1(new General.frmSirCodeBook1(MainGroup: "4502"));
            window1.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window1.Title = "ITEM CODE BOOK ADD/EDIT SCREEN";
            //window1.Owner = Application.Current.MainWindow;
            window1.ShowDialog();
        }
        private void btnSetItemRate_Click(object sender, RoutedEventArgs e)
        {
            HmsDialogWindow1 window1 = new HmsDialogWindow1(new Inventory.frmEntryInvMgt101(MainGroup: "4502"));
            window1.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window1.Title = "ITEM RATE ADD/EDIT SCREEN";
            //window1.Owner = Application.Current.MainWindow;
            window1.ShowDialog();
            this.GetServiceItemList(itrmGroup: "4502%");
        }
        private void BackupCancelInvoice(string InvoiceID, string exetype, string exermrk)
        {
            var pap1 = vmr.SetParamToBackupCancelInvoice(CompCode: WpfProcessAccess.CompInfList[0].comcod, InvoiceID: InvoiceID, exetype: exetype, exebyid: WpfProcessAccess.SignedInUserList[0].hccode, 
                exebynam: WpfProcessAccess.SignedInUserList[0].signinnam, exeses: WpfProcessAccess.SignedInUserList[0].sessionID, exetrm: WpfProcessAccess.SignedInUserList[0].terminalID, exermrk: exermrk);

            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            if(exetype == "CANCEL")
            {
                System.Windows.MessageBox.Show(ds1.Tables[0].Rows[0]["bkpmsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, 
                                MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }

            //    select memonum = @Desc01, memonum1 = ltrim(rtrim(brnsnam)) + substring(@Desc01, 6, 4) + substring(@Desc01, 14, 5), bkpmsg = 'Invoice ' 
            //          + iif(@Desc02='CANCEL', 'Cancel', 'Backup') + ' operation done successfully' from dbo.compbrn 
            //          where comcod = @ComCod and brncod = substring(@Desc01, 10, 4);
        }
        private void cmbDisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            decimal TotalDiscount = this.OrderItemList.Sum(x => x.idisam);
            if (TotalDiscount > 0)
                this.btnTotal_Click(null, null);
        }

        private void dgvTransList_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    this.dgvTransList.CommitEdit(DataGridEditingUnit.Cell, false);
                    this.dgvTransList.CommitEdit(DataGridEditingUnit.Row, false);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void dgvTransList_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
                this.dgvTransList_MouseDoubleClick(null, null);
        }
      

        private void autoTestItemSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetServiceItemDesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityCommercial.HmsServiceItem> GetServiceItemDesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityCommercial.HmsServiceItem>(
                this.ServiceItemList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoRefBySearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetRefBySourceDesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityCommercial.HmsRefByInf> GetRefBySourceDesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityCommercial.HmsRefByInf>(
                this.RefByInfList.Where((x, match) => (x.rfFullName).ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoMktStaffRefSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetStaffRefSirdesc(args.Pattern);
        }
     
    }
}
