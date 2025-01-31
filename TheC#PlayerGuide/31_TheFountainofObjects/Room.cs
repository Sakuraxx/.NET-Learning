namespace OOP.TheFountainofObjects;

public enum RoomType
{
    Entrance,
    Fountain,
    Empty,
}


public class Room
{
    public RoomType Type { get; set; } = RoomType.Empty;
}
