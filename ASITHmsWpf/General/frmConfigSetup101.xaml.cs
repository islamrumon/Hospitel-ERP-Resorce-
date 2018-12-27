using ASITFunLib;
using ASITHmsEntity;
using ASITHmsRpt1GenAcc.General;
using ASITHmsViewMan.General;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
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

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmConfigSetup101.xaml
    /// </summary>
    public partial class frmConfigSetup101 : UserControl
    {
        private vmConfigSetup1 vm1 = new vmConfigSetup1();
        private vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();
        private List<HmsEntityGeneral.UserInterfaceAuth.AppUserList> UserList1 = new List<HmsEntityGeneral.UserInterfaceAuth.AppUserList>();
        private List<HmsEntityGeneral.UserInterfaceAuth.uiObjInfo> uiObjList = new List<HmsEntityGeneral.UserInterfaceAuth.uiObjInfo>();
        private List<HmsEntityGeneral.UserInterfaceAuth.uiObjInfo> uiSubObjList = new List<HmsEntityGeneral.UserInterfaceAuth.uiObjInfo>();    // Part Object List    
        private List<HmsEntityGeneral.UserInterfaceAuth.uiObjInfo> uiModuleList = new List<HmsEntityGeneral.UserInterfaceAuth.uiObjInfo>();    // Module List


        private bool FrmInitialized = false;

        public frmConfigSetup101()
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
                this.ShowHideConfigurationOptions();
              
                this.Generate_uiSubObjList();
                if (WpfProcessAccess.StaffList == null)
                    WpfProcessAccess.GetCompanyStaffList();

                this.autoUserSearch.ContextMenu.Items.Clear();
                foreach (var item in WpfProcessAccess.StaffList)
                {
                    if (!(item.sircode == "950600801001" || item.sircode == "950600801002"))
                    {
                        MenuItem mnu1 = new MenuItem { Header = item.sirdesc1.Substring(6).Trim(), Tag = item.sircode };
                        mnu1.Click += this.autoUserSearch_ContextMenu_MouseClick;
                        this.autoUserSearch.ContextMenu.Items.Add(mnu1);
                    }
                }
                this.btnFind_Click(null, null);
            }
        }

        private void autoUserSearch_ContextMenu_MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.autoUserSearch.ItemsSource = WpfProcessAccess.StaffList;
                this.autoUserSearch.SelectedValue = ((MenuItem)sender).Tag.ToString().Trim();
             
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("UserCfg-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void ShowHideConfigurationOptions()
        {
            var AuthObj = new HmsEntityGeneral.UserInterfaceAuth();
            ////this.uiObjList = AuthObj.uiObjInfoList;     // When all privileges options to be shown

            // ------------- End of Privileges restricted based on application menu and super user privileges
            // Filter-01: User privileges based on Application Menue
            var MenuUidList1 = new List<string>();
            foreach (var mnu1 in WpfProcessAccess.AppMenuItemList)
                foreach (MenuItem mnu2 in mnu1.Items)
                    MenuUidList1.Add(mnu2.Uid.Split(',')[0]);

            foreach (var item in AuthObj.uiObjInfoList)
            {
                // Filter-02: Set user privileges as subset of super user privileges. So that any super user can't set privileges more then himself/herself
                var LoggedUserAuthList1 = WpfProcessAccess.SignedInUserAuthList.FindAll(x => x.uicode == item.uicode);
                string[] cod1 = item.uicode.Substring(4).Split('_');
                string cod2 = cod1[0].Substring(cod1[0].IndexOf('.') + 1);
                bool IsMenueExist1 = (MenuUidList1.FindAll(x => x.Contains(cod2)).Count > 0);
                if (LoggedUserAuthList1.Count > 0 && IsMenueExist1)
                {
                    this.uiObjList.Add(new HmsEntityGeneral.UserInterfaceAuth.uiObjInfo(_slnum: item.slnum, _moduleid: item.moduleid, _uicode: item.uicode, _uitype: item.uitype,
                        _uidesc: item.uidesc, _objallow: item.objallow, _fontbold: item.fontbold));
                }
            }
            // ------------- End of Privileges restricted based on application menu and super user privileges

            // Remove super user authority to provide user configuration permission to others
            string Userid = WpfProcessAccess.SignedInUserList[0].hccode;
            if (!(Userid == "950600801001" || Userid == "950600801002"))// || Userid == "950600801003"))
            {
                this.uiObjList = this.uiObjList.FindAll(x => !x.uicode.Contains("WPF_frmConfigSetup1")).ToList();
            }

            this.uiModuleList.Clear();
            string module1 = "XXXXXXXXXX";
            int slnum1 = 0;
            foreach (var item in this.uiObjList)
            {
                if (module1 != item.moduleid)
                {
                    module1 = item.moduleid;
                    var frmMod1 = WpfProcessAccess.AppFormsList.FindAll(x => x.Contains(module1));
                    if (frmMod1.Count > 0)
                    {
                        slnum1++;
                        this.uiModuleList.Add(new HmsEntityGeneral.UserInterfaceAuth.uiObjInfo(_slnum: slnum1.ToString("#") + ".", _moduleid: module1, _objallow: true));
                    }
                }
            }
            this.dgModule.ItemsSource = this.uiModuleList;

        }
     
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            this.imgPhoto.Source = this.imgPhoto2.Source;
            var pap1 = vmGenList1.SetParamAppUserList(WpfProcessAccess.CompInfList[0].comcpcod, "%");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            this.UserList1 = ds1.Tables[0].DataTableToList<HmsEntityGeneral.UserInterfaceAuth.AppUserList>().FindAll(x => !(x.hccode == "950600801001" || x.hccode == "950600801002"));
            this.ListBoxUserList.ItemsSource = this.UserList1;

            this.txtPrivCopyFromID.Text = "";
            this.txtSignInId.Text = "";
            this.txtUserPass1.Password = "";
            this.txtUserPass1h.Password = "";
            this.txtSignInRmrk.Text = "";
            this.lblHrId.Content = "";
            this.txtHrNamDsg.Text = "";
            if (this.autoUserSearch.SelectedValue == null)
                return;

            string hccode1 = this.autoUserSearch.SelectedValue.ToString().Trim();
            string hrInf1 = this.autoUserSearch.SelectedText.Trim();
            this.autoUserSearch.SelectedValue = null;
            if (WpfProcessAccess.StaffList.FindAll(x => x.sircode == hccode1).Count == 0)
                return;

            this.ListBoxUserList.SelectedIndex = -1;
            if (this.UserList1.FindAll(x => x.hccode == hccode1).Count > 0)
            {
                this.ShowExistingUserInfo(hccode1);
                this.txtSignInId.Focus();

                int j = 0;
                foreach (var item in this.UserList1)
                {
                    if (item.hccode == hccode1)
                    {
                        this.ListBoxUserList.SelectedIndex = j;
                        this.ListBoxUserList.ScrollIntoView(this.ListBoxUserList.SelectedItem);
                        break;
                    }
                    j++;
                }
                return;
            }

            this.txtSignInId.IsReadOnly = false;
            this.lblHrId.Content = hccode1;
            this.txtHrNamDsg.Text = hrInf1;
            this.btnUpdateUser.Focus();
        }

        private void ShowExistingUserInfo(string hccode1)
        {
            this.imgPhoto.Source = this.imgPhoto2.Source;
            this.dgPermission.ItemsSource = null;
            this.uiSubObjList.Clear();

            foreach (var mitem in this.uiModuleList)
                mitem.objallow = false;
            this.dgModule.Items.Refresh();



            var UserList1a = this.UserList1.FindAll(x => x.hccode == hccode1);
            if (UserList1a.Count == 0)
                return;

            this.lblHrId.Content = UserList1a[0].hccode; ;
            this.txtSignInId.Text = UserList1a[0].signinnam;
            this.txtHrNamDsg.Text = UserList1a[0].namedsg;
            this.txtSignInId.IsReadOnly = true;
            this.txtUserPass1.Password = "";
            this.txtUserPass1h.Password = UserList1a[0].hcpass;
            this.txtSignInRmrk.Text = UserList1a[0].userrmrk.Trim();
            this.autoUserSearch.SelectedValue = null;
            string TerminalID = Environment.MachineName.ToString().Trim().ToUpper();
            string newPass1 = "";
            string newPass2 = "";
            //this.imgPhoto.Source = this.imgPhoto2.Source;
            foreach (HmsEntityGeneral.UserInterfaceAuth.uiObjInfo item in this.uiObjList)
                item.objallow = false;

            this.dgPermission.Items.Refresh();

            var pap1 = vmGenList1.SetParamSignIn(WpfProcessAccess.CompInfList[0].comcpcod, UserList1a[0].signinnam, UserList1a[0].hcpass, TerminalID, newPass1, newPass2);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;
            if (!(ds1.Tables[0].Rows[0]["hcphoto"] is DBNull))
            {
                byte[] bytes = (byte[])ds1.Tables[0].Rows[0]["hcphoto"];
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.StreamSource = mem;
                bmp3.EndInit();
                this.imgPhoto.Source = bmp3;
            }

            string _comcod1 = ds1.Tables[0].Rows[0]["comcod"].ToString().Trim();
            string _hccode1 = ds1.Tables[0].Rows[0]["hccode"].ToString().Trim();

            /////////////////////////   
            if (ds1.Tables[1].Rows[0]["perdesc"] is DBNull)
                return;

            //string xmlbytostring = HmsHelper.ConvertBinaryToString((byte[])(ds1.Tables[1].Rows[0]["perdesc"]));
            string xmlbytostring = System.Text.ASCIIEncoding.Default.GetString((byte[])(ds1.Tables[1].Rows[0]["perdesc"]));
            char[] xmlDSArray = xmlbytostring.ToCharArray().Reverse().ToArray();
            string xmlDS = new string(xmlDSArray);
            DataSet ds1a = new DataSet();
            System.IO.StringReader xmlSR = new System.IO.StringReader(xmlDS);
            //ds1a.ReadXml(xmlSR, XmlReadMode.IgnoreSchema);
            ds1a.ReadXml(xmlSR);

            string _comcod2 = ds1a.Tables[1].Rows[0]["comcod"].ToString().Trim();
            string _hccode2 = ds1a.Tables[1].Rows[0]["hccode"].ToString().Trim();

            if (!(_comcod1 == _comcod2 && _hccode1 == _hccode2))
                return;

            DataView dv1 = ds1a.Tables[0].DefaultView;
            dv1.RowFilter = ("objallow=True");
            DataTable tbl1 = dv1.ToTable();
            if (tbl1.Rows.Count == 0)
                return;

            foreach (HmsEntityGeneral.UserInterfaceAuth.uiObjInfo item in this.uiObjList)
            {
                DataRow[] dr1 = tbl1.Select("moduleid='" + item.moduleid.Trim() + "' and uicode='" + item.uicode.Trim() + "'");
                if (dr1.Length > 0)
                    item.objallow = true;
            }

            foreach (var mitem in this.uiModuleList)
            {
                var lst1 = this.uiObjList.FindAll(x => x.moduleid == mitem.moduleid && x.objallow == true);
                mitem.objallow = (lst1.Count > 0 ? true : false);
            }
            this.dgModule.Items.Refresh();

            this.Generate_uiSubObjList();

            //this.dgPermission.Items.Refresh();
            //this.dgPermission.SelectedIndex = 0;
            //this.dgPermission.ScrollIntoView(this.dgPermission.Items[0]);
            //this.dgPermission.Items.Refresh();
        }

        private void btnCopyPrivilege_Click(object sender, RoutedEventArgs e)
        {
            string hccode1 = this.txtPrivCopyFromID.Text.Trim();
            if (hccode1.Length == 0)
                return;

            this.txtPrivCopyFromID.Text = "";

            var UserList1a = UserList1.FindAll(x => x.hccode == hccode1);
            if (UserList1a.Count == 0)
                return;

            string TerminalID = Environment.MachineName.ToString().Trim().ToUpper();
            string newPass1 = "";
            string newPass2 = "";

            var pap1 = vmGenList1.SetParamSignIn(WpfProcessAccess.CompInfList[0].comcpcod, UserList1a[0].signinnam, UserList1a[0].hcpass, TerminalID, newPass1, newPass2);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;

            if (ds1.Tables[0].Rows.Count == 0)
                return;

            if (ds1.Tables[1].Rows[0]["perdesc"] is DBNull)
                return;


            string xmlbytostring = System.Text.ASCIIEncoding.Default.GetString((byte[])(ds1.Tables[1].Rows[0]["perdesc"]));
            char[] xmlDSArray = xmlbytostring.ToCharArray().Reverse().ToArray();
            string xmlDS = new string(xmlDSArray);
            DataSet ds1a = new DataSet();
            System.IO.StringReader xmlSR = new System.IO.StringReader(xmlDS);
            ds1a.ReadXml(xmlSR);

            DataView dv1 = ds1a.Tables[0].DefaultView;
            dv1.RowFilter = ("objallow=True");
            DataTable tbl1 = dv1.ToTable();
            if (tbl1.Rows.Count == 0)
                return;

            foreach (HmsEntityGeneral.UserInterfaceAuth.uiObjInfo item in this.uiObjList)
                item.objallow = false;
            this.dgPermission.Items.Refresh();

            foreach (HmsEntityGeneral.UserInterfaceAuth.uiObjInfo item in this.uiObjList)
            {
                DataRow[] dr1 = tbl1.Select("moduleid='" + item.moduleid.Trim() + "' and uicode='" + item.uicode.Trim() + "'");
                if (dr1.Length > 0)
                    item.objallow = true;
            }

            foreach (var mitem in this.uiModuleList)
            {
                var lst1 = this.uiObjList.FindAll(x => x.moduleid == mitem.moduleid && x.objallow == true);
                mitem.objallow = (lst1.Count > 0 ? true : false);
            }
            this.dgModule.Items.Refresh();

            this.Generate_uiSubObjList();

        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            this.Update_uiObjList();

            string hccode1 = this.lblHrId.Content.ToString().Trim();

            string signinnam1 = this.txtSignInId.Text.Trim().ToUpper();
            string hcpass1 = this.txtUserPass1.Password.Trim();
            string hcpass1h = this.txtUserPass1h.Password.Trim();
            string AddEdit1 = (UserList1.FindAll(x => x.hccode == hccode1).Count() > 0 ? "EDITEXIST" : "ADDNEW");

            if (hccode1.Length < 12 || signinnam1.Length < 4)
            {
                MessageBox.Show("Does not meet the requirements of SignIn Name or Password", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            if ((AddEdit1.Contains("ADDNEW") || (AddEdit1.Contains("EDITEXIST") && hcpass1.Length > 0)) && (hcpass1.Length < 4 || hcpass1.Length > 10))
            {
                MessageBox.Show("Password length must be 4 to 10 characters", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            string hcpass2 = (hcpass1.Length == 0 ? hcpass1h : ASITUtility.EncodePassword(signinnam1 + hcpass1));
            string userrmrk1 = this.txtSignInRmrk.Text.Trim().ToUpper();

            this.dgPermission.Items.Refresh();
            DataTable tbl1v = ASITUtility2.ListToDataTable(this.uiObjList);
            DataView dv1 = tbl1v.DefaultView;
            dv1.RowFilter = ("objallow=True");
            DataTable tbl1 = dv1.ToTable();
            byte[] UserAuth1 = null;
            if (tbl1.Rows.Count > 0)
            {
                tbl1.TableName = "tblAuth";
                tbl1.Columns.Remove("slnum");
                tbl1.Columns.Remove("uidesc");
                tbl1.Columns.Remove("uitype");

                DataTable tbl2 = new DataTable("tblAuthID");
                tbl2.Columns.Add("comcod", typeof(System.String));
                tbl2.Columns.Add("hccode", typeof(System.String));
                tbl2.Rows.Add(new Object[] { WpfProcessAccess.CompInfList[0].comcod, hccode1 });

                DataSet ds = new DataSet("DsAuth");
                ds.Tables.Add(tbl1);
                ds.Tables.Add(tbl2);

                char[] xmlDSArray = ds.GetXml().ToCharArray().Reverse().ToArray();

                //char[] xmlDSArray2 = xmlDSArray.Reverse().ToArray();
                //string xmlDS2 = new string(xmlDSArray2);

                string xmlDS = new string(xmlDSArray);

                UserAuth1 = System.Text.ASCIIEncoding.Default.GetBytes(xmlDS);
            }
            var pap1 = vm1.SetParamUpdateUserInf(WpfProcessAccess.CompInfList[0].comcpcod, AddEdit1, hccode1, signinnam1, hcpass2, userrmrk1, UserAuth1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
            {
                MessageBox.Show("Could not update recommended change", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            if (ds1.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Could not update recommended change", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            if (AddEdit1.Contains("ADDNEW")) // Code to write to add updated data into List UserList1
                UserList1.Add(new HmsEntityGeneral.UserInterfaceAuth.AppUserList()
                {
                    hccode = hccode1,
                    hcpass = hcpass2,
                    namedsg = this.txtHrNamDsg.Text.Trim(),
                    signinnam = signinnam1,
                    slnum = (UserList1.Count() + 1).ToString() + ".",
                    userrmrk = userrmrk1
                });

            //this.dgvUserList.Items.Refresh();
            this.ListBoxUserList.Items.Refresh();
            //
            MessageBox.Show("Successfully Updated Information", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void ListBoxUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (this.ListBoxUserList.Items.Count == 0)
                return;

            int i = this.ListBoxUserList.SelectedIndex;
            if (i < 0)
                return;
            string hccode1 = UserList1[i].hccode;
            this.ShowExistingUserInfo(hccode1);
        }

        private void ListBoxUserList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Space)
                this.ListBoxUserList_MouseDoubleClick(null, null);
        }

        private void lblHeaderAllow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string tag1 = this.lblHeaderAllow.Tag.ToString();
            foreach (HmsEntityGeneral.UserInterfaceAuth.uiObjInfo item in this.uiSubObjList)// this.uiObjList)
                item.objallow = (tag1 == "None" ? true : false);

            if (this.dgPermission.Items.Count > 0)
            {
                this.dgPermission.SelectedIndex = 0;
                this.dgPermission.ScrollIntoView(this.dgPermission.Items[0]);
            }

            this.dgPermission.Items.Refresh();
            this.lblHeaderAllow.Tag = (tag1 == "None" ? "Allow" : "None");
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void lblHeaderAllModule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string tag1 = this.lblHeaderAllModule.Tag.ToString();
            foreach (HmsEntityGeneral.UserInterfaceAuth.uiObjInfo item in this.uiModuleList)
                item.objallow = (tag1 == "None" ? true : false);

            this.dgModule.SelectedIndex = 0;
            this.dgModule.ScrollIntoView(this.dgModule.Items[0]);

            this.dgModule.Items.Refresh();
            this.lblHeaderAllModule.Tag = (tag1 == "None" ? "Allow" : "None");
            this.Generate_uiSubObjList();
        }

        private void chkModule_Click(object sender, RoutedEventArgs e)
        {
            this.Generate_uiSubObjList();
        }

        private void Generate_uiSubObjList()
        {
            this.dgPermission.ItemsSource = null;
            this.uiSubObjList.Clear();
            var uiList0 = this.uiModuleList.FindAll(y => y.objallow == true);
            foreach (var item in uiList0)
            {
                var uiList1 = this.uiObjList.FindAll(x => x.moduleid == item.moduleid);
                foreach (var item1 in uiList1)
                {
                    this.uiSubObjList.Add(new HmsEntityGeneral.UserInterfaceAuth.uiObjInfo(_moduleid: item1.moduleid, _uicode: item1.uicode, _uitype: item1.uitype,
                        _objallow: item1.objallow, _uidesc: item1.uidesc, _slnum: item1.slnum, _fontbold: item1.fontbold));
                }
            }
            this.dgPermission.ItemsSource = this.uiSubObjList;
            this.dgPermission.SelectedIndex = 0;
        }

        private void Update_uiObjList()
        {
            foreach (var item in this.uiSubObjList)
            {
                var uiList1a = this.uiObjList.FindAll(x => x.moduleid == item.moduleid && x.uicode == item.uicode);
                uiList1a[0].objallow = item.objallow;
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            LocalReport rpt1 = null;
            string PrintId = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            var list3 = WpfProcessAccess.GetRptGenInfo(ServerTime: DateTime.Now);
            if (this.cmbReport.SelectedIndex == 0)
            {
                list3[0].RptParVal1 = "Application Sign-In User List (Alphabetic Order)";
                list3[0].RptParVal2 = "(Not on seniority basis)";
                var list1a = this.UserList1.FindAll(x => x.hccode.Substring(0, 7) != "9506008").OrderBy(y => y.signinnam).ToList();
                rpt1 = GeneralReportSetup.GetLocalReport("General.rptAppUserList01", list1a, null, list3);
            }
            else
            {
                this.ListBoxUserList_MouseDoubleClick(null, null);
                if (this.ListBoxUserList.SelectedItem == null)
                    return;
                var item1a = (HmsEntityGeneral.UserInterfaceAuth.AppUserList)this.ListBoxUserList.SelectedItem;

                list3[0].RptParVal1 = "Application Sign-In User Privileges Sheet";
                list3[0].RptParVal2 = item1a.signinnam.Trim() + " [ " + item1a.hccode + " - " + item1a.namedsg.Trim() + " ]";
                var list1b = this.uiSubObjList.FindAll(x => x.objallow == true).OrderBy(y => y.slnum.Trim().Replace(".", "")).ToList();
                rpt1 = GeneralReportSetup.GetLocalReport("General.rptAppUserAuth01", list1b, null, list3);
            }
            string WindowTitle1 = list3[0].RptParVal1;
            string pout1 = ((ComboBoxItem)(this.cmbOutputOption.SelectedItem)).Tag.ToString().Trim();
            string RptDisplayMode = (pout1 == "PDF" || pout1 == "WORD" || pout1 == "EXCEL" ? pout1 : (pout1 == "NP" ? "Normal" : "PrintLayout"));
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void autoUserSearch_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetUserListSirdesc(args.Pattern);
        }

        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetUserListSirdesc(string Pattern)
        {
            // match on contain (could do starts with)
            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
             WpfProcessAccess.StaffList.Where((x, match) => (x.sircode + x.sirdesc).ToLower().Trim().Contains(Pattern.ToLower().Trim()) 
                 && !(x.sircode == "950600801001" || x.sircode == "950600801002")).Take(100));
        }

        private void autoUserSearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.autoUserSearch.ContextMenu.IsOpen = true;
        }
    }
    public class AuthObjGridToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj1 = value as HmsEntityGeneral.UserInterfaceAuth.uiObjInfo;
            return string.Format("{0}: {1}", obj1.uitype, obj1.uidesc);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
