namespace SpaceInvaders.Console;

internal sealed class GameLoopOptions
{
  public GameLoopOptions(int targetFrameRate = 30) {
    if (targetFrameRate <= 0) {
      throw new ArgumentOutOfRangeException(nameof(targetFrameRate));
    }

    TargetFrameRate = targetFrameRate;
  }

  public int TargetFrameRate { get; }

  public TimeSpan TargetFrameDuration => TimeSpan.FromSeconds(1d / TargetFrameRate);
}
