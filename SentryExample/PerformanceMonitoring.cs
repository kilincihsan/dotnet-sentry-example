using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentryExample
{
    public class PerformanceMonitoring
    {
        public static void Test()
        {
            StartPerformanceMonitoring();
        }
        private static void StartPerformanceMonitoring()
        {
            // Transaction can be started by providing, at minimum, the name and the operation
            var transaction = SentrySdk.StartTransaction(
              "TEST",
              "TEST-OP"
            );

            // Transactions can have child spans (and those spans can have child spans as well)
            var span = transaction.StartChild("Retard Function");

            RetardFunction();

            span.Finish(); // Mark the span as finished

            var span2 = transaction.StartChild("Smart Function");

            SmartFunction();

            span2.Finish();
            transaction.Finish(); // Mark the transaction as finished and send it to Sentry
        }



        /// <summary>
        /// terrible retard function with lots of heap and 100% percent fail rate
        /// </summary>
        private static void RetardFunction()
        {
            var numbers = Enumerable.Range(1, 100001).Select(i => i.ToString()).ToArray();
            Random rnd = new Random();

            for (int i = 0; i < numbers.Length; i++) numbers[i] += rnd.Next(0, 100) == 0 ? "A" : "";

            foreach (var number in numbers)
            {
                try
                {
                    if (Convert.ToInt32(number) == 100000) Console.WriteLine("100000 is there.");
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        private static void SmartFunction()
        {
            var numbers = Enumerable.Range(1, 100001).Select(i => i.ToString()).ToArray();
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 1; i < numbers.Length; i++)
            {
                sb.Append(i);
                numbers[i]=sb.Append(rnd.Next(0, 100) == 0 ? "A" : "").ToString();
                sb.Clear();
            }

            foreach (var number in numbers)
            {
                if (int.TryParse(number, out int parsedNumber))
                {
                    if (parsedNumber == 100000) Console.WriteLine("100000 is there.");
                }
            }
        }

    }

}
