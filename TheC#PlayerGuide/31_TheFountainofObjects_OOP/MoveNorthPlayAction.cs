namespace OOP.TheFountainofObjects;

public class MoveNorthPlayAction : PlayAction
{
    public override void Execute(Player player)
    {
        player.MoveNorth();
    }
}
