using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Collections
{
    /// <summary>
    /// A simple class that shows example of different collections usage
    /// </summary>
    class Program
    {
        // Go to https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/collections
        // for detailed information
        // Or to https://www.tutorialspoint.com/csharp/csharp_collections.htm

        static void Main(string[] args)
        {
            #region List
            // A list use a explicit type; in this example, it's "string"
            var myList = new List<string>();
            myList.Add("toto");
            myList.Add("tata");
            myList.Add("titi");

            var query1 = myList[0]; // -> "toto"
            var query2 = myList.IndexOf("toto"); // -> 0 

            // With List (and ONLY list), we can check if a value is present by using the Exists methods:
            var isPresent1 = myList.Exists(item => item.Equals("titi"));

            // LINQ allows us to do that with Any
            var isPresent2 = myList.Any(item => item.Equals("titi"));

            // ------ Use of take and skip ------
            var employees = new List<string>();
            for (int i = 1; i <= 20; i++)
            {
                employees.Add($"Employee {i}");
            }

            // Number of employees that we want to take in one page
            var takeSize = 5;

            // Returns the sub list of employee that we want to display for the page numPage
            // The pages start at one
            List<string> GetPageEmployee(int numPage)
            {
                if (numPage <= 0)
                {
                    throw new ArgumentException("numPage must be > 0", "numPage");
                }

                // First, we skip to the correct index. For page 1, the index is 0
                return employees.Skip((numPage - 1) * takeSize).Take(takeSize).ToList();
            }

            // Get the first five employees
            var employeesPage1 = GetPageEmployee(1);

            // Get the employees from 11 to 15 -> page 3 starts at (3 * 5) + 1
            var employeesPage3 = GetPageEmployee(3);
            #endregion List

            #region ArrayList
            // -> an array list is a list of object
            var myArrayList = new ArrayList();
            myArrayList.Add("toto");
            myArrayList.Add("tata");
            myArrayList.Add("titi");

            var query3 = myArrayList[0]; // -> "toto"
            var query4 = myArrayList.IndexOf("toto"); // -> 0 

            // The array list only has "contains"
            var isPresent3 = myArrayList.Contains("titi");

            // It's not possible to directly use LINQ on a ArrayList...
            var isPresent4 = (from string s in myArrayList
                              where s.Equals("titi")
                              select s).ToList().Any();
            #endregion ArrayList

            #region Hashtable
            // The Hashtable class represents a collection of key-and-value pairs that are organized
            // based on the hash code of the key. It uses the key to access the elements in the collection.
            var myHashtable = new Hashtable();
            myHashtable.Add("toto", "this is toto");
            myHashtable.Add("tata", "this is tata");
            myHashtable.Add("titi", "this is titi");

            // Can't do that: the hashtable is not accessed by an index
            //var query5 = myHashtable[0];

            var query6 = myHashtable["toto"];
            foreach (var item in myHashtable)
            {
                // The result are organized based on the hash code of the key
                Console.WriteLine(((DictionaryEntry)item).Value);
            }
            #endregion Hashtable

            #region SortedList
            // A sorted list represents a collection of key-and-value pairs that
            // are sorted by the keys and are accessible by key and by index
            // => same as hashtable, but also accessible by index!
            var mySortedList = new SortedList();
            mySortedList.Add("toto", "this is toto");
            mySortedList.Add("tata", "this is tata");
            mySortedList.Add("titi", "this is titi");

            // Can't do that: the index must be of type string because the keys are strings
            //var query7 = sortedList[0];

            var query8 = mySortedList.GetByIndex(0); // "this is tata" -> it's sorted!
            var query9 = mySortedList["toto"]; // "this is toto" 
            #endregion SortedList

            // TODO: other collection types
        }
    }
}
