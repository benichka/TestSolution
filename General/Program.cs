using General.Interface;
using System;
using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

namespace General
{
    class Program
    {
        public const int SPECIAL_NUMBER = 213;

        static void Main(string[] args)
        {
            // Pas possible d'instancier une interface
            //var myInterfaceInstance = new TestInterface();

            #region working on value type
            int myInt1 = 2;
            int myInt2 = 2;

            // Passage par recopie : myInt ne bouge pas car les changements restent locaux à la méthode
            OperateOnInt(myInt1);

            // Passage par référence : les changements sont répercutés sur l'original
            OperateOnInt(ref myInt2);

            Console.WriteLine(myInt1);
            Console.WriteLine(myInt2);
            #endregion working on value type

            #region working on reference type
            var oneClass1 = new OneClass { MyProperty1 = 1 };
            var oneClass2 = new OneClass { MyProperty1 = 1 };

            // Un nouvel objet est instancié mais il s'agit d'une copie : oneClass1 n'est pas changé
            OperateOnRef(oneClass1);

            // Un nouvel objet est instancié et il pointe sur la référence de oneClass2, ce dernier est donc écrasé
            OperateOnRef(ref oneClass2);

            Console.WriteLine(oneClass1.MyProperty1);
            Console.WriteLine(oneClass2.MyProperty1);

            var myString1 = "toto";
            var myString2 = "toto";

            // String est un reference type considéré comme un value type !
            OperateOnString(myString1);
            OperateOnString(ref myString2);

            Console.WriteLine(myString1);
            Console.WriteLine(myString2);
            #endregion working on reference type

            #region struc vs class
            //var myStruct = new TestStruct()
            //{
            //    myField = "toto"
            //};

            ArrayList myArrayList = new ArrayList();
            //myArrayList.Add(myStruct);

            // Pas possible
            //TestStruct? myStructCasted = myArrayList[0] as TestStruct;

            /*
            Structures.MyStruct1 struct1 = new Structures.MyStruct1();
            struct1.MyProperty1 = 1;
            struct1.MyProperty2 = 2;

            OneClass oneClass = new OneClass();
            oneClass.MyProperty1 = 1;
            oneClass.MyProperty2 = 2;

            TestContainer container = new TestContainer();
            // Référence à la classe créée juste avant (class = référence type)
            container.MyOneClass = oneClass;
            // Copie à la structure créée juste avant (struct = value type)
            container.MyStruct1 = struct1;

            // Impossible : si l'on fait cela on modifie une copie de container.MyStruct1 et donc erreur de compil
            //container.MyStruct1.MyProperty1 = 4;
            // Il faudrait réinstancier l'objet
            //container.MyStruct1 = struct1;
            // => Si une struct doit changer, il faut utiliser un type class

            // container.MyStruct1 n'est pas mis à jour car strut = value type
            struct1.MyProperty1 = 5;
            container.MyOneClass.MyProperty1 = 5;

            Console.WriteLine($"oneClass.MyProperty1: {oneClass.MyProperty1}");
            Console.WriteLine($"container.MyStruct1.MyProperty1: {container.MyStruct1.MyProperty1}");
            */
            #endregion

            #region casting simple
            string myString = "123";
            int myInt = 2;

            // Impossible d'un type "trop éloigné" d'un autre
            //int myOtherInt = (int)myString;

            int myOtherInt = Convert.ToInt32(myString);
            #endregion casting simple

            #region basic LINQ Query
            /*
            // Create a new Hashtable and add some drinks with prices.
            Hashtable prices = new Hashtable
            {
                { "Café au Lait", 1.99M },
                { "Caffe Americano", 1.89M },
                { "Café Mocha", 2.99M },
                { "Cappuccino", 2.49M },
                { "Espresso", 1.49M },
                { "Espresso Romano", 1.59M },
                { "English Tea", 1.69M },
                { "Juice", 2.89M }
            };

            // Select all the drinks that cost less than $2.00, and order them by cost.
            var bargains =
               from string drink in prices.Keys
               where (Decimal)prices[drink] < 2.00M
               orderby prices[drink] ascending
               select drink;
            // Display the results.
            foreach (string bargain in bargains)
            {
                Console.WriteLine(bargain);
            }
            Console.ReadLine();
            */
            #endregion basic LINQ Query

            #region file manipulation
            Console.WriteLine("--------------- file manipulation ---------------");
            File.Delete("myFile.txt");

            FileInfo fileInfo = new FileInfo("myFile.txt"); // non-existent file
            Console.WriteLine(fileInfo.Exists);             // false
            File.Create("myFile.txt");
            Console.WriteLine(File.Exists("myFile.txt"));   // true
            Console.WriteLine(fileInfo.Exists);             // false -> it's "cached"
            fileInfo.Refresh();
            Console.WriteLine(fileInfo.Exists);             // true -> this time, refreshed!

            // Access to the temporary folder of the user
            var tmpPath = Path.GetTempPath();

            // Create a temporary file
            var tmpFile = Path.GetTempFileName();
            #endregion file manipulation

            #region date
            string myDateOK = "01/12/17";
            DateTime.TryParse(myDateOK, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime myDateAsDate1);
            var isFullDate_myDateAsDate1 = false;

            string myDateKO = "12/17";
            DateTime.TryParse(myDateKO, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime myDateAsDate2);
            var isFullDate_myDateAsDate2 = false;

            string myDateOK_2 = "2 décembre 2017";
            DateTime.TryParse(myDateOK_2, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime myDateAsDate3);
            var isFullDate_myDateAsDate3 = false;

            string myDateKO_2 = "2 décembre";
            DateTime.TryParse(myDateKO_2, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime myDateAsDate4);
            var isFullDate_myDateAsDate4 = false;

            if (myDateAsDate1.Date.GetDateTimeFormats(CultureInfo.InvariantCulture).Contains(myDateOK))
            {
                isFullDate_myDateAsDate1 = true;
            }

            if (myDateAsDate2.Date.GetDateTimeFormats(CultureInfo.InvariantCulture).Contains(myDateKO))
            {
                isFullDate_myDateAsDate2 = true;
            }

            if (myDateAsDate3.Date.GetDateTimeFormats(CultureInfo.InvariantCulture).Contains(myDateOK_2))
            {
                isFullDate_myDateAsDate3 = true;
            }

            //var fullDates = myDateAsDate4.Date.GetDateTimeFormats(CultureInfo.CurrentCulture).Where(date => )

            if (myDateAsDate4.Date.GetDateTimeFormats(CultureInfo.InvariantCulture).Contains(myDateKO_2))
            {
                isFullDate_myDateAsDate4 = true;
            }

            // => isFullDate_myDateAsDate1 = true and isFullDate_myDateAsDate2 = false
            #endregion date

            #region random stackoverflow question
            var result = IsSpecialNumberEntered("213");
            #endregion random stackoverflow question
        }

        static bool IsSpecialNumberEntered(string numberTextBoxTextValue)
        {
            return numberTextBoxTextValue.Equals(SPECIAL_NUMBER.ToString());
        }

        static void OperateOnString(string myString)
        {
            myString = "changed";
        }
        static void OperateOnString(ref string myString)
        {
            myString = "changed";
        }

        static void OperateOnInt(int myInt)
        {
            myInt = 42;
        }
        static void OperateOnInt(ref int myInt)
        {
            myInt = 42;
        }

        static void OperateOnRef(OneClass oneClass)
        {
            oneClass = new OneClass { MyProperty1 = 5 };
        }

        static void OperateOnRef(ref OneClass oneClass)
        {
            oneClass = new OneClass { MyProperty1 = 5 };
        }

        public static class TestConstraint<T> where T : class
        {

        }
    }
}
