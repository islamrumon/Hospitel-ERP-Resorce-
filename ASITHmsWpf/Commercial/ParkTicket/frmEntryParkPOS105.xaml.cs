using ASITHmsEntity;
using ASITFunLib;
using ASITHmsViewMan.Commercial;
using ASITHmsViewMan.General;

using ASITHmsWpf.UserControls;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;

namespace ASITHmsWpf.Commercial.ParkTicket
{
    /// <summary>
    /// Interaction logic for frmEntryParkPOS105.xaml
    /// </summary>
    public partial class frmEntryParkPOS105 : UserControl
    {
        private List<HmsEntityGeneral.AcInfCodeBook> TicketElementList = new List<HmsEntityGeneral.AcInfCodeBook>();
        private List<HmsEntityGeneral.SirInfCodeBook> ListSirInfCodeBook = new List<HmsEntityGeneral.SirInfCodeBook>();
        private List<vmEntryReportPark1.ParkTicketTemplate> ListTicketTemplate = new List<vmEntryReportPark1.ParkTicketTemplate>();

        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private vmEntryReportPark1 vm1 = new vmEntryReportPark1();

        public frmEntryParkPOS105()
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

            this.stkpEntry.Visibility = Visibility.Collapsed;

            this.lblSelectedTest.Content = "";
            this.lblSelectedTest.Tag = "";
            this.chkElement.IsChecked = false;
            this.stkpAddElement.Visibility = Visibility.Hidden;
            this.GetTestItemList();
            this.BuildMainItemTree();
        }

        private void ClearEntryOptions()
        {
            this.lblSelectedTest.Content = "";
            this.lblSelectedTest.Tag = "";
            this.lblRptTitle.Content = "";
            this.lblRptTitle.Tag = "";
            this.lblSubTitle.Content = "";
            this.lblSubTitle.Tag = "";
            this.lblTicketNote.Content = "";
            this.lblTicketNote.Tag = "";
            this.chkElement.IsChecked = false;
            this.chkElement_Click(null, null);
        }

        private void GetTestItemList()
        {
            this.ListSirInfCodeBook.Clear();
            this.TicketElementList.Clear();

            //var pap1 = this.vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "4502[01]%", "345");
            var pap1 = this.vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "4144[012345]%", "345");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.ListSirInfCodeBook = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();

            //var pap2 = this.vmGenList1.SetParamGeneralInfoCodeBook("SILBRPT");
            var pap2 = this.vmGenList1.SetParamGeneralInfoCodeBook("SIPOTKT");
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
            if (ds2 == null)
                return;

            this.TicketElementList = ds2.Tables[0].DataTableToList<HmsEntityGeneral.AcInfCodeBook>().FindAll(x => x.actcode.Substring(9, 3) != "000");
            ////foreach (var item in this.TicketElementList)
            ////    item.actdesc = item.actdesc.Trim() + (item.actcode.Substring(0, 9) == "SILBRPT03" ? " " + item.acttdesc.Trim() : "");

