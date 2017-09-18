using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceCounters
{
    class Program
    {
        static void Main(string[] args)
        {
            if (CreatePerformanceCounters())
            {
                Console.WriteLine("Created performance counter");
                Console.WriteLine("Please restart your application");
                Console.ReadKey();
                return;
            }

            // Attach a performance counter the the category of an existing counter
            var totalOperationCounter = new PerformanceCounter("MyCategory", "# operations executed", "", false);
            var operationPerSecondCounter = new PerformanceCounter("MyCategory", "# operations/second", "", false);

            totalOperationCounter.Increment();
            operationPerSecondCounter.Increment();
        }

        private static bool CreatePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists("MyCategory3"))
            {
                // First, create the collection
                CounterCreationDataCollection counters = new CounterCreationDataCollection()
                {
                    new CounterCreationData("# operations executed", "Total number of operations executed", PerformanceCounterType.NumberOfItems32),
                    new CounterCreationData("# operations/second", "Number of operations executed per second", PerformanceCounterType.RateOfCountsPerSecond32)
                };

                // Then, create the categories with the counters created just before
                PerformanceCounterCategory.Create("MyCategory", "Sample category for the certification", PerformanceCounterCategoryType.SingleInstance, counters);

                return true;
            }

            return false;
        }
    }
}
