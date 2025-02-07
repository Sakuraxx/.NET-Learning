using System.Text;
using Spectre.Console;

namespace AdvancedTopic.AsynchronousProgramming;

public class Game
{
    public int RandomlyRecreate(string word)
    {
        StringBuilder stringBuilder = new StringBuilder();
        Random random = new Random();
        bool isMatch = false;
        int attemptTiems = 0;
        do
        {
            stringBuilder.Clear();
            int len = word.Length;
            for (int i = 0; i < len; i++)
            {
                stringBuilder.Append((char)('a' + random.Next(26)));
            }
            if (stringBuilder.ToString().Equals(word))
            {
                isMatch = true;
            }
            attemptTiems++;
        } while (!isMatch);
        return attemptTiems;
    }


    public static void Main(string[] args)
    {
        Game game = new Game();
        string word = AnsiConsole.Prompt(
            new TextPrompt<string>("Please input a word, no more than 5 characters.")
            .PromptStyle("cyan"));
        int times = game.RandomlyRecreate(word);
        AnsiConsole.MarkupLineInterpolated($"Take [green]{times}[/] random creations to recreate this word.");
    }

}
