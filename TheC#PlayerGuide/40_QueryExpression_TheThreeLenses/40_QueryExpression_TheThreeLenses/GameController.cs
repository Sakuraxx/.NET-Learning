namespace AdvancedTopic.QueryExpression;
public class GameController
{
    public IEnumerable<int> NormalThreelens(int[] nums)
    {
        List<int> res = new List<int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] % 2 == 0)
            {
                res.Add(nums[i]);
            }
        }

        res.Sort();

        for (int i = 0; i < res.Count; i++)
        {
            res[i] *= 2;
        }

        return res;
    }


    public IEnumerable<int> KeyWordvBasedQueryThreeLens(int[] nums)
    {
        var res = from num in nums
                  where num % 2 == 0
                  orderby num
                  select num * 2;
        return res;
    }


    public void Run()
    {
        int[] nums = { 1, 9, 2, 8, 3, 7, 4, 6, 5 };
        IEnumerable<int> res = this.NormalThreelens(nums);
        res.ToList().ForEach(n => Console.Write(n + " "));
    }


    public static void Main(string[] args)
    {
        GameController controller = new GameController();
        controller.Run();
    }
}
