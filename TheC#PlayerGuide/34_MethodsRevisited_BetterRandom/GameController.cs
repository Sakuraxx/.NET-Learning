namespace AdvancedTopic.MethodsRevisited;

public static class RandomExtensions
{
    public static double NextDouble(this Random random, int maxValue)
    {
        return random.NextDouble() * maxValue;
    }

    public static string NextString(this Random random, params string[] strs)
    {
        int len = strs.Length;
        int idx = random.Next(0, len);
        return strs[idx];
    }

    public static bool CoinFlip(this Random random, double probabilityOfHeads = 0.5)
    {
        double prob = random.NextDouble();
        return prob < probabilityOfHeads;
    }
}


public class GameController
{
    public static void Main(string[] args)
    {
        Random random = new Random();
        Console.WriteLine("Random doulbe between 0~20: " + random.NextDouble(20));
        Console.WriteLine("Random string between [apple, banana, watermelon]: "+ 
            random.NextString(new string[] { "apple", "banana", "watermelon"}));
        Console.WriteLine("Fair coin toss, head is " + random.CoinFlip());
        Console.WriteLine("Unfair coin toss (probabilityOfHeads=0.75), head is " + random.CoinFlip(0.75));
        Console.WriteLine("Unfair coin toss (probabilityOfHeads=0.25), head is " + random.CoinFlip(0.25));
    }
}
