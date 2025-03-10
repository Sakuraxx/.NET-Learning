#region Usings

using System.Text.RegularExpressions;
using Spectre.Console;

#endregion Usings

namespace AdvancedTopic.OperatorOverloading;

public record BlockCoordinate(int Row, int Column)
{
    public static BlockCoordinate operator +(BlockCoordinate c, BlockOffset o)
        => new BlockCoordinate(c.Row + o.RowOffset, c.Column + o.ColumnOffset);

    public static BlockCoordinate operator +(BlockCoordinate c, Direction d)
    {
        switch (d)
        {
            case Direction.North:
                return new BlockCoordinate(c.Row - 1, c.Column);
            case Direction.South:
                return new BlockCoordinate(c.Row + 1, c.Column);
            case Direction.East:
                return new BlockCoordinate(c.Row, c.Column + 1);
            case Direction.West:
                return new BlockCoordinate(c.Row, c.Column - 1);
            default:
                break;
        }
        return c;
    }
}

public record BlockOffset(int RowOffset, int ColumnOffset);
public enum Direction { North, East, South, West }

public enum UserAction 
{
    AddBlockOffset,
    AddDirection,
    ExitGame
}

public class GameController
{
    public void Run()
    {
        UserAction action = UserAction.ExitGame;
        BlockCoordinate curCoordinate = new BlockCoordinate(0, 0);
        do
        {
            AnsiConsole.MarkupLineInterpolated($"Your current block coordinate is [green]({curCoordinate.Row}, {curCoordinate.Column})[/]");
            var userActions = Enum.GetValues<UserAction>().ToList();
            action = AnsiConsole.Prompt(new SelectionPrompt<UserAction>()
                .AddChoices(userActions)
                .Title("Please choose one action"));

            switch (action) 
            {
                case UserAction.AddBlockOffset:
                    var input = AnsiConsole.Prompt(
                    new TextPrompt<string>("Please input a block offset row, col:")
                        .PromptStyle("cyan")
                        .Validate(value =>
                        {
                            return Regex.IsMatch(value, @"^\d+,\d+$")
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Format error，please input row, col![/]");
                        }));

                    var numbers = input.Split(',');
                    int row = int.Parse(numbers[0]);
                    int col = int.Parse(numbers[1]);
                    curCoordinate = curCoordinate + new BlockOffset(row, col);
                    break;
                case UserAction.AddDirection:
                    var directions = Enum.GetValues<Direction>().ToList();
                    Direction dir = AnsiConsole.Prompt(new SelectionPrompt<Direction>()
                        .AddChoices(directions)
                        .Title("Please choose one direction"));
                    curCoordinate = curCoordinate + dir;
                    break;
                default:
                    break;
            }

            AnsiConsole.Clear();

        } while (action != UserAction.ExitGame);
    }


    public static void Main(string[] args)
    {
        GameController gameController = new GameController();
        gameController.Run();
    }

}