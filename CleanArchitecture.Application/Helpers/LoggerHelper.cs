using Serilog;
using System.Diagnostics;


namespace CleanArchitecture.Application.Helpers
{
    public static class LoggerHelper
    {
        public static void LogInformation(string message, params object[] args)
        {
            // Log to Serilog
            Log.Information(message, args);

            // Log to OpenTelemetry
            /*var activity = new Activity("LogInformation");
            activity.Start();
            activity.SetTag("Message", string.Format(message, args));
            activity.Stop();*/
        }

        public static void LogWarning(string message, params object[] args)
        {
            // Log to Serilog
            Log.Warning(message, args);

            // Log to OpenTelemetry
            /*var activity = new Activity("LogWarning");
            activity.Start();
            activity.SetTag("Message", string.Format(message, args));
            activity.Stop();*/
        }

        public static void LogError(string message, Exception ex, params object[] args)
        {
            // Log to Serilog
            Log.Error(ex, message, args);

            // Log to OpenTelemetry
            /*var activity = new Activity("LogError");
            activity.Start();
            activity.SetTag("Message", string.Format(message, args));
            activity.SetTag("Exception", ex.ToString());
            activity.SetStatus(ActivityStatusCode.Error, ex.Message);
            activity.Stop();*/
        }
    }
}
