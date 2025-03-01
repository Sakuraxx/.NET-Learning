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
    RuinedPotion,
}

enum UserAction
{
    StartMakingPotion,
    StopMakingPotion,
    CompletePotion,
    ContinueAddingIngredient
}

public class GameController
{
    private List<Potion> userPotionList = new List<Potion>();

    private void MakePotion()
    {
        /**
         * Potion made judge
         * water -> water
         * stardust + water -> elixir
         * snake venom  + elixir -> posion potion
         * drag breath + elixir -> flying potion
         * shadow glass + elixir -> invisibility potion
         * eyeshine gem + elixir -> night sight potion
         * shadow glass + night potion -> cloudy brew
         * eyeshine gem + invisibility potion -> cloudy brew
         * stardust + cloudy brew -> wraith
         * others -> ruined potion
         */

        List<Ingredient> ingredients = Enum.GetValues(typeof(Ingredient)).Cast<Ingredient>().ToList();
        List<Ingredient> curSelections = new List<Ingredient>();
        Potion curPotion = Potion.RuinedPotion;

        while (true)
        {
            var choice = AnsiConsole.Prompt(new SelectionPrompt<Ingredient>()
            .Title("You can pick one of the follwing ingredients to make the magic potion!")
            .AddChoices(ingredients));
            curSelections.Add(choice);
            if (curSelections.Count == 2 && curSelections.Contains(Ingredient.Water) && curSelections.Contains(Ingredient.Stardust))
            {
                curPotion = Potion.Elixir;
            }
            else if (curSelections.Count == 3)
            {
                if (curSelections.Contains(Ingredient.Water) && curSelections.Contains(Ingredient.Stardust))
                {
                    if (curSelections.Contains(Ingredient.SnakeVenom))
                    {
                        curPotion = Potion.PosionPotion;
                    }
                    else if (curSelections.Contains(Ingredient.DragonBreath))
                    {
                        curPotion = Potion.FlyingPotion;
                    }
                    else if (curSelections.Contains(Ingredient.EyeshineGem))
                    {
                        curPotion = Potion.NightSightPotion;
                    }
                    else if (curSelections.Contains(Ingredient.ShadowGlass))
                    {
                        curPotion = Potion.InvisibilityPotion;
                    }
                }
            }
            else if (curSelections.Count == 4)
            {
                if (curSelections.Contains(Ingredient.Water)
                    && curSelections.Contains(Ingredient.Stardust)
                    && curSelections.Contains(Ingredient.EyeshineGem)
                    && curSelections.Contains(Ingredient.ShadowGlass))
                {
                    curPotion = Potion.CloudyBrew;
                }
            }
            else if (curSelections.Count == 5
                    && curSelections.Contains(Ingredient.Water)
                    && curSelections.Contains(Ingredient.Stardust)
                    && curSelections.Contains(Ingredient.EyeshineGem)
                    && curSelections.Contains(Ingredient.ShadowGlass)
                    && curSelections.Count(c => c == Ingredient.Stardust) == 2)
            {
                curPotion = Potion.WraithPotion;
            }

            UserAction action = AnsiConsole.Prompt(new SelectionPrompt<UserAction>()
                .Title("Choose one action to continue the game.")
                .AddChoices(UserAction.CompletePotion, UserAction.ContinueAddingIngredient));

            if (action == UserAction.CompletePotion)
            {
                if (curPotion == Potion.RuinedPotion)
                {
                    AnsiConsole.WriteLine("Ooops, you made a ruined potion! Start over with water to make a new magic potion!");
                }
                else
                {
                    AnsiConsole.MarkupLineInterpolated($"Congrats!, you made [green]{curPotion}[/]");
                    userPotionList.Add(curPotion);
                }
                break;
            }
        }
    }


    public void Run()
    {
        while (true) {
            if (this.userPotionList.Count == 0)
            {
                AnsiConsole.WriteLine("You don't have any potions yet.");
            }
            else
            {
                string? potionsStr = string.Join(",", this.userPotionList);
                AnsiConsole.MarkupLineInterpolated($"Your current potions are [green]{potionsStr}[/]");
            }
            UserAction action = AnsiConsole.Prompt(new SelectionPrompt<UserAction>()
                .Title("What do you want to do?")
                .AddChoices(UserAction.StartMakingPotion, UserAction.StopMakingPotion));
            if(action == UserAction.StartMakingPotion)
            {
                this.MakePotion();
                AnsiConsole.Clear();
                continue;
            }
            break;
        }
    }


    public static void Main(string[] args)
    {
        GameController gameController = new GameController();
        gameController.Run();
    }

}
