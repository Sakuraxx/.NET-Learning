using Spectre.Console;

namespace OOP.Polymorphism;

public class Entrance
{
    enum Command
    {
        On,
        Off,
        North,
        South,
        West,
        East
    }

    public static void Main(string[] args)
    {
        List<Command> choices;
        do
        {
            choices = AnsiConsole.Prompt(
            new MultiSelectionPrompt<Command>()
            .Title("Please choose 3 commands below:")
            .AddChoices([Command.On, Command.Off, Command.North, Command.South, Command.West, Command.East]));
            AnsiConsole.Clear();
        } while (choices.Count != 3);

        AnsiConsole.MarkupLine($"You chose [green]{choices[0]}, {choices[1]}, {choices[2]}[/].");

        Robot robot = new Robot();
        for (int i = 0; i < 3; i++)
        {
            switch (choices[i]) 
            {
                case Command.On:
                    robot.Commands[i] = new OnCommand(); 
                    break;
                case Command.Off:
                    robot.Commands[i] = new OffCommand();
                    break;
                case Command.North:
                    robot.Commands[i] = new NorthCommand();
                    break;
                case Command.South:
                    robot.Commands[i] = new SouthCommand();
                    break;
                case Command.West:
                    robot.Commands[i] = new WestCommand();
                    break;
                case Command.East:
                    robot.Commands[i] = new EastCommand();
                    break;
            }
        }
        robot.Run();
    }
}
