namespace SpaceInvaders.Console;

internal readonly record struct PlayerState(int X, int Y);
internal readonly record struct ProjectileState(int X, int Y);

internal readonly record struct GameState(
  int Width,
  int Height,
  int FrameNumber,
  TimeSpan TotalElapsed,
  TimeSpan LastFrameDuration,
  PlayerState Player,
  ProjectileState[] Projectiles)
{
  public static GameState CreateDefault() {
    const int boardWidth = 30;
    const int boardHeight = 20;

    return new GameState(
      Width: boardWidth,
      Height: boardHeight,
      FrameNumber: 0,
      TotalElapsed: TimeSpan.Zero,
      LastFrameDuration: TimeSpan.Zero,
      Player: new PlayerState(boardWidth / 2, boardHeight - 1),
      Projectiles: []);
  }
}
