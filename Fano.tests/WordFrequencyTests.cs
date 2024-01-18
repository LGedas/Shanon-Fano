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
        public void IncrementFrequency_TwoDifferentObjects_NotEqual()
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
    }
}
