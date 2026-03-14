using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

namespace CubeOS95
{
    public sealed partial class GameSettings : Page
    {
        public GameSettings()
        {
            InitializeComponent();
        }
        private void BackToOSSelect_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(OSSelect), null, new SuppressNavigationTransitionInfo());
        }
    }
}
