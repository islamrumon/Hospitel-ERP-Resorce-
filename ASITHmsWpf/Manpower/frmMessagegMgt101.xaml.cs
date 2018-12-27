using ASITFunLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ASITHmsViewMan.Manpower;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using ASITHmsEntity;
using System.Net;

namespace ASITHmsWpf.Manpower
{
    /// <summary>
    /// Interaction logic for frmMessagegMgt101.xaml
    /// </summary>
    public partial class frmMessagegMgt101 : UserControl
    {
        // Operation to be set based on cmbViaSMS -- Hafiz 16-Nov-2018
        string TitaleTag1, TitaleTag2;

        private bool FrmInitialized = false;
        private List<ASITHmsEntity.HmsEntityManpower.MessageInfo> MessageList1 = new List<ASITHmsEntity.HmsEntityManpower.MessageInfo>();
        private List<vmMessagegMgt1.SMSRecipient> RecipientList = new List<vmMessagegMgt1.SMSRecipient>();
        private List<vmMessagegMgt1.SMSRecipient> DraftRecipientList = new List<vmMessagegMgt1.SMSRecipient>();

        private vmMessagegMgt1 vm1 = new vmMessagegMgt1();

        private vmReportHCM1 vm1r = new vmReportHCM1();

        public frmMessagegMgt101()
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

            if (!this.FrmInitialized)
            {
                this.ActivateAuthObjects();
                this.FrmInitialized = true;
                this.xctk_dtpFrom.Value = DateTime.Today.AddDays(-15);
                this.xctk_dtpTo.Value = DateTime.Today;
                this.stkpSmsData.Visibility = Visibility.Hidden;
                this.GridRecpList.Visibility = Visibility.Collapsed;
                this.lstRecp.Items.Clear();
                this.stkpSmsData.IsEnabled = false;
                this.btnExeAutoSendSMS.Visibility = Visibility.Hidden;
                this.dgvRecpList.Visibility = Visibility.Collapsed;
                if (WpfProcessAccess.StaffList == null)
                    WpfProcessAccess.GetCompanyStaffList();
            }
        }

