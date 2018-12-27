using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using System.Runtime.InteropServices;
using System.Windows.Interop;

using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Diagnostics;
using System.Windows.Forms;

using DirectShowLib;
using System.ComponentModel;
using System.Windows.Input;

using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

using System.Xml.Serialization;
using System.Text.RegularExpressions;

using System.Configuration;
using System.Data;

using System.Windows.Shapes;


namespace ASITHmsWpf
{
    public class HmsControlCalsses1
    {

    }

    #region RDLC report direct printing class
    public class RdlcDirectPrint
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;


        // Export the given report as an EMF (Enhanced Metafile) file.
        public void PrintReport(LocalReport report1, string PageSize = "A4", string PrinterName = "")
        {
            /*
                MarginTop = ReportViewer1.LocalReport.GetDefaultPageSettings.Margins.Top
                MarginLeft = ReportViewer1.LocalReport.GetDefaultPageSettings.Margins.Left
                MarginRight = ReportViewer1.LocalReport.GetDefaultPageSettings.Margins.Right
                MarginBottom = ReportViewer1.LocalReport.GetDefaultPageSettings.Margins.Bottom
              
                <PageHeight>11.69in</PageHeight>
                <PageWidth>8.27in</PageWidth>
                <LeftMargin>0.5in</LeftMargin>
                <RightMargin>0.25in</RightMargin>
                <TopMargin>0.5in</TopMargin>
                <BottomMargin>0.25in</BottomMargin>

             * For DeviceInfo Setup Pleae Visit -- Hafiz
              https://msdn.microsoft.com/en-us/library/hh231593.aspx
   

                string deviceInfo =
                  @"<DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>8.25in</PageWidth>
                    <PageHeight>11.75in</PageHeight>
                    <MarginTop>0.5in</MarginTop>
                    <MarginLeft>0.75in</MarginLeft>
                    <MarginRight>0.25in</MarginRight>
                    <MarginBottom>0.25in</MarginBottom>
                </DeviceInfo>";
            */

            ReportPageSettings ps1 = report1.GetDefaultPageSettings();
            string pageWidth1 = (ps1.PaperSize.Width / 100.00).ToString("##0.00").Trim();
            string pageHeight1 = (ps1.PaperSize.Height / 100.00).ToString("##0.00").Trim();
            string topmargin1 = (ps1.Margins.Top / 100.00).ToString("##0.00").Trim();
            string leftMargin1 = (ps1.Margins.Left / 100.00).ToString("##0.00").Trim();
            string rightMargin1 = (ps1.Margins.Right / 100.00).ToString("##0.00").Trim();
            string bottomMargin1 = (ps1.Margins.Bottom / 100.00).ToString("##0.00").Trim();

            string deviceInfo1 =
              @"<DeviceInfo>" +
                "<OutputFormat>EMF</OutputFormat>" +
                "<PageWidth>" + (ps1.IsLandscape ? pageHeight1 : pageWidth1) + "in</PageWidth>" +
                "<PageHeight>" + (ps1.IsLandscape ? pageWidth1 : pageHeight1) + "in</PageHeight>" +
                "<MarginTop>" + topmargin1 + "in</MarginTop>" +
                "<MarginLeft>" + leftMargin1 + "in</MarginLeft>" +
                "<MarginRight>" + rightMargin1 + "in</MarginRight>" +
                "<MarginBottom>" + bottomMargin1 + "in</MarginBottom>" +
            "</DeviceInfo>";

            //MessageBox.Show(ps1.PaperSize.Kind + "\n" + deviceInfo1);

            Warning[] warnings;
            m_streams = new List<Stream>();
            //report1.Render("Image", deviceInfo, CreateStream,  out warnings);
            report1.Render("Image", deviceInfo1, CreateStream, out warnings);

            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(ps1.PaperSize.Kind.ToString(),
                                                                                            int.Parse(ps1.PaperSize.Width.ToString()),
                                                                                            int.Parse(ps1.PaperSize.Height.ToString()));
            printDoc.DefaultPageSettings.Landscape = (ps1.IsLandscape);
            //PrintDocument pd = new PrintDocument();
            //pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PaperA4", 840, 1180);
            //pd.Print();
            if (PrinterName.Length > 0)
                printDoc.PrinterSettings.PrinterName = PrinterName; // "ASIT_SLK-TE321"

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

    }
    #endregion

