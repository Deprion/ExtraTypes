namespace ExtraTypes
{
    using System;
    [Serializable]
    public class CLDouble
    {
        ///<summary>
        /// Current array
        ///</summary>
        public LDouble[] ArrayOfElements;
        /// <summary>
        /// Length of array - 1
        /// </summary>
        private byte sizeOfRound;
        /// <summary>
        /// Number of decimal places
        /// </summary>
        private double LimitSubstract;
        /// <summary>
        /// Instantiate array with given length
        /// </summary>
        public CLDouble(byte length)
        {
            length = length > 6 ? (byte)6 : length <= 1 ? (byte)2 : length;
            ArrayOfElements = new LDouble[length];
            sizeOfRound = (byte)(length - 1);
            LimitSubstract = 1 / Math.Pow(10, sizeOfRound);
            InitializeArray();
        }
        /// <summary>
        /// Instantiate array
        /// </summary>
        public CLDouble(LDouble[] array)
        {
            if (array.Length > 6)
            {
                ArrayOfElements = new LDouble[6];
                Array.Copy(array, ArrayOfElements, 6);
            }
            else if (array.Length < 2)
            {
                ArrayOfElements = new LDouble[2];
                Array.Copy(array, ArrayOfElements, array.Length);
                if (ArrayOfElements[0] == null) ArrayOfElements[0] = new LDouble(0, 10, false, 0, true);
                ArrayOfElements[1] = new LDouble(0, 10, false, 0, false);
            }
            else
            {
                ArrayOfElements = new LDouble[array.Length];
                Array.Copy(array, ArrayOfElements, array.Length);
            }
            sizeOfRound = (byte)(ArrayOfElements.Length - 1);
            LimitSubstract = 1 / Math.Pow(10, sizeOfRound);
        }
        /// <summary>
        /// Initialization elements of array
        /// </summary>
        private void InitializeArray()
        {
            for (int i = 0; i < ArrayOfElements.Length; i++)
            {
                if (i != ArrayOfElements.Length - 1)
                {
                    ArrayOfElements[i] = new LDouble(0, 10, false, 0, true);
                }
                else
                {
                    ArrayOfElements[i] = new LDouble(0, 10, false, 0, false);
                }
            }
        }
        ///<summary>
        /// Add a value to the array
        ///</summary>
        public void AddValueInArray(LDouble value, int index = 0)
        {
            if (ArrayOfElements.Length > 6) return;
            if (index + 1 >= ArrayOfElements.Length)
            {
                Array.Resize(ref ArrayOfElements, ArrayOfElements.Length + 1);
                ArrayOfElements[ArrayOfElements.Length - 1] = value;
            }
            else if (index == 0)
            {
                LDouble[] tempArray = new LDouble[ArrayOfElements.Length + 1];
                ArrayOfElements.CopyTo(tempArray, 1);
                tempArray[0] = value;
                tempArray.CopyTo(ArrayOfElements, 0);
            }
            else
            {
                Array.Resize(ref ArrayOfElements, ArrayOfElements.Length + 1);
                for (int i = index; i < ArrayOfElements.Length; i++)
                {
                    LDouble current = ArrayOfElements[i];
                    ArrayOfElements[i] = value;
                    value = current;
                }
            }
        }
        private void FillLessValue(double num1, int index)
        {
            if (index >= 0)
            {
                if (num1 - (ArrayOfElements[index].Limit - ArrayOfElements[index].Current) > 0)
                {
                    num1 = num1 - (ArrayOfElements[index].Limit -
                        (LimitSubstract * Math.Pow(10, sizeOfRound - index)) - ArrayOfElements[index].Current);
                    ArrayOfElements[index].Current = ArrayOfElements[index].Limit - 
                        (LimitSubstract * Math.Pow(10, sizeOfRound - index));
                    FillLessValue(num1 * ArrayOfElements[index].Limit, --index);
                }
                else
                {
                    ArrayOfElements[index].Current = Math.Round
                        ((ArrayOfElements[index] + num1).Current, sizeOfRound);
                }
            }
        }
        private void AddValueLoop(double num1, int index)
        {
            if (index < sizeOfRound)
            {
                if (ArrayOfElements[index].IsOverflow(num1))
                {
                    AddValueLoop(num1 / ArrayOfElements[index].Limit, ++index);
                }
                else
                {
                    ArrayOfElements[index].Current = Math.Round
                        ((ArrayOfElements[index] + num1).Current, sizeOfRound);
                }
            }
            else
            {
                if (ArrayOfElements[index].IsOverflow(num1))
                {
                    num1 = num1 - (ArrayOfElements[index].Limit - 
                        (LimitSubstract * Math.Pow(10, sizeOfRound - index)) - ArrayOfElements[index].Current);

                    ArrayOfElements[index].Current = ArrayOfElements[index].Limit -
                        (LimitSubstract * Math.Pow(10, sizeOfRound - index));
                    FillLessValue(num1 * ArrayOfElements[index].Limit, --index);
                }
                else
                {
                    ArrayOfElements[index].Current = Math.Round
                        ((ArrayOfElements[index] + num1).Current, sizeOfRound);
                }
            }
        }
        private double MultiplyValue(double num, int index) 
        {
            for (int i = index; i > 0; --i)
            {
                num = Math.Round(num * ArrayOfElements[i].Limit,
                    sizeOfRound - (sizeOfRound - i));
            }
            return num;
        }
        private void IncreaseNumber(ref double num, int index)
        {
            for (int i = index; i > 0; --i)
            {
                num = Math.Round(num / ArrayOfElements[i].Limit);
            }
        }
        private void SubstractLessValue(double num1, int index)
        {
            if (index >= 0)
            {
                if (ArrayOfElements[index].IsDeficit(num1))
                {
                    num1 -= MultiplyValue(ArrayOfElements[index].Current, index);
                    ArrayOfElements[index].Current = 0;
                    SubstractLessValue(num1, --index);
                }
                else
                {
                    IncreaseNumber(ref num1, index);
                    ArrayOfElements[index].Current = Math.Round
                        (ArrayOfElements[index].Current - num1, sizeOfRound);
                }
            }
        }
        private bool isSubstractLessAvailable(ref double num1)
        {
            double available = 0;
            for (int i = sizeOfRound ; i >= 0; i--)
            {
                available += MultiplyValue(ArrayOfElements[i].Current, i);
                num1 *= ArrayOfElements[i].Limit;
            }
            if (available >= num1) return true;
            return false;
        }
        private bool SubstractValueLoop(double num1, int index)
        {
            if (index < ArrayOfElements.Length)
            {
                if (ArrayOfElements[index].IsDeficit(num1))
                {
                    return SubstractValueLoop(num1 / ArrayOfElements[index].Limit, ++index);
                }
                else
                {
                    ArrayOfElements[index].Current = Math.Round((ArrayOfElements[index] - num1).Current, sizeOfRound);
                    if (ArrayOfElements[index].Current < 1)
                    {
                        ArrayOfElements[index - 1].Current = Math.Round
                            ((ArrayOfElements[index - 1] + ArrayOfElements[index].Current
                            * ArrayOfElements[index].Limit).Current, sizeOfRound);
                        ArrayOfElements[index].Current = 0;
                    }
                    return true;
                }
            }
            if (isSubstractLessAvailable(ref num1))
            {
                SubstractLessValue(num1, --index);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Add value to element
        /// </summary>
        /// <param name="num1">Value</param>
        public void AddValue(double num1)
        {
            if (ArrayOfElements[0].IsOverflow(num1))
            {
                AddValueLoop(num1 / ArrayOfElements[0].Limit, 1);
            }
            else
            {
                ArrayOfElements[0].Current = Math.Round((ArrayOfElements[0] + num1).Current, sizeOfRound);
            }
        }
        /// <summary>
        /// Add value to element started from index
        /// </summary>
        /// <param name="num1">Value</param>
        /// <param name="index">Start Index</param>
        public void AddValue(double num1, int index)
        {
            if (ArrayOfElements[index].IsOverflow(num1))
            {
                AddValueLoop(num1 / ArrayOfElements[index].Limit, ++index);
            }
            else
            {
                ArrayOfElements[index].Current = Math.Round((ArrayOfElements[index] + num1).Current, sizeOfRound);
            }
        }
        /// <summary>
        /// Substract value from element
        /// </summary>
        /// <param name="num1">Value</param>
        public bool SubstractValue(double num1)
        {
            if (ArrayOfElements[0].IsDeficit(num1))
            {
                return SubstractValueLoop(num1 / ArrayOfElements[0].Limit, 1);
            }
            else
            {
                ArrayOfElements[0].Current = Math.Round((ArrayOfElements[0] - num1).Current, sizeOfRound);
                return true;
            }
        }
        /// <summary>
        /// Substract value from element started from index
        /// </summary>
        /// <param name="num1">Value</param>
        /// <param name="index">Index</param>
        public bool SubstractValue(double num1, int index)
        {
            if (ArrayOfElements[index].IsDeficit(num1))
            {
                return SubstractValueLoop(num1 / ArrayOfElements[index].Limit, ++index);
            }
            else
            {
                ArrayOfElements[index].Current = Math.Round((ArrayOfElements[index] - num1).Current, sizeOfRound);
                return true;
            }
        }
        /// <summary>
        /// Returns a string representing the current object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string toReturn = "";
            for (int i = 0; i < ArrayOfElements.Length; i++)
            {
                toReturn += $"index: {i} = {ArrayOfElements[i]}\n";
            }
            return toReturn;
        }
    }
}
