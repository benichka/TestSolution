using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncActionPredicate
{
    class Program
    {
        public static void Main(string[] args)
        {
            // In a func, all but the last item is a parameter. The last item is the return type
            Func<int, string> myFunc = new Func<int, string>(MethodToIllustrateFunc);

            // It can also be declared like that:
            Func<int, string> myFunc2 = MethodToIllustrateFunc;

            Console.WriteLine("--------------------- Func ---------------------");
            Console.WriteLine(myFunc(5));
            Console.WriteLine(myFunc2(52));

            // Action doesn't return a value; all item are parameters
            Action<int> myAction = new Action<int>(MethodToIllustrateAction);
            
            // Same a func: it can also be declared like that
            Action<int> myAction2 = MethodToIllustrateAction;
            Console.WriteLine("-------------------- Action --------------------");
            myAction(5);
            myAction2(52);
            // Same as:
            myAction.Invoke(5);
            myAction2.Invoke(52);
        }

        /// <summary>
        /// Method that takes an int as the only parameter and returns the string representation of it
        /// </summary>
        /// <param name="i">The int to returns as string</param>
        /// <returns>The string representation of the int passed as parameter</returns>
        public static string MethodToIllustrateFunc(int i)
        {
            return i.ToString();
        }

        /// <summary>
        /// Method that takes an int as the only parameter and output the string representation of it
        /// </summary>
        /// <param name="i">The int to output as string in the console</param>
        public static void MethodToIllustrateAction(int i)
        {
            Console.WriteLine(i.ToString());
        }
    }
}
