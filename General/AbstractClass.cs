using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public abstract class AbstractClass
    {
        // A property can be abstract because it's a special method
        public abstract int MyProperty1 { get; set; }

        // A property in an abstract class can also be non-abstract
        public int MyProperty2 { get; set; }

        // a field cannot be abstract because it's an implementation
        //public abstract int myProperty3;

        public int myProperty4;
    }
}
