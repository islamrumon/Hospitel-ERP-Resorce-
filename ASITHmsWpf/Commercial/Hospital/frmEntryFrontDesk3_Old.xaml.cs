using System;
using System.Collections;
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
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Threading;

namespace ASITHmsWpf.Commercial.Hospital
{
    /// <summary>
    /// Interaction logic for frmRegistration1.xaml
    /// </summary>
    public partial class frmEntryFrontDesk3_Old : UserControl
    {
        //DispatcherTimer timer1 = new DispatcherTimer();
        private DrCapture DrCam1;

        IntPtr m_ip = IntPtr.Zero;
        System.Windows.Forms.PictureBox picturebox1 = new System.Windows.Forms.PictureBox();
        const int VIDEODEVICE = 0; // zero based index of video capture device to use
        const int VIDEOWIDTH = 640; //  640; // Depends on video device caps
        const int VIDEOHEIGHT = 480; // 480; // Depends on video device caps
        const int VIDEOBITSPERPIXEL = 24; // BitsPerPixel values determined by device


        // https://msdn.microsoft.com/en-us/library/dd375468(v=vs.85).aspx for video capturing tutorials
        public frmEntryFrontDesk3_Old()
        {
            InitializeComponent();
            //timer1.Interval = TimeSpan.FromSeconds(1);
            //timer1.Tick += this.timer1_Tick;

            windowsFormsHost1.Child = picturebox1;
            // picturebox1.Height = 248;
            //picturebox1.Width = 328;
            //picturebox1.Paint += new System.Windows.Forms.PaintEventHandler(picturebox1_Paint);


            //DrCam1 = new DrCapture(VIDEODEVICE, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, picturebox1);
        }


        void picturebox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(@"C:\Temp\test.jpg");
            //System.Drawing.Point ulPoint = new System.Drawing.Point(0, 0);
            //e.Graphics.DrawImage(bmp, ulPoint);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
            //webcam = new HmsWebCamCapture();
            //webcam.InitializeWebCam(ref imgVideo);
        }

        private void bntSaveImage_Click(object sender, RoutedEventArgs e)
        {
            HmsWebCamHelper.SaveImageCapture((BitmapSource)imgCapture.Source);
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DrCam1.Dispose();
                if (m_ip != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(m_ip);
                    m_ip = IntPtr.Zero;
                }
            }
            catch
            {

            }
        }

        private void btnNewCapture_Click(object sender, RoutedEventArgs e)
        {
            //imgCapture.Source = imgRecord.Source;
            //return;
            try
            {
                if (DrCam1 == null)
                    return;
                if (DrCam1.Width <= 0)
                    return;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                // Release any previous buffer
                if (m_ip != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(m_ip);
                    m_ip = IntPtr.Zero;
                }

                // capture image
                m_ip = DrCam1.Click();
                System.Drawing.Bitmap b = new System.Drawing.Bitmap(DrCam1.Width, DrCam1.Height, DrCam1.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, m_ip);

                // If the image is upsidedown
                b.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);

                BitmapImage bitmapImage = new BitmapImage();

                using (var stream = new MemoryStream())
                {
                    b.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    stream.Seek(0, SeekOrigin.Begin);

                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }

                bitmapImage.Freeze();
                imgCapture.Dispatcher.Invoke((Action)(() => imgCapture.Source = bitmapImage));
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                //timer1.Stop();
                DrCam1.Dispose();
            }
            catch (Exception Exp)
            {

            }
            //this.windowsFormsHost1.Visibility = Visibility.Hidden;
        }

        private void btnNewStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //this.windowsFormsHost1.Visibility = Visibility.Visible;
                DrCam1 = new DrCapture(VIDEODEVICE, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, picturebox1);
                //timer1.Start();
            }
            catch (Exception Exp)
            {

            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DrCam1 != null)
                    this.ShowImageRecord();
                //tbArrivalDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy ddd hh:mm tt");
            }
            catch (Exception Exp)
            {

            }
        }

        private void ShowImageRecord()
        {
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                // Release any previous buffer
                if (m_ip != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(m_ip);
                    m_ip = IntPtr.Zero;
                }

                // capture image
                m_ip = DrCam1.Click();
                System.Drawing.Bitmap b = new System.Drawing.Bitmap(DrCam1.Width, DrCam1.Height, DrCam1.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, m_ip);

                // If the image is upsidedown
                b.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);

                BitmapImage bitmapImage = new BitmapImage();

                using (var stream = new MemoryStream())
                {
                    b.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    stream.Seek(0, SeekOrigin.Begin);

                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }

                bitmapImage.Freeze();
                imgRecord.Dispatcher.Invoke((Action)(() => imgCapture.Source = bitmapImage));
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                DrCam1.Dispose();
            }
            catch (Exception Exp)
            {

            }
        }

    }
}
