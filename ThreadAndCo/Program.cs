using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadAndCo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region examples 1 and 2
            // With a standard field, field is shared -> the final value will be 10 + 10 = 20.
            // If we add the attribute ThreadStatic, each thread gets its own copy of a field
            // -> there will be two sets of 10 numbers

            // Note: both thread are started independently; for such small iterations it may
            // seems that they execute one after another, but that's not the case.

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    standardField++;
                    Console.WriteLine($"Thread A: {standardField}");
                }
            }
            ).Start();

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    standardField++;
                    Console.WriteLine($"Thread B: {standardField}");
                }
            }).Start();

            Console.ReadKey();
            #endregion examples 1 and 2

            #region example 3
            new Thread(() =>
            {
                for (int x = 0; x < threadLocalField.Value; x++)
                {
                    Console.WriteLine($"Thread A: {x}");
                }
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < threadLocalField.Value; x++)
                {
                    Console.WriteLine($"Thread B: {x}");
                }
            }).Start();

            new Thread(() =>
            {
                for (int x = 0; x < threadLocalField.Value; x++)
                {
                    Console.WriteLine($"Thread C: {x}");
                }
            }).Start();

            Console.ReadKey();
            #endregion example 3

            #region example 4
            // Using the thread pool.
            // In the previous example, the thread dies after it is used -> that can be
            // resources consuming.
            // The thread pool reuses threads

            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine("Hello from the tread pool!");
            });

            Console.ReadKey();
            #endregion example 4
        }

        #region examples 1 and 2
        // A field is by default share with all threads that has access to it
        // By marking a field with the ThreadStatic attribute, each thread gets its own copy of a field.
        // Example 1: comment the attribute.
        // Example 2: uncomment it.
        [ThreadStatic]
        public static int standardField;
        #endregion examples 1 and 2

        #region example 3
        // A field that it initialize for each thread that will use it.
        // This field won't be shared in threads
        public static ThreadLocal<int> threadLocalField = new ThreadLocal<int>(() =>
        {
            return Thread.CurrentThread.ManagedThreadId;
        });
        #endregion example 3
    }
}
