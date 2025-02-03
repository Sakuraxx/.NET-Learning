namespace AdvancedTopics.Events;

public class CharberryTree
{
    private Random _random = new Random();
    public bool Ripe { get; set; }
    
    public event EventHandler<CharberryTreeRipenEventArgs>? Ripened;

    public void MaybeGrow()
    {
        // Only a tiny chance of ripening each time, but we try a lot!
        if (_random.NextDouble() < 0.00000001 && !Ripe)
        {
            Ripe = true;
            Ripened?.Invoke(this, new CharberryTreeRipenEventArgs(DateTime.Now));
        }
    }
}

public class CharberryTreeRipenEventArgs : EventArgs
{
    public DateTime RipenTime { get; set; }

    public CharberryTreeRipenEventArgs(DateTime dateTime) => RipenTime = dateTime;
}