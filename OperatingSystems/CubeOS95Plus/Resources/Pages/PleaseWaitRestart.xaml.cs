using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Threading.Tasks;

namespace CubeOS95.OperatingSystems.CubeOS95Plus.Resources.Pages
{
    public sealed partial class PleaseWaitRestart : Page
    {
        public PleaseWaitRestart()
        {
            this.InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Wait);
            await Task.Delay(1500);
            this.ProtectedCursor = InputSystemCursor.Create(InputSystemCursorShape.Arrow);
            await Task.Delay(3000);
            this.Frame.Navigate(typeof(OSSelect), null, new SuppressNavigationTransitionInfo());
        }
    }
}
