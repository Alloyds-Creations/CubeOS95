using CubeOS95.Services;
using CubeOS95.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace CubeOS95;

public sealed partial class BIOS_Settings : Page
{
    public BIOSSettingsViewModel ViewModel { get; }

    public BIOS_Settings()
    {
        ViewModel = new BIOSSettingsViewModel(new FrameNavigationService(() => Frame));
        InitializeComponent();
    }
}
