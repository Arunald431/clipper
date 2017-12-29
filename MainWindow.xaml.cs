using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace clipper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void manualClipButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CanvasWindow canvas = new CanvasWindow();            
            canvas.WindowState = WindowState.Maximized;
            canvas.ShowDialog();            
        }

        private void fullClipButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();   
            Thread.Sleep(200);
            Bitmap screenShotBmp = new Bitmap((int)System.Windows.Forms.SystemInformation.VirtualScreen.Width , (int)System.Windows.Forms.SystemInformation.VirtualScreen.Height - 60, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(screenShotBmp);
            g.CopyFromScreen(0, 0, 0, 0, screenShotBmp.Size, CopyPixelOperation.SourceCopy);
            

            IntPtr handle = IntPtr.Zero;
            handle = screenShotBmp.GetHbitmap();
            EditScreenshot editWindow = new EditScreenshot();
            editWindow.Show();
            var image = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            editWindow.displayImage.Source = image;
            Clipboard.SetImage(image);
        }
    }
}
