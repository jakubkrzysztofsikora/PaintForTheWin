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
        public MainWindow()
        {
            InitializeComponent();
            PaintingMediator paint = new PaintingMediator();
            CanvasBackService canvasService = new CanvasBackService();
            Canvas canvasNode = this.canvasNode;

            canvasNode.MouseDown += paint.OnCanvasMouseDown;
            canvasNode.MouseMove += paint.OnCanvasMouseMove;
            canvasNode.MouseUp += paint.OnCanvasMouseUp;
            canvasService.SetCanvas(canvasNode);
            paint.SetCanvasService(canvasService);
            paint.ChangeToolTo(eTool.Pencil);
            paint.ChangeActiveColor("#000000");
        }
    }
}
