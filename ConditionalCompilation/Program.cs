using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConditionalCompilation
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("I'm in debug mode");
#endif
#if TRACE
            Console.WriteLine("I'm in trace mode");
#endif
#if TESTCONFIG1
            Console.WriteLine("I'm in TestConfig1 mode");
#endif
#if TESTCONFIG2
            Console.WriteLine("I'm in TestConfig2 mode");
#endif
        }
    }
}
