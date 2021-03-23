﻿namespace ExtraTypes
{
    [System.Serializable]
    public struct LDouble
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
        public static LDouble operator +(LDouble num1, LDouble num2)
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
        public static bool operator >(LDouble num1, LDouble num2)
        {
            return num1.Current > num2.Current;
        }
        public static bool operator <(LDouble num1, LDouble num2)
        {
            return num1.Current < num2.Current;
        }
        public static bool operator >=(LDouble num1, LDouble num2)
        {
            return num1.Current >= num2.Current;
        }
        public static bool operator <=(LDouble num1, LDouble num2)
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
        /// Returns the amount of overflowing the limit
        /// </summary>
        public double AmountOfOverFlow(double num2)
        {
            double current = Current + num2;
            double toReturn = 0;
            double limit = Limit;
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
        public double AmountOfOverFlow(LDouble num2)
        {
            double current = Current + num2.Current;
            double toReturn = 0;
            double limit = Limit;
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
        public double AmountOfDeficit(double num2)
        {
            double current = Current - num2;
            double toReturn = 0;
            double limit = Limit;
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
        public double AmountOfDeficit(LDouble num2)
        {
            double current = Current - num2.Current;
            double toReturn = 0;
            double limit = Limit;
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
        public LDouble(double current, double limit) : this()
        {
            Limit = limit;
            Current = current;
            IsMultiplicator = false;
            IncreasableAmount = 0;
            AllowToOverFlow = true;
        }
        public LDouble(double current, double limit, bool isMultiplicator,
            double amount, bool isOverflow) : this()
        {
            Limit = limit;
            Current = current;
            IsMultiplicator = isMultiplicator;
            IncreasableAmount = amount;
            AllowToOverFlow = isOverflow;
        }
        public LDouble(LDouble example) : this()
        {
            Limit = example.Limit;
            Current = example.Current;
            IsMultiplicator = example.IsMultiplicator;
            IncreasableAmount = example.IncreasableAmount;
            AllowToOverFlow = example.AllowToOverFlow;
        }
    }
}