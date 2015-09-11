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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PaintForTheWin.CanvasComponents;
using PaintForTheWin.Ecosystem;

namespace PaintForTheWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PaintingMediator _paint;

        public MainWindow()
        {
            InitializeComponent();
            _paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = this.canvasNode;

            canvasNode.MouseDown += _paint.OnCanvasMouseDown;
            canvasNode.MouseMove += _paint.OnCanvasMouseMove;
            canvasNode.MouseUp += _paint.OnCanvasMouseUp;
            canvasService.SetCanvas(canvasNode);
            _paint.SetCanvasService(canvasService);
            _paint.ChangeToolTo(eTool.Pencil);
            _paint.ChangeActiveColor("#000000");
        }

        private void MenuItemUndo_OnClick(object sender, RoutedEventArgs e)
        {
            _paint.Undo();
        }
    }
}
