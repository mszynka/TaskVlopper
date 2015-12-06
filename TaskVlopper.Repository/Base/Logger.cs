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
        public static void LogException(string message)
        {
            //if (!Directory.Exists(@"C:\TaskVlopper"))
            //{
            //    Directory.CreateDirectory(@"C:\TaskVlopper");
            //}
            
            using (System.IO.StreamWriter file = 
                new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~\\Logger_" + DateTime.Now.ToShortDateString() + ".txt"), true))
            {
                file.WriteLine("[EXCEPTION] " +DateTime.Now.ToString());
                file.WriteLine(message + "\n");
            }
        }
    }
}
