using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano.tests
{
    public class CompressionDictionaryTests
    {
        [Fact]
        public void Constructor_FrequencyTable_CorrectIncialization()
        {
            int expectedSize = 3;
            var expectedKeys = new int[] { 0, 5, 7 };
            var expectedBits = new List<BitArray>
            {
                new BitArray(new[] { false,  }),
                new BitArray(new[] { true, false }),
                new BitArray(new[] { true, true }),
            };

            var frequency1 = new WordFrequency(new BitArray(new bool[] { false, false, false })); //0*2^2 + 0*2^1 + 0*2^0 = 0
            for (int i = 0; i < 3; i++) { frequency1.IncrementFrequency(); } //Frequency 3

            var frequency2 = new WordFrequency(new BitArray(new bool[] { true, false, true })); //1*2^2 + 0*2^1 + 1*2^0 = 5
            for (int i = 0; i < 2; i++) { frequency2.IncrementFrequency(); } //Frequency 2

            var frequency3 = new WordFrequency(new BitArray(new bool[] { true, true, true })); //1*2^2 + 1*2^1 + 1*2^0 = 7
            //Frequency 1

            List<WordFrequency> frequencies = new List<WordFrequency>
            {
                frequency1,
                frequency2,
                frequency3
             };

            CompressionDictionary compressionDictionary = new CompressionDictionary(frequencies);
            compressionDictionary.GenerateValues();

            Assert.Equal(expectedSize, compressionDictionary.Dictionary.Count);
            Assert.True(compressionDictionary.Dictionary.ContainsKey(expectedKeys[0]));
            Assert.True(compressionDictionary.Dictionary.ContainsKey(expectedKeys[1]));
            Assert.True(compressionDictionary.Dictionary.ContainsKey(expectedKeys[2]));

            Assert.Equal(expectedBits[0], compressionDictionary.Dictionary[expectedKeys[0]]);
            Assert.Equal(expectedBits[1], compressionDictionary.Dictionary[expectedKeys[1]]);
            Assert.Equal(expectedBits[2], compressionDictionary.Dictionary[expectedKeys[2]]);
        }
    }
}
