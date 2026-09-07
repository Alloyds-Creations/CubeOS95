using CubeOS95.ViewModels;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Foundation;

namespace CubeOS95.OperatingSystems.CubeOS95Plus;

public sealed partial class CubeOS95Plus : Page
{
    private Point _mouseDownLocation;
    private CanvasBitmap? _backgroundImage;
    private CanvasImageBrush? _backgroundBrush;

    public CubeOS95PlusViewModel ViewModel { get; }

    public CubeOS95Plus()
    {
        ViewModel = new CubeOS95PlusViewModel(new Services.FrameNavigationService(() => Frame));
        InitializeComponent();
    }

    public static Visibility BoolToVisibility(bool value) =>
        value ? Visibility.Visible : Visibility.Collapsed;

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        await ViewModel.OnNavigatedToAsync();
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        ViewModel.OnNavigatedFrom();
        base.OnNavigatedFrom(e);
    }

    private void BackgroundCanvas_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
    {
        args.TrackAsyncAction(Task.Run(async () =>
        {
            string imagePath = Path.GetFullPath(Path.Combine(
                AppContext.BaseDirectory,
                "OperatingSystems",
                "CubeOS95Plus",
                "Resources",
                "Images",
                "grid.png"));

            using var imageStream = File.OpenRead(imagePath);
            using var imageRandomAccessStream = imageStream.AsRandomAccessStream();
            _backgroundImage = await CanvasBitmap.LoadAsync(sender, imageRandomAccessStream);
            _backgroundBrush = new CanvasImageBrush(sender, _backgroundImage)
            {
                ExtendX = CanvasEdgeBehavior.Wrap,
                ExtendY = CanvasEdgeBehavior.Wrap,
                Transform = Matrix3x2.CreateScale(0.4f)
            };
        }).AsAsyncAction());
    }

    private void BackgroundCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
    {
        args.DrawingSession.FillRectangle(new Rect(new Point(), sender.RenderSize), _backgroundBrush);
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadPageAsync(cursor => ProtectedCursor = InputSystemCursor.Create(cursor));
    }

    private void Desktop_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        ViewModel.HideMenus();
    }

    private void Taskbar_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        DependencyObject? source = e.OriginalSource as DependencyObject;
        while (source is not null)
        {
            if (source == BeginButton)
            {
                return;
            }

            source = VisualTreeHelper.GetParent(source);
        }

        ViewModel.HideMenus();
    }

    private void ProgressBar_PointerPressed(object sender, PointerRoutedEventArgs e)
    {
        if (sender is Grid grid && e.GetCurrentPoint(grid).Properties.IsLeftButtonPressed)
        {
            _mouseDownLocation = e.GetCurrentPoint(grid).Position;
        }
    }

    private void ProgressBar_PointerMoved(object sender, PointerRoutedEventArgs e)
    {
        if (sender is Grid grid && e.GetCurrentPoint(grid).Properties.IsLeftButtonPressed)
        {
            Point position = e.GetCurrentPoint(grid).Position;
            double marginLeft = position.X + grid.Margin.Left - _mouseDownLocation.X;
            double marginTop = position.Y + grid.Margin.Top - _mouseDownLocation.Y;
            grid.Margin = new Thickness(marginLeft, marginTop, grid.Margin.Right, grid.Margin.Bottom);
        }
    }
}
