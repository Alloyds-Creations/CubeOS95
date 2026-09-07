using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CubeOS95.Services;
using Microsoft.UI.Input;
using System;
using System.Threading.Tasks;
using WinUI3Localizer;

namespace CubeOS95.ViewModels;

public abstract partial class CubeOsViewModelBase : ObservableObject, IDisposable
{
    private readonly INavigationService _navigationService;
    protected readonly GameAudioService Audio;

    [ObservableProperty]
    public partial bool IsBeginMenuOpen { get; set; }

    [ObservableProperty]
    public partial bool IsShutdownWindowVisible { get; set; }

    [ObservableProperty]
    public partial bool IsSettingsWindowVisible { get; set; }

    [ObservableProperty]
    public partial bool IsMyMenuWindowVisible { get; set; }

    [ObservableProperty]
    public partial bool IsGameModesWindowVisible { get; set; }

    [ObservableProperty]
    public partial bool IsEnglishSelected { get; set; }

    [ObservableProperty]
    public partial bool IsRussianSelected { get; set; }

    protected CubeOsViewModelBase(INavigationService navigationService, string introSoundPath, string outroSoundPath)
    {
        _navigationService = navigationService;
        IntroSoundPath = introSoundPath;
        OutroSoundPath = outroSoundPath;
        Audio = new GameAudioService();
        IsEnglishSelected = GameSettings.CurrentLanguage != "ru-RU";
        IsRussianSelected = !IsEnglishSelected;
    }

    protected string IntroSoundPath { get; }
    protected string OutroSoundPath { get; }

    public async Task OnNavigatedToAsync()
    {
        if (Localizer.Get() is ILocalizer localizer)
        {
            await localizer.SetLanguage(GameSettings.CurrentLanguage);
        }

        MainWindow.UpdateTitle();
        Audio.PlayIntro(IntroSoundPath);
    }

    public void OnNavigatedFrom()
    {
        Audio.StopIntro();
    }

    public Task LoadPageAsync(Action<InputSystemCursorShape> setCursor)
    {
        return LoadPageCoreAsync(setCursor);
    }

    public void HideMenus()
    {
        IsBeginMenuOpen = false;
    }

    [RelayCommand]
    private void PlayClick()
    {
        Audio.PlayClick();
    }

    [RelayCommand]
    private void OpenShutdownWindow()
    {
        IsBeginMenuOpen = false;
        IsShutdownWindowVisible = true;
        Audio.PlayClick();
    }

    [RelayCommand]
    private void CloseShutdownWindow()
    {
        IsShutdownWindowVisible = false;
        Audio.PlayClick();
    }

    [RelayCommand]
    private void OpenMyMenuWindow()
    {
        IsBeginMenuOpen = false;
        IsMyMenuWindowVisible = true;
        Audio.PlayClick();
    }

    [RelayCommand]
    private void CloseMyMenuWindow()
    {
        IsMyMenuWindowVisible = false;
        Audio.PlayClick();
    }

    [RelayCommand]
    private void OpenGameModesWindow()
    {
        IsBeginMenuOpen = false;
        IsGameModesWindowVisible = true;
        Audio.PlayClick();
    }

    [RelayCommand]
    private void CloseGameModesWindow()
    {
        IsGameModesWindowVisible = false;
        Audio.PlayClick();
    }

    [RelayCommand]
    private void OpenSettingsWindow()
    {
        IsBeginMenuOpen = false;
        IsSettingsWindowVisible = true;
        Audio.PlayClick();
    }

    [RelayCommand]
    private void CloseSettingsWindow()
    {
        IsSettingsWindowVisible = false;
        Audio.PlayClick();
    }

    [RelayCommand]
    private async Task SelectEnglishAsync()
    {
        await SetLanguageAsync("en-US");
    }

    [RelayCommand]
    private async Task SelectRussianAsync()
    {
        await SetLanguageAsync("ru-RU");
    }

    protected void NavigateTo(Type pageType)
    {
        _navigationService.Navigate(pageType);
    }

    protected void PlayOutro()
    {
        Audio.PlayOutro(OutroSoundPath);
    }

    private async Task SetLanguageAsync(string language)
    {
        if (Localizer.Get() is ILocalizer localizer)
        {
            await localizer.SetLanguage(language);
        }

        GameSettings.Save(new GameSettingsData { Language = language });
        IsEnglishSelected = language == "en-US";
        IsRussianSelected = language == "ru-RU";
        MainWindow.UpdateTitle();
        Audio.PlayClick();
    }

    private static async Task LoadPageCoreAsync(Action<InputSystemCursorShape> setCursor)
    {
        setCursor(InputSystemCursorShape.Wait);
        await Task.Delay(800);
        setCursor(InputSystemCursorShape.Arrow);
        await Task.Delay(400);
        setCursor(InputSystemCursorShape.Wait);
        await Task.Delay(2300);
        setCursor(InputSystemCursorShape.Arrow);
    }

    public abstract void Restart();
    public abstract void ShutDown();

    public void Dispose()
    {
        Audio.Dispose();
    }
}
