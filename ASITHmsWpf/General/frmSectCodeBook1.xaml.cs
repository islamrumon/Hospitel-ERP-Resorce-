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
using ASITHmsViewMan.General;
using ASITFunLib;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt1GenAcc.General;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmSirCodeBook1.xaml
    /// ToDo List for this form
    /// ===============================================================
    /// 01. Completed Task:     Main Branch group selection option
    /// 02. Completed Task:     Tree Definition for each selected group
    /// 03. Completed Task:     Details List View for each selected group
    /// 04. Completed Task:     Add/Edit Resource Code
    /// 05. Completed Task:     Print partial resource code book
    /// 06. Underconstion :     Add/Edit Additional Details for eache resource code
    /// 
    /// </summary>
    /// 
    /// </summary>
    public partial class frmSectCodeBook1 : UserControl
    {
        private bool FrmInitialized = false;

        private List<vmSectCodeBook1.SectListViewItem> ListSectInfCodeBook = new List<vmSectCodeBook1.SectListViewItem>();
        private List<vmSectCodeBook1.SectListViewItem> ListSectInfMain1CodeBook = new List<vmSectCodeBook1.SectListViewItem>();
        private List<vmSectCodeBook1.SectListViewItem> ListSectInfMain2CodeBook = new List<vmSectCodeBook1.SectListViewItem>();
        private List<vmSectCodeBook1.SectListViewItem> ListSectInfMain3CodeBook = new List<vmSectCodeBook1.SectListViewItem>();

        private List<vmHmsGeneralList1.GenDetailsListInfo> ListGenDetailsInfo { get; set; }

        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private vmSectCodeBook1 vm1 = new vmSectCodeBook1();


        public frmSectCodeBook1()
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
                this.gridCodeEntry.Visibility = Visibility.Collapsed;
                this.ActivateAuthObjects();
                this.GetSectionInfData();
                this.vm1.BindSectTree(this.treeSect, this.ListSectInfMain1CodeBook, this.ListSectInfMain2CodeBook, this.ListSectInfMain3CodeBook, this.ListSectInfCodeBook, this.GetContextMenu());
                this.stklstv.IsEnabled = false;
                this.PrepareListView();
                this.stklstv.IsEnabled = true;
            }
        }
        private void ActivateAuthObjects()
        {
            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmSectCodeBook1_chkAdEd") == null)
                this.chkAdEd.Visibility = Visibility.Hidden;
        }
        private void GetSectionInfData()
        {
            var list0 = WpfProcessAccess.CompInfList[0].BranchList.FindAll(x => x.brncod.Substring(2, 2) != "00");
            var list1 = WpfProcessAccess.CompInfList[0].SectionList;
            this.ListSectInfCodeBook.Clear();
            this.ListSectInfMain1CodeBook.Clear();
            this.ListSectInfMain2CodeBook.Clear();
            this.ListSectInfMain3CodeBook.Clear();
            foreach (var item in list0)
            {
                this.ListSectInfMain1CodeBook.Add(new vmSectCodeBook1.SectListViewItem() { sectcod = item.brncod + "00000000", sectcod1 = item.brncod + "-000-00-000", sectname = item.brnnam, sectdesc = item.brnsnam });
                this.ListSectInfCodeBook.Add(new vmSectCodeBook1.SectListViewItem() { sectcod = item.brncod + "00000000", sectcod1 = item.brncod + "-000-00-000", sectname = item.brnnam, sectdesc = item.brnsnam });
            }

            foreach (var item in list1)
            {
                string code1 = item.sectcod.Substring(0, 4) + "-" + item.sectcod.Substring(4, 3) + "-" + item.sectcod.Substring(7, 2) + "-" + item.sectcod.Substring(9, 3);

                this.ListSectInfCodeBook.Add(new vmSectCodeBook1.SectListViewItem() { sectcod = item.sectcod, sectcod1 = code1, sectname = item.sectname, sectdesc = item.sectdesc });

                if (item.sectcod.Substring(7, 5) == "00000" && item.sectcod.Substring(4, 8) != "00000000")
                    this.ListSectInfMain2CodeBook.Add(new vmSectCodeBook1.SectListViewItem() { sectcod = item.sectcod, sectcod1 = code1, sectname = item.sectname, sectdesc = item.sectdesc });
                else if (item.sectcod.Substring(9, 3) == "000" && item.sectcod.Substring(7, 5) != "00000")
                    this.ListSectInfMain3CodeBook.Add(new vmSectCodeBook1.SectListViewItem() { sectcod = item.sectcod, sectcod1 = code1, sectname = item.sectname, sectdesc = item.sectdesc });
            }

            this.ListSectInfCodeBook.Sort(delegate(vmSectCodeBook1.SectListViewItem x, vmSectCodeBook1.SectListViewItem y)
            {
                return (x.sectcod).CompareTo(y.sectcod);
            });
        }


        private void PrepareListView()
        {
            //this.lvSect.ItemsSource = null;
            this.dgvSect.ItemsSource = null;
            var lst111a = this.ListSectInfCodeBook.AsEnumerable().Select((x, index) => new { slnum = (index + 1).ToString() + ".", x.sectcod, x.sectcod1, x.sectname, x.sectdesc }).ToList();
            List<vmSectCodeBook1.SectListViewItem> list1 = new List<vmSectCodeBook1.SectListViewItem>();
            this.AtxtSectCode.Items.Clear();
            this.AtxtSectCode.AutoSuggestionList.Clear();
            foreach (var itema in lst111a)
            {
                list1.Add(new vmSectCodeBook1.SectListViewItem()
                {
                    slnum = itema.slnum,
                    sectcod = itema.sectcod,
                    sectcod1 = itema.sectcod1,
                    sectname = itema.sectname,
                    sectdesc = itema.sectdesc,
                    fbold = (itema.sectcod.Substring(9, 3) == "000" ? "Bold" : "Normal"),
                    fcolor = (itema.sectcod.Substring(4, 8) == "00000000" ? "Maroon" : (itema.sectcod.Substring(7, 5) == "00000" ? "Blue" : "Black"))
                });
                this.AtxtSectCode.AddSuggstionItem(itema.sectcod + " - " + itema.sectname.Trim(), itema.sectcod);
            }


            /*
             * =IIf((Fields!sectcod.Value.ToString().Substring(7,5)="00000"), "Blue",IIf((Fields!sectcod.Value.ToString().Substring(9,3)="000"),"DarkGreen","Black"))
             =IIf((Fields!sectcod.Value.ToString().Substring(7,5)="00000"),"DarkBlue",IIf((Fields!sectcod.Value.ToString().Substring(9,3)="000"),"Blue","Black"))
             */
            //this.lvSect.ItemsSource = list1;
            this.dgvSect.ItemsSource = list1;
            this.dgvSect.ContextMenu = this.GetContextMenu();

        }
        private ContextMenu GetContextMenu()
        {
            ContextMenu cm1 = new ContextMenu();
            MenuItem mi1 = new MenuItem();
            mi1.Header = "Add";
            mi1.Click += this.ContextMenu_OnClick;
            MenuItem mi2 = new MenuItem();
            mi2.Header = "Edit";
            mi2.Click += this.ContextMenu_OnClick;
            MenuItem mi3 = new MenuItem();
            mi3.Header = "View";
            mi3.Click += this.ContextMenu_OnClick;
            MenuItem mi4 = new MenuItem();
            mi4.Header = "Add/Edit Extra Info";
            mi4.Click += this.ContextMenu_OnClick;

            cm1.Items.Add(mi1);
            cm1.Items.Add(mi2);
            cm1.Items.Add(mi3);
            cm1.Items.Add(mi4);
            return cm1;
        }

        private void ContextMenu_OnClick(object sender, RoutedEventArgs e)
        {
            string ItemTxt1 = ((MenuItem)sender).Header.ToString();


            //if (this.treeSect.SelectedItem == null)
            //    return;
            //var tvi = (TreeViewItem)this.treeSect.SelectedItem;
            //string x = tvi.Tag.ToString();



            string sectcode1 = "XXXXXXXXXXXX";
            if (this.dgvSect.SelectedItem != null)
            {
                var itm1 = (vmSectCodeBook1.SectListViewItem)this.dgvSect.SelectedItem;
                sectcode1 = itm1.sectcod;
            }

            if (sectcode1 == "XXXXXXXXXXXX")
                return;

            this.showTextBoxData(sectcode1);

            this.chkAdEd.IsChecked = true;


            switch (ItemTxt1)
            {
                case "Add":
                    this.btnAcc_Click(new Button() { Name = "btnAdd" }, null);
                    break;
                case "Edit":
                    this.btnAcc_Click(new Button() { Name = "btnEdit" }, null);
                    break;
                case "View":
                    this.btnAdd.IsEnabled = false;
                    this.btnEdit.IsEnabled = false;
                    this.btnUpdate.IsEnabled = false;
                    break;
                case "Add/Edit Extra Info":
                    this.treeSect_GotFocus(this.treeSect, null);
                    break;
            }
        }


        private void showTextBoxData(string Code1)
        {
            this.txtblMnGr.Text = "";
            string cod2 = Code1.Substring(0, 4) + "00000000";
            string cod3 = Code1.Substring(0, 7) + "00000";
            string cod4 = Code1.Substring(0, 9) + "000";

            var subcod1 = this.ListSectInfMain1CodeBook.FindAll(x => x.sectcod == cod2);
            var subcod2 = this.ListSectInfMain2CodeBook.FindAll(x => x.sectcod == cod3);
            var subcod3 = this.ListSectInfMain3CodeBook.FindAll(x => x.sectcod == cod4);

            string MainDesc1 = (subcod1.Count > 0 && subcod1[0].sectcod != Code1 ? "Main : " + subcod1[0].sectcod1.Trim() + " " + subcod1[0].sectname.Trim() : "");
            MainDesc1 += (subcod1.Count > 0 && subcod2.Count > 0 && subcod2[0].sectcod != Code1 ? "\nSub.1 : " + subcod2[0].sectcod1.Trim() + " " + subcod2[0].sectname.Trim() : "");
            MainDesc1 += (subcod1.Count > 0 && subcod2.Count > 0 && subcod3.Count > 0 && subcod3[0].sectcod != Code1 ? "\nSub.2 : " + subcod3[0].sectcod1.Trim() + " " + subcod3[0].sectname.Trim() : "");

            this.txtblMnGr.Text = MainDesc1;


            vmSectCodeBook1.SectListViewItem[] row1 = this.ListSectInfCodeBook.FindAll(x => x.sectcod == Code1).ToArray();

            string sectCod1 = row1[0].sectcod.ToString();
            this.txtSectCode2.Text = sectCod1.Substring(0, 4);
            this.txtSectCode3.Text = sectCod1.Substring(4, 3);
            this.txtSectCode4.Text = sectCod1.Substring(7, 2);
            this.txtSectCode5.Text = sectCod1.Substring(9, 3);
            this.lblSectCode.Content = row1[0].sectcod1.ToString();
            this.lblSectCode.Tag = sectCod1;

            this.txtsectdesc.Text = row1[0].sectname.ToString();
            this.txtsecttdes.Text = row1[0].sectdesc;
            this.spnlCodeEntry.IsEnabled = false;

            this.chkExtraInfo.Visibility = Visibility.Visible;
            this.chkExtraInfo.IsChecked = false;
            this.chkExtraInfo_Click(null, null);

        }

        private void chkExtraInfo_Click(object sender, RoutedEventArgs e)
        {
            string sectCod1 = this.lblSectCode.Tag.ToString();
            this.stkpcanvasCode.Height = (this.chkExtraInfo.IsChecked == true ? 540 : 300);

            if (sender == null)
                return;

            string sectCod2 = sectCod1.Substring(0, 2);

            this.dgExtraInfo.ItemsSource = null;
            this.stkpExtraCode.Visibility = (this.chkExtraInfo.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
            //this.ListGenDetailsInfo.Clear();

            string gcodeGroup = "SISD";

            var pap1 = this.vmGenList1.SetParamGenDetailsInf(WpfProcessAccess.CompInfList[0].comcpcod, "SIRINF", sectCod1, gcodeGroup);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;


            this.ListGenDetailsInfo = ds1.Tables[0].DataTableToList<vmHmsGeneralList1.GenDetailsListInfo>();
            this.dgExtraInfo.ItemsSource = this.ListGenDetailsInfo;
            this.btnUpdate.IsEnabled = true;
            this.btnAcc_Click(this.btnEdit, null);
        }
        private void btnAcc_Click(object sender, RoutedEventArgs e)
        {
            string btnNam1 = ((Button)sender).Name.ToString().Trim();
            Window mainWindow1 = Application.Current.MainWindow;
            Label lblBaloon1 = (Label)mainWindow1.FindName("lblBaloon1");

            switch (btnNam1)
            {
                case "btnAdd":
                    this.spnlCodeEntry.IsEnabled = true;
                    this.txtSectCode2.IsEnabled = false;
                    this.txtSectCode3.IsEnabled = (true && this.chkLevel2.IsChecked == true); ;
                    this.txtSectCode4.IsEnabled = (true && this.chkLevel3.IsChecked == true); ;
                    this.txtSectCode5.IsEnabled = (true && this.chkLevel4.IsChecked == true); ;
                    this.btnUpdate.Tag = "Add";
                    this.addEdt();
                    this.txtSectCode5.Focus();
                    break;
                case "btnEdit":
                    this.spnlCodeEntry.IsEnabled = true;
                    this.txtSectCode2.IsEnabled = false;
                    this.txtSectCode3.IsEnabled = false;
                    this.txtSectCode4.IsEnabled = false;
                    this.txtSectCode5.IsEnabled = false;
                    this.btnUpdate.Tag = "Edit";
                    this.addEdt();
                    this.txtsectdesc.Focus();
                    break;
                case "btnUpdate":
                    this.UpdateSetInf();
                    break;
                case "btnCancel":

                    this.btnUpdate.Tag = "None";
                    btnUpdate.IsEnabled = false;
                    btnAdd.IsEnabled = true;
                    btnEdit.IsEnabled = true;
                    this.chkAdEd.IsChecked = false;
                    break;
            }
        }

        private void addEdt()
        {

            btnUpdate.IsEnabled = true;
            btnAdd.IsEnabled = false;
            btnEdit.IsEnabled = false;
        }

        private void UpdateSetInf()
        {
            string SectCode1 = this.txtSectCode2.Text.Trim() + this.txtSectCode3.Text.Trim() + this.txtSectCode4.Text.Trim() + this.txtSectCode5.Text.Trim();
            if ((SectCode1.Substring(2, 2) == "00" && SectCode1.Substring(4, 8) != "00000000")
               || (SectCode1.Substring(4, 3) == "000" && SectCode1.Substring(7, 5) != "00000") || (SectCode1.Substring(7, 2) == "00" && SectCode1.Substring(9, 3) != "000"))
            {
                MessageBox.Show("Could not add invalid code. Please try with valid code", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (this.txtSectCode5.IsEnabled)
            {
                var list1 = this.ListSectInfCodeBook.FindAll(x => x.sectcod == SectCode1);
                if (list1.Count > 0)
                {
                    MessageBox.Show("Could not add code. It is already exist in the code book", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            }
            if (System.Windows.MessageBox.Show("Confirm update", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
               MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }


            string SectDesc1 = this.txtsectdesc.Text.Trim();
            string SecttDesc1 = this.txtsecttdes.Text.Trim();
            string AddEdit1 = this.btnUpdate.Tag.ToString().Trim();
            var pap1 = this.vm1.SetParamUpdateSectInf(WpfProcessAccess.CompInfList[0].comcpcod, SectCode1, SectDesc1, SecttDesc1, AddEdit1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            if (ds2 == null)
            {
                MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (this.chkExtraInfo.IsChecked == true)
            {
                var pap1d = this.vmGenList1.SetParamUpdateGenInf(WpfProcessAccess.CompInfList[0].comcpcod, "SIRINF", SectCode1, this.ListGenDetailsInfo);
                DataSet ds2d = WpfProcessAccess.GetHmsDataSet(pap1: pap1d);
                if (ds2d == null)
                {
                    MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            }

            this.lblSectCode.Content = this.txtSectCode2.Text.Trim() + "-" + this.txtSectCode3.Text.Trim() + "-" + this.txtSectCode4.Text.Trim() + "-" + this.txtSectCode5.Text.Trim();
            this.lblSectCode.Tag = SectCode1;

            btnUpdate.IsEnabled = false;
            btnAdd.IsEnabled = true;
            btnEdit.IsEnabled = true;

            WpfProcessAccess.CompInfList = null;
            WpfProcessAccess.GetCompanyInfoList();
            this.GetSectionInfData();
            this.AddEditTreeNode(AddEdit1, SectCode1, SectDesc1);
            this.PrepareListView();

            this.ScrollSelectListViewItem(SectCode1);

        }


        private void AddEditTreeNode(string AddEdit1, string SectCode1, string SectDesc1)
        {
            TreeViewItem tvi1 = new TreeViewItem()
            {
                Tag = SectCode1,
                ContextMenu = this.GetContextMenu(),
                Header = (SectCode1.Substring(2, 10) == "0000000000" ? SectCode1.Substring(0, 2) : (SectCode1.Substring(4, 8) == "00000000" ? SectCode1.Substring(2, 2) : (SectCode1.Substring(7, 5) == "00000" ? SectCode1.Substring(4, 3) : (SectCode1.Substring(9, 3) == "000" ? SectCode1.Substring(7, 2) : SectCode1.Substring(9, 3))))) + " - " + SectDesc1
            };
            string psircode1 = (SectCode1.Substring(4, 8) == "00000000" ? SectCode1.Substring(0, 2) + "0000000000" : (SectCode1.Substring(7, 5) == "00000" ? SectCode1.Substring(0, 4) + "00000000" : (SectCode1.Substring(9, 3) == "000" ? SectCode1.Substring(0, 7) + "00000" : SectCode1.Substring(0, 9) + "000")));
            //string pactcode1 = (Sircode1.Substring(4, 8) == "00000000" ? Sircode1.Substring(0, 2) + "0000000000" : (Sircode1.Substring(8, 4) == "0000" ? Sircode1.Substring(0, 4) + "00000000" : Sircode1.Substring(0, 8) + "0000"));
            TreeView tv1a = null;


            tv1a = (TreeView)this.treeSect;

            if (tv1a == null)
                return;

            #region Editing Description
            if (AddEdit1.Contains("Edit"))
            {
                foreach (TreeViewItem item1a in tv1a.Items)
                {
                    if (item1a.Tag.ToString() == SectCode1)
                    {
                        item1a.Header = tvi1.Header;
                        break;
                    }
                    else if (item1a.Items.Count > 0)
                    {
                        foreach (TreeViewItem item1b in item1a.Items)
                        {
                            if (item1b.Tag.ToString() == SectCode1)
                            {
                                item1b.Header = tvi1.Header;
                                break;
                            }
                            else if (item1b.Items.Count > 0)
                            {
                                foreach (TreeViewItem item1c in item1b.Items)
                                {
                                    if (item1c.Tag.ToString() == SectCode1)
                                    {
                                        item1c.Header = tvi1.Header;
                                        break;
                                    }
                                    else if (item1c.Items.Count > 0)
                                    {
                                        foreach (TreeViewItem item1d in item1c.Items)
                                        {
                                            if (item1d.Tag.ToString() == SectCode1)
                                            {
                                                item1d.Header = tvi1.Header;
                                                break;
                                            }
                                            else if (item1d.Items.Count > 0)
                                            {
                                                foreach (TreeViewItem item1f in item1d.Items)
                                                {
                                                    if (item1f.Tag.ToString() == SectCode1)
                                                    {
                                                        item1f.Header = tvi1.Header;
                                                        break;
                                                    }
                                                }

                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                tvi1.IsSelected = true;
                return;
            }

            #endregion

            #region Adding New Code and Description
            foreach (TreeViewItem item1a in tv1a.Items)
            {
                if (item1a.Tag.ToString() == psircode1)
                {
                    item1a.Items.Add(tvi1);
                    break;
                }
                else if (item1a.Items.Count > 0)
                {
                    foreach (TreeViewItem item1b in item1a.Items)
                    {
                        if (item1b.Tag.ToString() == psircode1)
                        {
                            item1b.Items.Add(tvi1);
                            item1b.IsExpanded = true;
                            break;
                        }
                        else if (item1b.Items.Count > 0)
                        {
                            foreach (TreeViewItem item1c in item1b.Items)
                            {
                                if (item1c.Tag.ToString() == psircode1)
                                {
                                    item1c.Items.Add(tvi1);
                                    item1c.IsExpanded = true;
                                    break;
                                }
                                else if (item1c.Items.Count > 0)
                                {
                                    foreach (TreeViewItem item1d in item1c.Items)
                                    {
                                        if (item1d.Tag.ToString() == psircode1)
                                        {
                                            item1d.Items.Add(tvi1);
                                            item1d.IsExpanded = true;
                                            break;
                                        }

                                    }
                                }

                            }
                        }
                    }

                }
            }
            #endregion

            tvi1.IsSelected = true;
            return;
        }

        private void btnFindCode_Click(object sender, RoutedEventArgs e)
        {
            string sectcode1 = this.AtxtSectCode.Value.Trim();
            this.AtxtSectCode.Text = "";
            if (sectcode1.Length == 0)
                return;

            this.ScrollSelectListViewItem(sectcode1);
        }
        private void ScrollSelectListViewItem(string sectcode1)
        {
            int z = 0;

            foreach (var item3 in this.ListSectInfCodeBook)
            {
                if (item3.sectcod == sectcode1)
                    break;
                z++;
            }

            //this.lvSect.ScrollIntoView(this.lvSect.Items[z]);
            //this.lvSect.SelectedIndex = z;

            this.dgvSect.ScrollIntoView(this.dgvSect.Items[z]);
            this.dgvSect.SelectedIndex = z;
        }

        private void chkAdEd_Unchecked(object sender, RoutedEventArgs e)
        {
            this.gridCodeEntry.Visibility = Visibility.Collapsed;
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            this.chkPrint.IsChecked = false;

            LocalReport rpt1 = new LocalReport();
            var list3 = WpfProcessAccess.GetRptGenInfo();
            var list1 = WpfProcessAccess.CompInfList;
            string level1 = "1,2";
            level1 = level1 + (this.chkLevel3.IsChecked == true ? ",3" : "");
            level1 = level1 + (this.chkLevel4.IsChecked == true ? ",4" : "");
            list3[0].RptCompAdd3 = level1;

            rpt1 = GeneralReportSetup.GetLocalReport("General.RptComSections1", list1, null, list3);
            string WindowTitle1 = "Company Info";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: "PrintLayout");
        }

        private void chk_Checked(object sender, RoutedEventArgs e)
        {
            this.stkpcanvasCode.Height = 300;
            this.stkpExtraCode.Visibility = Visibility.Collapsed;
            this.chkExtraInfo.IsChecked = false;


            CheckBox chk = (CheckBox)sender;
            switch (chk.Name)
            {
                case "chkPrint":
                    if (this.chkPrint.IsChecked == true)
                    {
                        this.chkAdEd.IsChecked = false;
                        this.chkExtraInfo.IsChecked = false;
                    }
                    break;
                case "chkAdEd":
                    if (this.chkAdEd.IsChecked == true)
                    {
                        //if (lvSect.SelectedIndex >= 0)
                        //{
                        //    var item1 = (vmSectCodeBook1.SectListViewItem)this.lvSect.SelectedItem;
                        //    this.showTextBoxData(item1.sectcod);
                        //}
                        if (this.dgvSect.SelectedIndex >= 0)
                        {
                            var item1 = (vmSectCodeBook1.SectListViewItem)this.dgvSect.SelectedItem;
                            this.showTextBoxData(item1.sectcod);
                        }
                        this.chkPrint.IsChecked = false;
                        this.chkExtraInfo.IsChecked = false;
                        this.gridCodeEntry.Visibility = Visibility.Visible;
                    }
                    break;
                default:
                    break;
            }
        }

        private void treeSect_GotFocus(object sender, RoutedEventArgs e)
        {
            TreeView tv1a = (TreeView)sender;
            if (tv1a.SelectedItem == null)
                return;

            string tagCode = ((TreeViewItem)tv1a.SelectedItem).Tag.ToString().Trim();
            if (this.chkAdEd.IsChecked == true)
                this.showTextBoxData(tagCode);

            if (this.chkExtraInfo.IsChecked == true)
                this.ShowExtraData(tagCode);


            this.ScrollSelectListViewItem(tagCode);
        }

        private void ShowExtraData(string Code1)
        {
            //this.txtblCodeExtra.Text = "";

            var row1 = this.ListSectInfCodeBook.FindAll(x => x.sectcod == Code1).ToArray();

            //this.txtblCodeExtra.Text = Code1.Substring(0, 2) + "-" + Code1.Substring(2, 2) + "-" + Code1.Substring(4, 3) + "-" + Code1.Substring(7, 2) + "-" + Code1.Substring(9, 3) + " : " + row1[0].sirdesc.Trim();
        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CodeValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void lbldgExtraInfoRptSlno_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtsect_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtsir_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgvSect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.dgvSect.SelectedItem == null)
                return;

            var item1 = (vmSectCodeBook1.SectListViewItem)this.dgvSect.SelectedItem;
            this.chkAdEd.IsChecked = true;
            this.showTextBoxData(item1.sectcod);
            this.btnCancel.Focus();
        }

        private void dgvSect_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;

            this.dgvSect_MouseDoubleClick(null, null);

            //var item1 = (vmSectCodeBook1.SectListViewItem)this.dgvSect.SelectedItem;
            //this.chkAdEd.IsChecked = true;
            //this.showTextBoxData(item1.sectcod);
            //this.btnCancel.Focus();
        }

        private void dgvSect_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    this.dgvSect.CommitEdit(DataGridEditingUnit.Cell, false);
                    this.dgvSect.CommitEdit(DataGridEditingUnit.Row, false);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
