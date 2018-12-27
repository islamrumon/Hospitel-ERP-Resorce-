using ASITHmsEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Data;
using ASITFunLib;


namespace ASITHmsWpf.Commercial
{
    public class GridReportGenTr01
    {

        public GridReportGenTr01()
        {

        }

        private static DataGridColumn GetCol(string header1 = "No Header", string binding1 = "", string format1 = "", string style1 = "", string width = "")
        {
            var MyStyle = new Style(typeof(DataGridCell))
            {
                Setters = {
                    new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right)
                }
            };

            string format2 = "";
            int width1 = 0;
            var style2 = new Style();
            switch (format1)
            {
                case "Date":
                    format2 = "dd-MMM-yyyy";
                    break;
                case "SFormat":
                    format2 = "#,##0.00;(#,##0.00); - ";
                    break;
            }
            switch (width)
            {
                case "Width":
                    width1 = 300;
                    break;
                default:
                    if (width == "")
                        break;

                    width1 = Convert.ToInt32(width.Substring(5));
                    break;
            }
            switch (style1)
            {
                case "TxtRight":
                    style2 = MyStyle;
                    break;
            }

            DataGridColumn dc1 = new DataGridTextColumn { Header = header1, Binding = new Binding(binding1) { StringFormat = format2 }, CellStyle = style2, Width = width1 };
            return dc1;
        }
        public class SlsInv02
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.InvoiceTransList2> SlsInvList02)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 450;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                
                //="Date : " & Fields!invdat1.Value & ",  " & "Invoice No. : " & Mid(Fields!invno1.Value , 4, 2) & Right(Fields!invno1.Value ,6)
                dg1.Columns.Add(GridReportGenTr01.GetCol("SL No.", "slnum", width: "Width50"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Date", "invdat1", "Date", width: "Width90"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Invoice No.", "invno1", width: "Width110"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Item Description", "sirdesc", width: "Width"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Quantity", "invqty", "SFormat", "TxtRight", width: "Width55"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Rate", "itmrat", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Amount", "inetam", "SFormat", "TxtRight", width: "Width95"));

                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "SL No.", Binding = new Binding("slnum") });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Item Description", Binding = new Binding("sirdesc"), Width = 350 });

                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Quantity", Binding = new Binding("invqty") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Amount", Binding = new Binding("inetam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });

                dg1.ItemsSource = SlsInvList02;
                dg1.Items.Refresh();
                //ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dg1.ItemsSource);
                //if (cvTasks != null && cvTasks.CanGroup == true)
                //{
                //    cvTasks.GroupDescriptions.Clear();
                //    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("voudat1", new RelativeDateValueConverter()));
                //    //cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("memoDate1"));
                //}
                return dg1;

            }
        }
        public class SlsInv01
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.InvoiceTransList> SlsInvList01)
            {

                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 450;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportGenTr01.GetCol("SL No.", "slnum", width: "Width40"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Date", "invdat1", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Memo No.", "invno1", width: "Width110"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Customer Name", "custName", width: "Width210"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Sale Amt.", "totslam", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Discount", "tdisam", "SFormat", "TxtRight", width: "Width60"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("VAT", "tvatam", "SFormat", "TxtRight", width: "Width65"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Bill Amt", "billam", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Collection", "collam", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Due Amt", "dueam", "SFormat", "TxtRight", width: "Width80"));

                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "SL No.", Binding = new Binding("slnum") });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Memo No.", Binding = new Binding("invno1") });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Bill Amount", Binding = new Binding("billam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Collection Amount", Binding = new Binding("collam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Due Amount", Binding = new Binding("dueam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "VAT", Binding = new Binding("tvatam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Discount", Binding = new Binding("tdisam") { StringFormat = "#,##0.00;(#,##0.00); " }, CellStyle = style1 });

                dg1.ItemsSource = SlsInvList01;
                dg1.Items.Refresh();
                //ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
                //if (cvTasks != null && cvTasks.CanGroup == true)
                //{
                //    cvTasks.GroupDescriptions.Clear();
                //    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("voudat", new RelativeDateValueConverter()));
                //    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("vounum1"));

                //}
                return dg1;

            }
        }
        public class DailySales01
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.RetSalesTransList2> DailySalesList01)
            {
                foreach (var item in DailySalesList01)
                {
                    //=Mid(Fields!invno.Value,8,2) +"-"+ Right(Fields!invno.Value,5)
                    item.invno = item.invno.Substring(0,3) + item.invno.Substring(7, 2) + "-" + item.invno.Substring(13);
                    //= Right(Fields!custid.Value, 6) + ": " + Trim(Fields!custname.Value) + iif(len(Trim(Fields!invstatus.Value))> 0 and false ,
                    //Chr(10) + "Note: " +Trim(Fields!invstatus.Value), "")
                    item.custname = item.custid.Substring(6) + ": " + item.custname.Trim();
                }
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 450;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportGenTr01.GetCol("Inv. #", "invno", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Customer ID and Name", "custname",width: "Width250"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Amount", "itemam", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Gross", "grossam", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Discount", "discam", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("VAT", "vatam", "SFormat", "TxtRight", width: "Width65"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Total", "totalam", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Paid", "paidam", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Due", "dueam", "SFormat", "TxtRight", width: "Width80"));

                dg1.ItemsSource = DailySalesList01;
                dg1.Items.Refresh();
                return dg1;

            }
        }
        public class SalesCashRecv1
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.RetSalesCashRecv1> SalesCashRecvList1)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 450;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportGenTr01.GetCol("Date", "voudat", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Voucher #", "vounum1", width: "Width110"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Customer ID and Name", "custname", width: "Width"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Ref. No.", "remarks", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Recv. Amount", "recvam", "SFormat", "TxtRight", width: "Width80"));

                dg1.ItemsSource = SalesCashRecvList1;
                dg1.Items.Refresh();
                return dg1;

            }
        }     
        public class TrialBalance
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccTrialBalance1> AccTrialBlncLst)
            {
                foreach (var item in AccTrialBlncLst)
                {
                    //= iif(Fields!sectcod.Value="000000000000", Fields!actcode.Value, Fields!sectcod.Value)
                    item.actcode = item.sectcod.ToString() == "000000000000" ? item.actcode : "    "+ item.sectcod;
                }
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 450;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                //dg1.Columns.Add(GridReportAcc1.GetCol("Vou. Date", "voudat", "Date"));
                //dg1.Columns.Add(GridReportAcc1.GetCol("Vou. No.", "vounum1"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Code", "actcode", width: "Width100"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Description", "actdesc", width: "Width"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Opening", "opnam", "SFormat", "TxtRight", "Width90"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Debit", "curdr", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Credit", "curcr", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Net", "curam", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Closing", "clsam", "SFormat", "TxtRight", "Width90"));
                dg1.ItemsSource = AccTrialBlncLst;
                dg1.Items.Refresh();
                return dg1;

            }
        }
        public class CUSTLEDGER
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccLedger1> AccTrnLst2)
            {

                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 450;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportGenTr01.GetCol("Vou. Date", "voudat", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Code", "vounum1", width: "Width90"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Accounts Description", "trdesc", width: "Width"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Inner", "inram", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Debit", "dram", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Credit", "cram", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportGenTr01.GetCol("Balance", "blancam", "SFormat", "TxtRight", "Width80"));
                dg1.ItemsSource = AccTrnLst2;
                dg1.Items.Refresh();
                //ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
                //if (cvTasks != null && cvTasks.CanGroup == true)
                //{
                //    cvTasks.GroupDescriptions.Clear();
                //    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("voudat", new RelativeDateValueConverter()));
                //    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("vounum1"));

                //}
                return dg1;

            }
        }
    
    }
}