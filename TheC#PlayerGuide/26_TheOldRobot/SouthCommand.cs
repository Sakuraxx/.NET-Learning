namespace OOP.Polymorphism;

public class SouthCommand : RobotCommand
{
    public override void Run(Robot robot)
    {
        robot.Y -= 1;
    }
}
