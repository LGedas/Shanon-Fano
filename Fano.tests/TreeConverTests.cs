using Fano.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano.tests
{
    public class TreeConverTests
    {
        [Fact]
        public void treeConver_8bitsize_16chars_correct()
        {
            TestFileUtilities.MakeFile("aaaabbbbccccddee");

            int bitsWordLenght = 8;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght);

            // 8 bit size words
            // 0110 0001 - a Frequency : 4      code : 00       tree - left left
            // 0110 0010 - b Frequency : 4      code : 01       tree - left right
            // 0110 0011 - c Frequency : 4      code : 10       tree - right left
            // 0110 0100 - d Frequency : 2      code : 110      tree - right right left
            // 0110 0101 - e Frequency : 2      code : 111      tree - right right right

            bool[] answer = new bool[]                                    // tree representation 0 01a1b 01c 01d1e
            {
                false,                                                      // 0
                false,                                                      // 0
                true,                                                       // 1
                false, true, true, false, false, false, false, true,        // 0110 0001 - a
                true,                                                       // 1
                false, true, true, false, false, false, true, false,        // 0110 0010 - b 
                false,                                                      // 0
                true,                                                       // 1
                false, true, true, false, false, false, true, true,         // 0110 0011 - c
                false,                                                      // 0
                true,                                                       // 1
                false, true, true, false, false, true, false, false,        // 0110 0100 - d
                true,                                                       // 1
                false, true, true, false, false, true, false, true          // 0110 0101
            };

            WordFrequency expected = new WordFrequency(new BitArray(answer));

            parser.SetFrequencyTable();
            List<WordFrequency> frequencies = parser.Frequencies;
            Dictionary<int, BitArray> dictionary = DictionaryGenerator.For(frequencies);

            BinaryTree tree = new BinaryTree(frequencies, dictionary);
            Node root = tree.generateTree();

            TreeConverter sequence = new TreeConverter(root);
            BitArray bits = sequence.wordCodeTree;
            WordFrequency result = new WordFrequency(bits);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void treeConver_8bitsize_3chars_correct()
        {
            TestFileUtilities.MakeFile("abc");

            int bitsWordLenght = 8;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght);

            // 8 bit size words
            // 0110 0001 - a Frequency : 4      code : 00       tree - left left
            // 0110 0010 - b Frequency : 4      code : 01       tree - left right
            // 0110 0011 - c Frequency : 4      code : 10       tree - right left
            // 0110 0100 - d Frequency : 2      code : 110      tree - right right left
            // 0110 0101 - e Frequency : 2      code : 111      tree - right right right

            bool[] answer = new bool[]                                    // tree representation 0 01a1b 01c 01d1e
            {
                false,                                                      // 0
                true,                                                       // 1
                false, true, true, false, false, false, false, true,        // 0110 0001 - a
                false,                                                      // 0
                true,                                                       // 1
                false, true, true, false, false, false, true, false,        // 0110 0010 - b 
                true,                                                       // 1
                false, true, true, false, false, false, true, true          // 0110 0011 - c
            };

            WordFrequency expected = new WordFrequency(new BitArray(answer));

            parser.SetFrequencyTable();
            List<WordFrequency> frequencies = parser.Frequencies;
            Dictionary<int, BitArray> dictionary = DictionaryGenerator.For(frequencies);

            BinaryTree tree = new BinaryTree(frequencies, dictionary);
            Node root = tree.generateTree();

            TreeConverter sequence = new TreeConverter(root);
            BitArray bits = sequence.wordCodeTree;
            WordFrequency result = new WordFrequency(bits);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void treeConver_16bitsize_8chars_correct()
        {
            TestFileUtilities.MakeFile("aabbcc");

            int bitsWordLenght = 16;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght);

            // 8 bit size words
            // 0110 0001 - a Frequency : 4      code : 00       tree - left left
            // 0110 0010 - b Frequency : 4      code : 01       tree - left right
            // 0110 0011 - c Frequency : 4      code : 10       tree - right left
            // 0110 0100 - d Frequency : 2      code : 110      tree - right right left
            // 0110 0101 - e Frequency : 2      code : 111      tree - right right right

            bool[] answer = new bool[]                                      // tree representation 0 01a1b 01c 01d1e
            {
                false,                                                      // 0
                true,                                                       // 1
                false, true, true, false, false, false, false, true,        // 0110 0001 - a
                false, true, true, false, false, false, false, true,        // 0110 0001 - a
                false,                                                      // 0
                true,                                                       // 1
                false, true, true, false, false, false, true, false,        // 0110 0010 - b 
                false, true, true, false, false, false, true, false,        // 0110 0010 - b 
                true,                                                       // 1
                false, true, true, false, false, false, true, true,         // 0110 0011 - c
                false, true, true, false, false, false, true, true          // 0110 0011 - c
            };

            WordFrequency expected = new WordFrequency(new BitArray(answer));

            parser.SetFrequencyTable();
            List<WordFrequency> frequencies = parser.Frequencies;
            Dictionary<int, BitArray> dictionary = DictionaryGenerator.For(frequencies);

            BinaryTree tree = new BinaryTree(frequencies, dictionary);
            Node root = tree.generateTree();

            TreeConverter sequence = new TreeConverter(root);
            BitArray bits = sequence.wordCodeTree;
            WordFrequency result = new WordFrequency(bits);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void treeConver_3bitsize_2chars_correct()
        {
            TestFileUtilities.MakeFile("aa");

            int bitsWordLenght = 3;
            var parser = new Parser(TestFileUtilities.path, bitsWordLenght); 

            bool[] answer = new bool[]                                      // tree representation 0( 1X 0( 1Y 0( 1A1B ) )
            {
                false,                                                      // 0
                true,                                                       // 1
                false, false, false,                                        // 000 - X
                false,                                                      // 0
                true,                                                       // 1
                false, true, true,                                          // 011 - Y
                false,                                                      // 0
                true,                                                       // 1
                false, true, false,                                         // 010 - A 
                true,                                                       // 1 
                true, true, false                                           // 110 - B 
            };

            WordFrequency expected = new WordFrequency(new BitArray(answer));

            parser.SetFrequencyTable();
            List<WordFrequency> frequencies = parser.Frequencies;
            Dictionary<int, BitArray> dictionary = DictionaryGenerator.For(frequencies);

            BinaryTree tree = new BinaryTree(frequencies, dictionary);
            Node root = tree.generateTree();

            TreeConverter sequence = new TreeConverter(root);
            BitArray bits = sequence.wordCodeTree;
            WordFrequency result = new WordFrequency(bits);

            Assert.Equal(expected, result);
        }
    }
}
