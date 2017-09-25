using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAndCo
{
    class Program
    {
        public static void Main()
        {
            #region ContinueWith – MSDN
            Console.WriteLine("--------------------- ContinueWith – MSDN ---------------------");

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
            Console.WriteLine("--------------------- ContinueWith – with parameters ---------------------");

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
                Console.WriteLine($"Completed with result {i.Result}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            completedTask.Wait();
            #endregion ContinueWith -> with parameters

            #region Cancellation token – without exception
            // Using this pattern, the task will have a "RunToCompletion" status
            Console.WriteLine("--------------------- Cancellation token – without exception ---------------------");

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var taskWithToken1 = Task.Run(async () =>
            {
                var i = 0;
                while (!token.IsCancellationRequested)
                {
                    i++;
                    if (i != 1)
                    {
                        Console.Write(", ");
                    }
                    Console.Write(i);
                    await Task.Delay(1000);
                }
            }, token);

            Console.WriteLine("Press Enter to stop counting");
            Console.ReadLine();
            tokenSource.Cancel();
            #endregion Cancellation token – without exception

            #region Cancellation token – with exception
            // Using this pattern, the task will have a "Cancel" status
            Console.WriteLine("--------------------- Cancellation token – with exception ---------------------");

            var tokenSource2 = new CancellationTokenSource();
            var token2 = tokenSource2.Token;

            var taskWithToken2 = Task.Run(async () =>
            {
                var i = 0;
                while (!token2.IsCancellationRequested)
                {
                    i++;
                    if (i != 1)
                    {
                        Console.Write(", ");
                    }
                    Console.Write(i);
                    await Task.Delay(1000);
                }

                token2.ThrowIfCancellationRequested();
            }, token2);

            try
            {
                Console.WriteLine("Press Enter to stop counting");
                Console.ReadLine();
                tokenSource2.Cancel();

                taskWithToken2.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0].Message);
            }

            Console.WriteLine("Press Enter to continue");
            #endregion Cancellation token – with exception

            #region Cancellation token – with continuation
            Console.WriteLine("--------------------- Cancellation token – with continuation ---------------------");
            // Using this pattern, there is no need to "manually handle" the exception

            var tokenSource3 = new CancellationTokenSource();
            var token3 = tokenSource3.Token;

            var taskWithToken3 = Task.Run(async () =>
            {
                var i = 0;
                while (!token3.IsCancellationRequested)
                {
                    i++;
                    if (i != 1)
                    {
                        Console.Write(", ");
                    }
                    Console.Write(i);

                    await Task.Delay(1000);
                }

                token3.ThrowIfCancellationRequested();
            }, token3);

            var continuationTaskWithToken3 = taskWithToken3.ContinueWith((i) =>
            {
                // DON'T DO THAT: when the cancellation is "successfull", the task's Exception property returns null.
                // see https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation
                //i.Exception.Handle((e) => true);

                // Inform the user
                Console.WriteLine($"The task was canceled. Status: {i.Status}");
            }, TaskContinuationOptions.OnlyOnCanceled);

            Console.WriteLine("Press Enter to stop counting");
            Console.ReadLine();
            tokenSource3.Cancel();

            continuationTaskWithToken3.Wait();
            #endregion Cancellation token – with continuation

            #region book – listing 1-44
            Console.WriteLine("--------------------- book – listing 1-44 ---------------------");
            var tokenSource4 = new CancellationTokenSource();
            var token4 = tokenSource4.Token;

            // The continuation task is not retrieved, so we can't wait for it
            Task bookTask = Task.Run(() =>
            {
                while (!token4.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
                token4.ThrowIfCancellationRequested();
            }, token4).ContinueWith((bt) =>
            {
                // Again, don't do that
                // see the same link as above: https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-cancellation
                //bt.Exception.Handle((be) => true);
                Console.WriteLine("You have canceled the task");
            }, TaskContinuationOptions.OnlyOnCanceled);

            Console.WriteLine("Press Enter to stop counting");
            Console.ReadLine();
            tokenSource4.Cancel();

            // As the continuation task is not retrieve, it's the only way to keep the console opened
            Console.WriteLine("Press Enter to exit application");
            Console.ReadLine();
            #endregion book – listing 1-44

            #region book – listing 4-26 : parallel tasks
            Console.WriteLine("Long task will start after your pressed a key.");
            Console.ReadKey();
            Console.WriteLine("Starting long task...");
            // We can't await a task in main, so we just wait for it to complete...
            ExecuteMultipleRequestsInParallel().Wait();
            Console.WriteLine("Long task finished.");
            Console.ReadKey();
            #endregion book – listing 4-26 : parallel tasks
        }

        #region book – listing 4-26 : parallel tasks
        /// <summary>
        /// This method will parallely launch a list of tasks and wait for them to finish.
        /// </summary>
        /// <returns>The task.</returns>
        public static async Task ExecuteMultipleRequestsInParallel()
        {
            HttpClient client = new HttpClient();

            Task<string> microsoft = client.GetStringAsync("http://www.microsoft.com");
            Task<string> msdn = client.GetStringAsync("http://msdn.microsoft.com");
            Task<string> blogs = client.GetStringAsync("http://blogs.msdn.com");

            await Task.WhenAll(microsoft, msdn, blogs);
        }
        #endregion book – listing 4-26 : parallel tasks
    }
}
