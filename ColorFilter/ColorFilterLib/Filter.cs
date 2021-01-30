using System;
using System.Collections.Generic;
using System.Drawing;

namespace ColorFilterLib
{
    public class Filter
    {
        private Bitmap _currentImage;
        private Bitmap _grayscaledImage;
        private Bitmap _testImage;
        private Color _colorToFilter;
        private List<Pixel> _filteredPixels;
        private Color _marginLow;
        private Color _marginHigh;
        private bool _testMode;
        private readonly string _testImagePath = "test.jpg";

        public Filter(bool testMode)
        {
            _testMode = testMode;
        }

        public void LoadImage(string path)
        {
            _currentImage = (Bitmap)Image.FromFile(path);
            Console.WriteLine($"{path} loaded");
        }

        public void SetColorToFilter(int red, int green, int blue)
        {
            _colorToFilter = Color.FromArgb(red, green, blue);
            Console.WriteLine("color set");
        }

        public void SetPercentageMargin(int red, int green, int blue)
        {
            SetErrorMarginForColor((int)(red / 100f * 255), (int)(green / 100f * 255), (int)(blue / 100f * 255));
        }

        public void SetErrorMarginForColor(int red, int green, int blue)
        {
            var errorMargin = Color.FromArgb(red, green, blue);

            var lowR = _colorToFilter.R - errorMargin.R;
            var highR = _colorToFilter.R + errorMargin.R;

            lowR = lowR > -1 ? lowR : 0;
            highR = highR < 256 ? highR : 255;

            var lowG = _colorToFilter.G - errorMargin.G;
            var highG = _colorToFilter.G + errorMargin.G;

            lowG = lowG > -1 ? lowG : 0;
            highG = highG < 256 ? highG : 255;

            var lowB = _colorToFilter.B - errorMargin.B;
            var highB = _colorToFilter.B + errorMargin.B;

            lowB = lowB > -1 ? lowB : 0;
            highB = highB < 256 ? highB : 255;

            _marginLow = Color.FromArgb(lowR, lowG, lowB);
            _marginHigh = Color.FromArgb(highR, highG, highB);
        }

        public void FilterColors()
        {
            Console.WriteLine("Searching for matching pixels");
            _filteredPixels = new List<Pixel>();
            _grayscaledImage = new Bitmap(_currentImage.Width, _currentImage.Height);

            for (int x = 0; x < _currentImage.Width; x++)
            {
                for (int y = 0; y < _currentImage.Height; y++)
                {
                    var px = _currentImage.GetPixel(x, y);


                    var grayScale = (int)((px.R * 0.3) + (px.G * 0.59) + (px.B * 0.11));
                    var nc = Color.FromArgb(px.A, grayScale, grayScale, grayScale);

                    _grayscaledImage.SetPixel(x, y, nc);

                    if (!IsWithinCurrentMatch(px))
                    {
                        continue;
                    }

                    _filteredPixels.Add(new Pixel()
                    {
                        X = x,
                        Y = y,
                        Color = px
                    });
                }
            }
            Console.WriteLine($"{_filteredPixels.Count} pixels found");

        }

        private bool IsWithinCurrentMatch(Color color)
        {
            return color.R < _marginHigh.R && color.R > _marginLow.R &&
                color.G < _marginHigh.G && color.G > _marginLow.G &&
                color.B < _marginHigh.B && color.B > _marginLow.B;
        }

        public void ApplyFilter()
        {
            Console.WriteLine("setting pixels");
            foreach (var pixel in _filteredPixels)
            {
                _grayscaledImage.SetPixel(pixel.X, pixel.Y, pixel.Color);
            }
        }

        public void SaveResult(string path)
        {
            _grayscaledImage.Save(path);
            Console.WriteLine($"{path} saved");

            if (_testMode)
            {
                Console.WriteLine("Creating test image");
                _testImage = new Bitmap(_currentImage.Width * 2, _currentImage.Height);


                for (int x = 0; x < _currentImage.Width; x++)
                {
                    for (int y = 0; y < _currentImage.Height; y++)
                    {
                        _testImage.SetPixel(x, y, _currentImage.GetPixel(x, y));
                    }
                }

                for (int x = _currentImage.Width; x < _currentImage.Width*2; x++)
                {
                    for (int y = 0; y < _currentImage.Height; y++)
                    {
                        _testImage.SetPixel(x, y, _grayscaledImage.GetPixel(x - _currentImage.Width, y));
                    }
                }

                _testImage.Save(_testImagePath);
                Console.WriteLine($"{_testImagePath} saved");
            }
        }
    }
}
