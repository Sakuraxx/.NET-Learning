using Spectre.Console;

namespace AdvancedTopics.Events;

public class Harvester
{
    public Harvester(CharberryTree tree)
    {
        tree.Ripened += this.OnRipened;
    }

    public void OnRipened(object sender, CharberryTreeRipenEventArgs args)
    {
        if (sender is CharberryTree tree) 
        {
            tree.Ripe = false;
            AnsiConsole.MarkupLine("Harvest is done!");
        }
    }
}
