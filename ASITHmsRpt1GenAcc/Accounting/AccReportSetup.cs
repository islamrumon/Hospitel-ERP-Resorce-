
using ASITHmsEntity;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ASITFunLib;
using System.Collections;


namespace ASITHmsRpt1GenAcc.Accounting
{
    public static class AccReportSetup
    {
        public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset, object RptDataSet4)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
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
                case "Accounting.RptAccLedger1":
                case "Accounting.RptAccLedger2": Rpt1a = SetAccount_AccLedgerList1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccTrialBal1": Rpt1a = SetAccount_TrailBlncList1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccIncomeSt1": Rpt1a = SetAccount_RptAccIncomeSt1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccTransList": Rpt1a = SetAccount_TransactionList(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccTransList2": Rpt1a = SetAccount_TransactionList2(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccVou1":
                case "Accounting.RptAccVou1h": Rpt1a = SetAccount_Voucher1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;

                case "Accounting.RptAccPayCheq1": Rpt1a = SetAccount_PayCheq1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccMReceipt1": Rpt1a = SetAccount_MReceipt1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccRecon1": Rpt1a = SetAccount_RptAccRecon1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccRecPay1": Rpt1a = SetAccount_Recepayment(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccCashBook1": Rpt1a = SetAccount_CashBook(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccCashFlow1": Rpt1a = SetAccount_CashFlow(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptPayProTran1": Rpt1a = SetAccount_PayProTran1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptPayProTransList1": Rpt1a = SetAccount_RptPayProTransList1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptPayProTransList2": Rpt1a = SetAccount_RptPayProTransList2(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccIntComLon1": Rpt1a = SetAccount_RptAccIntComLon1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptAccIntComLon2": Rpt1a = SetAccount_RptAccIntComLon2(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Accounting.RptChqIssuLetter1": Rpt1a = SetAccount_RptChqIssuLetter1(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;

                case "Accounting.RptBgdProject01": Rpt1a = SetAccount_RptBgdProject01(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;

            }
            Rpt1a.Refresh();
            return Rpt1a;
        }

        private static LocalReport SetAccount_RptBgdProject01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            //Hashtable htbl1 = (Hashtable)RptDataSet2;
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.MasterBgdRpt1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetAccount_CashFlow(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var ds1 = (DataSet)RptDataSet;
            var List1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccCashFlow1>();
            DateTime FromDate = DateTime.Today.AddDays(-90);// lst1.Min(x => x.voudat);
            DateTime ToDate = DateTime.Today;   // lst1.Max(x => x.voudat);
            string RptTitle = "CASH FLOW STATEMENT" + " (From " + FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy") + ")" + "\n (This Report is Under Construction)";
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", RptTitle));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", List1));
            return Rpt1a;
        }

