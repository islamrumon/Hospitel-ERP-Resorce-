using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ASITHmsEntity;
using ASITHmsViewMan;
using ASITHmsViewMan.General;
using ASITFunLib;
using System.Globalization;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt1GenAcc.General;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Collections.ObjectModel;
//using System.Windows.Controls.ListViewItem;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for AccCodeBook1.xaml
    /// ToDo List for this form
    /// ===============================================================
    /// 01. Completed Task:     Expander Setup for Accounts Group
    /// 02. Completed Task:     Tree Definition for each Accounts Group
    /// 03. Completed Task:     Details List View for each Accounts Group
    /// 04. Completed Task:     Add/Edit Accounts Code
    /// 05. Completed Task:     Print Accounts Code Book
    /// 06. Underconstion :     Add/Edit Additional Details for eache A/c Code
    /// 
    /// </summary>
    public partial class frmAccCodeBook1 : UserControl
    {

        private bool FrmInitialized = false;
        private List<List<HmsEntityGeneral.AcInfCodeBook>> LListAcInfCodeBook { get; set; }
        private List<HmsEntityGeneral.AcInfCodeBook> CurListAcInfCodeBook { get; set; }
        private List<vmHmsGeneralList1.GenDetailsListInfo> ListGenDetailsInfo { get; set; }

        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private vmAccCodeBook1 vm1 = new vmAccCodeBook1();
        public frmAccCodeBook1()
        {
            InitializeComponent();
        }
        private void UnCheckedAllPopups()
        {
            this.chkPrint.IsChecked = false;
            this.chkAdEd.IsChecked = false;
            this.chkExtraInfo.IsChecked = false;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;
                this.UnCheckedAllPopups();
                WpfProcessAccess.AccCodeList = null;
                this.gridCodeEntry.Visibility = Visibility.Collapsed;
                this.ActivateAuthObjects();
                this.stklstv.IsEnabled = false;
                this.GetAcInfData();
                this.stklstv.IsEnabled = true;
            }
        }

        private void ActivateAuthObjects()
        {
            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmAccCodeBook1_chkAdEd") == null)
                this.chkAdEd.Visibility = Visibility.Hidden;
        }

        private void GetAcInfData()
        {
            string strLevels = "";
            strLevels = (this.chkLevel1.IsChecked == true ? "1" : "1");
            strLevels = (this.chkLevel2.IsChecked == true ? "12" : strLevels);
            strLevels = (this.chkLevel3.IsChecked == true ? "123" : strLevels);
            strLevels = (this.chkLevel4.IsChecked == true ? "1234" : strLevels);

            this.chkLevel1.IsChecked = strLevels.Contains("1");
            this.chkLevel2.IsChecked = strLevels.Contains("2");
            this.chkLevel3.IsChecked = strLevels.Contains("3");
            this.chkLevel4.IsChecked = strLevels.Contains("4");
            //ASITFunParams.ProcessAccessParams pap1 = this.vmGenList1.SetParamAcInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "%", "1234");
            ASITFunParams.ProcessAccessParams pap1 = this.vmGenList1.SetParamAcInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "%", strLevels);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var ListAcInfCodeBook = ds1.Tables[0].DataTableToList<HmsEntityGeneral.AcInfCodeBook>();

            this.LListAcInfCodeBook = vm1.GetGroupListAcInfCodeBook(ListAcInfCodeBook);
            var lst111a = ListAcInfCodeBook.AsEnumerable().Select((x, index) => new { slnum = (index + 1).ToString() + ".", x.actcode, x.actcode1, x.actdesc, x.actelev, x.acttype, x.acttdesc }).ToList();

            List<vmAccCodeBook1.AcListViewItem> list1 = new List<vmAccCodeBook1.AcListViewItem>();
            foreach (var itema in lst111a)
            {
                list1.Add(new vmAccCodeBook1.AcListViewItem()
                {
                    slnum = itema.slnum.Trim(),
                    actcode = itema.actcode.Trim(),
                    actcode1 = itema.actcode1,
                    actdesc = itema.actdesc,
                    actelev = itema.actelev,
                    acttype = itema.acttype,
                    acttdesc = itema.acttdesc,
                    fbold = (itema.actcode.Substring(8, 4) == "0000" ? "Bold" : "Normal"),
                    fcolor = (itema.actcode.Substring(2, 10) == "0000000000" ? "Maroon" : (itema.actcode.Substring(4, 8) == "00000000" ? "Blue" : "Black"))
                });
            }

            this.dgvAcc.ItemsSource = list1;
            this.dgvAcc.ContextMenu = this.GetContextMenu();
        }

        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {
            string strLevels = "";
            strLevels = (this.chkLevel1.IsChecked == true ? "1" : "1");
            strLevels = (this.chkLevel2.IsChecked == true ? "2" : strLevels);
            strLevels = (this.chkLevel3.IsChecked == true ? "3" : strLevels);
            strLevels = (this.chkLevel4.IsChecked == true ? "4" : strLevels);
            this.GetAcInfData();
            this.chkPrint.IsChecked = false;
            var list1 = this.LListAcInfCodeBook[0];
            string RptTitle = "Chart of Accounts (Level - " + strLevels + ")";
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            LocalReport rpt1 = GeneralReportSetup.GetLocalReport("General.rptAccCodeBook1", list1, RptTitle, list3); // ( R_01_RptSetup.RptSetupItemList1(ds1, ds2);
            string WindowTitle1 = "Accounts Code Book Report";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }
        private Expander[] expn1a()
        {
            Expander[] expn1 = {this.expAccAsset, this.expAccLiab, this.expAccRev, this.expAccDirCost, 
                                this.expAccOver,  this.expAccNoie, this.expAccFin, this.expAccOther};
            return expn1;
        }

        private TreeView[] treev1a()
        {
            TreeView[] treev1 = { this.treeAccAsset, this.treeAccLiab, this.treeAccRev, this.treeAccDirCost, 
                                this.treeAccOver,  this.treeAccNoie, this.treeAccFin, this.treeAccOther };
            return treev1;
        }
        private void expAcc_Expanded(object sender, RoutedEventArgs e)
        {
            // Important Note Level Wise Codebook to be prepare as Livels are defined under spnlLevel
            // Not it is not considerd for Code Book Tree and List View Preparation --- Hafiz 30-Dev-2015


            this.UnCheckedAllPopups();
            ////this.GetAcInfData();
            string ExpanderName1 = ((Expander)sender).Name.ToString().Trim();
            int expIndex = int.Parse(((Expander)sender).Tag.ToString());
            vmHmsGeneral1.ExpanderClose(expn1a(), ExpanderName1);
            vmHmsGeneral1.AllTreeViewExpandCollapse(treev1a(), false);
            vm1.BindAllTrees(treev1a(), this.LListAcInfCodeBook, expIndex, this.GetContextMenu());
            this.spnlLevel.IsEnabled = false;
        }

        private void CodeValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void expAcc_Collapsed(object sender, RoutedEventArgs e)
        {
            this.spnlLevel.IsEnabled = true;
            this.UnCheckedAllPopups();
        }
        private void btnExpandCollapse_Click(object sender, RoutedEventArgs e)
        {
            Expander[] expn1a = this.expn1a();
            TreeView[] treev1a = this.treev1a();
            bool ec1 = (((Button)sender).Name == "btnExpandAll" ? true : false);
            for (int i = 0; i < expn1a.Length; i++)
            {
                if (expn1a[i].IsExpanded)
                {
                    vmHmsGeneral1.TreeViewExpandCollapse(treev1a[i], ec1);
                    return;
                }
            }
        }
        private void treeAcc_GotFocus(object sender, RoutedEventArgs e)
        {
            TreeView tv1a = (TreeView)sender;
            if (tv1a.SelectedItem == null)
                return;

            string tagCode = ((TreeViewItem)tv1a.SelectedItem).Tag.ToString().Trim();
            if (this.chkAdEd.IsChecked == true)
                this.showTextBoxData(tagCode);

            this.ScrollSelectListViewItem(tagCode);
        }

        private void showTextBoxData(string Code1)
        {
            this.txtblMnGr.Text = "";

            string cod1 = Code1.Substring(0, 2) + "0000000000";
            string cod2 = Code1.Substring(0, 4) + "00000000";
            string cod3 = Code1.Substring(0, 8) + "0000";

            var maincod1 = this.LListAcInfCodeBook[1].FindAll(x => x.actcode == cod1);
            var subcod1 = this.LListAcInfCodeBook[2].FindAll(x => x.actcode == cod2);
            var subcod2 = this.LListAcInfCodeBook[3].FindAll(x => x.actcode == cod3);
            string MainDesc1 = (maincod1.Count > 0 && maincod1[0].actcode != Code1 ? " Main : " + maincod1[0].actcode1.Trim() + " " + maincod1[0].actdesc.Trim() : "");
            MainDesc1 += (maincod1.Count > 0 && subcod1.Count > 0 && subcod1[0].actcode != Code1 ? "\n   Sub : " + subcod1[0].actcode1.Trim() + " " + subcod1[0].actdesc.Trim() : "");
            MainDesc1 += (maincod1.Count > 0 && subcod1.Count > 0 && subcod2.Count > 0 && subcod2[0].actcode != Code1 ? "\nSub.2 : " + subcod2[0].actcode1.Trim() + " " + subcod2[0].actdesc.Trim() : "");

            this.txtblMnGr.Text = MainDesc1;

            var list1 = this.LListAcInfCodeBook[0].FindAll(x => x.actcode == Code1);
            string code2 = list1[0].actcode;
            this.txtActCode1.Text = code2.Substring(0, 2);
            this.txtActCode2.Text = code2.Substring(2, 2);
            this.txtActCode3.Text = code2.Substring(4, 4);
            this.txtActCode4.Text = code2.Substring(8, 4);
            this.lblActCode.Content = " " + code2.Substring(0, 2) + "-" + code2.Substring(2, 2) + "-" + code2.Substring(4, 4) + "-" + code2.Substring(8, 4);
            this.lblActCode.Tag = code2;
            this.txtAcDesc.Text = list1[0].actdesc;
            this.chkELevel2.IsChecked = (list1[0].actelev.Trim() == "2");
            this.txtActtype.Text = list1[0].acttype;
            this.txtActtdesc.Text = list1[0].acttdesc;
            this.spnlCodeEntry.IsEnabled = false;

            string AccCod2 = Code1.Substring(0, 4);
            // Details view shows based on following options
            if (Code1.Substring(8, 4) == "0000" || !(AccCod2 == "1902" || AccCod2 == "2902"))
                this.chkExtraInfo.Visibility = Visibility.Hidden;
            else
                this.chkExtraInfo.Visibility = Visibility.Visible;
            this.chkExtraInfo.IsChecked = false;
            this.chkExtraInfo_Click(null, null);
        }

        private void ScrollSelectListViewItem(string actcode1)
        {

            var item22 = this.dgvAcc.Items.OfType<vmAccCodeBook1.AcListViewItem>().ToList().FindAll(x => x.actcode == actcode1);
            if (item22.Count > 0)
            {
                this.dgvAcc.ScrollIntoView(item22[0]);
                this.dgvAcc.SelectedItem = item22[0];
            }
            return;



            ////int z = 0;
            //////foreach (var item3 in this.CurListAcInfCodeBook)
            ////foreach (var item3 in this.LListAcInfCodeBook[0])
            ////{
            ////    if (item3.actcode == actcode1)
            ////        break;
            ////    z++;
            ////}

            ////this.dgvAcc.ScrollIntoView(this.dgvAcc.Items[z]);
            ////this.dgvAcc.SelectedIndex = z;

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
                        if (this.dgvAcc.SelectedIndex >= 0)
                        {
                            var item1 = (vmAccCodeBook1.AcListViewItem)this.dgvAcc.SelectedItem;
                            this.showTextBoxData(item1.actcode);
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
            //TreeView tv1a = null;
            //foreach (var tv2 in this.treev1a())
            //{
            //    if (tv2.Items.Count > 0)
            //    {
            //        tv1a = tv2;
            //        break;
            //    }
            //}

            //if (tv1a == null)
            //    return;

            //if (tv1a.SelectedItem == null)
            //    return;

            //var tvi = (TreeViewItem)tv1a.SelectedItem;

            string actcode1 = "XXXXXXXXXXXX";

            //actcode1 = tvi.Tag.ToString();
            if (this.dgvAcc.SelectedItem != null)
            {

                var itm1 = (vmAccCodeBook1.AcListViewItem)this.dgvAcc.SelectedItem;
                actcode1 = itm1.actcode;
            }


            if (actcode1 == "XXXXXXXXXXXX")
                return;

            this.showTextBoxData(actcode1);
            this.chkAdEd.IsChecked = true;
            this.chkExtraInfo.IsChecked = false;
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
                    this.chkExtraInfo.IsChecked = true;
                    foreach (TreeView tv1 in this.treev1a())
                    {
                        if (tv1.Items.Count > 0)
                        {
                            this.treeAcc_GotFocus(tv1, null);
                            break;
                        }
                    }
                    break;
            }
        }

        private void btnAcc_Click(object sender, RoutedEventArgs e)
        {
            string btntContent1 = ((Button)sender).Name.ToString();
            Window mainWindow1 = Application.Current.MainWindow;
            Label lblBaloon1 = (Label)mainWindow1.FindName("lblBaloon1");
            //WpfProcessAccess.ShowBalloon(lblBaloon1, mainWindow1.Title, "You have pressed " + btntContent1 + " button", BalloonType.Information);

            string btnNam1 = ((Button)sender).Name.ToString().Trim();
            switch (btnNam1)
            {
                case "btnAdd":
                    this.spnlCodeEntry.IsEnabled = true;
                    this.txtActCode1.IsEnabled = false;
                    this.txtActCode2.IsEnabled = (true && this.chkLevel2.IsChecked == true);
                    this.txtActCode3.IsEnabled = (true && this.chkLevel3.IsChecked == true); ;
                    this.txtActCode4.IsEnabled = (true && this.chkLevel4.IsChecked == true); ;
                    this.btnUpdate.Tag = "Add";
                    this.addEdt();
                    this.txtActCode4.Focus();
                    break;
                case "btnEdit":
                    this.spnlCodeEntry.IsEnabled = true;
                    this.txtActCode1.IsEnabled = false;
                    this.txtActCode2.IsEnabled = false;
                    this.txtActCode3.IsEnabled = false;
                    this.txtActCode4.IsEnabled = false;
                    this.btnUpdate.Tag = "Edit";
                    this.addEdt();
                    this.txtAcDesc.Focus();
                    break;
                case "btnUpdate":
                    this.UpdateAcinf();
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
        private void UpdateAcinf()
        {
            string actcode1 = this.txtActCode1.Text.Trim() + this.txtActCode2.Text.Trim() + this.txtActCode3.Text.Trim() + this.txtActCode4.Text.Trim();
            actcode1 = actcode1.Replace(" ", "");
            if (actcode1.Length != 12)
            {
                MessageBox.Show("Could not add invalid code. Please try with valid code", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if ((actcode1.Substring(2, 2) == "00" && actcode1.Substring(4, 8) != "00000000")
               || (actcode1.Substring(4, 4) == "0000" && actcode1.Substring(8, 4) != "0000"))
            {
                MessageBox.Show("Could not add invalid code. Please try with valid code", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (this.txtActCode4.IsEnabled)
            {
                var list1 = this.LListAcInfCodeBook[0].FindAll(x => x.actcode == actcode1);
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

            this.chkELevel2.IsChecked = (actcode1.Substring(8, 4) == "0000" ? false : this.chkELevel2.IsChecked);
            string actdesc1 = this.txtAcDesc.Text.Trim();
            string actelev1 = (this.chkELevel2.IsChecked == true ? "2" : "");
            string acttype1 = this.txtActtype.Text.Trim();
            string acttdesc1 = this.txtActtdesc.Text.Trim();
            string AddEdit1 = this.btnUpdate.Tag.ToString().Trim();
            var pap1 = this.vm1.SetParamUpdateAcInf(WpfProcessAccess.CompInfList[0].comcpcod, actcode1, actdesc1, actelev1, acttype1, acttdesc1, AddEdit1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            if (ds2 == null)
            {
                if (WpfProcessAccess.DatabaseErrorInfoList.Count > 0)
                    MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);

                WpfProcessAccess.DatabaseErrorInfoList = null;
                return;
            }

            if (this.chkExtraInfo.IsChecked == true)
            {
                var pap1d = this.vmGenList1.SetParamUpdateGenInf(WpfProcessAccess.CompInfList[0].comcpcod, "ACINF", actcode1, this.ListGenDetailsInfo);
                DataSet ds2d = WpfProcessAccess.GetHmsDataSet(pap1: pap1d);
                if (ds2d == null)
                {
                    MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            }

            this.lblActCode.Content = this.txtActCode1.Text.Trim() + "-" + this.txtActCode2.Text.Trim() + "-" + this.txtActCode3.Text.Trim() + "-" + this.txtActCode4.Text.Trim();
            this.lblActCode.Tag = actcode1;

            btnUpdate.IsEnabled = false;
            btnAdd.IsEnabled = true;
            btnEdit.IsEnabled = true;
            this.spnlCodeEntry.IsEnabled = false;
            int expIndex = AddEditTreeNode(AddEdit1, actcode1, actdesc1);
            this.GetAcInfData();
            this.ScrollSelectListViewItem(actcode1);
        }

        private int AddEditTreeNode(string AddEdit1, string actcode1, string actdesc1)
        {
            TreeViewItem tvi1 = new TreeViewItem()
            {
                Tag = actcode1,
                ContextMenu = this.GetContextMenu(),
                Header = (actcode1.Substring(2, 10) == "0000000000" ? actcode1.Substring(0, 2) : (actcode1.Substring(4, 8) == "00000000" ? actcode1.Substring(2, 2) : (actcode1.Substring(8, 4) == "0000" ? actcode1.Substring(4, 4) : actcode1.Substring(8, 4)))) + " - " + actdesc1
            };
            string pactcode1 = (actcode1.Substring(4, 8) == "00000000" ? actcode1.Substring(0, 2) + "0000000000" : (actcode1.Substring(8, 4) == "0000" ? actcode1.Substring(0, 4) + "00000000" : actcode1.Substring(0, 8) + "0000"));

            TreeView tv1a = null;
            int expIndex = 0;
            foreach (var tv2 in this.treev1a())
            {
                if (tv2.Items.Count > 0)
                {
                    tv1a = tv2;
                    break;
                }
                expIndex++;
            }

            if (tv1a == null)
                return 0;

            #region Editing Description
            if (AddEdit1.Contains("Edit"))
            {
                foreach (TreeViewItem item1a in tv1a.Items)
                {
                    if (item1a.Tag.ToString() == actcode1)
                    {
                        item1a.Header = tvi1.Header;
                        break;
                    }
                    else if (item1a.Items.Count > 0)
                    {
                        foreach (TreeViewItem item1b in item1a.Items)
                        {
                            if (item1b.Tag.ToString() == actcode1)
                            {
                                item1b.Header = tvi1.Header;
                                break;
                            }
                            else if (item1b.Items.Count > 0)
                            {
                                foreach (TreeViewItem item1c in item1b.Items)
                                {
                                    if (item1c.Tag.ToString() == actcode1)
                                    {
                                        item1c.Header = tvi1.Header;
                                        break;
                                    }
                                    else if (item1c.Items.Count > 0)
                                    {
                                        foreach (TreeViewItem item1d in item1c.Items)
                                        {
                                            if (item1d.Tag.ToString() == actcode1)
                                            {
                                                item1d.Header = tvi1.Header;
                                                break;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                return expIndex;
            }
            #endregion

            #region Adding New Code and Description
            foreach (TreeViewItem item1a in tv1a.Items)
            {
                if (item1a.Tag.ToString() == pactcode1)
                {
                    item1a.Items.Add(tvi1);
                    break;
                }
                else if (item1a.Items.Count > 0)
                {
                    foreach (TreeViewItem item1b in item1a.Items)
                    {
                        if (item1b.Tag.ToString() == pactcode1)
                        {
                            item1b.Items.Add(tvi1);
                            item1b.IsExpanded = true;
                            break;
                        }
                        else if (item1b.Items.Count > 0)
                        {
                            foreach (TreeViewItem item1c in item1b.Items)
                            {
                                if (item1c.Tag.ToString() == pactcode1)
                                {
                                    item1c.Items.Add(tvi1);
                                    item1c.IsExpanded = true;
                                    break;
                                }
                            }
                        }
                    }

                }
            }
            tvi1.IsSelected = true;
            return expIndex;
        }
            #endregion



        private void chkAdEd_Unchecked(object sender, RoutedEventArgs e)
        {
            this.gridCodeEntry.Visibility = Visibility.Collapsed;
        }

        private void chkExtraInfo_Click(object sender, RoutedEventArgs e)
        {
            string ActCod1 = this.lblActCode.Tag.ToString();
            this.stkpcanvasCode.Height = (this.chkExtraInfo.IsChecked == true ? 540 : 300);

            if (sender == null)
                return;
            string ActCod2 = ActCod1.Substring(0, 4);

            this.dgExtraInfo.ItemsSource = null;
            this.stkpExtraCode.Visibility = (this.chkExtraInfo.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);

            string gcodeGroup = ((ActCod2.Substring(0, 4) == "1902" || ActCod2 == "2902") ? "ACBD" : "YYYY");

            var pap1 = this.vmGenList1.SetParamGenDetailsInf(WpfProcessAccess.CompInfList[0].comcpcod, "ACINF", ActCod1, gcodeGroup);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;


            this.ListGenDetailsInfo = ds1.Tables[0].DataTableToList<vmHmsGeneralList1.GenDetailsListInfo>();
            this.dgExtraInfo.ItemsSource = this.ListGenDetailsInfo;
            this.btnUpdate.IsEnabled = true;
            this.btnAcc_Click(this.btnEdit, null);
        }

        private void lbldgExtraInfoRptSlno_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm to add space", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
              MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            string actCode1 = this.lblActCode.Tag.ToString().Trim();
            string genCode1 = ((Label)sender).Tag.ToString();
            this.dgExtraInfo.ItemsSource = null;

            int index1 = this.ListGenDetailsInfo.FindLastIndex(x => x.gencode == genCode1);
            string newRptNo = (int.Parse(this.ListGenDetailsInfo[index1].repeatsl.Trim()) + 1).ToString();
            this.ListGenDetailsInfo.Add(new vmHmsGeneralList1.GenDetailsListInfo() { gencode = genCode1, gendesc = "          Do", repeatsl = newRptNo, tblcode = actCode1, dataval = "", slnum = 0 });
            this.ListGenDetailsInfo.Sort(delegate(vmHmsGeneralList1.GenDetailsListInfo x, vmHmsGeneralList1.GenDetailsListInfo y)
            {
                return (x.gencode + x.repeatsl.Trim()).CompareTo(y.gencode + y.repeatsl.Trim());
            });
            index1 = 1;
            foreach (var item in this.ListGenDetailsInfo)
            {
                item.slnum = index1;
                ++index1;
            }
            this.dgExtraInfo.ItemsSource = this.ListGenDetailsInfo;
        }

        private void btnFindCode_Click(object sender, RoutedEventArgs e)
        {
            if (this.AutoCompleteActCode.SelectedValue == null)
                return;
            string actcode1 = this.AutoCompleteActCode.SelectedValue.ToString().Trim();
            this.AutoCompleteActCode.SelectedValue = null;
            if (actcode1.Length == 0)
                return;

            this.ScrollSelectListViewItem(actcode1);
        }

        private void dgvAcc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.dgvAcc.SelectedItem == null)
                return;

            var item1 = (vmAccCodeBook1.AcListViewItem)this.dgvAcc.SelectedItem;
            this.chkAdEd.IsChecked = true;
            this.showTextBoxData(item1.actcode);
            this.btnCancel.Focus();
        }

        private void dgvAcc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;

            this.dgvAcc_MouseDoubleClick(null, null);

            //var item1 = (vmAccCodeBook1.AcListViewItem)this.dgvAcc.SelectedItem;
            //this.chkAdEd.IsChecked = true;
            //this.showTextBoxData(item1.actcode);
            //this.btnCancel.Focus();
        }

        private void AutoCompleteActCode_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemActdesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityGeneral.AcInfCodeBook> GetItemActdesc(string Pattern)
        {
            // match on contain (could do starts with) 

            return new ObservableCollection<HmsEntityGeneral.AcInfCodeBook>(
               this.LListAcInfCodeBook[0].Where((x, match) => x.actcode.Substring(9, 3) != "000" && x.actdesc1.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(200).OrderBy(m => m.actdesc1));
        }

        private void dgvAcc_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    this.dgvAcc.CommitEdit(DataGridEditingUnit.Cell, false);
                    this.dgvAcc.CommitEdit(DataGridEditingUnit.Row, false);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
    }

}
