using ColorFilterLib;
using ColorFilterWindows.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ColorFilterWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Filter _filter;
        public MainWindow()
        {
            InitializeComponent();

            _filter = new Filter();
            _filter.OnSetSourcePicture += OnSetSourcePicture;    
        }

        private void OnSetSourcePicture(Bitmap srcPicture)
        {
            SourceImage.Source = srcPicture.ToBitmapSource();
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var filePicker = new OpenFileDialog();

            var result = filePicker.ShowDialog();

            if (result.Value)
            {
                _filter.LoadImage(filePicker.FileName);
            }
        }
    }
}
