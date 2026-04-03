namespace SpaceInvaders.Console;

internal sealed class CoreGameSimulation : IGameSimulation
{
  private static readonly IInputSource NoInput = new NullInputSource();
  private readonly IInputSource _inputSource;

  public CoreGameSimulation()
    : this(NoInput) {
  }

  public CoreGameSimulation(IInputSource inputSource) {
    _inputSource = inputSource;
  }

  public GameState Update(GameState currentState, TimeSpan deltaTime) {
    var input = _inputSource.ReadInput();
    var horizontalDelta = input switch {
      { MoveLeft: true, MoveRight: false } => -1,
      { MoveLeft: false, MoveRight: true } => 1,
      _ => 0,
    };

    var nextPlayerX = Math.Clamp(currentState.Player.X + horizontalDelta, 0, currentState.Width - 1);
    var nextPlayer = currentState.Player with { X = nextPlayerX };
    var nextProjectiles = currentState.Projectiles
      .Select(projectile => projectile with { Y = projectile.Y - 1 })
      .Where(projectile => projectile.Y >= 0)
      .ToList();

    if (input.Fire) {
      var spawnY = nextPlayer.Y - 1;
      if (spawnY >= 0) {
        nextProjectiles.Add(new ProjectileState(nextPlayer.X, spawnY));
      }
    }

    return currentState with {
      FrameNumber = currentState.FrameNumber + 1,
      TotalElapsed = currentState.TotalElapsed + deltaTime,
      LastFrameDuration = deltaTime,
      Player = nextPlayer,
      Projectiles = nextProjectiles.ToArray(),
    };
  }

  private sealed class NullInputSource : IInputSource
  {
    public InputState ReadInput() {
      return default;
    }
  }
}
