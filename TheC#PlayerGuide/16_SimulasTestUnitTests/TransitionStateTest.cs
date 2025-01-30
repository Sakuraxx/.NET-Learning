using SimulasTest;

namespace SimulasTestUnitTests;

public class SimulasTestUnitTest
{
    private const string UNLOCK = "unlock";
    private const string LOCK = "lock";
    private const string CLOSE = "close";
    private const string OPEN = "open";


    [Theory]
    [InlineData(BoxState.Locked, UNLOCK, BoxState.Closed)]
    [InlineData(BoxState.Locked, LOCK, BoxState.Locked)]
    [InlineData(BoxState.Locked, CLOSE, BoxState.Locked)]
    [InlineData(BoxState.Locked, OPEN, BoxState.Locked)]

    [InlineData(BoxState.Closed, UNLOCK, BoxState.Closed)]
    [InlineData(BoxState.Closed, LOCK, BoxState.Locked)]
    [InlineData(BoxState.Closed, CLOSE, BoxState.Closed)]
    [InlineData(BoxState.Closed, OPEN, BoxState.Open)]

    [InlineData(BoxState.Open, UNLOCK, BoxState.Open)]
    [InlineData(BoxState.Open, LOCK, BoxState.Open)]
    [InlineData(BoxState.Open, CLOSE, BoxState.Closed)]
    [InlineData(BoxState.Open, OPEN, BoxState.Open)]
    public void TransistionStateTest(BoxState curState, string code, BoxState expectState)
    {
        // Arrange
        // Act
        BoxState actualTranState = Entrance.TransitionState(curState, code);

        // Assert
        Assert.Equal(expectState, actualTranState);
    }
}