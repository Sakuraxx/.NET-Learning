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

    private readonly Point fountainRoomPosition = new Point(0, 2);

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
        if(playPosition == this.fountainRoomPosition)
        {
            actionOptions.Append(ActionOption.EnableFountain);
        }
        ActionOption option = AnsiConsole.Prompt(
                                new SelectionPrompt<ActionOption>()
                                .Title("What do you want to do?")
                                .AddChoices(actionOptions));
        
        PlayAction action = null;
        switch(option)
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
        }
        return action;
    }

    public void Run()
    {
        Player player = new Player(0, ROW_NUM, 0, COL_NUM);

        player.Action = this.AskPlayerToChooseNextAction(player.Position);
        player.Play();
        AnsiConsole.MarkupLine($"You are in the room at (Row={player.Position.X}, Column={player.Position.Y}).");
    }
}
