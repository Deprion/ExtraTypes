namespace ExtraTypes
{
    [System.Serializable]
    public struct LLong
    {
        ///<summary>
        /// Current value
        ///</summary>
        public long Current;
        ///<summary>
        /// Max value
        ///</summary>
        public long Limit;
        ///<summary>
        /// Limit will be increase by multiplication by IncreasableAmount
        ///</summary>
        public bool IsMultiplicator;
        ///<summary>
        /// Amount on which Limit will be increase
        ///</summary>
        public double IncreasableAmount;
        /// <summary>
        ///  Allow to lvl up, when limit reached
        /// </summary>
        public bool AllowToOverFlow;
        public static LLong operator +(LLong num1, long num2)
        {
            if (num1.Current + num2 >= num1.Limit)
            {
                if (!num1.AllowToOverFlow)
                {
                    return new LLong(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                long limit = num1.Limit;
                long current = num1.Current + num2 - limit;
                if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                while (current >= limit)
                {
                    current -= limit;
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                }
                return new LLong(num1) { Current = current, Limit = limit };
            }
            else
            {
                return new LLong(num1) { Current = num1.Current + num2 };
            }
        }
        public static LLong operator +(LLong num1, LLong num2)
        {
            if (num1.Current + num2.Current >= num1.Limit)
            {
                if (!num1.AllowToOverFlow)
                {
                    return new LLong(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                long limit = num1.Limit;
                long current = num1.Current + num2.Current - limit;
                if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                while (current >= limit)
                {
                    current -= limit;
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                }
                return new LLong(num1) { Current = current, Limit = limit };
            }
            else
            {
                return new LLong(num1) { Current = num1.Current + num2.Current };
            }
        }
        public static LLong operator -(LLong num1, long num2)
        {
            long current = num1.Current - num2;
            long limit = num1.Limit;
            while (current < 0)
            {
                if (num1.IncreasableAmount != 0)
                {
                    num1.DecreaseLimit(ref limit);
                }
                current += limit;
            }
            return new LLong(num1) { Current = current, Limit = limit };
        }
        public static LLong operator -(LLong num1, LLong num2)
        {
            long current = num1.Current - num2.Current;
            long limit = num1.Limit;
            while (current < 0)
            {
                if (num1.IncreasableAmount != 0)
                {
                    num1.DecreaseLimit(ref limit);
                }
                current += limit;
            }
            return new LLong(num1) { Current = current, Limit = limit };
        }
        public static LLong operator *(LLong num1, long num2)
        {
            if (num1.Current * num2 >= num1.Limit)
            {
                long limit = num1.Limit;
                long current = num1.Current * num2 - limit;
                if (!num1.AllowToOverFlow)
                {
                    return new LLong(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                else
                {
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    while (current >= limit)
                    {
                        current -= limit;
                        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    }
                    return new LLong(num1) { Current = current, Limit = limit };
                }
            }
            else
            {
                return new LLong(num1) { Current = num1.Current * num2 };
            }
        }
        public static LLong operator *(LLong num1, double num2)
        {
            if ((long)(num1.Current * num2) >= num1.Limit)
            {
                long limit = num1.Limit;
                long current = (long)(num1.Current * num2) - limit;
                if (!num1.AllowToOverFlow)
                {
                    return new LLong(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                else
                {
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    while (current >= limit)
                    {
                        current -= limit;
                        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    }
                    return new LLong(num1) { Current = current, Limit = limit };
                }
            }
            else
            {
                return new LLong(num1) { Current = (long)(num1.Current * num2) };
            }
        }
        public static LLong operator *(LLong num1, LLong num2)
        {
            if (num1.Current * num2.Current >= num1.Limit)
            {
                long limit = num1.Limit;
                long current = num1.Current * num2.Current - limit;
                if (!num1.AllowToOverFlow)
                {
                    return new LLong(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                else
                {
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    while (current >= limit)
                    {
                        current -= limit;
                        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    }
                    return new LLong(num1) { Current = current, Limit = limit };
                }
            }
            else
            {
                return new LLong(num1) { Current = num1.Current * num2.Current };
            }
        }
        public static LLong operator /(LLong num1, double num2)
        {
            return new LLong(num1) { Current = (long)(num1.Current / num2) };
        }
        public static LLong operator /(LLong num1, long num2)
        {
            return new LLong(num1) { Current = num1.Current / num2 };
        }
        public static LLong operator /(LLong num1, LLong num2)
        {
            return new LLong(num1) { Current = num1.Current / num2.Current };
        }
        public static LLong operator ++(LLong num1)
        {
            return new LLong(num1 + 1);
        }
        public static LLong operator --(LLong num1)
        {
            return new LLong(num1 - 1);
        }
        public static bool operator >(LLong num1, long num2)
        {
            return num1.Current > num2;
        }
        public static bool operator <(LLong num1, long num2)
        {
            return num1.Current < num2;
        }
        public static bool operator >(LLong num1, LLong num2)
        {
            return num1.Current > num2.Current;
        }
        public static bool operator <(LLong num1, LLong num2)
        {
            return num1.Current < num2.Current;
        }
        public static bool operator >=(LLong num1, long num2)
        {
            return num1.Current >= num2;
        }
        public static bool operator <=(LLong num1, long num2)
        {
            return num1.Current <= num2;
        }
        public static bool operator >=(LLong num1, LLong num2)
        {
            return num1.Current >= num2.Current;
        }
        public static bool operator <=(LLong num1, LLong num2)
        {
            return num1.Current <= num2.Current;
        }
        ///<summary>
        /// Increase Limit taking into account IsMultiplicator
        ///</summary>
        public void IncreaseLimit(ref long toReturn)
        {
            if (!IsMultiplicator)
            {
                toReturn += (long)IncreasableAmount;
            }
            else
            {
                toReturn = (long)(toReturn * IncreasableAmount);
            }
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
        ///<summary>
        /// Decrease Limit taking into account IsMultiplicator
        ///</summary>
        public void DecreaseLimit(ref long toReturn)
        {
            if (!IsMultiplicator)
            {
                toReturn -= (long)IncreasableAmount;
            }
            else
            {
                toReturn = (long)(toReturn / IncreasableAmount);
            }
        }
        ///<summary>
        /// Decrease Limit taking into account IsMultiplicator
        ///</summary>
        public long DecreaseLimit(long toReturn)
        {
            if (!IsMultiplicator)
            {
                toReturn -= (long)IncreasableAmount;
            }
            else
            {
                toReturn = (long)(toReturn / IncreasableAmount);
            }
            return toReturn;
        }
        /// <summary>
        /// Returns the amount of overflowing the limit
        /// </summary>
        public long AmountOfOverFlow(long num2)
        {
            long current = Current + num2;
            long toReturn = 0;
            long limit = Limit;
            while (current >= limit)
            {
                toReturn++;
                current -= limit;
                if (IncreasableAmount != 0 && AllowToOverFlow)
                    IncreaseLimit(ref limit);
            }
            return toReturn;
        }
        /// <summary>
        /// Returns the amount of overflowing the limit
        /// </summary>
        public long AmountOfOverFlow(LLong num2)
        {
            long current = Current + num2.Current;
            long toReturn = 0;
            long limit = Limit;
            while (current >= limit)
            {
                toReturn++;
                current -= limit;
                if (IncreasableAmount != 0 && AllowToOverFlow)
                    IncreaseLimit(ref limit);
            }
            return toReturn;
        }
        /// <summary>
        /// Returns the amount of deficit
        /// </summary>
        /// <param name="num2">Value</param>
        public long AmountOfDeficit(long num2)
        {
            long current = Current - num2;
            long toReturn = 0;
            long limit = Limit;
            while (current < 0)
            {
                toReturn++;
                if (IncreasableAmount != 0)
                {
                    DecreaseLimit(ref limit);
                }
                current += limit;
            }
            return toReturn;
        }
        /// <summary>
        /// Returns the amount of deficit
        /// </summary>
        /// <param name="num2">Value</param>
        public double AmountOfDeficit(LLong num2)
        {
            long current = Current - num2.Current;
            long toReturn = 0;
            long limit = Limit;
            while (current < 0)
            {
                toReturn++;
                if (IncreasableAmount != 0)
                {
                    DecreaseLimit(ref limit);
                }
                current += limit;
            }
            return toReturn;
        }
        public LLong(long current, long limit) : this()
        {
            Limit = limit;
            Current = current;
            IsMultiplicator = false;
            IncreasableAmount = 0;
            AllowToOverFlow = true;
        }
        public LLong(long current, long limit, bool isMultiplicator,
            double amount, bool isOverflow) : this()
        {
            Limit = limit;
            Current = current;
            IsMultiplicator = isMultiplicator;
            IncreasableAmount = amount;
            AllowToOverFlow = isOverflow;
        }
        public LLong(LLong example) : this()
        {
            Limit = example.Limit;
            Current = example.Current;
            IsMultiplicator = example.IsMultiplicator;
            IncreasableAmount = example.IncreasableAmount;
            AllowToOverFlow = example.AllowToOverFlow;
        }
    }
}