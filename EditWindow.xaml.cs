using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace clipper
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        MainWindow main = new MainWindow();
        public EditWindow()
        {
            InitializeComponent();
        }        

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            main.Visibility = Visibility.Visible;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.DefaultExt = "png";
            dlg.Filter = "PNG|*.png|JPG|*.jpg;*.jpeg|BMP|*.bmp|GIF|*.gif";
            DialogResult res = dlg.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)displayImage.Source));
                using (FileStream stream = new FileStream(dlg.FileName, FileMode.Create))
                    encoder.Save(stream);
            }
        }

        private void btnClipBoard_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Size size = this.RenderSize;
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

            // Render a white background in Clipboard
            System.Windows.Shapes.Rectangle vRect = new System.Windows.Shapes.Rectangle()
            {
                Width = displayImage.Width,
                Height = displayImage.Height,
                Fill = System.Windows.Media.Brushes.White
            };
            vRect.Measure(size);
            vRect.Arrange(new Rect(size));
            rtb.Render(displayImage);

            this.Measure(size);
            this.Arrange(new Rect(size));
            rtb.Render(this);

            System.Windows.Clipboard.SetImage(rtb);
        }

        
    }
}
