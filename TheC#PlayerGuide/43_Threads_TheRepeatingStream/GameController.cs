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
            Thread.Sleep(1000);
        }
    }

    public static void Main(string[] args)
    {
        RecentNumbers recentNumbers = new RecentNumbers();
        Thread randomNumGenThread = new Thread(GenerateRandomNumbers);
        randomNumGenThread.Start(recentNumbers);
    }

}