    #region TreeView Customization Class
    // This class is used for managing Treeviews in Code Book entry forms.
    // Used in Content\HmsResDict01.xaml resource file
    public class TreeViewLineConverter : IValueConverter
    {
        // Used in HmsResDict01.xaml for controlling TreeViewItem Style
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);
            return ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
    #endregion

    #region Window Minimize and Maximize Controller Class
    //This class is used for preventing minimize option in report viewer window
    //This can be used for maximize option setup also
    // Used in HmsReportViewer1.xaml file. Can be used in any other window as required
    public static class WindowCustomizer
    {
        #region CanMaximize
        public static readonly DependencyProperty CanMaximize =
            DependencyProperty.RegisterAttached("CanMaximize", typeof(bool), typeof(Window),
                new PropertyMetadata(true, new PropertyChangedCallback(OnCanMaximizeChanged)));
        private static void OnCanMaximizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowHelper.EnableMaximize(window);
                    }
                    else
                    {
                        WindowHelper.DisableMaximize(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }
        public static void SetCanMaximize(DependencyObject d, bool value)
        {
            d.SetValue(CanMaximize, value);
        }
        public static bool GetCanMaximize(DependencyObject d)
        {
            return (bool)d.GetValue(CanMaximize);
        }
        #endregion CanMaximize

        #region CanMinimize
        public static readonly DependencyProperty CanMinimize =
            DependencyProperty.RegisterAttached("CanMinimize", typeof(bool), typeof(Window),
            new PropertyMetadata(true, new PropertyChangedCallback(OnCanMinimizeChanged)));
        private static void OnCanMinimizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;
            if (window != null)
            {
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate
                {
                    if ((bool)e.NewValue)
                    {
                        WindowHelper.EnableMinimize(window);
                    }
                    else
                    {
                        WindowHelper.DisableMinimize(window);
                    }
                    window.Loaded -= loadedHandler;
                };

                if (!window.IsLoaded)
                {
                    window.Loaded += loadedHandler;
                }
                else
                {
                    loadedHandler(null, null);
                }
            }
        }
        public static void SetCanMinimize(DependencyObject d, bool value)
        {
            d.SetValue(CanMinimize, value);
        }
        public static bool GetCanMinimize(DependencyObject d)
        {
            return (bool)d.GetValue(CanMinimize);
        }
        #endregion CanMinimize

        #region WindowHelper Nested Class
        public static class WindowHelper
        {
            private const Int32 GWL_STYLE = -16;
            private const Int32 WS_MAXIMIZEBOX = 0x00010000;
            private const Int32 WS_MINIMIZEBOX = 0x00020000;

            [DllImport("User32.dll", EntryPoint = "GetWindowLong")]
            private extern static Int32 GetWindowLongPtr(IntPtr hWnd, Int32 nIndex);

            [DllImport("User32.dll", EntryPoint = "SetWindowLong")]
            private extern static Int32 SetWindowLongPtr(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);

