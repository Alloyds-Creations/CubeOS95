using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System.Threading.Tasks;

namespace CubeOS95.OperatingSystems.CubeOS95Plus.Resources.Pages
{
    public sealed partial class StartingCubeOS95Plus : Page
    {
        public StartingCubeOS95Plus()
        {
            this.InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);
            this.Frame.Navigate(typeof(BootScreen), null, new SuppressNavigationTransitionInfo());
        }
    }
}