        private void ActivateAuthObjects()
        {
            try
            { 
                if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmMessagegMgt101_cmbViaSMS_SMARTPHONE") == null)
                    this.cmbViaSMS.SelectedIndex = 1;
                else if (WpfProcessAccess.SignedInUserAuthList.Find(x => x.uicode == "WPF_frmMessagegMgt101_cmbViaSMS_WEBSERVICE") == null)
                    this.cmbViaSMS.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show("HCH-SMS-01: " + exp.Message.ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void btnShowSMS_Click(object sender, RoutedEventArgs e)
        {
            if (this.btnShowSMS.Content.ToString().Contains("Clear SMS"))
            {
                this.lblTitle1.Content = "Message";
                this.lblTitle1.Tag = "Visible";
                this.lblTitle1.Visibility = Visibility.Collapsed;
                //this.lblMsgNum1.Content = "";
                //this.lblMsgNum1.Tag = "";
                this.cmbMsgStatus.Visibility = Visibility.Visible;
                this.stkpSmsData.Visibility = Visibility.Hidden;
                this.stkpSmsData.IsEnabled = false;
                this.stkpDateRange.IsEnabled = true;
                this.btnShowSMS.Content = "Show SMS _List";
                return;
            }
            this.RetriveMessages();
            this.btnShowSMS.Content = "Clear SMS _List";
        }
        private void RetriveMessages()
        {
            this.stkpSmsData.Visibility = Visibility.Visible;
            string[] msgstat1 = ((ComboBoxItem)this.cmbMsgStatus.SelectedItem).Tag.ToString().Split(',');
            string OutGoing1a = (msgstat1[0].Contains("OUTGOING") ? msgstat1[0] : "NOTHING");
            string Incoming1a = (msgstat1[0].Contains("INCOMING") ? msgstat1[0] : "NOTHING");
            string msgstatus1a = msgstat1[1];// "A";

            this.stkpSmsData.IsEnabled = false;
            this.dgMsgDetails.ItemsSource = null;

            string StartDate1a = this.xctk_dtpFrom.Text.Trim();
            string EndDate1a = this.xctk_dtpTo.Text.Trim();

            var pap1 = vm1r.SetParamShowMessageDetails(CompCode: WpfProcessAccess.CompInfList[0].comcpcod, StartDate1: StartDate1a, EndDate1: EndDate1a,
                        OutGoing1: OutGoing1a, Incoming1: Incoming1a, msgstatus1: msgstatus1a);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            //DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "JSON");
            if (ds1 == null)
                return;
            this.MessageList1 = ds1.Tables[0].DataTableToList<ASITHmsEntity.HmsEntityManpower.MessageInfo>();
            this.DraftRecipientList = ds1.Tables[1].DataTableToList<vmMessagegMgt1.SMSRecipient>();
            foreach (var item in this.MessageList1)
            {
                string msgnum1 = item.msgnum.Trim();
                string num1 = item.msgtonum.Trim();
                var DraftRecipientList2 = this.DraftRecipientList.FindAll(x => x.msgnum.Trim() == msgnum1).OrderBy(y => y.slnum).ToList();
                foreach (var item2 in DraftRecipientList2)
                {
                    num1 = (num1 == item2.rcvno.Trim() ? num1 : num1 + ", " + item2.rcvno.Trim());
                }
                //var num1 = item.msgtonum.Trim();

                item.msgtonum = num1;
            }



            //this.BuildMessageList();
            this.dgMsgDetails.ItemsSource = this.MessageList1;
            this.stkpSmsData.IsEnabled = true;
            var Title1a = ((ComboBoxItem)this.cmbMsgStatus.SelectedItem).Content.ToString();
            this.lblTitle1.Content = Title1a;
            this.lblTitle1.Tag = (Title1a.Contains("Draft") ? "Visible" : "Collapsed");
            this.lblTitle1.Visibility = Visibility.Visible;
            this.cmbMsgStatus.Visibility = Visibility.Collapsed;
            this.stkpDateRange.IsEnabled = false;
            var conm1 = new ContextMenu();
            var mi1 = new MenuItem() { Tag = "btnCopy", Header = "Copy", ToolTip = "Copy record", FontSize = 14 };
            mi1.Click += MenuGrid_Click;
            conm1.Items.Add(mi1);
            if (Title1a.Contains("Draft"))
            {
                conm1.Items.Add(new Separator());
                var mi2 = new MenuItem() { Tag = "btnEdit", Header = "Edit", ToolTip = "Edit record", FontSize = 14 };
                mi2.Click += MenuGrid_Click;
                conm1.Items.Add(mi2);
                conm1.Items.Add(new Separator());
                var mi3 = new MenuItem() { Tag = "btnDelete", Header = "Cancel", ToolTip = "Cancel record", FontSize = 14 };
                mi3.Click += MenuGrid_Click;
                conm1.Items.Add(mi3);
            }

            this.dgMsgDetails.ContextMenu = conm1;
            this.btnNav_Click(new Button() { Name = "btnTop", Tag = "btnTop" }, null);
        }
        private void btnNav_Click(object sender, RoutedEventArgs e)
        {
            string ActtionName = ((Button)sender).Tag.ToString().Trim();
            this.GridNavigationClick(ActtionName);
        }

        private void MenuGrid_Click(object sender, RoutedEventArgs e)
        {
            string ActtionName = ((MenuItem)sender).Tag.ToString().Trim();
            this.GridNavigationClick(ActtionName);
        }
        private void GridNavigationClick(string ActtionName = "btnTop")
        {
            if (this.dgMsgDetails.Items.Count == 0)
                return;

            if (this.dgMsgDetails.SelectedIndex < 0)
                this.dgMsgDetails.SelectedIndex = 0;

            int index1 = this.dgMsgDetails.SelectedIndex;
            var item1 = (ASITHmsEntity.HmsEntityManpower.MessageInfo)this.dgMsgDetails.SelectedItem;
            switch (ActtionName)
            {
                case "btnTop":
                    index1 = 0;
                    break;
                case "btnPrev":
                    index1 = this.dgMsgDetails.SelectedIndex - 1;
                    if (index1 < 0)
                        index1 = 0;
                    break;
                case "btnNext":
                    index1 = this.dgMsgDetails.SelectedIndex + 1;
                    if (index1 >= this.dgMsgDetails.Items.Count)
                        index1 = this.dgMsgDetails.Items.Count - 1;
                    break;
                case "btnBottom":
                    index1 = this.dgMsgDetails.Items.Count - 1;
                    break;
                case "btnDelete":
                    this.DeleteEditCopyMessage("Delete", index1);
                    break;
                case "btnEdit":
                    this.DeleteEditCopyMessage("Edit", index1);
                    break;
                case "btnCopy":
                    this.DeleteEditCopyMessage("Copy", index1);
                    break;
            }
            if (ActtionName == "btnDelete" || ActtionName == "btnEdit" || ActtionName == "btnCopy")
                return;

            this.dgMsgDetails.SelectedIndex = index1;

            var item21 = (ASITHmsEntity.HmsEntityManpower.MessageInfo)this.dgMsgDetails.Items[index1];
            this.dgMsgDetails.ScrollIntoView(item21);
        }
        private void DeleteEditCopyMessage(string DeleteEditCopy = "Copy", int index1 = 0)
        {
            var item21 = (ASITHmsEntity.HmsEntityManpower.MessageInfo)this.dgMsgDetails.Items[index1];
            if (DeleteEditCopy == "Delete")
            {
                MessageBoxResult msgresult = System.Windows.MessageBox.Show("Are you confirm to cancel the SMS\n" + item21.msgnum1 + ", " + item21.msgtime.ToString("dd-MMM-yyyy hh:mm:ss.fff tt") + "\n\n" + item21.msgbody,
                                  WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
                if (msgresult != MessageBoxResult.Yes)
                    return;
                string msgbody1a = item21.msgbody;
                string msgnum1a = item21.msgnum;
                string msgtime1a = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt"); // item21.msgtime.ToString("dd-MMM-yyyy hh:mm:ss.fff tt");
                string senderid1a = "OUTGOING";
                string receverid1a = item21.msgtonum;
                string preparebyid1a = WpfProcessAccess.SignedInUserList[0].hccode;
                string prepareses1a = WpfProcessAccess.SignedInUserList[0].sessionID;
                string preparetrm1a = WpfProcessAccess.SignedInUserList[0].terminalID;
                string msgstatus1a = "C";
                var pap1 = vm1.SetParmeterToUpdateSMSInfo(comcod: WpfProcessAccess.CompInfList[0].comcpcod, msgbody1: msgbody1a, msgnum1: msgnum1a, msgtime1: msgtime1a,
                           senderid1: senderid1a, receverid1: receverid1a, msgstatus1: msgstatus1a, preparebyid1: preparebyid1a, prepareses1: prepareses1a, preparetrm1: preparetrm1a);

                DataSet ds1s = WpfProcessAccess.GetHmsDataSet(pap1);
                if (ds1s == null)
                {
                    System.Windows.MessageBox.Show("Could not update the message into database\nPlease try again", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                item21.msgrmrk = "Cancelled";
                this.dgMsgDetails.Items.Refresh();
                return;
            }

            this.lblMsgNum1.Content = (DeleteEditCopy == "Edit" ? "Msg. No: " + item21.msgnum1 + ", Time: " + item21.msgtime.ToString("dd-MMM-yyyy hh:mm:ss.fff tt") : "");
            this.lblMsgNum1.Tag = (DeleteEditCopy == "Edit" ? item21.msgnum + "," + item21.msgtime.ToString("dd-MMM-yyyy hh:mm:ss.fff tt") : "");
            this.chkDraft.IsChecked = (DeleteEditCopy == "Edit" ? true : false);
            this.txtMsgToSend.Text = item21.msgbody;
            this.dgvRecpList.ItemsSource = null;
            var recpList1 = this.DraftRecipientList.FindAll(x => x.msgnum == item21.msgnum).OrderBy(y=>y.slnum);
            this.RecipientList.Clear();
            int sl1 = 1;
            foreach (var item in recpList1)
            {
                var recp1 = new vmMessagegMgt1.SMSRecipient() { slnum = sl1, msgnum = item.msgnum, hccode = item.hccode, hcname = item.hcname, rcvno = item.rcvno, smstag = item.smstag };
                this.RecipientList.Add(recp1);
                sl1++;
            }
            this.dgvRecpList.ItemsSource = this.RecipientList;
            if (this.RecipientList.Count > 0)
                this.dgvRecpList.SelectedIndex = 0;
            this.dgvRecpList.Visibility = Visibility.Visible;
            //this.txtRecpCellNo.Text = item21.msgsrid;
        }

        private void btnUpdateSMS_Click(object sender, RoutedEventArgs e)
        {
            var cmbViasms = ((ComboBoxItem)this.cmbViaSMS.SelectedItem).Tag.ToString();
            string msgbody1a = this.txtMsgToSend.Text.Trim();

            string msgnum1a = "MSO001";
            string tag1 = this.lblMsgNum1.Tag.ToString();
            if (tag1.Length > 18)
                msgnum1a = tag1.Substring(0, 18);

            string msgtime1a = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt");
            string senderid1a = "OUTGOING";

            this.RecipientList = this.RecipientList.FindAll(x => x.rcvno.Trim().Length > 0).OrderBy(y => y.slnum).ToList();

            if (msgbody1a.Length == 0 || this.RecipientList.Count == 0)
                return;

            int sl1 = 1;
            foreach (var item in this.RecipientList)
            {
                item.slnum = sl1;
                sl1++;
            }
            #region SMSSEND_PART
            if (cmbViasms == "WEBSERVICE")
            {
                string user = "asitdev";//ASIT";// ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd"; "";// 
                string pass = "asit2018";//A7PLAm55";// ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
                string ApiUrl = "http://codagecorporation.net/sms/index.php/cclapi/messageplatform/sendsms?username=";
                string var_from = "ccl-non-masking";
                string FullMessage1 = msgbody1a + "\nRef: " + WpfProcessAccess.SignedInUserList[0].signinnam + ", " + WpfProcessAccess.SignedInUserList[0].sessionID;
                foreach (var item in this.RecipientList)
                {
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&from=" + var_from + "&to=" + item.rcvno + "&message=" + FullMessage1);
                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                }
            }
            #endregion SMSSEND_PART
            string receverid1a = this.RecipientList[0].rcvno.Trim();// this.txtRecpCellNo.Text.Trim();// "0125252144";
            string preparebyid1a = WpfProcessAccess.SignedInUserList[0].hccode;
            string prepareses1a = WpfProcessAccess.SignedInUserList[0].sessionID;
            string preparetrm1a = WpfProcessAccess.SignedInUserList[0].terminalID;
            string msgstatus1a = (this.chkDraft.IsChecked == true || this.RecipientList.Count == 0 ? "D" : cmbViasms == "WEBSERVICE" ? "A" : "U");

            var pap1 = vm1.SetParmeterToUpdateSMSInfo(comcod: WpfProcessAccess.CompInfList[0].comcpcod, msgbody1: msgbody1a, msgnum1: msgnum1a, msgtime1: msgtime1a,
                senderid1: senderid1a, receverid1: receverid1a, msgstatus1: msgstatus1a,
                RecipientList1: this.RecipientList, preparebyid1: preparebyid1a, prepareses1: prepareses1a, preparetrm1: preparetrm1a);

            DataSet ds1s = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1s == null)
            {
                System.Windows.MessageBox.Show("Could not update the message into database\nPlease try again", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            DataRow dr1 = ds1s.Tables[0].Rows[0];
            this.lblMsgNum1.Content = "Msg. No: " + dr1["msgnum1"].ToString() + ", Time: " + Convert.ToDateTime(dr1["msgtime"]).ToString("dd-MMM-yyyy hh:mm:ss.fff tt");
            this.lblMsgNum1.Tag = dr1["msgnum"].ToString() + "," + Convert.ToDateTime(dr1["msgtime"]).ToString("dd-MMM-yyyy hh:mm:ss.fff tt");

            System.Windows.MessageBox.Show("Successfully updated the message into database", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);

        }
        
        private void btnUpdateSMS_Click_Old(object sender, RoutedEventArgs e) // To be deleted -- Hafiz 18-Nov-2018
        {
            string msgbody1a = this.txtMsgToSend.Text.Trim();
            if (msgbody1a.Length == 0)
                return;

            string msgnum1a = "MSO001";
            string tag1 = this.lblMsgNum1.Tag.ToString();
            if (tag1.Length > 18)
                msgnum1a = tag1.Substring(0, 18);

            string msgtime1a = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt");
            string senderid1a = "OUTGOING";

            this.RecipientList = this.RecipientList.FindAll(x=>x.rcvno.Trim().Length > 0).OrderBy(y => y.slnum).ToList();
            int sl1 = 1;
            foreach (var item in this.RecipientList)
            {
                item.slnum = sl1;
                sl1++;
            }
            string receverid1a = this.RecipientList[0].rcvno.Trim();// this.txtRecpCellNo.Text.Trim();// "0125252144";
            string preparebyid1a = WpfProcessAccess.SignedInUserList[0].hccode;
            string prepareses1a = WpfProcessAccess.SignedInUserList[0].sessionID;
            string preparetrm1a = WpfProcessAccess.SignedInUserList[0].terminalID;
            string msgstatus1a = (this.chkDraft.IsChecked == true || this.RecipientList.Count == 0 ? "D" : "U");

            var pap1 = vm1.SetParmeterToUpdateSMSInfo(comcod: WpfProcessAccess.CompInfList[0].comcpcod, msgbody1: msgbody1a, msgnum1: msgnum1a, msgtime1: msgtime1a,
                senderid1: senderid1a, receverid1: receverid1a, msgstatus1: msgstatus1a,
                RecipientList1: this.RecipientList, preparebyid1: preparebyid1a, prepareses1: prepareses1a, preparetrm1: preparetrm1a);

            DataSet ds1s = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1s == null)
            {
                System.Windows.MessageBox.Show("Could not update the message into database\nPlease try again", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            DataRow dr1 = ds1s.Tables[0].Rows[0];
            this.lblMsgNum1.Content = "Msg. No: " + dr1["msgnum1"].ToString() + ", Time: " + Convert.ToDateTime(dr1["msgtime"]).ToString("dd-MMM-yyyy hh:mm:ss.fff tt");
            this.lblMsgNum1.Tag = dr1["msgnum"].ToString() + "," + Convert.ToDateTime(dr1["msgtime"]).ToString("dd-MMM-yyyy hh:mm:ss.fff tt");

            System.Windows.MessageBox.Show("Successfully updated the message into database", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);

            //select comcod = @ComCod, msgnum = @Desc11, msgnum1 = substring(@Desc11, 1, 3) + substring(@Desc11, 8, 2) + '-' + substring(@Desc11, 10, 9), msgtime = convert(datetime, @Desc02);
            //DataSet ds1s = WpfProcessAccess.GetHmsDataSet(pap1, "PARAMETERS");
            //Message Status ==>  A = Active for read, U = Unsend, D = Draft, C = Canceled
        }
        private void btnClearSMSContent_Click(object sender, RoutedEventArgs e)
        {
            this.GridRecpList.Visibility = Visibility.Collapsed;
            this.dgvRecpList.Visibility = Visibility.Collapsed;
            this.dgvRecpList.ItemsSource = null;
            this.RecipientList.Clear();
            this.chkUnRegRecpnt.IsChecked = false;
            this.txtRecpName.Text = "";
            this.txtMsgToSend.Text = "";
            this.txtRecpCellNo.Text = "";
            this.chkDraft.IsChecked = false;
            this.lblMsgNum1.Content = "";
            this.lblMsgNum1.Tag = "";
            this.txtSMSTag.Text = "";
        }

        private void btnExeAutoSendSMS_Click(object sender, RoutedEventArgs e)
        {

            string Comcpcod1 = WpfProcessAccess.CompInfList[0].comcpcod;
            string Comcod1 = WpfProcessAccess.CompInfList[0].comcod;
            string OutGoing1a = "OUTGOING";
            string Incoming1a = "NOTHING";
            string ReadStatus = "U";
            string StartDate1a = DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy"); // this.xctk_dtpFrom.Text.Trim();
            string EndDate1a = DateTime.Today.ToString("dd-MMM-yyyy");

            DataSet ds1 = WpfProcessAccess.HmsDataService.GetDataSetResultWeb(_comCod: Comcpcod1, _ProcName: "dbo_hcm.SP_REPORT_HCM_TRANS_01", _ProcID: "MSGDETAILS01",
                       _parmXml01: null, _parmXml02: null, _parmBin01: null, _parm01: StartDate1a, _parm02: EndDate1a, _parm03: OutGoing1a, _parm04: Incoming1a, _parm05: ReadStatus,
                       _parm06: "", _parm07: "", _parm08: "", _parm09: "", _parm10: "", _parm11: "", _parm12: "", _parm13: "", _parm14: "", _parm15: "", _parm16: "", _parm17: "",
                       _parm18: "", _parm19: "", _parm20: "", comcod1: Comcod1);

            var oSMSList1 = ds1.Tables[0].DataTableToList<ASITHmsEntity.HmsEntityManpower.MessageInfo>();
            var oRecipientList = ds1.Tables[1].DataTableToList<vmMessagegMgt1.SMSRecipient>();

            return;
            foreach (var item21 in oSMSList1)
            {
                string msgnum1a = item21.msgnum;
                string msgbody1a = item21.msgbody.Trim(); 
                var oRecipientList2 = oRecipientList.FindAll(x => x.msgnum == msgnum1a).OrderBy(y => y.slnum).ToList();
                foreach (var item22 in oRecipientList2)
                {
                    string receverid2a = item22.rcvno.Trim();
                    // Following function tobe written for Xamirin Mobile Device
                    // this.SendSMSThroughMobile(msgbody1a, receverid2a);
                }

                string msgtime1a = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt"); // item21.msgtime.ToString("dd-MMM-yyyy hh:mm:ss.fff tt");
                string senderid1a = "OUTGOING";
                string receverid1a = item21.msgtonum;
                string WriteStatus = "A";
                DataSet ds1u = WpfProcessAccess.HmsDataService.GetDataSetResultWeb(_comCod: Comcpcod1, _ProcName: "dbo_hcm.SP_ENTRY_HCM_TRANS_01", _ProcID: "UPDATE_MESSAGEINF01",
                           _parmXml01: null, _parmXml02: null, _parmBin01: null, _parm01: msgnum1a, _parm02: msgtime1a, _parm03: senderid1a, _parm04: receverid1a, _parm05: WriteStatus,
                           _parm06: "", _parm07: "", _parm08: "", _parm09: "", _parm10: "", _parm11: "", _parm12: "", _parm13: "", _parm14: "", _parm15: "", _parm16: "", _parm17: "",
                           _parm18: "", _parm19: "", _parm20: "", comcod1: Comcod1);
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnAddSMSRecpnt_Click(object sender, RoutedEventArgs e)
        {            
            string phno1 = this.txtRecpCellNo.Text.Trim();
            string recid1 = this.txtRecpName.Tag.ToString();
            string recnam1 = this.txtRecpName.Text.Trim();
            string smstag1 = this.txtSMSTag.Text.Trim();
            this.lstRecp.ItemsSource = null;;
            //this.lstRecp.Items.Clear();
            this.txtRecpCellNo.Text = "";
            this.txtRecpName.Tag = "000000000000";
            this.txtRecpName.Text = "";
            this.txtSMSTag.Text = "";

            string msgnum1 = "";
            string tag1 = this.lblMsgNum1.Tag.ToString();
            if (tag1.Length > 18)
                msgnum1 = tag1.Substring(0, 18);

            if (phno1.Length == 0 || recnam1.Length == 0)
                return;

            this.dgvRecpList.Visibility = Visibility.Visible;

            //this.RecipientList.Clear();
            this.dgvRecpList.ItemsSource = null;
            int sl1 = this.RecipientList.Count + 1;
            var item2 = this.RecipientList.FindAll(x => x.rcvno.Contains(phno1));
            if (item2.Count > 0)
            {
                int sl1a = item2[0].slnum;
                this.dgvRecpList.ItemsSource = this.RecipientList;
                var item21 = (vmMessagegMgt1.SMSRecipient)this.dgvRecpList.Items[sl1a - 1];
                this.dgvRecpList.ScrollIntoView(item21);
                this.dgvRecpList.SelectedItem = item21;
            }
            else
            {
                var item1 = new vmMessagegMgt1.SMSRecipient() { slnum = sl1, msgnum = msgnum1, hccode = recid1, hcname = recnam1, rcvno = phno1, smstag = smstag1 };
                this.RecipientList.Add(item1);

                this.dgvRecpList.ItemsSource = this.RecipientList;
                var item22 = (vmMessagegMgt1.SMSRecipient)this.dgvRecpList.Items[this.dgvRecpList.Items.Count - 1];
                this.dgvRecpList.ScrollIntoView(item22);
                this.dgvRecpList.SelectedItem = item22;
            }
        }

        private void btnRecpRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.dgvRecpList.ItemsSource = null;
            this.RecipientList = this.RecipientList.FindAll(x => x.rcvno.Trim().Length > 0).OrderBy(y => y.slnum).ToList();
            int sl1 = 1;
            foreach (var item in this.RecipientList)
            {
                item.slnum = sl1;
                sl1++;
            }
            this.dgvRecpList.ItemsSource = this.RecipientList;
        }

        private void SelectItemInfo()
        {

            var lbi1 = (HmsEntityGeneral.SirInfCodeBook)this.lstRecp.SelectedItem;

            if (lbi1 == null)
                return;

            this.txtRecpName.Tag = lbi1.sircode;
            this.txtRecpName.Text = lbi1.sirdesc.Trim();
            this.txtRecpCellNo.Text = lbi1.sirtype.Trim();
        }
        private void lstRecp_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SelectItemInfo();
            this.txtSMSTag.Focus();
        }

        private void lstRecp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.SelectItemInfo();
                this.txtSMSTag.Focus();
            }
        }

        private void txtSMSTag_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridRecpList.Visibility = Visibility.Collapsed;
        }

        private void btnAddSMSRecpnt_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridRecpList.Visibility = Visibility.Collapsed;
        }

        private void txtRecpName_GotFocus(object sender, RoutedEventArgs e)
        {
            if(this.chkUnRegRecpnt.IsChecked == false)
                this.GridRecpList.Visibility = Visibility.Visible;
            else
                this.GridRecpList.Visibility = Visibility.Collapsed;
        }

        private void txtRecpName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.GridRecpList.Visibility != Visibility.Visible)
                return;

            this.lstRecp.ItemsSource = null;
            string StrDesc1 = this.txtRecpName.Text.Trim().ToUpper();
            if (StrDesc1.Length == 0)
                return;
            var List1a = WpfProcessAccess.StaffList.FindAll(x => (x.sircode + x.sirdesc.Trim() + x.sirtype.Trim() + x.sirunit.Trim()).ToUpper().Contains(StrDesc1));
            this.lstRecp.ItemsSource = List1a;
        }

        private void cmdAdAllContacts_Click(object sender, RoutedEventArgs e)
        {

            var list01 = (List<HmsEntityGeneral.SirInfCodeBook>)lstRecp.ItemsSource;
            if (list01 == null)
                return;

            this.dgvRecpList.Visibility = Visibility.Visible;
            this.dgvRecpList.ItemsSource = null;
            int sl1 = this.RecipientList.Count + 1;
            foreach (var item in list01)
            {
                var ditem1 = this.RecipientList.FindAll(x => x.rcvno.Trim() == item.sirtype.Trim());
                if (ditem1.Count == 0)
                {
                    var recp1 = new vmMessagegMgt1.SMSRecipient() { slnum = sl1, msgnum = "", hccode = item.sircode, hcname = item.sirdesc, rcvno = item.sirtype, smstag = "" };
                    this.RecipientList.Add(recp1);
                    sl1++;
                }
            }

            this.dgvRecpList.ItemsSource = this.RecipientList;
            var item22 = (vmMessagegMgt1.SMSRecipient)this.dgvRecpList.Items[this.dgvRecpList.Items.Count - 1];
            this.dgvRecpList.ScrollIntoView(item22);
            this.dgvRecpList.SelectedItem = item22;
            this.GridRecpList.Visibility = Visibility.Collapsed;
        }

        private void txtMsgToSend_GotFocus(object sender, RoutedEventArgs e)
        {
            this.GridRecpList.Visibility = Visibility.Collapsed;
        }     
    }
}
