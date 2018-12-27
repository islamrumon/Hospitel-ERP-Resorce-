using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ASITDataLib;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan;
using ASITHmsViewMan.General;
//using ASITHmsViewMan.Commercial;
using System.Net.NetworkInformation;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Reporting.WinForms;
using ASITHmsRpt0Main;


namespace ASITHmsWpf
{
    public static class WpfProcessAccess
    {
        #region Object Initialization

        public static string AppTitle = "CentERPoint Automation Management System";

        // Following title will aotomatically change after change option in App.xaml.cs
        //public static string AppTitle = "CentERPoint Foodshop Management System"; 
        //public static string AppTitle = "CentERPoint Trading Management System";

        public static string HmsVersion = "181121.1"; //"181026.1"; //"181011.1"; //"180915.1"; //"180816.1"; //"180714.1"; //"180627.1"; //"180602.1"; Product Version Control 
        // dbo.SP_REPORT_CODEBOOK_01 @ProcID = 'VERSIONCHK01' -- 

        public static string VersionType = "1"; // Development & Testing Version (0 from Published Version)
        public static string AppComCode = "6500"; // Development & Testing Version (0 from Published Version)
        public static string AppLocalImagePath = ""; // Development & Testing Version (0 from Published Version)
        public static string AppRptViewStyle = "Normal";//"Dialog"; // Development & Testing Version (0 from Published Version)

        public static readonly int AppUserLogLevel = 9;//9; // 0 - No Log, 1, 2, 3, 4, 5, 6, 7 8, 9 - Full Log

        public static List<HmsEntityGeneral.DatabaseErrorInfo> DatabaseErrorInfoList;       // Back-End Database and Commpunication Realtes Error List

        public static List<HmsEntityGeneral.AppVersionInfo> AppVersionList;                 // Application Version Control Information List
        public static List<HmsEntityGeneral.CompInfCodeBook> CompInfList;                   // User Company, Branches and Department List 

        public static List<HmsEntityManpower.SignInInfo> SignedInUserList;                  // Application User List from User Information Code Book (Sub-Set of Resource Code Book)
        public static List<HmsEntityGeneral.UserInterfaceAuth.uiObjSignInAuth> SignedInUserAuthList;  // Application User List from User Information Code Book (Sub-Set of Resource Code Book)
        public static List<string> AppFormsList;
        public static List<MenuItem> AppMenuItemList;
        
        public static List<HmsEntityGeneral.SirInfCodeBook> StaffGroupList;                 // Company's Human Resources Group List from Resource Code Book
        public static List<HmsEntityGeneral.SirInfCodeBook> StaffList;                      // Company's Human Resources List from Resource Code Book
        public static List<HmsEntityGeneral.SirInfCodeBook> SupplierContractorList;         // Supplier and Contructors List from Resource Code Book
        public static List<HmsEntityGeneral.SirInfCodeBook> InvItemGroupList;               // Inventory Items List from Resource Code Book
        public static List<HmsEntityGeneral.SirInfCodeBook> InvItemList;                    // Inventory Items List from Resource Code Book

        public static List<HmsEntityGeneral.AcInfCodeBook> GenInfoTitleList;                // Others General Information Title List from Accounts Code Book
        public static List<HmsEntityGeneral.AcInfCodeBook> AccCodeList;                     // Chart of Accounts
        public static List<HmsEntityGeneral.SirInfCodeBook> AccSirCodeList;                 // Chart of Subsidiary Accounts
        private static vmHmsGeneralList1 vmGenList1 = new vmHmsGeneralList1();

        #endregion  //  Object Initialization

        #region WCF Web / Local Data Service Management

        public static ASITHmsService1.ASITHmsServiceClient HmsDataService = new ASITHmsService1.ASITHmsServiceClient();

        public static ProcessAccess HmsDataServicepa1 = null;   // For using WCF Service      
        //public static ProcessAccess HmsDataServicepa1 = new ProcessAccess("DBConnStr", "WPF"); // For Local Connection// ConnType="Web", ConnType="WPF", ConnType="Fixed"


        //------------ Note for connection string --------------------------------------
        //public static ProcessAccess HmsDataServicepa1 = new ProcessAccess(@"Data Source=LOCALHOST\SQL2K12EXP;initial Catalog=ASITHMSDB;User ID=asitdev;Password=asitdev1234", "Fixed"); 
        // ConnType="Web", ConnType="WPF", ConnType="Fixed"

        public static string GetTestServiceData()
        {
            return HmsDataService.GetData(0);
        }

