namespace SpaceInvaders.Console;

using Spectre.Console;
using Spectre.Console.Rendering;

internal interface IConsoleWriter
{
  void Clear();

  void Write(IRenderable renderable);
}
