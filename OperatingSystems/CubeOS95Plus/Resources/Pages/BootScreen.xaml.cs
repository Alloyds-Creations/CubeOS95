using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;

namespace CubeOS95.OperatingSystems.CubeOS95Plus.Resources.Pages
{
    public sealed partial class BootScreen : Page
    {
        public BootScreen()
        {
            this.InitializeComponent();
        }

        private async void BootPage_Loaded(object sender, RoutedEventArgs e)
        {
            var storyboard = RootGrid.Resources["FadeStoryboard"] as Storyboard;

            if (storyboard != null)
            {
                storyboard.Begin();
            }

            await Task.Delay(2000);
            this.Frame.Navigate(typeof(CubeOS95Plus), null, new SuppressNavigationTransitionInfo());
        }
    }
}