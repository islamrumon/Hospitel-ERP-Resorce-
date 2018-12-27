using ASITHmsViewMan.Manpower;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASITHmsWpf.Marketing
{
    /// <summary>
    /// Interaction logic for frmSMSSedning.xaml
    /// </summary>
    public partial class frmSMSSedning : UserControl
    {
        private List<smsinfo> smsinfolist = new List<smsinfo>();
        private vmMessagegMgt1 vm1 = new vmMessagegMgt1();
        public frmSMSSedning()
        {
            InitializeComponent();
        }
        private void btnSendSms_Click_Old(object sender, RoutedEventArgs e)
        {
            try
            {
                //string comcod = comcode;
                //DataSet ds3 = purData.GetTransInfo(comcod, "SP_UTILITY_LOGIN_MGT", "SHOWAPIINFOFORFORGOTPASS", "", "", "", "", "");
                string user = "asitdev";//ASIT";// ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd";
                string pass = "asit2018";//A7PLAm55";// ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
                //string routeid = "3";// ds3.Tables[0].Rows[0]["apirouid"].ToString().Trim();//3;
                //string typeid = "1"; //ds3.Tables[0].Rows[0]["apitypeid"].ToString().Trim();//1;
                //string senders = "ASITNAHID"; //ds3.Tables[0].Rows[0]["apisender"].ToString().Trim(); //"ASITNAHID";  //Sender

                //string catname = "General";//ds3.Tables[0].Rows[0]["apicatname"].ToString().Trim();//General

                ////string ApiUrl = "http://193.105.74.159/api/v3/sendsms/plain?user=";// ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";

                string ApiUrl = "http://codagecorporation.net/sms/index.php/cclapi/messageplatform/sendsms?username=";

                string mobile = "88" + txtPhoneNo.Text; //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
                string var_from = "ccl-non-masking";

                string FullMessage1 = txtMessage.Text.Trim() + "\nSend By: " + WpfProcessAccess.CompInfList[0].comsnam.Trim() + "\n" + WpfProcessAccess.SignedInUserList[0].signinnam + ", " + WpfProcessAccess.SignedInUserList[0].sessionID;

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&from=" + var_from + "&to=" + mobile + "&message=" + FullMessage1);

                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&sender=" + senders
                //   + "&SMSText=" + txtMessage.Text + "&GSM=" + mobile + "&type=longSMS");

                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd(); // This JSON text tobe inputted into log record -- Hafiz 15-Sep-2018
                respStreamReader.Close();
                myResp.Close();
                this.fieldClear();
                /*
                  string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120

                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&from=" + var_from+"&to="+mobile+ "&message="+ SMSText);

                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                 */
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }// try
        }


        private void btnSendSms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string msgbody1a = this.txtMessage.Text.Trim();
                string mobile = txtPhoneNo.Text.Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
                if (msgbody1a.Length == 0 || mobile.Length == 0)
                    return;

                #region SMSSEND_PART
                string user = "asitdev";//ASIT";// ds3.Tables[0].Rows[0]["apiusrid"].ToString().Trim(); //"nahid@asit.com.bd"; "";// 
                string pass = "asit2018";//A7PLAm55";// ds3.Tables[0].Rows[0]["apipass"].ToString().Trim(); //"asit321";
                ////string ApiUrl = "http://193.105.74.159/api/v3/sendsms/plain?user=";// ds3.Tables[0].Rows[0]["apiurl"].ToString().Trim(); //"http://login.smsnet24.com/apimanager/sendsms?user_id=";
                string ApiUrl = "http://codagecorporation.net/sms/index.php/cclapi/messageplatform/sendsms?username=";
                string var_from = "ccl-non-masking";
                string FullMessage1 = txtMessage.Text.Trim() + "\nRef: " + WpfProcessAccess.SignedInUserList[0].signinnam + ", " + WpfProcessAccess.SignedInUserList[0].sessionID;
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&from=" + var_from + "&to=" + mobile + "&message=" + FullMessage1);
                //HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&sender=" + senders
                //   + "&SMSText=" + txtMessage.Text + "&GSM=" + mobile + "&type=longSMS");
                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                string responseString = respStreamReader.ReadToEnd();
                respStreamReader.Close();
                myResp.Close();
                this.fieldClear();
                #endregion SMSSEND_PART

                this.smsinfolist.Clear();
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(responseString);
                var insSmserr = dict["isError"];
                if (insSmserr == "true")
                    return;
                var insSmsId = dict["insertedSmsIds"];
                string[] smsid = insSmsId.Split(',');
                string[] mnumbers = mobile.Split(',');

                for (int i = 0; i < mnumbers.Length; i++)
                {
                    this.smsinfolist.Add(new smsinfo { smsbody = FullMessage1.Trim(), smsid = smsid[i], smsnum = mnumbers[i] });
                }

                string Comcpcod1 = WpfProcessAccess.CompInfList[0].comcpcod;
                string Comcod1 = WpfProcessAccess.CompInfList[0].comcod;
                string preparebyid1a = WpfProcessAccess.SignedInUserList[0].hccode;
                string prepareses1a = WpfProcessAccess.SignedInUserList[0].sessionID;
                string preparetrm1a = WpfProcessAccess.SignedInUserList[0].terminalID;
                string msgnum1a = "MSO001";
                string msgtime1a = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff tt");
                string senderid1a = "OUTGOING";
                string msgstatus1a = "A";
                foreach (var item in this.smsinfolist)
                {
                    var pap1 = vm1.SetParmeterToUpdateSMSInfo(comcod: WpfProcessAccess.CompInfList[0].comcpcod, msgbody1: item.smsbody.ToString().Trim(), msgnum1: msgnum1a, msgtime1: msgtime1a,
                        senderid1: senderid1a, receverid1: item.smsnum.Trim(), msgstatus1: msgstatus1a, preparebyid1: preparebyid1a, prepareses1: prepareses1a, preparetrm1: item.smsid.Trim());

                    DataSet ds1s = WpfProcessAccess.GetHmsDataSet(pap1);
                }

                /*
                    string mobile = "88" + ds3.Tables[1].Rows[i]["phno"].ToString().Trim(); //"880" + "1817610879";//this.txtMob.Text.ToString().Trim();1813934120
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(ApiUrl + user + "&password=" + pass + "&from=" + var_from+"&to="+mobile+ "&message="+ SMSText);
                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    respStreamReader.Close();
                    myResp.Close();
                 */
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }// try
        }
         private void fieldClear()
        {
            this.txtPhoneNo.Text = "";
            this.txtMessage.Text = "";
        }

    }
    public class smsinfo
    {
        public string smsnum { get; set; }
        public string smsbody { get; set; }
        public string smsid { get; set; }

    }
}
