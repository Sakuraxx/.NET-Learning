using System.Text.RegularExpressions;
using Spectre.Console;

namespace AdvancedTopic.Files;

public class GameController
{
    private string GetValidUserName()
    {
        string pattern = @"^(?!^(CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9])(\..*)?$)[^<>:""/\\|?*\r\n]+[^. \r\n]$";
        
        string username = AnsiConsole.Prompt(
        new TextPrompt<string>("Please input your name: ")
        .PromptStyle("cyan"));
        string fileName = username + ".txt";
        
        while(!Regex.IsMatch(fileName, pattern, RegexOptions.IgnoreCase))
        {
            username = AnsiConsole.Prompt(
                new TextPrompt<string>("Input name is invalid, please input anotehr name: ")
                .PromptStyle("cyan"));
            fileName = username + ".txt";
        }

        return username;
    }


    private int GetLastScore(string username)
    {
        string fileName = username + ".txt";
        if (!File.Exists(fileName))
        {
            try
            {
                FileStream fs = File.Create(fileName);
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        int lastScore = 0;
        if (File.Exists(fileName))
        {
            string? lastLine = File.ReadLines(fileName).DefaultIfEmpty(null).Last();
            lastScore = Convert.ToInt32(lastLine?.Split(',')[1]);
        }
        return lastScore;
    }


    private List<string> PlayGame(int curScore)
    {
        List<string> playRes = new List<string>();
        ConsoleKey key = ConsoleKey.Enter;
        AnsiConsole.MarkupLine("Please input some key, press the [red]Enter[/] key end the game.");
        do
        {
            key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
            {
                break;
            }
            curScore++;
            playRes.Add(key.ToString() + "," + curScore);
            AnsiConsole.MarkupLineInterpolated($"\nYour current score is {curScore}, press the [red]Enter[/] key end the game.");
        }
        while (true);

        return playRes;
    }


    public void Run()
    {
        string username = this.GetValidUserName();
        int lastScore =  this.GetLastScore(username);
        List<string> playRes = this.PlayGame(lastScore);
    }


    public static void Main(string[] args)
    {
        GameController controller = new GameController();
        controller.Run();
    }

}
