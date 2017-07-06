using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace StringOperation
{
    [StructLayout(LayoutKind.Sequential)]
    class BlittableValue
    {
        int x;
    }

    class Program
    {
        static unsafe void Main(string[] args)
        {
            // "Éclatement" d'un string
            var s = "toto";
            var v = s.ToList();

            var s1 = "toto";

            var myInstanceSC = new StringClass();
            myInstanceSC.stringInClass = "test";

            // On garde l'ancienne valeur car c'est une copie qui est passée
            ChangeString(s1);
            Console.WriteLine("\nChangeString(string value) on a string");
            Console.WriteLine(s1);

            // Le string est recopié tel un value type : on ne change donc pas myInstanceSC.stringInClass
            ChangeString(myInstanceSC.stringInClass);
            Console.WriteLine("\nChangeString(string value) on a string in an object instance");
            Console.WriteLine(myInstanceSC.stringInClass);

            // Interdit : pas possible de passer en référence un paramètre car c'est en fait une méthode
            //Console.WriteLine("\nChangeString(ref string value) on a property");
            //ChangeString(ref myInstanceSC.stringInClass);

            // Autorisé mais dégueulasse (membre public...)
            Console.WriteLine("\nChangeString(ref string value) on a public variable");
            ChangeString(ref myInstanceSC.StringInClassWOAccessor);
            Console.WriteLine(myInstanceSC.StringInClassWOAccessor);

            // Le string est changé depuis l'objet passé : myInstanceSC.stringInClass sera donc changé
            ChangeString(myInstanceSC);
            Console.WriteLine("\nChangeString(OneClass myOneClassObj)");
            Console.WriteLine(myInstanceSC.stringInClass);

            #region mutable/immutable
            // Incrémentation d'une variable : le contenu est remplacé à chaque fois, l'adresse reste la même
            //ImmutableInt();

            // Concaténation d'un string : une nouvelle instance est instanciée à chaque concaténation
            ImmutableString();

            // Plus simple : vérification si les ID coïncident :
            var string1 = "Jerry";
            ObjectIDGenerator generator = new ObjectIDGenerator();
            bool string1FirstTime;
            long string1Id = generator.GetId(string1, out string1FirstTime);


            bool string2FirstTime;
            string1 += " Seinfeld";
            long string2Id = generator.GetId(string1, out string2FirstTime);

            var same = string1Id == string2Id;
            Console.WriteLine(same);
            #endregion mutable/immutable

            // Test d'impression d'une adresse d'un type value
            int i = 2;
            object ov = new BlittableValue();
            int* ptr = &i;
            IntPtr addr = (IntPtr)ptr;

            Console.WriteLine(addr.ToString("x"));

            GCHandle h = GCHandle.Alloc(ov, GCHandleType.Pinned);
            addr = h.AddrOfPinnedObject();
            Console.WriteLine(addr.ToString("x"));
            h.Free();

            i++;
            ptr = &i;
            addr = (IntPtr)ptr;
            Console.WriteLine(addr.ToString("x"));
            h = GCHandle.Alloc(ov, GCHandleType.Pinned);
            addr = h.AddrOfPinnedObject();
            Console.WriteLine(addr.ToString("x"));
            h.Free();

            // Copie d'un string vers un autre (reference type)
            string1 = "toto";
            var stringCopy = string1;
            // true
            Console.WriteLine("both are the same reference: {0}", Object.ReferenceEquals(string1, stringCopy));
            // true
            Console.WriteLine("both are the same value: {0}", string1.Equals(stringCopy));

            string1 += "a";
            string1 = string1.Substring(0, string1.Length - 1);
            // false
            Console.WriteLine("both are the same reference: {0}", Object.ReferenceEquals(string1, stringCopy));
            // true
            Console.WriteLine("both are the same value: {0}", string1.Equals(stringCopy));

            // Copie d'un int vers un autre (value type)
            int myInt = 5;
            int* p1 = &myInt;

            var myInt2 = myInt;
            int* p2 = &myInt2;

            Console.WriteLine($"pointer to myInt: {(IntPtr)(p1)}; pointer to myInt2: {(IntPtr)(p2)}");
            // false
            Console.WriteLine("both are the same reference: {0}", Object.ReferenceEquals(myInt, myInt2));
            // true
            Console.WriteLine("both are the same value: {0}", myInt == myInt2);

            #region test de pointer
            // Test pointer sur un int
            int age = 32;
            int* age_ptr;
            age_ptr = &age;
            // L'âge
            Console.WriteLine("age = {0}", age);
            // La valeur du pointer
            Console.WriteLine("age_ptr = {0}", (IntPtr)age_ptr);
            // La valeur sur laquelle pointe le pointer = l'âge
            Console.WriteLine("*age_ptr = {0}", *age_ptr);

            // On augmente la valeur du pointer : on pointe donc vers n'importe quoi
            //age_ptr += 3;
            //Console.WriteLine("age_ptr = {0}", (IntPtr)age_ptr);
            //Console.WriteLine("*age_ptr = {0}", *age_ptr);

            // On augmente la valeur sur laquelle pointe le pointer -> l'âge
            *age_ptr += 3;
            Console.WriteLine("age_ptr = {0}", (IntPtr)age_ptr);
            Console.WriteLine("*age_ptr = {0}", *age_ptr);
            #endregion test de pointer

            #region switch sur un string.Empty
            /*
            var s1 = "un string";
            var s2 = string.Empty;
            
            switch (s1 ?? string.Empty)
            {
                case "toto":
                    break;
                // Impossible : le test ne peut pas se faire sur un champ readonly
                //case string.Empty:
                //    break;
                case "":
                    break;
                default:
                    break;
            }
            */
            #endregion switch sur un string.Empty

            #region utilisation de paramètres optionnels
            /*
            stringClass.MethodWithOptionalArgs("toto");
            */
            #endregion utilisation de paramètres optionnels
        }

        /// <summary>
        /// Changement d'un string passé en paramètre
        /// </summary>
        /// <param name="value">Le string passé en paramètre</param>
        static void ChangeString(string value)
        {
            value += " – changed";
        }

        /// <summary>
        /// Changement par référence d'un string passé en paramètre
        /// </summary>
        /// <param name="value">Le string passé en paramètre</param>
        static void ChangeString(ref string value)
        {
            value += " – changed";
        }

        /// <summary>
        /// Changement d'un string dans un objet StringClass
        /// </summary>
        /// <param name="myOneClassObj">Objet StringClass pour lequel changer le string "stringInClass"</param>
        static void ChangeString(StringClass myOneClassObj)
        {
            myOneClassObj.stringInClass += " – changed";
        }

        /// <summary>
        /// Explication sur le fait qu'un value type soit immutable
        /// </summary>
        static void ImmutableInt()
        {
            int i = 0;
            for (i = 0; i < 10; i++)
            {
                unsafe
                {
                    var p = &i;
                    Console.WriteLine($"value: {*p} – address: {(int)p}");
                }
            }
        }

        /// <summary>
        /// Explication sur le fait qu'un string soit immutable
        /// </summary>
        static void ImmutableString()
        {
            string s = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                var saved = s;
                Console.WriteLine($"Does s have the same reference that saved? {ReferenceEquals(saved, s)}");
                s += " " + i;
                Console.WriteLine($"What about now? {ReferenceEquals(saved, s)}");
            }
        }
    }
}
