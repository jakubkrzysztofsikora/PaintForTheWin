using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
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
        private Button _currentlyClickedToolButton;

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
            SetButtonAsActive(pencil);
        }

        private void MenuItemUndo_OnClick(object sender, RoutedEventArgs e)
        {
            _paint.Undo();
            if (_paint.GetNumberOfDoneChanges() == 0)
                this.UndoButton.IsEnabled = false;
        }

        private void MenuItemResize_OnClick(object sender, RoutedEventArgs e)
        {
            ResizeWindow resizeWindow = new ResizeWindow(_paint.GetCanvasSize());

            Nullable<bool> result = resizeWindow.ShowDialog();

            if (result == true)
                _paint.ChangeCanvasSize(resizeWindow.WidthInInput, resizeWindow.HeightInInput);
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
            _paint.ChangeThickness(1);
            Button clickedButton = sender as Button;
            SetButtonAsActive(clickedButton);
        }

        private void ChangeToolToRubber(object sender, RoutedEventArgs e)
        {
            _paint.ChangeToolTo(eTool.Rubber);
            _paint.ChangeThickness(15);
            Button clickedButton = sender as Button;
            SetButtonAsActive(clickedButton);
        }

        private void MenuEdit_OnClick(object sender, RoutedEventArgs e)
        {
            if (_paint.GetNumberOfDoneChanges() > 0)
                this.UndoButton.IsEnabled = true;
            else
                this.UndoButton.IsEnabled = false;
        }

        private void ChangeToolToRect(object sender, RoutedEventArgs e)
        {
            _paint.ChangeToolTo(eTool.Rectangle);
            Button clickedButton = sender as Button;
            SetButtonAsActive(clickedButton);
        }

        private void ChangeToolToEllipse(object sender, RoutedEventArgs e)
        {
            _paint.ChangeToolTo(eTool.Ellipse);
            Button clickedButton = sender as Button;
            SetButtonAsActive(clickedButton);
        }

        private void ChangeToolToLine(object sender, RoutedEventArgs e)
        {
            _paint.ChangeToolTo(eTool.Line);
            Button clickedButton = sender as Button;
            SetButtonAsActive(clickedButton);
        }

        private void ChangeActiveColor(object sender, MouseButtonEventArgs e)
        {
            ColorPicker colorPickerWindow = new ColorPicker(_paint.GetActiveColor());
            Nullable<bool> result = colorPickerWindow.ShowDialog();

            if (result == true)
                _paint.ChangeActiveColor(colorPickerWindow.SelectedPaintingColor.ToString());
                PaintingColor activeColor = _paint.GetActiveColor();

            ColorButton.Fill = new SolidColorBrush(activeColor.GetNativeColorObject());
        }

        private void ChangeToolToFill(object sender, RoutedEventArgs e)
        {
            _paint.ChangeToolTo(eTool.Fill);
            Button clickedButton = sender as Button;
            SetButtonAsActive(clickedButton);
        }

        private void MenuItemNew_OnClick(object sender, ExecutedRoutedEventArgs e)
        {
            _paint.New();
        }

        private void SetButtonAsActive(Button button)
        {
            if (_currentlyClickedToolButton != null)
                _currentlyClickedToolButton.Effect = null;

            _currentlyClickedToolButton = button;
            _currentlyClickedToolButton.Effect = new DropShadowEffect();
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }
    }
}
