using Spectre.Console;

namespace OOP.TheFountainofObjects;

public class GameController
{
    private static readonly int ROW_NUM = 4;
    private static readonly int COL_NUM = 4;

    private Room[][] rooms = new Room[ROW_NUM][];

    private void InitRooms()
    {
        for (int i = 0; i < ROW_NUM; i++)
        {
            rooms[i] = new Room[COL_NUM];
            for (int j = 0; j < COL_NUM; j++)
            {
                Room room = new Room();
                if (i == 0 && j == 0)
                {
                    room.Type = RoomType.Entrance;
                }
                else if (i == 0 && j == 2)
                {
                    room.Type = RoomType.Fountain;
                }
                this.rooms[i][j] = room;
            }
        }
    }

    public GameController()
    {
        this.InitRooms();
    }

    public void Run()
    {
        Player player = new Player(0, ROW_NUM, 0, COL_NUM);
        
        player.Action = new MoveNorthPlayAction();
        player.Play();
        AnsiConsole.MarkupLine($"You are in the room at (Row={player.Position.X}, Column={player.Position.Y}).");

        player.Action = new MoveEastPlayActioin();
        player.Play();
        AnsiConsole.MarkupLine($"You are in the room at (Row={player.Position.X}, Column={player.Position.Y}).");

        player.Action = new MoveSouthPlayAction();
        player.Play();
        AnsiConsole.MarkupLine($"You are in the room at (Row={player.Position.X}, Column={player.Position.Y}).");

        player.Action = new MoveWestPlayAction();
        player.Play();
        AnsiConsole.MarkupLine($"You are in the room at (Row={player.Position.X}, Column={player.Position.Y}).");
    }
}
