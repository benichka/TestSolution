using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Program
    {
        static void doWork()
        {
            Mammal myMammal = new Mammal("mammal de base");

            Horse myHorse = new Horse("premier cannasson");
            myHorse.Talk();

            IGrazer grazer = myHorse;
            grazer.Graze();

            int x = 591;
            Console.WriteLine($"x.Negate {x.Negate()}");

            TestProperties testProp = new TestProperties();
            Console.WriteLine(testProp.DateCreation);

        }

        static void Main(string[] args)
        {
            try
            {
                doWork();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
        }
    }
}
