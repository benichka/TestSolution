using System;

namespace InheritanceSimple
{
    abstract class MyAbstract
    {
        public abstract void MyAbstractMethod(string value);
        public virtual void MyVirtualMethod(string value)
        {
            Console.WriteLine("MyAbstract.MyVirtualMethod – " + value);
        }
        public void MyNonAbsMethod(string value)
        {
            Console.WriteLine("MyAbstract.MyMethod – " + value);
        }
    }
}
