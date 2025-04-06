namespace AdvancedTopic.DynamicObjects.Test;

public class UnitTest
{
    [Theory]
    // Integer tests
    [InlineData(1, 1, 2)]
    [InlineData(5, 10, 15)]
    [InlineData(-5, 5, 0)]
    [InlineData(0, 0, 0)]
    // Double tests
    [InlineData(1.5, 2.5, 4.0)]
    [InlineData(3.14, 2.71, 5.85)]
    [InlineData(-10.0, 5.5, -4.5)]
    // String tests
    [InlineData("Hello, ", "World!", "Hello, World!")]
    [InlineData("Test", "String", "TestString")]
    [InlineData("", "NotEmpty", "NotEmpty")]
    [InlineData("NotEmpty", "", "NotEmpty")]
    public void Test_DynamicAdds(dynamic a, dynamic b, dynamic expectedResult)
    {
        // Arrange - Parameters 'a', 'b', and 'expectedResult' are provided by [InlineData]

        // Act
        var actualResult = GameController.DynamicAdd(a, b);

        // Assert
        Assert.Equal(expectedResult, actualResult);

        Assert.IsType(expectedResult.GetType(), actualResult);
    }

    [Fact] // Use [Fact] for tests without inline parameters, like DateTime/TimeSpan
    public void Test_DynamicAdds_DateTimeAndTimeSpan()
    {
        // Arrange
        var startDate = new DateTime(2024, 1, 1, 12, 0, 0); // Use a fixed date for predictability
        var duration = TimeSpan.FromHours(3);
        var expectedResult = new DateTime(2024, 1, 1, 15, 0, 0);

        // Act
        // Explicitly cast to dynamic if startDate/duration aren't already dynamic,
        // although passing them directly usually works because the method expects dynamic.
        var actualResult = GameController.DynamicAdd((dynamic)startDate, (dynamic)duration);

        // Assert
        Assert.Equal(expectedResult, actualResult);
        Assert.IsType<DateTime>(actualResult);
    }

    [Fact]
    public void Test_DynamicAdds_Incompatible_DateTimeAndInt_ReturnsNull() // Assuming you chose to return null
    {
        // Arrange
        dynamic value1 = DateTime.Now;
        dynamic value2 = 5;

        // Act
        var result = GameController.DynamicAdd(value1, value2);

        // Assert
        Assert.Null(result);
    }
}
