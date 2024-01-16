using Fano;
using System.Collections;

namespace Fano.tests
{
    
    public class WordFrequenciesTests
    {
        

        [Fact]
        public void constructor_size8_CreatesWordFrequeciesWithCorrectLenght()
        {
            const int wordSize = 8;

            var frequency = new WordFrequencies(wordSize);

            Assert.Equal(frequency.BitWord.Length, wordSize);       
        }

        [Fact]
        public void constructor_bits101_CreatesWordFrequecieswithCorrectProperty() 
        {

        }
    }
}
