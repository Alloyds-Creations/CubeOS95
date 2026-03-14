using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace CubeOS95.OperatingSystems.CubeOS95Plus.Resources.Pages
{
    public sealed partial class SafeToTurnOff : Page
    {
        MediaPlayer SafeToTurnOffPlay = new MediaPlayer();
        public SafeToTurnOff()
        {
            this.InitializeComponent();
        }

        private static Uri ToAppUri(string appRelativePath)
        {
            string relativePath = appRelativePath.TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar);
            string absolutePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath));
            return new Uri(absolutePath);
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(3000);
            this.TurnOn.Visibility = Visibility.Visible;
        }
        private void TurnOn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OSSelect), null, new SuppressNavigationTransitionInfo());
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SafeToTurnOffPlay.Source = MediaSource.CreateFromUri(ToAppUri("/Assets/Sounds/fanshutdown.mp3"));
            SafeToTurnOffPlay.Play();
        }
    }
}
