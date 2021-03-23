namespace ExtraTypes
{
    using System;
    [Serializable]
    public struct CLDouble
    {
        ///<summary>
        /// Current array
        ///</summary>
        public LDouble[] ArrayOfElements;
        private byte sizeOfRound;
        private double LimitSubstract;
        /// <summary>
        /// Instantiate array with given length
        /// </summary>
        public CLDouble(byte length) : this()
        {
            length = length > 6 ? (byte)6 : length;
            ArrayOfElements = new LDouble[length];
            sizeOfRound = (byte)(length - 1);
            LimitSubstract = 1 / Math.Pow(10, sizeOfRound);
            for (int i = 0; i < length; i++)
            {
                if (i != length - 1)
                {
                    ArrayOfElements[i].Limit = 10;
                    ArrayOfElements[i].AllowToOverFlow = true;
                }
                else
                {
                    ArrayOfElements[i].Limit = 10;
                }
            }
        }
        /// <summary>
        /// Instantiate array
        /// </summary>
        public CLDouble(LDouble[] array) : this()
        {
            switch (array.Length > 6)
            {
                case true:
                    Array.Resize(ref array, 6);
                    ArrayOfElements = array;
                    break;
                case false:
                    ArrayOfElements = array;
                    break;
            }
            sizeOfRound = (byte)ArrayOfElements.Length;
            LimitSubstract = 1 / Math.Pow(10, sizeOfRound);
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
                if (ArrayOfElements[index].AmountOfOverFlow(num1) >= 1)
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
                if (ArrayOfElements[index].AmountOfOverFlow(num1) >= 1)
                {
                    num1 = num1 - (ArrayOfElements[index].Limit - 
                        (LimitSubstract * System.Math.Pow(10, sizeOfRound - index)) - ArrayOfElements[index].Current);
                    ArrayOfElements[index].Current = ArrayOfElements[index].Limit -
                        (LimitSubstract * System.Math.Pow(10, sizeOfRound - index));
                    FillLessValue(num1 * ArrayOfElements[index].Limit, --index);
                }
                else
                {
                    ArrayOfElements[index].Current = System.Math.Round
                        ((ArrayOfElements[index] + num1).Current, sizeOfRound);
                }
            }
        }
        private bool SubstractValueLoop(double num1, int index)
        {
            if (index < ArrayOfElements.Length)
            {
                if (ArrayOfElements[index].AmountOfDeficit(num1) >= 1)
                {
                    return SubstractValueLoop(num1 / ArrayOfElements[index].Limit, ++index);
                }
                else
                {
                    ArrayOfElements[index].Current = Math.Round((ArrayOfElements[index] - num1).Current, sizeOfRound);
                    if (ArrayOfElements[index].Current < 1)
                    {
                        double temp = ArrayOfElements[index].Current;
                        ArrayOfElements[index].Current = 0;
                        ArrayOfElements[index - 1].Current = Math.Round
                            ((ArrayOfElements[index - 1] + temp * ArrayOfElements[index].Limit).Current, sizeOfRound);
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Add value to element
        /// </summary>
        /// <param name="num1">Value</param>
        public void AddValue(double num1)
        {
            if (ArrayOfElements[0].AmountOfOverFlow(num1) >= 1)
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
            if (ArrayOfElements[index].AmountOfOverFlow(num1) >= 1)
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
            if (ArrayOfElements[0].AmountOfDeficit(num1) >= 1)
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
            if (ArrayOfElements[index].AmountOfDeficit(num1) >= 1)
            {
                return SubstractValueLoop(num1 / ArrayOfElements[index].Limit, ++index);
            }
            else
            {
                ArrayOfElements[index].Current = Math.Round((ArrayOfElements[index] - num1).Current, sizeOfRound);
                return true;
            }
        }
    }
}
