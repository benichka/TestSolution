using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // List
            // A list use a explicit type; in this example, it's "string"
            var myList = new List<string>();
            myList.Add("toto");
            myList.Add("tata");
            myList.Add("titi");

            var query1 = myList[0]; // -> "toto"
            var query2 = myList.IndexOf("toto"); // -> 0


            // ArrayList
            // -> an array list is a list of object
            var myArrayList = new ArrayList();
            myArrayList.Add("toto");
            myArrayList.Add("tata");
            myArrayList.Add("titi");

            var query3 = myArrayList[0]; // -> "toto"
            var query4 = myArrayList.IndexOf("toto"); // -> 0

            // Hashtable
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

            // SortedList
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

            // TODO: other collection types
        }
    }
}
