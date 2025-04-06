using Microsoft.CSharp.RuntimeBinder;

namespace AdvancedTopic.DynamicObjects;

public class GameController
{
    public static dynamic? DynamicAdd(dynamic a, dynamic b)
    {
        // The '+' operator will be resolved at runtime based on the actual types of 'a' and 'b'.
        // The C# runtime (DLR - Dynamic Language Runtime) will find the appropriate
        // operator overload for the specific types passed in.
        try
        {
            // Attempt the addition using runtime binding
            return a + b;
        }
        catch (RuntimeBinderException)
        {
            Console.WriteLine($"Warning: Could not add types {a?.GetType()} and {b?.GetType()}. Returning null."); // Optional logging
            return null;
        }
    }

    public static void Main(string[] args)
    {

    }

}
