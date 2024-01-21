using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano
{
    public class Parser
    {
        public List<WordFrequency> frequencies;
        public WordFrequency bitWord;
        public BitArray remainder;
        public const int byteSize = 8;
        public readonly int bitWordSize;
        public int bitIndex;
        public readonly string path;

        public Parser(string path, int bitWordSize)
        {
            this.path = path;
            this.bitWordSize = bitWordSize;
            bitWord = new WordFrequency(bitWordSize);
            frequencies = new List<WordFrequency>();
        }

        public BitArray Remainder => remainder;

        public List<WordFrequency> Frequencies => frequencies;

        public void SetFrequencyTable()
        {
            using (var file = new FileReader(path))
            {
                byte[] bytes = file.Read();

                while (bytes.Any())
                {
                    foreach (byte byteFromFile in bytes)
                    {
                        ParseByte(byteFromFile);
                    }

                    bytes = file.Read();
                }
            }

            frequencies.Sort(OrderDescending);

            SetRemainder();
        }

        private int OrderDescending(WordFrequency frequency1, WordFrequency frequency2)
        {
            if (frequency1.Frequency == frequency2.Frequency)
            {
                return 0;
            }

            return frequency1.Frequency < frequency2.Frequency ? 1 : -1;
        }

        private void SetRemainder()
        {
            remainder = bitWord.Bits;
            remainder.Length = bitIndex;
        }

        public void ParseByte(byte byteFromFile)
        {
            for (int i = 0; i < byteSize; i++)
            {
                InsertBit(byteFromFile, i);

                if (bitIndex == 0)
                {
                    TryInsertWord();
                }
            }
        }

        public void TryInsertWord()
        {
            WordFrequency word = frequencies.FirstOrDefault(item => item.Equals(bitWord));

            // If word exists increas it's frequency
            if (word != null)
            {
                word.IncrementFrequency();
                return;
            }

            frequencies.Add(new WordFrequency(new BitArray(bitWord.Bits)));
        }


        // NOTE: The values of bitWord.Bits are repeatedly overwritten during byte parsing.
        // In the case of parsing 'a' (0110 0001) with a word size of 3:
        // 1st iteration: 011   2nd iteration: 000   3rd iteration: 01 (0 left over from 2nd iteration)
        // The 3rd iteration might be incomplete theoretically, expecting '01' but actual bitWord.Bits is '010'.

        // TODO: When setting a "remainder," use the "bitIndex" counter to extract the right
        // number of bits. Consider resetting bitWord.Bits or adjusting the parsing logic to
        // handle cases where the bitWord size and byte representation may not perfectly align.

        // FUTURE CONSIDERATION: Rewrite the parsing logic to recreate BitArray newArray =
        // new BitArray(currentArray.Length + 1). When the new length is the desired bit size,
        // insert a new word, or if it already exists, increase its frequency. Then reset BitArray
        public void InsertBit(byte byteFromFile, int position)
        {
            bitWord.Bits[bitIndex] = GetBit(byteFromFile, position);

            SetBitIndex();
        }

        // Extracks a specific bit from Byte
        public bool GetBit(byte byteFromFile, int position) => Convert.ToBoolean((byteFromFile >> (byteSize - 1 - position)) & 1);

        private void SetBitIndex() => bitIndex = (bitIndex + 1) % bitWordSize; // index is always from 0 to ( bitsWordSize -1) 
    }
}
