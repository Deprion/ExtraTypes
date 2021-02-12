[System.Serializable]
public struct llong
{
    ///<summary>
    /// Current value
    ///</summary>
    public long Current { get; private set; }
    ///<summary>
    /// Max value
    ///</summary>
    public long Limit { get; set; }
    ///<summary>
    /// Limit will be increase over time by multiplicator
    ///</summary>
    public bool IsMultiplicator { get; set; }
    ///<summary>
    /// Amount on which Limit will be increase
    ///</summary>
    public double IncreasableAmount { get; set; }
    public static llong operator +(llong num1, long num2)
    {
        if (num1.Current + num2 > num1.Limit)
        {
            long limit = num1.Limit;
            long current = num1.Current + num2 - limit;
            if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
            while (current >= limit)
            {
                current = num1.Current + num2 - limit;
                if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
            }
            return new llong(num1) { Current = current, Limit = limit };
        }
        else
        {
            return new llong { Current = num1.Current + num2 };
        }
    }
    public static llong operator +(llong num1, llong num2)
    {
        if (num1.Current + num2.Current > num1.Limit)
        {
            long limit = num1.Limit;
            long current = num1.Current + num2.Current - limit;
            if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
            while (current >= limit)
            {
                current = num1.Current + num2.Current - limit;
                if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
            }
            return new llong(num1) { Current = current, Limit = limit };
        }
        else
        {
            return new llong { Current = num1.Current + num2.Current };
        }
    }
    public static llong operator -(llong num1, long num2)
    {
        return new llong(num1) { Current = num1.Current - num2 };
    }
    public static llong operator -(llong num1, llong num2)
    {
        return new llong(num1) { Current = num1.Current - num2.Current };
    }
    public static llong operator *(llong num1, long num2)
    {
        long limit = num1.Limit;
        long current = num1.Current * num2 - limit;
        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
        while (current >= limit)
        {
            current = num1.Current * num2 - limit;
            if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
        }
        return new llong(num1) { Current = current, Limit = limit };
    }
    public static llong operator *(llong num1, double num2)
    {
        long limit = num1.Limit;
        long current = (long)(num1.Current * num2) - limit;
        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
        while (current >= limit)
        {
            current = (long)(num1.Current * num2) - limit;
            if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
        }
        return new llong(num1) { Current = current, Limit = limit };
    }
    public static llong operator *(llong num1, llong num2)
    {
        long limit = num1.Limit;
        long current = num1.Current * num2.Current - limit;
        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
        while (current >= limit)
        {
            current = num1.Current * num2.Current - limit;
            if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
        }
        return new llong(num1) { Current = current, Limit = limit };
    }
    public static llong operator /(llong num1, double num2)
    {
        return new llong(num1) { Current = (long)(num1.Current * num2) };
    }
    public static llong operator /(llong num1, long num2)
    {
        return new llong(num1) { Current = num1.Current * num2 };
    }
    public static llong operator /(llong num1, llong num2)
    {
        return new llong(num1) { Current = num1.Current * num2.Current };
    }
    ///<summary>
    /// Increase Limit taking into account IsMultiplicator
    ///</summary>
    public long IncreaseLimit(ref long toReturn)
    {
        if (!IsMultiplicator)
        {
            toReturn += (long)IncreasableAmount;
        }
        else
        {
            toReturn = (long)(toReturn * IncreasableAmount);
        }
        return toReturn;
    }
    ///<summary>
    /// Increase Limit taking into account IsMultiplicator
    ///</summary>
    public long IncreaseLimit(long toReturn)
    {
        if (!IsMultiplicator)
        {
            toReturn += (long)IncreasableAmount;
        }
        else
        {
            toReturn = (long)(toReturn * IncreasableAmount);
        }
        return toReturn;
    }
    /// <summary>
    /// Returns the amount of overflowing the limit
    /// </summary>
    public long AmountOfOverFlow(llong num1, long num2)
    {
        long current = num1.Current + num2;
        long toReturn = 0;
        long limit = num1.Limit;
        while (current > limit)
        {
            toReturn++;
            current = current - limit;
            if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
        }
        return toReturn;
    }
    /// <summary>
    /// Returns the amount of overflowing the limit
    /// </summary>
    public long AmountOfOverFlow(llong num1, llong num2)
    {
        long current = num1.Current + num2.Current;
        long toReturn = 0;
        long limit = num1.Limit;
        while (current > limit)
        {
            toReturn++;
            current = current - limit;
            if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
        }
        return toReturn;
    }
    public llong(long current, long limit) : this()
    {
        Limit = limit;
        Current = current;
        IsMultiplicator = false;
        IncreasableAmount = 0;
    }
    public llong(long current, long limit, bool isMultiplicator, double amount) : this()
    {
        Limit = limit;
        Current = current;
        IsMultiplicator = isMultiplicator;
        IncreasableAmount = amount;
    }
    public llong(llong example) : this()
    {
        Limit = example.Limit;
        Current = example.Current;
        IsMultiplicator = example.IsMultiplicator;
        IncreasableAmount = example.IncreasableAmount;
    }
}