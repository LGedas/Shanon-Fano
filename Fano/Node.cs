using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fano
{
    public class Node
    {
        public BitArray Bits { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(BitArray bits )
        {
            Bits = bits;
            Left = null;
            Left = null;
        }

        public Node()
        {
            Left = null;
            Right = null;
        } 
    }
}
