namespace SpaceInvaders.Console.Tests;

using FluentAssertions;

public sealed class CoreGameSimulationTests
{
  [Fact]
  public void Update_MovesPlayerLeftAndClampsAtBoardEdge() {
    var simulation = new CoreGameSimulation(new StubInputSource(new InputState(MoveLeft: true, MoveRight: false, Fire: false)));
    var state = GameState.CreateDefault() with {
      Width = 5,
      Player = new PlayerState(0, 4),
    };

    var nextState = simulation.Update(state, TimeSpan.FromMilliseconds(16));

    nextState.Player.Should().Be(new PlayerState(0, 4));
  }

  [Fact]
  public void Update_MovesPlayerRightAndClampsAtBoardEdge() {
    var simulation = new CoreGameSimulation(new StubInputSource(new InputState(MoveLeft: false, MoveRight: true, Fire: false)));
    var state = GameState.CreateDefault() with {
      Width = 5,
      Player = new PlayerState(4, 4),
    };

    var nextState = simulation.Update(state, TimeSpan.FromMilliseconds(16));

    nextState.Player.Should().Be(new PlayerState(4, 4));
  }

  [Fact]
  public void Update_SpawnsProjectileAbovePlayerAfterMovement() {
    var simulation = new CoreGameSimulation(new StubInputSource(new InputState(MoveLeft: false, MoveRight: true, Fire: true)));
    var state = GameState.CreateDefault() with {
      Width = 8,
      Height = 6,
      Player = new PlayerState(3, 5),
    };

    var nextState = simulation.Update(state, TimeSpan.FromMilliseconds(16));

    nextState.Player.Should().Be(new PlayerState(4, 5));
    nextState.Projectiles.Should().Equal(new ProjectileState(4, 4));
  }

  [Fact]
  public void Update_MovesExistingProjectilesUpward() {
    var simulation = new CoreGameSimulation(new StubInputSource(default));
    var state = GameState.CreateDefault() with {
      Projectiles = [new ProjectileState(3, 5), new ProjectileState(8, 1)],
    };

    var nextState = simulation.Update(state, TimeSpan.FromMilliseconds(16));

    nextState.Projectiles.Should().Equal(
      new ProjectileState(3, 4),
      new ProjectileState(8, 0));
  }

  [Fact]
  public void Update_RemovesProjectilesAfterTheyLeaveTheBoard() {
    var simulation = new CoreGameSimulation(new StubInputSource(default));
    var state = GameState.CreateDefault() with {
      Projectiles = [new ProjectileState(2, 0)],
    };

    var nextState = simulation.Update(state, TimeSpan.FromMilliseconds(16));

    nextState.Projectiles.Should().BeEmpty();
  }

  private sealed class StubInputSource : IInputSource
  {
    private readonly InputState _input;

    public StubInputSource(InputState input) {
      _input = input;
    }

    public InputState ReadInput() {
      return _input;
    }
  }
}
