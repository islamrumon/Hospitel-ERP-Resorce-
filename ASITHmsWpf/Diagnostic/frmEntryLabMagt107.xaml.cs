using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Diagnostic;
using ASITHmsViewMan.General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ASITHmsWpf.UserControls;
using ASITHmsRpt5Diagnostic;
using Microsoft.Reporting.WinForms;

namespace ASITHmsWpf.Diagnostic
{
    /// <summary>
    /// Interaction logic for frmEntryLabMagt107.xaml
    /// </summary>
    public partial class frmEntryLabMagt107 : UserControl
    {

        private List<HmsEntityGeneral.AcInfCodeBook> TestElementList = new List<HmsEntityGeneral.AcInfCodeBook>();

        private List<HmsEntityCommercial.HmsServiceItem> ServiceItemList = new List<HmsEntityCommercial.HmsServiceItem>();             // Hospital/Diagnostic Centre Service Item List
        private List<HmsEntityGeneral.SirInfCodeBook> ListSirInfCodeBook = new List<HmsEntityGeneral.SirInfCodeBook>();
        private List<vmEntryLabMgt1.DiagRptTemplate> ListRptTemplate = new List<vmEntryLabMgt1.DiagRptTemplate>();


        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private vmEntryLabMgt1 vm1 = new vmEntryLabMgt1();
        public frmEntryLabMagt107()
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

            this.Objects_On_Init();

        }

        private void Objects_On_Init()
        {
            //for(int i=1; i<=20; i++)
            //    this.cmbGroup.Items.Add(i.ToString("00"));

            //for (int j = 0; j <= 20; j++)
            //    this.cmbSlNum.Items.Add(j.ToString("00"));

            //this.cmbGroup.Items.Add(new ComboBoxItem() { Content= i.ToString("00")});

            this.chkTestName.IsChecked = false;
            this.gridItemList.Visibility = Visibility.Collapsed;
            this.stkpEntry.Visibility = Visibility.Collapsed;

            this.lblSelectedTest.Content = "";
            this.lblSelectedTest.Tag = "";
            this.chkElement.IsChecked = false;
            this.stkpAddElement.Visibility = Visibility.Collapsed;
            this.GetTestItemList();
            this.BuildMainItemTree();
        }
        private void ClearEntryOptions()
        {
            this.lblSelectedTest.Content = "";
            this.lblSelectedTest.Tag = "";
            this.lblRptTitle.Content = "";
            this.lblRptTitle.Tag = "";
            this.lblSpecimen.Content = "";
            this.lblSpecimen.Tag = "";
            this.lblMachine.Content = "";
            this.lblMachine.Tag = "";
            this.chkElement.IsChecked = false;
            this.chkElement_Click(null, null);
        }
        private void GetTestItemList()
        {
            this.ListSirInfCodeBook.Clear();
            this.TestElementList.Clear();

            var pap1 = this.vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "4502[01]%", "345");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.ListSirInfCodeBook = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();

            var pap2 = this.vmGenList1.SetParamGeneralInfoCodeBook("SILBRPT");
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
            if (ds2 == null)
                return;

            this.TestElementList = ds2.Tables[0].DataTableToList<HmsEntityGeneral.AcInfCodeBook>().FindAll(x => x.actcode.Substring(9, 3) != "000");
            foreach (var item in this.TestElementList)
                item.actdesc = item.actdesc.Trim() + (item.actcode.Substring(0, 9) == "SILBRPT03" ? " " + item.acttdesc.Trim() : "");

