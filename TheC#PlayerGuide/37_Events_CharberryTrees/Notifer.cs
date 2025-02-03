using Spectre.Console;

namespace AdvancedTopics.Events;

public class Notifer
{
    public Notifer(CharberryTree tree)
    {
        tree.Ripened += this.OnRipened;
    }

    private void OnRipened(object sender, CharberryTreeRipenEventArgs args)
    {
        AnsiConsole.MarkupLine($"A charberry fruit has ripened! {args.RipenTime.ToString()}");
    }
}
