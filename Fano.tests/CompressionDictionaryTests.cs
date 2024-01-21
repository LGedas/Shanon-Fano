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
        public void Constructor_FrequencyTable_CorrectDictionary()
        {
            int expectedSize = 3;
            var expectedKeys = new int[] { 0, 5, 7 };
    
            // Dictionary values (words)
            var expectedBits = new List<BitArray>
            {
                new BitArray(new[] { false  }),         // Expected: 0
                new BitArray(new[] { true, false }),    // Expected: 10
                new BitArray(new[] { true, true }),     // Expected: 11
            };

            // Dictionary keys (codes) 
            var bitWord1 = new BitArray(new bool[] { false, false, false });    // 000 int represantation: 0 
            var bitWord2 = new BitArray(new bool[] { true, false, true });      // 101 int represantation: 5 
            var bitWord3 = new BitArray(new bool[] { true, true, true });       // 111 int represantation: 7 

            var frequency1 = new WordFrequency(bitWord1);
            for (int i = 0; i < 3; i++) { frequency1.IncrementFrequency(); }    // Frequency 3

            var frequency2 = new WordFrequency(bitWord2);
            for (int i = 0; i < 2; i++) { frequency2.IncrementFrequency(); }    // Frequency 2
           
            var frequency3 = new WordFrequency(bitWord3);                       // Frequency 1

            List<WordFrequency> frequencies = new List<WordFrequency>
            {
                frequency1,
                frequency2,
                frequency3
             };

            Dictionary<int, BitArray> wordCodesDictionary = DictionaryGenerator.For(frequencies);

            Assert.Equal(expectedSize, wordCodesDictionary.Count);
            Assert.True(wordCodesDictionary.ContainsKey(expectedKeys[0]));
            Assert.True(wordCodesDictionary.ContainsKey(expectedKeys[1]));
            Assert.True(wordCodesDictionary.ContainsKey(expectedKeys[2]));

            Assert.Equal(expectedBits[0], wordCodesDictionary[expectedKeys[0]]);
            Assert.Equal(expectedBits[1], wordCodesDictionary[expectedKeys[1]]);
            Assert.Equal(expectedBits[2], wordCodesDictionary[expectedKeys[2]]);
        }
    }
}
