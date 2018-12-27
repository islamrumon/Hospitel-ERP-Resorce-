using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
//using ASITHmsViewMan;
using ASITHmsViewMan.General;
using ASITFunLib;
using System.Globalization;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt1GenAcc.General;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmSirCodeBook1.xaml
    /// ToDo List for this form
    /// ===============================================================
    /// 01. Completed Task:     Main resource group selection option
    /// 02. Completed Task:     Tree Definition for each selected group
    /// 03. Completed Task:     Details List View for each selected group
    /// 04. Completed Task:     Add/Edit Resource Code
    /// 05. Completed Task:     Print partial resource code book
    /// 06. Underconstion :     Add/Edit Additional Details for eache resource code
    /// 
    /// </summary>
    /// 

    public partial class frmSirCodeBook1 : UserControl
    {
        private bool FrmInitialized = false;
        private List<List<HmsEntityGeneral.SirInfCodeBook>> LListSirInfCodeBook { get; set; }
        private List<HmsEntityGeneral.SirInfCodeBook> ListSirInfMainCodeBook { get; set; }
        private List<HmsEntityGeneral.SirInfCodeBook> ListSirInfMain1CodeBook { get; set; }
        private List<HmsEntityGeneral.SirInfCodeBook> ListSirInfMain2CodeBook { get; set; }

        private List<vmHmsGeneralList1.GenDetailsListInfo> ListGenDetailsInfo { get; set; }

        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private vmSirCodeBook1 vm1 = new vmSirCodeBook1();
        private string SirManinGroup = "";
        public frmSirCodeBook1()
        {
            InitializeComponent();
        }
        public frmSirCodeBook1(string MainGroup = "")
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            this.SirManinGroup = MainGroup;
            //this.GetGroupSirInfData();
            //this.cmbMainGroup1.DataContext = this;
            this.UnCheckedAllPopups();
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
                if (this.SirManinGroup.Length == 0)
                {
                    WpfProcessAccess.AccSirCodeList = null;
                    WpfProcessAccess.StaffGroupList = null;
                    WpfProcessAccess.StaffList = null;
                    WpfProcessAccess.SupplierContractorList = null;
                    WpfProcessAccess.InvItemGroupList = null;
                    WpfProcessAccess.InvItemList = null;
                }
                this.gridCodeEntry.Visibility = Visibility.Collapsed;
                this.ActivateAuthObjects();
                //this.lvSir.Visibility = Visibility.Hidden;
                this.dgvSir.Visibility = Visibility.Hidden;

                this.GetGroupSirInfData();
                if (this.ListSirInfMain1CodeBook.Count == 0)
                    return;

                this.cmbMainGroup1.ItemsSource = this.ListSirInfMain1CodeBook;// .DataContext = this;
                this.cmbMainGroup1.SelectedIndex = 0;
                this.cmbMainGroup1_DropDownClosed(null, null);
                this.cmbMainGroup_DropDownClosed(this.cmbMainGroup, null);
                string grup = "%";
                this.GetSirInfData(grup);
            }
        }

        private void ActivateAuthObjects()
        {
            if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmSirCodeBook1_chkAdEd") == null)
                this.chkAdEd.Visibility = Visibility.Hidden;
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

            string sircode1 = "XXXXXXXXXXXX";
            //if (this.treeSir.SelectedItem != null)
            //{
            //    var tvi = (TreeViewItem)this.treeSir.SelectedItem;
            //    sircode1 = tvi.Tag.ToString();
            //}
            //else 
            if (this.dgvSir.SelectedItem != null)
            {
                var itm1 = (vmSirCodeBook1.SirListViewItem)this.dgvSir.SelectedItem;
                sircode1 = itm1.sircode;
            }

            if (sircode1 == "XXXXXXXXXXXX")
                return;

            this.showTextBoxData(sircode1);

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
                    this.btnCancel.Focus();
                    break;
                case "Add/Edit Extra Info":
                    this.chkExtraInfo.IsChecked = true;
                    this.treeSir_GotFocus(this.treeSir, null);
                    break;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+-.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void GetGroupSirInfData()
        {
            var pap1 = this.vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, this.SirManinGroup + "%", "1234");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.ListSirInfMainCodeBook = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();

            foreach (var item in this.ListSirInfMainCodeBook)
            {
                item.sirtype = item.sircode.Substring(0, 4) + " - " + item.sirdesc.Trim();
                item.sirtdes = item.sircode.Substring(0, 9) + " - " + item.sirdesc.Trim();
            }

            this.ListSirInfMain1CodeBook = this.ListSirInfMainCodeBook.FindAll(x => x.sircode.Substring(4, 8) == "00000000");
        }

        private void cmbMainGroup1_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbMainGroup1.SelectedValue == null)
                return;

            this.cmbMainGroup1.ToolTip = this.cmbMainGroup1.Text.ToString();
            string maincode1 = this.cmbMainGroup1.SelectedValue.ToString().Substring(0, 4);
            maincode1 = (maincode1.Substring(2, 2) == "00" ? maincode1.Substring(0, 2) : maincode1);


            this.ListSirInfMain2CodeBook = this.ListSirInfMainCodeBook.FindAll(x => x.sircode.Substring(0, maincode1.Length) == maincode1.Substring(0, maincode1.Length));
            this.cmbMainGroup.ItemsSource = this.ListSirInfMain2CodeBook;
            this.cmbMainGroup.SelectedIndex = 0;
            this.cmbMainGroup.ToolTip = this.cmbMainGroup.Text.ToString();
        }
        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            this.stklstv.IsEnabled = false;
            // Important Note Level Wise Codebook to be prepare as Levels are defined under spnlLevel
            // Note that, it is not considerd for Code Book Tree and List View Preparation --- Hafiz 30-Dev-2015

            if (this.cmbMainGroup.SelectedValue == null)
                return;

            this.UnCheckedAllPopups();
            this.treeSir.Items.Clear();

            if (btnShowList.Content.ToString() == "_Next")
            {
                this.spnlLevel.IsEnabled = true;
                //this.lvSir.Visibility = Visibility.Hidden;
                this.dgvSir.Visibility = Visibility.Hidden;
                this.cmbMainGroup1.IsEnabled = true;
                this.cmbMainGroup.IsEnabled = true;

                this.btnShowList.Content = "_Show";
                return;
            }
            this.spnlLevel.IsEnabled = false;
            //this.lvSir.Visibility = Visibility.Hidden;
            this.dgvSir.Visibility = Visibility.Visible;
            this.cmbMainGroup1.IsEnabled = false;
            this.cmbMainGroup.IsEnabled = false;
            string mGrp1 = this.cmbMainGroup.SelectedValue.ToString().Substring(0, 9);
            mGrp1 = (mGrp1.Substring(7, 2) == "00" ? mGrp1.Substring(0, 7) : mGrp1);
            mGrp1 = (mGrp1.Length == 7 && mGrp1.Substring(4, 3) == "000" ? mGrp1.Substring(0, 4) : mGrp1);
            mGrp1 = (mGrp1.Length == 4 && mGrp1.Substring(2, 2) == "00" ? mGrp1.Substring(0, 2) : mGrp1);

            this.GetSirInfData(mGrp1);
            this.vm1.BindTree(mGrp1, this.treeSir, this.LListSirInfCodeBook, this.GetContextMenu());
            this.PrepareListView();

            this.btnShowList.Content = "_Next";
            this.stklstv.IsEnabled = true;
        }

        private void cmbMainGroup_DropDownClosed(object sender, EventArgs e)
        {
            this.cmbMainGroup.ToolTip = ((ComboBox)sender).Text.ToString();
        }


        private void btnPrint1_Click(object sender, RoutedEventArgs e)
        {

            string mGrp1 = this.cmbMainGroup.SelectedValue.ToString().Substring(0, 9);
            mGrp1 = (mGrp1.Substring(7, 2) == "00" ? mGrp1.Substring(0, 7) : mGrp1);
            mGrp1 = (mGrp1.Length == 7 && mGrp1.Substring(4, 3) == "000" ? mGrp1.Substring(0, 4) : mGrp1);
            mGrp1 = (mGrp1.Length == 4 && mGrp1.Substring(2, 2) == "00" ? mGrp1.Substring(0, 2) : mGrp1);

            string strLevels = "";
            strLevels = (this.chkLevel1.IsChecked == true ? "1" : "1");
            strLevels = (this.chkLevel2.IsChecked == true ? "2" : strLevels);
            strLevels = (this.chkLevel3.IsChecked == true ? "3" : strLevels);
            strLevels = (this.chkLevel4.IsChecked == true ? "4" : strLevels);
            strLevels = (this.chkLevel5.IsChecked == true ? "5" : strLevels);

            this.GetSirInfData(mGrp1);
            this.chkPrint.IsChecked = false;
            if (this.LListSirInfCodeBook == null)
                return;
            var list1 = this.LListSirInfCodeBook[0];
            string RptTitle = "Subsidiary Chart of Accounts (Level - " + strLevels + ")";
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            LocalReport rpt1 = GeneralReportSetup.GetLocalReport("General.rptSirCodeBook1", list1, RptTitle, list3); // ( R_01_RptSetup.RptSetupItemList1(ds1, ds2);
            string WindowTitle1 = "Subsidiary Accounts Code Book Report";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }
        private void btnExpandCollapse_Click(object sender, RoutedEventArgs e)
        {
            bool ec1 = (((Button)sender).Name == "btnExpandAll" ? true : false);
            vmHmsGeneral1.TreeViewExpandCollapse(this.treeSir, ec1);
        }
        private void addEdt()
        {

            btnUpdate.IsEnabled = true;
            btnAdd.IsEnabled = false;
            btnEdit.IsEnabled = false;
        }
        //private void UpdateDelete()
        //{

        //    btnUpdate.IsEnabled = false;
        //    btnAdd.IsEnabled = true;
        //    btnEdit.IsEnabled = true;
        //}
        private void btnAcc_Click(object sender, RoutedEventArgs e)
        {
            string btnNam1 = ((Button)sender).Name.ToString().Trim();
            Window mainWindow1 = Application.Current.MainWindow;
            Label lblBaloon1 = (Label)mainWindow1.FindName("lblBaloon1");

            switch (btnNam1)
            {
                case "btnAdd":
                    this.spnlCodeEntry.IsEnabled = true;
                    this.txtSirCode1.IsEnabled = false;
                    this.txtSirCode2.IsEnabled = false;// (true && this.chkLevel2.IsChecked == true);
                    this.txtSirCode3.IsEnabled = (true && this.chkLevel3.IsChecked == true); ;
                    this.txtSirCode4.IsEnabled = (true && this.chkLevel4.IsChecked == true); ;
                    this.txtSirCode5.IsEnabled = (true && this.chkLevel5.IsChecked == true); ;
                    this.btnUpdate.Tag = "Add";
                    this.addEdt();
                    this.txtSirCode5.Focus();
                    break;
                case "btnEdit":
                    this.spnlCodeEntry.IsEnabled = true;
                    this.txtSirCode1.IsEnabled = false;
                    this.txtSirCode2.IsEnabled = false;
                    this.txtSirCode3.IsEnabled = false;
                    this.txtSirCode4.IsEnabled = false;
                    this.txtSirCode5.IsEnabled = false;
                    this.btnUpdate.Tag = "Edit";
                    this.addEdt();
                    this.txtsirdesc.Focus();
                    break;
                case "btnUpdate":
                    this.UpdateSirInf();
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


        private void treeSir_GotFocus(object sender, RoutedEventArgs e)
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

            var row1 = this.LListSirInfCodeBook[0].FindAll(x => x.sircode == Code1).ToArray();

            //this.txtblCodeExtra.Text = Code1.Substring(0, 2) + "-" + Code1.Substring(2, 2) + "-" + Code1.Substring(4, 3) + "-" + Code1.Substring(7, 2) + "-" + Code1.Substring(9, 3) + " : " + row1[0].sirdesc.Trim();
        }
        private void PrepareListView()
        {
            //var lst111 = this.LListSirInfCodeBook[0].AsEnumerable().Select((x, index) => new { slnum = (index + 1).ToString() + ".", x.sircode, x.sirdesc, x.sirtype, x.sirtdes, x.sirunit, x.rowid, x.rowtime, x.sircode1, x.sirdesc1 }).ToList();
            var lst111a = this.LListSirInfCodeBook[0].AsEnumerable().Select((x, index) => new { slnum = (index + 1).ToString() + ".", x.sircode, x.sircode1, x.sirtype, x.sirdesc, x.sirtdes,
                x.sirunit, x.sirunit2, x.sirunit3, x.siruconf, x.siruconf3 }).ToList();
            List<vmSirCodeBook1.SirListViewItem> list1 = new List<vmSirCodeBook1.SirListViewItem>();
            this.AtxtSirCode.Items.Clear();
            this.AtxtSirCode.AutoSuggestionList.Clear();



            foreach (var itema in lst111a)
            {
                list1.Add(new vmSirCodeBook1.SirListViewItem()
                {
                    slnum = itema.slnum,
                    sircode = itema.sircode,
                    sircode1 = itema.sircode1,
                    sirtype = itema.sirtype,
                    sirdesc = itema.sirdesc,
                    sirtdes = itema.sirtdes,
                    sirunit = itema.sirunit,
                    sirunit2 = itema.sirunit2,
                    sirunit3 = itema.sirunit3,
                    siruconf = itema.siruconf,
                    siruconf3 = itema.siruconf3,
                    fbold = (itema.sircode.Substring(9, 3) =="000" ? "Bold" : "Normal"),
                    fcolor = (itema.sircode.Substring(2, 10) == "0000000000" ? "Maroon" : (itema.sircode.Substring(4, 8) == "00000000" ? "Blue" :
                         (itema.sircode.Substring(7, 5) == "00000" ? "DarkGreen" : "Black")))
                });
                this.AtxtSirCode.AddSuggstionItem(itema.sircode + " - " + itema.sirdesc.Trim(), itema.sircode);
            }

            //this.lvSir.ItemsSource = list1;
            this.dgvSir.ItemsSource = list1;
            this.dgvSir.ContextMenu = this.GetContextMenu();
        }

        private void ScrollSelectListViewItem(string sircode1)
        {

            var item22 = this.dgvSir.Items.OfType<vmSirCodeBook1.SirListViewItem>().ToList().FindAll(x=>x.sircode == sircode1);
            if(item22.Count > 0)
            {
                this.dgvSir.ScrollIntoView(item22[0]);
                this.dgvSir.SelectedItem = item22[0];
            }
            return;
        }

        private void showTextBoxData(string Code1)
        {
            this.txtblMnGr.Text = "";
            string cod1 = Code1.Substring(0, 2) + "0000000000";
            string cod2 = Code1.Substring(0, 4) + "00000000";
            string cod3 = Code1.Substring(0, 7) + "00000";
            string cod4 = Code1.Substring(0, 9) + "000";

            var maincod1 = this.ListSirInfMainCodeBook.FindAll(x => x.sircode == cod1);
            var subcod1 = this.ListSirInfMainCodeBook.FindAll(x => x.sircode == cod2);
            var subcod2 = this.ListSirInfMainCodeBook.FindAll(x => x.sircode == cod3);
            var subcod3 = this.ListSirInfMainCodeBook.FindAll(x => x.sircode == cod4);
            //var subcod4 = this.LListSirInfCodeBook[4].FindAll(x => x.sircode == cod4);

            string MainDesc1 = (maincod1.Count > 0 && maincod1[0].sircode != Code1 ? " Main : " + maincod1[0].sircode1.Trim() + " " + maincod1[0].sirdesc.Trim() : "");
            MainDesc1 += (maincod1.Count > 0 && subcod1.Count > 0 && subcod1[0].sircode != Code1 ? "\n   Sub : " + subcod1[0].sircode1.Trim() + " " + subcod1[0].sirdesc.Trim() : "");
            MainDesc1 += (maincod1.Count > 0 && subcod1.Count > 0 && subcod2.Count > 0 && subcod2[0].sircode != Code1 ? "\nSub.2 : " + subcod2[0].sircode1.Trim() + " " + subcod2[0].sirdesc.Trim() : "");
            MainDesc1 += (maincod1.Count > 0 && subcod1.Count > 0 && subcod2.Count > 0 && subcod3.Count > 0 && subcod3[0].sircode != Code1 ? "\nSub.3 : " + subcod3[0].sircode1.Trim() + " " + subcod3[0].sirdesc.Trim() : "");


            this.txtblMnGr.Text = MainDesc1;


            HmsEntityGeneral.SirInfCodeBook[] row1 = this.LListSirInfCodeBook[0].FindAll(x => x.sircode == Code1).ToArray();

            string sirCod1 = row1[0].sircode.Trim();
            this.txtSirCode1.Text = sirCod1.Substring(0, 2);
            this.txtSirCode2.Text = sirCod1.Substring(2, 2);
            this.txtSirCode3.Text = sirCod1.Substring(4, 3);
            this.txtSirCode4.Text = sirCod1.Substring(7, 2);
            this.txtSirCode5.Text = sirCod1.Substring(9, 3);
            this.lblSirCode.Content = row1[0].sircode1.ToString();
            this.lblSirCode.Tag = sirCod1;

            this.txtsirdesc.Text = row1[0].sirdesc.Trim();
            this.txtsirunit.Text = row1[0].sirunit.Trim();
            this.txtsirunit2.Text = row1[0].sirunit2.Trim();
            this.txtsirunit3.Text = row1[0].sirunit3.Trim();
            this.txtsiruconf.Text = row1[0].siruconf.ToString("#,##0.000000");
            this.txtsiruconf3.Text = row1[0].siruconf3.ToString("#,##0.000000");
            this.txtsirtype.Text = row1[0].sirtype.Trim();
            this.txtsirtdes.Text = row1[0].sirtdes.Trim();
            this.spnlCodeEntry.IsEnabled = false;
            this.stkpUnit.Visibility = Visibility.Visible;
            //this.stkpUnit.Visibility = ((sirCod1.Substring(0, 1) == "0" || sirCod1.Substring(0, 1) == "4") && sirCod1.Substring(9, 3) != "000" ? Visibility.Visible : Visibility.Collapsed);
            string sirCod2 = sirCod1.Substring(0, 2);

            // Details view shows based on following options
            if (sirCod1.Substring(9, 3) == "000" || !(sirCod2 == "25" || sirCod2 == "51" || sirCod2 == "55" || sirCod2 == "98" || sirCod2 == "99"))
                this.chkExtraInfo.Visibility = Visibility.Hidden;
            else
                this.chkExtraInfo.Visibility = Visibility.Visible;
            this.chkExtraInfo.IsChecked = false;
            this.stkpExtraCode.Visibility = Visibility.Collapsed;
            this.chkExtraInfo_Click(null, null);

        }

        private void txtsir_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter)) return;

            var element = sender as UIElement;
            if (element != null) element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
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
                        //if (lvSir.SelectedIndex >= 0)
                        //{
                        //    var item1 = (vmSirCodeBook1.SirListViewItem)this.lvSir.SelectedItem;
                        //    this.showTextBoxData(item1.sircode);
                        //}
                        if (this.dgvSir.SelectedIndex >= 0)
                        {
                            var item1 = (vmSirCodeBook1.SirListViewItem)this.dgvSir.SelectedItem;
                            this.showTextBoxData(item1.sircode);
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

        private void GetSirInfData(string mGrp1)
        {
            string strLevels = "";
            strLevels = (this.chkLevel1.IsChecked == true ? "1" : "1");
            strLevels = (this.chkLevel2.IsChecked == true ? "12" : strLevels);
            strLevels = (this.chkLevel3.IsChecked == true ? "123" : strLevels);
            strLevels = (this.chkLevel4.IsChecked == true ? "1234" : strLevels);
            strLevels = (this.chkLevel5.IsChecked == true ? "12345" : strLevels);


            this.chkLevel1.IsChecked = strLevels.Contains("1");
            this.chkLevel2.IsChecked = strLevels.Contains("2");
            this.chkLevel3.IsChecked = strLevels.Contains("3");
            this.chkLevel4.IsChecked = strLevels.Contains("4");
            this.chkLevel5.IsChecked = strLevels.Contains("5");

            ASITFunParams.ProcessAccessParams pap1 = this.vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, mGrp1, strLevels);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var ListAcInfCodeBook = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
            this.LListSirInfCodeBook = this.vm1.GetGroupListSirInfCodeBook(ListAcInfCodeBook);
        }    

        private void UpdateSirInf()
        {
            string Sircode1 = this.txtSirCode1.Text.Trim() + this.txtSirCode2.Text.Trim() + this.txtSirCode3.Text.Trim() + this.txtSirCode4.Text.Trim() + this.txtSirCode5.Text.Trim();
            if ((Sircode1.Substring(2, 2) == "00" && Sircode1.Substring(4, 8) != "00000000")
               || (Sircode1.Substring(4, 3) == "000" && Sircode1.Substring(7, 5) != "00000") || (Sircode1.Substring(7, 2) == "00" && Sircode1.Substring(9, 3) != "000"))
            {
                MessageBox.Show("Could not add invalid code. Please try with valid code", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (this.txtSirCode5.IsEnabled)
            {
                var list1 = this.LListSirInfCodeBook[0].FindAll(x => x.sircode == Sircode1);
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


            string Sirdesc1 = this.txtsirdesc.Text.Trim();
            string Sirtype1 = this.txtsirtype.Text.Trim();
            string Sirtdesc1 = this.txtsirtdes.Text.Trim();
            string sirUnit1 = this.txtsirunit.Text.Trim();
            string sirUnit2 = this.txtsirunit2.Text.Trim();
            string sirUnit3 = this.txtsirunit3.Text.Trim();
            string ConvFact12 = "0" + this.txtsiruconf.Text.Trim();
            string ConvFact13 = "0" + this.txtsiruconf3.Text.Trim();

            string AddEdit1 = this.btnUpdate.Tag.ToString().Trim();
            var pap1 = this.vm1.SetParamUpdateSirInf(WpfProcessAccess.CompInfList[0].comcpcod, Sircode1, Sirdesc1, Sirtype1, Sirtdesc1, sirUnit1, sirUnit2, sirUnit3, ConvFact12, ConvFact13, AddEdit1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            if (ds2 == null)
            {
                MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            if (this.chkExtraInfo.IsChecked == true)
            {
                var pap1d = this.vmGenList1.SetParamUpdateGenInf(WpfProcessAccess.CompInfList[0].comcpcod, "SIRINF", Sircode1, this.ListGenDetailsInfo);
                DataSet ds2d = WpfProcessAccess.GetHmsDataSet(pap1: pap1d);
                if (ds2d == null)
                {
                    MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
            }

            this.lblSirCode.Content = this.txtSirCode1.Text.Trim() + "-" + this.txtSirCode2.Text.Trim() + "-" + this.txtSirCode3.Text.Trim() + "-" + this.txtSirCode4.Text.Trim() + "-" + this.txtSirCode5.Text.Trim();
            this.lblSirCode.Tag = Sircode1;

            btnUpdate.IsEnabled = false;
            btnAdd.IsEnabled = true;
            btnEdit.IsEnabled = true;

            string mGrp1 = this.cmbMainGroup.SelectedValue.ToString().Substring(0, 9);
            mGrp1 = (mGrp1.Substring(7, 2) == "00" ? mGrp1.Substring(0, 7) : mGrp1);
            mGrp1 = (mGrp1.Length == 7 && mGrp1.Substring(4, 3) == "000" ? mGrp1.Substring(0, 4) : mGrp1);
            mGrp1 = (mGrp1.Length == 4 && mGrp1.Substring(2, 2) == "00" ? mGrp1.Substring(0, 2) : mGrp1);

            this.AddEditTreeNode(AddEdit1, Sircode1, Sirdesc1);
            this.GetSirInfData(mGrp1);

            this.PrepareListView();

            this.ScrollSelectListViewItem(Sircode1);


        }

        private void CodeValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void AddEditTreeNode(string AddEdit1, string Sircode1, string Sirdesc1)
        {
            TreeViewItem tvi1 = new TreeViewItem()
            {
                Tag = Sircode1,
                ContextMenu = this.GetContextMenu(),
                Header = (Sircode1.Substring(2, 10) == "0000000000" ? Sircode1.Substring(0, 2) : (Sircode1.Substring(4, 8) == "00000000" ? Sircode1.Substring(2, 2) : (Sircode1.Substring(7, 5) == "00000" ? Sircode1.Substring(4, 3) : (Sircode1.Substring(9, 3) == "000" ? Sircode1.Substring(7, 2) : Sircode1.Substring(9, 3))))) + " - " + Sirdesc1
            };
            string psircode1 = (Sircode1.Substring(4, 8) == "00000000" ? Sircode1.Substring(0, 2) + "0000000000" : (Sircode1.Substring(7, 5) == "00000" ? Sircode1.Substring(0, 4) + "00000000" : (Sircode1.Substring(9, 3) == "000" ? Sircode1.Substring(0, 7) + "00000" : Sircode1.Substring(0, 9) + "000")));
            //string pactcode1 = (Sircode1.Substring(4, 8) == "00000000" ? Sircode1.Substring(0, 2) + "0000000000" : (Sircode1.Substring(8, 4) == "0000" ? Sircode1.Substring(0, 4) + "00000000" : Sircode1.Substring(0, 8) + "0000"));
            TreeView tv1a = null;


            tv1a = (TreeView)treeSir;

            if (tv1a == null)
                return;

            #region Editing Description
            if (AddEdit1.Contains("Edit"))
            {
                foreach (TreeViewItem item1a in tv1a.Items)
                {
                    if (item1a.Tag.ToString() == Sircode1)
                    {
                        item1a.Header = tvi1.Header;
                        break;
                    }
                    else if (item1a.Items.Count > 0)
                    {
                        foreach (TreeViewItem item1b in item1a.Items)
                        {
                            if (item1b.Tag.ToString() == Sircode1)
                            {
                                item1b.Header = tvi1.Header;
                                break;
                            }
                            else if (item1b.Items.Count > 0)
                            {
                                foreach (TreeViewItem item1c in item1b.Items)
                                {
                                    if (item1c.Tag.ToString() == Sircode1)
                                    {
                                        item1c.Header = tvi1.Header;
                                        break;
                                    }
                                    else if (item1c.Items.Count > 0)
                                    {
                                        foreach (TreeViewItem item1d in item1c.Items)
                                        {
                                            if (item1d.Tag.ToString() == Sircode1)
                                            {
                                                item1d.Header = tvi1.Header;
                                                break;
                                            }
                                            else if (item1d.Items.Count > 0)
                                            {
                                                foreach (TreeViewItem item1f in item1d.Items)
                                                {
                                                    if (item1f.Tag.ToString() == Sircode1)
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

        private void chkExtraInfo_Click(object sender, RoutedEventArgs e)
        {
            string sirCod1 = this.lblSirCode.Tag.ToString();
            //this.canvasCode.Height = (this.chkExtraInfo.IsChecked == true ? 540 : 300);
            this.stkpcanvasCode.Height = (this.chkExtraInfo.IsChecked == true ? 540 : 300);

            if (sender == null)
                return;
            string sirCod2 = sirCod1.Substring(0, 4);

            this.dgExtraInfo.ItemsSource = null;
            this.stkpExtraCode.Visibility = (this.chkExtraInfo.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
            //this.ListGenDetailsInfo.Clear();

            string gcodeGroup = ((sirCod2.Substring(0, 1) == "5" || sirCod2.Substring(0, 2) == "98" || sirCod2.Substring(0, 2) == "99") ? "SICD" : (sirCod2.Substring(0, 4) == "2502" ? "SILC" : "YYYY"));

            var pap1 = this.vmGenList1.SetParamGenDetailsInf(WpfProcessAccess.CompInfList[0].comcpcod, "SIRINF", sirCod1, gcodeGroup);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;


            this.ListGenDetailsInfo = ds1.Tables[0].DataTableToList<vmHmsGeneralList1.GenDetailsListInfo>();
            this.dgExtraInfo.ItemsSource = this.ListGenDetailsInfo;
            this.btnUpdate.IsEnabled = true;
            this.btnAcc_Click(this.btnEdit, null);
        }

        private void chkAdEd_Unchecked(object sender, RoutedEventArgs e)
        {
            this.gridCodeEntry.Visibility = Visibility.Collapsed;
        }

        private void lbldgExtraInfoRptSlno_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (System.Windows.MessageBox.Show("Confirm to add space", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
              MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.DefaultDesktopOnly) != MessageBoxResult.Yes)
            {
                return;
            }
            string sirCode1 = this.lblSirCode.Tag.ToString().Trim();
            string genCode1 = ((Label)sender).Tag.ToString();
            this.dgExtraInfo.ItemsSource = null;

            int index1 = this.ListGenDetailsInfo.FindLastIndex(x => x.gencode == genCode1);
            string newRptNo = (int.Parse(this.ListGenDetailsInfo[index1].repeatsl.Trim()) + 1).ToString();
            this.ListGenDetailsInfo.Add(new vmHmsGeneralList1.GenDetailsListInfo() { gencode = genCode1, gendesc = "          Do", repeatsl = newRptNo, tblcode = sirCode1, dataval = "", slnum = 0 });
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

        private void btnFindSirCode_Click(object sender, RoutedEventArgs e)
        {
            if (this.AutoCompleteSirCode.SelectedValue == null)
                return;

            string sircode1 = this.AutoCompleteSirCode.SelectedValue.ToString().Trim();
            this.AutoCompleteSirCode.SelectedValue = null;
            if (sircode1.Length == 0)
                return;

            this.ScrollSelectListViewItem(sircode1);
        }

        private void dgvSir_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.dgvSir.SelectedItem == null)
                return;

            var item1 = (vmSirCodeBook1.SirListViewItem)this.dgvSir.SelectedItem;
            this.chkAdEd.IsChecked = true;
            this.showTextBoxData(item1.sircode);
            this.btnCancel.Focus();
        }

        private void dgvSir_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key != Key.Return)
                return;
            this.dgvSir_MouseDoubleClick(null, null);
            //var item1 = (vmSirCodeBook1.SirListViewItem)this.dgvSir.SelectedItem;
            ////MessageBox.Show(item1.actcode);
            //this.chkAdEd.IsChecked = true;
            //this.showTextBoxData(item1.sircode);
            //this.btnCancel.Focus();
        }

        private void AutoCompleteSirCode_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetItemSirdesc(string Pattern)
        {
            // match on contain (could do starts with) 

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                this.LListSirInfCodeBook[0].Where((x, match) => x.sircode.Substring(9, 3) != "000" && x.sirdesc1.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(200).OrderBy(m => m.sirdesc1));
        }

        private void dgvSir_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    this.dgvSir.CommitEdit(DataGridEditingUnit.Cell, false);
                    this.dgvSir.CommitEdit(DataGridEditingUnit.Row, false);
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
