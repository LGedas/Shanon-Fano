using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano
{
    public class CompressionDictionary
    {
        private readonly List<WordFrequency> _frequencyTable;
        private Dictionary<int, BitArray> _dictionary;  //dictionary 'key' is a decimal representention of bit sequence
        private readonly int[] _bitWords; //a copy of dictionary 'keys'

        public CompressionDictionary( List<WordFrequency> sortedFrequencyTable )
        {
            this._frequencyTable = sortedFrequencyTable;

            _dictionary = Enumerable.Range(0, sortedFrequencyTable.Count).ToDictionary(x => GetIntFromBitArray(x), x => (BitArray)null);
            _bitWords = _dictionary.Select(x => x.Key).ToArray();
        }

        public Dictionary<int, BitArray> Dictionary => _dictionary;

        public void GenerateValues()
        {
            FanoAlgorithm(0, _dictionary.Count - 1);
        }

        private void FanoAlgorithm(int left, int right)
        {
            if (left == right)
            {
                return;
            }

            int index = GetArraySplitIndex(left, right);

            AddBits(left, index, right);

            FanoAlgorithm(left, index);
            FanoAlgorithm(index + 1, right);
        }

        private void AddBits(int left, int index, int right)
        {
            for (int i = left; i <= right; i++)
            {
                bool value = i <= index ? false : true;
                AddBit(i, value);
            }
        }

        private void AddBit(int index, bool value)
        {
            if (_dictionary[_bitWords[index]] == null)
            {
                _dictionary[_bitWords[index]] = new BitArray(new[] { value });
                return;
            }

            BitArray currentArray = _dictionary[_bitWords[index]];
            BitArray newArray =  new BitArray(currentArray.Length + 1);

            for (int i = 0; i < currentArray.Length; i++)
            {
                newArray[i] = currentArray[i];
            }

            newArray[newArray.Length - 1] = value;

            _dictionary[_bitWords[index]] = newArray;
        }

        private int GetArraySplitIndex(int left, int right)
        {
            int maxArraySum = TotalFrequency(left, right);
            int arraySum = 0;

            for (int index = left; index < right; index++)
            {
                arraySum += _frequencyTable[index].Frequency;
                int nextArraySum = arraySum + _frequencyTable[index + 1].Frequency;

                int difference = Math.Abs(arraySum - (maxArraySum - arraySum));
                int nextDifference = Math.Abs(nextArraySum - (maxArraySum - nextArraySum));

                if (difference <= nextDifference)
                {
                    return index;
                }
            }

            throw new InvalidOperationException("Split index not found.");
        }

        private int TotalFrequency(int left, int right)
        {
            int sum = 0;

            for (int i = left; i <= right; i++)
            {
                sum += _frequencyTable[i].Frequency;
            }

            return sum;
        }

        private int GetIntFromBitArray(int index)
        {
            int sum = 0;

            foreach( bool bit in _frequencyTable[index].Bits )
            {
                sum = sum * 2 + (bit ? 1 : 0);
            }

            return sum;
        }
    }
}
