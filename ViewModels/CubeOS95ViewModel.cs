using CommunityToolkit.Mvvm.Input;
using CubeOS95.OperatingSystems.CubeOS95.Resources.Pages;
using CubeOS95.Services;

namespace CubeOS95.ViewModels;

public sealed partial class CubeOS95ViewModel(INavigationService navigationService)
    : CubeOsViewModelBase(
        navigationService,
        "/OperatingSystems/CubeOS95/Resources/Sounds/intro_cos95.mp3",
        "/OperatingSystems/CubeOS95/Resources/Sounds/outro_cos95.mp3")
{
    [RelayCommand]
    public override void Restart()
    {
        IsShutdownWindowVisible = false;
        NavigateTo(typeof(PleaseWaitRestart));
        Audio.PlayClick();
        PlayOutro();
    }

    [RelayCommand]
    public override void ShutDown()
    {
        IsShutdownWindowVisible = false;
        NavigateTo(typeof(PleaseWaitShutDown));
        Audio.PlayClick();
        PlayOutro();
    }
}
