﻿using Spectre.Console;

namespace AdvancedTopics.Delegates;

public class GameController
{
    private enum MethodsType
    {
        IsEven,
        IsPositive,
        IsMultiplesOfTen
    }

    private bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    private bool IsPositive(int number)
    {
        return number > 0;
    }

    private bool IsMultiplesOfTen(int number)
    {
        return number % 10 == 0;
    }

    private Sieve CreateSieve(MethodsType methodsType)
    {
        switch (methodsType) 
        {
            case MethodsType.IsEven:
                return new Sieve(IsEven);
            case MethodsType.IsPositive:
                return new Sieve(IsPositive);
            case MethodsType.IsMultiplesOfTen:
                return new Sieve(IsMultiplesOfTen);
        }
        return null;
    }

    public void Run()
    {
        MethodsType methodsType = AnsiConsole.Prompt(
            new SelectionPrompt<MethodsType>()
            .Title("What kind of filter do you want to use?")
            .AddChoices([MethodsType.IsEven, MethodsType.IsPositive, MethodsType.IsMultiplesOfTen]));
        Sieve? sieve = this.CreateSieve(methodsType);
        AnsiConsole.Clear();

        do
        {
            int number = AnsiConsole.Prompt(new TextPrompt<int>("Please input a number:"));
            if (sieve.IsGood(number))
            {
                AnsiConsole.MarkupLine($"Number [green]{number}[/] is GOOD.");
            }
            else
            {
                AnsiConsole.MarkupLine($"Number [red]{number}[/] is BAD.");
            }
        } while (true);
    }

    public static void Main(string[] args)
    {
        GameController gameController = new GameController();
        gameController.Run();
    }
}
