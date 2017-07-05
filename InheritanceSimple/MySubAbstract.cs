using System;

namespace InheritanceSimple
{
    class MySubAbstract : MyAbstract
    {
        public override void MyAbstractMethod(string value)
        {
            Console.WriteLine("MySubAbstract.MyAbstractMethod – " + value);
        }

        public override void MyVirtualMethod(string value)
        {
            Console.WriteLine("MySubAbstract.MyVirtualMethod – " + value);
        }

        public void TestUseNonAbsMethod(string value)
        {
            this.MyNonAbsMethod("MySubAbstract.TestUseNonAbsMethod – " + value);
        }
    }
}
