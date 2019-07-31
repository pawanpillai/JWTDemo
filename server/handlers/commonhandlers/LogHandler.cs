/* ==============================
 * AUTHOR       :   Pawan Pillai
 * CREATE DATE  :   March 2019
 * PURPOSE      :   Log Handler class is used to log and read errors and general application logs. 
 *                  Currently this class logs items in file system, later will be updated to log in database.
 * SPECIAL NOTES:   None
 * ==================================
 */
using System;
using System.IO;

namespace JWTDemo
{
    public class LogHandler
    {
        public LogHandler()
        {
        }

        public static void LogError(string entity, string error) { 
            //do nothing right now... will add later
        }

        public static void WriteToLogFile(string logMessage)
        {
            var logFile = Path.Combine(Directory.GetCurrentDirectory(), "logs", "log" + DateTime.Now.ToShortDateString().Replace("/","") + ".txt");

            using (StreamWriter streamWriter = File.AppendText(logFile))
            {
                streamWriter.WriteLine("-------------------------------");
                streamWriter.WriteLine("{0} {1}:", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                streamWriter.WriteLine("{0}", logMessage);
                streamWriter.WriteLine("-------------------------------");
            }
        }
    }
}
