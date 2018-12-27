using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.Data;
using ASITHmsViewMan.General;
using System.IO;
using Microsoft.Win32;

namespace ASITHmsWpf.General
{
    /// <summary>
    /// Interaction logic for frmConfigSetup105.xaml
    /// </summary>
    public partial class frmConfigSetup105 : UserControl
    {

        vmConfigSetup1 vm1 = new vmConfigSetup1();

        private bool FrmInitialized = false;
        public frmConfigSetup105()
        {
            InitializeComponent();
            //this.InitDatabaseManagement();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (!this.FrmInitialized)
            {
                this.FrmInitialized = true;
                this.InitDatabaseManagement();
            }
        }

        private void InitDatabaseManagement()
        {

            this.cmbSPName.Items.Add(new ComboBoxItem { Content = "DSP_TEST_DEVELOPMENT_01", Tag = "DBO.DSP_TEST_DEVELOPMENT_01" });
            this.cmbSPName.Items.Add(new ComboBoxItem { Content = "DSP_TEST_DEVELOPMENT_02", Tag = "DBO.DSP_TEST_DEVELOPMENT_02" });
        }
      
        private void btnUpDateSP_Click(object sender, RoutedEventArgs e)
        {
            string SqlStr1 = this.txtSqlScript.Text.Trim();
            bool strCheck1 = SqlStr1.ToUpper().Substring(0, 6).Contains("ALTER");
            strCheck1 = (strCheck1 && SqlStr1.ToUpper().Substring(0, 20).Contains("PROCEDURE"));
            bool strCheck2 = (SqlStr1.ToUpper().Substring(0, 80).Contains("DSP_TEST_DEVELOPMENT_01") || SqlStr1.ToUpper().Substring(0, 80).Contains("DSP_TEST_DEVELOPMENT_02"));
            strCheck1 = (strCheck1 && strCheck2);

            if (!strCheck1)
            {
                MessageBox.Show("Intial authentication failed. Did not execute scripts", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            var pap1 = this.vm1.SetParamExecuteNonQuery(WpfProcessAccess.CompInfList[0].comcpcod, SqlStr1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1, "NONQUERY");
            if (ds1 == null)
            {
                MessageBox.Show("Database script error occured. Did not execute scripts", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            else
            {
                MessageBox.Show(ds1.Tables[0].Rows[0]["SuccessMsg"].ToString(), WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
        }


        private void btnShowSP_Click(object sender, RoutedEventArgs e)
        {
            if (this.cmbSPName.SelectedIndex < 0)
                return;

            string SqlStr1 = ((ComboBoxItem)this.cmbSPName.Items[this.cmbSPName.SelectedIndex]).Tag.ToString();
            var pap1 = this.vm1.SetParamShowScript(WpfProcessAccess.CompInfList[0].comcpcod, SqlStr1);
            DataSet ds1 = WpfProcessAccess.GetHmsDataSet(pap1);
            if (ds1 == null)
            {
                MessageBox.Show("Database communication error occured.", WpfProcessAccess.AppTitle, MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            SqlStr1 = "";
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
                SqlStr1 += dr1["Text"];

            this.txtSqlScript.Text = SqlStr1;

            //execute sp_helptext '[dbo].[DSP_TEST_DEVELOPMENT_02]';


            //using System.Data.SqlClient;
            //using Microsoft.SqlServer.Management.Common;
            //using Microsoft.SqlServer.Management.Smo;
            //…
            //string connectionString = … /* some connection string */;
            //ServerConnection sc = new ServerConnection(connectionString);
            //Server s = new Server(connection);
            //Database db = new Database(s, … /* database name */);
            //StoredProcedure sp = new StoredProcedure(db, … /* stored procedure name */);
            //StringCollection statements = sp.Script;
        }

        private void btnUploadSP_Click(object sender, RoutedEventArgs e)
        {
            string readText = "";
            long threshold = 400000L;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a SQL Script File";
            openFileDialog.Filter = "All supported text file|*.sql;*.txt";
            //openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
            //  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
            //  "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                this.btnUploadSP.ToolTip = openFileDialog.FileName.ToString();
                var size = new FileInfo(openFileDialog.FileName).Length;
                if (size <= threshold)
                {
                    this.txtSPFileName.Text = openFileDialog.FileName.ToString() + " (Size: " + (size / 1024).ToString() + " Kb)";
                    readText = File.ReadAllText(openFileDialog.FileName);
                    this.txtSqlScript.Text = readText;
                }
                else
                {
                    this.txtSPFileName.Text = "File size > 400Kb";
                }
            }
        }  
    }
}
