namespace ExtraTypes
{
    [System.Serializable]
    public struct ldouble
    {
        ///<summary>
        /// Current value
        ///</summary>
        public double Current;
        ///<summary>
        /// Max value
        ///</summary>
        public double Limit;
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
        public static ldouble operator +(ldouble num1, double num2)
        {
            if (num1.Current + num2 >= num1.Limit)
            {
                if (!num1.AllowToOverFlow)
                {
                    return new ldouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                double limit = num1.Limit;
                double current = num1.Current + num2 - limit;
                if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                while (current >= limit)
                {
                    current -= limit;
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                }
                return new ldouble(num1) { Current = current, Limit = limit };
            }
            else
            {
                return new ldouble(num1) { Current = num1.Current + num2 };
            }
        }
        public static ldouble operator +(ldouble num1, ldouble num2)
        {
            if (num1.Current + num2.Current >= num1.Limit)
            {
                if (!num1.AllowToOverFlow)
                {
                    return new ldouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                double limit = num1.Limit;
                double current = num1.Current + num2.Current - limit;
                if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                while (current >= limit)
                {
                    current -= limit;
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                }
                return new ldouble(num1) { Current = current, Limit = limit };
            }
            else
            {
                return new ldouble(num1) { Current = num1.Current + num2.Current };
            }
        }
        public static ldouble operator -(ldouble num1, double num2)
        {
            return new ldouble(num1) { Current = num1.Current - num2 };
        }
        public static ldouble operator -(ldouble num1, ldouble num2)
        {
            return new ldouble(num1) { Current = num1.Current - num2.Current };
        }
        /*public static ldouble operator *(ldouble num1, long num2)
        {
            if (num1.Current * num2 >= num1.Limit)
            {
                long limit = num1.Limit;
                long current = num1.Current * num2 - limit;
                if (!num1.AllowToOverFlow)
                {
                    return new ldouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                else
                {
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    while (current >= limit)
                    {
                        current -= limit;
                        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    }
                    return new ldouble(num1) { Current = current, Limit = limit };
                }
            }
            else
            {
                return new ldouble(num1) { Current = num1.Current * num2 };
            }
        }*/
        public static ldouble operator *(ldouble num1, double num2)
        {
            if ((long)(num1.Current * num2) >= num1.Limit)
            {
                double limit = num1.Limit;
                double current = num1.Current * num2 - limit;
                if (!num1.AllowToOverFlow)
                {
                    return new ldouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                else
                {
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    while (current >= limit)
                    {
                        current -= limit;
                        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    }
                    return new ldouble(num1) { Current = current, Limit = limit };
                }
            }
            else
            {
                return new ldouble(num1) { Current = (num1.Current * num2) };
            }
        }
        public static ldouble operator *(ldouble num1, ldouble num2)
        {
            if (num1.Current * num2.Current >= num1.Limit)
            {
                double limit = num1.Limit;
                double current = num1.Current * num2.Current - limit;
                if (!num1.AllowToOverFlow)
                {
                    return new ldouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                else
                {
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    while (current >= limit)
                    {
                        current -= limit;
                        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    }
                    return new ldouble(num1) { Current = current, Limit = limit };
                }
            }
            else
            {
                return new ldouble(num1) { Current = num1.Current * num2.Current };
            }
        }
        public static ldouble operator /(ldouble num1, double num2)
        {
            return new ldouble(num1) { Current = num1.Current / num2 };
        }
        /*public static ldouble operator /(ldouble num1, long num2)
        {
            return new ldouble(num1) { Current = num1.Current / num2 };
        }*/
        public static ldouble operator /(ldouble num1, ldouble num2)
        {
            return new ldouble(num1) { Current = num1.Current / num2.Current };
        }
        public static ldouble operator ++(ldouble num1)
        {
            return new ldouble(num1 + 1);
        }
        public static ldouble operator --(ldouble num1)
        {
            return new ldouble(num1 - 1);
        }
        /*public static bool operator >(ldouble num1, long num2)
        {
            return num1.Current > num2;
        }
        public static bool operator <(ldouble num1, long num2)
        {
            return num1.Current < num2;
        }*/
        public static bool operator >(ldouble num1, ldouble num2)
        {
            return num1.Current > num2.Current;
        }
        public static bool operator <(ldouble num1, ldouble num2)
        {
            return num1.Current < num2.Current;
        }
        /*public static bool operator >=(ldouble num1, long num2)
        {
            return num1.Current >= num2;
        }
        public static bool operator <=(ldouble num1, long num2)
        {
            return num1.Current <= num2;
        }*/
        public static bool operator >=(ldouble num1, ldouble num2)
        {
            return num1.Current >= num2.Current;
        }
        public static bool operator <=(ldouble num1, ldouble num2)
        {
            return num1.Current <= num2.Current;
        }
        ///<summary>
        /// Increase Limit taking into account IsMultiplicator
        ///</summary>
        public void IncreaseLimit(ref double toReturn)
        {
            if (!IsMultiplicator)
            {
                toReturn += IncreasableAmount;
            }
            else
            {
                toReturn = toReturn * IncreasableAmount;
            }
        }
        ///<summary>
        /// Increase Limit taking into account IsMultiplicator
        ///</summary>
        public double IncreaseLimit(double toReturn)
        {
            if (!IsMultiplicator)
            {
                toReturn += IncreasableAmount;
            }
            else
            {
                toReturn = toReturn * IncreasableAmount;
            }
            return toReturn;
        }
        /// <summary>
        /// Returns the amount of overflowing the limit
        /// </summary>
        public double AmountOfOverFlow(ldouble num1, double num2)
        {
            double current = num1.Current + num2;
            double toReturn = 0;
            double limit = num1.Limit;
            while (current >= limit)
            {
                toReturn++;
                current -= limit;
                if (num1.IncreasableAmount != 0 && num1.AllowToOverFlow)
                    num1.IncreaseLimit(ref limit);
            }
            return toReturn;
        }
        /// <summary>
        /// Returns the amount of overflowing the limit
        /// </summary>
        public double AmountOfOverFlow(ldouble num1, ldouble num2)
        {
            double current = num1.Current + num2.Current;
            double toReturn = 0;
            double limit = num1.Limit;
            while (current >= limit)
            {
                toReturn++;
                current -= limit;
                if (num1.IncreasableAmount != 0 && num1.AllowToOverFlow)
                    num1.IncreaseLimit(ref limit);
            }
            return toReturn;
        }
        public ldouble(double current, double limit) : this()
        {
            Limit = limit;
            Current = current;
            IsMultiplicator = false;
            IncreasableAmount = 0;
            AllowToOverFlow = true;
        }
        public ldouble(double current, double limit, bool isMultiplicator,
            double amount, bool isOverflow) : this()
        {
            Limit = limit;
            Current = current;
            IsMultiplicator = isMultiplicator;
            IncreasableAmount = amount;
            AllowToOverFlow = isOverflow;
        }
        public ldouble(ldouble example) : this()
        {
            Limit = example.Limit;
            Current = example.Current;
            IsMultiplicator = example.IsMultiplicator;
            IncreasableAmount = example.IncreasableAmount;
            AllowToOverFlow = example.AllowToOverFlow;
        }
    }
}