using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CubeOS95.OperatingSystems.CubeOS95Plus.Resources.Pages;
using CubeOS95.Services;

namespace CubeOS95.ViewModels;

public sealed partial class CubeOS95PlusViewModel(INavigationService navigationService)
    : CubeOsViewModelBase(
        navigationService,
        "/OperatingSystems/CubeOS95Plus/Resources/Sounds/intro_cos95plus.mp3",
        "/OperatingSystems/CubeOS95Plus/Resources/Sounds/outro_cos95plus.mp3")
{
    [ObservableProperty]
    public partial int PowerChoiceIndex { get; set; }

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

    [RelayCommand]
    private void ConfirmPowerChoice()
    {
        if (PowerChoiceIndex == 0)
        {
            Restart();
        }
        else if (PowerChoiceIndex == 1)
        {
            ShutDown();
        }
    }
}