            this.cmbElementType_SelectionChanged(null, null);
        }
        private void BuildMainItemTree()
        {
            this.treeItemGroup.Items.Clear();
            var tvList1 = this.ListSirInfCodeBook.FindAll(x => x.sircode.Substring(7, 5) == "00000" && x.sircode.Substring(4, 8) != "00000000").ToList();
            foreach (var mitem in tvList1)
            {
                TreeViewItem tr1 = new TreeViewItem() { Header = mitem.sircode.Substring(4, 3) + " - " + mitem.sirdesc.Trim(), Tag = mitem.sircode, ToolTip = mitem.sirdesc1.Trim().ToUpper(),
                    FontWeight= FontWeights.Bold, Foreground = Brushes.Blue };
                var tvList2 = this.ListSirInfCodeBook.FindAll(x => x.sircode.Substring(0, 7) == mitem.sircode.Substring(0, 7) && x.sircode.Substring(7, 5) != "00000" && x.sircode.Substring(9, 3) == "000").ToList();
                foreach (var sitem in tvList2)
                {
                    TreeViewItem tr2 = new TreeViewItem() { Header = sitem.sircode.Substring(7, 2) + " - " + sitem.sirdesc.Trim(), Tag = sitem.sircode, ToolTip = sitem.sirdesc1.Trim().ToUpper()};
                    var tvList3 = this.ListSirInfCodeBook.FindAll(x => x.sircode.Substring(0, 9) == sitem.sircode.Substring(0, 9) && x.sircode.Substring(9, 3) != "000").ToList();
                    foreach (var ditem in tvList3)
                    {
                        TreeViewItem tr3 = new TreeViewItem() { Header = ditem.sircode.Substring(9, 3) + " - " + ditem.sirdesc.Trim(), Tag = ditem.sircode, ToolTip = ditem.sirdesc1.Trim().ToUpper(), FontWeight = FontWeights.Normal };
                        tr3.KeyDown += tr3_KeyDown;
                        tr3.MouseDoubleClick += tr3_MouseDoubleClick;
                        tr2.Items.Add(tr3);
                        MenuItem mnui1 = new MenuItem() { Header = ditem.sirdesc1.Trim(), Tag = ditem.sircode };
                        mnui1.Click += autoTicketItemSearch_ContextMenu_MouseClick;
                        this.autoTicketItemSearch.ContextMenu.Items.Add(mnui1);
                    }
                    tr2.IsExpanded = true;
                    tr1.Items.Add(tr2);
                }
                tr1.IsExpanded = true;
                this.treeItemGroup.Items.Add(tr1);
            }
        }

        void tr3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
            {
                this.SetValueTo_autoTestItemSearch(((TreeViewItem)sender).Tag.ToString().Trim());
                this.btnSelectTicketItem_Click(null, null);
            }
        }

        void tr3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SetValueTo_autoTestItemSearch(((TreeViewItem)sender).Tag.ToString().Trim());
            this.btnSelectTicketItem_Click(null, null);
        }

        private void autoTicketItemSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            this.SetValueTo_autoTestItemSearch(((MenuItem)sender).Tag.ToString().Trim());
        }

        private void SetValueTo_autoTestItemSearch(string value1)
        {
            this.autoTicketItemSearch.ItemsSource = this.ListSirInfCodeBook.FindAll(x => x.sircode.Substring(9, 3) != "000");// this.ShortRetSaleItemList;
            this.autoTicketItemSearch.SelectedValue = value1;
            this.autoTicketItemSearch.ToolTip = this.autoTicketItemSearch.SelectedValue.ToString().Trim() + " - " + this.autoTicketItemSearch.SelectedText.Trim();
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        

        private void chkElement_Click(object sender, RoutedEventArgs e)
        {
            this.stkpAddElement.Visibility = (this.chkElement.IsChecked == true ? Visibility.Visible : Visibility.Hidden);
            this.autoElementSearch.SelectedValue = null;
        }

        private void btnUpdateTemplate_Click(object sender, RoutedEventArgs e)
        {
            string isircod = this.lblSelectedTest.Tag.ToString().Trim();
            string titleid = this.lblRptTitle.Tag.ToString().Trim();
            string specmid = this.lblSubTitle.Tag.ToString().Trim();
            string dmachid = this.lblTicketNote.Tag.ToString().Trim();

            var ListTicketTemplate2 = this.ListTicketTemplate.ToList();
            //ListTicketTemplate2.Add(new vmEntryParkPOS1.ParkTicketTemplate(isircod, 0, 1, titleid, "", "N"));
            //ListTicketTemplate2.Add(new vmEntryParkPOS1.ParkTicketTemplate(isircod, 0, 2, specmid, "", "N"));
            //ListTicketTemplate2.Add(new vmEntryParkPOS1.ParkTicketTemplate(isircod, 0, 3, dmachid, "", "N"));
            ListTicketTemplate2.Sort(delegate(vmEntryReportPark1.ParkTicketTemplate x, vmEntryReportPark1.ParkTicketTemplate y)
            {
                return (x.elgrpsl.ToString("00") + x.elressl.ToString("00")).CompareTo(y.elgrpsl.ToString("00") + y.elressl.ToString("00"));
            });

            var pap1 = vm1.SetParamForTicketTemplateUpdate(WpfProcessAccess.CompInfList[0].comcpcod, ListTicketTemplate2, isircod);
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

        private void btnPrintTemplate_Click(object sender, RoutedEventArgs e)
        {
            string isircode = this.lblSelectedTest.Tag.ToString();
            // string CompCode, string ProcessID = "RPTDIAGNOSIS01", string isircode = "XXXXXXXXXXXXXXXXXX", string InvNum = "XXXXXXXXXXXXXXXXXX", string OrderBy = "DEFAULT"

            ////var pap1 = vm1.SetParamToPrintDiagRpt(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, ProcessID: "RPTDIAGNOSIS01", isircode: isircode);
            ////DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            ////if (ds1 == null)
            ////    return;

            ////var list1a = ds1.Tables[0].DataTableToList<HmsEntityDiagnostic.DiagnosticReport>();
            ////var list1b = ds1.Tables[1].DataTableToList<HmsEntityDiagnostic.DiagnosticReport>();
            ////var list1c = ds1.Tables[2].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01GenInf>();

            ////var list1d = "";
            ////if (ds1.Tables[3].Rows.Count > 0)
            ////{
            ////    if (!(ds1.Tables[3].Rows[0]["ptphoto"] is DBNull))
            ////    {
            ////        byte[] imge1 = (byte[])ds1.Tables[3].Rows[0]["ptphoto"];
            ////        list1d = Convert.ToBase64String(imge1);
            ////    }
            ////}

            ////var list1 = new List<Object>();
            ////list1.Add(list1a);
            ////list1.Add(list1b);
            ////list1.Add(list1c);
            ////list1.Add(list1d);

            ////string inputSource = ds1.Tables[2].Rows[0]["preparetrm"].ToString().Trim() + ", " + ds1.Tables[2].Rows[0]["preparebynam"].ToString().Trim()
            ////                 + ", " + ds1.Tables[2].Rows[0]["prepareses"].ToString().Trim() + ", " + Convert.ToDateTime(ds1.Tables[2].Rows[0]["rowtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

            ////var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: Convert.ToDateTime(ds1.Tables[2].Rows[0]["ServerTime"]), InputSource: inputSource);

            ////LocalReport rpt1 = DiagReportSetup.GetLocalReport("Lab.RptLabDiag01", list1, null, list3);
            string WindowTitle1 = "Diagnosis Report Template";
            string RptDisplayMode = "PrintLayout";
            ////WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void btnNav_Click(object sender, RoutedEventArgs e)
        {

            if (this.dgvTkt.Items.Count == 0)
                return;

            if (this.dgvTkt.SelectedIndex < 0)
                this.dgvTkt.SelectedIndex = 0;

            string ActtionName = ((Button)sender).Name.ToString().Trim();
            int index1 = this.dgvTkt.SelectedIndex;
            if (ActtionName == "btnDelete")
            {

                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to delete item\n" + this.ListTicketTemplate[index1].elgrpsl.ToString("00") + "." +
                    this.ListTicketTemplate[index1].elressl.ToString("00") + ". " + this.ListTicketTemplate[index1].eldesc.Trim(),
                                    WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;
                this.dgvTkt.ItemsSource = null;
                this.ListTicketTemplate[index1].elstyle = "XXXX";
                this.ListTicketTemplate = this.ListTicketTemplate.FindAll(x => x.elstyle != "XXXX");
                this.dgvTkt.ItemsSource = this.ListTicketTemplate;
                if (this.ListTicketTemplate.Count > 0)
                {
                    this.dgvTkt.SelectedIndex = (this.ListTicketTemplate.Count <= index1 ? this.ListTicketTemplate.Count - 1 : index1);
                }
                return;
            }
            switch (ActtionName)
            {
                case "btnTop":
                    index1 = 0;
                    break;
                case "btnPrev":
                    index1 = this.dgvTkt.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnNext":
                    index1 = this.dgvTkt.SelectedIndex + 1;
                    if (index1 >= this.dgvTkt.Items.Count)
                        index1 = this.dgvTkt.Items.Count - 1;
                    break;
                case "btnBottom":
                    index1 = this.dgvTkt.Items.Count - 1;
                    break;
            }
            this.dgvTkt.SelectedIndex = index1;

            var item21 = (vmEntryReportPark1.ParkTicketTemplate)this.dgvTkt.Items[index1];
            this.dgvTkt.ScrollIntoView(item21);
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
                this.TicketElementList.Where((x, match) => x.actcode.Substring(9, 3) != "000" && x.actcode.Substring(0, 9) == tag1 && x.actdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void btndgvTktHeader_Click(object sender, RoutedEventArgs e)
        {
            this.dgvTkt.ItemsSource = null;
            this.ListTicketTemplate.Sort(delegate(vmEntryReportPark1.ParkTicketTemplate x, vmEntryReportPark1.ParkTicketTemplate y)
            {
                return (x.elgrpsl.ToString("00") + x.elressl.ToString("00")).CompareTo(y.elgrpsl.ToString("00") + y.elressl.ToString("00"));
            });
            this.dgvTkt.ItemsSource = this.ListTicketTemplate;
        }

        private void cmbElementType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.autoElementSearch == null)
                return;

            this.autoElementSearch.SelectedValue = null;
            this.autoElementSearch.ContextMenu.Items.Clear();
            string tag1 = ((ComboBoxItem)this.cmbElementType.SelectedItem).Tag.ToString();
            var ElemList1 = this.TicketElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9));
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
            this.autoElementSearch.ItemsSource = this.TicketElementList.FindAll(x => x.actcode.Substring(0, 9) == tag1.Substring(0, 9) && x.actcode.Substring(9, 3) != "000");
            this.autoElementSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim(); ;
            this.autoElementSearch.ToolTip = this.autoElementSearch.SelectedValue.ToString().Trim().Substring(7, 5) + " - " + this.autoElementSearch.SelectedText.Trim();
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
            int elressl1 = 0;
            switch (elcode.Substring(0, 9))
            {
                case "SIPOTKT01":
                    this.lblRptTitle.Content = eldesc;
                    this.lblRptTitle.Tag = elcode;
                    break;
                case "SIPOTKT02":
                    this.lblSubTitle.Content = eldesc;
                    this.lblSubTitle.Tag = elcode;
                    break;
                case "SIPOTKT03":
                    this.lblTicketNote.Content = eldesc;
                    this.lblTicketNote.Tag = elcode;
                    break;
                case "SIPOTKT06":
                case "SIPOTKT08":
                    this.dgvTkt.ItemsSource = null;
                    bool isGrp = (elcode.Substring(0, 9) == "SIPOTKT06");
                    elressl1 = (isGrp ? 0 : slncod);
                    this.ListTicketTemplate.Add(new vmEntryReportPark1.ParkTicketTemplate(isircod, grpcod, elressl1, elcode, eldesc, (isGrp ? "B" : "N"), (isGrp ? "Bold" : "Normal")));
                    this.btndgvTktHeader_Click(null, null);
                    break;
            }
            int index1 = 0;
            foreach (var item in this.ListTicketTemplate)
            {
                if (item.isircode == isircod && item.elgrpsl == grpcod && item.elressl == elressl1)
                    break;

                ++index1;
            }
            this.dgvTkt.SelectedIndex = index1;

            var item21 = (vmEntryReportPark1.ParkTicketTemplate)this.dgvTkt.Items[index1];
            this.dgvTkt.ScrollIntoView(item21);
        }

        private void btnSelectTicketItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.autoTicketItemSearch.SelectedValue == null)
                return;

            this.ClearEntryOptions();
            this.lblSelectedTest.Content = this.autoTicketItemSearch.SelectedText.Trim();
            this.lblSelectedTest.Tag = this.autoTicketItemSearch.SelectedValue.ToString().Trim();
            this.lblSelectedTest.ToolTip = this.autoTicketItemSearch.SelectedValue.ToString().Trim() + " - " + this.autoTicketItemSearch.SelectedText.Trim().ToUpper();
            this.GetTestTicketTemplateInfo(this.lblSelectedTest.Tag.ToString().Trim());
            this.stkpEntry.Visibility = Visibility.Visible;
        }
        private void GetTestTicketTemplateInfo(string isircode = "XXXXXXXXXXXX")
        {
            var pap1 = vm1.SetParamToGetTicketTemplate(WpfProcessAccess.CompInfList[0].comcpcod, isircode);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.dgvTkt.ItemsSource = null;
            this.ListTicketTemplate.Clear();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                string elcode = dr1["elcode"].ToString().Trim();

                switch (elcode.Substring(0, 9))
                {
                    case "SIPOTKT01":
                        this.lblRptTitle.Content = dr1["eldesc"].ToString().Trim();
                        this.lblRptTitle.Tag = elcode;
                        break;
                    case "SIPOTKT02":
                        this.lblSubTitle.Content = dr1["eldesc"].ToString().Trim();
                        this.lblSubTitle.Tag = elcode;
                        break;
                    case "SIPOTKT03":
                        this.lblTicketNote.Content = dr1["eldesc"].ToString().Trim();
                        this.lblTicketNote.Tag = elcode;
                        break;
                    case "SIPOTKT06":
                    case "SIPOTKT08":
                        bool isGrp = (elcode.Substring(0, 9) == "SIPOTKT06");
                        this.ListTicketTemplate.Add(new vmEntryReportPark1.ParkTicketTemplate(dr1["sircode"].ToString().Trim(), int.Parse(dr1["elgrpsl"].ToString().Trim()),
                                (isGrp ? 0 : int.Parse(dr1["elressl"].ToString().Trim())), elcode, dr1["eldesc"].ToString().Trim(), dr1["elstyle"].ToString().Trim(), (isGrp ? "Bold" : "Normal")));
                        break;
                }
            }
            if (this.ListTicketTemplate.Count > 0)
                this.btndgvTktHeader_Click(null, null);


        }

        private void ItemGroup_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void autoElementSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoElementSearch.ContextMenu.IsOpen = true;
        }

        private void autoTicketItemSearch_PatternChanged(object sender, AutoComplete.AutoCompleteArgs args)
        {
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetItemSirdesc(args.Pattern);
            this.autoTicketItemSearch.ToolTip = null;
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetItemSirdesc(string Pattern)
        {
            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                this.ListSirInfCodeBook.Where((x, match) => x.sircode.Substring(9, 3) != "000" && x.sirdesc.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }

        private void autoTicketItemSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoTicketItemSearch.ContextMenu.IsOpen = true;
        }

        private void autoTicketItemSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.autoTicketItemSearch.SelectedValue != null)
                this.autoTicketItemSearch.ToolTip = this.autoTicketItemSearch.SelectedValue.ToString().Trim() + " - " + this.autoTicketItemSearch.SelectedText.Trim().ToUpper();
        }

    }
}
