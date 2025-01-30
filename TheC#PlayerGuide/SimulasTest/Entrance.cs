using Spectre.Console;

namespace SimulasTest;

public static class Entrance 
{
    enum BoxState
    {
        Open,
        Closed,
        Locked
    };

    public static void Main()
    {
        BoxState boxState = BoxState.Locked;

        while(true)
        {
            var choice = AnsiConsole.Prompt(
                    new TextPrompt<string>($"The chest is [green]{boxState.ToString()}[/]. What do you want to do?")
                    .PromptStyle("cyan"));
            choice = choice.ToLower();
            switch(boxState)
            {
                case BoxState.Open:
                    if(choice == "close")
                    {
                        boxState = BoxState.Closed;
                    }
                    break;
                case BoxState.Closed:
                    if(choice == "lock")
                    {
                        boxState = BoxState.Locked;
                    }
                    else if(choice == "open")
                    {
                        boxState = BoxState.Open;
                    }
                    break;
                case BoxState.Locked:
                    if(choice == "unlock")
                    {
                        boxState = BoxState.Closed;
                    }
                    break;
            }
        }
    }
}