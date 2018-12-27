using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using Microsoft.Reporting.WinForms;
using ASITHmsEntity;
using ASITFunLib;
namespace ASITHmsRpt4Commercial
{
    public static class CommReportSetup
    {
        #region GetLocalReport: Loading and receving report resources
        public static LocalReport GetLocalReport(string RptName = "XYZ", Object RptDataSet = null, Object RptDataSet2 = null, Object UserDataset = null, Object RptDataSet4 = null)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
            //Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt2Inventory.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("ASITHmsRpt4Commercial." + RptName + ".rdlc");
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
                case "Hospital.CommInv01": Rpt1a = SetInv_ComInv01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Hospital.RptCommInvList1":
                case "Hospital.RptCollDuesSum01": Rpt1a = SetInv_CommInvList1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Hospital.RptGroupWiseTrans1": Rpt1a = SetInv_GroupWiseTrans1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Hospital.RptCCStatus01": Rpt1a = SetInv_RptCCStatus01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Hospital.RptCollectionSum01": Rpt1a = SetInv_RptCollectionSum01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Hospital.RptDiscountList01": Rpt1a = SetInv_RptDiscountList01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Hospital.RptSalesSum01": Rpt1a = SetInv_RptSalesSum01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Hospital.RptReportingBill01": Rpt1a = SetInv_RptReportingBill01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Doctor.DocToken01": Rpt1a = SetInv_DoctorToken01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Doctor.DocToken01List": Rpt1a = SetInv_DocToken01List(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Pharmacy.PhSalesInv01": Rpt1a = SetInv_PhSalesInv01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RetSales.RetSalesInv01":
                case "RetSales.RetSalesChallan01": Rpt1a = SetInv_RetSalesInv01(Rpt1a, RptDataSet, RptDataSet2, UserDataset, RptDataSet4); break;
                case "Pharmacy.PhSalesTransList1":
                case "RetSales.RetSalesTransList1":
                case "Pharmacy.PhSalesTransList1s": Rpt1a = SetInv_SalesInvTransList01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RetSales.RetSalesTransList2": Rpt1a = SetInv_RetSalesTransList02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RetSales.RetSalesCashRecv1": Rpt1a = SetInv_RetSalesCashRecv01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "RetSales.RetSalesDetailsList1":
                case "Pharmacy.PhSalesDetailsList1":
                case "Pharmacy.PhSalesDetailsList1s": Rpt1a = SetInv_PhSalesDetailsList1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Pharmacy.PhInvCollList01":
                case "Pharmacy.PhInvCollList01s": Rpt1a = SetInv_PhInvCollList01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Pharmacy.PhDueList01":
                case "Pharmacy.PhDueList01s": Rpt1a = SetInv_PhDueList01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "ParkSales.ParkTicket01": Rpt1a = SetInv_ParkTicket01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "ParkSales.RptParkSalesList01":
                case "ParkSales.RptParkSalesSum01": Rpt1a = SetInv_RptParkSalesTrans01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

            }
            Rpt1a.Refresh();
            return Rpt1a;
        }
        #endregion


        #region Commercial Billing Reports - Hospital
        private static LocalReport SetInv_ComInv01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var lst1 = (List<Object>)RptDataSet;
            var lstitem0 = (List<HmsEntityCommercial.CommInv01.CommInv01TblItem>)lst1[0];
            var lstitem1 = (List<HmsEntityCommercial.CommInv01.CommInv01GenInf>)lst1[1];
            var lstitem2 = (List<HmsEntityCommercial.CommInv01.CommInv01TblSum>)lst1[2];
            var lstitem3 = (List<HmsEntityCommercial.CommInv01.CommInv01TblCol>)lst1[3];
            foreach (var item in lstitem0)
            {
                if (item.itemrmrk.Trim().Length > 0)
                    item.isirdesc = item.isirdesc.Trim() + ", " + item.itemrmrk.Trim();
            }

