namespace SpaceInvaders.Console.Tests;

using FluentAssertions;

public sealed class GameEngineTests
{
  [Fact]
  public async Task RunFramesAsync_AdvancesStateAndRendersEachFrame() {
    var clock = new FakeGameClock();
    var renderer = new RecordingRenderer();
    var stateStore = new GameStateStore(GameState.CreateDefault());
    var engine = new GameEngine(
      stateStore,
      new CoreGameSimulation(),
      renderer,
      clock,
      new GameLoopOptions(targetFrameRate: 20));

    await engine.RunFramesAsync(frameCount: 2, CancellationToken.None);

    stateStore.Current.FrameNumber.Should().Be(2);
    stateStore.Current.TotalElapsed.Should().Be(TimeSpan.FromMilliseconds(100));
    renderer.RenderedStates.Should().HaveCount(2);
    renderer.RenderedStates.Select(state => state.FrameNumber).Should().Equal(1, 2);
  }

  [Fact]
  public async Task RunFramesAsync_WaitsForRemainingFrameBudget() {
    var clock = new FakeGameClock(
      TimeSpan.Zero,
      TimeSpan.FromMilliseconds(5),
      TimeSpan.FromMilliseconds(50),
      TimeSpan.FromMilliseconds(70));
    var renderer = new RecordingRenderer();
    var stateStore = new GameStateStore(GameState.CreateDefault());
    var engine = new GameEngine(
      stateStore,
      new CoreGameSimulation(),
      renderer,
      clock,
      new GameLoopOptions(targetFrameRate: 20));

    await engine.RunFramesAsync(frameCount: 2, CancellationToken.None);

    clock.Delays.Should().Equal(
      TimeSpan.FromMilliseconds(45),
      TimeSpan.FromMilliseconds(30));
  }

  [Fact]
  public async Task RunAsync_StopsAfterCancellation() {
    using var cancellationSource = new CancellationTokenSource();
    var clock = new FakeGameClock();
    var renderer = new CancelAfterFirstRenderRenderer(cancellationSource);
    var stateStore = new GameStateStore(GameState.CreateDefault());
    var engine = new GameEngine(
      stateStore,
      new CoreGameSimulation(),
      renderer,
      clock,
      new GameLoopOptions(targetFrameRate: 30));

    await engine.RunAsync(cancellationSource.Token);

    renderer.RenderCount.Should().Be(1);
    stateStore.Current.FrameNumber.Should().Be(1);
  }

  [Fact]
  public async Task RunFramesAsync_RejectsNegativeFrameCounts() {
    var engine = new GameEngine(
      new GameStateStore(GameState.CreateDefault()),
      new CoreGameSimulation(),
      new RecordingRenderer(),
      new FakeGameClock(),
      new GameLoopOptions());

    Func<Task> act = async () => await engine.RunFramesAsync(-1, CancellationToken.None);

    await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
  }

  [Fact]
  public void GameLoopOptions_RejectsNonPositiveFrameRates() {
    var act = () => new GameLoopOptions(0);

    act.Should().Throw<ArgumentOutOfRangeException>();
  }

  private sealed class RecordingRenderer : IGameRenderer
  {
    public List<GameState> RenderedStates { get; } = [];

    public void Render(GameState state) {
      RenderedStates.Add(state);
    }
  }

  private sealed class CancelAfterFirstRenderRenderer : IGameRenderer
  {
    private readonly CancellationTokenSource _cancellationSource;

    public CancelAfterFirstRenderRenderer(CancellationTokenSource cancellationSource) {
      _cancellationSource = cancellationSource;
    }

    public int RenderCount { get; private set; }

    public void Render(GameState state) {
      RenderCount++;
      _cancellationSource.Cancel();
    }
  }

  private sealed class FakeGameClock : IGameClock
  {
    private readonly Queue<TimeSpan> _timestamps;
    private TimeSpan _lastTimestamp;

    public FakeGameClock(params TimeSpan[] timestamps) {
      _timestamps = new Queue<TimeSpan>(timestamps.Length == 0 ? [TimeSpan.Zero] : timestamps);
      _lastTimestamp = _timestamps.Peek();
    }

    public List<TimeSpan> Delays { get; } = [];

    public long GetTimestamp() {
      if (_timestamps.Count > 0) {
        _lastTimestamp = _timestamps.Dequeue();
      }

      return _lastTimestamp.Ticks;
    }

    public TimeSpan GetElapsedTime(long startTimestamp, long endTimestamp) {
      return TimeSpan.FromTicks(endTimestamp - startTimestamp);
    }

    public ValueTask DelayAsync(TimeSpan duration, CancellationToken cancellationToken) {
      Delays.Add(duration);
      return ValueTask.CompletedTask;
    }
  }
}
