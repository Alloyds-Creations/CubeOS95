using CommunityToolkit.Mvvm.Input;
using CubeOS95.Services;

namespace CubeOS95.ViewModels;

public sealed partial class BIOSSettingsViewModel(INavigationService navigationService)
{
    private readonly INavigationService _navigationService = navigationService;

    [RelayCommand]
    private void ExitWithoutSaving()
    {
        _navigationService.Navigate(typeof(OSSelect));
    }
}
