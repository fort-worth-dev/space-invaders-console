namespace SpaceInvaders.Console;

internal sealed class GameEngine
{
  private readonly IGameClock _clock;
  private readonly GameLoopOptions _options;
  private readonly IGameRenderer _renderer;
  private readonly IGameSimulation _simulation;
  private readonly GameStateStore _stateStore;

  public GameEngine(
    GameStateStore stateStore,
    IGameSimulation simulation,
    IGameRenderer renderer,
    IGameClock clock,
    GameLoopOptions options) {
    _stateStore = stateStore;
    _simulation = simulation;
    _renderer = renderer;
    _clock = clock;
    _options = options;
  }

  public async Task RunAsync(CancellationToken cancellationToken) {
    try {
      while (!cancellationToken.IsCancellationRequested) {
        await RunSingleFrameAsync(cancellationToken);
      }
    } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
    }
  }

  public async Task RunFramesAsync(int frameCount, CancellationToken cancellationToken) {
    if (frameCount < 0) {
      throw new ArgumentOutOfRangeException(nameof(frameCount));
    }

    for (int frameIndex = 0; frameIndex < frameCount; frameIndex++) {
      cancellationToken.ThrowIfCancellationRequested();
      await RunSingleFrameAsync(cancellationToken);
    }
  }

  private async Task RunSingleFrameAsync(CancellationToken cancellationToken) {
    var frameStartedAt = _clock.GetTimestamp();
    var nextState = _simulation.Update(_stateStore.Current, _options.TargetFrameDuration);
    _stateStore.Set(nextState);
    _renderer.Render(nextState);

    var frameFinishedAt = _clock.GetTimestamp();
    var workDuration = _clock.GetElapsedTime(frameStartedAt, frameFinishedAt);
    var remainingBudget = _options.TargetFrameDuration - workDuration;
    if (remainingBudget > TimeSpan.Zero) {
      await _clock.DelayAsync(remainingBudget, cancellationToken);
    }
  }
}
