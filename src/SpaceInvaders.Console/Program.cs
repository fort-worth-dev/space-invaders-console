namespace SpaceInvaders.Console;

using System.Diagnostics.CodeAnalysis;
using Spectre.Console;

[ExcludeFromCodeCoverage]
internal static class Program
{
  private static async Task Main(string[] args) {
    using var cancellationSource = new CancellationTokenSource();
    System.Console.CancelKeyPress += (_, eventArgs) => {
      eventArgs.Cancel = true;
      cancellationSource.Cancel();
    };

    var stateStore = new GameStateStore(GameState.CreateDefault());
    var renderer = new SpectreLayoutRenderer(new SpectreConsoleWriter(AnsiConsole.Console));
    var engine = new GameEngine(
      stateStore,
      new CoreGameSimulation(new SystemConsoleInputSource()),
      renderer,
      new SystemGameClock(),
      new GameLoopOptions(targetFrameRate: 30));

    await engine.RunAsync(cancellationSource.Token);
  }
}
