using Spectre.Console;

namespace AdvancedTopic.PatternMatching;

enum Ingredient
{
    Water,
    SnakeVenom,
    Stardust,
    DragonBreath,
    ShadowGlass,
    EyeshineGem,
}

enum Potion
{
    Elixir,
    PosionPotion,
    FlyingPotion,
    InvisibilityPotion,
    NightSightPotion,
    CloudyBrew,
    WraithPotion,
}

public class GameController
{
    private List<Potion> userPotionList = new List<Potion>();

    public void Run()
    {
        this.userPotionList.Add(Potion.Elixir);
        this.userPotionList.Add(Potion.InvisibilityPotion);
        if (this.userPotionList.Count == 0)
        {
            AnsiConsole.WriteLine("You don't have any potions yet.");
        }
        else
        {
            string? potionsStr = string.Join(",", this.userPotionList);
            AnsiConsole.MarkupLineInterpolated($"Your current potions are [green]{potionsStr}[/]");
        }
    }


    public static void Main(string[] args)
    {
        GameController gameController = new GameController();
        gameController.Run();
    }

}
