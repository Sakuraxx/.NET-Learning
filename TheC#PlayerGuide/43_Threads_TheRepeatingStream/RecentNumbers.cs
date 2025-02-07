namespace AdvancedTopic.Threads;

public class RecentNumbers
{
    private readonly object _numberLock = new object();

    private List<int> _numbers;
    public List<int> Numbers 
    {
        get
        {
            lock (_numberLock)
            {
                return this._numbers;
            }
        }
        set
        {
            lock (_numberLock)
            {
                this._numbers = value;
            }
        }
    }
}
