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
using System.Collections;

namespace ASITHmsRpt3Manpower
{
    public static class HcmReportSetup
    {

        #region GetLocalReport: Loading and receving report resources
        
        //public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        public static LocalReport GetLocalReport(string RptName = "XYZ", Object RptDataSet = null, Object RptDataSet2 = null, Object UserDataset = null, Object RptDataSet4 = null)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
//            Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt3Manpower.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("ASITHmsRpt3Manpower." + RptName + ".rdlc");
            LocalReport Rpt1a = new LocalReport();
            Rpt1a.DisplayName = RptName;
            Rpt1a.LoadReportDefinition(stream1);
            Rpt1a.DataSources.Clear();
            Rpt1a.SetParameters(new ReportParameter("ParmCompNam1", "ASIT Services Ltd."));
            Rpt1a.SetParameters(new ReportParameter("ParmFooter1", "Print Source: TerminalID, UserID, Session, Print Date & Time"));

            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("ParmCompNam1", list3[0].RptCompName));
                Rpt1a.SetParameters(new ReportParameter("ParmFooter1", list3[0].RptFooter1));
            }
            switch (Rpt1a.DisplayName.Trim())
            {
                case "HcmInfo.RptHcmGenInf01": Rpt1a = SetHcmInfo_HcmGenInf01(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Payroll.RptLeaveForm01": Rpt1a = SetHcmInfo_LeaveForm01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Payroll.RptAttenSchedule01": Rpt1a = SetHcmInfo_AttenSchedule01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Payroll.RptSalarySheet01":
                case "Payroll.RptOverTimeSheet01":
                case "Payroll.RptPaySlip001":
                case "Payroll.RptBonusSheet01": Rpt1a = SetPayroll_MonthlySalarySheet01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Payroll.RptBankLetter01": Rpt1a = SetPayroll_MonthlySalarySheetBankLetter01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;                  
                case "Payroll.RptAbstractMonthSalary01": Rpt1a = SetPayroll_AbstractMonthSalary01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Payroll.RptAbstMonthDeducSal01": Rpt1a = SetPayroll_AbstMonthDeducSal01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Payroll.RptDailyAttn01": Rpt1a = SetPayroll_rptDailyAttn01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Payroll.RptMonthAttnSum01": Rpt1a = SetPayroll_rptMonthAttnSum01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Payroll.RptLeaveDetails01": Rpt1a = SetPayroll_RptLeaveDetails01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
            }
            Rpt1a.Refresh();
            return Rpt1a;
        }

        private static LocalReport SetPayroll_RptLeaveDetails01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable rptParm = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", rptParm["Title1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", rptParm["Title2"].ToString()));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.HcmLeaveDetailsReport01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetPayroll_rptDailyAttn01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable rptParm = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ReportDate", rptParm["ReportDate"].ToString()));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.HcmDayWiseAttanReport>)RptDataSet));

            return Rpt1a;

        }

        private static LocalReport SetPayroll_rptMonthAttnSum01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable rptParm = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ReportDate", rptParm["ReportDate"].ToString()));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.HcmMonthAttnEvalReport01>)RptDataSet));

            return Rpt1a;

        }
        private static LocalReport SetHcmInfo_AttenSchedule01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Hashtable rptParm = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("Comlogo",rptParm["Comlogo"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("empId", rptParm["empId"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("empName", rptParm["empName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("slMnth", rptParm["slMnth"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParmNotes1", rptParm["ParmNotes1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParmBrnDept1", rptParm["ParmBrnDept1"].ToString()));           
            
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.RptAttnSchInfo>)RptDataSet));
           
            return Rpt1a;
        }

        #endregion

        #region General Information reports parameter settings
        private static LocalReport SetHcmInfo_HcmGenInf01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset, Object RptDataSet4)
        {
            var chk = (int[])RptDataSet4;
            //int[] chk = new int[2] { 0, 0 };

            var EmpInfoList = (List<HmsEntityManpower.HcmInfoTable>)RptDataSet;
            var EmpPhoto = (List<HmsEntityManpower.hcphoto>)RptDataSet2;

            var emEdu = EmpInfoList.FindAll(x => (x.actcode.Substring(0, 4) == "SIHE"));
            var emjob = EmpInfoList.FindAll(x => (x.actcode.Substring(0, 4) == "SIHJ"));
            var emPayroll = EmpInfoList.FindAll(x => (x.actcode.Substring(0, 7) == "SIHS001"));

            List<HmsEntityManpower.HcmGenInf01> GenInfoLst = new List<HmsEntityManpower.HcmGenInf01>();
            List<HmsEntityManpower.HcmEduInfo> EduInfoList = new List<HmsEntityManpower.HcmEduInfo>();
            List<HmsEntityManpower.HcmJobInfo> JobInfoList = new List<HmsEntityManpower.HcmJobInfo>();
            List<HmsEntityManpower.Payslip001> PayrollInfoList = new List<HmsEntityManpower.Payslip001>();

            GenInfoLst.Clear();
            EduInfoList.Clear();
            JobInfoList.Clear();
            PayrollInfoList.Clear();
           
            GenInfoLst.Add(new HmsEntityManpower.HcmGenInf01()
            {
                SIHI00101001 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101001")[0].dataval,
                SIHI00102001 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102001")[0].dataval,

                SIHI00101004 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101004")[0].dataval,
                SIHI00101005 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101005")[0].dataval,
                SIHI00101006 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101006")[0].dataval,
                SIHI00101007 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101007")[0].dataval,
                SIHI00101011 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101011")[0].dataval,
                SIHI00101012 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101012")[0].dataval,
                SIHI00101021 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101021")[0].dataval,
                SIHI00101022 = EmpInfoList.FindAll(x => x.actcode == "SIHI00101022")[0].dataval,
                SIHI00102011 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102011")[0].dataval,
                SIHI00102012 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102012")[0].dataval,
                SIHI00102021 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102021")[0].dataval,
                SIHI00102022 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102022")[0].dataval,
                SIHI00102023 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102023")[0].dataval,
                SIHI00102031 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102031")[0].dataval,
                SIHI00102032 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102032")[0].dataval,
                SIHI00102033 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102033")[0].dataval,
                SIHI00102041 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102041")[0].dataval,
                SIHI00102042 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102042")[0].dataval,
                SIHI00102043 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102043")[0].dataval,
                SIHI00102051 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102051")[0].dataval,
                SIHI00102052 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102052")[0].dataval,
                SIHI00102056 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102056")[0].dataval,
                SIHI00102058 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102058")[0].dataval,
                SIHI00102061 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102061")[0].dataval,
                SIHI00102062 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102062")[0].dataval,
                SIHI00102064 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102064")[0].dataval,
                SIHI00102066 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102066")[0].dataval,
                SIHI00102071 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102071")[0].dataval,
                SIHI00102072 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102072")[0].dataval,
                SIHI00102073 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102073")[0].dataval,
                SIHI00102076 = EmpInfoList.FindAll(x => x.actcode == "SIHI00102076")[0].dataval,
            });

            //education info
            var examnam = emEdu.FindAll(x => x.actcode.Substring(9, 3) == "001");
            var examinst = emEdu.FindAll(x => x.actcode.Substring(9, 3) == "002");
            var eduperiod = emEdu.FindAll(x => x.actcode.Substring(9, 3) == "003");
            var examyear = emEdu.FindAll(x => x.actcode.Substring(9, 3) == "004");
            var examresult = emEdu.FindAll(x => x.actcode.Substring(9, 3) == "005");
            var examrmrk = emEdu.FindAll(x => x.actcode.Substring(9, 3) == "006");

            if (chk[0] == 0)
            {
                for (int i = 0; i < examnam.Count; i++)
                {
                    EduInfoList.Add(new HmsEntityManpower.HcmEduInfo()
                    {
                        examnam = examnam[i].dataval,
                        examinst = examinst[i].dataval,
                        eduperiod = eduperiod[i].dataval,
                        examyear = examyear[i].dataval,
                        examresult = examresult[i].dataval,
                        examrmrk = examrmrk[i].dataval

                    });

                }
            }

            var jobcom = emjob.FindAll(x => x.actcode.Substring(9, 3) == "001");
            var jobdept = emjob.FindAll(x => x.actcode.Substring(9, 3) == "003");
            var jobdsg = emjob.FindAll(x => x.actcode.Substring(9, 3) == "002");
            var jobrmrks = emjob.FindAll(x => x.actcode.Substring(9, 3) == "007");
            var edate = emjob.FindAll(x => x.actcode.Substring(9, 3) == "005");
            var sdate = emjob.FindAll(x => x.actcode.Substring(9, 3) == "004");


            if (chk[1] == 0)
            {
                for (int i = 0; i < jobcom.Count; i++)
                {
                    JobInfoList.Add(new HmsEntityManpower.HcmJobInfo()
                    {
                        jobcom = jobcom[i].dataval,
                        jobdept = jobdept[i].dataval,
                        jobdsg = jobdsg[i].dataval,
                        jobrmrks = jobrmrks[i].dataval,
                        edate = emjob[i].dataval,
                        sdate = sdate[i].dataval
                    });
                }

            }


            try
            {
                PayrollInfoList.Add(new HmsEntityManpower.Payslip001()
                {

                    grosspay = decimal.Parse(emPayroll.Find(x => x.actcode == "SIHS00101099").dataval.ToString()),
                    saladd01 = decimal.Parse(emPayroll.Find(x => x.actcode == "SIHS00101002").dataval.ToString())
                });

            }
            catch (Exception)
            {
                             
            }                                   
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet", (List<HmsEntityManpower.HcmGenInf01>)GenInfoLst));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.HcmEduInfo>)EduInfoList));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet2", (List<HmsEntityManpower.HcmJobInfo>)JobInfoList));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet3", (List<HmsEntityManpower.Payslip001>)PayrollInfoList));
            // photos 
           
            string img1 = (EmpPhoto.Count >= 1) ? Convert.ToBase64String(EmpPhoto[0].photo) : "";
            string img2 = (EmpPhoto.Count >= 2) ? Convert.ToBase64String(EmpPhoto[1].photo) : "";
            string img3 = (EmpPhoto.Count >= 3) ? Convert.ToBase64String(EmpPhoto[2].photo) : "";

            Rpt1a.SetParameters(new ReportParameter("Userphoto", img1));
            Rpt1a.SetParameters(new ReportParameter("Signphoto", img2));
            Rpt1a.SetParameters(new ReportParameter("SubSignphoto", img3));

            return Rpt1a;
        }

        private static LocalReport SetHcmInfo_LeaveForm01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.HcmLeave01>)RptDataSet));
            //var list1 = (List<ASITHmsEntity.HmsEntityInventory.PurReqTransList>)RptDataSet;
            //var dt1 = list1.Min(x => x.reqdat);
            //var dt2 = list1.Max(x => x.reqdat);
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Purchase Requisition List (From " + dt1.ToString("dd-MMM-yyyy") + " To " + dt2.ToString("dd-MMM-yyyy") + ")"));

            return Rpt1a;
        }

        #endregion
        #region Payroll Information reports parameter settings
        

      
        private static LocalReport SetPayroll_MonthlySalarySheet01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.Payslip001>)RptDataSet));
            var list2 = (Hashtable)RptDataSet2;

            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list2["RptHead"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list2["RptPeriod"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetPayroll_MonthlySalarySheetBankLetter01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.Payslip001>)RptDataSet));
            var list2 = (Hashtable)RptDataSet2;

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;

            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list2["RptHead"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list2["RptPeriod"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamTkWord1", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("ParamLetID1", list3[0].RptParVal2));
            Rpt1a.SetParameters(new ReportParameter("ParamBankAc1", list3[0].RptParVal3));
            return Rpt1a;
        }


        private static LocalReport SetPayroll_AbstractMonthSalary01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.Payslip001>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetPayroll_AbstMonthDeducSal01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityManpower.Payslip001>)RptDataSet));
            return Rpt1a;
        }
        #endregion
    }
}
