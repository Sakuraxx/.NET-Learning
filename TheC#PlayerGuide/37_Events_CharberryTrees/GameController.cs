namespace AdvancedTopics.Events;

public class GameController
{
    public void Run()
    {
        CharberryTree tree = new CharberryTree();
        Notifer notifer = new Notifer(tree);
        Harvester harvester = new Harvester(tree);
        while (true)
            tree.MaybeGrow();
    }
}