        private static LocalReport SetAccount_RptChqIssuLetter1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            Hashtable htbl1 = (Hashtable)RptDataSet2;
            //            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParmLetDate", htbl1["LetDate"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParmBankName", htbl1["BankName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParmBranchName", htbl1["BranchName"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParmAddress1", htbl1["Address1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParmAddress2", htbl1["Address2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParmBankAcNum", htbl1["BankAcNum"].ToString()));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccChequeIssueToBank1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetAccount_RptAccIntComLon1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParmTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParmPeriod1", list3[0].RptHeader2));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccIntComLoanStat1>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetAccount_RptAccIntComLon2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParmTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParmPeriod1", list3[0].RptHeader2));
            Rpt1a.SetParameters(new ReportParameter("ParmOpenDate1", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("ParmCloseDate1", list3[0].RptParVal2));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccIntComLoanSum1>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetAccount_RptAccIncomeSt1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst1 = (List<HmsEntityAccounting.AccIncomeStatement1t>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", lst1[0].RptTile));
            string Period1 = (lst1[0].RptTile.Contains("BALANCE") ? "As On\n" + lst1[0].currDate2.ToString("dd-MMM-yyyy") :
                    lst1[0].currDate1.ToString("dd-MMM-yyyy") + "\nTo\n" + lst1[0].currDate2.ToString("dd-MMM-yyyy"));
            string Period2 = (lst1[0].RptTile.Contains("BALANCE") ? "As On\n" + lst1[0].prevDate2.ToString("dd-MMM-yyyy") :
                    lst1[0].prevDate1.ToString("dd-MMM-yyyy") + "\nTo\n" + lst1[0].prevDate2.ToString("dd-MMM-yyyy"));
            Rpt1a.SetParameters(new ReportParameter("ParamCurrPeriod", Period1));
            Rpt1a.SetParameters(new ReportParameter("ParamPrevPeriod", Period2));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccIncomeStatement1>)RptDataSet));
            return Rpt1a;
        }


        private static LocalReport SetAccount_PayProTran1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst0 = (List<HmsEntityAccounting.PayProTrans1>)RptDataSet;
            var lst1 = (List<HmsEntityAccounting.PayProTransectionList>)RptDataSet2;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.PayProTrans1>)RptDataSet));
            string vt1 = lst1[0].bppnum.Substring(0, 3);
            // -- PBC (Payment Budget Cash), PBB (Payment Budget Bank), PBA (Payment Budget Any Source)
            string vTitle = "Payment Proposal " + (vt1 == "PBC" ? "Cash" : (vt1 == "PBB" ? "Bank" : "Any Source"));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", vTitle));
            Rpt1a.SetParameters(new ReportParameter("bppnum1a", lst1[0].bppnum1));
            Rpt1a.SetParameters(new ReportParameter("bppdat1a", lst1[0].bppdat1));
            Rpt1a.SetParameters(new ReportParameter("bppnar1a", lst1[0].bppnar.Trim()));
            Rpt1a.SetParameters(new ReportParameter("bppref1a", lst1[0].bppref));
            Rpt1a.SetParameters(new ReportParameter("ScrollNo", lst1[0].rowid.ToString().Trim()));
            var ExtP1 = (HmsEntityAccounting.AccVoucher1p)RptDataSet4;
            Rpt1a.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(ExtP1.comlogo)));
            Rpt1a.SetParameters(new ReportParameter("inword1a", ExtP1.inWord));

            Rpt1a.SetParameters(new ReportParameter("Totbppam", lst0[0].bppam.ToString("#,##0;(#,##0); - ")));
            Rpt1a.SetParameters(new ReportParameter("Totbapam", lst0[0].bapam.ToString("#,##0;(#,##0); - ")));
            Rpt1a.SetParameters(new ReportParameter("Totdiffam", lst0[0].diffam.ToString("#,##0;(#,##0); - ")));
            return Rpt1a;
        }

        private static LocalReport SetAccount_RptPayProTransList1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst1 = (List<HmsEntityAccounting.PayProTransectionList>)RptDataSet;
            //DateTime FromDate = lst1.Min(x => x.bppdat);
            //DateTime ToDate = lst1.Max(x => x.bppdat);
            string DateRange = "(For the month of " + lst1[0].bppdat.ToString("MMMM-yyyy") + ")";// "(From " + FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy") + " )";
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", "Payment Proposal Transaction List"));
            Rpt1a.SetParameters(new ReportParameter("DateRange", DateRange));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.PayProTransectionList>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetAccount_RptPayProTransList2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst1 = (List<HmsEntityAccounting.PayProTransectionList2>)RptDataSet;
            //DateTime FromDate = lst1.Min(x => x.bppdat);
            //DateTime ToDate = lst1.Max(x => x.bppdat);
            string DateRange = "(For the month of " + lst1[0].bppdat.ToString("MMMM-yyyy") + ")";// "(From " + FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy") + " )";
            string Title1 = (string)RptDataSet4;

            Rpt1a.SetParameters(new ReportParameter("ParamTitle", Title1)); // "Payment Proposal Details Transaction List"
            Rpt1a.SetParameters(new ReportParameter("DateRange", DateRange));

            var lst0 = lst1.FindAll(x => x.sectcod == "000000000000" && x.actcode == "000000000000").ToList();

            Rpt1a.SetParameters(new ReportParameter("Totbppam", lst0.Sum(x => x.bppam).ToString("#,##0;(#,##0); - ")));
            Rpt1a.SetParameters(new ReportParameter("Totbapam", lst0.Sum(x => x.bapam).ToString("#,##0;(#,##0); - ")));
            Rpt1a.SetParameters(new ReportParameter("TotPayam", lst0.Sum(x => x.payam).ToString("#,##0;(#,##0); - ")));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.PayProTransectionList2>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetAccount_RptAccRecon1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var Params = (Hashtable)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", Params["Title1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", Params["Title2"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamAsOnDate", Params["AsOnDate"].ToString()));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccCashBanRecon1>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetAccount_Recepayment(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var ds1 = (DataSet)RptDataSet;
            var List1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccRecPay1>();
            var grpA = List1.FindAll(x => x.grp0 == "A" && x.slno1 != 0);
            var grpAT = List1.FindAll(x => x.grp0 == "A" && x.slno1 == 0);
            var grpFT = List1.FindAll(x => x.grp0 == "F");

            var grpB = List1.FindAll(x => x.grp0 == "B");
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccRecPay1>)grpA));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1t", (List<HmsEntityAccounting.AccRecPay1>)grpAT));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1ft", (List<HmsEntityAccounting.AccRecPay1>)grpFT));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet2", (List<HmsEntityAccounting.AccRecPay1>)grpB));
            Rpt1a.SetParameters(new ReportParameter("ParamNameHead", ds1.Tables[1].Rows[0]["reportnam"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("TrTyp", ds1.Tables[1].Rows[0]["ProcID"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamHeadDate", "(For The Period From " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["FromDate"]).ToString("dd-MMM-yyyy") +
                                               " To " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["ToDate"]).ToString("dd-MMM-yyyy") + ")"));
            if (Convert.ToDateTime(ds1.Tables[1].Rows[0]["FromDate"]) == Convert.ToDateTime(ds1.Tables[1].Rows[0]["ToDate"]))
                Rpt1a.SetParameters(new ReportParameter("ParamHeadDate", "(For The Date of " + Convert.ToDateTime(ds1.Tables[1].Rows[0]["FromDate"]).ToString("dd-MMM-yyyy ddd") + ")"));

            Rpt1a.SetParameters(new ReportParameter("ParmTable1Head1", ds1.Tables[1].Rows[0]["Table1Head1"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParmTable1Head2", ds1.Tables[1].Rows[0]["Table1Head2"].ToString()));


            return Rpt1a;
        }

        private static LocalReport SetAccount_CashBook(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {

            var lst1 = (List<HmsEntityAccounting.AccCashBook1>)RptDataSet;
            DateTime FromDate = lst1.Min(x => x.voudat);
            DateTime ToDate = lst1.Max(x => x.voudat);
            string drange1 = (FromDate == ToDate ? " (For The Date of " + FromDate.ToString("dd-MMM-yyyy ddd") + ")" :
                " (From " + FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy") + ")");
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", list3[0].RptHeader1.Trim() + drange1));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", lst1));
            return Rpt1a;
        }


        private static LocalReport SetAccount_AccLedgerList1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccLedger1>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet2", (List<HmsEntityAccounting.AccLedger1A>)RptDataSet2));
            return Rpt1a;
        }

        private static LocalReport SetAccount_TrailBlncList1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccTrialBalance1>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet2", (List<HmsEntityAccounting.AccTrialBalance1t>)RptDataSet2));
            var lst4 = (Hashtable)RptDataSet4;
            Rpt1a.SetParameters(new ReportParameter("ToDate", lst4["ToDate"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("fromDatep", lst4["fromDatep"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("fromDate", lst4["fromDate"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", lst4["RptTitle"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamPeriod", lst4["Period"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ReportType", lst4["ReportType"].ToString()));
            return Rpt1a;
        }

        private static LocalReport SetAccount_TransactionList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst1 = (List<HmsEntityAccounting.AccTransectionList>)RptDataSet;
            DateTime FromDate = lst1.Min(x => x.voudat);
            DateTime ToDate = lst1.Max(x => x.voudat);
            string DateRange = (FromDate == ToDate ? "( For the Date of " + FromDate.ToString("dd-MMM-yyyy") : "( From " + FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy")) + " )";
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", "Accounts Transaction Vouchers List"));
            Rpt1a.SetParameters(new ReportParameter("DateRange", DateRange));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccTransectionList>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetAccount_TransactionList2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst1 = (List<HmsEntityAccounting.AccLedger1>)RptDataSet;
            DateTime FromDate = lst1.Min(x => x.voudat);
            DateTime ToDate = lst1.Max(x => x.voudat);
            Hashtable list4 = (Hashtable)RptDataSet4;

            string DateRange = (FromDate == ToDate ? "( For the Date of " + FromDate.ToString("dd-MMM-yyyy") : "( From " + FromDate.ToString("dd-MMM-yyyy") + " To " + ToDate.ToString("dd-MMM-yyyy")) + " )";
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle", "Accounts Transaction List"));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", list4["Title"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("DateRange", DateRange));

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccLedger1>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetAccount_Voucher1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst1 = (List<HmsEntityAccounting.AccTransectionList>)RptDataSet2;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccVoucher1>)RptDataSet));
            string vt1 = lst1[0].vounum.Substring(0, 2);
            //string vTitle = (vt1 == "CC" || vt1 == "BC" ? "Deposit" : (vt1 == "CD" || vt1 == "BD" ? "Payment" : (vt1 == "FT" ? "Fund Transfer" :  (vt1 == "OP" ? "Opening" : "Journal")))) + " Voucher";
            string vTitle = (vt1 == "RV" ? "Receipt" : (vt1 == "PV" ? "Payment" : (vt1 == "FT" ? "Fund Transfer" : (vt1 == "OP" ? "Opening" : "Journal")))) + " Voucher";
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", vTitle));
            Rpt1a.SetParameters(new ReportParameter("vounum1a", lst1[0].vounum1));
            Rpt1a.SetParameters(new ReportParameter("voudat1a", lst1[0].voudat1));
            Rpt1a.SetParameters(new ReportParameter("vounar1a", lst1[0].vounar.Trim()));
            Rpt1a.SetParameters(new ReportParameter("advref1a", lst1[0].advref));
            Rpt1a.SetParameters(new ReportParameter("chqref1a", lst1[0].chqref));
            Rpt1a.SetParameters(new ReportParameter("vouref1a", lst1[0].vouref));
            Rpt1a.SetParameters(new ReportParameter("ScrollNo", lst1[0].rowid.ToString().Trim()));
            //Rpt1a.SetParameters(new ReportParameter("tcramt", cramt.ToString("#,##0.00; ")));
            var ExtP1 = (HmsEntityAccounting.AccVoucher1p)RptDataSet4;
            Rpt1a.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(ExtP1.comlogo)));
            Rpt1a.SetParameters(new ReportParameter("inword1a", ExtP1.inWord));
            //Rpt1a.SetParameters(new ReportParameter("voudat1a", lst1[0].voudat1));
            return Rpt1a;
        }

        private static LocalReport SetAccount_PayCheq1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst1 = (List<HmsEntityAccounting.AccTransectionList>)RptDataSet2;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccVoucher1>)RptDataSet));
            string vt1 = lst1[0].vounum.Substring(0, 2);
            //string vTitle = (vt1 == "CC" || vt1 == "BC" ? "Deposit" : (vt1 == "CD" || vt1 == "BD" ? "Payment" : (vt1 == "FT" ? "Fund Transfer" :  (vt1 == "OP" ? "Opening" : "Journal")))) + " Voucher";
            string vTitle = (vt1 == "PV" ? "Payment Cheque" : (vt1 == "FT" ? "Transfer  Cheque" : ""));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", vTitle));
            Rpt1a.SetParameters(new ReportParameter("vounum1a", lst1[0].vounum1));
            Rpt1a.SetParameters(new ReportParameter("voudat1a", lst1[0].voudat1));
            Rpt1a.SetParameters(new ReportParameter("vounar1a", lst1[0].vounar.Trim()));
            Rpt1a.SetParameters(new ReportParameter("advref1a", lst1[0].advref));
            Rpt1a.SetParameters(new ReportParameter("chqref1a", lst1[0].chqref));
            Rpt1a.SetParameters(new ReportParameter("vouref1a", lst1[0].vouref));
            Rpt1a.SetParameters(new ReportParameter("ScrollNo", lst1[0].rowid.ToString().Trim()));
            //Rpt1a.SetParameters(new ReportParameter("tcramt", cramt.ToString("#,##0.00; ")));
            var ExtP1 = (HmsEntityAccounting.AccVoucher1p)RptDataSet4;
            Rpt1a.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(ExtP1.comlogo)));
            Rpt1a.SetParameters(new ReportParameter("inword1a", ExtP1.inWord));
            //Rpt1a.SetParameters(new ReportParameter("voudat1a", lst1[0].voudat1));
            return Rpt1a;
        }

        private static LocalReport SetAccount_MReceipt1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var lst0 = ((List<HmsEntityAccounting.AccVoucher1>)RptDataSet).FindAll(x => x.cactcode == "000000000000" && x.sircode == "000000000000").ToList();
            var lst2 = ((List<HmsEntityAccounting.AccVoucher1>)RptDataSet).FindAll(x => x.cactcode == "000000000000" && x.sircode != "000000000000").ToList();
            var lst1 = (List<HmsEntityAccounting.AccTransectionList>)RptDataSet2;

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityAccounting.AccVoucher1>)RptDataSet));
            string vt1 = lst1[0].vounum.Substring(0, 2);
            string vTitle = (vt1 == "RV" ? "Money Receipt" : "");
            Rpt1a.SetParameters(new ReportParameter("ParamTitle", vTitle));
            Rpt1a.SetParameters(new ReportParameter("vounum1a", lst1[0].vounum1));
            Rpt1a.SetParameters(new ReportParameter("voudat1a", lst1[0].voudat1));
            Rpt1a.SetParameters(new ReportParameter("vounar1a", lst1[0].vounar.Trim()));
            Rpt1a.SetParameters(new ReportParameter("advref1a", lst1[0].advref));
            Rpt1a.SetParameters(new ReportParameter("chqref1a", lst1[0].chqref));
            Rpt1a.SetParameters(new ReportParameter("vouref1a", lst1[0].vouref));
            Rpt1a.SetParameters(new ReportParameter("ScrollNo", lst1[0].rowid.ToString().Trim()));
            Rpt1a.SetParameters(new ReportParameter("ParmAccCode", lst0[0].actcode));
            Rpt1a.SetParameters(new ReportParameter("ParmAccHead", lst0[0].trnDesc.Trim()));
            if (lst2.Count > 0)
            {
                Rpt1a.SetParameters(new ReportParameter("ParmClientCode", lst2[0].sircode));
                Rpt1a.SetParameters(new ReportParameter("ParmClientName", lst2[0].trnDesc.Trim().ToUpper().Replace("CUST. -", "")));
            }
            else
            {
                Rpt1a.SetParameters(new ReportParameter("ParmClientCode", lst0[0].actcode));
                Rpt1a.SetParameters(new ReportParameter("ParmClientName", lst0[0].trnDesc.Trim()));
            }

            Rpt1a.SetParameters(new ReportParameter("ParmAmount", lst0.Sum(x => x.cramt).ToString("#,##0.00")));

            var ExtP1 = (HmsEntityAccounting.AccVoucher1p)RptDataSet4;
            Rpt1a.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(ExtP1.comlogo)));
            Rpt1a.SetParameters(new ReportParameter("inword1a", ExtP1.inWord));
            return Rpt1a;
        }
    }
}
