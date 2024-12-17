using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace Laba5
{
    public partial class SutherlandCohenWindow : Window
    {
        public SutherlandCohenWindow()
        {
            InitializeComponent();
        }

        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "data.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                int segmentCount = int.Parse(lines[0]);

                List<Segment> segments = new List<Segment>();
                for (int i = 1; i <= segmentCount; i++)
                {
                    string[] parts = lines[i].Split(' ');
                    double x1 = double.Parse(parts[0]);
                    double y1 = double.Parse(parts[1]);
                    double x2 = double.Parse(parts[2]);
                    double y2 = double.Parse(parts[3]);
                    segments.Add(new Segment(x1, y1, x2, y2));
                }

                string[] windowParts = lines[segmentCount + 1].Split(' ');
                double xmin = double.Parse(windowParts[0]);
                double ymin = double.Parse(windowParts[1]);
                double xmax = double.Parse(windowParts[2]);
                double ymax = double.Parse(windowParts[3]);

                var segmentsCopy = new List<Segment>(segments);
                for(int i = 0;i < segmentsCopy.Count(); i++)
                {
                    segmentsCopy[i] = new Segment(segmentsCopy[i].X1, segmentsCopy[i].Y1, segmentsCopy[i].X2, segmentsCopy[i].Y2);
                }
                List<Segment> clippedSegments = SutherlandCohenClip(segmentsCopy, xmin, ymin, xmax, ymax);

                DrawCoordinateSystem(Canvas.ActualWidth, Canvas.ActualHeight, 100);

                DrawRectangle(xmin, ymin, xmax, ymax, Brushes.LightGray);

                foreach (var segment in segments)
                {
                    DrawLine(segment.X1, segment.Y1, segment.X2, segment.Y2, Brushes.Purple);
                }

                foreach (var segment in clippedSegments)
                {
                    DrawLine(segment.X1, segment.Y1, segment.X2, segment.Y2, Brushes.Orange);
                }
            }
        }

        private void DrawCoordinateSystem(double canvasWidth, double canvasHeight, double step)
        {
            Line xAxis = new Line
            {
                X1 = 0,
                Y1 = canvasHeight / 2,
                X2 = canvasWidth,
                Y2 = canvasHeight / 2,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Canvas.Children.Add(xAxis);

            Line yAxis = new Line
            {
                X1 = canvasWidth / 2,
                Y1 = 0,
                X2 = canvasWidth / 2,
                Y2 = canvasHeight,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Canvas.Children.Add(yAxis);

            TextBlock xAxisLabel = new TextBlock
            {
                Text = "X",
                Foreground = Brushes.Black,
                FontSize = 14
            };
            Canvas.SetLeft(xAxisLabel, canvasWidth - 20);
            Canvas.SetTop(xAxisLabel, canvasHeight / 2 - 20);
            Canvas.Children.Add(xAxisLabel);

            TextBlock yAxisLabel = new TextBlock
            {
                Text = "Y",
                Foreground = Brushes.Black,
                FontSize = 14
            };
            Canvas.SetLeft(yAxisLabel, canvasWidth / 2 + 10);
            Canvas.SetTop(yAxisLabel, 10);
            Canvas.Children.Add(yAxisLabel);

            for (double x = canvasWidth / 2; x <= canvasWidth; x += step) 
            {
                Line line = new Line
                {
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = canvasHeight,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection() { 2, 2 } 
                };
                Canvas.Children.Add(line);

                TextBlock label = new TextBlock
                {
                    Text = ((x - canvasWidth / 2)).ToString(),
                    Foreground = Brushes.Black,
                    FontSize = 10
                };
                Canvas.SetLeft(label, x - 10);
                Canvas.SetTop(label, canvasHeight / 2 + 10);
                Canvas.Children.Add(label);
            }

            for (double x = canvasWidth / 2; x >= 0; x -= step) 
            {
                Line line = new Line
                {
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = canvasHeight,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection() { 2, 2 } 
                };
                Canvas.Children.Add(line);

                TextBlock label = new TextBlock
                {
                    Text = ((canvasWidth / 2 - x)).ToString(),
                    Foreground = Brushes.Black,
                    FontSize = 10
                };
                Canvas.SetLeft(label, x - 10);
                Canvas.SetTop(label, canvasHeight / 2 + 10);
                Canvas.Children.Add(label);
            }

            for (double y = canvasHeight / 2; y <= canvasHeight; y += step)
            {
                Line line = new Line
                {
                    X1 = 0,
                    Y1 = y,
                    X2 = canvasWidth,
                    Y2 = y,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection() { 2, 2 }
                };
                Canvas.Children.Add(line);

                TextBlock label = new TextBlock
                {
                    Text = (-(y - canvasHeight / 2)).ToString(),
                    Foreground = Brushes.Black,
                    FontSize = 10
                };
                Canvas.SetLeft(label, canvasWidth / 2 + 10);
                Canvas.SetTop(label, y - 10);
                Canvas.Children.Add(label);
            }

            for (double y = canvasHeight / 2; y >= 0; y -= step)
            {
                Line line = new Line
                {
                    X1 = 0,
                    Y1 = y,
                    X2 = canvasWidth,
                    Y2 = y,
                    Stroke = Brushes.Gray,
                    StrokeThickness = 1,
                    StrokeDashArray = new DoubleCollection() { 2, 2 }
                };
                Canvas.Children.Add(line);

                TextBlock label = new TextBlock
                {
                    Text = ((canvasHeight / 2 - y)).ToString(),
                    Foreground = Brushes.Black,
                    FontSize = 10
                };
                Canvas.SetLeft(label, canvasWidth / 2 + 10);
                Canvas.SetTop(label, y - 10);
                Canvas.Children.Add(label);
            }
        }


        private void DrawRectangle(double xmin, double ymin, double xmax, double ymax, Brush color)
        {
            Rectangle rect = new Rectangle
            {
                Width = xmax - xmin,
                Height = ymax - ymin,
                Stroke = color,
                StrokeThickness = 2
            };
            System.Windows.Controls.Canvas.SetLeft(rect, xmin);
            System.Windows.Controls.Canvas.SetTop(rect, ymin);
            Canvas.Children.Add(rect);
        }

        private void DrawLine(double x1, double y1, double x2, double y2, Brush color)
        {
            Line line = new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                Stroke = color,
                StrokeThickness = 2
            };
            Canvas.Children.Add(line);
        }

        public List<Segment> SutherlandCohenClip(List<Segment> segments, double xmin, double ymin, double xmax, double ymax)
        {
            List<Segment> clippedSegments = new List<Segment>();

            foreach (var segment in segments)
            {
                int code1 = ComputeRegionCode(segment.X1, segment.Y1, xmin, ymin, xmax, ymax);
                int code2 = ComputeRegionCode(segment.X2, segment.Y2, xmin, ymin, xmax, ymax);

                while ((code1 | code2) != 0)
                {
                    if ((code1 & code2) != 0)
                    {
                        break; 
                    }

                    double x = 0, y = 0;
                    int outcode = code1 != 0 ? code1 : code2;

                    if ((outcode & 8) != 0) 
                    {
                        x = segment.X1 + (segment.X2 - segment.X1) * (ymax - segment.Y1) / (segment.Y2 - segment.Y1);
                        y = ymax;
                    }
                    else if ((outcode & 4) != 0) 
                    {
                        x = segment.X1 + (segment.X2 - segment.X1) * (ymin - segment.Y1) / (segment.Y2 - segment.Y1);
                        y = ymin;
                    }
                    else if ((outcode & 2) != 0) 
                    {
                        y = segment.Y1 + (segment.Y2 - segment.Y1) * (xmax - segment.X1) / (segment.X2 - segment.X1);
                        x = xmax;
                    }
                    else if ((outcode & 1) != 0) 
                    {
                        y = segment.Y1 + (segment.Y2 - segment.Y1) * (xmin - segment.X1) / (segment.X2 - segment.X1);
                        x = xmin;
                    }

                    if (outcode == code1)
                    {
                        segment.X1 = x;
                        segment.Y1 = y;
                        code1 = ComputeRegionCode(segment.X1, segment.Y1, xmin, ymin, xmax, ymax);
                    }
                    else
                    {
                        segment.X2 = x;
                        segment.Y2 = y;
                        code2 = ComputeRegionCode(segment.X2, segment.Y2, xmin, ymin, xmax, ymax);
                    }
                }

                if ((code1 | code2) == 0)
                {
                    clippedSegments.Add(segment);
                }
            }

            return clippedSegments;
        }

        private int ComputeRegionCode(double x, double y, double xmin, double ymin, double xmax, double ymax)
        {
            int code = 0;

            if (x < xmin) code |= 1;  
            if (x > xmax) code |= 2; 
            if (y < ymin) code |= 4;  
            if (y > ymax) code |= 8;  

            return code;
        }

    }
}
