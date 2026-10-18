using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Windows.Graphics;
using Windows.UI;
using Windows.UI.ViewManagement;
using WinUIEx;
using WinUIEx.Messaging;

namespace CubeOS95
{
    public sealed partial class MainWindow : WindowEx
    {
        public MainWindow(int MinWidth, int MinHeight, int MaxWidth, int MaxHeight)
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            AppWindow.SetIcon("Assets/icon.ico");
            AppWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
            AppWindow.Resize(new SizeInt32(1380, 807));

            OverlappedPresenter presenter = OverlappedPresenter.Create();

            AppWindow.SetPresenter(presenter);

            presenter.PreferredMinimumWidth = MinWidth;
            presenter.PreferredMinimumHeight = MinHeight;
            presenter.PreferredMaximumWidth = MaxWidth;
            presenter.PreferredMaximumHeight = MaxHeight;

            GameFrame.Navigate(typeof(GameIntro), null, new SuppressNavigationTransitionInfo());
        }
    }
}
