﻿using Spectre.Console;

namespace AdvancedTopic.AdvancedExceptionHandling;

public class GameController
{
    private List<int> playerANumList = new List<int>();
    private List<int> playerBNumList = new List<int>();

    public void Run()
    {
        Random random = new Random();
        bool isPlayerATurn = true;
        do
        {
            List<int> numList = isPlayerATurn ? playerANumList : playerBNumList;
            int num = -1;
            string curPlayer = isPlayerATurn ? "A" : "B";
            do
            {
                num = random.Next(10);
            } while (numList.FirstOrDefault(-1) != num && numList.Contains(num));
            AnsiConsole.MarkupLineInterpolated($"It's {curPlayer} turn, the number is [cyan]{num}[/]. Please enter any key to continue.");
            Console.ReadKey();

            if (numList.FirstOrDefault(-1) == num)
            {
                throw new Exception($"Player {curPlayer} ate  the oatmeal raisin cookie, he lose!");
            }

            numList.Add(num);


            isPlayerATurn = !isPlayerATurn;

        } while (true);
    }


    public static void Main(string[] args)
    {
        GameController controller = new GameController();
        try
        {
            controller.Run();
        }
        catch (Exception e) 
        { 
            Console.WriteLine(e.ToString());
        }
    }
}
