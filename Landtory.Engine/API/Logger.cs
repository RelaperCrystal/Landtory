using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GTA;

namespace Landtory.Engine.API
{
    public class Logger
    {
        private static bool Loaded;
        public enum LogLevel
        {
            Info,
            Warning,
            Error,
            Fatal
        }
        public Logger()
        {
            if (Loaded == false)
            {
                if (File.Exists("Landtory.log"))
                {
                    File.WriteAllText("Landtory.log", File.ReadAllText("Landtory.log") + Environment.NewLine + "---------------------------------------"
                        + Environment.NewLine + "Log started: " + DateTime.Now.ToString() + Environment.NewLine);
                    Loaded = true;
                }
                else
                {
                    File.WriteAllText("Landtory.log", "Landtory Mod Log File" + Environment.NewLine + "Log started: " + DateTime.Now.ToString() + Environment.NewLine);
                    Loaded = true;
                }
            }
        }
        /// <summary>
        /// Log text as LogLevel.
        /// </summary>
        /// <param name="text">Text that being logged.</param>
        /// <param name="level">Log Level.</param>
        public void Log(string text, LogLevel level = LogLevel.Info)
        {
            string target;
            switch (level)
            {
                case LogLevel.Info :
                    target = Environment.NewLine + "["+DateTime.Now.ToString()+"] [INFO] "+text;
                    break;
                case LogLevel.Warning :
                    target = Environment.NewLine + "["+DateTime.Now.ToString()+"] [WARN] "+text;
                    break;
                case LogLevel.Error : 
                    target = Environment.NewLine + "*!!!* ["+DateTime.Now.ToString()+"] [ERROR] "+text;
                    break;
                case LogLevel.Fatal :
                    target = Environment.NewLine + "*!!!* ["+DateTime.Now.ToString()+"] [FATAL] "+text;
                    break;
                default :
                    target = Environment.NewLine + "[" + DateTime.Now.ToString() + "] [INFO] " + text;
                    break;
            }
            File.WriteAllText("Landtory.log", File.ReadAllText("Landtory.log") + target);
        }
        /// <summary>
        /// Log text as LogLevel, with submitted sender.
        /// </summary>
        /// <param name="text">Text that being logged.</param>
        /// <param name="sender">Sender that being submitted and logged.</param>
        /// <param name="level">Log Level.</param>
        public void Log(string text, string sender, LogLevel level = LogLevel.Info)
        {
            string target;
            switch (level)
            {
                case LogLevel.Info:
                    target = Environment.NewLine + "[" + DateTime.Now.ToString() + "] [" + sender +"] [INFO] " + text;
                    break;
                case LogLevel.Warning:
                    target = Environment.NewLine + "[" + DateTime.Now.ToString() + "] [" + sender + "] [WARN] " + text;
                    break;
                case LogLevel.Error:
                    target = Environment.NewLine + "*!!!* [" + DateTime.Now.ToString() + "] [" + sender + "] [ERROR] " + text;
                    break;
                case LogLevel.Fatal:
                    target = Environment.NewLine + "*!!!* [" + DateTime.Now.ToString() + "] [" + sender + "] [FATAL] " + text;
                    break;
                default:
                    target = Environment.NewLine + "[" + DateTime.Now.ToString() + "] [" + sender + "] [INFO] " + text;
                    break;
            }
            File.WriteAllText("Landtory.log", File.ReadAllText("Landtory.log") + target);
        }
    }
}
