using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModifier
{
    /// <summary>
    /// A simple class that shows the use of access modifier
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Possible: internal by default
            var topLevel = new TopLevelClass();

            // Possible: still internal by default
            var inherited = new InheritedType();

            // Not possible: this class is private by default
            //var nested = new NestedType.NestedClassDefault();

            // Possible because it's now internal
            var nestedInternal = new NestedType.NestedClassInternal();
        }
    }
}
