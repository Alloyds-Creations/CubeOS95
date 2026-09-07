using CommunityToolkit.Mvvm.Input;
using CubeOS95.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using WinUI3Localizer;

namespace CubeOS95.ViewModels;

public sealed partial class GameIntroViewModel(INavigationService navigationService) : IDisposable
{
    private readonly INavigationService _navigationService = navigationService;
    private CancellationTokenSource? _introCancellation;
    private Func<CancellationToken, Task>? _runAnimations;

    public void SetAnimationRunner(Func<CancellationToken, Task> runAnimations)
    {
        _runAnimations = runAnimations;
    }

    public async Task OnNavigatedToAsync()
    {
        if (Localizer.Get() is ILocalizer localizer)
        {
            await localizer.SetLanguage(GameSettings.CurrentLanguage);
        }
    }

    [RelayCommand]
    private async Task StartIntroAsync()
    {
        _introCancellation?.Cancel();
        _introCancellation?.Dispose();
        _introCancellation = new CancellationTokenSource();

        if (_runAnimations is not null)
        {
            await _runAnimations(_introCancellation.Token);
        }

        if (!_introCancellation.IsCancellationRequested)
        {
            _navigationService.Navigate(typeof(OSSelect));
        }
    }

    public void Dispose()
    {
        _introCancellation?.Cancel();
        _introCancellation?.Dispose();
    }
}
