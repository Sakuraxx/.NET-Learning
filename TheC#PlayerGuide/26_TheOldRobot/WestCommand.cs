namespace OOP.Polymorphism;

public class WestCommand : RobotCommand
{
    public override void Run(Robot robot)
    {
        robot.X -= 1;
    }
}
