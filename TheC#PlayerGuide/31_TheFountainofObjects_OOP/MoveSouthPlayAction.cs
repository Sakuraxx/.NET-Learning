namespace OOP.TheFountainofObjects;

public class MoveSouthPlayAction : PlayAction
{
    public override void Execute(Player player)
    {
        player.MoveSouth();
    }
}
