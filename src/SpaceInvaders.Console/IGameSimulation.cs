namespace SpaceInvaders.Console;

internal interface IGameSimulation
{
  GameState Update(GameState currentState, TimeSpan deltaTime);
}
