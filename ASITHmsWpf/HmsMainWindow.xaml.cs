using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ASITHmsWpf
{
    /// <summary>
    /// Interaction logic for HmsMainWindow.xaml
    /// </summary>
    public partial class HmsMainWindow : Window
    {

        private bool FrmInitialized = false;
        private DispatcherTimer timer1 = new DispatcherTimer();
        private int TabItemIndex1 = 1;
        private int TabItemIndex1p = -1;
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
        private string ModuleOption1 = "Nothing";
        public HmsMainWindow()
        {
            InitializeComponent();
        }
        public HmsMainWindow(string ModuleOption = "")
        {
            InitializeComponent();
            this.ModuleOption1 = ModuleOption;

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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            if (this.FrmInitialized)
                return;

            this.timer1.Interval = TimeSpan.FromSeconds(30);
            this.timer1.Tick += this.timer1_Tick;
            this.timer1.Start();

            this.FrmInitialized = true;
            //this.TabUcGrid1.Visibility = Visibility.Collapsed;
            var empname1 = WpfProcessAccess.StaffList.FindAll(x => x.sircode == WpfProcessAccess.SignedInUserList[0].hccode);
            this.lblSignInNam.Content = WpfProcessAccess.SignedInUserList[0].signinnam.ToString();
            this.lblSignInNam.ToolTip = (empname1 == null ? WpfProcessAccess.SignedInUserList[0].hcname.ToString() : empname1[0].sirdesc.Trim());
            this.lblSessionId.Content = WpfProcessAccess.SignedInUserList[0].sessionID.ToString();
            this.lblTeminalId.Content = WpfProcessAccess.SignedInUserList[0].terminalID.ToString();

            this.AddContextMenu();
            this.AddDashBoardMenue();
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
                ////this.mnuFlowChart1.SetTitleImage(bmp3);
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
            ////this.mnuFlowChart1.ShowTabInformation(0);
            ////this.ChangeFlowChartVisibility(0);

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
                this.slider1.Maximum = 2;
                this.slider1.Value = 1.6;
            }
            else if (this.psWidth > 1900)  // (System.Windows.SystemParameters.VirtualScreenWidth > 1900)
            {
                this.slider1.Minimum = 1.0;
                this.slider1.Maximum = 1.5;
                this.slider1.Value = 1.4;
            }
            else
            {
                //MessageBox.Show(System.Windows.SystemParameters.VirtualScreenWidth.ToString());
                this.slider1.Minimum = 0.7;
                this.slider1.Maximum = 1.5;
                this.slider1.Value = 0.95;
            }

        }

     

        private void SliderZoom(object sender, ExecutedRoutedEventArgs e)
        {
            //int.Parse(e.Parameter.ToString().Trim()) - 1;
            if (e.Parameter.ToString().Trim() == "ZoomOut")
                slider1.Value += 0.1;// slider1.TickFrequency;
            else
                slider1.Value -= 0.1;
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FlowDocument flw = new FlowDocument();

            if (this.XpValue == 0)
            {
                this.XpValue = this.Width;
                this.YpValue = this.Height;
            }
            this.ResizeZoom(this.Width / this.XpValue * 0.95, this.Height / this.YpValue * 0.95, ReqestSource: "Window");

        }
        private void ResizeZoom(double XScaleValue, double YScaleValue, string ReqestSource = "Slider")
        {
            var scaler = this.UcGrid1.LayoutTransform as ScaleTransform;
            //var scaleg1 = this.HMGrid1.LayoutTransform as ScaleTransform;
            //var scaleg2 = this.DevGrid1.LayoutTransform as ScaleTransform;
            //HMGrid1   DevGrid1

            if (scaler == null)
            {
                this.UcGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
                //this.HMGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
                //this.DevGrid1.LayoutTransform = new ScaleTransform(XScaleValue, YScaleValue);
  
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
            }

            //if(ReqestSource == "Window")
            //{
            //    //scaleg1.ScaleX = XScaleValue;
            //    //scaleg1.ScaleY = YScaleValue;

            ////    scaleg2.ScaleX = XScaleValue;
            ////    scaleg2.ScaleY = YScaleValue;
            //}

            //this.slider1.ToolTip = (slider1.Value).ToString("##0%");
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            this.ResizeZoom(slider1.Value, slider1.Value);
            this.slider1.ToolTip = (slider1.Value).ToString("##0%");
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (this.Visibility != Visibility.Visible)
            {
                //if (WpfProcessAccess.AppUserLogLevel != "00")
                //    WpfProcessAccess.UpdateUserLogInfo(logref1: "Auto Sign-Out and Shutdown Successfull. Terminal Time : " + DateTime.Now.ToString("dddd dd-MMM-yyyy hh:mm:ss.fff tt"));

                Application.Current.Shutdown();
                return;
            }

            if (System.Windows.MessageBox.Show("Are you confirm to close application", WpfProcessAccess.AppTitle, MessageBoxButton.YesNoCancel,
                            MessageBoxImage.Question, MessageBoxResult.Cancel, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
            {
                if (WpfProcessAccess.AppUserLogLevel > 0)
                    WpfProcessAccess.UpdateUserLogInfo(logref1: "Sign-Out Successfull");

                Application.Current.Shutdown();
            }
            else
                e.Cancel = true;
        }

        private void TabItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            TabItem item = (TabItem)sender;
            if (item != null && Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)
                DragDrop.DoDragDrop(item, item, DragDropEffects.All);
        }
        private void TabItem_Drop(object sender, DragEventArgs e)
        {
            TabItem target = (TabItem)sender;
            TabItem source = (TabItem)e.Data.GetData(typeof(TabItem));
            if (source != null && target != null && !source.Equals(target))
            {
                TabControl tab = (TabControl)source.Parent;
                int sourceIndex = tab.Items.IndexOf(source);
                int targetIndex = tab.Items.IndexOf(target);
                tab.Items.Remove(source);
                tab.Items.Insert(targetIndex, source);

                // For Place Swiping of tab items
                //tab.Items.Remove(target);
                //tab.Items.Insert(sourceIndex, target);
            }
        }
        private void btnAppClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_StateChanged(object sender, EventArgs e)
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

        private void tbArrivalDateTime_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.dtpMW1.IsDropDownOpen = true;
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
                        {
                            MoveFocus_Next(textBoxBase);
                        }

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
                    {
                        return;
                    }
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
                            var mnui2c = new MenuItem() { Header = citem1.Header, Uid = citem1.Uid, Tag = "Tab", Height = 25 };
                            //var mnui2c = new MenuItem() { Header = citem1.Header, Uid = citem1.Uid, Tag = "Window", Height = 25 };
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
                        var mnui1c = new MenuItem() { Header = mnui1.Header, Uid = mnui1.Uid, Tag = "Tab", Height = 25 };
                        //var mnui1c = new MenuItem() { Header = mnui1.Header, Uid = mnui1.Uid, Tag = "Window", Height = 25 };
                        //mnui1c.Tag = "Window";
                        //mnui1c.Height = 25;
                        mnui1c.Click += this.MenuItem1_Click;
                        cm1.Items.Add(mnui1c);
                    }
                }
            }
            cm1.Items.Add(new Separator());
            MenuItem mir17 = new MenuItem() { Header = "_Window Based Screen" };
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
                        mir17.Items.Add(mnui2);
                }
                else
                {
                    string[] muid1 = mnui1.Uid.ToString().Split(',');
                    if (WpfProcessAccess.AppFormsList.FirstOrDefault(x => x.Contains(muid1[0])) != null)
                    {
                        var mnui1c = new MenuItem() { Header = mnui1.Header, Uid = mnui1.Uid, Tag = "Window", Height = 25 };
                        //mnui1c.Tag = "Window";
                        //mnui1c.Height = 25;
                        mnui1c.Click += this.MenuItem1_Click;
                        mir17.Items.Add(mnui1c);
                    }
                }
            }
            cm1.Items.Add(mir17);
            cm1.Items.Add(new Separator());
            if (cm1.Items.Count > 0)
            {
                cm1.Items.Add(new Separator());
                MenuItem mir18 = new MenuItem() { Header = "_Close All Windows", Tag = "ClearWindows", Uid = "CLEAR_ALL_WINDOWS", Height = 25 };
                mir18.Click += this.MenuItem1_Click;
                cm1.Items.Add(mir18);

                cm1.Items.Add(new Separator());
                MenuItem mir19 = new MenuItem() { Header = "Close All _Tabs", Tag = "ClearTabs", Uid = "CLEAR_ALL_TABS", Height = 35 };
                mir19.Click += this.MenuItem1_Click;
                cm1.Items.Add(mir19);
            }

            cm1.Items.Add(new Separator());
            MenuItem mir20 = new MenuItem() { Header = "E_xit Application", Tag = "EXIT", Uid = "EXIT_APPLICATION", Height = 35 };
            mir20.Click += this.MenuItem1_Click;
            cm1.Items.Add(mir20);
            this.imgClIcon.ContextMenu = cm1;
            this.UcGrid1.ContextMenu = cm1;
            this.btnASIT.ContextMenu = cm1;
            this.TabUcGrid1.ContextMenu = new ContextMenu();
        }

        private void GenAppMenuItem()
        {
            this.MenuItemList.Clear();
            if (this.ModuleOption1.Trim().Length == 0 || this.ModuleOption1 == "Nothing")
                this.MenuItemList = WpfProcessAccess.GetCommonMenuItemList().ToList();
            else
                this.MenuItemList = WpfProcessAccess.GetCommonMenuItemList(this.ModuleOption1).ToList();

            WpfProcessAccess.AppMenuItemList = this.MenuItemList;
        }
        private void AddDashBoardMenue()
        {
            this.ucDashboard1.tvMenu1.Items.Clear();
            //ContextMenu cm2 = this.UcGrid1.ContextMenu;
            foreach (var item1 in this.btnASIT.ContextMenu.Items)
            {
                if (item1 is MenuItem)
                {
                    MenuItem mnui1 = item1 as MenuItem;
                    var t1 = new TreeViewItem() { Header = mnui1.Header.ToString().Replace("_", ""), Tag = mnui1.Tag, Uid = mnui1.Uid, Cursor = Cursors.Hand };
                    foreach (var item2 in mnui1.Items)
                    {
                        if (item2 is MenuItem)
                        {
                            MenuItem mnui2 = item2 as MenuItem;
                            var t2 = new TreeViewItem() { Header = mnui2.Header.ToString().Replace("_", ""), Tag = mnui2.Tag, Uid = mnui2.Uid, Cursor = Cursors.Hand };
                            foreach (var item3 in mnui2.Items)
                            {
                                MenuItem mnui3 = item3 as MenuItem;
                                var t3 = new TreeViewItem() { Header = mnui3.Header.ToString().Replace("_", ""), Tag = mnui3.Tag, Uid = mnui3.Uid, FontWeight = FontWeights.Normal, Cursor = Cursors.Hand };
                                t3.KeyUp += TreeItem_KeyUp;
                                t3.MouseDoubleClick += TreeItem_MouseDoubleClick;
                                t2.Items.Add(t3);
                            }
                            if(t2.Items.Count ==0)
                            {
                                t2.FontWeight = FontWeights.Normal;
                                t2.KeyUp += TreeItem_KeyUp;
                                t2.MouseDoubleClick += TreeItem_MouseDoubleClick;
                            }
                            t1.Items.Add(t2);
                        }
                    }
                    if (t1.Items.Count == 0)
                    {

                        t1.KeyUp += TreeItem_KeyUp;
                        t1.MouseDoubleClick += TreeItem_MouseDoubleClick;
                    }
                    else
                    {
                        t1.Header = t1.Header.ToString().ToUpper();
                        t1.IsExpanded = true;
                    }
                    this.ucDashboard1.tvMenu1.Items.Add(t1);
                }
            }
        }

        void TreeItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string ObjectTag = ((TreeViewItem)sender).Tag.ToString();
            string ObjUid = ((TreeViewItem)sender).Uid.ToString();
            this.MenuItemsClick(ObjectTag, ObjUid);
        }

        void TreeItem_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return || e.Key == Key.Space)
            {
                string ObjectTag = ((TreeViewItem)sender).Tag.ToString();
                string ObjUid = ((TreeViewItem)sender).Uid.ToString();
                this.MenuItemsClick(ObjectTag, ObjUid);
            }
        }

        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            string ObjectTag = ((MenuItem)sender).Tag.ToString();
            string ObjUid = ((MenuItem)sender).Uid.ToString();
            this.MenuItemsClick(ObjectTag, ObjUid);
        }
        private void MenuItemsClick(string ObjectTag, string ObjUid)
        {
            //string ObjectTag = ((MenuItem)sender).Tag.ToString();
            //string ObjUid = ((MenuItem)sender).Uid.ToString();

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
                case "Tab":
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
                        }
                    }

                    UserControl uc1t = WpfProcessAccess.CreateUserControl(this.uc1Name); //WpfProcessAccess.CreateUserControl(uc1Name1);
                    if (uc1t == null)
                        return;

                    uc1t.Tag = (this.frmTag.Length > 0 ? this.frmTag : uc1t.Tag);
                    uc1t.Tag = (this.frmTag2.Length > 0 ? uc1t.Tag + "," + this.frmTag2 : uc1t.Tag);

                    ++this.TabItemIndex1;
                    UserControls.TabItemWithButton tbi1c = new UserControls.TabItemWithButton(_header1: "Page - " + this.TabItemIndex1.ToString("00"), _uc1: uc1t);
                    tbi1c.btn1.MouseDoubleClick += Btn1_MouseDoubleClick;
                    tbi1c.btn1.KeyUp += Btn1_KeyUp;
                    this.TabUcGrid1.Items.Add(tbi1c);
                    this.TabUcGrid1.SelectedIndex = this.TabUcGrid1.Items.Count - 1;
                    this.TabUcGrid1.Visibility = Visibility.Visible;
                    break;
                case "ClearWindows":
                    foreach (Window item in this.OwnedWindows)
                        item.Close();
                    break;
                case "ClearTabs":
                    int tabcnt = this.TabUcGrid1.Items.Count - 1;
                    for (int i = tabcnt; i > 0; i--)
                        this.TabUcGrid1.Items.RemoveAt(i);

                    this.TabItemIndex1 = 1;
                    this.TabItemIndex1p = -1;
                    this.TabUcGrid1.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }

        private void Btn1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.RemoveTabItem(((Button)sender).Tag.ToString());
        }

        private void Btn1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.RemoveTabItem(((Button)sender).Tag.ToString());
        }
        private void RemoveTabItem(string tag1 = "Nothing")
        {
            foreach (TabItem item1 in this.TabUcGrid1.Items)
            {
                if (tag1 == item1.Tag.ToString())
                {
                    this.TabUcGrid1.Items.Remove(item1);
                    break;
                }
            }
            if (this.TabUcGrid1.Items.Count == 1)
            {
                this.TabItemIndex1 = 1;
                this.TabUcGrid1.SelectedIndex = 0;
            }
            this.TabItemIndex1p = -1;
            this.TabUcGrid1_SelectionChanged(null, null);
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

    
        private void TabUcGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.TabUcGrid1.SelectedItem == null)
                return;
            if (this.TabItemIndex1p == this.TabUcGrid1.SelectedIndex)
                return;

            this.TabItemIndex1p = this.TabUcGrid1.SelectedIndex;
            TabItem tabItem = this.TabUcGrid1.Items[this.TabUcGrid1.SelectedIndex] as TabItem;
            DoubleAnimation anim = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(1000));
            var uc1t = (UserControl)(this.TabUcGrid1.SelectedIndex <= 0 ? this.ucDashboard1 : ((UserControls.TabItemWithButton)this.TabUcGrid1.SelectedItem).uc1);
            DoubleAnimation anim1 = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(1000));
            tabItem.BeginAnimation(TabItem.OpacityProperty, anim);
            uc1t.BeginAnimation(TabItem.OpacityProperty, anim1);

            if (this.TabUcGrid1.SelectedIndex <= 0)
            {
                this.txtTitle2.Text = "Dashboard";
                return;
            }

            if (((UserControls.TabItemWithButton)this.TabUcGrid1.SelectedItem).uc1.Tag != null)
            {
                this.txtTitle2.Text = uc1t.Tag.ToString();  // ((UserControls.TabItemWithButton)this.TabUcGrid1.SelectedItem).uc1.Tag.ToString();
                //tabItem.ToolTip = uc1t.Tag.ToString();      // ((UserControls.TabItemWithButton)this.TabUcGrid1.SelectedItem).uc1.Tag.ToString();
            }
        }

        private void txtTitle2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
                this.TabUcGrid1.SelectedIndex = 0;
        }
    }
}
