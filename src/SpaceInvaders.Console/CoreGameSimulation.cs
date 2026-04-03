namespace SpaceInvaders.Console;

internal sealed class CoreGameSimulation : IGameSimulation
{
  public GameState Update(GameState currentState, TimeSpan deltaTime) {
    return currentState with {
      FrameNumber = currentState.FrameNumber + 1,
      TotalElapsed = currentState.TotalElapsed + deltaTime,
      LastFrameDuration = deltaTime,
    };
  }
}
