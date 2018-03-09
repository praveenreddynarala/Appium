// -----------------------------------------------------------------------
// <copyright file="Log.cs" company="Ladbrokes Pvt Ltd">
// Copyright (c) Ladbrokes. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AutonitroLogger
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Log class
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Logger tool
        /// </summary>
        private static AutonitroLogger.AutonitroLog loggerTool;

        /// <summary>
        /// Gets Logger Tool
        /// </summary>
        private static AutonitroLogger.AutonitroLog LoggerTool
        {
            get
            {
                if (loggerTool == null)
                {
                    //// We're passing in an empty string since we only need to log to the trx file
                    var currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
                    string filePath = currentDirPath.FullName + "\\LogReportFolder";
                    System.Environment.SetEnvironmentVariable("ReportFilePath", filePath);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    filePath = filePath + "\\FileSample.txt";

                    // We're passing in an empty string since we only need to log to the trx file
                    loggerTool = new AutonitroLog(filePath, true, true);


                    //loggerTool = new Logger.AutoNitroLogger.AutonitroLogger(string.Empty, true, true);
                }

                return loggerTool;
            }
        }

        /// <summary>
        /// Add a success message (console color: green)
        /// </summary>
        /// <param name="msg">Takes text with format {0}{1}...{n}</param>
        /// <param name="args">argument values</param>
        public static void Success(string msg, params object[] args)
        {
            msg = " Success: " + msg;
            LoggerTool.TraceSuccess(msg, args);
        }

        /// <summary>
        /// Add an error message (console color: red)
        /// </summary>
        /// <param name="msg">Takes text with format {0}{1}...{n}</param>
        /// <param name="args">argument values</param>
        public static void Error(string msg, params object[] args)
        {
            msg = " Error: " + msg;
            LoggerTool.TraceError(msg, args);
        }

        /// <summary>
        /// Add an error message (console color: red)
        /// </summary>
        /// <param name="exception"> Exception object is formatted to a string</param>
        public static void Error(Exception exception)
        {
            Error("{0}\r\n{1}\r\n{2}", exception.Message, exception.TargetSite, exception.StackTrace);
        }

        /// <summary>
        /// Prints message in black text. This is ideal for debugging and presenting important information to the
        /// user
        /// </summary>
        /// <param name="msg">message value</param>
        /// <param name="args">argument values</param>
        public static void Info(string msg, params object[] args)
        {
            msg = " Info: " + msg;
            LoggerTool.TraceMessage(msg, args);
        }
    }
}
