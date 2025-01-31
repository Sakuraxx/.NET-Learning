namespace OOP.Polymorphism;

public class NorthCommand : RobotCommand
{
    public override void Run(Robot robot)
    {
        robot.Y += 1;
    }
}
