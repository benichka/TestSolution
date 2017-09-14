using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Iterate over a collection without using foreach
            List<int> intList = new List<int>() { 1, 3, 5, 0 };
            using (List<int>.Enumerator it = intList.GetEnumerator())
            {
                while (it.MoveNext())
                {
                    Console.WriteLine(it.Current);
                }
            }

            // Iterate over a class that implements IEnumerator
            var ben = new Person() { FirstName = "Benoit", LastName = "Masson-Bedeau" };
            var laurie = new Person() { FirstName = "Laurie", LastName = "Boulard" };
            var jb = new Person() { FirstName = "Jean-Baptiste", LastName = "Bedeau" };
            var people = new Person[] { ben, laurie, jb };

            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }
    }

    internal class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }

    internal class People : IEnumerable<Person>
    {
        private Person[] _people;

        public People(Person[] people)
        {
            this._people = people;
        }

        public IEnumerator<Person> GetEnumerator()
        {
            for (int i = 0; i < _people.Length; i++)
            {
                yield return _people[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _people.GetEnumerator();
        }
    }
}
