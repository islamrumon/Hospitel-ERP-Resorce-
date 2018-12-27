using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ASITHmsWpf.Marketing
{
    /// <summary>
    /// Interaction logic for frmChartControl.xaml
    /// </summary>
    public partial class frmChartControl : UserControl, INotifyPropertyChanged
    {
        public List<KeyValuePair<string, int>> Data { get; set; }
        public Dictionary<griddata, griddata> SampleData { get; private set; }
        private List<griddata> griddatalist = new List<griddata>();
        public frmChartControl()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                this.sampleDataShow();
                this.dgvChart.ItemsSource = this.griddatalist;
                this.chrtLine.DataContext = this.griddatalist;
                this.DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sampleDataShow()
        {
            Data = new List<KeyValuePair<string, int>>();
            Data.Add(new KeyValuePair<string, int>("Raha", 22));
            Data.Add(new KeyValuePair<string, int>("Delowra", 21));
            Data.Add(new KeyValuePair<string, int>("Shahid", 18));
            Data.Add(new KeyValuePair<string, int>("Nazrul", 32));
            Data.Add(new KeyValuePair<string, int>("Noman", 27));
            Data.Add(new KeyValuePair<string, int>("Babu", 29));
            Data.Add(new KeyValuePair<string, int>("Yousuf", 40));

            data1.Add(22, "Raha");
            data1.Add(21, "Delowra");
            data1.Add(18, "Shahid");
            data1.Add(32, "Nazrul");
            data1.Add(27, "Noman");
            data1.Add(29, "Babu");
            data1.Add(40, "Yousuf");

            SampleData = new Dictionary<griddata, griddata>();
            SampleData.Add(new griddata { Name = "Raha" }, new griddata { Age = 25 });
            SampleData.Add(new griddata { Name = "Delowra" }, new griddata { Age = 20 });
            SampleData.Add(new griddata { Name = "Shahid" }, new griddata { Age = 15 });
            SampleData.Add(new griddata { Name = "Nazrul" }, new griddata { Age = 35 });
            SampleData.Add(new griddata { Name = "Noman" }, new griddata { Age = 30 });
            SampleData.Add(new griddata { Name = "Babu" }, new griddata { Age = 49 });
            SampleData.Add(new griddata { Name = "Yousuf" }, new griddata { Age = 60 });

            griddatalist.Add(new griddata { Name = "Raha", Age = 25, Name1 = "Raha", Age1 = 20 });
            griddatalist.Add(new griddata { Name = "Delowra", Age = 20, Name1 = "Delowra", Age1 = 25 });
            griddatalist.Add(new griddata { Name = "Shahid", Age = 15, Name1 = "Shahid", Age1 = 10 });
            griddatalist.Add(new griddata { Name = "Nazrul", Age = 35, Name1 = "Nazrul", Age1 = 45 });
            griddatalist.Add(new griddata { Name = "Noman", Age = 30, Name1 = "Noman", Age1 = 40 });
            griddatalist.Add(new griddata { Name = "Babu", Age = 49, Name1 = "Babu", Age1 = 59 });
            griddatalist.Add(new griddata { Name = "Yousuf", Age = 60, Name1 = "Yousuf", Age1 = 70 });
        }

        private void TabCtrl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        public class griddata
        {
            public string Name { get; set; }
            public int Age { get; set; }

            public string Name1 { get; set; }
            public int Age1 { get; set; }
            public griddata()
            { }
        }

        Dictionary<int, string> data1 = new Dictionary<int, string>();
        public IDictionary<int, string> Data1
        {
            get { return this.data1; }
        }

        private KeyValuePair<int, string>? selectedKey = null;
        public KeyValuePair<int, string>? SelectedKey
        {
            get { return this.selectedKey; }
            set
            {
                this.selectedKey = value;
                this.OnPropertyChanged("SelectedKey");
                this.OnPropertyChanged("SelectedValue");
            }
        }

        public string SelectedValue
        {
            get
            {
                if (null == this.SelectedKey)
                {
                    return string.Empty;
                }

                return this.data1[this.SelectedKey.Value.Key];
            }
            set
            {
                this.data1[this.SelectedKey.Value.Key] = value;
                this.OnPropertyChanged("SelectedValue");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            var eh = this.PropertyChanged;
            if (null != eh)
            {
                eh(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
