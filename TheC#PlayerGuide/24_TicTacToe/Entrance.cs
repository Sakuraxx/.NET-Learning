using Spectre.Console;

namespace OOP.Class;

public class TicTacToe
{
    private static readonly int ROW_COL_NUM = 3;
    private static readonly char EMPTY = ' ';
    private static readonly char USER_X = 'X';
    private static readonly char USER_O = 'O';

    private char[][] tttMatrix = new char[ROW_COL_NUM][];

    public TicTacToe()
    {
        tttMatrix = new char[ROW_COL_NUM][];
        for(int i = 0; i < ROW_COL_NUM; i++)
        {
            this.tttMatrix[i] = new char[ROW_COL_NUM];
            for(int j = 0; j < ROW_COL_NUM; j++)
            {
                tttMatrix[i][j] = EMPTY;
            }
        }
    }

    enum Position
    {
        LeftTop,
        MiddleTopCenter,
        RightTop,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        MiddleBottomCenter,
        BottomRight
    }

    enum WinFlag
    {
        None,
        User_O_Win,
        User_X_Win,
        No_One_Win
    }

    private void PrintTTTMaxtrix()
    {
        for (int i = 0; i < ROW_COL_NUM; i++)
        {
            for (int j = 0; j < ROW_COL_NUM; j++)
            {
                AnsiConsole.Markup(this.tttMatrix[i][j].ToString() + "|");
            }
            AnsiConsole.MarkupLine("\n-------");
        }
    }

    public void Run()
    {
        WinFlag winFlag = WinFlag.None;
        bool isOTurn = true;
        while(winFlag == WinFlag.None)
        {
            char user = isOTurn ? USER_O : USER_X;
            AnsiConsole.MarkupLine($"It is {user}'s turn.");
            PrintTTTMaxtrix();

            List<Position> posOptions = new();
            for (int i = 0; i < ROW_COL_NUM; i++)
            {
                for(int j = 0; j < ROW_COL_NUM; j++)
                {
                    if(tttMatrix[i][j] == ' ')
                    {
                        posOptions.Add((Position)(i * ROW_COL_NUM + j));
                    }
                }
            }

            Position pos = AnsiConsole.Prompt(
                new SelectionPrompt<Position>()
                .Title("What square do you want to play in?")
                .AddChoices([.. posOptions]));

            int rowInMatrix = (int)pos / ROW_COL_NUM;
            int colInMatrix = (int)pos % ROW_COL_NUM;
            this.tttMatrix[rowInMatrix][colInMatrix] = user;

            // Judge row
            char ch;
            bool hasWinner;
            for (int i = 0; i < ROW_COL_NUM && winFlag == WinFlag.None; i++)
            {
                ch = this.tttMatrix[i][0];
                hasWinner = true;
                for (int j = 1; j < ROW_COL_NUM; j++)
                {
                    if (!this.tttMatrix[i][j].Equals(ch))
                    {
                        hasWinner = false;
                        break;
                    }
                }
                if (hasWinner && ch != EMPTY)
                {
                    winFlag = ch == USER_O ? WinFlag.User_O_Win : WinFlag.User_X_Win;
                    AnsiConsole.MarkupLine("Judge row", winFlag);
                }
            }

            // Judge col
            for (int j = 0; j < ROW_COL_NUM && winFlag == WinFlag.None; j++)
            {
                ch = this.tttMatrix[0][j];
                hasWinner = true;
                for (int i = 1; i < ROW_COL_NUM; i++)
                {
                    if (!this.tttMatrix[i][j].Equals(ch))
                    {
                        hasWinner = false;
                        break;
                    }
                }
                if (hasWinner && ch != EMPTY)
                {
                    winFlag = ch == USER_O ? WinFlag.User_O_Win : WinFlag.User_X_Win;
                    AnsiConsole.MarkupLine("Judge col", winFlag);
                }
            }

            // Judge across
            ch = this.tttMatrix[0][0];
            hasWinner = true;
            for (int i = 1; i <  ROW_COL_NUM && winFlag == WinFlag.None; i++)
            {
                for (int j = 1; j < ROW_COL_NUM; j++)
                {
                    if(i == j && !this.tttMatrix[i][j].Equals(ch))
                    {
                        hasWinner = false;
                        break;
                    }
                }
                if (!hasWinner) break;
            }
            if (hasWinner && ch != EMPTY)
            {
                winFlag = ch == USER_O ? WinFlag.User_O_Win : WinFlag.User_X_Win;
                AnsiConsole.MarkupLine("Judge across", winFlag);
            }

            // Judge diagonally across
            ch = this.tttMatrix[0][ROW_COL_NUM - 1];
            hasWinner = true;
            for (int i = 0; i < ROW_COL_NUM && winFlag == WinFlag.None; i++)
            {
                for (int j = ROW_COL_NUM - 2; j >= 0; j--)
                {
                    if ((i + j) == ROW_COL_NUM - 1 && !this.tttMatrix[i][j].Equals(ch))
                    {
                        hasWinner = false;
                        break;
                    }
                }
                if (!hasWinner) break;
            }
            if (hasWinner && ch != EMPTY)
            {
                winFlag = ch == USER_O ? WinFlag.User_O_Win : WinFlag.User_X_Win;
                AnsiConsole.MarkupLine("Judge diagonally across", winFlag);
            }

            // Judge if no one wins
            bool noWinner = true;
            for(int i = 0; i < ROW_COL_NUM; i++)
            {
                for(int j = 0; j < ROW_COL_NUM; j++)
                {
                    if (this.tttMatrix[i][j] == EMPTY)
                    {
                        noWinner = false;
                        break;
                    }
                }
                if (!noWinner)
                {
                    break;
                }
            }
            if (noWinner) 
            {
                winFlag = WinFlag.No_One_Win;
            }

            AnsiConsole.Clear();
            isOTurn = !isOTurn;
        }

        PrintTTTMaxtrix();
        switch (winFlag)
        {
            case WinFlag.No_One_Win:
                AnsiConsole.MarkupLine("No one wins!");
                break;
            case WinFlag.User_X_Win:
                AnsiConsole.MarkupLine("User X wins!");
                break;
            case WinFlag.User_O_Win:
                AnsiConsole.MarkupLine("User O wins!");
                break;
        }
    }
}


public static class Entrance
{
    public static void Main()
    {
        TicTacToe ticTacToe = new TicTacToe();
        ticTacToe.Run();
    }
}