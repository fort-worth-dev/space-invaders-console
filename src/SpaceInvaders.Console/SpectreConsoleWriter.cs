namespace SpaceInvaders.Console;

using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Rendering;

[ExcludeFromCodeCoverage]
internal sealed class SpectreConsoleWriter : IConsoleWriter
{
  private readonly IAnsiConsole _console;

  public SpectreConsoleWriter(IAnsiConsole console) {
    _console = console;
  }

  public void Clear() {
    _console.Clear();
  }

  public void Write(IRenderable renderable) {
    _console.Write(renderable);
  }
}
