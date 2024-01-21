using Fano.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano.tests
{
   public class ParserTests
    {
        [Fact]
        public void getFrequencyTable_wordSize8_CorrectTable()
        {
            TestFileUtilities.MakeFile("aaaabbbbccccddee");

            int bitsWordLenght = 8;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght);
            var expected = new int[] { 4, 4, 4, 2, 2 };

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
        }

        [Fact]
        public void getBit_01100001_correct()
        {
            byte Byte = 97;
            var expected = new[] { false, true, true, false, false, false, false, true };
            Parser parser = new Parser( "a", 8);
            var bits = new bool[8];

            for (int i = 0; i < 8; i++)
            {
                bits[i] = parser.GetBit(Byte, i);
            }

            Assert.Equal(expected, bits); 
        }

        [Fact]
        public void insertBit_01100001_correct()
        {
            byte Byte = 97;
            var expected = new[] { false, true, true, false, false, false, false, true };
            Parser parser = new Parser("a", 8);

            for (int i = 0; i < 8; i++ )
            {
                //parser.bitIndex = i;
                parser.InsertBit(Byte, i);
            }

            Assert.Equal(expected, parser.bitWord.Bits.Cast<bool>());
        }

        [Fact]
        public void insertword_01100001_correct()
        {
            byte A = 255;
            byte B = 0;

            var expectedA = new[] { true, true, true, true, true, true, true, true, };
            var expectedB = new[] { false, false, false, false, false, false, false, false, };
            var expected = new[] { 2, 1 };

            Parser parser = new Parser("\a.txt", 8);

            for (int i = 0; i < 8; i++)
            {
                parser.InsertBit(A, i);
            }
            BitArray coppy1 = new BitArray(parser.bitWord.Bits);

            parser.TryInsertWord();
            parser.TryInsertWord();
            var coppy2 = parser.bitWord;

            for (int i = 0; i < 8; i++)
            {
                parser.InsertBit(B, i);
            }
            var coppy3 = parser.bitWord;

            parser.TryInsertWord();
            var coppy4 = parser.bitWord;

            List<WordFrequency> freq = parser.Frequencies;

            Assert.Equal(expectedA, freq[0].Bits.Cast<bool>());
            Assert.Equal(expectedB, freq[1].Bits.Cast<bool>());

            Assert.Equal(expected[0], freq[0].Frequency);
            Assert.Equal(expected[1], freq[1].Frequency);
        }

        [Fact]
        public void parseByte_255_255_0_correctTable()
        {
            byte A = 255;
            byte B = 0;

            var expectedA = new[] { true, true, true, true };
            var expectedB = new[] { false, false, false, false };
            var expected = new[] { 4, 2 };

            Parser parser = new Parser("\a.txt", 4);

            parser.ParseByte(A);
            parser.ParseByte(A);
            parser.ParseByte(B);

            List<WordFrequency> freq = parser.Frequencies;
            
            //bits are euqual
            Assert.Equal(expectedA, freq[0].Bits.Cast<bool>());
            Assert.Equal(expectedB, freq[1].Bits.Cast<bool>());
            // frequencies are equal
            Assert.Equal(expected[0], freq[0].Frequency);
            Assert.Equal(expected[1], freq[1].Frequency);
        }

        [Fact]
        public void parseByte_ab_4itSizeWords_correctTabble()
        {
            byte A = 97;    // int representation
            byte B = 98;    // int representation

            var expected0 = new[] { false, true, true, false };
            var expected1 = new[] { false, false, false, true };
            var expected2 = new[] { false, false, true, false };
            var expected = new[] { 2, 1, 1 };

            Parser parser = new Parser("\a.txt", 4);

            parser.ParseByte(A);
            parser.ParseByte(B);

            List<WordFrequency> freq = parser.Frequencies;

            Assert.Equal(expected0, freq[0].Bits.Cast<bool>());
            Assert.Equal(expected1, freq[1].Bits.Cast<bool>());
            Assert.Equal(expected2, freq[2].Bits.Cast<bool>());

            Assert.Equal(expected[0], freq[0].Frequency);
            Assert.Equal(expected[1], freq[1].Frequency);
            Assert.Equal(expected[2], freq[2].Frequency);
        }

        [Fact]
        public void parseByte_aaa_3itSizeWords_correctTabble()
        {
            byte A = 97;    // 97 = 0110 0001

            var expected0 = new[] { false, true, true };        // 011
            var expected1 = new[] { false, false, false };      // 000
            var expected2 = new[] { false, true, false };       // 010
            var expected3 = new[] { true, true, false };        // 110
            var expected4 = new[] { true, false, true };        // 101
            var expected5 = new[] { true, false, false };       // 100
            var expected6 = new[] { false, false , true};       // 001
            var expected = new[] { 1, 2, 1, 1, 1, 1, 1 };

            Parser parser = new Parser("\a.txt", 3);

            parser.ParseByte(A);
            parser.ParseByte(A);
            parser.ParseByte(A);

            List<WordFrequency> freq = parser.Frequencies;

            Assert.Equal(expected0, freq[0].Bits.Cast<bool>());
            Assert.Equal(expected1, freq[1].Bits.Cast<bool>());
            Assert.Equal(expected2, freq[2].Bits.Cast<bool>());
            Assert.Equal(expected3, freq[3].Bits.Cast<bool>());
            Assert.Equal(expected4, freq[4].Bits.Cast<bool>());
            Assert.Equal(expected5, freq[5].Bits.Cast<bool>());
            Assert.Equal(expected6, freq[6].Bits.Cast<bool>());

            Assert.Equal(expected[0], freq[0].Frequency);
            Assert.Equal(expected[1], freq[1].Frequency);
            Assert.Equal(expected[2], freq[2].Frequency);
            Assert.Equal(expected[3], freq[3].Frequency);
            Assert.Equal(expected[4], freq[4].Frequency);
            Assert.Equal(expected[5], freq[5].Frequency);
            Assert.Equal(expected[6], freq[6].Frequency);
        }

        [Fact]
        public void parseByte_a_3itSizeWords_correctTabble()
        {
            byte A = 97;    // 97 = 0110 0001

            var expected0 = new[] { false, true, true };        // 011
            var expected1 = new[] { false, false, false };      // 000
            var expected2 = new[] { false, true };              // 01
            var expected = new[] { 1, 1 };

            Parser parser = new Parser("\a.txt", 3);

            parser.ParseByte(A);

            List<WordFrequency> freq = parser.Frequencies;

            Assert.Equal(expected0, freq[0].Bits.Cast<bool>());
            Assert.Equal(expected1, freq[1].Bits.Cast<bool>());

            Assert.Equal(expected[0], freq[0].Frequency);
            Assert.Equal(expected[1], freq[1].Frequency);
        }

        [Fact]
        public void getFrequencyTable_remainder_CorrectTable()
        {
            TestFileUtilities.MakeFile("a");

            int bitsWordLenght = 3;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght);
            byte A = 97;    // 97 = 0110 0001

            var expected = new int[] { 1, 1 };

            var expectedBits = new List<WordFrequency>
            {
                new WordFrequency(new BitArray(new [] {  false, true, true })),     // 011
                new WordFrequency(new BitArray(new [] { false, false, false })),    // 000
                new WordFrequency(new BitArray(new [] { false, true })),            // 01
            };

            parser.SetFrequencyTable();
            List<WordFrequency> frequencies = parser.Frequencies;

            var copy = parser.remainder;

            Assert.Equal(expectedBits[0], frequencies[0]);
            Assert.Equal(expectedBits[1], frequencies[1]);

            Assert.Equal(expected[0], frequencies[0].Frequency);
            Assert.Equal(expected[1], frequencies[1].Frequency);
        }

        [Fact]
        public void getFrequencyTable_MakeFileVariant1_wordSize8_CorrectTable()
        {
            TestFileUtilities.MakeFile("abcccdbdbeeabaca");

            int bitsWordLenght = 8;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght);
            var expected = new int[] { 4, 4, 4, 2, 2 };

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
        }

        [Fact]
        public void getFrequencyTable_MakeFileVariant2_wordSize16_CorrectTable()
        {
            TestFileUtilities.MakeFile("aaaabbbbccccddee");

            int bitsWordLenght = 16;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght);
            var expected = new int[] { 2, 2, 2, 1, 1 };

            var expectedBits = new List<WordFrequency>
            {
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, false, false, true, false, true, true, false, false, false, false, true })),   // 0110 0001 0110 0001 - aa
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, false, true, false, false, true, true, false, false, false, true, false })),   // 0110 0010 0110 0010 - bb
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, false, true, true, false, true, true, false, false, false, true, true })),     // 0110 0011 0110 0011 - cc 
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, true, false, false, false, true, true, false, false, true, false, false })),   // 0110 0100 0110 0100 - dd
                new WordFrequency(new BitArray(new [] { false, true, true, false, false, true, false, true, false, true, true, false, false, true, false, true }))      // 0110 0101 0110 0101 - ee
            };

            for (int i = 0; i < 2; i++) { expectedBits[0].IncrementFrequency(); }   // "a" Frequency : 2
            for (int i = 0; i < 2; i++) { expectedBits[1].IncrementFrequency(); }   // "b" Frequency : 2
            for (int i = 0; i < 2; i++) { expectedBits[2].IncrementFrequency(); }   // "c" Frequency : 2
            for (int i = 0; i < 1; i++) { expectedBits[3].IncrementFrequency(); }   // "d" Frequency : 1
            for (int i = 0; i < 1; i++) { expectedBits[4].IncrementFrequency(); }   // "e" Frequency : 1

            parser.SetFrequencyTable();
            List<WordFrequency> frequencies = parser.Frequencies;

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
        }
    }
}
