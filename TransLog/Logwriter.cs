using System;
using System.IO;

namespace TransLog
{
    public class Logwriter
    {
        public static string logfile { get; set; }

        public static string Store_Name { get; set; }


        public static string Receipt_reference { get; set; }




        public static void writelog(string text_to_write)
        {

            logfile = "AT Utility" + "-" + Store_Name + "-" + Receipt_reference + "-" + DateTime.Now.ToString("ddMMyyyy") + ".log";
            using (StreamWriter LogWriter = new StreamWriter(logfile, true))
            {
                LogWriter.WriteLine(text_to_write);// +" "+"TimeStamp="+ DateTime.Now.ToString("HH:mm:ss")


            }
        }
    }
}
