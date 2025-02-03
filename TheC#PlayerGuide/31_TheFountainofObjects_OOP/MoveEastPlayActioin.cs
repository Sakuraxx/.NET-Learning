namespace OOP.TheFountainofObjects;

public class MoveEastPlayActioin : PlayAction
{
    public override void Execute(Player player)
    {
        player.MoveEast();
    }
}
