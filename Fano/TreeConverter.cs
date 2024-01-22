using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fano
{
    public class TreeConverter
    {
        public Node root;
        public BitArray wordCodeTree;

        public TreeConverter(Node root)
        {
            this.root = root;
            wordCodeTree = new BitArray(0);
            generateSequence(root);
        }

        private void generateSequence(Node node)
        {
            if (node == null)
            {
                return ;
            }

            if (node.Bits != null)
            {
                // Append 'true' (1) followed by the bits from the node
                wordCodeTree.Length += 1;
                wordCodeTree[wordCodeTree.Length - 1] = true;
                //wordCodeTree.Length += node.Bits.Length;

                // Append bits individually
                foreach (bool bit in node.Bits)
                {
                    wordCodeTree.Length += 1;
                    wordCodeTree[wordCodeTree.Length - 1] = bit;
                }

            }
            else
            {
                // Append 'false'
                wordCodeTree.Length += 1;
                wordCodeTree[wordCodeTree.Length - 1] = false;
            }

            generateSequence(node.Left);
            generateSequence(node.Right);              
        }
    }
}
