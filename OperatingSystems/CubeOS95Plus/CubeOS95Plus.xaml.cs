using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.IO;
using Windows.Foundation;
using CubeOS95.OperatingSystems.CubeOS95Plus.Resources.Pages;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas;
using Windows.Media.Core;
using Windows.Media.Playback;
using Microsoft.UI.Input;

namespace CubeOS95.OperatingSystems.CubeOS95Plus
{
    public sealed partial class CubeOS95Plus : Page
    {
        private Point MouseDownLocation;
        MediaPlayer introPlay = new MediaPlayer();
        MediaPlayer outroPlay = new MediaPlayer();
        MediaPlayer clickPlay = new MediaPlayer();
        private CanvasBitmap backgroundImage;
        private CanvasImageBrush backgroundBrush;

        public static RadioButton ChoiceRestart { get; set; }
        public static RadioButton ChoiceShutdown { get; set; }
        public CubeOS95Plus()
        {
            this.InitializeComponent();
            ChoiceRestart = RestartOS;
            ChoiceShutdown = ShutdownOS;
        }

        private static Uri ToAppUri(string appRelativePath)
        {
            string relativePath = appRelativePath.TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar);
            string absolutePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath));
            return new Uri(absolutePath);
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
                backgroundImage = await CanvasBitmap.LoadAsync(sender, imageRandomAccessStream);
                backgroundBrush = new CanvasImageBrush(sender, backgroundImage);

                // Set the brush's edge behaviour to wrap, so the image repeats if the drawn region is too big
                backgroundBrush.ExtendX = backgroundBrush.ExtendY = CanvasEdgeBehavior.Wrap;
                backgroundBrush.Transform = System.Numerics.Matrix3x2.CreateScale(0.4f);
            }).AsAsyncAction());
        }
        private void BackgroundCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var session = args.DrawingSession;
            session.FillRectangle(new Rect(new Point(), sender.RenderSize), backgroundBrush);
        }
        private void BeginButton_Checked(object sender, RoutedEventArgs e)
        {
            this.BeginMenu.Visibility = Visibility.Visible;
        }
        private void BeginButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.BeginMenu.Visibility = Visibility.Collapsed;
        }
        private void ShutDown_Click(object sender, RoutedEventArgs e)
        {
            this.BeginButton.IsChecked = false;
            this.BeginMenu.Visibility = Visibility.Collapsed;
            this.ShutDownWindow.Visibility = Visibility.Visible;

            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void CloseShutDownWindow_Click(object sender, RoutedEventArgs e)
        {
            this.ShutDownWindow.Visibility = Visibility.Collapsed;
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            this.ShutDownWindow.Visibility = Visibility.Collapsed;
            this.Frame.Navigate(typeof(PleaseWaitRestart), null, new SuppressNavigationTransitionInfo());

            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();

            introPlay.Dispose();
            outroPlay.Source = MediaSource.CreateFromUri(ToAppUri("/OperatingSystems/CubeOS95Plus/Resources/Sounds/outro_s95plus.mp3"));
            outroPlay.Volume = 0.1;
            outroPlay.Play();
        }
        private void ShutDownButton_Click(object sender, RoutedEventArgs e)
        {
            this.ShutDownWindow.Visibility = Visibility.Collapsed;
            this.Frame.Navigate(typeof(PleaseWaitShutDown), null, new SuppressNavigationTransitionInfo());

            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();

            introPlay.Dispose();
            outroPlay.Source = MediaSource.CreateFromUri(ToAppUri("/OperatingSystems/CubeOS95Plus/Resources/Sounds/outro_s95plus.mp3"));
            outroPlay.Volume = 0.1;
            outroPlay.Play();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            introPlay.Source = MediaSource.CreateFromUri(ToAppUri("/OperatingSystems/CubeOS95Plus/Resources/Sounds/intro_s95plus.mp3"));
            introPlay.Volume = 0.1;
            introPlay.Play();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            OKButton.Click += OKButton_Click;
        }
        private void BeginButton_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void Mailbox_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void Achievements_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void SystemBrowser_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void MyMenu_Click(object sender, RoutedEventArgs e)
        {
            this.BeginButton.IsChecked = false;
            this.BeginMenu.Visibility = Visibility.Collapsed;
            this.MyMenuWindow.Visibility = Visibility.Visible;
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void LoadSave_Click(object sender, RoutedEventArgs e)
        {
            this.BeginButton.IsChecked = false;
            this.BeginMenu.Visibility = Visibility.Collapsed;
            this.GameModesAndChallengesWindow.Visibility = Visibility.Visible;
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.BeginButton.IsChecked = false;
            this.BeginMenu.Visibility = Visibility.Collapsed;
            this.SettingsWindow.Visibility = Visibility.Visible;
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void SystemDOS_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void SoundToggle_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void GameLanguage_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void MediaPlayer_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void Defragmentation_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void Firewall_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void Bin_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void CloseSettingsWindow_Click(object sender, RoutedEventArgs e)
        {
            this.SettingsWindow.Visibility = Visibility.Collapsed;
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void CloseMyMenuWindow_Click(object sender, RoutedEventArgs e)
        {
            this.MyMenuWindow.Visibility = Visibility.Collapsed;
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void ProgressBar_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid sp)
            {
                if (e.GetCurrentPoint(sp).Properties.IsLeftButtonPressed)
                {
                    MouseDownLocation.Y = e.GetCurrentPoint(sp).Position.Y;
                    MouseDownLocation.X = e.GetCurrentPoint(sp).Position.X;
                }
            }
        }
        private void ProgressBar_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid sp)
            {
                if (e.GetCurrentPoint(sp).Properties.IsLeftButtonPressed)
                {
                    var MarginLeft = e.GetCurrentPoint(sp).Position.X + sp.Margin.Left - MouseDownLocation.X;
                    var MarginTop = e.GetCurrentPoint(sp).Position.Y + sp.Margin.Top - MouseDownLocation.Y;
                    sp.Margin = new Thickness(MarginLeft, MarginTop, sp.Margin.Right, sp.Margin.Bottom);
                }
            }
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Wait);
            await Task.Delay(800);
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Arrow);
            await Task.Delay(400);
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Wait);
            await Task.Delay(2300);
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Arrow);
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
            if (this.PowerChoices.SelectedIndex == 0)
            {
                introPlay.Dispose();
                this.Frame.Navigate(typeof(PleaseWaitRestart), null, new SuppressNavigationTransitionInfo());
                outroPlay.Source = MediaSource.CreateFromUri(ToAppUri("/OperatingSystems/CubeOS95Plus/Resources/Sounds/outro_s95plus.mp3"));
                outroPlay.Volume = 0.1;
                outroPlay.Play();
            }
            if (this.PowerChoices.SelectedIndex == 1)
            {
                introPlay.Dispose();
                this.Frame.Navigate(typeof(PleaseWaitShutDown), null, new SuppressNavigationTransitionInfo());
                outroPlay.Source = MediaSource.CreateFromUri(ToAppUri("/OperatingSystems/CubeOS95Plus/Resources/Sounds/outro_s95plus.mp3"));
                outroPlay.Volume = 0.1;
                outroPlay.Play();
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
            this.ShutDownWindow.Visibility = Visibility.Collapsed;
        }
        private void ShutdownOS_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void RestartOS_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void CloseGameModesAndChallengesWindow_Click(object sender, RoutedEventArgs e)
        {
            this.GameModesAndChallengesWindow.Visibility = Visibility.Collapsed;
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
        private void DifficutlyLevelRelax_Click(object sender, RoutedEventArgs e)
        {
            clickPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/click.mp3"));
            clickPlay.Volume = 0.1;
            clickPlay.Play();
        }
    }
}
