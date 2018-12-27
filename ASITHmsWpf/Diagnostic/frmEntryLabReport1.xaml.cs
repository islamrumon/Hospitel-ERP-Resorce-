
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
using ASITHmsEntity;
using ASITHmsRpt5Diagnostic;
using Microsoft.Reporting.WinForms;
using System.Collections.ObjectModel;

namespace ASITHmsWpf.Diagnostic
{
    /// <summary>
    /// Interaction logic for frmEntryLabReport1.xaml
    /// </summary>
    public partial class frmEntryLabReport1 : UserControl
    {

        class City
        {
            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
            private int _cityID;

            public int CityID
            {
                get { return _cityID; }
                set { _cityID = value; }
            }
        }
       

        List<HmsEntityDiagnostic.RptPatientInfo> PatientLst = new List<HmsEntityDiagnostic.RptPatientInfo>();
        List<HmsEntityDiagnostic.RptResultClass> RptLst = new List<HmsEntityDiagnostic.RptResultClass>();
        List<HmsEntityGeneral.SirInfCodeBook> SirList1 = new List<HmsEntityGeneral.SirInfCodeBook>();
        public frmEntryLabReport1()
        {
            InitializeComponent();
            WpfProcessAccess.GetAccSirCodeList();
            SirList1 = WpfProcessAccess.AccSirCodeList.FindAll(x => x.sircode.Substring(9, 3) != "000");
        }

