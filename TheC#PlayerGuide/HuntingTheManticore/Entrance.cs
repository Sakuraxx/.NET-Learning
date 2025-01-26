using Spectre.Console;

namespace HuntingTheManticore;

public class Entrance
{
    public static int GetDisAwayFromManticore()
    {
        int disAwayFromManticore = AnsiConsole.Prompt(
            new TextPrompt<int>("[green]Player 1[/]，how far away from the city do you want to station the Manticore?")
            .PromptStyle("cyan"));

        AnsiConsole.Clear();

        return disAwayFromManticore;
    }

    public static void Main()
    {
        // Style
        var rule = new Rule()
        {
            Style = new Style(Color.Gold1),
            Border = BoxBorder.Ascii,
        };

        // Initail data
        int disAwayFromManticore = GetDisAwayFromManticore();
        int manticorePH = 10;
        int cityPH = 15;
        int round = 1;
        AnsiConsole.MarkupLine("[purple]Player 2[/], it's your turn.");
        while (manticorePH > 0 && cityPH > 0)
        {
            AnsiConsole.Write(rule);

            // The damage dealt to the Manticore depends on the turn number.
            int damage = 1;
            if (round % 3 == 0 && round % 5 == 0)
            {
                damage = 10;
            }
            else if (round % 3 == 0 || round % 5 == 0) 
            {
                damage = 3;
            }

            AnsiConsole.MarkupLineInterpolated($"STATUS: Round: [blue]{round}[/] City: [purple]{cityPH}/15[/] Manticore: [green]{manticorePH}/10[/]");
            AnsiConsole.MarkupLineInterpolated($"The cannon is expected to deal {damage} damage this round.");
            int desiredCannonRange = AnsiConsole.Prompt(
                new TextPrompt<int>("Enter desired cannon range:")
                .PromptStyle("cyan"));

            if (desiredCannonRange > disAwayFromManticore)
            {
                AnsiConsole.MarkupLine("That round OVERSHOT the target.");
            }
            else if(desiredCannonRange < disAwayFromManticore)
            {
                AnsiConsole.MarkupLine("That round FELL SHORT of the target.");
            }
            else
            {
                AnsiConsole.MarkupLine("That round was a DIRECT HIT!");
                manticorePH -= damage;
            }
            cityPH -= 1;
            round++;
        }

        if(manticorePH <= 0)
        {
            AnsiConsole.MarkupLine("The Manticore has been destroyed! The city of Consolas has been saved!");
        }
        else
        {
            AnsiConsole.MarkupLine("The Manticore has prevailed... The city of Consolas has fallen into ruins.");
        }
    }
}