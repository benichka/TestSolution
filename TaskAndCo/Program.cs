using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAndCo
{
    class Program
    {
        public static void Main()
        {
            #region ContinueWith – MSDN
            // See https://msdn.microsoft.com/en-us/library/dd270696(v=vs.110).aspx

            //The following example defines a task that populates an array with
            //1000000 random date and time values.It uses the ContinueWith(Action<Task>) method
            //to select the earliest and the latest date values once the array is fully populated. 

            // The example displays output like the following:
            //       Earliest date: 2/11/0110 12:03:41 PM
            //       Latest date: 7/29/9989 2:14:49 PM

            var firstTask = Task.Factory.StartNew(() =>
            {
                Random rnd = new Random();
                DateTime[] dates = new DateTime[1000000];
                Byte[] buffer = new Byte[8];
                int ctr = dates.GetLowerBound(0);
                while (ctr <= dates.GetUpperBound(0))
                {
                    rnd.NextBytes(buffer);
                    long ticks = BitConverter.ToInt64(buffer, 0);
                    if (ticks <= DateTime.MinValue.Ticks | ticks >= DateTime.MaxValue.Ticks)
                        continue;

                    dates[ctr] = new DateTime(ticks);
                    ctr++;
                }
                return dates;
            });

            Task continuationTask = firstTask.ContinueWith((antecedent) =>
            {
                DateTime[] dates = antecedent.Result;
                DateTime earliest = dates[0];
                DateTime latest = earliest;

                for (int ctr = dates.GetLowerBound(0) + 1; ctr <= dates.GetUpperBound(0); ctr++)
                {
                    if (dates[ctr] < earliest) earliest = dates[ctr];
                    if (dates[ctr] > latest) latest = dates[ctr];
                }
                Console.WriteLine("Earliest date: {0}", earliest);
                Console.WriteLine("Latest date: {0}", latest);
            });
            // Since a console application otherwise terminates, wait for the continuation to complete.
            continuationTask.Wait();
            #endregion ContinueWith – MSDN

            #region ContinueWith -> with parameters
            Task<int> t = Task.Run(() =>
            {
                return 42;
            });

            // Few ways to continue only if a condition is met
            t.ContinueWith((i) =>
            {
                Console.WriteLine("Canceled");
            }, TaskContinuationOptions.OnlyOnCanceled);

            t.ContinueWith((i) =>
            {
                Console.WriteLine("Faulted");
            }, TaskContinuationOptions.OnlyOnFaulted);

            var completedTask = t.ContinueWith((i) =>
            {
                Console.WriteLine("Completed");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            completedTask.Wait();
            #endregion ContinueWith -> with parameters
        }
    }
}
