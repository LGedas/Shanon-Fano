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

            var frequency = new WordFrequency(expectedSize);

            Assert.Equal(expectedSize, frequency.BitWord.Length);       
        }
        [Fact]
        public void constructor_bitWord101_CreatesCorrectObject()
        {
            const int expectedSize = 3;
            BitArray expectedArray = new BitArray(new bool[3] { true, false, true });

            var frequency = new WordFrequency(new BitArray (new bool[] { true, false, true } ) );

            Assert.Equal(expectedSize, frequency.BitWord.Length);
            Assert.Equal(expectedArray, frequency.BitWord);
        }

        [Fact]
        public void wordFrequencyEquallMethod_TwoEqualObjects_True() 
        {
            bool[] boolArray = new bool[3] { true, false, true };
            BitArray bits = new BitArray(boolArray);

            var freq1 = new WordFrequency( bits );               
            var freq2 = new WordFrequency( bits );

            Assert.Equal(freq1, freq2);
        }
    }
}
