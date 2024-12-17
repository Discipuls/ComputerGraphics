using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Laba4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RedrawCanvas();
        }

        private void OnStepAlgorithmClick(object sender, RoutedEventArgs e)
        {
            DrawStepAlgorithm();
        }

        private void OnCDAAlgorithmClick(object sender, RoutedEventArgs e)
        {
            DrawCDAAlgorithm();
        }

        private void OnBresenhamAlgorithmClick(object sender, RoutedEventArgs e)
        {
            DrawBresenhamAlgorithm();
        }

        private void OnBresenhamCircleClick(object sender, RoutedEventArgs e)
        {
            DrawBresenhamCircle();
        }

        private void RedrawCanvas()
        {
            DrawingCanvas.Children.Clear();
            DrawAxes();
            DrawGrid();
        }

        private void DrawAxes()
        {
            DrawingCanvas.Children.Add(new Line
            {
                X1 = 400,
                Y1 = 0,
                X2 = 400,
                Y2 = 600,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            });

            DrawingCanvas.Children.Add(new Line
            {
                X1 = 0,
                Y1 = 300,
                X2 = 800,
                Y2 = 300,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            });

            DrawingCanvas.Children.Add(new TextBlock
            {
                Text = "Ось X",
                FontSize = 16,
                Foreground = Brushes.Black,
                Margin = new Thickness(410, 10, 0, 0)
            });

            DrawingCanvas.Children.Add(new TextBlock
            {
                Text = "Ось Y",
                FontSize = 16,
                Foreground = Brushes.Black,
                Margin = new Thickness(10, 290, 0, 0)
            });
        }

        private void DrawGrid()
        {
            for (int i = -30; i <= 30; i++)
            {
                DrawingCanvas.Children.Add(new Line
                {
                    X1 = (i * 10 + 400),
                    Y1 = 0,
                    X2 = (i * 10 + 400),
                    Y2 = 600,
                    Stroke = Brushes.LightGray,
                    StrokeThickness = 0.5
                });

                DrawingCanvas.Children.Add(new Line
                {
                    X1 = 0,
                    Y1 = (i * 10 + 300),
                    X2 = 800,
                    Y2 = (i * 10 + 300),
                    Stroke = Brushes.LightGray,
                    StrokeThickness = 0.5
                });

                if (i != 0)
                {
                    if (i % 5 == 0)
                    {
                        DrawingCanvas.Children.Add(new TextBlock
                        {
                            Text = (i * 10).ToString(),
                            FontSize = 12,
                            Foreground = Brushes.Black,
                            Margin = new Thickness((i * 10 + 400) + 5, 290, 0, 0)
                        });
                        DrawingCanvas.Children.Add(new TextBlock
                        {
                            Text = (i * 10).ToString(),
                            FontSize = 12,
                            Foreground = Brushes.Black,
                            Margin = new Thickness(10, (i * 10 + 300) - 8, 0, 0)
                        });
                    }


                  
                }
            }
        }

        private void ClearCanvas()
        {
            DrawingCanvas.Children.Clear();
            RedrawCanvas();
        }


        private void DrawStepAlgorithm()
        {
            ClearCanvas();
            OutputTextBlock.Text = "Алгоритм Пошаговой растеризации (вычисления):\n";

            int x1 = 100, y1 = 100, x2 = 700, y2 = 500;
            Algorithms.StepAlgorithm(DrawingCanvas, x1, y1, x2, y2, OutputTextBlock);
        }

        private void DrawCDAAlgorithm()
        {
            ClearCanvas();
            OutputTextBlock.Text = "Алгоритм ЦДА (вычисления):\n";

            int x1 = 100, y1 = 100, x2 = 700, y2 = 500;
            Algorithms.CDAAlgorithm(DrawingCanvas, x1, y1, x2, y2, OutputTextBlock);
        }

        private void DrawBresenhamAlgorithm()
        {
            ClearCanvas();
            OutputTextBlock.Text = "Алгоритм Брезенхема (вычисления):\n";

            int x1 = 100, y1 = 100, x2 = 700, y2 = 500;
            Algorithms.BresenhamAlgorithm(DrawingCanvas, x1, y1, x2, y2, OutputTextBlock);
        }

        private void DrawBresenhamCircle()
        {
            ClearCanvas();
            OutputTextBlock.Text = "Алгоритм Брезенхема (окружность) (вычисления):\n";

            int xc = 400, yc = 300, r = 200;
            Algorithms.BresenhamCircle(DrawingCanvas, xc, yc, r, OutputTextBlock);
        }
    }
}
