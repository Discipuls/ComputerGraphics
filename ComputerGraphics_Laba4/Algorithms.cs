using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Laba4
{
    public static class Algorithms
    {
        public static void StepAlgorithm(Canvas canvas, int x1, int y1, int x2, int y2, TextBlock outputTextBlock)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            double xIncrement = dx / (double)steps;
            double yIncrement = dy / (double)steps;
            double x = x1;
            double y = y1;
            outputTextBlock.Text += "";


            for (int i = 0; i <= steps; i++)
            {
                var point = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(point, x);
                Canvas.SetTop(point, y);
                canvas.Children.Add(point);


                x += xIncrement;
                y += yIncrement;
            }
        }

        public static void CDAAlgorithm(Canvas canvas, int x1, int y1, int x2, int y2, TextBlock outputTextBlock)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            double x = x1;
            double y = y1;
            double xIncrement = dx / (double)steps;
            double yIncrement = dy / (double)steps;

            outputTextBlock.Text += $"dx = {dx}, dy = {dy}, steps = {steps}\n";
            outputTextBlock.Text += "Шаги: (1 из 100)\n";

            for (int i = 0; i <= steps; i++)
            {
                var point = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Green
                };
                Canvas.SetLeft(point, x);
                Canvas.SetTop(point, y);
                canvas.Children.Add(point);
                if (i % 50 == 0)
                {
                    outputTextBlock.Text += $"Точка: ({(int)Math.Round(x)}, {(int)Math.Round(y)})\n";

                }

                x += xIncrement;
                y += yIncrement;
            }
        }

        public static void BresenhamAlgorithm(Canvas canvas, int x1, int y1, int x2, int y2, TextBlock outputTextBlock)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;
            int err = dx - dy;

            outputTextBlock.Text += "";

            while (true)
            {
                var point = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Blue
                };
                Canvas.SetLeft(point, x1);
                Canvas.SetTop(point, y1);
                canvas.Children.Add(point);


                if (x1 == x2 && y1 == y2) break;

                int e2 = err * 2;
                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        public static void BresenhamCircle(Canvas canvas, int xc, int yc, int r, TextBlock outputTextBlock)
        {
            int x = 0;
            int y = r;
            int d = 3 - 2 * r;

            outputTextBlock.Text += "";


            while (x <= y)
            {
                DrawCirclePoints(canvas, xc, yc, x, y, outputTextBlock);
                if (d < 0)
                {
                    d = d + 4 * x + 6;
                }
                else
                {
                    d = d + 4 * (x - y) + 10;
                    y--;
                }
                x++;

            }
        }

        private static void DrawCirclePoints(Canvas canvas, int xc, int yc, int x, int y, TextBlock outputTextBlock)
        {
            DrawPoint(canvas, xc + x, yc + y, outputTextBlock);
            DrawPoint(canvas, xc - x, yc + y, outputTextBlock);
            DrawPoint(canvas, xc + x, yc - y, outputTextBlock);
            DrawPoint(canvas, xc - x, yc - y, outputTextBlock);
            DrawPoint(canvas, xc + y, yc + x, outputTextBlock);
            DrawPoint(canvas, xc - y, yc + x, outputTextBlock);
            DrawPoint(canvas, xc + y, yc - x, outputTextBlock);
            DrawPoint(canvas, xc - y, yc - x, outputTextBlock);
        }

        private static void DrawPoint(Canvas canvas, int x, int y, TextBlock outputTextBlock)
        {
            var point = new Ellipse
            {
                Width = 5,
                Height = 5,
                Fill = Brushes.Purple
            };
            Canvas.SetLeft(point, x);
            Canvas.SetTop(point, y);
            canvas.Children.Add(point);
        }
    }
}
