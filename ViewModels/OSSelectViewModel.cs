using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CubeOS95.OperatingSystems.CubeOS95.Resources.Pages;
using CubeOS95.OperatingSystems.CubeOS95Plus.Resources.Pages;
using CubeOS95.Services;
using System.Threading.Tasks;
using WinUI3Localizer;

namespace CubeOS95.ViewModels;

public sealed partial class OSSelectViewModel(INavigationService navigationService) : ObservableObject
{
    private readonly INavigationService _navigationService = navigationService;

    public async Task OnNavigatedToAsync()
    {
        if (Localizer.Get() is ILocalizer localizer)
        {
            await localizer.SetLanguage(GameSettings.CurrentLanguage);
        }
    }

    [RelayCommand]
    private void StartCubeOS95()
    {
        _navigationService.Navigate(typeof(StartingCubeOS95));
    }

    [RelayCommand]
    private void StartCubeOS95Plus()
    {
        _navigationService.Navigate(typeof(StartingCubeOS95Plus));
    }

    [RelayCommand]
    private void OpenGameSettings()
    {
        _navigationService.Navigate(typeof(BIOS_Settings));
    }

    [RelayCommand]
    private void ViewIntro()
    {
        _navigationService.Navigate(typeof(GameIntro));
    }
}
