using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.Reporting.WinForms;
using ASITHmsEntity;
using System.Threading.Tasks;

namespace ASITHmsRpt2Inventory
{
    public static class StoreReportSetup
    {

        #region GetLocalReport: Loading and receving report resources
        public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
            //Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt2Inventory.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("ASITHmsRpt2Inventory." + RptName + ".rdlc");
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
                case "Store.RptTransectionList": Rpt1a = SetInvStore_RptTransectionList(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptPurReqMemo01": Rpt1a = SetInvStore_PurReqMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptStoreReqMemo01": Rpt1a = SetInvStore_StoreReqMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptIssueMemo01": Rpt1a = SetInvStore_StoreIssueMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptMRRMemo01": Rpt1a = SetInvStore_RptMRRMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Procurement.PurQuotMemo01": Rpt1a = SetInvPur_QuotMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Procurement.RateFixMemo01": Rpt1a = SetInvPur_RateFixMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Procurement.PurBillRcvMemo01": Rpt1a = SetInvPur_BillRcvMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Procurement.PurOrderMemo01": Rpt1a = SetInvPur_OrderMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Procurement.PurReqApprMemo01": Rpt1a = SetInvPur_ApprMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Procurement.RptLcCostSheet01": Rpt1a = SetInvPur_RptLcCostSheet01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


                case "Store.RptClosingStock1":
                case "Store.RptClosingStock1L": Rpt1a = SetInvStore_ClosingStock01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptClosingStock2": Rpt1a = SetInvStore_ClosingStock02(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptMrrDetails1": Rpt1a = SetInvStore_RptMrrDetails1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Store.RptMrrDetails2": Rpt1a = SetInvStore_RptMrrDetails2(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptIssueDetails1": Rpt1a = SetInvStore_RptIssueDetails1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptStoreReqDetails1": Rpt1a = SetInvStore_RptStoreReqDetails1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptPurReqDetails1": Rpt1a = SetInvStore_RptPurReqDetails1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptPurMrrSum1": Rpt1a = SetInvStore_RptPurMrrSum1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptStoreIssueSum1": Rpt1a = SetInvStore_StoreIssueSum1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptPurReqSum1": Rpt1a = SetInvStore_RptPurReqSum1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "InvMgt.RptItemRateList1": Rpt1a = InvMgt_RptItemRateList1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "InvMgt.RptStdStockList1": Rpt1a = InvMgt_RptStdStockList1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptMStockMemo01": Rpt1a = SetInvStore_RptMStockMemo01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
                case "Store.RptMStockDetails1": Rpt1a = SetInvStore_RptMStockDetails1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;

                case "Store.RptItemStatus1": Rpt1a = SetInvStore_ItemStatus1(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;


            }
            Rpt1a.Refresh();
            return Rpt1a;

            // For Repeate Row Header when Tables used in data presentation
            //<KeepWithGroup>After</KeepWithGroup>
            //<RepeatOnNewPage>true</RepeatOnNewPage>
            //<KeepTogether>true</KeepTogether>
        }

    
        private static LocalReport SetInvPur_RptLcCostSheet01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {

            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptCurrency1", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptCurrency2", list3[0].RptFooter2));
                Rpt1a.SetParameters(new ReportParameter("parmOHNote1", list3[0].RptParVal1));

            }
            Hashtable lst1 = (Hashtable)RptDataSet;

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.InvLcCostInfo01>)lst1["GenInfo"]));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet2", (List<HmsEntityInventory.InvLcCostInfo01>)lst1["PayInfo"]));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet3", (List<HmsEntityInventory.InvLcCostInfo01>)lst1["CostInfo"]));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet4", (List<HmsEntityInventory.InvLcCostInfo01>)lst1["PayInfo2"]));
            //if (UserDataset != null)
            //{
            //    var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            //    Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
            //    Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
            //    Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            //}
            return Rpt1a;
        }

        private static LocalReport SetInvStore_ItemStatus1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.ItemStatusDetails>)RptDataSet));
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            return Rpt1a;
        }

        private static LocalReport SetInvStore_RptMStockMemo01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.MStockMemo>)RptDataSet));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Item Physical Stock Memo"));
            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("PrepareByID", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("CheckedByID", list3[0].RptParVal2));
            Rpt1a.SetParameters(new ReportParameter("VerifyedByID", list3[0].RptParVal3));
            Rpt1a.SetParameters(new ReportParameter("ApprovedByID", list3[0].RptParVal4));

            return Rpt1a;
        }


        #endregion

        #region Bill Receive reports parameter settings

        private static LocalReport SetInvStore_RptTransectionList(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.InvTransectionList>)RptDataSet));
            var dt1 = "";
            var dt2 = "";
            var MType1 = "";
            var Title1 = "";
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet;
            if (list1.Count > 0)
            {
                dt1 = list1.Min(x => x.memoDate).ToString("dd-MMM-yyyy");
                dt2 = list1.Max(x => x.memoDate).ToString("dd-MMM-yyyy");
                MType1 = list1[0].memonum.Substring(0, 3);
                switch (MType1)
                {
                    case "SRQ": Title1 = "Store Requisition"; break;
                    case "MRR": Title1 = "Item Receive"; break;
                    case "BIL": Title1 = "Purchase Bill"; break;
                    case "POR": Title1 = "Purchase Order"; break;
                    case "PQT": Title1 = "Purchase Quotation"; break;
                    case "REQ": Title1 = "Purchase Requisition"; break;
                    case "PAP": Title1 = "Purchase Approval"; break;
                    case "QRA": Title1 = "Quotation Approval"; break;
                    case "SIR": Title1 = "Store Issue/Consumption"; break;
                    case "MST": Title1 = "Physical Stock Entry"; break; 
                }
                Title1 = Title1 + " - Transection List ( " + dt1 + " To " + dt2 + ")";
            }
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", Title1));

            return Rpt1a;
        }

        private static LocalReport SetInvPur_BillRcvMemo01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurBillMemo>)RptDataSet));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Bill Receive Memo"));
            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store1", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Supplier", list1[0].ssirname));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));
            Rpt1a.SetParameters(new ReportParameter("Receveby", list1[0].recvbyName));

            return Rpt1a;
        }

        #endregion

        #region Purchase Approval reports parameter settings


        private static LocalReport SetInvPur_ApprMemo01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurApprovMemo>)RptDataSet));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Purchase Approve Memo"));
            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store1", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Supplier", list1[0].ssirname));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));
            Rpt1a.SetParameters(new ReportParameter("ApprovByname", list1[0].approvbyName));

            return Rpt1a;
        }

        #endregion

        #region Purchase Order reports parameter settings
        private static LocalReport SetInvPur_OrderMemo01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurOrderMemo>)RptDataSet));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "ABC Company LTD  \n34/A Mirpur, Dhaka \n01712######"));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle2", "Center Point, 8th Floor(UnitF), Tejkunipara, Farmgate. Dhala-1215, Bangladesh"));

            Rpt1a.SetParameters(new ReportParameter("OrderDat", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("Orderno", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("OrderSub", list1[0].posubject));
            Rpt1a.SetParameters(new ReportParameter("Orderlatter", list1[0].leterdes));

            string TermsDesc = "";
            for (int i = 1; i < list1.Count; i++)
            {
                TermsDesc += i.ToString().Trim() + ". " + list1[i].posubject.Trim() + " : " + list1[i].leterdes.Trim() + "\n";
            }
            Rpt1a.SetParameters(new ReportParameter("TermsDesc", TermsDesc));
            return Rpt1a;
        }

        #endregion

        #region Purchase Rate Fixation reports parameter settings
        private static LocalReport SetInvPur_RateFixMemo01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurRateMemo>)RptDataSet));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Rate Fixcation Memo"));
            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store1", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Supplier", list1[0].ssirname));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));
            Rpt1a.SetParameters(new ReportParameter("ApprovByname", list1[0].approvbyName));

            return Rpt1a;
        }

        #endregion

        #region Purchase Requisition reports parameter settings
        private static LocalReport SetInvStore_PurReqMemo01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var list1a = (List<HmsEntityInventory.PurReqMemo>)RptDataSet;
            decimal stockQty1 = list1a.Sum(x => Math.Abs(x.stockqty));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", list1a));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Purchase Requisition Memo"));
            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));
            Rpt1a.SetParameters(new ReportParameter("ParamStockQty", (list1[0].MemoStatus == "STOCK" ? "Stock Qty" : ""))); //(stockQty1 > 0 ? "Stock Qty" : "")));     

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("PrepareByID", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("CheckedByID", list3[0].RptParVal2));
            Rpt1a.SetParameters(new ReportParameter("VerifyedByID", list3[0].RptParVal3));
            Rpt1a.SetParameters(new ReportParameter("ApprovedByID", list3[0].RptParVal4));

            return Rpt1a;
        }

        #endregion

        #region Store Requisition reports parameter settings
        private static LocalReport SetInvStore_StoreReqMemo01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var list1a = (List<HmsEntityInventory.StoreReqMemo>)RptDataSet;
            decimal stockQty1 = list1a.Sum(x => Math.Abs(x.stockqty));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", list1a));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Store Requisition Memo"));
            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store1", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Store2", list1[0].sectname2));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));
            Rpt1a.SetParameters(new ReportParameter("ParamStockQty", (list1[0].MemoStatus == "STOCK" ? "Stock Qty" : "")));// (stockQty1 > 0 ? "Stock Qty" : "")));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("PrepareByID", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("CheckedByID", list3[0].RptParVal2));
            Rpt1a.SetParameters(new ReportParameter("VerifyedByID", list3[0].RptParVal3));
            Rpt1a.SetParameters(new ReportParameter("ApprovedByID", list3[0].RptParVal4));

            return Rpt1a;
        }

        #endregion

        #region Store Issue reports parameter settings
        private static LocalReport SetInvStore_StoreIssueMemo01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.StoreIssueMemo>)RptDataSet));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Store Issue Memo"));

            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store1", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Store2", list1[0].sectname2));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));
            Rpt1a.SetParameters(new ReportParameter("Receveby", list1[0].recvbyName));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("PrepareByID", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("CheckedByID", list3[0].RptParVal2));
            Rpt1a.SetParameters(new ReportParameter("VerifyedByID", list3[0].RptParVal3));
            Rpt1a.SetParameters(new ReportParameter("ApprovedByID", list3[0].RptParVal4));

            return Rpt1a;
        }

        #endregion
        #region Item MRR to Store reports parameter settings
        private static LocalReport SetInvStore_RptMRRMemo01(LocalReport Rpt1a, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            decimal carramt1 = 0.00m;
            decimal labamt1 = 0.00m;
            decimal othramt1 = 0.00m;
            string charges1 = list1[0].posubject.Trim();

            string[] charges = charges1.Split(',');
            carramt1 = (charges.Length > 0 ? decimal.Parse("0" + charges[0]) : carramt1);
            labamt1 = (charges.Length > 1 ? decimal.Parse("0" + charges[1]) : labamt1);
            othramt1 = (charges.Length > 2 ? decimal.Parse("0" + charges[2]) : othramt1);

            var list0 = (List<HmsEntityInventory.PurMrrMemo>)RptDataSet;
            var TotalMrrAmt = list0.Sum(x => x.mrramt);
            var TotalVATAmt = list0.Sum(x => x.vatamt);
            var TotalDiscAmt = list0.Sum(x => x.discamt);
            var TotalChrgAmt = list0.Sum(x => x.chrgamt);
            var TotalNetAmt = TotalMrrAmt + TotalVATAmt - TotalDiscAmt + TotalChrgAmt;
            var list0s = new List<HmsEntityInventory.PurMrrMemoSum>();

            list0s.Add(new HmsEntityInventory.PurMrrMemoSum() { sumhead = "Total :", sumamt = TotalMrrAmt });
            if (TotalVATAmt > 0)
                list0s.Add(new HmsEntityInventory.PurMrrMemoSum() { sumhead = "VAT (+) :", sumamt = TotalVATAmt });

            if (TotalDiscAmt > 0)
                list0s.Add(new HmsEntityInventory.PurMrrMemoSum() { sumhead = "Discount (-) :", sumamt = TotalDiscAmt });

            if (TotalChrgAmt > 0)
            {
                if (TotalChrgAmt == carramt1 + labamt1 + othramt1)
                {
                    if (carramt1 > 0)
                        list0s.Add(new HmsEntityInventory.PurMrrMemoSum() { sumhead = "Carring :", sumamt = carramt1 });

                    if (labamt1 > 0)
                        list0s.Add(new HmsEntityInventory.PurMrrMemoSum() { sumhead = "Labour :", sumamt = labamt1 });

                    if (othramt1 > 0)
                        list0s.Add(new HmsEntityInventory.PurMrrMemoSum() { sumhead = "Other Charge :", sumamt = othramt1 });
                }
                else
                    list0s.Add(new HmsEntityInventory.PurMrrMemoSum() { sumhead = "Carr./Lab Charge :", sumamt = TotalChrgAmt });
            }

            if (TotalVATAmt > 0 || TotalDiscAmt > 0 || TotalChrgAmt > 0)
                list0s.Add(new HmsEntityInventory.PurMrrMemoSum() { sumhead = "Net Total :", sumamt = TotalNetAmt });

            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurMrrMemo>)RptDataSet));
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1s", list0s));


            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Item Receive Memo"));
            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store1", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Supplier", list1[0].ssirname));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));
            Rpt1a.SetParameters(new ReportParameter("Receveby", list1[0].recvbyName));

            var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
            Rpt1a.SetParameters(new ReportParameter("PrepareByID", list3[0].RptParVal1));
            Rpt1a.SetParameters(new ReportParameter("CheckedByID", list3[0].RptParVal2));
            Rpt1a.SetParameters(new ReportParameter("VerifyedByID", list3[0].RptParVal3));
            Rpt1a.SetParameters(new ReportParameter("ApprovedByID", list3[0].RptParVal4));

            return Rpt1a;
        }

        #endregion

        #region Purchase Quotation reports parameter settings

        private static LocalReport SetInvPur_QuotMemo01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurQtnMemo>)RptDataSet));
            var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Suppliers Price Quotation Memo"));
            Rpt1a.SetParameters(new ReportParameter("MemoDate", list1[0].memoDate1));
            Rpt1a.SetParameters(new ReportParameter("MemoNo", list1[0].memonum1));
            Rpt1a.SetParameters(new ReportParameter("PrepareByName", list1[0].PreparByName));
            Rpt1a.SetParameters(new ReportParameter("Store1", list1[0].sectname));
            Rpt1a.SetParameters(new ReportParameter("Supplier", list1[0].ssirname));
            Rpt1a.SetParameters(new ReportParameter("Narration", list1[0].Naration));
            Rpt1a.SetParameters(new ReportParameter("Referance", list1[0].Referance));
            Rpt1a.SetParameters(new ReportParameter("Receveby", list1[0].recvbyName));

            return Rpt1a;
        }
        #endregion

        #region Inventory Summary Reports

        private static LocalReport SetInvStore_ClosingStock01(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.InvStockList>)RptDataSet));
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
                Rpt1a.SetParameters(new ReportParameter("ParmCurr1", list3[0].RptParVal2));   // Will be programmed when multicurrency enabled

            }
            //var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Suppliers Price Quotation Memo : " + list1[0].memonum1 + ", Dated : " + list1[0].memoDate1));

            return Rpt1a;
        }
        private static LocalReport SetInvStore_ClosingStock02(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.InvStockList02>)RptDataSet));
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
                Rpt1a.SetParameters(new ReportParameter("ParmCurr1", list3[0].RptParVal2));   // Will be programmed when multicurrency enabled
            }
            //var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Suppliers Price Quotation Memo : " + list1[0].memonum1 + ", Dated : " + list1[0].memoDate1));

            return Rpt1a;
        }
        private static LocalReport SetInvStore_RptMrrDetails1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.MrrMemoDetails>)RptDataSet));
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            return Rpt1a;
        }
        private static LocalReport SetInvStore_RptMrrDetails2(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.MrrMemoDetails>)RptDataSet));
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            return Rpt1a;
        }

        private static LocalReport SetInvStore_RptIssueDetails1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.IssueMemoDetails>)RptDataSet));
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            //var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Suppliers Price Quotation Memo : " + list1[0].memonum1 + ", Dated : " + list1[0].memoDate1));

            return Rpt1a;
        }

        private static LocalReport SetInvStore_RptStoreReqDetails1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.StoreReqMemoDetails>)RptDataSet));
            //var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Suppliers Price Quotation Memo : " + list1[0].memonum1 + ", Dated : " + list1[0].memoDate1));

            return Rpt1a;
        }

        private static LocalReport SetInvStore_RptPurReqDetails1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurReqMemoDetails>)RptDataSet));
            //var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Suppliers Price Quotation Memo : " + list1[0].memonum1 + ", Dated : " + list1[0].memoDate1));

            return Rpt1a;
        }

        private static LocalReport SetInvStore_RptMStockDetails1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.MStockMemoDetails>)RptDataSet));
            //var list1 = (List<HmsEntityInventory.InvTransectionList>)RptDataSet2;
            //Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Suppliers Price Quotation Memo : " + list1[0].memonum1 + ", Dated : " + list1[0].memoDate1));

            return Rpt1a;
        }

        private static LocalReport SetInvStore_StoreIssueSum1(LocalReport rpt1A, object rptDataSet, object rptDataSet2, object userDataset)
        {
            if (userDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)userDataset;
                rpt1A.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                rpt1A.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                rpt1A.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            rpt1A.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.StoreIssueSummary1>)rptDataSet));
            return rpt1A;
        }

        private static LocalReport SetInvStore_RptStoreReqSum1(LocalReport rpt1A, object rptDataSet, object rptDataSet2, object userDataset)
        {
            if (userDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)userDataset;
                rpt1A.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                rpt1A.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                rpt1A.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            rpt1A.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.StoreIssueSummary1>)rptDataSet));
            return rpt1A;
        }

        private static LocalReport SetInvStore_RptPurMrrSum1(LocalReport rpt1A, object rptDataSet, object rptDataSet2, object userDataset)
        {
            if (userDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)userDataset;
                rpt1A.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                rpt1A.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                rpt1A.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            rpt1A.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurMrrSummary1>)rptDataSet));
            return rpt1A;
        }

        private static LocalReport SetInvStore_RptPurReqSum1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            if (UserDataset != null)
            {
                var list3 = (List<HmsEntityGeneral.ReportGeneralInfo>)UserDataset;
                Rpt1a.SetParameters(new ReportParameter("RptHeader1", list3[0].RptHeader1));
                Rpt1a.SetParameters(new ReportParameter("RptHeader2", list3[0].RptHeader2));
                Rpt1a.SetParameters(new ReportParameter("RptHeader3", list3[0].RptParVal1));
            }
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.PurReqSummary1>)RptDataSet));
            return Rpt1a;
        }

        #endregion

        private static LocalReport InvMgt_RptItemRateList1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.InvItemRateList>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Inventory List"));

            return Rpt1a;
        }
        private static LocalReport InvMgt_RptStdStockList1(LocalReport Rpt1a, object RptDataSet, object RptDataSet2, object UserDataset)
        {
            Rpt1a.DataSources.Add(new ReportDataSource("RptDataSet1", (List<HmsEntityInventory.InvStdStockList>)RptDataSet));
            Rpt1a.SetParameters(new ReportParameter("ParamTitle1", "Inventory Stock List"));

            return Rpt1a;
        }

    }
}
