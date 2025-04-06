using System.Dynamic;
using Spectre.Console;

namespace AdvancedTopic.DynamicObjects;

public class GameController
{
    enum UserAction
    {
        CreateRobot,
        ExitGame
    }


    public void Run()
    {
        List<UserAction> actions = Enum.GetValues(typeof(UserAction)).Cast<UserAction>().ToList();
        bool exitGame = false;
        int robotId = 1;
        do
        {
            UserAction uAction = AnsiConsole.Prompt(new SelectionPrompt<UserAction>()
            .Title("What do you want to do?")
            .AddChoices(actions));

            switch (uAction)
            {
                case UserAction.CreateRobot:
                    this.CreateRobot(robotId);
                    robotId++;
                    break;
                case UserAction.ExitGame:
                    exitGame = true;
                    break;
            }

        }while (!exitGame);
    }

    private void CreateRobot(int robotId)
    {
        Console.WriteLine($"You are producing robot #{robotId}.");
        dynamic robot = new ExpandoObject();
        robot.ID = robotId;

        bool hasName = AnsiConsole.Prompt(new ConfirmationPrompt("Do you want to name this robot?"));
        if (hasName)
        {
            string name = AnsiConsole.Prompt(new TextPrompt<string>("What is its name? "));
            robot.Name = name;
        }

        bool hasSize = AnsiConsole.Prompt(new ConfirmationPrompt("Does this robot have a specific size?"));
        if (hasSize)
        {
            int height = AnsiConsole.Prompt(new TextPrompt<int>("What is its height?"));
            int width = AnsiConsole.Prompt(new TextPrompt<int>("What is its width?"));
            robot.Height = height;
            robot.Width = width;
        }

        bool hasColor = AnsiConsole.Prompt(new ConfirmationPrompt("Does this robot need to be a specific color?"));
        if (hasColor)
        {
            string color = AnsiConsole.Prompt(new TextPrompt<string>("What color?"));
            robot.Color = color;
        }

        this.PrintRebot(robot);
    }

    public void PrintRebot(dynamic robot)
    {
        foreach(KeyValuePair<string, object> property in (IDictionary<string, object>)robot)
        {
            Console.WriteLine($"{property.Key}: {property.Value}");
        }      
    }

    public static void Main(string[] args)
    {
        GameController controller = new GameController();
        controller.Run();
    }
}
