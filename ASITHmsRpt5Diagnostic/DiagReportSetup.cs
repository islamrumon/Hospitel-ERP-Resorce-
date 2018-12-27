using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Microsoft.Reporting.WinForms;
using ASITHmsEntity;

namespace ASITHmsRpt5Diagnostic
{
    public static class DiagReportSetup
    {

        public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
            //Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt2Inventory.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("ASITHmsRpt5Diagnostic." + RptName + ".rdlc");
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
                case "Lab.RptLabDiag01": Rpt1a = DiagReport_RptLabDiag01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Lab.RptLab01": Rpt1a = DiagReport_RptLab01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
            }
            Rpt1a.Refresh();
            return Rpt1a;
        }

        private static LocalReport DiagReport_RptLabDiag01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var lst1 = (List<Object>)RptDataSet;
            var lstitem0 = (List<HmsEntityDiagnostic.DiagnosticReport>)lst1[0];
            var lstitem1 = (List<HmsEntityDiagnostic.DiagnosticReport>)lst1[1];
            var lstitem2 = (List<HmsEntityCommercial.CommInv01.CommInv01GenInf>)lst1[2];

            lstitem1 = lstitem1.FindAll(x => !x.elstyle.ToUpper().Contains("H"));  // Remove 'H' marked items when print

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", lstitem1));
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;

            //eldesc = (eldesc.Contains("[") ? eldesc.Substring(0, eldesc.IndexOf("[")).Trim() : eldesc);
            string eldesc1a = lstitem0.Find(x => x.elcode.Substring(0, 9) == "SILBRPT01").eldesc1.Trim();
            eldesc1a = (eldesc1a.Contains("[") ? eldesc1a.Substring(0, eldesc1a.IndexOf("[")).Trim() : eldesc1a);     // Remove '[.... ]' marked area from title

            string speciman1a = lstitem0.Find(x => x.elcode.Substring(0, 9) == "SILBRPT02").eldesc1.Trim();
            if (speciman1a.Length > 0)
            {
                if (lstitem0[0].sircode.Substring(0, 4) == "4502")
                    speciman1a = (lstitem0[0].sircode.Substring(0, 7) == "4502111" ? "Specimen : " : "Part Scanned : ") + speciman1a;
                else
                    speciman1a = (int.Parse(lstitem0[0].sircode.Substring(1, 4)) <= 1199 ? "Specimen : " : "Part Scanned : ") + speciman1a;
            }

            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", eldesc1a));
            Rpt1a.SetParameters(new ReportParameter("ParamSpecim1", speciman1a));
            Rpt1a.SetParameters(new ReportParameter("ParamMachine1", lstitem0.Find(x => x.elcode.Substring(0, 9) == "SILBRPT03").eldesc1.Trim()
                    + " " + lstitem0.Find(x => x.elcode.Substring(0, 9) == "SILBRPT03").eldesc2.Trim()));

            Rpt1a.SetParameters(new ReportParameter("Parm_branch", lstitem2[0].brnnam.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptinvnum2", lstitem2[0].ptinvnum2));

            Rpt1a.SetParameters(new ReportParameter("Parm_ptinvdat", lstitem2[0].ptinvdat.ToString("dd-MMM-yyyy hh:mm tt")));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptname", lstitem2[0].ptname.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptgender", lstitem2[0].ptgender.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptage", lstitem2[0].ptage.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_refcardno", (lstitem2[0].refcardno.Trim().Length > 0 ? "Member ID : " + lstitem2[0].refcardno.Trim() : "")));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptphone", (lstitem2[0].ptphone.Trim().Length < 5 ? "" : lstitem2[0].ptphone.Trim())));
            Rpt1a.SetParameters(new ReportParameter("Parm_delivartime", lstitem2[0].delivartime.ToString("dd-MMM-yyyy hh:mm tt")));// + " at " + lstitem0[0].delivartime.ToString("hh:mm tt")));

            string RefNam1 = (lstitem2[0].rfFullName.Trim().Length == 0 ? lstitem2[0].ptrefnote.Trim() : lstitem2[0].rfFullName.Trim());
            Rpt1a.SetParameters(new ReportParameter("Parm_rfFullName", RefNam1));

            Rpt1a.SetParameters(new ReportParameter("Parm_ptinvuser", "User : " + lstitem2[0].preparebynam.Trim()));

            //Rpt1a.SetParameters(new ReportParameter("ParamColHead1", "Test Description"));
            int reshead1 = 0, rescnt1 = 0, refcnt1 = 0, culture1 = 0;
            foreach (var item in lstitem1)
            {
                reshead1 += (item.eldesc1.Trim().Length > 0 && item.elresval.Trim().Length > 0 ? 1 : 0);
                rescnt1 += (item.elresval.Trim().Length > 0 ? 1 : 0);
                refcnt1 += (item.elrefval.Trim().Length > 0 ? 1 : 0);
                culture1 += (item.eldesc2.Trim().Length > 0 ? 1 : 0);
            }

            Rpt1a.SetParameters(new ReportParameter("ParamColHead1", (reshead1 > 0 && culture1 == 0 ? "Test Description" : "")));
            Rpt1a.SetParameters(new ReportParameter("ParamColHead2", (rescnt1 > 0 ? "Test Result" : "")));
            Rpt1a.SetParameters(new ReportParameter("ParamColHead3", (refcnt1 > 0 && culture1 == 0 ? "Reference Value" : "")));
            Rpt1a.SetParameters(new ReportParameter("ParamLabSeal1n", list3[0].RptParVal1.Trim()));
            Rpt1a.SetParameters(new ReportParameter("ParamLabSeal1d", list3[0].RptParVal2.Trim()));
            Rpt1a.SetParameters(new ReportParameter("ParamLabSeal2n", list3[0].RptParVal3.Trim()));
            Rpt1a.SetParameters(new ReportParameter("ParamLabSeal2d", list3[0].RptParVal4.Trim()));
            Rpt1a.SetParameters(new ReportParameter("ParamLabSeal3n", list3[0].RptParVal5.Trim()));
            Rpt1a.SetParameters(new ReportParameter("ParamLabSeal3d", list3[0].RptParVal6.Trim()));


            string img1 = (string)lst1[3];//  (lstitem4.Count >= 1) ? Convert.ToBase64String(lstitem4[0].ptphoto) : "";

            Rpt1a.SetParameters(new ReportParameter("Parm_ptphoto", img1));

            string ShowBarCode = "SHOW"; // "HIDE";//
            switch (lstitem2[0].comcod)
            {
                case "6527":
                    ShowBarCode = "HIDE";
                    break;
            }
            Rpt1a.SetParameters(new ReportParameter("Parm_ShowBarCode", ShowBarCode));

            return Rpt1a;
        }

        private static LocalReport DiagReport_RptLab01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet2", (List<HmsEntityDiagnostic.RptResultClass>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityDiagnostic.RptPatientInfo>)RptDataSet2));
            // var list1 = (List<HmsEntityCommercial.InvoiceTransList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            return Rpt1a;
        }

    }
}
