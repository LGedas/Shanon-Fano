using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano
{
    public class WordFrequency
    {
        private BitArray _bits; //In the Shannon-Fano algorithm, a bit sequence, known as a bit-word, will be referred to simply as 'word'
        private int _frequency;

        public WordFrequency(int size)
        {
            _bits = new BitArray(size);
            _frequency = 0;
        }

        public WordFrequency(BitArray bits)
        {
            if (bits == null || bits.Length == 0)
                throw new ArgumentException("BitArray must not be null or empty.");

            _bits = bits;
            IncrementFrequency();
        }

        public int IncrementFrequency() => _frequency++;

        public int Frequency 
        {
            get { return _frequency; }
            private set { _frequency = value; }
        }

        public BitArray Bits
        {
            get { return _bits; }
            set { _bits = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            WordFrequency other = (WordFrequency)obj;

            if (Bits.Length != other.Bits.Length)
                return false;

            return Bits.Cast<bool>().SequenceEqual(other.Bits.Cast<bool>());
        }

        public override int GetHashCode() 
        { 
            return HashCode.Combine(Bits, Frequency); 
        }
        
    }
}
