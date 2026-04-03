namespace SpaceInvaders.Console;

internal sealed class GameStateStore
{
  private readonly Lock _sync = new();
  private GameState _current;

  public GameStateStore(GameState initialState) {
    _current = initialState;
  }

  public GameState Current {
    get {
      lock (_sync) {
        return _current;
      }
    }
  }

  public void Set(GameState nextState) {
    lock (_sync) {
      _current = nextState;
    }
  }
}
