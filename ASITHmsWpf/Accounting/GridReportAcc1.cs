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


namespace ASITHmsWpf.Accounting
{
    public class GridReportAcc1
    {

        public GridReportAcc1()
        {

        }
        #region This method using DataGridTextColumn
        //private static DataGridColumn GetCol(string header1 = "No Header", string binding1 = "", string format1 = "", string style1 = "", string width = "")
        //{
        //    var MyStyle = new Style(typeof(DataGridCell))
        //    {
        //        Setters = {
        //            new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right)
        //        }
        //    };

        //    string format2 = "";
        //    int width1 = 0;
        //    var style2 = new Style();
        //    switch (format1)
        //    {
        //        case "Date":
        //            format2 = "dd-MMM-yyyy";
        //            break;
        //        case "SFormat":
        //            format2 = "#,##0;(#,##0); ";
        //            break;
        //    }
        //    switch (width)
        //    {
        //        case "Width":
        //            width1 = 300;
        //            break;
        //        default:
        //            if (width == "")
        //                break;

        //            width1 = Convert.ToInt32(width.Substring(5));
        //            break;
        //    }
        //    switch (style1)
        //    {
        //        case "TxtRight":
        //            style2 = MyStyle;
        //            break;
        //    }

        //    DataGridColumn dc1 = new DataGridTextColumn { Header = header1, Binding = new Binding(binding1) { StringFormat = format2 }, CellStyle = style2, Width = width1 };
        //    return dc1;
        //}
        #endregion

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

            DataTemplate headertemplate1 = new DataTemplate();
            var textBlock1 = new FrameworkElementFactory(typeof(TextBlock));
            textBlock1.SetValue(TextBlock.TextProperty, header1);
            headertemplate1.VisualTree = textBlock1;

