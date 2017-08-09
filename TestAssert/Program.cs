using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssert
{
    class Program
    {
        static void Main(string[] args)
        {
            var myList = new ArrayList()
            {
                1, 2, 3, 4, "toto"
            };

            foreach (var item in myList)
            {
                Trace.Assert(item is int, "L'objet n'est pas un int");
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