        //public static DataSet GetHmsDataSet(ASITFunParams.ProcessAccessParams pap1, string ParamPassType = "JSON")
        public static DataSet GetHmsDataSet(ASITFunParams.ProcessAccessParams pap1, string ParamPassType = "CLASS")
        {
            try
            {
                //string Comcod1 = (WpfProcessAccess.CompInfList == null ? WpfProcessAccess.GetCompCode() : WpfProcessAccess.CompInfList[0].comcod);
                string Comcod1 = (WpfProcessAccess.CompInfList == null ? WpfProcessAccess.AppComCode : WpfProcessAccess.CompInfList[0].comcod);

                WpfProcessAccess.DatabaseErrorInfoList = null;
                DataSet ds1 = new DataSet();
                switch (ParamPassType.ToUpper())
                {
                    case "CLASS":
                        if (HmsDataServicepa1 == null)
                            ds1 = HmsDataService.GetDataSetResult(pap1: pap1, comcod1: Comcod1);
                        else
                            ds1 = HmsDataServicepa1.GetDataSetResult(pap1: pap1);
                        break;
                    case "PARAMETERS":
                        ds1 = HmsDataService.GetDataSetResultWeb(_comCod: pap1.comCod, _ProcName: pap1.ProcName, _ProcID: pap1.ProcID,
                       _parmXml01: pap1.parmXml01, _parmXml02: pap1.parmXml02, _parmBin01: pap1.parmBin01,
                       _parm01: pap1.parm01, _parm02: pap1.parm02, _parm03: pap1.parm03, _parm04: pap1.parm04, _parm05: pap1.parm05,
                       _parm06: pap1.parm06, _parm07: pap1.parm07, _parm08: pap1.parm08, _parm09: pap1.parm09, _parm10: pap1.parm10,
                       _parm11: pap1.parm11, _parm12: pap1.parm12, _parm13: pap1.parm13, _parm14: pap1.parm14, _parm15: pap1.parm15,
                       _parm16: pap1.parm16, _parm17: pap1.parm17, _parm18: pap1.parm18, _parm19: pap1.parm19, _parm20: pap1.parm20,
                       comcod1: Comcod1);
                        break;
                    case "XML":
                        string XmlPap1 = ASITUtility.XmlSerialize(pap1);
                        string XmlDs1;
                        if (HmsDataServicepa1 == null)
                            XmlDs1 = HmsDataService.GetXmlStrResult(XmlPap1: XmlPap1, comcod1: Comcod1);
                        else
                            XmlDs1 = HmsDataServicepa1.GetXmlStrResult(XmlPap1);

                        ds1 = ASITUtility.XmlDeserialize<DataSet>(XmlDs1);
                        break;
                    case "JSON": // Can't keep DataSet Name, So Xml DataSet Update to SQL Server not possible
                        // Recommended to Send/Receive DataSet without DataSet Name.
                        string JsonPap1 = JsonConvert.SerializeObject(pap1, Newtonsoft.Json.Formatting.Indented);
                        string parmXml01DsName = (pap1.parmXml01 == null ? "" : pap1.parmXml01.DataSetName);
                        string parmXml02DsName = (pap1.parmXml02 == null ? "" : pap1.parmXml02.DataSetName);
                        string JsonDs1;
                        if (HmsDataServicepa1 == null)
                            JsonDs1 = HmsDataService.GetJsonStrResult(JsonPap1: JsonPap1, parmXml01DsName: parmXml01DsName, parmXml02DsName: parmXml02DsName, comcod1: Comcod1);
                        else
                            JsonDs1 = HmsDataServicepa1.GetJsonStrResult(JsonPap1: JsonPap1, parmXml01DsName: parmXml01DsName, parmXml02DsName: parmXml02DsName);
                        ds1 = JsonConvert.DeserializeObject<DataSet>(JsonDs1);
                        break;
                    case "NONQUERY":
                        if (HmsDataServicepa1 == null)
                            ds1 = HmsDataService.GetDataSetNonQuerySQL(pap1: pap1, comcod1: Comcod1);
                        else
                            ds1 = HmsDataServicepa1.GetDataSetNonQuerySQL(pap1: pap1);
                        break;
                }
                if (ds1.Tables.Count == 0)
                {
                    DatabaseErrorInfoList.Add(new HmsEntityGeneral.DatabaseErrorInfo { errornumber = 0, errorseverity = 0, errorstate = 0, process_id = "", errorline = 0, errormessage = "Unknown Error Occured", errorprocedure = "" });
                    return null;
                }

                if (ds1.Tables[0].TableName.ToUpper().Contains("ERRORTABLE"))
                {
                    DatabaseErrorInfoList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.DatabaseErrorInfo>();
                    return null;
                }
                return ds1;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("WpfProAcc-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                Application.Current.Shutdown();
                return null;
            }
        }
        public static void ShowDatabaseErrorMessage(string customMsg = "")
        {
            // Error Logs to be written here further
            WpfProcessAccess.DatabaseErrorInfoList = null;
            string Msg1 = (customMsg.Length > 0 ? customMsg : "Database communication error occured.\nRefer to application error log for details.");
            System.Windows.MessageBox.Show(Msg1, WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        public static void ViewReportInWindow(LocalReport rpt1, string WindowTitle1 = "Report Preview", string ShowAs = "Unknown_Normal_Dialog", string RptDisplayMode = "PrintLayout",
            string RenderFileName1 = "", bool OpenFile1 = true, bool IsTopMost = false)
        {
            if (RptDisplayMode == "PDF")
            {
                Rdlc2FilePrint.PrintPDFView(rpt1: rpt1, RenderFileName1: RenderFileName1, OpeFile1: OpenFile1);
                return;
            }
            else if (RptDisplayMode == "WORD")
            {
                Rdlc2FilePrint.PrintWordView(rpt1: rpt1, RenderFileName1: RenderFileName1, OpeFile1: OpenFile1);
                return;
            }
            else if (RptDisplayMode == "EXCEL")
            {
                Rdlc2FilePrint.PrintExcelView(rpt1: rpt1, RenderFileName1: RenderFileName1, OpeFile1: OpenFile1);
                return;
            }

            if (ShowAs == "Unknown_Normal_Dialog")
                ShowAs = WpfProcessAccess.AppRptViewStyle;

            var window1 = new Window();
            //window1.Owner = Application.Current.MainWindow;
            switch (ShowAs.Substring(0, 1).ToUpper())
            {
                case "D":  // "Dialog"
                    window1 = new HmsReportViewer1(rpt1, RptDisplayMode);
                    window1.Title = WindowTitle1;
                    window1.ShowDialog();
                    break;
                case "N": // "Normal"
                    window1 = new HmsReportViewer2(rpt1, RptDisplayMode);
                    window1.Title = WindowTitle1;
                    window1.Show();
                    break;
                default:
                    window1 = new HmsReportViewer2(rpt1, RptDisplayMode);
                    window1.Title = WindowTitle1;
                    window1.Show();
                    break;
            }
            window1.Topmost = IsTopMost;
        }
        #endregion WCF Web Service Management


        #region User Company and Application User Information

        public static void GetAppConfigInfo()
        {
            /*
                 * 
                  string ii = System.IO.Path.Combine(Environment.CurrentDirectory, Application.ProductName + ".EXE");
                    Configuration Config1 = ConfigurationManager.OpenExeConfiguration(ii);
                    ii = Config1.AppSettings.Settings["COMPortConfig"].Value.ToString().Trim();
                    string[] jj = ii.Split(new string[] { "." }, StringSplitOptions.None);
                    string strCOMPortName = jj[0]; // "COM3";
                    string strBaudRate = jj[1];    // "19200";
                    string strParity = jj[2];      // "Even";
                    string strDataBits = jj[3];    // "8";
                    string strStopBits = jj[4];    //  "1";                 
                 */
            string ii = System.IO.Path.Combine(Environment.CurrentDirectory, System.Windows.Forms.Application.ProductName + ".EXE");
            Configuration Config1 = ConfigurationManager.OpenExeConfiguration(ii);
            //Configuration Config1_test = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string[] jj = Config1.AppSettings.Settings["VersionType"].Value.ToString().Trim().Split(new string[] { "." }, StringSplitOptions.None);

            WpfProcessAccess.AppComCode = jj[0].ToString();
            WpfProcessAccess.VersionType = jj[1].ToString();
            WpfProcessAccess.AppLocalImagePath = Config1.AppSettings.Settings["AppLocalImagePath"].Value.ToString().Trim();

            /*            
              var serviceModel = ServiceModelSectionGroup.GetSectionGroup(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
              var endpoints = serviceModel.Client.Endpoints;
              foreach (ChannelEndpointElement e in endpoints)
              {
                if (e.Name == "HTTP_Port")
                Console.WriteLine(e.Address);
              }
              Console.ReadLine();                         
             */
        }

        public static void GetAppVersionInfo()
        {
            var pap1 = vmGenList1.SetParamVersionGenInf();
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            WpfProcessAccess.AppVersionList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.AppVersionInfo>();
        }

        public static void GetCompanyInfoList()
        {
            if (WpfProcessAccess.CompInfList == null)
            {


                //ASITFunParams.ProcessAccessParams pap1 = vmGenList1.SetParamCompBrnSecCodeBook("6501"); // Company Code 6521 for Digilab
                //ASITFunParams.ProcessAccessParams pap1 = vmGenList1.SetParamCompBrnSecCodeBook(jj[0].ToString(), jj[1].ToString(), WpfProcessAccess.HmsVersion); // Company Code 6521 for Digilab
                ASITFunParams.ProcessAccessParams pap1 = vmGenList1.SetParamCompBrnSecCodeBook(WpfProcessAccess.AppComCode, WpfProcessAccess.VersionType, WpfProcessAccess.HmsVersion); // Company Code 6521 for Digilab
                //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                //WpfProcessAccess.HmsVersion

                WpfProcessAccess.CompInfList = vmGenList1.PrepareCompBrnSecList(ds1);
            }
        }

        public static void GetSignedInUserList(string SignInName = "ADMIN", string hcPass = "XXXX", string TerminalID = "UNKNOWN", string newPass1 = "ABCD", string newPass2 = "EFGH")
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            if (WpfProcessAccess.SignedInUserList == null)
            {
                var pap1 = vmGenList1.SetParamSignIn(WpfProcessAccess.CompInfList[0].comcpcod, SignInName, hcPass, TerminalID, newPass1, newPass2);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                if (ds1.Tables[0].Rows.Count == 0)
                    return;

                string _comcod1 = ds1.Tables[0].Rows[0]["comcod"].ToString().Trim();
                string _hccode1 = ds1.Tables[0].Rows[0]["hccode"].ToString().Trim();

                WpfProcessAccess.SignedInUserList = ds1.Tables[0].DataTableToList<HmsEntityManpower.SignInInfo>(); ;
                if (!(ds1.Tables[0].Rows[0]["hcphoto"] is DBNull))
                    WpfProcessAccess.SignedInUserList[0].hcphoto = (byte[])ds1.Tables[0].Rows[0]["hcphoto"];

                if (!(ds1.Tables[0].Rows[0]["hcinisign"] is DBNull))
                    WpfProcessAccess.SignedInUserList[0].hcinisign = (byte[])ds1.Tables[0].Rows[0]["hcinisign"];

                if (!(ds1.Tables[0].Rows[0]["hcfullsign"] is DBNull))
                    WpfProcessAccess.SignedInUserList[0].hcfullsign = (byte[])ds1.Tables[0].Rows[0]["hcfullsign"];

                WpfProcessAccess.SignedInUserAuthList = null;
                WpfProcessAccess.AppFormsList = null;

                //For Temporary Activiting all Form for Testing Purpose
                //WpfProcessAccess.AppFormsList = WpfProcessAccess.FormsList();
                //return;


                // User Authentacion table
                if (ds1.Tables[1].Rows[0]["perdesc"] is DBNull)
                {
                    //if (_hccode1 == "950600801001" || _hccode1 == "950600801002")     // FOR TOP MANAGEMENT
                    if (_hccode1 == "950100101001" || _hccode1 == "950100101002" || _hccode1 == "950600801001" || _hccode1 == "950600801002")     // FOR TOP MANAGEMENT
                    {
                        WpfProcessAccess.AppFormsList = WpfProcessAccess.FormsList();
                        var uiObjList = new HmsEntityGeneral.UserInterfaceAuth().uiObjInfoList;
                        WpfProcessAccess.SignedInUserAuthList = new List<HmsEntityGeneral.UserInterfaceAuth.uiObjSignInAuth>();
                        foreach (var item in uiObjList)
                            WpfProcessAccess.SignedInUserAuthList.Add(new HmsEntityGeneral.UserInterfaceAuth.uiObjSignInAuth() { moduleid = item.moduleid, uicode = item.uicode, objallow = true });
                    }
                    return;
                }

                //string xmlbytostring = HmsHelper.ConvertBinaryToString((byte[])(ds1.Tables[1].Rows[0]["perdesc"]));
                string xmlbytostring = (System.Text.ASCIIEncoding.Default.GetString((byte[])(ds1.Tables[1].Rows[0]["perdesc"])));
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

                WpfProcessAccess.SignedInUserAuthList = tbl1.DataTableToList<HmsEntityGeneral.UserInterfaceAuth.uiObjSignInAuth>();
                WpfProcessAccess.AppFormsList = new List<string>();
                var AllFormsList = WpfProcessAccess.FormsList();
                foreach (var item in WpfProcessAccess.SignedInUserAuthList)
                {
                    string frmId1 = AllFormsList.Find(x => x == item.moduleid.Trim() + "." + item.uicode.Trim().Substring(4));
                    if (frmId1 != null)
                        WpfProcessAccess.AppFormsList.Add(item.moduleid.Trim() + "." + item.uicode.Trim().Substring(4));
                }
            }
        }

        #endregion //User Company and Application User Information


        #region Resource Codebook Information from SIRINF, SIRSPCF table
        public static void GetCompanyStaffList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            if (WpfProcessAccess.StaffList == null)
            {
                var pap1 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "9[56]%", "5");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                WpfProcessAccess.StaffList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
            }
        }

