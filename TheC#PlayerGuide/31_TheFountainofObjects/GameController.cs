using Spectre.Console;

namespace OOP.TheFountainofObjects;

public enum ActionOption
{
    MoveNorth,
    MoveSouth,
    MoveWest,
    MoveEast,
    EnableFountain
}

public class GameController
{
    private static readonly int ROW_NUM = 4;
    private static readonly int COL_NUM = 4;

    private readonly Point fountainRoomPosition = new Point(2, 0);
    private readonly Point entranceRoomPostion = new Point(0, 0);

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
                    room = new FountainRoom();
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

    public PlayAction AskPlayerToChooseNextAction(Point playPosition)
    {
        ActionOption[] actionOptions = [ActionOption.MoveNorth, ActionOption.MoveSouth, ActionOption.MoveWest, ActionOption.MoveEast]; ;
        if (playPosition == this.fountainRoomPosition)
        {
            actionOptions = actionOptions.Append(ActionOption.EnableFountain).ToArray();
        }
        ActionOption option = AnsiConsole.Prompt(
                                new SelectionPrompt<ActionOption>()
                                .Title("What do you want to do?")
                                .AddChoices(actionOptions));

        PlayAction action = null;
        switch (option)
        {
            case ActionOption.MoveNorth:
                action = new MoveNorthPlayAction();
                break;
            case ActionOption.MoveSouth:
                action = new MoveSouthPlayAction();
                break;
            case ActionOption.MoveWest:
                action = new MoveWestPlayAction();
                break;
            case ActionOption.MoveEast:
                action = new MoveEastPlayActioin();
                break;
            case ActionOption.EnableFountain:
                action = new ActiveFountainRoomPlayAction();
                break;
        }
        return action;
    }

    public void Run()
    {
        Player player = new Player(0, ROW_NUM, 0, COL_NUM, this.rooms);

        do
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"You are in the room at (Row={player.Position.Y}, Column={player.Position.X}).");

            if (player.Position == this.entranceRoomPostion)
            {
                if (((FountainRoom)this.rooms[0][2]).IsActived)
                {
                    AnsiConsole.MarkupLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
                    break;
                }
                else
                {
                    AnsiConsole.MarkupLine("You see light coming from the cavern entrance.");
                }
            }
            else if(player.Position == this.fountainRoomPosition)
            {
                if (((FountainRoom)this.rooms[0][2]).IsActived)
                {
                    AnsiConsole.MarkupLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
                }
                else
                {
                    AnsiConsole.MarkupLine("You hear water dripping in this room. The Fountain of Objects is here!");
                }
            }

            player.Action = this.AskPlayerToChooseNextAction(player.Position);
            player.Play();
        }while(true);
    }
}
