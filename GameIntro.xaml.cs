using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Threading.Tasks;
using WinUI3Localizer;

namespace CubeOS95
{
    public sealed partial class GameIntro : Page
    {
        public GameIntro()
        {
            InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (Localizer.Get() is ILocalizer localizer)
            {
                await localizer.SetLanguage(GameSettings.CurrentLanguage);
            }
        }
        private async void GameIntro_Loaded(object sender, RoutedEventArgs e)
        {
            MadeByFadeInStoryboard.Begin();

            await Task.Delay(2200);

            MadeByFadeOutStoryboard.Begin();

            await Task.Delay(1200);

            DevInFadeInStoryboard.Begin();

            await Task.Delay(2200);

            DevInFadeOutStoryboard.Begin();

            await Task.Delay(1200);

            DisclmFadeInStoryboard.Begin();

            await Task.Delay(2200);

            DisclmFadeOutStoryboard.Begin();

            await Task.Delay(1100);

            this.Frame.Navigate(typeof(OSSelect), null, new SuppressNavigationTransitionInfo());
        }
    }
}
