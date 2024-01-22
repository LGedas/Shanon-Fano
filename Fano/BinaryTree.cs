using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fano
{
    public class BinaryTree
    {
        public Node Root;
        public readonly List<WordFrequency> frequencies;
        public readonly Dictionary<int, BitArray> dictionary;
        public int[] keys;

        public BinaryTree(List<WordFrequency> frequencies, Dictionary<int, BitArray> dictionary)
        {
            this.frequencies = frequencies;
            this.dictionary = dictionary;
            Root = new Node();
            keys = getKeys();
        }


        public Node generateTree()
        {
   

            for (int i = 0; i < dictionary.Count ; i++)
            {
                int key = keys[i];

                Insert(dictionary[key], frequencies[i].Bits);
            }

            return Root;
        }


        public void Insert(BitArray path, BitArray value)
        {
            Node currentNode = Root;

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == false)
                {
                    // Updated: Create a new node only if the left child does not exist
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = new Node();
                    }
                    currentNode = currentNode.Left;
                }

                if (path[i] == true)
                {
                    // Updated: Create a new node only if the right child does not exist
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = new Node();
                    }
                    currentNode = currentNode.Right;
                }

                if (i == path.Length - 1)
                {
                    currentNode.Bits = new BitArray(value);
                }       
            }
        }

        public int[] getKeys()
        {
            int size = frequencies.Count;
            int[] keys = new int[size];

            for (int i = 0; i < size; i++)
            {
                keys[i] = GetIntFromBitArray(frequencies[i].Bits);
            }

            return keys;
        }

        private int GetIntFromBitArray(BitArray bits)
        {
            int sum = 0;

            foreach (bool bit in bits)
            {
                sum = sum * 2 + (bit ? 1 : 0);
            }

            return sum;
        }
    }
}