            /*
                var list1a = ds1.Tables[0].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01TblItem>();
            var list1b = ds1.Tables[1].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01GenInf>();
            var list1c = ds1.Tables[2].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01TblSum>();
            var list1d = ds1.Tables[3].DataTableToList<HmsEntityCommercial.CommInv01.CommInv01TblCol>();
             
             */

            ////int cnt1 = lstitem1.Count;
            ////int cnt2 = (cnt1 < 14 ? 14 : (cnt1 < 28 ? 28 : (cnt1 < 42 ? 42 : (cnt1 < 56 ? 56 : cnt1))));
            ////for (int k = cnt1; k < cnt2; k++)
            ////{
            ////    lstitem1.Add(new HmsEntityCommercial.CommInv01.CommInv01TblItem()
            ////    {
            ////        comcod = "", gsircode = "", gsirdesc = "", icdisam = 0, icomam = 0, idisam = 0, inetam = 0, isircode = "", isirdesc = "",
            ////        isirtype = "", isirunit = "", itemqty = 0, itmam = 0, itmrate = 0, ivatam = 0, ptinvnum = "", refscomp = 0, rowid = 0, slnum = 0
            ////    });
            ////}


            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", lstitem0));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1s", lstitem2));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet2", lstitem3));

            Rpt1a.SetParameters(new ReportParameter("Parm_branch", lstitem1[0].brnnam.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptinvnum2", lstitem1[0].ptinvnum2));
            //Rpt1a.SetParameters(new ReportParameter("Parm_ptinvdat", lstitem1[0].ptinvdat.ToString("dd-MMM-yyyy") + " " + lstitem1[0].rowtime.ToString("hh:mm tt")));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptinvdat", lstitem1[0].ptinvdat.ToString("dd-MMM-yyyy hh:mm tt")));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptname", lstitem1[0].ptname.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptgender", lstitem1[0].ptgender.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptage", lstitem1[0].ptage.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_refcardno", (lstitem1[0].refcardno.Trim().Length > 0 ? "Member ID : " + lstitem1[0].refcardno.Trim() : "")));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptphone", (lstitem1[0].ptphone.Trim().Length<5 ? "" : lstitem1[0].ptphone.Trim())));
            Rpt1a.SetParameters(new ReportParameter("Parm_delivartime", lstitem1[0].delivartime.ToString("dd-MMM-yyyy hh:mm tt")));// + " at " + lstitem0[0].delivartime.ToString("hh:mm tt")));

            string RefNam1 = (lstitem1[0].rfFullName.Trim().Length == 0 ? lstitem1[0].ptrefnote.Trim() : lstitem1[0].rfFullName.Trim());
            Rpt1a.SetParameters(new ReportParameter("Parm_rfFullName", RefNam1));
            Rpt1a.SetParameters(new ReportParameter("ParmTotalInWord", "Inword: " + ASITUtility.Trans(Convert.ToDouble(lstitem2[0].SumAmt), 2)));
            Rpt1a.SetParameters(new ReportParameter("Parm_dueam", lstitem1[0].dueam.ToString()));
            string invNote1 = lstitem1[0].ptinvnote.Trim();
            if (lstitem1[0].refstaffid != "000000000000")
                invNote1 = invNote1 + (invNote1.Length > 0 ? "\n" : "") + "[OFFICIAL REF. OF  " + lstitem1[0].refstaffnam.Trim() + "]";

            Rpt1a.SetParameters(new ReportParameter("Parm_ptinvnote", invNote1));
            Rpt1a.SetParameters(new ReportParameter("Parm_ptinvuser", "User : " + lstitem1[0].preparebynam.Trim()));
            Rpt1a.SetParameters(new ReportParameter("Parm_DiscType", lstitem2.Find(x => x.SumHead.Contains("Payable")).SumAmt == 0 ? "100 % FREE" : ""));
            string ShowBarCode = "SHOW"; // "HIDE";//

            switch (lstitem1[0].comcod)
            {
                case "6527":
                    ShowBarCode = "HIDE";
                    break;
            }
            Rpt1a.SetParameters(new ReportParameter("Parm_ShowBarCode", ShowBarCode));
            //Parm_ptinvnote

            string img1 = (string)lst1[4];//  (lstitem4.Count >= 1) ? Convert.ToBase64String(lstitem4[0].ptphoto) : "";


            Rpt1a.SetParameters(new ReportParameter("Parm_ptphoto", img1));

            //Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.DoctorToken01>)RptDataSet));
            return Rpt1a;
        }

        private static LocalReport SetInv_CommInvList1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.CommInvSummInf>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }
        private static LocalReport SetInv_GroupWiseTrans1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.GroupWiseTrans01>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }

        private static LocalReport SetInv_RptReportingBill01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.ReportingBill01>)RptDataSet));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));

            return Rpt1a;
        }
        private static LocalReport SetInv_RptCCStatus01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.FDeskCollSumm01>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }
        private static LocalReport SetInv_RptCollectionSum01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.FDeskCollSumm01>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }
        private static LocalReport SetInv_RptDiscountList01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.FDeskDiscount01>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }

        private static LocalReport SetInv_RptSalesSum01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.FDeskSalesSumm01>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }

        private static LocalReport SetInv_DoctorToken01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.DoctorToken01>)RptDataSet));
            return Rpt1a;
        }
        private static LocalReport SetInv_DocToken01List(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.DoctorToken01>)RptDataSet));
            return Rpt1a;
        }




        #endregion Commercial Billing Reports - Hospital

        #region Commercial Reports - General Trading
        private static LocalReport SetInv_RetSalesInv01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset, object RptDataSet4)
        {
            var list0 = ((List<HmsEntityCommercial.PhSalesInvoice01>)RptDataSet);

            List<HmsEntityCommercial.PhSalesInvoice01Sum> list0a = new List<HmsEntityCommercial.PhSalesInvoice01Sum>();
            List<HmsEntityCommercial.PhSalesInvoice01Sum> list0d = new List<HmsEntityCommercial.PhSalesInvoice01Sum>();
            var list1 = (List<HmsEntityCommercial.InvoiceTransList>)RptDataSet2;

            var LstSum1 = list0.FindAll(x => x.rsircode.Substring(0, 2) == "01");
            decimal AmtSum1 = 0.00m, DisSum1 = 0.00m, VatSum1 = 0.00m;
            foreach (var item in LstSum1)
            {
                AmtSum1 = AmtSum1 + item.itmam;
                DisSum1 = DisSum1 + item.idisam;
                VatSum1 = VatSum1 + item.ivatam;
            }
            AmtSum1 = Math.Round(AmtSum1, 0);
            DisSum1 = Math.Round(DisSum1, 0);



            if (AmtSum1 > 0)
                list0a.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Total Amount :", sumamt = AmtSum1 });

            var Lstcarr1 = list0.FindAll(x => x.rsircode.Substring(0, 2) == "04" && x.sirdesc.Trim().ToUpper().Contains("CARRI"));
            if (Lstcarr1.Count > 0)
                list0a.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Carring Charge (+) :", sumamt = Lstcarr1[0].inetam });

            var Lstlab1 = list0.FindAll(x => x.rsircode.Substring(0, 2) == "04" && x.sirdesc.Trim().ToUpper().Contains("LABO"));
            if (Lstlab1.Count > 0)
                list0a.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Labour Charge (+) :", sumamt = Lstlab1[0].inetam });

            if (DisSum1 > 0)
                list0a.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Discount (-) :", sumamt = DisSum1 });

            if (VatSum1 > 0)
                list0a.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "VAT (+) :", sumamt = VatSum1 });

            AmtSum1 = 0.00m;
            foreach (var item in list0a)
            {
                AmtSum1 = AmtSum1 + item.sumamt * (item.sumhead.Contains("Discount") ? -1 : 1);
            }
            Hashtable list4 = (Hashtable)RptDataSet4;

            // Invoice Optional Outputs to be re-organized based on list4["memoType"]  -- Hafiz 10-Jun-2017
            //list4["memoType"] = memoType1; "INV01", "INV02", "INV00"

            decimal PrevBal1 = Convert.ToDecimal(list4["prevbal"]) - AmtSum1 + list1[0].collam;


            list0a.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Total Bill Amount :", sumamt = AmtSum1 });
            list0d.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Grand Total :", sumamt = AmtSum1 });
            list0d.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Paid Amount :", sumamt = list1[0].collam });
            list0d.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Current Dues :", sumamt = list1[0].dueam });

            if (list4["memoType"].ToString().Trim() != "INV02")
            {
                list0d.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Previous Dues :", sumamt = PrevBal1 });
                list0d.Add(new HmsEntityCommercial.PhSalesInvoice01Sum() { sumhead = "Total Dues :", sumamt = PrevBal1 + list1[0].dueam });
            }
            var list0b = list0.FindAll(x => x.rsircode.Substring(0, 2) == "04");

            list0 = list0.FindAll(x => x.rsircode.Substring(0, 2) == "01");
            //            if (list0.Count < 10)
            {
                for (int i = list0.Count; i <= 15; i++)
                {
                    list0.Add(new HmsEntityCommercial.PhSalesInvoice01()
                    {
                        slnum = 0,
                        comcod = "",
                        idisam = 0,
                        inetam = 0,
                        invno = "",
                        invqty = 0,
                        invrmrk = "",
                        itmam = 0,
                        itmrat = 0,
                        ivatam = 0,
                        rsircode = "",
                        sirdesc = "",
                        sirunit = ""
                    });
                }
            }

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", list0));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1s", list0a));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1d", list0d));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamAddress1", list3[0].RptCompAdd1));
            Rpt1a.SetParameters(new ReportParameter("ParamAddress2", list3[0].RptCompAdd3));

            if (list4["memoType"].ToString().Trim() != "INV00")
            {
                Rpt1a.SetParameters(new ReportParameter("ParmCompNam1", ""));
                Rpt1a.SetParameters(new ReportParameter("ParamAddress1", ""));
                Rpt1a.SetParameters(new ReportParameter("ParamAddress2", ""));
            }

            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Sales Invoice"));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list4["memoTitle"].ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamMemoNum1", list1[0].invno1));
            Rpt1a.SetParameters(new ReportParameter("ParamDate1", list1[0].invdat1));
            Rpt1a.SetParameters(new ReportParameter("ParamRcvAmt", list1[0].collam.ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamDueAmt", list1[0].dueam.ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamReferance", list1[0].invbyName));
            Rpt1a.SetParameters(new ReportParameter("ParamNarration", list1[0].invnar));
            Rpt1a.SetParameters(new ReportParameter("ParamToken1", list1[0].custid.Substring(6, 6)));
            Rpt1a.SetParameters(new ReportParameter("ParamPayType1", list1[0].paytype + (list1[0].invref.Trim().Length > 0 ? ", " + list1[0].invref.Trim() : "")));
            //Rpt1a.SetParameters(new ReportParameter("ParamPayType1", (list1[0].collam == 0 ? "100 % Credit" : list1[0].paytype "")));


            // Rpt1a.SetParameters(new ReportParameter("ParamCustName", list1[0].custid.Substring(6, 6) + " " + list1[0].custName.Trim()));

            Rpt1a.SetParameters(new ReportParameter("ParamCustName", list4["cuatnam"].ToString().Trim()));
            Rpt1a.SetParameters(new ReportParameter("ParamCustAdd1", list4["cuatAdd"].ToString().Trim()));
            Rpt1a.SetParameters(new ReportParameter("ParamCustAdd2", list4["cuatTel"].ToString().Trim()));

            Rpt1a.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String((byte[])list4["comlogo"])));
            Rpt1a.SetParameters(new ReportParameter("inword1a", list4["inWord"].ToString()));

            return Rpt1a;
        }

        private static LocalReport SetInv_PhDueList01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.InvDuesList01>)RptDataSet));
            // var list1 = (List<HmsEntityCommercial.InvoiceTransList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }

        private static LocalReport SetInv_SalesInvTransList01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.InvoiceTransList>)RptDataSet));
            // var list1 = (List<HmsEntityCommercial.InvoiceTransList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }

        private static LocalReport SetInv_RetSalesTransList02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.RetSalesTransList2>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }

        private static LocalReport SetInv_RetSalesCashRecv01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.RetSalesCashRecv1>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;

        }

        private static LocalReport SetInv_PhInvCollList01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.InvColList01>)RptDataSet));
            // var list1 = (List<HmsEntityCommercial.InvoiceTransList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            return Rpt1a;
        }

        private static LocalReport SetInv_PhSalesDetailsList1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            List<HmsEntityCommercial.InvoiceTransList2> list0 = (List<HmsEntityCommercial.InvoiceTransList2>)RptDataSet;

            string Sales1 = list0.FindAll(x => x.comcod != "AAAA").Sum(y => y.itmam).ToString("#,##0;(#,##0); - ");
            string Disc1 = list0.FindAll(x => x.comcod != "AAAA").Sum(y => y.idisam).ToString("#,##0;(#,##0); - ");
            string Vatam1 = list0.FindAll(x => x.comcod != "AAAA").Sum(y => y.ivatam).ToString("#,##0;(#,##0); - ");
            string SalVat1 = list0.FindAll(x => x.comcod != "AAAA").Sum(y => y.inetam + y.ivatam).ToString("#,##0;(#,##0); - ");

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.InvoiceTransList2>)RptDataSet));
            // var list1 = (List<HmsEntityCommercial.InvoiceTransList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", list3[0].RptHeader2));
            Rpt1a.SetParameters(new ReportParameter("ParamTopSum1", list3[0].RptParVal1));

            Rpt1a.SetParameters(new ReportParameter("ParamSalesAmt1", Sales1));
            Rpt1a.SetParameters(new ReportParameter("ParamDiscAmt1", Disc1));
            Rpt1a.SetParameters(new ReportParameter("ParamVATAmt1", Vatam1));
            Rpt1a.SetParameters(new ReportParameter("ParamSalesVat1", SalVat1));
            return Rpt1a;
        }

        private static LocalReport SetInv_PhSalesInv01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.PhSalesInvoice01>)RptDataSet));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamAddress1", list3[0].RptCompAdd1));
            Rpt1a.SetParameters(new ReportParameter("ParamAddress2", list3[0].RptCompAdd3));
            Rpt1a.SetParameters(new ReportParameter("ParamVATReg1", list3[0].RptHeader1));
            var list1 = (List<HmsEntityCommercial.InvoiceTransList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Sales Invoice"));
            Rpt1a.SetParameters(new ReportParameter("ParamMemoNum1", list1[0].invno1));
            Rpt1a.SetParameters(new ReportParameter("ParamDate1", list1[0].invdat1));
            Rpt1a.SetParameters(new ReportParameter("ParamRcvAmt", list1[0].collam.ToString()));
            Rpt1a.SetParameters(new ReportParameter("ParamDueAmt", list1[0].dueam.ToString("0")));
            Rpt1a.SetParameters(new ReportParameter("ParamReferance", list1[0].invref));
            Rpt1a.SetParameters(new ReportParameter("ParamNarration", list1[0].invnar));
            Rpt1a.SetParameters(new ReportParameter("ParamToken1", list1[0].slnum.ToString()));

            return Rpt1a;
        }
        #endregion Commercial Reports - General Trading

        #region Park Operation System
        private static LocalReport SetInv_ParkTicket01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.ParkTicketCoupon01>)RptDataSet));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamVATReg1", list3[0].RptHeader1));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Park Ticket / Coupon"));
            Rpt1a.SetParameters(new ReportParameter("ParamParkName1", list3[0].RptParVal1));
            
            return Rpt1a;
        }



        private static LocalReport SetInv_RptParkSalesTrans01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityCommercial.ParkSalesTrans01>)RptDataSet));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", list3[0].RptHeader1));
            return Rpt1a;
        }
        #endregion Park Operation System
    }
}
