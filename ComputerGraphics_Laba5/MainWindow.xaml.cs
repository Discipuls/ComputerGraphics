using System.Windows;

namespace Laba5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SutherlandCohenButton_Click(object sender, RoutedEventArgs e)
        {
            SutherlandCohenWindow sutherlandCohenWindow = new SutherlandCohenWindow();
            sutherlandCohenWindow.Show();
        }

        private void PolygonClipButton_Click(object sender, RoutedEventArgs e)
        {
            PolygonClipWindow polygonClipWindow = new PolygonClipWindow();
            polygonClipWindow.Show();
        }
    }
}
