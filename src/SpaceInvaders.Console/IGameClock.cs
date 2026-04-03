namespace SpaceInvaders.Console;

internal interface IGameClock
{
  ValueTask DelayAsync(TimeSpan duration, CancellationToken cancellationToken);

  TimeSpan GetElapsedTime(long startTimestamp, long endTimestamp);

  long GetTimestamp();
}
