using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Horse : Mammal, IGrazer
    {
        public Horse(string name) : base(name)
        { }

        public override void Talk()
        {
            Console.WriteLine("Huuuuuu huuuu huuuuu !");
        }

        void IGrazer.Graze()
        {
            Console.WriteLine("Miam miam !");
        }
    }
}
