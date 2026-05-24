using Microsoft.UI.Xaml.Media.Animation;
using WinUIEx;
using WinUIEx.Messaging;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System.Runtime.CompilerServices;
using Windows.Win32;

namespace CubeOS95
{
    public sealed partial class MainWindow : WindowEx
    {
        private WindowMessageMonitor _msgMonitor;
        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            AppWindow.SetIcon("Assets/icon.ico");
            SetTitleBar(AppTitleBar);

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
        }
    }
}
