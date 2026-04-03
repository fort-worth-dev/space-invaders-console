namespace SpaceInvaders.Console.Tests;

using FluentAssertions;

public sealed class GameStateTests
{
  [Fact]
  public void CreateDefault_CentersPlayerOnBottomRow() {
    var state = GameState.CreateDefault();

    state.Width.Should().Be(30);
    state.Height.Should().Be(20);
    state.Player.Should().Be(new PlayerState(15, 19));
    state.Projectiles.Should().BeEmpty();
    state.FrameNumber.Should().Be(0);
  }

  [Fact]
  public void StateStore_SetReplacesCurrentState() {
    var initialState = GameState.CreateDefault();
    var nextState = initialState with { FrameNumber = 8 };
    var store = new GameStateStore(initialState);

    store.Set(nextState);

    store.Current.Should().Be(nextState);
  }
}
