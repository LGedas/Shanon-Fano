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
        private BitArray _bitWord;
        private int _frequency;

        public WordFrequency( int size )
        {
            _bitWord = new BitArray( size );
        }

        public WordFrequency( BitArray bitWord )
        {
            _bitWord = bitWord;
            IncrementFrequency();
        }

        public int IncrementFrequency() => _frequency++;

        public int Frequency() => _frequency;

        public BitArray BitWord
        {
            get { return _bitWord; }
            set { _bitWord = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            WordFrequency other = (WordFrequency)obj;

            if (BitWord.Length != other.BitWord.Length) { return false; }

            for (int i = 0; i < other.BitWord.Length; i++)
            {
                if (BitWord[i] != other.BitWord[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode() 
        { 
            return HashCode.Combine(BitWord, Frequency); 
        }
        
    }
}
