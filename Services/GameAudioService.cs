using System;
using System.IO;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace CubeOS95.Services;

public sealed class GameAudioService : IDisposable
{
    private MediaPlayer? _introPlayer = new();
    private MediaPlayer? _outroPlayer = new();
    private MediaPlayer? _clickPlayer = new();

    public void PlayClick()
    {
        Play(_clickPlayer, "/Assets/Sounds/click.mp3");
    }

    public void PlayIntro(string relativePath)
    {
        Play(_introPlayer, relativePath);
    }

    public void PlayOutro(string relativePath)
    {
        StopAndDispose(ref _introPlayer);
        Play(_outroPlayer, relativePath);
    }

    public void StopIntro()
    {
        StopAndDispose(ref _introPlayer);
    }

    public void Dispose()
    {
        StopAndDispose(ref _introPlayer);
        StopAndDispose(ref _outroPlayer);
        StopAndDispose(ref _clickPlayer);
    }

    private static void Play(MediaPlayer? player, string relativePath)
    {
        if (player is null)
        {
            return;
        }

        player.Source = MediaSource.CreateFromUri(ToAppUri(relativePath));
        player.Volume = 0.1;
        player.Play();
    }

    private static void StopAndDispose(ref MediaPlayer? player)
    {
        if (player is null)
        {
            return;
        }

        player.Pause();
        player.Dispose();
        player = null;
    }

    private static Uri ToAppUri(string appRelativePath)
    {
        string relativePath = appRelativePath.TrimStart('/', '\\').Replace('/', Path.DirectorySeparatorChar);
        return new Uri(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath)));
    }
}