            /// <summary>
            /// Disables the maximize functionality of a WPF window.
            /// </summary>
            ///The WPF window to be modified.
            public static void DisableMaximize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MAXIMIZEBOX);
                }
            }

            /// <summary>
            /// Disables the minimize functionality of a WPF window.
            /// </summary>
            ///The WPF window to be modified.
            public static void DisableMinimize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MINIMIZEBOX);
                }
            }

            /// <summary>
            /// Enables the maximize functionality of a WPF window.
            /// </summary>
            ///The WPF window to be modified.
            public static void EnableMaximize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MAXIMIZEBOX);
                }
            }

            /// <summary>
            /// Enables the minimize functionality of a WPF window.
            /// </summary>
            ///The WPF window to be modified.
            public static void EnableMinimize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);
                    SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MINIMIZEBOX);
                }
            }

            /// <summary>
            /// Toggles the enabled state of a WPF window's maximize functionality.
            /// </summary>
            ///The WPF window to be modified.
            public static void ToggleMaximize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);

                    if ((windowStyle | WS_MAXIMIZEBOX) == windowStyle)
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MAXIMIZEBOX);
                    }
                    else
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MAXIMIZEBOX);
                    }
                }
            }

            /// <summary>
            /// Toggles the enabled state of a WPF window's minimize functionality.
            /// </summary>
            ///The WPF window to be modified.
            public static void ToggleMinimize(Window window)
            {
                lock (window)
                {
                    IntPtr hWnd = new WindowInteropHelper(window).Handle;
                    Int32 windowStyle = GetWindowLongPtr(hWnd, GWL_STYLE);

                    if ((windowStyle | WS_MINIMIZEBOX) == windowStyle)
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle & ~WS_MINIMIZEBOX);
                    }
                    else
                    {
                        SetWindowLongPtr(hWnd, GWL_STYLE, windowStyle | WS_MINIMIZEBOX);
                    }
                }
            }
        }
        #endregion WindowHelper Nested Class
    }
    #endregion

    #region TabControl Header Visiblility Class
    public class TabControlViewModel : INotifyPropertyChanged
    {
        private bool _tabHeaderVisible = true;
        public ICommand ToggleHeader
        {
            get;
            private set;
        }
        public bool TabHeaderVisible
        {
            get { return _tabHeaderVisible; }
            set
            {
                _tabHeaderVisible = value;
                OnPropertyChanged("TabHeaderVisible");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                changed(this, new PropertyChangedEventArgs(name));
            }
        }
    }
    #endregion

    #region RelativeDateValueConverter on Date format on DataGrid Group
    public class RelativeDateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as DateTime?;
            if (v == null)
            {
                return value;
            }

            return Convert(v.Value);
        }

        public static string Convert(DateTime v)
        {
            var d = v.Date;

            return d.ToString("dd-MMM-yyyy ddd");
        }

        public static int Compare(DateTime a, DateTime b)
        {
            return Convert(a) == Convert(b) ? 0 : a.CompareTo(b);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region Object XML File Manager
    public static class ObjXmlFileManager  // XmlSerialization
    {
        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
    #endregion
    #region Calculator Class
    public static class HmsCalculator
    {
        public static string Text2IntValue(string InputValue)
        {
            string OutputValue = "0.00";
            string[] num = Regex.Split(InputValue, @"\-|\+|\*|\/").Where(s => !String.IsNullOrEmpty(s)).ToArray(); // get Array for numbers
            string[] op = Regex.Split(InputValue, @"\d{1,3}").Where(s => !String.IsNullOrEmpty(s)).ToArray(); // get Array for mathematical operators +,-,/,*
            int numCtr = 0, lastVal = 0; // number counter and last Value accumulator
            string lastOp = ""; // last Operator
            foreach (string n in num)
            {
                numCtr++;
                if (numCtr == 1)
                {
                    lastVal = int.Parse(n); // if first loop lastVal will have the first numeric value
                }
                else
                {
                    if (!String.IsNullOrEmpty(lastOp)) // if last Operator not empty
                    {
                        // Do the mathematical computation and accumulation
                        switch (lastOp)
                        {
                            case "+":
                                lastVal = lastVal + int.Parse(n);
                                break;
                            case "-":
                                lastVal = lastVal - int.Parse(n);
                                break;
                            case "*":
                                lastVal = lastVal * int.Parse(n);
                                break;
                            case "/":
                                lastVal = lastVal / int.Parse(n);
                                break;

                        }
                    }
                }
                int opCtr = 0;
                foreach (string o in op)
                {
                    opCtr++;
                    if (opCtr == numCtr) //will make sure it will get the next operator
                    {
                        lastOp = o;  // get the last operator
                        break;
                    }
                }
                OutputValue = lastVal.ToString();
            }
            return OutputValue;
        }

        public static string Text2Value(string InputValue)
        {
            string OutputValue = "0.00";
            #region javascript calculat

            Type scriptType = Type.GetTypeFromCLSID(Guid.Parse("0E59F1D5-1FBE-11D0-8FF2-00A0D10038BC"));
            dynamic obj = Activator.CreateInstance(scriptType, false);
            obj.Language = "Javascript";
            string str = null;
            try
            {
                var res = obj.Eval(InputValue);
                str = Convert.ToString(res);
                //this.txtbFResult.Text = this.txtResult.Text + "=" + str;
                OutputValue = str;
            }
            catch (Exception)
            {
                return OutputValue;
                //throw;
            }
            #endregion
            return OutputValue;
        }
    }
    #endregion

    // Used in HCM General Information
    // Maybe not necessary for final delivery
    //public class SingInInfo
    //{
    //    string hccode { get; set; }
    //    string hcnamesub { get; set; }
    //    string hcname { get; set; }
    //}

    // *************************************************************
    #region WEBCAM MANAGEMENT FOR TAKING VIDEO AND STILL PHOTO FROM DIFFERENT SOURCE
    // Capturing image and video through webcam
    // Used in this application for capturing client photo mainly on front desk
    // and other useage as necessary
    public class DrCapture : ISampleGrabberCB, IDisposable
    {
        #region Member variables

        /// <summary> graph builder interface. </summary>
        private IFilterGraph2 m_FilterGraph = null;

        // Used to snap picture on Still pin
        private IAMVideoControl m_VidControl = null;
        private IPin m_pinStill = null;

        /// <summary> so we can wait for the async job to finish </summary>
        private ManualResetEvent m_PictureReady = null;

        private bool m_WantOne = false;

        /// <summary> Dimensions of the image, calculated once in constructor for perf. </summary>
        private int m_videoWidth;
        private int m_videoHeight;
        private int m_stride;

        /// <summary> buffer for bitmap data.  Always release by caller</summary>
        private IntPtr m_ipBuffer = IntPtr.Zero;

#if DEBUG
        // Allow you to "Connect to remote graph" from GraphEdit
        DsROTEntry m_rot = null;
#endif
        #endregion

        #region APIs
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, [MarshalAs(UnmanagedType.U4)] int Length);
        #endregion


        #region Capturing Methods and Properties
        // Zero based device index and device params and output window
        public DrCapture(int iDeviceNum, int iWidth, int iHeight, short iBPP, System.Windows.Forms.Control hControl)
        {
            DsDevice[] capDevices;

            // Get the collection of video devices
            capDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            if (iDeviceNum + 1 > capDevices.Length)
            {
                Dispose();
                return;
                //throw new Exception("No video capture devices found at that index!");
            }

            try
            {
                // Set up the capture graph
                SetupGraph(capDevices[iDeviceNum], iWidth, iHeight, iBPP, hControl);

                // tell the callback to ignore new images
                m_PictureReady = new ManualResetEvent(false);
            }
            catch
            {
                Dispose();
                throw new Exception("No video capture devices found at that index!");
                //throw;
            }
        }

        /// <summary> release everything. </summary>
        public void Dispose()
        {
#if DEBUG
            if (m_rot != null)
            {
                m_rot.Dispose();
            }
#endif
            CloseInterfaces();
            if (m_PictureReady != null)
            {
                m_PictureReady.Close();
            }
        }
        // Destructor
        ~DrCapture()
        {
            Dispose();
        }

        /// <summary>
        /// Get the image from the Still pin.  The returned image can turned into a bitmap with
        /// Bitmap b = new Bitmap(cam.Width, cam.Height, cam.Stride, PixelFormat.Format24bppRgb, m_ip);
        /// If the image is upside down, you can fix it with
        /// b.RotateFlip(RotateFlipType.RotateNoneFlipY);
        /// </summary>
        /// <returns>Returned pointer to be freed by caller with Marshal.FreeCoTaskMem</returns>
        public IntPtr Click()
        {
            int hr;
            if (m_PictureReady == null)
                return IntPtr.Zero;
            // get ready to wait for new image
            m_PictureReady.Reset();
            m_ipBuffer = Marshal.AllocCoTaskMem(Math.Abs(m_stride) * m_videoHeight);

            try
            {
                m_WantOne = true;

                // If we are using a still pin, ask for a picture
                if (m_VidControl != null)
                {
                    // Tell the camera to send an image
                    hr = m_VidControl.SetMode(m_pinStill, VideoControlFlags.Trigger);
                    DsError.ThrowExceptionForHR(hr);
                }

                // Start waiting
                if (!m_PictureReady.WaitOne(9000, false))
                {
                    throw new Exception("Timeout waiting to get picture");
                }
            }
            catch
            {
                Marshal.FreeCoTaskMem(m_ipBuffer);
                m_ipBuffer = IntPtr.Zero;
                throw;
            }

            // Got one
            return m_ipBuffer;
        }

        public int Width
        {
            get
            {
                return m_videoWidth;
            }
        }
        public int Height
        {
            get
            {
                return m_videoHeight;
            }
        }
        public int Stride
        {
            get
            {
                return m_stride;
            }
        }

        #endregion Capturing Methods and Properties

        #region build the capture graph for grabber

        /// <summary> build the capture graph for grabber. </summary>
        private void SetupGraph(DsDevice dev, int iWidth, int iHeight, short iBPP, System.Windows.Forms.Control hControl)
        {
            int hr;

            ISampleGrabber sampGrabber = null;
            IBaseFilter capFilter = null;
            IPin pCaptureOut = null;
            IPin pSampleIn = null;
            IPin pRenderIn = null;

            // Get the graphbuilder object
            m_FilterGraph = new FilterGraph() as IFilterGraph2;

            try
            {
#if DEBUG
                m_rot = new DsROTEntry(m_FilterGraph);
#endif
                // add the video input device
                hr = m_FilterGraph.AddSourceFilterForMoniker(dev.Mon, null, dev.Name, out capFilter);
                DsError.ThrowExceptionForHR(hr);

                // Find the still pin
                m_pinStill = DsFindPin.ByCategory(capFilter, PinCategory.Still, 0);

                // Didn't find one.  Is there a preview pin?
                if (m_pinStill == null)
                {
                    m_pinStill = DsFindPin.ByCategory(capFilter, PinCategory.Preview, 0);
                }

                // Still haven't found one.  Need to put a splitter in so we have
                // one stream to capture the bitmap from, and one to display.  Ok, we
                // don't *have* to do it that way, but we are going to anyway.
                if (m_pinStill == null)
                {
                    IPin pRaw = null;
                    IPin pSmart = null;

                    // There is no still pin
                    m_VidControl = null;

                    // Add a splitter
                    IBaseFilter iSmartTee = (IBaseFilter)new SmartTee();

                    try
                    {
                        hr = m_FilterGraph.AddFilter(iSmartTee, "SmartTee");
                        DsError.ThrowExceptionForHR(hr);

                        // Find the find the capture pin from the video device and the
                        // input pin for the splitter, and connnect them
                        pRaw = DsFindPin.ByCategory(capFilter, PinCategory.Capture, 0);
                        pSmart = DsFindPin.ByDirection(iSmartTee, PinDirection.Input, 0);

                        hr = m_FilterGraph.Connect(pRaw, pSmart);
                        DsError.ThrowExceptionForHR(hr);

                        // Now set the capture and still pins (from the splitter)
                        m_pinStill = DsFindPin.ByName(iSmartTee, "Preview");
                        pCaptureOut = DsFindPin.ByName(iSmartTee, "Capture");

                        // If any of the default config items are set, perform the config
                        // on the actual video device (rather than the splitter)
                        if (iHeight + iWidth + iBPP > 0)
                        {
                            SetConfigParms(pRaw, iWidth, iHeight, iBPP);
                        }
                    }
                    finally
                    {
                        if (pRaw != null)
                        {
                            Marshal.ReleaseComObject(pRaw);
                        }
                        if (pRaw != pSmart)
                        {
                            Marshal.ReleaseComObject(pSmart);
                        }
                        if (pRaw != iSmartTee)
                        {
                            Marshal.ReleaseComObject(iSmartTee);
                        }
                    }
                }
                else
                {
                    // Get a control pointer (used in Click())
                    m_VidControl = capFilter as IAMVideoControl;

                    pCaptureOut = DsFindPin.ByCategory(capFilter, PinCategory.Capture, 0);

                    // If any of the default config items are set
                    if (iHeight + iWidth + iBPP > 0)
                    {
                        SetConfigParms(m_pinStill, iWidth, iHeight, iBPP);
                    }
                }

                // Get the SampleGrabber interface
                sampGrabber = new SampleGrabber() as ISampleGrabber;

                // Configure the sample grabber
                IBaseFilter baseGrabFlt = sampGrabber as IBaseFilter;
                ConfigureSampleGrabber(sampGrabber);
                pSampleIn = DsFindPin.ByDirection(baseGrabFlt, PinDirection.Input, 0);

                // Get the default video renderer
                IBaseFilter pRenderer = new VideoRendererDefault() as IBaseFilter;
                hr = m_FilterGraph.AddFilter(pRenderer, "Renderer");
                DsError.ThrowExceptionForHR(hr);

                pRenderIn = DsFindPin.ByDirection(pRenderer, PinDirection.Input, 0);

                // Add the sample grabber to the graph
                hr = m_FilterGraph.AddFilter(baseGrabFlt, "Ds.NET Grabber");
                DsError.ThrowExceptionForHR(hr);

                if (m_VidControl == null)
                {
                    // Connect the Still pin to the sample grabber
                    hr = m_FilterGraph.Connect(m_pinStill, pSampleIn);
                    DsError.ThrowExceptionForHR(hr);

                    // Connect the capture pin to the renderer
                    hr = m_FilterGraph.Connect(pCaptureOut, pRenderIn);
                    DsError.ThrowExceptionForHR(hr);
                }
                else
                {
                    // Connect the capture pin to the renderer
                    hr = m_FilterGraph.Connect(pCaptureOut, pRenderIn);
                    DsError.ThrowExceptionForHR(hr);

                    // Connect the Still pin to the sample grabber
                    hr = m_FilterGraph.Connect(m_pinStill, pSampleIn);
                    DsError.ThrowExceptionForHR(hr);
                }

                // Learn the video properties
                SaveSizeInfo(sampGrabber);
                ConfigVideoWindow(hControl);

                // Start the graph
                IMediaControl mediaCtrl = m_FilterGraph as IMediaControl;
                hr = mediaCtrl.Run();
                DsError.ThrowExceptionForHR(hr);
            }
            finally
            {
                if (sampGrabber != null)
                {
                    Marshal.ReleaseComObject(sampGrabber);
                    sampGrabber = null;
                }
                if (pCaptureOut != null)
                {
                    Marshal.ReleaseComObject(pCaptureOut);
                    pCaptureOut = null;
                }
                if (pRenderIn != null)
                {
                    Marshal.ReleaseComObject(pRenderIn);
                    pRenderIn = null;
                }
                if (pSampleIn != null)
                {
                    Marshal.ReleaseComObject(pSampleIn);
                    pSampleIn = null;
                }
            }
        }

        private void SaveSizeInfo(ISampleGrabber sampGrabber)
        {
            int hr;

            // Get the media type from the SampleGrabber
            AMMediaType media = new AMMediaType();

            hr = sampGrabber.GetConnectedMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
            {
                throw new NotSupportedException("Unknown Grabber Media Format");
            }

            // Grab the size info
            VideoInfoHeader videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
            m_videoWidth = videoInfoHeader.BmiHeader.Width;
            m_videoHeight = videoInfoHeader.BmiHeader.Height;
            m_stride = m_videoWidth * (videoInfoHeader.BmiHeader.BitCount / 8);

            DsUtils.FreeAMMediaType(media);
            media = null;
        }
        #endregion
        // Set the video window within the control specified by hControl

        #region Configuring Video Window and Grabber
        private void ConfigVideoWindow(System.Windows.Forms.Control hControl)
        {
            int hr;

            IVideoWindow ivw = m_FilterGraph as IVideoWindow;

            // Set the parent
            hr = ivw.put_Owner(hControl.Handle);
            DsError.ThrowExceptionForHR(hr);

            // Turn off captions, etc
            hr = ivw.put_WindowStyle(DirectShowLib.WindowStyle.Child | DirectShowLib.WindowStyle.ClipChildren | DirectShowLib.WindowStyle.ClipSiblings);
            DsError.ThrowExceptionForHR(hr);

            // Yes, make it visible
            hr = ivw.put_Visible(OABool.True);
            DsError.ThrowExceptionForHR(hr);

            // Move to upper left corner
            System.Drawing.Rectangle rc = hControl.ClientRectangle;
            hr = ivw.SetWindowPosition(0, 0, rc.Right, rc.Bottom);
            DsError.ThrowExceptionForHR(hr);
        }

        private void ConfigureSampleGrabber(ISampleGrabber sampGrabber)
        {
            int hr;
            AMMediaType media = new AMMediaType();

            // Set the media type to Video/RBG24
            media.majorType = MediaType.Video;
            media.subType = MediaSubType.RGB24;
            media.formatType = FormatType.VideoInfo;
            hr = sampGrabber.SetMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(media);
            media = null;

            // Configure the samplegrabber
            hr = sampGrabber.SetCallback(this, 1);
            DsError.ThrowExceptionForHR(hr);
        }

        // Set the Framerate, and video size
        private void SetConfigParms(IPin pStill, int iWidth, int iHeight, short iBPP)
        {
            int hr;
            AMMediaType media;
            VideoInfoHeader v;

            IAMStreamConfig videoStreamConfig = pStill as IAMStreamConfig;

            // Get the existing format block
            hr = videoStreamConfig.GetFormat(out media);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                // copy out the videoinfoheader
                v = new VideoInfoHeader();
                Marshal.PtrToStructure(media.formatPtr, v);

                // if overriding the width, set the width
                if (iWidth > 0)
                {
                    v.BmiHeader.Width = iWidth;
                }

                // if overriding the Height, set the Height
                if (iHeight > 0)
                {
                    v.BmiHeader.Height = iHeight;
                }

                // if overriding the bits per pixel
                if (iBPP > 0)
                {
                    v.BmiHeader.BitCount = iBPP;
                }

                // Copy the media structure back
                Marshal.StructureToPtr(v, media.formatPtr, false);

                // Set the new format
                hr = videoStreamConfig.SetFormat(media);
                DsError.ThrowExceptionForHR(hr);
            }
            finally
            {
                DsUtils.FreeAMMediaType(media);
                media = null;
            }
        }

        #endregion Configuring Video Window and Grabber

        #region Shut down capture
        /// <summary> Shut down capture </summary>
        private void CloseInterfaces()
        {
            int hr;

            try
            {
                if (m_FilterGraph != null)
                {
                    IMediaControl mediaCtrl = m_FilterGraph as IMediaControl;

                    // Stop the graph
                    hr = mediaCtrl.Stop();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            if (m_FilterGraph != null)
            {
                Marshal.ReleaseComObject(m_FilterGraph);
                m_FilterGraph = null;
            }

            if (m_VidControl != null)
            {
                Marshal.ReleaseComObject(m_VidControl);
                m_VidControl = null;
            }

            if (m_pinStill != null)
            {
                Marshal.ReleaseComObject(m_pinStill);
                m_pinStill = null;
            }
        }

        /// <summary> sample callback, NOT USED. </summary>
        int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
        {
            Marshal.ReleaseComObject(pSample);
            return 0;
        }

        /// <summary> buffer callback, COULD BE FROM FOREIGN THREAD. </summary>
        int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            // Note that we depend on only being called once per call to Click.  Otherwise
            // a second call can overwrite the previous image.
            Debug.Assert(BufferLen == Math.Abs(m_stride) * m_videoHeight, "Incorrect buffer length");

            if (m_WantOne)
            {
                m_WantOne = false;
                Debug.Assert(m_ipBuffer != IntPtr.Zero, "Unitialized buffer");

                // Save the buffer
                CopyMemory(m_ipBuffer, pBuffer, BufferLen);

                // Picture is ready.
                m_PictureReady.Set();
            }

            return 0;
        }

        #endregion
    }

    public static class HmsImageManager
    {
        public static Bitmap ResizeImaze(Bitmap bitmap1 = null, int expWidth = 640, int expHeight = 480)
        {
            try
            {
                Bitmap bitmap2 = new Bitmap(expWidth, expHeight);

                double ratioX = (double)bitmap2.Width / (double)bitmap1.Width;
                double ratioY = (double)bitmap2.Height / (double)bitmap1.Height;
                double ratio = ratioX < ratioY ? ratioX : ratioY;

                int newHeight = Convert.ToInt32(bitmap1.Height * ratio);
                int newWidth = Convert.ToInt32(bitmap1.Width * ratio);

                using (Graphics g = Graphics.FromImage(bitmap2))
                {
                    g.DrawImage(bitmap1, 0, 0, newWidth, newHeight);
                }

                return bitmap2;
            }
            catch
            {
                return null;
            }
        }
    }
    public class HmsWebCamHelper
    {
        #region Loading and saving image as .jpg file
        //Block Memory Leak
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr handle);
        public static BitmapSource bs;
        public static IntPtr ip;
        public static BitmapSource LoadBitmap(System.Drawing.Bitmap source)
        {

            ip = source.GetHbitmap();

            bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, System.Windows.Int32Rect.Empty,

                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(ip);

            return bs;

        }
        public static void SaveImageCapture(BitmapSource bitmap)
        {
            if (bitmap == null)
                return;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.QualityLevel = 100;


            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Image" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + (new Random()).Next(1, 1000).ToString(); // Default file name
            dlg.DefaultExt = ".Jpg"; // Default file extension
            dlg.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save Image
                string filename = dlg.FileName;
                FileStream fstream = new FileStream(filename, FileMode.Create);
                encoder.Save(fstream);
                fstream.Close();
            }

        }
        #endregion
    }



    #endregion
}
