using Sentry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentryExample
{
    public class ErrorHandling
    {
        public static void Test() { 
            StartErrorHandling();
        }
        private static void StartErrorHandling()
        {
            try
            {
                RetardFunction();
                // you can also use the method below for crash
                //SentrySdk.CauseCrash(CrashType.Managed);
            }
            catch (Exception e)
            {
                NotifySentryException(e);
            }
        }

        private static void NotifySentryException(Exception e)
        {

            SentrySdk.CaptureMessage(e.Message, SentryLevel.Error);
            //SentrySdk.CaptureException(e);
        }

        /// <summary>
        /// terrible retard function with lots of heap and 100% percent fail rate
        /// </summary>
        private static void RetardFunction()
        {
            var numbers = Enumerable.Range(1, 10000001).Select(i => i.ToString()).ToArray();

            Random rnd = new Random();
            for (int i = 0; i < numbers.Length; i++) numbers[i] += rnd.Next(0, 100) == 0 ? "A" : "";

            foreach (var number in numbers)
            {
                if (Convert.ToInt32(number) == 10000000)
                {
                    break;
                }

            }
        }
    }
}
