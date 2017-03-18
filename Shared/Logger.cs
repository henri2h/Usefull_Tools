using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Usefull_Tools
{
    /// <summary>
    /// Logger class
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// App name
        /// </summary>
        public static string AppName
        {
            get { return appName; }
            set
            {
                appName = value;
                Files.setFDir = null;
            }
        }
        static string appName = "app";

        /// <summary>
        /// is loggin enabled
        /// </summary>
        public static bool logEnabled = true;
        /// <summary>
        /// is debug enabled
        /// </summary>
        public static bool debugEnabled = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public static void logMain(string data)
        {

            data = "[" + AppName + " | " + DateTime.Now.ToShortTimeString() + "] : " + data;
            debugLine(data, false);
            if (logEnabled) Files.saveFile("logs", "mainLog", data);
        }


        /// <summary>
        /// Print debug Line, if modify is true, debug will be display at the start of the line
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modify"></param>
        public static void debugLine(string data, bool modify = true)
        {
            if (debugEnabled)
            {
                if (modify)
                {
                    data = "[ Debug | " + DateTime.Now.ToShortTimeString() + "] : " + data;
                }

                // screen
                System.Console.WriteLine(data);
                System.Diagnostics.Debug.WriteLine(data);

                // files
                logFile(data, "log.txt");
            }

        }
        /// <summary>
        /// Log and display an error
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ex"></param>
        public static void logErrors(string data, Exception ex)
        {

            string path = Files.getUniquePath("errors", "error", ".err");
            string jsonString = JsonConvert.SerializeObject(ex);

            string s1 = "New error : " + data;
            string s2 = "Error saved in : " + path + Environment.NewLine;
            string s3 = ErrorHandeler.getErrorString(ex);

            debugLine(s1, true);
            debugLine(s2, true);
            debugLine(s3, false);
            debugLine(jsonString, false);


            if (logEnabled)
            {


                logFile(s1, "errorLog");
                logFile(s2, "errorLog");
                logFile(s3, "errorLog");

                Files.saveFile(path, data);
                Files.saveFile(path, jsonString);
            }

        }

        /// <summary>
        /// Log in a specified file
        /// </summary>
        /// <param name="data"></param>
        /// <param name="app"></param>
        public static void logFile(string data, string app)
        {
            data = "[" + app + " | " + DateTime.Now.ToShortTimeString() + "] : " + data;
            if (logEnabled) { Files.saveFile("logs", app, data); }
        }
    }
}
