using CubeOS95.Services;
using CubeOS95.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;

namespace CubeOS95;

public sealed partial class OSSelect : Page
{
    public OSSelectViewModel ViewModel { get; }

    public OSSelect()
    {
        ViewModel = new OSSelectViewModel(new FrameNavigationService(() => Frame));
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        await ViewModel.OnNavigatedToAsync();
    }
}
