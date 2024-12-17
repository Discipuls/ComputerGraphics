using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;


namespace Laba3_v4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap originalImage; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                originalImage = new Bitmap(openFileDialog.FileName);
                ImageDisplay.Source = BitmapToImageSource(originalImage);
            }
        }

        private void LinearContrastButton_Click(object sender, RoutedEventArgs e)
        {
            if (originalImage != null)
            {
                Bitmap resultImage = ImageProcessing.ApplyLinearContrast(originalImage);  
                ImageDisplay.Source = BitmapToImageSource(resultImage);  
            }
        }

        // Эквализация гистограммы
        private void EqualizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (originalImage != null)
            {
                Bitmap resultImage = ImageProcessing.EqualizeHistogram(originalImage);  
                ImageDisplay.Source = BitmapToImageSource(resultImage);
            }
        }

        private void SobelButton_Click(object sender, RoutedEventArgs e)
        {
            if (originalImage != null)
            {
                Bitmap resultImage = ImageProcessing.ApplySobelEdgeDetection(originalImage);
                ImageDisplay.Source = BitmapToImageSource(resultImage);
            }
        }

        private ImageSource BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Seek(0, SeekOrigin.Begin);
                PngBitmapDecoder decoder = new PngBitmapDecoder(memory, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }
    }
}
