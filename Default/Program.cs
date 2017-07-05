using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Default
{
    class Program
    {
        struct TestStruct
        {
            public string myField;

            public void testMethod()
            {

            }
        }

        static void Main(string[] args)
        {
            // Test sur les Structs

            var myStruct = new TestStruct()
            {
                myField = "toto"
            };

            ArrayList myArrayList = new ArrayList();
            myArrayList.Add(myStruct);

            // Pas possible
            //TestStruct? myStructCasted = myArrayList[0] as TestStruct;

            // Test sur les strings
            var s = "toto";
            var v = s.ToList();
        }
    }
}
