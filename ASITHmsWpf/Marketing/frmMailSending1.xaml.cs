using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// Interaction logic for frmMailSending.xaml
    /// </summary>
    public partial class frmMailSending : UserControl
    {
        public frmMailSending()
        {
            InitializeComponent();
        }
        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtTo.Text != string.Empty && this.txtMailMessage.Text != string.Empty)
            {


                //MailMessage mail = new MailMessage();
                //mail.Subject = "Testing Data";
                //mail.From = new MailAddress("raharaihan4@gmail.com");
                ListDictionary replacements = new ListDictionary();

                //mail.Body = this.Ganarate();// "Hello World. " + DateTime.Now.ToString("dddd dd-MMM-yyyy hh:mm:ss tt");
                //mail.To.Add("raharaihan4@gmail.com,hafiz@asit.com.bd,nasir_digilab@yahoo.com");
                string body = "<html><body><p>" + this.txtMailMessage.Text.ToString() + "</p><br/><br/><br/><br/><br/><br/>--------<br/> ASIT</body></html>";
                MailDefinition md = new MailDefinition();
                md.From = "asitm7a@gmail.com";
                md.IsBodyHtml = true;
                md.Subject = this.txtSubject.Text;
                MailMessage msg = md.CreateMailMessage(this.txtTo.Text, replacements, body, new System.Web.UI.Control());
                //MailMessage msg = md.CreateMailMessage("raharaihan4@gmail.com", replacements, body, new System.Web.UI.Control());
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = 587;
                client.Credentials = new NetworkCredential(md.From, "asitm7@321");
                client.Send(msg);
                fieldClear();
            }
            else
            {

            }
        }
        private void fieldClear()
        {
            this.txtTo.Text = "";
            this.txtSubject.Text = "";
            this.txtMailMessage.Text = "";
        }


    }
}
