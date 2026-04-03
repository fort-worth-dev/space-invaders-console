namespace SpaceInvaders.Console;

internal sealed class SystemConsoleInputSource : IInputSource
{
  public InputState ReadInput() {
    var moveLeft = false;
    var moveRight = false;
    var fire = false;

    while (System.Console.KeyAvailable) {
      var key = System.Console.ReadKey(intercept: true).Key;

      switch (key) {
        case ConsoleKey.A:
        case ConsoleKey.LeftArrow:
          moveLeft = true;
          break;
        case ConsoleKey.D:
        case ConsoleKey.RightArrow:
          moveRight = true;
          break;
        case ConsoleKey.Spacebar:
          fire = true;
          break;
      }
    }

    return new InputState(moveLeft, moveRight, fire);
  }
}
