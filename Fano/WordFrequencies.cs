using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano
{
    public class WordFrequencies
    {
        private BitArray _bitWord;
        private int _frequency;

        public WordFrequencies( int size )
        {
            _bitWord = new BitArray( size );
        }

        public WordFrequencies( BitArray bitWord )
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
    }
}
