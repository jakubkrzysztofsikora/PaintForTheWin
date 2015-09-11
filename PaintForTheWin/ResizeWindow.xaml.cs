using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PaintForTheWin
{
    /// <summary>
    /// Interaction logic for ResizeWindow.xaml
    /// </summary>
    public partial class ResizeWindow : Window
    {
        public double WidthInInput { get; set; }
        public double HeightInInput { get; set; }

        public ResizeWindow(Size size)
        {
            InitializeComponent();
            WidthInInput = size.Width;
            HeightInInput = size.Height;
            widthInput.Text = WidthInInput.ToString();
            heightInput.Text = HeightInInput.ToString();
        }

        private void SubmitResize(object sender, RoutedEventArgs e)
        {
            WidthInInput = Double.Parse(widthInput.Text);
            HeightInInput = Double.Parse(heightInput.Text);

            DialogResult = true;
        }
    }
}
