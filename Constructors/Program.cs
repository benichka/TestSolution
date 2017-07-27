using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructors
{
    /// <summary>
    /// A simple class that shows how to use constructors
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var instance1 = new OneClassWithoutCtor();

            // Not possible: this class doesn't provide a default constructor
            //var instance2 = new OneClassWithOneCtor();

            var instance3 = new OneClassThatInherits();
        }
    }

    public class OneClassWithoutCtor
    {
        // Class without any constructor: it can be instanciated without any problem
        // C# creates one by default that instantiates the object and sets member variables to the default values as listed in the Default Values Table

        public int MyProperty { get; set; }
    }

    public class OneClassWithOneCtor
    {
        // Class with

        public int MyProperty { get; set; }

        public OneClassWithOneCtor(int param)
        {
            MyProperty = param;
        }
    }

    public class OneClassThatInherits : OneClassWithoutCtor
    {
        // This class inherits a class that doesn't have a constructor:
        // nothing else is required

    }

    public class AnotherClassThatInherits : OneClassWithOneCtor
    {
        // This class inherits a class that has a constructor but no default constructor:
        // the call to one constructor is mandatory
        public AnotherClassThatInherits(int param) : base(param)
        {
        }
    }

}
