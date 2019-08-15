using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InctructionFileCreator
{
    class Pair<T>
    {
        public T A { get; set; }
        public T B { get; set; }

        public Pair()
        {
            
        }

        public Pair(T A, T B)
        {
            this.A = A;
            this.B = B;
        }

        public override string ToString()
        {
            return "{" + A + ";" + B + "}";
        }
    }
}
