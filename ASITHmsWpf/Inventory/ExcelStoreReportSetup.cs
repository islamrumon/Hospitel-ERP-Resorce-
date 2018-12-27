using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using ASITHmsEntity;
using Microsoft.Reporting.WinForms;
using System.Configuration;


namespace ASITHmsWpf.Inventory
{
    public static class ExcelStoreReportSetup
    {
        public static string ExcelForStock01(List<HmsEntityGeneral.ReportGeneralInfo> list3 , List<HmsEntityInventory.InvStockList> RptStockList )
        {
            try
            {
                #region MyRegion

                var rptName = list3[0].RptHeader1;
                var rptDateRange = list3[0].RptHeader2;

                
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = false;
                //app.WindowState = XlWindowState.xlMaximized;

                Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet ws = wb.Worksheets[1]; //app.ActiveSheet as Microsoft.Office.Interop.Excel.Worksheet;               
                Worksheet ws2 = wb.Worksheets.Add(wb.Worksheets[wb.Worksheets.Count]);

                
                ws2.Name = "myWorkSheet";

                ws.Name = "TestData";

                int i = 7;

                #region Report Header                

                ws2.Range["B3"].Value = list3[0].RptCompName.ToString();
                ws2.Range["B3:J3"].HorizontalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;
                ws2.Range["B3:J3"].VerticalAlignment = XlVAlign.xlVAlignCenter;               
                ws2.Range["B3:J3"].Font.Bold = true;
                ws2.Range["B3:J3"].Font.Size = 18;
                ws2.Range["B3:J3"].Font.Name = "Calibri";

                ws2.Range["B4"].Value = rptName + rptDateRange;                
                ws2.Range["B4:J4"].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws2.Range["B4:J4"].HorizontalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;
                ws2.Range["B4:J4"].Font.Bold = true;
                ws2.Range["B4:J4"].Font.Size = 15;
                ws2.Range["B4:J4"].Font.Name = "Calibri";

                ws2.Range["B5"].Value = "SL.#";
                ws2.Range["B55:B6"].VerticalAlignment = XlVAlign.xlVAlignBottom;
                ws2.Range["B5"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws2.Range["B5:B6"].MergeCells = true;
                ws2.Range["B5:B6"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                ws2.Range["B5:B6"].Borders.Weight = 2;
                ws2.Range["B:B"].ColumnWidth = 3;
                ws2.Range["A:A"].ColumnWidth = 1;
                ws2.Range["5:6"].RowHeight = 15;

                ws2.Range["C5"].Value = "Item Code";
                ws2.Columns["C:C"].NumberFormat = "000000000000";
                ws2.Range["C5"].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws2.Range["C5"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws2.Range["C5:C6"].MergeCells = true;
                ws2.Range["C5:C6"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                ws2.Range["C5:C6"].Borders.Weight = 2;
                ws2.Range["C:C"].ColumnWidth = 12.75;
                ws2.Range["5:6"].RowHeight = 15;

                ws2.Range["D5"].Value = "Item Description";
                ws2.Range["D5"].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws2.Range["D5"].HorizontalAlignment = XlHAlign.xlHAlignLeft;
                ws2.Range["D5:D6"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                ws2.Range["D5:D6"].MergeCells = true;
                ws2.Range["D:D"].ColumnWidth = 40;


                ws2.Range["E5"].Value = "Unit";
                ws2.Range["E5:E6"].MergeCells = true;
                ws2.Range["E5"].ColumnWidth = 6;
                ws2.Range["E5"].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws2.Range["E5"].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws2.Range["E5:E6"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                ws2.Range["F5"].Value = "Opening Balance";
                ws2.Range["F5"].HorizontalAlignment = XlHAlign.xlHAlignRight;
                ws2.Range["F5"].VerticalAlignment = XlVAlign.xlVAlignTop;
                ws2.Range["F5"].WrapText = true;
                ws2.Range["F5:F6"].MergeCells = true;
                ws2.Range["F5:F6"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                ws2.Range["G5"].Value = "Reporting Period Transactions";
                ws2.Range["G5:I5"].HorizontalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;
                ws2.Range["G6"].Value = "Rcv. (in)";
                ws2.Range["H6"].Value = "Is./Co.(out)";
                ws2.Range["H:H"].ColumnWidth = 12;
                ws2.Range["I6"].Value = "Net Position";
                ws2.Range["I:I"].ColumnWidth = 12;
                ws2.Range["G5:I6"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                ws2.Range["J5"].Value = "Closing Balance";
                ws2.Range["J5:J6"].MergeCells = true;
                ws2.Range["J5"].HorizontalAlignment = XlHAlign.xlHAlignRight;
                ws2.Range["J5"].VerticalAlignment = XlVAlign.xlVAlignTop;
                ws2.Range["J5"].WrapText = true;
                ws2.Range["J5:J6"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                #endregion Report Header end

                foreach (var item in RptStockList)
                {
                    string j = i.ToString();
                    ws2.Range["B" + j].NumberFormat = "0. ";
                    ws2.Range["B" + j].Value = item.slnum;
                    ws2.Range["C" + j].Value = item.sircode;
                    ws2.Range["D" + j].Value = item.sirdesc;
                    ws2.Range["E" + j].Value = item.sirunit;
                    ws2.Range["F" + j + ":J" + j].NumberFormat = "_(* #,##0.00_);_(* -#,##0.00_);_(*  - ??_);_(@_)";
                    ws2.Range["F" + j].Value = item.opnqty;
                    ws2.Range["G" + j].Value = item.recvqty;
                    ws2.Range["H" + j].Value = item.isuqty;
                    ws2.Range["I" + j].Value = item.netqty;
                    ws2.Range["J" + j].Value = item.clsqty;
                    ++i;
                }

                ws2.PageSetup.PrintTitleRows = "$3:$6";
                ws2.PageSetup.RightFooter = "Page &P of &N";
                ws2.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                ws2.PageSetup.Zoom = false;
                ws2.PageSetup.FitToPagesWide = 1;
                ws2.PageSetup.FitToPagesTall = 1000;
                ws2.PageSetup.PrintQuality = 600;
                ws2.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                ws2.Range["A7"].Select();
                ws2.Application.ActiveWindow.FreezePanes = true;

                string ii = System.IO.Path.Combine(Environment.CurrentDirectory, System.Windows.Forms.Application.ProductName + ".EXE");
                Configuration Config1 = ConfigurationManager.OpenExeConfiguration(ii);
                string FilePath1 = Config1.AppSettings.Settings["AppLocalImagePath"].Value.ToString().Trim();
                string FileName1 = FilePath1 + "StockStatus1.xlsx";// @"C:\Temps\vitoshacademy.xlsx";
                
                wb.SaveCopyAs(FileName1);

                #endregion
                return FileName1;
            }
            catch (Exception exp1)
            {
                return "File not created";
            }
        }           
    }
}
