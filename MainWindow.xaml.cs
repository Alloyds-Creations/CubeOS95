using Microsoft.UI.Xaml.Media.Animation;
using WinUIEx;
using WinUIEx.Messaging;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace CubeOS95
{
    public sealed partial class MainWindow : Window
    {
        private WindowMessageMonitor _msgMonitor;
        public MainWindow()
        {
            this.InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            this.CenterOnScreen();
            this.AppWindow.SetIcon("Assets/icon.ico");
            SetTitleBar(AppTitleBar);
            AppWindow.Resize(new SizeInt32(1382, 809));

            this.GameFrame.Navigate(typeof(OSSelect), null, new SuppressNavigationTransitionInfo());

            _msgMonitor = new WindowMessageMonitor(this);
            _msgMonitor.WindowMessageReceived += (_, e) =>
            {
                const int WM_NCLBUTTONDBLCLK = 0x00A3;
                if (e.Message.MessageId == WM_NCLBUTTONDBLCLK)
                {
                    // Disable double click on title bar to maximize window
                    e.Result = 0;
                    e.Handled = true;
                }
            };

            var appWindow = this.AppWindow;
            if (appWindow != null)
            {
                var presenter = appWindow.Presenter as OverlappedPresenter;
                if (presenter != null)
                {
                    presenter.IsResizable = false;
                    presenter.IsMaximizable = false;
                }
            }
        }
    }
}
