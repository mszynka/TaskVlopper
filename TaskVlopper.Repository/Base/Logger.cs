using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base;
using System.Web;
namespace TaskVlopper.Repository.Base
{
    public static class Logger
    {
        [AttributeUsage(AttributeTargets.Field)]
        public class ReprAttribute : Attribute
        {
            public string Representation;
            public ReprAttribute(string representation)
            {
                this.Representation = representation;
            }
            public override string ToString()
            {
                return this.Representation;
            }
        }


        public enum LoggerSeverityEnum
        {
            [Repr("EXCEPTION")]
            Exception,

            [Repr("WARNING")]
            Warning,

            [Repr("INFO")]
            Info,

            [Repr("LOG")]
            Log
        }

        public static void Log(string message, LoggerSeverityEnum severity = LoggerSeverityEnum.Log)
        {
            try
            {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~\\Logger\\" + DateTime.Now.ToShortDateString().ToString().Replace("/", "_") + ".txt"), true))
                {
                    file.WriteLine("[" + severity.ToString() + "] " + DateTime.Now.ToString());
                    file.WriteLine(message + "\n");
                }
            }
            catch (Exception ex)
            {
                if (!Directory.Exists(@"C:\Temp\Logger"))
                {
                    Directory.CreateDirectory(@"C:\Temp\Logger");
                }

                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(string.Join("", "C:\\Temp\\Logger\\", DateTime.Now.ToShortDateString().ToString().Replace("/", "_") + ".txt"), true))
                {
                    file.WriteLine("[" + severity.ToString() + "] " + DateTime.Now.ToString());
                    file.WriteLine(message + "\n");
                    file.WriteLine("[EXCEPTION] While handling exception another occured!");
                    file.WriteLine(ex.Message.ToString() + "\n");

                }
            }
        }

        public static void LogException(string message)
        {
            Logger.Log(message, LoggerSeverityEnum.Exception);
        }

        public static void LogWarning(string message)
        {
            Logger.Log(message, LoggerSeverityEnum.Warning);
        }

        public static void LogInfo(string message)
        {
            Logger.Log(message, LoggerSeverityEnum.Info);
        }
    }
}
