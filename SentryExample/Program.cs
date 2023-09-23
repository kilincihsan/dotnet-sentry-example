using Sentry;

namespace SentryExample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // These WinForms options are usually set by default
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Add this so Sentry can catch unhandled exceptions
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            // Configure the options for Sentry
            var sentryOptions = new SentryOptions
            {
                // Use your own URL here
                Dsn = "https://123124234234123.ingest.sentry.io/4505925715689472",

                // When configuring for the first time, to see what the SDK is doing:
                Debug = true,

                // Set traces_sample_rate to 1.0 to capture 100% of transactions for performance monitoring.
                // We recommend adjusting this value in production.
                TracesSampleRate = 1.0,

                // Enable Global Mode since this is a client app
                IsGlobalModeEnabled = true,

                //TODO: any other options you need go here
            };

            // Initialize Sentry and run the main form of the application
            using (SentrySdk.Init(sentryOptions))
            {

                ErrorHandling.Test();
                PerformanceMonitoring.Test();
            }
        }
    }
}