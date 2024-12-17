using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Laba5
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public partial class PolygonClipWindow : Window
    {
        public PolygonClipWindow()
        {
            InitializeComponent();
        }

        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "data2.txt";
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                int n = int.Parse(lines[0]); 
                List<LineSegment> lineSegments = new List<LineSegment>();
                for (int i = 1; i <= n; i++)
                {
                    string[] parts = lines[i].Split(' ');
                    double x1 = double.Parse(parts[0]);
                    double y1 = double.Parse(parts[1]);
                    double x2 = double.Parse(parts[2]);
                    double y2 = double.Parse(parts[3]);
                    lineSegments.Add(new LineSegment(new Point(x1, y1), new Point(x2, y2)));
                }

                int m = int.Parse(lines[n + 1]); 
                List<Point> clippingPolygonPoints = new List<Point>();
                for (int i = n + 2; i < n + 2 + m; i++)
                {
                    string[] parts = lines[i].Split(' ');
                    double x = double.Parse(parts[0]);
                    double y = double.Parse(parts[1]);
                    clippingPolygonPoints.Add(new Point(x, y));
                }

                List<LineSegment> clippedSegments = new List<LineSegment>();
                foreach (var segment in lineSegments)
                {
                    var clippedSegment = ClipSegmentWithPolygon(segment, clippingPolygonPoints);
                    if (clippedSegment != null)
                    {
                        clippedSegments.Add(clippedSegment);
                    }
                }
                DrawCoordinateSystem(Canvas.ActualWidth, Canvas.ActualHeight, 100);


                DrawPolygon(clippingPolygonPoints, Brushes.LightBlue);
                foreach (var segment in lineSegments)
                {
                    DrawLine(segment.Start, segment.End, Brushes.Red);
                }

                foreach (var segment in clippedSegments)
                {
                    DrawLine(segment.Start, segment.End, Brushes.Green);
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

        private void DrawLine(Point p1, Point p2, Brush color)
        {
            Line line = new Line()
            {
                X1 = p1.X,
                Y1 = p1.Y,
                X2 = p2.X,
                Y2 = p2.Y,
                Stroke = color,
                StrokeThickness = 2
            };
            Canvas.Children.Add(line);
        }

        private void DrawPolygon(List<Point> points, Brush fillColor)
        {
            Polygon polygon = new Polygon()
            {
                Points = new PointCollection(points),
                Stroke = Brushes.Black,
                Fill = fillColor,
                StrokeThickness = 2
            };
            Canvas.Children.Add(polygon);
        }

        private LineSegment ClipSegmentWithPolygon(LineSegment segment, List<Point> polygon)
        {
            List<Point> clippedPoints = new List<Point>();
            LineSegment clippedSegment = segment;

            for (int i = 0; i < polygon.Count; i++)
            {
                Point p1 = polygon[i];
                Point p2 = polygon[(i + 1) % polygon.Count];

                clippedSegment = ClipSegment(clippedSegment, p1, p2);

                if (clippedSegment == null)
                {
                    return null;
                }
            }

            return clippedSegment;
        }

        private double CrossProduct(Point p1, Point p2, Point p3)
        {
            return (p2.X - p1.X) * (p3.Y - p1.Y) - (p2.Y - p1.Y) * (p3.X - p1.X);
        }

        public LineSegment ClipSegment(LineSegment segment, Point p1, Point p2)
        {
            double cross1 = CrossProduct(p1, p2, segment.Start);
            double cross2 = CrossProduct(p1, p2, segment.End);

            if (cross1 >= 0 && cross2 >= 0)
            {
                return segment;
            }
            if (cross1 <= 0 && cross2 <= 0)
            {
                return segment;
            }
            Point intersection = GetIntersection(segment.Start, segment.End, p1, p2);

            if (cross1 < 0)
            {
                return new LineSegment(intersection, segment.End);
            }
            else
            {
                return new LineSegment(segment.Start, intersection);
            }
        }

        private Point GetIntersection(Point s1, Point e1, Point s2, Point e2)
        {
            double x1 = s1.X, y1 = s1.Y, x2 = e1.X, y2 = e1.Y;
            double x3 = s2.X, y3 = s2.Y, x4 = e2.X, y4 = e2.Y;

            double denom = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            double intersectX = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denom;
            double intersectY = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denom;

            return new Point(intersectX, intersectY);
        }
    }

}
