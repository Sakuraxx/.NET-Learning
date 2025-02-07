using System.Text;
using Spectre.Console;

namespace AdvancedTopic.Threads;

public class GameController
{
    public static void GenerateRandomNumbers(object? recentNumbers)
    {
        if(recentNumbers == null || recentNumbers is not RecentNumbers) { return; }
        RecentNumbers recNumbers = (RecentNumbers)recentNumbers;
        Random random = new Random();
        List<int> result = new List<int>();
        StringBuilder stringBuilder = new StringBuilder();
        while (true)
        {
            result.Clear();
            stringBuilder.Clear();
            for (int i = 0; i < 10; i++)
            {
                result.Add(random.Next(10));
                stringBuilder.Append(result[i]);
            }
            recNumbers.Numbers = result;
            AnsiConsole.MarkupLineInterpolated($"Generate: [blue]{stringBuilder.ToString()}[/]");
            Thread.Sleep(3000);
        }
    }

    public static void AskUserToInputKey(object? recentNumbers)
    {
        if (recentNumbers == null || recentNumbers is not RecentNumbers) { return; }
        RecentNumbers recNumbers = (RecentNumbers)recentNumbers;
        while (true)
        {
            string key = AnsiConsole.Prompt(
                new TextPrompt<string>("Please input a key")
                .PromptStyle("cyan"));
            string lastTwoCharsInKey = key.Substring(key.Length - 2);
            List<int> nums = recNumbers.Numbers;
            string lastTwoCharsInRecentNumbers = nums[^2].ToString() + nums[^1].ToString();
            
            if (lastTwoCharsInKey.Equals(lastTwoCharsInRecentNumbers))
            {
                AnsiConsole.MarkupLine("[green]Identified the repeat![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Wrong key![/]");
            }
        }
    }

    public static void Main(string[] args)
    {
        RecentNumbers recentNumbers = new RecentNumbers();
        Thread randomNumGenThread = new Thread(GenerateRandomNumbers);
        randomNumGenThread.Start(recentNumbers);

        Thread askUserInputKey = new Thread(AskUserToInputKey);
        askUserInputKey.Start(recentNumbers);
    }

}
