using AdvancedTopic.QueryExpression;

namespace _40_QueryExpression_TheThreeLenes_Test;

public class UnitTest
{
    [Theory]
    [InlineData(new int[] { 1, 9, 2, 8, 3, 7, 4, 6, 5 })]
    public void Test_NormalThreelens(int[] numbers)
    {
        // Arrange
        GameController gameController = new GameController();
        int[] expected = new int[] { 4, 8, 12, 16 };
        
        // Act
        IEnumerable<int> res = gameController.NormalThreelens(numbers);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(expected, res.ToArray());
    }


    [Theory]
    [InlineData(new int[] { 1, 9, 2, 8, 3, 7, 4, 6, 5 })]
    public void Test_KeyWordvBasedQueryThreeLens(int[] numbers)
    {
        // Arrange
        GameController gameController = new GameController();
        int[] expected = new int[] { 4, 8, 12, 16 };

        // Act
        IEnumerable<int> res = gameController.KeyWordvBasedQueryThreeLens(numbers);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(expected, res.ToArray());
    }


    [Theory]
    [InlineData(new int[] { 1, 9, 2, 8, 3, 7, 4, 6, 5 })]
    public void Test_MethodCallBasedQueryThreeLens(int[] numbers)
    {
        // Arrange
        GameController gameController = new GameController();
        int[] expected = new int[] { 4, 8, 12, 16 };

        // Act
        IEnumerable<int> res = gameController.MethodCallBasedQueryThreeLens(numbers);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(expected, res.ToArray());
    }
}