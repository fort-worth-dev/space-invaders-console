namespace SpaceInvaders.Console;

using System.Text;
using Spectre.Console;

internal sealed class SpectreLayoutRenderer : IGameRenderer
{
  private readonly IConsoleWriter _consoleWriter;

  public SpectreLayoutRenderer(IConsoleWriter consoleWriter) {
    _consoleWriter = consoleWriter;
  }

  public string BuildBoard(GameState state) {
    var buffer = new StringBuilder((state.Width + 1) * state.Height);

    for (int y = 0; y < state.Height; y++) {
      for (int x = 0; x < state.Width; x++) {
        var tile = state.Player.X == x && state.Player.Y == y ? '^' : ' ';
        buffer.Append(tile);
      }

      if (y < state.Height - 1) {
        buffer.AppendLine();
      }
    }

    return buffer.ToString();
  }

  public Layout CreateScene(GameState state) {
    var root = new Layout("Root");
    var header = new Layout("Header") { Size = 3 };
    var body = new Layout("Body");
    var footer = new Layout("Footer") { Size = 3 };

    root.SplitRows(header, body, footer);
    header.Update(new Panel(new Markup("[bold yellow]Space Invaders[/]")).Expand());
    body.Update(
      new Panel(new Markup(Markup.Escape(BuildBoard(state)))) {
        Border = BoxBorder.Double,
        Expand = true,
      });
    footer.Update(
      new Columns([
        new Markup($"[grey]Frame:[/] {state.FrameNumber}"),
        new Markup($"[grey]Elapsed:[/] {state.TotalElapsed:mm\\:ss\\.fff}"),
        new Markup($"[grey]FPS Target:[/] {state.LastFrameDuration.TotalMilliseconds:0} ms"),
      ]));

    return root;
  }

  public void Render(GameState state) {
    _consoleWriter.Clear();
    _consoleWriter.Write(CreateScene(state));
  }
}