        private static readonly City[] dataSource = new City[] {
            new City{CityID=1, Name="Toronto"}, 
            new City{CityID=2, Name="Montreal"},
            new City{CityID=3, Name="Edmonton"},
            new City{CityID=4, Name="Ottawa"},
            new City{CityID=5, Name="Montreal"},
            new City{CityID=6, Name="Calgary"},
            new City{CityID=7, Name="Winnipeg"},
            new City{CityID=8, Name="Yellow knif"},
            new City{CityID=9, Name="Mississauga"},
            new City{CityID=10, Name="Oakville"},
            new City{CityID=11, Name="Hamilton"},
            new City{CityID=12, Name="Burlington"},
        }; 


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // PatientLst.Add(new hm)
            #region Create Data
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "13.1 gm/dl", NRange = "11.5 - 16.0 gm/dl ", TstName = "Hb" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "06 mm in 1st hr", NRange = "0-20 mm in 1st hr.", TstName = "ESR" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "4.53 Million/Cmm", NRange = "4.0-6.5 Million/Cmm ", TstName = "RBC" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "7,500 /Cmm", NRange = "4,000 - 11,000/Cmm", TstName = "WBC" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "3,25,000 /Cmm", NRange = "1 ,50,000-4,50,000/Cmm ", TstName = "Platelets" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "300 /Cmm", NRange = "Upto 600/Cmm", TstName = "CE" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "70%", NRange = "40-70% ", TstName = "Polymorph " });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "20%", NRange = "20-40%", TstName = "Lymphocyte" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "6%", NRange = "37-50% ", TstName = "Eosinophil " });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "86.3 fL", NRange = "26-32 pg ", TstName = "Monocyte" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "CBC", Result1 = "33.5 g/dL", NRange = "32-36 g/dL", TstName = "HCT/PCV" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "Prothrombin Time", Result1 = "12 Sec.", NRange = "10-14 Sec.", TstName = "Control " });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "Prothrombin Time", Result1 = "12 Sec.", NRange = "", TstName = "Patient" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "Prothrombin Time", Result1 = "100%", NRange = "", TstName = "Prothrombin Index " });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "Prothrombin Time", Result1 = "1.0", NRange = "32-36 g/dL", TstName = "Ratio" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "Prothrombin Time", Result1 = "1.0", NRange = "", TstName = "INR" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "HAEMATOLOGY", SubGrp = "Prothrombin Time", Result1 = "1.0", NRange = "", TstName = "I S I" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "S.Bilirubin (Total)", Result1 = "6 pmol/I", NRange = "At Birth : upto 85 pmoVl", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "S.GOT (AST)", Result1 = "36 U/1 ", NRange = "5 Days : upto 205 pmoVl ", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "S.GPT (ALT)", Result1 = "26 U/I ", NRange = "1 Month : upto 25 pmoVl ", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "S.Urea", Result1 = "2.09 mmol/I", NRange = "Adults : upto 21 pmoVl", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "S.Creatinine", Result1 = "67 pmol/I", NRange = "upto 35 U/I", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "S.Uric Acid", Result1 = "517 pmol/I", NRange = "Adult : 1.8-7.2 mmoVl", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "S.Electrolyte ", Result1 = "", NRange = "Adults < 50 years : 53-110 pmoVl ", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "Sodium", Result1 = "", NRange = "Adults > 50 years : 53-127 pmoVl ", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "Potassium ", Result1 = "", NRange = "Child : 27-62 pmoVl", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "TCO2", Result1 = "", NRange = "Male 208-428 pmoVl ", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "BIOCHEMISTRY", SubGrp = "HCO3", Result1 = "", NRange = "Female 142-357 pmoVl", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "IMMUNOCHEMISTRY", SubGrp = "Thyroid Stimulating Hormone (TSH) 0.34 — 5.0 plU/mL", Result1 = "", NRange = "", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "IMMUNOCHEMISTRY", SubGrp = "Free thyroxine (FT4)", Result1 = "", NRange = "", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "IMMUNOCHEMISTRY", SubGrp = "", Result1 = "2.87 plU/mL ", NRange = "0.34 — 5.0 plU/mL", TstName = "" });
            RptLst.Add(new HmsEntityDiagnostic.RptResultClass() { RecNo = "547170", MainGrp = "IMMUNOCHEMISTRY", SubGrp = "", Result1 = "0.81 ng/dL", NRange = "0.70 - 1.48 ng/dL", TstName = "" });


            PatientLst.Add(new HmsEntityDiagnostic.RptPatientInfo() { Pname = "Sabid Hossain", Pid = "11204", gender = "male", RcvDate = DateTime.Today.ToString(), Page = "24 Y", RecNo = "547012", RefBy = "Dr. Dewan Abdul Hakim MBBS" });

            #endregion

            var CmpInfo = WpfProcessAccess.CompInfList[0];



            var list3 = WpfProcessAccess.GetRptGenInfo();
            list3[0].RptHeader1 = "Lab Report";            //"Due Details List( From :" + frmdat + "  To : " + todat + " )";
            LocalReport rpt1 = DiagReportSetup.GetLocalReport("Lab.RptLab01", RptLst, PatientLst, list3);
            rpt1.SetParameters(new ReportParameter("comlabel", Convert.ToBase64String(CmpInfo.comlabel)));
            rpt1.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(CmpInfo.comlogo)));
            rpt1.SetParameters(new ReportParameter("Address", CmpInfo.comadd1));
            rpt1.SetParameters(new ReportParameter("Url", CmpInfo.comadd4));
            rpt1.SetParameters(new ReportParameter("Contact", CmpInfo.comadd3));
           // Rpt1.SetParameters(new ReportParameter("comlogo", Convert.ToBase64String(CmpInfo.comadd4)));
            string WindowTitle1 = "Lab Report";
            string RptDisplayMode = "PrintLayout";
            WpfProcessAccess.ViewReportInWindow(rpt1: rpt1, WindowTitle1: WindowTitle1, RptDisplayMode: RptDisplayMode);
        }

        private void autoCities_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            //check
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = frmEntryLabReport1.GetCities(args.Pattern);
        }

      

        /// <summary>
        /// Get a list of cities that follow a pattern
        /// </summary>
        /// <returns></returns>
        private static ObservableCollection<City> GetCities(string Pattern)
        {
            // match on contain (could do starts with)
            return new ObservableCollection<City>(
                frmEntryLabReport1.dataSource.
                Where((city, match) => city.Name.ToLower().Trim().Contains(Pattern.ToLower().Trim())));
        }

        private void autoSirdesc_PatternChanged(object sender, UserControls.AutoComplete.AutoCompleteArgs args)
        {
            //check
            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
                args.DataSource = this.GetSirdesc(args.Pattern);
        }
        private ObservableCollection<HmsEntityGeneral.SirInfCodeBook> GetSirdesc(string Pattern)
        {
            // match on contain (could do starts with)

            return new ObservableCollection<HmsEntityGeneral.SirInfCodeBook>(
                SirList1.Where((x, match) => x.sirdesc1.ToLower().Trim().Contains(Pattern.ToLower().Trim())).Take(100));
        }
    }
}
