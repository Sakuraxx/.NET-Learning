namespace AdvancedTopics.Events;

public class GameController
{
    public void Run()
    {
        CharberryTree tree = new CharberryTree();
        while (true)
            tree.MaybeGrow();
    }
}
