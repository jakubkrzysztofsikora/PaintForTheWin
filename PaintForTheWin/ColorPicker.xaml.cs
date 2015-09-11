using System.Windows;
using PaintForTheWin.Ecosystem;

namespace PaintForTheWin
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        public PaintingColor SelectedPaintingColor { get; set; }
        public ColorPicker(PaintingColor color)
        {
            InitializeComponent();
            SelectedPaintingColor = color;
            _colorPicker.SelectedColor = SelectedPaintingColor.GetNativeColorObject();
        }

        private void SaveColor(object sender, RoutedEventArgs e)
        {
            SelectedPaintingColor = PaintingColor.CreateFromHex(_colorPicker.SelectedColorText);
            DialogResult = true;
        }
    }
}
