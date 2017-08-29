using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    class Program
    {
        #region standard things
        /// <summary>
        /// Example delegate
        /// </summary>
        /// <param name="x">First parameter</param>
        /// <param name="y">Second parameter</param>
        /// <returns>Result of the function used in the delegate</returns>
        public delegate int Calculate(int x, int y);

        /// <summary>
        /// Example delegate to execute parameterless void function
        /// </summary>
        public delegate void Execute();
        #endregion standard things

        #region covariance/contravariance
        /// <summary>
        /// Example delegate for covariance
        /// </summary>
        /// <returns>An object</returns>
        public delegate object CovarianceDelegate();

        /// <summary>
        /// Example delegate for contravariance
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>A string</returns>
        public delegate string ContravarianceDelegate(string s);
        #endregion covariance/contravariance

        static void Main(string[] args)
        {
            #region standard things
            Calculate calcAdd = Add;
            var myResultAdd = calcAdd(1, 2);

            Calculate calcMult = Mult;
            var myResultMult = calcMult(1, 2);

            // Pointless: it will return the last operation -> 1*2
            //Calculate multi = Add;
            //multi += Mult;

            //var resultMulti = multi(1, 2);

            Execute exec = DisplaySomething;
            exec += DisplaySomethingElse;

            exec();
            #endregion standard things

            #region covariance/contravariance
            // Covariance: CovarianceDelegate returns an object, but CovarianceMethod returns a string.
            // A string is more derived than an object
            CovarianceDelegate covDel = CovarianceMethod;

            var covDelResult = covDel();
            // The result is a type of the return type of the method
            var typeOfCovDelResult = covDelResult.GetType();

            // Contravariance: ContravarianceDelegate takes a string as parameter, but ContravarianceMethod takes an object.
            // An object is less derived than an string
            ContravarianceDelegate contraVDel = ContravarianceMethod;

            string myTestString = "test string";
            object myTestStringAsObject = myTestString;

            // CAN'T do that: the delegate expect a string
            //var contraVDelResult = contraVDel(myTestStringAsObject);

            // This is the only code that will compile
            var contraVDelResult = contraVDel(myTestString);
            #endregion covariance/contravariance
        }

        #region standard things
        /// <summary>
        /// Simple function to add number
        /// </summary>
        /// <param name="x">First parameter</param>
        /// <param name="y">Second parameter</param>
        /// <returns>sum of x and y</returns>
        public static int Add(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        /// Simple function to multiply number
        /// </summary>
        /// <param name="x">First parameter</param>
        /// <param name="y">Second parameter</param>
        /// <returns>product of x and y</returns>
        public static int Mult(int x, int y)
        {
            return x * y;
        }

        /// <summary>
        /// Simple method that display a text in the console
        /// </summary>
        public static void DisplaySomething()
        {
            Console.WriteLine("Display something");
        }

        public static void DisplaySomethingElse()
        {
            Console.WriteLine("Display something else");
        }
        #endregion standard things

        #region covariance/contravariance
        public static string CovarianceMethod()
        {
            return "Covariance method";
        }

        public static string ContravarianceMethod(object o)
        {
            var typeOfO = o.GetType();
            // When called via the delegate, o is already considered a string
            return o as string;
        }
        #endregion covariance/contravariance
    }
}
