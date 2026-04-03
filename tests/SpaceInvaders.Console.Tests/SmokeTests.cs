namespace SpaceInvaders.Console.Tests;

using System.Reflection;
using FluentAssertions;

public sealed class SmokeTests
{
  [Fact]
  public void ApplicationAssemblyLoads() {
    var assembly = Assembly.Load("SpaceInvaders.Console");

    assembly.Should().NotBeNull();
    assembly.GetName().Name.Should().Be("SpaceInvaders.Console");
  }
}
