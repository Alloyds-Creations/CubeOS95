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
using WinUI3Localizer;

namespace CubeOS95
{
    public sealed partial class MainWindow : WindowEx
    {
        private readonly UISettings systemUiSettings = new UISettings();

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate int SetPreferredAppModeDelegate(int preferredAppMode);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        private delegate bool FlushMenuThemesDelegate();

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr module, string procedureName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr module, IntPtr ordinal);

        public MainWindow(int MinWidth, int MinHeight, int MaxWidth, int MaxHeight)
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            AppWindow.SetIcon("Assets/icon.ico");
            AppWindow.TitleBar.IconShowOptions = IconShowOptions.ShowIconAndSystemMenu;
            systemUiSettings.ColorValuesChanged += SystemUiSettings_ColorValuesChanged;
            ApplyMenuTheme();
            ApplyTitleBarTheme();
            AppWindow.Resize(new SizeInt32(1380, 807));

            OverlappedPresenter presenter = OverlappedPresenter.Create();

            AppWindow.SetPresenter(presenter);

            presenter.PreferredMinimumWidth = MinWidth;
            presenter.PreferredMinimumHeight = MinHeight;
            presenter.PreferredMaximumWidth = MaxWidth;
            presenter.PreferredMaximumHeight = MaxHeight;

            GameFrame.Navigate(typeof(GameIntro), null, new SuppressNavigationTransitionInfo());

            CenterWindow();
        }
        private void SystemUiSettings_ColorValuesChanged(UISettings sender, object args)
        {
            DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal, ApplySystemTheme);
        }

        private void ApplySystemTheme()
        {
            ApplyMenuTheme();
            ApplyTitleBarTheme();
        }

        private void ApplyTitleBarTheme()
        {
            bool isDark = IsSystemUsingDarkTheme();
            Color background = isDark
                ? Color.FromArgb(255, 0, 0, 0)
                : Color.FromArgb(255, 255, 255, 255);
            Color foreground = isDark
                ? Color.FromArgb(255, 255, 255, 255)
                : Color.FromArgb(255, 0, 0, 0);
            Color hoverBackground = isDark
                ? Color.FromArgb(255, 48, 48, 48)
                : Color.FromArgb(255, 230, 230, 230);
            Color pressedBackground = isDark
                ? Color.FromArgb(255, 80, 80, 80)
                : Color.FromArgb(255, 210, 210, 210);
            Color inactiveForeground = isDark
                ? Color.FromArgb(255, 128, 128, 128)
                : Color.FromArgb(255, 102, 102, 102);

            AppWindowTitleBar titleBar = AppWindow.TitleBar;
            titleBar.BackgroundColor = background;
            titleBar.ForegroundColor = foreground;
            titleBar.ButtonBackgroundColor = background;
            titleBar.ButtonForegroundColor = foreground;
            titleBar.ButtonHoverBackgroundColor = hoverBackground;
            titleBar.ButtonHoverForegroundColor = foreground;
            titleBar.ButtonPressedBackgroundColor = pressedBackground;
            titleBar.ButtonPressedForegroundColor = foreground;
            titleBar.InactiveBackgroundColor = background;
            titleBar.InactiveForegroundColor = inactiveForeground;
            titleBar.ButtonInactiveBackgroundColor = background;
            titleBar.ButtonInactiveForegroundColor = inactiveForeground;
        }

        private void ApplyMenuTheme()
        {
            IntPtr module = LoadLibrary("uxtheme.dll");
            if (module == IntPtr.Zero)
            {
                return;
            }

            // UxTheme exports these APIs by ordinal on supported Windows versions.
            IntPtr setPreferredAppMode = GetProcAddress(module, (IntPtr)135);
            if (setPreferredAppMode != IntPtr.Zero)
            {
                int preferredAppMode = IsSystemUsingDarkTheme() ? 2 : 3;
                Marshal.GetDelegateForFunctionPointer<SetPreferredAppModeDelegate>(setPreferredAppMode)(preferredAppMode);
            }

            IntPtr flushMenuThemes = GetProcAddress(module, (IntPtr)136);
            if (flushMenuThemes != IntPtr.Zero)
            {
                Marshal.GetDelegateForFunctionPointer<FlushMenuThemesDelegate>(flushMenuThemes)();
            }
        }

        private bool IsSystemUsingDarkTheme()
        {
            Windows.UI.Color background = systemUiSettings.GetColorValue(UIColorType.Background);
            return (background.R + background.G + background.B) < 384;
        }
        private void CenterWindow()
        {
            var area = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Nearest)?.WorkArea;
            if (area == null) return;
            AppWindow.Move(new PointInt32((area.Value.Width - AppWindow.Size.Width) / 2, (area.Value.Height - AppWindow.Size.Height) / 2));
        }
        public static void UpdateTitle()
        {
            if (App.m_window is MainWindow mainWindow)
            {
                string version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString(2) ?? "0.0";
                string title = mainWindow.AppTitleBar.Title;
                if (!string.IsNullOrEmpty(title))
                {
                    title = System.Text.RegularExpressions.Regex.Replace(title, @"\d+\.\d+", version);
                }
                else
                {
                    title = $"CubeOS 95 - Version {version} Alpha";
                }
                mainWindow.Title = title;
                mainWindow.AppTitleBar.Title = title;
            }
        }
    }
}
