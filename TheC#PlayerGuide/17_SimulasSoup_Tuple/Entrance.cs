using Spectre.Console;

namespace OOP.Tuples;

public static class SimulasSoup
{
    public enum Type
    {
        Soup,
        Stew,
        Gumbo
    };

    public enum Ingredient
    {
        Mushrooms,
        Chicken,
        Carrots,
        Potatoes
    };

    public enum Seasoning
    {
        Spicy,
        Salty,
        Sweet
    };

    public static (Type, Ingredient, Seasoning) AskUserFoodChoices()
    {
        Type type = AnsiConsole.Prompt(
                    new SelectionPrompt<Type>()
                    .Title("What type of recipe do you want to cook?")
                    .AddChoices([Type.Soup, Type.Stew, Type.Gumbo]));

        Ingredient ingredient = AnsiConsole.Prompt(
            new SelectionPrompt<Ingredient>()
            .Title("What type of ingredient do you want to cook?")
            .AddChoices([Ingredient.Mushrooms, Ingredient.Chicken, Ingredient.Carrots, Ingredient.Potatoes]));

        Seasoning seasoning = AnsiConsole.Prompt(
            new SelectionPrompt<Seasoning>()
            .Title("What type of recipe do you want to cook?")
            .AddChoices([Seasoning.Spicy, Seasoning.Salty, Seasoning.Sweet]));

        return (type, ingredient, seasoning);
    }


    public static void Main()
    {
        (Type type, Ingredient ingredient, Seasoning seasoning) = AskUserFoodChoices();
        AnsiConsole.MarkupLine($"Congrats, you made the [cyan]{seasoning} {ingredient} {type}[/]!");
    }
}