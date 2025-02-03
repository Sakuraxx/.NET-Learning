using Spectre.Console;

namespace SimulasTest;

public enum BoxState
{
    Open,
    Closed,
    Locked
};

public static class Entrance 
{
    public static BoxState TransitionState(BoxState curState, string code)
    {
        code = code.ToLower();
        switch (curState)
        {
            case BoxState.Open:
                if (code == "close")
                {
                    curState = BoxState.Closed;
                }
                break;
            case BoxState.Closed:
                if (code == "lock")
                {
                    curState = BoxState.Locked;
                }
                else if (code == "open")
                {
                    curState = BoxState.Open;
                }
                break;
            case BoxState.Locked:
                if (code == "unlock")
                {
                    curState = BoxState.Closed;
                }
                break;
        }
        return curState;
    }

    public static void Main()
    {
        BoxState boxState = BoxState.Locked;

        while(true)
        {
            var choice = AnsiConsole.Prompt(
                    new TextPrompt<string>($"The chest is [green]{boxState.ToString()}[/]. What do you want to do?")
                    .PromptStyle("cyan"));
            
            boxState = TransitionState(boxState, choice);
        }
    }
}