namespace OOP.TheFountainofObjects;

public record Point
{
    public int X;
    public int Y;
}

public class Player
{
    private int XMinBound;
    private int XMaxBound;
    private int YMinBound;
    private int YMaxBound;

    public Player(int minX, int maxX, int minY, int maxY)
    {
        this.XMinBound = minX;
        this.XMaxBound = maxX;
        this.YMinBound = minY;
        this.YMaxBound = maxY;
    }

    public void MoveNorth()
    {
        if(this.Position.Y + 1 <= this.YMaxBound)
        {
            this.Position.Y += 1;
        }
    }

    public void MoveSouth()
    {
        if(this.Position.Y - 1 >= this.YMinBound)
        {
            this.Position.Y -= 1;
        }
    }

    public void MoveWest()
    {
        if(this.Position.X - 1 >= this.XMinBound)
        {
            this.Position.X -= 1;
        }
    }

    public void MoveEast()
    {
        if(this.Position.X + 1 <= this.XMaxBound)
        {
            this.Position.X += 1;
        }
    }

    public Point Position { get; set; } = new Point();

    public PlayAction Action { get; set; }

    public void Play()
    {
        Action?.Execute(this);
    }
}