        public static void GetCompanyStaffGroupList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            if (WpfProcessAccess.StaffGroupList == null)
            {
                var pap1 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "950[126]%", "3");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                WpfProcessAccess.StaffGroupList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                //ds1.Tables[0].DataTableToList
            }
        }

        public static void GetSupplierContractorList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            if (WpfProcessAccess.SupplierContractorList == null)
            {
                var pap1 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "9[89]%", "5");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                WpfProcessAccess.SupplierContractorList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
            }
        }

        public static void GetInventoryItemGroupList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            if (WpfProcessAccess.InvItemGroupList == null)
            {
                var pap1 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "0[1-9]%", "3"); //"[0-4]%"
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var pap2 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "4521%", "3"); //"[0-4]%"
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
                if (ds2 == null)
                    return;


                var pap3 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "41%", "3"); //"[0-4]%"
                DataSet ds3 = WpfProcessAccess.GetHmsDataSet(pap3);
                if (ds3 == null)
                    return;

                var pap4 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "21%", "3"); //"[0-4]%"
                DataSet ds4 = WpfProcessAccess.GetHmsDataSet(pap4);
                if (ds4 == null)
                    return;


                var list1 = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                var list2 = ds2.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                var list3 = ds3.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                var list4 = ds4.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();

                WpfProcessAccess.InvItemGroupList = list1.Union(list2).Union(list3).Union(list4).ToList();
            }
        }

        public static void GetInventoryItemList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            if (WpfProcessAccess.InvItemList == null)
            {
                var pap1 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "0[1-9]%", "5"); //"[0-4]%"
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var pap2 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "4521%", "5"); //"[0-4]%"
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
                if (ds2 == null)
                    return;

                var pap3 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "41%", "5"); //"[0-4]%"
                DataSet ds3 = WpfProcessAccess.GetHmsDataSet(pap3);
                if (ds3 == null)
                    return;

                var pap4 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "21%", "5"); //"[0-4]%"
                DataSet ds4 = WpfProcessAccess.GetHmsDataSet(pap4);
                if (ds4 == null)
                    return;


                var list1 = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                var list2 = ds2.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                var list3 = ds3.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                var list4 = ds4.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                WpfProcessAccess.InvItemList = list1.Union(list2).Union(list3).Union(list4).ToList();

                //WpfProcessAccess.InvItemList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
            }
        }

        public static void GetGenInfoTitleList()
        {
            string CodeGroup = "%";
            if (WpfProcessAccess.GenInfoTitleList == null)
            {
                var pap1 = vmGenList1.SetParamGeneralInfoCodeBook(CodeGroup);
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                WpfProcessAccess.GenInfoTitleList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.AcInfCodeBook>();

            }
        }

        public static void GetAccCodeList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            if (WpfProcessAccess.AccCodeList == null)
            {
                // string CompCode, string FilterGroup = "%", string FilterLevel = "1", string OutputControl = ""
                var pap1 = vmGenList1.SetParamAcInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "%", "1234");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                WpfProcessAccess.AccCodeList = ds1.Tables[0].DataTableToList<HmsEntityGeneral.AcInfCodeBook>();
            }
        }
        public static void GetAccSirCodeList()
        {
            if (WpfProcessAccess.CompInfList == null)
                return;

            if (WpfProcessAccess.AccSirCodeList == null)
            {
                var pap1 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "[0123458][0-9]%", "12345");
                DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1 == null)
                    return;

                var pap2 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "9[589]%", "12345");
                DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap2);
                if (ds2 == null)
                    return;

                //var pap3 = vmGenList1.SetParamSirInfCodeBook(WpfProcessAccess.CompInfList[0].comcpcod, "45%", "12345");
                //DataSet ds3 = WpfProcessAccess.GetHmsDataSet(pap3);
                //if (ds3 == null)
                //    return;

                var list1 = ds1.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                var list2 = ds2.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();
                //var list3 = ds3.Tables[0].DataTableToList<HmsEntityGeneral.SirInfCodeBook>();

                WpfProcessAccess.AccSirCodeList = list1.Union(list2).ToList(); //list1.Union(list2).Union(list3).ToList();
            }
        }



        public static DataSet UpdateDeleteDraftTransaction(string UpadateDelete1, DataSet ds1, string draftnum1, Int64 rowid1, string draftrmrk1, string draftbyid1, string draftses1, string drafttrm1)
        {
            var pap1 = vmGenList1.SetParamUpdateDrafts(CompCode: WpfProcessAccess.CompInfList[0].comcod, ds1a: ds1, UpadateDelete1a: UpadateDelete1, draftnum1a: draftnum1,
                rowid1a: rowid1, draftrmrk1a: draftrmrk1, draftbyid1a: draftbyid1, draftses1a: draftses1, drafttrm1a: drafttrm1);

            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            return ds2;
        }

        public static List<vmHmsGeneralList1.DraftTransactionList> GetDraftTransactionList(string memohead1 = "XXX", string draftDesc1 = "%", string signinnam1 = "%", string drafttrm1 = "%", 
            string DateFrom1 = "01-Jan-2001", string DateTo1 = "31-Dec-2099")
        {
            var pap1 = vmGenList1.SetParamDraftTransList(CompCode: WpfProcessAccess.CompInfList[0].comcod, memohead1a: memohead1, draftDesc1a: draftDesc1, signinnam1a: signinnam1, 
                        drafttrm1a: drafttrm1, DateFrom1a: DateFrom1, DateTo1a: DateTo1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            if (ds2 == null)
                return null;

            return ds2.Tables[0].DataTableToList<vmHmsGeneralList1.DraftTransactionList>().ToList();
        }

        public static DataSet RetriveDraftTransactionInfo(string MemoNum1 = "XXXXXXXX", Int64 rowid1 = 0)
        {
            var pap1 = vmGenList1.SetParamDraftTransInfo(CompCode: WpfProcessAccess.CompInfList[0].comcod, draftnum1a: MemoNum1, rowid1a: rowid1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            return ds2;
        }

        public static DataSet UpdateUserLogInfo(string logbyid1 = "000000000000", string logses1 = "", string logtrm1 = "", string trnnum1 = "000000000000000000", string logref1 = "", DataSet logdata1 = null)
        {
            if (WpfProcessAccess.SignedInUserList != null)
            {
                logbyid1 = (logbyid1 == "000000000000" ? WpfProcessAccess.SignedInUserList[0].hccode : logbyid1);
                logses1 = (logses1.Length == 0 ? WpfProcessAccess.SignedInUserList[0].sessionID : logses1);
                logtrm1 = (logtrm1.Length == 0 ? WpfProcessAccess.SignedInUserList[0].terminalID : logtrm1);
                logref1 = WpfProcessAccess.SignedInUserList[0].signinnam.Trim() + " : " + logref1.Trim() + " at WST : " + DateTime.Now.ToString("dddd dd-MMM-yyyy hh:mm:ss.fff tt"); 
            }
            var pap1 = vmGenList1.SetParamUpdateUserLog(CompCode: WpfProcessAccess.CompInfList[0].comcod, logbyid1a: logbyid1, logses1a: logses1, logtrm1a: logtrm1, trnnum1a: trnnum1, logref1a: logref1, logdata1a: logdata1);
            DataSet ds2 = WpfProcessAccess.GetHmsDataSet(pap1: pap1);
            return ds2;
        }


        public static void GetAllGenCodeList()  // Not yet implemented anywhere 
        {
            WpfProcessAccess.AccCodeList = null;
            WpfProcessAccess.AccSirCodeList = null;
            WpfProcessAccess.StaffGroupList = null;
            WpfProcessAccess.StaffList = null;
            WpfProcessAccess.SupplierContractorList = null;
            WpfProcessAccess.GenInfoTitleList = null;
            WpfProcessAccess.InvItemGroupList = null;
            WpfProcessAccess.InvItemList = null;

            WpfProcessAccess.GetAccCodeList();
            WpfProcessAccess.GetAccSirCodeList();
            WpfProcessAccess.GetCompanyStaffGroupList();
            WpfProcessAccess.GetCompanyStaffList();
            WpfProcessAccess.GetSupplierContractorList();
            WpfProcessAccess.GetGenInfoTitleList();
            WpfProcessAccess.GetInventoryItemGroupList();
            WpfProcessAccess.GetInventoryItemList();
        }

        #endregion //Codebook Information from SIRINF, SIRSPCF table

        #region RDLC Report Related Functions
        public static List<HmsEntityGeneral.ReportGeneralInfo> GetRptGenInfo(DateTime ServerTime = default(DateTime), string InputSource = "")
        {
            var list3 = new List<HmsEntityGeneral.ReportGeneralInfo>();
            list3.Add(new HmsEntityGeneral.ReportGeneralInfo()
            {
                RptCompName = WpfProcessAccess.CompInfList[0].comnam,
                RptCompAdd1 = WpfProcessAccess.CompInfList[0].comadd1,
                RptCompAdd2 = WpfProcessAccess.CompInfList[0].comadd2,
                RptCompAdd3 = WpfProcessAccess.CompInfList[0].comadd3,
                RptCompAdd4 = WpfProcessAccess.CompInfList[0].comadd4,
                RptFooter1 = (InputSource.Length > 0 ? "Input Source: " + InputSource + " / " : "") + "Print Source: " + WpfProcessAccess.SignedInUserList[0].terminalID + ", " +
                             WpfProcessAccess.SignedInUserList[0].signinnam + ", " +
                             WpfProcessAccess.SignedInUserList[0].sessionID + ", " + (ServerTime.Year > 1900 ? ServerTime.ToString("dd-MMM-yyyy hh:mm:ss tt") : DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"))

            });
            return list3;
        }
        #endregion  // RDLC Report Related Functions

        #region Others Functions relarted to run the application

        // var new1 = new List<MyObject>(a1);
        // var new1 = new List<MyObject>(a1.Select(x => x.Clone()));
        // var clonedList = originaList.DeepClone();


        public static string GetMacAddress()
        {
            string strMac1 = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string strMac2 = "";
                //if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                if ((nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet || nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                    && nic.OperationalStatus == OperationalStatus.Up)
                {
                    //return nic.GetPhysicalAddress();
                    var address1 = nic.GetPhysicalAddress();

                    byte[] bytes = address1.GetAddressBytes();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        strMac2 += bytes[i].ToString("X2");
                        // Display the physical address in hexadecimal.
                        //Console.Write("{0}", bytes[i].ToString("X2"));
                        // Insert a hyphen after each byte, unless we are at the end of the
                        // address.
                        if (i != bytes.Length - 1)
                        {
                            //strMac2 += "-";
                            //Console.Write("-");
                        }
                    }
                    //return strMac2;
                }
                if (strMac2.Trim().Length > 0)
                    strMac1 += (strMac1.Trim().Length > 0 ? ", " : "") + strMac2;
            }
            return strMac1;
        }


        #endregion  // Others Functions relarted to run the application

        #region Common MenuItem List

  
        public static List<MenuItem> GetCommonMenuItemList()
        {
            return vmHmsGeneral1.MenuDefination.GetCommonMenuItemList();
        }

        public static List<MenuItem> GetCommonMenuItemList(string ModuleOption1 = "NOTHING")
        {
            return vmHmsGeneral1.MenuDefination.GetCommonMenuItemList(ModuleOption1);
        }
        #endregion

        #region Form List and Creating Forms
        public static List<string> FormsList()
        {
            List<string> frmlst1 = new List<string>();

            frmlst1.Add("Commercial.Hospital.frmEntryFrontDesk101");
            frmlst1.Add("Commercial.Hospital.frmEntryFrontDesk102");
            frmlst1.Add("Commercial.Hospital.frmEntryFrontDesk103");
            frmlst1.Add("Commercial.Hospital.frmEntryFrontDesk104");
            frmlst1.Add("Commercial.Hospital.frmEntryFrontDesk3_Old");
            frmlst1.Add("Commercial.Hospital.frmEntryDocVisit1");

            frmlst1.Add("Commercial.Pharmacy.frmEntryPharmaPOS1");
            frmlst1.Add("Commercial.FoodShop.frmEntryRestauPOS101");
            frmlst1.Add("Commercial.FoodShop.frmEntryRestauPOS102");
            frmlst1.Add("Commercial.FoodShop.frmEntryRestauPOS103");

            frmlst1.Add("Commercial.Pharmacy.frmReportPharmaPOS1");
            frmlst1.Add("Commercial.Trading.frmEntryGenTrPOS101");
            frmlst1.Add("Commercial.Trading.frmEntryGenTrPOS103");

            frmlst1.Add("Commercial.ParkTicket.frmEntryParkPOS101");
            frmlst1.Add("Commercial.ParkTicket.frmEntryParkPOS102");
            frmlst1.Add("Commercial.ParkTicket.frmEntryParkPOS103");
            frmlst1.Add("Commercial.ParkTicket.frmEntryParkPOS105");

            frmlst1.Add("Commercial.SuperShop.frmEntryShopPOS101");
            frmlst1.Add("Commercial.SuperShop.frmEntryShopPOS102");
            frmlst1.Add("Commercial.SuperShop.frmEntryShopPOS103");

            frmlst1.Add("Commercial.RealEstate.frmRealSaleMgt101");
            frmlst1.Add("Commercial.RealEstate.frmRealSaleMgt102");
            frmlst1.Add("Commercial.RealEstate.frmRealSaleMgt103");
            frmlst1.Add("Commercial.RealEstate.frmRealSaleMgt107");

            frmlst1.Add("Diagnostic.frmEntryLabReport1");
            frmlst1.Add("Diagnostic.frmEntryLabMagt101_SAMPLERECEIVE");
            frmlst1.Add("Diagnostic.frmEntryLabMagt101_REPORTDOCS");
            frmlst1.Add("Diagnostic.frmEntryLabMagt101_REPORTSUBMIT");
            frmlst1.Add("Diagnostic.frmEntryLabMagt107");

            frmlst1.Add("Inventory.frmEntryInvMgt101");
            frmlst1.Add("Inventory.frmEntryInvMgt102");
            frmlst1.Add("Inventory.frmEntryInvMgt103");
            frmlst1.Add("Inventory.frmEntryStoreReq1");
            frmlst1.Add("Inventory.frmEntryStoreIssue1");
            frmlst1.Add("Inventory.frmEntryItemRcv1");
            frmlst1.Add("Inventory.frmEntryItemStock1");

            frmlst1.Add("Inventory.frmEntryPurReq1");
            frmlst1.Add("Inventory.frmEntryPurReqAppr1");
            frmlst1.Add("Inventory.frmEntryPurOrder1");
            frmlst1.Add("Inventory.frmEntryPurQuotation1");
            frmlst1.Add("Inventory.frmEntryPurRateFix1");
            frmlst1.Add("Inventory.frmEntryPurBillRcv1");
            frmlst1.Add("Inventory.frmEntryPurLCInfo1");
            frmlst1.Add("Inventory.frmReportStore1");
            frmlst1.Add("Inventory.frmInvDashBoard1");
            frmlst1.Add("Inventory.frmPurDashBoard1");

            frmlst1.Add("Accounting.frmEntryAccMgt101");
            frmlst1.Add("Accounting.frmEntryAccMgt102");
            frmlst1.Add("Accounting.frmEntryAccMgt103");
            frmlst1.Add("Accounting.frmEntryVoucher1");
            frmlst1.Add("Accounting.frmReportAcc1");

            frmlst1.Add("Budget.BgdAccounts.frmAccBgd101");
            frmlst1.Add("Budget.BgdAccounts.frmAccBgd102");
            frmlst1.Add("Budget.BgdInventory.frmInvBgd101");
            frmlst1.Add("Budget.BgdRealEstate.frmRealBgd101");

            frmlst1.Add("Marketing.frmEntryMarketing1");
            frmlst1.Add("Marketing.frmReportMarketing1");

            frmlst1.Add("Manpower.frmEntryAttn101");
            frmlst1.Add("Manpower.frmEntryAttn102");
            frmlst1.Add("Manpower.frmEntryAttn103");
            frmlst1.Add("Manpower.frmEntryAttn104");
            frmlst1.Add("Manpower.frmEntryPayroll101");
            frmlst1.Add("Manpower.frmEntryRecruit1");
            frmlst1.Add("Manpower.frmEntryHRGenral1");
            frmlst1.Add("Manpower.frmReportHCM1");
            frmlst1.Add("Manpower.frmMessagegMgt101");
            frmlst1.Add("Manpower.frmMessagegMgt102");
            frmlst1.Add("Manpower.frmMessagegMgt103");

            frmlst1.Add("General.frmAccCodeBook1");
            frmlst1.Add("General.frmSirCodeBook1");
            frmlst1.Add("General.frmSectCodeBook1");
            frmlst1.Add("General.frmOtherCodeBook1");
            frmlst1.Add("General.frmConfigSetup1");
            frmlst1.Add("General.frmReportAdmin1");

            frmlst1.Add("MISReports.frmMISGeneral1");
            frmlst1.Add("MISReports.frmMISHospital1");

            return frmlst1;
        }

        public static UserControl CreateUserControl(string ucName1)
        {

            UserControl usrCtrl1 = null;
            string[] ucName1a = ucName1.Split(',');
            switch (ucName1a[0])
            {
                case "Commercial.Hospital.frmEntryFrontDesk101": usrCtrl1 = new Commercial.Hospital.frmEntryFrontDesk101(); break;
                case "Commercial.Hospital.frmEntryFrontDesk102": usrCtrl1 = new Commercial.Hospital.frmEntryFrontDesk102(); break;
                case "Commercial.Hospital.frmEntryFrontDesk103": usrCtrl1 = new Commercial.Hospital.frmEntryFrontDesk103(); break;
                case "Commercial.Hospital.frmEntryFrontDesk104": usrCtrl1 = new Commercial.Hospital.frmEntryFrontDesk104(); break;
                case "Commercial.Hospital.frmEntryDocVisit1": usrCtrl1 = new Commercial.Hospital.frmEntryDocVisit1(); break;
                case "Commercial.Hospital.frmEntryFrontDesk3_Old": usrCtrl1 = new Commercial.Hospital.frmEntryFrontDesk3_Old(); break;

                case "Commercial.Pharmacy.frmEntryPharmaPOS1": usrCtrl1 = new Commercial.Pharmacy.frmEntryPharmaPOS1(); break;
                case "Commercial.FoodShop.frmEntryRestauPOS101": usrCtrl1 = new Commercial.FoodShop.frmEntryRestauPOS101(); break; // new Commercial.frmEntryRestauPOS1old(); break;
                case "Commercial.FoodShop.frmEntryRestauPOS102": usrCtrl1 = new Commercial.FoodShop.frmEntryRestauPOS102(); break;
                case "Commercial.FoodShop.frmEntryRestauPOS103": usrCtrl1 = new Commercial.FoodShop.frmEntryRestauPOS103(); break; 
                case "Commercial.Pharmacy.frmReportPharmaPOS1": usrCtrl1 = new Commercial.Pharmacy.frmReportPharmaPOS1(); break;
                case "Commercial.Trading.frmEntryGenTrPOS101": usrCtrl1 = new Commercial.Trading.frmEntryGenTrPOS101(); break;
                case "Commercial.Trading.frmEntryGenTrPOS103": usrCtrl1 = new Commercial.Trading.frmEntryGenTrPOS103(); break;

                case "Commercial.ParkTicket.frmEntryParkPOS101": usrCtrl1 = new Commercial.ParkTicket.frmEntryParkPOS101(); break;
                case "Commercial.ParkTicket.frmEntryParkPOS102": usrCtrl1 = new Commercial.ParkTicket.frmEntryParkPOS102(); break;
                case "Commercial.ParkTicket.frmEntryParkPOS103": usrCtrl1 = new Commercial.ParkTicket.frmEntryParkPOS103(); break;
                case "Commercial.ParkTicket.frmEntryParkPOS105": usrCtrl1 = new Commercial.ParkTicket.frmEntryParkPOS105(); break;

                case "Commercial.SuperShop.frmEntryShopPOS101": usrCtrl1 = new Commercial.SuperShop.frmEntryShopPOS101(); break;
                case "Commercial.SuperShop.frmEntryShopPOS102": usrCtrl1 = new Commercial.SuperShop.frmEntryShopPOS102(); break;
                case "Commercial.SuperShop.frmEntryShopPOS103": usrCtrl1 = new Commercial.SuperShop.frmEntryShopPOS103(); break;


                case "Commercial.RealEstate.frmRealSaleMgt101": usrCtrl1 = new Commercial.RealEstate.frmRealSaleMgt101(); break;
                case "Commercial.RealEstate.frmRealSaleMgt102": usrCtrl1 = new Commercial.RealEstate.frmRealSaleMgt102(); break;
                case "Commercial.RealEstate.frmRealSaleMgt103": usrCtrl1 = new Commercial.RealEstate.frmRealSaleMgt103(); break;
                case "Commercial.RealEstate.frmRealSaleMgt107": usrCtrl1 = new Commercial.RealEstate.frmRealSaleMgt107(); break;

                case "Diagnostic.frmEntryLabMagt101_SAMPLERECEIVE": usrCtrl1 = new Diagnostic.frmEntryLabMagt101() { Uid = "SAMPLERECEIVE" }; break;
                case "Diagnostic.frmEntryLabMagt101_REPORTDOCS": usrCtrl1 = new Diagnostic.frmEntryLabMagt101() { Uid = "REPORTDOCS" }; break;
                case "Diagnostic.frmEntryLabMagt101_REPORTSUBMIT": usrCtrl1 = new Diagnostic.frmEntryLabMagt101() { Uid = "REPORTSUBMIT" }; break;
            
                case "Diagnostic.frmEntryLabMagt107": usrCtrl1 = new Diagnostic.frmEntryLabMagt107(); break;
                case "Diagnostic.frmEntryLabReport1": usrCtrl1 = new Diagnostic.frmEntryLabReport1(); break;

                case "Inventory.frmEntryInvMgt101": usrCtrl1 = new Inventory.frmEntryInvMgt101(); break;
                case "Inventory.frmEntryInvMgt102": usrCtrl1 = new Inventory.frmEntryInvMgt102(); break;
                case "Inventory.frmEntryInvMgt103": usrCtrl1 = new Inventory.frmEntryInvMgt103(); break;
                case "Inventory.frmEntryStoreReq1": usrCtrl1 = new Inventory.frmEntryStoreReq1() { IsActiveTransListWindow = false }; break;
                case "Inventory.frmEntryStoreIssue1": usrCtrl1 = new Inventory.frmEntryStoreIssue1(); break;
                case "Inventory.frmEntryItemRcv1": usrCtrl1 = new Inventory.frmEntryItemRcv1(); break;
                case "Inventory.frmEntryItemStock1": usrCtrl1 = new Inventory.frmEntryItemStock1(); break;

                case "Inventory.frmEntryPurReq1": usrCtrl1 = new Inventory.frmEntryPurReq1(); break;
                case "Inventory.frmEntryPurReqAppr1": usrCtrl1 = new Inventory.frmEntryPurReqAppr1(); break;
                case "Inventory.frmEntryPurOrder1": usrCtrl1 = new Inventory.frmEntryPurOrder1(); break;
                case "Inventory.frmEntryPurQuotation1": usrCtrl1 = new Inventory.frmEntryPurQuotation1(); break;
                case "Inventory.frmEntryPurRateFix1": usrCtrl1 = new Inventory.frmEntryPurRateFix1(); break;
                case "Inventory.frmEntryPurBillRcv1": usrCtrl1 = new Inventory.frmEntryPurBillRcv1(); break;
                case "Inventory.frmEntryPurLCInfo1": usrCtrl1 = new Inventory.frmEntryPurLCInfo1(); break;
                case "Inventory.frmReportStore1": usrCtrl1 = new Inventory.frmReportStore1(); break;

                case "Accounting.frmEntryAccMgt101": usrCtrl1 = new Accounting.frmEntryAccMgt101(); break;
                case "Accounting.frmEntryAccMgt102": usrCtrl1 = new Accounting.frmEntryAccMgt102(); break;
                case "Accounting.frmEntryAccMgt103": usrCtrl1 = new Accounting.frmEntryAccMgt103(); break;
                case "Accounting.frmEntryVoucher1": usrCtrl1 = new Accounting.frmEntryVoucher1(); break;

                case "Accounting.frmReportAcc1": usrCtrl1 = new Accounting.frmReportAcc1(); break;



                case "Budget.BgdAccounts.frmAccBgd101": usrCtrl1 = new Budget.BgdAccounts.frmAccBgd101(); break;
                case "Budget.BgdAccounts.frmAccBgd102": usrCtrl1 = new Budget.BgdAccounts.frmAccBgd102(); break;
                case "Budget.BgdInventory.frmInvBgd101": usrCtrl1 = new Budget.BgdInventory.frmInvBgd101(); break;
                    
                case "Budget.BgdRealEstate.frmRealBgd101": usrCtrl1 = new Budget.BgdRealEstate.frmRealBgd101(); break;

                case "Marketing.frmEntryMarketing1": usrCtrl1 = new Marketing.frmEntryMarketing1(); break;
                case "Marketing.frmReportMarketing1": usrCtrl1 = new Marketing.frmReportMarketing1(); break;

                case "Manpower.frmEntryAttn101": usrCtrl1 = new Manpower.frmEntryAttn101(); break;
                case "Manpower.frmEntryAttn102": usrCtrl1 = new Manpower.frmEntryAttn102(); break;
                case "Manpower.frmEntryAttn103": usrCtrl1 = new Manpower.frmEntryAttn103(); break;
                case "Manpower.frmEntryAttn104": usrCtrl1 = new Manpower.frmEntryAttn104(); break;
                case "Manpower.frmEntryPayroll101": usrCtrl1 = new Manpower.frmEntryPayroll101(); break;
                case "Manpower.frmEntryRecruit1": usrCtrl1 = new Manpower.frmEntryRecruit1(); break;
                case "Manpower.frmEntryHRGenral1": usrCtrl1 = new Manpower.frmEntryHRGenral1(); break;
                case "Manpower.frmReportHCM1": usrCtrl1 = new Manpower.frmReportHCM1(); break;
                case "Manpower.frmMessagegMgt101": usrCtrl1 = new Manpower.frmMessagegMgt101(); break;
                case "Manpower.frmMessagegMgt102": usrCtrl1 = new Manpower.frmMessagegMgt102(); break;
                case "Manpower.frmMessagegMgt103": usrCtrl1 = new Manpower.frmMessagegMgt103(); break;

                case "General.frmAccCodeBook1": usrCtrl1 = new General.frmAccCodeBook1(); break;
                case "General.frmSirCodeBook1": usrCtrl1 = new General.frmSirCodeBook1(); break;
                case "General.frmSectCodeBook1": usrCtrl1 = new General.frmSectCodeBook1(); break;
                case "General.frmOtherCodeBook1": usrCtrl1 = new General.frmOtherCodeBook1(); break;
                case "General.frmConfigSetup1": usrCtrl1 = new General.frmConfigSetup1(); break;
                case "General.frmReportAdmin1": usrCtrl1 = new General.frmReportAdmin1(); break;

                case "MISReports.frmMISGeneral1": usrCtrl1 = new MISReports.frmMISGeneral1(); break;
                case "MISReports.frmMISHospital1": usrCtrl1 = new MISReports.frmMISHospital1(); break;

                default: usrCtrl1 = new General.frmReportAdmin1(); break;
            }
            return usrCtrl1;
        }

        #endregion //Form List and Creating Forms
    }
}

