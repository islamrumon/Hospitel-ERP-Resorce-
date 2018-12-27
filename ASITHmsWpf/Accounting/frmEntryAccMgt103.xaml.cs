using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan.Accounting;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using ASITHmsRpt1GenAcc.Accounting;
using System.ComponentModel;
using ASITHmsViewMan.General;


namespace ASITHmsWpf.Accounting
{
    /// <summary>
    /// Interaction logic for frmEntryAccMgt103.xaml
    /// </summary>
    public partial class frmEntryAccMgt103 : UserControl
    {

        private List<HmsEntityGeneral.AcInfCodeBook> CactcodeList = new List<HmsEntityGeneral.AcInfCodeBook>();

        private List<HmsEntityAccounting.AccChequeIssueToBank1> VoucherListTable = new List<HmsEntityAccounting.AccChequeIssueToBank1>();
        private List<HmsEntityAccounting.AccChequeIssueToBank1> VoucherListPrint = new List<HmsEntityAccounting.AccChequeIssueToBank1>();
        private vmEntryAccMgt1 vm1 = new vmEntryAccMgt1();
        public frmEntryAccMgt103()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            this.Objects_On_Init();  
        }

        private void Objects_On_Init()
        {
            this.xctk_dtpFrom.Value = DateTime.Today.AddDays(-7);
            this.xctk_dtpTo.Value = DateTime.Today;
            if (WpfProcessAccess.AccCodeList == null)
                WpfProcessAccess.GetAccCodeList();

            this.CactcodeList = WpfProcessAccess.AccCodeList.FindAll(x => (x.actcode.Substring(0, 4) == "1901" || x.actcode.Substring(0, 4) == "1902" || x.actcode.Substring(0, 4) == "2902") && (x.actcode.Substring(8, 4) != "0000"));

            this.CactcodeList.Sort(delegate(HmsEntityGeneral.AcInfCodeBook x, HmsEntityGeneral.AcInfCodeBook y)
            {
                return x.actdesc.CompareTo(y.actdesc);
            });
            this.cmbChqBank.Items.Clear();
            foreach (var itemb in CactcodeList)
            {
                if (itemb.actcode.Substring(0, 4) == "1902" || itemb.actcode.Substring(0, 4) == "2902")
                    this.cmbChqBank.Items.Add(new ComboBoxItem() { Content = itemb.actdesc, Tag = itemb.actcode });
            }
            this.cmbChqBank.SelectedIndex = 0;
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnShowVoucher_Click(object sender, RoutedEventArgs e)
        {
            string Date1 = this.xctk_dtpFrom.Text.Trim();
            string Date2 = this.xctk_dtpTo.Text.Trim();
            string BankId = ((ComboBoxItem)this.cmbChqBank.SelectedItem).Tag.ToString();
            var pap1 = vm1.SetParamShowChequeIssueLetter(WpfProcessAccess.CompInfList[0].comcod, StartDate: Date1, EndDate: Date2, BankAcCode: BankId);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;
            this.VoucherListTable = ds1.Tables[0].DataTableToList<HmsEntityAccounting.AccChequeIssueToBank1>();
            foreach (var item in this.VoucherListTable)
                item.aminw = ASITFunLib.ASITUtility.Trans(double.Parse(item.trnam.ToString()), 0);

            this.dgVouList1.ItemsSource = this.VoucherListTable;
        }

        private void btnPrintLetter_Click(object sender, RoutedEventArgs e)
        {
            this.VoucherListPrint = this.VoucherListTable.FindAll(x => x.Mark1 == true).ToList();
            if(this.VoucherListPrint.Count==0)
            {
                return;
            }
            int i = 1;
            foreach (var item in this.VoucherListPrint)
            {
                item.slnum = i;
                ++i;
            }

            LocalReport rpt1 = null;

            string BankId = ((ComboBoxItem)this.cmbChqBank.SelectedItem).Tag.ToString();
            var vmGenList1 = new vmHmsGeneralList1();
            var pap1 = vmGenList1.SetParamGenDetailsInf(WpfProcessAccess.CompInfList[0].comcpcod, "ACINF", BankId, "ACBD");
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
                return;


            var BankDetailsInfo = ds1.Tables[0].DataTableToList<vmHmsGeneralList1.GenDetailsListInfo>();
            
            string BankName = "", BranchName = "", BankAcNum = "", Address1 = "", Address2 = "";
            foreach (var item in BankDetailsInfo)
            {
                if (item.gencode == "ACBD00101001" && item.repeatsl == "1") 
                    BankName = item.dataval.Trim();
                else if(item.gencode == "ACBD00101002" && item.repeatsl == "1") 
                    BranchName = item.dataval.Trim();
                else if (item.gencode == "ACBD00101003" && item.repeatsl == "1")
                    Address1 = item.dataval.Trim();
                else if (item.gencode == "ACBD00101003" && item.repeatsl == "2")
                    Address2 = item.dataval.Trim();
                else if (item.gencode == "ACBD00101006" && item.repeatsl == "1")
                    BankAcNum = item.dataval.Trim();
            }

            Hashtable list2 = new Hashtable();
            list2["LetDate"] = "Date : " + DateTime.Today.ToString("dd-MMM-yyyy");
            list2["BankName"] = BankName;
            list2["BranchName"] = BranchName;
            list2["Address1"] = Address1;
            list2["Address2"] = Address2;
            list2["BankAcNum"] = BankAcNum;

            string WindowTitle1 = "Cheque Issue Letter to Bank";

            var list3 = WpfProcessAccess.GetRptGenInfo();

            rpt1 = AccReportSetup.GetLocalReport("Accounting.RptChqIssuLetter1", this.VoucherListPrint, list2, list3, null);

            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);

        }
    }
}