            DataTemplate cellTemplate1 = new DataTemplate();
            var textBlock2 = new FrameworkElementFactory(typeof(TextBlock));
            textBlock2.SetBinding(TextBlock.TextProperty, new Binding(binding1) { StringFormat = format2 });
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
        private static DataGrid GetDGrid()
        {
            DataGrid dg1 = new DataGrid();
            dg1.AutoGenerateColumns = false;
            dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
            dg1.IsReadOnly = true;
            dg1.CanUserSortColumns = false;
            dg1.Width = 910;
            dg1.HorizontalAlignment = HorizontalAlignment.Center;
            dg1.Height = 410;
            dg1.RowHeaderWidth = 0;
            dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;   
            return dg1;
        }
        public class ViewTranList1
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccTransectionList> AccTrnLst)
            {
                #region Datagrid

                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. Date", "voudat", "Date", "TxtCenter", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. No.", "vounum1", style1: "TxtCenter", width: "Width110"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Voucher Ref.", "vouref", width: "Width200"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Cheque Ref.", "chqref", width: "Width100"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Naration", "vounar", width: "Width330"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Status", "vstatus", width: "Width40"));
                dg1.ItemsSource = AccTrnLst;
                dg1.Items.Refresh();
                return dg1;
                #endregion
            }
        }
        public class ViewTranList2
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccLedger1> AccTrnLst2)
            {
                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. Date", "voudat", "Date", "TxtCenter", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. No.", "vounum1", style1: "TxtCenter", width: "Width110"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Code", "trcode", style1: "TxtCenter", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Description", "trdesc", width: "Width"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Inner", "inram", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Debit", "dram", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Credit", "cram", "SFormat", "TxtRight", "Width70"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Ren. Day", "recndt", "Date", "TxtCenter", "Width75"));
                dg1.ItemsSource = AccTrnLst2;
                dg1.Items.Refresh();
                #region this block line using group column,this is on test
                //ICollectionView cvTasks = CollectionViewSource.GetDefaultView(dgOverall01.ItemsSource);
                //if (cvTasks != null && cvTasks.CanGroup == true)
                //{
                //    cvTasks.GroupDescriptions.Clear();
                //    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("voudat", new RelativeDateValueConverter()));
                //    cvTasks.GroupDescriptions.Add(new PropertyGroupDescription("vounum1"));
                //}
                #endregion
                return dg1;

            }
        }
        public class CashBookTranList1
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccCashBook1> CashBookTransList)
            {

                foreach (var item in CashBookTransList)
                {
                    item.cactdesc = item.actdesc.Trim() == "" & item.sirdesc.Trim() == "" ? item.cactdesc.Trim() : "" + item.sirdesc.Trim() == "" ?
                        "->" + item.actdesc.Trim() : "-->" + item.sirdesc.Trim();
                    item.grp2 = (item.grp1 == "00" || item.grp1 == "99" || item.vounum.Trim().Length == 0 ? "" :
                       item.vounum.Substring(0, 3) + item.vounum.Substring(7, 2) + "-" + item.vounum.Substring(9, 4) + "-" + item.vounum.Substring(13));
                    // =IIF(Fields!grp1.Value="00" or Fields!grp1.Value="99" or Len(Trim(Fields!vounum.Value))=0 , "", 
                    //left(Fields!vounum.Value, 3) + mid(Fields!vounum.Value, 8,2) + "-"+ mid(Fields!vounum.Value, 10,4) + "-" + right(Fields!vounum.Value, 5))
                }
                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. Date", "voudat", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. No.", "grp2", width: "Width120")); // computed from  vounum
                dg1.Columns.Add(GridReportAcc1.GetTCol("Code", "cactcode", width: "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Description", "cactdesc", width: "Width250"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Inner", "inram", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Cash", "casham", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Bank", "bankam", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Total", "cumam", "SFormat", "TxtRight", "Width80"));
                dg1.ItemsSource = CashBookTransList;
                dg1.Items.Refresh();
                return dg1;

            }
        }
        public class AccRecPay
        {
            public static DataGrid GetDataGrid(object RecPaydtset)
            {
                var ds1 = (DataSet)RecPaydtset;
                var List1 = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccRecPay1>();

                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Account Head (Receipt)", "actdesc1", width: "Width340"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Rec./In Amount", "amt1", "SFormat", "TxtRight", "Width100"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Head Of Account (Payment)", "actdesc2", style1: "TxtLeft", width: "Width340"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Pay./Out Amount", "amt4", "SFormat", "TxtRight", "Width100"));
                dg1.ItemsSource = List1;
                dg1.Items.Refresh();
                return dg1;

            }
        }
        public class AccLedger
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccLedger1> accLedgerLst)
            {

                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. Date", "voudat", "Date", "TxtCenter", "Width75"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. No.", "vounum1", style1: "TxtCenter", width: "Width110"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Description", "trdesc", width: "Width270"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Inner", "inram", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Debit", "dram", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Credit", "cram", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Balance", "blancam", "SFormat", "TxtRight", "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Ren. Day", "recndt", "Date", width: "Width80"));
                dg1.ItemsSource = accLedgerLst;
                dg1.Items.Refresh();

                return dg1;

            }
        }

        public class TrialBalance
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccTrialBalance1> AccTrialBlncLst)
            {
                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Code", "actcode", style1: "TxtCenter", width: "Width100"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Description", "actdesc", width: "Width350"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Opening", "opnam", "SFormat", "TxtRight", "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Debit", "curdr", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Credit", "curcr", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Net", "curam", "SFormat", "TxtRight", "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Closing", "clsam", "SFormat", "TxtRight", "Width90"));
                dg1.ItemsSource = AccTrialBlncLst;
                dg1.Items.Refresh();
                return dg1;

            }
        }
        public class IncomeStatement
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccIncomeStatement1> AccIncomeStLst)
            {
                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Description", "actdesc", width: "Width400"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Current Period", "closam", "SFormat", "TxtRight", "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Previous Period", "closam_p", "SFormat", "TxtRight", "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Difference", "varam", "SFormat", "TxtRight", "Width90"));
                dg1.ItemsSource = AccIncomeStLst;
                dg1.Items.Refresh();
                return dg1;

            }
        }
        public class PayProTranList1
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccLedger1> AccTrnLst2)
            {

                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. Date", "voudat", "Date", "TxtCenter", "Width75"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. No.", "vounum1", style1: "TxtCenter", width: "Width110"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Code", "trcode", style1: "TxtCenter", width: "Width85"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Description", "trdesc", width: "Width"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Inner", "inram", "SFormat", "TxtRight", "Width75"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Debit", "dram", "SFormat", "TxtRight", "Width75"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Credit", "cram", "SFormat", "TxtRight", "Width75"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Ren. Day", "recndt", "Date", width: "Width75"));
                dg1.ItemsSource = AccTrnLst2;
                dg1.Items.Refresh();

                //}
                return dg1;

            }
        }//PayPropTrnLst2
        public class InterCompLoanStatus1
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccIntComLoanStat1> InterComLoanStat1)
            {

                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. Date", "voudat", "Date", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Vou. No.", "vounum1", width: "Width110"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Control Acoount Head", "cactdesc", width: "Width180"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Sub Head", "sirdesc", width: "Width140"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Received", "lonrcv", "SFormat", "TxtRight", width: "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Paid", "lonpay", "SFormat", "TxtRight", width: "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Net Position", "netlon", "SFormat", "TxtRight", width: "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Narration", "vounar", width: "Width110"));
                dg1.ItemsSource = InterComLoanStat1;
                dg1.Items.Refresh();

                return dg1;
            }
        }
        public class InterCompLoanSum1
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.AccIntComLoanSum1> InterComLoanSum1)
            {

                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("Name of Company", "actdesc", width: "Width170"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Received", "opnrcv", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Paid", "opnpay", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Net Postion", "opnnet", "SFormat", "TxtRight", width: "Width80"));

                dg1.Columns.Add(GridReportAcc1.GetTCol("Received", "currcv", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Paid", "curpay", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Net Postion", "curnet", "SFormat", "TxtRight", width: "Width80"));

                dg1.Columns.Add(GridReportAcc1.GetTCol("Received", "clsrcv", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Paid", "clspay", "SFormat", "TxtRight", width: "Width80"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Net Postion", "clsnet", "SFormat", "TxtRight", width: "Width80"));

                dg1.ItemsSource = InterComLoanSum1;
                dg1.Items.Refresh();

                return dg1;
            }
        }

        public class PayPropTrnLst
        {
            public static DataGrid GetDataGrid(List<HmsEntityAccounting.PayProTransectionList2> PayPropTrnLst01)
            {
                foreach (var item in PayPropTrnLst01)
                {
                    item.actcode = item.actcode.Trim() == "000000000000" ? item.actcode : item.sircode.Trim() != "000000000000" ? item.sircode : item.actcode;
                }

                DataGrid dg1 = GetDGrid();

                dg1.Columns.Add(GridReportAcc1.GetTCol("A/C Code", "actcode", style1: "TxtCenter", width: "Width100"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Head of Accounts", "trnDesc", width: "Width400"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Prop. Bgd", "bppam", "SFormat", "TxtRight", width: "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Aprv. Bgd", "bapam", "SFormat", "TxtRight", width: "Width90"));
                dg1.Columns.Add(GridReportAcc1.GetTCol("Paid Amt", "payam", "SFormat", "TxtRight", width: "Width90"));
                dg1.ItemsSource = PayPropTrnLst01;
                dg1.Items.Refresh();

                return dg1;
            }
        }

    }
}