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

namespace ASITHmsWpf.Manpower
{
    class GridReportHCM1
    {
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
                    format2 = "hh:mm tt";
                    break;
                case "Date2":
                    format2 = "dd-ddd";
                    break;
                case "SFormat":
                    format2 = "##0;-##0; ";
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

        private static DataGridColumn GetTCol1(string header1 = "No Header", string binding1 = "", string format1 = "", string style1 = "", string width = "")
        {
            var MyStyle = new Style(typeof(DataGridCell))
            {
                Setters = {
                    new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right)
                }
            };
            var MyStyle1 = new Style(typeof(DataGridCell))
            {
                Setters = {
                    new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center)
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
                case "Date1":
                    format2 = "hh:mm tt";
                    break;
                case "Date2":
                    format2 = "dd-ddd";
                    break;
                case "SFormat":
                    format2 = "#,##0.00;(#,##0.00); ";
                    break;
                case "SFormat2":
                    format2 = "##0.00;X; ";
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
                case "TxtCenter":
                    style2 = MyStyle1;
                    break;
            }

            DataGridColumn dc1 = new DataGridTextColumn { Header = header1, Binding = new Binding(binding1) { StringFormat = format2 }, CellStyle = style2, Width = width1 };
            return dc1;
        }
        public class IndAttndance01
        {
            public static DataGrid GetDataGrid(List<HmsEntityManpower.HcmDayWiseAttanReport> attanReportList)
            {
                foreach (var item in attanReportList)
                {
                    //= iif(Fields!OutTime1.Value <> Fields!InTime2.Value, Fields!OutTime1.Value, "")
                    item.OutTime1 = item.OutTime1 != item.InTime2 ? item.OutTime1 : "";
                    //= iif(Fields!OutTime1.Value <> Fields!InTime2.Value, Fields!InTime2.Value, "")
                    item.InTime2 = item.OutTime1 != item.InTime2 ? item.InTime2 : "";

                }
                #region DataGrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 430;
                dg1.FrozenColumnCount = 6;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                   
                #endregion

                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                

                dg1.Columns.Add(GridReportHCM1.GetTCol("Emp ID", "staffid", width: "Width60"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Name & Designation", "hcnamdsg", width: "Width320"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Date", "attndate", "Date", width: "Width90"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("In Time", "InTime1", width: "Width70"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Brk. Start", "OutTime1", width: "Width70"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Brk. End", "InTime2", width: "Width70"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("OutTime", "OutTime2", width: "Width80"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Actual Machine Responses", "atndtl", width: "Width250"));

                dg1.ItemsSource = attanReportList;
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
        public class AttenSchedule01
        {
            public static DataGrid GetDataGrid(List<HmsEntityManpower.RptAttnSchInfo> AttnSchInfoList)
            {
                foreach (var item in AttnSchInfoList)
                {
                    //item.attndate =DateTime.Parse(item.attndate.ToString("dd-ddd"));
                    ////=IIf(left(Fields!attnstatid.Value, 9) = "SIHA00501", Format(Fields!intime1.Value,"hh:mm tt"), "")   
                    ////item.intime1 = item.attnstatid.Substring(0, 9) == "SIHA00501" ? item.intime1 : null);
                    ////=IIf(left(Fields!attnstatid.Value, 9) = "SIHA00501" and (Fields!outtime1.Value <> Fields!intime2.Value), Format(Fields!outtime1.Value,"hh:mm tt"), "")
                    //item.outtime1 = DateTime.Parse(item.attnstatid.Substring(0, 9) == "SIHA00501" & item.outtime1 != item.intime2 ? item.intime1.ToString("hh:mm tt") : "");
                    ////=IIf(left(Fields!attnstatid.Value, 9) = "SIHA00501" and (Fields!outtime1.Value <> Fields!intime2.Value), Format(Fields!intime2.Value,"hh:mm tt"), "")
                    //item.intime2 = DateTime.Parse(item.attnstatid.Substring(0, 9) == "SIHA00501" & item.outtime1 != item.intime2 ? item.intime2.ToString("hh:mm tt") : "");
                    ////=IIf(left(Fields!attnstatid.Value, 9) = "SIHA00501", Format(Fields!outtime2.Value,"hh:mm tt"), "")
                    //item.outtime2 = DateTime.Parse(item.attnstatid.ToString().Substring(0, 9) == "SIHA00501" ? item.outtime2.ToString("hh:mm tt") : "");
                    ////=iif(Fields!actworkhr.Value < 0, "X", Format(Fields!actworkhr.Value, "##0.00;X; "))
                    //item.actworkhr = item.actworkhr < 0 ? "X" : item.actworkhr;
                }
                #region DataGrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 430;
                dg1.FrozenColumnCount = 12;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                   
                #endregion

                dg1.Columns.Add(GridReportHCM1.GetTCol("Date \n&\tDay", "attndate", "Date2", width: "Width60"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Attandence \n Status\n Description", "attnstat", width: "Width120"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Office \n Start\n Time", "intime1", "Date1", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Begin\n Time", "outtime1", "Date1", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("End\n Time", "intime2", "Date1", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Dur.\n Hour", "breakhr", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("End\n Time", "outtime2", "Date1", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Sch\n Hour", "schworkhr", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Act.\n Hour", "actworkhr", "SFormat2", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Less\n Hour", "lesworkhr", "SFormat2", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("O.T.\n Hour", "otworkhr", "SFormat2", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("L.In\n Min", "latein", "SFormat2", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("E.Out\n Min", "earlyout", "SFormat2", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Remarks", "attnrmrk", width: "Width250"));

                dg1.ItemsSource = AttnSchInfoList;
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

        public class MonthAttenSchedule01
        {
            public static DataGrid GetDataGrid(List<HmsEntityManpower.HcmMonthAttnEvalReport01> MonthlyAttnSum01)
            {

                #region DataGrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 430;
                dg1.FrozenColumnCount = 5;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                   
                dg1.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                   
                #endregion

                dg1.Columns.Add(GridReportHCM1.GetTCol("Emp. ID", "hccode", width: "Width100"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Name of Employee & Designation", "hcnamdsg", width: "Width350"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Office", "offwrkday", "SFormat", "TxtRight", width: "Width60"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Schedul", "schwrkday", "SFormat", "TxtRight", width: "Width60"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Day-Off", "dayoffgen", "SFormat", "TxtRight", width: "Width60"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("On-Leave", "dayoffleav", "SFormat", "TxtRight", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Total-Off", "dayofftotal", "SFormat", "TxtRight", width: "Width60"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Day", "sabsentday", "SFormat", "TxtRight", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Total", "spresentday", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Error", "spresentday", "SFormat2", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Confirm", "cpresentday", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Schedul", "cschwrkhour", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Actual", "cactwrkhour", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("OT /Less", "cactwrkhour", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Point", "cactlateday", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Hour", "cactlatehour", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Point", "cacteoutday", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Hour", "cacteouthour", "SFormat", "TxtCenter", width: "Width50"));

                dg1.ItemsSource = MonthlyAttnSum01;
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

        public class LeaveDetails01
        {
            public static DataGrid GetDataGrid(List<HmsEntityManpower.HcmLeaveDetailsReport01> LeaveDetList)
            {
                foreach (var item in LeaveDetList)
                {
                    //=Fields!begndat1.Value + iif(Len(Trim(Fields!begndat1.Value)) =0, "", " To ") + Fields!enddat1.Value
                    item.begndat1 = item.begndat1 + (item.begndat1.Trim().Length == 0 ? "" : " To " + item.enddat1);
                }

                #region DataGrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 430;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;// Visibility.Visible;                     
                #endregion

                dg1.Columns.Add(GridReportHCM1.GetTCol("L.ID", "leavid", width: "Width40"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Description", "lreason", width: "Width200"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Leave Period", "begndat1", width: "Width200"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Day(s)", "totday", "SFormat", "TxtRight", width: "Width60"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Apply Date", "submitdat1", width: "Width110"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Approve Date", "aprvdat1", width: "Width110"));

                dg1.ItemsSource = LeaveDetList;
                dg1.Items.Refresh();

                return dg1;

            }
        }

        public class MonthlySalarySheet01
        {
            public static DataGrid GetDataGrid(List<HmsEntityManpower.Payslip001> MonthlySalaryList)
            {

                #region DataGrid Design
                DataGrid dg1 = new DataGrid();
                dg1.AutoGenerateColumns = false;
                dg1.AlternatingRowBackground = (Brush)new BrushConverter().ConvertFrom("#FFE4F2E1");
                dg1.IsReadOnly = true;
                dg1.CanUserSortColumns = false;
                dg1.Width = 915;
                dg1.HorizontalAlignment = HorizontalAlignment.Center;
                dg1.Height = 430;
                dg1.FrozenColumnCount = 7;
                dg1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                   
                dg1.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;// Visibility.Visible;                   
                #endregion

                dg1.Columns.Add(GridReportHCM1.GetTCol("Emp. ID", "hccode", width: "Width100"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Name of Employee & Designation", "hcname", width: "Width250"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Work \nDay", "workdays", "SFormat", "TxtRight", width: "Width40"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Pay \nDay", "paydays", "SFormat", "TxtRight", width: "Width40"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Conso. \nPay", "saladd01", "SFormat", "TxtRight", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Other \nAllow.", "saladd02", "SFormat", "TxtRight", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Adj. \nPay", "saladd08", "SFormat", "TxtRight", width: "Width60"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Gross \nPay", "grosspay", "SFormat", "TxtRight", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Abs.\n&\nLate ", "salded01", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("P.F", "salded02", "SFormat", "TxtCenter", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("P.F\n Loan", "salded03", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("P.F\nInter.", "salded04", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Inc.\nTax.", "salded05", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Adv.\nSalary ", "salded07", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Other\nDedu.", "salded08", "SFormat", "TxtCenter", width: "Width50"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Total\nDedu.", "salded10", "SFormat", "TxtCenter", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Net\nPay", "netpay", "SFormat", "TxtCenter", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Cash", "cashpay", "SFormat", "TxtCenter", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Bank", "bankpay", "SFormat", "TxtCenter", width: "Width65"));
                dg1.Columns.Add(GridReportHCM1.GetTCol("Sign", "sfassd", "SFormat", "TxtCenter", width: "Width50"));

                dg1.ItemsSource = MonthlySalaryList;
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
