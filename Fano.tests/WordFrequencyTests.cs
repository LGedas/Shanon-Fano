using Fano;
using System.Collections;
using System.Diagnostics;

namespace Fano.tests
{
    
    public class WordFrequencyTests
    {               
        [Fact]
        public void constructor_size8_CreatesWordFrequeciesWithCorrectLenght()
        {
            const int expectedSize = 8;

            var word = new WordFrequency(expectedSize);

            Assert.Equal(expectedSize, word.Bits.Length);       
        }
        [Fact]
        public void constructor_bitWord101_CreatesCorrectObject()
        {
            const int expectedSize = 3;
            const int expectedFrequency = 1;
            BitArray expectedArray = new BitArray(new bool[3] { true, false, true });

            var frequency = new WordFrequency(new BitArray (new bool[] { true, false, true } ) );

            Assert.Equal(expectedSize, frequency.Bits.Length);
            Assert.Equal(expectedArray, frequency.Bits);
            Assert.Equal(expectedFrequency, frequency.Frequency );
        }

        [Fact]
        public void EqualsMethod_TwoEqualObjects_True() 
        {
            bool[] boolArray = new bool[3] { true, false, true };
            BitArray bits = new BitArray(boolArray);

            var freq1 = new WordFrequency( bits );               
            var freq2 = new WordFrequency( bits );

            Assert.Equal(freq1, freq2);
        }

        [Fact]
        public void EqualsMethod_TwoDifferentSizeObjects_NotEqual()
        {
            bool[] boolArray1 = new bool[] { true, false, true };
            bool[] boolArray2 = new bool[] { true, false, true, false };
            BitArray bits1 = new BitArray(boolArray1);
            BitArray bits2 = new BitArray(boolArray2);

            var freq1 = new WordFrequency(bits1);
            var freq2 = new WordFrequency(bits2);

            Assert.NotEqual(freq1, freq2);
        }

        [Fact]
        public void EqualsMethod_TwoDifferentValueObjects_NotEqual()
        {
            bool[] boolArray1 = new bool[] { true, false, true };
            bool[] boolArray2 = new bool[] { true, false, false };
            BitArray bits1 = new BitArray(boolArray1);
            BitArray bits2 = new BitArray(boolArray2);

            var freq1 = new WordFrequency(bits1);
            var freq2 = new WordFrequency(bits2);

            Assert.NotEqual(freq1, freq2);
        }

        [Fact]
        public void IncrementFrequency_CorrectlyIncremented()
        {
            const int expectedFrequency = 2;
            bool[] boolArray = new bool[3] { true, false, true };
            BitArray bits = new BitArray(boolArray);

            var word = new WordFrequency(bits);
            word.IncrementFrequency();

            Assert.Equal(expectedFrequency, word.Frequency);
        }

        [Fact]
        public void Constructor_NullBitArray_ThrowsArgumentException()
        {
            BitArray nullBitArray = null;

            Assert.Throws<ArgumentException>(() => new WordFrequency(nullBitArray));
        }

        [Fact]
        public void Constructor_EmptyBitArray_ThrowsArgumentException()
        {
            BitArray emptyBitArray = new BitArray(0);

            Assert.Throws<ArgumentException>(() => new WordFrequency(emptyBitArray));
        }

        [Fact]
        public void EqualsMethod2_TwoEqualObjects_True()
        {
            bool[] boolArray = new bool[3] { true, false, true };
            BitArray bits = new BitArray(boolArray);

            var freq1 = new WordFrequency(bits);
            var freq2 = new WordFrequency(bits);

            Assert.True(freq2.Equals(freq1));
            Assert.True(freq1.Equals(freq2));
        }

        [Fact]
        public void EqualsMethod3_TwoEqualObjects_True()
        {
            var bitWord1 = new BitArray(new bool[] { false, false, false });    // 000 int represantation: 0 
            var bitWord2 = new BitArray(new bool[] { true, false, true });      // 101 int represantation: 5 
            var bitWord3 = new BitArray(new bool[] { true, true, true });       // 111 int represantation: 7 

            var bitWord4 = new BitArray(new bool[] { true, true, false });  // 110

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

            WordFrequency word = frequencies.FirstOrDefault(item => item.Bits.Equals(bitWord1));
            Assert.True(frequencies[0].Equals(word));

            Assert.True(frequencies[0].Equals(frequency1));
            Assert.True(frequencies[0].Bits.Equals(bitWord1));

            Assert.True(frequencies[0].Equals(frequency1));
            Assert.True(frequencies[0].Bits.Equals(bitWord1));

            Assert.NotNull(frequencies.FirstOrDefault(item => item.Bits.Equals(bitWord1)));
            Assert.NotNull(frequencies.FirstOrDefault(item => item.Bits == bitWord1));

            Assert.Null(frequencies.FirstOrDefault(item => item.Bits.Equals(bitWord4)));
            Assert.Null(frequencies.FirstOrDefault(item => item.Bits == bitWord4));

        }

        [Fact]
        public void EqualsMethod4_TwoEqualObjects_True()
        {
            var bitWord1 = new BitArray(new bool[] { false, false, false });    // 000 int represantation: 0 
            var bitWord2 = new BitArray(new bool[] { true, false, true });      // 101 int represantation: 5 
            var bitWord3 = new BitArray(new bool[] { true, true, true });       // 111 int represantation: 7 

            var bitWord4 = new BitArray(new bool[] { true, true, false });  // 110

            var frequency1 = new WordFrequency(bitWord1);
            for (int i = 0; i < 2; i++) { frequency1.IncrementFrequency(); }    // Frequency 3

            var frequency2 = new WordFrequency(bitWord2);
            for (int i = 0; i < 1; i++) { frequency2.IncrementFrequency(); }    // Frequency 2

            var frequency3 = new WordFrequency(bitWord3);                       // Frequency 1

            List<WordFrequency> frequencies = new List<WordFrequency>
           {
               frequency1,
               frequency2,
               frequency3
            };

            WordFrequency word = frequencies.FirstOrDefault(item => item.Bits.Equals(bitWord1));
            Assert.True(frequencies[0].Equals(word));

            bitWord1 = bitWord2;
            word = frequencies.FirstOrDefault(item => item.Bits.Equals(bitWord1));
            Assert.True(frequencies[1].Equals(word));


        }


    }
}
