using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;

namespace CubeOS95.Services;

public interface INavigationService
{
    void Navigate(Type pageType);
}

public sealed class FrameNavigationService(Func<Frame?> frameAccessor) : INavigationService
{
    public void Navigate(Type pageType)
    {
        frameAccessor()?.Navigate(pageType, null, new SuppressNavigationTransitionInfo());
    }
}
