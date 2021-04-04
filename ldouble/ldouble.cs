namespace ExtraTypes
{
    [System.Serializable]
    public class LDouble
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
        public static LDouble operator +(LDouble num1, double num2)
        {
            return Addition(num1, num2);
        }
        public static LDouble operator +(LDouble num1, LDouble num2)
        {
            return Addition(num1, num2);
        }
        public static LDouble operator -(LDouble num1, double num2)
        {
            double current = num1.Current - num2;
            double limit = num1.Limit;
            while (current < 0)
            {
                if (num1.IncreasableAmount != 0)
                {
                    num1.DecreaseLimit(ref limit);
                }
                current += limit;
            }
            return new LDouble(num1) { Current = current, Limit = limit };
        }
        public static LDouble operator -(LDouble num1, LDouble num2)
        {
            double current = num1.Current - num2.Current;
            double limit = num1.Limit;
            while (current < 0)
            {
                if (num1.IncreasableAmount != 0)
                {
                    num1.DecreaseLimit(ref limit);
                }
                current += limit;
            }
            return new LDouble(num1) { Current = current, Limit = limit };
        }
        public static LDouble operator *(LDouble num1, double num2)
        {
            if ((long)(num1.Current * num2) >= num1.Limit)
            {
                double limit = num1.Limit;
                double current = num1.Current * num2 - limit;
                if (!num1.AllowToOverFlow)
                {
                    return new LDouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                else
                {
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    while (current >= limit)
                    {
                        current -= limit;
                        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    }
                    return new LDouble(num1) { Current = current, Limit = limit };
                }
            }
            else
            {
                return new LDouble(num1) { Current = (num1.Current * num2) };
            }
        }
        public static LDouble operator *(LDouble num1, LDouble num2)
        {
            if (num1.Current * num2.Current >= num1.Limit)
            {
                double limit = num1.Limit;
                double current = num1.Current * num2.Current - limit;
                if (!num1.AllowToOverFlow)
                {
                    return new LDouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                else
                {
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    while (current >= limit)
                    {
                        current -= limit;
                        if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                    }
                    return new LDouble(num1) { Current = current, Limit = limit };
                }
            }
            else
            {
                return new LDouble(num1) { Current = num1.Current * num2.Current };
            }
        }
        public static LDouble operator /(LDouble num1, double num2)
        {
            return new LDouble(num1) { Current = num1.Current / num2 };
        }
        public static LDouble operator /(LDouble num1, LDouble num2)
        {
            return new LDouble(num1) { Current = num1.Current / num2.Current };
        }
        public static LDouble operator ++(LDouble num1)
        {
            return new LDouble(num1 + 1);
        }
        public static LDouble operator --(LDouble num1)
        {
            return new LDouble(num1 - 1);
        }
        public static bool operator >(LDouble num1, double num2)
        {
            return num1.Current > num2;
        }
        public static bool operator <(LDouble num1, double num2)
        {
            return num1.Current < num2;
        }
        public static bool operator >(LDouble num1, LDouble num2)
        {
            return num1.Current > num2.Current;
        }
        public static bool operator <(LDouble num1, LDouble num2)
        {
            return num1.Current < num2.Current;
        }
        public static bool operator >=(LDouble num1, double num2)
        {
            return num1.Current >= num2;
        }
        public static bool operator <=(LDouble num1, double num2)
        {
            return num1.Current <= num2;
        }
        public static bool operator >=(LDouble num1, LDouble num2)
        {
            return num1.Current >= num2.Current;
        }
        public static bool operator <=(LDouble num1, LDouble num2)
        {
            return num1.Current <= num2.Current;
        }
        /// <summary>
        /// Returns a string representing the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Current.ToString();
        }
        /// <summary>
        /// Instead of operator "+"
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <returns></returns>
        private static LDouble Addition(LDouble num1, double num2)
        {
            if (num1.Current + num2 >= num1.Limit)
            {
                if (!num1.AllowToOverFlow)
                {
                    return new LDouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                double limit = num1.Limit;
                double current = num1.Current + num2 - limit;
                if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                while (current >= limit)
                {
                    current -= limit;
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                }
                return new LDouble(num1) { Current = current, Limit = limit };
            }
            else
            {
                return new LDouble(num1) { Current = num1.Current + num2 };
            }
        }
        /// <summary>
        /// Instead of operator "+"
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <returns></returns>
        private static LDouble Addition(LDouble num1, LDouble num2)
        {
            if (num1.Current + num2.Current >= num1.Limit)
            {
                if (!num1.AllowToOverFlow)
                {
                    return new LDouble(num1) { Current = num1.Limit, Limit = num1.Limit };
                }
                double limit = num1.Limit;
                double current = num1.Current + num2.Current - limit;
                if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                while (current >= limit)
                {
                    current -= limit;
                    if (num1.IncreasableAmount != 0) num1.IncreaseLimit(ref limit);
                }
                return new LDouble(num1) { Current = current, Limit = limit };
            }
            else
            {
                return new LDouble(num1) { Current = num1.Current + num2.Current };
            }
        }
        /// <summary>
        /// increase value with return, where "true" is overflow
        /// </summary>
        /// <param name="num">Value</param>
        /// <returns></returns>
        public bool AddValue(double num)
        {
            if (IsOverflow(num))
            {
                LDouble temp = Addition(this, num);
                Limit = temp.Limit;
                Current = temp.Current;
                return true;
            }
            else
            {
                Current += num;
                return false;
            }
        }
        /// <summary>
        /// increase value with return, where "true" is overflow
        /// </summary>
        /// <param name="num">Value</param>
        /// <returns></returns>
        public bool AddValue(LDouble num)
        {
            if (IsOverflow(num))
            {
                LDouble temp = Addition(this, num);
                Limit = temp.Limit;
                Current = temp.Current;
                return true;
            }
            else
            {
                Current += num.Current;
                return false;
            }
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
                toReturn *= IncreasableAmount;
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
                toReturn *= IncreasableAmount;
            }
            return toReturn;
        }
        ///<summary>
        /// Decrease Limit taking into account IsMultiplicator
        ///</summary>
        public void DecreaseLimit(ref double toReturn)
        {
            if (!IsMultiplicator)
            {
                toReturn -= IncreasableAmount;
            }
            else
            {
                toReturn /= IncreasableAmount;
            }
        }
        ///<summary>
        /// Decrease Limit taking into account IsMultiplicator
        ///</summary>
        public double DecreaseLimit(double toReturn)
        {
            if (!IsMultiplicator)
            {
                toReturn -= IncreasableAmount;
            }
            else
            {
                toReturn /= IncreasableAmount;
            }
            return toReturn;
        }
        /// <summary>
        /// Returns bool value of overflowing
        /// </summary>
        /// <param name="num">Value</param>
        /// <returns></returns>
        public bool IsOverflow(double num)
        {
            if (Current + num >= Limit) return true;
            return false;
        }
        /// <summary>
        /// Returns bool value of overflowing
        /// </summary>
        /// <param name="num">Value</param>
        /// <returns></returns>
        public bool IsOverflow(LDouble num)
        {
            if (Current + num.Current >= Limit) return true;
            return false;
        }
        /// <summary>
        /// Returns the amount of overflowing the limit
        /// </summary>
        /// <param name="num2">Value</param>
        /// <returns></returns>
        public double AmountOfOverFlow(double num2)
        {
            double current = Current + num2;
            double toReturn = 0;
            double limit = Limit == 0 ? 1 : Limit;
            if (current >= limit && !AllowToOverFlow) return 1;
            if (IncreasableAmount != 0)
            {
                while (current >= limit)
                {
                    toReturn++;
                    current -= limit;
                    IncreaseLimit(ref limit);
                }
            }
            else
            {
                while (current >= limit)
                {
                    toReturn++;
                    current -= limit;
                }
            }
            return toReturn;
        }
        /// <summary>
        /// Returns the amount of overflowing the limit
        /// </summary>
        /// <param name="num2">Value</param>
        /// <returns></returns>
        public double AmountOfOverFlow(LDouble num2)
        {
            double current = Current + num2.Current;
            double toReturn = 0;
            double limit = Limit == 0 ? 1 : Limit;
            if (current >= limit && !AllowToOverFlow) return 1;
            if (IncreasableAmount != 0)
            {
                while (current >= limit)
                {
                    toReturn++;
                    current -= limit;
                    IncreaseLimit(ref limit);
                }
            }
            else
            {
                while (current >= limit)
                {
                    toReturn++;
                    current -= limit;
                }
            }
            return toReturn;
        }
        /// <summary>
        /// Returns bool value of deficit
        /// </summary>
        /// <param name="num2">Value</param>
        /// <returns></returns>
        public bool IsDeficit(double num2)
        {
            if (Current - num2 < 0) return true;
            return false;
        }
        /// <summary>
        /// Returns bool value of deficit
        /// </summary>
        /// <param name="num2">Value</param>
        /// <returns></returns>
        public bool IsDeficit(LDouble num2)
        {
            if (Current - num2.Current < 0) return true;
            return false;
        }
        /// <summary>
        /// Returns the amount of deficit
        /// </summary>
        /// <param name="num2">Value</param>
        public double AmountOfDeficit(double num2)
        {
            double current = Current - num2;
            double toReturn = 0;
            double limit = Limit == 0 ? 1 : Limit;
            if (IncreasableAmount != 0)
            {
                while (current < 0)
                {
                    toReturn++;
                    DecreaseLimit(ref limit);
                    current += limit;
                }
            }
            else
            {
                while (current < 0)
                {
                    toReturn++;
                    current += limit;
                }
            }
            return toReturn;
        }
        /// <summary>
        /// Returns the amount of deficit
        /// </summary>
        /// <param name="num2">Value</param>
        public double AmountOfDeficit(LDouble num2)
        {
            double current = Current - num2.Current;
            double toReturn = 0;
            double limit = Limit == 0 ? 1 : Limit;
            if (IncreasableAmount != 0)
            {
                while (current < 0)
                {
                    toReturn++;
                    DecreaseLimit(ref limit);
                    current += limit;
                }
            }
            else
            {
                while (current < 0)
                {
                    toReturn++;
                    current += limit;
                }
            }
            return toReturn;
        }
        /// <summary>
        /// Empty constructor
        /// </summary>
        public LDouble()
        {
            Limit = 10;
            Current = 0;
            IsMultiplicator = false;
            IncreasableAmount = 0;
            AllowToOverFlow = true;
        }
        /// <summary>
        /// Standart constructor
        /// </summary>
        /// <param name="current">Current</param>
        /// <param name="limit">Limit</param>
        public LDouble(double current, double limit)
        {
            Limit = limit;
            Current = current;
            IsMultiplicator = false;
            IncreasableAmount = 0;
            AllowToOverFlow = true;
        }
        /// <summary>
        /// Extended constructor
        /// </summary>
        /// <param name="current">Current</param>
        /// <param name="limit">Limit</param>
        /// <param name="isMultiplicator">IsMultiplicator</param>
        /// <param name="amount">IncreasableAmount, 0 means don't increase limit</param>
        /// <param name="isOverflow">IsOverflow</param>
        public LDouble(double current, double limit, bool isMultiplicator,
            double amount, bool isOverflow)
        {
            Limit = limit;
            Current = current;
            IsMultiplicator = isMultiplicator;
            IncreasableAmount = amount;
            AllowToOverFlow = isOverflow;
        }
        /// <summary>
        /// Standart constructor
        /// </summary>
        /// <param name="example">LDouble value</param>
        public LDouble(LDouble example)
        {
            Limit = example.Limit;
            Current = example.Current;
            IsMultiplicator = example.IsMultiplicator;
            IncreasableAmount = example.IncreasableAmount;
            AllowToOverFlow = example.AllowToOverFlow;
        }
    }
}