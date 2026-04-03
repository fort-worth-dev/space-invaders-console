namespace SpaceInvaders.Console.Tests;

using FluentAssertions;
using Spectre.Console;
using Spectre.Console.Rendering;

public sealed class SpectreLayoutRendererTests
{
  [Fact]
  public void CreateScene_UsesLayoutAndPlacesPlayerOnBoard() {
    var renderer = new SpectreLayoutRenderer(new NoOpConsoleWriter());
    var state = new GameState(
      Width: 5,
      Height: 4,
      FrameNumber: 3,
      TotalElapsed: TimeSpan.FromMilliseconds(75),
      LastFrameDuration: TimeSpan.FromMilliseconds(25),
      Player: new PlayerState(2, 3),
      Projectiles: [new ProjectileState(2, 1)]);

    var scene = renderer.CreateScene(state);
    var board = renderer.BuildBoard(state);

    scene.Should().BeOfType<Layout>();
    board.Should().Be(
      """
           
        |  
           
        ^  
      """);
  }

  [Fact]
  public void Render_ClearsConsoleAndWritesScene() {
    var console = new RecordingConsoleWriter();
    var renderer = new SpectreLayoutRenderer(console);

    renderer.Render(GameState.CreateDefault());

    console.ClearCalls.Should().Be(1);
    console.Writes.Should().ContainSingle().Which.Should().BeOfType<Layout>();
  }

  private sealed class NoOpConsoleWriter : IConsoleWriter
  {
    public void Clear() {
    }

    public void Write(IRenderable renderable) {
    }
  }

  private sealed class RecordingConsoleWriter : IConsoleWriter
  {
    public int ClearCalls { get; private set; }

    public List<IRenderable> Writes { get; } = [];

    public void Clear() {
      ClearCalls++;
    }

    public void Write(IRenderable renderable) {
      Writes.Add(renderable);
    }
  }
}
