using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Mammal
    {
        private string _name;

        public Mammal(string name)
        {
            this._name = name;
        }

        public virtual void Talk()
        {
            Console.WriteLine($"ich bin {_name}");
        }

        public override string ToString()
        {
            return $"I'm a Mammal and my name is {_name}";
        }
    }
}
