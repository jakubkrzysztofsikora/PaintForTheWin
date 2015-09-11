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
using Microsoft.Win32;
using PaintForTheWin.CanvasComponents;
using PaintForTheWin.Ecosystem;

namespace PaintForTheWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PaintingMediator _paint;

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
            if (_paint.GetNumberOfDoneChanges() == 0)
                this.UndoButton.IsEnabled = false;
        }

        private void MenuItemResize_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MenuItemRotateRight_OnClick(object sender, RoutedEventArgs e)
        {
            _paint.Rotate(90);
        }

        private void MenuItemRotateLeft_OnClick(object sender, RoutedEventArgs e)
        {
            _paint.Rotate(-90);
        }

        private void MenuItemFullRotate_OnClick(object sender, RoutedEventArgs e)
        {
            _paint.Rotate(180);
        }

        private void MenuItemReverseHorizontally_OnClick(object sender, RoutedEventArgs e)
        {
            _paint.Reverse(eDirection.Horizontal);
        }

        private void MenuItemReverseVertically_OnClick(object sender, RoutedEventArgs e)
        {
            _paint.Reverse(eDirection.Vertical);
        }

        private void MenuItemOpen_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Choose File to Open";
            dialog.Filter = "Image documents (.bmp)|*.bmp";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
                _paint.LoadImage(dialog.FileName);
        }

        private void MenuItemSave_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Choose Location to Save";
            dialog.FileName = "Sample Image";
            dialog.DefaultExt = ".bmp";
            dialog.Filter = "Image documents (.bmp)|*.bmp";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
                _paint.Save(dialog.FileName);
        }

        private void MenuItemExit_OnClick(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void ChangeToolToPencil(object sender, RoutedEventArgs e)
        {
            _paint.ChangeToolTo(eTool.Pencil);
        }

        private void ChangeToolToRubber(object sender, RoutedEventArgs e)
        {
            _paint.ChangeToolTo(eTool.Rubber);
        }

        private void MenuEdit_OnClick(object sender, RoutedEventArgs e)
        {
            if (_paint.GetNumberOfDoneChanges() > 0)
                this.UndoButton.IsEnabled = true;
            else
                this.UndoButton.IsEnabled = false;
        }
    }
}