            this.TestElementList = this.TestElementList.OrderBy(x => x.actdesc).ToList();
            this.cmbElementType_SelectionChanged(null, null);
        }
        private void BuildMainItemTree()
        {
            this.treeItemGroup.Items.Clear();
            var tvList1 = this.ListSirInfCodeBook.FindAll(x => x.sircode.Substring(7, 5) == "00000" && x.sircode.Substring(4, 8) != "00000000").ToList();
            foreach (var mitem in tvList1)
            {
                TreeViewItem tr1 = new TreeViewItem() { Header = mitem.sircode.Substring(4, 3) + " - " + mitem.sirdesc.Trim(), Tag = mitem.sircode, ToolTip = mitem.sirdesc1.Trim().ToUpper() };
                var tvList2 = this.ListSirInfCodeBook.FindAll(x => x.sircode.Substring(0, 7) == mitem.sircode.Substring(0, 7) && x.sircode.Substring(7, 5) != "00000" && x.sircode.Substring(9, 3) == "000").ToList();
                foreach (var sitem in tvList2)
                {
                    TreeViewItem tr2 = new TreeViewItem() { Header = sitem.sircode.Substring(7, 2) + " - " + sitem.sirdesc.Trim(), Tag = sitem.sircode, ToolTip = sitem.sirdesc1.Trim().ToUpper() };
                    var tvList3 = this.ListSirInfCodeBook.FindAll(x => x.sircode.Substring(0, 9) == sitem.sircode.Substring(0, 9) && x.sircode.Substring(9, 3) != "000").ToList();
                    foreach (var ditem in tvList3)
                    {
                        TreeViewItem tr3 = new TreeViewItem() { Header = ditem.sircode.Substring(9, 3) + " - " + ditem.sirdesc.Trim(), Tag = ditem.sircode, ToolTip = ditem.sirdesc1.Trim().ToUpper() };
                        tr3.KeyDown += tr3_KeyDown;
                        tr3.MouseDoubleClick += tr3_MouseDoubleClick;
                        tr2.Items.Add(tr3);
                        MenuItem mnui1 = new MenuItem() { Header = ditem.sirdesc1.Trim(), Tag = ditem.sircode };
                        mnui1.Click += autoTestItemSearch_ContextMenu_MouseClick;
                        this.autoTestItemSearch.ContextMenu.Items.Add(mnui1);
                    }

                    tr1.Items.Add(tr2);
                }
                this.treeItemGroup.Items.Add(tr1);
            }
        }

        void tr3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.SetValueTo_autoTestItemSearch(((TreeViewItem)sender).Tag.ToString().Trim());
        }

        void tr3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SetValueTo_autoTestItemSearch(((TreeViewItem)sender).Tag.ToString().Trim());
            this.btnSelectTestItem_Click(null, null);
        }

        private void autoTestItemSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            this.SetValueTo_autoTestItemSearch(((MenuItem)sender).Tag.ToString().Trim());
        }

        private void SetValueTo_autoTestItemSearch(string value1)
        {
            this.autoTestItemSearch.ItemsSource = this.ListSirInfCodeBook.FindAll(x => x.sircode.Substring(9, 3) != "000");// this.ShortRetSaleItemList;
            this.autoTestItemSearch.SelectedValue = value1;
            this.autoTestItemSearch.ToolTip = this.autoTestItemSearch.SelectedValue.ToString().Trim() + " - " + this.autoTestItemSearch.SelectedText.Trim();
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void autoTestItemSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
            this.autoTestItemSearch.ToolTip = null;
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetItemSirdesc(string Pattern)
        {
            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                this.ListSirInfCodeBook.Where((x, match) => x.sircode.Substring(9, 3) != "000" && x.sirdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }
        private void autoTestItemSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoTestItemSearch.ContextMenu.IsOpen = true;
        }

        private void btnSelectTestItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoTestItemSearch.SelectedValue == null)
                return;

            this.ClearEntryOptions();
            this.lblSelectedTest.Content = this.autoTestItemSearch.SelectedText.Trim();
            this.lblSelectedTest.Tag = this.autoTestItemSearch.SelectedValue.ToString().Trim();
            this.lblSelectedTest.ToolTip = this.autoTestItemSearch.SelectedValue.ToString().Trim() + " - " + this.autoTestItemSearch.SelectedText.Trim().ToUpper();
            this.chkTestName.IsChecked = false;
            this.chkTestName_Click(null, null);
            this.GetTestRptTemplateInfo(this.lblSelectedTest.Tag.ToString().Trim());
        }

        private void GetTestRptTemplateInfo(string isircode = "XXXXXXXXXXXX")
        {
            foreach (ComboBoxItem item in this.cmbElementType.Items)
            {
                if (item.Tag.ToString() == "SILBRPT02")
                {
                    item.Content = (isircode.Substring(0, 7) == "4502111" ? "Specimen" : "Part Scanned");
                    this.lblSpecimenTitle.Content = (isircode.Substring(0, 7) == "4502111" ? "Specimen :" : "Part Scanned :");
                    break;
                }
            }

            var pap1 = vm1.SetParamToGetRptTemplate(WpfProcessAccess.CompInfList[0].comcpcod, isircode);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.dgvRpt.ItemsSource = null;
            this.ListRptTemplate.Clear();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                string elcode = dr1["elcode"].ToString().Trim();

                switch (elcode.Substring(0, 9))
                {
                    case "SILBRPT01":
                        this.lblRptTitle.Content = dr1["eldesc1"].ToString().Trim();
                        this.lblRptTitle.Tag = elcode;
                        break;
                    case "SILBRPT02":
                        this.lblSpecimen.Content = dr1["eldesc1"].ToString().Trim();
                        this.lblSpecimen.Tag = elcode;
                        break;
                    case "SILBRPT03":
                        this.lblMachine.Content = dr1["eldesc1"].ToString().Trim() + " " + dr1["eldesc2"].ToString().Trim();
                        this.lblMachine.Tag = elcode;
                        break;
                    case "SILBRPT06":
                    case "SILBRPT08":
                        bool isGrp = (elcode.Substring(0, 9) == "SILBRPT06");
                        this.ListRptTemplate.Add(new vmEntryLabMgt1.DiagRptTemplate(dr1["sircode"].ToString().Trim(), int.Parse(dr1["elgrpsl"].ToString().Trim()),
                                (isGrp ? 0 : int.Parse(dr1["elressl"].ToString().Trim())), elcode, dr1["eldesc1"].ToString().Trim(), dr1["elresval"].ToString().Trim(),
                                    dr1["elrefval"].ToString().Trim(), "Visible", (isGrp ? "Bold" : "Normal"), (isGrp ? false : true), dr1["elstyle"].ToString().Trim()));


                        //this.ListRptTemplate.Add(new vmEntryLabMgt1.DiagRptTemplate(dr1["sircode"].ToString().Trim(), int.Parse(dr1["elgrpsl"].ToString().Trim()),
                        //        (isGrp ? 0 : int.Parse(dr1["elressl"].ToString().Trim())), elcode, dr1["eldesc1"].ToString().Trim(), dr1["elresval"].ToString().Trim(),
                        //            dr1["elrefval"].ToString().Trim(), (isGrp ? "Collapsed" : "Visible"), (isGrp ? "Bold" : "Normal"), (isGrp ? false : true), dr1["elstyle"].ToString().Trim()));

                        break;
                }
            }
            if (this.ListRptTemplate.Count > 0)
                this.btndgvRptHeader_Click(null, null);
        }
        private void ItemGroup_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void autoTestItemSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.autoTestItemSearch.SelectedValue != null)
                this.autoTestItemSearch.ToolTip = this.autoTestItemSearch.SelectedValue.ToString().Trim() + " - " + this.autoTestItemSearch.SelectedText.Trim().ToUpper();
        }

        private void chkTestName_Click(object sender, RoutedEventArgs e)
        {
            this.gridItemList.Visibility = (this.chkTestName.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
            this.stkpEntry.Visibility = (this.chkTestName.IsChecked == false ? Visibility.Visible : Visibility.Collapsed);
        }

        private void cmbElementType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.autoElementSearch == null)
                return;

            this.autoElementSearch.SelectedValue = null;
            this.autoElementSearch.ContextMenu.Items.Clear();
            string tag1 = ((ComboBoxItem)this.cmbElementType.SelectedItem).Tag.ToString();
            var ElemList1 = this.TestElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9));
            foreach (var ditem in ElemList1)
            {
                MenuItem mnui1 = new MenuItem() { Header = ditem.actdesc.Trim(), Tag = ditem.actcode };
                mnui1.Click += autoElementSearch_ContextMenu_MouseClick;
                this.autoElementSearch.ContextMenu.Items.Add(mnui1);
            }
        }

        private void autoElementSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            string tag1 = ((ComboBoxItem)this.cmbElementType.SelectedItem).Tag.ToString();
            this.autoElementSearch.ItemsSource = this.TestElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9) && x.actcode.Substring(9, 3) != "000");
            this.autoElementSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim(); ;
            this.autoElementSearch.ToolTip = this.autoElementSearch.SelectedValue.ToString().Trim().Substring(7, 5) + " - " + this.autoElementSearch.SelectedText.Trim();
        }

        private void autoElementSearch_PatternChanged(object sender, AutoComplete.AutoCompleteArgs args)
        {
            string tag1 = ((ComboBoxItem)this.cmbElementType.SelectedItem).Tag.ToString();
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetElementActDesc(args.Pattern, tag1);
            this.autoElementSearch.ToolTip = null;

        }
        private ObservableCollection<HmsEntityGeneral.AcInfCodeBook> GetElementActDesc(string Pattern, string tag1)
        {
            return new ObservableCollection<HmsEntityGeneral.AcInfCodeBook>(
                this.TestElementList.Where((x, match) => x.actcode.Substring(9, 3) != "000" && x.actcode.Substring(0, 9) == tag1 && x.actdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }
        private void autoElementSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoElementSearch.ContextMenu.IsOpen = true;
        }
        private void btnSelectElement_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoElementSearch.SelectedValue == null)
                return;

            string isircod = this.lblSelectedTest.Tag.ToString().Trim();
            string elcode = this.autoElementSearch.SelectedValue.ToString();
            string eldesc = this.autoElementSearch.SelectedText.Trim();
            int grpcod = (int)this.udGroup.Value;
            int slncod = (int)this.udSlNum.Value;
            this.autoElementSearch.SelectedValue = null;
            this.autoElementSearch.ToolTip = null;
            eldesc = (elcode.Substring(9, 3) == "001" ? "" : eldesc);
            switch (elcode.Substring(0, 9))
            {
                case "SILBRPT01":
                    //eldesc = (eldesc.Contains("[") ? eldesc.Substring(0, eldesc.IndexOf("[")).Trim() : eldesc);
                    this.lblRptTitle.Content = (eldesc.Length == 0 ? "( - )" : eldesc);
                    this.lblRptTitle.Tag = elcode;
                    break;
                case "SILBRPT02":
                    this.lblSpecimen.Content = (eldesc.Length ==  0 ? "( - )" : eldesc);
                    this.lblSpecimen.Tag = elcode;
                    break;
                case "SILBRPT03":
                    this.lblMachine.Content = (eldesc.Length ==  0 ? "( - )" : eldesc);
                    this.lblMachine.Tag = elcode;
                    break;
                case "SILBRPT06":
                case "SILBRPT08":
                    this.dgvRpt.ItemsSource = null;
                    bool isGrp = (elcode.Substring(0, 9) == "SILBRPT06");
                    this.ListRptTemplate.Add(new vmEntryLabMgt1.DiagRptTemplate(isircod, grpcod, (isGrp ? 0 : slncod), elcode, eldesc, "", "",
                        "Visible", (isGrp ? "Bold" : "Normal"), (isGrp ? false : true), (isGrp ? "BU" : "N")));
                    //this.ListRptTemplate.Add(new vmEntryLabMgt1.DiagRptTemplate(isircod, grpcod, (isGrp ? 0 : slncod), elcode, eldesc, "", "",
                    //                        (isGrp ? "Collapsed" : "Visible"), (isGrp ? "Bold" : "Normal"), (isGrp ? false : true), (isGrp ? "BU" : "N")));
                    this.btndgvRptHeader_Click(null, null);

                    var selitm1 = this.ListRptTemplate.Find(x => x.elgrpsl == grpcod && x.elcode == elcode);
                    this.dgvRpt.ScrollIntoView(selitm1);
                    break;
            }
        }

        private void chkElement_Click(object sender, RoutedEventArgs e)
        {
            this.stkpAddElement.Visibility = (this.chkElement.IsChecked == true ? Visibility.Visible : Visibility.Collapsed);
            this.autoElementSearch.SelectedValue = null;
        }

        private void btnUpdateTemplate_Click(object sender, RoutedEventArgs e)
        {
            string isircod = this.lblSelectedTest.Tag.ToString().Trim();
            string titleid = this.lblRptTitle.Tag.ToString().Trim();
            string specmid = this.lblSpecimen.Tag.ToString().Trim();
            string dmachid = this.lblMachine.Tag.ToString().Trim();

            var ListRptTemplate2 = this.ListRptTemplate.ToList();
            ListRptTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(isircod, 0, 1, titleid, "", "", "", "", "N", true, "B"));
            ListRptTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(isircod, 0, 2, specmid, "", "", "", "", "N", true, "B"));
            ListRptTemplate2.Add(new vmEntryLabMgt1.DiagRptTemplate(isircod, 0, 3, dmachid, "", "", "", "", "N", true, "B"));
            ListRptTemplate2.Sort(delegate(vmEntryLabMgt1.DiagRptTemplate x, vmEntryLabMgt1.DiagRptTemplate y)
            {
                return (x.elgrpsl.ToString("00") + x.elressl.ToString("00")).CompareTo(y.elgrpsl.ToString("00") + y.elressl.ToString("00"));
            });

            var pap1 = vm1.SetParamForRptTemplateUpdate(WpfProcessAccess.CompInfList[0].comcpcod, ListRptTemplate2, isircod);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            if (ds1 == null)
            {
                MessageBox.Show(WpfProcessAccess.DatabaseErrorInfoList[0].errormessage, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            System.Windows.MessageBox.Show("Update SuccessFull!!", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information,
                          MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            //---- Write to XML File -----------------------
            //string RecovDataFile = "TestCacheData1.xml";
            //ObjXmlFileManager.WriteToXmlFile<List<vmEntryLabMgt1.DiagRptTemplate>>(RecovDataFile, ListRptTemplate2);

        }

        private void btndgvRptHeader_Click(object sender, RoutedEventArgs e)
        {
            this.dgvRpt.ItemsSource = null;
            this.ListRptTemplate.Sort(delegate(vmEntryLabMgt1.DiagRptTemplate x, vmEntryLabMgt1.DiagRptTemplate y)
            {
                return (x.elgrpsl.ToString("00") + x.elressl.ToString("00")).CompareTo(y.elgrpsl.ToString("00") + y.elressl.ToString("00"));
            });
            this.dgvRpt.ItemsSource = this.ListRptTemplate;

        }

        private void btnNav_Click(object sender, RoutedEventArgs e)
        {

            if (this.dgvRpt.Items.Count == 0)
                return;

            if (this.dgvRpt.SelectedIndex < 0)
                this.dgvRpt.SelectedIndex = 0;

            string ActtionName = ((Button)sender).Name.ToString().Trim();
            int index1 = this.dgvRpt.SelectedIndex;
            if (ActtionName == "btnDelete")
            {

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to delete item\n" + this.ListRptTemplate[index1].elgrpsl.ToString("00") + "." +
                    this.ListRptTemplate[index1].elressl.ToString("00") + ". " + this.ListRptTemplate[index1].eldesc.Trim(),
                                    WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;
                this.dgvRpt.ItemsSource = null;
                this.ListRptTemplate[index1].elstyle = "XXXX";
                this.ListRptTemplate = this.ListRptTemplate.FindAll(x => x.elstyle != "XXXX");
                this.dgvRpt.ItemsSource = this.ListRptTemplate;
                if (this.ListRptTemplate.Count > 0)
                {
                    this.dgvRpt.SelectedIndex = (this.ListRptTemplate.Count <= index1 ? this.ListRptTemplate.Count - 1 : index1);
                }
                return;
            }
            switch (ActtionName)
            {
                case "btnTop":
                    index1 = 0;
                    break;
                case "btnPrev":
                    index1 = this.dgvRpt.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnNext":
                    index1 = this.dgvRpt.SelectedIndex + 1;
                    if (index1 >= this.dgvRpt.Items.Count)
                        index1 = this.dgvRpt.Items.Count - 1;
                    break;
                case "btnBottom":
                    index1 = this.dgvRpt.Items.Count - 1;
                    break;
            }
            this.dgvRpt.SelectedIndex = index1;

            var item21 = (vmEntryLabMgt1.DiagRptTemplate)this.dgvRpt.Items[index1];
            this.dgvRpt.ScrollIntoView(item21);
        }

        private void btnPrintTemplate_Click(object sender, RoutedEventArgs e)
        {
            string isircode = this.lblSelectedTest.Tag.ToString();
            // string CompCode, string ProcessID = "RPTDIAGNOSIS01", string isircode = "XXXXXXXXXXXXXXXXXX", string InvNum = "XXXXXXXXXXXXXXXXXX", string OrderBy = "DEFAULT"

            var pap1 = vm1.SetParamToPrintDiagRpt(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: "RPTDIAGNOSIS01", isircode: isircode);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            var list1a = ds1.Tables[0].DataTableToList<HmsEntityDiagnostic.DiagnosticReport>();
            var list1b = ds1.Tables[1].DataTableToList<HmsEntityDiagnostic.DiagnosticReport>();
            var list1c = ds1.Tables[2].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01GenInf>();
            var list1d = "";
            if (ds1.Tables[3].Rows.Count > 0)
            {
                if (!(ds1.Tables[3].Rows[0]["ptphoto"] is DBNull))
                {
                    byte[] imge1 = (byte[])ds1.Tables[3].Rows[0]["ptphoto"];
                    list1d = Convert.ToBase64String(imge1);
                }
            }
            // Start of Rearrange For Culture Test
            #region Start of Rearrange For Culture Test

            if (list1a[0].sircode.Substring(0, 9) == "450211166")
            {
                var list1b1 = list1b.FindAll(x => x.elgrpsl == 2 && x.elressl > 0).OrderBy(y => y.elressl).ToList();
                int cnt1 = list1b1.Count;
                if (cnt1 > 0)
                {
                    for (int u = cnt1; u < 36; u++)
                    {
                        var cc1 = new HmsEntityDiagnostic.DiagnosticReport()
                        {
                            sircode = "000000000000",
                            elgrpsl = 0,
                            elressl = 0,
                            elcode = "",
                            eldesc1 = "",
                            eldesc2 = "",
                            elrefval = "",
                            elresval = "",
                            elstyle = "",
                            sirdesc = ""
                        };

                        list1b1.Add(cc1);
                    }
                
                    var list1bb = new List<HmsEntityDiagnostic.DiagnosticReport>();
                    for (int k = 0; k < 12; k++)
                    {
                        var newitem = new HmsEntityDiagnostic.DiagnosticReport();
                        newitem.sircode = list1b1[k].sircode;
                        newitem.elgrpsl = list1b1[k].elgrpsl;
                        newitem.elressl = list1b1[k].elressl;

                        newitem.sirdesc = (k + 1).ToString("00") + ". " + list1b1[k].eldesc1.Trim();
                        newitem.elcode = ": " + list1b1[k].elresval.Trim();
                        newitem.eldesc2 = "CULTURE";

                        newitem.eldesc1 = (list1b1[k + 12].eldesc1.Trim().Length > 0 ? (k + 13).ToString("00") + ". " + list1b1[k + 12].eldesc1.Trim() : "");
                        newitem.elresval = (list1b1[k + 12].eldesc1.Trim().Length > 0 ? ": " + list1b1[k + 12].elresval.Trim() : "");

                        newitem.elrefval = (list1b1[k + 24].eldesc1.Trim().Length > 0 ? (k + 25).ToString("00") + ". " + list1b1[k + 24].eldesc1.Trim() : "");
                        newitem.elstyle = (list1b1[k + 24].eldesc1.Trim().Length > 0 ? ": " + list1b1[k + 24].elresval.Trim() : "");

                        list1bb.Add(newitem);

                    }

                    ////string[] desc2a = { "", "", "" };
                    ////string[] desc2b = { "", "", "" };
                    ////int sl = 0;
                    ////int cnt2 = 1;
                    ////foreach (var item in list1b1)
                    ////{
                    ////    desc2a[sl] = item.eldesc1.Trim();
                    ////    desc2b[sl] = ": " + item.elresval.Trim();
                    ////    if (sl == 2 || list1b1.Count == cnt2)
                    ////    {
                    ////        var newitem = new HmsEntityDiagnostic.DiagnosticReport();
                    ////        newitem.sircode = item.sircode;
                    ////        newitem.elgrpsl = item.elgrpsl;
                    ////        newitem.elressl = item.elressl;

                    ////        newitem.sirdesc = desc2a[0];
                    ////        newitem.elcode = desc2b[0];

                    ////        newitem.eldesc2 = "CULTURE";
                    ////        newitem.eldesc1 = desc2a[1];
                    ////        newitem.elresval = desc2b[1];

                    ////        newitem.elrefval = desc2a[2];
                    ////        newitem.elstyle = desc2b[2];

                    ////        desc2a[0] = desc2a[1] = desc2a[2] = "";
                    ////        desc2b[0] = desc2b[1] = desc2b[2] = "";
                    ////        list1bb.Add(newitem);
                    ////        sl = -1;
                    ////    }
                    ////    sl++;
                    ////    cnt2++;
                    ////}

                    ////cnt2 = list1bb.Count;
                    ////sl = 1;
                    ////foreach (var item in list1bb)
                    ////{
                    ////    item.sirdesc = (item.sirdesc.Trim().Length > 0 ?  sl.ToString("00") + ". " : "") + item.sirdesc.Trim();
                    ////    item.eldesc1 = (item.eldesc1.Trim().Length > 0 ?  (sl + cnt2).ToString("00") + ". " : "") + item.eldesc1.Trim();
                    ////    item.elrefval = (item.elrefval.Trim().Length > 0 ? (sl + cnt2 + cnt2).ToString("00") + ". " : "") + item.elrefval.Trim();
                    ////    sl++;
                    ////}

                    list1b = list1b.FindAll(x => x.elgrpsl != 2 || (x.elgrpsl == 2 && x.elressl == 0));
                    list1b = list1b.Concat(list1bb).ToList().OrderBy(y => y.elgrpsl.ToString("00") + y.elressl.ToString("00")).ToList();
                }
            }

            #endregion End of Rearrange For Culture Test

            var list1 = new List<Object>();
            // eldesc = (eldesc.Contains("[") ? eldesc.Substring(0, eldesc.IndexOf("[")).Trim() : eldesc);


            list1.Add(list1a);
            list1.Add(list1b);
            list1.Add(list1c);
            list1.Add(list1d);

            //string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["preparebynam"].ToString().Trim()
            //                 + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            //var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);

            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]));

            list3[0].RptParVal1 = "";
            list3[0].RptParVal2 = "";
            list3[0].RptParVal3 = "";
            list3[0].RptParVal4 = "";
            list3[0].RptParVal5 = "";
            list3[0].RptParVal6 = "";


            LocalReport rpt1 = DiagReportSetup.GetLocalReport("Lab.RptLabDiag01", list1, null, list3);
            string WindowTitle1 = "Diagnosis Report Template";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }



    }
}
