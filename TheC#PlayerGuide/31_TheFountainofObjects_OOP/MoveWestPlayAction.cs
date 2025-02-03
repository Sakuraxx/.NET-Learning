namespace OOP.TheFountainofObjects;

public class MoveWestPlayAction : PlayAction
{
    public override void Execute(Player player)
    {
        player.MoveWest();
    }
}
