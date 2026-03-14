using CommunityToolkit.WinUI.Animations;
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
using CubeOS95.OperatingSystems.CubeOS95.Resources.Pages;
using CubeOS95.OperatingSystems.CubeOS95Plus.Resources.Pages;

namespace CubeOS95
{
    public sealed partial class OSSelect : Page
    {
        public OSSelect()
        {
            this.InitializeComponent();
        }
        private void StartSystem95OS_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StartingCubeOS95), null, new SuppressNavigationTransitionInfo());
        }
        private void StartSystem95PlusOS_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StartingCubeOS95Plus), null, new SuppressNavigationTransitionInfo());
        }
        private void GameSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GameSettings), null, new SuppressNavigationTransitionInfo());
        }
    }
}
