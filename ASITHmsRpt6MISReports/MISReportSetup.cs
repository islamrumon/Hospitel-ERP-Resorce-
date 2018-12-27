using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Microsoft.Reporting.WinForms;
using ASITHmsEntity;

namespace ASITHmsRpt6MISReports
{
    public static class MISReportSetup
    {
        public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
            //Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt2Inventory.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("ASITHmsRpt6MISReports." + RptName + ".rdlc");
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
                case "Hospital.RptRefByBillSum01":
                case "Hospital.RptRefByBillSum02": Rpt1a = Hospital_RptRefByBillSum01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Hospital.RptRefByLedger01":
                case "Hospital.RptRefByLedger02": 
                    Rpt1a = Hospital_RptRefByLedger01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

            }
            Rpt1a.Refresh();
            return Rpt1a;
        }

  
        private static LocalReport Hospital_RptRefByBillSum01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityMISReports.MISHospital.RefByPerformance>)RptDataSet));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));

            return Rpt1a;
        }

        private static LocalReport Hospital_RptRefByLedger01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityMISReports.MISHospital.RefByLedger>)RptDataSet));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            Rpt1a.SetParameters(new ReportParameter("ParamToken1", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("ParamInWord1", list3[0].RptParVal2));

            return Rpt1a;
        }
    }
}
