using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Reflection;
using Windows.Graphics;
using WinRT.Interop;
using WinUIEx;
using WinUIEx.Messaging;
using WinUI3Localizer;

namespace CubeOS95
{
    public sealed partial class MainWindow : WindowEx
    {
        private WindowMessageMonitor _msgMonitor;
        public MainWindow(int MinWidth, int MinHeight, int MaxWidth, int MaxHeight)
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            AppWindow.SetIcon("Assets/icon.ico");
            AppWindow.Resize(new SizeInt32(1382, 805));

            OverlappedPresenter presenter = OverlappedPresenter.Create();

            presenter.IsResizable = false;
            presenter.IsMaximizable = false;

            AppWindow.SetPresenter(presenter);

            presenter.PreferredMinimumWidth = MinWidth;
            presenter.PreferredMinimumHeight = MinHeight;
            presenter.PreferredMaximumWidth = MaxWidth;
            presenter.PreferredMaximumHeight = MaxHeight;

            GameFrame.Navigate(typeof(GameIntro), null, new SuppressNavigationTransitionInfo());

            _msgMonitor = new WindowMessageMonitor(this);
            _msgMonitor.WindowMessageReceived += (_, e) =>
            {
                const int WM_NCLBUTTONDBLCLK = 0x00A3;
                if (e.Message.MessageId == WM_NCLBUTTONDBLCLK)
                {
                    e.Result = 0;
                    e.Handled = true;
                }
            };

            CenterWindow();
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
