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

namespace ASITHmsWpf.Inventory
{
    public class GridReportInv1
    {
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
                    format2 = "#,##0.00;(#,##0.00); ";
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
        public class StockBalance
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.InvStockList> RptStockList)
            {

                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Opening\nBalance", "opnqty", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rcv.(in)", "recvqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Is./Co(out)", "isuqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Net\nPosition", "netqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Closing\nBalance", "clsqty", "SFormat", "TxtRight", "Width80"));


                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Item Description", Binding = new Binding("sirdesc") });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Unit", Binding = new Binding("sirunit") });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Opening\nBalance", Binding = new Binding("opnqty") { StringFormat = nFormat }, CellStyle = style2 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Rcv.(in)", Binding = new Binding("recvqty") { StringFormat = nFormat }, CellStyle = style2 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Is./Co(out)", Binding = new Binding("isuqty") { StringFormat = nFormat }, CellStyle = style2 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Net\nPosition", Binding = new Binding("netqty") { StringFormat = nFormat }, CellStyle = style2 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Closing Balance", Binding = new Binding("clsqty") { StringFormat = nFormat }, CellStyle = style2 });
                //dgOverall01.Columns.Add(new DataGridTextColumn() { Header = "Min.Level", Binding = new Binding("minstock") { StringFormat = nFormat }, CellStyle = style2 });

                dg1.ItemsSource = RptStockList;
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
        public class StockBalanceWithLbl
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.InvStockList> RptStockList)
            {

                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width250"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Opening\nBalance", "opnqty", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rcv.(in)", "recvqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Is./Co(out)", "isuqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Net\nPosition", "netqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Closing Balance", "clsqty", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Min.Level", "minstock", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Less exist", "lessqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Max Level", "maxstock", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Excess", "excesqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Re-Order\nLevel", "reordrlvl", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Req.\nRequired", "rreqqty", "SFormat", "TxtRight", "Width50"));


                dg1.ItemsSource = RptStockList;
                dg1.Items.Refresh();

                return dg1;

            }
        }

        public class StockBalanceStatus
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.InvStockList02> RptStockList02)
            {

                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width210"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width40"));
                dg1.Columns.Add(GridReportInv1.GetCol("Opening\nBalance", "opnqty", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("MRR", "mrrqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Other Rcv.", "orcvqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Total Rcv.", "trcvqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Issue Qty", "isuqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Consum.", "conqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Sales", "salqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Total Issue", "tisuqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Net\nPosition", "netqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Closing\nBalance", "clsqty", "SFormat", "TxtRight", "Width80"));

                dg1.ItemsSource = RptStockList02;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class StrReq01
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.StoreIssueSummary1> storeSumList)
            {

                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Store", "sectName", width: "Width150"));
                dg1.Columns.Add(GridReportInv1.GetCol("Store", "sectName2", width: "Width150"));
                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "trnqty", "SFormat", "TxtRight", "Width60"));

                dg1.ItemsSource = storeSumList;
                dg1.Items.Refresh();
                ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dg1.ItemsSource);
                if (cvTasks != null && cvTasks.CanGroup == true)
                {
                    cvTasks.GroupDescriptions.Clear();
                    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("sectName"));
                    //cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("sectName2"));
                }
                return dg1;

            }
        }
        public class PurReq01
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.PurReqSummary1> purReqLst)
            {

                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Location", "sectName", width: "Width150"));
                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "reqqty", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width40"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rate", "reqrat", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Amount", "reqamt", "SFormat", "TxtRight", "Width80"));


                dg1.ItemsSource = purReqLst;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class MRR01
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.PurMrrSummary1> purMrrLst)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Location", "sectName", width: "Width130"));
                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width40"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "mrrqty", "SFormat", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rate", "mrrrat", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Amount", "mrramt", "SFormat", "TxtRight", "Width80"));


                dg1.ItemsSource = purMrrLst;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class memoTranList
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.InvTransectionList> RptList)
            {
                foreach (var item in RptList)
                {
                    // iif((Fields!sectname2.Value) = "",(Fields!ssirname.Value),(Fields!sectname2.Value))
                    item.sectname2 = item.sectname2.Trim() == "" ? item.ssirname.Trim() : item.sectname2.Trim();
                    //= iif((Fields!PreparByName.Value) = "",iif((Fields!recvbyName.Value) = "",(Fields!approvbyName.Value),(Fields!recvbyName.Value)),(Fields!PreparByName.Value))
                    item.PreparByName = item.PreparByName.Trim() == "" ? (item.recvbyName.Trim() == "" ? item.approvbyName.Trim() : item.recvbyName.Trim()) : item.PreparByName.Trim();

                }
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Date", "memoDate1", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Memo No", "memonum1", width: "Width120"));
                dg1.Columns.Add(GridReportInv1.GetCol("Department", "sectname", width: "Width250"));
                dg1.Columns.Add(GridReportInv1.GetCol("Store / Supplier Name", "sectname2", width: "Width130"));
                dg1.Columns.Add(GridReportInv1.GetCol("Prepare / Rcv. / AppRv. By", "PreparByName", width: "Width310"));

                dg1.ItemsSource = RptList;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class StrReqT01
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.StoreReqMemoDetails> storeReqDetailsList)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Date", "srfdat1", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Location1", "sectname", width: "Width120"));
                dg1.Columns.Add(GridReportInv1.GetCol("Req. To", "sectname2", width: "Width120"));
                dg1.Columns.Add(GridReportInv1.GetCol("req. No", "srfno1", width: "Width100"));
                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width100"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "srfqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Req. By Name", "srfbyName", width: "Width100"));
                dg1.Columns.Add(GridReportInv1.GetCol("Ref.", "srfref", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Narration", "srfnar", width: "Width95"));


                dg1.ItemsSource = storeReqDetailsList;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class StrReqT02
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.IssueMemoDetails> issueDetailsList)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                //dg1.Columns.Add(GridReportInv1.GetCol("Date", "srfdat1", "Date"));
                //dg1.Columns.Add(GridReportInv1.GetCol("Location1", "sectname"));
                dg1.Columns.Add(GridReportInv1.GetCol("Issue To", "sectname2", width: "Width130"));
                dg1.Columns.Add(GridReportInv1.GetCol("Inv. Code", "invcode", width: "Width90"));
                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width250"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width40"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "sirqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Issue By Name", "sirbyName", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Receive By", "recvbyName", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Ref.", "sirref", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Narration", "sirnar", width: "Width95"));

                dg1.ItemsSource = issueDetailsList;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class StrReqT03
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.PurReqMemoDetails> purReqDetailsList)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width250"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width40"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "reqqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rate", "reqrate", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Amount", "reqamt", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Receive By", "reqbyName", width: "Width150"));
                dg1.Columns.Add(GridReportInv1.GetCol("Ref.", "reqref", width: "Width100"));
                dg1.Columns.Add(GridReportInv1.GetCol("Narration", "reqnar", width: "Width150"));

                dg1.ItemsSource = purReqDetailsList;
                dg1.Items.Refresh();

                return dg1;

            }
        }

        public class StrMst06
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.MStockMemoDetails> MStockMemoDetails)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width250"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width40"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "mstkqty", "SFormat", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rate", "mstkrate", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Amount", "mstkamt", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Taken By", "mstkbyName", width: "Width150"));
                dg1.Columns.Add(GridReportInv1.GetCol("Ref.", "mstkref", width: "Width100"));
                dg1.Columns.Add(GridReportInv1.GetCol("Narration", "mstknar", width: "Width150"));

                dg1.ItemsSource = MStockMemoDetails;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class StrReqT04
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.MrrMemoDetails> mrrMemoDetailsList)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Date", "mrrdat1", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Inv. Code", "mrrno1", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width250"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width40"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "mrrqty", "SFormat", "TxtRight", width: "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rate", "mrrrate", "SFormat", "TxtRight", width: "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Amount", "mrramt", "SFormat", "TxtRight", width: "Width80"));

                dg1.Columns.Add(GridReportInv1.GetCol("Ref.", "mrrref", width: "Width90"));
                dg1.Columns.Add(GridReportInv1.GetCol("Narration", "mrrnar", width: "Width120"));

                dg1.ItemsSource = mrrMemoDetailsList;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class StrReqT05
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.MrrMemoDetails> mrrMemoDetailsList)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Date", "mrrdat1", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Inv. Code", "mrrno1", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Item Description", "sirdesc", width: "Width250"));
                dg1.Columns.Add(GridReportInv1.GetCol("Unit", "sirunit", width: "Width40"));
                dg1.Columns.Add(GridReportInv1.GetCol("Quantity", "mrrqty", "SFormat", "TxtRight", width: "Width50"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rate", "mrrrate", "SFormat", "TxtRight", width: "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Amount", "mrramt", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Supplier", "ssirname", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Order No", "orderno1", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Chalan No", "chlnno", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Chalan Date", "chlndat1", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Mfg. Date", "mfgdat", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Exo. Date", "expdat", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Batch. No", "batchno", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Receive By", "recvbyName", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Ref.", "mrrref", width: "Width90"));
                dg1.Columns.Add(GridReportInv1.GetCol("Narration", "mrrnar", width: "Width120"));

                dg1.ItemsSource = mrrMemoDetailsList;
                dg1.Items.Refresh();

                return dg1;

            }
        }
        public class ItemStatusD
        {
            public static DataGrid GetDataGrid(List<HmsEntityInventory.ItemStatusDetails> itemStatusList)
            {
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 400;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible; //Visibility.Visible;                

                dg1.Columns.Add(GridReportInv1.GetCol("Date", "trndat", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportInv1.GetCol("Memo No", "memonum1", width: "Width110"));
                dg1.Columns.Add(GridReportInv1.GetCol("Description", "trndesc", width: "Width290"));
                dg1.Columns.Add(GridReportInv1.GetCol("In-Qty", "inqty", "SFormat", "TxtRight", width: "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Out-Qty", "outqty", "SFormat", "TxtRight", width: "Width70"));
                dg1.Columns.Add(GridReportInv1.GetCol("Balance", "balqty", "SFormat", "TxtRight", width: "Width90"));
                dg1.Columns.Add(GridReportInv1.GetCol("Rate", "itmrat", "SFormat", "TxtRight", width: "Width90"));
                dg1.Columns.Add(GridReportInv1.GetCol("Amount", "itmamt", "SFormat", "TxtRight", width: "Width90"));

                dg1.ItemsSource = itemStatusList;
                dg1.Items.Refresh();

                return dg1;

            }
        }
    }
}
