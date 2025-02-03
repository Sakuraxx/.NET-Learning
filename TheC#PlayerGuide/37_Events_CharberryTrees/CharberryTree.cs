namespace AdvancedTopics.Events;

public class CharberryTree
{
    private Random _random = new Random();
    public bool Ripe { get; set; }
    public void MaybeGrow()
    {
        // Only a tiny chance of ripening each time, but we try a lot!
        if (_random.NextDouble() < 0.00000001 && !Ripe)
        {
            Ripe = true;
        }
    }
}
