using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.Reporting.WinForms;
using ASITHmsEntity;
using System.Threading.Tasks;

namespace ASITHmsRpt1GenAcc.General
{
    public static class GeneralReportSetup
    {
        public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
//            Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt1GenAcc.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("ASITHmsRpt1GenAcc." + RptName + ".rdlc");
            LocalReport Rpt1a = new LocalReport();
            Rpt1a.DisplayName = RptName;
            Rpt1a.LoadReportDefinition(stream1);
            Rpt1a.DataSources.Clear();
            Rpt1a.SetParameters(new ReportParameter("ParmCompNam1", "Advanced Software & IT Services Ltd."));
            Rpt1a.SetParameters(new ReportParameter("ParmFooter1", "Print Source: TerminalID, UserID, Session, Print Date & Time"));
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("ParmCompNam1", list3[0].RptCompName));
                Rpt1a.SetParameters(new ReportParameter("ParmFooter1", list3[0].RptFooter1));
            }
            switch (Rpt1a.DisplayName.Trim())
            {
                case "General.rptAccCodeBook1": Rpt1a = SetGeneral_rptAccCodeBook1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "General.rptSirCodeBook1": Rpt1a = SetGeneral_rptSirCodeBook1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "General.RptComSections1": Rpt1a = SetGeneral_RptComSections1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "General.rptAppUserList01": Rpt1a = SetGeneral_rptAppUserList01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "General.rptAppUserAuth01": Rpt1a = SetGeneral_rptAppUserAuth01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
            }
            Rpt1a.Refresh();
            return Rpt1a;
        }
        private static LocalReport SetGeneral_rptAppUserAuth01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptParVal2));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityGeneral.UserInterfaceAuth.uiObjInfo>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetGeneral_rptAppUserList01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptParVal2));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityGeneral.UserInterfaceAuth.AppUserList>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetGeneral_RptComSections1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            string level1 = list3[0].RptCompAdd3.Trim();

            var list1 = (List<HmsEntityGeneral.CompInfCodeBook>) RptDataSet;
            var branchlst = list1[0].BranchList;
            var Sectlst = list1[0].SectionList;
            var RptList = new List<HmsEntityGeneral.RptSectList>();
            int i = 1;
            foreach (var item in Sectlst)
            {
                string scod1 = item.sectcod.Substring(0, 4) + "-" + item.sectcod.Substring(4, 3) + "-" + item.sectcod.Substring(7, 2) + "-" + item.sectcod.Substring(9, 3);
                string bcod1 = branchlst.Find(x => x.brncod == item.sectcod.Substring(0, 4)).brncod;
                string bnam1 = branchlst.Find(x => x.brncod == item.sectcod.Substring(0, 4)).brnnam;
                string sl1 = i.ToString() + ".";
                if (item.sectcod.Substring(7, 5) == "00000")
                    RptList.Add(new HmsEntityGeneral.RptSectList() { slnum = sl1, comcod = Sectlst[0].comcod, sectcod = item.sectcod, sectcod1 = scod1, sectname = item.sectname, sectdesc = item.sectdesc, brncod = bcod1, brnsnam = bnam1 });
                else if (level1.Contains("3") && item.sectcod.Substring(9, 3) == "000")
                    RptList.Add(new HmsEntityGeneral.RptSectList() { slnum = sl1, comcod = Sectlst[0].comcod, sectcod = item.sectcod, sectcod1 = scod1, sectname = item.sectname,  sectdesc = item.sectdesc, brncod = bcod1, brnsnam = bnam1 });
                else if (level1.Contains("4") && item.sectcod.Substring(9, 3) != "000")
                    RptList.Add(new HmsEntityGeneral.RptSectList() { slnum = sl1, comcod = Sectlst[0].comcod, sectcod = item.sectcod, sectcod1 = scod1, sectname = item.sectname, sectdesc = item.sectdesc, brncod = bcod1, brnsnam = bnam1 });
                i++;
            }
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", RptList));

            return Rpt1a;
        }
        private static LocalReport SetGeneral_rptAccCodeBook1(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityGeneral.AcInfCodeBook>)RptDataSet));

            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", ((string)RptDataSet2).ToString()));

            ////Rpt1a.DataSources.Add(new ReportDataSource(RptDataSet.Tables[0].TableName, RptDataSet.Tables[0]));
            ////Rpt1a.SetParameters(new ReportParameter("ParmCompNam1", RptDataSet.Tables[1].Rows[0]["CompNam1"].ToString().Trim()));
            ////Rpt1a.SetParameters(new ReportParameter("ParamTitle1", RptDataSet.Tables[1].Rows[0]["RptTitle1"].ToString().Trim()));
            ////Rpt1a.SetParameters(new ReportParameter("ParmFooter1", RptDataSet.Tables[1].Rows[0]["RptTime"].ToString().Trim()));
            return Rpt1a;
        }

        private static LocalReport SetGeneral_rptSirCodeBook1(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityGeneral.SirInfCodeBook>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", ((string)RptDataSet2).ToString()));
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Subsidiary Chart of Accounts (All Level)"));

            ////Rpt1a.DataSources.Add(new ReportDataSource(RptDataSet.Tables[0].TableName, RptDataSet.Tables[0]));
            ////Rpt1a.SetParameters(new ReportParameter("ParmCompNam1", RptDataSet.Tables[1].Rows[0]["CompNam1"].ToString().Trim()));
            ////Rpt1a.SetParameters(new ReportParameter("ParamTitle1", RptDataSet.Tables[1].Rows[0]["RptTitle1"].ToString().Trim()));
            ////Rpt1a.SetParameters(new ReportParameter("ParmFooter1", RptDataSet.Tables[1].Rows[0]["RptTime"].ToString().Trim()));
            return Rpt1a;
        }

        //private static LocalReport DefineReport(string RptName, DataSet RptDataSet, DataSet UserDataset)
        //{
        //    Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt1GenAcc.dll");
        //    Stream stream1 = assembly1.GetManifestResourceStream("ASITHmsRpt1GenAcc." + RptName + ".rdlc");
        //    LocalReport Rpt1a = new LocalReport();
        //    Rpt1a.LoadReportDefinition(stream1);
        //    Rpt1a.DataSources.Clear();
        //    ////Rpt1a.DataSources.Add(new ReportDataSource(RptDataSet.Tables[0].TableName, RptDataSet.Tables[0]));
        //    ////Rpt1a.SetParameters(new ReportParameter("ParmCompNam1", RptDataSet.Tables[1].Rows[0]["CompNam1"].ToString().Trim()));
        //    ////Rpt1a.SetParameters(new ReportParameter("ParamTitle1", RptDataSet.Tables[1].Rows[0]["RptTitle1"].ToString().Trim()));
        //    ////Rpt1a.SetParameters(new ReportParameter("ParmFooter1", RptDataSet.Tables[1].Rows[0]["RptTime"].ToString().Trim()));
        //    return Rpt1a;
        //}

        //public static LocalReport RptAccCodeBook1(DataSet RptDataSet, DataSet UserDataset)
        //{
        //    LocalReport LocalRpt1 = DefineReport("General.rptAccCodeBook1", RptDataSet, UserDataset);
        //    //ReportParameter[] param1 = new ReportParameter[LocalRpt1.GetParameters().Count()];
        //    //param1[0] = new ReportParameter("ParamTitle", RptDataSet.Tables[1].Rows[0]["RptTitle"].ToString().Trim());
        //    return LocalRpt1;
        //}
    }
}
