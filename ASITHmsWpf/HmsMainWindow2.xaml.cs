using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
using System.Windows.Controls.Primitives;
using ASITFunLib;
using ASITHmsEntity;
using ASITHmsViewMan;
using ASITHmsViewMan.General;
using System.Windows.Threading;
using System.IO;
using ASITHmsWpf.General;
using System.Threading;


namespace ASITHmsWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class HmsMainWindow2 : Window
    {

        private DispatcherTimer timer1 = new DispatcherTimer();
        private DispatcherTimer timerClick1 = new DispatcherTimer();
        private int timerClick1Counter = 0;
        private string uc1Name = "Nothing";
        private string frmTag = "Nothing";
        private string frmTag2 = "Nothing";
        private List<MenuItem> MenuItemList = new List<MenuItem>();

        private double psWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        private double psHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
        private double vsWidth = System.Windows.SystemParameters.VirtualScreenWidth;
        private double vsHeight = System.Windows.SystemParameters.VirtualScreenHeight;

        private double XpValue = 0;
        private double YpValue = 0;

        public HmsMainWindow2()
        {
            InitializeComponent();
            
            this.VMGrid1.Margin = new Thickness(-220, 0,0,0);
            this.timer1.Interval = TimeSpan.FromSeconds(30);
            this.timer1.Tick += this.timer1_Tick;
            this.timer1.Start();
            this.UcGrid1.Visibility = Visibility.Hidden;

            this.UcGrid1.Visibility = Visibility.Hidden;
            this.lblWait1.Visibility = Visibility.Hidden;

            this.timerClick1Counter = 0;
            this.timerClick1.Interval = TimeSpan.FromSeconds(0);
            this.timerClick1.Tick += this.timerClick1_Tick;
            this.timerClick1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.tbArrivalDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy ddd hh:mm tt");
        }

        private void Mailto_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:info@asit.com.bd?Subject=The%20subject%20of%20the%20mail");
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.asit.com.bd");
        }


        #region Main Window Management


        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            if (WpfProcessAccess.AppRptViewStyle != "Normal")
                this.btnMinimize.Visibility = Visibility.Visible;

            this.RemoveExtraMenus(this.mnuFrontDesk, this.acriFrontDesk);
            this.RemoveExtraMenus(this.mnuLab, this.acriLab);
            this.RemoveExtraMenus(this.mnuStore, this.acriStore);
            this.RemoveExtraMenus(this.mnuAcc, this.acriAcc);
            this.RemoveExtraMenus(this.mnuMkt, this.acriMkt);
            this.RemoveExtraMenus(this.mnuHcm, this.acriHcm);
            this.RemoveExtraMenus(this.mnuAdmin, this.acriAdmin);
            this.RemoveExtraMenus(this.mnuSetup, this.acriSetup);
            this.RemoveExtraMenus(this.mnuMis, this.acriMis);
            this.RemoveExtraMenus(this.mnuMisc, this.acriMisc);


            var empname1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == WpfProcessAccess.SignedInUserList[0].hccode);
            this.lblSignInNam.Content = WpfProcessAccess.SignedInUserList[0].signinnam.ToString();
            this.lblSignInNam.ToolTip = (empname1==null ? WpfProcessAccess.SignedInUserList[0].hcname.ToString(): empname1[0].sirdesc.Trim());
            this.lblSessionId.Content = WpfProcessAccess.SignedInUserList[0].sessionID.ToString();
            this.lblTeminalId.Content = WpfProcessAccess.SignedInUserList[0].terminalID.ToString();

            this.AddContextMenu();

            if ((WpfProcessAccess.SignedInUserList[0].hcphoto != null))
            {
                byte[] bytes = (byte[])WpfProcessAccess.SignedInUserList[0].hcphoto;
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.StreamSource = mem;
                bmp3.EndInit();
                this.imgSignInUser.Source = bmp3;
                //this.UserPhoto.Source = bmp3;
            }

            if ((WpfProcessAccess.CompInfList[0].comlabel != null))
            {
                byte[] bytes = WpfProcessAccess.CompInfList[0].comlabel;
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.StreamSource = mem;
                bmp3.EndInit();
                this.imgClTitle.Source = bmp3;
                this.mnuFlowChart1.SetTitleImage(bmp3);
            }

            if ((WpfProcessAccess.CompInfList[0].comlogo != null))
            {
                byte[] bytes = WpfProcessAccess.CompInfList[0].comlogo;
                MemoryStream mem = new MemoryStream(bytes);
                BitmapImage bmp3 = new BitmapImage();
                bmp3.BeginInit();
                bmp3.StreamSource = mem;
                bmp3.EndInit();
                this.imgClIcon.Source = bmp3;
            }

            this.timer1_Tick(null, null);
            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewKeyUpEvent, new KeyEventHandler(Window_PreviewKeyUp));
            this.mnuFlowChart1.ShowTabInformation(0);
            this.ChangeFlowChartVisibility(0);



            //this.Width = System.Windows.SystemParameters.VirtualScreenWidth;
            //this.Height = System.Windows.SystemParameters.VirtualScreenHeight;
            if (this.psWidth < 900)  // (System.Windows.SystemParameters.VirtualScreenWidth < 900)
            {
                this.slider1.Minimum = 0.5;
                this.slider1.Maximum = 0.7;
                this.slider1.Value = 0.55;
                this.stkpContact.Visibility = Visibility.Collapsed;
                this.imgClTitle.Width = 250;
            }
            else if (this.psWidth < 1200)  // (System.Windows.SystemParameters.VirtualScreenWidth < 1200)
            {
                this.slider1.Minimum = 0.5;
                this.slider1.Maximum = 1.0;
                this.slider1.Value = 0.78;
                this.stkpContact.Visibility = Visibility.Collapsed;
                this.imgClTitle.Width = 450;
            }
            else if (this.psWidth > 3000)  // (System.Windows.SystemParameters.VirtualScreenWidth > 3000)
            {
                this.slider1.Minimum = 1.2;
                this.slider1.Maximum = 1.5;
                this.slider1.Value = 1.4;
            }
            else if (this.psWidth > 1900)  // (System.Windows.SystemParameters.VirtualScreenWidth > 1900)
            {
                this.slider1.Minimum = 1.0;
                this.slider1.Maximum = 1.5;
                this.slider1.Value = 1.2;
            }
            else
            {
                //MessageBox.Show(System.Windows.SystemParameters.VirtualScreenWidth.ToString());
                this.slider1.Minimum = 0.7;
                this.slider1.Maximum = 1.5;
                this.slider1.Value = 1.0;
            }

        }


        private void RemoveExtraMenus(Menu mnu1, Expander expn1)
        {
            int cnt1 = mnu1.Items.Count;
            var mnuitm1 = new MenuItem[cnt1];

            for (int i = 0; i < cnt1; i++)
                mnuitm1[i] = (MenuItem)mnu1.Items[i];

            for (int j = 0; j < cnt1; j++)
            {
                string[] strTag1 = mnuitm1[j].Tag.ToString().Trim().Split(',');
                if (WpfProcessAccess.AppFormsList == null)
                    mnu1.Items.Remove(mnuitm1[j]);
                else if (WpfProcessAccess.AppFormsList.FirstOrDefault(x => x.Contains(strTag1[0])) == null)
                    mnu1.Items.Remove(mnuitm1[j]);
                else if (strTag1.Length > 1 && strTag1[0].Contains("frmEntryVoucher1"))
                {
                    var vtypeList = HmsEntityAccounting.GetVoucherType().FindAll(x => x.vtitle.ToUpper().Contains(strTag1[1].ToUpper()));// && !x.vtitle.ToUpper().Contains("BUDGET"));
                    if (strTag1.Length > 2)
                        vtypeList = vtypeList.FindAll(x => x.vtagid.Contains(strTag1[2])).ToList();
                    int found1 = 0;
                    foreach (var item1 in vtypeList)
                    {
                        if (WpfProcessAccess.SignedInUserAuthList.FirstOrDefault(x => x.uicode == "WPF_frmEntryVoucher1_cmbVouType_" + item1.vtagid) != null)
                            found1++;
                    }

                    if (found1 == 0)
                        mnu1.Items.Remove(mnuitm1[j]);
                }
            }
            if (mnu1.Items.Count == 0)
                expn1.Visibility = Visibility.Collapsed;
        }
        private void RemoveFlowChartGrid(Grid gridx)
        {
            string[] strTag1 = gridx.Tag.ToString().Trim().Split(',');
            if (WpfProcessAccess.AppFormsList == null)
                gridx.Visibility = Visibility.Collapsed;
            else if (WpfProcessAccess.AppFormsList.FirstOrDefault(x => x.Contains(strTag1[0])) == null)
                gridx.Visibility = Visibility.Collapsed;
            else if (strTag1.Length > 1 && strTag1[0].Contains("frmEntryVoucher1"))
            {
                var vtypeList = HmsEntityAccounting.GetVoucherType().FindAll(x => x.vtitle.ToUpper().Contains(strTag1[1].ToUpper()));// && !x.vtitle.ToUpper().Contains("BUDGET"));
                if (strTag1.Length > 2)
                    vtypeList = vtypeList.FindAll(x => x.vtagid.Contains(strTag1[2])).ToList();
                int found1 = 0;
                foreach (var item1 in vtypeList)
                {
                    if (WpfProcessAccess.SignedInUserAuthList.FirstOrDefault(x => x.uicode == "WPF_frmEntryVoucher1_cmbVouType_" + item1.vtagid) != null)
                        found1++;
                }

                if (found1 == 0)
                    gridx.Visibility = Visibility.Collapsed;
            }

        }
        private void ChangeFlowChartVisibility(int Index)
        {
            if (Index == 0)
            {
                this.mnuFlowChart1.Grid01.Visibility = this.acriFrontDesk.Visibility;
                this.mnuFlowChart1.Grid02.Visibility = this.acriLab.Visibility;
                this.mnuFlowChart1.Grid03.Visibility = this.acriStore.Visibility;
                this.mnuFlowChart1.Grid04.Visibility = this.acriAcc.Visibility;
                this.mnuFlowChart1.Grid05.Visibility = this.acriMkt.Visibility;
                this.mnuFlowChart1.Grid06.Visibility = this.acriHcm.Visibility;
                this.mnuFlowChart1.Grid07.Visibility = this.acriAdmin.Visibility;
                this.mnuFlowChart1.Grid08.Visibility = this.acriSetup.Visibility;
                this.mnuFlowChart1.Grid09.Visibility = this.acriMis.Visibility;
                this.mnuFlowChart1.Grid10.Visibility = this.acriMisc.Visibility;
            }
            else
            {
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid01);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid02);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid03);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid04);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid05);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid06);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid07);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid08);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid09);
                this.RemoveFlowChartGrid(this.mnuFlowChart1.Grid10);
            }
        }
        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IInputElement inputElement = Keyboard.FocusedElement;
                if (inputElement != null)
                {
                    System.Windows.Controls.Primitives.TextBoxBase textBoxBase = inputElement as System.Windows.Controls.Primitives.TextBoxBase;
                    if (textBoxBase != null)
                    {
                        if (!textBoxBase.AcceptsReturn)
                            MoveFocus_Next(textBoxBase);
                        return;
                    }
                    if (
                        MoveFocus_Next(inputElement as ComboBox)
                        ||
                        MoveFocus_Next(inputElement as Button)
                        ||
                        MoveFocus_Next(inputElement as DatePicker)
                        ||
                        MoveFocus_Next(inputElement as CheckBox)
                        ||
                        MoveFocus_Next(inputElement as DataGrid)
                        ||
                        MoveFocus_Next(inputElement as TabItem)
                        ||
                        MoveFocus_Next(inputElement as RadioButton)
                        ||
                        MoveFocus_Next(inputElement as ListBox)
                        ||
                        MoveFocus_Next(inputElement as ListView)
                        ||
                        MoveFocus_Next(inputElement as PasswordBox)
                        ||
                        MoveFocus_Next(inputElement as Window)
                        ||
                        MoveFocus_Next(inputElement as Page)
                        ||
                        MoveFocus_Next(inputElement as Frame)
                    )
                        return;
                }
            }
        }

        private bool MoveFocus_Next(UIElement uiElement)
        {
            if (uiElement != null)
            {
                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                return true;
            }
            return false;
        }

        private void HmsMainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (XpValue == 0)
            {
                this.XpValue = this.Width;
                this.YpValue = this.Height;
            }
            this.ResizeZoom(this.Width / this.XpValue * 0.95, this.Height / this.YpValue * 0.95);
            
            
            //if (this.WindowState == WindowState.Maximized)
            //{
            //    //SystemParameters.VirtualScreenWidth
            //    //SystemParameters.VirtualScreenHeight
            //    this.Height = SystemParameters.PrimaryScreenHeight;
            //    this.Width = SystemParameters.PrimaryScreenWidth;
            //}
            ////this.Grid1.Height = this.Height;
            ////this.Grid1.Width = this.Width;
            //////this.DevGrid1.Width = this.Width;
            ////this.Grid1.Margin = new Thickness(0, 0, 0, 0);
            //this.VMGrid1.Height = this.Height;
            //this.HMGrid1.Width = this.Width;
            //////this.DevGrid1.Margin = new Thickness(0, (this.Height - this.DevGrid1.Height), 0, 0);
            //////this.UcGrid1.Margin = new Thickness((this.Width - 1200) / 2, 90, 0, 0);
            ////this.UcGrid1.Margin = new Thickness((this.MainWindow1.Width - this.UcGrid1.Width ) / 2, 90, 0, 0);
        }

        private void btnAppClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Application.Current.Shutdown();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
           
            string scrCaption1 = "";
            for (int k = 0; k < this.UcGrid1.Children.Count; k++)
            {
                string TypeName1 = this.UcGrid1.Children[k].GetType().Name.ToString();
                if (TypeName1.Contains("frm"))
                {
                    scrCaption1 = ((UserControl)this.UcGrid1.Children[k]).Tag.ToString().Trim();
                    this.UcGrid1.Children.Remove(this.UcGrid1.Children[k]);
                }
            }
            this.UcGrid1.Visibility = Visibility.Hidden;
            this.VMGrid1.IsEnabled = true;
            this.VMGrid1.Visibility = Visibility.Visible;
            this.MMGrid1.IsEnabled = true;
            this.MMGrid1.Visibility = Visibility.Visible;
            this.timerClick1Counter = 0;
            this.timerClick1.Stop();
            //WpfProcessAccess.ShowBalloon(this.lblBaloon1, this.Title, scrCaption1 + " Has Been Closed", BalloonType.Information);
        }

        /// <summary>
        /// //////////////////
        /// </summary>
        /// <param name="uc1Name"></param>
        /// <param name="frmTag"></param>
        private void frm_Show(string uc1Name1 = "", string frmTag1 = "", string frmTag2 = "")
        {
            for (int k = 0; k < this.UcGrid1.Children.Count; k++)
            {
                string TypeName1 = this.UcGrid1.Children[k].GetType().Name.ToString();
                if (TypeName1.Contains("frm"))
                    this.UcGrid1.Children.Remove(this.UcGrid1.Children[k]);
            }

            if (uc1Name1 == "Nothing")
                return;

            UserControl uc1 = WpfProcessAccess.CreateUserControl(uc1Name1);
            if (uc1 == null)
                return;


            uc1.Name = "frmUc1";
            uc1.Tag = (frmTag1.Length > 0 ? frmTag1 : uc1.Tag);
            uc1.Tag = (frmTag2.Length > 0 ? uc1.Tag + "," + frmTag2 : uc1.Tag);

            this.lblTitle1.Content = (uc1.Tag == null ? "" : uc1.Tag.ToString());
            uc1.Height = 650;
            uc1.Width = 1200;
            uc1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            uc1.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            uc1.Margin = new Thickness((this.UcGrid1.Width - 1200) / 2, 25, 0, 0); //new Thickness((this.UcGrid1.Width - 1200) / 2, 40, 0, 0);

            int i = this.UcGrid1.Children.Count;
            this.UcGrid1.Children.Insert(i, uc1);
            this.ShowAnimation2(uc1);
            this.UcGrid1.Visibility = Visibility.Visible;
            this.VMGrid1.IsEnabled = false;
            this.VMGrid1.Visibility = Visibility.Hidden;
        }

        private void ShowAnimation2(UserControl uc1)
        {
            var sb = new Storyboard();

            var fade = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(2) };
            Storyboard.SetTarget(fade, this.UcGrid1);
            Storyboard.SetTargetProperty(fade, new PropertyPath(Grid.OpacityProperty));
            sb.Children.Add(fade);

              Random rnd1 = new Random();
            int rnd2 = rnd1.Next(1, 100);

            if (rnd2 % 2 == 0 || rnd2 % 5 == 0 || rnd2 % 7 == 0)
            {
                var movex = new DoubleAnimation() { From = 0, To = uc1.Width, Duration = TimeSpan.FromSeconds(1) };
                Storyboard.SetTarget(movex, this.UcGrid1);
                Storyboard.SetTargetProperty(movex, new PropertyPath(Grid.WidthProperty));
                sb.Children.Add(movex);
            }
            if (rnd2 % 3 == 0 || rnd2 % 5 == 0 || rnd2 % 11 == 0)
            {
                var movey = new DoubleAnimation() { From = 0, To = uc1.Height, Duration = TimeSpan.FromSeconds(1) };
                Storyboard.SetTarget(movey, this.UcGrid1);
                Storyboard.SetTargetProperty(movey, new PropertyPath(Grid.HeightProperty));
                sb.Children.Add(movey);
            }
            sb.Begin();
        }

        private void tbArrivalDateTime_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.dtpMW1.IsDropDownOpen = true;
        }

        private void MainWindow1_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.Height = psHeight;// System.Windows.SystemParameters.VirtualScreenHeight;
                this.Width = psWidth;// System.Windows.SystemParameters.VirtualScreenWidth;
                this.Left = 0;
                this.Top = 0;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                this.Height = psHeight * 0.75;// System.Windows.SystemParameters.VirtualScreenHeight * 0.85;
                this.Width = psWidth * 0.75;// System.Windows.SystemParameters.VirtualScreenWidth * 0.85;
                Rect workArea = SystemParameters.WorkArea;
                this.Left = (workArea.Width - this.Width) / 2 + workArea.Left;
                this.Top = (workArea.Height - this.Height) / 2 + workArea.Top;
            }         
        }

        #endregion Main Window Management


        #region Main Menu Management
        public void acrMainActivation(Object sender, ExecutedRoutedEventArgs e)
        {
            if (!this.VMGrid1.IsEnabled)
                return;

            Expander[] acrMain = { this.acriFrontDesk, this.acriLab, this.acriStore, this.acriAcc, this.acriMkt, 
                                   this.acriHcm, this.acriAdmin, this.acriSetup, this.acriMis, this.acriMisc };

            foreach (Expander expn2 in acrMain)
                expn2.IsExpanded = false;

            if (e.Parameter.ToString().Trim() == "0")
                return;

            int index1 = int.Parse(e.Parameter.ToString().Trim()) - 1;
            acrMain[index1].IsExpanded = true;
            //this.MenukeyNevigate(index1);
        }

        public void FlowActivation(Object sender, RoutedEventArgs e)
        {
            if (!this.VMGrid1.IsEnabled)
                return;

            Expander[] acrMain = { this.acriFrontDesk, this.acriLab, this.acriStore, this.acriAcc, this.acriMkt, 
                                   this.acriHcm, this.acriAdmin, this.acriSetup, this.acriMis, this.acriMisc };

            foreach (Expander expn2 in acrMain)
                expn2.IsExpanded = false;

            if (((Grid)sender).Tag.ToString().Trim() == "0")
                return;

            int index1 = int.Parse(((Grid)sender).Tag.ToString().Trim()) - 1;
            acrMain[index1].IsExpanded = true;
            //this.MenukeyNevigate(index1);
        }

        public void MenukeyNevigate(int index2)
        {
            Menu[] mainMenu = { this.mnuFrontDesk, this.mnuLab, this.mnuStore, this.mnuAcc,
                                  this.mnuMkt, this.mnuHcm, this.mnuAdmin, this.mnuSetup, this.mnuMis, this.mnuMisc };

            foreach (var mnu1 in mainMenu)
            {
                foreach (var mnui1 in mnu1.Items)
                {
                    ((MenuItem)mnui1).Header = ((MenuItem)mnui1).Header.ToString().Replace("_", "").Trim();
                }
            }

            foreach (var mnui1 in mainMenu[index2].Items)
            {
                ((MenuItem)mnui1).Header = "      _" + ((MenuItem)mnui1).Header.ToString();

            }
            this.MMGrid1.IsEnabled = true;
            this.MMGrid1.Visibility = Visibility.Visible;
            this.mnuFlowChart1.ShowTabInformation(index2 + 1);
            this.ChangeFlowChartVisibility(index2 + 1);
            //this.MMGrid1.Background = new SolidColorBrush(Colors.Blue);
        }

        private void acri_Expanded(object sender, RoutedEventArgs e)
        {
            string ExpName1 = ((Expander)sender).Name.ToString().Trim();

            this.MMGrid1.IsEnabled = false;
            this.MMGrid1.Visibility = Visibility.Hidden;
            foreach (Expander exp in expMain.Children)
            {
                if (exp != sender)
                {
                    exp.IsExpanded = false;
                }
            }

            Expander[] acrMain = { this.acriFrontDesk, this.acriLab, this.acriStore, this.acriAcc, this.acriMkt, 
                                   this.acriHcm, this.acriAdmin, this.acriSetup, this.acriMis, this.acriMisc };

            int Index3 = 0;
            foreach (var item1 in acrMain)
            {
                if (item1.Name.ToString().Trim() == ExpName1)
                {
                    this.MenukeyNevigate(Index3);
                    return;
                }
                ++Index3;
            }
        }

        private void acri_Collapsed(object sender, RoutedEventArgs e)
        {
            this.MMGrid1.IsEnabled = true;
            this.MMGrid1.Visibility = Visibility.Visible;
            this.mnuFlowChart1.ShowTabInformation(0);
            this.ChangeFlowChartVisibility(0);
            //this.MMGrid1.Background = new SolidColorBrush(Colors.Aqua);
        }

        public void mnuiAll_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Grid)
            {
                if (((Grid)sender).Tag == null)
                {
                    return;
                }
                this.mnuOperation(((Grid)sender).Tag.ToString().Trim());
            }
            if (sender is MenuItem)
            {
                this.mnuOperation(((MenuItem)sender).Tag.ToString().Trim());
            }
        }

        public void mnuOperation(string FormTag)
        {
            if (WpfProcessAccess.AppFormsList == null)
                return;

            //WpfProcessAccess.ShowBalloon(lblBaloon1: this.lblBaloon1, caption1:"Pleasw wait while loading information into memory");
            //List<string> List1u = WpfProcessAccess.FormsList();
            //foreach (var item1 in List1u)           
            foreach (var item1 in WpfProcessAccess.AppFormsList)
            {
                //if (item1.GetType().ToString().Trim().Contains(((MenuItem)sender).Tag.ToString().Trim()))
                string[] tagPart1 = FormTag.Trim().Split(',');
                //if (item1.GetType().ToString().Trim().Contains(tagPart1[0]))
                if (item1.Contains(tagPart1[0]))
                {
                    string frmTag1 = (tagPart1.Length > 1 ? tagPart1[1] : "");
                    string frmTag2 = (tagPart1.Length > 2 ? tagPart1[2] : "");
                    this.uc1Name = item1;
                    this.frmTag = frmTag1;
                    this.frmTag2 = frmTag2;
                    this.timerClick1.Start();
                    //this.frm_Show(item1, frmTag);
                    return;
                }
            }

        }
        private void timerClick1_Tick(object sender, EventArgs e)
        {
            if (timerClick1Counter == 0)
            {
                //WpfProcessAccess.ShowBalloon(this.lblBaloon1, "", "Loading information ........ ", BalloonType.Information);
                this.MMGrid1.IsEnabled = false;
                this.MMGrid1.Visibility = Visibility.Hidden;
                this.lblWait1.Visibility = Visibility.Visible;
                timerClick1Counter = 1;
                return;
            }
            this.timerClick1Counter = 0;
            this.timerClick1.Stop();
            this.frm_Show(this.uc1Name, this.frmTag, this.frmTag2);
            this.lblWait1.Visibility = Visibility.Hidden;
        }

        private void acrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int k = 0; k < this.UcGrid1.Children.Count; k++)
            {
                string TypeName1 = this.UcGrid1.Children[k].GetType().Name.ToString();
                if (TypeName1.Contains("frm"))
                    this.UcGrid1.Children.Remove(this.UcGrid1.Children[k]);
            }
        }



        #endregion // Main Menu Management


        #region Slider Management for Zooming/Unzooming User Control
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if (this.UcGrid1.Visibility == Visibility.Hidden)
            //    return;

            //var scaler = this.UcGrid1.LayoutTransform as ScaleTransform;
            //var scaler1 = this.MMGrid1.LayoutTransform as ScaleTransform;
            //if (scaler == null || scaler1 == null)
            //{
            //    this.MMGrid1.LayoutTransform = new ScaleTransform(slider1.Value, slider1.Value);
            //    this.UcGrid1.LayoutTransform = new ScaleTransform(slider1.Value, slider1.Value);
            //}
            //else if (scaler.HasAnimatedProperties || scaler1.HasAnimatedProperties)
            //{
            //    // Do nothing because the value is being changed by animation.
            //    // Setting scaler.ScaleX will cause infinite recursion due to the
            //    // binding specified in the XAML.
            //}
            //else
            //{
            //    scaler.ScaleX = slider1.Value;
            //    scaler.ScaleY = slider1.Value;

            //    scaler1.ScaleX = slider1.Value;
            //    scaler1.ScaleY = slider1.Value;
            //}
            this.ResizeZoom(slider1.Value, slider1.Value);
            this.slider1.ToolTip = (slider1.Value).ToString("##0%");
            ////this.UcGrid1.Margin = new Thickness((this.MainWindow1.Width - this.UcGrid1.Width * slider1.Value) / 2, 90, 0, 0);


            //this.MMGrid1.Margin = new Thickness((this.MainWindow1.Width - this.MMGrid1.Width * slider1.Value) / 2, 80, 0, 0);
        }
        private void ResizeZoom(double XScaleValue, double YScaleValue)
        {
            var scaler = this.UcGrid1.LayoutTransform as ScaleTransform;
            var scaler1 = this.MMGrid1.LayoutTransform as ScaleTransform;
            var scaler2 = this.VMGrid1.LayoutTransform as ScaleTransform;
            
            if (scaler == null || scaler1 == null)
            {
                this.MMGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
                this.VMGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
                this.UcGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
            }
            else if (scaler.HasAnimatedProperties)
            {
                // Do nothing because the value is being changed by animation.
                // Setting scaler.ScaleX will cause infinite recursion due to the
                // binding specified in the XAML.
            }
            else
            {
                scaler.ScaleX = XScaleValue;
                scaler.ScaleY = YScaleValue;
                scaler1.ScaleX = XScaleValue;
                scaler1.ScaleY = YScaleValue;
                scaler2.ScaleX = XScaleValue;
                scaler2.ScaleY = YScaleValue;
            }
            //this.slider1.ToolTip = (slider1.Value).ToString("##0%");
        }

        private void SliderZoom(object sender, ExecutedRoutedEventArgs e)
        {
            //int.Parse(e.Parameter.ToString().Trim()) - 1;
            if (e.Parameter.ToString().Trim() == "ZoomOut")
                slider1.Value += 0.1;// slider1.TickFrequency;
            else
                slider1.Value -= 0.1;
        }
        #endregion //Slider Management for Zooming/Unzooming User Control

        private void MainWindow1_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.Visibility != Visibility.Visible)
            {
                Application.Current.Shutdown();
                return;
            }
  
            if (System.Windows.MessageBox.Show("Are you confirm to close application", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                            MessageBoxImage.Question, MessageBoxResult.Cancel, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
            else
                e.Cancel = true;
        }

   

        private void AddContextMenu()
        {
            this.GenAppMenuItem();
            ContextMenu cm1 = new ContextMenu() { FontSize = 14, FontWeight = FontWeights.Bold };
            foreach (MenuItem mnui1 in this.MenuItemList)
            {
                if (mnui1.Uid.ToString().Contains("MenuItemGroup"))
                {
                    var mnui2 = new MenuItem() { Header = mnui1.Header };
                    foreach (MenuItem citem1 in mnui1.Items)
                    {
                        string[] muid2 = citem1.Uid.ToString().Split(',');
                        if (WpfProcessAccess.AppFormsList.FirstOrDefault(x => x.Contains(muid2[0])) != null)
                        {
                            var mnui2c = new MenuItem() { Header = citem1.Header, Uid = citem1.Uid, Tag = "Window", Height = 25 };
                            mnui2c.Click += this.MenuItem1_Click;
                            mnui2.Items.Add(mnui2c);
                        }
                    }
                    if (mnui2.Items.Count > 0)
                        cm1.Items.Add(mnui2);
                }
                else
                {
                    string[] muid1 = mnui1.Uid.ToString().Split(',');
                    if (WpfProcessAccess.AppFormsList.FirstOrDefault(x => x.Contains(muid1[0])) != null)
                    {
                        var mnui1c = new MenuItem() { Header = mnui1.Header, Uid = mnui1.Uid, Tag = "Window", Height = 25 };
                        mnui1c.Tag = "Window";
                        mnui1c.Height = 25;
                        mnui1c.Click += this.MenuItem1_Click;
                        cm1.Items.Add(mnui1c);
                    }
                }
            }
            if (cm1.Items.Count > 0)
            {
                cm1.Items.Add(new Separator());
                MenuItem mir18 = new MenuItem() { Header = "Clear All Windows", Tag = "ClearWindows", Uid = "CLEAR_ALL_WINDOWS", Height = 25 };
                mir18.Click += this.MenuItem1_Click;
                cm1.Items.Add(mir18);
            }
            //MenuItem mir1 = new MenuItem() { Header = "New Window", Tag = "Window", Uid = "ADD_NEW_WINDOW", Height = 35 };
            //mir1.Click += this.MenuItem1_Click;
            //cm1.Items.Add(mir1);

            //MenuItem mir1e = new MenuItem() { Header = "New Extended Window", Tag = "ExtWindow", Uid = "ADD_NEW_EXT_WINDOW", Height = 35 };
            //mir1e.Click += this.MenuItem1_Click;
            //cm1.Items.Add(mir1e);

            //cm1.Items.Add(new Separator());
            //MenuItem mir3 = new MenuItem() { Header = "New Tab", Tag = "Tab", Uid = "ADD_NEW_CUSTOM_TAB", Height = 35 };
            //mir3.Click += this.MenuItem1_Click;
            //cm1.Items.Add(mir3);

            //cm1.Items.Add(new Separator());
            //MenuItem mir19 = new MenuItem() { Header = "Clear All Tabs", Tag = "ClearTabs", Uid = "CLEAR_ALL_TABS", Height = 35 };
            //mir19.Click += this.MenuItem1_Click;
            //cm1.Items.Add(mir19);

            cm1.Items.Add(new Separator());
            MenuItem mir20 = new MenuItem() { Header = "E_xit Application", Tag = "EXIT", Uid = "EXIT_APPLICATION", Height = 35 };
            mir20.Click += this.MenuItem1_Click;
            cm1.Items.Add(mir20);
            this.imgClIcon.ContextMenu = cm1;
            this.MMGrid1.ContextMenu = cm1;
            this.btnASIT.ContextMenu = cm1;
            this.MainWindow1.ContextMenu = cm1;
            this.UcGrid1.ContextMenu = new ContextMenu();
        }
        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            string ObjectTag = ((MenuItem)sender).Tag.ToString();
            string ObjUid = ((MenuItem)sender).Uid.ToString();

            switch (ObjectTag)
            {
                case "EXIT":
                    this.Close();
                    break;
                case "Window":

                    if (WpfProcessAccess.AppFormsList == null)
                        return;

                    foreach (var item1 in WpfProcessAccess.AppFormsList)
                    {
                        //if (item1.GetType().ToString().Trim().Contains(((MenuItem)sender).Tag.ToString().Trim()))
                        string[] tagPart1 = ObjUid.Trim().Split(',');
                        //if (item1.GetType().ToString().Trim().Contains(tagPart1[0]))
                        if (item1.Contains(tagPart1[0]))
                        {
                            string frmTag1 = (tagPart1.Length > 1 ? tagPart1[1] : "");
                            string frmTag2 = (tagPart1.Length > 2 ? tagPart1[2] : "");
                            this.uc1Name = item1;
                            this.frmTag = frmTag1;
                            this.frmTag2 = frmTag2;
                            break;
                            //this.timerClick1.Start();
                            //this.frm_Show(item1, frmTag);
                            //return;
                        }
                    }

                    UserControl uc1 = WpfProcessAccess.CreateUserControl(this.uc1Name); //WpfProcessAccess.CreateUserControl(uc1Name1);
                    if (uc1 == null)
                        return;

                    uc1.Tag = (this.frmTag.Length > 0 ? this.frmTag : uc1.Tag);
                    uc1.Tag = (this.frmTag2.Length > 0 ? uc1.Tag + "," + this.frmTag2 : uc1.Tag);

                    HmsChildWindow win1 = new HmsChildWindow(uc1, this.frmTag, this.frmTag2) { Owner = this };
                    win1.Top = this.Height;
                    win1.Left = this.Width * -1;
                    win1.Show();
                    win1.Height = this.Height - this.Height * 0.25;// 130;
                    win1.Width = this.Width - this.Width * 0.25;// 80;
                    //win1.Top = this.Top + this.Height * 0.25 / 4 + 90;
                    //win1.Left = this.Left + this.Width * 0.25 / 2;
                    var Top1 = this.Top + this.Height * 0.25 / 4 + 90;
                    var Left1 = this.Left + this.Width * 0.25 / 2;

                    var sb = new Storyboard();


                    //Random rnd1 = new Random();
                    //int rnd2 = rnd1.Next(1, 100);

                    //if (rnd2 % 2 == 0 || rnd2 % 5 == 0 || rnd2 % 7 == 0)
                    //{
                    //    var movex = new DoubleAnimation() { From = 0, To = win1.Width, Duration = TimeSpan.FromSeconds(1) };
                    //    Storyboard.SetTarget(movex, win1);
                    //    Storyboard.SetTargetProperty(movex, new PropertyPath(Grid.WidthProperty));
                    //    sb.Children.Add(movex);
                    //}
                    //if (rnd2 % 3 == 0 || rnd2 % 5 == 0 || rnd2 % 11 == 0)
                    //{
                    //    var movey = new DoubleAnimation() { From = 0, To = win1.Height, Duration = TimeSpan.FromSeconds(1) };
                    //    Storyboard.SetTarget(movey, win1);
                    //    Storyboard.SetTargetProperty(movey, new PropertyPath(Grid.HeightProperty));
                    //    sb.Children.Add(movey);
                    //}

                    var moveX = new DoubleAnimation(Left1, new Duration(TimeSpan.FromSeconds(2)));
                    Storyboard.SetTarget(moveX, win1);
                    Storyboard.SetTargetProperty(moveX, new PropertyPath("(Canvas.Left)"));
                    sb.Children.Add(moveX);

                    var moveY = new DoubleAnimation(Top1, new Duration(TimeSpan.FromSeconds(2)));
                    Storyboard.SetTarget(moveY, win1);
                    Storyboard.SetTargetProperty(moveY, new PropertyPath("(Canvas.Top)"));
                    sb.Children.Add(moveY);

                    var fade = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(2) };
                    Storyboard.SetTarget(fade, win1);
                    Storyboard.SetTargetProperty(fade, new PropertyPath(Grid.OpacityProperty));
                    sb.Children.Add(fade);

                    sb.Begin();


                    break;

                case "ClearWindows":
                    foreach (Window item in this.OwnedWindows)
                        item.Close();
                    break;
                default:
                    break;
            }
        }

        public void ShowContextMenue(Object sender, ExecutedRoutedEventArgs e)
        {
            this.btnASIT_Click(null, null);
        }

        private void btnASIT_Click(object sender, RoutedEventArgs e)
        {
            this.btnASIT.ContextMenu.PlacementTarget = this.btnASIT; //sender as UIElement;
            this.btnASIT.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Top;
            this.btnASIT.ContextMenu.IsOpen = true;          
        }

        private void GenAppMenuItem()
        {
            this.MenuItemList.Clear();
            this.MenuItemList = WpfProcessAccess.GetCommonMenuItemList().ToList();
        }
    
    }
}
