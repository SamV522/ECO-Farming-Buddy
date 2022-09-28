using System;
using System.IO;

namespace ECO_Farming_Buddy.Helpers
{
    internal static class LogHelper
    {
        public static string LogFileName = "Log.txt";

        public static void Log(string Message)
        {
            Message = $"{DateTime.UtcNow}: {Message}{Environment.NewLine}";
            File.AppendAllText($"{AppDomain.CurrentDomain.BaseDirectory}{LogFileName}", Message);
        }

        public static void Log(Exception e)
        {
            Log(e.ToString());
        }
    }
}
