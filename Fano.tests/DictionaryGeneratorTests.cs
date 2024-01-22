using Fano.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano.tests
{
    public class DictionaryGeneratorTests
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

        [Fact]
        public void getFrequencyTable_ParserFromFile_CorrectTable()
        {
            TestFileUtilities.MakeFile("abcccdbdbeeabaca");

            int bitsWordLenght = 8;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght);
            var expected = new int[] { 4, 4, 4, 2, 2 };
            var expectedSize = 5;
            var expectedKeys = new int[] { 97, 98, 99, 100, 101 };

            // Dictionary values (words)
            var expectedCodes = new List<BitArray>
            {
                new BitArray(new[] { false, false }),           // Expected: 00
                new BitArray(new[] { false, true }),            // Expected: 01
                new BitArray(new[] { true, false }),            // Expected: 10
                new BitArray(new[] { true, true, false }),      // Expected: 110
                new BitArray(new[] { true, true, true }),       // Expected: 111
            };

            // Dicitonary Keys
            var expectedBits = new List<WordFrequency>
            {
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, false, false, true })),    // 0110 0001 - a
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, false, true, false })),    // 0110 0010 - b
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, false, true, true })),     // 0110 0011 - c 
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, true, false, false })),    // 0110 0100 - d
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, true, false, true }))      // 0110 0101 - e
            };

            for (int i = 0; i < 3; i++) { expectedBits[0].IncrementFrequency(); }   // "a" Frequency : 4
            for (int i = 0; i < 3; i++) { expectedBits[1].IncrementFrequency(); }   // "b" Frequency : 4
            for (int i = 0; i < 3; i++) { expectedBits[2].IncrementFrequency(); }   // "c" Frequency : 4
            for (int i = 0; i < 2; i++) { expectedBits[3].IncrementFrequency(); }   // "d" Frequency : 2
            for (int i = 0; i < 2; i++) { expectedBits[4].IncrementFrequency(); }   // "e" Frequency : 2

            parser.SetFrequencyTable();
            List<WordFrequency> frequencies = parser.Frequencies;
            Dictionary<int, BitArray> wordCodesDictionary = DictionaryGenerator.For(frequencies);

            Assert.Equal(expectedBits[0], frequencies[0]);
            Assert.Equal(expectedBits[1], frequencies[1]);
            Assert.Equal(expectedBits[2], frequencies[2]);
            Assert.Equal(expectedBits[3], frequencies[3]);
            Assert.Equal(expectedBits[4], frequencies[4]);

            Assert.Equal(expected[0], frequencies[0].Frequency);
            Assert.Equal(expected[1], frequencies[1].Frequency);
            Assert.Equal(expected[2], frequencies[2].Frequency);
            Assert.Equal(expected[3], frequencies[3].Frequency);
            Assert.Equal(expected[4], frequencies[4].Frequency);

            Assert.Equal(expectedSize, wordCodesDictionary.Count);
            Assert.True(wordCodesDictionary.ContainsKey(expectedKeys[0]));
            Assert.True(wordCodesDictionary.ContainsKey(expectedKeys[1]));
            Assert.True(wordCodesDictionary.ContainsKey(expectedKeys[2]));
            Assert.True(wordCodesDictionary.ContainsKey(expectedKeys[3]));
            Assert.True(wordCodesDictionary.ContainsKey(expectedKeys[4]));
            
            Assert.Equal(expectedCodes[0], wordCodesDictionary[expectedKeys[0]]);
            Assert.Equal(expectedCodes[1], wordCodesDictionary[expectedKeys[1]]);
            Assert.Equal(expectedCodes[2], wordCodesDictionary[expectedKeys[2]]);
            Assert.Equal(expectedCodes[3], wordCodesDictionary[expectedKeys[3]]);
            Assert.Equal(expectedCodes[4], wordCodesDictionary[expectedKeys[4]]);
        }
    }
}
