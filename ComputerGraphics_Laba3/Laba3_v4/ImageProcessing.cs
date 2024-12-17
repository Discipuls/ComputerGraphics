using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba3_v4
{
        public static class ImageProcessing
        {
            public static Bitmap ApplyLinearContrast(Bitmap inputImage)
            {
                Bitmap outputImage = new Bitmap(inputImage);
                int width = inputImage.Width;
                int height = inputImage.Height;

                int min = 255, max = 0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixelColor = inputImage.GetPixel(x, y);
                        int brightness = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                        min = Math.Min(min, brightness);
                        max = Math.Max(max, brightness);
                    }
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixelColor = inputImage.GetPixel(x, y);
                        int brightness = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                        int newBrightness = (int)(((brightness - min) / (float)(max - min)) * 255);
                        newBrightness = Math.Clamp(newBrightness, 0, 255);

                        outputImage.SetPixel(x, y, Color.FromArgb(newBrightness, newBrightness, newBrightness));
                    }
                }

                return outputImage;
            }

            public static Bitmap EqualizeHistogram(Bitmap inputImage)
            {
                Bitmap outputImage = new Bitmap(inputImage);
                int width = inputImage.Width;
                int height = inputImage.Height;
                int[] histogram = new int[256];
                int[] cumulativeHistogram = new int[256];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixelColor = inputImage.GetPixel(x, y);
                        int brightness = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                        histogram[brightness]++;
                    }
                }

                cumulativeHistogram[0] = histogram[0];
                for (int i = 1; i < 256; i++)
                {
                    cumulativeHistogram[i] = cumulativeHistogram[i - 1] + histogram[i];
                }

                int totalPixels = width * height;
                for (int i = 0; i < 256; i++)
                {
                    cumulativeHistogram[i] = (int)((float)cumulativeHistogram[i] / totalPixels * 255);
                }

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color pixelColor = inputImage.GetPixel(x, y);
                        int brightness = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                        int newBrightness = cumulativeHistogram[brightness];
                        outputImage.SetPixel(x, y, Color.FromArgb(newBrightness, newBrightness, newBrightness));
                    }
                }

                return outputImage;
            }

            public static Bitmap ApplySobelEdgeDetection(Bitmap inputImage)
            {
                Bitmap outputImage = new Bitmap(inputImage);
                int width = inputImage.Width;
                int height = inputImage.Height;

                int[,] Gx = {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
            };
                int[,] Gy = {
                { -1, -2, -1 },
                {  0,  0,  0 },
                {  1,  2,  1 }
            };

                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        int gradientX = 0;
                        int gradientY = 0;

                        for (int ky = -1; ky <= 1; ky++)
                        {
                            for (int kx = -1; kx <= 1; kx++)
                            {
                                Color pixelColor = inputImage.GetPixel(x + kx, y + ky);
                                int gray = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);

                                gradientX += gray * Gx[ky + 1, kx + 1];
                                gradientY += gray * Gy[ky + 1, kx + 1];
                            }
                        }

                        int magnitude = (int)Math.Min(Math.Sqrt(gradientX * gradientX + gradientY * gradientY), 255);
                        outputImage.SetPixel(x, y, Color.FromArgb(magnitude, magnitude, magnitude));
                    }
                }

                return outputImage;
            }
        }


    }
