public abstract class CurrencyHolder
{
    public int Value { get; protected set; }

    public abstract void Load();
    public abstract void Save();

    public void Add(int value)
    {
        if (value > 0)
            Value += value;
    }

    public bool TryRemove(int value)
    {
        if (Value - value >= 0)
        {
            Value -= value;
            return true;
        }

        return false;
    }
}
