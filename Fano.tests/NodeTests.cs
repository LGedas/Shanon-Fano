using Fano.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Fano.tests
{
    public class NodeTests
    {
        [Fact]
        public void Node_create_Created()
        {
            BitArray bitArray1 = new BitArray(new[] { true, true, true });
            BitArray bitArray2 = new BitArray(new[] { false, false, false });

            Node node = new Node();
            node.Left = new Node(bitArray1);
            node.Right = new Node(bitArray2);

            Assert.True(node.Left.Bits.Cast<bool>().SequenceEqual(bitArray1.Cast<bool>()));            
            Assert.True(node.Right.Bits.Cast<bool>().SequenceEqual(bitArray2.Cast<bool>()));
        }

        [Fact]
        public void Maketree()
        {
            BitArray bitArray1 = new BitArray(new[] { true, true, true });
            BitArray bitArray2 = new BitArray(new[] { false, false, false });

            Node root = new Node();
            root.Left = new Node(bitArray1);
            root.Right = new Node(bitArray2);

            Node currentNode = root;
            int x = 0;

            for (int i=0; i<bitArray1.Length; i++)
            {
                if (bitArray1[i] == false)
                {
                    currentNode.Left = new Node();
                    currentNode = currentNode.Left;
                }

                if (bitArray1[i] == true)
                {
                    currentNode.Right = new Node();
                    currentNode = currentNode.Right;
                }
                if (i == bitArray1.Length - 1 ) { currentNode.Bits = new BitArray(new[] { false, false, false }); }
            }

            Assert.True(root.Right.Right.Right.Bits.Cast<bool>().SequenceEqual(bitArray2.Cast<bool>()));
        }

        [Fact]
        public void Maketree2()
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

            parser.SetFrequencyTable();
            List<WordFrequency> frequencies = parser.Frequencies;
            Dictionary<int, BitArray> dictionary = DictionaryGenerator.For(frequencies);

            BinaryTree tree = new BinaryTree(frequencies, dictionary);
            Node root = tree.generateTree();  

            Assert.True(root.Left.Left.Bits.Cast<bool>().SequenceEqual(frequencies[0].Bits.Cast<bool>()));
            Assert.True(root.Left.Right.Bits.Cast<bool>().SequenceEqual(frequencies[1].Bits.Cast<bool>()));
            Assert.True(root.Right.Left.Bits.Cast<bool>().SequenceEqual(frequencies[2].Bits.Cast<bool>()));
            Assert.True(root.Right.Right.Left.Bits.Cast<bool>().SequenceEqual(frequencies[3].Bits.Cast<bool>()));
            Assert.True(root.Right.Right.Right.Bits.Cast<bool>().SequenceEqual(frequencies[4].Bits.Cast<bool>()));
        }
    }
}
