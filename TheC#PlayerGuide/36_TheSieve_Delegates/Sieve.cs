namespace AdvancedTopics.Delegates;

public class Sieve
{
    private Func<int, bool> doGoodJudge;

    public Sieve(Func<int, bool> func)
    {
        this.doGoodJudge = func;
    }

    public bool IsGood(int number)
    {
        return this.doGoodJudge.Invoke(number);
    }
}
