using ASITHmsEntity;
using ASITHmsWpf.Properties;    // To be confirmed later
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;


namespace ASITHmsWpf.Commercial
{
    public class GridReportFrontDesk01
    {
        public GridReportFrontDesk01()
        {

        }
        private static DataGridTemplateColumn GetTCol(string header1 = "No Header", string binding1 = "", string format1 = "", string style1 = "", string width = "")
        {
            DataGridTemplateColumn col1 = new DataGridTemplateColumn();

            string format2 = "";
            int width1 = 0;
            switch (format1)
            {
                case "Date":
                    format2 = "dd-MMM-yyyy";
                    break;
                case "Date1":
                    format2 = "dd.MM.yy hh:mm tt";
                    break;
                case "SFormat":
                    format2 = "#,##0.00;(#,##0.00); ";
                    break;
                case "SFormat2":
                    format2 = "#,##0;(#,##0); ";
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

            DataTemplate headertemplate1 = new DataTemplate();
            var textBlock1 = new FrameworkElementFactory(typeof(TextBlock));
            textBlock1.SetValue(TextBlock.TextProperty, header1);
            //textBlock1.SetValue(TextBlock.WidthProperty, Convert.ToDouble(width1));
            headertemplate1.VisualTree = textBlock1;

            DataTemplate cellTemplate1 = new DataTemplate();
            var textBlock2 = new FrameworkElementFactory(typeof(TextBlock));
            textBlock2.SetBinding(TextBlock.TextProperty, new Binding(binding1) { StringFormat = format2 });
            textBlock2.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            //textBlock2.SetValue(TextBlock.WidthProperty, Convert.ToDouble(width1));
            switch (style1)
            {
                case "TxtRight":
                    textBlock2.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
                    break;
                case "TxtCenter":
                    textBlock2.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                    break;
                default:
                    textBlock2.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Left);
                    break;
            }
            cellTemplate1.VisualTree = textBlock2;

            col1.HeaderTemplate = headertemplate1;
            col1.CellTemplate = cellTemplate1;
            col1.Width = width1;
            return col1;
        }
        public class TransectionInvoiceList //"A01. Sales invoice list" AND "A02. Invoice wise details"
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.CommInvSummInf> RptList)
            {
                #region listbinding
                //string ptname01 = "";
                foreach (var item in RptList)
                {
                    //=Fields!ptname.Value + iif(Len(Fields!rfFullName.Value) > 0 , ", Ref: " + Fields!rfFullName.Value, "")
                    item.ptname += item.rfFullName.ToString().Trim() == "" ? "" : ", Ref: " + item.rfFullName;
                    //item.ptname += ptname01;
                }
                #endregion

                #region datagrid
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 930;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 440;
                dg1.RowHeaderWidth = 0;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;     
                dg1.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;     
                dg1.FrozenColumnCount = 6;
                #region Datagrid Row Group Style
                //FrameworkElementFactory datagridRowsPresenter = new FrameworkElementFactory(typeof(DataGridRowsPresenter));
                ////datagridRowsPresenter.SetValue();
                //ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate();
                //itemsPanelTemplate.VisualTree = datagridRowsPresenter;
                //GroupStyle groupStyle = new GroupStyle();
                //groupStyle.Panel = itemsPanelTemplate;
                //dg1.GroupStyle.Add(groupStyle);

                //Uri resourceLocater = new Uri("/YourAssemblyName;component/SubDirectory/YourFile.xaml", System.UriKind.Relative);
                //ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                //groupStyle.ContainerStyle = resourceDictionary["GroupHeaderStyle"] as Style;
                //ListCollectionView collectionView = new ListCollectionView(RptList);
                //collectionView.GroupDescriptions.Add(new PropertyGroupDescription("brnnam"));
                #endregion

                #endregion

                #region Datagrid Columns Design
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Sl #", "slnum", "SFormat2", "TxtRight", "Width30"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Date & Time", "ptinvdat", "Date1", "TxtCenter", "Width110"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Invoice #", "ptinvnum2", style1: "TxtCenter", width: "Width90"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("User Name", "signinnam", width: "Width75"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Terminal", "preparetrm", width: "Width75"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Description", "ptname", width: "Width300"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Item\nQty", "titemqty", "SFormat2", "TxtRight", "Width30"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Total\nAmount", "titmam", "SFormat2", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Discount\nAmount", "ticdisam", "SFormat2", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Net\nAmount", "tinetam", "SFormat2", "TxtRight", "Width50"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("VAT\nAmt.", "tivatam", "SFormat2", "TxtRight", "Width40"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Total Bill\nAmount", "tbillam", "SFormat2", "TxtRight", "Width55"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Collection\nAmount", "tbilcolam", "SFormat2", "TxtRight", "Width55"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Due\nAmount", "tdueam", "SFormat2", "TxtRight", "Width50"));
                #endregion

                dg1.ItemsSource = RptList;
                return dg1;
            }
        }
        public class GroupWiseTransactionList  //"A03. Group wise sales details" AND "B01. Group sales summary"  
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.GroupWiseTrans01> RptList)
            {
                #region datagrid
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 930;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.RowHeaderWidth = 0;
                dg1.Height = 440;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible; 
                #endregion

                #region listbinding
                int ii = 0;
                foreach (var item in RptList)
                {
                    //ii = (item.colstyle.Contains("NL") ? ii + 1 : 0);
                    ii = ii + 1;
                    item.slnum = ii;
                }
                #endregion
                #region Datagrid Columns Design
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Sl. #", "slnum", style1: "TxtRight", width: "Width25"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Item Name & Transaction Description", "trdesc", width: "Width620"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Qty", "itemqty", "SFormat2", "TxtRight", "Width30"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Gr. Amount", "titmam", "SFormat2", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Discount", "idisam", "SFormat2", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Net Amount", "inetam", "SFormat2", "TxtRight", "Width60"));
                #endregion
                dg1.ItemsSource = RptList;
                return dg1;
            }
        }

        public class DateWiseCollection01
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.FDeskSalesSumm01> RptList, string colhead1, string combrns)
            {
                #region Datagrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 930;
                dg1.RowHeaderWidth = 0;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 440;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;  
                #endregion
                #region Header Design
                string header01 = (colhead1 == "NONE" || colhead1 == "BRANCHBYDAY" ? "Description" : (combrns.ToString().Substring(2) != "00" ? "Description" : "Date"));
                string header02 = (colhead1 == "DAYBYBRANCH" || colhead1 == "BRANCHSUMM" ? (combrns.ToString().Substring(2) != "00" ? "Date" : "Description") : " Date");

                string width01 = (colhead1 == "NONE" || colhead1 == "BRANCHBYDAY" ? (colhead1 == "NONE" ? (combrns.ToString().Substring(2) != "00" ? "Width140" : "Width110") : "Width140") : (colhead1 == "BRANCHSUMM" ? "Width140" : (combrns.ToString().Substring(2) != "00" ? "Width140" : "Width110")));
                string width02 = (colhead1 == "DAYBYBRANCH" || colhead1 == "BRANCHSUMM" ? (combrns != "0000" ? "Width110" : " Width130") : "Width100");

                //string header01 = "";
                //string header02 = "";
                //if (colhead1 == "NONE" || colhead1 == "BRANCHBYDAY")
                //{
                //    header01 = "Description";
                //    header02 = "Date";
                //}
                //else
                //{
                //    header01 = "Date";
                //    header02 = "Description";
                //}
                #endregion
                #region Datagrid Columns Design
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol(header01, "grp1desc", width: width01.Trim()));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol(header02, "grp2desc", width: width02.Trim()));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("New Invoice\nQuantity", "invqty", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Gross Sales\nAmount", "gsalam", "SFormat", "TxtRight", "Width85"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Discount\nAmount", "disam", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Net Sales\nAmount", "nsalam", "SFormat", "TxtRight", "Width85"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Collection\nAmount", "collam", "SFormat", "TxtRight", "Width90"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Due Amount", "dueam", "SFormat", "TxtRight", "Width90"));
                //dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Ren. Day", "recndt", "Date", width: "Width80"));
                #endregion
                dg1.ItemsSource = RptList;
                return dg1;
            }
        }

        public class DiscountList1 // "A04. Ref. wise dues details" ,"A07. Discount reference list" AND "A08. Dues reference list" 
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.FDeskDiscount01> RptList)
            {
                #region Datagrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 930;
                dg1.RowHeaderWidth = 0;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 440;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;  
                dg1.FrozenColumnCount = 9;
                #endregion
                #region Header Design
                foreach (var item in RptList)
                {
                    item.refstaffnam = item.refstaffnam + " " + item.ptinvnote;
                }
                #endregion
                #region Datagrid Columns Design
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Invoice\nDate & Time", "invdat", "Date1", "TxtCenter", "Width100"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("User Name", "username", width: "Width80"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Invoice No.", "ptinvnum2", style1: "TxtCenter", width: "Width90"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Patient Name", "ptname", width: "Width150"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Total\nAmount", "totam", "SFormat2", "TxtRight", "Width55"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Discount\nAmount", "disam", "SFormat2", "TxtRight", "Width55"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Net Bill\nAmount", "netam", "SFormat2", "TxtRight", "Width55"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Last Coll.\nDate & Time", "coldat", "Date1", "TxtCenter", "Width100"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("New Coll\nAmount", "collam", "SFormat2", "TxtRight", "Width55"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Due\nAmount", "dueam", "SFormat2", "TxtRight", "Width55"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Delay\n(Day)", "daydiff", "SFormat2", "TxtRight", "Width40"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Discount refrerence", "refstaffnam", width: "Width250"));
                #endregion
                dg1.ItemsSource = RptList;
                return dg1;
            }
        }
        public class CCChargeListc1
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.FDeskCollSumm01> RptList)
            {
                #region Datagrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 930;
                dg1.RowHeaderWidth = 0;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 440;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;  
                #endregion
                #region Header Design

                #endregion
                #region Datagrid Columns Design
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Invoice Date & Time", "coldat", "Date1", "TxtCenter", "Width120"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("User Name", "username", width: "Width100"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Invoice No.", "ptinvnum2", style1: "TxtCenter", width: "Width95"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Patient Name", "ptname", width: "Width200"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("CC Amount", "totam", "SFormat2", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("CC Paid Amount", "ncolam", "SFormat2", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Balance Amount", "ocolam", "SFormat2", "TxtRight", "Width80"));
                #endregion
                dg1.ItemsSource = RptList;
                return dg1;
            }
        }
        public class CollDuesSum01
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.CommInvSummInf> RptList)
            {
                #region Datagrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 930;
                dg1.RowHeaderWidth = 0;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 440;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;  
                #endregion
                #region Header Design

                #endregion
                #region Datagrid Columns Design
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Sl #", "slnum", "SFormat2", "TxtRight", "Width30"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Invoice #", "ptinvnum2", style1: "TxtCenter", width: "Width110"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Due Amount", "tdueam", "SFormat2", "TxtRight", "Width80"));
                #endregion
                dg1.ItemsSource = RptList;
                return dg1;
            }
        }
        public class InvWiseColl
        {
            public static DataGrid GetDataGrid(List<HmsEntityCommercial.FDeskCollSumm01> RptList)
            {
                #region Datagrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                //dg1.AlternatingRowBackground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFE4F2E1")); 
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 930;
                dg1.RowHeaderWidth = 0;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 440;
                //dg1.ColumnHeaderStyle
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;  
                #endregion
                #region Header Design

                #endregion
                #region Datagrid Columns Design
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Collection\nDate & Time", "coldat", "Date1", "TxtCenter", "Width105"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Invoice\nDate & Time", "invdat", "Date1", "TxtCenter", "Width105"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("User Name", "username", width: "Width80"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Invoice No.", "ptinvnum2", style1: "TxtCenter", width: "Width85"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Patient Name", "ptname", width: "Width200"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Total\nAmount", "totam", "SFormat2", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Discount\nAmount", "disam", "SFormat2", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Net Bill\nAmount", "netam", "SFormat2", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("New Coll\nAmount", "ncolam", "SFormat2", "TxtRight", "Width60"));
                dg1.Columns.Add(GridReportFrontDesk01.GetTCol("Due Coll\nAmount", "ocolam", "SFormat2", "TxtRight", "Width60"));
                #endregion
                dg1.ItemsSource = RptList;
                return dg1;
            }
        }
    }
}
