namespace OOP.Polymorphism;

public class EastCommand : RobotCommand
{
    public override void Run(Robot robot)
    {
        robot.X += 1;
    }
}
