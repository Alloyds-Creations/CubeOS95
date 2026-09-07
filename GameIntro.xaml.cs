using CubeOS95.Services;
using CubeOS95.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CubeOS95;

public sealed partial class GameIntro : Page
{
    public GameIntroViewModel ViewModel { get; }

    public GameIntro()
    {
        ViewModel = new GameIntroViewModel(new FrameNavigationService(() => Frame));
        InitializeComponent();
        ViewModel.SetAnimationRunner(RunIntroAnimationsAsync);
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        await ViewModel.OnNavigatedToAsync();
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        ViewModel.Dispose();
        base.OnNavigatedFrom(e);
    }

    private async void GameIntro_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.StartIntroCommand.ExecuteAsync(null);
    }

    private async Task RunIntroAnimationsAsync(CancellationToken cancellationToken)
    {
        await BeginStoryboardAsync(MadeByFadeInStoryboard, cancellationToken);
        await DelayAsync(2200, cancellationToken);
        MadeByFadeOutStoryboard.Begin();
        await DelayAsync(1200, cancellationToken);
        DevInFadeInStoryboard.Begin();
        await DelayAsync(2200, cancellationToken);
        DevInFadeOutStoryboard.Begin();
        await DelayAsync(1200, cancellationToken);
        DisclmFadeInStoryboard.Begin();
        await DelayAsync(2200, cancellationToken);
        DisclmFadeOutStoryboard.Begin();
        await DelayAsync(1100, cancellationToken);
    }

    private static async Task BeginStoryboardAsync(Storyboard storyboard, CancellationToken cancellationToken)
    {
        storyboard.Begin();
        await Task.Yield();
        cancellationToken.ThrowIfCancellationRequested();
    }

    private static async Task DelayAsync(int milliseconds, CancellationToken cancellationToken)
    {
        await Task.Delay(milliseconds, cancellationToken);
    }
}
