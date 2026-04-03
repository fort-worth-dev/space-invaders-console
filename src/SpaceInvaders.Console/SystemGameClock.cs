namespace SpaceInvaders.Console;

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

[ExcludeFromCodeCoverage]
internal sealed class SystemGameClock : IGameClock
{
  public ValueTask DelayAsync(TimeSpan duration, CancellationToken cancellationToken) {
    return new ValueTask(Task.Delay(duration, cancellationToken));
  }

  public TimeSpan GetElapsedTime(long startTimestamp, long endTimestamp) {
    return Stopwatch.GetElapsedTime(startTimestamp, endTimestamp);
  }

  public long GetTimestamp() {
    return Stopwatch.GetTimestamp();
  }
}
